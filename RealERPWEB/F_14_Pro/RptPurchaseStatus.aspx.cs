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
using Microsoft.Reporting.WinForms;
using RealERPLIB;
using RealERPRPT;
using RealERPRDLC;
namespace RealERPWEB.F_14_Pro
{
    public partial class RptPurchaseStatus : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        static string prevPage = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string url = HttpContext.Current.Request.Url.AbsoluteUri.ToString();
                int index1 = (url.Contains("&")) ? url.IndexOf('&') : url.Length;
                int index2 = (url.Contains("&")) ? url.Substring(index1 + 1).IndexOf('&') : 0;

                int indexofamp = index1 + (index2 > 0 ? index2 + 1 : index2);

                if (this.Request.QueryString["Rpt"].ToString() != "GenBillTrack")
                {
                    if ((!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp),
                            (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean(hst["permission"]))
                        Response.Redirect("../AcceessError.aspx");
                }




                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                //((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
               // this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));
                CommonButton();




                string Type = this.Request.QueryString["Rpt"].ToString();
                //((Label)this.Master.FindControl("lblTitle")).Text = (Type == "DaywPur") ? "Day Wise Purchase"
                //    : (Type == "PurSum") ? "Purchase Summary (Project Wise)"
                //    : (Type == "PenBill") ? "Pending Bill" : (Type == "IndSup") ? "Purchase History-Supplier Wise Report"
                //    : (Type == "Purchasetrk") ? "Purchase Tracking-01 " : (Type == "Purchasetrk02") ? "Purchase Tracking-02"
                //    : (Type == "PurBilltk") ? "Bill Tracking"
                //    : (Type == "MatRateVar") ? "Rate Variance - Materials"
                //    : (Type == "Ordertrk") ? "Order Tracking-01"
                //    : (Type == "GenBillTrack") ? "General Bill Tracking"
                //    : (Type == "BillRegTrack") ? "Bill Reister Tracking" : "Budget Tracking";



                if (Type == "BillRegTrack")
                {
                    this.serail.Visible = true;
                    this.main.Visible = false;
                    this.datepart.Visible = false;
                    this.GetBillNo();
                }
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                string qdate1 = this.Request.QueryString["Date1"];
                string qdate2 = this.Request.QueryString["Date2"];

                this.txtfromdate1.Text = System.DateTime.Today.ToString("01-" + "MMM-yyyy");
                this.txttodate1.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                this.txttodate.Text = qdate2.Length > 0 ? qdate2 : date;
                this.txtFDate.Text = qdate1.Length > 0 ? qdate1 : "01" + date.Substring(2);
                if (this.ddlProjectName.Items.Count == 0)
                {
                    if (Type == "GenBillTrack")
                    {
                        this.main.Visible = false;
                        this.genbillno.Visible = true;
                        this.datepart.Visible = false;
                        this.genbillno.Visible = true;
                        this.GetGeneralBillNo();
                    }
                    else
                    {
                        this.GetProjectName();
                    }
                }
                this.ShowView();
                if (Type == "Ordertrk")
                {
                    this.GetOrderNo();
                }
                else if (Type == "GenBillTrack")
                {
                    this.prjSection.Visible = true;
                    this.genbillno.Visible = true;
                }
                else
                {
                    this.GetReqno01();
                    this.LoadSertial();
                }
                this.imgbtnFindMatCom_Click(null, null);
            }
        }

        protected void gvGenBillTracking_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvGenBillTracking.EditIndex = e.NewEditIndex;
            DataTable dt = (DataTable)Session["tblpurchase"];
            gvGenBillTracking.DataSource = dt;
            gvGenBillTracking.DataBind();
            string comcod = this.GetComeCode();
            int rowindex = (gvGenBillTracking.PageSize) * (this.gvGenBillTracking.PageIndex) + e.NewEditIndex;
            DropDownList ddlgrdacccode = (DropDownList)this.gvGenBillTracking.Rows[e.NewEditIndex].FindControl("ddlgrdacccode");

            ViewState["gindex"] = e.NewEditIndex;
            string SearchProject = "%";
            DataSet ds2 = MktData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETACCCODE", SearchProject, "", "", "", "", "", "", "", "");
            DataTable dt2 = ds2.Tables[0];
            ViewState["HeadAcc1"] = ds2.Tables[0];
            ddlgrdacccode.DataTextField = "actdesc1";
            ddlgrdacccode.DataValueField = "actcode";
            ddlgrdacccode.DataSource = dt2;
            ddlgrdacccode.DataBind();
            string pactcode = ((Label)this.gvGenBillTracking.Rows[e.NewEditIndex].FindControl("lgvpactcode")).Text.Trim();
            ddlgrdacccode.SelectedValue = pactcode;
            ViewState["targetPactcode"] = pactcode;

            DropDownList ddlgrdresouce = (DropDownList)this.gvGenBillTracking.Rows[e.NewEditIndex].FindControl("ddlrgrdesuorcecode");
            string SearchResourche = "%";
            DataSet ds3 = MktData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETRESCODE", "", SearchResourche, "", "", "", "", "", "", "");
            DataTable dt3 = ds3.Tables[0];
            Session["HeadRsc1"] = ds3.Tables[0];
            ddlgrdresouce.DataTextField = "resdesc1";
            ddlgrdresouce.DataValueField = "rescode";
            ddlgrdresouce.DataSource = dt3;
            ddlgrdresouce.DataBind();
            string rsircode = ((Label)this.gvGenBillTracking.Rows[e.NewEditIndex].FindControl("lgvrsircode")).Text.Trim();
            ddlgrdresouce.SelectedValue = rsircode;
            ViewState["targetSircode"] = rsircode;
        }

        protected void gvGenBillTracking_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            this.GetSingleProjectDetails();
            string comcod = this.GetComeCode();
            DataTable dt = (DataTable)Session["tblpurchase"];
            int rowindex = (int)ViewState["gindex"];
            string actcode = ((DropDownList)this.gvGenBillTracking.Rows[e.RowIndex].FindControl("ddlgrdacccode")).SelectedValue.ToString();
            string rescode = ((DropDownList)this.gvGenBillTracking.Rows[e.RowIndex].FindControl("ddlrgrdesuorcecode")).SelectedValue.ToString();
            ViewState["actcode"] = actcode;
            ViewState["rescode"] = rescode;
            string txtactcode = ((DropDownList)this.gvGenBillTracking.Rows[e.RowIndex].FindControl("ddlgrdacccode")).SelectedItem.ToString();
            string txtrescode = ((DropDownList)this.gvGenBillTracking.Rows[e.RowIndex].FindControl("ddlrgrdesuorcecode")).SelectedItem.ToString();
            ViewState["actcodedesc"] = txtactcode;
            ViewState["rescodedesc"] = txtrescode;
            string vounum = ((Label)this.gvGenBillTracking.Rows[e.RowIndex].FindControl("lgvvounum")).Text.Trim();
            string reqno = ((Label)this.gvGenBillTracking.Rows[e.RowIndex].FindControl("lgvreqno")).Text.Trim();
            string rsircode = ((Label)this.gvGenBillTracking.Rows[e.RowIndex].FindControl("lgvrsircode")).Text.Trim();
            string spcfcod = ((Label)this.gvGenBillTracking.Rows[e.RowIndex].FindControl("lgvspcfcod")).Text.Trim();
            string pactcode = ((Label)this.gvGenBillTracking.Rows[e.RowIndex].FindControl("lgvpactcode")).Text.Trim();
            string billno1 = ((Label)this.gvGenBillTracking.Rows[e.RowIndex].FindControl("lgvBillNo")).Text.Trim();
            string amount = ((TextBox)gvGenBillTracking.Rows[e.RowIndex].FindControl("lgvamount2")).Text.Trim();
            ViewState["amount"] = amount;
            string billno = this.ddlGenBillTracking.SelectedValue.ToString();
            int index = (this.gvGenBillTracking.PageIndex) * this.gvGenBillTracking.PageSize + e.RowIndex;

            dt.Rows[index]["pactcode"] = actcode;
            dt.Rows[index]["rsircode"] = rescode;
            dt.Rows[index]["vounum"] = vounum;
            dt.Rows[index]["reqno"] = reqno;
            dt.Rows[index]["spcfcod"] = spcfcod;
            dt.Rows[index]["billno"] = billno1;
            dt.Rows[index]["amt"] = amount;
            dt.Rows[index]["actdesc"] = txtactcode;
            dt.Rows[index]["rsirdesc"] = txtrescode;

            Session["tblpurchase"] = dt;
            if (Checksamehead.Checked)
            {
                this.UpdateGridView();
            }
            else
            {
                this.gvGenBillTracking.EditIndex = -1;
                gvGenBillTracking.DataSource = dt;
                gvGenBillTracking.DataBind();
            }
        }


        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            this.SaveValue();
            DataTable dt = (DataTable)Session["tblpurchase"];

            foreach (DataRow dr2 in dt.Rows)
            {
                string pactcode = dr2["pactcode"].ToString();
                string rsircode = dr2["rsircode"].ToString();
                string demooldpactcode = dr2["demopactcode"].ToString();
                string demooldrescode = dr2["demorescode"].ToString();
                string vounum = dr2["vounum"].ToString();
                string reqno = dr2["reqno"].ToString();
                string spcfcod = dr2["spcfcod"].ToString();
                string billno1 = dr2["billno"].ToString();
                string demogrpdesc = dr2["demogrpdesc"].ToString();
                string slnum = dr2["slnum"].ToString();
                string amount = dr2["amt"].ToString();
                string oldamount = dr2["oldamt"].ToString();

                if (pactcode != demooldpactcode || rsircode != demooldrescode || amount != oldamount)
                {
                    bool result = MktData.UpdateTransInfo2(comcod, "SP_REPORT_REQ_STATUS", "INSERTUPDATEGENBILLTRACKING", vounum, pactcode, reqno, rsircode, spcfcod, billno1, demooldpactcode, demooldrescode, demogrpdesc, slnum, amount, "", "", "", "", "", "", "", "", "", "");

                    if (result == false)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }
                }
            }

            //((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);
            this.lbtnOk_Click(null, null);
        }



        private void SaveValue()
        {
            DataTable tbl1 = (DataTable)Session["tblpurchase"];
            for (int i = 0; i < gvGenBillTracking.Rows.Count; i++)
            {
                string vounum = ((Label)this.gvGenBillTracking.Rows[i].FindControl("lgvvounum")).Text.Trim();
                string reqno = ((Label)this.gvGenBillTracking.Rows[i].FindControl("lgvreqno")).Text.Trim();
                string spcfcod = ((Label)this.gvGenBillTracking.Rows[i].FindControl("lgvspcfcod")).Text.Trim();
                string pactcode = ((Label)this.gvGenBillTracking.Rows[i].FindControl("lgvpactcode")).Text.Trim();
                string rsircode = ((Label)this.gvGenBillTracking.Rows[i].FindControl("lgvrsircode")).Text.Trim();
                string demopactcode = ((Label)this.gvGenBillTracking.Rows[i].FindControl("lgvdemopactcode")).Text.Trim();
                string demorescode = ((Label)this.gvGenBillTracking.Rows[i].FindControl("lgvdemorescode")).Text.Trim();
                string demogrpdesc = ((Label)this.gvGenBillTracking.Rows[i].FindControl("lgvdemogrpdesc")).Text.Trim();
                string slnum = ((Label)this.gvGenBillTracking.Rows[i].FindControl("lgvslnum")).Text.Trim();
                string amount = ((TextBox)this.gvGenBillTracking.Rows[i].FindControl("lgvamount1")).Text.Trim();
                string oldamount = ((Label)this.gvGenBillTracking.Rows[i].FindControl("lgvoldamt")).Text.Trim();


                tbl1.Rows[i]["pactcode"] = pactcode;
                tbl1.Rows[i]["rsircode"] = rsircode;
                tbl1.Rows[i]["demopactcode"] = demopactcode;
                tbl1.Rows[i]["demorescode"] = demorescode;
                tbl1.Rows[i]["vounum"] = vounum;
                tbl1.Rows[i]["reqno"] = reqno;
                tbl1.Rows[i]["spcfcod"] = spcfcod;
                tbl1.Rows[i]["amt"] = amount;
                tbl1.Rows[i]["oldamt"] = oldamount;
                tbl1.Rows[i]["grpdesc"] = demogrpdesc;
                tbl1.Rows[i]["slnum"] = slnum;
            }
            Session["tblpurchase"] = tbl1;
        }


        private void UpdateGridView()
        {
            DataTable tbl1 = (DataTable)Session["tblpurchase"];
            for (int i = 0; i < gvGenBillTracking.Rows.Count; i++)
            {
                string vounum = ((Label)this.gvGenBillTracking.Rows[i].FindControl("lgvvounum")).Text.Trim();
                string reqno = ((Label)this.gvGenBillTracking.Rows[i].FindControl("lgvreqno")).Text.Trim();
                string spcfcod = ((Label)this.gvGenBillTracking.Rows[i].FindControl("lgvspcfcod")).Text.Trim();
                string pactcode = ((Label)this.gvGenBillTracking.Rows[i].FindControl("lgvpactcode")).Text.Trim();
                string rsircode = ((Label)this.gvGenBillTracking.Rows[i].FindControl("lgvrsircode")).Text.Trim();
                string demopactcode = ((Label)this.gvGenBillTracking.Rows[i].FindControl("lgvdemopactcode")).Text.Trim();
                string demorescode = ((Label)this.gvGenBillTracking.Rows[i].FindControl("lgvdemorescode")).Text.Trim();
                string demogrpdesc = ((Label)this.gvGenBillTracking.Rows[i].FindControl("lgvdemogrpdesc")).Text.Trim();
                string slnum = ((Label)this.gvGenBillTracking.Rows[i].FindControl("lgvslnum")).Text.Trim();
                //string amount = ((Label)this.gvGenBillTracking.Rows[i].FindControl("lgvamount1")).Text.Trim();
                string oldamount = ((Label)this.gvGenBillTracking.Rows[i].FindControl("lgvoldamt")).Text.Trim();

                string selectedactcode = (string)ViewState["actcode"];
                string selectedrescode = (string)ViewState["rescode"];
                string selectedactcodedesc = (string)ViewState["actcodedesc"];
                string selectedrescodedesc = (string)ViewState["rescodedesc"];
                string txtamt = (string)ViewState["amount"];
                string targetrPactcode = (string)ViewState["targetPactcode"];
                string targetrSircode = (string)ViewState["targetSircode"];

                if (targetrPactcode == pactcode && targetrSircode == rsircode)
                {
                    tbl1.Rows[i]["pactcode"] = selectedactcode;
                    tbl1.Rows[i]["rsircode"] = selectedrescode;
                    tbl1.Rows[i]["demopactcode"] = demopactcode;
                    tbl1.Rows[i]["demorescode"] = demorescode;
                    tbl1.Rows[i]["vounum"] = vounum;
                    tbl1.Rows[i]["reqno"] = reqno;
                    tbl1.Rows[i]["spcfcod"] = spcfcod;
                    tbl1.Rows[i]["amt"] = txtamt;
                    tbl1.Rows[i]["oldamt"] = oldamount;
                    tbl1.Rows[i]["grpdesc"] = demogrpdesc;
                    tbl1.Rows[i]["slnum"] = slnum;
                    tbl1.Rows[i]["actdesc"] = selectedactcodedesc;
                    tbl1.Rows[i]["rsirdesc"] = selectedrescodedesc;
                }
            }
            Session["tblpurchase"] = tbl1;

            this.gvGenBillTracking.EditIndex = -1;
            gvGenBillTracking.DataSource = tbl1;
            gvGenBillTracking.DataBind();
        }


        protected void GetSingleProjectDetails()
        {
            string comcod = this.GetComeCode();
            int rowindex = (int)ViewState["gindex"];

            string actcode = ((DropDownList)this.gvGenBillTracking.Rows[rowindex].FindControl("ddlgrdacccode")).SelectedValue.ToString();
            string rescode = ((DropDownList)this.gvGenBillTracking.Rows[rowindex].FindControl("ddlrgrdesuorcecode")).SelectedValue.ToString();
            string billno = this.ddlGenBillTracking.SelectedValue.ToString();

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETPROJECTDETAILS", actcode, rescode, billno, "", "", "", "", "", "");

            if (ds1 == null)
            {
                return;
            }

            Session["projectdetails"] = ds1.Tables[0];
        }


        protected void ddlgrdacccode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvGenBillTracking_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvGenBillTracking.EditIndex = -1;
            DataTable dt = (DataTable)Session["tblpurchase"];
            gvGenBillTracking.DataSource = dt;
            gvGenBillTracking.DataBind();
        }




        public void CommonButton()
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace("%20", " "), (DataSet)Session["tblusrlog"]);
            ((Label)this.Master.FindControl("lblmsg")).Visible = false;
            ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;


            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            ((CheckBox)this.Master.FindControl("CheckBox1")).Visible = false;

            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }


        private void LoadSertial()
        {
            string comcod = this.GetComeCode();
            //string txtSProject = "%" + this.txtSrcProject.Text + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_BILLMGT", "LOADSERIAL", "", "", "", "", "", "", "", "", "");
            this.ddlSerialno.DataTextField = "slnum1";
            this.ddlSerialno.DataValueField = "slnum";
            this.ddlSerialno.DataSource = ds1.Tables[0];
            this.ddlSerialno.DataBind();
        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            comcod = this.Request.QueryString["comcod"].Length > 0 ? this.Request.QueryString["comcod"].ToString() : comcod;
            return comcod;

        }

        private void GetProjectName()
        {

            string comcod = this.GetComeCode();
            string txtSProject = "%" + this.txtSrcProject.Text + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();

            string qpactcode = Request.QueryString["prjcode"] ?? "";

            if (qpactcode.Length > 0)
            {
                ddlProjectName.SelectedValue = (Request.QueryString["prjcode"].ToString());
                ddlProjectName.Enabled = false;
            }
            if (Request.QueryString["Rpt"].ToString() == "BgdBal" && Request.QueryString.AllKeys.Contains("pactcode"))
            {
                ddlProjectName.SelectedValue = (Request.QueryString["pactcode"].ToString());
                ddlProjectName.Enabled = false;
            }


            //if (Request.QueryString["prjcode"].Length > 0)
            //{
            //    ddlProjectName.SelectedValue = (Request.QueryString["prjcode"].ToString());
            //    ddlProjectName.Enabled = false;
            //}



        }
        private void GetSupplier()
        {
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string txtSrchSupplier = "%%";
            DataSet ds2 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETSUPPLIER", pactcode, txtSrchSupplier, "", "", "", "", "", "", "");

            DataTable dt = ds2.Tables[0];
            DataRow dr1 = dt.NewRow();
            dr1["ssircode"] = "000000000000";
            dr1["ssirdesc"] = "All Suppler";
            dt.Rows.Add(dr1);


            this.ddlSupplier.DataTextField = "ssirdesc";
            this.ddlSupplier.DataValueField = "ssircode";
            this.ddlSupplier.DataSource = dt;
            this.ddlSupplier.DataBind();
            this.ddlSupplier.SelectedValue = "000000000000";
            ds2.Dispose();



        }

        private void GetMaterialCode()
        {
            string comcod = this.GetComeCode();

            //string pactcode = this.ddlProjectName.SelectedValue.ToString();
            // string txtSrchSupplier = this.txtSrcSupplier.Text.Trim() + "%";
            DataSet ds3 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETRESOURCE", "%%", "", "", "", "", "", "", "", "");
            this.ddlMatCode.DataTextField = "sirdesc";
            this.ddlMatCode.DataValueField = "sircode";
            this.ddlMatCode.DataSource = ds3.Tables[0];
            this.ddlMatCode.DataBind();
            //  this.colorBind();
            //foreach (ListItem lteam in ddlMatCode.Items)
            //{
            //    string matcode = lteam.Value.Substring(9, 3).ToString();
            //    if (matcode == "000")
            //    {
            //        lteam.Attributes.Add("style", "background-color:#a3ffa3");
            //    }
            //}
        }

        protected void imgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void imgbtnFindSupplier_Click(object sender, EventArgs e)
        {
            this.GetSupplier();
        }
        private void ShowView()
        {
            string rpt = this.Request.QueryString["Rpt"].ToString().Trim();
            switch (rpt)
            {
                case "DaywPur":
                    this.GetMaterialCode();
                    this.PnlSupplier.Visible = true;
                    this.LblReqno.Visible = true;
                    this.txtSrcMrfNo.Visible = true;
                    this.imgbtnFindRequiSition.Visible = true;
                    this.lblMcod.Visible = true;
                    this.ddlMatCode.Visible = true;
                    this.MultiView1.ActiveViewIndex = 0;
                    this.chkDirect.Visible = true;
                    this.GetSupplier();

                    break;

                case "PurSum":
                    this.lblRptGroup.Visible = true;
                    this.ddlRptGroup.Visible = true;
                    this.lblrpttype.Visible = true;
                    this.ddlrpttype.Visible = true;
                    this.MultiView1.ActiveViewIndex = 1;
                    break;

                case "PenBill":
                    this.MultiView1.ActiveViewIndex = 2;
                    break;

                case "IndSup":
                    this.PnlSupplier.Visible = true;
                    this.GetSupplier();
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "Purchasetrk":
                    this.lblProjectName.Visible = false;
                    this.txtSrcProject.Visible = false;
                    this.ddlProjectName.Visible = false;
                    this.imgbtnFindProject.Visible = false;
                    this.lbldateto.Visible = false;
                    this.txttodate.Visible = false;
                    this.lblRptGroup.Visible = false;
                    this.ddlRptGroup.Visible = false;
                    this.lblPage.Visible = false;
                    this.ddlpagesize.Visible = false;

                    //this.GetReqno();
                    this.MultiView1.ActiveViewIndex = 3;
                    break;


                case "Purchasetrk02":
                    this.lblProjectName.Visible = false;
                    this.txtSrcProject.Visible = false;
                    this.ddlProjectName.Visible = false;
                    this.imgbtnFindProject.Visible = false;
                    this.lbldateto.Visible = false;
                    this.txttodate.Visible = false;
                    this.lblRptGroup.Visible = false;
                    this.ddlRptGroup.Visible = false;
                    this.lblPage.Visible = false;
                    this.ddlpagesize.Visible = false;

                    //this.GetReqno();
                    this.MultiView1.ActiveViewIndex = 5;
                    break;

                case "BgdBal":
                    this.lbldatefrm.Visible = false;
                    this.txtFDate.Visible = false;
                    this.lbldateto.Visible = false;
                    this.txttodate.Visible = false;
                    this.lblRptGroup.Visible = false;
                    this.ddlRptGroup.Visible = false;
                    this.lblPage.Visible = false;
                    this.ddlpagesize.Visible = false;

                    this.GetMaterial();
                    this.MultiView1.ActiveViewIndex = 4;
                    break;

                case "PurBilltk":
                    this.lblProjectName.Visible = false;
                    this.txtSrcProject.Visible = false;
                    this.ddlProjectName.Visible = false;
                    this.imgbtnFindProject.Visible = false;
                    this.lbldatefrm.Visible = false;
                    this.txtFDate.Visible = false;
                    this.lbldateto.Visible = false;
                    this.txttodate.Visible = false;
                    this.lblRptGroup.Visible = false;
                    this.ddlRptGroup.Visible = false;
                    this.lblPage.Visible = false;
                    this.ddlpagesize.Visible = false;
                    //this.GetReqno();
                    this.MultiView1.ActiveViewIndex = 6;
                    break;

                case "MatRateVar":
                    this.lblProjectName.Visible = false;
                    this.txtSrcProject.Visible = false;
                    this.ddlProjectName.Visible = false;
                    this.imgbtnFindProject.Visible = false;
                    //this.lbldatefrm.Text = "";
                    //this.lbldateto.Text = "Present Date";
                    this.lblRptGroup.Visible = false;
                    this.ddlRptGroup.Visible = false;
                    this.lblPage.Visible = false;
                    this.ddlpagesize.Visible = false;
                    //this.GetReqno();
                    this.MultiView1.ActiveViewIndex = 7;
                    break;


                case "BillRegTrack":

                    this.MultiView1.ActiveViewIndex = 8;
                    break;

                case "Ordertrk":
                    this.lblProjectName.Visible = true;
                    this.txtSrcProject.Visible = true;
                    this.ddlProjectName.Visible = true;
                    this.imgbtnFindProject.Visible = true;
                    this.lbldateto.Visible = false;
                    this.txttodate.Visible = false;
                    this.lblRptGroup.Visible = false;
                    this.ddlRptGroup.Visible = false;
                    this.lblPage.Visible = false;
                    this.ddlpagesize.Visible = false;
                    this.lbldatefrm.Visible = false;
                    this.txtFDate.Visible = false;
                    this.lbtnOk.Visible = false;
                    //this.GetReqno();
                    this.MultiView1.ActiveViewIndex = 9;
                    break;

                case "GenBillTrack":
                    this.MultiView1.ActiveViewIndex = 10;
                    break;
            }
        }


        protected void imgbtnFindReqno01_Click(object sender, EventArgs e)
        {
            this.GetReqno01();
        }


        private void GetReqno01()
        {
            Session.Remove("tblreq");
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string Mrfno = "%" + this.txtSrcRequisition01.Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETREQNO", pactcode, date, Mrfno, "", "", "", "", "", "");
            this.ddlReqNo01.DataTextField = "reqno1";
            this.ddlReqNo01.DataValueField = "reqno";
            this.ddlReqNo01.DataSource = ds1.Tables[0];
            this.ddlReqNo01.DataBind();
            Session["tblreq"] = ds1.Tables[0];
            ds1.Dispose();

        }

        private void GetReqno02()
        {
            Session.Remove("tblreq");
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string Mrfno = this.txtSrcRequisition02.Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETREQNO", pactcode, date, Mrfno, "", "", "", "", "", "");
            this.ddlReqNo02.DataTextField = "reqno1";
            this.ddlReqNo02.DataValueField = "reqno";
            this.ddlReqNo02.DataSource = ds1.Tables[0];
            this.ddlReqNo02.DataBind();
            Session["tblreq"] = ds1.Tables[0];
            ds1.Dispose();

        }


        private void GetGeneralBillNo()
        {
            Session.Remove("tblreq");
            string comcod = this.GetComeCode();
            string txtsearch = "%" + this.TextGenBillTrack.Text + "%";
            string frmdat = this.txtfromdate1.Text.Trim();
            string todat = this.txttodate1.Text.Trim();


            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETGENERALBILLNO", txtsearch, frmdat, todat, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlGenBillTracking.DataTextField = "reqno1";
            this.ddlGenBillTracking.DataValueField = "reqno";
            this.ddlGenBillTracking.DataSource = ds1.Tables[0];
            this.ddlGenBillTracking.DataBind();
            Session["tblgenbilltrk"] = ds1.Tables[0];
            ds1.Dispose();
        }



        private void GetBillNo()
        {
            Session.Remove("tblreq");
            string comcod = this.GetComeCode();
            string billno = this.txtBillSearch.Text.Trim();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETBILLNO", billno, "", "", "", "", "", "", "", "");
            this.ddlBillno.DataTextField = "billno1";
            this.ddlBillno.DataValueField = "billno";
            this.ddlBillno.DataSource = ds1.Tables[1];
            this.ddlBillno.DataBind();
            Session["tblreq"] = ds1.Tables[0];
            ds1.Dispose();
        }

        private void GetMaterial()
        {
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string txtfindMat = "%" + this.txtSrcMat.Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETMATERIAL", pactcode, txtfindMat, "", "", "", "", "", "", "");
            this.ddlMaterial.DataTextField = "rsirdesc";
            this.ddlMaterial.DataValueField = "rsircode";
            this.ddlMaterial.DataSource = ds1.Tables[0];
            this.ddlMaterial.DataBind();

            if (Request.QueryString["Rpt"].ToString() == "BgdBal" && Request.QueryString.AllKeys.Contains("rsircode"))
            {
                ddlMaterial.SelectedValue = (Request.QueryString["rsircode"].ToString());
                this.ShowBgdBal();
            }

        }


        protected void imgbtnFindMat_Click(object sender, EventArgs e)
        {

            this.GetMaterial();

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string rpt = this.Request.QueryString["Rpt"].ToString().Trim();
            switch (rpt)
            {
                case "DaywPur":
                    this.RptDayPurchase();
                    break;
                case "PurSum":
                    string rpttype = ddlrpttype.SelectedItem.ToString();
                    if (rpttype == "Details")
                        this.RptPurchaseDetails();
                    else
                        this.RptSummary();
                    break;

                case "PenBill":
                    break;

                case "IndSup":
                    this.RptIndSup();
                    break;
                case "Purchasetrk":
                    this.RptPurchaseTrack();
                    break;

                case "BgdBal":
                    this.RptBgdBal();
                    break;

                case "PurBilltk":
                    this.PrintBurBillTrack();
                    break;

                case "MatRateVar":
                    PrintMatRateVariance();
                    break;
                case "BillRegTrack":
                    this.PrintBillRegTrack();
                    break;
                case "Ordertrk":
                    this.PrintOrderTracking();
                    break;
                case "Purchasetrk02":
                    this.PrintPurchaseTrack02();
                    break;

            }
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Materials Purchase Status";
                string eventdesc = "Print Report: " + rpt;
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }




        }

        private void RptDayPurchase()
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            switch (comcod)
            {
                case "3330":
                    //case "3101":
                    this.RptDayPurchaseBridge();
                    break;

                case "3101":
                case "3354":
                    this.RptDayPurchaseEdison();
                    break;

                default:
                    this.RptDayPurchaseGen();
                    break;

            }
        }

        private void RptDayPurchaseBridge()
        {

            DataTable dt = (DataTable)Session["tblpurchase"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd MMMM, yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd MMMM, yyyy");

            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);
            var lst = dt.DataTableToList<RealEntity.C_12_Inv.RptDayWisePurchase>();


            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptDayWisePurchase", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("date", "From " + fromdate + " To " + todate));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Day Wise Purchase Report"));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));
            // Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void RptDayPurchaseEdison()
        {

            DataTable dt = (DataTable)Session["tblpurchase"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd MMMM, yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd MMMM, yyyy");

            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);
            var lst = dt.DataTableToList<RealEntity.C_12_Inv.RptDayWisePurchase>();


            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptDayWisePurchaseEdison", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("date", "From " + fromdate + " To " + todate));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Day Wise Purchase Report"));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));
            // Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        private void RptDayPurchaseGen()
        {


            DataTable dt = (DataTable)Session["tblpurchase"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd MMMM, yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd MMMM, yyyy");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);
            var lst = dt.DataTableToList<RealEntity.C_12_Inv.RptDayWisePurchase>();

            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.RptPurchaseStatus1", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("date", "From " + fromdate + " To " + todate));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Day Wise Purchase Report"));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));
            Rpt1.SetParameters(new ReportParameter("comLogo", ComLogo));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        private void RptPurchaseDetails()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd MMMM, yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd MMMM, yyyy");
            string newsum = ((Label)this.gvPurSum.FooterRow.FindControl("lgvFAmtS")).Text.ToString();
            DataTable dt = (DataTable)Session["tblpurchase"];

            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.RptPurchaseSummary02>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptPurchaseSummary", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("txtDate", "From " + fromdate + " To " + todate));
            Rpt1.SetParameters(new ReportParameter("txtGroup", "Group: " + this.ddlRptGroup.SelectedItem.Text));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Purchase Summary"));
            Rpt1.SetParameters(new ReportParameter("txtGrandTotal", newsum));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void RptSummary()
        {

            DataTable dt = (DataTable)Session["tblpurchase"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string frmdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            string rpthead = "Summary Report";

            if (dt == null)
                return;
            var lst = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.RptSummaryProject>();


            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptSummaryProject", lst, null, null);

            Rpt1.EnableExternalImages = true;
            //Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("comname", comnam));
            //Rpt1.SetParameters(new ReportParameter("txtProject", "Project Name : " + this.ddlProjectName.SelectedItem.Text));
            Rpt1.SetParameters(new ReportParameter("txtdate", " (" + "From  " + frmdate + " To " + todate + ")"));
            Rpt1.SetParameters(new ReportParameter("txtTitle", rpthead));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void RptIndSup()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd MMMM, yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd MMMM, yyyy");

            DataTable dt = (DataTable)Session["tblpurchase"];

            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.RptIndSupPurchase>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptIndSupPurchae", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("txtDate", "From " + fromdate + " To " + todate));
            Rpt1.SetParameters(new ReportParameter("txtSupplier", this.ddlSupplier.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Purchase History- Supplier Wise"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void RptPurchaseTrack()
        {
            DataTable dt2 = (DataTable)Session["tblreq"];
            DataTable dt = (DataTable)Session["tblpurchase"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string reqno = this.ddlReqNo01.SelectedValue.ToString();

            var list = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.RptPurTrack01>();
            LocalReport rpt = new LocalReport();
            rpt = RptSetupClass1.GetLocalReport("R_14_Pro.RptPurchaseTra", list, null, null);
            rpt.SetParameters(new ReportParameter("compName", comnam));
            rpt.SetParameters(new ReportParameter("projectName", (((DataTable)Session["tblreq"]).Select("reqno='" + reqno + "'"))[0]["actdesc"].ToString()));
            rpt.SetParameters(new ReportParameter("rptTitle", "Purchase Tracking"));
            rpt.SetParameters(new ReportParameter("mrfNo", "MRF No: " + dt.Rows[0]["refno"].ToString()));
            rpt.SetParameters(new ReportParameter("narration", this.txtReqNarr.Text.Trim()));
            rpt.SetParameters(new ReportParameter("userInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = rpt;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void PrintPurchaseTrack02()
        {
            DataTable dt = (DataTable)Session["tblpurchase"];
            DataTable dt1 = (DataTable)Session["tblpurchase1"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string reqno = this.ddlReqNo01.SelectedValue.ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            var list = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.EclassReqAllOrderList>();
            var list2 = dt1.DataTableToList<RealEntity.C_14_Pro.EClassPur.EclassReqAllMrrList>();
            LocalReport rpt = new LocalReport();
            rpt = RptSetupClass1.GetLocalReport("R_14_Pro.RptPurchaseTrack02", list, list2, null);
            rpt.EnableExternalImages = true;
            rpt.SetParameters(new ReportParameter("compName", comnam));
            rpt.SetParameters(new ReportParameter("reqno", reqno));
            rpt.SetParameters(new ReportParameter("comlogo", ComLogo));
            rpt.SetParameters(new ReportParameter("rptTitle", "Purchase Tracking"));
            rpt.SetParameters(new ReportParameter("userInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = rpt;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        private void RptBgdBal()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string date = System.DateTime.Now.ToString("dd.MM.yyyy");
            string txtuserinfo = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataTable dt = ((DataTable)Session["tblpurchase"]);
            var lst = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.EclassBudgetTracking>();

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptBudgetTracking", lst, null, null);

            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("Rptname", "Budget Tracking"));
            Rpt1.SetParameters(new ReportParameter("comlogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("project", this.ddlProjectName.SelectedItem.Text.Substring(13)));
            Rpt1.SetParameters(new ReportParameter("material", this.ddlMaterial.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("opening", this.lblvalOpenig.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("bgdqty", this.lblvalBudget.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("transfer", this.lblvaltrans.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("ttlsuply", this.lblvalTotalSupp.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("balqty", this.lblvalBalance.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("printFooter", txtuserinfo));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";

        }


        private void PrintBillRegTrack()
        {
            DataTable dt = (DataTable)Session["tblpurchase"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string session = hst["session"].ToString();
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string paymentid = this.ddlSerialno.SelectedValue;
            var list = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.RptBillRegTrack>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RDLCAccountSetup.GetLocalReport("R_14_Pro.RptBillRegTrack", list, null, null);

            Rpt1.SetParameters(new ReportParameter("comname", comnam));
            Rpt1.SetParameters(new ReportParameter("paymentid", paymentid));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void PrintBurBillTrack()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string billno = this.ddlBillno.SelectedValue.ToString();
            DataTable dt = (DataTable)Session["tblpurchase"];

            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.RptBillTracking>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.rptPurchaseBillTk", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("projectName", (((DataTable)Session["tblreq"]).Select("billno='" + billno + "'"))[0]["actdesc"].ToString()));
            Rpt1.SetParameters(new ReportParameter("supplierName", (((DataTable)Session["tblreq"]).Select("billno='" + billno + "'"))[0]["ssirdesc"].ToString()));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Purchase Bill Tracking"));
            Rpt1.SetParameters(new ReportParameter("refNo", "Ref No: " + (((DataTable)Session["tblreq"]).Select("billno='" + billno + "'"))[0]["billref"].ToString()));
            Rpt1.SetParameters(new ReportParameter("reqDate", "Bill Date: " + this.ddlBillno.SelectedItem.Text.Substring(13, 11)));
            Rpt1.SetParameters(new ReportParameter("billNo", "Bill  No: " + ASTUtility.Left(this.ddlBillno.SelectedItem.Text.Trim(), 11)));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintMatRateVariance()
        {

            DataTable dt = (DataTable)Session["tblpurchase"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string billno = this.ddlBillno.SelectedValue.ToString();
            string frmdate = " Variance Date Range:-  Past Date: " + Convert.ToDateTime(this.txtFDate.Text).ToString("dd.MM.yyyy") + " Present Date: " + Convert.ToDateTime(this.txttodate.Text).ToString("dd.MM.yyyy");
            var matratevar = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.EClassMatRateVar>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptMatRateVar", matratevar, null, null);


            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("frmdate", frmdate));
            var datefrm = Convert.ToDateTime(this.txtFDate.Text.Trim());
            var dateto = Convert.ToDateTime(this.txttodate.Text.Trim());
            string monthcount = "";
            for (int i = 1; i < 13; i++)
            {
                //if (datefrm > dateto)
                //    break;
                monthcount = "month" + i.ToString();
                Rpt1.SetParameters(new ReportParameter(monthcount, datefrm.ToString("MMM yy")));
                datefrm = datefrm.AddMonths(1);

            }

            Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            this.ShowValue();


        }
        private void ShowValue()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string rpt = this.Request.QueryString["Rpt"].ToString().Trim();
            switch (rpt)
            {
                case "DaywPur":
                    this.lblPage.Visible = true;
                    this.ddlpagesize.Visible = true;
                    this.lblMcod.Visible = true;
                    this.ddlMatCode.Visible = true;
                    this.LblReqno.Visible = true;
                    this.txtSrcMrfNo.Visible = true;
                    this.imgbtnFindRequiSition.Visible = true;



                    this.ShowDayPur();
                    break;

                case "PurSum":
                    this.lblPage.Visible = true;
                    this.ddlpagesize.Visible = true;
                    this.ShowPurSum();
                    break;

                case "PenBill":
                    break;
                case "IndSup":
                    this.lblPage.Visible = true;
                    this.ddlpagesize.Visible = true;
                    this.LblReqno.Visible = true;
                    this.txtSrcMrfNo.Visible = true;
                    this.imgbtnFindRequiSition.Visible = true;
                    this.ShowIndSupplier();
                    break;

                case "Purchasetrk":
                    //this.ShowPurChaseTrk();
                    this.pnlnarration.Visible = true;
                    this.ShowPurChaseTrk01();
                    break;

                case "Purchasetrk02":

                    this.ShowPurChaseTrk02();
                    break;

                case "BgdBal":
                    this.Panelbgdbal.Visible = true;
                    this.ShowBgdBal();
                    break;

                case "PurBilltk":
                    this.ShowPurchaseBill();
                    break;

                case "MatRateVar":
                    this.ShowMatRVariacne();
                    break;
                case "BillRegTrack":
                    this.BillRegTrack();
                    break;
                case "GenBillTrack":
                    this.pnlnrbilltrac.Visible = true;
                    this.GeneralBillTrack();
                    break;

            }
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Materials Purchase Status";
                string eventdesc = "Show Report: " + rpt;
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        protected void imgbtnFindRequiSition_Click(object sender, EventArgs e)
        {
            string rpt = this.Request.QueryString["Rpt"].ToString().Trim();
            switch (rpt)
            {
                case "DaywPur":
                    this.ShowDayPur();
                    break;



                case "IndSup":
                    this.ShowIndSupplier();
                    break;

            }

        }


        private void ShowDayPur()
        {
            this.colorBind();
            Session.Remove("tblpurchase");
            string comcod = this.GetComeCode();
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string mrfno = "%" + this.txtSrcMrfNo.Text.Trim() + "%";
            string rescode = ((this.ddlMatCode.SelectedValue.ToString() == "000000000000") ? "" : (this.ddlMatCode.SelectedValue.Substring(9, 3).ToString() == "000") ? (this.ddlMatCode.SelectedValue.ToString().Substring(0, 9)).ToString() : this.ddlMatCode.SelectedValue.ToString()) + "%";
            string dirorin = (this.chkDirect.Checked) ? "direct" : "";
            string supplier = ((this.ddlSupplier.SelectedValue.ToString() == "000000000000") ? "" : this.ddlSupplier.SelectedValue.ToString()) + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "REQSATIONMRRSTATUS", fromdate, todate, pactcode, mrfno, rescode, dirorin, supplier, "", "");
            if (ds1.Tables[0].Rows.Count == 0)
            {
                this.gvPurStatus.DataSource = null;
                this.gvPurStatus.DataBind();
                return;
            }

            DataTable dt = HiddenSameData(ds1.Tables[0]);
            Session["tblpurchase"] = dt;


            this.LoadGrid();
        }

        public void colorBind()
        {
            foreach (ListItem lteam in ddlMatCode.Items)
            {
                string matcode = lteam.Value.Substring(9, 3).ToString();
                if (matcode == "000")
                {
                    lteam.Attributes.Add("style", "background-color:#a3ffa3");
                }
            }
        }
        private void ShowPurSum()
        {
            Session.Remove("tblpurchase");

            string comcod = this.GetComeCode();
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string pactcode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000" ? "" : this.ddlProjectName.SelectedValue.ToString()) + "%";
            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));

            string Calltype = this.ddlrpttype.SelectedIndex == 0 ? "RPTPURSUMMARYALLPROJECT" : "RPTPURSUMMARY";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", Calltype, fromdate, todate, pactcode, mRptGroup, "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvPurSum.DataSource = null;
                this.gvPurSum.DataBind();
                this.gvpursumall.DataSource = null;
                this.gvpursumall.DataBind();
                return;
            }

            Session["tblpurchase"] = ds1.Tables[0];
            this.LoadGrid();

        }
        private void ShowIndSupplier()
        {
            Session.Remove("tblpurchase");
            string comcod = this.GetComeCode();
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string supplier = this.ddlSupplier.SelectedValue.ToString();
            string mrfno = this.txtSrcMrfNo.Text.Trim() + "%";

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTINDSUPINFO", fromdate, todate, pactcode, supplier, mrfno, "", "", "", "");
            if (ds1 == null)
            {
                this.gvPurStatus.DataSource = null;
                this.gvPurStatus.DataBind();
                return;
            }

            DataTable dt = HiddenSameData(ds1.Tables[0]);
            Session["tblpurchase"] = dt;
            this.LoadGrid();


        }

        private string getCompanyRecom()
        {
            string comcod = this.GetComeCode();
            string comprecom = "";
            switch (comcod)
            {
                //case "3101":
                case "1103":
                    comprecom = "comprecom";
                    break;

                case "3367": //Epic
                case "3348": //Credence

                    comprecom = "firstasecapp";
                    break;

                case "3315": //Assure
                case "3316":
                case "3317":

                    comprecom = "iscrmchecked";
                    break;

                default:
                    comprecom = "";
                    break;

            }
            return comprecom;
        }

        private void ShowPurChaseTrk01()
        {

            Session.Remove("tblpurchase");
            string comcod = this.GetComeCode();
            string reqno = this.ddlReqNo01.SelectedValue.ToString();
            string recom = this.getCompanyRecom();

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTPURCHASETRACK01", reqno, recom, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvPurstk01.DataSource = null;
                this.gvPurstk01.DataBind();

                return;
            }
            DataTable dt = this.HiddenSameData(ds1.Tables[0]);
            Session["tblpurchase"] = ds1.Tables[0];
            Session["tblreqDescrip"] = ds1.Tables[2];
            Session["tblproject"] = ds1.Tables[1];

            this.txtReqNarr.Text = ds1.Tables[1].Rows.Count == 0 ? "" : ds1.Tables[1].Rows[0]["reqnar"].ToString();
            ///this.lblshipsupdate.Text = ds1.Tables[0].Rows[0]["shipsupdat"].ToString();
            this.LoadGrid();
            this.Date_Bind02();


        }


        private void GeneralBillTrack()
        {

            Session.Remove("tblpurchase");
            string comcod = this.GetComeCode();
            string reqno = this.ddlGenBillTracking.SelectedValue.ToString();
            //string recom = this.getCompanyRecom();
            string frmdate = this.txtfromdate1.Text.ToString();
            string todate = this.txttodate1.Text.ToString();

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETGENERALBILL", reqno, frmdate, todate, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvGenBillTracking.DataSource = null;
                this.gvGenBillTracking.DataBind();
                return;
            }

            
            this.Checksamehead.Visible = true;
            DataTable dt = this.HiddenSameData(ds1.Tables[0]);
            //DataTable dt = ds1.Tables[0];
            Session["tblpurchase"] = ds1.Tables[0];
            this.txtnrr.Text = ds1.Tables[0].Rows[0]["narr"].ToString();

            //this.gvGenBillTracking.DataSource = dt;
            //this.gvGenBillTracking.DataBind();

            this.LoadGrid();
            //this.Date_Bind02();
        }


        private void Date_Bind02()
        {
            string comcod = this.GetComeCode();

            if (comcod == "3315" || comcod == "3316" || comcod == "3101")
            {
                PnlDescrip.Visible = true;
            }

            DataTable dt2 = (DataTable)Session["tblreqDescrip"];

            if (dt2.Rows.Count == 0)
                return;
            this.gvDescrip.DataSource = dt2;
            this.gvDescrip.DataBind();
        }


        private void BillRegTrack()
        {
            Session.Remove("tblpurchase");
            string comcod = this.GetComeCode();
            string serial = this.ddlSerialno.SelectedValue.ToString();

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_BILLMGT", "BILLREGTRACKING", serial, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvPurstk01.DataSource = null;
                this.gvPurstk01.DataBind();

                return;
            }
            // DataTable dt = this.HiddenSameData (ds1.Tables[0]);
            Session["tblpurchase"] = ds1.Tables[0];

            this.LoadGrid();
        }

        private void ShowPurChaseTrk02()
        {
            Session.Remove("tblpurchase");
            string comcod = this.GetComeCode();
            string reqno = this.ddlReqNo02.SelectedValue.ToString();

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTPURCHASETRACK02", reqno, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvPurstk.DataSource = null;
                this.gvPurstk.DataBind();
                this.gvPurstk2.DataSource = null;
                this.gvPurstk2.DataBind();
                return;
            }
            DataTable dt = HiddenSameData(ds1.Tables[0]);
            Session["tblpurchase"] = ds1.Tables[0];
            Session["tblpurchase1"] = ds1.Tables[1];
            this.LoadGrid();
        }


        private void ShowPurchaseBill()
        {
            Session.Remove("tblpurchase");
            string comcod = this.GetComeCode();
            string billno = this.ddlBillno.SelectedValue.ToString();

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTPURBILLTRACK", billno, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvPurBilltk.DataSource = null;
                this.gvPurBilltk.DataBind();

                return;
            }
            DataTable dt = HiddenSameData(ds1.Tables[0]);
            Session["tblpurchase"] = ds1.Tables[0];
            this.LoadGrid();

        }

        private string CompBudgetBalance()
        {
            string comcod = this.GetComeCode();
            string reqorapproved = "";
            switch (comcod)
            {
                case "3315":
                case "3316":
                case "3317":
                    reqorapproved = "req";
                    break;

                default:
                    break;



            }
            return reqorapproved;

        }
        private void ShowBgdBal()
        {

            try
            {
                Session.Remove("tblpurchase");
                string comcod = this.GetComeCode();
                string pactcode = this.ddlProjectName.SelectedValue.ToString();
                string rescode = this.ddlMaterial.SelectedValue.ToString();
                string reqorapproved = this.CompBudgetBalance();
                DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTBUDGETBAL", pactcode, rescode, reqorapproved, "", "", "", "", "", "");
                if (ds1 == null)
                {
                    this.gvBgdBal.DataSource = null;
                    this.gvBgdBal.DataBind();
                    return;
                }

                Session["tblpurchase"] = ds1.Tables[0];

                this.lblvalBudget.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["bgdqty"]).ToString("#,##0;(#,##0); ");

                this.lblvalOpenig.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["opqty"]).ToString("#,##0;(#,##0); ");
                this.lbltxtvaldqty.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["dqty"]).ToString("#,##0;(#,##0); ");

                this.lblvalRequisition.Text = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[0].Compute("sum(areqty)", "")) ?
                                             0 : ds1.Tables[0].Compute("sum(areqty)", ""))).ToString("#,##0;(#,##0); ");
                this.lblvaltrans.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["trnqty"]).ToString("#,##0;(#,##0); ");

                this.lblvalTotalSupp.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["tosupqty"]).ToString("#,##0;(#,##0); ");
                this.lbllsdqty.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["tsldqty"]).ToString("#,##0;(#,##0); ");

                this.lblvalBalance.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["bgdbal"]).ToString("#,##0;(#,##0); ");
                this.LoadGrid();
            }

            catch (Exception ex)
            {

                ((Label)this.Master.FindControl("lblmsg")).Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

            }

        }

        private void ShowMatRVariacne()
        {


            Session.Remove("tblpurchase");
            string comcod = this.GetComeCode();

            int mon = ASTUtility.Datediff(Convert.ToDateTime(this.txttodate.Text.Trim()), Convert.ToDateTime(this.txtFDate.Text.Trim()));
            if (mon > 12)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Month Less Than Equal Twelve');", true);
                return;


            }
            //uddl month wise rate

            string frmdate = Convert.ToDateTime(this.txtFDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");
            string ResCode = ((this.ddlMaterialscom.SelectedValue == "000000000000") ? "" : this.ddlMaterialscom.SelectedValue.ToString()) + "%";
            // DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTMATRATEVARIANCE", frmdate, todate, ResCode, "", "", "", "", "", "");

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTMATRATEVARIANCMONTHWISE", frmdate, todate, ResCode, "", "", "", "", "", "");
            if (ds1 == null)
            {


                this.gvPurMatRVar.DataSource = null;
                this.gvPurMatRVar.DataBind();

                return;
            }

            Session["tblpurchase"] = this.HiddenSameData(ds1.Tables[0]);
            //this.gvPurMatRVar.Columns[4].HeaderText = "Price On <br />" + Convert.ToDateTime(this.txtFDate.Text.Trim()).ToString("dd.MM.yyyy");
            //this.gvPurMatRVar.Columns[5].HeaderText = "Price On <br />" + Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd.MM.yyyy"); 
            this.LoadGrid();

        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
            {
                return dt1;
            }

            string rpt = this.Request.QueryString["Rpt"].ToString().Trim();

            string reqno = "", matcode = "", spcfcod = ""; string grp = ""; string grpdesc = "";
            switch (rpt)
            {
                case "DaywPur":
                case "IndSup":
                    string pactcode = dt1.Rows[0]["pactcode"].ToString();
                    string mrrno = dt1.Rows[0]["mrrno"].ToString();

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["pactcode"].ToString() == pactcode && dt1.Rows[j]["mrrno"].ToString() == mrrno)
                        {
                            pactcode = dt1.Rows[j]["pactcode"].ToString();
                            mrrno = dt1.Rows[j]["mrrno"].ToString();
                            dt1.Rows[j]["pactdesc"] = "";
                            dt1.Rows[j]["mrrno1"] = "";
                        }

                        else
                        {

                            if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                            {
                                dt1.Rows[j]["pactdesc"] = "";
                            }

                            if (dt1.Rows[j]["mrrno"].ToString() == mrrno)
                            {
                                dt1.Rows[j]["mrrno1"] = "";
                            }
                            pactcode = dt1.Rows[j]["pactcode"].ToString();
                            mrrno = dt1.Rows[j]["mrrno"].ToString();

                        }

                    }

                    break;

                case "PurSum":
                    break;

                case "PenBill":
                    break;




                case "GenBillTrack":

                    grp = dt1.Rows[0]["grp"].ToString();
                    grpdesc = dt1.Rows[0]["grpdesc"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == grp)
                            dt1.Rows[j]["grpdesc"] = "";

                        grp = dt1.Rows[j]["grp"].ToString();

                    }

                    break;

                case "Purchasetrk":
      

                    grp = dt1.Rows[0]["grp"].ToString();
                    grpdesc = dt1.Rows[0]["grpdesc"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == grp)
                            dt1.Rows[j]["grpdesc"] = "";

                        grp = dt1.Rows[j]["grp"].ToString();

                    }
                    break;


                case "PurBilltk":

                    grp = dt1.Rows[0]["grp"].ToString();
                    grpdesc = dt1.Rows[0]["grpdesc"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == grp)
                            dt1.Rows[j]["grpdesc"] = "";

                        grp = dt1.Rows[j]["grp"].ToString();

                    }

                    break;

                   

                case "Purchasetrk02":
                    //string ppactcode = dt1.Rows[0]["pactcode"].ToString();
                    //string matcode = dt1.Rows[0]["rsircode"].ToString();
                    //string spcfcod = dt1.Rows[0]["spcfcod"].ToString();
                    //for (int j = 1; j < dt1.Rows.Count; j++)
                    //{
                    //    if (dt1.Rows[j]["pactcode"].ToString() == ppactcode && dt1.Rows[j]["rsircode"].ToString() == matcode && dt1.Rows[j]["spcfcod"].ToString() ==spcfcod)
                    //    {
                    //        ppactcode = dt1.Rows[j]["pactcode"].ToString();
                    //        matcode = dt1.Rows[j]["rsircode"].ToString();
                    //        spcfcod = dt1.Rows[j]["spcfcod"].ToString();
                    //        dt1.Rows[j]["pactdesc"] = "";
                    //        dt1.Rows[j]["rsirdesc"] = "";
                    //        dt1.Rows[j]["rsirunit"] = "";
                    //        dt1.Rows[j]["spcfdesc"] = "";
                    //        dt1.Rows[j]["areqty"] = 0.0000000;
                    //    }

                    //    else
                    //    {
                    //         if (dt1.Rows[j]["pactcode"].ToString() == ppactcode)
                    //            dt1.Rows[j]["pactdesc"] = "";
                    //         if (dt1.Rows[j]["rsircode"].ToString() == matcode)
                    //             dt1.Rows[j]["rsirdesc"] = "";
                    //         if (dt1.Rows[j]["spcfcod"].ToString() == spcfcod)
                    //             dt1.Rows[j]["spcfdesc"] = "";




                    //        ppactcode = dt1.Rows[j]["pactcode"].ToString();
                    //        matcode = dt1.Rows[j]["rsircode"].ToString();
                    //        spcfcod = dt1.Rows[j]["spcfcod"].ToString();
                    //    }
                    //}

                    break;



                case "MatRateVar":

                    string rsircode = dt1.Rows[0]["rsircode"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["rsircode"].ToString() == rsircode)
                        {

                            dt1.Rows[j]["rsirdesc"] = "";
                            dt1.Rows[j]["rsirunit"] = "";
                        }

                        rsircode = dt1.Rows[j]["rsircode"].ToString();

                    }

                    break;

                case "Ordertrk":

                    string grp1 = dt1.Rows[0]["grp"].ToString();
                    string grpdesc1 = dt1.Rows[0]["grpdesc"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == grp1)
                            dt1.Rows[j]["grpdesc"] = "";

                        grp1 = dt1.Rows[j]["grp"].ToString();

                    }
                    break;


            }

            return dt1;

        }


        private void LoadGrid()
        {

            try
            {
                DataTable dt = ((DataTable)Session["tblpurchase"]).Copy();
                string comcod = this.GetComeCode();
                if ((dt.Rows.Count == 0)) //Problem
                    return;

                string rpt = this.Request.QueryString["Rpt"].ToString().Trim();
                switch (rpt)
                {
                    case "DaywPur":
                    case "IndSup":
                        this.gvPurStatus.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvPurStatus.DataSource = dt;
                        this.gvPurStatus.DataBind();
                        if (comcod == "3340" || comcod == "3101")
                        {
                            this.gvPurStatus.Columns[17].Visible = true;
                            this.gvPurStatus.Columns[18].Visible = true;
                            this.gvPurStatus.Columns[19].Visible = true;


                        }
                        ((Label)this.gvPurStatus.FooterRow.FindControl("lgvFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ?
                                             0 : dt.Compute("sum(amt)", ""))).ToString("#,##0;(#,##0); ");
                        if (ddlProjectName.SelectedValue.ToString() != "000000000000")
                        {

                            if (ddlMatCode.SelectedValue.ToString() != "000000000000")
                            {
                                ((Label)this.gvPurStatus.FooterRow.FindControl("lgvFqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty)", "")) ?
                                                0 : dt.Compute("sum(qty)", ""))).ToString("#,##0.00;(#,##0.00); ");
                            }

                        }

                        Session["Report1"] = gvPurStatus;
                        ((HyperLink)this.gvPurStatus.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                        break;

                    case "PurSum":



                        this.gvPurSum.DataSource = null;
                        this.gvPurSum.DataBind();
                        this.gvpursumall.DataSource = null;
                        this.gvpursumall.DataBind();

                        if (this.ddlrpttype.SelectedIndex == 0)
                        {

                            this.gvpursumall.DataSource = dt;
                            this.gvpursumall.DataBind();
                            if (dt.Rows.Count > 0)
                            {
                                ((Label)this.gvpursumall.FooterRow.FindControl("lgvFAmtSall")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ?
                                                     0 : dt.Compute("sum(amt)", ""))).ToString("#,##0;(#,##0); ");

                                ((Label)this.gvpursumall.FooterRow.FindControl("lgvFpercntall")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(percnt)", "")) ?
                                                    0 : dt.Compute("sum(percnt)", ""))).ToString("#,##0;(#,##0); ");
                            }




                        }

                        else
                        {

                            this.gvPurSum.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                            this.gvPurSum.DataSource = dt;
                            this.gvPurSum.DataBind();

                            DataTable dt2 = dt.Copy();
                            DataView dv2 = dt2.DefaultView;
                            dv2.RowFilter = "qty > '0'";
                            dt2 = dv2.ToTable();
                            ((Label)this.gvPurSum.FooterRow.FindControl("lgvFAmtS")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(amt)", "")) ?
                                                 0 : dt2.Compute("sum(amt)", ""))).ToString("#,##0;(#,##0); ");





                        }


                        break;

                    case "PenBill":



                        break;

                    case "Purchasetrk":
                        this.gvPurstk01.DataSource = dt;
                        this.gvPurstk01.DataBind();

                        if (comcod == "3315" || comcod == "3316" || comcod == "3101")
                        {
                            this.gvPurstk01.Columns[12].Visible = true;
                        }
                        break;
                    case "BillRegTrack":
                        this.gvBillRegTrack.DataSource = dt;
                        this.gvBillRegTrack.DataBind();
                        break;
                    case "GenBillTrack":
                        this.gvGenBillTracking.DataSource = dt;
                        this.gvGenBillTracking.DataBind();
                        break;
                    case "Purchasetrk02":
                        DataTable dt1 = (DataTable)Session["tblpurchase1"];
                        this.gvPurstk.DataSource = dt;
                        this.gvPurstk.DataBind();

                        this.gvPurstk2.DataSource = dt1;
                        this.gvPurstk2.DataBind();

                        break;


                    case "BgdBal":
                        this.gvBgdBal.DataSource = dt;
                        this.gvBgdBal.DataBind();


                        ((Label)this.gvBgdBal.FooterRow.FindControl("lgvFareqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(areqty)", "")) ?
                                                0 : dt.Compute("sum(areqty)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvBgdBal.FooterRow.FindControl("lgvFprogqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(progqty)", "")) ?
                                            0 : dt.Compute("sum(progqty)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvBgdBal.FooterRow.FindControl("lgvFordrqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ordrqty)", "")) ?
                                            0 : dt.Compute("sum(ordrqty)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvBgdBal.FooterRow.FindControl("lgvFmrrqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mrrqty)", "")) ?
                                            0 : dt.Compute("sum(mrrqty)", ""))).ToString("#,##0;(#,##0); ");

                        ((Label)this.gvBgdBal.FooterRow.FindControl("lgvFordradjqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(oadjqty)", "")) ?
                                           0 : dt.Compute("sum(oadjqty)", ""))).ToString("#,##0;(#,##0); ");


                        break;


                    case "PurBilltk":
                        this.gvPurBilltk.DataSource = dt;
                        this.gvPurBilltk.DataBind();
                        DataView dv = dt.DefaultView;
                        dv.RowFilter = ("grp='F'");
                        dt = dv.ToTable();

                        ((Label)this.gvPurBilltk.FooterRow.FindControl("lblgvFbillamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ?
                                            0 : dt.Compute("sum(amt)", ""))).ToString("#,##0;(#,##0); ");
                        break;

                    case "MatRateVar":
                        this.gvPurMatRVar.DataSource = dt;
                        this.gvPurMatRVar.DataBind();
                        this.MonthWiseRate();
                        break;

                    case "Ordertrk":
                        this.gvorder.DataSource = dt;
                        this.gvorder.DataBind();
                        break;

                }

            }
            catch (Exception ex)
            {


            }




        }


        private void MonthWiseRate()
        {
            double amt1, amt2, amt3, amt4, amt5, amt6, amt7, amt8, amt9, amt10, amt11, amt12;
            DateTime datefrm, dateto;
            DataTable dt = ((DataTable)Session["tblpurchase"]).Copy();

            amt1 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt1)", "")) ? 0.00 : dt.Compute("sum(amt1)", "")));
            amt2 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt2)", "")) ? 0.00 : dt.Compute("sum(amt2)", "")));
            amt3 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt3)", "")) ? 0.00 : dt.Compute("sum(amt3)", "")));
            amt4 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt4)", "")) ? 0.00 : dt.Compute("sum(amt4)", "")));
            amt5 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt5)", "")) ? 0.00 : dt.Compute("sum(amt5)", "")));
            amt6 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt6)", "")) ? 0.00 : dt.Compute("sum(amt6)", "")));
            amt7 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt7)", "")) ? 0.00 : dt.Compute("sum(amt7)", "")));
            amt8 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt8)", "")) ? 0.00 : dt.Compute("sum(amt8)", "")));
            amt9 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt9)", "")) ? 0.00 : dt.Compute("sum(amt9)", "")));
            amt10 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt10)", "")) ? 0.00 : dt.Compute("sum(amt10)", "")));
            amt11 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt11)", "")) ? 0.00 : dt.Compute("sum(amt11)", "")));
            amt12 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt12)", "")) ? 0.00 : dt.Compute("sum(amt12)", "")));

            this.gvPurMatRVar.Columns[4].Visible = (amt1 != 0);
            this.gvPurMatRVar.Columns[5].Visible = (amt2 != 0);
            this.gvPurMatRVar.Columns[6].Visible = (amt3 != 0);
            this.gvPurMatRVar.Columns[7].Visible = (amt4 != 0);
            this.gvPurMatRVar.Columns[8].Visible = (amt5 != 0);
            this.gvPurMatRVar.Columns[9].Visible = (amt6 != 0);
            this.gvPurMatRVar.Columns[10].Visible = (amt7 != 0);
            this.gvPurMatRVar.Columns[11].Visible = (amt8 != 0);
            this.gvPurMatRVar.Columns[12].Visible = (amt9 != 0);
            this.gvPurMatRVar.Columns[13].Visible = (amt10 != 0);
            this.gvPurMatRVar.Columns[14].Visible = (amt11 != 0);
            this.gvPurMatRVar.Columns[15].Visible = (amt12 != 0);

            datefrm = Convert.ToDateTime(this.txtFDate.Text.Trim());
            dateto = Convert.ToDateTime(this.txttodate.Text.Trim());
            for (int i = 4; i < 16; i++)
            {
                if (datefrm > dateto)
                    break;

                this.gvPurMatRVar.Columns[i].HeaderText = datefrm.ToString("MMM yy");
                datefrm = datefrm.AddMonths(1);

            }

            this.gvPurMatRVar.DataSource = dt;
            this.gvPurMatRVar.DataBind();
            Session["Report1"] = gvPurMatRVar;

            ((HyperLink)this.gvPurMatRVar.HeaderRow.FindControl("hlbtntbCdataExelMat")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";




        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadGrid();
        }
        protected void gvPurStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvPurStatus.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
        protected void gvPurSum_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvPurSum.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }



        protected void imgbtnFindReqno02_Click(object sender, EventArgs e)
        {
            this.GetReqno02();
        }
        protected void imgbtnFindBill_Click(object sender, EventArgs e)
        {
            this.GetBillNo();
        }

        protected void imgbtnFindMatCom_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            string txtfindMat = "%" + this.txtMatcomSearch.Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETMATERIALCOM", txtfindMat, "", "", "", "", "", "", "", "");
            this.ddlMaterialscom.DataTextField = "sirdesc";
            this.ddlMaterialscom.DataValueField = "sircode";
            this.ddlMaterialscom.DataSource = ds1.Tables[0];
            this.ddlMaterialscom.DataBind();
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {


            string url = HttpContext.Current.Request.Url.AbsoluteUri.ToString();
            int index1 = (url.Contains("&")) ? url.IndexOf('&') : url.Length;
            int index2 = (url.Contains("&")) ? url.Substring(index1 + 1).IndexOf('&') : 0;

            int indexofamp = index1 + index2;

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

            if (!(Convert.ToBoolean(dr1[0]["entry"])))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You Have No Permission');", true);
                return;
            }

            string comcod = this.GetComeCode();
            string Billno = this.ddlBillno.SelectedValue.ToString();
            bool result = MktData.UpdateTransInfo2(comcod, "SP_REPORT_REQ_STATUS", "DELETEPURCHASE", Billno, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted Fail');", true);
                return;

            }

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted Successfully.');", true);




        }


        protected void gvPurstk01_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                string grpdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grpdesc")).ToString().Trim();
                LinkButton btnPrint = (LinkButton)e.Row.FindControl("btnPrintReqInfo");

                if (grpdesc == "")
                {
                    btnPrint.Visible = false;
                    return;

                }
                else
                {
                    e.Row.Attributes["style"] = "background-color:#C0C0C0; font-weight:bold;";
                    btnPrint.Visible = true;


                    /*
                    if (grpdesc == "1. Requisition" || grpdesc == "2. Requisition Checked" || grpdesc == "3. Requisition Approved" || grpdesc == "4. Order Process"
                      || grpdesc == "5. Purchase Order" || grpdesc == "6. Materials Received" || grpdesc == "7. Bill Confirmation" || grpdesc == "11. Cheque Preparation" || grpdesc == "12. Reconcilation")
                    {

                        e.Row.Attributes["style"] = "background-color:#C0C0C0; font-weight:bold;";

                    }*/
                }

            }


        }




        protected void btnPrintReqInfo_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string usrid = hst["usrid"].ToString();
            string trmnid = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string portAdd = hst["portnum"].ToString().Length == 0 ? "" : (":" + hst["portnum"].ToString());
            string hostname = "http://" + HttpContext.Current.Request.Url.Authority+ portAdd + HttpContext.Current.Request.ApplicationPath;



            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string reqno = ((Label)this.gvPurstk01.Rows[RowIndex].FindControl("lgvReqno")).Text.Trim();
            string reqdate = ((Label)this.gvPurstk01.Rows[RowIndex].FindControl("lgvAppDat0")).Text.Trim();

            //string grpdesc = ((Label)this.gvPurstk01.Rows[RowIndex].FindControl("lgvReqno")).Text.Trim();

            DataTable dt = (DataTable)Session["tblproject"];
            string pactcode = dt.Rows[0]["pactcode"].ToString();
            string reqinfo = ASTUtility.Left(reqno, 3);
            string reqinfo2 = ASTUtility.Left(reqno, 2);
            //string path = ResolveUrl("~/F_99_Allinterface/PurchasePrint?Type=");
            //string path2 = ResolveUrl("~/F_14_Pro/PurBillEntry?Type=");
            string path = "/F_99_Allinterface/PurchasePrint?Type=";
            string path2 = "/F_14_Pro/PurBillEntry?Type=";
            string path3 = "/F_17_Acc/AccPrint.aspx?Type=";
            string url = "";
            switch (reqinfo)
            {
                case "REQ":
                    url = path + "ReqPrint&reqno=" + reqno + "&reqdat=" + reqdate;
                    break;
                case "PAP":
                    url = path + "PurApproval&approvno=" + reqno + "&approvdat=" + reqdate;
                    break;
                case "POR":
                    url = path + "OrderPrint&orderno=" + reqno + "&reqdat=" + reqdate;
                    break;
                case "MRR":
                    url = path + "MRReceipt&mrno=" + reqno + "&reqdat=" + reqdate;
                    break;
                case "PBL":
                    url = path2 + "BillPrint&genno=" + reqno + "&Date1=" + reqdate;
                    break;
                default:
                    if (reqinfo2 == "JV" || reqinfo2 == "BC" || reqinfo2 == "CC" || reqinfo2 == "BD" || reqinfo2 == "CD")
                    {
                        url = path3 + "accVou&vounum=" + reqno + "&paytype=" + "0";
                    }
                    break;
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "printTracking('" + hostname + url + "');", true);

        }



        protected void ddlProjectName_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.imgbtnFindMat_Click(null, null);
            //this.colorBind();


        }

        protected void imgbtnFindReqno01_Click1(object sender, EventArgs e)
        {

        }

        protected void imgbtnFindReqno01_Click2(object sender, EventArgs e)
        {

        }
        protected void lbtnorder_Click(object sender, EventArgs e)
        {
            this.GetOrderNo();
        }
        private void GetOrderNo()
        {
            Session.Remove("tblorder");
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            //string date = Convert.ToDateTime(this.tos.Text).ToString("dd-MMM-yyyy");
            string orderno = "%" + this.txtsrchorder.Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "GETORDER", pactcode, "", orderno, "", "", "", "", "", "");
            this.ddlOrder.DataTextField = "orderno1";
            this.ddlOrder.DataValueField = "orderno";
            this.ddlOrder.DataSource = ds1.Tables[0];
            this.ddlOrder.DataBind();
            Session["tblorder"] = ds1.Tables[0];
            ds1.Dispose();
        }


        protected void lbtnOkOrder_Click(object sender, EventArgs e)
        {
            Session.Remove("tblpurchase");
            string comcod = this.GetComeCode();
            string orderno = this.ddlOrder.SelectedValue.ToString();

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "RPTORDERTRACING", orderno, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvorder.DataSource = null;
                this.gvorder.DataBind();

                return;
            }
            Session["tblpurchase"] = this.HiddenSameData(ds1.Tables[0]);
            this.LoadGrid();
        }

        private void PrintOrderTracking()
        {
            DataTable dt = (DataTable)Session["tblpurchase"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string orderno = this.ddlOrder.SelectedValue.ToString();

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "GETPROJECT", orderno, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                return;
            }
            string project = ds1.Tables[0].Rows[0]["actdesc"].ToString();
            //DataTable dt2 = (DataTable)Session["tblorder"];
            //DataRow dr = dt2.AsEnumerable()
            //              .SingleOrDefault(r => r.Field<string>("orderno") == orderno);

            //SP_REPORT_PURCHASE '3101','GETPROJECT', 'POR20210300001'


            var list = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.RptOrderTrack01>();
            LocalReport rpt = new LocalReport();
            rpt = RptSetupClass1.GetLocalReport("R_14_Pro.RptOrderTracking", list, null, null);
            rpt.SetParameters(new ReportParameter("comName", comnam));
            rpt.SetParameters(new ReportParameter("txtProject", project));
            rpt.SetParameters(new ReportParameter("txtTitle", "Order Tracking"));
            // rpt.SetParameters(new ReportParameter("narration", this.txtReqNarr.Text.Trim()));
            rpt.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = rpt;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void LinkGenBillTrack_Click(object sender, EventArgs e)
        {
            this.GetGeneralBillNo();
        }


        protected void Checksamehead_CheckedChanged(object sender, EventArgs e)
        {
            //this.gvGenBillTracking_RowEditing(null, null);
        }



        //protected void btnPrintReqInfo1_Click(object sender, EventArgs e)
        //{

        //}



        //protected void ddlGenBillTracking_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    this.GetGeneralBillNo();
        //}
    }
}