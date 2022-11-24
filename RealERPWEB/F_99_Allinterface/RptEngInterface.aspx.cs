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
using RealERPRDLC;
using RealERPLIB;
using RealERPRPT;
using Microsoft.Reporting.WinForms;
namespace RealERPWEB.F_99_Allinterface
{
    public partial class RptEngInterface : System.Web.UI.Page
    {
        // public static string recvno = "", centrid = "", custid = "", orderno1 = "", orderdat = "", Delstatus = "", Delorderno = "", RDsostatus = "";
        ProcessAccess accData = new ProcessAccess();
        Common Common = new Common();
        //Xml_BO_Class lst = new Xml_BO_Class();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string qusrid = this.Request.QueryString["usrid"] ?? "";
                if (qusrid.Length > 0)
                {
                    this.GetComNameAAdd();
                    this.GetUserPermission();
                    this.MasComNameAndAdd();

                }

                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "General Bill";//



                double day = Convert.ToInt32(System.DateTime.Today.ToString("dd")) - 1;
                this.txtdate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                //this.txFdate.Text = DateTime.Today.AddDays(-day).ToString("dd-MMM-yyyy");
                this.RadioButtonList1.SelectedIndex = 0;
                this.gridVisibility();


                this.lbtnOk_Click(null, null);
                //this.txtIme_TextChanged(null, null);


                //this.RadioButtonList1_SelectedIndexChanged(null, null);

            }
        }


        private void GetComNameAAdd()
        {
            string comcod = this.GetCompCode();
            //Access Database (List View)
            UserLogin ulog = new UserLogin();
            DataSet ds1 = ulog.GetNameAdd();

            DataView dv = ds1.Tables[0].DefaultView;
            dv.RowFilter = ("comcod = '" + comcod + "'");
            DataTable dt = dv.ToTable();
            Session["tbllog"] = dt;
            ds1.Dispose();


        }
        private void GetUserPermission()
        {
            string comcod = this.GetCompCode();

            string usrid = this.Request.QueryString["usrid"];
            string HostAddress = Request.UserHostAddress.ToString();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "LOGINUSERNAMEAPASS", usrid, "", "", "", "", "", "", "", "");

            //  if()

            //  ProcessAccess ulogin = (ASTUtility.Left(this.ddlCompany.SelectedValue.ToString(), 1) == "4") ? new ProcessAccess() : new ProcessAccess();

            string username = ds1.Tables[0].Rows[0]["username"].ToString();
            string pass = ds1.Tables[0].Rows[0]["password"].ToString();

            //string decodepass = ASTUtility.EncodePassword(pass);

            //        string pass = ASTUtility.EncodePassword(hst["password"].ToString());
            string modulid = "AA";
            string modulename = "All Module";
            DataSet ds5 = accData.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "LOGINUSER", username, pass, modulid, modulename, "", "", "", "", "");
            Session["tblusrlog"] = ds5;

            DataTable dt1 = (DataTable)Session["tbllog"];
            DataTable dt2 = new DataTable();

            //if ((DataTable)Session["tbllog1"] == null)
            // {
            dt2.Columns.Add("comcod", Type.GetType("System.String"));
            dt2.Columns.Add("comnam", Type.GetType("System.String"));
            dt2.Columns.Add("comsnam", Type.GetType("System.String"));
            dt2.Columns.Add("comadd1", Type.GetType("System.String"));
            dt2.Columns.Add("comadd", Type.GetType("System.String"));
            dt2.Columns.Add("usrsname", Type.GetType("System.String"));
            dt2.Columns.Add("session", Type.GetType("System.String"));
            dt2.Columns.Add("compsms", Type.GetType("System.String"));
            dt2.Columns.Add("compmail", Type.GetType("System.String"));

            Session["tbllog1"] = dt2;
            // }

            DataRow[] dr = dt1.Select("comcod='" + comcod + "'");
            // Hashtable hst = (Hashtable)Session["tblLogin"];
            Hashtable hst = new Hashtable();

            if (dr.Length > 0)
            {

                hst["comnam"] = dr[0]["comnam"];
                hst["comnam"] = dr[0]["comnam"];
                hst["comsnam"] = dr[0]["comsnam"];
                hst["comadd1"] = dr[0]["comadd1"];
                hst["comweb"] = dr[0]["comadd3"];
                hst["combranch"] = dr[0]["combranch"];
                hst["comadd"] = dr[0]["comadd"];


                DataRow dr2 = dt2.NewRow();
                dr2["comcod"] = comcod;
                dr2["comnam"] = dr[0]["comnam"];
                dr2["comsnam"] = dr[0]["comsnam"];
                dr2["comadd1"] = dr[0]["comadd1"];
                dr2["comadd"] = dr[0]["comadd"];

                dt2.Rows.Add(dr2);

            }
            string sessionid = (ASTUtility.RandNumber(111111, 999999)).ToString();
            hst["comcod"] = comcod;
            hst["deptcode"] = ds5.Tables[0].Rows[0]["deptcode"];

            // hst["comnam"] = ComName;
            hst["modulenam"] = "";
            hst["username"] = ds5.Tables[0].Rows[0]["usrsname"];
            hst["userfname"] = ds5.Tables[0].Rows[0]["usrname"];
            hst["compname"] = HostAddress;
            hst["usrid"] = ds5.Tables[0].Rows[0]["usrid"];
            hst["password"] = pass;
            hst["session"] = sessionid;
            hst["trmid"] = "";
            hst["commod"] = "1";
            hst["compsms"] = ds5.Tables[0].Rows[0]["compsms"];
            hst["ssl"] = ds5.Tables[0].Rows[0]["ssl"];
            hst["opndate"] = ds5.Tables[0].Rows[0]["opndate"];
            hst["empid"] = ds5.Tables[0].Rows[0]["empid"];
            hst["teamid"] = ds5.Tables[0].Rows[0]["teamid"];
            hst["mcomcod"] = ds5.Tables[5].Rows[0]["mcomcod"];
            hst["usrdesig"] = ds5.Tables[0].Rows[0]["usrdesig"];
            hst["events"] = ds5.Tables[0].Rows[0]["eventspanel"];
            hst["usrrmrk"] = ds5.Tables[0].Rows[0]["usrrmrk"];
            hst["userrole"] = ds5.Tables[0].Rows[0]["userrole"];
            hst["compmail"] = ds5.Tables[0].Rows[0]["compmail"];
            hst["userimg"] = ds5.Tables[0].Rows[0]["imgurl"];
            hst["comunpost"] = ds5.Tables[0].Rows[0]["comunpost"];
            hst["homeurl"] = ds5.Tables[0].Rows[0]["homeurl"];

            Session["tblLogin"] = hst;
            dt2.Rows[0]["usrsname"] = ds5.Tables[0].Rows[0]["usrsname"];
            dt2.Rows[0]["session"] = sessionid;
            Session["tbllog1"] = dt2;





        }

        private void MasComNameAndAdd()
        {
            //((Image)this.Master.FindControl("ComLogo")).ImageUrl = "";
            string comcod = this.GetCompCode();
            DataTable dt1 = ((DataTable)Session["tbllog"]);
            DataRow[] dr = dt1.Select("comcod='" + comcod + "'");
            DataTable dt = ((DataTable)Session["tbllog1"]);
            dt.Rows[0]["comcod"] = comcod;
            Session["tbllog1"] = dt;
            ((Label)this.Master.FindControl("LblGrpCompany")).Text = ((DataTable)Session["tbllog1"]).Rows[0]["comnam"].ToString();
            //((Label)this.Master.FindControl("lbladd")).Text = (dr[0]
        }

        private void gridVisibility()
        {

            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "2306":
                    this.gvReqInfo.Columns[5].Visible = false;
                    this.gvReqInfo.Columns[8].Visible = false;
                    this.gvPenApproval.Columns[4].Visible = false;
                    this.gvPenApproval.Columns[7].Visible = false;
                    this.gvPenApproval.Columns[15].Visible = false;
                    this.gvFinlApproval.Columns[4].Visible = false;
                    this.gvFinlApproval.Columns[7].Visible = false;
                    this.gvFinlApproval.Columns[1].Visible = false;
                    this.gvPayOrder.Columns[4].Visible = false;
                    this.gvPayOrder.Columns[7].Visible = false;
                    this.gvPayOrder.Columns[15].Visible = true;
                    break;

                default:
                    this.gvReqInfo.Columns[5].Visible = false;
                    this.gvReqInfo.Columns[8].Visible = false;
                    this.gvPenApproval.Columns[4].Visible = false;
                    this.gvPenApproval.Columns[7].Visible = false;
                    this.gvPenApproval.Columns[15].Visible = false;
                    this.gvFinlApproval.Columns[4].Visible = false;
                    this.gvFinlApproval.Columns[7].Visible = false;
                    this.gvFinlApproval.Columns[1].Visible = false;
                    this.gvPayOrder.Columns[4].Visible = false;
                    this.gvPayOrder.Columns[7].Visible = false;
                    this.gvPayOrder.Columns[15].Visible = false;
                    break;


            }


        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

            string comcod = this.GetCompCode();

            switch (comcod)
            {
                case "3333":
                    this.Timer1.Interval = 10000; // 10 Secnod
                    break;





                default:
                    this.Timer1.Interval = 3600000; //30 Second
                    break;



            }

        }


        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            string value = this.RadioButtonList1.SelectedValue.ToString();
            switch (value)
            {
                case "5":
                    this.PrintToDaysApproval();
                    break;

                case "6":
                    this.PrintFinalApproval();
                    break;

                case "7":
                    this.PrintPaymentDue();
                    break;

                default:
                    break;


            }



        }

        private void PrintToDaysApproval()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MMM.yyyy hh:mm:ss tt");
            ReportDocument rptstk = new RealERPRPT.R_15_DPayReg.RptToDaysApproval();
            TextObject txtCompanyName = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            txtCompanyName.Text = comnam;

            string todate = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");

            TextObject txtdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            txtdate.Text = todate;

            DataTable dt = (DataTable)Session["tbldate1"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = "faprvdat = '" + todate + "'";
            dt = dv.ToTable();



            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptstk.SetDataSource(dt);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstk;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        protected void Timer1_Tick(object sender, EventArgs e)
        {
            //DataTable dt = (DataTable)Session["tblspledger"];
            //if (dt == null)
            lbtnOk_Click(null, null);


        }
        public string GetCompCode()
        {
            string qcomcod = this.Request.QueryString["comcod"] ?? "";
            Hashtable hst = (Hashtable)Session["tblLogin"];
            qcomcod = qcomcod.Length > 0 ? qcomcod : hst["comcod"].ToString();
            return (qcomcod);
        }



        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            // this.Countqty();
            this.reqStatus();
            this.RadioButtonList1_SelectedIndexChanged(null, null);
            //  this.RadioButtonList1_SelectedIndexChanged(null, null);
        }



        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            this.lblprintstkl.Text = "";
            string value = this.RadioButtonList1.SelectedValue.ToString();
            switch (value)
            {
                case "0":
                    //Requistion status

                    this.pnlReqInfo.Visible = true;
                    this.pnlPendapp.Visible = false;
                    this.PnlReqChecked.Visible = false;
                    this.pnlfrec.Visible = false;
                    this.pnlsrec.Visible = false;
                    this.pnlthrec.Visible = false;
                    this.pnlpayOrder.Visible = false;
                    this.pnlFinalApp.Visible = false;
                    this.PnlreqInfo1.Visible = false;
                    //this.RadioButtonList1.Items[0].Attributes["style"] = "background: #D0DECA; display:block;";
                    this.RadioButtonList1.Items[0].Attributes["class"] = "lblactive blink_me";
                    break;

                case "1":
                    this.pnlReqInfo.Visible = true;
                    this.pnlPendapp.Visible = false;
                    this.PnlReqChecked.Visible = false;
                    this.pnlfrec.Visible = false;
                    this.pnlsrec.Visible = false;
                    this.pnlthrec.Visible = false;
                    this.pnlpayOrder.Visible = false;
                    this.pnlFinalApp.Visible = false;
                    this.PnlreqInfo1.Visible = false;
                    this.RadioButtonList1.Items[1].Attributes["class"] = "lblactive blink_me";
                    break;

                case "2":// requisition Checked

                    this.pnlReqInfo.Visible = false;
                    this.PnlReqChecked.Visible = true;
                    this.pnlPendapp.Visible = false;
                    this.pnlfrec.Visible = false;
                    this.pnlsrec.Visible = false;
                    this.pnlthrec.Visible = false;
                    this.pnlpayOrder.Visible = false;
                    this.pnlFinalApp.Visible = false;
                    this.PnlreqInfo1.Visible = false;
                    // this.RadioButtonList1.Items[2].Attributes["style"] = "background: #D0DECA; display:block;";
                    this.RadioButtonList1.Items[2].Attributes.Add("class", "lblactive");
                    break;

                case "3":// First Approval/ Checked

                    this.pnlReqInfo.Visible = false;
                    this.pnlPendapp.Visible = true;
                    this.PnlReqChecked.Visible = false;
                    this.pnlfrec.Visible = false;
                    this.pnlsrec.Visible = false;
                    this.pnlthrec.Visible = false;
                    this.pnlpayOrder.Visible = false;
                    this.pnlFinalApp.Visible = false;
                    this.PnlreqInfo1.Visible = false;
                    // this.RadioButtonList1.Items[2].Attributes["style"] = "background: #D0DECA; display:block;";
                    this.RadioButtonList1.Items[3].Attributes.Add("class", "lblactive");
                    break;


                case "4":// First Recommendation

                    this.pnlReqInfo.Visible = false;
                    this.pnlPendapp.Visible = false;
                    this.PnlReqChecked.Visible = false;
                    this.pnlfrec.Visible = true;
                    this.pnlsrec.Visible = false;
                    this.pnlthrec.Visible = false;
                    this.pnlpayOrder.Visible = false;
                    this.pnlFinalApp.Visible = false;
                    this.PnlreqInfo1.Visible = false;
                    // this.RadioButtonList1.Items[2].Attributes["style"] = "background: #D0DECA; display:block;";
                    this.RadioButtonList1.Items[4].Attributes["class"] = "lblactive blink_me";

                    break;


                case "5":// Second Recommendation

                    this.pnlReqInfo.Visible = false;
                    this.pnlPendapp.Visible = false;
                    this.PnlReqChecked.Visible = false;
                    this.pnlfrec.Visible = false;
                    this.pnlsrec.Visible = true;
                    this.pnlthrec.Visible = false;
                    this.pnlpayOrder.Visible = false;
                    this.pnlFinalApp.Visible = false;
                    this.PnlreqInfo1.Visible = false;


                    // this.RadioButtonList1.Items[2].Attributes["style"] = "background: #D0DECA; display:block;";
                    this.RadioButtonList1.Items[5].Attributes["class"] = "lblactive blink_me";

                    break;


                case "6":// Third Recommendation

                    this.pnlReqInfo.Visible = false;
                    this.pnlPendapp.Visible = false;
                    this.PnlReqChecked.Visible = false;
                    this.pnlfrec.Visible = false;
                    this.pnlsrec.Visible = false;
                    this.pnlthrec.Visible = true;
                    this.pnlpayOrder.Visible = false;
                    this.pnlFinalApp.Visible = false;
                    this.PnlreqInfo1.Visible = false;
                    this.RadioButtonList1.Items[6].Attributes["class"] = "lblactive blink_me";

                    break;




                case "7":  // Final Approval

                    this.pnlReqInfo.Visible = false;
                    this.pnlPendapp.Visible = false;
                    this.PnlReqChecked.Visible = false;
                    this.pnlfrec.Visible = false;
                    this.pnlsrec.Visible = false;
                    this.pnlthrec.Visible = false;
                    this.pnlFinalApp.Visible = true;
                    this.pnlpayOrder.Visible = false;
                    this.PnlreqInfo1.Visible = false;
                    this.RadioButtonList1.Items[7].Attributes["class"] = "lblactive blink_me";
                    //this.RadioButtonList1.Items[3].Attributes["style"] = "background: #D0DECA; display:block;";

                    break;


                case "8":// Payment Due
                    this.pnlFinalApp.Visible = false;
                    this.pnlReqInfo.Visible = false;
                    this.pnlPendapp.Visible = false;
                    this.PnlReqChecked.Visible = false;
                    this.pnlfrec.Visible = false;
                    this.pnlsrec.Visible = false;
                    this.pnlthrec.Visible = false;
                    this.pnlpayOrder.Visible = true;
                    this.PnlreqInfo1.Visible = false;
                    this.RadioButtonList1.Items[8].Attributes["class"] = "lblactive blink_me";
                    //this.RadioButtonList1.Items[3].Attributes["style"] = "background: #D0DECA; display:block;";

                    break;

                case "9": // Today Approval
                          //Requistion statusPnlreqInfo1

                    this.PnlreqInfo1.Visible = true;
                    this.pnlReqInfo.Visible = false;
                    this.PnlReqChecked.Visible = false;
                    this.pnlPendapp.Visible = false;
                    this.pnlfrec.Visible = false;
                    this.pnlsrec.Visible = false;
                    this.pnlthrec.Visible = false;
                    this.pnlpayOrder.Visible = false;
                    this.pnlFinalApp.Visible = false;
                    this.RadioButtonList1.Items[9].Attributes["class"] = "lblactive blink_me";
                    //this.RadioButtonList1.Items[4].Attributes["style"] = "background: #D0DECA; display:block;";
                    break;






            }
        }

        private void DataCountShow()
        {
            string comcod = this.GetCompCode();
            string FirstApp = "";
            string frecom = "", secrecom = "";

            switch (comcod)
            {
                case "1103":
                    FirstApp = "Checked";
                    frecom = "1st Recom.";
                    secrecom = "2nd Recom.";
                    break;


                case "1102": //IBCEL
                    FirstApp = "Checked";
                    frecom = "Forward";
                    secrecom = "Approval";

                    break;

                default:
                    frecom = "1st Recom.";
                    secrecom = "2nd Recom.";
                    FirstApp = "First Approval";
                    break;


            }


            DataTable dt = (DataTable)ViewState["tblcount"];

            this.RadioButtonList1.Items[0].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + ((dt.Rows.Count == 0) ? 0 : Convert.ToDouble(dt.Rows[0]["reqstatus"])).ToString("#,##0;(#,##0);") + "</div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'>Status</div></div></div>";
            this.RadioButtonList1.Items[1].Text = "<div class='circle-tile'><a><div class='circle-tile-heading red counter'>" + ((dt.Rows.Count == 0) ? 0 : Convert.ToDouble(dt.Rows[0]["apprst"])).ToString("#,##0;(#,##0);") + "</i></div></a><div class='circle-tile-content red'><div class='circle-tile-description text-faded'>Attachment</div></div></div>";
            this.RadioButtonList1.Items[2].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + ((dt.Rows.Count == 0) ? 0 : Convert.ToDouble(dt.Rows[0]["reqcheck"])).ToString("#,##0;(#,##0);") + "</i></div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'>Req Checked</div></div></div>";
            this.RadioButtonList1.Items[3].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + ((dt.Rows.Count == 0) ? 0 : Convert.ToDouble(dt.Rows[0]["apprst"])).ToString("#,##0;(#,##0);") + "</i></div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'>" + FirstApp + "</div></div></div>";
            this.RadioButtonList1.Items[4].Text = "<div class='circle-tile'><a><div class='circle-tile-heading orange  counter'>" + ((dt.Rows.Count == 0) ? 0 : Convert.ToDouble(dt.Rows[0]["frecom"])).ToString("#,##0;(#,##0);") + "</i></div></a><div class='circle-tile-content  orange '><div class='circle-tile-description text-faded'>" + frecom + "</div></div></div>";
            this.RadioButtonList1.Items[5].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-gray e  counter'>" + ((dt.Rows.Count == 0) ? 0 : Convert.ToDouble(dt.Rows[0]["secrecom"])).ToString("#,##0;(#,##0);") + "</i></div></a><div class='circle-tile-content dark-gray'><div class='circle-tile-description text-faded'>" + secrecom + "</div></div></div>";
            this.RadioButtonList1.Items[6].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + ((dt.Rows.Count == 0) ? 0 : Convert.ToDouble(dt.Rows[0]["threcom"])).ToString("#,##0;(#,##0);") + "</i></div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'>Forward</div></div></div>";

            this.RadioButtonList1.Items[7].Text = "<div class='circle-tile'><a><div class='circle-tile-heading red   counter'>" + ((dt.Rows.Count == 0) ? 0 : Convert.ToDouble(dt.Rows[0]["fapp"])).ToString("#,##0;(#,##0);") + "</i></div></a><div class='circle-tile-content red'><div class='circle-tile-description text-faded'>Final Approval</div></div></div>";
            this.RadioButtonList1.Items[8].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple  counter'>" + ((dt.Rows.Count == 0) ? 0 : Convert.ToDouble(dt.Rows[0]["payorder"])).ToString("#,##0;(#,##0);") + "</i></div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'>Payment Due</div></div></div>";
            this.RadioButtonList1.Items[9].Text = "<div class='circle-tile'><a><div class='circle-tile-heading orange counter'>" + ((dt.Rows.Count == 0) ? 0 : Convert.ToDouble(dt.Rows[0]["todayppval"])).ToString("#,##0;(#,##0);") + "</i></div></a><div class='circle-tile-content orange'><div class='circle-tile-description text-faded'>ToDays Approval</div></div></div>";






            //this.RadioButtonList1.Items[0].Text = "<span class='fa  fa-signal fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + ((dt.Rows.Count == 0) ? 0 : Convert.ToDouble(dt.Rows[0]["reqstatus"])).ToString("#,##0;(#,##0);") + "</span>" + "<span class='lbldata2'>" + "Requistion Status" + "</span>";
            //this.RadioButtonList1.Items[1].Text = "<span class='fa fa-pencil-square-o fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + ((dt.Rows.Count == 0) ? 0 : Convert.ToDouble(dt.Rows[0]["apprst"])).ToString("#,##0;(#,##0);") + "</span>" + "<span class=lbldata2>" + "CS" + "</span>";
            //this.RadioButtonList1.Items[2].Text = "<span class='fa fa-pencil-square-o fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + ((dt.Rows.Count == 0) ? 0 : Convert.ToDouble(dt.Rows[0]["apprst"])).ToString("#,##0;(#,##0);") + "</span>" + "<span class=lbldata2>" + "First Approval" + "</span>";
            //this.RadioButtonList1.Items[3].Text = "<span class='fa fa-pencil-square-o fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + ((dt.Rows.Count == 0) ? 0 : Convert.ToDouble(dt.Rows[0]["fapp"])).ToString("#,##0;(#,##0);") + "</span>" + "<span class=lbldata2>" + "Final Approval" + "</span>";

            //this.RadioButtonList1.Items[4].Text = "<span class='fa fa-check-square-o fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + ((dt.Rows.Count == 0) ? 0 : Convert.ToDouble(dt.Rows[0]["payorder"])).ToString("#,##0;(#,##0);") + "</span>" + "<span class=lbldata2>" + "Payment Due" + "</span>";
            //this.RadioButtonList1.Items[5].Text = "<span class='fa fa-check-square-o fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + ((dt.Rows.Count == 0) ? 0 : Convert.ToDouble(dt.Rows[0]["todayppval"])).ToString("#,##0;(#,##0);") + "</span>" + "<span class=lbldata2>" + "ToDays Approval" + "</span>";






        }

        private string ComUserWiseInterface()
        {

            string comcod = this.GetCompCode();
            string userwise = "";
            switch (comcod)
            {
                case "3370"://CPDL
                case "3101":
                    userwise = "UserWise";
                    break;


                default:
                    break;
              
            
            
            }
            return userwise;



        }

        private void reqStatus()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();

            string comcod = this.GetCompCode();
            // string frmdate = Convert.ToDateTime(this.txFdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            string refno = "%%";
            string userwise = this.ComUserWiseInterface();



            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_SALES_INTERFACE", "BILLREGISTER", todate, refno, usrid, userwise, "", "", "", "", "");
            if (ds1 == null)
                return;


            Session["tbldate"] = ds1.Tables[1];
            Session["tbldate1"] = ds1.Tables[2];
            ViewState["tblcount"] = ds1.Tables[0];
            this.DataCountShow();


            // All Recive
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();

            DataView dv = new DataView();
            dt = ((DataTable)ds1.Tables[1]).Copy();
            // dt1 = ((DataTable)ds1.Tables[2]).Copy();

            dv = dt.DefaultView;
            this.Data_Bind("gvReqInfo", dv.ToTable());


            //req checked  checkbyid <>''

            //
            switch (comcod)
            {

                case "3370"://CPDL
                case "3101":
                    dv.RowFilter = ("checkbyid = ''and suserid='"+ usrid + "'");

                    break;
                default:
                    dv.RowFilter = ("checkbyid = ''");
                    break;
            
            }


           
            dv = dt.DefaultView;
            this.Data_Bind("gvReqCheck", dv.ToTable());



            //dt = ((DataTable)ds1.Tables[1]).Copy();  // and appamt <= 0
            dv.RowFilter = ("checkbyid <>'' and aprvbyid=''");
            dv = dt.DefaultView;
            this.Data_Bind("gvPenApproval", dv.ToTable());


            //First Recommendate
            dv.RowFilter = ("checkbyid <>'' and aprvbyid <>'' and  frecid=''  and appamt > 0.00");
            //dv.RowFilter = ("empid ='" + usrid + "'");
            dv = dt.DefaultView;
            this.Data_Bind("gvfrec", dv.ToTable());



            //Second Recommendate
            dv.RowFilter = ("checkbyid <>'' and aprvbyid <>'' and  frecid<>'' and  secrecid='' and faprvbyid='' and appamt > 0.00 ");
            //dv.RowFilter = ("empid ='" + usrid + "'");
            dv = dt.DefaultView;
            this.Data_Bind("gvsrec", dv.ToTable());



            //Third Recommendate
            dv.RowFilter = ("checkbyid <>'' and aprvbyid <>'' and  frecid<>'' and  secrecid<>'' and threcid='' and faprvbyid='' and appamt > 0.00 ");
            //dv.RowFilter = ("empid ='" + usrid + "'");
            dv = dt.DefaultView;
            this.Data_Bind("gvthrec", dv.ToTable());


            /// final approval 
            dv.RowFilter = ("checkbyid <>'' and aprvbyid <>'' and  frecid<>'' and  secrecid<>'' and threcid<>'' and faprvbyid='' and appamt > 0.00");
            dv = dt.DefaultView;
            this.Data_Bind("gvFinlApproval", dv.ToTable());
            Session["tblfApproal"] = dv.ToTable();

            //pen approval / payment due
            dv.RowFilter = ("checkbyid <>'' and balamt > 0 and faprvbyid<>''");
            dv = dt.DefaultView;
            Session["tblpaydue"] = dv.ToTable();
            this.Data_Bind("gvPayOrder", dv.ToTable());
            this.Data_Bind("gvReqInfo1", dt);

        }


        private void Data_Bind(string gv, DataTable dt)
        {
            //string todate = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            string value = this.RadioButtonList1.SelectedValue.ToString();
            DataTable dt1 = (DataTable)Session["tbldate1"];


            string todate = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");

            switch (gv)
            {
                case "gvReqInfo":
                    this.gvReqInfo.DataSource = dt;
                    this.gvReqInfo.DataBind();

                    if (value == "1" || value == "2")
                    {
                        gvReqInfo.Columns[13].Visible = true;
                    }
                    else
                    {
                        gvReqInfo.Columns[13].Visible = false;
                    }
                    break;

                case "gvReqCheck":
                    this.gvReqCheck.DataSource = dt;
                    this.gvReqCheck.DataBind();
                    break;

                case "gvPenApproval":
                    this.gvPenApproval.DataSource = dt;
                    this.gvPenApproval.DataBind();
                    break;

                case "gvfrec":
                    this.gvfrec.DataSource = dt;
                    this.gvfrec.DataBind();
                    break;
                case "gvsrec":
                    this.gvsrec.DataSource = dt;
                    this.gvsrec.DataBind();
                    break;

                case "gvthrec":
                    this.gvthrec.DataSource = dt;
                    this.gvthrec.DataBind();
                    break;

                case "gvFinlApproval":
                    this.gvFinlApproval.DataSource = dt;
                    this.gvFinlApproval.DataBind();
                    break;

                case "gvPayOrder":
                    this.gvPayOrder.DataSource = dt;
                    this.gvPayOrder.DataBind();
                    if (dt.Rows.Count == 0)
                        return;

                    ((Label)this.gvPayOrder.FooterRow.FindControl("lblgvFreqamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(reqamt)", "")) ? 0.00 :
                        dt.Compute("sum(reqamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvPayOrder.FooterRow.FindControl("lblgvFApamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(appamt)", "")) ? 0.00 :
                   dt.Compute("sum(appamt)", ""))).ToString("#,##0;(#,##0); ");

                    break;

                case "gvReqInfo1":

                    DataView dv = new DataView();
                    dv = dt1.DefaultView;
                    dv.RowFilter = ("faprvdat = '" + todate + "'");
                    this.gvReqInfo1.DataSource = dv.ToTable();
                    this.gvReqInfo1.DataBind();
                    break;

            }

        }

        private DataTable HiddenSameData(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return dt;

            string pactcode = dt.Rows[0]["pactcode"].ToString();
            string reqdat1 = dt.Rows[0]["reqdat1"].ToString();


            for (int j = 1; j < dt.Rows.Count; j++)
            {
                if (dt.Rows[j]["pactcode"].ToString() == pactcode && dt.Rows[j]["reqdat1"].ToString() == reqdat1)
                {
                    pactcode = dt.Rows[j]["pactcode"].ToString();
                    dt.Rows[j]["pactdesc"] = "";
                    reqdat1 = dt.Rows[j]["reqdat1"].ToString();
                    dt.Rows[j]["reqdat1"] = "";

                }

                else
                    pactcode = dt.Rows[j]["pactcode"].ToString();
                reqdat1 = dt.Rows[j]["reqdat1"].ToString();

            }

            return dt;
        }


        protected void gvReqInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink1 = (HyperLink)e.Row.FindControl("hlkQutation");
                //HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");
                //HyperLink hlink3 = (HyperLink)e.Row.FindControl("lnkbtnEditIN");
                //HyperLink hlnkgvgvmrfno = (HyperLink)e.Row.FindControl("hlnkgvgvmrfno");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("HyInprPrint11");


                string comcod = this.GetCompCode();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();


                TableCell cell = e.Row.Cells[10];
                string cstatus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "cstatus")).ToString();
                if (cstatus == "Pending First Approval")
                {
                    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1DA1F2");
                }
                if (cstatus == "Payment")
                {
                    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#3B5998");
                }
                if (cstatus == "Pending Payment")
                {
                    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF6550");
                }



                ////string fDate = Convert.ToDateTime(this.txFdate.Text).ToString("dd-MMM-yyyy");
                ////string tDate = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");

                //  hlink1.NavigateUrl = "~/F_99_Allinterface/LinkQutaAttached";//&comcod=" + comcod + "&pactcode=" + pactcode + "&reqno=" + reqno;

                ////hlink2.NavigateUrl = "~/F_20_Service/RecProductEntry?Type=Entry";
                ////hlink2.ToolTip = "Create New";

                hlink1.NavigateUrl = "~/F_99_Allinterface/LinkQutaAttached?Type=QutAttached&reqno=" + reqno + "&app=0";
                hlink2.NavigateUrl = "~/F_34_Mgt/OtherReqEntry?Type=OreqPrint&prjcode=" + pactcode + "&genno=" + reqno;


            }
        }
        protected void gvPenApproval_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnEntry");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("hlkQutation");
                HyperLink hlnkbudleno = (HyperLink)e.Row.FindControl("hlnkbudleno");
                // HyperLink hlink3 = (HyperLink)e.Row.FindControl("HyInprPrint");


                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string userid = hst["usrid"].ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();



                // TableCell cell = e.Row.Cells[10];
                string suserid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "suserid")).ToString();
                string cstatus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "cstatus")).ToString();
                string bundno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "bundno")).ToString();
                //if (cstatus == "Pending First Approval")
                //{
                //    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1DA1F2");
                //}
                //if (cstatus == "Payment")
                //{
                //    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#3B5998");
                //}
                //if (cstatus == "Pending Payment")
                //{
                //    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF6550");





                //}

                switch (comcod)
                {
                    case "3336":
                    case "3337":
                        //  case "3101":
                        hlink1.Enabled = (userid == suserid) ? true : false;
                        hlink1.Attributes["style"] = (userid == suserid) ? "background:blue;" : " background:red;";
                        hlink1.NavigateUrl = "~/F_34_Mgt/OtherReqEntry?Type=OreqApproved&prjcode=" + pactcode + "&genno=" + reqno;
                        break;

                    default:
                        hlink1.NavigateUrl = "~/F_34_Mgt/OtherReqEntry?Type=OreqApproved&prjcode=" + pactcode + "&genno=" + reqno;
                        // hlink1.Enabled = (userid == suserid) ? true : false;
                        // hlink1.Attributes["style"] = (userid == suserid) ? "background:blue;" : " background:red;";
                        break;


                }

                //hlink1.Enabled = (userid == suserid) ? true : false;
                //hlink1.Attributes["style"] = (userid == suserid) ? "background:blue;" : " background:red;";

                hlink1.NavigateUrl = "~/F_34_Mgt/OtherReqEntry?Type=OreqApproved&prjcode=" + pactcode + "&genno=" + reqno;
                hlink2.NavigateUrl = "~/F_99_Allinterface/LinkQutaAttached?Type=QutAttached&reqno=" + reqno + "&app=1";
                hlnkbudleno.NavigateUrl = "~/F_12_Inv/PurTopSheetCashPur?Type=Entry&genno=" + bundno;


            }

        }

        protected void gvFinlApproval_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink HyInprPrint = (HyperLink)e.Row.FindControl("HyInprPrint");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("hlkQutationfapp");
                HyperLink lnkbtnEntry = (HyperLink)e.Row.FindControl("lnkbtnEntry");
                HyperLink hlnkbudlenoapp = (HyperLink)e.Row.FindControl("hlnkbudlenoapp");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();



                TableCell cell = e.Row.Cells[10];
                string cstatus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "cstatus")).ToString();
                string bundno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "bundno")).ToString();
                if (cstatus == "Pending First Approval")
                {
                    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1DA1F2");
                }
                if (cstatus == "Payment")
                {
                    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#3B5998");
                }
                if (cstatus == "Pending Payment")
                {
                    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF6550");
                }

                HyInprPrint.NavigateUrl = "~/F_34_Mgt/OtherReqEntry?Type=OreqPrint&prjcode=" + pactcode + "&genno=" + reqno;
                lnkbtnEntry.NavigateUrl = "~/F_34_Mgt/OtherReqEntry?Type=FinalAppr&prjcode=" + pactcode + "&genno=" + reqno;
                hlink2.NavigateUrl = "~/F_99_Allinterface/LinkQutaAttached?Type=QutAttached&reqno=" + reqno + "&app=1";
                hlnkbudlenoapp.NavigateUrl = "~/F_12_Inv/PurTopSheetCashPur?Type=Entry&genno=" + bundno;


            }
        }

        protected void gvPayOrder_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrintpay");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("hlnkpaymentSch");
                LinkButton hlink3 = (LinkButton)e.Row.FindControl("hlnkprjlink");



                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string supcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "supcode")).ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();

                TableCell cell = e.Row.Cells[10];

                string cstatus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "cstatus")).ToString();
                if (cstatus == "Pending First Approval")
                {
                    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1DA1F2");
                }
                if (cstatus == "Payment")
                {
                    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#3B5998");
                }
                if (cstatus == "Pending Payment")
                {
                    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF6550");
                }

                hlink1.NavigateUrl = "~/F_34_Mgt/OtherReqEntry?Type=OreqPrint&prjcode=" + pactcode + "&genno=" + reqno;
                if ((comcod.Substring(0, 1) == "2" || comcod.Substring(0, 1) == "3") && pactcode.Substring(0, 2) == "26" && supcode.Substring(0, 2) == "99")
                {

                    hlink2.Visible = true;
                    hlink2.NavigateUrl = "~/F_14_Pro/LandOwnerPaymentSch?Type=Report&prjcode=" + pactcode;
                }
                if (pactcode.Substring(0, 2) == "26" && supcode.Substring(0, 2) == "99")
                {

                    hlink3.Visible = true;
                }

            }
        }

        protected void gvPayment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                //HyperLink hlink1 = (HyperLink)e.Row.FindControl("hlnkgvgvmrfno");

                TableCell cell = e.Row.Cells[10];
                string cstatus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "cstatus")).ToString();
                if (cstatus == "Pending First Approval")
                {
                    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#1DA1F2");
                }
                if (cstatus == "Payment")
                {
                    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#3B5998");
                }
                if (cstatus == "Pending Payment")
                {
                    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF6550");
                }


                //hlink1.NavigateUrl = "~/F_34_Mgt/OtherReqEntry?Type=OreqEntry";//&comcod=" + comcod + "&centrid=" + centrid + "&recvno=" + recvno + "&imesimeno=" + imesimeno;


            }
        }
        protected void btnDelOrderfapproved_Click(object sender, EventArgs e)
        {
            GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            int rowindex = gvr.RowIndex;
            string comcod = this.GetCompCode();
            string reqno = ((Label)this.gvPenApproval.Rows[rowindex].FindControl("lblgvreqnopapr")).Text.Trim();
            bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "DELETEOTHERREQ", reqno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {

                this.reqStatus();
                this.RadioButtonList1_SelectedIndexChanged(null, null);

                //  dt.Rows[rowindex].Delete();



            }

            else
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Update Fail');", true);
                return;

            }


            //DataView dv = dt.DefaultView;
            //ViewState.Remove("tblmatissue");
            //ViewState["tblmatissue"] = dv.ToTable();
            //this.grvissue_DataBind();

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Labour Issue Information";
            //    string eventdesc = "Delete Labour";
            //    string eventdesc2 = "Project Name: " + this.ddlprjlist.SelectedItem.Text.Substring(14) + "- " + "Sub Contractor Name: " +
            //            this.ddlcontractorlist.SelectedItem.Text.Substring(14) + "- " + "Issue No: " + this.lblCurISSNo1.Text.Trim().Substring(0, 3) +
            //            ASTUtility.Right((this.txtCurISSDate.Text.Trim()), 4) + this.lblCurISSNo1.Text.Trim().Substring(3, 2) + this.txtCurISSNo2.Text.Trim() + "- " +
            //            ((Label)this.grvissue.Rows[e.RowIndex].FindControl("lblitemcode")).Text.Trim();
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)ViewState["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Final Approval";
                string eventdesc = "Final Approval Delete";
                string eventdesc2 = "Requisition No: " + reqno;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }

        private string GetComDelSkip3()

        {
            string comcod = this.GetCompCode();
            string delskip = "";
            switch (comcod)
            {
                case "1102"://IBCEL
                    break;

                case "3370":// CPDL
                case "3368":// Finaly              
                case "3101":// Finaly              
                    delskip = "delskip5";
                    break;


                default:
                    delskip = "delskip4";
                    break;

              


            }


            return delskip;
        }

        protected void btnDelOrder_Click(object sender, EventArgs e)
        {


            GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            int rowindex = gvr.RowIndex;
            string comcod = this.GetCompCode();
            string reqno = ((Label)this.gvFinlApproval.Rows[rowindex].FindControl("lblgvreqnoFnApp")).Text.Trim();
            string delskip = this.GetComDelSkip3();
            bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "DELETEOTHERREQ", reqno, delskip, "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {

                this.reqStatus();
                this.RadioButtonList1_SelectedIndexChanged(null, null);

                //  dt.Rows[rowindex].Delete();
            }

            else
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Update Fail');", true);
                return;

            }

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Order Bil";
                string eventdesc = "Delete Order Bill";
                string eventdesc2 = "General Bill No: " + reqno;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }


            //DataView dv = dt.DefaultView;
            //ViewState.Remove("tblmatissue");
            //ViewState["tblmatissue"] = dv.ToTable();
            //this.grvissue_DataBind();

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Labour Issue Information";
            //    string eventdesc = "Delete Labour";
            //    string eventdesc2 = "Project Name: " + this.ddlprjlist.SelectedItem.Text.Substring(14) + "- " + "Sub Contractor Name: " +
            //            this.ddlcontractorlist.SelectedItem.Text.Substring(14) + "- " + "Issue No: " + this.lblCurISSNo1.Text.Trim().Substring(0, 3) +
            //            ASTUtility.Right((this.txtCurISSDate.Text.Trim()), 4) + this.lblCurISSNo1.Text.Trim().Substring(3, 2) + this.txtCurISSNo2.Text.Trim() + "- " +
            //            ((Label)this.grvissue.Rows[e.RowIndex].FindControl("lblitemcode")).Text.Trim();
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)ViewState["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}

        }


        protected void gvfrec_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnEntryfrec");
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();
                hlink1.NavigateUrl = "~/F_34_Mgt/OtherReqEntry?Type=FirstRecom&prjcode=" + pactcode + "&genno=" + reqno;

            }

        }
        protected void gvsrec_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnEntrysrec");
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();
                hlink1.NavigateUrl = "~/F_34_Mgt/OtherReqEntry?Type=SecRecom&prjcode=" + pactcode + "&genno=" + reqno;

            }

        }
        protected void gvthrec_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnEntrythrec");
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();
                hlink1.NavigateUrl = "~/F_34_Mgt/OtherReqEntry?Type=ThirdRecom&prjcode=" + pactcode + "&genno=" + reqno;

            }


        }


        protected void btnDelfrec_Click(object sender, EventArgs e)
        {


            GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            int rowindex = gvr.RowIndex;
            string comcod = this.GetCompCode();
            string reqno = ((Label)this.gvfrec.Rows[rowindex].FindControl("lblgvreqnofrec")).Text.Trim();


           
            bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "DELETEFIRSTRECON", reqno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {

                this.reqStatus();
                this.RadioButtonList1_SelectedIndexChanged(null, null);

                //  dt.Rows[rowindex].Delete();
            }

            else
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Update Fail');", true);
                return;
            }
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = " General Bill";
                string eventdesc = "General Bill Delete First Recom";
                string eventdesc2 = "General Bill No: " + reqno;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }


            //DataView dv = dt.DefaultView;
            //ViewState.Remove("tblmatissue");
            //ViewState["tblmatissue"] = dv.ToTable();
            //this.grvissue_DataBind();

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Labour Issue Information";
            //    string eventdesc = "Delete Labour";
            //    string eventdesc2 = "Project Name: " + this.ddlprjlist.SelectedItem.Text.Substring(14) + "- " + "Sub Contractor Name: " +
            //            this.ddlcontractorlist.SelectedItem.Text.Substring(14) + "- " + "Issue No: " + this.lblCurISSNo1.Text.Trim().Substring(0, 3) +
            //            ASTUtility.Right((this.txtCurISSDate.Text.Trim()), 4) + this.lblCurISSNo1.Text.Trim().Substring(3, 2) + this.txtCurISSNo2.Text.Trim() + "- " +
            //            ((Label)this.grvissue.Rows[e.RowIndex].FindControl("lblitemcode")).Text.Trim();
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)ViewState["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}

        }

        protected void btnDelsrec_Click(object sender, EventArgs e)
        {


            GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            int rowindex = gvr.RowIndex;
            string comcod = this.GetCompCode();
            string reqno = ((Label)this.gvsrec.Rows[rowindex].FindControl("lblgvreqnosrec")).Text.Trim();


            

            bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "DELETESECONDRECOMMEND", reqno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");


            if (result == true)
            {

                this.reqStatus();
                this.RadioButtonList1_SelectedIndexChanged(null, null);

                //  dt.Rows[rowindex].Delete();
            }

            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Update Fail');", true);
                return;
            }

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = " General Bill";
                string eventdesc = "General Bill Delete Second Recom";
                string eventdesc2 = "General Bill No: " + reqno;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }


            //DataView dv = dt.DefaultView;
            //ViewState.Remove("tblmatissue");
            //ViewState["tblmatissue"] = dv.ToTable();
            //this.grvissue_DataBind();

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Labour Issue Information";
            //    string eventdesc = "Delete Labour";
            //    string eventdesc2 = "Project Name: " + this.ddlprjlist.SelectedItem.Text.Substring(14) + "- " + "Sub Contractor Name: " +
            //            this.ddlcontractorlist.SelectedItem.Text.Substring(14) + "- " + "Issue No: " + this.lblCurISSNo1.Text.Trim().Substring(0, 3) +
            //            ASTUtility.Right((this.txtCurISSDate.Text.Trim()), 4) + this.lblCurISSNo1.Text.Trim().Substring(3, 2) + this.txtCurISSNo2.Text.Trim() + "- " +
            //            ((Label)this.grvissue.Rows[e.RowIndex].FindControl("lblitemcode")).Text.Trim();
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)ViewState["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}

        }

        protected void btnDelthrec_Click(object sender, EventArgs e)
        {


            GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            int rowindex = gvr.RowIndex;
            string comcod = this.GetCompCode();
            string reqno = ((Label)this.gvFinlApproval.Rows[rowindex].FindControl("lblgvreqnoFnApp")).Text.Trim();
            bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "DELETEOTHERREQ", reqno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {

                this.reqStatus();
                this.RadioButtonList1_SelectedIndexChanged(null, null);

                //  dt.Rows[rowindex].Delete();
            }

            else
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Update Fail');", true);

            }


            //DataView dv = dt.DefaultView;
            //ViewState.Remove("tblmatissue");
            //ViewState["tblmatissue"] = dv.ToTable();
            //this.grvissue_DataBind();

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Labour Issue Information";
            //    string eventdesc = "Delete Labour";
            //    string eventdesc2 = "Project Name: " + this.ddlprjlist.SelectedItem.Text.Substring(14) + "- " + "Sub Contractor Name: " +
            //            this.ddlcontractorlist.SelectedItem.Text.Substring(14) + "- " + "Issue No: " + this.lblCurISSNo1.Text.Trim().Substring(0, 3) +
            //            ASTUtility.Right((this.txtCurISSDate.Text.Trim()), 4) + this.lblCurISSNo1.Text.Trim().Substring(3, 2) + this.txtCurISSNo2.Text.Trim() + "- " +
            //            ((Label)this.grvissue.Rows[e.RowIndex].FindControl("lblitemcode")).Text.Trim();
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)ViewState["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}

        }
        protected void hlnkprjlink_Click(object sender, EventArgs e)
        {

            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string comcod = this.GetCompCode();
            string rescode = ((Label)this.gvPayOrder.Rows[Rowindex].FindControl("lblsupcode")).Text.Trim();
            string sircode = ((Label)this.gvPayOrder.Rows[Rowindex].FindControl("lblrescode")).Text.Trim();
            string com = comcod.Substring(0, 1);
            if (comcod.Substring(0, 1) == "2" || comcod.Substring(0, 1) == "3")
            {
                bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_LPROCUREMENT", "INSERORUPDATEPAYCODE",
                             rescode, sircode, "", "", "", "", "", "", "", "", "", "", "", "", "");
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Linking Failed";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;

                }

                ((Label)this.Master.FindControl("lblmsg")).Text = "Successfully Linked";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            }
            else
            {
                return;
            }

        }
        protected void lnkbtnfinalAprv_Click(object sender, EventArgs e)
        {
            //DataTable dt = (DataTable)Session["tblfApproal"];
            //string comcod = this.GetCompCode();
            //int index;
            //for (int i = 0; i < this.gvFinlApproval.Rows.Count; i++)
            //{
            //    string chkemerge = (((CheckBox)gvFinlApproval.Rows[i].FindControl("chkfinalAprv")).Checked) ? "True" : "False";
            //    index = (this.gvFinlApproval.PageSize) * (this.gvFinlApproval.PageIndex) + i;
            //    if (chkemerge == "True")
            //    {
            //        dt.Rows[index]["chkapprv"] = "True";

            //    }
            //}
            //DataView dv1 = dt.Copy().DefaultView;
            //dv1.RowFilter = ("chkapprv='True'");       
            //DataTable dt01 = dv1.ToTable();
            //Session["tblfApproal"] = dt01;
            //this.PrintFinalApproval();

        }
        protected void chkAllfinalAprv_CheckedChanged(object sender, EventArgs e)
        {
            int i, index;
            if (((CheckBox)this.gvFinlApproval.HeaderRow.FindControl("chkAllfinalAprv")).Checked)
            {

                for (i = 0; i < this.gvFinlApproval.Rows.Count; i++)
                {
                    ((CheckBox)this.gvFinlApproval.Rows[i].FindControl("chkfinalAprv")).Checked = true;
                    index = (this.gvFinlApproval.PageSize) * (this.gvFinlApproval.PageIndex) + i;

                }


            }

            else
            {
                for (i = 0; i < this.gvFinlApproval.Rows.Count; i++)
                {
                    ((CheckBox)this.gvFinlApproval.Rows[i].FindControl("chkfinalAprv")).Checked = false;
                    index = (this.gvFinlApproval.PageSize) * (this.gvFinlApproval.PageIndex) + i;
                    //dt.Rows[index]["chkmerge"] = "False";

                }

            }

        }

        private void PrintFinalApproval()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string todate = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");

            DataTable dt = (DataTable)Session["tblfApproal"];


            var list = dt.DataTableToList<RealEntity.C_99_AllInterface.GbFinalApproval>();
            LocalReport rpt = new LocalReport();
            rpt = RptSetupClass1.GetLocalReport("R_99_AllInterface.RptGbFinalApproval", list, null, null);
            rpt.EnableExternalImages = true;
            rpt.SetParameters(new ReportParameter("comName", comnam));
            rpt.SetParameters(new ReportParameter("txtTitle", "General Bill Final Approval"));
            rpt.SetParameters(new ReportParameter("date1", "For The Month Of " + todate));
            rpt.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            rpt.SetParameters(new ReportParameter("comLogo", ComLogo));

            Session["Report1"] = rpt;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintPaymentDue()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string todate = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");

            DataTable dt = (DataTable)Session["tblpaydue"];

            var list = dt.DataTableToList<RealEntity.C_99_AllInterface.GbFinalApproval>();
            LocalReport rpt = new LocalReport();
            rpt = RptSetupClass1.GetLocalReport("R_99_AllInterface.RptGbFinalApproval", list, null, null);
            rpt.EnableExternalImages = true;
            rpt.SetParameters(new ReportParameter("comName", comnam));
            rpt.SetParameters(new ReportParameter("txtTitle", "General Bill Payment Due"));
            rpt.SetParameters(new ReportParameter("date1", "For The Month Of " + todate));
            rpt.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            rpt.SetParameters(new ReportParameter("comLogo", ComLogo));

            Session["Report1"] = rpt;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void gvReqCheck_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                   
                    
                    Hashtable hst = (Hashtable)Session["tblLogin"];
                    
                    string userid = hst["usrid"].ToString();
                    string comcod = this.GetCompCode();
                    HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnReqChecked");
                    string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                    string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();
                    switch (comcod)
                    {
                        case "3370"://CPDL
                        case "3101":                           
                            string suserid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "suserid")).ToString();
                            if (suserid == userid)
                            {
                                hlink1.Attributes["style"] = "color:green;";
                                hlink1.Enabled = true;
                            }
                            else

                            {
                                hlink1.Attributes["style"] = "color:red;";
                                hlink1.Enabled = false;

                            }
                            break;

                        default:
                            break;
                    
                    
                    
                    }

                    hlink1.NavigateUrl = "~/F_34_Mgt/OtherReqEntry?Type=OreqChecked&prjcode=" + pactcode + "&genno=" + reqno;




                }

                
               

                
            }
        }

        protected void btnDelReqChecked_Click(object sender, EventArgs e)
        {
            GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            int rowindex = gvr.RowIndex;
            string comcod = this.GetCompCode();
            string reqno = ((Label)this.gvReqCheck.Rows[rowindex].FindControl("lblgvReqChkreqno")).Text.Trim();
            bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "DELETEOTHERREQCHECK", reqno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {

                this.reqStatus();
                this.RadioButtonList1_SelectedIndexChanged(null, null);
            }

            else
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Update Fail');", true);
                return;

            }


            

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Final Approval";
                string eventdesc = "Final Approval Delete";
                string eventdesc2 = "Requisition No: " + reqno;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }
    }
}