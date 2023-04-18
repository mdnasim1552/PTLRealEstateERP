using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace RealERPWEB.F_09_LCM
{
    public partial class LcReceive : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        ProcessAccess Purdata = new ProcessAccess();
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


                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "LC Receive Form";
                this.txtreceivedate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetLcNumber();
                this.GetStore();

            }
            this.CommonButton();
            if (fileuploadExcel.HasFile)
            {

                ViewState.Remove("tblupdata");
                this.lmsg.Visible = true;
                string connString = "";
                string StrFileName = string.Empty;
                if (fileuploadExcel.PostedFile != null && fileuploadExcel.PostedFile.FileName != "")
                {
                    StrFileName = fileuploadExcel.PostedFile.FileName.Substring(fileuploadExcel.PostedFile.FileName.LastIndexOf("\\") + 1);
                    string StrFileType = fileuploadExcel.PostedFile.ContentType;
                    int IntFileSize = fileuploadExcel.PostedFile.ContentLength;
                    if (IntFileSize <= 0)
                    {
                        this.lmsg.Text = "Uploading Fail";
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
                        this.lmsg.Text = "Uploading Successfully";


                    }
                }






                string strFileType = Path.GetExtension(fileuploadExcel.FileName).ToLower();
                string apppath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath.ToString();
                string path = apppath + "ExcelFile\\" + StrFileName;
                //string path = Server.MapPath("~") + ("\\ExcelFile\\" + StrFileName);


                //Connection String to Excel Workbook
                if (strFileType.Trim() == ".xls")
                {
                    connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                }
                else if (strFileType.Trim() == ".xlsx")
                {

                    connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties='Excel 12.0 Xml;HDR=YES;'";
                }

                string query = "SELECT [LC_Number],[Product_Id],[Pack_No],[M_IMEI],[S_IMEI], [Serial_No],[Color] FROM [Sheet1$]";
                OleDbConnection conn = new OleDbConnection(connString);
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                OleDbCommand cmd = new OleDbCommand(query, conn);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                //DataView dv = ds.Tables[0].DefaultView;
                //dv.RowFilter = ("LC_Number <> ''");
                //DataTable dt = dv.ToTable();
                //ViewState["tblupdata"] = dt;

                ViewState["tblupdata"] = ds.Tables[0];
                // this.DataInsert();
                da.Dispose();
                conn.Close();
                conn.Dispose();
                //this.GetExelData();



            }
        }

        private void CommonButton()
        {
            //((Label)this.Master.FindControl("lblANMgsBox")).Visible = false;
            ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            ((CheckBox)this.Master.FindControl("CheckBox1")).Visible = false;

            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = false;

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event

            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkbtnSave_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);


        }



        private string GetCompCode()
        {
            if (this.Request.QueryString["comcod"].Length == 0)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                return (hst["comcod"].ToString());
            }
            else
            {
                return (this.Request.QueryString["comcod"].ToString());
            }
        }
        public void GetLcNumber()
        {
            string comcod = this.GetCompCode();
            string SlcNO = "%%";
            DataSet ds1 = Purdata.GetTransInfo(comcod, "SP_LC_INFO", "RETRIVE_LC_VALUE", SlcNO, "14", "ACINF", "", "", "", "", "", ""); // table Desc 3
            this.ddlLcCode.DataTextField = "actdesc";
            this.ddlLcCode.DataValueField = "actcode";
            this.ddlLcCode.DataSource = ds1.Tables[0];
            this.ddlLcCode.DataBind();
            if (this.Request.QueryString["actcode"].Length > 0)
            {
                this.ddlLcCode.SelectedValue = this.Request.QueryString["actcode"].ToString();
                this.LbtnOk_Click(null, null);
            }

        }

        private void LoadRCVNO()
        {
            string comcod = this.GetCompCode();
            string storid = this.ddlStorid.SelectedValue.ToString();
            string grrdat = this.txtreceivedate.Text.ToString().Substring(0, 11);
            DataSet ds = Purdata.GetTransInfo(comcod, "SP_LC_INTERFACE", "GETRCVNO", grrdat, storid, "", "", "", "", "", "", "");
            this.ddlPreGrn.DataTextField = "rcvno";
            this.ddlPreGrn.DataValueField = "rcvno";
            this.ddlPreGrn.DataSource = ds.Tables[0];
            this.ddlPreGrn.DataBind();
            this.txtgrrno.Text = ds.Tables[0].Rows[0]["rcvno"].ToString();
        }
        protected void LbtnOk_Click(object sender, EventArgs e)
        {
            this.ddlLcCode.Enabled = false;
            if (this.Request.QueryString["genno"].Length > 0)
            {
                this.PreGrn();
                this.Get_Pre_RCVINFO();
            }
            // this.LoadGRRNo();
        }


        private string GetCallType()
        {
            string CallType = "";
            string comcod = this.GetCompCode();
            switch (comcod) 
            {
                case "1211":
                     CallType = "RETRIVELCSTORE";
                    break;

                default:
                     CallType = "RETRIVELCSTORE1";
                    break;
            
            
            }

            return CallType;



        }
        private void GetStore()
        {
            string lcno2 = this.ddlLcCode.SelectedValue.ToString();
            string comcod = this.GetCompCode();
            string LcCode = ASTUtility.Left(this.ddlLcCode.SelectedValue.ToString(), 4);
            string LccodeType = (LcCode == "1401") ? "actcode like '15%'" : (LcCode == "1402") ? "actcode like '17%'" : (LcCode == "1403") ? "actcode like '11%'"
                : "actcode like '1[157]%'";
            string Calltype = this.GetCallType();
            DataSet ds5 = Purdata.GetTransInfo(comcod, "SP_LC_INFO", Calltype, LccodeType, lcno2, "", "", "", "", "", "", "");
            this.ddlStorid.DataTextField = "actdesc1";
            this.ddlStorid.DataValueField = "actcode";
            this.ddlStorid.DataSource = ds5.Tables[0];
            this.ddlStorid.DataBind();

        }
        protected void imgbtnPreGrn_Click(object sender, EventArgs e)
        {

            this.PreGrn();
        }
        private void PreGrn()
        {
            string comcod = this.GetCompCode();
            string strcode = (this.Request.QueryString["centrid"].Length > 0) ? this.Request.QueryString["centrid"].ToString() : this.ddlStorid.SelectedValue.ToString();
            string filter2 = "%" + this.txtgrrno.Text.Trim() + "%";
            DataSet ds5 = Purdata.GetTransInfo(comcod, "SP_LC_INTERFACE", "GETPRERCV", strcode, filter2, "", "", "", "", "", "", ""); //table Desc 2

            if (ds5.Tables[0].Rows.Count == 0)
                return;

            this.lnkReceive.Visible = false;

            this.ddlPreGrn.DataTextField = "rcvno1";
            this.ddlPreGrn.DataValueField = "rcvno";
            this.ddlPreGrn.DataSource = ds5.Tables[0];
            this.ddlPreGrn.DataBind();
            if (this.Request.QueryString["genno"].Length > 0)
            {
                this.ddlPreGrn.SelectedValue = this.Request.QueryString["genno"].ToString();
            }
        }
        protected void lnkReceive_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            LoadRCVNO();
            if (this.ddlStorid.Items.Count == 0)
            {
                this.lmsg.Text = "Please Select Store Id.";
                this.lmsg.ForeColor = System.Drawing.Color.LightPink;
                return;
            }
            string lcno2 = this.ddlLcCode.SelectedValue.ToString();

            string rcvdat = this.txtreceivedate.Text.Trim().Substring(0, 11);
            string grrno = this.txtgrrno.Text.Trim();
            DataSet ds6 = Purdata.GetTransInfo(comcod, "SP_LC_INTERFACE", "ORDERRECEIVE", lcno2, rcvdat, grrno, "", "", "", "", "", "");
            var List = ds6.Tables[0].DataTableToList<RealEntity.C_09_LCM.EClassLCMGT>();

            if (ViewState["tblupdata"] != null)
            {

                //   this.pnlexcelheading.Visible = true;
                DataTable dt = (DataTable)ViewState["tblupdata"];
                List<RealEntity.C_09_LCM.EClassExcelData> lst = dt.DataTableToList<RealEntity.C_09_LCM.EClassExcelData>();
                //IEnumerable<MFGOBJ.C_09_LCM.EClassExcelData> lst1 = (from Product in lst
                //select Product).GroupBy(n => new {n.Product_Id}).Select(g=>g.FirstOrDefault());

                var lst1 = (from Product in lst
                            group Product by Product.Product_Id into g

                            select new SumClass
                            {
                                Product_Id = g.Key,
                                rcvqty = g.Count()
                            }).ToList();

                foreach (RealEntity.C_09_LCM.EClassLCMGT cr in List)
                {
                    string rescod = cr.rescod.ToString();
                    cr.rcvqty =
                     cr.rcvqty = (lst1.FindAll(p => p.Product_Id == rescod)).Count > 0 ? (lst1.FindAll(p => p.Product_Id == rescod))[0].rcvqty : 0.00;


                }

                DataTable dtu = (DataTable)ViewState["tblupdata"];

                this.rpprodetails.DataSource = (DataTable)ViewState["tblupdata"];
                this.rpprodetails.DataBind();

            }

            else
            {
                this.pnlexcelheading.Visible = false;
                this.rpprodetails.DataSource = null;
                this.rpprodetails.DataBind();

            }

            //   ViewState["tbllccost"] = ds6.Tables[1];
            ViewState["TblReceive"] = List;
            Data_Bind();
            this.lmsg.Text = "";
        }

        class SumClass
        {
            public string Product_Id { get; set; }
            public double rcvqty { get; set; }

        }
        private void Data_Bind()
        {
            try
            {
                var rcvdata = (List<RealEntity.C_09_LCM.EClassLCMGT>)ViewState["TblReceive"];

                this.dgvReceive.DataSource = rcvdata;
                this.dgvReceive.DataBind();

                this.Rcv_Footcal();


            }
            catch (Exception ex)
            {
                this.lmsg.Text = "Error:" + ex.Message;
            }

        }
        private void Rcv_Footcal()
        {
            try
            {

                var list1 = (List<RealEntity.C_09_LCM.EClassLCMGT>)ViewState["TblReceive"];
                if (list1.Count == 0)
                    return;
                ((Label)this.dgvReceive.FooterRow.FindControl("lblgvFordqty")).Text = (list1.Select(p => p.ordrqty).Sum() == 0.00) ? "0" : list1.Select(p => p.ordrqty).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.dgvReceive.FooterRow.FindControl("lblgrvFFreeqty1")).Text = (list1.Select(p => p.freeqty).Sum() == 0.00) ? "0" : list1.Select(p => p.freeqty).Sum().ToString("#,##0;(#,##0); ");

                ((Label)this.dgvReceive.FooterRow.FindControl("lblgvFreuptlast")).Text = (list1.Select(p => p.rcvuptolast).Sum() == 0.00) ? "0" : list1.Select(p => p.rcvuptolast).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.dgvReceive.FooterRow.FindControl("lblgvFrmainord")).Text = (list1.Select(p => p.remainordr).Sum() == 0.00) ? "0" : list1.Select(p => p.remainordr).Sum().ToString("#,##0;(#,##0); ");

                ((Label)this.dgvReceive.FooterRow.FindControl("lblgvFrcvQty")).Text = (list1.Select(p => p.rcvqty).Sum() == 0.00) ? "0" : list1.Select(p => p.rcvqty).Sum().ToString("#,##0;(#,##0); ");

            }
            catch (Exception ex)
            {
                this.lmsg.Text = "Error:" + ex.Message;
            }

        }
        protected void chkExel_CheckedChanged(object sender, EventArgs e)
        {
            this.pnlExel.Visible = this.chkExel.Checked;
        }
        private void Get_Pre_RCVINFO()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();

            if (this.ddlPreGrn.Items.Count == 0)
            {
                this.lmsg.Text = "Please Select Pre No.";
                this.lmsg.ForeColor = System.Drawing.Color.LightPink;
                return;
            }
            string lcno2 = this.ddlLcCode.SelectedValue.ToString();
            string preno = this.ddlPreGrn.SelectedValue.ToString();

            DataSet ds6 = Purdata.GetTransInfo(comcod, "SP_LC_INTERFACE", "GET_PRE_RCVINFO", lcno2, preno, "", "", "", "", "", "", "");
            var list = ds6.Tables[0].DataTableToList<RealEntity.C_09_LCM.EClassLCMGT>();
            ViewState["TblReceive"] = list;

            this.txtreceivedate.Text = Convert.ToDateTime(list[0].rcvdate).ToString("dd-MMM-yyyy");
            this.txtgrrno.Text = list[0].rcvno.ToString();
            this.ddlStorid.SelectedValue = list[0].storid.ToString();
            Data_Bind();
        }
        private void Save_Rec_Value()
        {
            var list = (List<RealEntity.C_09_LCM.EClassLCMGT>)ViewState["TblReceive"];
            int RowIndex = 0;

            //  double tocost = Convert.ToDouble("0" + ((Label)this.gvlccost.FooterRow.FindControl("lblgrvFcurlcCost")).Text);
            for (int i = 0; i < this.dgvReceive.Rows.Count; i++)
            {
                double Qty = Convert.ToDouble("0" + ((TextBox)this.dgvReceive.Rows[i].FindControl("txtgvrcvQty")).Text.Trim());
                RowIndex = this.dgvReceive.PageIndex * this.dgvReceive.PageSize + i;
                list[RowIndex].rcvqty = Qty;
                list[RowIndex].rcvqty = Qty;
            }

            ViewState["TblReceive"] = list;
        }

        protected void dgvReceive_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                TextBox gvrcvQty = (TextBox)e.Row.FindControl("txtgvrcvQty");

                double balqty = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "rcvqty"));


                if (balqty == 0.00)
                {

                    //gvrcvQty.Enabled = false;
                    gvrcvQty.ToolTip = "Balance Qty Zero";
                }
                else
                {

                }
            }
        }

        private void lnkbtnSave_Click(object sender, EventArgs e)
        {
            this.lmsg.Visible = true;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string PostedByid = (this.Request.QueryString["Type"] == "Entry") ? userid : "";
            string Posttrmid = (this.Request.QueryString["Type"] == "Entry") ? Terminal : "";
            string PostSession = (this.Request.QueryString["Type"] == "Entry") ? Sessionid : "";
            string Posteddat = (this.Request.QueryString["Type"] == "Entry") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : "01-Jan-1900";
            string actcode = this.ddlLcCode.SelectedValue.ToString();
            string storid = this.ddlStorid.SelectedValue.ToString();
            string rcvno = this.txtgrrno.Text.Trim();
            string rcvdate = this.txtreceivedate.Text.Substring(0, 11).ToString();
            this.Save_Rec_Value();
            string Edit = "";
            if (ViewState["TblReceive"] != null)
            {
                var list = (List<RealEntity.C_09_LCM.EClassLCMGT>)ViewState["TblReceive"];
                DataTable tbl4 = ASITUtility03.ListToDataTable(list);
                //  DataTable dt = ((DataTable)ViewState["tbllccost"]).Copy();

                DataSet ds1 = new DataSet("ds1");


                //ds1.Tables.Add(dt1);
                //ds1.Tables.Add(dt2);
                //ds1.Tables.Add(dt3);
                //ds1.Tables[0].TableName = "tbl1";
                //ds1.Tables[1].TableName = "tbl2";
                //ds1.Tables[2].TableName = "tbl3";

                ds1.Tables.Add(tbl4);
                //ds1.Tables.Add(dt);
                ds1.Tables[0].TableName = "tbl1";
                //ds1.Tables[1].TableName = "tbl2";



                string lotno = tbl4.Rows[0]["lotno"].ToString();
                string expdat = Convert.ToDateTime(tbl4.Rows[0]["expdate"]).ToString("dd-MMM-yyyy");
                string lcno = this.ddlLcCode.SelectedValue.ToString();
                string expdat1 = Convert.ToDateTime((expdat == "" ? "1-Jan-1900" : expdat)).ToString("dd-MMM-yyyy");
                DataSet ds112 = Purdata.GetTransInfoNew(comcod, "SP_LC_INTERFACE", "UPDATELCDETAILS", ds1, null, null, Edit, rcvdate, storid, lotno, expdat1, PostedByid, PostSession, Posttrmid, Posteddat, lcno, rcvno, "", "", "", "");
                if (ds112.Tables[0].Rows.Count != 0)
                {
                    this.txtgrrno.Text = ds112.Tables[0].Rows[0]["memonum"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Update Successfully');", true);
                    //  ((LinkButton)this.dgvReceive.FooterRow.FindControl("lnkFinalUpdateR")).Enabled = false;
                    //this.lmsg.Text = "";
                    //
                }


            }
            if (ViewState["tblupdata"] != null)
            {
                DataTable dt = (DataTable)ViewState["tblupdata"];


                DataSet ds1xml = new DataSet("ds1");
                ds1xml.Tables.Add(dt.Copy()); // Voucher Table
                ds1xml.Tables[0].TableName = "tbl1";
                rcvno = this.txtgrrno.Text.Trim();
                bool result = Purdata.UpdateXmlTransInfo(comcod, "SP_LC_INTERFACE", "INSORUPDATELCRECINFC", ds1xml, null, null, "000000000000000", actcode, storid, rcvno, "", "", "", "", "", "", "", "", "", "", "");

                if (!result)
                {
                    this.lmsg.Text = "Error:" + Purdata.ErrorObject["Msg"].ToString();
                    return;
                }

            }
        }

        private void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {
            this.Save_Rec_Value();
            this.Data_Bind();
        }

    }
}