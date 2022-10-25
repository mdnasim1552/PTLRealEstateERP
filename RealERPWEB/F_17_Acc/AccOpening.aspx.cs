using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using CrystalDecisions.ReportSource;
using RealERPLIB;
using RealERPRPT;
using System.Data.OleDb;
using Microsoft.Reporting.WinForms;

namespace RealERPWEB.F_17_Acc
{
    public partial class AccOpening : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");

                DataRow[] dr = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                // ViewState["empid"] = dr[0].ItemArray[2].ToString();
                ViewState["formid"] = dr[0].ItemArray[3].ToString();
                //ViewState["formname"] = dr[0].ItemArray[4].ToString();

                ((Label)this.Master.FindControl("lblTitle")).Text = "Account's Opening";
                CommonButton();
                //ViewState["formname"] = dr[0].ItemArray[4].ToString();
                //lnkbtnHelp.OnClientClick = this.getHelpDocUrl();

                //this.BtnLink.Attributes.Add("href", "http://cloudasit-001-site11.itempurl.com/Interior_Help/account-manual.html#item-2-3");
            }
            if (this.dgv2.Rows.Count == 0)
            {
                Session.Remove("AccTbl01");
                this.GridLoad();
                this.DateSet();
                this.pnlsub.Visible = false;
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
                        StrFileName = fileuploadExcel.PostedFile.FileName.Substring(fileuploadExcel.PostedFile.FileName.LastIndexOf("\\") + 1);
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
                        connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    }
                    else if (strFileType.Trim() == ".xlsx")
                    {
                        connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties='Excel 12.0 Xml;HDR=YES;'";
                    }

                    //string query = "SELECT [Product],[Category],[Qty(Pcs)],[Value],[Unit Price],[ERP CODE] FROM [Sheet1$]";
                    string query = "SELECT [actcode],[actdesc],[resdesc],[rescode],[spclcode],[specification],[unit],[Dr],[Cr],[qty] FROM [Sheet1$]";
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

        public void CommonButton()
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace("%20", " "), (DataSet)Session["tblusrlog"]);
            ((Label)this.Master.FindControl("lblmsg")).Visible = false;
            ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;


            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            ((CheckBox)this.Master.FindControl("CheckBox1")).Visible = false;

            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = false;
            ((LinkButton)this.Master.FindControl("btnClose")).Text = "Back";
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkbtnSave_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(LnkfTotal_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(lnkSubmit_Click);



        }


        protected void btnexcuplosd_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string vounum = GetVou();
            string date1 = this.txtdate.Text.Substring(0, 11);

            DataTable dt = (DataTable)Session["XcelData"];
            dt.TableName = "tbl";
            DataSet ds = new DataSet();
            ds.DataSetName = "dst";
            ds.Merge(dt);
            DataSet ds4 = accData.GetTransInfoNew(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVOUPLOADFROMEXL", ds, null, null, vounum, date1, "", "", "", "", "", "");

            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            this.GridLoad();
        }
        private void DateSet()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string datepart;
            DataSet ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETOPENINGDATE", "", "", "", "", "", "", "", "", "");
            DataTable dt4 = ds4.Tables[0];
            if (dt4.Rows.Count == 0)
            {
                datepart = "";
            }
            else
            {
                datepart = Convert.ToDateTime(dt4.Rows[0]["voudat"]).ToString("dd-MMM-yyyy ddd");
            }
            if (datepart == "")
            {
                this.txtdate.Text = datepart.ToString();
                this.txtdate.Enabled = true;
            }
            else
            {
                this.txtdate.Text = datepart;
                this.txtdate.Enabled = false;
            }
        }
        private void GridLoad()
        {
            Session.Remove("AccTbl01");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string filter = "%" + this.txtFilter.Text.Trim() + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETOPENINGACC", filter, "", "", "", "", "", "", "", "");
            Session["AccTbl01"] = ds1.Tables[0];
            this.dgv2_DataBind();

        }
        protected void ImageButton1_Click(object sender, EventArgs e)
        {
            this.GridLoad();

        }
        private void SessionUpdate()
        {

            DataTable tblt01 = (DataTable)Session["AccTbl01"];
            int TblRowIndex;

            for (int i = 0; i < this.dgv2.Rows.Count; i++)
            {
                double dgvTrnDrAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv2.Rows[i].FindControl("txtgvDrAmt")).Text.Trim()));
                double dgvTrnCrAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv2.Rows[i].FindControl("txtgvCrAmt")).Text.Trim()));
                //string dgvTrnRemarks = ((TextBox)this.dgv2.Rows[i].FindControl("txtgvRemarks")).Text.Trim();

                string gvtxtmaingvrmks = ((TextBox)this.dgv2.Rows[i].FindControl("gvtxtmaingvrmks")).Text.Trim();

                TblRowIndex = (dgv2.PageIndex) * dgv2.PageSize + i;

                tblt01.Rows[TblRowIndex]["Dr"] = dgvTrnDrAmt;
                tblt01.Rows[TblRowIndex]["Cr"] = dgvTrnCrAmt;
                tblt01.Rows[TblRowIndex]["opnnar"] = gvtxtmaingvrmks;

                //  tblt01.Rows[TblRowIndex]["Remarks"] = dgvTrnRemarks;
            }
            Session["AccTbl01"] = tblt01;
        }
        private void SessionUpdate2()
        {

            DataTable tblt02 = (DataTable)Session["AccTbl02"];
            int TblRowIndex2;

            for (int j = 0; j < this.dgv3.Rows.Count; j++)
            {
                // double dgvTrnRate;
                double dgvTrnQty = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.dgv3.Rows[j].FindControl("gvtxtQty")).Text.Trim()));
                // double dgvTrnRate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv3.Rows[j].FindControl("gvtxtRate")).Text.Trim()));
                double dgvTrnDrAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv3.Rows[j].FindControl("gvtxtDrAmt")).Text.Trim()));
                double dgvTrnCrAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv3.Rows[j].FindControl("gvtxtCrAmt")).Text.Trim()));
                double dgvTrnRate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv3.Rows[j].FindControl("txtgvRate")).Text.Trim()));
                string rmrks = ((TextBox)this.dgv3.Rows[j].FindControl("gvtxtgvremarks")).Text.Trim();

                //if (dgvTrnDrAmt == 0 && dgvTrnCrAmt == 0)
                //{
                //    dgvTrnRate = 0;
                //}
                //else
                //{
                //    dgvTrnRate = (dgvTrnQty == 0 ? 0.00 : (dgvTrnDrAmt + dgvTrnCrAmt) / dgvTrnQty);
                //}


                dgvTrnRate = (dgvTrnDrAmt) > 0 ? (Math.Abs(dgvTrnQty) > 0 ? (dgvTrnDrAmt) / Math.Abs(dgvTrnQty) : 0.00) : dgvTrnRate;
                dgvTrnDrAmt = dgvTrnDrAmt > 0 ? dgvTrnDrAmt : dgvTrnRate * Math.Abs(dgvTrnQty);

                //   dgvTrnDrAmt = dgvTrnQty > 0 ? dgvTrnQty * dgvTrnRate : dgvTrnDrAmt;







                // dgvTrnCrAmt = dgvTrnRate > 0 ? dgvTrnQty * dgvTrnRate : dgvTrnCrAmt;

                ((TextBox)this.dgv3.Rows[j].FindControl("txtgvRate")).Text = dgvTrnRate.ToString("#,##0.00;(#,##0.00); ");
                ((TextBox)this.dgv3.Rows[j].FindControl("gvtxtDrAmt")).Text = dgvTrnDrAmt.ToString("#,##0.00;(#,##0.00); ");
                ((TextBox)this.dgv3.Rows[j].FindControl("gvtxtCrAmt")).Text = dgvTrnCrAmt.ToString("#,##0.00;(#,##0.00); ");
                TblRowIndex2 = (dgv3.PageIndex) * dgv3.PageSize + j;
                tblt02.Rows[TblRowIndex2]["qty"] = dgvTrnQty;
                tblt02.Rows[TblRowIndex2]["rate"] = dgvTrnRate;
                tblt02.Rows[TblRowIndex2]["Dr"] = dgvTrnDrAmt;
                tblt02.Rows[TblRowIndex2]["Cr"] = dgvTrnCrAmt;
                tblt02.Rows[TblRowIndex2]["rmrks"] = rmrks;


            

            }
            Session["AccTbl02"] = tblt02;

            this.dgv3_DataBind();
        }
        protected void dgv2_RowCreated(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onclick", "this.className='normalrow'");

                //e.Row.Attributes.Add("onmouseover", "this.className='highlightrow'");
                //e.Row.Attributes.Add("onmouseout", "this.className='normalrow'");
            }

        }
        protected void gvlnkLevel_Click(object sender, EventArgs e)
        {
            this.ddlpagem.Visible = false;
            this.lblpagem.Visible = false;
            Session.Remove("AccTbl02");
            this.SessionUpdate();
        }
        private void ShowResource()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string acccode01 = this.txtActcode.Text.Trim().Substring(0, 12);
            //string acttype=  (((DataTable)Session["AccTbl01"]).Select("actcode='" + acccode01 + "'"))[0]["acttype"].ToString(); 

            string filter2 = "%" + this.txtResSearch.Text.Trim() + "%";
            string SearchInfo = "";
            string type = (((DataTable)Session["AccTbl01"]).Select("actcode='" + acccode01 + "'"))[0]["acttype"].ToString();
            if (type.Length > 0)
            {

                int len;
                string[] ar = type.Split('/');
                foreach (string ar1 in ar)
                {


                    if (ar1.Contains("-"))
                    {
                        len = ar1.IndexOf("-");
                        SearchInfo = SearchInfo + "left(a.sircode,'" + len + "') between " + ar1.Trim().Replace("-", " and ") + " ";
                    }
                    else
                    {
                        len = ar1.Length;


                        SearchInfo = SearchInfo + "left(a.sircode,'" + len + "')" + " = " + "'" + ar1 + "' ";
                        //SearchInfo = SearchInfo + "left(a.sircode,'" + len + "')" + " = " + ar1 + " ";
                    }
                    SearchInfo = SearchInfo + " or ";

                }
                if (SearchInfo.Length > 0)
                    SearchInfo = "(" + SearchInfo.Substring(0, SearchInfo.Length - 3) + ")";
            }



            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETOPENINGRES", filter2, acccode01, SearchInfo, "", "", "", "", "", "");
            Session["AccTbl02"] = this.HiddenSameData(ds2.Tables[0]);
            this.dgv3_DataBind();
            this.dgv2.Visible = false;
            this.pnlsub.Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;

        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string rescode = dt1.Rows[0]["rescode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["rescode"].ToString() == rescode)
                    dt1.Rows[j]["resdesc"] = "";

                rescode = dt1.Rows[j]["rescode"].ToString();
            }
            return dt1;

        }

        protected void gvlnkFTotal_Click(object sender, EventArgs e)
        {
            this.SessionUpdate2();
            this.dgv3_DataBind();
        }
        private void TotalCalculation2()
        {
            this.SessionUpdate2();

        }
        protected void ImageButton2_Click(object sender, EventArgs e)
        {
            this.ShowResource();
        }
        protected void dgv2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Session.Remove("RowIndex");
            if (e.CommandName == "")
            {
                GridViewRow row = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
                if (row.RowType.ToString() == "DataRow")
                {
                    GridViewRow gvr = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                    int RowIndex = gvr.RowIndex;
                    Session["RowIndex"] = RowIndex;
                    this.lblacccode1.Visible = false;
                    this.txtFilter.Visible = false;
                    this.ImageButton1.Visible = false;
                    this.ShowActCode();
                    gvr.BackColor = System.Drawing.Color.Blue;

                }
            }
        }
        private void ShowActCode()
        {
            int rowin = (int)Session["RowIndex"];
            int rowin1 = (dgv2.PageIndex * dgv2.PageSize) + rowin;
            this.txtActcode.Text = ((Label)this.dgv2.Rows[rowin].FindControl("lblAccdesc")).Text;
            this.ShowResource();
        }
        protected void lnkSubmit_Click(object sender, EventArgs e)
        {

            this.GridLoad();
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            this.pnlsub.Visible = false;
            this.dgv2.Visible = true;
            this.lblacccode1.Visible = true;
            this.txtFilter.Visible = true;
            this.ImageButton1.Visible = true;
            this.ddlpagem.Visible = true;
            this.lblpagem.Visible = true;


            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = false;

            //int Rowindex1 = (int)Session["RowIndex"];
            //int Rowindex2 = (dgv2.PageSize * dgv2.PageIndex) + Rowindex1;
            //DataTable tblt03 = (DataTable)Session["AccTbl01"];
            //DataTable tblt03d = (DataTable)Session["AccTbl02"];
            //double Dramt = Convert.ToDouble((Convert.IsDBNull(tblt03d.Compute("Sum(dr)", "")) ? 0.00 : tblt03d.Compute("Sum(dr)", "")));
            //double Cramt = Convert.ToDouble((Convert.IsDBNull(tblt03d.Compute("Sum(cr)", "")) ? 0.00 : tblt03d.Compute("Sum(cr)", "")));
            // //    double Cramt= Convert.ToDouble("0" + ((TextBox)this.dgv3.FooterRow.FindControl("gvtxtftCramt")).Text);
            //double Amt=(Dramt - Cramt);
            //tblt03.Rows[Rowindex2]["Dr"] = (Amt > 0 ? Amt : 0);
            //tblt03.Rows[Rowindex2]["Cr"] = (Amt < 0 ? (Amt * (-1)) : 0);

            //Session["AccTbl01"] = tblt03;
            //this.dgv2_DataBind();
            //this.Panel2.Visible = false;


            //this.TotalCalculation1();

        }


        private void UpdateTable02()
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            this.SessionUpdate2();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string UserId = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string EditDate = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

            string vounum = GetVou();
            string actcode = this.txtActcode.Text.Trim().Substring(0, 12);
            string cactcode = "000000000000";

            string vtcode = "00";
            string voudat = this.txtdate.Text.Substring(0, 11);
            DataTable tblt03 = (DataTable)Session["AccTbl02"];


            DataTable dt = tblt03.Copy();
            dt.Columns.Remove("resdesc");
            dt.Columns.Remove("resunit");
            dt.Columns.Remove("spcfdesc");
            DataView dv = dt.DefaultView;
            dv.RowFilter = "Dr <> 0 or Cr<>0 or qty<>0";
            dt = dv.ToTable();

            DataSet ds1 = new DataSet("ds1");
            ds1.Tables.Add(dt);
            ds1.Tables[0].TableName = "tbl1";
            string xml = ds1.GetXml();
            bool result = accData.UpdateXmlTransInfo (comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVOPNUPDATEAXML", ds1, null, null, vounum, actcode, cactcode, voudat, vtcode, UserId, EditDate, Terminal);

            if (!result)
            {
                // string error = accData.ErrorObject["Msg"].ToString().Trim();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Update Failed..!!');", true);
                return;
            }

            //t1.Columns.Remove("flrdes");
            //dt1.Columns.Remove("rsirdesc");
            //dt1.Columns.Remove("rsirdesc1");
            //dt1.Columns.Remove("rsirunit");


            //for (int i = 0; i < tblt03.Rows.Count; i++)
            //{
            //    string rescode = tblt03.Rows[i]["rescode"].ToString();
            //    string spcfcod = tblt03.Rows[i]["spcfcod"].ToString();
            //    string trnqty = tblt03.Rows[i]["qty"].ToString();
            //    double Dramt = Convert.ToDouble(tblt03.Rows[i]["Dr"]);
            //    double Cramt = Convert.ToDouble(tblt03.Rows[i]["Cr"]);
            //    string trnamt = Convert.ToString(Dramt - Cramt);
            //    string trnremark = Convert.ToDouble(Dramt).ToString();
            //    string rmrks = tblt03.Rows[i]["rmrks"].ToString();


            //    //if ((Dramt - Cramt) != 0)
            //    //{
            //    bool resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVOPNUPDATEA", vounum, actcode,
            //            rescode, cactcode, voudat, trnqty, trnremark, vtcode, trnamt, spcfcod, UserId, EditDate, Terminal, rmrks, "");
            //    if (!resulta)
            //    {
            //        // string error = accData.ErrorObject["Msg"].ToString().Trim();
            //        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Fail.');", true);
            //        return;
            //    }
            //    //}     
            //}


            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully.');", true);
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Account Opening";
                string eventdesc = "Update Resourc Balance";
                string eventdesc2 = vounum;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        private string GetVou()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string date1 = this.txtdate.Text.Substring(0, 11);
            DataSet ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETOPNVOUCHER", date1, "", "", "", "", "", "", "", "");
            DataTable dt4 = ds4.Tables[0];
            string ss = dt4.Rows[0]["couvounum"].ToString();
            //string dd = ss.Substring(12);
            if (ss.Substring(12) == "00")
            {
                string vounum = ss.Substring(0, 12) + "01";
                //-----------Update Transaction B Table-----------------//
                try
                {
                    bool resultb = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACOPNUPDATE", vounum, date1,
                                "", "", "", "", "Accounts Opening", "00", "", "", "", "", "", "", "");
                    if (!resultb)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        //return ;
                    }
                    ss = vounum;
                }
                catch (Exception e)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + e.Message;
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                }
                //-------------------------------------------------------//

            }
            return ss;
        }


        private void lnkbtnSave_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            //---------------Check Dr. and Cr------//
            this.SessionUpdate();
            DataTable tblt07 = (DataTable)Session["AccTbl01"];
            for (int i = 0; i < tblt07.Rows.Count; i++)
            {
                double Dramt01 = Convert.ToDouble(tblt07.Rows[i]["Dr"]);
                double Cramt01 = Convert.ToDouble(tblt07.Rows[i]["Cr"]);
                if (Dramt01 > 0 && Cramt01 > 0)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Choose Only Dr. Or Cr. Amount.";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
                //else
                //{


                //}
            }
            //-------------------------//

            this.UpdateTable01();

        }
        private void UpdateTable01()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];

                string comcod = hst["comcod"].ToString();
                string UserId = hst["usrid"].ToString();
                string EditDate = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                string Terminal = hst["compname"].ToString();
                string vounum1 = GetVou();
                string cactcode = "000000000000";
                string spclcode = "000000000000";
                string rescode = "000000000000";
                string vtcode = "00";
                string trnqty = "0";

                string voudat = this.txtdate.Text.Substring(0, 11);
                DataTable tblt05 = (DataTable)Session["AccTbl01"];
                for (int i = 0; i < tblt05.Rows.Count; i++)
                {
                    string actcode = tblt05.Rows[i]["actcode"].ToString();
                    string actlev = tblt05.Rows[i]["actelev"].ToString();
                    double Dramt = Convert.ToDouble(tblt05.Rows[i]["Dr"]);
                    double Cramt = Convert.ToDouble(tblt05.Rows[i]["Cr"]);
                    string trnamt = Convert.ToString(Dramt - Cramt);
                    string trnremarks = Convert.ToDouble(Dramt).ToString();
                    string opnnar = tblt05.Rows[i]["opnnar"].ToString();

                    //if ((Dramt - Cramt) != 0 && actlev != "2")
                    if (actlev != "2")
                    {
                        bool resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER",
                                "ACVOPNUPDATEA", vounum1, actcode, rescode, cactcode, voudat, trnqty,
                                trnremarks, vtcode, trnamt, spclcode, UserId, EditDate, Terminal, opnnar, "");
                        if (!resulta)
                        {
                            //((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                            //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Fail.');", true);

                            return;
                        }

                    }
                }
                //   ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully.";
                //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Successfully.');", true);
                //   if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Account Opening";
                    string eventdesc = "Update Balance";
                    string eventdesc2 = vounum1;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }

            }
            catch (Exception e)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + e.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }

        }
        private void LnkfTotal_Click(object sender, EventArgs e)
        {
            this.SessionUpdate();
            this.dgv2_DataBind();
        }
        private void TotalCalculation1()
        {

            DataTable tblt06 = (DataTable)Session["AccTbl01"];
            if (tblt06.Rows.Count == 0)
                return;
            double DrAmt = Convert.ToDouble((Convert.IsDBNull(tblt06.Compute("Sum(Dr)", "")) ? 0.00 : tblt06.Compute("Sum(Dr)", "")));
            double CrAmt = Convert.ToDouble((Convert.IsDBNull(tblt06.Compute("Sum(Cr)", "")) ? 0.00 : tblt06.Compute("Sum(Cr)", "")));

            ((TextBox)this.dgv2.FooterRow.FindControl("txtTgvDrAmt")).Text = DrAmt.ToString("#,##0.00;(#,##0.00);  ");
            ((TextBox)this.dgv2.FooterRow.FindControl("txtTgvCrAmt")).Text = CrAmt.ToString("#,##0.00;(#,##0.00);  ");



            double Balance = DrAmt - CrAmt;
            ((Label)this.dgv2.FooterRow.FindControl("lblgvFDr")).Text = (Balance > 0) ? "" : Math.Abs(Balance).ToString("#,##0.00;(#,##0.00);  ");
            ((Label)this.dgv2.FooterRow.FindControl("lblgvFCr")).Text = (Balance < 0) ? "" : Math.Abs(Balance).ToString("#,##0.00;(#,##0.00);  ");
        }



        protected void dgv2ddlPageNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SessionUpdate();
            this.dgv2.PageIndex = ((DropDownList)this.dgv2.FooterRow.FindControl("dgv2ddlPageNo")).SelectedIndex;
            this.dgv2_DataBind();



        }
        protected void dgv2_DataBind()
        {
            DataTable tbl1 = (DataTable)Session["AccTbl01"];
            this.dgv2.PageSize = Convert.ToInt32(this.ddlpagem.SelectedValue.ToString());
            this.dgv2.DataSource = tbl1;
            this.dgv2.DataBind();
            if (tbl1.Rows.Count == 0)
                return;

            this.TotalCalculation1();
            //((DropDownList)this.dgv2.FooterRow.FindControl("dgv2ddlPageNo")).Visible = false;
            //double TotalPage = Math.Ceiling(tbl1.Rows.Count * 1.00 / this.dgv2.PageSize);
            //((DropDownList)this.dgv2.FooterRow.FindControl("dgv2ddlPageNo")).Items.Clear();
            //for (int i = 1; i <= TotalPage; i++)
            //    ((DropDownList)this.dgv2.FooterRow.FindControl("dgv2ddlPageNo")).Items.Add("Page: " + i.ToString() + " of " + TotalPage.ToString());
            //if (TotalPage > 1)
            //    ((DropDownList)this.dgv2.FooterRow.FindControl("dgv2ddlPageNo")).Visible = true;
            //((DropDownList)this.dgv2.FooterRow.FindControl("dgv2ddlPageNo")).SelectedIndex = this.dgv2.PageIndex;
        }

        // protected void dgv3ddlPageNo_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    this.SessionUpdate2();

        //    this.dgv3.PageIndex = ((DropDownList)this.dgv3.FooterRow.FindControl("dgv3ddlPageNo")).SelectedIndex;
        //    //((DropDownList)this.dgv3.FooterRow.FindControl("dgv3ddlPageNo")).SelectedIndex
        //    this.dgv3_DataBind();
        //    this.TotalCalculation2();
        //}

        protected void dgv3_DataBind()
        {
            DataTable tblt03 = (DataTable)Session["AccTbl02"];
            this.dgv3.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.dgv3.DataSource = tblt03;
            this.dgv3.DataBind();
            if (tblt03.Rows.Count == 0)
                return;
            Session["Report1"] = dgv3;
            if (tblt03.Rows.Count > 0)
            {
                ((HyperLink)this.dgv3.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }

            ((TextBox)this.dgv3.FooterRow.FindControl("gvtxtftDramt")).Text = Convert.ToDouble((Convert.IsDBNull(tblt03.Compute("Sum(Dr)", "")) ?
            0.00 : tblt03.Compute("Sum(Dr)", ""))).ToString("#,##0.00;(#,##0.00);  ");
            ((TextBox)this.dgv3.FooterRow.FindControl("gvtxtftCramt")).Text = Convert.ToDouble((Convert.IsDBNull(tblt03.Compute("Sum(Cr)", "")) ?
            0.00 : tblt03.Compute("Sum(Cr)", ""))).ToString("#,##0.00;(#,##0.00);  ");

        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SessionUpdate2();
            this.dgv3_DataBind();
        }
        protected void dgv3_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SessionUpdate2();
            this.dgv3.PageIndex = e.NewPageIndex;
            this.dgv3_DataBind();
        }
        protected void lnkbtnUpdateRes_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            this.UpdateTable02();

        }
        protected void dgv3_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];

            string comcod = hst["comcod"].ToString();
            string UserId = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string EditDate = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");



            int rowindex = (this.dgv3.PageSize) * (this.dgv3.PageIndex) + e.RowIndex;
            DataTable dt = (DataTable)Session["AccTbl02"];
            string vounum1 = GetVou();
            string actcode = this.txtActcode.Text.Trim().Substring(0, 12);

            string rescode = dt.Rows[rowindex]["rescode"].ToString();
            string spcfcod = dt.Rows[rowindex]["spcfcod"].ToString();
            string cactcode = "000000000000";
            bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "DELATEOPENACTRNA", vounum1, actcode, rescode, spcfcod, cactcode, UserId, Terminal, EditDate, "", "", "", "", "", "");

            if (result == true)
            {

                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            Session.Remove("AccTbl02");
            Session["AccTbl02"] = dv.ToTable();
            this.dgv3_DataBind();


        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        protected void lbtndelete_Click(object sender, EventArgs e)
        {

            GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            int rownum = gvr.RowIndex;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string UserId = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string EditDate = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            int rowindex = (this.dgv2.PageSize) * (this.dgv2.PageIndex) + rownum;
            string comcod = this.GetCompCode();



            string vounum1 = GetVou();
            string cactcode = "000000000000";
            string spcfcod = "000000000000";
            string rescode = "000000000000";
            string vtcode = "00";
            string trnqty = "0";
            string voudat = this.txtdate.Text.Substring(0, 11);
            DataTable tblt05 = (DataTable)Session["AccTbl01"];
            string actcode = tblt05.Rows[rowindex]["actcode"].ToString();
            string actlev = tblt05.Rows[rowindex]["actelev"].ToString();
            if (actlev != "2")
            {
                bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "DELATEOPENACTRNA", vounum1, actcode, rescode, spcfcod, cactcode, UserId, Terminal, EditDate, "", "", "", "", "", "");

                if (result == true)
                {

                    tblt05.Rows[rowindex].Delete();
                }
            }
            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Please Select Main Head";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);


            }

            DataView dv = tblt05.DefaultView;
            Session.Remove("AccTbl01");
            Session["AccTbl01"] = dv.ToTable();
            this.dgv2_DataBind();

        }
        protected void dgv2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SessionUpdate();
            this.dgv2.PageIndex = e.NewPageIndex;
            this.dgv2_DataBind();
        }

        protected void ddlpagem_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SessionUpdate();
            this.dgv2_DataBind();
        }
        protected void lnkbtnHelp_Click(object sender, EventArgs e)
        {

            string formid = ViewState["formid"].ToString();
            DataSet ds3 = accData.GetTransInfo("3101", "[dbo].[SP_POPULATE_HELP_DOCUMENTATION]", "GETHELPDOCUMENTS", formid);
            if (ds3.Tables[0].Rows.Count == 0)
                return;
            DataTable dt3 = ds3.Tables[0];
            string domain = ds3.Tables[0].Rows[0]["domain"].ToString();
            string url = ds3.Tables[0].Rows[0]["url"].ToString();
            string data = domain + url;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "showwindow('" + data + "');", true);
        }


        private string getHelpDocUrl()
        {
            // string helpid = lnkbtnHelp.CommandArgument.ToString();     
            string helpid = "";
            DataSet ds3 = accData.GetTransInfo("3101", "[dbo].[SP_POPULATE_HELP_DOCUMENTATION]", "GETHELPBYHELPID", helpid);
            if (ds3.Tables[0].Rows.Count == 0)
                return "";
            DataTable dt3 = ds3.Tables[0];
            //string helpid = ds3.Tables[0].Rows[0]["helpid"].ToString();
            string helpdesc = ds3.Tables[0].Rows[0]["helpdesc"].ToString();
            string domain = ds3.Tables[0].Rows[0]["domain"].ToString();
            string url = ds3.Tables[0].Rows[0]["url"].ToString();
            string data = domain + url;
            // comcod,helpid,helpdesc,domain,url,remarks,formid,formname
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "showwindow('" + data + "');", true);
            return data;
        }


        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            string openingDate = this.txtdate.Text;
            string reportType = this.GetReportType();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string branch = hst["combranch"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string fdr= ((Label)this.dgv2.FooterRow.FindControl("lblgvFDr")).Text;
            string fcr= ((Label)this.dgv2.FooterRow.FindControl("lblgvFCr")).Text;
            string acchead = "";
           
            for (var i = 0; i < dgv2.Rows.Count; i++)
            {
                string ah = ((Label)this.dgv2.Rows[i].FindControl("lblAccdesc")).Text;
                acchead = ah.Remove(0, 13);
            }


            if (fdr == "")
            {
                fdr = "0";
            }
            if (fcr == "")
            {
                fcr = "0";
            }


            LocalReport Rpt1 = new LocalReport();
            if(reportType == "Summary")
            {
                DataTable dt = (DataTable)Session["AccTbl01"];
                DataView dv = dt.DefaultView;
                dv.RowFilter = ("Dr>0 or Cr>0");
                dt = dv.ToTable();
                var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassAccounts.AccOpening>();

                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptAccOpening", list, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("openingDate", openingDate));
                Rpt1.SetParameters(new ReportParameter("comnam", comnam));
                Rpt1.SetParameters(new ReportParameter("comadd", comadd));
                Rpt1.SetParameters(new ReportParameter("RptTitle", "Opening Voucher"));
                Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
                Rpt1.SetParameters(new ReportParameter("comLogo", ComLogo));
                Rpt1.SetParameters(new ReportParameter("branch", branch));
                Rpt1.SetParameters(new ReportParameter("fdr", fdr));
                Rpt1.SetParameters(new ReportParameter("fcr", fcr));
            }
            else
            {
                DataTable dt = (DataTable)Session["AccTbl02"];
                DataView dv = dt.DefaultView;
                dv.RowFilter = ("Dr>0 or Cr>0");
                dt = dv.ToTable();
                var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassAccounts.AccOpening>();

                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptAccOpeningDetails", list, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("openingDate", openingDate));
                Rpt1.SetParameters(new ReportParameter("comnam", comnam));
                Rpt1.SetParameters(new ReportParameter("comadd", comadd));
                Rpt1.SetParameters(new ReportParameter("RptTitle", "Opening Voucher Details"));
                Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
                Rpt1.SetParameters(new ReportParameter("comLogo", ComLogo));
                Rpt1.SetParameters(new ReportParameter("branch", branch));
                Rpt1.SetParameters(new ReportParameter("acchead", acchead));

            }








            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private string GetReportType()
        {
            string Type = "";
            int index = this.ddreportType.SelectedIndex;
            switch (index)
            {
                case 0:
                    Type = "Summary";
                    break;

                case 1:
                    Type = "Details";
                    break;

                default:
                    Type = "Summary";
                    break;
            }
            return Type;
        }

    }
}
