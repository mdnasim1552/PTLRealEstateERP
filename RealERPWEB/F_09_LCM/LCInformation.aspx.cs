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
using System.Text;
using System.IO;
using System.Data.OleDb;
using System.Xml.Linq;
using RealERPLIB;
using RealERPRPT;
using RealEntity.C_09_LCM;
using RealEntity.C_22_Sal;
namespace RealERPWEB.F_09_LCM
{
    public partial class LCInformation : System.Web.UI.Page
    {
        public string code;
        string Upload = "";
        //ProcessAccess proc1 = new ProcessAccess("ASITPUR");
        ProcessAccess proc1 = new ProcessAccess();
        static string prevPage = String.Empty;
        SalesInvoice_BL lst = new SalesInvoice_BL();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Page.Form.Enctype = "multipart/form-data";
            if (!IsPostBack)
            {
                ViewState.Remove("tblupdata");
                prevPage = Request.UrlReferrer.ToString();
                //((Label)this.Master.FindControl("lblTitle")).Text = (Request.QueryString["tname"].ToString() == "order") ? "L/C Openning" :
                //    (Request.QueryString["tname"].ToString() == "receive") ? "Foreign Material Recived" : "L/C Costing";   //=

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.ViewState.Remove("TblOrder");
                string imp = Request.QueryString["tid"].ToString();
                if (imp == "lc")
                {
                    //this.lbldetails1.Enabled = false;
                    //this.lnkpreorder.Visible = false;
                    //this.ddlordrno.Visible = false;
                    this.CurrencyInf();
                    this.GetOther();


                }

                else if (imp == "sup")
                {
                    this.lblLcno.Text = "Supplier Id";
                    //this.lblLCExdate.Text = "Date";
                    //this.lblsupplier.Text = "Order No.";
                    this.dgvOrder.Columns[5].HeaderText = "Rate";
                    this.dgvOrder.Columns[6].HeaderText = "Amount";
                    //this.lbldetails1.Text = "Order Id.";
                    //this.lbldetails1.Enabled = true;

                }
                this.imgbtnLcsearch_Click(null, null);

                ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;

                ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
                ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;

                ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
                ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;

            }


            if (fileuploadExcel.HasFile)
            {

                ViewState.Remove("tblupdata");

                string connString = "";
                string StrFileName = string.Empty;
                if (fileuploadExcel.PostedFile != null && fileuploadExcel.PostedFile.FileName != "")
                {
                    StrFileName = fileuploadExcel.PostedFile.FileName.Substring(fileuploadExcel.PostedFile.FileName.LastIndexOf("\\") + 1);
                    string StrFileType = fileuploadExcel.PostedFile.ContentType;
                    int IntFileSize = fileuploadExcel.PostedFile.ContentLength;
                    if (IntFileSize <= 0)
                    {


                        ((Label)this.Master.FindControl("lblmsg")).Text = " Uploading Fail";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);



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
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);


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

                DataView dv = ds.Tables[0].DefaultView;
                dv.RowFilter = ("LC_Number <> ''");
                DataTable dt = dv.ToTable();
                ViewState["tblupdata"] = dt;

                //Session["tblupdata"] = ds.Tables[0];
                // this.DataInsert();
                da.Dispose();
                conn.Close();
                conn.Dispose();
                //this.GetExelData();



            }



        }
        private void GetOther()
        {
            string comcod = this.ComCod();
            //ViewState.Remove("tblcur");
            DataSet ds1 = proc1.GetTransInfo(comcod, "SP_LC_INFO", "GETCURRENCYAGST", "", "", "", "", "", "", "", "", "");
            ViewState["tblSup"] = ds1.Tables[0];
            ViewState["tblBank"] = ds1.Tables[1];
            ds1.Dispose();
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
            //(LinkButton)this.Master.FindControl("lnkbtnLedger")).Click += new EventHandler(lnkbtnLedger_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Click += new EventHandler(lnkPrint_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnTranList")).Click += new EventHandler(lnkPrint_Click);

            //((LinkButton)this.Master.FindControl("lnkbtnNew")).Click += new EventHandler(btnNew_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnAdd")).Click += new EventHandler(lnkbtnAdd_Click1);
            // ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Click += new EventHandler(lnkbtnEdit_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(btnUpdate_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(btnReCalculate_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnDelete")).Click += new EventHandler(lnkbtnDelete_Click);
            //((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
            //((CheckBox)this.Master.FindControl("chkBoxN")).CheckedChanged += new EventHandler(chkPayment_CheckedChanged);
            //}
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }

        protected string ComCod()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            return comcod;
        }
        private void PreGrn()
        {
            string comcod = this.ComCod();
            string strcode = this.ddlStorid.SelectedValue.ToString();
            string filter2 = "%" + this.txtgrrno.Text.Trim() + "%";
            DataSet ds5 = proc1.GetTransInfo(comcod, "SP_LC_INFO", "GETPREGRN", strcode, filter2, "", "", "", "", "", "", ""); //table Desc 2

            if (ds5.Tables[0].Rows.Count == 0)
                return;

            this.lnkReceive.Visible = false;
            this.lnkOk.Visible = true;

            this.ddlPreGrn.DataTextField = "grrno1";
            this.ddlPreGrn.DataValueField = "grrno";
            this.ddlPreGrn.DataSource = ds5.Tables[0];
            this.ddlPreGrn.DataBind();
        }

        private void GetOrderId()
        {
            string comcod = this.ComCod();
            //if (this.txtLcDate.Text == "")
            //{
            //    this.lmsg.Text = "Please Select Order Date.";
            //    return;
            //}


            //string supid= this.ddlLcCode.SelectedValue.ToString();
            //string ordrdat = this.txtLcDate.Text.ToString().Substring(0, 11);
            //DataSet tbl7 = proc1.GetTransInfo(comcod, "SP_LC_INFO", "GETORDRID", ordrdat, "LCINFO1", "", "", "", "", "", "", ""); //// table name Desc 2 ta
            ////this.txtdetails1.Text = tbl7.Tables[0].Rows[0]["ordrno"].ToString();
            //this.dgvOrder.DataSource = null;
            //this.dgvOrder.DataBind();
            //Session.Remove("TblOrder");

        }
        protected void imgbtnLcsearch_Click(object sender, EventArgs e)
        {

            if (this.lnkOpen.Text == "New")
                return;
            string comcod = this.ComCod();
            //------Setting Page Content----------------------// 
            string imp1 = Request.QueryString["tid"].ToString();
            //if (imp1 == "lc")
            //{
            //     code = "1401";
            //}
            //else if (imp1 == "sup")
            //{
            //     code = "1402";  
            //}
            //else if (imp1 == "cst")
            //{
            //    code = "1401";
            //}
            //-----------------------------------------------//
            string SlcNO = "%%";
            DataSet ds1 = proc1.GetTransInfo(comcod, "SP_LC_INFO", "RETRIVE_LC_VALUE", SlcNO, "14", "ACINF", "", "", "", "", "", ""); // table Desc 3
            this.ddlLcCode.DataTextField = "actdesc";
            this.ddlLcCode.DataValueField = "actcode";
            this.ddlLcCode.DataSource = ds1.Tables[0];
            this.ddlLcCode.DataBind();
            ViewState["tblStoreType"] = ds1.Tables[0];
        }

        protected void lnkbtnSaveCust_Click(object sender, EventArgs e)
        {

            this.detailsinfo();

        }

        private void SaveValue()
        {
            DataTable dt = (DataTable)ViewState["tblLcinfo"];
            string Gvalue = "";
            for (int i = 0; i < this.gvPersonalInfo.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                string gtype = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lgvgval")).Text.Trim();

                if (Gcode == "25001" || Gcode == "25003" || Gcode == "25008" || Gcode == "25016" || Gcode == "25030" || Gcode == "25033" || Gcode == "25035" || Gcode == "25038" || Gcode == "25054" || Gcode == "25055")
                {

                    Gvalue = (((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim() == "") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim();
                }
                else if (Gcode == "25018" || Gcode == "25025" || Gcode == "25050")
                {

                    Gvalue = (((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlAlType")).Items.Count == 0) ? ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim()
                        : ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlAlType")).SelectedValue.ToString();
                }
                else
                {
                    Gvalue = (((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcurrency")).Items.Count == 0) ?
                       ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim() : ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcurrency")).SelectedValue.ToString();
                }

                dt.Rows[i]["gdesc1"] = Gvalue;
            }


            //for (int i = 0; i < this.gvPersonalInfo.Rows.Count; i++)
            //{
            //    string Gcode = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
            //    string gtype = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lgvgval")).Text.Trim();
            //    string Gvalue = (((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlGvList")).Items.Count == 0) ? ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim() : ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlGvList")).SelectedValue.ToString();


            //    dt.Rows[i]["gdesc1"] = Gvalue;
            //}

            ViewState["tblLcinfo"] = dt;

        }


        protected void detailsinfo()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                this.SaveValue();
                string actcode = this.ddlLcCode.SelectedValue.ToString();
                DataSet ds1 = new DataSet("ds1");
                DataTable dt = ((DataTable)ViewState["tblLcinfo"]).Copy();
                ds1.Tables.Add(dt);
                ds1.Tables[0].TableName = "tbl1";

                bool result = proc1.UpdateXmlTransInfo(comcod, "SP_LC_INFO", "INSERTORUPDATELCINF", ds1, null, null, actcode, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = proc1.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                    return;
                }
                ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Customer INFORMATION";
                    string eventdesc = "Update Sup Info";
                    string eventdesc2 = "";
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }
            }

            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            }

        }

        protected void lnkOpen_Click(object sender, EventArgs e)
        {

            this.ResourceCode();
            if (this.lnkOpen.Text == "Open")
            {
                this.lnkOpen.Text = "New";
                //--------------------------Order,Receive,Costing-----------//
                string criteria = Request.QueryString["tname"].ToString();
                switch (criteria)
                {
                    case "order":
                        this.GetGenInfo();
                        this.CallOrder();
                        break;

                    case "receive":


                        this.GetGenInfo();
                        this.ShowStorList();
                        CallReceive();
                        this.LoadGRRNo();
                        this.MultiView1.ActiveViewIndex = 1;
                        ((LinkButton)this.gvPersonalInfo.FooterRow.FindControl("lnkbtnSaveCust")).Visible = false;
                        break;

                    case "costing":
                        this.MultiView1.ActiveViewIndex = 2;
                        this.ResCosting();
                        break;

                }
                this.ddlLcCode.Enabled = false;
                //--------------------------------------//

            }
            else
            {
                this.lnkOpen.Text = "Open";
                this.MultiView1.ActiveViewIndex = -1;

                this.ddlLcCode.Enabled = true;
            }
        }

        private void GetGenInfo()
        {
            string comcod = this.ComCod();
            string ActCode = this.ddlLcCode.SelectedValue.ToString();
            DataSet ds1 = proc1.GetTransInfo(comcod, "SP_LC_INFO", "CUSTERSONALINFO", ActCode, "", "", "", "", "", "", "", "");
            ViewState["tblLcinfo"] = ds1.Tables[0];

            this.gvPersonalInfo.DataSource = ds1.Tables[0];
            this.gvPersonalInfo.DataBind();
            DataTable dt = ds1.Tables[0];
            DataTable dtsup = (DataTable)ViewState["tblSup"];
            DataTable dtBank = (DataTable)ViewState["tblBank"];

            List<RealEntity.C_22_Sal.Sales_BO.ConvInf> lst12 = (List<RealEntity.C_22_Sal.Sales_BO.ConvInf>)ViewState["tblcur"];
            var lst1 = (List<RealEntity.C_22_Sal.Sales_BO.Currencyinf>)ViewState["tblcurdesc"];


            DropDownList ddlgval;
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string Gcode = dt.Rows[i]["gcod"].ToString();

                switch (Gcode)
                {
                    case "25010": //Currency
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("PanelOther")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcurrency"));
                        ddlgval.DataTextField = "curdesc";
                        ddlgval.DataValueField = "curcode";
                        ddlgval.DataSource = lst1;
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        this.ddlcurrency_SelectedIndexChanged(null, null);
                        break;

                    case "25025": //Supplier
                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("pnlcurrency")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlAlType"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dtsup;
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        ddlgval.Attributes["style"] = "width:470px;";
                        break;

                    case "25018": //Bank
                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("pnlcurrency")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlAlType"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dtBank;
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        ddlgval.Attributes["style"] = "width:470px;";
                        break;
                    case "25050": //INSURANCE COMPANY
                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("pnlcurrency")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlAlType"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dtsup;
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        ddlgval.Attributes["style"] = "width:470px;";
                        break;
                    case "25001": //Date Time 
                    case "25003":
                    case "25008":
                    case "25016":
                    case "25030":
                    case "25033":
                    case "25035":
                    case "25038":
                    case "25054":
                    case "25055":
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("pnlcurrency")).Visible = false;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcurrency")).Items.Clear();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcurrency")).Visible = false;

                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("PanelOther")).Visible = false;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlAlType")).Items.Clear();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlAlType")).Visible = false;
                        break;

                    default:
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("pnlcurrency")).Visible = false;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcurrency")).Items.Clear();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcurrency")).Visible = false;

                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("PanelOther")).Visible = false;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlAlType")).Items.Clear();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlAlType")).Visible = false;
                        break;

                }

            }


        }
        protected void ddlcurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt1 = (DataTable)ViewState["tblLcinfo"];
            DataView dv = dt1.DefaultView;
            dv.RowFilter = ("gcod='25011'");
            dt1 = dv.ToTable();
            string Rate = dt1.Rows[0]["gdesc1"].ToString();
            if (Rate.Length != 0)
            {
                this.txtconv.Text = Convert.ToDouble(dt1.Rows[0]["gdesc1"]).ToString("#,##0.0000;-#,##0.0000; ");
            }
            var lst1 = (List<RealEntity.C_22_Sal.Sales_BO.ConvInf>)ViewState["tblcur"];
            string fcode = "001";
            for (int i = 0; i < this.gvPersonalInfo.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();


                switch (Gcode)
                {
                    case "25010":
                        string tcode = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcurrency")).SelectedValue.ToString();

                        if (Rate.Length != 0)
                        {
                            ((TextBox)this.gvPersonalInfo.Rows[i + 1].FindControl("txtgvVal")).Text = Convert.ToDouble(dt1.Rows[0]["gdesc1"]).ToString("#,##0.0000;-#,##0.0000; ");
                            this.txtconv.Text = Convert.ToDouble(dt1.Rows[0]["gdesc1"]).ToString("#,##0.0000;-#,##0.0000; ");
                        }
                        else
                        {
                            ((TextBox)this.gvPersonalInfo.Rows[i + 1].FindControl("txtgvVal")).Text = Convert.ToDouble((lst1.FindAll(p => p.fcode == fcode && p.tcode == tcode))[0].conrate).ToString("#,##0.0000;-#,##0.0000; ");
                            this.txtconv.Text = Convert.ToDouble((lst1.FindAll(p => p.fcode == fcode && p.tcode == tcode))[0].conrate).ToString("#,##0.0000;-#,##0.0000; ");
                        }

                        break;

                }

            }
        }

        private void CallReceive()
        {
            //------Setting Page Content----------------------// 
            string imp2 = Request.QueryString["tid"].ToString();
            string tname = Request.QueryString["tname"].ToString();
            if (imp2 == "lc")
            {

                //this.lnkgeninfo.Visible = false;
                try
                {
                    if (tname == "order")
                    {
                        this.Panel3.Visible = true;
                    }

                    this.MultiView1.ActiveViewIndex = 1;
                    this.txtreceivedate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                    this.chkExel.Checked = false;
                    this.dgvReceive.DataSource = null;
                    this.dgvReceive.DataBind();
                    ViewState.Remove("tblupdata");
                    this.rpprodetails.DataSource = null;
                    this.rpprodetails.DataBind();
                    this.txtgrrno.Text = "";

                }


                catch (Exception ex)
                {

                    ((Label)this.Master.FindControl("lblmsg")).Text = ex.Message;
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

                }
            }
            else if (imp2 == "sup")
            {

                try
                {
                    this.Panel3.Visible = true;
                    this.MultiView1.ActiveViewIndex = 1;
                    this.txtreceivedate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                    this.dgvReceive.DataSource = null;
                    this.dgvReceive.DataBind();
                    this.txtgrrno.Text = "";

                }


                catch (Exception ex)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = ex.Message;
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                }

            }
            else if (imp2 == "cst")
            {

            }
            //-----------------------------------------------//
        }
        private void CallOrder()
        {
            //------Setting Page Content----------------------// 
            string imp2 = Request.QueryString["tid"].ToString();
            string tname = Request.QueryString["tname"].ToString();

            if (imp2 == "lc")
            {
                try
                {
                    ViewState.Remove("TblOrder");
                    if (tname == "order")
                    {
                        this.Panel3.Visible = true;
                    }
                    this.MultiView1.ActiveViewIndex = 0;
                    this.showOrder();
                    LoadOrderDgv();

                }


                catch (Exception ex)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = ex.Message;
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);


                }
            }
            else if (imp2 == "sup")
            {

                try
                {
                    //Session.Remove("TblOrder");
                    //string comcod = this.ComCod();
                    //string lcno = this.ddlLcCode.SelectedValue.ToString();
                    //DataSet ds3 = proc1.GetTransInfo(comcod, "SP_LC_INFO", "RETRIVELCINFO", lcno, "LCINFO1", "", "", "", "", "", "", ""); //table Desc 2

                    this.Panel3.Visible = true;
                    this.MultiView1.ActiveViewIndex = 0;
                    this.showOrder();
                    LoadOrderDgv();

                }


                catch (Exception ex)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = ex.Message;
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                }
                #region dd
                //try
                //{
                //    this.txtLcDate.Text = "";
                //    this.txtsup.Text = "";
                //    this.txtcsup.Text = "";
                //    //this.txtbank.Text = "";
                //    this.ddlbank.SelectedIndex = 0;
                //    //this.txtcur.Text = "";
                //    this.ddlcur.SelectedIndex = 0;
                //    this.txtconv.Text = "";
                //    this.txtdetails1.Text = "";
                //    this.txtdetails2.Text = "";
                //    this.txtdetails3.Text = "";
                //    this.txtdetails4.Text = "";
                //    this.txtdetails5.Text = "";
                //    this.txtdetails6.Text = "";
                //    this.Panel3.Visible = true;
                //    this.MultiView1.ActiveViewIndex = 0;

                //}
                //catch (Exception ex)
                //{
                //    this.lmsg.Text = "Error:" + ex.Message;
                //}
                #endregion
            }
            else if (imp2 == "cst")
            {

            }
            //-----------------------------------------------//
        }


        private void ResCosting()
        {
            string comcod = this.ComCod();
            string lccode3 = this.ddlLcCode.SelectedValue.ToString();
            DataSet ds7 = proc1.GetTransInfo(comcod, "SP_PUR_COST", "SHOWLCRESCOST", lccode3, "LCINFO3", "", "", "", "", "", "", ""); // table Deac 2
            ViewState["TblOrder"] = ds7.Tables[0];
            this.dgvCosting.DataSource = ds7.Tables[0];
            this.dgvCosting.DataBind();

            if (ds7.Tables[0].Rows.Count > 0)
            {
                ((Label)this.dgvCosting.FooterRow.FindControl("lblgvftfcamt")).Text = Convert.ToDouble((Convert.IsDBNull(ds7.Tables[0].Compute("Sum(amtfc)", "")) ?
                0.00 : ds7.Tables[0].Compute("Sum(amtfc)", ""))).ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.dgvCosting.FooterRow.FindControl("lblgvftdpamt")).Text = Convert.ToDouble((Convert.IsDBNull(ds7.Tables[0].Compute("Sum(amtdp)", "")) ?
                0.00 : ds7.Tables[0].Compute("Sum(amtdp)", ""))).ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.dgvCosting.FooterRow.FindControl("lblgvftToamt")).Text = Convert.ToDouble((Convert.IsDBNull(ds7.Tables[0].Compute("Sum(totalamt)", "")) ?
                0.00 : ds7.Tables[0].Compute("Sum(totalamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
            }

        }
        private void showOrder()
        {
            string comcod = this.ComCod();
            string lcno1 = this.ddlLcCode.SelectedValue.ToString().Trim();
            string ordrid = "";
            DataSet ds5 = proc1.GetTransInfo(comcod, "SP_LC_INFO", "RETRIVELCINFO2", lcno1, ordrid, "LCINFO2", "", "", "", "", "", ""); //table Desc3
            ViewState["TblOrder"] = ds5.Tables[0];
            LoadOrderDgv();
            //Calculation();
        }

        protected void lnkAddTable_Click(object sender, EventArgs e)
        {
            if (ViewState["TblOrder"] == null)
            {
                DataTable tbl2 = new DataTable();
                tbl2.Columns.Add("rescod", Type.GetType("System.String"));
                tbl2.Columns.Add("resdesc", Type.GetType("System.String"));
                tbl2.Columns.Add("spcfcode", Type.GetType("System.String"));
                tbl2.Columns.Add("spcfdesc", Type.GetType("System.String"));
                tbl2.Columns.Add("scode", Type.GetType("System.String"));
                tbl2.Columns.Add("unit", Type.GetType("System.String"));
                tbl2.Columns.Add("ordrqty", Type.GetType("System.Double"));
                tbl2.Columns.Add("freeqty", Type.GetType("System.Double"));
                tbl2.Columns.Add("rate", Type.GetType("System.Double"));
                tbl2.Columns.Add("amount", Type.GetType("System.Double"));
                tbl2.Columns.Add("bdamount", Type.GetType("System.Double"));
                ViewState["TblOrder"] = tbl2;
            }
            double ConAmt = Convert.ToDouble(this.txtconv.Text);



            DataTable tbl3 = (DataTable)ViewState["TblOrder"];
            DataTable tblr = (DataTable)ViewState["Material"];

            string rescode = this.ddlResList.SelectedValue.Trim().ToString();
            string spcfcode = this.ddlResSpcf.SelectedValue.ToString();

            //  foreach (string rescode in arrbilno)
            //    {

            DataRow[] dr2 = tbl3.Select("rescod='" + rescode + "' AND spcfcode='" + spcfcode + "'");
            if (dr2.Length == 0)
            {
                DataRow[] drb = tblr.Select("rescod='" + rescode + "'");

                DataRow dr1 = tbl3.NewRow();
                dr1["rescod"] = rescode;
                dr1["resdesc"] = (((DataTable)ViewState["Material"]).Select("rescod='" + rescode + "'"))[0]["resdesc"].ToString();
                dr1["spcfcode"] = spcfcode;
                if (spcfcode == "000000000000")
                {
                    dr1["spcfdesc"] = "";

                }
                else
                {
                    dr1["spcfdesc"] = (((DataTable)ViewState["tblSpcf"]).Select("rsircode='" + rescode + "' and spcfcod='" + spcfcode + "'"))[0]["spcfdesc"].ToString();
                }

                dr1["scode"] = drb[0]["resdesc3"];
                dr1["unit"] = drb[0]["sirunit"];
                dr1["ordrqty"] = 0;
                dr1["freeqty"] = 0;
                dr1["rate"] = Convert.ToDouble(drb[0]["sirval"]) / ConAmt;
                dr1["amount"] = 0;
                dr1["bdamount"] = 0;
                tbl3.Rows.Add(dr1);

            }
            //   }

            ViewState["TblOrder"] = tbl3;
            LoadOrderDgv();


        }
        private void Footcal()
        {
            DataTable dt1 = (DataTable)ViewState["TblOrder"];
            if (dt1.Rows.Count == 0)
                return;
            ((Label)this.dgvOrder.FooterRow.FindControl("lblgrvFOrderQty")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(ordrqty)", "")) ? 0.00 : dt1.Compute("sum(ordrqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.dgvOrder.FooterRow.FindControl("lblgrvFFreeqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(freeqty)", "")) ? 0.00 : dt1.Compute("sum(freeqty)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.dgvOrder.FooterRow.FindControl("lblgrvFamount")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amount)", "")) ? 0.00 : dt1.Compute("sum(amount)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.dgvOrder.FooterRow.FindControl("lblgrvFBDTamount")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(bdamount)", "")) ? 0.00 : dt1.Compute("sum(bdamount)", ""))).ToString("#,##0.00;(#,##0.00); ");



        }

        private void LoadOrderDgv()
        {
            try
            {
                DataTable tbl4 = (DataTable)ViewState["TblOrder"];
                if (tbl4.Rows.Count <= 0)
                {
                    this.dgvOrder.DataSource = null;
                    this.dgvOrder.DataBind();
                    return;

                }
                //Calculation();
                this.dgvOrder.DataSource = tbl4;
                this.dgvOrder.DataBind();
                Session["Report1"] = dgvOrder;
                ((HyperLink)this.dgvOrder.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                this.Footcal();
            }
            catch (Exception ex)
            {

                ((Label)this.Master.FindControl("lblmsg")).Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

            }

        }

        protected void lnkTotal_Click(object sender, EventArgs e)
        {
            this.Calculation();
            this.LoadOrderDgv();

        }
        private void Calculation()
        {
            try
            {
                DataTable tbl4 = (DataTable)ViewState["TblOrder"];

                double Tamt = 0.00;
                double BDTamt = 0.00;
                for (int i = 0; i < dgvOrder.Rows.Count; i++)
                {
                    double Orderqty = Convert.ToDouble("0" + ((TextBox)dgvOrder.Rows[i].FindControl("txtgrvOrderQty")).Text.Trim());
                    double Freeqty = Convert.ToDouble("0" + ((TextBox)dgvOrder.Rows[i].FindControl("txtgrvFreeqty")).Text.Trim());
                    double Rate = Convert.ToDouble("0" + ((TextBox)dgvOrder.Rows[i].FindControl("txtgrvRate")).Text.Trim());
                    double Amount = Orderqty * Rate;
                    Tamt += Amount;
                    double BDTAmount = Amount * Convert.ToDouble(this.txtconv.Text); //().trim().ToString("#,##0.00;(#,##0.00); ");
                    BDTamt += BDTAmount;
                    ((TextBox)dgvOrder.Rows[i].FindControl("txtgrvOrderQty")).Text = Orderqty.ToString("#,##0.00;(#,##0.00); ");
                    ((TextBox)dgvOrder.Rows[i].FindControl("txtgrvRate")).Text = Rate.ToString("#,##0.000000;(#,##0.000000); ");
                    ((Label)dgvOrder.Rows[i].FindControl("lblgrvamount")).Text = Amount.ToString("#,##0.00;(#,##0.00); ");
                    ((Label)dgvOrder.Rows[i].FindControl("lblgrvBDTamount")).Text = BDTAmount.ToString("#,##0.00;(#,##0.00); ");



                    tbl4.Rows[i]["ordrqty"] = Orderqty;
                    tbl4.Rows[i]["freeqty"] = Freeqty;
                    tbl4.Rows[i]["rate"] = Rate;
                    tbl4.Rows[i]["amount"] = Amount;
                    tbl4.Rows[i]["bdamount"] = BDTAmount;
                }
                ViewState["TblOrder"] = tbl4;

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);


            }


        }
        protected void lnkFinalUpdate_Click(object sender, EventArgs e)    // fn up order
        {

            string comcod = this.ComCod();
            string actcode = this.ddlLcCode.SelectedValue.ToString();
            this.Calculation();


            for (int i = 0; i < dgvOrder.Rows.Count; i++)
            {

                string rescode = ((Label)dgvOrder.Rows[i].FindControl("lblgvResCode")).Text.Replace("-", "");
                string spcfcode = ((Label)dgvOrder.Rows[i].FindControl("lblgrvspccode")).Text.ToString();
                string ordrqty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)dgvOrder.Rows[i].FindControl("txtgrvOrderQty")).Text.Replace(",", ""))).ToString();
                string Freeqty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)dgvOrder.Rows[i].FindControl("txtgrvFreeqty")).Text.Replace(",", ""))).ToString();
                string rate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)dgvOrder.Rows[i].FindControl("txtgrvRate")).Text.Replace(",", ""))).ToString();
                DataSet ds4 = proc1.GetTransInfo(comcod, "SP_LC_INFO", "LCINFO2_UPDATE", actcode, rescode, ordrqty, rate, "", Freeqty, spcfcode, "", ""); //table Desc 6
                if (ds4 == null)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = this.proc1.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }

                ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            }




        }
        protected void dgvOrder_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            string comcod = this.ComCod();
            DataTable dt = (DataTable)ViewState["TblOrder"];
            string lcno2 = this.ddlLcCode.SelectedValue.ToString();
            string rescode = ((Label)this.dgvOrder.Rows[e.RowIndex].FindControl("lblgvResCode")).Text.Trim();
            DataSet result = proc1.GetTransInfo(comcod, "SP_LC_INFO", "DELETELCMAT", lcno2, rescode);

            if (result.Tables[0].Rows[0]["msg"] == "Sucess")
            {
                int RowIndex = dgvOrder.PageSize * dgvOrder.PageIndex + e.RowIndex;

                try
                {
                    if (ViewState["TblOrder"] == null)
                    {
                        return;
                    }
                    DataTable tbl1 = (DataTable)ViewState["TblOrder"];
                    tbl1.Rows[RowIndex].Delete();
                }
                catch (Exception ex)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = proc1.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);


                }
                this.LoadOrderDgv();
            }
            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            }
        }
        private void ShowStorList()
        {
            this.lnkReceive.Visible = true;
            this.lnkOk.Visible = false;
            string lcno2 = this.ddlLcCode.SelectedValue.ToString();
            string orderno = "";
            string comcod = this.ComCod();
            //string actcode = (comcod == "7307") ? "actcode like '1[57]%'" : "actcode like '17%'";
            string LcCode = ASTUtility.Left(this.ddlLcCode.SelectedValue.ToString(), 4);
            string LccodeType = (LcCode == "1401") ? "actcode like '15%'" : (LcCode == "1402") ? "actcode like '17%'" : (LcCode == "1403") ? "actcode like '11%'"
                : "actcode like '1[157]%'";
            DataSet ds5 = proc1.GetTransInfo(comcod, "SP_LC_INFO", "RETRIVELCSTORE1", LccodeType, "", "", "", "", "", "", "", ""); //table Desc 2
            this.ddlStorid.DataTextField = "actdesc1";
            this.ddlStorid.DataValueField = "actcode";
            this.ddlStorid.DataSource = ds5.Tables[0];
            this.ddlStorid.DataBind();
            //string dd=Convert.ToDateTime()
        }
        protected void lnkReceive_Click(object sender, EventArgs e)
        {

            this.pnlCosting.Visible = true;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            if (this.ddlStorid.Items.Count == 0)
            {

                ((Label)this.Master.FindControl("lblmsg")).Text = "Please Select Store Id.";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);


                return;
            }
            string lcno2 = this.ddlLcCode.SelectedValue.ToString();

            string rcvdat = this.txtreceivedate.Text.Trim().Substring(0, 11);
            string grrno = this.txtgrrno.Text.Trim();
            DataSet ds6 = proc1.GetTransInfo(comcod, "SP_INV_LC_INFO", "ORDERRECEIVE", lcno2, rcvdat, grrno, "", "", "", "", "", "");
            DataTable dt5 = ds6.Tables[0];

            if (ViewState["tblupdata"] != null)
            {

                this.pnlexcelheading.Visible = true;
                DataTable dt = (DataTable)ViewState["tblupdata"];
                List<RealEntity.C_09_LCM.EClassExcelData> lst = dt.DataTableToList<RealEntity.C_09_LCM.EClassExcelData>();
                //IEnumerable<RealEntity.C_09_LCM.EClassExcelData> lst1 = (from Product in lst
                //select Product).GroupBy(n => new {n.Product_Id}).Select(g=>g.FirstOrDefault());

                var lst1 = (from Product in lst
                            group Product by Product.Product_Id into g

                            select new SumClass
                            {
                                Product_Id = g.Key,
                                rcvqty = g.Count()
                            }).ToList();




                foreach (DataRow dr in dt5.Rows)
                {

                    string rescod = dr["rescod"].ToString();
                    dr["recqty"] = (lst1.FindAll(p => p.Product_Id == rescod)).Count > 0 ? (lst1.FindAll(p => p.Product_Id == rescod))[0].rcvqty : 0.00;


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

            ViewState["tbllccost"] = ds6.Tables[1];
            ViewState["TblReceive"] = dt5;
            LoadReceiveDgv();


        }

        class SumClass
        {
            public string Product_Id { get; set; }
            public double rcvqty { get; set; }

        }

        private void CostData_Bind()
        {
            DataTable tbl6 = (DataTable)ViewState["tbllccost"];
            this.gvlccost.DataSource = tbl6;
            this.gvlccost.DataBind();
            this.LCFooterCal();

        }
        private void LCFooterCal()
        {


            DataTable dt = (DataTable)ViewState["tbllccost"];

            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvlccost.FooterRow.FindControl("lblgrvFtolcCost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tolccost)", "")) ?
                           0 : dt.Compute("sum(tolccost)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvlccost.FooterRow.FindControl("lblgrvFprelcCost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(utorecamt)", "")) ?
                            0 : dt.Compute("sum(utorecamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvlccost.FooterRow.FindControl("lblgrvFcurlcCost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(recamt)", "")) ?
                            0 : dt.Compute("sum(recamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvlccost.FooterRow.FindControl("lblgrvFlcbalance")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(balamt)", "")) ?
                            0 : dt.Compute("sum(balamt)", ""))).ToString("#,##0.00;(#,##0.00); ");



        }
        private void LoadGRRNo()
        {
            string comcod = this.ComCod();
            string storid = this.ddlStorid.SelectedValue.ToString();
            string grrdat = this.txtreceivedate.Text.ToString().Substring(0, 11);
            DataSet tbl3 = proc1.GetTransInfo(comcod, "SP_LC_INFO", "GETGRRNO", grrdat, storid, "", "", "", "", "", "", "");
            this.txtgrrno.Text = tbl3.Tables[0].Rows[0]["grrno"].ToString();
        }
        protected void lnkTotalRcv_Click(object sender, EventArgs e)
        {
            this.Save_Rec_Value();
            this.LoadReceiveDgv();
        }
        private void LoadReceiveDgv()
        {
            try
            {
                DataTable tbl6 = (DataTable)ViewState["TblReceive"];

                this.dgvReceive.DataSource = tbl6;
                this.dgvReceive.DataBind();

                this.Rcv_Footcal();
                //((TextBox)this.dgvOrder.FindControl("txtexpeirdate")).Text = "12/12/2009"; 
                //((Label)this.grvItmCode.FooterRow.FindControl("lblgrvFamount")).Text = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(amount)", "")) ?
                //0.00 : tbl1.Compute("Sum(amount)", ""))).ToString("#,##0.00;(#,##0.00); ");
                //Session["EditProduct"] = tbl1;

            }
            catch (Exception ex)
            {


                ((Label)this.Master.FindControl("lblmsg")).Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);


            }

        }
        private void Rcv_Footcal()
        {
            try
            {

                DataTable dt1 = (DataTable)ViewState["TblReceive"];
                if (dt1.Rows.Count == 0)
                    return;
                ((Label)this.dgvReceive.FooterRow.FindControl("lblgvFordqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(ordrqty)", "")) ? 0.00 : dt1.Compute("sum(ordrqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.dgvReceive.FooterRow.FindControl("lblgrvFFreeqty1")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(freeqty)", "")) ? 0.00 : dt1.Compute("sum(freeqty)", ""))).ToString("#,##0.00;(#,##0.00); ");

                ((Label)this.dgvReceive.FooterRow.FindControl("lblgvFreuptlast")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(rcvuptolast)", "")) ? 0.00 : dt1.Compute("sum(rcvuptolast)", ""))).ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.dgvReceive.FooterRow.FindControl("lblgvFrmainord")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(remainordr)", "")) ? 0.00 : dt1.Compute("sum(remainordr)", ""))).ToString("#,##0.00;(#,##0.00); ");

                ((Label)this.dgvReceive.FooterRow.FindControl("lblgvFrcvQty")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(recqty)", "")) ? 0.00 : dt1.Compute("sum(recqty)", ""))).ToString("#,##0.00;(#,##0.00); ");

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);



            }

        }
        private void Save_Rec_Value()
        {
            DataTable dt = (DataTable)ViewState["TblReceive"];
            int RowIndex = 0;

            //  double tocost = Convert.ToDouble("0" + ((Label)this.gvlccost.FooterRow.FindControl("lblgrvFcurlcCost")).Text);
            for (int i = 0; i < this.dgvReceive.Rows.Count; i++)
            {
                double Qty = Convert.ToDouble("0" + ((TextBox)this.dgvReceive.Rows[i].FindControl("txtgvrcvQty")).Text.Trim());
                RowIndex = this.dgvReceive.PageIndex * this.dgvReceive.PageSize + i;
                dt.Rows[RowIndex]["recqty"] = Qty;
                dt.Rows[RowIndex]["recqty"] = Qty;
            }

            ViewState["TblReceive"] = dt;
        }
        protected void lnkFinalUpdateR_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string PostedByid = (this.Request.QueryString["Type"] == "Entry") ? userid : "";
            string Posttrmid = (this.Request.QueryString["Type"] == "Entry") ? Terminal : "";
            string PostSession = (this.Request.QueryString["Type"] == "Entry") ? Sessionid : "";
            string Posteddat = (this.Request.QueryString["Type"] == "Entry") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : "01-Jan-1900";
            string actcode = this.ddlLcCode.SelectedValue.ToString();
            string storid = this.ddlStorid.SelectedValue.ToString();
            string grrno = this.txtgrrno.Text.Trim();
            string rcvdate = this.txtreceivedate.Text.Substring(0, 11).ToString();
            this.Save_Rec_Value();
            string Edit = "";
            if (ViewState["TblReceive"] != null)
            {
                DataTable tbl4 = ((DataTable)ViewState["TblReceive"]).Copy();
                DataTable dt = ((DataTable)ViewState["tbllccost"]).Copy();

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
                string expdat = tbl4.Rows[0]["date"].ToString();
                string lcno = this.ddlLcCode.SelectedValue.ToString();
                string expdat1 = Convert.ToDateTime((expdat == "" ? "1-Jan-1900" : expdat)).ToString("dd-MMM-yyyy");
                DataSet ds112 = proc1.GetTransInfoNew(comcod, "SP_LC_INFO", "UPDATELCDETAILS", ds1, null, null, Edit, rcvdate, storid, lotno, expdat1, PostedByid, PostSession, Posttrmid, Posteddat, lcno, "", "", "", "", "");
                if (ds112.Tables[0].Rows.Count != 0)
                {
                    this.txtgrrno.Text = ds112.Tables[0].Rows[0]["memonum"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Update Successfully');", true);
                    ((LinkButton)this.dgvReceive.FooterRow.FindControl("lnkFinalUpdateR")).Enabled = false;
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
                grrno = this.txtgrrno.Text.Trim();
                bool result = proc1.UpdateXmlTransInfo(comcod, "SP_INV_LC_INFO", "INSORUPDATELCRECINFC", ds1xml, null, null, grrno, actcode, storid, "", "", "", "", "", "", "", "", "", "", "", "");

                if (!result)
                {

                    ((Label)this.Master.FindControl("lblmsg")).Text = proc1.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

                    return;
                }

            }



            //------------------Rate Update------------------//
            //  string imp2 = Request.QueryString["tid"].ToString();
            //if (imp2 == "lc")
            //{
            //    RateUpdate();
            //}
            //else if (imp2 == "sup")
            //{
            //    RateUpdate2();
            //}
            //else if (imp2 == "cst")
            //{

            //}
        }




        private void RateUpdate()
        {
            string lccode4 = this.ddlLcCode.SelectedValue.ToString();
            DataSet ds8 = proc1.GetTransInfo(ComCod(), "SP_LC_INFO", "SHOWLCRESCOST", lccode4, "LCINFO3", "", "", "", "", "", "", ""); // table desc 2
            DataTable dt8 = ds8.Tables[0];
            for (int i = 0; i < dt8.Rows.Count; i++)
            {
                string rescode = dt8.Rows[i]["rescod"].ToString();
                //string rcvqty = dt8.Rows[i]["rcvqty"].ToString();
                string ratetotal = dt8.Rows[i]["ratetotal"].ToString();
                DataSet ds9 = proc1.GetTransInfo(ComCod(), "SP_LC_INFO", "LCINFO3_REUPDATE", lccode4, rescode, ratetotal, "LCINFO3", "", "", "", "", ""); // table Desc 4
                if (ds9 == null)
                {

                    ((Label)this.Master.FindControl("lblmsg")).Text = proc1.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

                    return;
                }
            }
        }

        private void RateUpdate2()
        {
            string supcod = this.ddlLcCode.SelectedValue.ToString();
            string orderno = "";
            DataSet ds8 = proc1.GetTransInfo(ComCod(), "SP_LC_INFO", "SHOWSUPRESCOST", supcod, orderno, "LCINFO2", "", "", "", "", "", ""); // table Desc 4
            DataTable dt8 = ds8.Tables[0];
            for (int i = 0; i < dt8.Rows.Count; i++)
            {
                string rescode = dt8.Rows[i]["rescod"].ToString();
                string ratetotal = dt8.Rows[i]["rate"].ToString();
                DataSet ds9 = proc1.GetTransInfo(ComCod(), "SP_LC_INFO", "LCINFO3_REUPDATE", supcod, rescode, ratetotal, "LCINFO3", "", "", "", "", ""); //table Desc 4
                if (ds9 == null)
                {


                    ((Label)this.Master.FindControl("lblmsg")).Text = this.proc1.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

                    return;
                }
            }
        }
        protected void lbldetails1_Click(object sender, EventArgs e)
        {
            GetOrderId();
        }

        //protected void lnkpreorder_Click(object sender, EventArgs e)
        //{
        //    string supno = this.ddlLcCode.SelectedValue.ToString();
        //    DataSet ds4 = proc1.GetTransInfo(ComCod(), "SP_LC_INFO", "RETRIVEORINFO", supno, "LCINFO1", "", "", "", "", "", "", ""); // table Desc 2
        //    if (ds4.Tables[0].Rows.Count <= 0)
        //    {
        //        this.lmsg.Text = "Error:There is no order. ";
        //        this.lmsg.ForeColor = System.Drawing.Color.Red;
        //        return;
        //    }
        //    this.ddlordrno.DataTextField = "prdtdtl1";
        //    this.ddlordrno.DataValueField = "prdtdtl1";
        //    this.ddlordrno.DataSource = ds4.Tables[0];
        //    this.ddlordrno.DataBind();
        //    this.lmsg.Text = "";
        //    GetOrdrinfo();
        //}
        private void GetOrdrinfo()
        {
            //string supno = this.ddlLcCode.SelectedValue.ToString();
            //string ordrid = this.ddlordrno.SelectedValue.ToString();
            //DataSet ds4 = proc1.GetTransInfo(ComCod(), "SP_LC_INFO", "RTVORDRINFO", supno, ordrid, "LCINFO1", "", "", "", "", "", ""); //table Desc 3
            //DataTable dt3 = ds4.Tables[0];

            //this.txtLcDate.Text = Convert.ToDateTime(dt3.Rows[0]["lcdate"]).ToString("dd-MMM-yyyy");
            //this.txtShipDate.Text = Convert.ToDateTime(dt3.Rows[0]["shipdate"]).ToString("dd-MMM-yyyy");
            //this.txtShipADate.Text = Convert.ToDateTime(dt3.Rows[0]["shipardate"]).ToString("dd-MMM-yyyy");
            //this.txtDelDate.Text = Convert.ToDateTime(dt3.Rows[0]["deldate"]).ToString("dd-MMM-yyyy");
            //this.txtLcExpDate.Text = Convert.ToDateTime(dt3.Rows[0]["expdate"]).ToString("dd-MMM-yyyy");



            //this.txtsup.Text = dt3.Rows[0]["splrname"].ToString();
            ////this.txtcsup.Text = dt3.Rows[0]["csplname"].ToString();
            //this.ddlSupplier.SelectedValue = dt3.Rows[0]["csplname"].ToString();

            ////this.txtbank.Text = dt3.Rows[0]["bankname"].ToString();
            //this.ddlbank.SelectedValue = dt3.Rows[0]["bankname"].ToString();
            ////this.txtcur.Text = dt3.Rows[0]["currency"].ToString();
            //this.ddlcur.SelectedValue = dt3.Rows[0]["currency"].ToString();
            //this.txtconv.Text = Convert.ToDouble(dt3.Rows[0]["cnvrsion"]).ToString("#,##0.00;(#,##0.00); ");
            //this.txtdetails1.Text = dt3.Rows[0]["prdtdtl1"].ToString();
            //this.txtdetails2.Text = dt3.Rows[0]["prdtdtl2"].ToString();
            //this.txtdetails3.Text = dt3.Rows[0]["prdtdtl3"].ToString();
            //this.txtdetails4.Text = dt3.Rows[0]["prdtdtl4"].ToString();
            //this.txtdetails5.Text = dt3.Rows[0]["prdtdtl5"].ToString();
            //this.txtdetails6.Text = dt3.Rows[0]["prdtdtl6"].ToString();
            showOrder();

        }
        protected void ddlordrno_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetOrdrinfo();
            RmvRcvGrd();
        }
        private void RmvRcvGrd()
        {
            this.dgvReceive.DataSource = null;
            this.dgvReceive.DataBind();
        }

        //private void LoadAcccombo()
        //{
        //    try
        //    {
        //        Hashtable hst = (Hashtable)Session["tblLogin"];
        //        string comcod = hst["comcod"].ToString();
        //        string ttsrch = "%";
        //        string UserId = hst["usrid"].ToString();
        //        DataSet ds1 = proc1.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETCONHEADPAONPAY", ttsrch, UserId, "", "", "", "", "", "", "");
        //        DataTable dt1 = ds1.Tables[0];
        //        this.ddlbank.DataSource = dt1;
        //        this.ddlbank.DataTextField = "actdesc1";
        //        this.ddlbank.DataValueField = "actcode";
        //        this.ddlbank.DataBind();
        //        //this.GetPriviousVoucher();
        //    }
        //    catch (Exception ex)
        //    {
        //        // this.lmsg.Text = "Error:" + ex.Message;
        //    }

        //}

        private void CurrencyInf()
        {
            DataSet ds = lst.Curreny();
            var lstConv = ds.Tables[0].DataTableToList<RealEntity.C_22_Sal.Sales_BO.ConvInf>();
            ViewState["tblcur"] = lstConv;

            var lstCurryDesc = ds.Tables[1].DataTableToList<RealEntity.C_22_Sal.Sales_BO.Currencyinf>();
            ViewState["tblcurdesc"] = lstCurryDesc;

        }
        private void ResourceCode()
        {
            string comcod = this.ComCod();
            string filter1 = "%%";
            string actcode = this.ddlLcCode.SelectedValue.ToString();
            //string LcCode1 = ASTUtility.Left(this.ddlLcCode.SelectedValue.ToString(), 8);
            string curdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string LcCode = ASTUtility.Left(this.ddlLcCode.SelectedValue.ToString(), 4);
            //LcCode = (LcCode == "1401") ? LcCode1 : LcCode;

            DataTable dt = (DataTable)ViewState["tblStoreType"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("actcode='" + actcode + "'");
            dt = dv.ToTable();
            string Codetype = dt.Rows[0]["code"].ToString().Trim();
            string SearchInfo = "";
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

            SearchInfo = (SearchInfo.Length == 0) ? ((LcCode == "1401") ? "sircode like '0101%'"
                : (LcCode == "1402") ? "sircode like '4[12]%'"
                : (LcCode == "1403") ? "sircode like '21%'" : "sircode like '0101%' or sircode like '41%' or sircode like '21%'") : SearchInfo;

            //string LccodeType = ((LcCode == "14010001") || (LcCode == "14010002")) ? "sircode like '0101%'" : (LcCode == "14010002") ? "sircode like '0102%'"
            //    : (LcCode == "14010003") ? "sircode like '0103%'" : (LcCode == "14010004") ? "sircode like '0104%'"
            //    : (LcCode == "14010051") ? "sircode like '010100110%' or sircode like '010100152%' or sircode like '010100153%'"
            //    : (LcCode == "1402") ? "sircode like '4[12]%'" 
            //    : (LcCode == "1403") ? "sircode like '21%'" : "sircode like '0101%' or sircode like '41%' or sircode like '21%'";

            //string LccodeType = (LcCode == "14010001") ? "sircode like '0101%'" : (LcCode == "14010002") ? "sircode like '0102%'"
            //    : (LcCode == "14010003") ? "sircode like '0103%'" : (LcCode == "14010004") ? "sircode like '0104%'"
            //    : (LcCode == "1402") ? "sircode like '4[12]%'"
            //    : (LcCode == "1403") ? "sircode like '21%'" : "sircode like '0101%' or sircode like '41%' or sircode like '21%'";

            //DataSet ds4 = proc1.GetTransInfo(comcod, "SP_LC_INFO", "RETRIVELCRES", filter1, "sirinf_v", LccodeType, "", "", "", "", "", "");  // table Desc 2
            DataSet ds1 = proc1.GetTransInfo(comcod, "SP_LC_INFO", "MATCODELIST", "000000000000", curdate, "%", SearchInfo, "", "", "", "", "");

            this.ddlResList.DataTextField = "resdesc1";
            this.ddlResList.DataValueField = "rescod";
            this.ddlResList.DataSource = ds1.Tables[0];
            this.ddlResList.DataBind();
            ViewState["Material"] = ds1.Tables[0];
            ViewState["tblSpcf"] = ds1.Tables[1];
            ImgbtnSpecification_Click(null, null);
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string firstQuery = Request.QueryString["tname"].ToString();
            string secondQuery = Request.QueryString["tid"].ToString();

            if (firstQuery == "order" && secondQuery == "lc")
            {
                ReportDocument rfo = new RealERPRPT.R_09_LCM.rptForeignOrder();
                DataTable dataOrder1 = (DataTable)ViewState["TblOrder"];

                TextObject rptNames = rfo.ReportDefinition.ReportObjects["rptName"] as TextObject;
                rptNames.Text = "L/C Opening Report";

                TextObject txtCompanyName = rfo.ReportDefinition.ReportObjects["companyname"] as TextObject;
                txtCompanyName.Text = comnam;




                TextObject date = rfo.ReportDefinition.ReportObjects["txtdate"] as TextObject;
                date.Text = "LC Date: " + "";

                //TextObject txtdate = rfo.ReportDefinition.ReportObjects["txtdate"] as TextObject;
                //txtdate.Text = "LC Date: " + this.txtShipDate.Text;

                //TextObject date2 = rfo.ReportDefinition.ReportObjects["txtdate"] as TextObject;
                //date2.Text = "LC Date: " + this.txtShipADate.Text;

                //TextObject date3 = rfo.ReportDefinition.ReportObjects["txtdate"] as TextObject;
                //date3.Text = "LC Date: " + this.txtDelDate.Text;

                //TextObject date4 = rfo.ReportDefinition.ReportObjects["txtdate"] as TextObject;
                //date4.Text = "LC Date: " + this.txtLcExpDate.Text;



                //TextObject baton = rfo.ReportDefinition.ReportObjects["txtBank"] as TextObject;
                //baton.Text = "Bank Name: " + this.ddlbank.SelectedItem.Text;

                //TextObject pro = rfo.ReportDefinition.ReportObjects["txtlc"] as TextObject;
                //pro.Text = "L/C Number : " + this.ddlLcCode.SelectedItem.Text.Substring(13);

                //TextObject curr = rfo.ReportDefinition.ReportObjects["txtCurr"] as TextObject;
                //curr.Text = "Currency: " + this.ddlcur.SelectedItem.Text;

                //TextObject conv = rfo.ReportDefinition.ReportObjects["txtConv"] as TextObject;
                //conv.Text = "Convertion Rate: " + this.txtconv.Text;

                //TextObject Order = rfo.ReportDefinition.ReportObjects["txtOrder"] as TextObject;
                //Order.Text = "Order No: " + this.txtsup.Text;

                //TextObject sup = rfo.ReportDefinition.ReportObjects["txtSup"] as TextObject;
                //sup.Text = "Supplier: " + this.ddlSupplier.SelectedItem.Text.ToString().Trim();
                //TextObject txtdet1 = rfo.ReportDefinition.ReportObjects["txtdet1"] as TextObject;
                //txtdet1.Text = "Details: " + this.txtdetails1.Text;
                //TextObject txtdet2 = rfo.ReportDefinition.ReportObjects["txtdet2"] as TextObject;
                //txtdet2.Text = "Details: " + this.txtdetails2.Text;

                TextObject txtuserinfo = rfo.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

                //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                //rfo.SetParameterValue("ComLogo", ComLogo);


                rfo.SetDataSource(dataOrder1);
                Session["Report1"] = rfo;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                               ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            }
            else if (firstQuery == "order" && secondQuery == "sup")
            {
                ReportDocument rfo2 = new RealERPRPT.R_09_LCM.rptForeignOrder();
                DataTable dataOrder2 = (DataTable)ViewState["TblOrder"];
                TextObject rptNames = rfo2.ReportDefinition.ReportObjects["rptName"] as TextObject;
                rptNames.Text = "Local Order Report";

                TextObject txtCompanyName = rfo2.ReportDefinition.ReportObjects["companyname"] as TextObject;
                txtCompanyName.Text = comnam;
                //TextObject date = rfo2.ReportDefinition.ReportObjects["txtdate"] as TextObject;
                //date.Text = "LC Date: " + this.txtLcExpDate.Text;

                //TextObject baton = rfo2.ReportDefinition.ReportObjects["txtBank"] as TextObject;
                //baton.Text = "Bank Name: " + this.ddlbank.SelectedItem.Text;

                //TextObject pro = rfo2.ReportDefinition.ReportObjects["txtlc"] as TextObject;
                //pro.Text = "L/C Number : " + this.ddlLcCode.SelectedItem.Text.Substring(13);

                //TextObject curr = rfo2.ReportDefinition.ReportObjects["txtCurr"] as TextObject;
                //curr.Text = "Currency: " + this.ddlcur.SelectedItem.Text;

                //TextObject conv = rfo2.ReportDefinition.ReportObjects["txtConv"] as TextObject;
                //conv.Text = "Convertion Rate: " + this.txtconv.Text;

                //TextObject Order = rfo2.ReportDefinition.ReportObjects["txtOrder"] as TextObject;
                //Order.Text = "Order No: " + this.txtsup.Text;

                //TextObject sup = rfo2.ReportDefinition.ReportObjects["txtSup"] as TextObject;
                //sup.Text = "C. Supplier: " + this.ddlSupplier.SelectedItem.Text.ToString().Trim();
                //TextObject txtdet1 = rfo2.ReportDefinition.ReportObjects["txtdet1"] as TextObject;
                //txtdet1.Text = "Details: " + this.txtdetails1.Text;
                //TextObject txtdet2 = rfo2.ReportDefinition.ReportObjects["txtdet2"] as TextObject;
                //txtdet2.Text = "Details: " + this.txtdetails2.Text;


                TextObject txtuserinfo = rfo2.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
                //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                //rfo2.SetParameterValue("ComLogo", ComLogo);

                rfo2.SetDataSource(dataOrder2);
                Session["Report1"] = rfo2;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                               ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }
            else if (firstQuery == "costing" && secondQuery == "cst")
            {
                ReportDocument rfo3 = new RealERPRPT.R_09_LCM.rptLCCosting();


                TextObject txtCompanyName = rfo3.ReportDefinition.ReportObjects["companyname"] as TextObject;
                txtCompanyName.Text = comnam;
                TextObject pro = rfo3.ReportDefinition.ReportObjects["txtlc"] as TextObject;
                pro.Text = "L/C Number : " + this.ddlLcCode.SelectedItem.Text.Substring(13);

                TextObject txtuserinfo = rfo3.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

                //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                //rfo.SetParameterValue("ComLogo", ComLogo);


                rfo3.SetDataSource((DataTable)ViewState["TblOrder"]);
                Session["Report1"] = rfo3;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            }
            else if (firstQuery == "receive" && secondQuery == "lc")
            {

                ReportDocument rfmr = new RealERPRPT.R_09_LCM.rptForeignMaterialReceive();
                DataTable d1 = (DataTable)ViewState["TblReceive"];
                //DataTable d2 = (DataTable)Session["LoadGRRNo"];
                //TextObject mrrN = rfmr.ReportDefinition.ReportObjects["MrnNo"] as TextObject;
                //mrrN.Text = d2.Rows[0]["grrno"].ToString();
                TextObject rptNames = rfmr.ReportDefinition.ReportObjects["rptName"] as TextObject;
                rptNames.Text = "Foreign Material Receiving Report";

                TextObject txtCompanyName = rfmr.ReportDefinition.ReportObjects["companyname"] as TextObject;
                txtCompanyName.Text = comnam;
                //TextObject date = rfmr.ReportDefinition.ReportObjects["txtdate"] as TextObject;
                //date.Text = "LC Date: " + this.txtLcExpDate.Text;

                //TextObject baton = rfmr.ReportDefinition.ReportObjects["txtBank"] as TextObject;
                //baton.Text = "Bank Name: " + this.ddlbank.SelectedItem.Text;

                //TextObject pro = rfmr.ReportDefinition.ReportObjects["txtlc"] as TextObject;
                //pro.Text = "L/C Number : " + this.ddlLcCode.SelectedItem.Text.Substring(13);

                //TextObject curr = rfmr.ReportDefinition.ReportObjects["txtCurr"] as TextObject;
                //curr.Text = "Currency: " + this.ddlcur.SelectedItem.Text;

                //TextObject conv = rfmr.ReportDefinition.ReportObjects["txtConv"] as TextObject;
                //conv.Text = "Convertion Rate: " + this.txtconv.Text;

                //TextObject Order = rfmr.ReportDefinition.ReportObjects["txtOrder"] as TextObject;
                //Order.Text = "Order No: " + this.txtsup.Text;

                ///

                TextObject Strore = rfmr.ReportDefinition.ReportObjects["txtStrore"] as TextObject;
                Strore.Text = "Store Name: " + this.ddlStorid.SelectedItem.Text.Substring(13);
                TextObject Grn = rfmr.ReportDefinition.ReportObjects["txtGrn"] as TextObject;
                Grn.Text = "GRN No: " + this.txtgrrno.Text;
                TextObject RecDat = rfmr.ReportDefinition.ReportObjects["txtRecDat"] as TextObject;
                RecDat.Text = "Received Date: " + this.txtreceivedate.Text;
                ////////////////

                TextObject txtuserinfo = rfmr.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);


                rfmr.SetDataSource(d1);
                Session["Report1"] = rfmr;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }
            else
            {
                ReportDocument rlmr = new RealERPRPT.R_09_LCM.rptForeignMaterialReceive();
                DataTable d11 = (DataTable)ViewState["TblReceive"];
                DataTable d22 = (DataTable)ViewState["LoadGRRNo"];
                TextObject mrrN = rlmr.ReportDefinition.ReportObjects["MrnNo"] as TextObject;
                mrrN.Text = d22.Rows[0]["grrno"].ToString();
                TextObject rptName = rlmr.ReportDefinition.ReportObjects["rptName"] as TextObject;
                rptName.Text = "Local Material Receive Report";

                TextObject txtCompanyName = rlmr.ReportDefinition.ReportObjects["companyname"] as TextObject;
                txtCompanyName.Text = comnam;
                //TextObject date = rlmr.ReportDefinition.ReportObjects["txtdate"] as TextObject;
                //date.Text = "LC Date: " + this.txtLcExpDate.Text;

                //TextObject baton = rlmr.ReportDefinition.ReportObjects["txtBank"] as TextObject;
                //baton.Text = "Bank Name: " + this.ddlbank.SelectedItem.Text;

                //TextObject pro = rlmr.ReportDefinition.ReportObjects["txtlc"] as TextObject;
                //pro.Text = "L/C Number : " + this.ddlLcCode.SelectedItem.Text.Substring(13);

                //TextObject curr = rlmr.ReportDefinition.ReportObjects["txtCurr"] as TextObject;
                //curr.Text = "Currency: " + this.ddlcur.SelectedItem.Text;

                //TextObject conv = rlmr.ReportDefinition.ReportObjects["txtCurr"] as TextObject;
                //conv.Text = "Convertion Rate: " + this.txtconv.Text;

                //TextObject Order = rlmr.ReportDefinition.ReportObjects["txtOrder"] as TextObject;
                //Order.Text = "Order No: " + this.ddlSupplier.SelectedItem.Text.ToString().Trim();// this.txtcsup.Text;

                TextObject Strore = rlmr.ReportDefinition.ReportObjects["txtStrore"] as TextObject;
                Strore.Text = "Store Name: " + this.ddlStorid.SelectedItem.Text.Substring(13);
                TextObject Grn = rlmr.ReportDefinition.ReportObjects["txtGrn"] as TextObject;
                Grn.Text = "GRN No: " + this.txtgrrno.Text;
                TextObject RecDat = rlmr.ReportDefinition.ReportObjects["txtRecDat"] as TextObject;
                RecDat.Text = "Received Date: " + this.txtreceivedate.Text;

                TextObject txtuserinfo = rlmr.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

                rlmr.SetDataSource(d11);
                Session["Report1"] = rlmr;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }
        }


        /// <summary>
        /// Rate Update
        /// </summary>

        protected void lnkRateupdate_Click(object sender, EventArgs e)
        {

            string actcode = this.ddlLcCode.SelectedValue.ToString();
            for (int i = 0; i < this.dgvCosting.Rows.Count; i++)
            {
                string rescode = ((Label)dgvCosting.Rows[i].FindControl("lblgvResCode2")).Text.Replace("-", "");
                string trate = "0" + ((Label)dgvCosting.Rows[i].FindControl("lblgvTotalrat")).Text.Replace(",", "");

                DataSet ds07 = proc1.GetTransInfo(ComCod(), "SP_LC_INFO", "LCINFO3_UPDATE01", actcode, rescode, trate, "LCINFO3", "", "", "", "", ""); // table Desc 4
                if (ds07 == null)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = proc1.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);


                    //this.lmsg.ForeColor = System.Drawing.Color.Red;

                    return;
                }

                ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

                // this.lmsg.ForeColor = System.Drawing.Color.Green;

            }
        }
        /// <summary>
        /// All Over Rate Update
        /// </summary>    
        protected void lnkovrallupdate_Click(object sender, EventArgs e)
        {

            DataSet ds08 = proc1.GetTransInfo(ComCod(), "SP_LC_INFO", "OVERALLUPDATE", "ALL", "", "", "", "", "", "", "", ""); //table Desc 1
            if (ds08 == null)
            {

                ((Label)this.Master.FindControl("lblmsg")).Text = this.proc1.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

                // this.lmsg.ForeColor = System.Drawing.Color.Red;

                return;
            }

            ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            //this.lmsg.ForeColor = System.Drawing.Color.Green;
        }
        protected void imgbtnPreGrn_Click(object sender, EventArgs e)
        {

            this.PreGrn();
        }

        protected void lnkOk_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            if (this.ddlPreGrn.Items.Count == 0)
            {

                ((Label)this.Master.FindControl("lblmsg")).Text = "Please Select Pre No.";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);


                return;
            }
            string lcno2 = this.ddlLcCode.SelectedValue.ToString();
            string preno = this.ddlPreGrn.SelectedValue.ToString();

            DataSet ds6 = proc1.GetTransInfo(comcod, "SP_LC_INFO", "GETPREGRNINFO", lcno2, preno, "", "", "", "", "", "", "");
            DataTable dt5 = ds6.Tables[0];
            ViewState["TblReceive"] = dt5;

            this.txtreceivedate.Text = Convert.ToDateTime(dt5.Rows[0]["rcvdate"]).ToString("dd-MMM-yyyy");
            this.txtgrrno.Text = dt5.Rows[0]["grrno"].ToString();
            LoadReceiveDgv();
        }


        protected void lnkSameValue_Click(object sender, EventArgs e)
        {
            this.Save_Value();
            LoadOrderDgv();

        }
        private void Save_Value()
        {
            DataTable dt = (DataTable)ViewState["TblOrder"];

            int RowIndex = 0;

            for (int i = 0; i < this.dgvOrder.Rows.Count; i++)
            {
                double Qty = Convert.ToDouble("0" + ((TextBox)this.dgvOrder.Rows[i].FindControl("txtgrvOrderQty")).Text.Trim());
                RowIndex = this.dgvOrder.PageIndex * this.dgvOrder.PageSize + i;
                dt.Rows[RowIndex]["ordrqty"] = Qty;
            }

            ViewState["TblOrder"] = dt;
        }
        protected void dgvReceive_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                TextBox gvrcvQty = (TextBox)e.Row.FindControl("txtgvrcvQty");

                double balqty = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "recqty"));


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
        //protected void dgvOrder_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    this.Save_Value();
        //    this.dgvOrder.PageIndex = e.NewPageIndex;
        //    this.LoadOrderDgv();

        //}

        protected void chkExel_CheckedChanged(object sender, EventArgs e)
        {
            this.pnlExel.Visible = this.chkExel.Checked;
        }
        protected void lbtnUploadData_Click(object sender, EventArgs e)
        {

        }


        protected void lnkTotalLcCost_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tbllccost"];
            int RowIndex = 0;

            for (int i = 0; i < this.gvlccost.Rows.Count; i++)
            {
                double tolccost = Convert.ToDouble("0" + ((Label)this.gvlccost.Rows[i].FindControl("lblgvtolcCost")).Text.Trim());
                double utorecamt = Convert.ToDouble("0" + ((Label)this.gvlccost.Rows[i].FindControl("lblgvprelcCost")).Text.Trim());
                double recamt = ASTUtility.StrPosOrNagative((((TextBox)this.gvlccost.Rows[i].FindControl("txtgvcurlcCostt")).Text.Trim()));

                RowIndex = this.dgvOrder.PageIndex * this.dgvOrder.PageSize + i;
                dt.Rows[RowIndex]["recamt"] = recamt;
                dt.Rows[RowIndex]["balamt"] = tolccost - utorecamt - recamt;
            }

            ViewState["tbllccost"] = dt;
            this.CostData_Bind();
        }
        protected void ddlResList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ImgbtnSpecification_Click(null, null);
        }
        protected void ImgbtnSpecification_Click(object sender, EventArgs e)
        {


            string mResCode = this.ddlResList.SelectedValue.ToString().Substring(0, 9);
            // string spcfcod1 = this.ddlResSpcf.SelectedValue.ToString();
            this.ddlResSpcf.Items.Clear();
            DataTable tbl1 = (DataTable)ViewState["tblSpcf"];
            DataView dv1 = tbl1.DefaultView;
            dv1.RowFilter = "mspcfcod = '" + mResCode + "' or mspcfcod = '000000000'";
            DataTable dt = dv1.ToTable();

            if (dt.Rows.Count > 1)
            {
                dt.Rows[0].Delete();
            }



            this.ddlResSpcf.DataTextField = "spcfdesc";
            this.ddlResSpcf.DataValueField = "spcfcod";
            this.ddlResSpcf.DataSource = dt;
            this.ddlResSpcf.DataBind();

        }
    }
}
