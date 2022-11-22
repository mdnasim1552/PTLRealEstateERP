using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Data.OleDb;
using System.IO;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPLIB;
using RealERPRPT;
using Microsoft.Reporting.WinForms;
using RealERPRDLC;
namespace RealERPWEB.F_22_Sal
{
    public partial class SalesOpening : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                this.GetopeningNoDate();
                this.GetProjectName();
                this.NameChangee();
                this.ShowOpeningInfo();
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                string type = this.Request.QueryString["Type"].ToString();

                ((Label)this.Master.FindControl("lblTitle")).Text = type == "Consolidate" ? "Sales & Collection Opening (Consolidate)"
                         : type == "Details" ? "Collection Break Down (Project Wise)"
                         : type == "Details02" ? "Collection Break Down (All Project)" : "SALES OPENING INFORMATION ";




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
                    string query = "SELECT * FROM [Sheet1$]";
                    // "SELECT [MRManual],[ChequeNo],[BankName],[BranchName],[PayDate],[PaidAmt],[ReplaceChqNo],[ReconDate] FROM [Sheet1$]";
                    //string query =


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
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        protected void CreateTable()
        {

            DataTable dttemp = new DataTable();
            dttemp.Columns.Add("mrno", Type.GetType("System.String"));
            dttemp.Columns.Add("paidamount", Type.GetType("System.Double"));
            dttemp.Columns.Add("paytype", Type.GetType("System.String"));
            dttemp.Columns.Add("paytypecod", Type.GetType("System.String"));
            dttemp.Columns.Add("chequeno", Type.GetType("System.String"));
            dttemp.Columns.Add("bankname", Type.GetType("System.String"));
            dttemp.Columns.Add("branchname", Type.GetType("System.String"));
            dttemp.Columns.Add("paydate", Type.GetType("System.String"));
            dttemp.Columns.Add("refid", Type.GetType("System.String"));
            dttemp.Columns.Add("repchqno", Type.GetType("System.String"));
            dttemp.Columns.Add("remarks", Type.GetType("System.String"));
            dttemp.Columns.Add("collfrm", Type.GetType("System.String"));
            dttemp.Columns.Add("collfrmd", Type.GetType("System.String"));
            dttemp.Columns.Add("RecType", Type.GetType("System.String"));
            dttemp.Columns.Add("RecTyped", Type.GetType("System.String"));
            dttemp.Columns.Add("billno", Type.GetType("System.String"));
            dttemp.Columns.Add("cactcode", Type.GetType("System.String"));
            dttemp.Columns.Add("cactdesc", Type.GetType("System.String"));
            dttemp.Columns.Add("recndt", Type.GetType("System.String"));
            dttemp.Columns.Add("instype", Type.GetType("System.String"));
            dttemp.Columns.Add("insdesc", Type.GetType("System.String"));

            Session["tblopnmrr"] = dttemp;

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void GetopeningNoDate()
        {

            string comcod = this.GetCompCode();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "SALOPENDATANDNO", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.lblReceiveNo.Text = (ds1.Tables[0].Rows.Count == 0) ? "000000001" : ds1.Tables[0].Rows[0]["mrno"].ToString();
            this.txtOpeningDate.Text = (ds1.Tables[0].Rows.Count == 0) ? System.DateTime.Today.ToString("dd-MMM-yyyy") : Convert.ToDateTime(ds1.Tables[0].Rows[0]["mrdate"]).ToString("dd-MMM-yyyy");
            this.txtOpeningDate.Enabled = (ds1.Tables[0].Rows.Count == 0) ? true : false;
            ds1.Dispose();

        }
        protected void imgbtnFindProject_Click(object sender, EventArgs e)
        {

            this.GetProjectName();
        }
        private void GetProjectName()
        {


            string comcod = this.GetCompCode();
            string PactDesc = "%" + this.txtSearchPro.Text.Trim() + "%";
            string Type = this.Request.QueryString["Type"].ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETPROJECTOPENING", PactDesc, Type, "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            ds1.Dispose();
        }

        private void NameChangee()
        {


            this.gvSOpening.Columns[9].Visible = (this.Request.QueryString["Type"].ToString() == "Consolidate");
            this.gvSOpening.Columns[10].Visible = (this.Request.QueryString["Type"].ToString() == "Details") || (this.Request.QueryString["Type"].ToString() == "Details02");
            this.gvSOpening.Columns[11].Visible = (this.Request.QueryString["Type"].ToString() == "Details") || (this.Request.QueryString["Type"].ToString() == "Details02");


        }
        private void ShowOpeningInfo()
        {
            Session.Remove("tblsopening");
            string comcod = this.GetCompCode();
            string Receiveno = this.lblReceiveNo.Text.Trim();
            string pactcode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            string UnitName = "%" + this.txtSearchUnit.Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "SALOPENINGINFO", Receiveno, pactcode, UnitName, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvSOpening.DataSource = null;
                this.gvSOpening.DataBind();
                return;
            }
            Session["tblsopening"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();



        }


        private void ShowLink()
        {
            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "Details":
                case "Details02":
                    if (this.Request.QueryString["Type"].ToString() == "Details02")
                    {
                        this.lblproject.Visible = false;
                        this.txtSearchPro.Visible = false;
                        this.imgbtnFindProject.Visible = false;
                        this.ddlProjectName.Visible = false;
                        this.Label1.Visible = false;
                        this.txtSearchUnit.Visible = false;
                    }

                    for (int i = 0; i < this.gvSOpening.Rows.Count; i++)
                    {
                        //((TextBox)gvSOpening.Rows[i].FindControl("txtopnamt")).ReadOnly = true ;
                        string pactcode1 = ((Label)gvSOpening.Rows[i].FindControl("lblgvPactcode")).Text.Trim();
                        string usircode = ((Label)gvSOpening.Rows[i].FindControl("lblgvUsircode")).Text.Trim();
                        LinkButton lbtn1 = (LinkButton)gvSOpening.Rows[i].FindControl("lbtopnamtdet");
                        if (lbtn1 != null)
                            lbtn1.CommandArgument = pactcode1 + usircode;
                    }
                    break;
                default:
                    break;



            }




        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string pactcode = dt1.Rows[0]["pactcode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                    dt1.Rows[j]["pactdesc"] = "";

                }

                else
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                }


            }

            return dt1;
        }



        private void SaveValue()
        {
            DataTable dt = (DataTable)Session["tblsopening"];
            int TblRowIndex;
            double schamt;
            for (int i = 0; i < this.gvSOpening.Rows.Count; i++)
            {

                schamt = Convert.ToDouble("0" + ((Label)this.gvSOpening.Rows[i].FindControl("lblgvschamt")).Text.Trim());
                double openingamt = Convert.ToDouble("0" + ASTUtility.ExprToValue(((TextBox)this.gvSOpening.Rows[i].FindControl("txtopnamt")).Text.Trim()));

                if (schamt < openingamt)
                {

                    // ((TextBox)this.gvSOpening.Rows[i].FindControl("txtopnamt")).BackColor = System.Drawing.Color.Red;
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Not Within the Schedule Amount";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

                }


                TblRowIndex = (gvSOpening.PageIndex) * (gvSOpening.PageSize) + i;
                dt.Rows[TblRowIndex]["opnamt"] = openingamt;


            }
            Session["tblsopening"] = dt;

        }


        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblsopening"];
            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "Details":
                case "Details02":
                    this.gvSOpening.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvSOpening.DataSource = dt;
                    this.gvSOpening.DataBind();
                    this.FooterCal();
                    this.ShowLink();
                    break;

                case "Consolidate":
                    this.gvSOpening.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvSOpening.DataSource = dt;
                    this.gvSOpening.DataBind();
                    this.FooterCal();

                    break;



            }





        }

        private void FooterCal()
        {
            DataTable dt = (DataTable)Session["tblsopening"];
            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvSOpening.FooterRow.FindControl("lgvFToAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opnamt)", "")) ? 0.00 : dt.Compute("sum(opnamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvSOpening.FooterRow.FindControl("lgvFToAmt02")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opnamt1)", "")) ? 0.00 : dt.Compute("sum(opnamt1)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvSOpening.FooterRow.FindControl("lgvFToAmtdet")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opndet)", "")) ? 0.00 : dt.Compute("sum(opndet)", ""))).ToString("#,##0;(#,##0); ");




        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string Receiveno = this.lblReceiveNo.Text.Trim();
            string pactcode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            string UnitName = "%" + this.txtSearchUnit.Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "SALOPENINGINFO", Receiveno, pactcode, UnitName, "", "", "", "", "", "");
            if (ds1 == null)
                return;

            DataTable dt1 = HiddenSameData(ds1.Tables[0]);
            
            LocalReport Rpt1 = new LocalReport();
            var lst = dt1.DataTableToList<RealEntity.C_22_Sal.EClassSales.SalesOpening>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptSalesOpening", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));          
            Rpt1.SetParameters(new ReportParameter("printdate", "Date: " + Convert.ToDateTime(this.txtOpeningDate.Text).ToString("dd-MMM-yyyy")));          
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Sales Opening" ));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            this.SaveValue();
            DataTable dt = (DataTable)Session["tblsopening"];
            string comcod = this.GetCompCode();
            string mrno = this.lblReceiveNo.Text.Trim();
            string type = "82001";
            string mrdate = Convert.ToDateTime(this.txtOpeningDate.Text).ToString("dd-MMM-yyyy");
            string paydate = Convert.ToDateTime(this.txtOpeningDate.Text).ToString("dd-MMM-yyyy");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string pactcode = dt.Rows[i]["pactcode"].ToString();
                string usircode = dt.Rows[i]["usircode"].ToString();
                double openingamt = Convert.ToDouble(dt.Rows[i]["opnamt"].ToString());
                bool Result;
                if (openingamt > 0)
                {
                    Result = MktData.UpdateTransInfo01(comcod, "SP_ENTRY_SALSMGT", "INSERTORUPDATEMRINF", pactcode, usircode, mrno, type, mrdate, openingamt.ToString(), "", "", "", paydate, "", "", "", "", "", "", "", "", "", "", "", "", "0");






                    if (Result == false)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                        return;
                    }
                    else
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                    }
                }
                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Sales Opening";
                    string eventdesc = "Update Opening";
                    string eventdesc2 = mrno;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }


            }




        }
        protected void imgbtnFindUnit_Click(object sender, EventArgs e)
        {
            this.ShowOpeningInfo();
        }
        protected void gvSOpening_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvSOpening.PageIndex = e.NewPageIndex;
            this.Data_Bind();

        }
        protected void lbtopnamtdet_Click(object sender, EventArgs e)
        {
            string pactcode = Convert.ToString(((LinkButton)sender).CommandArgument).Trim().Substring(0, 12);
            string usircode = Convert.ToString(((LinkButton)sender).CommandArgument).Trim().Substring(12);
            this.lblPactCodeAUsircode.Text = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            DataTable dt = ((DataTable)Session["tblsopening"]).Copy();
            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = ("pactcode='" + pactcode + "' and usircode='" + usircode + "'");
            dt = dv1.ToTable();

            this.gvSOpening.DataSource = dt;
            this.gvSOpening.DataBind();
            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvSOpening.FooterRow.FindControl("lgvFToAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opnamt)", "")) ? 0.00 : dt.Compute("sum(opnamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvSOpening.FooterRow.FindControl("lgvFToAmtdet")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opndet)", "")) ? 0.00 : dt.Compute("sum(opndet)", ""))).ToString("#,##0;(#,##0); ");


            this.MultiView1.ActiveViewIndex = 0;
            this.ShowOpenMoney();
            this.ShowPaytype();
            this.ShowBillNo();

        }

        private void ShowOpenMoney()
        {
            Session.Remove("tblopnmrr");
            string comcod = this.GetCompCode();
            string pactcode = this.lblPactCodeAUsircode.Text.Substring(0, 12);
            string usircode = this.lblPactCodeAUsircode.Text.Substring(12);
            DataSet ds4 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETOPENMONEYRECPT", pactcode, usircode, "", "", "", "", "", "", "");
            Session["tblopnmrr"] = ds4.Tables[0];
            this.Money_DataBind();
            ds4.Dispose();




        }
        private void ShowPaytype()
        {
            Session.Remove("tblpaytype");
            string comcod = this.GetCompCode();
            DataSet ds4 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "PAYTYPEANDBANK", "", "", "", "", "", "", "", "", "");
            Session["tblpaytype"] = ds4;
            ds4.Dispose();



        }

        private void ShowBillNo()
        {
            string pactcode = this.ddlProjectName.SelectedValue;
            string comcod = this.GetCompCode();
            DataSet ds = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETOPENBILLNUM", pactcode, "", "", "", "", "", "", "", "");
            Session["tblbillno"] = ds.Tables[0];
        }
        protected void lbtnBack_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = -1;
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            this.lblPactCodeAUsircode.Text = "";
            this.ShowOpeningInfo();

        }
        private string GetMrNo()
        {

            string comcod = this.GetCompCode();
            DataSet ds3 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETOPNMRNO", "", "", "", "", "", "", "", "", "");
            return (ds3.Tables[0].Rows[0]["mrno"].ToString());

        }

        private string IncrmentMrNo(string mrno)
        {

            string imrno = (Convert.ToInt32(mrno) + 1).ToString();
            return ("OP" + ASTUtility.Right("0000000" + imrno, 7));
        }



        protected void lbtnGenerate_Click(object sender, EventArgs e)
        {


            DataTable dt = ((DataTable)Session["tblopnmrr"]);
            string mrno = this.GetMrNo();

            int mrcount = Convert.ToInt32("0" + this.txtnofomrr.Text.Trim());
            for (int i = 0; i < mrcount; i++)
            {
                DataRow dr = dt.NewRow();
                dr["mrno"] = mrno;
                dr["paidamt"] = 0.00;
                dr["paytype"] = "";
                dr["paytypecod"] = "";
                dr["chequeno"] = "";
                dr["bankname"] = "";
                dr["bbranch"] = "";
                dr["paydate"] = "";
                dr["refno"] = "";
                dr["repchqno"] = "";
                dr["rmrks"] = "";
                dr["collfrm"] = "";
                dr["collfrmd"] = "";
                dr["RecType"] = "";
                dr["RecTyped"] = "";
                dr["cactcode"] = "";
                dr["cactdesc"] = "";
                dr["recndt"] = "";
                dr["instype"] = "";
                dr["insdesc"] = "";
                dr["billno"] = "";


                dt.Rows.Add(dr);
                mrno = this.IncrmentMrNo(mrno.Substring(2));

            }
            Session["tblopnmrr"] = dt;
            this.Money_DataBind();
            this.pnlgenMrr.Visible = false;
        }


        private void Money_DataBind()
        {
            DataTable dt = (DataTable)Session["tblopnmrr"];
            this.gvMoney.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvMoney.DataSource = dt;
            this.gvMoney.DataBind();
            if (dt.Rows.Count > 1)
                ((Label)this.gvMoney.FooterRow.FindControl("lblgvFPaidamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(paidamt)", "")) ? 0.00 : dt.Compute("sum(paidamt)", ""))).ToString("#,##0;(#,##0); ");







        }

        protected void chkVisible_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkVisible.Checked == true)
            {

                this.pnlgenMrr.Visible = true;

            }
            else
            {
                this.pnlgenMrr.Visible = false;
            }

        }
        protected void lbTotal_Click(object sender, EventArgs e)
        {
            this.SaveMoneyValue();
            this.Money_DataBind();
        }


        private void SaveMoneyValue()
        {

            DataTable tbl1 = (DataTable)Session["tblopnmrr"];
            for (int i = 0; i < this.gvMoney.Rows.Count; i++)
            {
                tbl1.Rows[i]["chequeno"] = ((TextBox)this.gvMoney.Rows[i].FindControl("txtgvChequeno")).Text.Trim();
                tbl1.Rows[i]["bankname"] = ((TextBox)this.gvMoney.Rows[i].FindControl("txtgvbankna")).Text.Trim();
                tbl1.Rows[i]["bbranch"] = ((TextBox)this.gvMoney.Rows[i].FindControl("txtgvBrance")).Text.Trim();
                tbl1.Rows[i]["paydate"] = ((TextBox)this.gvMoney.Rows[i].FindControl("txtgvpaydate")).Text.Trim();
                tbl1.Rows[i]["refno"] = ((TextBox)this.gvMoney.Rows[i].FindControl("txtgvrefid")).Text.Trim();
                tbl1.Rows[i]["paidamt"] = ASTUtility.StrPosOrNagative(((TextBox)this.gvMoney.Rows[i].FindControl("txtgvpaidamount")).Text.Trim()).ToString();
                tbl1.Rows[i]["repchqno"] = ((TextBox)this.gvMoney.Rows[i].FindControl("txtgvrRpChq")).Text.Trim();
                tbl1.Rows[i]["rmrks"] = ((TextBox)this.gvMoney.Rows[i].FindControl("txtgvremarks")).Text.Trim();
                tbl1.Rows[i]["recndt"] = ((TextBox)this.gvMoney.Rows[i].FindControl("txtgvrecndate")).Text.Trim();



            }
            Session["tblopnmrr"] = tbl1;

        }
        protected void gvMoney_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)Session["tblopnmrr"];
            int rowindex = (this.gvMoney.PageSize) * (this.gvMoney.PageIndex) + e.RowIndex;
            string mrrno = dt.Rows[rowindex]["mrno"].ToString();
            string pactcode = this.lblPactCodeAUsircode.Text.Substring(0, 12);
            string usircode = this.lblPactCodeAUsircode.Text.Substring(12);
            string Chequeno = dt.Rows[rowindex]["chequeno"].ToString();

            bool result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "DELETEMROPNREFNO", mrrno, pactcode, usircode, Chequeno, "", "", "", "", "", "", "", "", "", "", "");

            if (result)
            {

                dt.Rows[rowindex].Delete();

            }

            Session.Remove("tblopnmrr");
            DataView dv = dt.DefaultView;
            // dv.RowFilter = ("mrno<>''");
            Session["tblopnmrr"] = dv.ToTable();
            this.Money_DataBind();
        }
        protected void gvMoney_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvMoney.EditIndex = -1;
            this.Money_DataBind();
        }
        protected void gvMoney_RowEditing(object sender, GridViewEditEventArgs e)
        {

            this.SaveMoneyValue();
            this.gvMoney.EditIndex = e.NewEditIndex;
            this.Money_DataBind();


            DataSet ds4 = (DataSet)Session["tblpaytype"];
            DataTable dtbill = (DataTable)Session["tblbillno"];
            string reconbank = ((Label)this.gvMoney.Rows[e.NewEditIndex].FindControl("lblgvreconbank")).Text.Trim();
            string PayType = ((Label)this.gvMoney.Rows[e.NewEditIndex].FindControl("lbgvpaytype")).Text.Trim();
            string Collfrm = ((Label)this.gvMoney.Rows[e.NewEditIndex].FindControl("lblgvColl")).Text.Trim();
            string RecType = ((Label)this.gvMoney.Rows[e.NewEditIndex].FindControl("lblgvRecType")).Text.Trim();
            string insType = ((Label)this.gvMoney.Rows[e.NewEditIndex].FindControl("lblgvinsType")).Text.Trim();
            string Billno = ((Label)this.gvMoney.Rows[e.NewEditIndex].FindControl("lblgvBillnomain")).Text.Trim();

            DropDownList ddlgvreconbank = (DropDownList)this.gvMoney.Rows[e.NewEditIndex].FindControl("ddlgvreconbank");
            ddlgvreconbank.DataTextField = "cactdesc";
            ddlgvreconbank.DataValueField = "cactcode";
            ddlgvreconbank.DataSource = ds4.Tables[3];
            ddlgvreconbank.DataBind();
            ddlgvreconbank.SelectedValue = (reconbank == "") ? "290200020002" : reconbank;

            DropDownList ddlpaytype = (DropDownList)this.gvMoney.Rows[e.NewEditIndex].FindControl("ddlgvpaytype");
            ddlpaytype.DataTextField = "gdesc";
            ddlpaytype.DataValueField = "gcod";
            ddlpaytype.DataSource = ds4.Tables[0];
            ddlpaytype.DataBind();
            ddlpaytype.SelectedValue = PayType;

            DropDownList ddlgvColl = (DropDownList)this.gvMoney.Rows[e.NewEditIndex].FindControl("ddlgvColl");
            ddlgvColl.DataTextField = "gdesc";
            ddlgvColl.DataValueField = "gcod";
            ddlgvColl.DataSource = ds4.Tables[1];
            ddlgvColl.DataBind();
            ddlgvColl.SelectedValue = (Collfrm == "") ? "53061001001" : Collfrm;


            DropDownList ddlgvRecType = (DropDownList)this.gvMoney.Rows[e.NewEditIndex].FindControl("ddlgvRecType");
            ddlgvRecType.DataTextField = "gdesc";
            ddlgvRecType.DataValueField = "gcod";
            ddlgvRecType.DataSource = ds4.Tables[2];
            ddlgvRecType.DataBind();
            ddlgvRecType.SelectedValue = (RecType == "") ? "54003" : RecType;




            DropDownList ddlgvinsType = (DropDownList)this.gvMoney.Rows[e.NewEditIndex].FindControl("ddlgvinsType");
            ddlgvinsType.DataTextField = "gdesc";
            ddlgvinsType.DataValueField = "gcod";
            ddlgvinsType.DataSource = ds4.Tables[4];
            ddlgvinsType.DataBind();
            ddlgvinsType.SelectedValue = (insType == "") ? "100000000" : insType;

            DropDownList ddlgvbillno = (DropDownList)this.gvMoney.Rows[e.NewEditIndex].FindControl("ddlgvBillNo");
            ddlgvbillno.DataTextField = "billno1";
            ddlgvbillno.DataValueField = "billno";
            ddlgvbillno.DataSource = dtbill;
            ddlgvbillno.DataBind();
            ddlgvbillno.SelectedValue = Billno;// (Billno == "") ? "100000000" : insType;


            //DataTable tbl1 = (DataTable)Session["tblopnmrr"];
            //int index= e.NewEditIndex;
            //tbl1.Rows[index]["chequeno"] = ((TextBox)this.gvMoney.Rows[e.NewEditIndex].FindControl("txtgvChequeno")).Text.Trim();
            //tbl1.Rows[index]["bankname"] = ((TextBox)this.gvMoney.Rows[e.NewEditIndex].FindControl("txtgvbankna")).Text.Trim();
            //tbl1.Rows[index]["bbranch"] = ((TextBox)this.gvMoney.Rows[e.NewEditIndex].FindControl("txtgvBrance")).Text.Trim();
            //tbl1.Rows[index]["paydate"] = ((TextBox)this.gvMoney.Rows[e.NewEditIndex].FindControl("txtgvpaydate")).Text.Trim();
            //tbl1.Rows[index]["refno"] = ((TextBox)this.gvMoney.Rows[e.NewEditIndex].FindControl("txtgvrefid")).Text.Trim();
            //tbl1.Rows[index]["paidamt"] = ASTUtility.StrPosOrNagative(((TextBox)this.gvMoney.Rows[e.NewEditIndex].FindControl("txtgvpaidamount")).Text.Trim()).ToString();
            //tbl1.Rows[index]["repchqno"] = ((TextBox)this.gvMoney.Rows[e.NewEditIndex].FindControl("txtgvrRpChq")).Text.Trim();
            //tbl1.Rows[index]["rmrks"] = ((TextBox)this.gvMoney.Rows[e.NewEditIndex].FindControl("txtgvremarks")).Text.Trim();
            //tbl1.Rows[index]["recndt"] = ((TextBox)this.gvMoney.Rows[e.NewEditIndex].FindControl("txtgvrecndate")).Text.Trim();
            //Session["tblopnmrr"] = tbl1;

        }
        protected void gvMoney_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            DataTable tbl1 = (DataTable)Session["tblopnmrr"];
            string cactcode = ((DropDownList)this.gvMoney.Rows[e.RowIndex].FindControl("ddlgvreconbank")).SelectedValue.ToString();
            string cactdesc = ((DropDownList)this.gvMoney.Rows[e.RowIndex].FindControl("ddlgvreconbank")).SelectedItem.ToString();
            string Paytypecod = ((DropDownList)this.gvMoney.Rows[e.RowIndex].FindControl("ddlgvpaytype")).SelectedValue.ToString();
            string Paytype = ((DropDownList)this.gvMoney.Rows[e.RowIndex].FindControl("ddlgvpaytype")).SelectedItem.ToString();
            string Collfrm = ((DropDownList)this.gvMoney.Rows[e.RowIndex].FindControl("ddlgvColl")).SelectedValue.ToString();
            string Collfrmdesc = ((DropDownList)this.gvMoney.Rows[e.RowIndex].FindControl("ddlgvColl")).SelectedItem.ToString();
            string RecType = ((DropDownList)this.gvMoney.Rows[e.RowIndex].FindControl("ddlgvRecType")).SelectedValue.ToString();
            string RecTypedesc = ((DropDownList)this.gvMoney.Rows[e.RowIndex].FindControl("ddlgvRecType")).SelectedItem.ToString();
            string BillNo1 = ((DropDownList)this.gvMoney.Rows[e.RowIndex].FindControl("ddlgvBillNo")).Text;
            string BillNo = ((DropDownList)this.gvMoney.Rows[e.RowIndex].FindControl("ddlgvBillNo")).SelectedValue.ToString();

            string InsType = ((DropDownList)this.gvMoney.Rows[e.RowIndex].FindControl("ddlgvinsType")).SelectedValue.ToString();
            string InsTypedesc = ((DropDownList)this.gvMoney.Rows[e.RowIndex].FindControl("ddlgvinsType")).SelectedItem.ToString();
            string SchCode = this.GetSchCode(InsType);
            int index = (this.gvMoney.PageIndex) * this.gvMoney.PageSize + e.RowIndex;


            tbl1.Rows[index]["chequeno"] = ((TextBox)this.gvMoney.Rows[e.RowIndex].FindControl("txtgvChequeno")).Text.Trim();
            tbl1.Rows[index]["bankname"] = ((TextBox)this.gvMoney.Rows[e.RowIndex].FindControl("txtgvbankna")).Text.Trim();
            tbl1.Rows[index]["bbranch"] = ((TextBox)this.gvMoney.Rows[e.RowIndex].FindControl("txtgvBrance")).Text.Trim();
            tbl1.Rows[index]["paydate"] = ((TextBox)this.gvMoney.Rows[e.RowIndex].FindControl("txtgvpaydate")).Text.Trim();
            tbl1.Rows[index]["refno"] = ((TextBox)this.gvMoney.Rows[e.RowIndex].FindControl("txtgvrefid")).Text.Trim();
            tbl1.Rows[index]["paidamt"] = ASTUtility.StrPosOrNagative(((TextBox)this.gvMoney.Rows[e.RowIndex].FindControl("txtgvpaidamount")).Text.Trim()).ToString();
            tbl1.Rows[index]["repchqno"] = ((TextBox)this.gvMoney.Rows[e.RowIndex].FindControl("txtgvrRpChq")).Text.Trim();
            tbl1.Rows[index]["rmrks"] = ((TextBox)this.gvMoney.Rows[e.RowIndex].FindControl("txtgvremarks")).Text.Trim();
            tbl1.Rows[index]["recndt"] = ((TextBox)this.gvMoney.Rows[e.RowIndex].FindControl("txtgvrecndate")).Text.Trim() == "" ?
                    ((TextBox)this.gvMoney.Rows[e.RowIndex].FindControl("txtgvpaydate")).Text.Trim() : ((TextBox)this.gvMoney.Rows[e.RowIndex].FindControl("txtgvrecndate")).Text.Trim();
            tbl1.Rows[index]["cactcode"] = cactcode;
            tbl1.Rows[index]["cactdesc"] = cactdesc;
            tbl1.Rows[index]["paytypecod"] = Paytypecod;
            tbl1.Rows[index]["paytype"] = Paytype;
            tbl1.Rows[index]["collfrm"] = Collfrm;
            tbl1.Rows[index]["collfrmd"] = Collfrmdesc;
            tbl1.Rows[index]["RecType"] = RecType;
            tbl1.Rows[index]["RecTyped"] = RecTypedesc;
            tbl1.Rows[index]["billno1"] = BillNo1;
            tbl1.Rows[index]["billno"] = BillNo;

            tbl1.Rows[index]["instype"] = InsType;
            tbl1.Rows[index]["insdesc"] = InsTypedesc;
            tbl1.Rows[index]["schcode"] = SchCode;
            Session["tblopnmrr"] = tbl1;
            this.gvMoney.EditIndex = -1;
            this.Money_DataBind();
        }
        protected void lbtnUpdateMoney_Click(object sender, EventArgs e)
        {
            this.SaveMoneyValue();
            DataTable dt1 = (DataTable)Session["tblopnmrr"];
            string comcod = this.GetCompCode();
            string pactcode = this.lblPactCodeAUsircode.Text.Substring(0, 12);
            string usircode = this.lblPactCodeAUsircode.Text.Substring(12);
            bool result = true;
            //string  mrno = this.GetMrNo();
            foreach (DataRow dr in dt1.Rows)
            {
                string mrno = dr["mrno"].ToString();
                string type = dr["paytypecod"].ToString();
                // string type = dr["paytypecod"].ToString();
                string mrdate = ASTUtility.DateFormat(dr["paydate"].ToString());
                string Recndate = ASTUtility.DateFormat(dr["recndt"].ToString());

                //DateTime Opndate = Convert.ToDateTime(this.txtOpeningDate.Text);
                //bool dcon = ASITUtility02.TransactionDateOpening(Opndate, Convert.ToDateTime(mrdate));
                //if (!dcon)
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('MR  Date less than Opening Date');", true);
                //    return;
                //}


                //dcon = ASITUtility02.TransactionDateOpening(Opndate, Convert.ToDateTime(Recndate));
                //if (!dcon)
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Reconcillation Date is  less  than Opening Date');", true);
                //    return;
                //}





                double paidamt = Convert.ToDouble(dr["paidamt"]);
                string chqno = dr["chequeno"].ToString();
                string bname = dr["bankname"].ToString();
                string branchname = dr["bbranch"].ToString();
                string paydate = ASTUtility.DateFormat(dr["paydate"].ToString());
                string refno = dr["refno"].ToString();
                string repchqno = dr["repchqno"].ToString();
                string remrks = dr["rmrks"].ToString();//
                string schcode = dr["schcode"].ToString();
                string Collfrm = dr["collfrm"].ToString();
                string RecType = dr["recType"].ToString();
                string billno = dr["billno"].ToString();
                string cactcode = dr["cactcode"].ToString();

                paidamt = (RecType == "54097") ? paidamt * -1 : paidamt;


                if (paidamt != 0)
                    result = MktData.UpdateTransInfo01(comcod, "SP_ENTRY_SALSMGT", "INSERTORUPDATEMROINF", pactcode, usircode, mrno, type, mrdate, paidamt.ToString(), chqno,
                                                      bname, branchname, paydate, refno, remrks, schcode, repchqno, Collfrm, RecType, cactcode, Recndate, billno, "", "");

                if (result == false)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                    return;
                }
                else
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                }
                //mrno = this.IncrmentMrNo(mrno.Substring(2));
            }

            ((LinkButton)this.gvMoney.FooterRow.FindControl("lbtnUpdateMoney")).Enabled = false;



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
            }

            if (SchCode == "")
                return "";

            return SchCode;


            //string instype = (this.Request.QueryString["Type"] == "CustCare") ? dt1.Rows[i]["instype"].ToString() : "";
            //SchCode = this.GetSchCode(instype);

            //this.ddlType.SelectedValue = ((dr1[0]["schcode"].ToString() == "") ? "10" : (dr1[0]["schcode"].ToString().Substring(0, 5) == "81988") ? "11" : (dr1[0]["schcode"].ToString().Substring(0, 5) == "81990") ? "13" :
            //      (dr1[0]["schcode"].ToString().Substring(0, 5) == "81991") ? "14" : (dr1[0]["schcode"].ToString().Substring(0, 5) == "81993") ? "16" : "10") + "0000000";


        }
        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowOpeningInfo();
        }

        protected void btnexcuplosd_OnClick(object sender, EventArgs e)
        {
            DataTable dt = ((DataTable)Session["tblopnmrr"]);
            DataTable dt1 = (DataTable)Session["XcelData"];
            string mrno = this.GetMrNo();

            //int mrcount = Convert.ToInt32 ("0" + this.txtnofomrr.Text.Trim ());


            foreach (DataRow dr1 in dt1.Rows)
            {
                if (dr1["MRManual"].ToString().Trim().Length == 0)
                    break;

                DataRow dr = dt.NewRow();
                dr["mrno"] = mrno;
                dr["paidamt"] = Convert.ToDouble(dr1["PaidAmt"]).ToString("#,##0;(#,##0); ");
                dr["paytype"] = "";
                dr["paytypecod"] = "";
                dr["chequeno"] = dr1["ChequeNo"];
                dr["bankname"] = dr1["BankName"];
                dr["bbranch"] = dr1["BranchName"];
                dr["paydate"] = Convert.ToDateTime(dr1["PayDate"]).ToString("dd-MMM-yyyy");
                dr["refno"] = dr1["MRManual"];
                dr["repchqno"] = dr1["ReplaceChqNo"];
                dr["rmrks"] = "";
                dr["collfrm"] = "";
                dr["collfrmd"] = "";
                dr["RecType"] = "";
                dr["RecTyped"] = "";
                dr["billno"] = "";
                dr["cactcode"] = "";
                dr["cactdesc"] = "";
                dr["recndt"] = (dr1["ReconDate"].ToString().Trim().Length == 0) ? "01-jan-1900" : Convert.ToDateTime(dr1["ReconDate"]).ToString("dd-MMM-yyyy");
                dr["instype"] = "";
                dr["insdesc"] = "";
                dt.Rows.Add(dr);



                mrno = this.IncrmentMrNo(mrno.Substring(2));
            }



            Session["tblopnmrr"] = dt;
            this.Money_DataBind();

            this.panelexcel.Visible = false;

            //for (int i = 0; i < mrcount; i++)
            //{
            //    DataRow dr = dt.NewRow ();
            //    dr["mrno"] = mrno;
            //    dr["paidamt"] = 0.00;
            //    dr["paytype"] = "";
            //    dr["paytypecod"] = "";
            //    dr["chequeno"] = "";
            //    dr["bankname"] = "";
            //    dr["bbranch"] = "";
            //    dr["paydate"] = "";
            //    dr["refno"] = "";
            //    dr["repchqno"] = "";
            //    dr["rmrks"] = "";
            //    dr["collfrm"] = "";
            //    dr["collfrmd"] = "";
            //    dr["RecType"] = "";
            //    dr["RecTyped"] = "";
            //    dr["cactcode"] = "";
            //    dr["cactdesc"] = "";
            //    dr["recndt"] = "";
            //    dr["instype"] = "";
            //    dr["insdesc"] = "";


            //    dt.Rows.Add (dr);
            //    mrno = this.IncrmentMrNo (mrno.Substring (2));

            //}




            //((Label)this.Master.FindControl ("lblmsg")).Text = "Updated Successfully";
            //ScriptManager.RegisterStartupScript (this, GetType (), "alert", "HideLabel(1);", true);

        }
        protected void chkexcel_OnCheckedChanged(object sender, EventArgs e)
        {
            if (this.chkexcel.Checked == true)
            {

                this.panelexcel.Visible = true;

            }
            else
            {
                this.panelexcel.Visible = false;
            }


        }
        protected void gvSOpening_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtopnamt = (TextBox)e.Row.FindControl("txtopnamt");

                //double schamt = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "schamt"));
                double schamt = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "schamt"));
                double opnamt = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "opnamt"));
                if (schamt < opnamt)
                {

                    txtopnamt.Attributes["style"] = "color:red; text-align:right;";
                }


            }

        }
    }
}