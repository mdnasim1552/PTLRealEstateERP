
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web.Security;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using System.Web.UI.DataVisualization.Charting;
using System.IO;
using RealERPLIB;
using RealEntity.C_09_LCM;
//using ACCRPT;
namespace RealERPWEB.F_09_LCM
{
    public partial class LCAllInfo : System.Web.UI.Page
    {
        ProcessAccess SalesData = new ProcessAccess();
        Common cDate = new Common();
        public static string orderno = "", actcode = "", custid = "", orderno1 = "", orderdat = "";
        public static string billstatus = "";
        public static string Desc = "";
        public static string actdesc = "";
        public static string Date = "";
        static string prevPage = String.Empty;

        BL_AllLCInfo LCAllInformation = new BL_AllLCInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (prevPage.Length == 0)
                {
                    prevPage = Request.UrlReferrer.ToString();
                }

                ((Label)this.Master.FindControl("lblprintstk")).Text = "";
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                this.MultiView1.ActiveViewIndex = 0;
                double day = Convert.ToInt32(System.DateTime.Today.ToString("dd")) - 1;
                this.txtfromdate.Text = DateTime.Today.AddDays(-day).ToString("dd-MMM-yyyy");

                this.txttodate.Text = DateTime.Today.ToString("dd-MMM-yyyy");


                this.GetLcType();
                this.GetTransaction();
                this.CommonButton();

                Hashtable hst = (Hashtable)Session["tblLogin"];

                string qType = this.Request.QueryString["Type"].ToString();
                //((Label)this.Master.FindControl("lblTitle")).Text = (qType == "All") ? "LC Information" : "";




            }
        }
        private void CommonButton()
        {



            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Attributes.Add("href", "../F_09_LCM/LCInformation.aspx?tname=order&tid=lc");
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Attributes.Add("target", "_blank");



            ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;

            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            ((CheckBox)this.Master.FindControl("CheckBox1")).Visible = false;

            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            //((LinkButton)this.Master.FindControl("btnClose")).Visible = true;

        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            this.GetTransaction();
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Click += new EventHandler(lnkbtnLedger_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Click += new EventHandler(lnkbtnAdd_Click1);
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Click += new EventHandler(lnkbtnEdit_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Click += new EventHandler(lnkbtnDelete_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);


        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    string comcod = this.GetCompCode();
            //    Hashtable hst = (Hashtable)Session["tblLogin"];
            //    string comnam = hst["comnam"].ToString();
            //    string compname = hst["compname"].ToString();
            //    string comadd = hst["comadd1"].ToString();
            //    string username = hst["username"].ToString();
            //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");


            //    DataSet ds = SalesData.GetTransInfo(comcod, "SP_ENTRY_SALES_ORDER", "RPTSALESORDER", orderno, centrid, "", "", "", "", "", "", "");

            //    double Amount = Convert.ToDouble((Convert.IsDBNull(ds.Tables[0].Compute("sum(amount)", "")) ? 0.00 : ds.Tables[0].Compute("sum(amount)", "")));
            //    double IndDis = Convert.ToDouble((Convert.IsDBNull(ds.Tables[0].Compute("sum(ordis)", "")) ? 0.00 : ds.Tables[0].Compute("sum(ordis)", "")));
            //    double Dues = Convert.ToDouble((Convert.IsDBNull(ds.Tables[1].Compute("sum(dues)", "")) ? 0.00 : ds.Tables[1].Compute("sum(dues)", "")));
            //    double Curamt = Convert.ToDouble((Convert.IsDBNull(ds.Tables[1].Compute("sum(collamt)", "")) ? 0.00 : ds.Tables[1].Compute("sum(collamt)", "")));
            //    double OvDis = Convert.ToDouble((Convert.IsDBNull(ds.Tables[1].Compute("sum(invdis)", "")) ? 0.00 : ds.Tables[1].Compute("sum(invdis)", "")));
            //    double Npayamt = ((Amount - (IndDis + OvDis)) + Dues) - Curamt;

            //    ReportDocument rptChallan = new RealERPRPT.R_21_Mkt.RptSalesOrder();
            //    TextObject txtrptcomp = rptChallan.ReportDefinition.ReportObjects["Company"] as TextObject;
            //    txtrptcomp.Text = comnam;
            //    TextObject txtCompAdd = rptChallan.ReportDefinition.ReportObjects["txtCompAdd"] as TextObject;
            //    txtCompAdd.Text = comadd;
            //    TextObject txtrptHeader = rptChallan.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            //    txtrptHeader.Text = "SALES ORDER";

            //    TextObject txtStore = rptChallan.ReportDefinition.ReportObjects["txtStore"] as TextObject;
            //    txtStore.Text = ds.Tables[1].Rows[0]["centrdesc"].ToString();

            //    TextObject txtChallan = rptChallan.ReportDefinition.ReportObjects["txtChallan"] as TextObject;
            //    txtChallan.Text = orderno1;
            //    TextObject txtDate = rptChallan.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //    txtDate.Text = Convert.ToDateTime(ds.Tables[1].Rows[0]["orderdat"]).ToString("dd-MMM-yyyy");

            //    TextObject txtUser = rptChallan.ReportDefinition.ReportObjects["txtUser"] as TextObject;
            //    txtUser.Text = ds.Tables[1].Rows[0]["teamdesc"].ToString().ToUpper();


            //    TextObject txtCust = rptChallan.ReportDefinition.ReportObjects["txtCust"] as TextObject;
            //    txtCust.Text = ds.Tables[1].Rows[0]["custdesc"].ToString();
            //    TextObject txtCustadd = rptChallan.ReportDefinition.ReportObjects["txtAdd"] as TextObject;
            //    txtCustadd.Text = ds.Tables[1].Rows[0]["custadd"].ToString();
            //    TextObject txtPhone = rptChallan.ReportDefinition.ReportObjects["txtPhone"] as TextObject;
            //    txtPhone.Text = ds.Tables[1].Rows[0]["custphone"].ToString();

            //    TextObject txtCuSal = rptChallan.ReportDefinition.ReportObjects["txtCuSal"] as TextObject;
            //    txtCuSal.Text = (Amount - (IndDis + OvDis)).ToString("#,##0.00;(#,##0.00); ");

            //    TextObject txtDis = rptChallan.ReportDefinition.ReportObjects["txtDis"] as TextObject;
            //    txtDis.Text = (IndDis + OvDis).ToString("#,##0.00;(#,##0.00); ");
            //    TextObject txtDues = rptChallan.ReportDefinition.ReportObjects["txtDues"] as TextObject;
            //    txtDues.Text = Dues.ToString("#,##0.00;(#,##0.00); ");
            //    TextObject txtColl = rptChallan.ReportDefinition.ReportObjects["txtColl"] as TextObject;
            //    txtColl.Text = Curamt.ToString("#,##0.00;(#,##0.00); ");
            //    TextObject txtNetAmt = rptChallan.ReportDefinition.ReportObjects["txtNetAmt"] as TextObject;
            //    txtNetAmt.Text = Npayamt.ToString("#,##0.00;(#,##0.00); ");

            //    TextObject txtInWords = rptChallan.ReportDefinition.ReportObjects["txtInWords"] as TextObject;
            //    txtInWords.Text = "IN-WARD : " + ASTUtility.Trans(((Amount - (IndDis + OvDis))), 2).ToUpper();

            //    TextObject txtSignComp = rptChallan.ReportDefinition.ReportObjects["txtSignComp"] as TextObject;
            //    txtSignComp.Text = comnam;

            //    TextObject txtuserinfo = rptChallan.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //    rptChallan.SetDataSource(ds.Tables[0]);

            //    if (ConstantInfo.LogStatus == true)
            //    {
            //        string eventtype = "SALES ORDER";
            //        string eventdesc = "Print Report";
            //        string eventdesc2 = "ORDER: " + orderno1;
            //        bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //    }


            //    string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //    rptChallan.SetParameterValue("ComLogo", ComLogo);

            //    Session["Report1"] = rptChallan;

            //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            //}
            //catch (Exception ex)
            //{

            //}

        }
        private void GetLcType()
        {

            string comcod = this.GetCompCode();
            DataSet ds2 = SalesData.GetTransInfo(comcod, "SP_LC_INFO", "GETLCTYPE", "", "", "", "", "", "", "", "", "");
            if (ds2.Tables[0].Rows.Count == 0)
            {
                return;
            }
            else
            {
                this.ddllctypelist.DataSource = ds2.Tables[0];
                this.ddllctypelist.DataTextField = "actdesc";
                this.ddllctypelist.DataValueField = "actcode";
                this.ddllctypelist.SelectedValue = "000000000000";
                this.ddllctypelist.DataBind();

                ds2.Dispose();

            }
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void GetTransaction()
        {

            string date1 = this.txtfromdate.Text;
            string date2 = this.txttodate.Text;
            string actcode = (this.ddllctypelist.SelectedValue == "000000000000") ? "%" : this.ddllctypelist.SelectedValue.ToString();
            var lstOrder = LCAllInformation.GetAllLCInfo(date1, date2, actcode);

            if (lstOrder == null)
            {
                this.gvTransaction.DataSource = null;
                this.gvTransaction.DataBind();
                return;
            }
            ViewState["tblgltransection"] = lstOrder;

            this.Data_Bind();





        }
        private void Data_Bind()
        {

            var lst = (List<RealEntity.C_09_LCM.BO_AllLCInfo.AllLCInfolist>)ViewState["tblgltransection"];
            if (lst.Count == 0)
            {
                return;
            }
            this.gvTransaction.DataSource = lst;
            this.gvTransaction.DataBind();
            this.FooterCal();

            Session["Report1"] = gvTransaction;
            HyperLink link = (HyperLink)this.gvTransaction.HeaderRow.FindControl("hlbtntbCdataExel") as HyperLink;
            link.NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            //((HyperLink)this.gvTransaction.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";



        }






        protected void lnkFinalUpdate_Click(object sender, EventArgs e)
        {

        }
        protected void lnkbtnLedger_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('RptAccVouher.aspx?vounum=" + actcode + "&comcod=" + comcod + "', target='_blank');</script>";
            //  ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('RptAccVouher.aspx?vounum=JV201510000015" + "', target='_blank');</script>";
        }
        protected void gvTransaction_RowCommand(object sender, GridViewCommandEventArgs e) //
        {

            if (e.CommandName == "")
            {
                GridViewRow row = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
                if (row.RowType.ToString() == "DataRow")
                {
                    ((Label)this.Master.FindControl("lblprintstk")).Text = "";
                    GridViewRow gvr = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                    int RowIndex = gvr.RowIndex;
                    actcode = ((Label)this.gvTransaction.Rows[RowIndex].FindControl("lgvactcode")).Text.Trim();
                    //custid = ((Label)this.gvTransaction.Rows[RowIndex].FindControl("lblgvcustid")).Text.Trim();
                    //orderno = ((Label)this.gvTransaction.Rows[RowIndex].FindControl("lblgvorderno")).Text.Trim();
                    //orderno1 = ((Label)this.gvTransaction.Rows[RowIndex].FindControl("lblgvorderno1")).Text.Trim();
                    //orderdat = ((Label)this.gvTransaction.Rows[RowIndex].FindControl("lnkgvdate")).Text.Trim();
                    //billstatus = ((Label)this.gvTransaction.Rows[RowIndex].FindControl("lblgvbillstatus")).Text.Trim();
                    Desc = ((LinkButton)this.gvTransaction.Rows[RowIndex].FindControl("lnkgvLcno")).Text.Trim();
                    //
                    //centrdesc = ((Label)this.gvTransaction.Rows[RowIndex].FindControl("lblgvacno")).Text.Trim();
                    //Date = ((Label)this.gvTransaction.Rows[RowIndex].FindControl("lnkgvdate")).Text.Trim();

                }
            }
        }
        private void FooterCal()
        {
            var lst = (List<RealEntity.C_09_LCM.BO_AllLCInfo.AllLCInfolist>)ViewState["tblgltransection"];

            if (lst.Count == 0)
                return;
            ((Label)this.gvTransaction.FooterRow.FindControl("lblfamtusd")).Text = (lst.Select(p => p.fcamt).Sum() == 0.00) ? "0.00" : lst.Select(p => p.fcamt).Sum().ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvTransaction.FooterRow.FindControl("lblFAmountbdt")).Text = (lst.Select(p => p.bdamt).Sum() == 0.00) ? "0.00" : lst.Select(p => p.bdamt).Sum().ToString("#,##0;(#,##0); ");


        }

        protected void lnkbtnEdit_Click(object sender, EventArgs e)
        {
            if (billstatus == "True")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('This Bill Already Adjusted');", true);
            }
            else
            {
                if (orderno.Length != 0)
                    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('AccInv.aspx?Type=Edit" + "&vounum=" + actcode + "', target='_self');</script>";
            }
        }



        protected void lnkbtnAdd_Click1(object sender, EventArgs e)
        {

            string qType = this.Request.QueryString["Type"].ToString();
            if (qType == "All")
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../F_09_LCM/LCInformation.aspx?tname=order&tid=lc" + "', target='_self');</script>";
            }

        }
        protected void lnkbtnDelete_Click(object sender, EventArgs e)
        {
            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            //if (!Convert.ToBoolean(dr1[0]["deleteCk"]))
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
            //    return;
            //}


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();


            bool result = SalesData.UpdateTransInfo(comcod, "SP_REPORTS_LC_INFO", "GETLCALLINFODELETE", actcode, "", "", "", "");

            if (result == true)
            {
                var lst = ((List<BO_AllLCInfo.AllLCInfolist>)ViewState["tblgltransection"]);
                var lstno = lst.FindAll((p => p.actcode != actcode));
                ViewState["tblgltransection"] = lstno;
            }


            this.Data_Bind();


        }
        protected void lnkgvLcno_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            for (int i = 0; i < this.gvTransaction.Rows.Count; i++)
            {
                if (i == index)
                    this.gvTransaction.Rows[i].Attributes["style"] = "border:2px solid blue;";
                else
                {

                    this.gvTransaction.Rows[i].Attributes["style"] = "border:none;";
                }

            }
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }
    }
}