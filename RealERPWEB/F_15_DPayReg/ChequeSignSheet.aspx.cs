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
using RealERPRDLC;

using RealEntity;
namespace RealERPWEB.F_15_DPayReg
{
    public partial class ChequeSignSheet : System.Web.UI.Page
    {
        //public static string Narration = "";
        public static double TAmount = 0;
        ProcessAccess accData = new ProcessAccess();
        public static int PageNumber = 0;
        AutoCompleted AutoData = new AutoCompleted();
        // public static string lblVounum = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");



                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                if (Request.QueryString.AllKeys.Contains("reqno"))
                {
                    // print cheque from bill register interface
                    this.getChequeIssue();
                }
                else
                {
                    this.GetProjectName();
                    this.Bankcode();
                    this.ColumnVisible();
                    this.ComInitial();
                    this.lnkOk_Click(null, null);
                }

                if (Cache["cactcode"] == null)
                {

                    string cactcode = this.ddlBankName.SelectedValue.ToString();
                    Cache.Insert("cactcode", cactcode, null, DateTime.Now.AddHours(2), TimeSpan.Zero);
                }
                else
                {
                    string ccactcode = (string)Cache["cactcode"];
                    this.ddlBankName.SelectedValue = ccactcode;

                }
                string comcod = this.GetCompCode();
                if (comcod == "3336" || comcod == "3337")
                {
                    this.checkpb.Visible = true;
                    this.withoutchqdate.Visible = true;
                }
                else
                {
                    this.checkpb.Visible = false;
                }
                ((Label)this.Master.FindControl("lblTitle")).Text = "Cheque Preparation";
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;

                this.GetPayeeType();
            }
            

        }


        private void ComInitial()
        {
            string comcod = this.GetCompCode();

            switch (comcod)
            {

                case "3336":
                case "3337":

                    this.chkPrint.Checked = true;
                    this.ddlBankName.SelectedValue = "190200010001";
                    //  this.ChboxPayee.Checked = true;

                    break;

                default:

                    break;
            }


        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private void ColumnVisible()
        {
            if (this.Request.QueryString["Type"] == "Acc")
            {

                this.dgv1.Columns[7].Visible = false;
                this.dgv1.Columns[8].Visible = false;
            }
            else
            {
                this.dgv1.Columns[7].Visible = true;
                this.dgv1.Columns[8].Visible = true;
            }
        }
        private void GetBillList()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string srchproject = "%" + this.txtserchmrf.Text.Trim() + "%";
            string pactcode = (this.ddlProject.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProject.SelectedValue.ToString();


            string slnum = ""; //comcod == "3333" ? "" : this.Request.QueryString["slnum"].ToString();
                               // For Data Retirve
            switch (comcod)
            {
                case "3333":
                case "3335":
                    // case "3101":
                    break;


                default:
                    slnum = this.Request.QueryString["slnum"].ToString();
                    break;



            }

            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "GETISSUEWISECHK", pactcode, srchproject, "", slnum, "", "", "", "", "");
            if (ds2 == null)
                return;





            this.lstBillList.DataTextField = "textfield";
            this.lstBillList.DataValueField = "valefield";
            this.lstBillList.DataSource = ds2.Tables[0];
            this.lstBillList.DataBind();

            if (comcod == "1103")
            {
                this.ddlBankName.SelectedValue = ds2.Tables[0].Rows[0]["cactcode"].ToString();

            }
            else
            {

            }
            this.lstBillList.Focus();
            // For selected 
            switch (comcod)
            {
                case "3335":
                    // case "3101":
                    if (this.lstBillList.Items.Count > 0)
                    {

                        string slnum1 = this.Request.QueryString["slnum"].ToString();
                        bool sel = false;
                        foreach (ListItem lstitem in lstBillList.Items)
                        {
                            if (slnum1 == lstitem.Value)
                            {

                                this.lstBillList.SelectedValue = slnum1;
                                sel = true;
                                break;

                            }

                        }
                        if (!sel)
                        {
                            this.lstBillList.SelectedIndex = 0;
                        }
                        //this.lstBillList.SelectedValue = this.Request.QueryString["slnum"].ToString();;

                    }


                    break;

                default:

                    //if (this.lstBillList.Items.Count > 0)
                    //    this.lstBillList.SelectedValue = slnum;

                    if (this.lstBillList.Items.Count > 0)
                        this.lstBillList.SelectedIndex = 0;
                    break;

            }



            this.ShowData();

        }

        private void ChequeNo()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string bankname = this.ddlBankName.SelectedValue.ToString();
            string flag = "";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "TOPCHEQUE", bankname, flag, "", "", "", "", "", "", "");
            this.ddlcheque.DataTextField = "chequeno";
            this.ddlcheque.DataValueField = "chequeno";
            this.ddlcheque.DataSource = ds1.Tables[0];
            this.ddlcheque.DataBind();
            if (comcod == "3336")
            {
                this.ddlcheque.SelectedIndex = 0;
            }
            else
            {
                this.ddlcheque.SelectedIndex = (ds1.Tables[0].Rows.Count == 1) ? 0 : 1;
            }

            this.ddlcheque_SelectedIndexChanged(null, null);


        }

        private void GetPayeeType()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();


            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETPAYEETYPE", "", "", "", "", "", "", "", "", "");
            this.ddlpayeelist.DataTextField = "gdesc";
            this.ddlpayeelist.DataValueField = "gcod";
            this.ddlpayeelist.DataSource = ds1.Tables[0];
            ViewState["tblpayeetype"] = ds1.Tables[0];
            this.ddlpayeelist.DataBind();
            this.ddlpayeelist.SelectedValue = "";
        }

        private void Refrsh()
        {

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void ImgbtnFindProjectName_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

        private void GetProjectName()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string srchproject = "%" + this.txtProjectSearch.Text.Trim() + "%";
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "GETACTCODENAME", srchproject, "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;

            this.ddlProject.DataTextField = "actdesc";
            this.ddlProject.DataValueField = "actcode";
            this.ddlProject.DataSource = ds2.Tables[0];
            this.ddlProject.DataBind();




        }

        protected void lnkOk_Click(object sender, EventArgs e)
        {
            this.btnUpdate.Visible = true;
            // this.previousnar();
            this.GetBillList();
            this.ChequeNo();
            this.GetRecAndPayto();
            //this.ShowData();
            this.PnlNarration.Visible = true;



        }

        private void previousnar()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string ConAccHead = this.ddlBankName.SelectedValue.ToString();
            //string VNo1 = (ConAccHead.Substring(0, 4) == "1901" ? "C" : "B");
            // string VNo2 = (VNo1 == "J" ? "V" : (lblTitle.Contains("Payment") ? "D" : (lblTitle.Contains("Contra") ? "T" : "C")));
            string VNo3 = (ConAccHead.Substring(0, 4) == "1901" ? "CD" : "BD");
            string date = this.txtdate.Text.Trim();
            DataSet ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "LASTNARRATION", VNo3, date, "", "", "", "", "", "", "");


            if (ds4.Tables[0].Rows.Count == 0)
                this.txtNarration.Text = "";

            else
                this.txtNarration.Text = ds4.Tables[0].Rows[0]["vernar"].ToString();
        }

        public void GetRecAndPayto()
        {
           
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            AutoData.GetRecAndPayto(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "GETPAYRECCOD", "", "", "", "", "", "", "", "", "");

        }

        private void ShowData()
        {


            try
            {
                
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string Date = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
                string SrchRefno = "%" + this.txtserchmrf.Text.Trim() + "%";
                string Issueno = this.lstBillList.SelectedValue.ToString();

                string pactcode = ((this.ddlProject.SelectedValue.ToString() == "000000000000") ? "" : this.ddlProject.SelectedValue.ToString()) + "%";
                string RptType = (this.Request.QueryString["Type"] == "Acc") ? "Acc" : "Mgt";
                DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "SHOWCHQSIGN", Date, pactcode, "", "", SrchRefno, RptType, Issueno, "", "");
                if (ds1 == null)
                {
                    this.dgv1.DataSource = null;
                    this.dgv1.DataBind();
                    return;
                }



                ViewState["tbChqSign"] = this.HiddenSameDate(ds1.Tables[0]);



                this.txtPayto.Text = ds1.Tables[0].Rows[0]["payto"].ToString();

                if (ds1.Tables[1].Rows[0]["billno"].ToString() == "GBL")
                {

                    DataTable dt = ds1.Tables[0];
                    string billno = dt.Rows[0]["billno"].ToString();
                    string narration = "";
                    int i = 0;

                    foreach (DataRow dr1 in dt.Rows)
                    {

                        if (i == 0)
                        {
                            i++;
                            narration = narration + dr1["narr"].ToString() + ", ";
                            continue;
                        }
                        if (billno == dr1["billno"].ToString())
                            ;
                        else
                        {
                            narration = narration + dr1["narr"].ToString() + ", ";


                        }

                        billno = dr1["billno"].ToString();



                    }

                    this.txtNarration.Text = narration.Length > 0 ? narration.Substring(0, narration.Length - 2) : "";


                }

                else
                {
                    if (comcod == "3333" )
                    {
                        this.txtNarration.Text = ds1.Tables[2].Rows.Count==0 ? "": ds1.Tables[2].Rows[0]["billnar"].ToString();
                    }
                    else
                    {
                        this.previousnar();

                    }
                }
                //   Session["UserLog"] = ds1.Tables[2];

                this.Data_Bind();



            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error :" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }

        }





        protected void lstBillList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowData();
            //this.previousnar();


        }


        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["tbChqSign"];
            this.dgv1.DataSource = dt;
            this.dgv1.DataBind();



            if (dt.Rows.Count > 0)
            {

                double amount = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amount)", "")) ? 0.00 : dt.Compute("Sum(amount)", "")));
                double tax = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tax)", "")) ? 0.00 : dt.Compute("Sum(tax)", "")));
                double netamt = amount - tax;// Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(netamt)", "")) ? 0.00 : dt.Compute("Sum(netamt)", "")));
                ((Label)this.dgv1.FooterRow.FindControl("lblgvFchqamt")).Text = amount.ToString("#,##0;(#,##0); ");
                ((Label)this.dgv1.FooterRow.FindControl("lblgvFtax")).Text = tax.ToString("#,##0;(#,##0); ");
                ((Label)this.dgv1.FooterRow.FindControl("lblgvFnetamt")).Text = netamt.ToString("#,##0;(#,##0); ");

                this.lblInword.Text = ASTUtility.Trans(amount, 2);
            }

        }

        private void SaveValue()
        {
            DataTable dt = (DataTable)ViewState["tbChqSign"];
            double chequeamt = 0.00;
            double tax = 0.00;
            string chequedate;
            double netamt = 0.00;
            string Chequeno;
            int i = 0;

            foreach (GridViewRow gv1 in dgv1.Rows)
            {



                chequeamt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1.FindControl("txtgvAmount")).Text.Trim()));
                tax = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1.FindControl("txtgvtax")).Text.Trim()));
                chequedate = Convert.ToDateTime(((TextBox)gv1.FindControl("txtgvChqdate")).Text.Trim()).ToString("dd-MMM-yyyy");
                Chequeno = ((TextBox)gv1.FindControl("txtgvChqNo")).Text.Trim();
                netamt = chequeamt - tax;

                dt.Rows[i]["amount"] = chequeamt;
                dt.Rows[i]["tax"] = tax;
                dt.Rows[i]["netamt"] = netamt;
                dt.Rows[i]["Chequeno"] = Chequeno;
                dt.Rows[i]["chqdate"] = chequedate;
                i++;
            }

            ViewState["tblt01"] = dt;
        }


        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();

        }

        private DataTable HiddenSameDate(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;

            string slnum = dt1.Rows[0]["slnum"].ToString();
            string actcode = dt1.Rows[0]["actcode"].ToString();
            string rescode = dt1.Rows[0]["rescode"].ToString();

            int j;



            for (j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["slnum"].ToString() == slnum && dt1.Rows[0]["actcode"].ToString() == actcode && dt1.Rows[j]["rescode"].ToString() == rescode)
                {
                    //dt1.Rows[j]["actdesc"] = "";
                    // dt1.Rows[j]["resdesc"] = "";
                    dt1.Rows[j]["apamt"] = 0.00;
                }
                else
                {
                    if (dt1.Rows[j]["slnum"].ToString() == slnum)
                    {
                        dt1.Rows[j]["apamt"] = 0.00;
                    }

                    //if (dt1.Rows[j]["actcode"].ToString() == actcode)
                    //{
                    //    dt1.Rows[j]["actdesc"] = "";
                    //}

                    //if (dt1.Rows[j]["rescode"].ToString() == rescode)
                    //{
                    //    dt1.Rows[j]["resdesc"] = "";
                    //}

                }

                slnum = dt1.Rows[j]["slnum"].ToString();
                actcode = dt1.Rows[j]["actcode"].ToString();
                rescode = dt1.Rows[j]["rescode"].ToString();
            }





            return dt1;


        }
        protected void CalculatrGridTotal()
        {
            DataTable dttotal = (DataTable)ViewState["tbltopage"];
            double cramt = Convert.ToDouble(((DataTable)ViewState["tbltopage"]).Rows[0]["cramt"]);
            ((Label)this.dgv1.FooterRow.FindControl("lgvFCrAmt")).Text = cramt.ToString("#,##0;-#,##0; ");
        }


        protected DateTime GetBackDate()
        {
            string comcod = this.GetCompCode();
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "GETBDATE", "", "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {

                return (System.DateTime.Today);
            }

            return (Convert.ToDateTime(ds2.Tables[0].Rows[0]["bdate"]));

        }


        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            if (chkCrVou.Checked)
            {
                if (this.chkPrint.Checked)

                    switch (comcod)
                    {
                        case "3101":
                        case "3336":
                        case "3337":
                            this.PrintchKSuvastu();
                            break;
                        default:
                            this.PrinCheque();
                            break;
                    }
                else
                    this.PrintVoucher();
            }
            else
            {
                if (this.chkPrint.Checked)
                {
                    switch (comcod)
                    {
                        case "3101":
                        case "3336":
                        case "3337":
                            this.PrintchKSuvastu();
                            break;
                        default:
                            this.RptPostDatChq();
                            break;
                    }


                }
                else
                {
                    this.VouPrint();
                }
            }


        }


        private void PrintchKSuvastu()
        {

            try
            {

                //   string curvoudat = this.txtEntryDate.Text.Substring(0, 11);
                string vounum = lblVoun.Text;

                string paytype = this.ChboxPayee.Checked ? "0" : "1";
                string cquepbl = (this.checkpb.Checked) == false ? "0" : "1";
                string woutchqdat = (this.withoutchqdate.Checked) == false ? "0" : "1";


                //  string type = "Type=AccCheque&vounum=" + vounum + "&paytype=" + paytype + "&pbl=" + cquepbl + "&woutchqdat=" + woutchqdat;


                string type = "Type=AccCheque&vounum=" + vounum + "&paytype=" + paytype + "&pbl=" + cquepbl + "&woutchqdat=" + woutchqdat; ;



                string printype = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "target", "PrintRptCheque('" + type + "');", true);

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
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
                case "3333":
                    vouprint = "VocherPrintMod";
                    break;

                default:
                    vouprint = "VocherPrint";
                    break;
            }
            return vouprint;
        }
        private string CompanyPrintCheque()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string chequeprint = "";
            switch (comcod)
            {

                case "2305":
                case "3305":

                case "3307":
                case "3308":
                case "3309":
                    chequeprint = "PrintCheque01";
                    break;


                case "1301":
                case "2301":
                case "3301":
                    chequeprint = "PrintCheque02";
                    break;


                case "3306":
                    chequeprint = "PrintCheque03";
                    break;



                default:
                    chequeprint = "PrintCheque01";
                    break;
            }
            return chequeprint;
        }

        private void PrinCheque()
        {

            try
            {

                //   string curvoudat = this.txtEntryDate.Text.Substring(0, 11);
                string vounum = lblVoun.Text;

                string paytype = this.ChboxPayee.Checked ? "0" : "1";

                string type = "Type=AccCheque&vounum=" + vounum + "&paytype=" + paytype;

                string printype = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "target", "PrintRptCheque('" + type + "');", true);

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }



            //try
            //{
            //    //Hashtable hst = (Hashtable)Session["tblLogin"];
            //    //string comcod = hst["comcod"].ToString();
            //    //string curvoudat = this.txtdate.Text.Substring(0, 11);

            //    ////DataTable dt = (DataTable)Session["tbChqSign"];
            //    ////DataTable dt1 = dt.Copy();

            //    ////slnum = dt1.Rows[i]["slnum"].ToString();

            //    //string vounum = lblVoun.Text;
            //    //DataSet _ReportDataSet = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", "PRINTVOUCHER01", vounum, "", "", "", "", "", "", "", "");
            //    //if (_ReportDataSet == null)
            //    //    return;
            //    //DataTable dt = _ReportDataSet.Tables[0];
            //    //if (dt.Rows.Count == 0)
            //    //    return;
            //    //double toamt, dramt, cramt;
            //    //dramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(Dr)", "")) ? 0.00 : dt.Compute("sum(Dr)", "")));
            //    //cramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(Cr)", "")) ? 0.00 : dt.Compute("sum(Cr)", "")));
            //    //if (dramt > 0 && cramt <= 0)
            //    //    toamt = dramt;
            //    //else
            //    //    toamt = cramt;
            //    //string chequedat = Convert.ToDateTime(this.txtdate.Text).ToString("ddMMyyyy");
            //    //chequedat = chequedat.Substring(0, 1) + "   " + chequedat.Substring(1, 1) + "   " + chequedat.Substring(2, 1) + "   " + chequedat.Substring(3, 1) + "   " + chequedat.Substring(4, 1) + "   " + chequedat.Substring(5, 1) + "   " + chequedat.Substring(6, 1) + "   " + chequedat.Substring(7, 1);
            //    //string payto = this.txtPayto.Text.Trim();
            //    //string amt1 = ASTUtility.Trans(Math.Round(toamt), 2);
            //    //int len = amt1.Length;
            //    //string amt2 = amt1.Substring(7, (len - 8));


            //    //string Chequeprint = this.CompanyPrintCheque();
            //    ////if(Chequeprint=="PrintCheque01")
            //    ////    this.PrintCheque01();

            //    //ReportDocument rptinfo = new ReportDocument();
            //    //if (Chequeprint == "PrintCheque01")
            //    //    rptinfo = new RealERPRPT.R_17_Acc.PrintCheck();

            //    //else if (Chequeprint == "PrintCheque02")
            //    //    rptinfo = new RealERPRPT.R_17_Acc.PrintCheeque02();
            //    ////else if (Chequeprint == "PrintCheque03")
            //    ////    rptinfo = new RealERPRPT.R_17_Acc.PrintCheeque03();
            //    //else
            //    //    rptinfo = new RealERPRPT.R_17_Acc.PrintCheeque02();

            //    //TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
            //    //date.Text = chequedat;
            //    //TextObject rpttxtpayto = rptinfo.ReportDefinition.ReportObjects["txtpayto"] as TextObject;
            //    //rpttxtpayto.Text = payto;
            //    //TextObject rpttxtamtinword = rptinfo.ReportDefinition.ReportObjects["txtamtinword"] as TextObject;
            //    //rpttxtamtinword.Text = amt2;
            //    //TextObject rpttxtamt = rptinfo.ReportDefinition.ReportObjects["txtamt"] as TextObject;
            //    //rpttxtamt.Text = toamt.ToString("#,##0;(#,##0); ") + "/=";


            //    //rptinfo.SetDataSource(dt);
            //    //Session["Report1"] = rptinfo;
            //    //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //    //             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



            //    Hashtable hst = (Hashtable)Session["tblLogin"];
            //    string comcod = hst["comcod"].ToString();
            //    string curvoudat = this.txtdate.Text.Substring(0, 11);
            //    string vounum = lblVoun.Text;



            //    DataSet _ReportDataSet = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", "PRINTVOUCHER01", vounum, "", "", "", "", "", "", "", "");
            //    //if (_ReportDataSet == null)
            //    //    return;

            //    //string vounum = this.ddlChkVouNo.SelectedValue.ToString();
            //    //DataSet _ReportDataSet = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "PRINTCHECK", vounum, "", "", "", "", "", "", "", "");
            //    if (_ReportDataSet == null)
            //        return;
            //    DataTable dt1 = _ReportDataSet.Tables[0];
            //    string voudat = Convert.ToDateTime(this.txtdate.Text).ToString("ddMMyyyy");
            //    //string voudat = Convert.ToDateTime(this.txtEntryDate.Text).ToString("ddMMyyyy"); // Convert.ToDateTime(dt1.Rows[0]["voudat"]).ToString("ddMMyyyy");
            //    // voudat = voudat.Substring(0, 1) + "   " + voudat.Substring(1, 1) + "   " + voudat.Substring(2, 1) + "   " + voudat.Substring(3, 1) + "   " + voudat.Substring(4, 1) + "   " + voudat.Substring(5, 1) + "   " + voudat.Substring(6, 1) + "   " + voudat.Substring(7, 1);
            //    string payto = this.txtPayto.Text.Trim();
            //    //double amt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(Dr)", "")) ? 0.00 : dt1.Compute("sum(Dr)", ""))); //Convert.ToDouble(dt1.Rows[0]["tamt"].ToString());
            //    double toamt, dramt, cramt;
            //    dramt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(Dr)", "")) ? 0.00 : dt1.Compute("sum(Dr)", "")));
            //    cramt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(Cr)", "")) ? 0.00 : dt1.Compute("sum(Cr)", "")));
            //    if (dramt > 0 && cramt <= 0)
            //        toamt = dramt;
            //    else
            //        toamt = cramt;



            //    string amt1 = ASTUtility.Trans(Math.Round(toamt), 2);
            //    int len = amt1.Length;
            //    string amt2 = amt1.Substring(7, (len - 8));
            //    string wam1 = string.Empty;
            //    string wam2 = string.Empty;
            //    string Chequeprint = this.CompanyPrintCheque();
            //    string[] amtWrd1 = ASTUtility.Trans(Math.Round(toamt, 0), 2).Split('(', ')');
            //    string[] amtdivide = amtWrd1[1].Split(' ');

            //    string value = this.ChboxPayee.Checked ? "A/C Payee" : "";



            //    for (int i = 2; i <= amtdivide.Length - 1; i++)
            //    {
            //        if (i == amtdivide.Length)
            //        {
            //            return;
            //        }
            //        else if (i > 8)
            //        {
            //            wam1 += " " + amtdivide[i].ToString();
            //        }
            //        else
            //        {
            //            wam2 += " " + amtdivide[i].ToString();
            //        }
            //    }


            //    Hashtable hshtbl = new Hashtable();
            //    hshtbl["bankName"] = "";
            //    hshtbl["payTo"] = payto;
            //    hshtbl["acpayee"] = value;
            //    hshtbl["date"] = voudat;
            //    hshtbl["amtWord"] = wam2;//.ToUpper();
            //    hshtbl["amtWord1"] = wam1;//.ToUpper();
            //    hshtbl["amt"] = toamt.ToString("#,##0;(#,##0); ") + "/-";

            //    LocalReport rpt1 = new LocalReport();

            //    rpt1 = RptSetupClass1.GetLocalReport("R_17_Acc.RptCheque", hshtbl, null, null);

            //    Session["Report1"] = rpt1;
            //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
            //        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //}
            //catch (Exception ex)
            //{
            // ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            // ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            // ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            //}

        }


        private void PrintCheque01()
        {

        }
        private void PrintCheque02()
        {
        }


        private string GetCompInstar()
        {

            string comcod = this.GetCompCode();
            string printinstar = "";
            switch (comcod)
            {
                case "3330":
                    break;

                default:
                    printinstar = "Innstar";
                    break;


            }
            return printinstar;
        }
        private void PrintVoucher()
        {
            try
            {



                string vounum = lblVoun.Text;
                string paytype = "0";
                // hlink1.NavigateUrl = "~/F_17_Acc/GeneralAccounts.aspx?Mod=Management&vounum=" + vounum;
                // hlnkPrintVoucher.NavigateUrl = "~/F_17_Acc/AccPrint.aspx?Type=accVou&vounum=" + vounum;

                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../F_17_Acc/AccPrint.aspx?Type=accVou&vounum=" + vounum + "&paytype=" + paytype
                           + "', target='_blank');</script>";



                //Hashtable hst = (Hashtable)Session["tblLogin"];
                //string comcod = hst["comcod"].ToString();
                //string comnam = hst["comnam"].ToString();
                //string comadd = hst["comadd1"].ToString();
                //string combranch = hst["combranch"].ToString();
                //string compname = hst["compname"].ToString();
                //string username = hst["username"].ToString();
                //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
                //string curvoudat = this.txtdate.Text.Substring(0, 11);
                //string vounum = lblVoun.Text;
                //string PrintInstar = this.GetCompInstar();
                ////string vounum = this.ddlPrivousVou.SelectedValue.ToString();
                //DataSet _ReportDataSet = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", "PRINTVOUCHER01", vounum, PrintInstar, "", "", "", "", "", "", "");
                //if (_ReportDataSet == null)
                //    return;
                //DataTable dt = _ReportDataSet.Tables[0];
                //if (dt.Rows.Count == 0)
                //    return;
                //double dramt, cramt;
                //dramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(Dr)", "")) ? 0.00 : dt.Compute("sum(Dr)", "")));
                //cramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(Cr)", "")) ? 0.00 : dt.Compute("sum(Cr)", "")));



                //if (dramt > 0 && cramt > 0)
                //{
                //    TAmount = cramt;

                //}
                //else if (dramt > 0 && cramt <= 0)
                //{
                //    TAmount = dramt;
                //}
                //else
                //{
                //    TAmount = cramt;
                //}

                //DataTable dt1 = _ReportDataSet.Tables[1];
                //string Vounum = dt1.Rows[0]["vounum"].ToString();
                //string voudat = Convert.ToDateTime(dt1.Rows[0]["voudat"]).ToString("dd-MMM-yyyy");
                //string refnum = dt1.Rows[0]["refnum"].ToString();
                //string payto = dt1.Rows[0]["payto"].ToString();
                //string receivedBank = dt1.Rows[0]["banknam"].ToString();
                //string voutype = dt1.Rows[0]["voutyp"].ToString();
                //string venar = dt1.Rows[0]["venar"].ToString();
                //string Posteddat = Convert.ToDateTime(dt1.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy");
                //string Type = this.CompanyPrintVou();

                //string billno = dt.Rows[0]["billno"].ToString();
                //string billno1 = dt.Rows[0]["billno"].ToString();
                ////string[] billno1;

                ////string[] billno;

                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    // dt1.Rows[j]["useridapp"].ToString() == useridapp

                //    if (billno1 == dt.Rows[i]["billno"].ToString())
                //    {

                //    }

                //    else
                //    {
                //        billno += dt.Rows[i]["billno"].ToString() +  (((dt.Rows[i]["billno"].ToString()).Length == 0) ? " " : ", ");
                //    }


                //    billno1 = dt.Rows[i]["billno"].ToString();

                //}

                //int l = billno.Trim().Length;
                //billno = billno.Substring(0, l - 1);





                //ReportDocument rptinfo = new ReportDocument();
                //if (Type == "VocherPrint")
                //{

                //    string Vounum1 = dt1.Rows[0]["vounum"].ToString().Substring(0, 2);

                //    rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher();


                //    if (Vounum1 == "BD" || Vounum1 == "CD")
                //    {

                //        TextObject txtBillno = rptinfo.ReportDefinition.ReportObjects["txtBillno"] as TextObject;
                //        txtBillno.Text = "Bill No : " + billno;
                //        TextObject txtissuno = rptinfo.ReportDefinition.ReportObjects["txtissuno"] as TextObject;
                //        txtissuno.Text = "Payment ID : " + dt1.Rows[0]["isunum"].ToString();

                //    }




                //}
                //else if (Type == "VocherPrint1")
                //{
                //    rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher1();
                //    TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                //    txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
                //    TextObject txtisunum1 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
                //    txtisunum1.Text = "Issue No: " + (this.lblisunum.Text.Trim() == "" ? this.lblisunum.Text.Trim() : ASTUtility.Right(this.lblisunum.Text.Trim(), 6));
                //    TextObject txtPosteddate1 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
                //    txtPosteddate1.Text = "Entry Date: " + Posteddat;

                //}
                //else if (Type == "VocherPrint2")
                //{
                //    rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher2();
                //    TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                //    txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
                //    TextObject txtisunum2 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
                //    txtisunum2.Text = "Issue No: " + (this.lblisunum.Text.Trim() == "" ? this.lblisunum.Text.Trim() : ASTUtility.Right(this.lblisunum.Text.Trim(), 6));
                //    TextObject txtPosteddate2 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
                //    txtPosteddate2.Text = "Entry Date: " + Posteddat;
                //}
                //else if (Type == "VocherPrint3")
                //{
                //    rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher3();
                //    TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                //    txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
                //    TextObject txtisunum1 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
                //    txtisunum1.Text = "Issue No: " + (this.lblisunum.Text.Trim() == "" ? this.lblisunum.Text.Trim() : ASTUtility.Right(this.lblisunum.Text.Trim(), 6));
                //    TextObject txtPosteddate1 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
                //    txtPosteddate1.Text = "Entry Date: " + Posteddat;

                //}



                //else if (Type == "VocherPrintMod")
                //{
                //    if (ASTUtility.Left(vounum, 2) == "JV")
                //    {
                //        rptinfo = new RealERPRPT.R_17_Acc.rptPrintVocherAlli();

                //        //TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //        //vounum1.Text = "Voucher No.: " + vounum;
                //        //TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //        //date.Text = "Voucher Date: " + voudat;
                //        //TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //        //naration.Text = "Narration: " + venar;
                //    }




                //    else
                //    {
                //        string vouno = vounum.Substring(0, 2);
                //        string paytoorecived = (vouno == "BC" || vouno == "CC") ? "Recieved From" : "Pay To";

                //        if (vouno == "BC" || vouno == "CC")
                //        {
                //            rptinfo = new RealERPRPT.R_17_Acc.rptPrintVocherAlli02();
                //            TextObject txtDesc = rptinfo.ReportDefinition.ReportObjects["txtDesc"] as TextObject;
                //            txtDesc.Text = dt1.Rows[0]["cactdesc"].ToString();// this.ddlConAccHead.SelectedItem.Text.Substring(13).ToString();
                //            //TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                //            //rpttxtPartyName.Text = (payto == "") ? "" : payto;


                //            //TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //            //naration.Text = venar;

                //            TextObject txtporrecieved = rptinfo.ReportDefinition.ReportObjects["txtporrecieved"] as TextObject;
                //            txtporrecieved.Text = paytoorecived;

                //            TextObject txtusername = rptinfo.ReportDefinition.ReportObjects["username"] as TextObject;
                //            txtusername.Text = username;

                //            TextObject txtProject = rptinfo.ReportDefinition.ReportObjects["txtProject"] as TextObject;
                //            txtProject.Text = ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "16" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "18" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "26" ? "Project" : "Head Office";

                //            //TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //            //vounum1.Text = vounum;
                //            //TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //            //date.Text = voudat;
                //            ////TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                //            //Refnum.Text = refnum;
                //            TextObject txtReceivedBank = rptinfo.ReportDefinition.ReportObjects["txtReceivedBank"] as TextObject;
                //            txtReceivedBank.Text = receivedBank;


                //        }

                //        else
                //        {
                //            rptinfo = new RealERPRPT.R_17_Acc.rptPrintVocherAlli03();

                //            if (vouno == "BD" || vouno == "CD")
                //            {

                //                //TextObject txtBillno = rptinfo.ReportDefinition.ReportObjects["txtBillno"] as TextObject;
                //                //txtBillno.Text = "Bill No : " + billno;
                //                TextObject txtissuno = rptinfo.ReportDefinition.ReportObjects["txtissuno"] as TextObject;
                //                txtissuno.Text = "Payment ID : " + dt1.Rows[0]["isunum"].ToString();

                //            }

                //            TextObject txtDesc = rptinfo.ReportDefinition.ReportObjects["txtDesc"] as TextObject;
                //            txtDesc.Text = dt1.Rows[0]["cactdesc"].ToString();// this.ddlConAccHead.SelectedItem.Text.Substring(13).ToString();
                //            //TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                //            //rpttxtPartyName.Text = (payto == "") ? "" : payto;


                //            //TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //            //naration.Text = venar;

                //            TextObject txtporrecieved = rptinfo.ReportDefinition.ReportObjects["txtporrecieved"] as TextObject;
                //            txtporrecieved.Text = paytoorecived;

                //            TextObject txtusername = rptinfo.ReportDefinition.ReportObjects["username"] as TextObject;
                //            txtusername.Text = username;

                //            TextObject txtProject = rptinfo.ReportDefinition.ReportObjects["txtProject"] as TextObject;
                //            txtProject.Text = ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "16" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "18" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "26" ? "Project" : "Head Office";

                //            //TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //            //vounum1.Text = vounum;
                //            //TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //            //date.Text = voudat;
                //            //TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                //            //Refnum.Text = refnum;


                //        }




                //    }






                //}

                //else
                //{
                //    rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher4();
                //    TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                //    txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
                //    TextObject txtisunum3 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
                //    txtisunum3.Text = "Issue No: " + (this.lblisunum.Text.Trim() == "" ? this.lblisunum.Text.Trim() : ASTUtility.Right(this.lblisunum.Text.Trim(), 6));
                //    TextObject txtPosteddate3 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
                //    txtPosteddate3.Text = "Entry Date: " + Posteddat;
                //}


                //rptinfo.SetDataSource(_ReportDataSet.Tables[0]);
                //TextObject txtCompanyName = rptinfo.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
                //txtCompanyName.Text = comnam;
                //TextObject txtcAdd = rptinfo.ReportDefinition.ReportObjects["compadd"] as TextObject;
                //txtcAdd.Text = comadd;
                //TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //vounum1.Text = vounum;
                //TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //date.Text =  voudat;
                //TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                //Refnum.Text = refnum;
                //TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                //rpttxtPartyName.Text = (this.txtPayto.Text.Trim() == "") ? "" : this.txtPayto.Text.Trim();
                //TextObject voutype1 = rptinfo.ReportDefinition.ReportObjects["voutype"] as TextObject;
                //voutype1.Text = voutype;
                //TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //naration.Text = venar;

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

        private void VouPrint()
        {
            try
            {

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string comnam = hst["comnam"].ToString();
                string comadd = hst["comadd1"].ToString();
                string combranch = hst["combranch"].ToString();
                string compname = hst["compname"].ToString();

                string username = hst["username"].ToString();
                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
                string curvoudat = this.txtdate.Text;
                string vounum = lblVoun.Text;


                string hostname = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/F_17_Acc/";
                string currentptah = "AccPrint.aspx?Type=PostDatVou&vounum=" + vounum;
                string totalpath = hostname + currentptah;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('" + totalpath + "', target='_blank');</script>";




                //DataSet _ReportDataSet = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "PRINTVOUCHER01", vounum, "", "", "", "", "", "", "", "");
                //if (_ReportDataSet == null)
                //    return;
                //DataTable dt = _ReportDataSet.Tables[0];
                //if (dt.Rows.Count == 0)
                //    return;
                //double dramt, cramt;
                //dramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(Dr)", "")) ? 0.00 : dt.Compute("sum(Dr)", "")));
                //cramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(Cr)", "")) ? 0.00 : dt.Compute("sum(Cr)", "")));



                //if (dramt > 0 && cramt > 0)
                //{
                //    TAmount = cramt;

                //}
                //else if (dramt > 0 && cramt <= 0)
                //{
                //    TAmount = dramt;
                //}
                //else
                //{
                //    TAmount = cramt;
                //}

                //string Type = this.CompanyPrintVou();
                //ReportDocument rptinfo = new ReportDocument();
                //if (Type == "VocherPrint")
                //{
                //    rptinfo = new RealERPRPT.R_17_Acc.rptBankVoucher();
                //}
                //else if (Type == "VocherPrint1")
                //{
                //    rptinfo = new RealERPRPT.R_17_Acc.rptBankVoucher1();

                //}
                //else if (Type == "VocherPrint2")
                //{
                //    rptinfo = new RealERPRPT.R_17_Acc.rptBankVoucher2();
                //}

                //else if (Type == "VocherPrint3")
                //{
                //    rptinfo = new RealERPRPT.R_17_Acc.rptBankVoucher3();
                //}

                //else if (Type == "VocherPrintMod")
                //{
                //    rptinfo = new RealERPRPT.R_17_Acc.rptBankVoucherAlli();
                //}

                //else
                //{
                //    rptinfo = new RealERPRPT.R_17_Acc.rptBankVoucher4();
                //}


                ////-----------------------------
                //DataTable dt1 = _ReportDataSet.Tables[1];
                ////string Vounum = dt1.Rows[0]["vounum"].ToString();
                //string voudat = Convert.ToDateTime(dt1.Rows[0]["voudat"]).ToString("dd-MMM-yyyy");
                //string venar = dt1.Rows[0]["venar"].ToString();
                ////ReportDocument rptinfo = new RealERPRPT.R_17_Acc.rptBankVoucher();
                //rptinfo.SetDataSource(_ReportDataSet.Tables[0]);
                //TextObject txtCompanyName = rptinfo.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
                //txtCompanyName.Text = comnam;
                //TextObject txtcAdd = rptinfo.ReportDefinition.ReportObjects["compadd"] as TextObject;
                //txtcAdd.Text = comadd;

                //TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                //txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";

                //TextObject rpttxtVoutype = rptinfo.ReportDefinition.ReportObjects["txtVoutype"] as TextObject;
                //rpttxtVoutype.Text = "Payment Voucher";
                //TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //vounum1.Text = "Voucher No: " + vounum;
                //TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //date.Text = " Date:" + voudat;
                //TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //naration.Text = "Naration: " + venar;
                //TextObject rpttxtamt = rptinfo.ReportDefinition.ReportObjects["txtamt"] as TextObject;
                //rpttxtamt.Text = ASTUtility.Trans(Math.Round(TAmount), 2);
                //TextObject txtuserinfo = rptinfo.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

                //if (ConstantInfo.LogStatus == true)
                //{
                //    string eventtype = "Post Dated Cheque";
                //    string eventdesc = "Print Voucher";
                //    string eventdesc2 = vounum;
                //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                //}
                //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                //rptinfo.SetParameterValue("ComLogo", ComLogo);
                //Session["Report1"] = rptinfo;
                //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                //              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }
        private string GetCChequePrint()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string vouprint = "";
            switch (comcod)
            {

                case "2305":
                case "3305":
                case "3306":
                case "3307":
                case "3308":
                    vouprint = "CPrint1";
                    break;

                case "3309":
                    vouprint = "CPrint2";
                    break;
                default:
                    vouprint = "CPrint1";
                    break;

            }
            return vouprint;
        }



        private void RptPostDatChq()
        {
            try
            {

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string vounum = lblVoun.Text.Trim();
                string chqno = this.ddlcheque.SelectedValue.ToString();
                DataSet _ReportDataSet = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "RPTPOSTDATCHECK", vounum, chqno, "", "", "", "", "", "", "");
                if (_ReportDataSet == null)
                    return;
                DataTable dt1 = _ReportDataSet.Tables[0];
                string chequedat = Convert.ToDateTime(dt1.Rows[0]["chequedat"]).ToString("ddMMyyyy");
                chequedat = chequedat.Substring(0, 1) + "   " + chequedat.Substring(1, 1) + "   " + chequedat.Substring(2, 1) + "   " + chequedat.Substring(3, 1) + "   " + chequedat.Substring(4, 1) + "   " + chequedat.Substring(5, 1) + "   " + chequedat.Substring(6, 1) + "   " + chequedat.Substring(7, 1);
                string payto = dt1.Rows[0]["payto"].ToString();
                double amt = Convert.ToDouble(dt1.Rows[0]["trnam"].ToString());
                string amt1 = ASTUtility.Trans(Math.Round(amt), 2);
                int len = amt1.Length;
                string amt2 = amt1.Substring(7, (len - 8));


                string Chequeprint = this.CompanyPrintCheque();
                ReportDocument rptinfo = new ReportDocument();




                if (Chequeprint == "PrintCheque01")
                    rptinfo = new RealERPRPT.R_17_Acc.PrintCheck();
                else if (Chequeprint == "PrintCheque02")
                    rptinfo = new RealERPRPT.R_17_Acc.PrintCheeque02();
                else if (Chequeprint == "PrintChequeAssure")
                    rptinfo = new RealERPRPT.R_17_Acc.PrintCheequeAssure();
                else
                    rptinfo = new RealERPRPT.R_17_Acc.PrintCheckHolding();


                TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                date.Text = chequedat;
                TextObject rpttxtpayto = rptinfo.ReportDefinition.ReportObjects["txtpayto"] as TextObject;
                rpttxtpayto.Text = payto;
                TextObject rpttxtamtinword = rptinfo.ReportDefinition.ReportObjects["txtamtinword"] as TextObject;
                rpttxtamtinword.Text = amt2;
                TextObject rpttxtamt = rptinfo.ReportDefinition.ReportObjects["txtamt"] as TextObject;
                rpttxtamt.Text = amt.ToString("#,##0;(#,##0); ") + "/=";

                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Voucher Print";
                    string eventdesc = "Top Sheet";
                    string eventdesc2 = "Voucher No.: " + vounum;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }
                rptinfo.SetDataSource(_ReportDataSet.Tables[0]);
                Session["Report1"] = rptinfo;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }



        }

        //private void RptPostDatChq()
        //{
        //    try
        //    {

        //        Hashtable hst = (Hashtable)Session["tblLogin"];
        //        string comcod = hst["comcod"].ToString();
        //        string vounum = lblVoun.Text;
        //        DataTable dtchk = (DataTable)Session["tbChqSign"];

        //        string chqno = dtchk.Rows[0]["chequeno"].ToString(); //this.ddlChqList.SelectedValue.Substring(14);
        //        DataSet _ReportDataSet = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "RPTPOSTDATCHECK", vounum, chqno, "", "", "", "", "", "", "");
        //        if (_ReportDataSet == null)
        //            return;
        //        DataTable dt1 = _ReportDataSet.Tables[0];
        //        string chequedat = Convert.ToDateTime(dt1.Rows[0]["chequedat"]).ToString("ddMMyyyy");
        //        chequedat = chequedat.Substring(0, 1) + "   " + chequedat.Substring(1, 1) + "   " + chequedat.Substring(2, 1) + "   " + chequedat.Substring(3, 1) + "   " + chequedat.Substring(4, 1) + "   " + chequedat.Substring(5, 1) + "   " + chequedat.Substring(6, 1) + "   " + chequedat.Substring(7, 1);
        //        string payto = dt1.Rows[0]["payto"].ToString();
        //        double amt = Convert.ToDouble(dt1.Rows[0]["trnam"].ToString());
        //        string amt1 = ASTUtility.Trans(Math.Round(amt), 2);
        //        int len = amt1.Length;
        //        string amt2 = amt1.Substring(7, (len - 8));

        //        string type = this.CompanyPrintCheque();
        //        ReportDocument rptinfo = new ReportDocument();
        //        if (type == "PrintCheque01")
        //            rptinfo = new RealERPRPT.R_17_Acc.PrintCheck();

        //        else if (type == "PrintCheque02")
        //            rptinfo = new RealERPRPT.R_17_Acc.PrintCheeque02();
        //        else rptinfo = new RealERPRPT.R_17_Acc.PrintCheckHolding();




        //        TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
        //        date.Text = chequedat;
        //        TextObject rpttxtpayto = rptinfo.ReportDefinition.ReportObjects["txtpayto"] as TextObject;
        //        rpttxtpayto.Text = payto;
        //        TextObject rpttxtamtinword = rptinfo.ReportDefinition.ReportObjects["txtamtinword"] as TextObject;
        //        rpttxtamtinword.Text = amt2;
        //        TextObject rpttxtamt = rptinfo.ReportDefinition.ReportObjects["txtamt"] as TextObject;
        //        rpttxtamt.Text = amt.ToString("#,##0;(#,##0); ") + "/=";

        //        if (ConstantInfo.LogStatus == true)
        //        {
        //            string eventtype = "Post Dated Cheque";
        //            string eventdesc = "Print Cheque";
        //            string eventdesc2 = vounum + "  " + chqno;
        //            bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
        //        }
        //        rptinfo.SetDataSource(_ReportDataSet.Tables[0]);
        //        Session["Report1"] = rptinfo;
        //        ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
        //                      ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        //    }
        //    catch (Exception ex)
        //    {
        //     ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
        //    }

        //}

        protected void dgv1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlnkAccdesc1 = (HyperLink)e.Row.FindControl("hlnkAccdesc1");



                string actcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();
                string subcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();
                string subdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "resdesc")).ToString();


                if (subcode != "000000000000")
                {
                    hlnkAccdesc1.NavigateUrl = "~/F_17_Acc/LinkAccSpLedger.aspx?Type=DetailLedger&Date1=" + Convert.ToDateTime(this.txtdate.Text).AddDays(-90).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy") + "&sircode=" + subcode + "&sirdesc=" + subdesc;



                }
                else
                {
                    hlnkAccdesc1.NavigateUrl = "~/F_17_Acc/LinkAccLedger.aspx?Type=Ledger&RType=GLedger&Date1=" + Convert.ToDateTime(this.txtdate.Text).AddDays(-90).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy") + "&sircode=" + subcode + "&actcode=" + actcode;



                }

            }
        }


        protected void dgv1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.dgv1.EditIndex = -1;
            this.Data_Bind();
        }
        protected void dgv1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.dgv1.EditIndex = e.NewEditIndex;
            this.Data_Bind();


            try
            {
                string comcod = this.GetCompCode();
                string ttsrch = "%";
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string UserId = hst["usrid"].ToString();
                string Cactcode = ((Label)this.dgv1.Rows[e.NewEditIndex].FindControl("lblgvCactCod")).Text.Trim();

                DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "GETCONACCHEAD", ttsrch, UserId, "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;

                if (ds2.Tables[0].Rows.Count == 0)
                    return;

                DropDownList ddl2 = (DropDownList)this.dgv1.Rows[e.NewEditIndex].FindControl("ddlCactcode");
                ddl2.DataTextField = "cactdesc";
                ddl2.DataValueField = "cactcode";
                ddl2.DataSource = ds2.Tables[0];
                ddl2.DataBind();
                ddl2.SelectedValue = Cactcode;

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }






        }
        protected void dgv1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            DataTable tbl1 = (DataTable)ViewState["tbChqSign"];

            string Cactcode = ((DropDownList)this.dgv1.Rows[e.RowIndex].FindControl("ddlCactcode")).SelectedValue.ToString();
            string Cactdesc = ((DropDownList)this.dgv1.Rows[e.RowIndex].FindControl("ddlCactcode")).SelectedItem.Text.Trim();
            string Chequeno = ((TextBox)this.dgv1.Rows[e.RowIndex].FindControl("txtgvChqNo")).Text.Trim();

            double Amount = Convert.ToDouble("0" + ((TextBox)this.dgv1.Rows[e.RowIndex].FindControl("txtgvAmount")).Text.Trim());
            string Chqdate = ((TextBox)this.dgv1.Rows[e.RowIndex].FindControl("txtgvChqdate")).Text.Trim();
            //string Narration = ((TextBox)this.dgv1.Rows[e.RowIndex].FindControl("lblgvNarr")).Text.Trim();


            int index = (this.dgv1.PageIndex) * this.dgv1.PageSize + e.RowIndex;

            tbl1.Rows[index]["cactcode"] = Cactcode;
            tbl1.Rows[index]["cactdesc"] = Cactdesc;
            tbl1.Rows[index]["chequeno"] = Chequeno;
            tbl1.Rows[index]["amount"] = Amount;
            //  tbl1.Rows[index]["narr"] = Narration;

            ViewState["tbChqSign"] = tbl1;
            this.dgv1.EditIndex = -1;
            this.Data_Bind();
        }

        private void Session_tbChqSign_Update()
        {
            DataTable tbl1 = (DataTable)ViewState["tbChqSign"];
            int index = 0;

            foreach (GridViewRow gv1 in dgv1.Rows)
            {

                string Chequeno = ((TextBox)gv1.FindControl("txtgvChqNo")).Text.Trim();
                double Amount = Convert.ToDouble("0" + ((TextBox)gv1.FindControl("txtgvAmount")).Text.Trim());
                string Chqdate = ((TextBox)gv1.FindControl("txtgvChqdate")).Text.Trim();
                // index = (this.dgv1.PageSize) * (this.dgv1.PageIndex) + j;
                tbl1.Rows[index]["chequeno"] = Chequeno;
                tbl1.Rows[index]["amount"] = Amount;
                tbl1.Rows[index]["chqdate"] = Chqdate;
                index++;


            }



            ViewState["tbChqSign"] = tbl1;
        }


        //protected void lbtnFinalUpdate_Click(object sender, EventArgs e)
        //{
        //    try
        //    {

        //        Hashtable hst = (Hashtable)Session["tblLogin"];
        //        string comcod = hst["comcod"].ToString();
        //        string EditByid = hst["usrid"].ToString();
        //        string Edittrmid = hst["compname"].ToString();
        //        string EditSession = hst["session"].ToString();
        //        string Editdat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");






        //        DataTable dt1 = (DataTable)Session["tbChqSign"];
        //        string Type = this.Request.QueryString["Type"].ToString();

        //        if (Type == "VenSelect")
        //        {

        //            switch (comcod)
        //            {
        //                case "3301":
        //                case "1301":
        //                case "2301":
        //                    break;

        //                default:
        //                    foreach (DataRow dr2 in dt1.Rows)
        //                    {

        //                        if (Convert.ToDouble(dr2["mreqrat"]) < Convert.ToDouble(dr2["reqrat"]))
        //                        {

        //                         ((Label)this.Master.FindControl("lblmsg")).Text = "Rate Equal or Below Aproved  Rate";
        //                            return;
        //                        }

        //                    }

        //                    break;

        //            }

        //        }
        //        bool result = false;




        //        string Reqno1 = "XXXXXXXXXXXXXX";

        //        foreach (DataRow dr in dt1.Rows)
        //        {

        //            string gpsl = dr["gpsl"].ToString();
        //            if (gpsl == "2") continue;
        //            string Reqno = dr["reqno"].ToString();
        //            string mRSIRCODE = dr["rsircode"].ToString();
        //            string mSPCFCOD = dr["spcfcod"].ToString();
        //            double mPREQTY = Convert.ToDouble(dr["preqty"]);
        //            double mAREQTY = Convert.ToDouble(dr["areqty"]);
        //            string mREQRAT = dr["reqrat"].ToString();
        //            string mEXPUSEDT = dr["expusedt"].ToString();
        //            string mREQNOTE = dr["reqnote"].ToString();
        //            string PursDate = dr["pursdate"].ToString();
        //            string Lpurrate = dr["lpurrate"].ToString();
        //            string storecode = dr["storecode"].ToString();
        //            string ssircode = dr["ssircode"].ToString();
        //            string orderno = dr["orderno"].ToString();

        //            if (mPREQTY >= mAREQTY)
        //            {

        //                result = accData.UpdateTransInfo(comcod, "SP_ENTRY_REQUISITION_APPROVAL", "UPDATEPURREQAINF", Reqno, mRSIRCODE, mSPCFCOD, mPREQTY.ToString(), mAREQTY.ToString(), mREQRAT, mEXPUSEDT, mREQNOTE,
        //                            PursDate, Lpurrate, storecode, ssircode, orderno, "", "");
        //                if (!result)
        //                {
        //                 ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
        //                    return;
        //                }
        //            }




        //            else
        //            {
        //             ((Label)this.Master.FindControl("lblmsg")).Text = "Aprove Qty Must be Less Or Equal  Req. Qty";
        //                return;

        //            }






        //            if (Reqno != Reqno1)
        //            {
        //                result = accData.UpdateTransInfo(comcod, "SP_ENTRY_REQUISITION_APPROVAL", "UPDATEREQNO", Reqno, EditByid, Editdat, Edittrmid, EditSession, "", "", "", "", "", "", "", "", "", "");


        //                if (!result)
        //                {
        //                 ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
        //                    return;
        //                }
        //            }

        //            Reqno1 = Reqno;


        //        }




        //     ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";


        //    }
        //    catch (Exception ex)
        //    {
        //     ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
        //    }


        //}
        private void Bankcode()
        {

            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string UserId = hst["usrid"].ToString();
                string comcod = this.GetCompCode();
                string ttsrch = "%" + this.txtserchBankName.Text + "%";
                DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "GETCONACCHEAD", ttsrch, UserId, "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;

                if (ds2.Tables[0].Rows.Count == 0)
                    return;
                this.ddlBankName.DataTextField = "cactdesc";
                this.ddlBankName.DataValueField = "cactcode";
                this.ddlBankName.DataSource = ds2.Tables[0];
                this.ddlBankName.DataBind();


                this.ChequeNo();

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }
        protected void imgbtnSrchBank_Click(object sender, EventArgs e)
        {
            this.Bankcode();
        }





        protected void ddlBankName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ChequeNo();
        }
        private string GetRecciptVoucher()
        {

            string comcod = this.GetCompCode();
            string recvou = "";
            switch (comcod)
            {
                case "1103":

                    recvou = "Receipt Voucher";
                    break;
                default:
                    recvou = "Deposit Voucher";
                    break;
            }


            return recvou;


        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                //  DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);

                bool resulta = false;
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

                ((Label)this.Master.FindControl("lblmsg")).Visible = true;

                if (!Convert.ToBoolean(dr1[0]["entry"]))
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
                //this.Session_tbChqSign_Update();
                this.SaveValue();

                string acvounum = "";
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();

                string userid = hst["usrid"].ToString();
                string Terminal = hst["compname"].ToString();
                string Sessionid = hst["session"].ToString();
                //string code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();

                string slnum = lstBillList.SelectedValue.ToString().Substring(0, 9);//code.Substring(0, 9);
                string actcode = "";
                string rescode = "";
                //string chequeno = code.Substring(33).ToString();
                string chequeno = this.txtRefNum.Text.Trim();// this.ddlcheque.SelectedValue.ToString();
                string cactcode = this.ddlBankName.SelectedValue.ToString();
                string vtcode = "";
                /////////////////////////////////////////////////////////
                DataTable dt = (DataTable)ViewState["tbChqSign"];
                DataTable dt1 = dt.Copy();
                DataView dv = dt1.DefaultView;
                dv.RowFilter = ("slnum='" + slnum + "'");
                dt1 = dv.ToTable();


                DataRow[] drt = dt.Select("tax>0");
                bool isjv = drt.Length == 0 ? false : true;



                // Check Existing Voucher


                DataSet dschk = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "EXISTINGPAYID", slnum, "", "", "", "", "", "", "", "");
                if (dschk.Tables[0].Rows[0]["vounum"].ToString() != "00000000000000")
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Voucher No already Existing in this Payment ID";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }



                /////////////////////////////////////////////////

                foreach (DataRow dr2 in dt1.Rows)
                {

                    if (Convert.ToDouble(dr2["amount"]) <= 0)
                    {

                        ((Label)this.Master.FindControl("lblmsg")).Text = "Please Input Amount";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }

                    if (Convert.ToDouble(dr2["apamt1"]) < Convert.ToDouble(dr2["amount"]))
                    {

                        ((Label)this.Master.FindControl("lblmsg")).Text = "Amount Equal or Below Aproved  Amount";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }

                }




                string voudat = ASTUtility.DateFormat(this.txtdate.Text);
                DateTime Bdate = this.GetBackDate();
                bool dcon = ASITUtility02.TransactionDateCon(Bdate, Convert.ToDateTime(voudat));
                if (!dcon)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Issue Date is equal or less Current Date');", true);
                    return;
                }

                //Voucher Number
                if (chkCrVou.Checked || isjv)
                {
                    try
                    {
                        DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETOPENINGDATE", "", "", "", "", "", "", "", "", "");
                        if (ds2.Tables[0].Rows.Count == 0)
                        {
                            return;
                        }
                        DateTime txtopndate = Convert.ToDateTime(ds2.Tables[0].Rows[0]["voudat"]);

                        if (txtopndate >= Convert.ToDateTime(this.txtdate.Text.Trim().Substring(0, 11)))
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                            ((Label)this.Master.FindControl("lblmsg")).Text = "Voucher Date Must  Be Greater then Opening Date";
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                            return;
                        }

                        string ConAccHead = this.ddlBankName.SelectedValue.ToString();
                        string vactcode = dt1.Rows[0]["actcode"].ToString();
                        string VNo1 = isjv ? "J" : ((vactcode.Substring(0, 2) == "19" || vactcode.Substring(0, 2) == "29") ? "C" : ConAccHead.Substring(0, 4) == "1901" ? "C" : "B");
                        string VNo2 = isjv ? "V" : ((vactcode.Substring(0, 2) == "19" || vactcode.Substring(0, 2) == "29") ? "T" : "D");
                        string VNo3 = Convert.ToString(VNo1 + VNo2);
                        vtcode = (VNo3 == "CT") ? "92" : "99";

                        string entrydate = this.txtdate.Text.Substring(0, 11).Trim();
                        DataSet ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETNEWVOUCHER", entrydate, VNo3, "", "", "", "", "", "", "");
                        DataTable dt4 = ds4.Tables[0];
                        acvounum = dt4.Rows[0]["couvounum"].ToString();
                        lblVoun.Text = dt4.Rows[0]["couvounum"].ToString();
                        string pvno1 = ds4.Tables[1].Rows[0]["lastvounum"].ToString().Trim();

                    }
                    catch (Exception ex)
                    {

                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + ex.Message + "');", true);

                    }

                }
                else
                {
                    DataSet ds5 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "GETNEWVOUCHER", voudat, "PV", "", "", "", "", "", "", "");
                    ViewState["NEWVOU"] = ds5.Tables[0];
                    DataTable dt12 = (DataTable)ViewState["NEWVOU"];

                    if (this.Request.QueryString["Type"] == "Acc")
                    {

                        acvounum = dt12.Rows[0]["couvounum"].ToString();

                    }
                    else
                    {
                        foreach (DataRow dr2 in dt1.Rows)
                        {
                            acvounum = dr2["newvocnum"].ToString();

                        }
                    }
                    lblVoun.Text = dt12.Rows[0]["couvounum"].ToString();
                }



                /////////////////////////////////////////////////////////
                string vounarration1 = "";
                string vounarration2 = "";


                vounarration1 = this.txtNarration.Text;

                vounarration2 = (vounarration1.Length > 200 ? vounarration1.Substring(200) : "");
                vounarration1 = (vounarration1.Length > 200 ? vounarration1.Substring(0, 200) : vounarration1);
                string mAPROVDAT = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");

                //Log Entry
                string tblPostedByid = "";
                string tblPostedtrmid = "";
                string tblPostedSession = "";
                string tblPosteddat = "01-Jan-1900";
                string PostedByid = (this.Request.QueryString["Type"] == "Acc") ? userid : (tblPostedByid == "") ? userid : tblPostedByid;
                string Posttrmid = (this.Request.QueryString["Type"] == "Acc") ? Terminal : (tblPostedtrmid == "") ? Terminal : tblPostedtrmid;
                string PostSession = (this.Request.QueryString["Type"] == "Acc") ? Sessionid : (tblPostedSession == "") ? Sessionid : tblPostedSession;
                string Posteddat = (this.Request.QueryString["Type"] == "Acc") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : (tblPosteddat == "01-Jan-1900") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : tblPosteddat;
                string EditByid = (this.Request.QueryString["Type"] == "Acc") ? "" : userid;
                string Editdat = (this.Request.QueryString["Type"] == "Acc") ? "01-Jan-1900" : System.DateTime.Today.ToString("dd-MMM-yyyy");

                string pounaction = "";
                string aprovbyid = "";
                string aprvtrmid = "";
                string aprvseson = "";
                string aprvdat = "01-jan-1900";
                string srinfo = "";
                string edit = "";
                string rbankname = "";
                string chequedat = Convert.ToDateTime(dt1.Rows[0]["chqdate"]).ToString("dd-MMM-yyyy");
                string userdate = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

                string sameChqval = this.SameChqValue(chequeno.ToString().Trim().ToUpper());
                string payeeType = this.ddlpayeelist.SelectedValue.ToString();

                //string voutype = "Online Payment Voucher";
                string voutype = "";

                if (chkCrVou.Checked || isjv)
                {
                    try
                    {
                        string recvou = this.GetRecciptVoucher();
                        voutype = (ASTUtility.Left(acvounum, 2) == "JV" ? "Journal Voucher" :
                                 (ASTUtility.Left(acvounum, 2) == "CT" ? "Contra Voucher" :
                                 (ASTUtility.Left(acvounum, 2) == "CD" ? "Cash Payment Voucher" :
                                 (ASTUtility.Left(acvounum, 2) == "BD" ? "Bank Payment Voucher" :
                                 (ASTUtility.Left(acvounum, 2) == "CC" ? ("Cash " + recvou) :
                                 (ASTUtility.Left(acvounum, 2) == "BC" ? ("Bank " + recvou) : "Unknown Voucher"))))));




                        string Payto = this.txtPayto.Text;
                        string vouno = acvounum.Substring(0, 2);


                        if ((this.Request.QueryString["Type"] == "Acc"))
                        {
                            if ((vouno == "BD" || vouno == "CT" || vouno == "JV") && cactcode.Substring(0, 4) != "1901")
                            {
                                switch (comcod)
                                {
                                    case "3101":
                                    case "3354":
                                        if (chequeno == "")
                                        {
                                            ((Label)this.Master.FindControl("lblmsg")).Text = "Cheque no Required ...";
                                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                                            return;
                                        }
                                        break;
                                    default:
                                        break;
                                }
                                if (chequeno != "")
                                {
                                    // todo for rtgs 
                                    string chkref = this.SameChqValue(chequeno);
                                    if (chkref.ToString().Trim().ToUpper() != sameChqval)
                                    {
                                        DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "CHEQUENOCHECK", chequeno, "", "", "", "", "", "", "", "");
                                        if (ds1.Tables[0].Rows.Count > 0)
                                        {
                                            ((Label)this.Master.FindControl("lblmsg")).Text = "This Cheque no is already exist.";
                                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                                            return;
                                        }
                                    }
                                }
                            }
                        }



                        //-----------Update Transaction B Table-----------------//
                        //bool resultb = accData.UpdateTransInfo2(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", acvounum, voudat, chequeno, "", vounarration1,
                        //                vounarration2, voutype, vtcode, "EDIT", PostedByid, Posttrmid, PostSession, Posteddat, EditByid, Editdat, payto, slnum, "", "", "", "");

                        bool resultb = accData.UpdateTransHREMPInfo3(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE02", acvounum, voudat, chequeno, srinfo, vounarration1,
                       vounarration2, voutype, vtcode, edit, PostedByid, Posttrmid, PostSession, Posteddat, EditByid, Editdat, Payto, slnum, aprovbyid, aprvtrmid, aprvseson, aprvdat, pounaction, rbankname, chequedat, "", payeeType);

                        if (!resultb)
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                            return;
                        }

                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            slnum = dt1.Rows[i]["slnum"].ToString();
                            actcode = dt1.Rows[i]["actcode"].ToString();
                            rescode = dt1.Rows[i]["rescode"].ToString();
                            string spclcode = dt1.Rows[i]["spcfcod"].ToString();
                            string trnqty = "0.00";
                            string trnamt = Convert.ToDouble(dt1.Rows[i]["amount"]).ToString();
                            string trnremarks = "";// dt1.Rows[i]["refno"].ToString();
                            string recndt = "01-Jan-1900";
                            string rpcode = "";
                            string billno = dt1.Rows[i]["billno"].ToString();
                            double taxamt = Convert.ToDouble(dt1.Rows[i]["tax"]);



                            resulta = accData.UpdateTransHREMPInfo3(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE02", acvounum, actcode, rescode, cactcode,
                                                           voudat, trnqty, trnremarks, vtcode, trnamt, spclcode, recndt, rpcode, billno, userid, userdate, Terminal, "", "", "", "", "", "", "", "", "", "");
                            if (!resulta)
                            {
                                ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                                return;
                            }

                            //Tax

                            if (taxamt > 0)
                            {

                                //  && actcode.Substring(0, 4) <= "2602"
                                string tvsactcode = ((actcode.Substring(0, 2) == "26") ? ("23" + actcode.Substring(2)) : "239800010001");
                                string tvsrescode = "970100101001";
                                taxamt = taxamt * -1;
                                resulta = accData.UpdateTransHREMPInfo3(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE02", acvounum, tvsactcode, tvsrescode, cactcode,
                                voudat, trnqty, trnremarks, vtcode, taxamt.ToString(), spclcode, recndt, rpcode, billno, userid, userdate, Terminal, "", "", "", "", "", "", "", "", "", "");

                            }
                            if (!resulta)
                            {
                                ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                                return;
                            }
                            resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "UPDATEPROAPP", slnum, actcode, rescode, acvounum, billno, chequedat, "", "", "", "", "", "", "", "");

                        }

                       



                        // Another Part of Journal
                        if (isjv)
                        {
                            double netam = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(netamt)", "")) ? 0.00 : dt.Compute("Sum(netamt)", "")));
                            netam = netam * -1;
                            string conactcode = this.ddlBankName.SelectedValue.ToString();
                            rescode = "000000000000";
                            string spclcode = "000000000000";
                            string trnqty = "0";
                            string trnremarks = "";
                            string recndt = "01-Jan-1900";
                            string rpcode = "";
                            string billno = "";

                            resulta = accData.UpdateTransHREMPInfo3(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE02", acvounum, conactcode, rescode, cactcode,
                            voudat, trnqty, trnremarks, vtcode, netam.ToString(), spclcode, recndt, rpcode, billno, userid, userdate, Terminal, "", "", "", "", "", "", "", "", "", "");
                            if (!resulta)
                            {
                                ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                                return;
                            }
                        }


                        if ((ASTUtility.Left(acvounum, 2) == "BD") || (ASTUtility.Left(acvounum, 2) == "JV") || (ASTUtility.Left(acvounum, 2) == "CT"))
                        {
                            bool resultd = accData.UpdateTransInfo2(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "UPDATECHQLIST", cactcode, chequeno, acvounum, "", "",
                                           "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                        }

                    }
                    catch (Exception ex)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    }
                }



                //////////////////////////////PDC----------------------------------------------------------------------------

                else
                {
                    try
                    {
                        string vouno = acvounum.Substring(0, 2);
                        if ((this.Request.QueryString["Type"] == "Acc"))
                        {
                            switch (comcod)
                            {
                                case "3101":
                                case "3354":
                                    if (chequeno == "")
                                    {
                                        ((Label)this.Master.FindControl("lblmsg")).Text = "Cheque no Required ...";
                                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                                        return;
                                    }
                                    break;
                                default:
                                    break;
                            }
                            if (chequeno != "")
                            {
                                DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "CHEQUENOCHECK", chequeno, "", "", "", "", "", "", "", "");
                                if (ds1.Tables[0].Rows.Count > 0)
                                {
                                    ((Label)this.Master.FindControl("lblmsg")).Text = "This Cheque no is already exist.";
                                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                                    return;
                                }
                            }
                        }


                        voutype = "Payment Voucher";
                        //-----------Update Payment B Table-----------------//
                        bool resultb = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "INOFUPOLACPMNTB", acvounum, voudat, vounarration1,
                                        vounarration2, voutype, "99", "EDIT", PostedByid, Posttrmid, PostSession, Posteddat, EditByid, Editdat, "", "");
                        if (!resultb)
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                            return;
                        }
                        //-----------Update Online Payment  A Table-----------------//



                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            slnum = dt1.Rows[i]["slnum"].ToString();
                            actcode = dt1.Rows[i]["actcode"].ToString();
                            rescode = dt1.Rows[i]["rescode"].ToString();
                            //chequeno = dt1.Rows[i]["chequeno"].ToString();

                            string chequedate = Convert.ToDateTime(dt1.Rows[i]["chqdate"]).ToString("dd-MMM-yyyy");
                            string Dramt = Convert.ToDouble(dt1.Rows[i]["amount"]).ToString();
                            string trnremarks = dt1.Rows[i]["refno"].ToString();// +dt.Rows[i]["remarks"].ToString();
                            string payto = dt1.Rows[i]["payto"].ToString();
                            string billno = dt1.Rows[i]["billno"].ToString();


                            resulta = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "INOFUPOLACPMNTA", acvounum, actcode, rescode, chequeno, cactcode,
                                          voudat, Dramt, chequedate, trnremarks, "99", payto, slnum, "00000000000000", billno, "");
                            if (!resulta)
                            {
                                ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                                return;
                            }

                            bool resultpa = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "UPDATEPROAPP", slnum, actcode, rescode, acvounum, billno, chequedat, "", "", "", "", "", "", "", "", "");

                        }

                        bool resultd = accData.UpdateTransInfo2(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "UPDATECHQLIST", cactcode, chequeno, acvounum, "", "",
                                        "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");


                    }
                    catch (Exception ex)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    }

                }



                for (int i = 0; i < dt.Rows.Count; i++)
                {



                    string slnum1 = dt.Rows[i]["slnum"].ToString();
                    string chequeno1 = dt.Rows[i]["chequeno"].ToString();

                    if (slnum == slnum1)
                    {


                        dt.Rows[i]["newvocnum"] = acvounum.Substring(0, 2) + acvounum.Substring(6, 2) + "-" + acvounum.Substring(8);
                        //dt1.Rows[i]["recndt"] = recondat;

                    }

                }
                ViewState["tbChqSign"] = dt;
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully.";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                this.btnUpdate.Visible = false;

                ((LinkButton)this.Master.FindControl("lnkPrint")).Focus();
                string eventdesc = "Voucher: " + " Dated: " + this.txtdate.Text.Trim();
                string eventdesc2 = "";//this.txtNarration.Text.Trim();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), "", eventdesc, eventdesc2);

                this.Data_Bind();

                // Auto Voucher Print
                switch (comcod)
                {
                    case "3336":
                    case "3337":
                        this.PrintchKSuvastu();
                        break;
                    default:
                        break;

                }


                string ccactcode = this.ddlBankName.SelectedValue.ToString();
                Cache.Insert("cactcode", ccactcode, null, DateTime.Now.AddHours(2), TimeSpan.Zero);




            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }

        //Same Cheque No
        private string SameChqValue(string refno1)
        {
            string sameChqval = "";
            switch (refno1)
            {
                case "BT":
                    sameChqval = "BT";
                    break;
                case "RTGS":
                    sameChqval = "RTGS";
                    break;
                case "BFTEN":
                    sameChqval = "BFTEN";
                    break;
                case "N/A":
                    sameChqval = "N/A";
                    break;
                case "ONLINEDEPOSIT":
                    sameChqval = "ONLINEDEPOSIT";
                    break;
                default:
                    sameChqval = "";
                    break;
            }
            return sameChqval;
        }
        protected void ddlcheque_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlcheque.Items.Count == 0)
                return;
            this.txtRefNum.Text = this.ddlcheque.SelectedItem.Text;
        }


        private void getChequeIssue()
        {
            try
            {
               
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string Date = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
                string Issueno = Request.QueryString["slnum"].ToString() == "" ? "" : Request.QueryString["slnum"].ToString();
                string billno = Request.QueryString["billno"].ToString() == "" ? "" : Request.QueryString["billno"].ToString();

                string pactcode = ((Request.QueryString["actcode"].ToString() == "000000000000") ? "" : Request.QueryString["actcode"].ToString()) + "%";
                DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "CHEQUEISSUEPRINT", Issueno, billno, "", "", "", "");
                if (ds1.Tables[0].Rows.Count == 0)
                    return;

                ViewState["dscheqeinfo"] = ds1;
                this.printChequeIssue();

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error :" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }

        private void printChequeIssue()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");


            DataSet ds1 = (DataSet)ViewState["dscheqeinfo"];
            DataTable dt1 = ds1.Tables[0];
            DataTable dt2 = ds1.Tables[1];
            DataTable dt3 = ds1.Tables[2];

            string paytype = "Pay Type : " + dt3.Rows[0]["paytype"].ToString();
            string payto = "Pay To : " + dt3.Rows[0]["payto"].ToString();
            string reqnar = "Narration : " + dt3.Rows[0]["reqnar"].ToString();
            string date1 = "Req Date : " + Convert.ToDateTime(dt3.Rows[0]["reqdat"]).ToString("dd-MM-yyyy");

            string billno = Request.QueryString["billno"].ToString() == "" ? "" : Request.QueryString["billno"].ToString();
            string payid = Request.QueryString["slnum"].ToString() == "" ? "" : Request.QueryString["slnum"].ToString();


            string sign1 = dt2.Rows[0]["entryuser"].ToString() + "\n" + dt2.Rows[0]["entrydesig"].ToString() + "\n" + Convert.ToDateTime(dt2.Rows[0]["entryDate"]).ToString("dd-MM-yyyy");
            string sign2 = dt2.Rows[0]["chkuser"].ToString() + "\n" + dt2.Rows[0]["chkuserdesig"].ToString() + "\n" + Convert.ToDateTime(dt2.Rows[0]["chkdate"]).ToString("dd-MM-yyyy");
            string sign3 = dt2.Rows[0]["fruser"].ToString() + "\n" + dt2.Rows[0]["fruserdesig"].ToString() + "\n" + Convert.ToDateTime(dt2.Rows[0]["frdate"]).ToString("dd-MM-yyyy");
            string sign4 = dt2.Rows[0]["aprvuser"].ToString() + "\n" + dt2.Rows[0]["aprvuserdesig"].ToString() + "\n" + Convert.ToDateTime(dt2.Rows[0]["aprvdate"]).ToString("dd-MM-yyyy");

            var list = dt1.DataTableToList<RealEntity.C_14_Pro.EClassPur.ChequeSheet01>();

            LocalReport rpt = new LocalReport();
            rpt = RptSetupClass1.GetLocalReport("R_15_DPayReg.RptChequeIssue", list, null, null);
            rpt.EnableExternalImages = true;
            rpt.SetParameters(new ReportParameter("compName", comnam));
            rpt.SetParameters(new ReportParameter("txtTitle", "Bill Information"));
            rpt.SetParameters(new ReportParameter("date1", date1));

            rpt.SetParameters(new ReportParameter("paytype", paytype));
            rpt.SetParameters(new ReportParameter("payto", payto));
            rpt.SetParameters(new ReportParameter("reqnar", reqnar));
            rpt.SetParameters(new ReportParameter("billno", "Bill No: " + billno));
            rpt.SetParameters(new ReportParameter("payid", "Pay Id : " + payid));

            rpt.SetParameters(new ReportParameter("txtsign1", sign1));
            rpt.SetParameters(new ReportParameter("txtsign2", sign2));
            rpt.SetParameters(new ReportParameter("txtsign3", sign3));
            rpt.SetParameters(new ReportParameter("txtsign4", sign4));

            rpt.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            rpt.SetParameters(new ReportParameter("comLogo", ComLogo));

            Session["Report1"] = rpt;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
        }


    }
}


