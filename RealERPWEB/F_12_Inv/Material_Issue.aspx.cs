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

using System.Data.OleDb;
//using MFGLIB;
//using MFGRPT;
//using System.IO;
//using System.Data.OleDb;
//using MFGOBJ.C_22_Salfg;
namespace RealERPWEB.F_12_Inv
{
    public partial class Material_Issue : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        static string prevPage = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (prevPage.Length == 0)
                {
                    prevPage = Request.UrlReferrer.ToString();
                }
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

                ((Label)this.Master.FindControl("lblTitle")).Text = "MATERIALS ISSUE STATUS";

                // this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.txtCurDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetProjectName();
                this.GetDeparment();
                this.GetEmployeeList();
                this.CommonButton();
                if (this.Request.QueryString["genno"].ToString().Length > 0)
                {
                    this.PreList();
                }

                if (this.Request.QueryString["Type"].ToString() == "Link")
                {
                    this.GetIssuenfo(); ;
                    this.IndentIssue();
                }

            }
            if (fileuploadExcel.HasFile)
            {
                try
                {
                    Session.Remove("XcelData");

                    string connString = "";
                    string StrFileName = string.Empty;
                    if (fileuploadExcel.PostedFile != null && fileuploadExcel.PostedFile.FileName != "")
                    {
                        StrFileName = fileuploadExcel.PostedFile.FileName.Substring(fileuploadExcel.PostedFile.FileName.LastIndexOf("\\") + 1);
                        string StrFileType = fileuploadExcel.PostedFile.ContentType;
                        int IntFileSize = fileuploadExcel.PostedFile.ContentLength;
                        if (IntFileSize <= 0)
                        {
                            //  this.lmsg.Text = "Uploading Fail";
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
                            //   this.lmsg.Text = "Uploading Successfully";
                        }
                    }

                    string strFileType = Path.GetExtension(fileuploadExcel.FileName).ToLower();
                    string apppath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath.ToString();
                    string path = apppath + "ExcelFile\\" + StrFileName;
                    //  string path = Server.MapPath("~") + ("ExcelFile\\" + StrFileName);

                    //Connection String to Excel Workbook
                    if (strFileType.Trim() == ".xls")
                    {
                        connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    }
                    else if (strFileType.Trim() == ".xlsx")
                    {

                        connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties='Excel 12.0 Xml;HDR=YES;'";
                    }
                    string typr = this.Request.QueryString["Type"].ToString();
                    string query = string.Empty;
                    string query2 = string.Empty;
                    if (typr == "main" || typr == "Entry")
                    {
                        query = "select * from [Sheet1$]";
                        // query = "SELECT [Division],[ID],[Position],[DBCode],[Territory],[TSM],[1301],[1302],[1303],[1304],[1305],[1306],[1307],[1308],[1309],[1310],[1311],[1312],[1313],[1314],[1315],[1316],[1317],[1318],[1319],[1320],[201],[202],[203],[204],[205],[206],[207],[208],[209],[601],[602],[603],[1101],[1102],[1103],[1104],[1105],[1106],[1107],[1108],[1109],[1110],[1111],[1112],[1113],[1114],[1115],[1116],[1117],[1118] FROM [Sheet1$]";
                        //  query2 = "SELECT [1301],[1302],[1303],[1304],[1305],[1306],[1307],[1308],[1309],[1310],[1311],[1312],[1313],[13014],[1315],[1316],[1317],[1318],[1319],[1320],[201],[202],[203],[204] FROM [Sheet1$]";
                        //,[201],[202],[203],[204]
                    }
                    else
                    {
                        query = "SELECT [Code],[Name],[ProCod],[Qty],[Rate],[Amount] FROM [Sheet1$]";
                    }

                    OleDbConnection conn = new OleDbConnection(connString);
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    OleDbCommand cmd = new OleDbCommand(query, conn);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    DataView dv = ds.Tables[0].DefaultView;
                    dv.RowFilter = ("Name <> ''");
                    DataTable dt = dv.ToTable();
                    Session["XcelData"] = dv.ToTable();
                    // this.DataInsert();
                    da.Dispose();
                    conn.Close();
                    conn.Dispose();
                    //this.GetExelData();

                    if (typr == "main")
                    {
                        this.PrepairData();
                    }
                    else if (typr == "Entry")
                    {
                        this.PrepairDataProwise();
                    }
                    else
                    {
                        this.Databind();
                    }

                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }

        protected void PrepairData()
        {
            //List<string> procods = new List<string>();
            //List<TargetDetails> Datalst = new List<TargetDetails>();
            //List<TargetDetails> Finallst = new List<TargetDetails>();


            //DataTable dt = (DataTable)Session["XcelData"];

            //for (int i = 11; i < dt.Columns.Count; i++)
            //{
            //    procods.Add(dt.Columns[i].ColumnName.ToString());
            //}

            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    string id1 = dt.Rows[i]["id"].ToString();
            //    string dbcode1 = dt.Rows[i]["DBCode"].ToString();
            //    Datalst.Add(new TargetDetails() { id = id1, dbcode = dbcode1 });
            //}

            //foreach (var item in Datalst)
            //{

            //    foreach (var item1 in procods)
            //    {
            //        Double qty1 = dt.AsEnumerable().Where(row => row.Field<string>("id").ToString() == item.id && row.Field<string>("DBCode").ToString() == item.dbcode).Select(row => row.Field<Double>(item1)).First();

            //        Finallst.Add(new TargetDetails() { id = item.id, dbcode = item.dbcode, proid = item1, proqty = qty1.ToString() });
            //    }

            //}

            //DataTable fdt = ASITUtility03.ListToDataTable((List<TargetDetails>)Finallst);
            //Session["XcelDatamfg"] = fdt;
        }
        protected void PrepairDataProwise()
        {
            //List<string> procods = new List<string>();
            //List<IndentIssueItem> Datalst = new List<IndentIssueItem>();
            //List<IndentIssueItem> Finallst = new List<IndentIssueItem>();


            //DataTable dt = (DataTable)Session["XcelData"];

            //for (int i = 3; i < dt.Columns.Count; i++)
            //{
            //    procods.Add(dt.Columns[i].ColumnName.ToString());
            //}

            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    string id1 = dt.Rows[i]["id"].ToString();
            //    string Name = dt.Rows[i]["Name"].ToString();
            //    string remark = dt.Rows[i]["REMARKS"].ToString();
            //    Datalst.Add(new IndentIssueItem() { id = id1, name = Name, remarks = remark });
            //}

            //foreach (var item in Datalst)
            //{

            //    foreach (var item1 in procods)
            //    {
            //        Double qty1 = dt.AsEnumerable().Where(row => row.Field<string>("id").ToString() == item.id).Select(row => row.Field<Double>(ASTUtility.Right(item1, 3))).First();
            //        var temp = dt.AsEnumerable().Where(row => row.Field<double>("id") == Convert.ToDouble(item.id)).Select(row => row.Field<double>(item1)).First();
            //        double qty1 = Convert.ToDouble("0" + dt.AsEnumerable().Where(row => row.Field<double>("id") == Convert.ToDouble(item.id)).Select(row => row.Field<double>(item1)).First());
            //        string fqty = Convert.ToString(qty1);
            //        Finallst.Add(new IndentIssueItem() { id = item.id, name = item.name, issueqty = fqty, rsircode = item1, remarks = item.remarks });
            //    }
            //}

            //DataTable fdt = ASITUtility03.ListToDataTable((List<IndentIssueItem>)Finallst);
            //Session["XcelDatamfg"] = fdt;
        }

        protected void Databind()
        {
            DataTable dt = (DataTable)Session["XcelData"];

            this.gvIssue.DataSource = dt;
            this.gvIssue.DataBind();
        }


        private void CommonButton()
        {

            ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;


            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;


            //
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;

            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            //((LinkButton)this.Master.FindControl("btnClose")).Visible = false;
            if (this.Request.QueryString["Type"] == "Approve")
            {
                ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = true;
                ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Text = "Approve";
                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Text = "Update";
                ((LinkButton)this.Master.FindControl("lnkbtnLedger")).OnClientClick = "return confirm('Do you want to Approve?');";
                //     ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;
                //   ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            }


        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lbtnUpdate_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Click += new EventHandler(lnkbtnNew_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lbtnTotal_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Click += new EventHandler(lnkbtnLedger_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }
        private void lnkbtnLedger_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            string posteddat = DateTime.Today.ToString("dd-MMM-yyyy");

            string isuno = this.Request.QueryString["genno"].ToString();
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_MATERIAL_ISSUE", "APPROVEINDENTISSUE", isuno, usrid, sessionid, trmid, posteddat);
            if (result == true)
            {
                Response.Redirect(prevPage);
                // ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Successfully Approved');", true);
            }

        }
        protected void lnkbtnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("../F_07_Inv/Material_Issue.aspx?Type=Entry&genno=");
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../F_07_Inv/Material_Issue.aspx?Type=Entry&genno=" + "', target='_self');</script>";
        }
        protected DateTime GetBackDate()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "GETBDATE", "", "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {

                return (System.DateTime.Today);
            }

            return (Convert.ToDateTime(ds2.Tables[0].Rows[0]["bdate"]));
        }
        private void lbtnUpdate_Click(object sender, EventArgs e)
        {
            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            //if (!Convert.ToBoolean(dr1[0]["entry"]))
            //{
            //    this.lblmsg1.Text = "You have no permission";
            //    return;
            //}
            this.SaveValue();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string PostedByid = hst["usrid"].ToString();
            string Posttrmid = hst["compname"].ToString();
            string PostSession = hst["session"].ToString();
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblIssue"];
            string curdate = this.txtCurDate.Text.ToString().Trim();
            string curdate1 = Convert.ToDateTime(this.txtCurDate.Text.ToString().Trim()).ToString("dd-MM-yyyy");
            string formatdate = Convert.ToString(curdate1);
            DateTime Bdate = this.GetBackDate();
            bool dcon = ASITUtility02.TransactionDateCon(Bdate, Convert.ToDateTime(curdate));
            if (!dcon)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Issue Date is equal or less Current Date');", true);
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                ((Label)this.Master.FindControl("lblANMgsBox")).Visible = true;
                return;
            }
            
            
            string Refno = this.txtrefno.Text.ToString();
            if (Refno.Length == 0)
            {
                this.lblmsg1.Text = "Ref. No. Should Not Be Empty";
                return;
            }

            if (this.ddlPreList.Items.Count == 0)
                this.GetLSDNo();

            string Issueno = (Request.QueryString["Type"])== "Link"? this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + formatdate.Trim().Substring(3, 2) + this.txtCurNo2.Text.ToString().Trim()
                : this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.txtCurNo2.Text.ToString().Trim();

            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "CHECKEDDUPINDREFNO", Refno, "", "", "", "", "", "", "", "");
            if (ds2.Tables[0].Rows.Count == 0) ;


            else
            {

                DataView dv1 = ds2.Tables[0].DefaultView;
                dv1.RowFilter = ("issueno <>'" + Issueno + "'");
                DataTable dt1 = dv1.ToTable();
                if (dt1.Rows.Count == 0)
                    ;
                else
                {
                    this.lblmsg1.Text = "Found Duplicate Ref. No.";
                    //this.ddlPrevReqList.Items.Clear();
                    return;
                }
            }

         
            string pactcode = this.ddlProject.SelectedValue.ToString();
            string reqno = (this.Request.QueryString["sircode"].Length == 0) ? "" : this.Request.QueryString["sircode"].ToString();
            bool result;
            result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "INSORUPTXTTTOEMPINF", "indissueb", Issueno, curdate, Refno, PostedByid, Posttrmid, PostSession, Posteddat, pactcode, reqno, "", "", "", "");


            foreach (DataRow dr in dt.Rows)
            {
                string rsircode = dr["rsircode"].ToString().Trim();
                string spcfcod = dr["spcfcod"].ToString().Trim();
                string deptcode = dr["deptcode"].ToString().Trim();
                string issueqty = dr["issueqty"].ToString().Trim();
                //string refundqty = dr["refundqty"].ToString().Trim();
                string issueamt = dr["issueamt"].ToString().Trim();
                string empid = dr["empid"].ToString().Trim();
                string remarks = dr["remarks"].ToString().Trim();

                result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "INSORUPTXTTTOEMPINF", "indissuea", Issueno, rsircode, spcfcod,
                   deptcode, issueqty, issueamt, empid, remarks, "", "", "", "", "", "");
            }

            if (Request.QueryString["Type"] == "Link")
            {
                DataTable tb21 = (DataTable)ViewState["tbltolissue"];
                string status = "Complated";
                string issuno = tb21.Rows[0]["indentissuno"].ToString();
                result = purData.UpdateTransInfo(comcod, "SP_REPORT_INDENT_STATUS", "INSERTINTOINDENTLOG", issuno, PostedByid, curdate, Posttrmid, PostSession, status);


            }

            //string issuno = tb21.Select();
       


            this.lblmsg1.Text = "Updated Successfully";
            this.txtCurDate.Enabled = false;

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }


        private void GetProjectName()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = (hst["usrid"].ToString());
            string comcod = this.GetCompCode();

            // DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO02", "GETPROJECT", Srchname, "", "", "", "", "", "", "", "");
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_REQ_CENSTORE", "PRJCODELIST", "11020099%", "FxtAst", "", userid, "", "", "", "", "");
            if (ds2 == null)
                return;
            ViewState["tblStoreType"] = ds2.Tables[0];
            this.ddlProject.DataTextField = "actdesc1";
            this.ddlProject.DataValueField = "actcode";
            this.ddlProject.DataSource = ds2.Tables[0];
            this.ddlProject.DataBind();


        }

        private void IndentIssue()
        {
            string comcod = this.GetCompCode();
           
            this.lbtnOk_Click(null, null);
            this.GetMatList();
            this.ImgbtnSpecification_Click(null, null);
            this.lbtnSelectAll_Click(null, null);



        }

        private void GetMatList()
        {
            string comcod = this.GetCompCode();
            string mProject = Request.QueryString["Type"].ToString() == "Link" ? Request.QueryString["prjcode"].ToString() : this.ddlProject.SelectedValue.ToString();
            string mSrchTxt = "%";
            string date = this.txtCurDate.Text.Trim();
            DataTable dt = (DataTable)ViewState["tblStoreType"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("actcode='" + mProject + "'");
            dt = dv.ToTable();
            string Codetype = dt.Rows[0]["acttype"].ToString();
            string SearchInfo = "";
            string reqno = "";
            if (Request.QueryString["Type"].ToString() == "Link")
            {
                string pactcode = Request.QueryString["prjcode"].ToString();
                reqno = (this.Request.QueryString["sircode"].Length == 0) ? this.Request.QueryString["sircode"].ToString() : " ";


            }
            else
            {
                reqno = (this.Request.QueryString["sircode"].Length == 0) ? "" : this.Request.QueryString["sircode"].ToString();

            }

            if (Codetype.Length > 0)
            {

                int len;
                string[] ar = Codetype.Split('/');
                foreach (string ar1 in ar)
                {


                    if (ar1.Contains("-"))
                    {
                        len = ar1.IndexOf("-");
                        SearchInfo = SearchInfo + "left(sircode,'" + len + "') between " + ar1.Trim().Replace("-", " and ") + " ";
                    }
                    else
                    {
                        len = ar1.Length;

                        SearchInfo = SearchInfo + "left(sircode,'" + len + "')" + " = " + ar1 + " ";
                    }
                    SearchInfo = SearchInfo + " or ";

                }
                if (SearchInfo.Length > 0)
                    SearchInfo = "(" + SearchInfo.Substring(0, SearchInfo.Length - 3) + ")";
            }




            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "INDENTGETMATLIST", mProject, mSrchTxt, date, reqno, "", "", "", "", "");

            if (ds1 == null)
            {
                this.ddlResList.Items.Clear();
                // this.ddlResSpcf.Items.Clear();
                return;
            }
            ViewState["tblMat"] = ds1.Tables[0];

            ViewState["tblSpcf"] = ds1.Tables[2];
            this.ddlResList.DataTextField = "rsirdesc";
            this.ddlResList.DataValueField = "rsircode";
            this.ddlResList.DataSource = ds1.Tables[1];
            this.ddlResList.DataBind();


            this.ImgbtnSpecification_Click(null, null);
            //this.GetSpecification();


        }



        private void GetSpecification()
        {



            //string mResCode = this.ddlResList.SelectedValue.ToString();
            //this.ddlResSpcf.Items.Clear();
            //DataTable tbl1 = (DataTable)ViewState["tblMat"];
            //DataView dv1 = tbl1.DefaultView;
            //dv1.RowFilter = "rsircode = '" + mResCode + "'";
            //DataTable dt = dv1.ToTable();
            //this.ddlResSpcf.DataTextField = "spcfdesc";
            //this.ddlResSpcf.DataValueField = "spcfcod";
            //this.ddlResSpcf.DataSource = dt;
            //this.ddlResSpcf.DataBind();


        }
        protected void GetDeparment()
        {
            string comcod = this.GetCompCode();

            string department = this.Request.QueryString["Type"].ToString() == "Link" ? this.Request.QueryString["sircode"].ToString() + "%" : "%%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "FXTASSTGETDEPARTMENT", department, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ds1.Tables[0].Rows.Add(comcod, "000000000000", "Department");
            ds1.Tables[0].Rows.Add(comcod, "AAAAAAAAAAAA", "-------Select-----------");


            this.ddlDeptCode.DataTextField = "fxtgdesc";
            this.ddlDeptCode.DataValueField = "fxtgcod";
            this.ddlDeptCode.DataSource = ds1.Tables[0];
            this.ddlDeptCode.DataBind();
            this.ddlDeptCode.SelectedValue = this.Request.QueryString["Type"].ToString() == "Link" ? this.Request.QueryString["sircode"].ToString() : "AAAAAAAAAAAA";
            if (this.Request.QueryString["prjcode"].Length > 0)
            {
                string deptcode = this.Request.QueryString["prjcode"].ToString();

                this.ddlDeptCode.SelectedValue = deptcode;

            }
        }
        private void GetEmployeeList()
        {
            string comcod = this.GetCompCode();

            string dpcode = this.ddlDeptCode.SelectedValue.ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GETEMPNAME", "%", dpcode, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.ddlEmpList.Items.Clear();
                return;
            }


            this.ddlEmpList.DataTextField = "sirdesc";
            this.ddlEmpList.DataValueField = "sircode";
            this.ddlEmpList.DataSource = ds1.Tables[0];
            this.ddlEmpList.DataBind();

            ds1.Dispose();

        }

        protected void ImgbtnFindRes_Click(object sender, EventArgs e)
        {
            this.GetMatList();
        }
        protected void ImgbtnSpecification_Click(object sender, EventArgs e)
        {
            string mResCode = this.ddlResList.SelectedValue.ToString();
            // string spcfcod1 = this.ddlResSpcf.SelectedValue.ToString();
            this.ddlResSpcf.Items.Clear();
            DataTable tbl1 = (DataTable)ViewState["tblSpcf"];
            DataView dv1 = tbl1.DefaultView;
            dv1.RowFilter = "rsircode = '" + mResCode + "' or spcfcod = '000000000000'";
            DataTable dt = dv1.ToTable();

            //if (dt.Rows.Count > 1)
            //{
            //    dt.Rows[0].Delete();
            //}


            this.ddlResSpcf.DataTextField = "spcfdesc";
            this.ddlResSpcf.DataValueField = "spcfcod";
            this.ddlResSpcf.DataSource = dt;
            this.ddlResSpcf.DataBind();


        }
        protected void ddlResList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ImgbtnSpecification_Click(null, null);

        }



        private void PreList()
        {


            string comcod = this.GetCompCode();
            string curdate = this.txtCurDate.Text.ToString().Trim();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GETPREISSUELIST", curdate, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlPreList.DataTextField = "issueno1";
            this.ddlPreList.DataValueField = "issueno";
            this.ddlPreList.DataSource = ds1.Tables[0];
            this.ddlPreList.DataBind();
            if (this.Request.QueryString["genno"].Length > 0)
            {
                string genno = this.Request.QueryString["genno"].ToString();

                this.ddlPreList.SelectedValue = genno;
                this.lbtnOk_Click(null, null);
            }


        }

        protected void ImgbtnFindPrevious_Click(object sender, EventArgs e)
        {
            this.PreList();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";

                this.ddlProject.Visible = true;
                this.ddlProject.Enabled = false;
                this.ddlDeptCode.Enabled = false;
                this.ddlEmpList.Enabled = false;
                this.PanelSub.Visible = true;
                this.lblPreViousList.Visible = false;
                this.txtSrchPrevious.Visible = false;
                this.ImgbtnFindPrevious.Visible = false;
                this.ddlPreList.Visible = false;

                this.GetMatList();
                this.GetIssuenfo();


                if (this.Request.QueryString["Type"] == "Entry")
                {
                    if (this.ddlPreList.Items.Count == 0)
                    {
                        if (this.ddlDeptCode.SelectedValue.ToString() == "AAAAAAAAAAAA")
                        {
                            this.lblmsg1.Visible = true;
                            this.lblmsg1.Text = "Please Select Department";
                            return;
                        }
                    }

                }

                return;
            }
            this.lblmsg1.Text = "";
            this.lbtnOk.Text = "Ok";
            this.ddlProject.Visible = true;
            this.ddlProject.Enabled = true;
            this.ddlDeptCode.Enabled = true;
            this.ddlEmpList.Enabled = true;

            this.txtCurDate.Enabled = true;
            this.PanelSub.Visible = false;
            this.ImgbtnFindPrevious.Visible = true;
            this.ddlPreList.Visible = true;
            this.ddlPreList.Items.Clear();
            this.ddlResList.Items.Clear();
            //this.ddlResSpcf.Items.Clear();
            this.gvIssue.DataSource = null;
            this.gvIssue.DataBind();
        }

        protected void GetLSDNo()
        {

            string comcod = GetCompCode();
            string mIssueNo = "NEWISU";
            if (this.ddlPreList.Items.Count > 0)
                mIssueNo = this.ddlPreList.SelectedValue.ToString();

            string date = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString();


            if (mIssueNo == "NEWISU")
            {
                DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GETISSUENO", date,
                       "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;
                if (ds2.Tables[0].Rows.Count > 0)
                {

                    this.lblCurNo1.Text = ds2.Tables[0].Rows[0]["maxissueno1"].ToString().Substring(0, 6);
                    this.txtCurNo2.Text = ds2.Tables[0].Rows[0]["maxissueno1"].ToString().Substring(6, 5);
                    this.ddlPreList.DataTextField = "maxissueno1";
                    this.ddlPreList.DataValueField = "maxissueno";
                    this.ddlPreList.DataSource = ds2.Tables[0];
                    this.ddlPreList.DataBind();
                }
            }

        }

        private void GetIssuenfo()
        {


            ViewState.Remove("tblIssue");
            string comcod = this.GetCompCode();
            string CurDate1 = this.txtCurDate.Text.Trim();
            string mISUNo = "NEWISU";
            if (this.ddlPreList.Items.Count > 0)
            {
                this.txtCurDate.Enabled = false;
                mISUNo = this.ddlPreList.SelectedValue.ToString();

            }
            string pactcode = this.ddlProject.SelectedValue.ToString();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GETISSUEINFO", mISUNo, CurDate1, "");

            //  DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETMETERIALS", CurDate1, mISUNo, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblIssue"] = ds1.Tables[0];


            if (mISUNo == "NEWISU")
            {
                ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GETISSUENO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                this.lblCurNo1.Text = ds1.Tables[0].Rows[0]["maxissueno1"].ToString().Trim().Substring(0, 6);
                this.txtCurNo2.Text = ds1.Tables[0].Rows[0]["maxissueno1"].ToString().Trim().Substring(6);
                return;
            }



            this.ddlProject.SelectedValue = ds1.Tables[1].Rows[0]["pactcode"].ToString();
            this.ddlDeptCode.SelectedValue = ds1.Tables[1].Rows[0]["deptcode"].ToString();
            this.ddlEmpList.SelectedValue = ds1.Tables[1].Rows[0]["empid"].ToString();

            this.txtCurDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["issuedat"]).ToString("dd-MMM-yyyy");
            this.txtrefno.Text = ds1.Tables[1].Rows[0]["refno"].ToString();
            this.lblCurNo1.Text = ds1.Tables[1].Rows[0]["issueno1"].ToString().Trim().Substring(0, 6);
            this.txtCurNo2.Text = ds1.Tables[1].Rows[0]["issueno1"].ToString().Trim().Substring(6);
            this.Data_Bind();
        }


        private void Data_Bind()
        {

            this.gvIssue.DataSource = (DataTable)ViewState["tblIssue"];
            this.gvIssue.DataBind();
            this.FooterCalCulation();


        }
        private void FooterCalCulation()
        {
            DataTable dt1 = (DataTable)ViewState["tblIssue"];

            if (dt1.Rows.Count == 0)
                return;
            if (this.GetCompCode() == "7305")
            {
                ((Label)this.gvIssue.FooterRow.FindControl("lblFgvissueqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(issueqty)", "")) ?
            0.00 : dt1.Compute("sum(issueqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
            }

            ((Label)this.gvIssue.FooterRow.FindControl("lgvFAmount")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(issueamt)", "")) ?
            0.00 : dt1.Compute("sum(issueamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
        }




        private void SaveValue()
        {

            DataTable dt1 = (DataTable)ViewState["tblIssue"];

            for (int i = 0; i < this.gvIssue.Rows.Count; i++)
            {

                double stcqty = Convert.ToDouble("0" + ((Label)this.gvIssue.Rows[i].FindControl("lblgvstkqty")).Text.Trim());
                double issueqty = Convert.ToDouble("0" + ((TextBox)this.gvIssue.Rows[i].FindControl("txtgvissueqty")).Text.Trim());
                double rate = Convert.ToDouble("0" + ((Label)this.gvIssue.Rows[i].FindControl("lblgvstkrate")).Text.Trim());
                string Remarks = ((TextBox)this.gvIssue.Rows[i].FindControl("txtgvremarks")).Text.Trim();

                if (stcqty < issueqty)
                {
                    this.lblmsg1.Text = "Invalid Issue Qty";
                    return;
                }
                int rowindex = ((this.gvIssue.PageIndex) * (this.gvIssue.PageSize)) + i;

                dt1.Rows[rowindex]["issueqty"] = issueqty;
                dt1.Rows[rowindex]["issueamt"] = (issueqty) * rate;
                dt1.Rows[rowindex]["remarks"] = Remarks;

            }
            ViewState["tblIssue"] = dt1;
        }
        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }




        protected void lbtnSelect_Click(object sender, EventArgs e)
        {



            //this.Panel2.Visible = true;
            this.SaveValue();
            DataTable tbl1 = (DataTable)ViewState["tblIssue"];
            string mResCode = this.ddlResList.SelectedValue.ToString();
            // string Specification = this.ddlResSpcf.SelectedValue.ToString();
            string Empcode = this.ddlDeptCode.SelectedValue.ToString();
            string spcfcod = this.ddlResSpcf.SelectedValue.ToString();
            DataRow[] dr2 = tbl1.Select("rsircode = '" + mResCode + "' and spcfcod='" + spcfcod + "'");
            if (dr2.Length == 0)
            {
                DataRow dr1 = tbl1.NewRow();

                dr1["comcod"] = this.GetCompCode();
                dr1["rsircode"] = this.ddlResList.SelectedValue.ToString();
                dr1["spcfcod"] = this.ddlResSpcf.SelectedValue.ToString();
                dr1["deptcode"] = this.ddlDeptCode.SelectedValue.ToString();
                dr1["rsirdesc"] = this.ddlResList.SelectedItem.Text.Trim();
                dr1["spcfdesc"] = this.ddlResSpcf.SelectedItem.Text.Trim();
                dr1["deptname"] = this.ddlDeptCode.SelectedItem.Text.Trim();
                dr1["empid"] = this.ddlEmpList.SelectedValue.ToString();
                DataTable tbl2 = (DataTable)ViewState["tblMat"];
                DataRow[] dr3 = tbl2.Select("rsircode = '" + mResCode + "' and spcfcod='" + spcfcod + "'");
                dr1["rsirunit"] = dr3[0]["rsirunit"];
                dr1["stkqty"] = dr3[0]["stkqty"];
                dr1["stkrate"] = dr3[0]["stkrate"];
                dr1["issueqty"] = dr3[0]["issueqty"];
                dr1["issueamt"] = 0;
                dr1["remarks"] = "";
                tbl1.Rows.Add(dr1);
            }

            ViewState["tblIssue"] = tbl1;
            this.Data_Bind();

        }
        protected void lbtnSelectAll_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            this.SaveValue();
            DataTable tbl1 = (DataTable)ViewState["tblIssue"];
            string mResCode = this.ddlResList.SelectedValue.ToString();
            // string Specification = this.ddlResSpcf.SelectedValue.ToString();
            string Empcode = this.ddlEmpList.SelectedValue.ToString();
            string mSrchTxt = "%";
            string date = this.txtCurDate.Text.Trim();
            DataTable tbl2 = (DataTable)ViewState["tblMat"];
            if (this.Request.QueryString["Type"] == "Link")
            {
                string pact = this.Request.QueryString["prjcode"].ToString();

                string dept = this.Request.QueryString["sircode"].ToString();

                DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_INDENT_STATUS", "GETDEPTWISECOMPINDENTET", pact, mSrchTxt, date, dept, "", "", "", "", "");
                ViewState["tbltolissue"] = ds1.Tables[0];

                DataTable tb21 = (DataTable)ViewState["tbltolissue"];

                for (int i = 0; i < tb21.Rows.Count; i++)
                {


                    DataRow[] dr3 = tbl1.Select("rsircode = '" + tb21.Rows[i]["rsircode"].ToString() + "'");
                    if (dr3.Length == 0)
                    {
                        DataRow dr1 = tbl1.NewRow();
                        dr1["comcod"] = this.GetCompCode(); ;
                        dr1["rsircode"] = tb21.Rows[i]["rsircode"];
                        dr1["spcfcod"] = this.ddlResSpcf.SelectedValue.ToString();
                        dr1["deptcode"] = this.ddlDeptCode.SelectedValue.ToString();
                        dr1["rsirdesc"] = tb21.Rows[i]["rsirdesc"];
                        dr1["spcfdesc"] = this.ddlResSpcf.SelectedItem.Text.Trim();
                        dr1["deptname"] = this.ddlDeptCode.SelectedItem.Text.Trim();
                        dr1["empid"] = this.ddlEmpList.SelectedValue.ToString();



                        dr1["rsirunit"] = tb21.Rows[i]["rsirunit"];
                        dr1["stkqty"] = tb21.Rows[i]["stkqty"];
                        dr1["stkrate"] = tb21.Rows[i]["stkrate"];
                        dr1["issueqty"] = tb21.Rows[i]["qtyapp"];
                        dr1["issueamt"] = 0;
                        dr1["remarks"] = "";

                        tbl1.Rows.Add(dr1);
                    }


                }


            }
            else
            {



                for (int i = 0; i < tbl2.Rows.Count; i++)
                {


                    DataRow[] dr3 = tbl1.Select("rsircode = '" + tbl2.Rows[i]["rsircode"].ToString() + "'");
                    if (dr3.Length == 0)
                    {
                        DataRow dr1 = tbl1.NewRow();
                        dr1["comcod"] = this.GetCompCode(); ;
                        dr1["rsircode"] = tbl2.Rows[i]["rsircode"];
                        dr1["spcfcod"] = this.ddlResSpcf.SelectedValue.ToString();
                        dr1["deptcode"] = this.ddlDeptCode.SelectedValue.ToString();
                        dr1["rsirdesc"] = tbl2.Rows[i]["rsirdesc"];
                        dr1["spcfdesc"] = this.ddlResSpcf.SelectedItem.Text.Trim();
                        dr1["deptname"] = this.ddlDeptCode.SelectedItem.Text.Trim();
                        dr1["empid"] = this.ddlEmpList.SelectedValue.ToString();



                        dr1["rsirunit"] = tbl2.Rows[i]["rsirunit"];
                        dr1["stkqty"] = tbl2.Rows[i]["stkqty"];
                        dr1["stkrate"] = tbl2.Rows[i]["stkrate"];
                        dr1["issueqty"] = 0;
                        dr1["issueamt"] = 0;
                        dr1["remarks"] = "";

                        tbl1.Rows.Add(dr1);
                    }


                }
            }
            ViewState["tblIssue"] = tbl1;
            this.Data_Bind();




        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string curdate = this.txtCurDate.Text.ToString().Trim();
            string Issueno = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.txtCurNo2.Text.ToString().Trim();


            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('/F_23_SaM/Print.aspx?Type=IssueChallan&comcod=" + comcod + "&issueno=" + Issueno + "&issuedat=" + curdate + "', target='_blank');</script>";
        }



        protected void gvIssue_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblIssue"];

            int rowindex = (this.gvIssue.PageSize) * (this.gvIssue.PageIndex) + e.RowIndex;

            string rescode = ((Label)this.gvIssue.Rows[e.RowIndex].FindControl("lblgvMatCode")).Text.Trim();
            string spcfcod = ((Label)this.gvIssue.Rows[e.RowIndex].FindControl("lblgvspcfcod")).Text.Trim();
            dt.Rows[rowindex].Delete();
            string comcod = this.GetCompCode();
            string curdate = this.txtCurDate.Text.ToString().Trim();
            DateTime Bdate = this.GetBackDate();
            string Issueno = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.txtCurNo2.Text.ToString().Trim();
            bool res = purData.UpdateTransInfo(comcod, "SP_ENTRY_MATERIAL_ISSUE", "REMOVEMATFROMISSUE", Issueno, rescode, spcfcod, "", "", "", "", "", "");
            if (res == true)
            {
                this.lblmsg1.Text = "Material Deleted Sucessfully";
            }

            ViewState["tblIssue"] = dt;
            this.Data_Bind();
        }

        protected void lnkbtn_exlup_Click(object sender, EventArgs e)
        {
            // List<IndentIssueItem> indissue = (List<IndentIssueItem>)Session["XcelDatamfg"];
            DataTable fdt = (DataTable)Session["XcelDatamfg"];
            string comcod = this.GetCompCode();
            string date = this.txtCurDate.Text;
            string refno = this.txtrefno.Text;
            string dept = this.ddlDeptCode.SelectedValue.ToString();
            string store = this.ddlProject.SelectedValue.ToString();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            string posteddat = DateTime.Today.ToString("dd-MMM-yyyy");
            DataSet ds = new DataSet("ds1");
            ds.Merge(fdt);
            ds.Tables[0].TableName = "tblIndent";

            bool res = purData.UpdateXmlTransInfo(comcod, "SP_ENTRY_MATERIAL_ISSUE", "INDENT_ISSUE_EXCEL_UPLOAD", ds, null, null, date, refno, dept, store, posteddat, usrid, trmid, sessionid);
            if (res == true)
            {
                this.lblmsg1.Text = "Saved Sucessfully";
            }
        }
        protected void ddlDeptCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetEmployeeList();
        }

        protected void txtCurDate_TextChanged(object sender, EventArgs e)
        {
            if (Request.QueryString["Type"] == "Link")
            {
                string comcod = this.GetCompCode();
                string curdate02 = this.txtCurDate.Text.Trim();
                DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GETPREISSUELIST", curdate02, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;

                this.ddlPreList.DataTextField = "issueno1";
                this.ddlPreList.DataValueField = "issueno";
                this.ddlPreList.DataSource = ds1.Tables[0];
                this.ddlPreList.DataBind();
               
                


            }
        }
    }

}