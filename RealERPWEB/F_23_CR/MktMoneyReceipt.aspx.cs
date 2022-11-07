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
using System.Data.OleDb;
using RealERPLIB;
using RealERPRPT;
using Microsoft.Reporting.WinForms;
using RealERPRDLC;
namespace RealERPWEB.F_23_CR
{
    public partial class MktMoneyReceipt : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {



            this.lbtnUpdate.Attributes.Add("onClick", " javascript:return confirm('You sure you want to Save the record?');");
            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ////this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                Session.Remove("Unit");
                this.tableintosession();
                if (this.Request.QueryString["Type"] == "CustCare")
                {
                    this.chkPrevious.Visible = false;
                    this.chkConsolidate.Checked = true;
                }
                this.InforInitialize();
                this.comHideLabel();
                this.GetProjectName();
                this.GetInsType();
                string qPrjCode = this.Request.QueryString["prjcode"] ?? "";
                if (qPrjCode.Length > 0)
                {
                    this.lbtnOk_Click(null, null);
                }
                // this.GetCollectTeam();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Collection Sales";
                this.txtSrcPro.Focus();

                this.txtReceiveDate_CalendarExtender.EndDate = System.DateTime.Today;





            }



            if (fileuploadExcel.HasFile)
            {
                try
                {
                    Session.Remove("XcelData");
                    //  ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                    string connString = "";
                    string StrFileName = string.Empty;
                    if (fileuploadExcel.PostedFile != null && fileuploadExcel.PostedFile.FileName != "")
                    {
                        StrFileName =
                            fileuploadExcel.PostedFile.FileName.Substring(
                                fileuploadExcel.PostedFile.FileName.LastIndexOf("\\") + 1);
                        string StrFileType = fileuploadExcel.PostedFile.ContentType;
                        int IntFileSize = fileuploadExcel.PostedFile.ContentLength;
                        if (IntFileSize <= 0)
                        {
                            //  ((Label)this.Master.FindControl("lblmsg")).Text = "Uploading Fail";
                            // ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert(' file Uploading failed');", true);
                            return;
                        }
                        else
                        {
                            string savelocation = Server.MapPath("~") + "\\ExcelFile\\";
                            string[] filePaths = Directory.GetFiles(savelocation);
                            foreach (string filePath in filePaths)
                                File.Delete(filePath);
                            fileuploadExcel.PostedFile.SaveAs(Server.MapPath("~") + "\\ExcelFile\\" + StrFileName);
                            //   ((Label)this.Master.FindControl("lblmsg")).Text = "Uploading Successfully";
                        }
                    }

                    string strFileType = Path.GetExtension(fileuploadExcel.FileName).ToLower();
                    string apppath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath.ToString();
                    //string path = apppath + "ExcelFile\\" + StrFileName;
                    string path = Server.MapPath("~") + ("\\ExcelFile\\" + StrFileName);

                    //Connection String to Excel Workbook
                    if (strFileType.Trim() == ".xls")
                    {
                        connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path +
                                     ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    }
                    else if (strFileType.Trim() == ".xlsx")
                    {
                        connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path +
                                     ";Extended Properties='Excel 12.0 Xml;HDR=YES;'";
                    }

                    //string query = "SELECT [Product],[Category],[Qty(Pcs)],[Value],[Unit Price],[ERP CODE] FROM [Sheet1$]";
                    string query =
                        "SELECT * FROM [Sheet1$]";
                    OleDbConnection conn = new OleDbConnection(connString);
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    OleDbCommand cmd = new OleDbCommand(query, conn);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    Session["XcelData"] = ds.Tables[0];
                    // this.DataInsert();
                    da.Dispose();
                    conn.Close();
                    conn.Dispose();
                    //this.GetExelData();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        private void InforInitialize()
        {

            string comcod = this.GetComCode();

            switch (comcod)
            {
                case "2305": //rupayan
                case "3305":
                case "3306":
                case "3309":
                case "3310":
                case "3311":

                // case "3101":
                case "3333":
                case "3339":
                    this.ddlRecType.Visible = true;
                    this.lblRecType.Visible = true;
                    break;



                default:
                    this.ddlRecType.Visible = false;
                    this.lblRecType.Visible = false;
                    break;

            }

        }

        private void comHideLabel()
        {
            string comcod = this.GetComCode();
            switch (comcod)
            {
                //case "3101":
                case "3368":
                    this.lblpaydate.Text = "Cheque Date";

                    break;
                default:
                    this.lblpaydate.Text = "Pay Date";
                    break;
            }
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

        private string CompanyPrintMR()
        {

            string comcod = this.GetComCode();
            string mrprint = "";
            switch (comcod)
            {
                case "1301":
                case "3301":
                    mrprint = "MRPrint1";
                    break;

                //case "3101":
                case "2325":
                case "3325":
                    mrprint = "MRPrint2";
                    break;



                case "3335":
                    //case "3101":
                    mrprint = "MRPrint3";
                    break;

                case "3337":
                case "3336":
                    // case "3101":
                    mrprint = "MRPrint4";
                    break;

                case "3339":
                    //case "3101":
                    mrprint = "MRPrint5";
                    break;

                case "3356":
                case "3101":
                    mrprint = "MRPrintIntech";
                    break;

                default:
                    mrprint = "MRPrint";
                    break;
            }
            return mrprint;
        }




        private void GetProjectName()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();
            string ddldesc = hst["ddldesc"].ToString();
            string qPrjCode = this.Request.QueryString["prjcode"] ?? "";
            string txtSProject = qPrjCode.Length > 0 ? qPrjCode : "%" + this.txtSrcPro.Text + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETPROJECTNAME", txtSProject, userid, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            string TextField = (ddldesc == "True" ? "actdesc" : "actdesc1");
            this.ddlProjectName.DataTextField = TextField;
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();

        }

        private string GetCompanyIns()
        {
            string comcod = this.GetComCode();
            string combdpmay = "";
            switch (comcod)
            {

                case "3336":
                case "3337":
                    combdpmay = "bdappay";
                    break;

                default:
                    break;

            }

            return combdpmay;


        }
        private void GetInsType()
        {

            ViewState.Remove("tblinttype");
            string comcod = this.GetComCode();

            string combdpmay = this.GetCompanyIns();

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETCOLLINSTYPE", combdpmay, "", "", "", "", "", "", "", "");
            this.ddlType.DataTextField = "gdesc";
            this.ddlType.DataValueField = "gcod";
            this.ddlType.DataSource = ds1.Tables[0];
            this.ddlType.DataBind();
            ds1.Dispose();
            this.ddlType.SelectedValue = "100000000";
            ViewState["tblinttype"] = ds1.Tables[0];
            ds1.Dispose();



        }
        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
                this.GetProjectName();
        }
        protected void lbtnsrchunit_Click(object sender, EventArgs e)
        {
            this.LoadGrid();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            if (this.lbtnOk.Text == "Ok")
            {

                this.lbtnOk.Text = "New";
                this.lblProjectdesc.Text = this.ddlProjectName.SelectedItem.Text;
                this.ddlProjectName.Visible = false;
                this.lblProjectdesc.Visible = true;
                this.txtReceiveDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtpaydate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.LoadGrid();


            }
            else
            {
                this.lbtnOk.Text = "Ok";

                this.ddlProjectName.Visible = true;
                this.lblProjectdesc.Text = "";
                this.lblProjectdesc.Visible = false;
                this.btnexcuplosd.Enabled = true;
                this.gvSpayment.DataSource = null;
                this.gvSpayment.DataBind();
                // this.ClearScreen();
                MultiView1.ActiveViewIndex = -1;
                this.gvSpayment.Visible = true;

                this.Clearmrscreen();




            }
        }

        private void ClearScreen()
        {
            this.ddlProjectName.Visible = true;
            this.lblProjectdesc.Text = "";
            this.lblProjectdesc.Visible = false;
            this.gvSpayment.DataSource = null;
            this.gvSpayment.DataBind();
            this.ddlPreMrr.Items.Clear();
        }




        private void LoadGrid()
        {
            ViewState.Remove("tblData");

            string comcod = this.GetComCode();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string qusirCode = this.Request.QueryString["usircode"] ?? "";
            string srchunit = qusirCode.Length > 0 ? qusirCode : "%" + this.txtsrchunit.Text.Trim() + "%";
            string isLO = "0";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "DETAILSUSERINFINFO", PactCode, srchunit, isLO, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.gvSpayment.DataSource = ds1.Tables[0];
            this.gvSpayment.DataBind();
            ViewState["tblData"] = ds1.Tables[0];

            for (int i = 0; i < gvSpayment.Rows.Count; i++)
            {
                string usircode = ((Label)gvSpayment.Rows[i].FindControl("lblgvItmCod")).Text.Trim();
                LinkButton lbtn1 = (LinkButton)gvSpayment.Rows[i].FindControl("lbtnusize");
                if (lbtn1 != null)
                    if (lbtn1.Text.Trim().Length > 0)
                        lbtn1.CommandArgument = usircode;
                ViewState["usricode"] = usircode;
            }
            this.rbtnList1.SelectedIndex = 0;

        }


        protected void lbtnusize_Click(object sender, EventArgs e)
        {

            try
            {
                this.MultiView1.ActiveViewIndex = 0;

                string usircode = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();


                DataTable dtOrder = (DataTable)ViewState["tblData"];
                DataView dv1 = dtOrder.DefaultView;
                dv1.RowFilter = "usircode like('" + usircode + "')";
                dtOrder = dv1.ToTable();
                this.gvSpayment.DataSource = dtOrder;
                this.gvSpayment.DataBind();
                this.lblCode.Text = usircode;
                this.lblPhone.Text = dv1.ToTable().Rows[0]["custphn"].ToString();

                this.txtPaidamt.Focus();


                this.PayInf();
                this.GetCurMrNo();
                this.PayType();
                this.PrintDupOrOrginal();
                this.BankName();

            }

            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }

        }

        private void BankName()
        {
            string comcod = this.GetComCode();
            DataSet ds4 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETBANKNAME", "", "", "", "", "", "", "", "", "");
            this.ddlbank.DataTextField = "gdesc";
            this.ddlbank.DataValueField = "gcod";
            this.ddlbank.DataSource = ds4.Tables[0];
            this.ddlbank.DataBind();

        }

        private DataTable GetCollectTeam(DataSet ds1)
        {

            DataTable dt;
            string comcod = this.GetComCode();
            switch (comcod)
            {
                // rupayan
                case "2305":
                case "3305":
                case "3306":
                case "3309":
                case "3310":
                case "3311":
                    //leisure
                    //case "2325":
                    //case "3325":
                    dt = ds1.Tables[3];
                    break;

                default:
                    dt = ds1.Tables[1];
                    break;



            }


            return dt;

        }

        private string comcollectselect()

        {

            string comcod = this.GetComCode();

            string empselect = "";
            switch (comcod)
            {

                case "3305": // RHEL
                case "3311": // Ctg            
                    break;


                default:
                    empselect = "select";
                    break;



            }

            return empselect;

        }

        private bool IsArrayExist(string empid)
        {

            bool isexist = false;
            string value;
            foreach (ListItem litem in ddlCollType.Items)
            {

                value = litem.Value;
                if (value == empid)
                {
                    isexist = true;
                    break;


                }



            }


            return isexist;


        }
        private void PayType()
        {
            try
            {
                string type = this.Request.QueryString["Type"].ToString();
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetComCode();
                string userid = "";
                if (type == "CustCare")
                {
                    string usr = hst["usrid"].ToString();
                    userid = usr.Length > 0 ? usr : "";
                }

                DataSet ds4 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "PAYTYPE", userid, "", "", "", "", "", "", "", "");
                this.ddlpaytype.DataTextField = "gdesc";
                this.ddlpaytype.DataValueField = "gcod";
                this.ddlpaytype.DataSource = ds4.Tables[0];
                this.ddlpaytype.DataBind();

                DataTable dt = this.GetCollectTeam(ds4);
                this.ddlCollType.DataTextField = "gdesc";
                this.ddlCollType.DataValueField = "gcod";
                this.ddlCollType.DataSource = dt;
                this.ddlCollType.DataBind();

                // string  empselect= this.comcollectselect();

                //if (empselect.Length > 0)
                //{

                if (hst["empid"].ToString().Length > 0)
                {

                    bool result = IsArrayExist(hst["empid"].ToString());
                    //if(this.ddlCollType.Items.Contains(hst["empid"].ToString()))
                    if (result)
                        this.ddlCollType.SelectedValue = hst["empid"].ToString();

                }

                // }

                this.ddlRecType.DataTextField = "gdesc";
                this.ddlRecType.DataValueField = "gcod";
                this.ddlRecType.DataSource = ds4.Tables[2];
                this.ddlRecType.DataBind();
                this.ddlRecType.SelectedValue = "54003";


                ds4.Dispose();
            }
            catch (Exception ex)
            {

                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }

        //protected void GetPreMrNo()
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();
        //    string mREQNO = "NEWISS";
        //    if (this.ddlPrevISSList.Items.Count > 0)
        //        mREQNO = this.ddlPrevISSList.SelectedValue.ToString();

        //    string mREQDAT = this.GetStdDate(this.txtCurISSDate.Text.Trim());
        //    if (mREQNO == "NEWISS")
        //    {
        //        DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETLASTISSUEINFO", mREQDAT,
        //               "", "", "", "", "", "", "", "");
        //        if (ds2 == null)
        //            return;
        //        if (ds2.Tables[0].Rows.Count > 0)
        //        {
        //            mREQNO = ds2.Tables[0].Rows[0]["maxisuno"].ToString();
        //            this.lblCurISSNo1.Text = ds2.Tables[0].Rows[0]["maxisuno1"].ToString().Substring(0, 6);
        //            this.txtCurISSNo2.Text = ds2.Tables[0].Rows[0]["maxisuno1"].ToString().Substring(6, 5);
        //            this.ddlPrevISSList.DataTextField = "maxisuno1";
        //            this.ddlPrevISSList.DataValueField = "maxisuno";
        //            this.ddlPrevISSList.DataSource = ds2.Tables[0];
        //            this.ddlPrevISSList.DataBind();
        //        }
        //    }

        //}
        private void PrintDupOrOrginal()
        {
            string comcod = this.GetComCode();
            switch (comcod)
            {
                case "3301":
                case "1301":
                    //case "3101":
                    this.chkOrginal.Visible = true;
                    break;




            }


        }
        protected void lbtnBack_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = -1;
            this.gvSpayment.Visible = true;
            this.LoadGrid();
            this.Clearmrscreen();
        }
        private void Clearmrscreen()
        {
            Session.Remove("sessionforgrid");
            this.txtPaidamt.Text = "";
            this.txtchqno.Text = "";
            //this.txtBName.Text = "";
            this.txtBranchName.Text = "";
            this.txtrefid.Text = "";
            this.txtremarks.Text = "";
            this.txtbookno.Text = "";
            this.txtRpChqNo.Text = "";
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";


            this.grvacc.DataSource = null;
            this.grvacc.DataBind();
            this.tableintosession();
            this.txtPaidamt.Focus();
            this.ddlPreMrr.Items.Clear();
            this.ddlType.Enabled = true;
            this.lblSchCode.Text = "";
            this.chkOrginal.Checked = false;
            this.chkOrginal.Enabled = true;
            this.btnexcuplosd.Enabled = true;
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
        }
        private void PayInf()
        {
            string comcod = this.GetComCode();
            string UsirCode = this.lblCode.Text;
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string date = this.txtReceiveDate.Text.Trim().Length == 0 ? System.DateTime.Today.ToString("dd-MMM-yyyy") : this.txtReceiveDate.Text;
            //string date =Convert.ToDateTime(this.txtReceiveDate.Text).ToString("dd-MMM-yyyy");
            string ProcName = this.chkConsolidate.Checked ? "SP_REPORT_SALSMGT01" : "SP_ENTRY_SALSMGT";
            string CallType = this.chkConsolidate.Checked ? "RPTCLIENTLEDGER" : "INSTALLMANTWITHMRR";
            DataSet ds2 = MktData.GetTransInfo(comcod, ProcName, CallType, PactCode, UsirCode, date, "", "", "", "", "", "");
           
            if(ds2.Tables[0].Rows.Count==0)
            {
                return;

            }

            this.HiddenSameDate(ds2.Tables[0]);
            this.ShowTotalAmt();

            this.HiddenSameDate2(ds2.Tables[0]);
        }

        private void HiddenSameDate(DataTable dtable)
        {
            Session.Remove("status");

            string gcod = dtable.Rows[0]["gcod"].ToString();



            DataTable dt1 = dtable;
            DataView dv1 = dt1.DefaultView;
            dv1.RowFilter = "grp like 'AA' ";
            dt1 = dv1.ToTable();
            //this.gvSpayment.DataSource = dtOrder;
            //this.gvSpayment.DataBind();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["gcod"].ToString() == gcod)
                {
                    gcod = dt1.Rows[j]["gcod"].ToString();
                    dt1.Rows[j]["gcod"] = "";
                    dt1.Rows[j]["gdesc"] = "";
                    dt1.Rows[j]["pactcode"] = "";
                    dt1.Rows[j]["usircode"] = "";
                    dt1.Rows[j]["schamt"] = 0;
                    dt1.Rows[j]["schdate"] = "";
                }

                else
                {
                    gcod = dt1.Rows[j]["gcod"].ToString();
                }

            }
            Session["status"] = dt1;
            this.gvPayment.DataSource = dt1;
            this.gvPayment.DataBind();
            if (dt1.Rows.Count > 0)
            {
                Session["Report1"] = gvPayment;
                ((HyperLink)this.gvPayment.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }
            DataTable dt2 = dtable;
            DataView dv2 = dt2.DefaultView;
            dv2.RowFilter = "grp like 'BB' ";
            dt2 = dv2.ToTable();
            if (dt2 == null)
                return;
            this.gvCDHonour.DataSource = dv2;
            this.gvCDHonour.DataBind();
            this.lblchqdishonour.Visible = true;
        }

        private void HiddenSameDate2(DataTable dtable)
        {
            Session.Remove("rptstatus");
            string gcod = dtable.Rows[0]["gcod"].ToString();

            DataTable dt1 = dtable;
            //DataView dv1 = dt1.DefaultView;
            //dv1.RowFilter = "grp like 'AA' ";
            //dt1 = dv1.ToTable();
            //this.gvSpayment.DataSource = dtOrder;
            //this.gvSpayment.DataBind();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["gcod"].ToString() == gcod)
                {
                    gcod = dt1.Rows[j]["gcod"].ToString();
                    dt1.Rows[j]["gcod"] = "";
                    dt1.Rows[j]["gdesc"] = "";
                    dt1.Rows[j]["pactcode"] = "";
                    dt1.Rows[j]["usircode"] = "";
                    dt1.Rows[j]["schamt"] = 0;
                    dt1.Rows[j]["schdate"] = "";
                }

                else
                {
                    gcod = dt1.Rows[j]["gcod"].ToString();
                }

            }
            Session["rptstatus"] = dt1;

        }


        private void GetCurMrNo()
        {

            try
            {
                string comcod = this.GetComCode();
                DataSet ds3 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETNEWMRNO", "", "", "", "", "", "", "", "", "");
                this.lblReceiveNo.Text = ds3.Tables[0].Rows[0]["mrno"].ToString();
                ds3.Dispose();

            }

            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + ex.Message + "');", true);

            }
        }



        private void GetMrNo()
        {

            try
            {
                string comcod = this.GetComCode();
                DataSet ds3 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETNEWMRNO", "", "", "", "", "", "", "", "", "");
                this.lblReceiveNo.Text = ds3.Tables[0].Rows[0]["mrno"].ToString();
                ds3.Dispose();

            }

            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + ex.Message + "');", true);

            }
        }



        private void GetddlMrNo()
        {

            try
            {
                string comcod = this.GetComCode();
                DataSet ds3 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETNEWMRNO", "", "", "", "", "", "", "", "", "");
                this.lblReceiveNo.Text = ds3.Tables[0].Rows[0]["mrno"].ToString();
                this.ddlPreMrr.DataTextField = "mrno";
                this.ddlPreMrr.DataValueField = "mrno";
                this.ddlPreMrr.DataSource = ds3.Tables[0];
                this.ddlPreMrr.DataBind();
                ds3.Dispose();

            }

            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + ex.Message + "');", true);

            }
        }

        private void ShowTotalAmt()
        {

            DataTable dt = (DataTable)Session["status"];
            double SAmount = 0;
            double PAmount = 0, BalAmt = 0;
            SAmount = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(schamt)", "")) ? 0.00 : dt.Compute("Sum(schamt)", "")));
            PAmount = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(paidamt)", "")) ? 0.00 : dt.Compute("Sum(paidamt)", "")));
            BalAmt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(balamt)", "")) ? 0.00 : dt.Compute("Sum(balamt)", "")));

            ((Label)this.gvPayment.FooterRow.FindControl("lfAmt")).Text = SAmount.ToString("#,##0;(#,##0); -");
            ((Label)this.gvPayment.FooterRow.FindControl("lgvfpayamt")).Text = PAmount.ToString("#,##0;(#,##0); ");
            ((Label)this.gvPayment.FooterRow.FindControl("lgvFbalanceAmt")).Text = BalAmt.ToString("#,##0;(#,##0); -");


            // ((Label)this.gvPayment.FooterRow.FindControl("lfexessAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(exessamt)", "")) ? 0.00 : dt.Compute("Sum(exessamt)", ""))).ToString("#,##0;(#,##0); -");

        }

        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string logmrno = (ddlPreMrr.Items.Count > 0) ? this.ddlPreMrr.SelectedItem.ToString() : "NEW";
                DataSet ds3 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETMRNOLOG", logmrno, "", "", "", "", "", "", "", "");
                Session["UserLog"] = ds3.Tables[0];
                this.SaveValue();

                DataTable tbl2 = (DataTable)Session["status"];
                string SchCode = "";

                if (ddlPreMrr.Items.Count == 0)
                    this.GetddlMrNo();
                string mrno = this.lblReceiveNo.Text.Trim();
                string compcod = this.GetComCode();
                switch (compcod)
                {

                    case "3336":
                    case "3340":
                    case "3337":
                    case "3101":
                        string refno = this.txtrefid.Text.Trim();
                        DataSet ds1 = MktData.GetTransInfo(compcod, "SP_ENTRY_PURCHASE_01", "CHECKMRRREFNO", refno, "", "", "",
                            "", "", "", "", "");
                        if (ds1.Tables[0].Rows.Count == 0)
                            ;
                        else
                        {

                            DataView dv1 = ds1.Tables[0].DefaultView;
                            dv1.RowFilter = ("mrno <>'" + mrno + "'");
                            DataTable dtc = dv1.ToTable();
                            if (dtc.Rows.Count == 0)
                                return;
                            else
                            {
                                ((Label)this.Master.FindControl("lblmsg")).Text = "Found Duplicate M.R No";
                                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                                //this.ddlPrevReqList.Items.Clear();
                                return;
                            }

                        }

                        break;


                    default:
                        break;

                }

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                if (!Convert.ToBoolean(dr1[0]["entry"]))
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }




                string Usircode = this.lblCode.Text.Trim();
                //string mrno = this.lblReceiveNo.Text.Trim();
                if (mrno.Length == 0)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Required MR No";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }


                // string SchCode=
                string mrdate = Convert.ToDateTime(this.txtReceiveDate.Text).ToString("dd-MMM-yyyy");
                //////////////////////userlog
                DataTable dtuser = (DataTable)Session["UserLog"];
                string tblPostedByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postedbyid"].ToString();
                string tblPostedtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postrmid"].ToString();
                string tblPostedSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postseson"].ToString();
                string tblPosteddat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["entrydat"]).ToString("dd-MMM-yyyy hh:mm:ss tt");

                string userid = hst["usrid"].ToString();
                string Terminal = hst["compname"].ToString();
                string Sessionid = hst["session"].ToString();
                string PostedByid = (this.Request.QueryString["type"] == "CustCare") ? userid : (tblPostedByid == "") ? userid : tblPostedByid;
                string Posttrmid = (this.Request.QueryString["type"] == "CustCare") ? Terminal : (tblPostedtrmid == "") ? Terminal : tblPostedtrmid;
                string PostSession = (this.Request.QueryString["type"] == "CustCare") ? Sessionid : (tblPostedSession == "") ? Sessionid : tblPostedSession;
                string Posteddat = (this.Request.QueryString["type"] == "CustCare") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : (tblPosteddat == "01-Jan-1900") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : tblPosteddat;
                string EditByid = (this.Request.QueryString["type"] == "CustCare") ? "" : userid;
                string Editdat = (this.Request.QueryString["type"] == "CustCare") ? "01-Jan-1900" : System.DateTime.Today.ToString("dd-MMM-yyyy");

                DataTable dt1 = ((DataTable)Session["sessionforgrid"]).Copy();

                // Company Balance

                switch (comcod)
                {
                    case "3340"://Urban
                    case "3101"://Pintech
                        double SAmount = 0;
                        double PAmount = 0, BalAmt = 0;
                        DataTable dt = ((DataTable)Session["status"]).Copy();

                        DataView dv = dt.DefaultView;
                        dv.RowFilter = ("mrno <>'" + mrno + "'");
                        DataTable dep = dv.ToTable();

                        //Without Service
                        DataTable dem = dt1.Copy();
                        DataView dvm = dem.DefaultView;
                        dvm.RowFilter = ("recType not in ('54004', '54006', '54008', '54009', '54012', '54015', '54020')");
                        dem = dvm.ToTable();

                        SAmount = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(schamt)", "")) ? 0.00 : dt.Compute("Sum(schamt)", "")));
                        PAmount = Convert.ToDouble((Convert.IsDBNull(dep.Compute("Sum(paidamt)", "")) ? 0.00 : dep.Compute("Sum(paidamt)", "")));
                        BalAmt = SAmount - PAmount;

                        double paidamt = Convert.ToDouble((Convert.IsDBNull(dem.Compute("Sum(paidamount)", "")) ? 0.00 : dem.Compute("Sum(paidamount)", "")));
                        double disamt = Convert.ToDouble((Convert.IsDBNull(dem.Compute("Sum(disamt)", "")) ? 0.00 : dem.Compute("Sum(disamt)", "")));
                        double topaidamt = paidamt + disamt;
                        if (topaidamt > BalAmt)
                        {

                            ((Label)this.Master.FindControl("lblmsg")).Text = "Receipt Amount exceed schedule";
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                            return;

                        }
                        break;
                    default:
                        break;



                }

                bool result = true;
                string PactCode = "";
                for (int i = 0; i < dt1.Rows.Count; i++)
                {

                    string instype = (this.Request.QueryString["Type"] == "CustCare") ? dt1.Rows[i]["instype"].ToString() : "";
                    SchCode = this.GetSchCode(instype);
                    string RecType = dt1.Rows[i]["recType"].ToString();

                    // Utility Charge  (Problem)
                    switch (comcod)
                    {
                        case "3305": // Housing
                        case "2305": // land
                        case "3306": // Ratul
                        case "3309": // Holding
                        case "3311": // chitagong
                        case "3310": // Rcu
                        case "3325":// Leisure
                        case "2325"://Liesure
                        case "3101"://Liesure

                            PactCode = this.ddlProjectName.SelectedValue.ToString();
                            break;

                        case "3340":// Urban                   
                            PactCode = (RecType == "54004" || RecType == "54006" || RecType == "54008" || RecType == "54009" || RecType == "54012" || RecType == "54015" || RecType == "54018" || RecType == "54020") ? ("25" + this.ddlProjectName.SelectedValue.ToString().Substring(2)) : this.ddlProjectName.SelectedValue.ToString();

                            break;

                        case "3339":
                            //case "3101":

                            PactCode = (RecType == "54004" || RecType == "54006" || RecType == "54009" || RecType == "54012" || RecType == "54015" || RecType == "54018" || RecType == "54020") ? ("27" + this.ddlProjectName.SelectedValue.ToString().Substring(2)) : this.ddlProjectName.SelectedValue.ToString();
                            break;

                        default:
                            PactCode = (RecType == "54004" || RecType == "54006" || RecType == "54009" || RecType == "54012" || RecType == "54015" || RecType == "54018" || RecType == "54020") ? ("25" + this.ddlProjectName.SelectedValue.ToString().Substring(2)) : this.ddlProjectName.SelectedValue.ToString();
                            break;


                    }

                    //string PactCode = (comcod == "3325" || comcod == "2325") ? this.ddlProjectName.SelectedValue.ToString() :
                    //    (RecType == "54004" || RecType == "54006" || RecType == "54008" || RecType == "54009" || RecType == "54012" || RecType == "54015" || RecType == "54020") ? ("25" + this.ddlProjectName.SelectedValue.ToString().Substring(2)) : this.ddlProjectName.SelectedValue.ToString();
                    string type = dt1.Rows[i]["paytypecod"].ToString(); // this.ddlpaytype.SelectedValue.ToString();repchqno
                    double paidamt = Convert.ToDouble(dt1.Rows[i]["paidamount"]);
                    string chqno = dt1.Rows[i]["chequeno"].ToString();
                    string bname = dt1.Rows[i]["bankname"].ToString();
                    string branchname = dt1.Rows[i]["branchname"].ToString();
                    string paydate = Convert.ToDateTime(dt1.Rows[i]["paydate"].ToString()).ToString("dd-MMM-yyyy");
                    string refno = dt1.Rows[i]["refid"].ToString();
                    string repchqno = dt1.Rows[i]["repchqno"].ToString();
                    string remrks = dt1.Rows[i]["remarks"].ToString();//
                    string Collfrm = dt1.Rows[i]["collfrm"].ToString();
                    string bookno = dt1.Rows[i]["bookno"].ToString();

                    paidamt = (RecType == "54097") ? paidamt * -1 : paidamt;
                    double disamt = Convert.ToDouble(dt1.Rows[i]["disamt"]);
                    string isLO = "0";
                    //string type1 = this.Request.QueryString["Type"];
                    //string management = (type1 == "Management" ? "management" : ""); // mr edit 

                    //schamt = schamt + paidamt;
                    if (paidamt != 0 || disamt != 0)
                       
                        result = MktData.UpdateTransInfo01(comcod, "SP_ENTRY_SALSMGT", "INSERTORUPDATEMRINF", PactCode, Usircode, mrno, type, mrdate, paidamt.ToString(), chqno,
                                                          bname, branchname, paydate, refno, remrks, PostedByid, PostSession, Posttrmid, Posteddat, EditByid, 
                                                          Editdat, SchCode, repchqno, Collfrm, RecType, disamt.ToString(), bookno,"","0.00", isLO);

                    if (result == false)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }

                }


                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

                //Auto Update Approved
                string Type = this.Request.QueryString["Type"];


                if (Type == "CustCare")
                {
                    switch (comcod)
                    {
                        case "3339": //Tropical
                        case "3367": //epic
                        //case "3101": //ptl


                            break;

                        default:
                            // DataTable dt = (DataTable) Session["tblapprecpt"];
                            //int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

                            string appid = hst["usrid"].ToString();
                            string appsession = hst["session"].ToString();
                            string termid = hst["compname"].ToString();

                            string appdate = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

                            foreach (DataRow dr2 in dt1.Rows)
                            {
                                string chqno = dr2["chequeno"].ToString();
                                bool result1 = MktData.UpdateTransInfo(comcod, "SP_REPORT_ACCOUNTS_INTERFACE",
                                    "INSERTRECPTAPPROVAL", appid, appdate,
                                    appsession, termid, chqno, mrno, "", "", "", "", "", "", "", "", "");

                                if (!result1)
                                {
                                    ((Label)this.Master.FindControl("lblmsg")).Text = MktData.ErrorObject["Msg"].ToString();
                                    ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                                        "alert('Update Failed !!!');", true);
                                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

                                    return;
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                                        "alert('Data Update Successfully !!!.');", true);
                                }

                            }
                            break;
                    }
                }
                if (Type == "Management")
                {

                    // DataTable dt = (DataTable) Session["tblapprecpt"];
                    //int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

                    string appid = hst["usrid"].ToString();
                    string appsession = hst["session"].ToString();
                    string termid = hst["compname"].ToString();

                    string appdate = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

                    foreach (DataRow dr2 in dt1.Rows)
                    {
                        string chqno = dr2["chequeno"].ToString();
                        bool result1 = MktData.UpdateTransInfo(comcod, "SP_REPORT_ACCOUNTS_INTERFACE",
                            "INSERTRECPTAPPROVAL", appid, appdate,
                            appsession, termid, chqno, mrno, "", "", "", "", "", "", "", "", "");

                        if (!result1)
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = MktData.ErrorObject["Msg"].ToString();
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                                "alert('Update Failed !!!');", true);
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

                            return;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                                "alert('Data Update Successfully !!!.');", true);
                        }

                    }
                }

                //Log Report
                string eventtype = "Money Receipt";
                string eventdesc = "Receipt No: " + mrno + " Dated: " + mrdate;
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                this.PayInf();
                this.lblSchCode.Text = SchCode;
                this.lbtRefreshMrr.Focus();
                // this.ddlType.Enabled = false;

                //
                //string compsms = hst["compsms"].ToString();
                //if (compsms == "True")
                //{
                //    DataSet dsSm = CALogRecord.CheckStatus(comcod, "2301");
                //    if (dsSm.Tables[0].Rows.Count == 0)
                //        return;
                //    if (dsSm.Tables[0].Rows[0]["sactive"].ToString() == "True")
                //    {
                //        //string Phone = this.lblPhone.Text.Trim();
                //        //double amt = Convert.ToDouble("0" + ((Label)this.grvacc.FooterRow.FindControl("txtFTotal")).Text);
                //        //string ntype = dsSm.Tables[0].Rows[0]["gcod"].ToString();
                //        //string smsstatus = (dsSm.Tables[0].Rows[0]["sactive"].ToString() == "True") ? "Y" : "N";
                //        //string smscontent = dsSm.Tables[0].Rows[0]["smscont"].ToString().Replace("XXXXX", amt.ToString());
                //        //string mailstatus = (dsSm.Tables[0].Rows[0]["mactive"].ToString() == "True") ? "Y" : "N";
                //        //string mailcontent = dsSm.Tables[0].Rows[0]["mailcont"].ToString();
                //        //string mailattch = "";


                //        //bool IsSMSaved = CALogRecord.AddSMRecord(comcod, ((Hashtable)Session["tblLogin"]), PactCode, Usircode, mrno, mrdate, ntype, smsstatus, smscontent.Replace("YYYYY", mrno), mailstatus,
                //        //        mailcontent, mailattch, Phone, "");
                //    }
                //}
                PactCode = this.ddlProjectName.SelectedValue.ToString();
                // string usercode = ViewState["usricode"].ToString();
                switch (comcod)
                {
                    //case "3101": // Pintech                   
                    case "3356": //Intech                    
                        this.SMSSendMoneyRecipt(comcod, PactCode, Usircode, mrno, mrdate);
                        break;
                    default:
                        break;
                }


            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Errp:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }

        private void SMSSendMoneyRecipt(string comcod, string PactCode, string Usircode, string mrno, string mrdate)
        {
            DataSet ds = MktData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_SALMGT", "GETOUTSTANDAMT", PactCode, Usircode,
                      "", "", "", "", "", "", "");
            if (ds == null)
            {
                return;
            }
            DataSet dssms = CALogRecord.CheckStatus(comcod, "2301");
            string payableamt = Convert.ToDouble(((Label)this.grvacc.FooterRow.FindControl("txtFTotal")).Text).ToString("#,##0.00; (#,##0.00) ");
            string cutname = ds.Tables[0].Rows[0]["custname"].ToString() == "" ? "" : ds.Tables[0].Rows[0]["custname"].ToString();
            string custphone = ds.Tables[0].Rows[0]["custphone"].ToString() == "" ? "" : ds.Tables[0].Rows[0]["custphone"].ToString();
            string trcvamt = ds.Tables[0].Rows[0]["trecvamt"].ToString() == "" ? "" : Convert.ToDouble(ds.Tables[0].Rows[0]["trecvamt"]).ToString("#,##0.00;(#,##0.00) "); ;
            string payment = ds.Tables[0].Rows[0]["payamt"].ToString() == "" ? "" : Convert.ToDouble(ds.Tables[0].Rows[0]["payamt"]).ToString("#,##0.00;(#,##0.00) ");
            string paymentdate = ds.Tables[0].Rows[0]["paydate"].ToString() == "" ? "" : Convert.ToDateTime(ds.Tables[0].Rows[0]["paydate"]).ToString("dd-MMM-yyyy");
            string dues = ds.Tables[0].Rows[0]["duesamt"].ToString() == "" ? "" : Convert.ToDouble(ds.Tables[0].Rows[0]["duesamt"]).ToString("#,##0.00;(#,##0.00) ");

            string paymod = this.ddlpaytype.SelectedItem.Text.ToString();
            string cheq = this.txtchqno.Text.ToString();

            string tempeng = dssms.Tables[0].Rows[0]["smscont"].ToString();
            tempeng = tempeng.Replace("[name]", cutname);
            tempeng = tempeng.Replace("[date]", paymentdate);
            tempeng = tempeng.Replace("[payamt]", payableamt);
            tempeng = tempeng.Replace("[duesamt]", dues);
            tempeng = tempeng.Replace("[paymode]", paymod);
            tempeng = tempeng.Replace("[chequeno]", cheq);
            string smtext = tempeng;
            SendSmsProcess sms = new SendSmsProcess();
            string ntype = dssms.Tables[0].Rows[0]["gcod"].ToString();
            string smsstatus = (dssms.Tables[0].Rows[0]["sactive"].ToString() == "True") ? "Y" : "N";
            bool resultsms = sms.SendSMSClient(comcod, smtext, custphone);
            if (resultsms == true)
            {
                bool IsSMSaved = CALogRecord.AddSMRecord(comcod, ((Hashtable)Session["tblLogin"]), PactCode, Usircode, mrno, mrdate, ntype, smsstatus, smtext, "",
                           "", "", custphone, "");
            }
        }
        private string GetSchCode(string instype)
        {
            string SchCode = "";
            if (instype.Length == 0)
                return SchCode;
            string sindex = instype.Substring(0, 2);
            switch (sindex)
            {
                case "11":
                    SchCode = "81988";
                    break;
                case "13":
                    SchCode = "81990";
                    break;
                case "14":
                    SchCode = "81991";
                    break;

                case "16":
                    SchCode = "81993";
                    break;
                case "18":
                    SchCode = "81996";
                    break;

                case "26": // Upgration
                    SchCode = "81986";
                    break;
                case "30":
                    SchCode = "81998";
                    break;
            }
            if (SchCode == "")
                return "";

            return SchCode;
        }
        protected void lbtRefreshMrr_Click(object sender, EventArgs e)
        {
            this.Clearmrscreen();
            this.GetCurMrNo();
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string usircode = this.lblCode.Text;
            string mrno = this.lblReceiveNo.Text.Trim();
            string mrdate = Convert.ToDateTime(this.txtReceiveDate.Text).ToString("dd-MMM-yyyy");


            string hostname = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/F_17_Acc/";
            //string hostname = "~/F_17_Acc/";
            string currentptah = "PrintMoneyReceipt?Type=moneyReceipt&pactcode=" + pactcode + "&usircode=" + usircode + "&mrno=" + mrno + "&mrdate=" + mrdate;
            string totalpath = hostname + currentptah;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('" + totalpath + "', target='_blank');</script>";


        }

        private void MoneyReceiptPrint()
        {
            string comcod = this.GetComCode();
            string mrrno = this.ddlPreMrr.SelectedValue.ToString().Trim();
            string mPrint = this.chkOrginal.Checked ? "1" : "0";
            bool result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "INSORUPDATEMPRINT", mrrno, mPrint, "", "", "", "", "", "", "", "", "", "", "", "", "");
        }
        protected void rbtnList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void chkPrevious_CheckedChanged(object sender, EventArgs e)
        {
            this.Panel3.Visible = this.chkPrevious.Checked;
            if (this.chkPrevious.Checked)
            {
                this.PreviousMrr();
            }
        }
        private void PreviousMrr()
        {
            Session.Remove("tblpremrr");
            string comcod = this.GetComCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string usircode = this.lblCode.Text.Trim();
            DataSet ds4 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GetPreMrr", pactcode, usircode, "", "", "", "", "", "", "");
            if (ds4 == null)
                return;
            this.ddlPreMrr.DataTextField = "mrrno1";
            this.ddlPreMrr.DataValueField = "mrrno";
            this.ddlPreMrr.DataSource = ds4.Tables[1];
            this.ddlPreMrr.DataBind();
            Session["tblpremrr"] = ds4.Tables[0];
        }

        protected void lbtnShowPreMrr_Click(object sender, EventArgs e)
        {
            Session.Remove("sessionforgrid");
            this.tableintosession();
            DataTable dt = (DataTable)Session["tblpremrr"];
            DataTable dt01 = (DataTable)Session["sessionforgrid"];
            string mrrno = this.ddlPreMrr.SelectedValue.ToString();
            // DataTable dt02 = dt.Select("mrrno='" + mrrno + "'");
            DataRow[] dr1 = dt.Select("mrrno='" + mrrno + "'");
            if (dr1.Length > 0)
            {
                this.lblReceiveNo.Text = dr1[0]["mrrno"].ToString();
                this.txtReceiveDate.Text = Convert.ToDateTime(dr1[0]["mrdate"]).ToString("dd-MMM-yyyy");
                this.txtPaidamt.Text = Convert.ToDouble(dr1[0]["paidamt"]).ToString("#,##0;-#,##0; ");

                this.ddlpaytype.SelectedValue = dr1[0]["paytype"].ToString();
                this.txtchqno.Text = dr1[0]["chqno"].ToString();
                //this.txtBName.Text 
                this.ddlbank.SelectedItem.Text = dr1[0]["bankname"].ToString();
                this.txtBranchName.Text = dr1[0]["bbranch"].ToString();
                this.txtpaydate.Text = Convert.ToDateTime(dr1[0]["paydate"]).ToString("dd-MMM-yyyy");
                this.txtrefid.Text = dr1[0]["refno"].ToString();
                this.txtRpChqNo.Text = dr1[0]["repchqno"].ToString();
                this.txtremarks.Text = dr1[0]["rmrks"].ToString();
                this.txtbookno.Text = dr1[0]["bookno"].ToString();
                this.ddlCollType.SelectedValue = dr1[0]["collfrm"].ToString();
                this.ddlRecType.SelectedValue = dr1[0]["recType"].ToString();
                this.lblSchCode.Text = dr1[0]["schcode"].ToString();
                string instypecode = ((dr1[0]["schcode"].ToString() == "") ?
                    ((this.ddlRecType.SelectedValue == "54001") ? "07"
                    : (this.ddlRecType.SelectedValue == "54050") ? "08"
                    : (this.ddlRecType.SelectedValue == "54002") ? "09" : "10") : (dr1[0]["schcode"].ToString().Substring(0, 5) == "81988") ? "11" : (dr1[0]["schcode"].ToString().Substring(0, 5) == "81990") ? "13" :
                    (dr1[0]["schcode"].ToString().Substring(0, 5) == "81991") ? "14" : (dr1[0]["schcode"].ToString().Substring(0, 5) == "81993") ? "16" : "10") + "0000000";


                DataRow[] drs = ((DataTable)ViewState["tblinttype"]).Select("gcod='" + instypecode + "'");

                this.ddlType.SelectedValue = drs.Length > 0 ? instypecode : "100000000";
                //foreach(ListItem lstitem in ddlType.Items)                
                //{
                //        string value=lstitem.Value;

                //        if (instypecode == value) 
                //        {

                //            lstitem.Value = value;
                //            break;
                //        }
                //}
                this.ddlType.Enabled = false;
                this.chkOrginal.Checked = Convert.ToBoolean(dr1[0]["oprint"]);
                this.chkOrginal.Enabled = !(Convert.ToBoolean(dr1[0]["oprint"]));
                this.txtdiscountamt.Text = Convert.ToDouble(dr1[0]["disamt"]).ToString("#,##0;-#,##0; ");
            }
            //---------
            DataTable dtstatus = dt;//.Tables[0];
            DataView dv1 = dtstatus.DefaultView;
            dv1.RowFilter = "mrrno='" + mrrno + "'";
            DataTable dtmr = dv1.ToTable();
            for (int i = 0; dtmr.Rows.Count > i; i++)
            {
                DataRow drforgrid = dt01.NewRow();
                drforgrid["paidamount"] = Convert.ToDouble(dtmr.Rows[i]["paidamt"]).ToString("#,##0;-#,##0;");
                drforgrid["paytype"] = dtmr.Rows[i]["paytypedesc"].ToString(); // this.ddlpaytype.SelectedItem.Text.Trim(); paytypedesc
                drforgrid["paytypecod"] = dtmr.Rows[i]["paytype"].ToString();
                drforgrid["chequeno"] = dtmr.Rows[i]["chqno"].ToString();
                drforgrid["bankname"] = dtmr.Rows[i]["bankname"].ToString();
                drforgrid["branchname"] = dtmr.Rows[i]["bbranch"].ToString();
                drforgrid["paydate"] = Convert.ToDateTime(dtmr.Rows[i]["paydate"]).ToString("dd-MMM-yyyy");
                drforgrid["refid"] = dtmr.Rows[i]["refno"].ToString();
                drforgrid["repchqno"] = dtmr.Rows[i]["repchqno"].ToString();
                drforgrid["remarks"] = dtmr.Rows[i]["rmrks"].ToString();
                drforgrid["bookno"] = dtmr.Rows[i]["bookno"].ToString();
                drforgrid["collfrm"] = dtmr.Rows[i]["collfrm"].ToString();
                drforgrid["collfrmd"] = dtmr.Rows[i]["collfrmd"].ToString();
                drforgrid["RecType"] = dtmr.Rows[i]["RecType"].ToString();
                drforgrid["RecTyped"] = dtmr.Rows[i]["RecTyped"].ToString();
                drforgrid["disamt"] = dtmr.Rows[i]["disamt"].ToString();
                dt01.Rows.Add(drforgrid);
                //---------
            }
            this.chkPrevious.Checked = false;
            this.chkPrevious_CheckedChanged(null, null);
            this.grvacc_DataBind();
        }
        protected void tableintosession()
        {
            DataTable dttemp = new DataTable();
            dttemp.Columns.Add("mrno", Type.GetType("System.String"));
            dttemp.Columns.Add("paidamount", Type.GetType("System.Double"));
            dttemp.Columns.Add("paytype", Type.GetType("System.String"));
            dttemp.Columns.Add("instype", Type.GetType("System.String"));
            dttemp.Columns.Add("insdesc", Type.GetType("System.String"));
            dttemp.Columns.Add("paytypecod", Type.GetType("System.String"));
            dttemp.Columns.Add("chequeno", Type.GetType("System.String"));
            dttemp.Columns.Add("bankname", Type.GetType("System.String"));
            dttemp.Columns.Add("branchname", Type.GetType("System.String"));
            dttemp.Columns.Add("mrdate", Type.GetType("System.String"));
            dttemp.Columns.Add("paydate", Type.GetType("System.String"));
            dttemp.Columns.Add("refid", Type.GetType("System.String"));
            dttemp.Columns.Add("repchqno", Type.GetType("System.String"));
            dttemp.Columns.Add("remarks", Type.GetType("System.String"));
            dttemp.Columns.Add("bookno", Type.GetType("System.String"));
            dttemp.Columns.Add("collfrm", Type.GetType("System.String"));
            dttemp.Columns.Add("collfrmd", Type.GetType("System.String"));
            dttemp.Columns.Add("RecType", Type.GetType("System.String"));
            dttemp.Columns.Add("RecTyped", Type.GetType("System.String"));
            dttemp.Columns.Add("disamt", Type.GetType("System.Double"));
            Session["sessionforgrid"] = dttemp;
        }
        protected void ibtnCollfrm_Click(object sender, EventArgs e)
        {
            //  this.GetSalesOrCustCare();
        }
        //private void GetSalesOrCustCare()
        //{
        //    string comcod = this.GetComCode();
        //    string SechSorCusName = "%" + this.txtSrchCollfrm.Text.Trim() + "%";
        //    DataSet ds4 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETSALEORCUSTCARETM", SechSorCusName, "", "", "", "", "", "", "", "");
        //    DataTable dt = this.GetCollectTeam02(ds4);
        //    this.ddlCollType.DataTextField = "gdesc";
        //    this.ddlCollType.DataValueField = "gcod";
        //    this.ddlCollType.DataSource = dt;
        //    this.ddlCollType.DataBind();
        //    //this.ddlCollType.SelectedValue = "53061001001";
        //    //this.ddlCollType.DataTextField = "gdesc";
        //    //this.ddlCollType.DataValueField = "gcod";
        //    //this.ddlCollType.DataSource = ds4.Tables[0];
        //    //this.ddlCollType.DataBind();
        //   // this.ddlCollType.SelectedValue = "53061001001";
        // }
        private DataTable GetCollectTeam02(DataSet ds1)
        {
            DataTable dt;
            string comcod = this.GetComCode();
            switch (comcod)
            {

                case "3305":
                case "2305":
                case "3306":
                case "3309":
                case "3311":
                case "3310":

                    dt = ds1.Tables[1];
                    break;
                default:
                    dt = ds1.Tables[0];
                    break;
            }
            return dt;
        }

        protected void lblAddToTable_Click(object sender, EventArgs e)
        {
            try
            {
                string chequeno = this.txtchqno.Text.Trim();
                string instype = this.ddlType.SelectedValue.ToString().Trim();
                string mrno = this.lblReceiveNo.Text.Trim();
                string comcod = this.GetComCode();
                switch (comcod)
                {
                    //case "3336":
                    case "3340":
                    case "3337":
                    case "3101":
                    case "3353":
                        string refno = this.txtrefid.Text.Trim();
                        if (refno.Length == 0)
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = "Please Input MR No (Manual)";
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                            return;
                        }
                        DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "CHECKMRRREFNO", refno, "", "", "",
                            "", "", "", "", "");
                        if (ds1.Tables[0].Rows.Count == 0) ;
                        else
                        {
                            DataView dv1 = ds1.Tables[0].DefaultView;
                            dv1.RowFilter = ("mrno <>'" + mrno + "'");
                            DataTable dtc = dv1.ToTable();
                            if (dtc.Rows.Count == 0)
                                ;
                            else
                            {
                                ((Label)this.Master.FindControl("lblmsg")).Text = "Found Duplicate M.R No";
                                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                                //this.ddlPrevReqList.Items.Clear();
                                return;
                            }
                        }
                        break;
                    //case "3336":
                    //case "3340":
                    //    double schamt= Convert.ToDouble ("0" + ((Label)this.gvPayment.FooterRow.FindControl("lfAmt")).Text.Trim ());                   
                    //    double rcvAmt = Convert.ToDouble("0" + ((Label)this.gvPayment.FooterRow.FindControl("lgvfpayamt")).Text.Trim());                  
                    //    double paidamt = Convert.ToDouble(this.txtPaidamt.Text);
                    //    double actpaidAmt = schamt - rcvAmt;
                    //    if (paidamt > actpaidAmt)
                    //    {
                    //        ((Label)this.Master.FindControl ("lblmsg")).Text = "Receipt Amount Not Exceeded To Balance Amount!!!";
                    //        ScriptManager.RegisterStartupScript (this, GetType (), "alert", "HideLabel(0);", true);
                    //        return;
                    //    }

                    //  break;
                    default:
                        break;
                }

                DataTable dt = ((DataTable)Session["sessionforgrid"]);
                // Company Balance

                switch (comcod)
                {
                    case "3340"://Urban
                    case "3101"://Urban

                        double SAmount = 0;
                        double PAmount = 0, BalAmt = 0;
                        DataTable dts = ((DataTable)Session["status"]).Copy();
                        DataView dv = dts.DefaultView;
                        dv.RowFilter = ("mrno <>'" + mrno + "'");
                        DataTable dep = dv.ToTable();

                        SAmount = Convert.ToDouble((Convert.IsDBNull(dts.Compute("Sum(schamt)", "")) ? 0.00 : dts.Compute("Sum(schamt)", "")));
                        PAmount = Convert.ToDouble((Convert.IsDBNull(dep.Compute("Sum(paidamt)", "")) ? 0.00 : dep.Compute("Sum(paidamt)", "")));
                        BalAmt = SAmount - PAmount;
                        //double paidamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(paidamount)", "")) ? 0.00 : dt.Compute("Sum(paidamount)", "")));
                        //double disamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(disamt)", "")) ? 0.00 : dt.Compute("Sum(disamt)", "")));
                        string RecpType = this.ddlRecType.SelectedValue.ToString();

                        double mramt = (RecpType == "54004" || RecpType == "54006" || RecpType == "54008" || RecpType == "54009" || RecpType == "54012" || RecpType == "54015" || RecpType == "54020") ? 0.00 : ASTUtility.StrPosOrNagative(this.txtPaidamt.Text.Trim());
                        double topaidamt = mramt; //+ mramt;
                        if (topaidamt > BalAmt)
                        {

                            ((Label)this.Master.FindControl("lblmsg")).Text = "Receipt Amount exceed schedule";
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                            return;
                        }

                        break;
                    default:
                        break;
                }

                string paytype = this.ddlpaytype.SelectedValue.ToString();
                switch (comcod)
                {
                    case "3339": // Tropical
                        chequeno = (chequeno.Length == 0) ? (paytype == "82001" ? chequeno : this.ddlpaytype.SelectedItem.Text) : chequeno;
                        break;
                    default:

                        chequeno = (chequeno.Length == 0) ? "CASH" : chequeno;
                        break;
                }
                DataRow[] projectrow1 = dt.Select("chequeno = '" + chequeno + "'and instype='" + instype + "'"); //repchqno
                if (projectrow1.Length == 0)
                {
                    DataRow drforgrid = dt.NewRow();
                    drforgrid["instype"] = this.ddlType.SelectedValue.ToString();
                    drforgrid["insdesc"] = this.ddlType.SelectedItem.Text;
                    drforgrid["paidamount"] = ASTUtility.StrPosOrNagative(this.txtPaidamt.Text.Trim());
                    drforgrid["paytype"] = this.ddlpaytype.SelectedItem.Text.Trim();
                    drforgrid["paytypecod"] = this.ddlpaytype.SelectedValue.ToString();
                    //string comcod = this.GetComCode();
                    //  string chequeno = this.txtchqno.Text.Trim();
                    drforgrid["chequeno"] = chequeno;
                    drforgrid["bankname"] = (this.ddlpaytype.SelectedValue.ToString() == "82002") ? "" : this.ddlbank.SelectedItem.Text.ToString(); //this.txtBName.Text.Trim();
                    drforgrid["branchname"] = this.txtBranchName.Text.Trim();
                    drforgrid["paydate"] = this.txtpaydate.Text.Trim();
                    drforgrid["refid"] = this.txtrefid.Text.Trim();
                    drforgrid["repchqno"] = this.txtRpChqNo.Text.Trim();
                    drforgrid["remarks"] = this.txtremarks.Text.Trim();
                    drforgrid["bookno"] = this.txtbookno.Text.Trim();
                    drforgrid["collfrm"] = this.ddlCollType.SelectedValue.ToString();
                    drforgrid["collfrmd"] = this.ddlCollType.SelectedItem.Text.Trim();
                    drforgrid["RecType"] = this.ddlRecType.SelectedValue.ToString();
                    drforgrid["RecTyped"] = this.ddlRecType.SelectedItem.Text.Trim();
                    drforgrid["disamt"] = Convert.ToDouble("0" + this.txtdiscountamt.Text.Trim());
                    dt.Rows.Add(drforgrid);
                }
                else
                {
                    projectrow1[0]["collfrm"] = this.ddlCollType.SelectedValue.ToString();
                    projectrow1[0]["collfrmd"] = this.ddlCollType.SelectedItem.Text.Trim();
                    projectrow1[0]["RecType"] = this.ddlRecType.SelectedValue.ToString();
                    projectrow1[0]["RecTyped"] = this.ddlRecType.SelectedItem.Text.Trim();

                }

                //Two Time 
                switch (comcod)
                {
                    case "3340"://Urban
                    case "3101"://Urban

                        double SAmount = 0;
                        double PAmount = 0, BalAmt = 0;
                        DataTable dts = ((DataTable)Session["status"]).Copy();
                        DataView dv = dts.DefaultView;
                        dv.RowFilter = ("mrno <>'" + mrno + "'");
                        DataTable dep = dv.ToTable();


                        //Without Service
                        DataTable dem = dt.Copy();
                        DataView dvm = dem.DefaultView;
                        dvm.RowFilter = ("recType not in ('54004', '54006', '54008', '54009', '54012', '54015', '54020')");
                        dem = dvm.ToTable();

                        SAmount = Convert.ToDouble((Convert.IsDBNull(dts.Compute("Sum(schamt)", "")) ? 0.00 : dts.Compute("Sum(schamt)", "")));
                        PAmount = Convert.ToDouble((Convert.IsDBNull(dep.Compute("Sum(paidamt)", "")) ? 0.00 : dep.Compute("Sum(paidamt)", "")));
                        BalAmt = SAmount - PAmount;
                        double paidamt = Convert.ToDouble((Convert.IsDBNull(dem.Compute("Sum(paidamount)", "")) ? 0.00 : dem.Compute("Sum(paidamount)", "")));
                        double disamt = Convert.ToDouble((Convert.IsDBNull(dem.Compute("Sum(disamt)", "")) ? 0.00 : dem.Compute("Sum(disamt)", "")));
                        // double mramt = ASTUtility.StrPosOrNagative(this.txtPaidamt.Text.Trim());
                        double topaidamt = paidamt + disamt; //+ mramt;
                        if (topaidamt > BalAmt)
                        {

                            ((Label)this.Master.FindControl("lblmsg")).Text = "Receipt Amount exceed schedule";
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                            return;

                        }

                        break;
                    default:
                        break;
                }

                Session["sessionforgrid"] = dt;




                this.grvacc_DataBind();
                this.txtPaidamt.Focus();
            }

            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

            }

        }

        protected void grvacc_DataBind()
        {
            DataTable tbl1 = (DataTable)Session["sessionforgrid"];
            this.grvacc.DataSource = tbl1;
            this.grvacc.DataBind();

            if (tbl1.Rows.Count > 0)
            {
                ((Label)this.grvacc.FooterRow.FindControl("txtFTotal")).Text = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(paidamount)", "")) ? 0.00 : tbl1.Compute("Sum(paidamount)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.grvacc.FooterRow.FindControl("lblgvFdisamt")).Text = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(disamt)", "")) ? 0.00 : tbl1.Compute("Sum(disamt)", ""))).ToString("#,##0;(#,##0); ");



                this.lbtnUpdate.Visible = true;
            }

        }

        protected void grvacc_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {


            string comcod = this.GetComCode();
            DataTable dt = (DataTable)Session["sessionforgrid"];
            string mrrno = this.ddlPreMrr.SelectedValue.ToString().Trim();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string usircode = this.lblCode.Text.Trim();
            string refid = ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvCheckno")).Text.Trim();
            bool result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "DELETEMRRREFNO", mrrno, pactcode, usircode, refid, "", "", "", "", "", "", "", "", "", "", "");

            if (result)
            {
                int rowindex = (this.grvacc.PageSize) * (this.grvacc.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
                DataView dv = dt.DefaultView;
                dv.RowFilter = ("chequeno<>''");
                Session["sessionforgrid"] = dv.ToTable();
                this.grvacc_DataBind();
            }




        }

        private void SaveValue()
        {
            DataTable tbl1 = (DataTable)Session["sessionforgrid"];
            for (int i = 0; i < grvacc.Rows.Count; i++)
            {
                tbl1.Rows[i]["chequeno"] = ((TextBox)this.grvacc.Rows[i].FindControl("txtgvCheckno")).Text.Trim();
                tbl1.Rows[i]["bankname"] = ((TextBox)this.grvacc.Rows[i].FindControl("txtgvbankna")).Text.Trim();
                tbl1.Rows[i]["branchname"] = ((TextBox)this.grvacc.Rows[i].FindControl("txtgvBrance")).Text.Trim();
                tbl1.Rows[i]["paydate"] = ((TextBox)this.grvacc.Rows[i].FindControl("txtgvpaydate")).Text.Trim();
                tbl1.Rows[i]["refid"] = ((TextBox)this.grvacc.Rows[i].FindControl("txtgvrefid")).Text.Trim();

                tbl1.Rows[i]["paidamount"] = ASTUtility.StrPosOrNagative(((TextBox)this.grvacc.Rows[i].FindControl("txtgvpaidamount")).Text.Trim()).ToString();
                tbl1.Rows[i]["disamt"] = ASTUtility.StrPosOrNagative(((TextBox)this.grvacc.Rows[i].FindControl("txtgvdisamt")).Text.Trim()).ToString();
                //tbl1.Rows[i]["paidamount"] = Convert.ToDouble("0" + ((TextBox)this.grvacc.Rows[i].FindControl("txtgvpaidamount")).Text.Trim()).ToString();
                tbl1.Rows[i]["repchqno"] = ((TextBox)this.grvacc.Rows[i].FindControl("txtgvrRpChq")).Text.Trim();
                tbl1.Rows[i]["remarks"] = ((TextBox)this.grvacc.Rows[i].FindControl("txtgvremarks")).Text.Trim();




                //tbl1.Rows[i]["collfrm"] = ((TextBox)this.grvacc.Rows[i].FindControl("txtgvColl")).Text.Trim();
                //tbl1.Rows[i]["recType"] = ((TextBox)this.grvacc.Rows[i].FindControl("txtgvRecType")).Text.Trim();
            }
            Session["sessionforgrid"] = tbl1;


        }
        protected void lbTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.grvacc_DataBind();
        }
        protected void ddlpaytype_SelectedIndexChanged(object sender, EventArgs e)
        {
            string paytype = this.ddlpaytype.SelectedValue.ToString();
            if (paytype == "82002" || paytype == "82007")
            {
                txtpaydate.Text = Convert.ToDateTime(this.txtReceiveDate.Text).ToString("dd-MMM-yyyy");
            }
            this.lblbranch.Visible = (paytype != "82002");
            this.txtBranchName.Visible = (paytype != "82002");
            this.ddlbank.Visible = (paytype != "82002");

            this.lblbankname.Visible = (paytype != "82002");
            this.ddlbank.Visible = (paytype != "82002");

        }
        protected void txtReceiveDate_TextChanged(object sender, EventArgs e)
        {
            string paytype = this.ddlpaytype.SelectedValue.ToString();
            if (paytype == "82002" || paytype == "82007")
            {
                txtpaydate.Text = this.txtReceiveDate.Text.Trim().Length == 0 ? System.DateTime.Today.ToString("dd-MMM-yyyy") : this.txtReceiveDate.Text;
                //txtpaydate.Text = Convert.ToDateTime(this.txtReceiveDate.Text).ToString("dd-MMM-yyyy");
            }
        }
        protected void rbtInsType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }




        protected void chkConsolidate_CheckedChanged(object sender, EventArgs e)
        {
            //AutoPostBack="True"
            this.PayInf();
        }
        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string instype = this.ddlType.SelectedValue.ToString();

            switch (instype)
            {
                case "070000000":
                    this.ddlRecType.SelectedValue = "54001";
                    break;

                case "080000000":
                    this.ddlRecType.SelectedValue = "54050";
                    break;

                case "090000000":
                    this.ddlRecType.SelectedValue = "54002";
                    break;


                case "100000000":
                    this.ddlRecType.SelectedValue = "54003";
                    break;

                case "110000000":
                    this.ddlRecType.SelectedValue = "54005";
                    break;

                case "130000000":
                    this.ddlRecType.SelectedValue = "54010";
                    break;

                case "140000000":
                    this.ddlRecType.SelectedValue = "54008";
                    break;


                case "160000000":
                    this.ddlRecType.SelectedValue = "54016";
                    break;

                case "180000000":
                    this.ddlRecType.SelectedValue = "54004";
                    break;

                case "190000000":
                    this.ddlRecType.SelectedValue = "54006";
                    break;

                case "200000000":
                    this.ddlRecType.SelectedValue = "54012";
                    break;

                case "210000000":
                    this.ddlRecType.SelectedValue = "54015";
                    break;

                case "230000000":
                    this.ddlRecType.SelectedValue = "54020";
                    break;

                case "240000000":
                    this.ddlRecType.SelectedValue = "54009";
                    break;

                case "260000000"://Upgradation
                    this.ddlRecType.SelectedValue = "54017";
                    break;

                case "290000000"://Sales Permission
                    this.ddlRecType.SelectedValue = "54018";
                    break;
                case "270000000":
                    this.ddlRecType.SelectedValue = "54022";
                    break;

                case "280000000":
                    this.ddlRecType.SelectedValue = "54024";
                    break;

                case "300000000":
                    this.ddlRecType.SelectedValue = "54025";
                    break;

                default:
                    this.ddlRecType.SelectedValue = "54003";
                    break;

            }
        }


        private string GetExcelMrNo()
        {

            try
            {
                string comcod = this.GetComCode();
                DataSet ds3 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETNEWMRNO", "", "", "", "", "", "", "", "", "");
                ds3.Dispose();
                return (ds3.Tables[0].Rows[0]["mrno"].ToString());


            }

            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + ex.Message + "');", true);
                return null;

            }



        }

        private string IncrmentExcelMrNo(string mrno)
        {

            string imrno = (Convert.ToInt32(mrno) + 1).ToString();
            return (ASTUtility.Right("000000000" + imrno, 9));
        }
        protected void btnexcuplosd_OnClick(object sender, EventArgs e)
        {

            string comcod = this.GetComCode();
            DataTable dt = ((DataTable)Session["sessionforgrid"]);
            DataTable dt1 = (DataTable)Session["XcelData"];
            string mrno = this.GetExcelMrNo();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string Usircode = this.lblCode.Text.Trim();

            //int mrcount = Convert.ToInt32 ("0" + this.txtnofomrr.Text.Trim ());


            foreach (DataRow dr1 in dt1.Rows)
            {
                if (dr1["MR #"].ToString().Trim().Length == 0)
                    break;



                DataRow dr = dt.NewRow();
                dr["mrno"] = mrno;
                dr["instype"] = "100000000";
                dr["insdesc"] = "";
                dr["paidamount"] = dr1["Amount1"];
                dr["paytype"] = "CHEQUE";
                dr["paytypecod"] = "82001";
                dr["chequeno"] = dr1["Cheque No"];
                dr["bankname"] = dr1["Bank"].ToString().Trim().Length == 0 ? "" : dr1["Bank"].ToString();
                dr["branchname"] = dr1["Branch"].ToString().Trim().Length == 0 ? "" : dr1["Branch"].ToString();

                dr["mrdate"] = Convert.ToDateTime(dr1["Date1"]);
                dr["paydate"] = dr1["CHQ  Date"].ToString().Length == 0 ? Convert.ToDateTime(dr1["Date1"]) : Convert.ToDateTime(dr1["CHQ  Date"]);
                dr["refid"] = dr1["MR #"];
                dr["repchqno"] = "";
                dr["remarks"] = "";
                dr["bookno"] = "";
                dr["collfrm"] = this.ddlCollType.SelectedValue.ToString();
                dr["collfrmd"] = this.ddlCollType.SelectedItem.ToString();
                string Ins = dr1["Insl't 1"].ToString();
                string rectype = "54003";//;Ins=="";
                string rectypdesce = "";//;Ins=="";
                dr["RecType"] = rectype;
                dr["RecTyped"] = Ins;
                dr["disamt"] = 0.00;
                dt.Rows.Add(dr);
                mrno = this.IncrmentExcelMrNo(mrno.Substring(2));
            }

            //Duplicate MRR No
            foreach (DataRow dr1 in dt.Rows)
            {
                string refno = dr1["refid"].ToString();
                string compcod = this.GetComCode();
                switch (compcod)
                {

                    case "3336":
                    case "3340":
                    case "3337":
                    case "3101":

                        DataSet dsd = MktData.GetTransInfo(compcod, "SP_ENTRY_PURCHASE_01", "CHECKMRRREFNO", refno, "", "", "",
                            "", "", "", "", "");





                        if (dsd.Tables[0].Rows.Count == 0)
                            ;


                        else
                        {

                            //DataView dv1 = dsd.Tables[0].DefaultView;
                            //dv1.RowFilter = ("mrno <>'" + mrno + "'");
                            //DataTable dtc = dv1.ToTable();
                            //if (dtc.Rows.Count == 0)
                            ;
                            //else
                            //{
                            ((Label)this.Master.FindControl("lblmsg")).Text = "Found Duplicate M.R No" + refno;
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                            //this.ddlPrevReqList.Items.Clear();
                            return;
                            //  }

                        }

                        break;


                    default:
                        break;

                }


            }




            Session["sessionforgrid"] = dt;
            DataSet ds1 = new DataSet("ds1");
            ds1.Merge(dt);
            ds1.Tables[0].TableName = "tbl1";

            Hashtable hst = (Hashtable)Session["tblLogin"];
            DataTable dtuser = (DataTable)Session["UserLog"];
            //string tblPostedByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postedbyid"].ToString();
            //string tblPostedtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postrmid"].ToString();
            //string tblPostedSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postseson"].ToString();
            //string tblPosteddat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["entrydat"]).ToString("dd-MMM-yyyy hh:mm:ss tt");

            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string PostedByid = userid;
            string Posttrmid = Terminal;
            string PostSession = Sessionid;
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string EditByid = "";
            string Editdat = "01-Jan-1900";

            bool result = MktData.UpdateXmlTransInfo(comcod, "SP_ENTRY_SALSMGT", "INSERTORUPDATEMULMRINF", ds1, null, null, pactcode, Usircode, PostedByid, PostSession, Posttrmid, Posteddat, EditByid, Editdat, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == false)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }



            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            this.btnexcuplosd.Enabled = false;

            // this.Money_DataBind();

            //this.panelexcel.Visible = false;
        }
    }
}


