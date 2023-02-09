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
using Microsoft.Reporting.WinForms;
namespace RealERPWEB.F_12_Inv
{
    public partial class PurInterComMatTransfer : System.Web.UI.Page
    {
        public static double TAmount;
        ProcessAccess accData = new ProcessAccess();
        static string prevPage = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((Label)this.Master.FindControl("lblprintstk")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "MATERIAL TRANSFER INFORMATION";
                Hashtable hst = (Hashtable)Session["tblLogin"];
                this.lblFromCmpName.Text = hst["comnam"].ToString();
                this.txtfdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txttdate.Text = this.txtfdate.Text;
                this.rbtnList1.SelectedIndex = 0;
                this.CompanyPost();
                this.GetHeadAccout();
                this.GetToCompany();
                this.GetToHeadAccount();
                this.GetfVoucherNo();
                this.GettVoucherNo();
                this.TableCreate();
                this.TableCreateTo();

                //prevPage = Request.UrlReferrer.ToString();
            }

        }


        private void CompanyPost()
        {
            string comcod = this.GetComcode();

            switch (comcod)
            {
                // case "3101": // Test
                case "3332":
                case "3339":
                    this.chkpost.Checked = true;
                    break;

                default:
                    this.chkpost.Checked = false;
                    break;
            }


        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private string GetComcode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        private void GetHeadAccout()  // accountcode
        {


            string comcod = this.GetComcode();
            string filter = "%%";// this.txtsercacc.Text + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETHEADACCOUNT", filter, "", "", "", "", "", "", "", "");
            this.ddlAccHead.DataSource = ds1.Tables[0];
            this.ddlAccHead.DataTextField = "actdesc";
            this.ddlAccHead.DataValueField = "actcode";
            this.ddlAccHead.DataBind();

        }




        private void GetToCompany()
        {

            string comcod = this.GetComcode();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETTOCOMPANY", "", "", "", "", "", "", "", "", "");
            this.ddlToCompany.DataSource = ds1.Tables[0];
            this.ddlToCompany.DataTextField = "comnam";
            this.ddlToCompany.DataValueField = "comcod";
            this.ddlToCompany.DataBind();


        }

        private void GetToHeadAccount()
        {
            string comcod = this.ddlToCompany.SelectedValue.ToString();
            string filter = this.txtsertoheacc.Text + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETHEADACCOUNT", filter, "", "", "", "", "", "", "", "");
            this.ddlAcctoHead.DataSource = ds1.Tables[0];
            this.ddlAcctoHead.DataTextField = "actdesc";
            this.ddlAcctoHead.DataValueField = "actcode";
            this.ddlAcctoHead.DataBind();

        }

        private void GetProjectlist()
        {

            string comcod = this.GetComcode();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GetProjectFromList", "%%", "", "", "", "", "", "", "", "");
            Session["projectlist"] = ds1.Tables[0];
            if (ds1 == null)
                return;

            this.ddlprjlistfrom.DataTextField = "actdesc1";
            this.ddlprjlistfrom.DataValueField = "actcode";
            this.ddlprjlistfrom.DataSource = ds1.Tables[0];
            this.ddlprjlistfrom.DataBind();
            this.Resourcelist();
        }


        private void Resourcelist()
        {
            string comcod = this.GetComcode();
            string ProjectCode = this.ddlprjlistfrom.SelectedValue.ToString().Trim();
            string FindResDesc = this.txtSearchRes.Text.Trim() + "%";
            string curdate = this.txtfdate.Text.ToString().Trim();
            string balcon = "";

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GetProjResList", ProjectCode, curdate, FindResDesc, balcon, "", "", "", "", "");
            Session["projectreslist"] = ds1.Tables[0];
            ViewState["tblspcf"] = ds1.Tables[1];

            if (ds1 == null)
                return;
            if (ds1.Tables[0].Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Materials are not available for Store');", true);
                return;
            }


            DataView dv = ds1.Tables[0].DefaultView;
            dv.Sort = "rsircode";
            DataTable dt = dv.ToTable(true, "rsircode", "resdesc");
            this.ddlreslist.DataTextField = "resdesc";
            this.ddlreslist.DataValueField = "rsircode";
            this.ddlreslist.DataSource = dt;
            this.ddlreslist.DataBind();
            ds1.Dispose();
            this.GetSpecification();
        }

        private void GetSpecification()
        {
            string mResCode = this.ddlreslist.SelectedValue.ToString().Substring(0, 9);
            //string spcfcod1 = this.ddlResSpcf.SelectedValue.ToString();
            this.ddlResSpcf.Items.Clear();
            DataTable tbl1 = (DataTable)ViewState["tblspcf"];
            DataView dv1 = tbl1.DefaultView;
            dv1.RowFilter = "mspcfcod = '" + mResCode + "' or spcfcod = '000000000000'";
            DataTable dt = dv1.ToTable();
            this.ddlResSpcf.DataTextField = "spcfdesc";
            this.ddlResSpcf.DataValueField = "spcfcod";
            this.ddlResSpcf.DataSource = dt;
            this.ddlResSpcf.DataBind();


        }
        protected void ibtnFindfrmproject_Click(object sender, EventArgs e)
        {
            this.GetProjectlist();
        }
        protected void ddlprjlistfrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Resourcelist();
            this.GetfVoucherNo();
        }
        protected void ddlreslist_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSpecification();
        }
        private void GetfVoucherNo()
        {

            try
            {
                string comcod = this.GetComcode();

                DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETOPENINGDATE", "", "", "", "", "", "", "", "", "");
                if (ds2.Tables[0].Rows.Count == 0)
                {
                    return;

                }

                DateTime txtopndate = Convert.ToDateTime(ds2.Tables[0].Rows[0]["voudat"]);

                if (txtopndate > Convert.ToDateTime(ASTUtility.DateFormat(this.txtfdate.Text.Trim())))
                {
                    // ((Label)this.Master.FindControl("lblmsg")).Text = "Voucher Date Must  Be Greater then Opening Date";
                    return;

                }
                string VNo = "JV";
                string entrydate = ASTUtility.DateFormat(this.txtfdate.Text.Trim());
                DataSet ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETNEWVOUCHER", entrydate, VNo, "", "", "", "", "", "", "");
                DataTable dt4 = ds4.Tables[0];
                this.lblfVoucherNo.Text = dt4.Rows[0]["couvounum"].ToString().Substring(0, 2) + dt4.Rows[0]["couvounum"].ToString().Substring(6, 2) + '-' + dt4.Rows[0]["couvounum"].ToString().Substring(8);

            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + ex.Message + "');", true);

            }






        }
        private void GettVoucherNo()
        {



            try
            {
                string comcod = this.ddlToCompany.SelectedValue.ToString();

                DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETOPENINGDATE", "", "", "", "", "", "", "", "", "");
                if (ds2.Tables[0].Rows.Count == 0)
                {
                    return;

                }

                DateTime txtopndate = Convert.ToDateTime(ds2.Tables[0].Rows[0]["voudat"]);

                if (txtopndate > Convert.ToDateTime(ASTUtility.DateFormat(this.txttdate.Text.Trim())))
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Voucher Date Must  Be Greater then Opening Date";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;

                }
                string VNo = "JV";
                string entrydate = ASTUtility.DateFormat(this.txttdate.Text.Trim());
                DataSet ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETNEWVOUCHER", entrydate, VNo, "", "", "", "", "", "", "");
                DataTable dt4 = ds4.Tables[0];

                this.lbltVoucherNo.Text = dt4.Rows[0]["couvounum"].ToString().Substring(0, 2) + dt4.Rows[0]["couvounum"].ToString().Substring(6, 2) + '-' + dt4.Rows[0]["couvounum"].ToString().Substring(8);

            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + ex.Message + "');", true);

            }

        }


        private string CompanyPrintVou()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string vouprint = "";
            switch (comcod)
            {

                case "2305":
                    vouprint = "VocherPrint4";
                    break;

                case "3306":
                case "3307":
                case "3308":
                    vouprint = "VocherPrint1";
                    break;
                case "3305":
                case "3310":
                case "3311":
                    vouprint = "VocherPrint2";
                    break;
                case "3309":
                    vouprint = "VocherPrint3";
                    break;
                case "3101":
                case "3315":
                case "3316":
                case "3317":
                    vouprint = "VocherPrint5";
                    break;
                default:
                    vouprint = "VocherPrint";
                    break;
            }
            return vouprint;
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = (this.rbtnList1.SelectedIndex == 0) ? hst["comcod"].ToString() : this.ddlToCompany.SelectedValue.ToString();
                string comnam = (this.rbtnList1.SelectedIndex == 0) ? hst["comnam"].ToString() : this.ddlToCompany.SelectedItem.Text;
                string comadd = hst["comadd1"].ToString();
                string combranch = hst["combranch"].ToString();
                string compname = hst["compname"].ToString();
                string username = hst["username"].ToString();
                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
                string voudat = Convert.ToDateTime(this.txtfdate.Text.Trim()).ToString("dd-MMM-yyyy");
                string vounum = (this.rbtnList1.SelectedIndex == 0) ? (this.lblfVoucherNo.Text.Trim().Substring(0, 2) + voudat.Substring(7, 4) +
                    this.lblfVoucherNo.Text.Trim().Substring(2, 2) + this.lblfVoucherNo.Text.Trim().Substring(5)) : (this.lbltVoucherNo.Text.Trim().Substring(0, 2) + voudat.Substring(7, 4) +
                                this.lbltVoucherNo.Text.Trim().Substring(2, 2) + this.lbltVoucherNo.Text.Trim().Substring(5));

                DataSet _ReportDataSet = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", "PRINTVOUCHER01", vounum, "", "", "", "", "", "", "", "");
                if (_ReportDataSet == null)
                    return;
                DataTable dt = _ReportDataSet.Tables[0];
                if (dt.Rows.Count == 0)
                    return;
                double dramt, cramt;
                dramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(Dr)", "")) ? 0.00 : dt.Compute("sum(Dr)", "")));
                cramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(Cr)", "")) ? 0.00 : dt.Compute("sum(Cr)", "")));



                if (dramt > 0 && cramt > 0)
                {
                    TAmount = cramt;

                }
                else if (dramt > 0 && cramt <= 0)
                {
                    TAmount = dramt;
                }
                else
                {
                    TAmount = cramt;
                }

                DataTable dt1 = _ReportDataSet.Tables[1];
                string Vounum = dt1.Rows[0]["vounum"].ToString();
                //string voudat = Convert.ToDateTime(dt1.Rows[0]["voudat"]).ToString("dd-MMM-yyyy");
                string refnum = dt1.Rows[0]["refnum"].ToString();
                string voutype = dt1.Rows[0]["voutyp"].ToString();
                string venar = dt1.Rows[0]["venar"].ToString();
                string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
                string Posteddat = Convert.ToDateTime(dt1.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy");
                string Type = this.CompanyPrintVou();

                LocalReport Rpt1 = new LocalReport();

                //ReportDocument rptinfo = new ReportDocument();

                if (Type == "VocherPrint")
                {
                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucherDefault", list, null, null);
                    Rpt1.EnableExternalImages = true;

                    //rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher();
                }
                else if (Type == "VocherPrint1")
                {

                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucher1", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("txtissuno", "Issue No: "));
                    Rpt1.SetParameters(new ReportParameter("txtComBranch", (combranch.Length > 0) ? ("Unit: " + combranch) : ""));
                    Rpt1.SetParameters(new ReportParameter("entrydate1", "Entry Date: " + Posteddat));


                    //rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher1();
                    //TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                    //txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
                    //TextObject txtisunum1 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
                    //txtisunum1.Text = "Issue No: ";
                    //TextObject txtPosteddate1 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
                    //txtPosteddate1.Text = "Entry Date: " + Posteddat;

                }
                else if (Type == "VocherPrint2")
                {
                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucher2", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("txtissuno", "Issue No: "));
                    Rpt1.SetParameters(new ReportParameter("txtComBranch", (combranch.Length > 0) ? ("Unit: " + combranch) : ""));
                    Rpt1.SetParameters(new ReportParameter("entrydate1", "Entry Date: " + Posteddat));

                    //rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher2();
                    //TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                    //txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
                    //TextObject txtisunum2 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
                    //txtisunum2.Text = "Issue No: ";
                    //TextObject txtPosteddate2 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
                    //txtPosteddate2.Text = "Entry Date: " + Posteddat;

                }
                else if (Type == "VocherPrint3")
                {
                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucher3", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("txtissuno", "Issue No: "));
                    Rpt1.SetParameters(new ReportParameter("txtComBranch", (combranch.Length > 0) ? ("Unit: " + combranch) : ""));
                    Rpt1.SetParameters(new ReportParameter("entrydate1", "Entry Date: " + Posteddat));


                    //rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher3();
                    //TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                    //txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
                    //TextObject txtisunum1 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
                    //txtisunum1.Text = "Issue No: ";
                    //TextObject txtPosteddate1 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
                    //txtPosteddate1.Text = "Entry Date: " + Posteddat;

                }
                else if (Type == "VocherPrint5")
                {
                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucher5", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("txtissuno", "Issue No: "));
                    Rpt1.SetParameters(new ReportParameter("txtComBranch", (combranch.Length > 0) ? ("Unit: " + combranch) : ""));
                    Rpt1.SetParameters(new ReportParameter("entrydate1", "Entry Date: " + Posteddat));


                    //rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher5();
                    //TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                    //txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
                    //TextObject txtisunum1 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
                    //txtisunum1.Text = "Issue No: ";
                    //TextObject txtPosteddate1 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
                    //txtPosteddate1.Text = "Entry Date: " + Posteddat;

                }
                else
                {

                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucher4", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("txtissuno", "Issue No: "));
                    Rpt1.SetParameters(new ReportParameter("txtComBranch", (combranch.Length > 0) ? ("Unit: " + combranch) : ""));
                    Rpt1.SetParameters(new ReportParameter("entrydate1", "Entry Date: " + Posteddat));


                    //rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher4();
                    //TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                    //txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
                    //TextObject txtisunum3 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
                    //txtisunum3.Text = "Issue No: ";
                    //TextObject txtPosteddate3 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
                    //txtPosteddate3.Text = "Entry Date: " + Posteddat;
                }



                Rpt1.SetParameters(new ReportParameter("comnam", comnam));
                Rpt1.SetParameters(new ReportParameter("comadd", comadd));
                Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                Rpt1.SetParameters(new ReportParameter("txtPartyName", ""));
                Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                Rpt1.SetParameters(new ReportParameter("refnum", "Cheque/Ref. No.: " + refnum));
                Rpt1.SetParameters(new ReportParameter("InWrd", ASTUtility.Trans(Math.Round(TAmount), 2)));
                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
                Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));

                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";



                //rptinfo.SetDataSource(_ReportDataSet.Tables[0]);
                //TextObject txtCompanyName = rptinfo.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
                //txtCompanyName.Text = comnam;
                //TextObject txtcAdd = rptinfo.ReportDefinition.ReportObjects["compadd"] as TextObject;
                //txtcAdd.Text = comadd;
                //TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //vounum1.Text = "Voucher No.: " + vounum;
                //TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //date.Text = "Voucher Date: " + voudat;
                //TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                //Refnum.Text = "Cheque/Ref. No.: " + refnum;
                //TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                //rpttxtPartyName.Text = "";
                //TextObject voutype1 = rptinfo.ReportDefinition.ReportObjects["voutype"] as TextObject;
                //voutype1.Text = voutype;
                //TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //naration.Text = "Narration: " + venar;

                ////TextObject txtBname = rptinfo.ReportDefinition.ReportObjects["bankname"] as TextObject;
                ////txtBname.Text = this.ddlConAccHead.SelectedItem.Text.Substring(13);
                //TextObject rpttxtamt = rptinfo.ReportDefinition.ReportObjects["txtamt"] as TextObject;
                //rpttxtamt.Text = ASTUtility.Trans(Math.Round(TAmount), 2);

                //TextObject txtuserinfo = rptinfo.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);


                ////string comcod = this.GetComeCode();
                ////string comcod = hst["comcod"].ToString();
                //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                //rptinfo.SetParameterValue("ComLogo", ComLogo);
                //Session["Report1"] = rptinfo;
                //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                //              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }




        protected void imgbtnFindAccount_Click(object sender, EventArgs e)
        {
            this.GetHeadAccout();
        }

        protected void ddlConAccHead_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetfVoucherNo();
        }




        protected void imgbtnFindtoAccount_Click(object sender, EventArgs e)
        {
            this.GetToHeadAccount();
        }
        protected void ddlToCompany_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.GetToHeadAccount();
            this.GettVoucherNo();

        }
        protected void ddlContAccHead_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetToHeadAccount();
        }
        protected void lbtnRefresh_Click(object sender, EventArgs e)
        {

            //this.txttdate.Text = this.txtfdate.Text;
            //this.lbltRefNum.Text = this.txtRefNum.Text;
            //this.lbltcramt.Text = Convert.ToDouble(this.txtDrAmt.Text).ToString("#,##0.00;(#,##0.00); ");
            //this.txtDrAmt.Text = Convert.ToDouble(this.txtDrAmt.Text).ToString("#,##0.00;(#,##0.00); ");
            this.txtqtyto.Text = this.txtfqty.Text;
            this.txttdate.Text = this.txtfdate.Text;
            this.lbltRefNum.Text = this.txtRefNum.Text;
            this.txttSrinfo.Text = this.txtSrinfo.Text;
            this.txttNarration.Text = this.txtNarration.Text;

            this.GetfVoucherNo();
            this.GettVoucherNo();
        }

        private string GetIssueNo()
        {
            string comcod = this.GetComcode();
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "GETISSUENO", "", "", "", "", "", "", "", "", "");
            return ds2.Tables[0].Rows[0]["isunum"].ToString();
        }


        private string GettoIssueNo()
        {
            string comcod = this.ddlToCompany.SelectedValue.ToString();
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "GETISSUENO", "", "", "", "", "", "", "", "", "");
            return ds2.Tables[0].Rows[0]["isunum"].ToString();
        }





        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {



            ((Label)this.Master.FindControl("lblmsg")).Visible = true;

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            // this.lbtnRefresh_Click(null, null);
            DataTable dt = (DataTable)ViewState["tblpmattrans"];
            DataTable dt1 = (DataTable)ViewState["tblpmattransto"];
            Hashtable hst = (Hashtable)Session["tblLogin"];

            //string userid = hst["usrid"].ToString();
            //string Terminal = hst["compname"].ToString();
            //string Sessionid = hst["session"].ToString();

            string PostedByid = hst["usrid"].ToString();
            string Posttrmid = hst["compname"].ToString();
            string PostSession = hst["session"].ToString();
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

            string EditByid = "";
            string Editdat = "01-Jan-1900";
            string recndt = "01-jan-1900";
            string rpcode = "";
            string billno = "";
            string comcod = this.GetComcode();
            this.GetfVoucherNo();
            this.GettVoucherNo();
            string voudat = Convert.ToDateTime(this.txtfdate.Text).ToString("dd-MMM-yyyy");
            string vounum1 = this.lblfVoucherNo.Text.Trim().Substring(0, 2) + voudat.Substring(7, 4) +
                            this.lblfVoucherNo.Text.Trim().Substring(2, 2) + this.lblfVoucherNo.Text.Trim().Substring(5);
            string voudat2 = Convert.ToDateTime(this.txttdate.Text).ToString("dd-MMM-yyyy");
            string vounum2 = this.lbltVoucherNo.Text.Trim().Substring(0, 2) + voudat2.Substring(7, 4) +
                           this.lbltVoucherNo.Text.Trim().Substring(2, 2) + this.lbltVoucherNo.Text.Trim().Substring(5);


            string refnum = this.txtRefNum.Text.Trim();


            string srinfo1 = this.txtSrinfo.Text.Trim();
            string srinfo2 = this.txttSrinfo.Text.Trim();
            string vounarration1, vounarration2, vouno, voutype, cactcode = "000000000000";
            double trnamt;
            vounarration1 = this.txtNarration.Text.Trim();
            vounarration2 = (vounarration1.Length > 200 ? vounarration1.Substring(200) : "");
            vounarration1 = (vounarration1.Length > 200 ? vounarration1.Substring(0, 200) : vounarration1);
            vouno = this.lblfVoucherNo.Text.Trim().Substring(0, 2);
            voutype = "Journal Voucher";
            //cactcode = this.ddlConAccHead.SelectedValue.ToString();
            string vtcode = "99";
            string edit = "";
            string actcode = "";
            string rescode = "";
            string spclcode = "";
            string trnqty = "0";
            double Dramt, Cramt;
            // Dramt = Convert.ToDouble(this.txtDrAmt.Text.Trim());
            string trnremarks = "";
            bool resultb = false, resulta = false;

            try
            {
                if (refnum.Length == 0)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Ref./CheqNo. Should Not Be Empty";
                    return;
                }


                DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETREFNO", refnum, "", "", "", "", "", "", "", "");

                if (ds2.Tables[0].Rows.Count > 0)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Found Duplicate Ref./CheqNo !!!!";
                    return;
                }



                string pounaction = "";
                string aprovbyid = "";
                string aprvtrmid = "";
                string aprvseson = "";
                string aprvdat = "01-jan-1900";
                string Payto = "";
                string isunum = "";


                // Present Company

                string CallType = (this.chkpost.Checked) ? "ACVUPDATEUNPOSTED" : "ACVUPDATE02";

                //-----------Update Transaction B Table-----------------//
                resultb = accData.UpdateTransHREMPInfo3(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", CallType, vounum1, voudat, refnum, srinfo1, vounarration1,
                                vounarration2, voutype, vtcode, edit, PostedByid, Posttrmid, PostSession, Posteddat, EditByid, Editdat, Payto, isunum, aprovbyid, aprvtrmid, aprvseson, aprvdat, pounaction, "", "", "1", "");





                //resultb = accData.UpdateTransInfo2(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", vounum1, voudat, refnum, srinfo1, vounarration1,
                //               vounarration2, voutype, vtcode, edit, PostedByid, Postedtrmid, PostedSession, Posteddat, EditByid, Editdat, "", "", "1", "", "", "");



                if (!resultb)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }

                //-----------Update Transaction A Table-----------------//
                actcode = this.ddlprjlistfrom.SelectedValue.ToString();
                string sectcode = "000000000000";
                foreach (DataRow dr in dt.Rows)
                {


                    rescode = dr["rsircode"].ToString();
                    spclcode = dr["spcfcod"].ToString();
                    trnqty = dr["qty"].ToString();
                    trnamt = (Convert.ToDouble(dr["amt"].ToString()) * -1);


                    resulta = accData.UpdateTransHREMPInfo3(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", CallType, vounum1, actcode, rescode, cactcode,
                              voudat, trnqty, trnremarks, vtcode, trnamt.ToString(), spclcode, recndt, rpcode, billno, PostedByid, Posteddat, Posttrmid, sectcode, "", "", "", "", "", "", "", "", "");

                    //resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", vounum1, actcode, rescode, cactcode,
                    //            voudat, trnqty, trnremarks, vtcode, trnamt.ToString(), spclcode, recndt, rpcode, billno, "", "");
                    if (!resulta)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }

                }

                actcode = this.ddlAccHead.SelectedValue.ToString();
                rescode = "000000000000";
                spclcode = "000000000000";
                trnqty = "0";
                double dramt = Convert.ToDouble("0" + ((Label)this.grvacc.FooterRow.FindControl("lgvFAmount")).Text);

                //resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", vounum1, actcode, rescode, cactcode,
                //                voudat, trnqty, trnremarks, vtcode, dramt.ToString(), spclcode, recndt, rpcode, billno, "", "");

                resulta = accData.UpdateTransHREMPInfo3(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", CallType, vounum1, actcode, rescode, cactcode,
                             voudat, trnqty, trnremarks, vtcode, dramt.ToString(), spclcode, recndt, rpcode, billno, PostedByid, Posteddat, Posttrmid, sectcode, "", "", "", "", "", "", "", "", "");


                if (!resulta)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }




                //////////Update Transfer To

                comcod = this.ddlToCompany.SelectedValue.ToString();
                actcode = this.ddlprjlistto.SelectedValue.ToString();
                vounarration1 = this.txttNarration.Text.Trim();
                vounarration2 = (vounarration1.Length > 200 ? vounarration1.Substring(200) : "");
                vounarration1 = (vounarration1.Length > 200 ? vounarration1.Substring(0, 200) : vounarration1);
                vouno = this.lbltVoucherNo.Text.Trim().Substring(0, 2);


                resultb = accData.UpdateTransHREMPInfo3(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", CallType, vounum2, voudat2, refnum, srinfo2, vounarration1,
                             vounarration2, voutype, vtcode, edit, PostedByid, Posttrmid, PostSession, Posteddat, EditByid, Editdat, Payto, isunum, aprovbyid, aprvtrmid, aprvseson, aprvdat, pounaction, "", "", "1", "");



                //resultb = accData.UpdateTransInfo2(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", vounum2,  voudat2, refnum, srinfo2, vounarration1,
                //             vounarration2, voutype, vtcode, edit, PostedByid, Postedtrmid, PostedSession, Posteddat, EditByid, Editdat, "", "", "1", "", "", "");
                if (!resultb)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }

                //-----------Update Transaction A Table-----------------//



                foreach (DataRow dr in dt1.Rows)
                {


                    rescode = dr["rsircode"].ToString();
                    spclcode = dr["spcfcod"].ToString();
                    trnqty = dr["qty"].ToString();
                    trnamt = (Convert.ToDouble(dr["amt"].ToString()));


                    resulta = accData.UpdateTransHREMPInfo3(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", CallType, vounum2, actcode, rescode, cactcode,
                          voudat2, trnqty, trnremarks, vtcode, trnamt.ToString(), spclcode, recndt, rpcode, billno, PostedByid, Posteddat, Posttrmid, sectcode, "", "", "", "", "", "", "", "", "");

                    //resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", vounum2, actcode, rescode, cactcode,
                    //            voudat2, trnqty, trnremarks, vtcode, trnamt.ToString(), spclcode, recndt, rpcode, billno, "", "");
                    if (!resulta)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }

                }

                actcode = this.ddlAcctoHead.SelectedValue.ToString();
                rescode = "000000000000";
                spclcode = "000000000000";
                trnqty = "0";
                double cramt = Convert.ToDouble("0" + ((Label)this.grvacc.FooterRow.FindControl("lgvFAmount")).Text) * -1;

                resulta = accData.UpdateTransHREMPInfo3(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", CallType, vounum2, actcode, rescode, cactcode,
                        voudat2, trnqty, trnremarks, vtcode, cramt.ToString(), spclcode, recndt, rpcode, billno, PostedByid, Posteddat, Posttrmid, sectcode, "", "", "", "", "", "", "", "", "");


                //resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", vounum2, actcode, rescode, cactcode,
                //                voudat2, trnqty, trnremarks, vtcode, cramt.ToString(), spclcode, recndt, rpcode, billno, "", "");



                if (!resulta)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }







                ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully.";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

                this.lbtnRefresh.Enabled = false;
                this.lbtnUpdate.Enabled = false;
                this.txtfdate.Enabled = false;

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Inter Company Payment Voucher";
                string eventdesc = "Update Voucher";
                string eventdesc2 = vounum2;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }



        }


        protected void ImgbtnFindRes_Click(object sender, EventArgs e)
        {
            this.Resourcelist();
        }
        protected void ibtnFindNotify_Click(object sender, EventArgs e)
        {
            this.GetProjectlist();
        }


        private void SaveValue()
        {

            DataTable dt = (DataTable)ViewState["tblpmattrans"];
            int rowindex;
            for (int i = 0; i < this.grvacc.Rows.Count; i++)
            {
                double qty = Convert.ToDouble("0" + ((TextBox)this.grvacc.Rows[i].FindControl("txtqty")).Text.Trim());
                double rate = Convert.ToDouble("0" + ((TextBox)this.grvacc.Rows[i].FindControl("txtrate")).Text.Trim());
                double amt = qty * rate;


                rowindex = (grvacc.PageIndex) * grvacc.PageSize + i;
                dt.Rows[rowindex]["qty"] = qty;
                dt.Rows[rowindex]["rate"] = rate;
                dt.Rows[rowindex]["amt"] = amt;



            }
            ViewState["tblpmattrans"] = dt;

        }

        protected void lnktotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        protected void lnkselect_Click(object sender, EventArgs e)
        {

            string rescode = this.ddlreslist.SelectedValue.ToString().Trim();
            string spcfcod = this.ddlResSpcf.SelectedValue.ToString();
            DataTable dt = (DataTable)ViewState["tblpmattrans"];
            DataTable dt1 = (DataTable)Session["projectreslist"];
            DataRow[] dr1 = dt.Select("rsircode = '" + rescode + "' and spcfcod = '" + spcfcod + "'");
            double qty = Convert.ToDouble("0" + this.txtfqty.Text.Trim());
            // double rate = 0.00;
            if (dr1.Length == 0)
            {
                DataRow drforgrid = dt.NewRow();
                drforgrid["rsircode"] = rescode;
                drforgrid["rsirdesc"] = this.ddlreslist.SelectedItem.Text;
                drforgrid["spcfcod"] = spcfcod;
                drforgrid["spcfdesc"] = this.ddlResSpcf.SelectedItem.Text;
                DataRow[] dr = dt1.Select("rsircode = '" + rescode + "' and spcfcod = '" + spcfcod + "'");
                drforgrid["sirunit"] = (dt1.Select("rsircode = '" + rescode + "' and spcfcod = '" + spcfcod + "'"))[0]["sirunit"].ToString();
                drforgrid["qty"] = qty;
                drforgrid["rate"] = (dt1.Select("rsircode = '" + rescode + "' and spcfcod = '" + spcfcod + "'"))[0]["rate"].ToString();
                drforgrid["amt"] = qty * Convert.ToDouble((dt1.Select("rsircode = '" + rescode + "' and spcfcod = '" + spcfcod + "'"))[0]["rate"]);
                dt.Rows.Add(drforgrid);
            }

            else

            {
                dr1[0]["qty"] = qty;
                dr1[0]["rate"] = (dt1.Select("rsircode = '" + rescode + "' and spcfcod = '" + spcfcod + "'"))[0]["rate"].ToString();
                dr1[0]["amt"] = qty * Convert.ToDouble((dt1.Select("rsircode = '" + rescode + "' and spcfcod = '" + spcfcod + "'"))[0]["rate"]);
            }
            ViewState["tblpmattrans"] = dt;
            this.Data_Bind();

        }

        private void TableCreate()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("rsircode", Type.GetType("System.String"));
            dt.Columns.Add("spcfcod", Type.GetType("System.String"));
            dt.Columns.Add("rsirdesc", Type.GetType("System.String"));
            dt.Columns.Add("sirunit", Type.GetType("System.String"));
            dt.Columns.Add("spcfdesc", Type.GetType("System.String"));
            dt.Columns.Add("qty", Type.GetType("System.Double"));
            dt.Columns.Add("rate", Type.GetType("System.Double"));
            dt.Columns.Add("amt", Type.GetType("System.Double"));
            ViewState["tblpmattrans"] = dt;

        }

        private void TableCreateTo()
        {
            DataTable dt1 = new DataTable();

            dt1.Columns.Add("rsircode", Type.GetType("System.String"));
            dt1.Columns.Add("spcfcod", Type.GetType("System.String"));
            dt1.Columns.Add("rsirdesc", Type.GetType("System.String"));
            dt1.Columns.Add("sirunit", Type.GetType("System.String"));
            dt1.Columns.Add("spcfdesc", Type.GetType("System.String"));
            dt1.Columns.Add("qty", Type.GetType("System.Double"));
            dt1.Columns.Add("rate", Type.GetType("System.Double"));
            dt1.Columns.Add("amt", Type.GetType("System.Double"));
            ViewState["tblpmattransto"] = dt1;
        }
        private void Data_Bind()
        {


            this.grvacc.DataSource = (DataTable)ViewState["tblpmattrans"];
            this.grvacc.DataBind();

            // this.grvacc.Columns[1].Visible = (this.lblVoucherNo.Text.Trim() == "" || this.lblVoucherNo.Text.Trim() == "00000000000000");
            //((LinkButton)this.grvacc.FooterRow.FindControl("lnkupdate")).Visible = (this.lblVoucherNo.Text.Trim() == "" || this.lblVoucherNo.Text.Trim() == "00000000000000");
            this.FooterCalCulation();


        }

        private void FooterCalCulation()
        {
            DataTable dt1 = (DataTable)ViewState["tblpmattrans"];

            if (dt1.Rows.Count == 0)
                return;
            ((Label)this.grvacc.FooterRow.FindControl("lgvFAmount")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt)", "")) ?
            0.00 : dt1.Compute("sum(amt)", ""))).ToString("#,##0.00;(#,##0.00);-"); ;




        }


        protected void imgbtnFindtCAccount_Click(object sender, EventArgs e)
        {

        }
        protected void ibtnFindtoproject_Click(object sender, EventArgs e)
        {
            this.GetToProject();
        }

        private void GetToProject()
        {


            string comcod = this.ddlToCompany.SelectedValue.ToString();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GetProjectFromList", "%%", "", "", "", "", "", "", "", "");
            // Session["projectlist"] = ds1.Tables[0];
            if (ds1 == null)
                return;

            this.ddlprjlistto.DataTextField = "actdesc1";
            this.ddlprjlistto.DataValueField = "actcode";
            this.ddlprjlistto.DataSource = ds1.Tables[0];
            this.ddlprjlistto.DataBind();
            this.ResourceTo();
        }

        private void ResourceTo()
        {

            string comcod = this.ddlToCompany.SelectedValue.ToString();
            string FindResDesc = this.txtResource.Text.Trim() + "%";
            string curdate = this.txtfdate.Text.ToString().Trim();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "RESOURLISTTO", FindResDesc, "", "", "", "", "", "", "", "");
            Session["projectreslistto"] = ds1.Tables[0];
            ViewState["tblspcfto"] = ds1.Tables[1];

            if (ds1 == null)
                return;

            this.ddlResource.DataTextField = "resdesc";
            this.ddlResource.DataValueField = "rsircode";
            this.ddlResource.DataSource = ds1.Tables[0];
            this.ddlResource.DataBind();
            ds1.Dispose();
            this.GetSpecificationto();

        }

        private void GetSpecificationto()
        {
            string mResCode = this.ddlResource.SelectedValue.ToString().Substring(0, 9);
            //string spcfcod1 = this.ddlResSpcf.SelectedValue.ToString();
            this.ddlspecfi.Items.Clear();
            DataTable tbl1 = (DataTable)ViewState["tblspcfto"];
            DataView dv1 = tbl1.DefaultView;
            dv1.RowFilter = "mspcfcod = '" + mResCode + "' or spcfcod = '000000000000'";
            DataTable dt = dv1.ToTable();
            this.ddlspecfi.DataTextField = "spcfdesc";
            this.ddlspecfi.DataValueField = "spcfcod";
            this.ddlspecfi.DataSource = dt;
            this.ddlspecfi.DataBind();
        }
        protected void btnsource_Click(object sender, EventArgs e)
        {
            this.ResourceTo();
        }
        protected void btnselect_Click(object sender, EventArgs e)
        {

            string rescode = this.ddlResource.SelectedValue.ToString().Trim();
            string spcfcod = this.ddlspecfi.SelectedValue.ToString();
            DataTable dt = (DataTable)ViewState["tblpmattransto"];
            DataTable dt1 = (DataTable)Session["projectreslistto"];
            DataRow[] dr1 = dt.Select("rsircode = '" + rescode + "' and spcfcod = '" + spcfcod + "'");
            double qty = Convert.ToDouble("0" + this.txtqtyto.Text.Trim());
            double rate = 0.00;

            //DataTable tbl2 = (DataTable)ViewState["tblMat"];
            //DataRow[] dr3 = tbl2.Select("rsircode = '" + mResCode + "'");
            //dr1["rsirunit"] = dr3[0]["rsirunit"];

            if (dr1.Length == 0)
            {
                DataRow drforgrid = dt.NewRow();
                drforgrid["rsircode"] = rescode;
                drforgrid["rsirdesc"] = this.ddlResource.SelectedItem.Text;
                drforgrid["spcfcod"] = spcfcod;
                drforgrid["spcfdesc"] = this.ddlspecfi.SelectedItem.Text;
                //DataRow[] dr = dt1.Select("rsircode = '" + rescode + "' and spcfcod= '" + spcfcod + "'");
                DataRow[] dr3 = dt1.Select("rsircode = '" + rescode + "'");
                drforgrid["sirunit"] = dr3[0]["sirunit"];


                //drforgrid["sirunit"] = (dt1.Select("rsircode = '" + rescode + "' and spcfcod = '" + spcfcod + "'"))[0]["sirunit"].ToString();
                drforgrid["qty"] = qty;
                drforgrid["rate"] = rate;//(dt1.Select("rsircode = '" + rescode + "' and spcfcod = '" + spcfcod + "'"))[0]["rate"].ToString();
                drforgrid["amt"] = qty * rate;// Convert.ToDouble((dt1.Select("rsircode = '" + rescode + "' and spcfcod = '" + spcfcod + "'"))[0]["rate"]);
                dt.Rows.Add(drforgrid);
            }

            else
            {
                dr1[0]["qty"] = qty;
                dr1[0]["rate"] = rate; //(dt1.Select("rsircode = '" + rescode + "' and spcfcod = '" + spcfcod + "'"))[0]["rate"].ToString();
                dr1[0]["amt"] = qty * rate;// Convert.ToDouble((dt1.Select("rsircode = '" + rescode + "' and spcfcod = '" + spcfcod + "'"))[0]["rate"]);
            }
            ViewState["tblpmattransto"] = dt;
            this.Data_Binds();
        }

        private void Data_Binds()
        {
            DataTable dt1 = (DataTable)ViewState["tblpmattransto"];
            this.gvaccto.DataSource = dt1;
            this.gvaccto.DataBind();


            if (dt1.Rows.Count == 0)
                return;
            ((Label)this.gvaccto.FooterRow.FindControl("lgvFAmountto")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt)", "")) ?
            0.00 : dt1.Compute("sum(amt)", ""))).ToString("#,##0.00;(#,##0.00);-"); ;
        }

        private void SaveVaule1()
        {
            DataTable dt = (DataTable)ViewState["tblpmattransto"];
            int rowindex;
            for (int i = 0; i < this.gvaccto.Rows.Count; i++)
            {
                double qty = Convert.ToDouble("0" + ((TextBox)this.gvaccto.Rows[i].FindControl("txtqtyto")).Text.Trim());
                double rate = Convert.ToDouble("0" + ((TextBox)this.gvaccto.Rows[i].FindControl("txtrateto")).Text.Trim());
                double amt = qty * rate;


                rowindex = (gvaccto.PageIndex) * gvaccto.PageSize + i;
                dt.Rows[rowindex]["qty"] = qty;
                dt.Rows[rowindex]["rate"] = rate;
                dt.Rows[rowindex]["amt"] = amt;



            }
            ViewState["tblpmattransto"] = dt;

        }


        protected void lnktotalto_Click(object sender, EventArgs e)
        {
            this.SaveVaule1();
            this.Data_Binds();
        }

        protected void lnkNextbtn_Click(object sender, EventArgs e)
        {
            string prevPage = Request.UrlReferrer.ToString();
            Response.Redirect(prevPage);
        }
        protected void ddlResource_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSpecificationto();
        }
    }
}






