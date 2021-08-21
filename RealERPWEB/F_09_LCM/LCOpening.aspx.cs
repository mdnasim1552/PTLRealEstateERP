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
using RealEntity;
using RealERPLIB;
using RealEntity.C_22_Sal;
namespace RealERPWEB.F_09_LCM
{
    public partial class LCOpening : System.Web.UI.Page
    {
        ProcessAccess proc1 = new ProcessAccess();
        static string prevPage = String.Empty;
        SalesInvoice_BL lst = new SalesInvoice_BL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                prevPage = Request.UrlReferrer.ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = (Request.QueryString["Type"].ToString() == "Open") ? "L/C Openning" :
                    (Request.QueryString["Type"].ToString() == "receive") ? "Foreign Material Recived" : "L/C Costing";   //=




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
                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Text = "Approve";
                ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
                ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;

                imgbtnLcsearch_Click(null, null);
                this.GetOther();
                this.CurrencyInf();
            }


        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            //((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
            //(LinkButton)this.Master.FindControl("lnkbtnLedger")).Click += new EventHandler(lnkbtnLedger_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Click += new EventHandler(lnkPrint_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnTranList")).Click += new EventHandler(lnkPrint_Click);

            //((LinkButton)this.Master.FindControl("lnkbtnNew")).Click += new EventHandler(btnNew_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnAdd")).Click += new EventHandler(lnkbtnAdd_Click1);
            // ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Click += new EventHandler(lnkbtnEdit_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(btnUpdate_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(btnReCalculate_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnDelete")).Click += new EventHandler(lnkbtnDelete_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
            //((CheckBox)this.Master.FindControl("chkBoxN")).CheckedChanged += new EventHandler(chkPayment_CheckedChanged);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string comcod = this.ComCod();
            string reqno = this.Request.QueryString["genno"].ToString();
            string actcode = this.ddlLcCode.SelectedValue.ToString();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            DataSet ds = proc1.GetTransInfoNew(comcod, "SP_LC_INTERFACE", "APPROVE_LC_REQ", null, null, null, reqno, actcode, userid, Sessionid, Terminal, Posteddat);


            if (ds == null)
                return;

            this.lmsg.Visible = true;
            this.lmsg.Text = ds.Tables[0].Rows[0]["msg"].ToString();



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

        protected void imgbtnLcsearch_Click(object sender, EventArgs e)
        {

            string comcod = this.ComCod();
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
            this.lmsg.Visible = true;
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



            ViewState["tblLcinfo"] = dt;

        }


        protected void detailsinfo()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            this.SaveValue();
            string actcode = this.ddlLcCode.SelectedValue.ToString();
            DataSet ds1 = new DataSet("ds1");
            DataTable dt = ((DataTable)ViewState["tblLcinfo"]).Copy();
            ds1.Tables.Add(dt);
            ds1.Tables[0].TableName = "tbl1";
            //string xml = ds1.GetXml();
            //return;
            bool result = proc1.UpdateXmlTransInfo(comcod, "SP_LC_INFO", "INSERTORUPDATELCINF", ds1, null, null, actcode, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (result == true)
            {
                this.lmsg.Text = "Updated Successfully";
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
        private void CurrencyInf()
        {
            DataSet ds = lst.Curreny();
            var lstConv = ds.Tables[0].DataTableToList<RealEntity.C_22_Sal.Sales_BO.ConvInf>();
            ViewState["tblcur"] = lstConv;

            var lstCurryDesc = ds.Tables[1].DataTableToList<RealEntity.C_22_Sal.Sales_BO.Currencyinf>();
            ViewState["tblcurdesc"] = lstCurryDesc;

        }
        protected void lnkOpen_Click(object sender, EventArgs e)
        {
            this.lmsg.Visible = false;
            //this.LoadAcccombo();
            //this.CurrencyInf();
            //this.SupplierInfo();
            this.ResourceCode();
            if (this.lnkOpen.Text == "Open")
            {
                this.lnkOpen.Text = "New";
                //--------------------------Order,Receive,Costing-----------//
                string criteria = Request.QueryString["Type"].ToString();
                switch (criteria)
                {
                    case "Open":
                        this.GetGenInfo();
                        this.CallOrder();
                        break;


                }
                this.ddlLcCode.Enabled = false;
                this.lbtnReq.Visible = true;
                //--------------------------------------//

            }
            else
            {
                this.lnkOpen.Text = "Open";
                this.MultiView1.ActiveViewIndex = -1;
                this.ddlLcCode.Enabled = true;
                this.lbtnReq.Visible = false;
                this.gvPersonalInfo.DataSource = null;
                this.gvPersonalInfo.DataBind();
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
                        ddlgval.Attributes["style"] = "width:200px;";
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
                        ddlgval.Attributes["style"] = "width:200px;";
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
                        ddlgval.Attributes["style"] = "width:200px;";
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

                            string conrate = Convert.ToDouble((lst1.FindAll(p => p.fcode == fcode && p.tcode == tcode))[0].conrate).ToString("#,##0.0000;-#,##0.0000; ");
                            ((TextBox)this.gvPersonalInfo.Rows[i + 1].FindControl("txtgvVal")).Text = Convert.ToDouble((lst1.FindAll(p => p.fcode == fcode && p.tcode == tcode))[0].conrate).ToString("#,##0.0000;-#,##0.0000; ");
                            this.txtconv.Text = Convert.ToDouble((lst1.FindAll(p => p.fcode == fcode && p.tcode == tcode))[0].conrate).ToString("#,##0.0000;-#,##0.0000; ");
                        }

                        break;

                }

            }
        }

        private void CallOrder()
        {

            string tname = Request.QueryString["Type"].ToString();

            try
            {

                if (tname == "Open")
                {
                    this.Panel3.Visible = true;
                }
                this.MultiView1.ActiveViewIndex = 0;
                this.showOrder();
                LoadOrderDgv();

            }


            catch (Exception ex)
            {
                this.lmsg.Text = "Error:" + ex.Message;
            }


        }
        private void showOrder()
        {
            ViewState.Remove("TblOrder");
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
            this.lmsg.Text = "";

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
                this.lmsg.Text = "Error:" + ex.Message;
            }

        }

        protected void lnkTotal_Click(object sender, EventArgs e)
        {
            this.Calculation();
            this.LoadOrderDgv();
            this.lmsg.Visible = false;
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
                this.lmsg.Text = "Error:There is no Order!! ";
            }


        }
        protected void lnkFinalUpdate_Click(object sender, EventArgs e)    // fn up order
        {
            this.lmsg.Visible = true;
            string comcod = this.ComCod();
            string actcode = this.ddlLcCode.SelectedValue.ToString();
            this.Calculation();
            string orderid = this.Request.QueryString["genno"].ToString();


            for (int i = 0; i < dgvOrder.Rows.Count; i++)
            {

                string rescode = ((Label)dgvOrder.Rows[i].FindControl("lblgvResCode")).Text.Replace("-", "");
                string spcfcode = ((Label)dgvOrder.Rows[i].FindControl("lblgrvspccode")).Text.ToString();
                string ordrqty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)dgvOrder.Rows[i].FindControl("txtgrvOrderQty")).Text.Replace(",", ""))).ToString();
                string Freeqty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)dgvOrder.Rows[i].FindControl("txtgrvFreeqty")).Text.Replace(",", ""))).ToString();
                string rate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)dgvOrder.Rows[i].FindControl("txtgrvRate")).Text.Replace(",", ""))).ToString();
                DataSet ds4 = proc1.GetTransInfo(comcod, "SP_LC_INFO", "LCINFO2_UPDATE", actcode, rescode, ordrqty, rate, orderid, Freeqty, spcfcode, "", ""); //table Desc 6
                if (ds4 == null)
                {
                    this.lmsg.Text = "Error:" + proc1.ErrorObject["Msg"].ToString();
                    // this.lmsg.ForeColor = System.Drawing.Color.Red;

                    return;
                }
                this.lmsg.Text = "Record Update Successfully.";
                //this.lmsg.ForeColor = System.Drawing.Color.Green;

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
                    this.lmsg.Text = "Error:" + ex.Message;
                }
                this.LoadOrderDgv();
            }
            else
            {
                this.lmsg.Visible = true;
                this.lmsg.Text = "Delete Data Unsuccessfully";
            }
        }



        class SumClass
        {
            public string Product_Id { get; set; }
            public double rcvqty { get; set; }

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
            string Codetype = dt.Rows[0]["code"].ToString();
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

            //string LccodeType = (LcCode == "1401") ? "sircode like '0101%'"
            //    : (LcCode == "1402") ? "sircode like '4[12]%'"
            //    : (LcCode == "1403") ? "sircode like '21%'" : "sircode like '0101%' or sircode like '41%' or sircode like '21%'";


            //DataSet ds4 = proc1.GetTransInfo(comcod, "SP_LC_INFO", "RETRIVELCRES", filter1, "sirinf_v", LccodeType, "", "", "", "", "", "");  // table Desc 2
            DataSet ds1 = proc1.GetTransInfo(comcod, "SP_ENTRY_REQ_CENSTORE", "MATCODELIST", "01%", "%", SearchInfo, "", "", "", "", "");

            this.ddlResList.DataTextField = "resdesc";
            this.ddlResList.DataValueField = "rescod";
            this.ddlResList.DataSource = ds1.Tables[0];
            this.ddlResList.DataBind();
            ViewState["Material"] = ds1.Tables[0];
            ViewState["tblSpcf"] = ds1.Tables[1];
            ImgbtnSpecification_Click(null, null);
        }


        protected void lnkSameValue_Click(object sender, EventArgs e)
        {
            this.Save_Value();
            LoadOrderDgv();
            this.lmsg.Visible = false;
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


        protected void ddlResList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ImgbtnSpecification_Click(null, null);
        }
        protected void ImgbtnSpecification_Click(object sender, EventArgs e)
        {


            string mResCode = this.ddlResList.SelectedValue.ToString();
            // string spcfcod1 = this.ddlResSpcf.SelectedValue.ToString();
            this.ddlResSpcf.Items.Clear();
            DataTable tbl1 = (DataTable)ViewState["tblSpcf"];
            DataView dv1 = tbl1.DefaultView;
            dv1.RowFilter = "rsircode = '" + mResCode.Substring(0, 9) + "' or spcfcod = '000000000000'";
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

        protected void lbtnReq_Click(object sender, EventArgs e)
        {
            string comcod = this.ComCod();
            string reqno = this.Request.QueryString["genno"].ToString();
            string actcode = this.ddlLcCode.SelectedValue.ToString();
            DataSet ds = proc1.GetTransInfoNew(comcod, "SP_LC_INTERFACE", "UPDATE_LC_WITH_REQ", null, null, null, reqno, actcode);

            if (ds == null)
                return;

            this.lmsg.Visible = true;
            this.lmsg.Text = ds.Tables[0].Rows[0]["msg"].ToString();
            this.showOrder();


        }
    }
}
