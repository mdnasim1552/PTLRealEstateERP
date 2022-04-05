using AjaxControlToolkit;
using Microsoft.Reporting.WinForms;
using RealEntity.C_22_Sal;
using RealERPLIB;
using RealERPRDLC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_28_MPro
{
    public partial class MktMarketSurvey : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        public static int i, j;
        public static string Url = "";
        static string prevPage = String.Empty;
        Common Common = new Common();
        SalesInvoice_BL lst = new SalesInvoice_BL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");



                ((Label)this.Master.FindControl("lblTitle")).Text = "Market Survey Information Input/Edit Screen";
                this.CommonButton();
                this.txtCurMSRDate.Text = DateTime.Today.ToString("dd.MM.yyyy");


                if (this.Request.QueryString["genno"].Length != 0)
                {
                    this.lnkReqList_Click(null, null);
                    this.lbtnMSROk_Click(null, null);
                }
                this.viewseciton();
            }
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Click += new EventHandler(btnForward_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lbtnResUpdate1_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Click += new EventHandler(btnReqDelete_Click);
        }



        private void CommonButton()
        {


            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
          
            if (this.Request.QueryString["Type"] == "Entry")
            {
                ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Text = "Next";
                this.txtMSRNarr2.ReadOnly = true;
                this.txtMSRNarr3.ReadOnly = true;
            }
            else if (this.Request.QueryString["Type"] == "Approval")
            {

                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Text = "Approved";
                ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Text = "Checked";
                this.txtMSRNarr2.ReadOnly = true;
                this.txtMSRNarr3.ReadOnly = true;
            }
          

            else
            {
                ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = true;
                ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Text = "Requisition Delete";
                ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Text = "Approved";
                this.txtMSRNarr2.ReadOnly = true;
                this.txtMSRNarr.ReadOnly = true;

                this.gvBestSelect.Columns[8].Visible = true;
            }

        }

        private void viewseciton()
        {
            string qtype = this.Request.QueryString["Type"].ToString();
            if (qtype == "Entry")
            {
                this.AsyncFileUpload1.Visible = true;
                this.imgLoader.Visible = true;
                this.ddlBestSupplier.Visible = true;
                this.Label2.Visible = true;
                if (this.txtCurMSRNo2.Text == "00000")
                {
                    this.pnlQutatt.Visible = false;
                }
                else
                {
                    this.pnlQutatt.Visible = true;
                    this.btnShowimg_Click(null, null);
                }




            }
            else
            {

              
                this.AsyncFileUpload1.Visible = false;
                this.imgLoader.Visible = false;
                this.ddlBestSupplier.Visible = false;
                this.Label2.Visible = false;
                this.pnlQutatt.Visible = true;
                this.btnShowimg.Visible = false;
                this.btnShowimg_Click(null, null);
            }


        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }
        protected void ImgbtnFindPreMR_Click(object sender, EventArgs e)
        {

        }
        protected void lbtnMSROk_Click(object sender, EventArgs e)
        {
            if (this.lbtnMSROk.Text == "New")
            {

                this.ImgbtnFindPreMR.Visible = true;
                this.ddlPrevMSRList.Visible = true;
                this.lblPreMrList.Visible = true;
                this.txtPreMSRSearch.Visible = true;
                this.ddlPrevMSRList.Items.Clear();
                this.lblCurMSRNo1.Text = "MSR" + DateTime.Today.ToString("MM") + "-";
                this.txtCurMSRDate.Enabled = true;
                this.lbltitel1.Visible = false;
                this.lbltitel2.Visible = false;
                this.lblJus.Visible = false;

                this.ddlReqList.Items.Clear();
                this.ddlReqList.Enabled = true;

                this.gvResInfo.DataSource = null;
                this.gvResInfo.DataBind();

                this.gvBestSelect.DataSource = null;
                this.gvBestSelect.DataBind();
                this.fotpanel.Visible = false;
                this.lbtnMSROk.Text = "Ok";
                return;
            }
            this.ddlReqList.Enabled = false;

            this.lbltitel1.Visible = true;
            this.lbltitel2.Visible = true;
            this.lblJus.Visible = true;
            this.txtMSRNarr.Visible = true;
            this.ImgbtnFindPreMR.Visible = false;
            this.ddlPrevMSRList.Visible = false;
            this.lblPreMrList.Visible = false;
            this.txtPreMSRSearch.Visible = false;

            this.fotpanel.Visible = true;
            this.lbtnMSROk.Text = "New";
            this.Get_Survey_Info();


        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            this.CSPrint();

        }

        private void CSPrint()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString(); 
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string CurDate1 = this.GetStdDate(this.txtCurMSRDate.Text.Trim());
            string Reqno = this.ddlReqList.SelectedValue.ToString();
            string msrno = ASTUtility.Left(this.lblCurMSRNo1.Text.Trim(), 3) + ASTUtility.Right(this.txtCurMSRDate.Text.Trim(), 4) + this.lblCurMSRNo1.Text.Trim().Substring(3, 2) + this.txtCurMSRNo2.Text.Trim();

            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT_04", "GETMATREQWISE", Reqno,
                        "%", CurDate1, "", "", "", "", "", "");


            DataTable dt = ds2.Tables[1];
            DataTable dt1 = ds2.Tables[0];


            var lst = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.ComparativeStatementCreate>();
            var lst1 = dt1.DataTableToList<RealEntity.C_14_Pro.EClassPur.ComparativeStatementCreate>();

            string preparedby = ds2.Tables[2].Rows[0]["postedname"].ToString() + "\n" + ds2.Tables[2].Rows[0]["posteddat"];
            string checkedby = ds2.Tables[2].Rows[0]["fwdname"].ToString() + "\n" + ds2.Tables[2].Rows[0]["fwddat"];
            string varifiedby = ds2.Tables[2].Rows[0]["auditname"].ToString() + "\n" + ds2.Tables[2].Rows[0]["auditdat"];

            string SVJ = ds2.Tables[2].Rows[0]["msrnar"].ToString();
            string SVJ2 = ds2.Tables[2].Rows[0]["msrnar2"].ToString();
            string SVJ3 = ds2.Tables[2].Rows[0]["msrnar3"].ToString();

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass1.GetLocalReport("R_28_MPro.RptMktPurMarketSurvey", lst, lst1, null);
            Rpt1.SetParameters(new ReportParameter("BestS", "Best Selection"));
            Rpt1.SetParameters(new ReportParameter("CS", "Comparative Statement"));
            Rpt1.SetParameters(new ReportParameter("SVJ", "Purchase Justification: " + SVJ));
            Rpt1.SetParameters(new ReportParameter("SVJ2", "Audit Justification: " + SVJ2));
            Rpt1.SetParameters(new ReportParameter("SVJ3", "MD Justification: " + SVJ3));
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("msrno", "MSR No: " + msrno));
            Rpt1.SetParameters(new ReportParameter("Reqno", "Req No: " + Reqno));
            Rpt1.SetParameters(new ReportParameter("CurDate1", "Date: " + CurDate1));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Marketing Market Survey Information"));
            Rpt1.SetParameters(new ReportParameter("preparedby", preparedby));
            Rpt1.SetParameters(new ReportParameter("checkedby", checkedby));
            Rpt1.SetParameters(new ReportParameter("varifiedby", varifiedby));
            Rpt1.SetParameters(new ReportParameter("paystatus", ""));
            Rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
        }
        private void Get_Survey_Info()
        {

            string comcod = this.GetCompCode();
            string CurDate1 = this.GetStdDate(this.txtCurMSRDate.Text.Trim());
            string txtsearch = "%" + this.txtReqSearch.Text.ToString() + "%";
            string reqno = this.ddlReqList.SelectedValue.ToString();

            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT_04", "GETMATREQWISE", reqno,
                          txtsearch, CurDate1, "", "", "", "", "", "");

            if (ds2 == null)
                return;

            if (ds2.Tables[2].Rows.Count > 0)
            {
                this.lblCurMSRNo1.Text = ds2.Tables[2].Rows[0]["msrno1"].ToString().Substring(0, 6);
                this.txtCurMSRNo2.Text = ds2.Tables[2].Rows[0]["msrno1"].ToString().Substring(6, 5);
                this.txtCurMSRDate.Text = Convert.ToDateTime(ds2.Tables[2].Rows[0]["msrdat"]).ToString("dd.MM.yyyy");
                this.txtCurMSRDate.Enabled = false;

                this.ddlPrevMSRList.DataTextField = "msrno1";
                this.ddlPrevMSRList.DataValueField = "msrno";
                this.ddlPrevMSRList.DataSource = ds2.Tables[2];
                this.ddlPrevMSRList.DataBind();

                this.txtMSRNarr.Text = ds2.Tables[2].Rows[0]["msrnar"].ToString();
                this.txtMSRNarr2.Text = ds2.Tables[2].Rows[0]["msrnar2"].ToString();
                this.txtMSRNarr3.Text = ds2.Tables[2].Rows[0]["msrnar3"].ToString();


            }
            Session["tblBestSelect"] = ds2.Tables[1];
            Session["tblsup"] = ds2.Tables[0];
            Session["tblStdcharging"] = ds2.Tables[3];
            Session["tblSupCharging"] = ds2.Tables[4];
            this.gvResInfo_DataBind();
            this.Charging_DataBind();
            this.ddlBestSupplierinfo();

        }
        protected void Charging_DataBind()
        {

            DataTable tbl1 = (DataTable)Session["tblSupCharging"];

            DataTable tblcharging = (DataTable)Session["tblStdcharging"];
            for (int i = 0; i < tblcharging.Rows.Count; i++)
            {
                this.gvcharging.Columns[6 + i].Visible = true;
                this.gvcharging.Columns[6 + i].HeaderText = tblcharging.Rows[i]["sirdesc"].ToString();

            }
            this.gvcharging.DataSource = tbl1;
            this.gvcharging.DataBind();



        }

        protected void gvResInfo_DataBind()
        {
            string reqtype = ASTUtility.Right(this.ddlReqList.SelectedItem.Text.Trim(), 2);

            if (reqtype == "LC")
            {

                this.gvBestSelect.Columns[10].Visible = true;
                this.gvBestSelect.Columns[11].Visible = true;
                this.gvBestSelect.Columns[14].Visible = true;

                this.gvBestSelect.Columns[12].HeaderText = "FC Rate";
                this.gvBestSelect.Columns[13].HeaderText = "FC Amount";

                ////////////////////////

                this.gvResInfo.Columns[09].Visible = true;
                this.gvResInfo.Columns[10].Visible = true;
                this.gvResInfo.Columns[13].Visible = true;

                this.gvResInfo.Columns[11].HeaderText = "FC Rate";
                this.gvResInfo.Columns[12].HeaderText = "FC Amount";
            }




            DataTable tbl1 = (DataTable)Session["tblsup"];
            DataTable tbl2 = (DataTable)Session["tblBestSelect"];
            string comcod = this.GetCompCode();

            if (tbl1.Rows.Count == 0)
                return;
            if (tbl2.Rows.Count == 0)
                return;
            tbl1 = this.HiddenSameData(tbl1);



            this.gvResInfo.DataSource = tbl1;
            this.gvResInfo.DataBind();


            this.gvBestSelect.DataSource = HiddenSameData(tbl2);
            this.gvBestSelect.DataBind();
            for (int i = 0; i < this.gvBestSelect.Rows.Count; i++)
            {
                Label txtgvRate = (Label)gvBestSelect.Rows[i].FindControl("lblgvRateBSel");
                txtgvRate.Style.Add("color", "blue");


                string supcode1 = ((Label)this.gvBestSelect.Rows[i].FindControl("lblgvSuplBSel")).Text.Trim();
                if (supcode1 == "000000000000")
                {
                    LinkButton txtgvBsup = (LinkButton)gvBestSelect.Rows[i].FindControl("lblgrmet1BSel");
                    txtgvBsup.Style.Add("color", "red");
                }
            }


            this.FooterAmount();

        }

        protected void lnkReqList_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            string CurDate1 = this.GetStdDate(this.txtCurMSRDate.Text.Trim());
            string txtsearch = "%" + this.txtReqSearch.Text.ToString() + "%";
            string Type = (this.Request.QueryString["Type"] == "Check") ? "Check" : (this.Request.QueryString["Type"] == "Approved") ? "Approved"
                : (this.Request.QueryString["Type"] == "Audit") ? "Audit" : "Next";

            string Gennno = (this.Request.QueryString["genno"].Length == 0) ? "%" : this.Request.QueryString["genno"].ToString() + "%";
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT_04", "GETRPUREQNO", CurDate1,
                          txtsearch, Gennno, Type, "", "", "", "", "");

            if (ds2 == null)
                return;
            if (ds2.Tables[0].Rows.Count > 0)
            {
                this.ddlReqList.DataTextField = "REQNO1";
                this.ddlReqList.DataValueField = "REQNO";
                this.ddlReqList.DataSource = ds2.Tables[0];
                this.ddlReqList.DataBind();
            }
        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string acttype;


            acttype = dt1.Rows[0]["acttype"].ToString();
           

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["acttype"].ToString() == acttype )
                {
                    dt1.Rows[j]["rsirdesc"] = "";
                    dt1.Rows[j]["propqty"] = 0.00;
                    dt1.Rows[j]["stkqty"] = 0.00;
                    dt1.Rows[j]["reqnote"] = "";
                }

                acttype = dt1.Rows[j]["acttype"].ToString();
                
            }

            return dt1;
        }

        private void FooterAmount()
        {
            DataTable tbl2 = (DataTable)Session["tblBestSelect"];

            if (tbl2.Rows.Count == 0)
                return;


            ((Label)this.gvBestSelect.FooterRow.FindControl("lblFAmountbs")).Text = Convert.ToDouble((Convert.IsDBNull(tbl2.Compute("sum(amount)", "")) ?
                                0 : tbl2.Compute("sum(amount)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvBestSelect.FooterRow.FindControl("lblFbdtamt")).Text = Convert.ToDouble((Convert.IsDBNull(tbl2.Compute("sum(bdtamt)", "")) ?
                               0 : tbl2.Compute("sum(bdtamt)", ""))).ToString("#,##0.00;(#,##0.00); ");


        }



        private void BS_SaveValue()
        {

            DataTable dt = (DataTable)Session["tblBestSelect"];
            for (int i = 0; i < this.gvBestSelect.Rows.Count; i++)
            {
                dt.Rows[i]["areqty"] = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvBestSelect.Rows[i].FindControl("txtgvareqty")).Text.Trim()));
                dt.Rows[i]["rate"] = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((Label)this.gvBestSelect.Rows[i].FindControl("lblgvRateBSel")).Text.Trim()));

                dt.Rows[i]["paytype"] = ((TextBox)this.gvBestSelect.Rows[i].FindControl("txtgvpaytype")).Text.Trim();
                dt.Rows[i]["advamt"] = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvBestSelect.Rows[i].FindControl("txtgvadvamt")).Text.Trim()));

                //dt.Rows[i]["conrate"] = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvBestSelect.Rows[i].FindControl("txtconrate")).Text.Trim()));


            }
            Session["tblBestSelect"] = dt;

        }


        private void SaveValue()
        {
            int index;
            DataTable dt = (DataTable)Session["tblsup"];
            for (int i = 0; i < this.gvResInfo.Rows.Count; i++)
            {
                index = (this.gvResInfo.PageSize) * (this.gvResInfo.PageIndex) + i;

                string approved = (((CheckBox)gvResInfo.Rows[i].FindControl("chkboxgv")).Checked) ? "True" : "False"; //dt.Rows[index]["approved"].ToString();

                double Reqqty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((Label)this.gvResInfo.Rows[i].FindControl("lblgvpropqty_01")).Text.Trim()));
                double Rate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvResInfo.Rows[i].FindControl("txtgvRate")).Text.Trim()));
                double csreqqty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvResInfo.Rows[i].FindControl("txtgvcsreqqty")).Text.Trim()));
                double advamt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvResInfo.Rows[i].FindControl("txtgvadvamtC")).Text.Trim()));
                string paytype = ((TextBox)this.gvResInfo.Rows[i].FindControl("txtgvpaytypeC")).Text.Trim();
                string remakrs = ((TextBox)this.gvResInfo.Rows[i].FindControl("TextRemarks")).Text.Trim();
                dt.Rows[i]["approved"] = (((CheckBox)gvResInfo.Rows[i].FindControl("chkboxgv")).Checked) ? "True" : "False";

                dt.Rows[i]["advamt"] = advamt;
                dt.Rows[i]["paytype"] = paytype;
                dt.Rows[i]["msrrmrk"] = remakrs;
                dt.Rows[i]["rate"] = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvResInfo.Rows[i].FindControl("txtgvRate")).Text.Trim()));
                dt.Rows[i]["csreqqty"] = (approved == "False") ? 0.00 : (csreqqty == 0) ? Reqqty : Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvResInfo.Rows[i].FindControl("txtgvcsreqqty")).Text.Trim()));
                ((TextBox)this.gvResInfo.Rows[j].FindControl("txtgvcsreqqty")).Text = (approved == "False") ? "" : (csreqqty == 0) ? Reqqty.ToString() : csreqqty.ToString();


                dt.Rows[i]["amount"] = (approved == "False") ? 0.00 : (Rate * ((csreqqty == 0) ? Reqqty : csreqqty));
                dt.Rows[i]["bdtamt"] = (approved == "False") ? 0.00 : (Rate  * ((csreqqty == 0) ? Reqqty : csreqqty));
               

            }
            Session["tblsup"] = dt;

        }

        protected void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.BS_SaveValue();
            this.gvResInfo_DataBind();
            this.Chargin_SaveValue();
            this.Charging_DataBind();
        }

        protected void GetMSRNo()
        {
            string comcod = this.GetCompCode();
            string mMSRNO = "NEWMSR";
            if (this.ddlPrevMSRList.Items.Count > 0)
                mMSRNO = this.ddlPrevMSRList.SelectedValue.ToString();

            string mREQDAT = this.GetStdDate(this.txtCurMSRDate.Text.Trim());
            if (mMSRNO == "NEWMSR")
            {
                DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT_04", "GETLASTMSRINFO", mREQDAT,
                       "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    mMSRNO = ds2.Tables[0].Rows[0]["maxmsrno"].ToString();

                    this.lblCurMSRNo1.Text = ds2.Tables[0].Rows[0]["maxmsrno1"].ToString().Substring(0, 6);
                    this.txtCurMSRNo2.Text = ds2.Tables[0].Rows[0]["maxmsrno1"].ToString().Substring(6, 5);

                    this.ddlPrevMSRList.DataTextField = "maxmsrno";
                    this.ddlPrevMSRList.DataValueField = "maxmsrno1";
                    this.ddlPrevMSRList.DataSource = ds2.Tables[0];
                    this.ddlPrevMSRList.DataBind();
                }
            }

        }
        protected void lbtnResUpdate1_Click(object sender, EventArgs e)
        {
            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                Response.Redirect("~/AcceessError.aspx");

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "You have no permission" + "');", true);
               
                return;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();

            this.lnkbtnRecalculate_Click(null, null);
            //this.SaveValue();
            DataTable tbl1 = (DataTable)Session["tblsup"];

            if (this.ddlPrevMSRList.Items.Count == 0)
                this.GetMSRNo();
            string mMSRDAT = this.GetStdDate(this.txtCurMSRDate.Text.Trim());
            string reqno = this.ddlReqList.SelectedValue.ToString();
            string mMSRNO = this.lblCurMSRNo1.Text.Trim().Substring(0, 3) + this.txtCurMSRDate.Text.Trim().Substring(6, 4) + this.lblCurMSRNo1.Text.Trim().Substring(3, 2) + this.txtCurMSRNo2.Text.Trim();


            int index;
            string Rsircode = "000000000000", spcfcod = "000000000000";
            double chkqty = 0.00, treqqty = 0.00;
            //for (int j = 0; j < this.gvResInfo.Rows.Count; j++)
            //{

            //    index = (this.gvResInfo.PageSize) * (this.gvResInfo.PageIndex) + j;

            //    string acttype = tbl1.Rows[index]["acttype"].ToString();
            //    string spcfcode = "000000000000";
            //    string approved = tbl1.Rows[index]["approved"].ToString();

            //    double gvpropqty = Convert.ToDouble(tbl1.Rows[index]["propqty"]);



            //    double gvcsreqqty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvResInfo.Rows[j].FindControl("txtgvcsreqqty")).Text.Trim()));



            //    if (Rsircode == Resocde && spcfcod == spcfcode)
            //    {
            //        chkqty = chkqty - gvcsreqqty;
            //        if (chkqty < 0)
            //        {
            //            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Not Within the Requisition" + "');", true);
            //            return;
            //        }
            //    }
            //    else
            //    {
            //        chkqty = gvpropqty - gvcsreqqty;
            //    }

            //    Rsircode = tbl1.Rows[index]["rsircode"].ToString();
            //    spcfcod = tbl1.Rows[index]["spcfcod"].ToString();


            //}
         




            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                double mRESRATE = Convert.ToDouble(tbl1.Rows[i]["rate"].ToString());

                if (mRESRATE == 0.00)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Check Supplier Rate" + "');", true);
                    return;
                }



            }


           
            string PostedByid = comcod + ASTUtility.Right(hst["usrid"].ToString(), 3);
            string Postedtrmid = hst["compname"].ToString();
            string PostedSession = hst["session"].ToString();
            string PostedDat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");


            string aprvbyid =(this.Request.QueryString["Type"]=="Approval")? comcod + ASTUtility.Right(hst["usrid"].ToString(), 3) : "" ;
            string aprvtrmid = (this.Request.QueryString["Type"] == "Approval") ? hst["compname"].ToString():"";
            string aprvSession = (this.Request.QueryString["Type"] == "Approval") ? hst["session"].ToString():"";
            string aprvDat = (this.Request.QueryString["Type"] == "Approval") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt"):"01-Jan-1900";


            

            string mMSRNAR = this.txtMSRNarr.Text.Trim();
            string mMSRNAR2 = this.txtMSRNarr2.Text.Trim();
            string mMSRNAR3 = this.txtMSRNarr3.Text.Trim();

            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT_04", "UPDATEPURMSRINFO", "PURMSRB",
                             mMSRNO, mMSRDAT, PostedByid, Postedtrmid, PostedSession, PostedDat, mMSRNAR, reqno, mMSRNAR2, mMSRNAR3, aprvbyid, aprvtrmid, aprvSession, aprvDat);
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);
                return;
            }

            //DataTable tbl1 = (DataTable)Session["tblsup"];
            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                int index1 = (this.gvResInfo.PageSize) * (this.gvResInfo.PageIndex) + i;
                string acttype = tbl1.Rows[i]["acttype"].ToString();
                string mSPCFCOD ="000000000000";
                string mSSIRCODE = tbl1.Rows[i]["ssircode"].ToString();
                string mRESRATE = tbl1.Rows[i]["rate"].ToString();

                string mMSRRMRK = tbl1.Rows[i]["msrrmrk"].ToString();
                string mMSRRQty = tbl1.Rows[i]["csreqqty"].ToString();
                string mMSRRBrand = "";//tbl1.Rows[i]["brand"].ToString();
                string mMSRRDelivery = "0.00";// tbl1.Rows[i]["delivery"].ToString();

                string mMSRRPay = "0.00";//tbl1.Rows[i]["payment"].ToString();

                string mMaxrate = "0.00";//tbl1.Rows[i]["maxrate"].ToString();
                string mPaylimit = "0.00";//tbl1.Rows[i]["paylimit"].ToString();
               
                string mAppr = (((CheckBox)gvResInfo.Rows[index1].FindControl("chkboxgv")).Checked) ? "True" : "False";
                string paytype = tbl1.Rows[i]["paytype"].ToString();
                string advamt = tbl1.Rows[i]["advamt"].ToString();


                result = purData.UpdateTransInfo3(comcod, "SP_ENTRY_MKT_PROCUREMENT_04", "UPDATEPURMSRINFO", "PURMSRA",
                         mMSRNO, acttype, mSPCFCOD, mSSIRCODE, mRESRATE, mMSRRMRK, mMSRRQty, mMSRRBrand, mMSRRDelivery, mMSRRPay, mMaxrate, mPaylimit, "", "", mAppr, paytype, advamt);

                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);
                    return;
                }

                this.pnlQutatt.Visible = true;

            }



            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string mSSIRCODE = tbl1.Rows[i]["ssircode"].ToString();
                string acttype = tbl1.Rows[i]["acttype"].ToString();
                string mSPCFCOD ="000000000000";
                string rate = tbl1.Rows[i]["rate"].ToString();
                string approved = tbl1.Rows[i]["approved"].ToString();

                result = purData.UpdateTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT_04", "UPDATESUPLRESINFOAREQ", mSSIRCODE, acttype, mSPCFCOD, rate, reqno, approved, "", "", "", "");
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);
                    return;
                }
            }





            // add new charging valuee----- by Safi
            DataTable newCharging = (DataTable)Session["tblnewCharging"];
            DataSet ds1 = new DataSet("ds1");
            ds1.Merge(newCharging);
            ds1.Tables[0].TableName = "tbl1";
            result = purData.UpdateXmlTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT_04", "UPDATESUPLCHARGING", ds1, null, null, mMSRNO, "", "", "", "", "", "");
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);
                return;
            }

            string Type = this.Request.QueryString["Type"].ToString();
            string msg = "";
            switch (Type)
            {
                case "Approval":
                    msg = "Market Survey Approved successfully";
                    break;

                default:
                    msg = "Market Survey Updated successfully";
                    break;
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = this.lblReqList.Text;
                string eventdesc = "Update Supplier";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

            this.Get_Survey_Info();




        }
        protected void gvResInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;


        }




        protected void lbtTotal1_Click(object sender, EventArgs e)
        {
            this.FooterAmount();
            this.gvResInfo_DataBind();
        }
        protected void btnForward_Click(object sender, EventArgs e)
        {

            try
            {

                string comcod = this.GetCompCode();
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string userid = comcod + ASTUtility.Right(hst["usrid"].ToString(), 3);
                string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                string reqno = this.ddlReqList.SelectedValue.ToString();
                string surveynpo = ASTUtility.Left(this.lblCurMSRNo1.Text.Trim(), 3) + ASTUtility.Right(this.txtCurMSRDate.Text.Trim(), 4) + this.lblCurMSRNo1.Text.Trim().Substring(3, 2) + this.txtCurMSRNo2.Text.Trim();
                string Sessionid = hst["session"].ToString();
                string Terminal = hst["compname"].ToString();

                string Type = (this.Request.QueryString["Type"] == "Check") ? "Check" : (this.Request.QueryString["Type"] == "Approved") ? "Approved"
                    : (this.Request.QueryString["Type"] == "Audit") ? "Audit" : "Next";
                if (Type == "Approved")
                {
                    DataSet ds = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "CHQREQAPP", reqno, "", "", "", "", "", "", "", "");

                    if (Convert.ToDouble(ds.Tables[0].Rows[0]["areqty"]) == 0)
                    {
                        this.AppReq();
                    }
                }



                bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "UPDATEMSERVAYNUM", reqno, surveynpo, userid, Date, Type, Sessionid,
                       Terminal, "", "", "", "", "", "", "", "");
                if (result == false)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Update Failed!" + "');", true);
                    return;
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Updated successfully" + "');", true);

            }
            catch (Exception ex)
            {

            }
        }
        private void AppReq()
        {
            string comcod = this.GetCompCode();
            this.BS_SaveValue();
            DataTable tbl1 = (DataTable)Session["tblBestSelect"];
            // string mMSRNO = ASTUtility.Left(this.lblCurMSRNo1.Text.Trim(), 3) + ASTUtility.Right(this.txtCurMSRDate.Text.Trim(), 4) + this.lblCurMSRNo1.Text.Trim().Substring(3, 2) + this.txtCurMSRNo2.Text.Trim();
            string Reqno = this.ddlReqList.SelectedValue.ToString();
            string mMSRNO = this.lblCurMSRNo1.Text.Trim().Substring(0, 3) + this.txtCurMSRDate.Text.Trim().Substring(6, 4) + this.lblCurMSRNo1.Text.Trim().Substring(3, 2) + this.txtCurMSRNo2.Text.Trim();

            DataSet ds1 = new DataSet("ds1");
            ds1.Merge(tbl1);
            ds1.Tables[0].TableName = "tbl1";

            bool result = purData.UpdateXmlTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "UPDATECSAPPREQ", ds1, null, null, Reqno, mMSRNO, "", "", "", "", "", "", "",
              "", "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);
                return;
            }

        }



        // ======================== file upload================


        private void ddlBestSupplierinfo()
        {
            DataTable dt = (DataTable)Session["tblsup"];
            DataView dv = dt.DefaultView;



            dv.RowFilter = ("ssircode <> '000000000000'");
            DataTable tbSupList = dv.ToTable(true, "ssircode", "supdesc");
            this.ddlBestSupplier.DataTextField = "supdesc";
            this.ddlBestSupplier.DataValueField = "ssircode";
            this.ddlBestSupplier.DataSource = tbSupList;
            this.ddlBestSupplier.DataBind();
        }


        protected void FileUploadComplete(object sender, AsyncFileUploadEventArgs e)
        {
            string comcod = this.GetCompCode();
            string msrno = ASTUtility.Left(this.lblCurMSRNo1.Text.Trim(), 3) + ASTUtility.Right(this.txtCurMSRDate.Text.Trim(), 4) + this.lblCurMSRNo1.Text.Trim().Substring(3, 2) + this.txtCurMSRNo2.Text.Trim();
            //lblCurMSRNo1
            string filename = System.IO.Path.GetFileName(AsyncFileUpload1.FileName);
            // i = i + 1;
            string ssircode = "";

            if (AsyncFileUpload1.HasFile)
            {
                ssircode = this.ddlBestSupplier.SelectedValue.ToString();

                string extension = Path.GetExtension(AsyncFileUpload1.PostedFile.FileName);
                AsyncFileUpload1.SaveAs(Server.MapPath("~/Uploads/Survey/") + comcod + msrno + ssircode + extension);

                Url = comcod + msrno + ssircode + extension;

            }
            //Url = Url.Substring(0,(Url.Length-1));

            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "QUTATIONIMAGEUPLOAD", msrno, ssircode, Url, "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {


                //this.lblMesg.Text=" Successfully Updated ";
            }

        }
        protected void btnShowimg_Click(object sender, EventArgs e)
        {
            Session.Remove("tblimgPath");

            DataTable tbfilePath = new DataTable();
            tbfilePath.Columns.Add("filePath1", Type.GetType("System.String"));
            tbfilePath.Columns.Add("supinfo", Type.GetType("System.String"));
            tbfilePath.Columns.Add("msrno", Type.GetType("System.String"));
            tbfilePath.Columns.Add("ssircode", Type.GetType("System.String"));

            Session["tblimgPath"] = tbfilePath;

            DataTable tbl2 = (DataTable)Session["tblimgPath"];
            string comcod = this.GetCompCode();
            string ssircode = this.ddlBestSupplier.SelectedValue.ToString();

            string msrno = ASTUtility.Left(this.lblCurMSRNo1.Text.Trim(), 3) + ASTUtility.Right(this.txtCurMSRDate.Text.Trim(), 4) + this.lblCurMSRNo1.Text.Trim().Substring(3, 2) + this.txtCurMSRNo2.Text.Trim();

            DataSet ds = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "QUTATIONIMAGESHOW", msrno, "", "", "", "", "", "", "", "");
            if (ds == null)
            {
                return;
            }

            DataTable tbl1 = ds.Tables[0];

            string Url = "";
            string supinfo = "";
            for (int j = 0; j < tbl1.Rows.Count; j++)
            {

                Url = "../Uploads/Survey/" + tbl1.Rows[j]["attacheddoc"].ToString().Trim();
                supinfo = tbl1.Rows[j]["SIRDESC"].ToString().Trim();
                DataRow dr1 = tbl2.NewRow();
                dr1["filePath1"] = Url;
                dr1["supinfo"] = supinfo;
                dr1["msrno"] = tbl1.Rows[j]["msrno"].ToString().Trim(); ;
                dr1["ssircode"] = tbl1.Rows[j]["ssircode"].ToString().Trim(); ;
                tbl2.Rows.Add(dr1);

            }




            ListViewEmpAll.DataSource = tbl2;
            ListViewEmpAll.DataBind();
            Session["tblimgPath"] = tbl2;


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
                        imgname.ImageUrl = "~/Images/pdf-icon.png";
                        break;
                    case ".xls":
                    case ".xlsx":
                        imgname.ImageUrl = "~/Images/Excel_2013_logo.png";
                        break;
                    case ".doc":
                    case ".docx":
                        imgname.ImageUrl = "~/Images/word-icon.png";
                        break;


                }

            }

        }





        protected void UploadFile(object sender, EventArgs e)
        {
            string path = Server.MapPath("~/Uploads/Survey/") + Path.GetFileName(FileUpload1.FileName);
            if (File.Exists(path))
            {
                Session["PostedFile"] = FileUpload1.PostedFile;
                ClientScript.RegisterClientScriptBlock(this.GetType(), "confirm", "Confirm();", true);
            }
            else
            {
                FileUpload1.SaveAs(path);
            }
        }
        protected void ConfirmReplace(object sender, EventArgs e)
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                HttpPostedFile postedFile = (Session["PostedFile"] as HttpPostedFile);
                string path = Server.MapPath("~/Uploads/Survey/") + Path.GetFileName(postedFile.FileName);
                postedFile.SaveAs(path);
            }
            Session["PostedFile"] = null;
        }
        protected void btnDelall_OnClick(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            for (int j = 0; j < this.ListViewEmpAll.Items.Count; j++)
            {
                string mrsno = ((Label)this.ListViewEmpAll.Items[j].FindControl("msrno")).Text.ToString();
                string ssircode = ((Label)this.ListViewEmpAll.Items[j].FindControl("ssircode")).Text.ToString();
                string filesname = ((Label)this.ListViewEmpAll.Items[j].FindControl("ImgLink")).Text.ToString();
                if (((CheckBox)this.ListViewEmpAll.Items[j].FindControl("ChDel")).Checked == true)
                {
                    bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "REMOVESURVEYIMG", mrsno, ssircode, "", "", "", "", "", "", "", "", "", "", "");

                    if (result == true)
                    {
                        string filePath = Server.MapPath("~/MFGWEB/");
                        System.IO.File.Delete(filePath + filesname);

                        this.lblMesg.Text = " Files Removed ";
                    }


                }




            }
            if (this.Request.QueryString["genno"].Length != 0)
            {
                this.lnkReqList_Click(null, null);
                this.lbtnMSROk.Text = "Ok";
                this.lbtnMSROk_Click(null, null);

            }
            this.viewseciton();

        }
        protected void btnReqDelete_Click(object sender, EventArgs e)
        {

            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["deleteCk"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "You have no permission" + "');", true);
                return;
            }

            string Reqno = this.Request.QueryString["genno"].ToString();
            string Type = "Audit";
            if (Reqno.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Please select your item for Delete" + "');", true);
            }

            string comcod = this.GetCompCode();

            bool result = purData.UpdateTransInfo(comcod, "SP_REPORT_PURCHASE_INTERFACE_01", "REVCSPART", Reqno, Type, "", "", "");

            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "CS Audit Delete Successfully" + "');", true);
            }
            Common.LogStatus("Purchase Interface", "CS Audit Delete", "Order No: ", Reqno);


        }
        private string XmlDataInsert(string Reqno, string Apprno, DataTable dt)
        {
            //Log Data
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string usrid = hst["usrid"].ToString();
            string trmnid = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");


            DataSet ds1 = new DataSet("ds1");

            dt.Columns.Add("delbyid", typeof(string));
            dt.Columns.Add("delseson", typeof(string));
            dt.Columns.Add("deldate", typeof(DateTime));

            dt.Rows[0]["delbyid"] = usrid;
            dt.Rows[0]["delseson"] = session;
            dt.Rows[0]["deldate"] = Date;


            ds1.Merge(dt);
            ds1.Tables[0].TableName = "tbl1";
            //ds1.Tables[1].TableName = "tbl2";

            bool resulta = purData.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "UPDATEXML01", ds1, null, null, Reqno, Apprno);

            if (!resulta)
            {

                //return;
            }
            else
            {
                ((Label)this.Master.FindControl("lblANMgsBox")).Visible = true;
                ((Label)this.Master.FindControl("lblANMgsBox")).Text = "Successfully Deleted";
                ((Label)this.Master.FindControl("lblANMgsBox")).Attributes["style"] = "background:Green;";

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
            }

            return "";
        }
        private void Chargin_SaveValue()
        {
            //    int index;
            Session.Remove("tblnewCharging");
            this.NewChargingTable();


            DataTable dt = (DataTable)Session["tblStdcharging"];
            DataTable dt2 = (DataTable)Session["tblSupCharging"];
            DataTable newchrg = (DataTable)Session["tblnewCharging"];
            string supl = "";

            for (int i = 0; i < this.gvcharging.Rows.Count; i++)
            {
                supl = ((Label)gvcharging.Rows[i].FindControl("lblgvSuplCod1")).Text.Trim();
                double suplamt = Convert.ToDouble("0" + ((Label)gvcharging.Rows[i].FindControl("lblgvSupamt")).Text.Trim());


                double total1 = 0.00;
                double total2 = 0.00;
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    double amount = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)gvcharging.Rows[i].FindControl("gvText" + j)).Text.Trim()));
                    dt2.Rows[i]["c" + j] = amount;
                    if (dt.Rows[j]["sircode"].ToString().Substring(0, 9) != "019999902")
                    {
                        total1 += amount;
                    }
                    else
                    {
                        total2 += amount;
                    }

                    DataRow dr = newchrg.NewRow();
                    dr["supcode"] = supl;
                    dr["rsircode"] = dt.Rows[j]["sircode"].ToString();
                    dr["amount"] = amount;
                    newchrg.Rows.Add(dr);
                }
                dt2.Rows[i]["tamt"] = total1 - total2;
                dt2.Rows[i]["tax"] = 0;
                dt2.Rows[i]["vat"] = 0;

            }
            Session["tblnewCharging"] = newchrg;
            Session["tblSupCharging"] = dt2;
        }

        private void NewChargingTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("supcode", typeof(System.String));
            dt.Columns.Add("rsircode", typeof(System.String));
            dt.Columns.Add("amount", typeof(System.Double));
            Session["tblnewCharging"] = dt;
        }

        protected void lblgrmet1BSel_Click(object sender, EventArgs e)
        {
            //string comcod = this.GetCompCode();
            //GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            //int index = row.RowIndex;
            //string rsircode = ((Label)this.gvBestSelect.Rows[index].FindControl("lblgvacttypeBSel1")).Text.ToString();
            //string spcfcod = "000000000000";
            //string reqno = this.Request.QueryString["genno"].ToString();
            //DataSet mathistory = purData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "GETMATWISEPURINF", rsircode, spcfcod, reqno, "", "");
            //if (mathistory.Tables[0].Rows.Count == 0)
            //{

            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "FnDanger();", true);
            //    return;
            //}
            //this.lblstore.Text = mathistory.Tables[0].Rows[0]["actdesc"].ToString();
            //this.lblmat.Text = mathistory.Tables[0].Rows[0]["subdesc"].ToString();
            //this.lblspc.Text = mathistory.Tables[0].Rows[0]["spcfdesc"].ToString();
            //string todate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            //var matlist = mathistory.Tables[0].DataTableToList<RealEntity.C_12_Inv.EclassPurchase.MaterialWiseStock>();
            //Session["MatWiseStock"] = matlist;
            //this.gvMatHis.DataSource = matlist;
            //this.gvMatHis.DataBind();
            ////-----------------------mat pur history
            //DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTMATPURHISTORY", "000000000000", rsircode, "", todate, "OpDate", "", "", "", "");
            //if (ds1 == null)
            //{
            //    this.gvMatPurHis.DataSource = null;
            //    this.gvMatPurHis.DataBind();
            //    return;
            //}
            //Session["MatPurHis"] = HiddenSameDate(ds1.Tables[0]);
            //this.gvMatPurHis.DataSource = HiddenSameDate(ds1.Tables[0]);
            //this.gvMatPurHis.DataBind();
            //FooterCalculation();
            //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);


        }

        protected void lblgrsirdescs1_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;

            string rsircode = ((Label)this.gvResInfo.Rows[index].FindControl("lblrsircode")).Text.ToString();
            Session["rsircode"] = rsircode;           
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT_04", "GETMSRSUPLIST", "%", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlSupl2.DataTextField = "ssirdesc1";
            this.ddlSupl2.DataValueField = "ssircode";
            this.ddlSupl2.DataSource = ds1.Tables[0];
            this.ddlSupl2.DataBind();
            //DataSet ds = lst.Curreny();
            //var lstConv = ds.Tables[0].DataTableToList<RealEntity.C_22_Sal.Sales_BO.ConvInf>();
            //Session["tblcur"] = lstConv;

            //var lstCurryDesc = ds.Tables[1].DataTableToList<RealEntity.C_22_Sal.Sales_BO.Currencyinf>();
            //Session["tblcurdesc"] = lstCurryDesc;
            //this.ddlCurrency.DataValueField = "curcode";
            //this.ddlCurrency.DataTextField = "curdesc";
            //this.ddlCurrency.DataSource = lstCurryDesc;
            //this.ddlCurrency.DataBind();

            //string fcode = "001";
            //string tcode = this.ddlCurrency.SelectedValue.ToString();
            //List<RealEntity.C_22_Sal.Sales_BO.ConvInf> lst1 = (List<RealEntity.C_22_Sal.Sales_BO.ConvInf>)Session["tblcur"];

            //double method = (((List<RealEntity.C_22_Sal.Sales_BO.ConvInf>)Session["tblcur"]).FindAll(p => p.fcode == fcode && p.tcode == tcode))[0].conrate;


            //this.lblConRate.Text = Convert.ToDouble(method).ToString("#,##0.000000;-#,##0.000000; ");
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openServyModal();", true);


        }
        
        protected void UpdateData_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();            
            string SSIRCODE = this.ddlSupl2.SelectedValue.ToString();
            string rsircode = Session["rsircode"].ToString();
            string spcfcod = "000000000000";
            string mRMRKS = "";
            string mRate = this.TextRate.Text.ToString();
            string mDelsys = "0";
            string mPayss = "0";
            string mPaylimit = "0";
            //purData.GetTransInfoNew(comcod, "SP_ENTRY_PURCHASE_04", "UPDATESRSUPLIST", null, null, null, SSIRCODE, rsircode, spcfcod, TextRate, Currency, conRate, "", "", "", "", "", "", "", "", "", "", "", "", "", "");

            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_MKT_PROCUREMENT_04", "UPDATESUPLRES",
                          SSIRCODE, rsircode, spcfcod, mRMRKS, mDelsys, mPayss, mRate, mPaylimit, "", "", "", "", "", "", "");


            Response.Redirect(Request.RawUrl, true);

        }

        protected void btnCurr_Click(object sender, EventArgs e)
        {

        }

        protected void Resource_List(string pmSrchTxt)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETMSRRESLIST", pmSrchTxt, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            Session["tblMat"] = ds1.Tables[0];
            Session["tblSpcf"] = ds1.Tables[1];
        }

        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["MatPurHis"];
            if (dt.Rows.Count == 0)
                return;

            ((Label)this.gvMatPurHis.FooterRow.FindControl("lgvMRRQty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mrrqty)", "")) ?
                                0 : dt.Compute("sum(mrrqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvMatPurHis.FooterRow.FindControl("lgvFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mrramt)", "")) ?
                                0 : dt.Compute("sum(mrramt)", ""))).ToString("#,##0;(#,##0); ");

        }
        private DataTable HiddenSameDate(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string mrrno = dt1.Rows[0]["mrrno"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if ((dt1.Rows[j]["mrrno"].ToString() == mrrno))
                {
                    mrrno = dt1.Rows[j]["mrrno"].ToString();
                    dt1.Rows[j]["mrrno1"] = "";
                    dt1.Rows[j]["mrrdat1"] = "";
                    dt1.Rows[j]["pactdesc"] = "";
                }

                else
                {
                    mrrno = dt1.Rows[j]["mrrno"].ToString();
                }

            }
            return dt1;

        }
        protected void gvMatHis_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                string reqno = this.Request.QueryString["genno"].ToString();
                string comcod = this.GetCompCode();
                HyperLink printlink = (HyperLink)e.Row.FindControl("LbtnPrint");
                string grp = ((Label)e.Row.FindControl("lblGp")).Text.ToString();
                string genno = ((Label)e.Row.FindControl("lblgenno")).Text.ToString();

                switch (grp)
                {
                    case "B":
                        printlink.Visible = true;
                        printlink.NavigateUrl = "~/F_11_Pro/PuchasePrint.aspx?Type=OrderPrint&comcod=" + comcod + "&orderno=" + genno;
                        printlink.CssClass = "btn btn-xs btn-info";
                        break;

                }




            }
        }
        protected void LbtnModalPrint_Click(object sender, EventArgs e)
        {

            string plug = this.txtflag.Text.ToString();

            Print_PurchaseIssue();
           

        }

        private void Print_PurchaseIssue()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();  //address
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            var list = (List<RealEntity.C_12_Inv.EclassPurchase.MaterialWiseStock>)Session["MatWiseStock"];
            string storename = list[0].actdesc;
            string material = list[0].subdesc;
            string specification = list[0].subdesc;

            LocalReport rpt1 = new LocalReport();

            rpt1 = RptSetupClass1.GetLocalReport("RD_07_Inv.RptMWIssueandPS", list, null, null);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("storename", storename));

            rpt1.SetParameters(new ReportParameter("material", material));
            rpt1.SetParameters(new ReportParameter("specification", specification));
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("rptTitle", "Material Wise Issue and Purchase stock"));
            //rpt1.SetParameters(new ReportParameter("FromToDate", ""));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
            //rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));

            Session["Report1"] = rpt1;
            ScriptManager.RegisterStartupScript(this, GetType(), "target", "PrintRDLC();", true);
        }
       

    }
}