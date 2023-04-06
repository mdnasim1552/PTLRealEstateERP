using System;
using System.Collections;
using System.Collections.Generic;
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

using RealEntity.C_17_Acc;
using RealERPLIB;

using Microsoft.Reporting.WinForms;
using RealERPRDLC;
namespace RealERPWEB.F_22_Sal
{

    public partial class AccSumCustPayStatus : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        //decimal cinsamount = 0;
        //decimal payamount = 0;
        ProcessAccess purData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;

                //Hashtable hst = (Hashtable)Session["tblLogin"];
                //if ((!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp),
                //        (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean(hst["permission"]))
                //    Response.Redirect("~/AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));


                this.txFdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                CommonButton();

                this.showClLedger();

                //this.lblHeadtitle.Text = (this.Request.QueryString["Type"].ToString() == "ClLedger") ? "Client Ledger Report" : "CUSTOMER PAYMENT STATUS";
                //  ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                string Type = "ClLedger";// this.Request.QueryString["Type"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = Type == "ClLedger" ? "Client Ledger" : Type == "ClPayDetails" ? "Client Payment Details" : "PAYMENT STATUS";
                //Type == "RecPayABal" ? "RECEIVED AND PAYMENT STATUS" : "CUSTOMER PAYMENT STATUS"; // "RECEIVED AND PAYMENT STATUS";

            }
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

            //string Type = this.Request.QueryString["Type"].ToString();


            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }




        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        private void showClLedger()
        {
            string comcod = this.Request.QueryString["Comcod"].ToString();
            //string pactcode = this.ddlProjectName.SelectedValue.ToString();
            // string custid = this.ddlCustName.SelectedValue.ToString();
            string Date = Convert.ToDateTime(this.txFdate.Text).ToString("dd-MMM-yyyy");
            string custid = this.Request.QueryString["CustID"].ToString();
            string pactcode = this.Request.QueryString["Pid"].ToString();

            string acccomcod = this.GetComeCode();
            string CallType = "INSTALLMANTWITHMRR";
            string spname = "";
            if (acccomcod == "3349")
            {
                spname = "SP_REPORT_ACCOUNTSMAP_CRD";
            }
            else
            {

                spname = "SP_REPORT_ACCOUNTSMAP";
            }


            DataSet ds2 = purData.GetTransInfo(comcod, spname, CallType, pactcode, custid, Date, acccomcod, "", "", "", "", "");
            //DataSet ds2= purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT01", "INSTALLMANTWITHMRR", pactcode, custid, Date, "", "", "", "", "", "");

            if (ds2 == null)
            {
                this.gvCustLedger.DataSource = null;
                this.gvCustLedger.DataBind();
                return;
            }





            Session["tblCustPayment03"] = this.HiddenSameDate2(ds2.Tables[0]);
            Session["tblCustPayment01"] = ds2.Tables[1];

            this.txtCustName.Text = "Project : " + ds2.Tables[1].Rows[0]["actdesc"].ToString() + ", Name : " + ds2.Tables[1].Rows[0]["name"].ToString() + ", Plot : " + ds2.Tables[1].Rows[0]["aptname"].ToString() + ", Unit : " + ds2.Tables[1].Rows[0]["aptsize"].ToString();
            DataTable dt = ds2.Tables[3];
            this.Data_Bind();

            if (acccomcod == "3349")
            {

                Session["tblCustPayment04"] = this.HiddenSameDate4(ds2.Tables[4]);
                Session["tblCustPayinfo"] = ds2.Tables[5];
                this.pnlhideCasSales.Visible = true;
                this.gvCustLedger2.DataSource = ds2.Tables[4];
                this.gvCustLedger2.DataBind();


            }
            else
            {

                this.pnlhidedataLink.Visible = true;
                Session["tblCustPayment04"] = ds2.Tables[4];
                this.gvAccCash.DataSource = ds2.Tables[4];
                this.gvAccCash.DataBind();
            }
            this.FooterCalculation();

        }



        private string calltype()
        {

            string comcod = this.GetComeCode();
            string calltype = "INSTALLMANTWITHMRR";


            return calltype;

        }

        private string procedure()
        {

            string comcod = this.GetComeCode();
            string procedure = "SP_ENTRY_SALSMGT";

            return procedure;

        }


        private DataTable HiddenSameData(DataTable dtable)
        {

            //if (dtable.Rows.Count == 0)
            //    return dtable;

            //string gcod = dt1.Rows[0]["gcod"].ToString();

            //for (int j = 1; j < dt1.Rows.Count; j++)
            //{
            //    if (dt1.Rows[j]["grp"].ToString() == "AA" && dt1.Rows[j]["gcod"].ToString() == gcod)
            //    {
            //        gcod = dt1.Rows[j]["gcod"].ToString();
            //        dt1.Rows[j]["gcod"] = "";
            //        dt1.Rows[j]["gdesc"] = "";
            //        dt1.Rows[j]["pactcode"] = "";
            //        dt1.Rows[j]["usircode"] = "";
            //        dt1.Rows[j]["schamt"] = 0;
            //        dt1.Rows[j]["schdate"] = "";
            //    }

            //    else
            //    {
            //        gcod = dt1.Rows[j]["gcod"].ToString();
            //    }

            //}
            //return dt1;
            //Session.Remove("tblCustPayment");
            string gcod = dtable.Rows[0]["gcod"].ToString();

            DataTable dt1 = dtable;
            DataView dv1 = dt1.DefaultView;
            dv1.RowFilter = "grp like 'AA' ";
            dt1 = dv1.ToTable();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["gcod"].ToString() == gcod)
                {
                    gcod = dt1.Rows[j]["gcod"].ToString();
                    dt1.Rows[j]["gcod"] = "";
                    dt1.Rows[j]["gdesc"] = "";
                    dt1.Rows[j]["pactcode"] = "";
                    dt1.Rows[j]["usircode"] = "";
                    //dt1.Rows[j]["schamt"] = 0;
                    dt1.Rows[j]["schdate"] = "01-Jan-1900";
                }

                else
                {
                    gcod = dt1.Rows[j]["gcod"].ToString();
                }

            }

            Session["tblCustPayment"] = dt1;


            //DataTable dt2 = dtable;
            //DataView dv2 = dt2.DefaultView;
            //dv2.RowFilter = "grp like 'BB' ";
            //dt2 = dv2.ToTable();
            //if (dt2 == null)
            //    return;

            return dt1;

        }
        private DataTable HiddenSameDate2(DataTable dtable)
        {

            Session.Remove("tblCustPayment03");
            string gcod = dtable.Rows[0]["gcod"].ToString();
            DataTable dt1 = dtable;

            DataView dv1 = dt1.DefaultView;
            dv1.RowFilter = "grp like 'AA' ";
            dt1 = dv1.ToTable();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["gcod"].ToString() == gcod)
                {
                    gcod = dt1.Rows[j]["gcod"].ToString();
                    dt1.Rows[j]["gcod"] = "";
                    dt1.Rows[j]["gdesc"] = "";
                    dt1.Rows[j]["pactcode"] = "";
                    dt1.Rows[j]["usircode"] = "";
                    dt1.Rows[j]["schamt"] = 0;
                    dt1.Rows[j]["asondues"] = 0;
                    dt1.Rows[j]["schdate"] = "01-Jan-1900";
                    dt1.Rows[j]["intdesc"] = "";
                }

                //else if (dt1.Rows[j]["grp"].ToString() == "AA")
                //{
                //    dt1.Rows[j]["intdesc"] = "";
                //}

                else
                {
                    gcod = dt1.Rows[j]["gcod"].ToString();
                }

            }

            //for (int j = 1; j < dt1.Rows.Count; j++)
            //{
            //    if (dt1.Rows[j]["grp"].ToString() == "AA" && dt1.Rows[j]["gcod"].ToString() == gcod)
            //    {
            //        gcod = dt1.Rows[j]["gcod"].ToString();
            //        dt1.Rows[j]["gcod"] = "";
            //        dt1.Rows[j]["gdesc"] = "";
            //        dt1.Rows[j]["pactcode"] = "";
            //        dt1.Rows[j]["usircode"] = "";
            //        dt1.Rows[j]["schamt"] = 0;
            //        dt1.Rows[j]["asondues"] = 0;
            //        dt1.Rows[j]["schdate"] = "01-Jan-1900";
            //    }
            //    else if (dt1.Rows[j]["grp"].ToString() == "AA")
            //    {
            //        dt1.Rows[j]["intdesc"] = "";
            //    }

            //    else
            //    {
            //        gcod = dt1.Rows[j]["gcod"].ToString();
            //    }

            //}

            int torow = dt1.Rows.Count - 1;

            if (torow > 0)
                for (int j = 0; j < dt1.Rows.Count; j++)
                {

                    if (j == torow)
                    {
                        double ppaidamt = Convert.ToDouble(dt1.Rows[j - 1]["paidamt"].ToString());

                        DateTime schdate = System.DateTime.Today;
                        //string schdate = System.DateTime.Now;

                        if (dt1.Rows[j - 1]["schdate"].ToString().Trim().Length > 0)
                        {
                            schdate = Convert.ToDateTime(dt1.Rows[j - 1]["schdate"]);
                        }


                        DateTime date = Convert.ToDateTime(this.txFdate.Text);


                        if (ppaidamt > 0)
                        {

                            double schamt = Convert.ToDouble(dt1.Rows[j]["schamt1"].ToString());
                            double paidamt = Convert.ToDouble(dt1.Rows[j]["paidamt"].ToString());
                            dt1.Rows[j]["balamt"] = schdate <= date ? 0.00 : schamt - (paidamt > 0 ? paidamt : 0.00);
                        }
                    }
                    else
                    {

                        double npaidamt = Convert.ToDouble(dt1.Rows[j + 1]["paidamt"].ToString());
                        if (npaidamt > 0)
                        {
                            double schamt = Convert.ToDouble(dt1.Rows[j]["schamt"].ToString());
                            double paidamt = Convert.ToDouble(dt1.Rows[j]["schamt"].ToString());
                            dt1.Rows[j]["balamt"] = schamt - paidamt;
                        }


                        else if (npaidamt < 0)
                        {

                            dt1.Rows[j]["balamt"] = 0.00;


                        }


                    }
                }


            return dt1;
            //Session["tblCustPayment"] = dt1;

        }

        private DataTable HiddenSameDate4(DataTable dtable)
        {
            if (dtable.Rows.Count == 0)
                return dtable;
            Session.Remove("tblCustPayment04");
            string gcod = dtable.Rows[0]["gcod"].ToString();
            string Type = "ClLedger";
            DataTable dt1 = dtable;

            switch (Type)
            {



                case "ClLedger":


                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == "AA" && dt1.Rows[j]["gcod"].ToString() == gcod)
                        {
                            gcod = dt1.Rows[j]["gcod"].ToString();
                            dt1.Rows[j]["gcod"] = "";
                            dt1.Rows[j]["gdesc"] = "";
                            dt1.Rows[j]["pactcode"] = "";
                            dt1.Rows[j]["usircode"] = "";
                            dt1.Rows[j]["schamt"] = 0;
                            dt1.Rows[j]["asondues"] = 0;
                            dt1.Rows[j]["schdate"] = "";
                        }

                        else
                        {
                            gcod = dt1.Rows[j]["gcod"].ToString();
                        }

                    }

                    int torow = dt1.Rows.Count - 1;

                    if (torow > 0)
                        for (int j = 0; j < dt1.Rows.Count; j++)
                        {

                            if (j == torow)
                            {
                                double ppaidamt = Convert.ToDouble(dt1.Rows[j - 1]["paidamt"].ToString());

                                DateTime schdate = System.DateTime.Today;
                                //string schdate = System.DateTime.Now;

                                if (dt1.Rows[j - 1]["schdate"].ToString().Trim().Length > 0)
                                {
                                    schdate = Convert.ToDateTime(dt1.Rows[j - 1]["schdate"]);
                                }

                                DateTime date = Convert.ToDateTime(this.txFdate.Text);

                                if (ppaidamt > 0)
                                {

                                    double schamt = Convert.ToDouble(dt1.Rows[j]["schamt1"].ToString());
                                    double paidamt = Convert.ToDouble(dt1.Rows[j]["paidamt"].ToString());
                                    dt1.Rows[j]["balamt"] = schdate <= date ? 0.00 : schamt - (paidamt > 0 ? paidamt : 0.00);
                                }
                            }
                            else
                            {

                                double npaidamt = Convert.ToDouble(dt1.Rows[j + 1]["paidamt"].ToString());
                                if (npaidamt > 0)
                                {
                                    double schamt = Convert.ToDouble(dt1.Rows[j]["schamt"].ToString());
                                    double paidamt = Convert.ToDouble(dt1.Rows[j]["schamt"].ToString());
                                    dt1.Rows[j]["balamt"] = schamt - paidamt;
                                }


                                else if (npaidamt < 0)
                                {

                                    dt1.Rows[j]["balamt"] = 0.00;


                                }


                            }
                        }

                    break;
            }


            return dt1;
            //Session["tblCustPayment"] = dt1;

        }
        private void Data_Bind()
        {
            string Type = "ClLedger";// this.Request.QueryString["Type"].ToString();

            DataTable dt = (DataTable)Session["tblCustPayment03"];
            //DataTable dt = this.HiddenSameData(dt2);


            if (Type == "ClLedger")
            {

                bool result = false;

                if (result == true)
                {
                    this.gvCustLedger.Columns[13].Visible = true;
                    this.gvCustLedger.Columns[14].Visible = false;
                }

                this.gvCustLedger.DataSource = dt;
                this.gvCustLedger.DataBind();
                //  this.FooterCalculation();
                Session["Report1"] = gvCustLedger;
                //   ((HyperLink)this.gvCustLedger.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }




        }


        private void FooterCalculation()
        {
            string comcod = this.GetComeCode();

            DataTable dt = (DataTable)Session["tblCustPayment03"];
            ((Label)this.gvCustLedger.FooterRow.FindControl("lblFscamt")).Text =
              Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(schamt)", "")) ?
              0.00 : dt.Compute("Sum(schamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvCustLedger.FooterRow.FindControl("lblFrcvamt")).Text =
             Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(paidamt)", "")) ?
             0.00 : dt.Compute("Sum(paidamt)", ""))).ToString("#,##0;(#,##0); ");
            double Schamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(schamt)", "")) ? 0.00 : dt.Compute("Sum(schamt)", "")));
            double rcvamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(paidamt)", "")) ? 0.00 : dt.Compute("Sum(paidamt)", "")));
            double balamt = Schamt - rcvamt;
            ((Label)this.gvCustLedger.FooterRow.FindControl("lblFBalTamt")).Text = balamt.ToString("#,##0;(#,##0); ");


            if (comcod == "3349")
            {
                DataTable dt2 = (DataTable)Session["tblCustPayment04"];
                if (dt2 == null || dt2.Rows.Count == 0)
                    return;




                ((Label)this.gvCustLedger2.FooterRow.FindControl("lblFscamt2")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(schamt)", "")) ? 0.00 : dt2.Compute("Sum(schamt)", ""))).ToString("#,##0;(#,##0); ");

                ((Label)this.gvCustLedger2.FooterRow.FindControl("lblFrcvamt2")).Text =
                Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(paidamt)", "")) ?
                0.00 : dt2.Compute("Sum(paidamt)", ""))).ToString("#,##0;(#,##0); ");

                double Schamtx = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(schamt)", "")) ? 0.00 : dt2.Compute("Sum(schamt)", "")));
                double rcvamtx = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(paidamt)", "")) ? 0.00 : dt2.Compute("Sum(paidamt)", "")));
                double balamtx = Schamtx - rcvamtx;
                ((Label)this.gvCustLedger2.FooterRow.FindControl("lblFBalTamt2")).Text = balamtx.ToString("#,##0;(#,##0); ");

                double ttschm = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(schamt)", "")) ? 0.00 : dt.Compute("Sum(schamt)", ""))) + Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(schamt)", "")) ? 0.00 : dt2.Compute("Sum(schamt)", "")));
                double ttlrecv = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(paidamt)", "")) ? 0.00 : dt.Compute("Sum(paidamt)", ""))) + Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(paidamt)", "")) ? 0.00 : dt2.Compute("Sum(paidamt)", "")));

                this.TotalbtnAppCost.InnerText = (ttschm).ToString("#,##0;(#,##0); ");
                this.TotalbtnAppRecived.InnerText = (ttlrecv).ToString("#,##0;(#,##0); ");
                this.TotalbtnAppBalance.InnerText = (ttschm - ttlrecv).ToString("#,##0;(#,##0); ");

            }


        }



        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            //IQBAL NAYAN
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            //this.PrintCleintLedger();
            this.printCustPayLedger();
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Print Report";
                string eventdesc2 = this.Request.QueryString["Type"].ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        private void printCustPayLedger()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string date = System.DateTime.Now.ToString("dd-MMM-yyyy");
            string session = hst["session"].ToString();
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;


            DataTable dt = (DataTable)Session["tblCustPayment03"];
            DataTable dt4 = (DataTable)Session["tblCustPayment04"];
            DataTable dt1 = (DataTable)Session["tblCustPayment01"];
            DataTable dtpay = (DataTable)Session["tblCustPayinfo"];

            string rptcustname = "", rptCustAdd = "", rptCustPhone = "", rptpactdesc = "", projAddress = "", rptUnitDesc = "", rptUsize = "", rptSalesteam = "", rptsalesdate = "", rptagreementdate = "", rptHandoverdate="";




            string custname = dt1.Rows[0]["name"].ToString();
            string prjname = dt1.Rows[0]["actdesc"].ToString();
            string plot = dt1.Rows[0]["aptname"].ToString();

            string txtTitle = hst["username"].ToString();

            string appCost = "", appRecv = "", appBal = "";

            if (comcod == "3349")
            {
                appCost = this.TotalbtnAppCost.InnerText.ToString();
                appRecv = this.TotalbtnAppRecived.InnerText.ToString();
                appBal = this.TotalbtnAppBalance.InnerText.ToString();

                //// string rptunittype = ds5.Tables[1].Rows[0]["unittype"].ToString();
                //string txtdate = "Print date: " + this.txtDate.Text.Trim();
                //string rptcustname = ds5.Tables[1].Rows[0]["name"].ToString();
                //string rptcustadd = ds5.Tables[1].Rows[0]["peraddress"].ToString();
                //string rptcustphone = ds5.Tables[1].Rows[0]["telephone"].ToString();
                //string rptpactdesc = ds5.Tables[1].Rows[0]["projectname"].ToString();
                //string projadd = ds5.Tables[1].Rows[0]["proadd"].ToString();

                //string rptunitdesc = ds5.Tables[1].Rows[0]["aptname"].ToString();
                //string rptusize = ds5.Tables[1].Rows[0]["aptsize"].ToString();
                //string rptsalesteam = ds5.Tables[1].Rows[0]["salesteam"].ToString();
                //string rptsalesdate = Convert.ToDateTime(ds5.Tables[1].Rows[0]["saledate"]).ToString("dd-MMM-yyyy");
                //string rptagreementdate = (Convert.ToDateTime(ds5.Tables[1].Rows[0]["agdate"]).ToString("dd-MMM-yyyy") == "01-jan-1900") ? "" : Convert.ToDateTime(ds5.Tables[1].Rows[0]["agdate"]).ToString("dd-MMM-yyyy");
                //string rpthandoverdate = (Convert.ToDateTime(ds5.Tables[1].Rows[0]["hoverdate"]).ToString("dd-MMM-yyyy") == "01-jan-1900") ? "" : Convert.ToDateTime(ds5.Tables[1].Rows[0]["hoverdate"]).ToString("dd-MMM-yyyy");


                rptcustname = dtpay.Rows[0]["name"].ToString();
                rptCustAdd = dtpay.Rows[0]["peraddress"].ToString();
                rptCustPhone = dtpay.Rows[0]["telephone"].ToString();
                rptpactdesc = dtpay.Rows[0]["projectname"].ToString();
                projAddress = dtpay.Rows[0]["proadd"].ToString();
                rptUnitDesc = dtpay.Rows[0]["aptname"].ToString();
                rptUsize = dtpay.Rows[0]["aptsize"].ToString();
                rptSalesteam = dtpay.Rows[0]["salesteam"].ToString();
                rptsalesdate = Convert.ToDateTime(dtpay.Rows[0]["saledate"]).ToString("dd-MMM-yyyy");
                rptagreementdate = (Convert.ToDateTime(dtpay.Rows[0]["agdate"]).ToString("dd-MMM-yyyy") == "01-jan-1900") ? "" : Convert.ToDateTime(dtpay.Rows[0]["agdate"]).ToString("dd-MMM-yyyy");
                rptHandoverdate = (Convert.ToDateTime(dtpay.Rows[0]["hoverdate"]).ToString("dd-MMM-yyyy") == "01-jan-1900") ? "" : Convert.ToDateTime(dtpay.Rows[0]["hoverdate"]).ToString("dd-MMM-yyyy");
            }


            var dtlist1 = dt.DataTableToList<RealEntity.C_22_Sal.EClassSales_02.AccCustPayLedgerCHL>();
            var dtlist2 = dt4.DataTableToList<RealEntity.C_22_Sal.EClassSales_02.AccCustPayLedgerCHL>();

            LocalReport Rpt1a = new LocalReport();
            Rpt1a = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptAccSumCustPayStatus", dtlist1, dtlist2, null);
            Rpt1a.SetParameters(new ReportParameter("comname", comnam));
            Rpt1a.SetParameters(new ReportParameter("txtComAddress", comadd));
            Rpt1a.SetParameters(new ReportParameter("date", "Date : " + date));

            Rpt1a.SetParameters(new ReportParameter("printFooter", printFooter));
            //Rpt1a.SetParameters(new ReportParameter("txtassum", "Assumed Value"));
            //Rpt1a.SetParameters(new ReportParameter("txthide", "Hide Cash"));
            Rpt1a.SetParameters(new ReportParameter("txtProjName", "Project - " + prjname));
            Rpt1a.SetParameters(new ReportParameter("txtname", "Customer Name : " + custname));
            Rpt1a.SetParameters(new ReportParameter("txtplot", "Flat : " + plot));
            Rpt1a.SetParameters(new ReportParameter("rptcustname", rptcustname));
            Rpt1a.SetParameters(new ReportParameter("rptCustAdd", rptCustAdd));
            Rpt1a.SetParameters(new ReportParameter("rptCustPhone", rptCustPhone));
            Rpt1a.SetParameters(new ReportParameter("rptpactdesc", rptpactdesc));
            Rpt1a.SetParameters(new ReportParameter("projAddress", projAddress));
            Rpt1a.SetParameters(new ReportParameter("rptUnitDesc", rptUnitDesc));
            Rpt1a.SetParameters(new ReportParameter("rptUsize", rptUsize));
            Rpt1a.SetParameters(new ReportParameter("rptSalesteam", rptSalesteam));
            Rpt1a.SetParameters(new ReportParameter("rptsalesdate", rptsalesdate));
            Rpt1a.SetParameters(new ReportParameter("rptagreementdate", rptagreementdate));
            Rpt1a.SetParameters(new ReportParameter("rptSalesteam", rptSalesteam));
            Rpt1a.SetParameters(new ReportParameter("rptHandoverdate", rptHandoverdate));
            Rpt1a.SetParameters(new ReportParameter("appCost", appCost));
            Rpt1a.SetParameters(new ReportParameter("appRecv", appRecv));
            Rpt1a.SetParameters(new ReportParameter("appBal", appBal));


            Session["Report1"] = Rpt1a;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                       ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintCleintLedger()
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            this.PrintCleintLedgergen(); ;

        }

        private string ClientCalltype()
        {
            bool result = false;
            string Calltype = "";
            switch (result)
            {
                case true:
                    Calltype = "RPTCLIENTLEDGER";
                    break;

                default:
                    Calltype = "INSTALLMANTWITHMRR";
                    break;


            }


            return Calltype;

        }

        private void PrintCleintLedgergen()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comcod = this.GetComeCode();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string custid = this.Request.QueryString["CustID"].ToString();
            string pactcode = this.Request.QueryString["Pid"].ToString();

            string Date = Convert.ToDateTime(this.txFdate.Text).ToString("dd-MMM-yyyy");

            string CallType = this.ClientCalltype();
            DataSet ds5 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT01", CallType, pactcode, custid, Date, "", "", "", "", "", "");

            DataTable tblins = this.HiddenSameDate2(ds5.Tables[0]);

            double aprment = Convert.ToDouble(ds5.Tables[1].Rows[0]["aptprice"]);
            double carparking = Convert.ToDouble(ds5.Tables[1].Rows[0]["carparking"]);
            double utility = Convert.ToDouble(ds5.Tables[1].Rows[0]["utility"]);
            double modicharge = Convert.ToDouble(ds5.Tables[1].Rows[0]["adwrk"]);
            double delcharge = Convert.ToDouble(ds5.Tables[1].Rows[0]["delchg"]);
            double regisfee = Convert.ToDouble(ds5.Tables[1].Rows[0]["regavat"]);
            double assciationfee = Convert.ToDouble(ds5.Tables[1].Rows[0]["devcharge"]);
            double transfee = Convert.ToDouble(ds5.Tables[1].Rows[0]["transfee"]);
            double bgcost = Convert.ToDouble(ds5.Tables[1].Rows[0]["bgcost"]);
            //double proadd = Convert.ToDouble(ds5.Tables[1].Rows[0]["proadd"]);




            // rdlc start

            string rptwelfarefund = Convert.ToDecimal(ds5.Tables[1].Rows[0]["wefund"]).ToString("#,##0;(#,##0); ");
            string rptothers = Convert.ToDecimal(ds5.Tables[1].Rows[0]["others"]).ToString("#,##0;(#,##0); ");
            string rpttoprice = Convert.ToDecimal(ds5.Tables[1].Rows[0]["acprice"]).ToString("#,##0;(#,##0); ");
            string rptcooperative = Convert.ToDecimal(ds5.Tables[1].Rows[0]["coorcost"]).ToString("#,##0;(#,##0); ");
            string rptdiscount = Convert.ToDecimal(ds5.Tables[1].Rows[0]["discount"]).ToString("#,##0;(#,##0); ");


            DataTable dtsum = ds5.Tables[2];
            double tsalevalue = Convert.ToDouble(ds5.Tables[1].Rows[0]["acprice"]);
            double treceived = Convert.ToDouble((Convert.IsDBNull(tblins.Compute("Sum(paidamt)", "")) ? 0.00
                    : tblins.Compute("Sum(paidamt)", "")));

            double fcheque = (dtsum.Rows.Count == 0) ? 0 : (Convert.ToDouble(dtsum.Rows[0]["fcheque"]) > 0) ? Convert.ToDouble(dtsum.Rows[0]["fcheque"]) : 0.00;
            double retcheque = (dtsum.Rows.Count == 0) ? 0 : (Convert.ToDouble(dtsum.Rows[0]["retcheque"]) > 0) ? Convert.ToDouble(dtsum.Rows[0]["retcheque"]) : 0.00;
            double pcheque = (dtsum.Rows.Count == 0) ? 0 : (Convert.ToDouble(dtsum.Rows[0]["pcheque"]) > 0) ? Convert.ToDouble(dtsum.Rows[0]["pcheque"]) : 0.00;
            double reconamt = treceived - (fcheque + retcheque + pcheque);
            double netbal = tsalevalue - reconamt;



            string rptunittype = ds5.Tables[1].Rows[0]["unittype"].ToString();
            string txtdate = "Print date: " + Date;
            string rptcustname = ds5.Tables[1].Rows[0]["name"].ToString();
            string rptcustadd = ds5.Tables[1].Rows[0]["peraddress"].ToString();
            string rptcustphone = ds5.Tables[1].Rows[0]["telephone"].ToString();
            string rptpactdesc = ds5.Tables[1].Rows[0]["projectname"].ToString();
            string projadd = ds5.Tables[1].Rows[0]["proadd"].ToString();

            string rptunitdesc = ds5.Tables[1].Rows[0]["aptname"].ToString();
            string rptusize = ds5.Tables[1].Rows[0]["aptsize"].ToString();
            string rptsalesteam = ds5.Tables[1].Rows[0]["salesteam"].ToString();
            string rptsalesdate = Convert.ToDateTime(ds5.Tables[1].Rows[0]["saledate"]).ToString("dd-MMM-yyyy");
            string rptagreementdate = (Convert.ToDateTime(ds5.Tables[1].Rows[0]["agdate"]).ToString("dd-MMM-yyyy") == "01-jan-1900") ? "" : Convert.ToDateTime(ds5.Tables[1].Rows[0]["agdate"]).ToString("dd-MMM-yyyy");
            string rpthandoverdate = (Convert.ToDateTime(ds5.Tables[1].Rows[0]["hoverdate"]).ToString("dd-MMM-yyyy") == "01-jan-1900") ? "" : Convert.ToDateTime(ds5.Tables[1].Rows[0]["hoverdate"]).ToString("dd-MMM-yyyy");

            string rptdelcharge = (delcharge > 0) ? delcharge.ToString("#,##0;(#,##0); ") : "";

            //string txtearlyben = (ebenamt > 0) ? ("early benefit: " + ebenamt.ToString("#,##0;(#,##0); ")) : ""; ;

            string printFooter = ASTUtility.Concat(compname, username, printdate);
            string comlogo = new Uri(Server.MapPath(@"~\image\logo" + comcod + ".jpg")).AbsoluteUri;

            var lst = tblins.DataTableToList<RealEntity.C_23_CRR.EClassSales_03.EClassClientPayDetails>();
            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RptSetupClass1.GetLocalReport("R_23_CR.RptClientLedger02", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("ComLogo", comlogo));
            Rpt1.SetParameters(new ReportParameter("CompName", comnam));
            Rpt1.SetParameters(new ReportParameter("Compadd", comadd));
            Rpt1.SetParameters(new ReportParameter("title", "Client Ledger"));
            Rpt1.SetParameters(new ReportParameter("txtDate", txtdate));

            Rpt1.SetParameters(new ReportParameter("rptcustname", rptcustname));
            Rpt1.SetParameters(new ReportParameter("rptCustAdd", rptcustadd));
            Rpt1.SetParameters(new ReportParameter("rptCustPhone", rptcustphone));
            Rpt1.SetParameters(new ReportParameter("rptpactdesc", rptpactdesc));
            Rpt1.SetParameters(new ReportParameter("projAddress", projadd));
            Rpt1.SetParameters(new ReportParameter("rptUnitDesc", rptunitdesc));
            Rpt1.SetParameters(new ReportParameter("rptUsize", rptusize));
            Rpt1.SetParameters(new ReportParameter("rptSalesteam", rptsalesteam));
            Rpt1.SetParameters(new ReportParameter("rptsalesdate", rptsalesdate));
            Rpt1.SetParameters(new ReportParameter("rptagreementdate", rptagreementdate));
            Rpt1.SetParameters(new ReportParameter("rptHandoverdate", rpthandoverdate));

            Rpt1.SetParameters(new ReportParameter("txtbgcost", bgcost.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptapartmentprice", aprment.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptcarparking", carparking.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptUtility", utility.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptcooperative", rptcooperative));
            Rpt1.SetParameters(new ReportParameter("regisfee", regisfee.ToString("#,##0;(#,##0); ")));
            //Rpt1.SetParameters(new ReportParameter("transfee", transfee.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("modicharge", modicharge.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("welfarefund", rptwelfarefund));
            Rpt1.SetParameters(new ReportParameter("rptOthers", rptothers));
            Rpt1.SetParameters(new ReportParameter("receivableTotal", rpttoprice));
            Rpt1.SetParameters(new ReportParameter("lessdisamt", rptdiscount));

            Rpt1.SetParameters(new ReportParameter("txttovalueamt", rpttoprice));
            Rpt1.SetParameters(new ReportParameter("txttoreceived", treceived.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtfcheque", fcheque.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtrcheque", retcheque.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtpcheque", pcheque.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtnetencash", reconamt.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtnetbalance", netbal.ToString("#,##0;(#,##0); ")));

            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            Session["Report1"] = Rpt1;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        private void CompanyType()
        {
            string comcod = this.GetComeCode();


            //    this.PrintPaymentSchedule();


        }




        protected void gvCustLedger_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                TableCell cell01 = new TableCell();
                cell01.Text = "";
                cell01.HorizontalAlign = HorizontalAlign.Center;
                cell01.ColumnSpan = 6;

                TableCell cell02 = new TableCell();
                cell02.Text = "Details";
                cell02.HorizontalAlign = HorizontalAlign.Center;
                cell02.ColumnSpan = 4;

                TableCell cell03 = new TableCell();
                cell03.Text = "";
                cell03.HorizontalAlign = HorizontalAlign.Center;
                cell03.ColumnSpan = 2;

                TableCell cell04 = new TableCell();
                cell04.Text = "Amount";
                cell04.HorizontalAlign = HorizontalAlign.Center;
                cell04.ColumnSpan = 2;

                TableCell cell05 = new TableCell();
                cell05.Text = "";
                cell05.HorizontalAlign = HorizontalAlign.Center;
                cell05.ColumnSpan = 2;

                gvrow.Cells.Add(cell01);
                gvrow.Cells.Add(cell02);
                gvrow.Cells.Add(cell03);
                gvrow.Cells.Add(cell04);
                gvrow.Cells.Add(cell05);
                gvCustLedger.Controls[0].Controls.AddAt(0, gvrow);
            }

        }


    }
}
