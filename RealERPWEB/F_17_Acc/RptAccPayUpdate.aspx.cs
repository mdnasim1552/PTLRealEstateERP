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
using CrystalDecisions.ReportSource;
using RealERPLIB;
using RealERPRPT;
using RealERPRDLC;
using Microsoft.Reporting.WinForms;
namespace RealERPWEB.F_17_Acc
{
    public partial class RptAccPayUpdate : System.Web.UI.Page
    {
        //public static string Narration = "";
        public static double TAmount = 0;
        ProcessAccess accData = new ProcessAccess();
        public static int PageNumber = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                string type = this.Request.QueryString["Type"].ToString();
                //((Label)this.Master.FindControl("lblTitle")).Text = type == "GroupWiseChqIssued" ? "Cheque Issued (Group Wise)" : type == "PVSegment"? "Segment wise Post Dated Voucher Report" : "Day Wise Issued (Cheque Date)";
                //this.Master.Page.Title = "Day Wise Issued (Cheque Date)";

                this.txtfrmdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfrmdate.Text = "01" + this.txtfrmdate.Text.Trim().Substring(2);
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.ViewSection();
                this.GetBankName();
            }

        }



        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        private void ViewSection()
        {

            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "ChqIsssued":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "GroupWiseChqIssued":
                    this.GetGroupName();
                    this.MultiView1.ActiveViewIndex = 1;
                    break;


                case "DateWiseChqIssued":
                    this.MultiView1.ActiveViewIndex = 2;
                    this.lblfrmrange.Visible = true;
                    this.lbltorange.Visible = true;
                    this.txtfrmrange.Visible = true;
                    this.txttorange.Visible = true;
                    this.txttorange.Visible = true;
                    this.chkPdc.Visible = true;
                    this.chkhonour.Visible = true;




                    break;

                case "PayStatusResource":

                    this.MultiView1.ActiveViewIndex = 3;
                    break;

                case "PVSegment":

                   
                    this.lblsegment.Visible = true;
                    this.ddlRange.Visible = true;
                    this.chkrange.Visible = true;

                    


                    this.MultiView1.ActiveViewIndex = 4;
                    break;


            }


        }


        protected void chkrange_CheckedChanged(object sender, EventArgs e)
        {
             if(chkrange.Checked)
            {
                this.lblfrmrange.Visible = true;
                this.lbltorange.Visible = true;
                this.txtfrmrange.Visible = true;
                this.txttorange.Visible = true;
                this.txttorange.Visible = true;

            }

            else
            {
                this.lblfrmrange.Visible = false;
                this.lbltorange.Visible = false;
                this.txtfrmrange.Visible = false;
                this.txttorange.Visible = false;
                this.txttorange.Visible = false;
                this.txtfrmrange.Text = "";
                this.txttorange.Text = "";

            }
            
        }

        protected void imgbtnSrchBank_Click(object sender, EventArgs e)
        {
            this.GetBankName();
        }

        private void GetBankName()
        {

            string comcod = this.GetCompCode();
            string SeachBankName = "%" + this.txtserchBankName.Text.Trim() + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETBANKNAME", SeachBankName, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.ddlBankName.Items.Clear();
                return;
            }

            DataTable dt = ds1.Tables[0];
            DataRow dr1 = dt.NewRow();
            dr1["comcod"] = this.GetCompCode();
            dr1["actcode"] = "000000000000";
            dr1["actdesc"] = "All Bank";
            dr1["actdesc1"] = "000000000000- All Bank";
            dt.Rows.Add(dr1);
            DataView dv = dt.DefaultView;
            dv.Sort = ("actcode");
            dt = dv.ToTable();

            this.ddlBankName.DataTextField = "actdesc1";
            this.ddlBankName.DataValueField = "actcode";
            this.ddlBankName.DataSource = dt;
            this.ddlBankName.DataBind();
            ds1.Dispose();
        }

        private void GetGroupName()
        {

            string comcod = this.GetCompCode();
            string SearhcGroup = "%" + this.txtserchGrpName.Text.Trim() + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PAYMENT", "GETGROUPNAME", SearhcGroup, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlGroupDesc.DataTextField = "sirdesc";
            this.ddlGroupDesc.DataValueField = "sircode";
            this.ddlGroupDesc.DataSource = ds1.Tables[0];
            this.ddlGroupDesc.DataBind();
            this.ddlGroupDesc.SelectedValue = "000000000000";
            ds1.Dispose();
        }
        protected void lnkOk_Click(object sender, EventArgs e)
        {
            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "ChqIsssued":
                    this.ShowChqIssued();
                    break;

                case "GroupWiseChqIssued":
                    this.ShowgrpwiseChqIssued();
                    break;

                case "DateWiseChqIssued":
                    this.ShowDateWiseChqIssued();
                    this.lblPage.Visible = true;
                    this.ddlpagesize.Visible = true;
                    break;

                case "PayStatusResource":
                    this.ShowPayStatusResource();
                    //this.lblPage.Visible = true;
                    //this.ddlpagesize.Visible = true;
                    break;
                case "PVSegment":
                
                    this.ShowDateWisePv();
                    this.lblPage.Visible = true;
                    this.ddlpagesize.Visible = true;
                    break;

                    



            }



        }


        private void ShowChqIssued()
        {
            try
            {
                Session.Remove("tblpenchq");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
                string voutype = "PV%";

                string BankName = ((this.ddlBankName.SelectedValue.ToString() == "000000000000") ? "" : this.ddlBankName.SelectedValue.ToString()) + "%";
                DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "REPORTPERPOSDATECHQ", frmdate, todate, voutype, "", "", "", BankName, "", "");
                if (ds1 == null)
                {
                    this.dgv1.DataSource = null;
                    this.dgv1.DataBind();
                    return;
                }
                Session["tblpenchq"] = this.HiddenSameDate(ds1.Tables[0]);
                this.Data_Bind();
            }
            catch (Exception ex)
            {

            }

        }

        private void ShowgrpwiseChqIssued()
        {

            try
            {
                Session.Remove("tblpenchq");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
                string voutype = "PV%";

                string GroupCode = ((this.ddlGroupDesc.SelectedValue.ToString() == "000000000000") ? "" : this.ddlGroupDesc.SelectedValue.ToString().Substring(0, 2)) + "%";
                string BankName = ((this.ddlBankName.SelectedValue.ToString() == "000000000000") ? "" : this.ddlBankName.SelectedValue.ToString()) + "%";
                DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PAYMENT", "RPTGROUPWISEPDCHEQUE", frmdate, todate, voutype, GroupCode, BankName, "", "", "", "");
                if (ds1 == null)
                {
                    this.gvgrpchqissued.DataSource = null;
                    this.gvgrpchqissued.DataBind();
                    return;
                }
                Session["tblpenchq"] = ds1.Tables[0];
                this.Data_Bind();



            }
            catch (Exception ex)
            {

            }




        }

        private void ShowDateWisePv()
        {
            try
            {
                Session.Remove("tblpenchq");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
                string voutype = "PV%";
                double frmrange = (Convert.ToDouble("0" + this.txtfrmrange.Text.ToString())); // Convert.ToDouble(txtfrmsalary.Text);
                double torange = Convert.ToDouble("0" + this.txttorange.Text.ToString()) == 0 ? 2000000000000 : Convert.ToDouble(txttorange.Text);
                string BankName = ((this.ddlBankName.SelectedValue.ToString() == "000000000000") ? "" : this.ddlBankName.SelectedValue.ToString()) + "%";
                string range = this.ddlRange.SelectedValue.ToString();
                string chkrange = this.chkrange.Checked ? "Range" : "";

                DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", "REPORTPVWISEVOURANGE", frmdate, todate, voutype, BankName, chkrange, range, frmrange.ToString(), torange.ToString(), "","");
                if (ds1 == null)
                {
                    this.dgv5.DataSource = null;
                    this.dgv5.DataBind();
                    return;
                }

                Session["tblpenchq"] = ds1.Tables[0];
                // Session["tblpenchq"] = this.HiddenSameDate(ds1.Tables[0]);
                this.Data_Bind();



            }

            catch (Exception ex)
            {

            }

        }


        private void ShowDateWiseChqIssued()
        {

            try
            {
                Session.Remove("tblpenchq");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
                string voutype = "PV%";
                double frmrange = (Convert.ToDouble("0" + this.txtfrmrange.Text.ToString())); // Convert.ToDouble(txtfrmsalary.Text);
                double torange = Convert.ToDouble("0" + this.txttorange.Text.ToString()) == 0 ? 2000000000000 : Convert.ToDouble(txttorange.Text);
                string BankName = ((this.ddlBankName.SelectedValue.ToString() == "000000000000") ? "" : this.ddlBankName.SelectedValue.ToString()) + "%";
                string onlypdc = chkPdc.Checked ? "Pdc" : "";
                string honoured = chkhonour.Checked ? "Honoured" : "";

                DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", "REPORTCHEQUEISSUEWISE", frmdate, todate, voutype, "", "", "", BankName, frmrange.ToString(), torange.ToString(), onlypdc, honoured);
                if (ds1 == null)
                {
                    this.dgv2.DataSource = null;
                    this.dgv2.DataBind();
                    return;
                }

                Session["tblpenchq"] = ds1.Tables[0];
                // Session["tblpenchq"] = this.HiddenSameDate(ds1.Tables[0]);
                this.Data_Bind();



            }

            catch (Exception ex)
            {

            }

        }





        private void ShowPayStatusResource()
        {

            try
            {
                Session.Remove("tblpenchq");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
               // string BankName = ((this.ddlBankName.SelectedValue.ToString() == "000000000000") ? "" : this.ddlBankName.SelectedValue.ToString()) + "%";
               string BankName = this.ddlBankName.SelectedValue.ToString() + "%";
                DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", "REPORTMONWISEPAYMENTBANKWISE", frmdate, todate, BankName, "", "", "", "", "", "", "");
                if (ds1 == null)
                {
                    this.dgv4.DataSource = null;
                    this.dgv4.DataBind();
                    return;
                }

                Session["tblpenchq"] = ds1.Tables[0];
                // Session["tblpenchq"] = this.HiddenSameDate(ds1.Tables[0]);
                this.Data_Bind();



            }

            catch (Exception ex)
            {

            }

        }


        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblpenchq"];
            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "ChqIsssued":
                    this.dgv1.DataSource = dt;
                    this.dgv1.DataBind();
                    Session["Report1"] = dgv1;
                    if (dt.Rows.Count > 0)
                        ((HyperLink)this.dgv1.HeaderRow.FindControl("hlbtnbtbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    this.CalculatrGridTotal();
                    break;

                case "GroupWiseChqIssued":
                    this.gvgrpchqissued.DataSource = dt;
                    this.gvgrpchqissued.DataBind();
                    Session["Report1"] = gvgrpchqissued;
                    if (dt.Rows.Count > 0)
                        ((HyperLink)this.gvgrpchqissued.HeaderRow.FindControl("hlbtnbtbCdataExelgp")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    this.CalculatrGridTotal();
                    break;


                case "DateWiseChqIssued":
                    this.dgv2.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.dgv2.DataSource = dt;
                    this.dgv2.DataBind();
                    //Session["Report1"] = dgv2;
                    //if (dt.Rows.Count > 0)
                    //((HyperLink)this.dgv2.HeaderRow.FindControl("hlbtnbtbCdateWiseExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    this.CalculatrGridTotal();
                    break;


                case "PayStatusResource":
                    this.dgv4.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.dgv4.DataSource = dt;
                    this.dgv4.DataBind();
                    //Session["Report1"] = dgv2;
                    //if (dt.Rows.Count > 0)
                    //((HyperLink)this.dgv2.HeaderRow.FindControl("hlbtnbtbCdateWiseExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    this.CalculatrGridTotal();
                    break;


                case "PVSegment":
                    this.dgv5.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.dgv5.DataSource = dt;
                    this.dgv5.DataBind();
                    //Session["Report1"] = dgv2;
                    //if (dt.Rows.Count > 0)
                    //((HyperLink)this.dgv2.HeaderRow.FindControl("hlbtnbtbCdateWiseExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    this.CalculatrGridTotal();
                    break;
            }








        }

        private DataTable HiddenSameDate(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;
            string grp = dt1.Rows[0]["grp"].ToString();
            string pactcode = dt1.Rows[0]["actcode"].ToString();
            string cactcode = dt1.Rows[0]["cactcode"].ToString();
            int j;
            for (j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["grp"].ToString() == grp)
                {
                    grp = dt1.Rows[j]["grp"].ToString();
                    dt1.Rows[j]["grpdesc"] = "";
                }

                else
                {
                    grp = dt1.Rows[j]["grp"].ToString();

                }

            }



            for (j = 1; j < dt1.Rows.Count; j++)
            {
                if ((dt1.Rows[j]["actcode"].ToString() == pactcode) && (dt1.Rows[j]["cactcode"].ToString() == cactcode))
                {
                    pactcode = dt1.Rows[j]["actcode"].ToString();
                    cactcode = dt1.Rows[j]["cactcode"].ToString();
                    dt1.Rows[j]["actdesc"] = "";
                    dt1.Rows[j]["cactdesc"] = "";
                }

                else
                {
                    if (dt1.Rows[j]["actcode"].ToString() == pactcode)
                        dt1.Rows[j]["actdesc"] = "";
                    if (dt1.Rows[j]["cactcode"].ToString() == cactcode)
                        dt1.Rows[j]["cactdesc"] = "";
                    pactcode = dt1.Rows[j]["actcode"].ToString();
                    cactcode = dt1.Rows[j]["cactcode"].ToString();
                }

            }
            return dt1;


            //grpcode = dt1.Rows[0]["grpcode"].ToString();
            //            string actcode = dt1.Rows[0]["actcode"].ToString();
            //            for (j = 1; j < dt1.Rows.Count; j++)
            //            {
            //                if (dt1.Rows[j]["grpcode"].ToString() == grpcode && dt1.Rows[j]["actcode"].ToString() == actcode)
            //                {
            //                    grpcode = dt1.Rows[j]["grpcode"].ToString();
            //                    actcode = dt1.Rows[j]["actcode"].ToString();
            //                    dt1.Rows[j]["grpdesc"] = "";
            //                    dt1.Rows[j]["actdesc"] = "";

            //                }

            //                else
            //                {


            //                    if (dt1.Rows[j]["grpcode"].ToString() == grpcode)
            //                    {
            //                        dt1.Rows[j]["grpdesc"] = "";
            //                    }
            //                    if (dt1.Rows[j]["actcode"].ToString() == actcode)
            //                    {
            //                        dt1.Rows[j]["actdesc"] = "";
            //                    }

            //                    grpcode = dt1.Rows[j]["grpcode"].ToString();
            //                    actcode = dt1.Rows[j]["actcode"].ToString();

            //                }

            //            }
            //        break;

            //}


            //  return dt1;



        }
        protected void CalculatrGridTotal()
        {

            DataTable dt = (DataTable)Session["tblpenchq"];
            DataTable dt1 = dt.Copy();
            DataView dv = new DataView();
            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "ChqIsssued":


                    dv = dt1.DefaultView;
                    dv.RowFilter = ("typesum='ZZZZ'");
                    dt1 = dv.ToTable();
                    ((Label)this.dgv1.FooterRow.FindControl("lgvFCrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(cramt)", "")) ? 0 : dt1.Compute("sum(cramt)", ""))).ToString("#,##0;-#,##0; ");
                    break;

                case "GroupWiseChqIssued":
                    dv = dt1.DefaultView;
                    dv.RowFilter = ("typesum='TTTT'");
                    dt1 = dv.ToTable();
                    ((Label)this.gvgrpchqissued.FooterRow.FindControl("lgvFpayam")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(payam)", "")) ? 0 : dt1.Compute("sum(payam)", ""))).ToString("#,##0;-#,##0; ");
                    break;

                case "DateWiseChqIssued":


                    //dv = dt1.DefaultView;
                    //dv.RowFilter = ("typesum='ZZZZ'");
                    //dt1 = dv.ToTable();
                    ((Label)this.dgv2.FooterRow.FindControl("lgvDatFCrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(cramt)", "")) ? 0 : dt1.Compute("sum(cramt)", ""))).ToString("#,##0;-#,##0; ");
                    Session["Report1"] = dgv2;
                    //if (dt.Rows.Count > 0)
                    ((HyperLink)this.dgv2.HeaderRow.FindControl("hlbtnbtbCdateWiseExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    break;

                case "PayStatusResource":
                    ((Label)this.dgv4.FooterRow.FindControl("lblgvFamt4")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(trnam)", "")) ? 0 : dt1.Compute("sum(trnam)", ""))).ToString("#,##0.00;#,##0.00; ");
                   // Session["Report1"] = dgv2;
                    //if (dt.Rows.Count > 0)
                    //((HyperLink)this.dgv2.HeaderRow.FindControl("hlbtnbtbCdateWiseExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    break;
               
                case "PVSegment":

                    if (dt1.Rows.Count == 0)
                        return;
                    ((Label)this.dgv5.FooterRow.FindControl("lgvDatFCrAmtp")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(cramt)", "")) ? 0 : dt1.Compute("sum(cramt)", ""))).ToString("#,##0.00;#,##0.00; ");
                    Session["Report1"] = dgv5;
                    if (dt.Rows.Count > 0)
                    ((HyperLink)this.dgv5.HeaderRow.FindControl("hlbtnbtbCdateWiseExelp")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    break;

                    
            }



        }


        protected void lnkPrint_Click(object sender, EventArgs e)
        {


            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "ChqIsssued":
                    this.PrintChequeIssued();

                    break;

                case "GroupWiseChqIssued":
                    this.PrintChequeIssuedGrpWise();
                    break;

                case "DateWiseChqIssued":
                    this.PrintDateWiseIssued();
                    break;
            }




        }

        private void PrintDateWiseIssued()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblpenchq"];

            ReportDocument rptsale = new RealERPRPT.R_17_Acc.rptDatWiseIssue();
            //TextObject rptCname = rptsale.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //rptCname.Text = comnam;
            TextObject rptDate = rptsale.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            rptDate.Text = "From : " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy") + " To:" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            TextObject txtuserinfo = rptsale.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptsale.SetDataSource(dt);
            Session["Report1"] = rptsale;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintChequeIssued()
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
            DataTable dt = (DataTable)Session["tblpenchq"];
            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.DayWiseissueCheek>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptPostDatCheque", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("Date", "From : " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy") + " To:" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "List of Post Dated Cheque"));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        private void PrintChequeIssuedGrpWise()
        {

            //Iqbal  Nayan
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
            DataTable dt = (DataTable)Session["tblpenchq"];
            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.DaywiseGpIssue>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptChqIssuedGrpWise", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("Date", "From : " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy") + " To:" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Cheque Issued - Group Wise"));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt = (DataTable)Session["tblpenchq"];
            //ReportDocument rptsale = new RealERPRPT.R_17_Acc.RptChqIssuedGrpWise();
            //TextObject rptCname = rptsale.ReportDefinition.ReportObjects["txtcompanyname"] as TextObject;
            //rptCname.Text = comnam;
            //TextObject rptDate = rptsale.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //rptDate.Text = "From : " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy") + " To:" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            //TextObject txtGranTotal = rptsale.ReportDefinition.ReportObjects["txtGranTotal"] as TextObject;
            //txtGranTotal.Text = ((Label)this.gvgrpchqissued.FooterRow.FindControl("lgvFpayam")).Text;
            //TextObject txtuserinfo = rptsale.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptsale.SetDataSource(dt);
            //Session["Report1"] = rptsale;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void dgv1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label prodesc = (Label)e.Row.FindControl("lgactdesc");
                Label amt = (Label)e.Row.FindControl("lgvcramt");
                //Label sign = (Label)e.Row.FindControl("gvsign");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "typesum")).ToString().Trim();


                if (code == "")
                {
                    return;
                }

                else if (ASTUtility.Right(code, 1) == "Z")
                {
                    prodesc.Font.Bold = true;
                    amt.Font.Bold = true;
                    //sign.Font.Bold = true;
                    prodesc.Style.Add("text-align", "right");

                }


            }
        }


        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }

        protected void dgv2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.dgv2.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void imgbtnSrchGroup_Click(object sender, EventArgs e)
        {
            this.GetGroupName();
        }
        protected void gvgrpchqissued_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label resdesc = (Label)e.Row.FindControl("lgvresdescgp");
                Label amt = (Label)e.Row.FindControl("lgvpayam");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "typesum")).ToString().Trim();


                if (code == "")
                {
                    return;
                }

                else if (code == "TTTT")
                {
                    resdesc.Font.Bold = true;
                    amt.Font.Bold = true;
                    //sign.Font.Bold = true;
                    resdesc.Style.Add("text-align", "right");

                }


            }
        }

        protected void dgv5_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.Data_Bind();
        }

        protected void dgv5_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink = (HyperLink)e.Row.FindControl("HLgvvounum");
           
                string vounum = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "vounum")).ToString();

                if (vounum == "")
                {
                    return;
                }


                if (vounum.Trim().Length == 14)
                {
                    if (ASTUtility.Left(vounum, 2) == "PV")
                    {
                        hlink.NavigateUrl = "RptAccVouher02.aspx?vounum=" + vounum;
                        hlink.Text = vounum.Substring(0, 2) + vounum.Substring(6, 2) + "-" + vounum.Substring(8, 6);
                      
                    }

                }



            }




        }

        protected void chkhonour_CheckedChanged(object sender, EventArgs e)
        {
            if (chkhonour.Checked)
            {
                chkPdc.Checked = false;
            }

        }

        protected void chkPdc_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPdc.Checked)
            {
                chkhonour.Checked = false;
            }

            

        }
    }
}


