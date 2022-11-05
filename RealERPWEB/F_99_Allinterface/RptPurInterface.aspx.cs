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
using Microsoft.Reporting.WinForms;
using RealERPLIB;
using RealERPRDLC;
using System.Net;
using System.Net.Mail;
using EASendMail;
using System.IO;
namespace RealERPWEB.F_99_Allinterface
{
    public partial class RptPurInterface : System.Web.UI.Page
    {
        // public static string recvno = "", centrid = "", custid = "", orderno1 = "", orderdat = "", Delstatus = "", Delorderno = "", RDsostatus = "";
        ProcessAccess accData = new ProcessAccess();
        Common Common = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                string qusrid = this.Request.QueryString["usrid"] ?? "";
                if (qusrid.Length > 0)
                {
                    this.GetComNameAAdd();
                    this.GetUserPermissionurl();
                    this.MasComNameAndAddurl();

                }

                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                if ((!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp),
                        (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean(hst["permission"]))
                    Response.Redirect("~/AcceessError");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Purchase Interface";//

                //string date = System.DateTime.Today.ToString("dd-MMM-yyyy") ;
                // this.txtfrmdate.Text = Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy");
                this.GetFromDate();
                this.txttoDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtmrfno.Attributes.Add("placeholder", ReadCookie());

                this.RadioButtonList1.SelectedIndex = 0;
                this.GetCompanyName();
                this.CompanyVisibility();

                //string mcomcod = hst["mcomcod"].ToString().Trim();

                //if (mcomcod != "0000")
                //{




                //}

            }
        }

        private void CompanyVisibility()
        {

            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3354"://ERL
                            // case "3101":
                    this.hlnkMktInterface.Visible = true;
                    break;

                default:
                    this.hlnkMktInterface.Visible = false;
                    break;


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

        private string ReadCookie()
        {
            HttpCookie nameCookie = Request.Cookies["MRF"];
            string refno = nameCookie != null ? nameCookie.Value.Split('=')[1] : "Mrf No";
            return refno;
        }

        private void GetUserPermissionurl()
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

        private void MasComNameAndAddurl()
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








        private void GetCompanyName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = ASTUtility.Right(hst["usrid"].ToString(), 3);
            //string comcod = hst["mcomcod"].ToString();
            string comcod = this.GetCompCode();
            DataSet ds = accData.GetTransInfo(comcod, "SP_UTILITY_GRPUSER_MGT", "GET_MOTHER_COMPANY", usrid, "", "", "", "", "", "", "", "");
            this.ddlCompany.DataTextField = "comname";
            this.ddlCompany.DataValueField = "comcod";
            this.ddlCompany.DataSource = ds.Tables[0];
            this.ddlCompany.DataBind();
            this.ddlCompany.SelectedValue = this.GetCompCode();
            if (ds.Tables[0].Rows.Count > 1)
            {
                this.MultCom.Visible = true;
                this.ddlCompany_SelectedIndexChanged(null, null);
            }
            else
            {
                this.MultCom.Visible = false;
                lbtnOk_Click(null, null);

            }

        }
        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.GetUserPermission();
            this.MasComNameAndAdd();
            lbtnOk_Click(null, null);


        }

        private void MasComNameAndAdd()
        {
            //((Image)this.Master.FindControl("ComLogo")).ImageUrl = "";
            string comcod = this.ddlCompany.SelectedValue.ToString();
            DataTable dt1 = ((DataTable)Session["tbllog"]);
            DataRow[] dr = dt1.Select("comcod='" + comcod + "'");
            DataTable dt = ((DataTable)Session["tbllog1"]);
            dt.Rows[0]["comcod"] = comcod;
            Session["tbllog1"] = dt;
            ((Label)this.Master.FindControl("LblGrpCompany")).Text = this.ddlCompany.SelectedItem.Text.Trim();// ((DataTable)Session["tbllog1"]).Rows[0]["comnam"].ToString();
                                                                                                              //((Label)this.Master.FindControl("lbladd")).Text = (dr[0]["comadd"].ToString().Substring(0, 6) == "<br />") ? dr[0]["comadd"].ToString().Substring(6) : dr[0]["comadd"].ToString();
                                                                                                              //((Image)this.Master.FindControl("ComLogo")).ImageUrl = "~/Image/" + "LOGO" + comcod + ".PNG";//"~/Image/LOGO1101.PNG";//


            //string logo = "~/Image/" + "LOGO" + comcod + ".PNG";
            //this.MasComNameAndAdd();
        }
        private void GetUserPermission()
        {




            ProcessAccess ulogin = (ASTUtility.Left(this.ddlCompany.SelectedValue.ToString(), 1) == "4") ? new ProcessAccess() : new ProcessAccess();
            string comcod = this.ddlCompany.SelectedValue.ToString();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string username = hst["username"].ToString();
            string txtuserpass = hst["password"].ToString();
            string pass = ASTUtility.EncodePassword(txtuserpass);

            //        string pass = ASTUtility.EncodePassword(hst["password"].ToString());
            string modulid = "";// this.ddlCompany.SelectedValue.ToString();
            string modulename = "";// this.ddlCompany.SelectedItem.Text.Trim();
            DataSet ds5 = ulogin.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "LOGINUSER", username, pass, modulid, modulename, "", "", "", "", "");
            Session["tblusrlog"] = ds5;
            string Comcode = this.ddlCompany.SelectedValue.ToString();
            string ComName = this.ddlCompany.SelectedItem.ToString();

            DataTable dt1 = (DataTable)Session["tbllog"];
            DataTable dt2 = (DataTable)Session["tbllog1"];
            DataRow[] dr = dt1.Select("comcod='" + Comcode + "'");
            // Hashtable hst = (Hashtable)Session["tblLogin"];
            if (dr.Length > 0)
            {
                hst["comnam"] = dr[0]["comnam"];
                hst["comsnam"] = dr[0]["comsnam"];
                hst["comadd1"] = dr[0]["comadd1"];
                hst["comweb"] = dr[0]["comadd3"];
                hst["combranch"] = dr[0]["combranch"];

                dt2.Rows[0]["comnam"] = dr[0]["comnam"];
                dt2.Rows[0]["comsnam"] = dr[0]["comsnam"];
                dt2.Rows[0]["comadd1"] = dr[0]["comadd1"];
                dt2.Rows[0]["comadd"] = dr[0]["comadd"];
            }

            hst["comcod"] = Comcode;
            //  hst["comnam"] = ComName;
            hst["usrid"] = ds5.Tables[0].Rows[0]["usrid"];
            hst["modulenam"] = "";
            hst["trmid"] = "";
            Session["tblLogin"] = hst;
            Session["tbllog1"] = dt2;

        }
        private void GetFromDate()
        {

            string comcod = this.GetCompCode();
            string date = System.DateTime.Today.ToString("dd-MMM-yyyy");

            switch (comcod)
            {

                case "1101":
                case "3333"://Alliance
                case "3353"://Manama
                case "3355"://Green Wood
                case "3368"://finlay
                case "3367"://Epic
                //case "3101":
                case "3340":

                    Hashtable hst = (Hashtable)Session["tblLogin"];
                    this.txtfrmdate.Text = Convert.ToDateTime(hst["opndate"].ToString()).AddDays(1).ToString("dd-MMM-yyyy");
                    break;

                case "1108":
                case "1109":
                case "3315":
                case "3316":
                case "3317":
                case "3354": // Edison  
                case "3101": // pintech  
                    this.txtfrmdate.Text = Convert.ToDateTime(date.ToString()).AddMonths(-3).ToString("dd-MMM-yyyy");
                    break;


                default:

                    this.txtfrmdate.Text = Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy");
                    break;



            }




        }
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            //DataTable dt = (DataTable)Session["tblspledger"];
            //if (dt == null)
            lbtnOk_Click(null, null);
        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            string comcod = this.GetCompCode();

            switch (comcod)
            {
                case "3336": //
                case "3333":
                case "3335":
                    this.Timer1.Interval = 3600000;
                    break;

                case "3340":
                    this.Timer1.Interval = 600000;
                    break;


                case "1206": // acme
                case "1207": // acme
                case "3338": // acme
                case "3369": // acme
                    this.Timer1.Interval = 60000;
                    break;

                case "3339":
                    this.Timer1.Interval = 300000;//5  Minute;
                    break;


                case "3325":
                case "2325":
                case "3344": // Terranova
                case "3347":// Terranova
                    this.Timer1.Interval = 36000000;
                    break;

                // case "3101":// p2p
                case "1205":
                case "3351":
                case "3352":
                case "8306":

                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).Visible = false;
                    ((LinkButton)this.Master.FindControl("lnkPrint")).Visible = false;

                    this.Timer1.Interval = 3600000;
                    break;


                default:
                    this.Timer1.Interval = 3600000;
                    // this.Timer1.Interval = 300000; 
                    break;



            }

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        public string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string qcomcod = this.Request.QueryString["comcod"] ?? "";
            comcod = qcomcod.Length > 0 ? qcomcod : comcod;
            return comcod;
        }

        //protected void txtdate_TextChanged(object sender, EventArgs e)
        //{
        //    this.SaleRequRpt();
        //    this.RadioButtonList1_SelectedIndexChanged(null, null);
        //}
        //protected void lnkOk_Click(object sender, EventArgs e)
        //{
        //    this.SaleRequRpt();
        //}

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            //DateTime frmdate = Convert.ToDateTime(this.txtfrmdate.Text);
            //DateTime todate = Convert.ToDateTime(this.txttodate.Text);
            //int mon = ASTUtility.Datediff(todate, frmdate);
            //if (mon > 1)
            //{

            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert(' Month Less Than Equal Two.');", true);
            //    return;
            //}


            this.PurchaseInfoRpt();
            this.RadioButtonList1_SelectedIndexChanged(null, null);
        }

        protected void lnkInteface_Click(object sender, EventArgs e)
        {
            this.pnlInterf.Visible = true;
            this.pnlPurchase.Visible = false;
            this.RadioButtonList1.SelectedIndex = 0;
        }
        protected void lnkRept_Click(object sender, EventArgs e)
        {
            this.pnlInterf.Visible = false;
            this.pnlPurchase.Visible = true;
        }


        private string CompanyLength()
        {
            string comcod = this.GetCompCode();
            string length = "";
            switch (comcod)
            {
                //case "3101":           

                case "3330": // Bridge
                case "3333": // Alliance
                case "3335": // Edison
                case "3336": // Suvastu
                case "3337": // Suvastu
                case "1206": // Acme
                case "1207": // Acme
                case "3338": // Acme
                case "3369": // Acme
                case "3339": // Tropical                         
                case "3344": // Terranova

                    length = "";

                    break;


                default:
                    length = "length";

                    break;
            }

            return length;

        }


        private void PurchaseInfoRpt()
        {
            Session.Remove("Alltable");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string mcomcod = hst["mcomcod"].ToString();

            //string comcod = (mcomcod == "0000") ? this.GetCompCode() : this.ddlCompany.SelectedValue.ToString();
            //usrid = comcod + usrid.Substring(4,3);
            string comcod = this.GetCompCode();
            // usrid = comcod + usrid.Substring(4, 3);


            string length = this.CompanyLength(); //project permission user wise for uddl

            string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy");
            // string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string ptype = "001";
            string mrfno = "%" + this.txtmrfno.Text.Trim() + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_PURCHASE_INTERFACE02", "RPTPURCHASEDASHBOARD", frmdate, ptype, length, usrid, mrfno, todate, "", "", "");

            Session["Alltable"] = ds1;
            Session["tblreqChk1"] = (ds1.Tables[1]).Copy();

            if (ds1 == null)
                return;

            string reqcheck = "Checked";
            string chkSecondApp = "";
            string reqcheckapp = "";
            string reqapproval = "Req. App";
            string reqforward = "";
            string OrderfApproved = "";
            string Order2ndAprv = "Ord. 2nd App";
            string billAudit = "Final Bill App.";


            switch (comcod)
            {
                case "3336":
                case "3340":
                    reqcheck = "Req. App.";
                    reqforward = "Forward";
                    reqapproval = "Rate App.";
                    billAudit = "Bill Audit";
                    break;


                case "3335":
                    reqcheck = "Checked";
                    reqforward = "Req. App.";
                    reqapproval = "Req. Final App.";
                    OrderfApproved = "Ord. 1st App";
                    break;

                //case "3101":
                case "3355"://  Green Wood
                    reqcheck = "Checked";
                    reqforward = "Forward";
                    reqapproval = "Req. App.";
                    OrderfApproved = "Ord. 1st App";
                    Order2ndAprv = "Ord. Final App";
                    break;


                //case "3354"://  Edison Real Estate 
                case "1205":  //P2P Construction
                case "3351":  //wecon Properties
                case "3352":  //p2p360
                    OrderfApproved = "Ord. Approval";
                    break;


                case "3354"://  Edison Real Estate 
                    OrderfApproved = "Ord. 1st App";
                    Order2ndAprv = "Ord. Final App";
                    break;

                case "3367": //Epic
                //case "3101": //Epic
                    chkSecondApp = "Mgt App.";
                    reqcheckapp = "Checked App.";
                   
                    break;

                //  case "3101":  
                case "3368"://  Finlay
                    reqcheck = "Checked";
                    reqcheckapp = "Checked App.";
                    reqforward = "Forward";
                    reqapproval = "Req. App.";
                    OrderfApproved = "Ord. 1st App";
                    Order2ndAprv = "Ord. Final App";
                    break;

              


                default:
                    reqcheck = "Checked";
                    reqforward = "Forward";
                    reqapproval = "Req. App.";
                    OrderfApproved = "Ord. 1st App";
                    Order2ndAprv = "Ord. 2nd App";
                    chkSecondApp = "2nd App.";
                    reqcheckapp = "1st App.";
                    break;


            }



            this.RadioButtonList1.Items[0].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + Convert.ToDouble(ds1.Tables[7].Rows[0]["reqqty"]).ToString("#,##0;(#,##0); ") + "</div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'>Status</div></div></div>";
            this.RadioButtonList1.Items[1].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + Convert.ToDouble(ds1.Tables[7].Rows[0]["iscrchecked"]).ToString("#,##0;(#,##0); ") + "</i></div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'>CRM Check</div></div></div>";

            this.RadioButtonList1.Items[2].Text = "<div class='circle-tile'><a><div class='circle-tile-heading red counter'>" + Convert.ToDouble(ds1.Tables[7].Rows[0]["chqqty"]).ToString("#,##0;(#,##0); ") + "</i></div></a><div class='circle-tile-content red'><div class='circle-tile-description text-faded'>" + reqcheck + "</div></div></div>";

            this.RadioButtonList1.Items[3].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + Convert.ToDouble(ds1.Tables[7].Rows[0]["faprvqty"]).ToString("#,##0;(#,##0); ") + "</i></div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'>" + reqcheckapp + "</div></div></div>";

            this.RadioButtonList1.Items[4].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + Convert.ToDouble(ds1.Tables[7].Rows[0]["saprvqty"]).ToString("#,##0;(#,##0); ") + "</i></div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'>" + chkSecondApp + "</div></div></div>"; //2nd App.

            this.RadioButtonList1.Items[5].Text = "<div class='circle-tile'><a><div class='circle-tile-heading orange counter'>" + Convert.ToDouble(ds1.Tables[7].Rows[0]["raproqty"]).ToString("#,##0;(#,##0); ") + "</i></div></a><div class='circle-tile-content orange'><div class='circle-tile-description text-faded'>Rate Prop.</div></div></div>";

            this.RadioButtonList1.Items[6].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-gray counter'>" + Convert.ToDouble(ds1.Tables[7].Rows[0]["frecom"]).ToString("#,##0;(#,##0); ") + "</i></div></a><div class='circle-tile-content dark-gray'><div class='circle-tile-description text-faded'>1st Recom.</div></div></div>";
            this.RadioButtonList1.Items[7].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue  counter'>" + Convert.ToDouble(ds1.Tables[7].Rows[0]["secrecom"]).ToString("#,##0;(#,##0); ") + "</i></div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'>2nd Recom.</div></div></div>";
            this.RadioButtonList1.Items[8].Text = "<div class='circle-tile'><a><div class='circle-tile-heading red counter'>" + Convert.ToDouble(ds1.Tables[7].Rows[0]["threcom"]).ToString("#,##0;(#,##0); ") + "</i></div></a><div class='circle-tile-content red'><div class='circle-tile-description text-faded'>" + reqforward + "</div></div></div>";
            this.RadioButtonList1.Items[9].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + Convert.ToDouble(ds1.Tables[7].Rows[0]["appqty"]).ToString("#,##0;(#,##0); ") + "</i></div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'>" + reqapproval + "</div></div></div>";
            this.RadioButtonList1.Items[10].Text = "<div class='circle-tile'><a><div class='circle-tile-heading orange counter'>" + Convert.ToDouble(ds1.Tables[7].Rows[0]["ordprqty"]).ToString("#,##0;(#,##0); ") + "</i></div></a><div class='circle-tile-content orange'><div class='circle-tile-description text-faded'>Order Proc.</div></div></div>";
            this.RadioButtonList1.Items[11].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-gray counter'>" + Convert.ToDouble(ds1.Tables[7].Rows[0]["worderqty"]).ToString("#,##0;(#,##0); ") + "</i></div></a><div class='circle-tile-content dark-gray'><div class='circle-tile-description text-faded'>Pur. Order</div></div></div>";
            this.RadioButtonList1.Items[12].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + Convert.ToDouble(ds1.Tables[7].Rows[0]["ofapp"]).ToString("#,##0;(#,##0); ") + "</i></div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'>" + OrderfApproved + "</div></div></div>";

            this.RadioButtonList1.Items[13].Text = "<div class='circle-tile'><a><div class='circle-tile-heading red counter'>" + Convert.ToDouble(ds1.Tables[7].Rows[0]["osecapp"]).ToString("#,##0;(#,##0); ") + "</i></div></a><div class='circle-tile-content red'><div class='circle-tile-description text-faded'>" + Order2ndAprv + "</div></div></div>";

            this.RadioButtonList1.Items[14].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + Convert.ToDouble(ds1.Tables[7].Rows[0]["mrrqty"]).ToString("#,##0;(#,##0); ") + "</i></div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'>Received</div></div></div>";

            this.RadioButtonList1.Items[15].Text = "<div class='circle-tile'><a><div class='circle-tile-heading red counter'>" + Convert.ToDouble(ds1.Tables[7].Rows[0]["mrrapp"]).ToString("#,##0;(#,##0); ") + "</i></div></a><div class='circle-tile-content red'><div class='circle-tile-description text-faded'>Received(App)</div></div></div>";

            this.RadioButtonList1.Items[16].Text = "<div class='circle-tile'><a><div class='circle-tile-heading orange counter'>" + Convert.ToDouble(ds1.Tables[7].Rows[0]["billqty"]).ToString("#,##0;(#,##0); ") + "</i></div></a><div class='circle-tile-content orange'><div class='circle-tile-description text-faded'>Bill Confirm.</div></div></div>";
            //this.RadioButtonList1.Items[14].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-gray counter'>" + Convert.ToDouble(ds1.Tables[7].Rows[0]["compqty"]).ToString("#,##0;(#,##0); ") + "</i></div></a><div class='circle-tile-content dark-gray''><div class='circle-tile-description text-faded'>Acc. Update</div></div></div>";

            //Added
            this.RadioButtonList1.Items[18].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-gray counter'>" + Convert.ToDouble(ds1.Tables[7].Rows[0]["compqty"]).ToString("#,##0;(#,##0); ") + "</i></div></a><div class='circle-tile-content dark-gray''><div class='circle-tile-description text-faded'>Acc. Update</div></div></div>";

            if (this.RadioButtonList1.Items[17].Enabled == true)
            {
                this.RadioButtonList1.Items[17].Text = "<div class='circle-tile'><a><div class='circle-tile-heading red counter'>" + Convert.ToDouble(ds1.Tables[7].Rows[0]["billaudit"]).ToString("#,##0;(#,##0); ") + "</i></div></a><div class='circle-tile-content red''><div class='circle-tile-description text-faded'>"+ billAudit + "</div></div></div>";

            }
            ds1.Dispose();



            ////ReqInfo
            //DataTable dt = new DataTable();
            //DataView dv = new DataView();
            //dt = ((DataTable)ds1.Tables[0]).Copy();
            //this.Data_Bind("gvReqInfo", dv.ToTable());

            ////Req Check
            //dt = ((DataTable)ds1.Tables[1]).Copy();
            //dv = dt.DefaultView;
            //dv.RowFilter = ("cstatus = 'Requisition Checked' ");
            //this.Data_Bind("gvReqChk", dv.ToTable());
            ////

            ////First Approval
            //dt = ((DataTable)ds1.Tables[1]).Copy();
            //dv = dt.DefaultView;
            //dv.RowFilter = ("cstatus = 'First Approval' ");
            //this.Data_Bind("gvreqfapproved", dv.ToTable());

            ////Rate Proposal
            //dt = ((DataTable)ds1.Tables[1]).Copy();
            //dv = dt.DefaultView;
            //dv.RowFilter = ("cstatus = 'Rate Proposal' ");
            //this.Data_Bind("gvRatePro", dv.ToTable());


            ////First Recommendate
            //dt = ((DataTable)ds1.Tables[1]).Copy();
            //dv = dt.DefaultView;
            //dv.RowFilter = ("cstatus='First Recommendation'  ");
            ////dv.RowFilter = ("empid ='" + usrid + "'");
            //this.Data_Bind("gvFRec", dv.ToTable());



            ////Second Recommendate
            //dt = ((DataTable)ds1.Tables[1]).Copy();
            //dv = dt.DefaultView;
            //dv.RowFilter = ("cstatus='Second Recommendation'  ");
            ////dv.RowFilter = ("empid ='" + usrid + "'");
            //this.Data_Bind("gvSecRec", dv.ToTable());



            ////Third Recommendate
            //dt = ((DataTable)ds1.Tables[1]).Copy();
            //dv = dt.DefaultView;
            //dv.RowFilter = ("cstatus='Forward'  ");
            ////dv.RowFilter = ("empid ='" + usrid + "'");
            //this.Data_Bind("gvThRec", dv.ToTable());



            ////Rate Approval
            //dt = ((DataTable)ds1.Tables[1]).Copy();
            //dv = dt.DefaultView;
            //dv.RowFilter = ("cstatus='Requisition Approval'  ");
            ////dv.RowFilter = ("empid ='" + usrid + "'");
            //this.Data_Bind("gvRateApp", dv.ToTable());





            ////Order Process

            //dt = (DataTable)ds1.Tables[2];
            //this.Data_Bind("gvOrdeProc", dt);


            ////Work Order
            //dt = (DataTable)ds1.Tables[3];
            ////dv = dt.DefaultView;
            ////dv.RowFilter = ("digstatus = 'Diagnosis' and approved= '' ");
            //this.Data_Bind("gvWrkOrd", dt);


            ////Work Order(1st App)
            //dt = (DataTable)ds1.Tables[4];
            //dv = dt.DefaultView;
            //dv.RowFilter = ("cstatus='Purchase Order (1st Approval)'  ");
            //this.Data_Bind("gvordfapp", dv.ToTable());


            ////Work Order(2nd App)
            //dv = dt.DefaultView;
            //dv.RowFilter = ("cstatus='Purchase Order (2nd Approval)'  ");
            //this.Data_Bind("gvordsapp", dv.ToTable());



            ////MRR
            //dt = (DataTable)ds1.Tables[5];
            //this.Data_Bind("grvMRec", dt);


            ////Bill
            //dt = (DataTable)ds1.Tables[6];
            //this.Data_Bind("gvPurBill", dt);






        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            string value = this.RadioButtonList1.SelectedValue.ToString();
            DataSet ds1 = (DataSet)Session["Alltable"];
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataView dv = new DataView();
            string comcod = this.GetCompCode();
            switch (value)
            {
                case "0":
                    //Status
                    // All Reqinfo  
                    dt = ((DataTable)ds1.Tables[0]).Copy();
                    dv = dt.DefaultView;
                    dt1 = dv.ToTable();
                    this.Data_Bind("gvReqInfo", dt1);
                    this.pnlReqInfo.Visible = true;
                    this.PnlReqChq.Visible = false;
                    this.Pnlfirstapp.Visible = false;
                    this.Pnlsecapp.Visible = false;
                    this.pnlRatePro.Visible = false;
                    this.pnlfRec.Visible = false;
                    this.pnlSecRec.Visible = false;
                    this.pnlThRec.Visible = false;
                    this.pnlRateApp.Visible = false;
                    this.PanelOrProc.Visible = false;
                    this.PaneWorder.Visible = false;
                    this.pnlorderfapp.Visible = false;
                    this.pnlordersapp.Visible = false;
                    this.PanelRecv.Visible = false;
                    this.pnlmrrapp.Visible = false;

                    this.PanelComp.Visible = false;
                    this.PanelBill.Visible = false;
                    this.PanelBillAudit.Visible = false;
                    this.RadioButtonList1.Items[0].Attributes["class"] = "lblactive blink_me";

                    // this.RadioButtonList1.Items[0].Attributes["style"] =  "background:#5A5C59; display:block";
                    if (dt1.Rows.Count > 0)
                    {
                        ((TextBox)this.gvReqInfo.HeaderRow.FindControl("txtSearchrefnum")).Attributes.Add("placeholder", ReadCookie());
                    }

                    break;
                case "1":
                    //Checked
                    dt = ((DataTable)ds1.Tables[1]).Copy();
                    dv = dt.DefaultView;
                    dv.RowFilter = ("cstatus = 'CRM Check' ");
                    this.Data_Bind("gvCRM", dv.ToTable());
                    this.pnlReqInfo.Visible = false;
                    this.pnlCRM.Visible = true;
                    this.PnlReqChq.Visible = false;
                    this.Pnlfirstapp.Visible = false;
                    this.Pnlsecapp.Visible = false;
                    this.pnlRatePro.Visible = false;
                    this.pnlfRec.Visible = false;
                    this.pnlSecRec.Visible = false;
                    this.pnlThRec.Visible = false;
                    this.pnlRateApp.Visible = false;
                    this.PanelOrProc.Visible = false;
                    this.PaneWorder.Visible = false;
                    this.pnlorderfapp.Visible = false;
                    this.pnlordersapp.Visible = false;
                    this.PanelRecv.Visible = false;
                    this.pnlmrrapp.Visible = false;
                    this.PanelComp.Visible = false;
                    this.PanelBill.Visible = false;
                    this.PanelBillAudit.Visible = false;
                    this.RadioButtonList1.Items[1].Attributes["class"] = "lblactive blink_me";
                    //this.RadioButtonList1.Items[1].Attributes.Add("class", "lblactive");
                    //this.RadioButtonList1.Items[1].Attributes["style"] = "background:#5A5C59; display:block";
                    break;

                case "2":
                    //Checked
                    dt = ((DataTable)ds1.Tables[1]).Copy();
                    dv = dt.DefaultView;
                    dv.RowFilter = ("cstatus = 'Checked' ");
                    dt1 = dv.ToTable();
                    this.Data_Bind("gvReqChk", dt1);
                    this.pnlReqInfo.Visible = false;
                    this.pnlCRM.Visible = false;

                    this.PnlReqChq.Visible = true;
                    this.Pnlfirstapp.Visible = false;
                    this.Pnlsecapp.Visible = false;
                    this.pnlRatePro.Visible = false;
                    this.pnlfRec.Visible = false;
                    this.pnlSecRec.Visible = false;
                    this.pnlThRec.Visible = false;
                    this.pnlRateApp.Visible = false;
                    this.PanelOrProc.Visible = false;
                    this.PaneWorder.Visible = false;
                    this.pnlorderfapp.Visible = false;
                    this.pnlordersapp.Visible = false;
                    this.PanelRecv.Visible = false;
                    this.pnlmrrapp.Visible = false;
                    this.PanelComp.Visible = false;
                    this.PanelBill.Visible = false;
                    this.PanelBillAudit.Visible = false;
                    this.RadioButtonList1.Items[2].Attributes["class"] = "lblactive blink_me";
                    //this.RadioButtonList1.Items[1].Attributes.Add("class", "lblactive");
                    //this.RadioButtonList1.Items[1].Attributes["style"] = "background:#5A5C59; display:block";
                    switch (comcod)
                    {
                        case "3316"://Assure
                        case "3315"://Assure
                        case "3317"://Assure
                        //case "3101"://Assure
                        case "1108"://engineering
                        case "1109"://tourism

                            for (int i = 0; i < this.gvReqChk.Rows.Count; i++)
                            {
                                (gvReqChk.Columns[11]).Visible = true;
                                //((TextBox)this.grvissue.Rows[i].FindControl("txtissueamt")).Enabled = false;
                                ((LinkButton)this.gvReqChk.Rows[i].FindControl("btnDirecdelReq")).Visible = true;
                            }
                            break;
                    }

                    if (dt1.Rows.Count > 0)
                    {
                        ((TextBox)this.gvReqChk.HeaderRow.FindControl("txtSearchrefnumrchq")).Attributes.Add("placeholder", ReadCookie());

                    }


                    break;





                case "3":
                    //First Approval
                    dt = ((DataTable)ds1.Tables[1]).Copy();
                    dv = dt.DefaultView;
                    dv.RowFilter = ("cstatus = 'First Approval' ");
                    dt1 = dv.ToTable();

                    this.Data_Bind("gvreqfapproved", dt1);
                    this.pnlReqInfo.Visible = false;
                    this.pnlCRM.Visible = false;

                    this.PnlReqChq.Visible = false;
                    this.Pnlfirstapp.Visible = true;
                    this.Pnlsecapp.Visible = false;
                    this.pnlRatePro.Visible = false;
                    this.PaneWorder.Visible = false;
                    this.pnlfRec.Visible = false;
                    this.pnlSecRec.Visible = false;
                    this.pnlThRec.Visible = false;
                    this.pnlRateApp.Visible = false;
                    this.PanelOrProc.Visible = false;
                    this.pnlorderfapp.Visible = false;
                    this.pnlordersapp.Visible = false;
                    this.PanelRecv.Visible = false;
                    this.pnlmrrapp.Visible = false;
                    this.PanelComp.Visible = false;
                    //this.PanelQC.Visible = false;
                    this.PanelBill.Visible = false;
                    this.PanelBillAudit.Visible = false;
                    this.RadioButtonList1.Items[3].Attributes["class"] = "lblactive blink_me";
                    // this.RadioButtonList1.Items[2].Attributes.Add("class", "lblactive");
                    //this.RadioButtonList1.Items[2].Attributes["style"] = "background:#5A5C59; display:block";
                    break;


                case "4":
                    //2nd Approval
                    dt = ((DataTable)ds1.Tables[1]).Copy();
                    dv = dt.DefaultView;
                    dv.RowFilter = ("cstatus = 'Second Approval' ");
                    this.Data_Bind("gvreqsecapproved", dv.ToTable());
                    this.pnlReqInfo.Visible = false;
                    this.PnlReqChq.Visible = false;
                    this.pnlCRM.Visible = false;

                    this.Pnlfirstapp.Visible = false;
                    this.Pnlsecapp.Visible = true;
                    this.pnlRatePro.Visible = false;
                    this.PaneWorder.Visible = false;
                    this.pnlfRec.Visible = false;
                    this.pnlSecRec.Visible = false;
                    this.pnlThRec.Visible = false;
                    this.pnlRateApp.Visible = false;
                    this.PanelOrProc.Visible = false;
                    this.pnlorderfapp.Visible = false;
                    this.pnlordersapp.Visible = false;
                    this.PanelRecv.Visible = false;
                    this.pnlmrrapp.Visible = false;
                    this.PanelComp.Visible = false;
                    //this.PanelQC.Visible = false;
                    this.PanelBill.Visible = false;
                    this.PanelBillAudit.Visible = false;
                    this.RadioButtonList1.Items[4].Attributes["class"] = "lblactive blink_me";
                    // this.RadioButtonList1.Items[2].Attributes.Add("class", "lblactive");
                    //this.RadioButtonList1.Items[2].Attributes["style"] = "background:#5A5C59; display:block";
                    break;

                case "5":
                    //Rate Proposal
                    dt = ((DataTable)ds1.Tables[1]).Copy();
                    dv = dt.DefaultView;
                    dv.RowFilter = ("cstatus = 'Rate Proposal' ");
                    dt1 = dv.ToTable();
                    this.Data_Bind("gvRatePro", dt1);
                    this.pnlReqInfo.Visible = false;
                    this.PnlReqChq.Visible = false;
                    this.pnlCRM.Visible = false;

                    this.Pnlfirstapp.Visible = false;
                    this.Pnlsecapp.Visible = false;
                    this.pnlRatePro.Visible = true;
                    this.PaneWorder.Visible = false;
                    this.pnlfRec.Visible = false;
                    this.pnlSecRec.Visible = false;
                    this.pnlThRec.Visible = false;
                    this.pnlRateApp.Visible = false;
                    this.PanelOrProc.Visible = false;
                    this.pnlorderfapp.Visible = false;
                    this.pnlordersapp.Visible = false;
                    this.PanelRecv.Visible = false;
                    this.pnlmrrapp.Visible = false;
                    this.PanelComp.Visible = false;
                    //this.PanelQC.Visible = false;
                    this.PanelBill.Visible = false;
                    this.PanelBillAudit.Visible = false;
                    this.RadioButtonList1.Items[5].Attributes["class"] = "lblactive blink_me";
                    // this.RadioButtonList1.Items[3].Attributes.Add("class", "lblactive");
                    // this.RadioButtonList1.Items[3].Attributes["style"] = "background:#5A5C59; display:block";
                    if (dt1.Rows.Count > 0)
                    {
                        ((TextBox)this.gvRatePro.HeaderRow.FindControl("txtSearchrefnumratepro")).Attributes.Add("placeholder", ReadCookie());
                    }

                    break;



                case "6":
                    //First Rec
                    dt = ((DataTable)ds1.Tables[1]).Copy();
                    dv = dt.DefaultView;
                    dv.RowFilter = ("cstatus='First Recommendation'  ");
                    this.Data_Bind("gvFRec", dv.ToTable());

                    this.pnlCRM.Visible = false;
                    this.pnlReqInfo.Visible = false;
                    this.PnlReqChq.Visible = false;
                    this.Pnlfirstapp.Visible = false;
                    this.Pnlsecapp.Visible = false;
                    this.pnlRatePro.Visible = false;
                    this.PaneWorder.Visible = false;
                    this.pnlfRec.Visible = true;
                    this.pnlSecRec.Visible = false;
                    this.pnlThRec.Visible = false;
                    this.pnlRateApp.Visible = false;
                    this.PanelOrProc.Visible = false;
                    this.pnlorderfapp.Visible = false;
                    this.pnlordersapp.Visible = false;
                    this.PanelRecv.Visible = false;
                    this.pnlmrrapp.Visible = false;
                    this.PanelComp.Visible = false;
                    //this.PanelQC.Visible = false;
                    this.PanelBill.Visible = false;
                    this.PanelBillAudit.Visible = false;
                    this.RadioButtonList1.Items[6].Attributes["class"] = "lblactive blink_me";
                    // this.RadioButtonList1.Items[4].Attributes.Add("class", "lblactive");
                    //  this.RadioButtonList1.Items[4].Attributes["style"] = "background:#5A5C59; display:block";


                    break;


                case "7":
                    //Second Rec
                    dt = ((DataTable)ds1.Tables[1]).Copy();
                    dv = dt.DefaultView;
                    dv.RowFilter = ("cstatus='Second Recommendation'  ");
                    this.Data_Bind("gvSecRec", dv.ToTable());
                    this.pnlReqInfo.Visible = false;
                    this.PnlReqChq.Visible = false;
                    this.Pnlfirstapp.Visible = false;
                    this.Pnlsecapp.Visible = false;
                    this.pnlRatePro.Visible = false;
                    this.PaneWorder.Visible = false;
                    this.pnlCRM.Visible = false;

                    this.pnlfRec.Visible = false;
                    this.pnlSecRec.Visible = true;
                    this.pnlThRec.Visible = false;
                    this.pnlRateApp.Visible = false;
                    this.PanelOrProc.Visible = false;
                    this.pnlorderfapp.Visible = false;
                    this.pnlordersapp.Visible = false;
                    this.PanelRecv.Visible = false;
                    this.pnlmrrapp.Visible = false;
                    this.PanelComp.Visible = false;
                    //this.PanelQC.Visible = false;
                    this.PanelBill.Visible = false;
                    this.PanelBillAudit.Visible = false;
                    this.RadioButtonList1.Items[7].Attributes["class"] = "lblactive blink_me";

                    break;


                case "8":
                    //Third Rec
                    dt = ((DataTable)ds1.Tables[1]).Copy();
                    dv = dt.DefaultView;
                    dv.RowFilter = ("cstatus='Forward'  ");
                    this.Data_Bind("gvThRec", dv.ToTable());
                    this.pnlReqInfo.Visible = false;
                    this.PnlReqChq.Visible = false;
                    this.Pnlfirstapp.Visible = false;
                    this.Pnlsecapp.Visible = false;
                    this.pnlRatePro.Visible = false;
                    this.PaneWorder.Visible = false;
                    this.pnlfRec.Visible = false;
                    this.pnlSecRec.Visible = false;
                    this.pnlThRec.Visible = true;
                    this.pnlCRM.Visible = false;
                    this.pnlRateApp.Visible = false;
                    this.PanelOrProc.Visible = false;
                    this.pnlorderfapp.Visible = false;
                    this.pnlordersapp.Visible = false;
                    this.PanelRecv.Visible = false;
                    this.pnlmrrapp.Visible = false;
                    this.PanelComp.Visible = false;
                    //this.PanelQC.Visible = false;
                    this.PanelBill.Visible = false;
                    this.PanelBillAudit.Visible = false;
                    this.RadioButtonList1.Items[8].Attributes["class"] = "lblactive blink_me";
                    //this.RadioButtonList1.Items[6].Attributes.Add("class", "lblactive");
                    //this.RadioButtonList1.Items[6].Attributes["style"] = "background:#5A5C59; display:block";


                    break;



                case "9":
                    //Approval
                    dt = ((DataTable)ds1.Tables[1]).Copy();
                    dv = dt.DefaultView;
                    dv.RowFilter = ("cstatus='Requisition Approval'  ");
                    dt1 = dv.ToTable();
                    this.Data_Bind("gvRateApp", dt1);
                    this.pnlReqInfo.Visible = false;
                    this.PnlReqChq.Visible = false;
                    this.Pnlfirstapp.Visible = false;
                    this.Pnlsecapp.Visible = false;
                    this.pnlCRM.Visible = false;
                    this.pnlRatePro.Visible = false;
                    this.PaneWorder.Visible = false;
                    this.pnlfRec.Visible = false;
                    this.pnlSecRec.Visible = false;
                    this.pnlThRec.Visible = false;
                    this.pnlRateApp.Visible = true;
                    this.PanelOrProc.Visible = false;
                    this.pnlorderfapp.Visible = false;
                    this.pnlordersapp.Visible = false;
                    this.PanelRecv.Visible = false;
                    this.pnlmrrapp.Visible = false;
                    this.PanelComp.Visible = false;
                    //this.PanelQC.Visible = false;
                    this.PanelBill.Visible = false;
                    this.PanelBillAudit.Visible = false;
                    this.RadioButtonList1.Items[9].Attributes["class"] = "lblactive blink_me";
                    //this.RadioButtonList1.Items[7].Attributes.Add("class", "lblactive");
                    // this.RadioButtonList1.Items[7].Attributes["style"] = "background:#5A5C59; display:block";
                    if (dt1.Rows.Count > 0)
                    {
                        ((TextBox)this.gvRateApp.HeaderRow.FindControl("txtSearchrefnuml")).Attributes.Add("placeholder", ReadCookie());
                    }
                    break;


                case "10":

                    dt = (DataTable)ds1.Tables[2];
                    this.Data_Bind("gvOrdeProc", dt);
                    //Order Process
                    this.pnlReqInfo.Visible = false;
                    this.PnlReqChq.Visible = false;
                    this.pnlCRM.Visible = false;
                    this.Pnlfirstapp.Visible = false;
                    this.Pnlsecapp.Visible = false;
                    this.pnlRatePro.Visible = false;
                    this.PaneWorder.Visible = false;
                    this.pnlfRec.Visible = false;
                    this.pnlSecRec.Visible = false;
                    this.pnlThRec.Visible = false;
                    this.pnlRateApp.Visible = false;
                    this.PanelOrProc.Visible = true;
                    this.pnlorderfapp.Visible = false;
                    this.pnlordersapp.Visible = false;
                    this.PanelRecv.Visible = false;
                    this.pnlmrrapp.Visible = false;
                    this.PanelComp.Visible = false;
                    // this.PanelQC.Visible = false;
                    this.PanelBill.Visible = false;
                    this.PanelBillAudit.Visible = false;
                    this.RadioButtonList1.Items[10].Attributes["class"] = "lblactive blink_me";
                    //this.RadioButtonList1.Items[8].Attributes.Add("class", "lblactive");
                    // this.RadioButtonList1.Items[8].Attributes["style"] = "background:#5A5C59; display:block";
                    if (dt.Rows.Count > 0)
                    {
                        ((TextBox)this.gvOrdeProc.HeaderRow.FindControl("txtSearchrefnumordpro")).Attributes.Add("placeholder", ReadCookie());

                    }


                    break;
                case "11":
                    //Work Order

                    dt = (DataTable)ds1.Tables[3];
                    this.Data_Bind("gvWrkOrd", dt);
                    this.pnlReqInfo.Visible = false;
                    this.PnlReqChq.Visible = false;
                    this.pnlCRM.Visible = false;
                    this.Pnlfirstapp.Visible = false;
                    this.Pnlsecapp.Visible = false;
                    this.pnlRatePro.Visible = false;
                    this.PaneWorder.Visible = true;
                    this.pnlfRec.Visible = false;
                    this.pnlSecRec.Visible = false;
                    this.pnlThRec.Visible = false;
                    this.pnlRateApp.Visible = false;
                    this.PanelOrProc.Visible = false;
                    this.pnlorderfapp.Visible = false;
                    this.pnlordersapp.Visible = false;
                    this.PanelRecv.Visible = false;
                    this.pnlmrrapp.Visible = false;
                    this.PanelComp.Visible = false;
                    //this.PanelQC.Visible = false;
                    this.PanelBill.Visible = false;
                    this.PanelBillAudit.Visible = false;
                    this.RadioButtonList1.Items[11].Attributes["class"] = "lblactive blink_me";
                    // this.RadioButtonList1.Items[9].Attributes.Add("class", "lblactive");
                    // this.RadioButtonList1.Items[9].Attributes["style"] = "background:#5A5C59; display:block";
                    if (dt.Rows.Count > 0)
                    {
                        ((TextBox)this.gvWrkOrd.HeaderRow.FindControl("txtSearchrefnumporder")).Attributes.Add("placeholder", ReadCookie());
                    }
                    break;


                case "12":
                    //Work Order(1st Approval)

                    dt = (DataTable)ds1.Tables[4];
                    dv = dt.DefaultView;
                    dv.RowFilter = ("cstatus='Purchase Order (1st Approval)'  ");
                    this.Data_Bind("gvordfapp", dv.ToTable());
                    this.pnlReqInfo.Visible = false;
                    this.PnlReqChq.Visible = false;
                    this.Pnlfirstapp.Visible = false;
                    this.Pnlsecapp.Visible = false;
                    this.pnlRatePro.Visible = false;
                    this.PaneWorder.Visible = false;
                    this.pnlfRec.Visible = false;
                    this.pnlCRM.Visible = false;
                    this.pnlSecRec.Visible = false;
                    this.pnlThRec.Visible = false;
                    this.pnlRateApp.Visible = false;
                    this.PanelOrProc.Visible = false;
                    this.pnlorderfapp.Visible = true;
                    this.pnlordersapp.Visible = false;
                    this.PanelRecv.Visible = false;
                    this.pnlmrrapp.Visible = false;
                    this.PanelComp.Visible = false;
                    //this.PanelQC.Visible = false;
                    this.PanelBill.Visible = false;
                    this.PanelBillAudit.Visible = false;
                    this.RadioButtonList1.Items[12].Attributes["class"] = "lblactive blink_me";
                    // this.RadioButtonList1.Items[9].Attributes.Add("class", "lblactive");
                    // this.RadioButtonList1.Items[9].Attributes["style"] = "background:#5A5C59; display:block";

                    break;

                case "13":
                    //Work Order(2nd Appr)
                    dt = (DataTable)ds1.Tables[4];
                    dv = dt.DefaultView;
                    dv.RowFilter = ("cstatus='Purchase Order (2nd Approval)'  ");
                    this.Data_Bind("gvordsapp", dv.ToTable());
                    this.pnlReqInfo.Visible = false;
                    this.PnlReqChq.Visible = false;
                    this.Pnlfirstapp.Visible = false;
                    this.Pnlsecapp.Visible = false;
                    this.pnlRatePro.Visible = false;
                    this.PaneWorder.Visible = false;
                    this.pnlfRec.Visible = false;
                    this.pnlSecRec.Visible = false;
                    this.pnlThRec.Visible = false;
                    this.pnlRateApp.Visible = false;
                    this.PanelOrProc.Visible = false;
                    this.pnlorderfapp.Visible = false;
                    this.pnlCRM.Visible = false;
                    this.pnlordersapp.Visible = true;
                    this.PanelRecv.Visible = false;
                    this.pnlmrrapp.Visible = false;
                    this.PanelComp.Visible = false;
                    //this.PanelQC.Visible = false;
                    this.PanelBill.Visible = false;
                    this.PanelBillAudit.Visible = false;
                    this.RadioButtonList1.Items[13].Attributes["class"] = "lblactive blink_me";
                    // this.RadioButtonList1.Items[9].Attributes.Add("class", "lblactive");
                    // this.RadioButtonList1.Items[9].Attributes["style"] = "background:#5A5C59; display:block";

                    break;






                case "14":
                    //MRR
                    //MRR
                    dt = (DataTable)ds1.Tables[5];
                    this.Data_Bind("grvMRec", dt);
                    this.pnlReqInfo.Visible = false;
                    this.PnlReqChq.Visible = false;
                    this.Pnlfirstapp.Visible = false;
                    this.Pnlsecapp.Visible = false;
                    this.pnlRatePro.Visible = false;
                    this.PaneWorder.Visible = false;
                    this.pnlfRec.Visible = false;
                    this.pnlCRM.Visible = false;
                    this.pnlSecRec.Visible = false;
                    this.pnlThRec.Visible = false;
                    this.pnlRateApp.Visible = false;
                    this.PanelOrProc.Visible = false;
                    this.pnlorderfapp.Visible = false;
                    this.pnlordersapp.Visible = false;
                    this.PanelRecv.Visible = true;
                    this.pnlmrrapp.Visible = false;
                    this.PanelComp.Visible = false;
                    //this.PanelQC.Visible = false;
                    this.PanelBill.Visible = false;
                    this.PanelBillAudit.Visible = false;
                    this.RadioButtonList1.Items[14].Attributes["class"] = "lblactive blink_me";
                    //this.RadioButtonList1.Items[10].Attributes.Add("class", "lblactive");
                    // this.RadioButtonList1.Items[10].Attributes["style"] = "background:#5A5C59; display:block";
                    if (dt.Rows.Count > 0)
                    {
                        ((TextBox)this.grvMRec.HeaderRow.FindControl("txtSearchrefnummrec")).Attributes.Add("placeholder", ReadCookie());
                    }
                    break;


                case "15"://MRR(Approved)
                    //MRR
                    //MRR
                    //dt = (DataTable)ds1.Tables[6];
                    //this.Data_Bind("gvmrrapp", dt);

                    dt = ((DataTable)ds1.Tables[6]).Copy();
                    dv = dt.DefaultView;
                    dv.RowFilter = ("mrrapp = 0");
                    this.Data_Bind("gvmrrapp", dv.ToTable());

                    this.pnlReqInfo.Visible = false;
                    this.PnlReqChq.Visible = false;
                    this.Pnlfirstapp.Visible = false;
                    this.Pnlsecapp.Visible = false;
                    this.pnlRatePro.Visible = false;
                    this.PaneWorder.Visible = false;
                    this.pnlfRec.Visible = false;
                    this.pnlCRM.Visible = false;
                    this.pnlSecRec.Visible = false;
                    this.pnlThRec.Visible = false;
                    this.pnlRateApp.Visible = false;
                    this.PanelOrProc.Visible = false;
                    this.pnlorderfapp.Visible = false;
                    this.pnlordersapp.Visible = false;
                    this.PanelRecv.Visible = false;
                    this.pnlmrrapp.Visible = true;
                    this.PanelComp.Visible = false;
                    //this.PanelQC.Visible = false;
                    this.PanelBill.Visible = false;
                    this.PanelBillAudit.Visible = false;
                    this.RadioButtonList1.Items[15].Attributes["class"] = "lblactive blink_me";
                    //this.RadioButtonList1.Items[10].Attributes.Add("class", "lblactive");
                    // this.RadioButtonList1.Items[10].Attributes["style"] = "background:#5A5C59; display:block";
                    //if (dt.Rows.Count > 0)
                    //{
                    //    ((TextBox)this.grvMRec.HeaderRow.FindControl("txtSearchrefnummrec")).Attributes.Add("placeholder", ReadCookie());
                    //}
                    break;



                case "16":
                    //Bill
                    //Bill
                    //dt = (DataTable)ds1.Tables[6];
                    //this.Data_Bind("gvPurBill", dt);

                    dt = ((DataTable)ds1.Tables[6]).Copy();
                    dv = dt.DefaultView;
                    dv.RowFilter = ("mrrapp = 1");
                    this.Data_Bind("gvPurBill", dv.ToTable());

                    this.pnlReqInfo.Visible = false;
                    this.PnlReqChq.Visible = false;
                    this.Pnlfirstapp.Visible = false;
                    this.Pnlsecapp.Visible = false;
                    this.pnlRatePro.Visible = false;
                    this.PaneWorder.Visible = false;
                    this.pnlfRec.Visible = false;
                    this.pnlCRM.Visible = false;
                    this.pnlSecRec.Visible = false;
                    this.pnlThRec.Visible = false;
                    this.pnlRateApp.Visible = false;
                    this.PanelOrProc.Visible = false;
                    this.pnlorderfapp.Visible = false;
                    this.pnlordersapp.Visible = false;
                    this.PanelRecv.Visible = false;
                    this.pnlmrrapp.Visible = false;
                    this.PanelComp.Visible = false;
                    //this.PanelQC.Visible = false;
                    this.PanelBill.Visible = true;
                    this.PanelBillAudit.Visible = false;

                    this.RadioButtonList1.Items[16].Attributes["class"] = "lblactive blink_me";
                    // this.RadioButtonList1.Items[11].Attributes.Add("class", "lblactive");
                    // this.RadioButtonList1.Items[11].Attributes["style"] = "background:#5A5C59; display:block";
                    if (dt.Rows.Count > 0)
                    {
                        ((TextBox)this.gvPurBill.HeaderRow.FindControl("txtSearchrefnumbill")).Attributes.Add("placeholder", ReadCookie());
                    }


                    break;


                case "17": // Bill Audit
                    dt = (DataTable)ds1.Tables[8];
                    this.Data_Bind("gvPurBillAudit", dt);
                    this.pnlReqInfo.Visible = false;
                    this.PnlReqChq.Visible = false;
                    this.Pnlfirstapp.Visible = false;
                    this.Pnlsecapp.Visible = false;
                    this.pnlRatePro.Visible = false;
                    this.PaneWorder.Visible = false;
                    this.pnlfRec.Visible = false;
                    this.pnlCRM.Visible = false;
                    this.pnlSecRec.Visible = false;
                    this.pnlThRec.Visible = false;
                    this.pnlRateApp.Visible = false;
                    this.PanelOrProc.Visible = false;
                    this.pnlorderfapp.Visible = false;
                    this.pnlordersapp.Visible = false;
                    this.PanelRecv.Visible = false;
                    this.pnlmrrapp.Visible = false;
                    this.PanelComp.Visible = false;
                    //this.PanelQC.Visible = false;
                    this.PanelBill.Visible = false;
                    this.PanelBillAudit.Visible = true;
                    this.RadioButtonList1.Items[17].Attributes["class"] = "lblactive blink_me";
                    break;
                case "18":
                    //Compilte
                    this.pnlReqInfo.Visible = false;
                    this.PnlReqChq.Visible = false;
                    this.Pnlfirstapp.Visible = false;
                    this.Pnlsecapp.Visible = false;
                    this.pnlRatePro.Visible = false;
                    this.pnlCRM.Visible = false;
                    this.PaneWorder.Visible = false;
                    this.pnlfRec.Visible = false;
                    this.pnlSecRec.Visible = false;
                    this.pnlThRec.Visible = false;
                    this.pnlRateApp.Visible = false;
                    this.PanelOrProc.Visible = false;
                    this.pnlorderfapp.Visible = false;
                    this.pnlordersapp.Visible = false;
                    this.PanelRecv.Visible = false;
                    this.pnlmrrapp.Visible = false;
                    this.PanelBill.Visible = false;
                    this.PanelComp.Visible = true;
                    this.PanelBillAudit.Visible = false;
                    this.RadioButtonList1.Items[18].Attributes["class"] = "lblactive blink_me";
                    //  this.RadioButtonList1.Items[12].Attributes.Add("class", "lblactive");
                    // this.RadioButtonList1.Items[12].Attributes["style"] = "background:#5A5C59; display:block";

                    break;








            }
        }

        protected void gvReqInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrint");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");
                // HyperLink hlink3 = (HyperLink)e.Row.FindControl("lnkbtnEditIN");
                HyperLink hlnkgvgvmrfno = (HyperLink)e.Row.FindControl("hlnkgvgvmrfno");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();
                string mrfno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mrfno")).ToString();
                string comcod1 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();



                TableCell cell = e.Row.Cells[10];
                string cstatus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "cstatus")).ToString();
                if (cstatus == "Bill Confirm")
                {
                    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#4BCF9E");
                }
                if (cstatus == "Purchase Invoice")
                {
                    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#00CBF3");
                }
                if (cstatus == "Rate Proposal")
                {
                    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#5EB75B");
                }
                if (cstatus == "Order Process")
                {
                    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#D95350");
                }
                if (cstatus == "Requisition Approval")
                {
                    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#EFAD4D");
                }
                if (cstatus == "Materials Receved")
                {
                    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#769BF4");
                }
                if (cstatus == "Purchase Order")
                {
                    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#76C9B5");
                }


                //string fDate = Convert.ToDateTime(this.txFdate.Text).ToString("dd-MMM-yyyy");
                //string tDate = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");



                //hlink2.NavigateUrl = "~/F_20_Service/RecProductEntry?Type=Entry";
                //hlink2.ToolTip = "Create New";

                hlnkgvgvmrfno.NavigateUrl = "~/F_14_Pro/RptPurchasetracking?Type=Purchasetrk&reqno=" + reqno + "&comcod=" + comcod1;


            }
        }



        protected void gvReqChk_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrint");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();
                string reqdat = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "reqdat1")).ToString("dd-MMM-yyyy");
                //string comcod1 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();
                //string imesimeno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mimei")).ToString();

                //hlink1.NavigateUrl = "~/F_20_Service/Ser_Print?Type=ProReceived&comcod=" + comcod + "&centrid=" + centrid + "&recvno=" + recvno + "&imesimeno=" + imesimeno;

                hlink2.NavigateUrl = "~/F_12_Inv/PurReqEntry?InputType=ReqCheck&prjcode=" + pactcode + "&genno=" + reqno;

                hlink1.NavigateUrl = "~/F_99_Allinterface/PurchasePrint?Type=ReqPrint&reqno=" + reqno + "&reqdat=" + reqdat;


            }
        }
        protected void gvRatePro_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrint");

                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");
                HyperLink hlink3 = (HyperLink)e.Row.FindControl("HyInprPrintCS");


                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();
                string reqdat = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "reqdat1")).ToString("dd-MMM-yyyy");
                string msrno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "msrno")).ToString();


                //hlink1.NavigateUrl = "~/F_20_Service/Ser_Print?Type=ProReceived&comcod=" + comcod + "&centrid=" + centrid + "&recvno=" + recvno + "&imesimeno=" + imesimeno;

                hlink1.NavigateUrl = "~/F_99_Allinterface/PurchasePrint?Type=ReqPrint&reqno=" + reqno + "&reqdat=" + reqdat;
                hlink2.NavigateUrl = "~/F_12_Inv/PurReqApproval?Type=RateInput&prjcode=" + pactcode + "&genno=" + reqno;



                if (comcod == "1205" || comcod == "3351" || comcod == "3352" || comcod == "3101")
                {
                    hlink3.Visible = true;
                    if (msrno == "")
                    {
                        hlink3.Enabled = false;
                        hlink3.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        hlink3.Enabled = true;
                        hlink3.ForeColor = System.Drawing.Color.Green;
                    }

                }
                else
                {
                    hlink3.Visible = false;
                }
                hlink3.NavigateUrl = "~/F_14_Pro/PurMktSurvey02?Type=CS" + "&msrno=" + msrno;


                //string comcod1 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();
                //string imesimeno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mimei")).ToString();

                //hlink1.NavigateUrl = "~/F_20_Service/Ser_Print?Type=ProReceived&comcod=" + comcod + "&centrid=" + centrid + "&recvno=" + recvno + "&imesimeno=" + imesimeno;

                //hlink2.NavigateUrl = "~/F_12_Inv/PurReqEntry?InputType=ReqCheck&prjcode=" + pactcode + "&genno=" + reqno;

                //hlink1.NavigateUrl = "~/F_99_Allinterface/PurchasePrint?Type=ReqPrint&reqno=" + reqno + "&reqdat=" + reqdat;

            }
        }
        protected void gvRateApp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrint");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");
                HyperLink hlink3 = (HyperLink)e.Row.FindControl("HyInprPrintCS");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();
                string msrno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "msrno")).ToString();

                string reqdat = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "reqdat1")).ToString("dd-MMM-yyyy");
                //hlink1.NavigateUrl = "~/F_20_Service/Ser_Print?Type=ProReceived&comcod=" + comcod + "&centrid=" + centrid + "&recvno=" + recvno + "&imesimeno=" + imesimeno;

                hlink1.NavigateUrl = "~/F_99_Allinterface/PurchasePrint?Type=ReqPrint&reqno=" + reqno + "&reqdat=" + reqdat;
                hlink2.NavigateUrl = "~/F_12_Inv/PurReqApproval?Type=Approval&prjcode=" + pactcode + "&genno=" + reqno;

                if (comcod == "1205" || comcod == "3351" || comcod == "3352" || comcod == "3101")
                {
                    hlink3.Visible = true;
                    if (msrno == "")
                    {
                        hlink3.Enabled = false;
                        hlink3.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        hlink3.Enabled = true;
                        hlink3.ForeColor = System.Drawing.Color.Green;
                    }

                }
                else
                {
                    hlink3.Visible = false;
                }
                hlink3.NavigateUrl = "~/F_14_Pro/PurMktSurvey02?Type=CS" + "&msrno=" + msrno;
            }
        }

        protected void gvOrdeProc_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprFAppPrint");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();

                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();
                string ptype = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ptype")).ToString();
                string prjcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();


                hlink1.NavigateUrl = "~/F_99_Allinterface/PurchasePrint?Type=ReqFinalAppPrint&reqno=" + reqno;


                //hlink1.NavigateUrl = "~/F_20_Service/Ser_Print?Type=ProReceived&comcod=" + comcod + "&centrid=" + centrid + "&recvno=" + recvno + "&imesimeno=" + imesimeno;

                if (ptype == "003")
                {
                    hlink2.NavigateUrl = "~/F_12_Inv/PurMTReqEntry?Type=Entry&genno=" + reqno + "&prjcode=" + prjcode;

                }
                else
                    hlink2.NavigateUrl = "~/F_14_Pro/PurAprovEntry?InputType=PurProposal&genno=" + reqno;

            }
        }
        protected void gvWrkOrd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrint");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();


                string aprovno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "aprovno")).ToString();
                string approvdat = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "aprovdat1")).ToString();
                string ssircode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ssircode")).ToString(); 

                //hlink1.NavigateUrl = "~/F_20_Service/Ser_Print?Type=ProReceived&comcod=" + comcod + "&centrid=" + centrid + "&recvno=" + recvno + "&imesimeno=" + imesimeno;
                hlink1.NavigateUrl = "~/F_99_Allinterface/PurchasePrint?Type=PurApproval&approvno=" + aprovno + "&approvdat=" + approvdat;

                hlink2.NavigateUrl = "~/F_14_Pro/PurWrkOrderEntry?InputType=OrderEntry&genno=" + aprovno+ "&ssircode="+ ssircode;

            }
        }


        protected void lnkbtnOrder_Click(object sender, EventArgs e)
        {
            LinkButton lnkbtnOrder = (LinkButton)FindControl("lnkbtnOrder");

            // string []paymentid=new string[100];
            string comcod = this.GetCompCode();
            int i = 0;
            string caprovno = "", pssirocde = "";
            foreach (GridViewRow gv1 in gvWrkOrd.Rows)
            {

                string chkemerge = ((CheckBox)gv1.FindControl("chkorder")).Checked ? "True" : "False";
                string aprovno = ((Label)gv1.FindControl("lblgvaprovno")).Text;
                string ssirocde = ((Label)gv1.FindControl("lblgvssircode")).Text;
                if (chkemerge == "True")
                {
                    if (i == 0)
                    {
                        caprovno += aprovno;
                        pssirocde = ssirocde;
                        i++;

                    }
                    else
                    {
                        if (pssirocde != ssirocde)
                        {

                            ((Label)this.Master.FindControl("lblmsg")).Text = "Please Select Same Supplier Name";
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                            return;

                        }
                        else
                        {
                            caprovno += aprovno;

                        }


                        pssirocde = ssirocde;



                    }

                    // paymentid[i++] = slnum;
                }
            }

            string hostname = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/F_14_Pro/";
            string currentptah = "PurWrkOrderEntry?InputType=OrderEntry&genno=" + caprovno;
            string totalpath = hostname + currentptah;
            ScriptManager.RegisterStartupScript(this, GetType(), "target", "FunPurchaseOrder('" + totalpath + "');", true);


            //Response.Redirect("~/F_14_Pro/PurWrkOrderEntry?InputType=OrderEntry&genno=" + caprovno);
        }


        protected void grvMRec_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlnkcrystal = (HyperLink)e.Row.FindControl("HyInprPrint"); // crystal print link
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");

                HyperLink hlnkrldc = (HyperLink)e.Row.FindControl("HyperLink2");
                LinkButton lnktbnrdlc = (LinkButton)e.Row.FindControl("lnkRdlcPrint_Recived");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string orderno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "orderno")).ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string sircode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ssircode")).ToString();
                //string imesimeno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mimei")).ToString();

                switch (comcod)
                {
                    // hide crystal print options
                    case "3101":
                    case "3355":
                    case "3353":
                    case "1205":
                    case "3351":
                    case "3352":
                    case "3330":
                    case "3336":// shuvastu
                    case "3337":
                    case "3364": // JBS
                    case "3339": // Tropical
                    case "3354": // Edison
                    case "3366": // Lanco

                    case "1108": // assure
                    case "1109": // assure
                    case "3315": // assure
                    case "3316": // assure

                    case "3357": // Cube
                    case "3367": // Epic


                    case "3358": // Entrust
                    case "3359": // Entrust
                    case "3360": // Entrust
                    case "3361": // Entrust 

                    case "1206": // acme 
                    case "1207": // acme 
                    case "3338": // acme 
                    case "3369": // acme 
                    case "3370": // cpdl 
                        hlnkcrystal.Visible = false;
                        lnktbnrdlc.Visible = false;
                        break;

                    //case "3101":
                    case "3368": // finlay 
                        hlnkcrystal.Visible = false;
                        hlnkrldc.Visible = false;
                        lnktbnrdlc.Visible = true;
                        break;

                    default:
                        hlnkcrystal.Visible = true;
                        lnktbnrdlc.Visible = false;
                        break;
                }

                hlnkcrystal.NavigateUrl = "~/F_99_Allinterface/PurchasePrint?Type=OrderPrint&orderno=" + orderno;
                hlnkrldc.NavigateUrl = "~/F_99_Allinterface/PurchasePrint?Type=OrderPrintNew&orderno=" + orderno;

                if (orderno.Substring(0, 3) == "POR")
                    hlink2.NavigateUrl = "~/F_12_Inv/PurMRREntry?Type=Entry&prjcode=" + pactcode + "&genno=" + orderno + "&sircode=" + sircode;
                else
                    hlink2.NavigateUrl = "~/F_12_Inv/PurMTReqGatePass?Type=Entry&prjcode=" + pactcode + "&genno=" + orderno;
            }
        }
        protected void gvPurBill_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrint");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");
                LinkButton btnDelBill = (LinkButton)e.Row.FindControl("btnDelBill");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string orderno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "orderno")).ToString();
                string mrrno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mrrno")).ToString();
                // string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string sircode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ssircode")).ToString();

                string prjname = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactdesc")).ToString();
                string suppliername = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ssirdesc")).ToString();



                hlink1.NavigateUrl = "~/F_99_Allinterface/PurchasePrint?Type=MRReceipt&mrno=" + mrrno + "&sircode=" + sircode + "&supname=" + suppliername + "&prjname=" + prjname;

                //hlink1.NavigateUrl = "~/F_20_Service/Ser_Print?Type=ProReceived&comcod=" + comcod + "&centrid=" + centrid + "&recvno=" + recvno + "&imesimeno=" + imesimeno;
                if (orderno.Substring(0, 3) == "POR")
                    hlink2.NavigateUrl = "~/F_14_Pro/PurBillEntry?Type=BillEntry&genno=" + orderno + "&sircode=" + sircode;
                else
                    hlink2.NavigateUrl = "~/F_12_Inv/MaterialsTransfer?Type=Entry&genno=" + mrrno;


                //if (comcod == "1205" || comcod == "3351" || comcod == "3352" || comcod == "8306")
                //{
                //    btnDelBill.Visible = false;
                //}


            }
        }

        protected void gvPurBillAudit_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrint");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntryBillAudit");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                // string orderno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "orderno")).ToString();
                string mrrno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mrrno")).ToString();
                // string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string sircode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ssircode")).ToString();

                string billno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "billno")).ToString();



                //hlink1.NavigateUrl = "~/F_20_Service/Ser_Print?Type=ProReceived&comcod=" + comcod + "&centrid=" + centrid + "&recvno=" + recvno + "&imesimeno=" + --imesimeno;
                // if (orderno.Substring(0, 3) == "POR")
                hlink2.NavigateUrl = "~/F_14_Pro/PurBillEntry?Type=BillEntryAudit&genno=" + billno + "&sircode=" + sircode;
                //else
                //    hlink2.NavigateUrl = "~/F_12_Inv/MaterialsTransfer?Type=Entry&genno=" + mrrno;
            }
        }




        protected void grvComp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrint");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();
                string billno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "billno")).ToString();
                //string imesimeno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mimei")).ToString();

                //hlink1.NavigateUrl = "~/F_20_Service/Ser_Print?Type=ProReceived&comcod=" + comcod + "&centrid=" + centrid + "&recvno=" + recvno + "&imesimeno=" + imesimeno;

                //hlink2.NavigateUrl = "~/F_07_Inv/PurBillEntry?Type=BillEntry";
                hlink1.NavigateUrl = "~/F_99_Allinterface/PuchasePrint?Type=BillPrint&comcod=" + comcod + "&billno=" + billno;
                hlink2.NavigateUrl = "~/F_14_Pro/RptPurchaseStatus?Type=Purchase&Rpt=Purchasetrk";

            }
        }
        private DataTable HiddenSameData(DataTable dt1)
        {


            if (dt1.Rows.Count == 0)
                return dt1;
            string comcod = this.GetCompCode();

            switch (comcod)
            {
                // case "3101":
                case "3340":
                    break;
                default:
                    int i = 0;
                    string pactcode = dt1.Rows[0]["pactcode"].ToString();
                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        if (i == 0)
                        {


                            pactcode = dr1["pactcode"].ToString();
                            i++;
                            continue;
                        }

                        if (dr1["pactcode"].ToString() == pactcode)
                        {

                            dr1["pactdesc"] = "";

                        }


                        pactcode = dr1["pactcode"].ToString();
                    }


                    //string pactcode = dt1.Rows[0]["pactcode"].ToString();
                    //for (int j = 1; j < dt1.Rows.Count; j++)
                    //{
                    //    if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                    //    {
                    //        pactcode = dt1.Rows[j]["pactcode"].ToString();
                    //        dt1.Rows[j]["pactdesc"] = "";
                    //    }

                    //    else
                    //        pactcode = dt1.Rows[j]["pactcode"].ToString();

                    //}

                    break;
            }



            return dt1;
        }
        private void Data_Bind(string gv, DataTable dt)
        {
            string comcod = this.GetCompCode();

            switch (gv)
            {
                case "gvReqInfo":
                    this.gvReqInfo.DataSource = HiddenSameData(dt);
                    this.gvReqInfo.DataBind();

                    if (comcod == "1205" || comcod == "3351" || comcod == "3352")
                    {
                        this.gvReqInfo.Columns[4].Visible = true;
                    }
                    else
                    {
                        this.gvReqInfo.Columns[4].Visible = false;
                    }
                    break;

                case "gvReqChk":
                    this.gvReqChk.DataSource = HiddenSameData(dt);
                    this.gvReqChk.DataBind();
                    break;

                case "gvCRM":
                    this.gvCRM.DataSource = HiddenSameData(dt);
                    this.gvCRM.DataBind();

                    break;



                case "gvreqfapproved":
                    this.gvreqfapproved.DataSource = HiddenSameData(dt);
                    this.gvreqfapproved.DataBind();

                    break;

                case "gvreqsecapproved":
                    this.gvreqsecapproved.DataSource = HiddenSameData(dt);
                    this.gvreqsecapproved.DataBind();

                    break;




                case "gvRatePro":

                    this.gvRatePro.DataSource = HiddenSameData(dt);
                    this.gvRatePro.DataBind();
                    if (comcod == "3348")
                    {
                        this.gvRatePro.Columns[4].Visible = true;
                        this.gvRatePro.Columns[5].Visible = true;


                    }

                    if (dt.Rows.Count == 0)
                        return;

                    break;

                case "gvFRec":
                    this.gvFRec.DataSource = HiddenSameData(dt);
                    this.gvFRec.DataBind();
                    break;

                case "gvSecRec":
                    this.gvSecRec.DataSource = HiddenSameData(dt);
                    this.gvSecRec.DataBind();
                    break;

                case "gvThRec":
                    this.gvThRec.DataSource = HiddenSameData(dt);
                    this.gvThRec.DataBind();
                    break;


                case "gvRateApp":
                    this.gvRateApp.DataSource = HiddenSameData(dt);
                    this.gvRateApp.DataBind();
                    break;

                case "gvOrdeProc":

                    this.gvOrdeProc.DataSource = HiddenSameData(dt);
                    this.gvOrdeProc.DataBind();

                    if (dt.Rows.Count == 0)
                        return;



                    break;
                case "gvWrkOrd":
                    this.gvWrkOrd.DataSource = HiddenSameData(dt);
                    this.gvWrkOrd.DataBind();
                    break;



                case "gvordfapp":
                    this.gvordfapp.DataSource = HiddenSameData(dt);
                    this.gvordfapp.DataBind();
                    break;

                case "gvordsapp":
                    this.gvordsapp.DataSource = HiddenSameData(dt);
                    this.gvordsapp.DataBind();
                    break;




                case "grvMRec":

                    this.grvMRec.DataSource = HiddenSameData(dt);
                    this.grvMRec.DataBind();

                    //if (dt.Rows.Count == 0)
                    //    return;


                    //((Label)this.gvDispatch.FooterRow.FindControl("lblDispAmtTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(itmamt)", "")) ?
                    //0 : dt.Compute("sum(itmamt)", ""))).ToString("#,##0.00;(#,##0.00);");
                    //((Label)this.gvDispatch.FooterRow.FindControl("lblDispPTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(paidamt)", "")) ?
                    //0 : dt.Compute("sum(paidamt)", ""))).ToString("#,##0.00;(#,##0.00);");
                    //((Label)this.gvDispatch.FooterRow.FindControl("lblDispQTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(itmqty)", "")) ?
                    //0 : dt.Compute("sum(itmqty)", ""))).ToString("#,##0;(#,##0);");


                    break;

                case "gvmrrapp":

                    this.gvmrrapp.DataSource = HiddenSameData(dt);
                    this.gvmrrapp.DataBind();

                    //if (dt.Rows.Count == 0)
                    //    return;


                    //((Label)this.gvDispatch.FooterRow.FindControl("lblDispAmtTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(itmamt)", "")) ?
                    //0 : dt.Compute("sum(itmamt)", ""))).ToString("#,##0.00;(#,##0.00);");
                    //((Label)this.gvDispatch.FooterRow.FindControl("lblDispPTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(paidamt)", "")) ?
                    //0 : dt.Compute("sum(paidamt)", ""))).ToString("#,##0.00;(#,##0.00);");
                    //((Label)this.gvDispatch.FooterRow.FindControl("lblDispQTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(itmqty)", "")) ?
                    //0 : dt.Compute("sum(itmqty)", ""))).ToString("#,##0;(#,##0);");


                    break;





                case "gvPurBill":

                    this.gvPurBill.DataSource = HiddenSameData(dt);
                    this.gvPurBill.DataBind();
                    if (comcod == "3367")
                    {
                        this.gvPurBill.Columns[9].Visible = true;
                    }
                    break;

                case "grvComp":

                    this.grvComp.DataSource = HiddenSameData(dt);
                    this.grvComp.DataBind();
                    break;

                case "gvPurBillAudit":
                    this.gvPurBillAudit.DataSource = HiddenSameData(dt);
                    this.gvPurBillAudit.DataBind();
                    break;


            }


            this.FooterCalculation(gv, dt);




        }

        private void FooterCalculation(string gv, DataTable dt)
        {


            if (dt.Rows.Count == 0)
                return;
            switch (gv)
            {

                case "gvReqInfo":
                    ((Label)this.gvReqInfo.FooterRow.FindControl("lblgvFApamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(apamt)", "")) ?
                    0 : dt.Compute("sum(apamt)", ""))).ToString("#,##0;(#,##0);");




                    break;
                case "gvReqChk":


                    break;

                case "gvRatePro":



                    break;
                case "gvRateApp":



                    break;

                case "gvOrdeProc":


                    ((Label)this.gvOrdeProc.FooterRow.FindControl("lblgvFOrProAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(apamt)", "")) ?
                    0 : dt.Compute("sum(apamt)", ""))).ToString("#,##0;(#,##0);");



                    break;
                case "gvWrkOrd":
                    ((Label)this.gvWrkOrd.FooterRow.FindControl("lblgvFWoamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(woamt)", "")) ?
                    0 : dt.Compute("sum(woamt)", ""))).ToString("#,##0;(#,##0);");


                    break;

                case "grvMRec":
                    ((Label)this.grvMRec.FooterRow.FindControl("lblgvFWoamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(recvamt)", "")) ?
                    0 : dt.Compute("sum(recvamt)", ""))).ToString("#,##0;(#,##0);");

                    ((Label)this.grvMRec.FooterRow.FindControl("lblgvFreceivedamtor")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mrramt)", "")) ?
                  0 : dt.Compute("sum(mrramt)", ""))).ToString("#,##0;(#,##0);");
                    ((Label)this.grvMRec.FooterRow.FindControl("lblgvFbalamtor")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(balamt)", "")) ?
                  0 : dt.Compute("sum(balamt)", ""))).ToString("#,##0;(#,##0);");


                    break;




                case "gvPurBill":
                    ((Label)this.gvPurBill.FooterRow.FindControl("lblgvFmrramt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mrramt)", "")) ?
                    0 : dt.Compute("sum(mrramt)", ""))).ToString("#,##0;(#,##0);");



                    break;
                case "grvComp":


                    break;
            }
        }



        protected void lnkbtnPrintRD_Click(object sender, EventArgs e)
        {


            //    int rbtIndex = Convert.ToInt16(this.RadioButtonList1.SelectedIndex);
            //    this.RadioButtonList1.Items[rbtIndex].Attributes["style"] = "background: #189697;  display:block; -webkit-border-radius:30px;-moz-border-radius: 30px;border-radius: 30px;";

            //    string code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            //    string centrid = ASTUtility.Left(code, 12);
            //    string orderno = ASTUtility.Right(code, 14);

            //    try
            //    {

            //        string comcod = this.GetCompCode();
            //        Hashtable hst = (Hashtable)Session["tblLogin"];
            //        string comnam = hst["comnam"].ToString();
            //        string compname = hst["compname"].ToString();
            //        string comadd = hst["comadd1"].ToString();
            //        string username = hst["username"].ToString();
            //        string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");




            //        DataSet ds2 = accData.GetTransInfo(comcod, "dbo_Sales.SP_REPORT_SALES_INFO", "RPTCUSTINFORMATION", orderno, centrid, "", "", "", "", "", "", "");
            //        if (ds2 == null)
            //            return;
            //        ReportDocument rptSOrder = new ReportDocument();
            //        //string qType = this.Request.QueryString["Type"].ToString();
            //        //if (qType == "dNote")
            //        //{
            //        //    rptSOrder = new MFGRPT.R_23_SaM.RptSalDelNoteZelta();
            //        //    TextObject txtHeader = rptSOrder.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            //        //    txtHeader.Text = "DELIVERY NOTE";
            //        //}
            //        //else
            //        //{
            //        rptSOrder = new MFGRPT.R_23_SaM.RptSalOrdrZelta();
            //        TextObject txtHeader = rptSOrder.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            //        txtHeader.Text = "SALES ORDER";
            //        // }


            //        TextObject txtrptcomp = rptSOrder.ReportDefinition.ReportObjects["Company"] as TextObject;
            //        txtrptcomp.Text = comnam;



            //        TextObject txtCompAdd = rptSOrder.ReportDefinition.ReportObjects["txtCompAdd"] as TextObject;
            //        txtCompAdd.Text = comadd;

            //        TextObject txtsaledate = rptSOrder.ReportDefinition.ReportObjects["txtsaledate"] as TextObject;
            //        txtsaledate.Text = ds2.Tables[2].Rows[0]["orderdat"].ToString().Trim();

            //        TextObject txtCust = rptSOrder.ReportDefinition.ReportObjects["txtCust"] as TextObject;
            //        txtCust.Text = ds2.Tables[2].Rows[0]["name"].ToString().Trim();

            //        TextObject txtAdd = rptSOrder.ReportDefinition.ReportObjects["txtAdd"] as TextObject;
            //        txtAdd.Text = ds2.Tables[2].Rows[0]["addr"].ToString().Trim();

            //        TextObject txtPhone = rptSOrder.ReportDefinition.ReportObjects["txtPhone"] as TextObject;
            //        txtPhone.Text = ds2.Tables[2].Rows[0]["phone"].ToString().Trim();

            //        TextObject txtTrans = rptSOrder.ReportDefinition.ReportObjects["txtTrans"] as TextObject;
            //        txtTrans.Text = ds2.Tables[0].Rows[0]["courie"].ToString().Trim();

            //        TextObject txtStore = rptSOrder.ReportDefinition.ReportObjects["txtStore"] as TextObject;
            //        txtStore.Text = ds2.Tables[2].Rows[0]["storename"].ToString().Trim();


            //        TextObject txtCode = rptSOrder.ReportDefinition.ReportObjects["txtCode"] as TextObject;
            //        txtCode.Text = ds2.Tables[2].Rows[0]["sirtdes"].ToString().Trim();

            //        TextObject txtOrdTime = rptSOrder.ReportDefinition.ReportObjects["txtOrdTime"] as TextObject;
            //        txtOrdTime.Text = Convert.ToDateTime(ds2.Tables[0].Rows[0]["posteddat"]).ToString("hh:mm:ss tt").Trim();

            //        TextObject txtRemarks = rptSOrder.ReportDefinition.ReportObjects["txtRemarks"] as TextObject;
            //        txtRemarks.Text = (ds2.Tables[2].Rows[0]["narration"]).ToString().Trim();

            //        TextObject txtChannel = rptSOrder.ReportDefinition.ReportObjects["txtChannel"] as TextObject;
            //        txtChannel.Text = (ds2.Tables[2].Rows[0]["chnl"]).ToString().Trim();

            //        TextObject txtsaleNo = rptSOrder.ReportDefinition.ReportObjects["txtsaleNo"] as TextObject;
            //        txtsaleNo.Text = orderno;
            //        //BALANCE 

            //        DataTable dt = ds2.Tables[0];
            //        DataTable dt2 = ds2.Tables[1];
            //        DataTable dt3 = ds2.Tables[2];

            //        double oStdAmt, Dipsamt, ordAmt, balAmt;
            //        oStdAmt = Convert.ToDouble((Convert.IsDBNull(dt3.Compute("sum(dues)", "")) ? 0.00 : dt3.Compute("sum(dues)", "")));
            //        ordAmt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tamount)", "")) ? 0.00 : dt.Compute("sum(tamount)", "")));
            //        Dipsamt = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(paidamt)", "")) ? 0.00 : dt2.Compute("sum(paidamt)", "")));

            //        balAmt = (oStdAmt + ordAmt) - Dipsamt;
            //        //if (qType == "All")
            //        //{
            //        TextObject txtOutStdBal = rptSOrder.ReportDefinition.ReportObjects["txtOutStdBal"] as TextObject;
            //        txtOutStdBal.Text = oStdAmt.ToString("#,##0.00;(#,##0.00);");

            //        TextObject txtDipositeAmt = rptSOrder.ReportDefinition.ReportObjects["txtDipositeAmt"] as TextObject;
            //        txtDipositeAmt.Text = Dipsamt.ToString("#,##0.00;(#,##0.00);");

            //        TextObject txtOrderAmt = rptSOrder.ReportDefinition.ReportObjects["txtOrderAmt"] as TextObject;
            //        txtOrderAmt.Text = ordAmt.ToString("#,##0.00;(#,##0.00);");

            //        TextObject txtBalanceAmt = rptSOrder.ReportDefinition.ReportObjects["txtBalanceAmt"] as TextObject;
            //        txtBalanceAmt.Text = balAmt.ToString("#,##0.00;(#,##0.00);");
            //        //}

            //        TextObject txtAppby = rptSOrder.ReportDefinition.ReportObjects["txtAppby"] as TextObject;
            //        txtAppby.Text = ds2.Tables[2].Rows[0]["appby"].ToString().Trim();

            //        TextObject txtPreBy = rptSOrder.ReportDefinition.ReportObjects["txtPreBy"] as TextObject;
            //        txtPreBy.Text = ds2.Tables[0].Rows[0]["usrname"].ToString().Trim();

            //        TextObject txtuserinfo = rptSOrder.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //        txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //        rptSOrder.SetDataSource(ds2.Tables[0]);

            //        // rptSOrder.Subreports["RptSaleOrderPaymentInfo.rpt"].SetDataSource((DataTable)ViewState["tblpaysch"]);

            //        // rptSOrder.Subreports["RptSaleOrderPaymentInfo.rpt"].SetDataSource(ds2.Tables[1]);


            //        string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //        rptSOrder.SetParameterValue("ComLogo", ComLogo);

            //        Session["Report1"] = rptSOrder;

            //        this.lblprintstkl.Text = @"<script>window.open('../RptViewer?PrintOpt=" +
            //                     "PDF" + "', target='_blank');</script>";


            //        Common.LogStatus("Order", "Order Print", "Order No: ", orderno + " - " + centrid);

            //    }
            //    catch (Exception ex)
            //    {

            //    }

        }

        protected void lnkbtnView_Click(object sender, EventArgs e)
        {
            //    int rbtIndex = Convert.ToInt16(this.RadioButtonList1.SelectedIndex);
            //    this.RadioButtonList1.Items[rbtIndex].Attributes["style"] = "background: #189697;  display:block; -webkit-border-radius:30px;-moz-border-radius: 30px;border-radius: 30px;";
            //    string code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            //    string centrid = ASTUtility.Left(code, 12);
            //    string Delorderno = ASTUtility.Right(code, 14);

            //    if (Delorderno.Length == 0)
            //        return;
            //    try
            //    {
            //        string comcod = this.GetCompCode();
            //        Hashtable hst = (Hashtable)Session["tblLogin"];
            //        string comnam = hst["comnam"].ToString();
            //        string compname = hst["compname"].ToString();
            //        string comadd = hst["comadd1"].ToString();
            //        string username = hst["username"].ToString();
            //        string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //        DataSet ds = accData.GetTransInfo(comcod, "SP_REPORT_CHALLAN", "PRINTDELIVERYCHALLAN", Delorderno, centrid, "", "", "", "", "", "", "");

            //        double Qty = Convert.ToDouble(ds.Tables[0].Rows[0]["delqty"]);
            //        //double Vat = Convert.ToDouble((Convert.IsDBNull(ds.Tables[0].Compute("sum(vat)", "")) ? 0.00 : ds.Tables[0].Compute("sum(vat)", "")));

            //        ReportDocument rptChallan = new MFGRPT.R_23_SaM.RptDelChallanZelta();

            //        TextObject txtCompAdd = rptChallan.ReportDefinition.ReportObjects["txtCompAdd"] as TextObject;
            //        txtCompAdd.Text = comnam + "\n" + "Corporate Office" + "\n" + comadd;
            //        TextObject txtrptHeader = rptChallan.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            //        txtrptHeader.Text = "DELIVERY CHALLAN";

            //        TextObject txtDelNo = rptChallan.ReportDefinition.ReportObjects["txtDelNo"] as TextObject;
            //        txtDelNo.Text = Delorderno;// "DO" + sdelno.Substring(3);
            //        TextObject txtChallan = rptChallan.ReportDefinition.ReportObjects["txtChallan"] as TextObject;
            //        txtChallan.Text = ds.Tables[1].Rows[0]["orderno"].ToString();
            //        TextObject txtDate = rptChallan.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //        txtDate.Text = Convert.ToDateTime(ds.Tables[1].Rows[0]["memodat"]).ToString("dd-MMM-yyyy");
            //        //TextObject txtOrder = rptChallan.ReportDefinition.ReportObjects["txtOrder"] as TextObject;
            //        //txtOrder.Text = (ds.Tables[2].Rows[0]["orderno"].ToString() == "00000000000000") ? "CURRENT SALES" :
            //        //    ASTUtility.Left(ds.Tables[2].Rows[0]["orderno"].ToString(), 2) + ds.Tables[2].Rows[0]["orderno"].ToString().Substring(7, 2) + "-" + ASTUtility.Right(ds.Tables[2].Rows[0]["orderno"].ToString(), 5); ;

            //        TextObject txtCust = rptChallan.ReportDefinition.ReportObjects["txtCust"] as TextObject;
            //        txtCust.Text = ds.Tables[1].Rows[0]["custname"].ToString();
            //        TextObject txtCustadd = rptChallan.ReportDefinition.ReportObjects["txtAdd"] as TextObject;
            //        txtCustadd.Text = ds.Tables[1].Rows[0]["custadd"].ToString();
            //        TextObject txtPhone = rptChallan.ReportDefinition.ReportObjects["txtPhone"] as TextObject;
            //        txtPhone.Text = ds.Tables[1].Rows[0]["custphone"].ToString();

            //        TextObject txtBag = rptChallan.ReportDefinition.ReportObjects["txtBag"] as TextObject;
            //        txtBag.Text = Convert.ToDouble(ds.Tables[1].Rows[0]["bagqty"]).ToString("#,##0;(#,##0);");

            //        TextObject txtSsirdesc = rptChallan.ReportDefinition.ReportObjects["txtSsirdesc"] as TextObject;
            //        txtSsirdesc.Text = ds.Tables[1].Rows[0]["ssirdesc"].ToString();

            //        TextObject txtRemarks = rptChallan.ReportDefinition.ReportObjects["txtRemarks"] as TextObject;
            //        txtRemarks.Text = ds.Tables[1].Rows[0]["narr"].ToString();

            //        TextObject txtOrdTime = rptChallan.ReportDefinition.ReportObjects["txtDelTime"] as TextObject;
            //        txtOrdTime.Text = Convert.ToDateTime(ds.Tables[1].Rows[0]["posteddat"].ToString()).ToString("hh:mm:ss tt");
            //        TextObject txtuserinfo = rptChallan.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //        txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //        rptChallan.SetDataSource(ds.Tables[0]);
            //        TextObject txtPreBy = rptChallan.ReportDefinition.ReportObjects["txtPreBy"] as TextObject;
            //        txtPreBy.Text = ds.Tables[1].Rows[0]["username"].ToString();

            //        TextObject txtDesBy = rptChallan.ReportDefinition.ReportObjects["txtDesBy"] as TextObject;
            //        txtDesBy.Text = ds.Tables[1].Rows[0]["apusername"].ToString();

            //        string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //        rptChallan.SetParameterValue("ComLogo", ComLogo);

            //        Session["Report1"] = rptChallan;
            //        this.lblprintstkl.Text = @"<script>window.open('../RptViewer?PrintOpt=" +
            //                     "PDF" + "', target='_blank');</script>";
            //        //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer?PrintOpt=" +
            //        //              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //        ///
            //        if (ConstantInfo.LogStatus == true)
            //        {
            //            string eventtype = "Delivery ORDER";
            //            string eventdesc = "Print Report";
            //            string eventdesc2 = "Del No : " + Delorderno;
            //            bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //        }


            //    }
            //    catch (Exception ex)
            //    {

            //    }
        }


        protected void lbtnDel_Click(object sender, EventArgs e)
        {
            //((Label)this.Master.FindControl("lblprintstk")).Text = "";
            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            //if (!Convert.ToBoolean(dr1[0]["deleteCk"]))
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
            //    return;
            //}
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();

            //string code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            //string centrid = ASTUtility.Left(code, 12);
            //string orderno = ASTUtility.Right(code, 14);

            ////if (RDsostatus != "Approved")
            ////    return;

            //bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_SALES_ORDER_APPROVAL", "ORDERLASTAPPDELETE", centrid, orderno, "", "", "");

            //if (result == true)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Order Reverse Successfully');", true);
            //}
            //Common.LogStatus("Sales Interface", "Order Reverse", "Order No: ", orderno + " - " + centrid);
        }
        protected void btnDelRev_Click(object sender, EventArgs e)
        {
            //((Label)this.Master.FindControl("lblprintstk")).Text = "";
            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            //if (!Convert.ToBoolean(dr1[0]["deleteCk"]))
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
            //    return;
            //}
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string usrid = hst["usrid"].ToString();

            //string code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            //string centrid = ASTUtility.Left(code, 12);
            //string orderno = ASTUtility.Right(code, 14);
            //if (orderno.Length == 0)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please select your item for Delete');", true);
            //    return;
            //}

            //DataSet ds = accData.GetTransInfo(comcod, "SP_ENTRY_SALES_ORDER_APPROVAL", "CHKFINALAPP", usrid, centrid, "", "", "");
            //if (ds.Tables[0].Rows.Count == 0)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You Have no Permission');", true);
            //    return;
            //}

            //bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_SALES_ORDER_APPROVAL", "ORDERAPPDELETE", centrid, orderno, "", "", "");

            //if (result == true)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Order Reverse Successfully');", true);
            //}
            //Common.LogStatus("Sales Interface", "Order Reverse", "Order No: ", orderno + " - " + centrid);
        }
        protected void btnDelDO_Click(object sender, EventArgs e)
        {
            //((Label)this.Master.FindControl("lblprintstk")).Text = "";
            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            //if (!Convert.ToBoolean(dr1[0]["deleteCk"]))
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
            //    return;
            //}
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string usrid = hst["usrid"].ToString();

            //string code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            //string centrid = ASTUtility.Left(code, 12);
            //string Delorderno = ASTUtility.Right(code, 14);
            //if (Delorderno.Length == 0)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please select your item for Delete');", true);
            //    return;
            //}


            //DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_SALES_ORDER_02", "SHOWDELIVERYORDER", centrid, Delorderno);

            //DataSet ds = lst.GetDataSetForXmlDo(ds1, centrid, Delorderno);

            //bool resulta = accData.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "UPDATEXML01", ds, null, null, centrid, Delorderno);

            //if (!resulta)
            //{

            //    return;
            //}
            //bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_SALES_ORDER_02", "DELETEDOLIST", centrid, Delorderno, "", "", "");
            //if (result == true)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('DO Delete Successfully');", true);
            //}
            //Common.LogStatus("Sales Interface", "DO Delete", "DO No: ", Delorderno + " - " + centrid);
        }
        protected void txtTrack_TextChanged(object sender, EventArgs e)
        {
            this.TrackingHistory_Modal();
        }

        private void TrackingHistory_Modal()
        {
            string comcod = this.GetCompCode();

            string date = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            string Mrfno = this.txtTrack.Text.Trim() + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETREQNO", "000000000000", date, Mrfno, "", "", "", "", "", "");

            string reqno = ds1.Tables[0].Rows[0]["reqno"].ToString();

            DataSet ds = accData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTPURCHASETRACK01", reqno, "", "", "", "", "", "", "", "");
            if (ds == null)
            {
                //this.pnlErrorMsg.Visible = true;
                return;
            }
            else
            {
                //this.PnlProdInor.Visible = true;

            }
            //DataTable dt = HiddenSameData(ds.Tables[0]);

            this.gvPurstk01.DataSource = (ds.Tables[0]);
            this.gvPurstk01.DataBind();
            this.lblMIMEInfo.Focus();
            this.txtTrack.Text = "";

        }



        protected void btnDelReq_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            string url = "PurReqEntry?InputType=Entry";

            DataRow[] dr1 = ASTUtility.PagePermission1(url, (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["delete"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }

            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string genno = ((Label)this.gvReqChk.Rows[RowIndex].FindControl("lblgvreqnorq")).Text.Trim();
            string pactcode = ((Label)this.gvReqChk.Rows[RowIndex].FindControl("lblgvpactcode")).Text.Trim();
            string catdesc = ((Label)this.gvReqChk.Rows[RowIndex].FindControl("lblgvcatagorychk")).Text.Trim();
            string mrfno = ((Label)this.gvReqChk.Rows[RowIndex].FindControl("lblgvmrfno")).Text.Trim();

            DataTable dt = (DataTable)Session["tblreqChk1"];
            string pactdesc = dt.Select("pactcode='" + pactcode + "'")[0]["pactdesc"].ToString();
            //pactcode 
            spanReqInfo.InnerText = "MPR No - " + mrfno + " ( Delete )";
            spanpactdec.InnerText = pactdesc.ToString();
            this.lblreqno.Text = genno.ToString();
            this.lblmrfno.Text = mrfno.ToString();
            this.lblpactcode.Text = pactcode.ToString();
            this.lblresdesc.Text = "Material : " + catdesc.ToString();
            //catdesc

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openReqModal();", true);

        }


        private string GetFirsApporReqChecek()
        {
            string comcod = this.GetCompCode();
            string firstapporreccheck = "";

            switch (comcod)
            {

                case "3101": // Epic
                case "3367": // Epic
                case "3348": // Credence
                             //  case "3101":
                    firstapporreccheck = "FirstaSecond";

                    break;


                //  case "3338": // Acme           
                ////  case "3101":
                //      firstapporreccheck = "Second";

                //      break;

                default:

                    firstapporreccheck = "firstapporovedcheck";

                    break;


            }

            return firstapporreccheck;

        }
        protected void btnDelReqCheck_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            string url = "PurReqEntry?InputType=ReqCheck";
            DataRow[] dr1 = ASTUtility.PagePermission1(url, (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["delete"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string genno = ((Label)this.gvRatePro.Rows[RowIndex].FindControl("lblgvreqnocheck")).Text.Trim();


            //accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_INTERFACE", "RPTACCOUNTDASHBOARD", "%", Date, "%", "", "", "", "", "", "");



            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPURREQINFO", genno, "", "", "", "", "", "", "", "");
            //  DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPURBILLINFO", genno, "",

            if (ds1 == null)
                return;


            bool result = this.XmlDataInsertReq(genno, ds1);

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Fail');", true);
                return;

            }

            string firstapporreccheck = this.GetFirsApporReqChecek();
            bool resulbill = accData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELETEREQCHECKORFAPP", genno, firstapporreccheck, "", "", "", "", "", "", "", "", "", "", "", "", "");




            if (!resulbill)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted  Fail');", true);
                return;
            }
            else
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted Successfully.');", true);

            this.PurchaseInfoRpt();
            this.RadioButtonList1_SelectedIndexChanged(null, null);

        }



        protected void btnDelfapproved_Click(object sender, EventArgs e)
        {


            string comcod = this.GetCompCode();
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            string url = "PurReqEntry?InputType=ReqCheck";
            DataRow[] dr1 = ASTUtility.PagePermission1(url, (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["delete"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string genno = ((Label)this.gvreqfapproved.Rows[RowIndex].FindControl("lblgvreqnorqfapproved")).Text.Trim();


            //accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_INTERFACE", "RPTACCOUNTDASHBOARD", "%", Date, "%", "", "", "", "", "", "");



            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPURREQINFO", genno, "", "", "", "", "", "", "", "");
            //  DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPURBILLINFO", genno, "",

            if (ds1 == null)
                return;


            bool result = this.XmlDataInsertReq(genno, ds1);

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Fail');", true);
                return;

            }


            bool resulbill = accData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELETEREQCHECK", genno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");




            if (!resulbill)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted  Fail');", true);
                return;
            }
            else
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted Successfully.');", true);

            this.PurchaseInfoRpt();
            this.RadioButtonList1_SelectedIndexChanged(null, null);
        }


        private string DelComReqApproval()
        {


            string comcod = this.GetCompCode();
            string delreqapp = "";

            switch (comcod)
            {


                //case "3101":
                case "1103":
                    delreqapp = "DelFSTRECCOM";
                    break;

                default:
                    break;

            }








            return delreqapp;

        }
        protected void btnDelReqRateApp_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            string url = "PurReqApproval?Type=RateInput";
            DataRow[] dr1 = ASTUtility.PagePermission1(url, (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["delete"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string genno = ((Label)this.gvRateApp.Rows[RowIndex].FindControl("lblgvreqnorapp")).Text.Trim();


            //accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_INTERFACE", "RPTACCOUNTDASHBOARD", "%", Date, "%", "", "", "", "", "", "");



            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPURREQINFO", genno, "", "", "", "", "", "", "", "");
            //  DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPURBILLINFO", genno, "",

            if (ds1 == null)
                return;

            bool result = this.XmlDataInsertReq(genno, ds1);

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Fail');", true);
                return;

            }


            DataView dv = ds1.Tables[0].DefaultView;
            dv.RowFilter = ("reqrat>0");
            DataTable dt = dv.ToTable();

            foreach (DataRow drd in dt.Rows)
            {

                string delreqapp = this.DelComReqApproval();
                string rsircode = drd["rsircode"].ToString();
                string spcfcod = drd["spcfcod"].ToString();
                bool resulbill = accData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELETEREQRATEAPP", genno, delreqapp, rsircode, spcfcod, "", "", "", "", "", "", "", "", "", "", "");




                if (!resulbill)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted  Fail');", true);
                    return;
                }
            }


            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted Successfully.');", true);

            this.PurchaseInfoRpt();
            this.RadioButtonList1_SelectedIndexChanged(null, null);

        }

        protected void btnDelReqApproval_Click(object sender, EventArgs e)
        {


            string url = "PurReqApproval?Type=Approval";
            string comcod = this.GetCompCode();
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataRow[] dr1 = ASTUtility.PagePermission1(url, (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["delete"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string genno = ((Label)this.gvOrdeProc.Rows[RowIndex].FindControl("lblgvreqno")).Text.Trim();


            //accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_INTERFACE", "RPTACCOUNTDASHBOARD", "%", Date, "%", "", "", "", "", "", "");



            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPURREQINFO", genno, "", "", "", "", "", "", "", "");
            //  DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPURBILLINFO", genno, "",

            if (ds1 == null)
                return;


            bool result = this.XmlDataInsertReq(genno, ds1);

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Fail');", true);
                return;

            }


            bool resulbill = accData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELETEREQAPROVAL", genno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");




            if (!resulbill)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted  Fail');", true);
                return;
            }
            else
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted Successfully.');", true);

            this.PurchaseInfoRpt();
            this.RadioButtonList1_SelectedIndexChanged(null, null);

        }

        protected void btnDelAprovedNo_Click(object sender, EventArgs e)
        {
            string url = "PurAprovEntry?InputType=PurProposal";
            string comcod = this.GetCompCode();

            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataRow[] dr1 = ASTUtility.PagePermission1(url, (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["delete"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string genno = ((Label)this.gvWrkOrd.Rows[RowIndex].FindControl("lblgvaprovno")).Text.Trim();


            //accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_INTERFACE", "RPTACCOUNTDASHBOARD", "%", Date, "%", "", "", "", "", "", "");


            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPURAPROVINFO", genno, "", "", "", "", "", "", "", "");
            //  DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPURBILLINFO", genno, "",

            if (ds1 == null)
                return;


            bool result = this.XmlDataInsert(genno, ds1);

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Fail');", true);
                return;

            }


            bool resulbill = accData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELETEAPROVINFO", genno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");




            if (!resulbill)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted  Fail');", true);
                return;
            }
            else
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted Successfully.');", true);

            this.PurchaseInfoRpt();
            this.RadioButtonList1_SelectedIndexChanged(null, null);
            //int rowindex = (this.gvPurchase.PageSize) * (this.gvPurchase.PageIndex) + RowIndex;


            //string code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            //string centrid = ASTUtility.Left(code, 12);
            //string orderno = ASTUtility.Right(code, 14);
            //if (orderno.Length == 0)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please select your item for Delete');", true);
            //}

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();

            //DataSet ds1 = accData.GetTransInfo(comcod, "dbo_sales.SP_REPORT_SALES_INFO", "DELORDERSHOW", centrid, orderno);

            //DataSet ds = lst.GetDataSetForXml(ds1, centrid, orderno);

            //bool resulta = accData.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "UPDATEXML01", ds, null, null, centrid, orderno);

            //if (!resulta)
            //{

            //    return;
            //}

            //bool result = accData.UpdateTransInfo(comcod, "dbo_Sales.SP_REPORT_SALES_INFO", "ORDERDELETE", centrid, orderno, "", "", "");

            //if (result == true)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Order Delete Successfully');", true);
            //}
            //Common.LogStatus("Sales Interface", "Order Delete", "Order No: ", orderno + " - " + centrid);
        }


        private string GetDelOrder()
        {
            string comcod = this.GetCompCode();
            string delorder = "";
            switch (comcod)
            {

                case "3355":  // Green Wood
                case "3335":  // Edison
                    delorder = "delforsapp";
                    break;


                case "3354":  // Edison Real Estate
                case "1205":  //P2P Construction
                case "3351":  //wecon Properties
                case "3352":  //p2p360
                case "3101":  // Green Wood
                    delorder = "delsapp";
                    break;

                default:
                    break;


            }

            return delorder;
        }
        protected void btnDelOrder_Click(object sender, EventArgs e)
        {
            string url = "PurWrkOrderEntry?InputType=OrderEntry";
            string comcod = this.GetCompCode();
            //switch (comcod)
            //{
            //    case "1205":
            //    case "3351":
            //    case "3352":
            //        url = "PurMRREntry?Type=Entry";
            //        break;
            //    default:
            //        url = "PurWrkOrderEntry?InputType=OrderEntry"; // PurMRREntry?Entry
            //        break;
            //}
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataRow[] dr1 = ASTUtility.PagePermission1(url, (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["delete"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string genno = ((Label)this.grvMRec.Rows[RowIndex].FindControl("lblgvorderno")).Text.Trim();
            string toorder = Convert.ToDouble(((Label)this.grvMRec.Rows[RowIndex].FindControl("lblgvWoamt")).Text.Trim()).ToString();


            //accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_INTERFACE", "RPTACCOUNTDASHBOARD", "%", Date, "%", "", "", "", "", "", "");


            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPURORDERINFO", genno, "",
                         //  DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPURBILLINFO", genno, "",
                         "", "", "", "", "", "", "");
            if (ds1 == null)
                return;


            bool result = this.XmlDataInsertOrder(genno, ds1);

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Fail');", true);
                return;

            }

            string delorder = this.GetDelOrder();
            bool resulbill = accData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELETEORDERNOAAPPROVED", genno, delorder, toorder, "", "", "", "", "", "", "", "", "", "", "", "");




            if (!resulbill)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted  Fail');", true);
                return;
            }
            else
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted Successfully.');", true);

            this.PurchaseInfoRpt();
            this.RadioButtonList1_SelectedIndexChanged(null, null);
            //int rowindex = (this.gvPurchase.PageSize) * (this.gvPurchase.PageIndex) + RowIndex;


            //string code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            //string centrid = ASTUtility.Left(code, 12);
            //string orderno = ASTUtility.Right(code, 14);
            //if (orderno.Length == 0)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please select your item for Delete');", true);
            //}

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();

            //DataSet ds1 = accData.GetTransInfo(comcod, "dbo_sales.SP_REPORT_SALES_INFO", "DELORDERSHOW", centrid, orderno);

            //DataSet ds = lst.GetDataSetForXml(ds1, centrid, orderno);

            //bool resulta = accData.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "UPDATEXML01", ds, null, null, centrid, orderno);

            //if (!resulta)
            //{

            //    return;
            //}

            //bool result = accData.UpdateTransInfo(comcod, "dbo_Sales.SP_REPORT_SALES_INFO", "ORDERDELETE", centrid, orderno, "", "", "");

            //if (result == true)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Order Delete Successfully');", true);
            //}
            //Common.LogStatus("Sales Interface", "Order Delete", "Order No: ", orderno + " - " + centrid);
        }


        protected void btnDelBill_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string url = "";
            switch (comcod)
            {
                // p2p
                //case "1205":
                //case "3351":
                //case "3352":
                //    url = "PurBillEntry?Type=BillEntry";
                //    break;
                default:
                    url = "PurMRREntry?Type=Entry";
                    break;
            }
            //hlink2.NavigateUrl = "~/F_14_Pro/PurBillEntry?Type=BillEntry&genno=" + orderno + "&sircode=" + sircode;

            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataRow[] dr1 = ASTUtility.PagePermission1(url, (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["delete"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string genno = ((Label)this.gvPurBill.Rows[RowIndex].FindControl("lblgvmrrno")).Text.Trim();


            //accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_INTERFACE", "RPTACCOUNTDASHBOARD", "%", Date, "%", "", "", "", "", "", "");

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPURMRRINFO", genno, "",
                         //  DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPURBILLINFO", genno, "",
                         "", "", "", "", "", "", "");
            if (ds1 == null)
                return;


            bool result = this.XmlDataInsert(genno, ds1);

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Fail');", true);
                return;

            }


            bool resulbill = accData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELPURMRRINFO", genno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!resulbill)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted  Fail');", true);
                return;
            }
            else
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted Successfully.');", true);

            this.PurchaseInfoRpt();
            this.RadioButtonList1_SelectedIndexChanged(null, null);
            //int rowindex = (this.gvPurchase.PageSize) * (this.gvPurchase.PageIndex) + RowIndex;


            //string code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            //string centrid = ASTUtility.Left(code, 12);
            //string orderno = ASTUtility.Right(code, 14);
            //if (orderno.Length == 0)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please select your item for Delete');", true);
            //}

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();

            //DataSet ds1 = accData.GetTransInfo(comcod, "dbo_sales.SP_REPORT_SALES_INFO", "DELORDERSHOW", centrid, orderno);

            //DataSet ds = lst.GetDataSetForXml(ds1, centrid, orderno);

            //bool resulta = accData.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "UPDATEXML01", ds, null, null, centrid, orderno);

            //if (!resulta)
            //{

            //    return;
            //}

            //bool result = accData.UpdateTransInfo(comcod, "dbo_Sales.SP_REPORT_SALES_INFO", "ORDERDELETE", centrid, orderno, "", "", "");

            //if (result == true)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Order Delete Successfully');", true);
            //}
            //Common.LogStatus("Sales Interface", "Order Delete", "Order No: ", orderno + " - " + centrid);
        }



        protected void btnDelBillAudit_Click(object sender, EventArgs e)
        {

            string url = "PurBillEntry?Type=BillEntry";
            string comcod = this.GetCompCode();
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataRow[] dr1 = ASTUtility.PagePermission1(url, (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["delete"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string genno = ((Label)this.gvPurBillAudit.Rows[RowIndex].FindControl("lblgvbillnonoaud")).Text.Trim();


            //accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_INTERFACE", "RPTACCOUNTDASHBOARD", "%", Date, "%", "", "", "", "", "", "");




            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPURBILLINFO", genno, "",
                         "", "", "", "", "", "", "");
            if (ds1 == null)
                return;


            bool result = this.XmlDataInsert(genno, ds1);

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Fail');", true);
                return;

            }


            //bool resulbill = accData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELPURMRRINFO", genno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");

            bool resulbillaudit = accData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELPURBILLAUDITINFO", genno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!resulbillaudit)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted  Fail');", true);
                return;
            }
            else
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted Successfully.');", true);

            this.PurchaseInfoRpt();
            this.RadioButtonList1_SelectedIndexChanged(null, null);
            //int rowindex = (this.gvPurchase.PageSize) * (this.gvPurchase.PageIndex) + RowIndex;


            //string code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            //string centrid = ASTUtility.Left(code, 12);
            //string orderno = ASTUtility.Right(code, 14);
            //if (orderno.Length == 0)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please select your item for Delete');", true);
            //}

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();

            //DataSet ds1 = accData.GetTransInfo(comcod, "dbo_sales.SP_REPORT_SALES_INFO", "DELORDERSHOW", centrid, orderno);

            //DataSet ds = lst.GetDataSetForXml(ds1, centrid, orderno);

            //bool resulta = accData.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "UPDATEXML01", ds, null, null, centrid, orderno);

            //if (!resulta)
            //{

            //    return;
            //}

            //bool result = accData.UpdateTransInfo(comcod, "dbo_Sales.SP_REPORT_SALES_INFO", "ORDERDELETE", centrid, orderno, "", "", "");

            //if (result == true)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Order Delete Successfully');", true);
            //}
            //Common.LogStatus("Sales Interface", "Order Delete", "Order No: ", orderno + " - " + centrid);
        }



        private bool XmlDataInsertReq(string geno, DataSet ds)
        {
            //Log Data
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string usrid = hst["usrid"].ToString();
            string trmnid = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");


            DataSet ds1 = new DataSet("ds1");
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("delbyid", typeof(string));
            dt1.Columns.Add("delseson", typeof(string));
            dt1.Columns.Add("deltrmnid", typeof(string));
            dt1.Columns.Add("deldate", typeof(DateTime));

            DataRow dr1 = dt1.NewRow();
            dr1["delbyid"] = usrid;
            dr1["delseson"] = session;
            dr1["deltrmnid"] = trmnid;
            dr1["deldate"] = Date;
            dt1.Rows.Add(dr1);
            dt1.TableName = "tbl1";

            ds1.Merge(dt1);
            ds1.Merge(ds.Tables[0]);
            ds1.Merge(ds.Tables[1]);
            ds1.Merge(ds.Tables[2]);

            ds1.Tables[0].TableName = "tbl1";
            ds1.Tables[1].TableName = "tbl2";
            ds1.Tables[2].TableName = "tbl3";
            ds1.Tables[3].TableName = "tbl4";


            bool resulta = accData.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "UPDATEXML01", ds1, null, null, geno);

            if (!resulta)
            {

                return false;
            }


            return true;


        }
        private bool XmlDataInsertOrder(string geno, DataSet ds)
        {
            //Log Data
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string usrid = hst["usrid"].ToString();
            string trmnid = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");


            DataSet ds1 = new DataSet("ds1");
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("delbyid", typeof(string));
            dt1.Columns.Add("delseson", typeof(string));
            dt1.Columns.Add("deltrmnid", typeof(string));
            dt1.Columns.Add("deldate", typeof(DateTime));

            DataRow dr1 = dt1.NewRow();
            dr1["delbyid"] = usrid;
            dr1["delseson"] = session;
            dr1["deltrmnid"] = trmnid;
            dr1["deldate"] = Date;
            dt1.Rows.Add(dr1);
            dt1.TableName = "tbl1";

            ds1.Merge(dt1);
            ds1.Merge(ds.Tables[0]);
            ds1.Merge(ds.Tables[1]);
            ds1.Merge(ds.Tables[2]);
            ds1.Merge(ds.Tables[3]);
            ds1.Tables[0].TableName = "tbl1";
            ds1.Tables[1].TableName = "tbl2";
            ds1.Tables[2].TableName = "tbl3";
            ds1.Tables[3].TableName = "tbl4";
            ds1.Tables[4].TableName = "tbl5";

            bool resulta = accData.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "UPDATEXML01", ds1, null, null, geno);

            if (!resulta)
            {

                return false;
            }


            return true;


        }

        private bool XmlDataInsert(string Reqno, DataSet ds)
        {
            //Log Data
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string usrid = hst["usrid"].ToString();
            string trmnid = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");


            DataSet ds1 = new DataSet("ds1");
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("delbyid", typeof(string));
            dt1.Columns.Add("delseson", typeof(string));
            dt1.Columns.Add("deltrmnid", typeof(string));
            dt1.Columns.Add("deldate", typeof(DateTime));

            DataRow dr1 = dt1.NewRow();
            dr1["delbyid"] = usrid;
            dr1["delseson"] = session;
            dr1["deltrmnid"] = trmnid;
            dr1["deldate"] = Date;
            dt1.Rows.Add(dr1);
            dt1.TableName = "tbl1";

            ds1.Merge(dt1);
            ds1.Merge(ds.Tables[0]);
            ds1.Merge(ds.Tables[1]);
            ds1.Tables[0].TableName = "tbl1";
            ds1.Tables[1].TableName = "tbl2";
            ds1.Tables[2].TableName = "tbl3";

            bool resulta = accData.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "UPDATEXML01", ds1, null, null, Reqno);

            if (!resulta)
            {

                return false;
            }


            return true;
        }









        //private DataTable HiddenSameData(DataTable dt1)
        //{
        //    if (dt1.Rows.Count == 0)
        //    {
        //        return dt1;
        //    }


        //    string grp = dt1.Rows[0]["grp"].ToString();

        //    for (int j = 1; j < dt1.Rows.Count; j++)
        //    {
        //        if (dt1.Rows[j]["grp"].ToString() == grp)
        //        {
        //            grp = dt1.Rows[j]["grp"].ToString();
        //            dt1.Rows[j]["grpdesc"] = "";

        //        }

        //        else
        //        {
        //            grp = dt1.Rows[j]["grp"].ToString();
        //        }

        //    }


        //    return dt1;

        //}


        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            string value = this.RadioButtonList1.SelectedValue.ToString();
            switch (value)
            {
                case "0":
                    this.PrintRequisitionStatus();
                    break;
                case "1":
                case "2":
                case "3":
                case "4":
                    this.Printcrateappanorder();
                    break;

                case "5":
                    this.PrintPurchaseOrder();
                    break;

                case "6":
                    this.PrintCashReceive();
                    break;

                case "7":
                    this.PrintBillConfirmation();
                    break;

            }



        }

        private void PrintRequisitionStatus()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            //string comcod = hst["comcod"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            //string hostname = hst["hostname"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;


            DataSet ds1 = (DataSet)Session["Alltable"];
            DataTable dt = ds1.Tables[0];


            var list = dt.DataTableToList<RealEntity.C_99_AllInterface.RptReqSts>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RDLCAccountSetup.GetLocalReport("R_99_AllInterface.RptReqSts", list, null, null);

            Rpt1.SetParameters(new ReportParameter("txtCom", comnam));
            Rpt1.SetParameters(new ReportParameter("txtAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));



            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void Printcrateappanorder()
        {

            DataSet ds1 = (DataSet)Session["Alltable"];
            DataTable dt = new DataTable();
            string value = RadioButtonList1.SelectedValue.ToString();
            switch (value)
            {
                case "1":
                    dt = ds1.Tables[1];
                    break;
                case "2":
                    dt = ds1.Tables[1];
                    break;
                case "3":
                    dt = ds1.Tables[1];
                    break;
                case "4":
                    dt = ds1.Tables[2];
                    break;

            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            //string comcod = hst["comcod"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            //string hostname = hst["hostname"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            string title = (value == "1") ? "Requisition Chequed" : (value == "2") ? "Cash Purchase" : (value == "3") ? "Requisition Approval" : "Order Process";




            var list = dt.DataTableToList<RealEntity.C_99_AllInterface.Rptcrateappanorder>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RDLCAccountSetup.GetLocalReport("R_99_AllInterface.Rptcrateappanorder", list, null, null);

            Rpt1.SetParameters(new ReportParameter("txtCom", comnam));
            Rpt1.SetParameters(new ReportParameter("txtAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("title", title));



            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        private void PrintPurchaseOrder()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            //string comcod = hst["comcod"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            //string hostname = hst["hostname"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;


            DataSet ds1 = (DataSet)Session["Alltable"];
            DataTable dt = ds1.Tables[3];


            var list = dt.DataTableToList<RealEntity.C_99_AllInterface.RptPurOrder>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RDLCAccountSetup.GetLocalReport("R_99_AllInterface.RptPurOrder", list, null, null);

            Rpt1.SetParameters(new ReportParameter("txtCom", comnam));
            Rpt1.SetParameters(new ReportParameter("txtAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));



            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintCashReceive()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            //string comcod = hst["comcod"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            //string hostname = hst["hostname"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;


            DataSet ds1 = (DataSet)Session["Alltable"];
            DataTable dt = ds1.Tables[4];


            var list = dt.DataTableToList<RealEntity.C_99_AllInterface.RptCashRcv>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RDLCAccountSetup.GetLocalReport("R_99_AllInterface.RptCashRcv", list, null, null);

            Rpt1.SetParameters(new ReportParameter("txtCom", comnam));
            Rpt1.SetParameters(new ReportParameter("txtAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));



            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintBillConfirmation()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            //string comcod = hst["comcod"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            //string hostname = hst["hostname"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;


            DataSet ds1 = (DataSet)Session["Alltable"];
            DataTable dt = ds1.Tables[5];


            var list = dt.DataTableToList<RealEntity.C_99_AllInterface.RptBillCon>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RDLCAccountSetup.GetLocalReport("R_99_AllInterface.RptBillCon", list, null, null);

            Rpt1.SetParameters(new ReportParameter("txtCom", comnam));
            Rpt1.SetParameters(new ReportParameter("txtAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));



            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void gvreqfapproved_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink2 = (HyperLink)e.Row.FindControl("hlnkbtnEntryfapproved");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();
                string comcod1 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();
                //string imesimeno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mimei")).ToString();

                //hlink1.NavigateUrl = "~/F_20_Service/Ser_Print?Type=ProReceived&comcod=" + comcod + "&centrid=" + centrid + "&recvno=" + recvno + "&imesimeno=" + imesimeno;

                hlink2.NavigateUrl = "~/F_12_Inv/PurReqEntry?InputType=ReqFirstApproved&prjcode=" + pactcode + "&genno=" + reqno + "&comcod=" + comcod1;

            }
        }

        //protected void lnkmod_Click(object sender, EventArgs e)
        //{
        //    DataSet ds1 = (DataSet)Session["tblusrlog"];
        //    ds1.Tables[0].Rows[0]["moduleid"] = "14";
        //    ds1.Tables[0].Rows[0]["moduleid2"] = "14";
        //    // Response.Redirect("StepofOperation");
        //    ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('../../StepofOperation', target='_self');</script>";
        //    Session["tblusrlog"] = ds1;
        //}
        protected void gvFRec_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrintfrec");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntryfrec");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();

                //hlink1.NavigateUrl = "~/F_20_Service/Ser_Print?Type=ProReceived&comcod=" + comcod + "&centrid=" + centrid + "&recvno=" + recvno + "&imesimeno=" + imesimeno;

                hlink1.NavigateUrl = "~/F_99_Allinterface/PurchasePrint?Type=ReqPrint&reqno=" + reqno;
                hlink2.NavigateUrl = "~/F_12_Inv/PurReqApproval?Type=FirstRecom&prjcode=" + pactcode + "&genno=" + reqno;

            }
        }
        protected void gvSecRec_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrintsrec");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntrysrec");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();

                //hlink1.NavigateUrl = "~/F_20_Service/Ser_Print?Type=ProReceived&comcod=" + comcod + "&centrid=" + centrid + "&recvno=" + recvno + "&imesimeno=" + imesimeno;

                hlink1.NavigateUrl = "~/F_99_Allinterface/PurchasePrint?Type=ReqPrint&reqno=" + reqno;
                hlink2.NavigateUrl = "~/F_12_Inv/PurReqApproval?Type=SecRecom&prjcode=" + pactcode + "&genno=" + reqno;

            }

        }
        protected void gvThRec_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrintthrec");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntrythrec");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();

                //hlink1.NavigateUrl = "~/F_20_Service/Ser_Print?Type=ProReceived&comcod=" + comcod + "&centrid=" + centrid + "&recvno=" + recvno + "&imesimeno=" + imesimeno;

                hlink1.NavigateUrl = "~/F_99_Allinterface/PurchasePrint?Type=ReqPrint&reqno=" + reqno;
                hlink2.NavigateUrl = "~/F_12_Inv/PurReqApproval?Type=ThirdRecom&prjcode=" + pactcode + "&genno=" + reqno;

            }
        }


        protected void btnDelReqfrec_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            string url = "PurReqApproval?Type=RateInput";
            DataRow[] dr1 = ASTUtility.PagePermission1(url, (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["delete"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string genno = ((Label)this.gvFRec.Rows[RowIndex].FindControl("lblgvreqnofrec")).Text.Trim();


            //accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_INTERFACE", "RPTACCOUNTDASHBOARD", "%", Date, "%", "", "", "", "", "", "");



            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPURREQINFO", genno, "", "", "", "", "", "", "", "");
            //  DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPURBILLINFO", genno, "",

            if (ds1 == null)
                return;

            bool result = this.XmlDataInsertReq(genno, ds1);

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Fail');", true);
                return;

            }


            DataView dv = ds1.Tables[0].DefaultView;
            dv.RowFilter = ("frecid=''");
            DataTable dt = dv.ToTable();

            foreach (DataRow drd in dt.Rows)
            {

                // string delreqapp = this.DelComReqApproval();
                string rsircode = drd["rsircode"].ToString();
                string spcfcod = drd["spcfcod"].ToString();
                bool resulbill = accData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELETEREQFIRSTRECOM", genno, rsircode, spcfcod, "", "", "", "", "", "", "", "", "", "", "", "");




                if (!resulbill)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted  Fail');", true);
                    return;
                }
            }


            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted Successfully.');", true);

            this.PurchaseInfoRpt();
            this.RadioButtonList1_SelectedIndexChanged(null, null);
        }

        protected void btnDelReqsrec_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            string url = "PurReqApproval?Type=RateInput";
            DataRow[] dr1 = ASTUtility.PagePermission1(url, (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["delete"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string genno = ((Label)this.gvSecRec.Rows[RowIndex].FindControl("lblgvreqnosrec")).Text.Trim();


            //accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_INTERFACE", "RPTACCOUNTDASHBOARD", "%", Date, "%", "", "", "", "", "", "");



            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPURREQINFO", genno, "", "", "", "", "", "", "", "");
            //  DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPURBILLINFO", genno, "",

            if (ds1 == null)
                return;

            bool result = this.XmlDataInsertReq(genno, ds1);

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Fail');", true);
                return;

            }


            DataView dv = ds1.Tables[0].DefaultView;
            dv.RowFilter = ("secrecid=''");
            DataTable dt = dv.ToTable();

            foreach (DataRow drd in dt.Rows)
            {

                // string delreqapp = this.DelComReqApproval();
                string rsircode = drd["rsircode"].ToString();
                string spcfcod = drd["spcfcod"].ToString();
                bool resulbill = accData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELETEREQSECONDDRECOM", genno, rsircode, spcfcod, "", "", "", "", "", "", "", "", "", "", "", "");




                if (!resulbill)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted  Fail');", true);
                    return;
                }
            }


            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted Successfully.');", true);

            this.PurchaseInfoRpt();
            this.RadioButtonList1_SelectedIndexChanged(null, null);


        }


        private string GetSkiptwo()
        {

            string comcod = this.GetCompCode();
            string skiptwo = "";
            switch (comcod)
            {

                case "3335":
                    //case "3101":   
                    skiptwo = "Skiptwo";
                    break;


                default:
                    break;

            }


            return skiptwo;

        }
        protected void btnDelReqthrec_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            string url = "PurReqApproval?Type=RateInput";
            DataRow[] dr1 = ASTUtility.PagePermission1(url, (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["delete"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string genno = ((Label)this.gvThRec.Rows[RowIndex].FindControl("lblgvreqnothrec")).Text.Trim();


            //accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_INTERFACE", "RPTACCOUNTDASHBOARD", "%", Date, "%", "", "", "", "", "", "");



            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPURREQINFO", genno, "", "", "", "", "", "", "", "");
            //  DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPURBILLINFO", genno, "",

            if (ds1 == null)
                return;

            bool result = this.XmlDataInsertReq(genno, ds1);

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Fail');", true);
                return;

            }


            DataView dv = ds1.Tables[0].DefaultView;
            dv.RowFilter = ("threcid=''");
            DataTable dt = dv.ToTable();

            foreach (DataRow drd in dt.Rows)
            {

                // string delreqapp = this.DelComReqApproval();
                string rsircode = drd["rsircode"].ToString();
                string spcfcod = drd["spcfcod"].ToString();
                string skiptwo = this.GetSkiptwo();
                bool resulbill = accData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELETEREQTHIRDRECOM", genno, rsircode, spcfcod, skiptwo, "", "", "", "", "", "", "", "", "", "", "");




                if (!resulbill)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted  Fail');", true);
                    return;
                }
            }


            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted Successfully.');", true);

            this.PurchaseInfoRpt();
            this.RadioButtonList1_SelectedIndexChanged(null, null);

        }



        protected void btnofapp_Click(object sender, EventArgs e)
        {

            string url = "PurWrkOrderEntry?InputType=FirstApp";
            string comcod = this.GetCompCode();
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataRow[] dr1 = ASTUtility.PagePermission1(url, (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["delete"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string genno = ((Label)this.gvordfapp.Rows[RowIndex].FindControl("lblgvordernoofapp")).Text.Trim();


            //accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_INTERFACE", "RPTACCOUNTDASHBOARD", "%", Date, "%", "", "", "", "", "", "");


            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPURORDERINFO", genno, "",
                         //  DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPURBILLINFO", genno, "",
                         "", "", "", "", "", "", "");
            if (ds1 == null)
                return;


            bool result = this.XmlDataInsertOrder(genno, ds1);

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Fail');", true);
                return;

            }



            bool resulbill = accData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELETEORDERFIRSTAPP", genno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");




            if (!resulbill)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted  Fail');", true);
                return;
            }
            else
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted Successfully.');", true);

            this.PurchaseInfoRpt();
            this.RadioButtonList1_SelectedIndexChanged(null, null);


        }
        protected void btnosapp_Click(object sender, EventArgs e)
        {


            string url = "PurWrkOrderEntry?InputType=SecondApp";
            string comcod = this.GetCompCode();
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataRow[] dr1 = ASTUtility.PagePermission1(url, (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["delete"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string genno = ((Label)this.gvordsapp.Rows[RowIndex].FindControl("lblgvordernosapp")).Text.Trim();


            //accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_INTERFACE", "RPTACCOUNTDASHBOARD", "%", Date, "%", "", "", "", "", "", "");


            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPURORDERINFO", genno, "",
                         //  DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPURBILLINFO", genno, "",
                         "", "", "", "", "", "", "");
            if (ds1 == null)
                return;


            bool result = this.XmlDataInsertOrder(genno, ds1);

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Fail');", true);
                return;

            }



            bool resulbill = accData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELETEORDERSECONDAPP", genno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");




            if (!resulbill)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted  Fail');", true);
                return;
            }
            else
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted Successfully.');", true);

            this.PurchaseInfoRpt();
            this.RadioButtonList1_SelectedIndexChanged(null, null);





        }



        protected void gvordfapp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntryofapp");
                HyperLink hlnkPrintofapp = (HyperLink)e.Row.FindControl("HyInprPrintofapp");


                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();

                string orderno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "orderno")).ToString();


                //hlink1.NavigateUrl = "~/F_20_Service/Ser_Print?Type=ProReceived&comcod=" + comcod + "&centrid=" + centrid + "&recvno=" + recvno + "&imesimeno=" + imesimeno;

                hlink2.NavigateUrl = "~/F_14_Pro/PurWrkOrderEntry?InputType=FirstApp&genno=" + orderno;
                hlnkPrintofapp.NavigateUrl = "~/F_99_Allinterface/PurchasePrint?Type=OrderPrint&orderno=" + orderno;

            }

        }
        protected void gvordsapp_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntryosapp");
                HyperLink hlnkPrintosapp = (HyperLink)e.Row.FindControl("HyInprPrintosapp");
                HyperLink hlnkPrintosappReq = (HyperLink)e.Row.FindControl("HyInprPrintosappReq");


                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();

                string orderno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "orderno")).ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();
                string reqdat = Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy");

                //hlink1.NavigateUrl = "~/F_20_Service/Ser_Print?Type=ProReceived&comcod=" + comcod + "&centrid=" + centrid + "&recvno=" + recvno + "&imesimeno=" + imesimeno;

                hlink2.NavigateUrl = "~/F_14_Pro/PurWrkOrderEntry?InputType=SecondApp&genno=" + orderno;
                hlnkPrintosapp.NavigateUrl = "~/F_99_Allinterface/PurchasePrint?Type=OrderPrint&orderno=" + orderno;
                hlnkPrintosappReq.NavigateUrl = "~/F_99_Allinterface/PurchasePrint?Type=ReqPrint&reqno=" + reqno + "&reqdat=" + reqdat;


            }
        }
        protected void lbtnSendMail_Click(object sender, EventArgs e)
        {

            // /F_14_Pro/PurWrkOrderEntry?InputType=OrderEntry&genno=PAP20191100003

            string url = "PurWrkOrderEntry?InputType=OrderEntry";
            string comcod = this.GetCompCode();
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataRow[] dr1 = ASTUtility.PagePermission1(url, (DataSet)Session["tblusrlog"]);
            if (dr1.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }


            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string genno = ((Label)this.grvMRec.Rows[RowIndex].FindControl("lblgvorderno")).Text.Trim();

            this.AutoSavePDF(genno);
            bool ssl = Convert.ToBoolean(((Hashtable)Session["tblLogin"])["ssl"].ToString());


            switch (ssl)
            {
                case true:
                    this.SendSSLMail(genno);

                    break;

                case false:
                    this.SendNormalMail(genno);
                    break;

            }



        }
        private string PrintCallType()
        {


            string comcod = this.GetCompCode();
            string Calltype = "";
            switch (comcod)
            {
                case "3301":
                case "1301":
                case "3330":
                    //case "3101":
                    Calltype = "SHOWORKORDER01";
                    break;

                case "3332":
                    // case "3101":
                    Calltype = "SHOWORKORDER02";

                    break;
                default:
                    Calltype = "SHOWORKORDER01";
                    break;
            }
            return Calltype;


        }
        private string GetCompOrderCopy()
        {

            string comcod = this.GetCompCode();
            string ordernocopy = "";
            switch (comcod)
            {
                case "3330":
                    // case "3101":
                    ordernocopy = "Bridge";
                    break;
                // case "3101":
                case "3332":
                    ordernocopy = "Innstar";
                    break;
                default:
                    ordernocopy = "";
                    break;


            }
            return ordernocopy;


        }

        private void AutoSavePDF(string orderno)
        {




            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetCompCode();
                string comnam = hst["comnam"].ToString();
                string comadd = hst["comadd1"].ToString();
                string compname = hst["compname"].ToString();
                string username = hst["username"].ToString();
                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
                string wrkid = orderno;

                string Calltype = this.PrintCallType();
                string ordercopy = this.GetCompOrderCopy();
                DataSet _ReportDataSet = this.accData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", Calltype, wrkid, ordercopy, "", "", "", "", "", "", "");


                ViewState["tblOrder"] = _ReportDataSet.Tables[0];
                DataTable dt = _ReportDataSet.Tables[0];
                DataView dv = dt.DefaultView;
                dv.RowFilter = ("grp='A'");
                dt = dv.ToTable();


                string Para1 = _ReportDataSet.Tables[1].Rows[0]["leterdes"].ToString();
                string Orderdate = Convert.ToDateTime(_ReportDataSet.Tables[1].Rows[0]["orderdat"]).ToString("MMMM  dd, yyyy");
                string SupName = _ReportDataSet.Tables[1].Rows[0]["ssirdesc"].ToString();
                string Address = _ReportDataSet.Tables[1].Rows[0]["address"].ToString();
                string Cperson = _ReportDataSet.Tables[1].Rows[0]["cperson"].ToString();
                string Phone = _ReportDataSet.Tables[1].Rows[0]["phone"].ToString();
                string mobile = _ReportDataSet.Tables[1].Rows[0]["mobile"].ToString();

                // DataTable dtterm = _ReportDataSet.Tables[2];

                DataTable dtterm = _ReportDataSet.Tables[2];
                DataTable dtord = _ReportDataSet.Tables[4];
                DataTable dtpaycch = _ReportDataSet.Tables[5];

                // string Type = this.CompanyPrintWorkOrder();
                ReportDocument rptwork = new ReportDocument();

                string fax = _ReportDataSet.Tables[1].Rows[0]["fax"].ToString();

                string trmplace = ((comcod == "3338") ? "1. " + dtterm.Rows[0]["termssubj"].ToString() : "*" + dtterm.Rows[0]["termssubj"].ToString() + " : ");
                string place = dtterm.Rows[0]["termsdesc"].ToString().Trim();
                string trmpdate = ((comcod == "3338") ? "2. " + dtterm.Rows[1]["termssubj"].ToString() : "*" + dtterm.Rows[1]["termssubj"].ToString() + " : ");
                string pdate = dtterm.Rows[1]["termsdesc"].ToString().Trim();
                string trmcarring = ((comcod == "3338") ? "3. " + dtterm.Rows[2]["termssubj"].ToString() : "*" + dtterm.Rows[2]["termssubj"].ToString() + " : ");
                string carring = dtterm.Rows[2]["termsdesc"].ToString().Trim();
                string trmbill = (comcod == "3330") ? "" : (comcod == "3338") ? "4. " + (dtterm.Rows[3]["termssubj"]).ToString() : "*" + dtterm.Rows[3]["termssubj"].ToString() + ": ";
                string bill = (comcod == "3330") ? ("* " + dtterm.Rows[3]["termsdesc"].ToString().Trim()) : dtterm.Rows[3]["termsdesc"].ToString().Trim();
                string trmpayment = ((comcod == "3338") ? dtterm.Rows[4]["termssubj"].ToString() : "*" + dtterm.Rows[4]["termssubj"].ToString() + " : ");
                string payment = dtterm.Rows[4]["termsdesc"].ToString().Trim();

                string trmothers = ((comcod == "3338") ? dtterm.Rows[5]["termssubj"].ToString() : "*" + dtterm.Rows[5]["termssubj"].ToString() + " : ");
                string Others = dtterm.Rows[5]["termsdesc"].ToString().Trim();

                // For Acme




                //      
                string trmcperson = ((dtterm.Rows.Count == 0) ? "" : dtterm.Select("termsid='010'").Length > 0 ? "* " + (dtterm.Select("termsid='010'")[0]["termssubj"]).ToString() + " : " : "");
                string cperson = ((dtterm.Rows.Count == 0) ? "" : dtterm.Select("termsid='010'").Length > 0 ? (dtterm.Select("termsid='010'")[0]["termsdesc"]).ToString() : ""); ;



                switch (comcod)
                {


                    case "3332":
                        //case "3101":
                        rptwork = new RealERPRPT.R_14_Pro.rptWorkOrderInstar();
                        TextObject rpttxtReqIns = rptwork.ReportDefinition.ReportObjects["txtReq"] as TextObject;
                        rpttxtReqIns.Text = _ReportDataSet.Tables[3].Rows[0]["reqnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqdat"].ToString();
                        TextObject rpttxtReqAppIns = rptwork.ReportDefinition.ReportObjects["txtReqA"] as TextObject;
                        rpttxtReqAppIns.Text = _ReportDataSet.Tables[3].Rows[0]["reqanam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqadat"].ToString();
                        TextObject rpttxtOrdIns = rptwork.ReportDefinition.ReportObjects["txtOrd"] as TextObject;
                        rpttxtOrdIns.Text = _ReportDataSet.Tables[3].Rows[0]["appnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["appdat"].ToString();
                        TextObject rpttxtWordIns = rptwork.ReportDefinition.ReportObjects["txtWord"] as TextObject;
                        rpttxtWordIns.Text = _ReportDataSet.Tables[3].Rows[0]["ordnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["orddat"].ToString();
                        TextObject rpttxtcheckIns = rptwork.ReportDefinition.ReportObjects["check"] as TextObject;
                        rpttxtcheckIns.Text = _ReportDataSet.Tables[3].Rows[0]["checknam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["checkdat"].ToString();


                        break;


                    case "3336":
                    case "3337":


                        rptwork = new RealERPRPT.R_14_Pro.rptWorkOrderSuvastu();


                        //Sign In
                        TextObject txtatt = rptwork.ReportDefinition.ReportObjects["txtatt"] as TextObject;
                        txtatt.Text = Cperson;

                        TextObject rpttxtReqSuv = rptwork.ReportDefinition.ReportObjects["txtReq"] as TextObject;
                        rpttxtReqSuv.Text = _ReportDataSet.Tables[3].Rows[0]["reqnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqdat"].ToString();
                        TextObject rpttxtReqAppSuv = rptwork.ReportDefinition.ReportObjects["txtReqA"] as TextObject;
                        rpttxtReqAppSuv.Text = _ReportDataSet.Tables[3].Rows[0]["reqanam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqadat"].ToString();
                        TextObject rpttxtOrdSuv = rptwork.ReportDefinition.ReportObjects["txtOrd"] as TextObject;
                        rpttxtOrdSuv.Text = _ReportDataSet.Tables[3].Rows[0]["appnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["appdat"].ToString();
                        TextObject rpttxtWordSuv = rptwork.ReportDefinition.ReportObjects["txtWord"] as TextObject;
                        rpttxtWordSuv.Text = _ReportDataSet.Tables[3].Rows[0]["ordnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["orddat"].ToString();
                        TextObject rpttxtcheckSuv = rptwork.ReportDefinition.ReportObjects["check"] as TextObject;
                        rpttxtcheckSuv.Text = _ReportDataSet.Tables[3].Rows[0]["checknam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["checkdat"].ToString();
                        TextObject txtAdvancedSuv = rptwork.ReportDefinition.ReportObjects["txtAdvanced"] as TextObject;
                        txtAdvancedSuv.Text = Convert.ToDouble(dtord.Rows[0]["advamt"]).ToString("#,##0.00;(#,##0.00); "); ;
                        TextObject txtappbySuv = rptwork.ReportDefinition.ReportObjects["txtappby"] as TextObject;
                        txtappbySuv.Text = "Approved By";

                        TextObject txtmoblieNumber = rptwork.ReportDefinition.ReportObjects["txtmoblieNumber"] as TextObject;
                        txtmoblieNumber.Text = mobile;

                        //txtappby.Text = (comcod == "3335") ? "Head of Procurement" : "Approved By";

                        // sign end 
                        break;


                    case "3338":
                        // case "3101":

                        rptwork = new RealERPRPT.R_14_Pro.rptWorkOrderAcme();


                        //Sign In


                        TextObject rpttxtReqAcme = rptwork.ReportDefinition.ReportObjects["txtReq"] as TextObject;
                        rpttxtReqAcme.Text = _ReportDataSet.Tables[3].Rows[0]["reqnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqdat"].ToString();
                        TextObject rpttxtReqAppAcme = rptwork.ReportDefinition.ReportObjects["txtReqA"] as TextObject;
                        rpttxtReqAppAcme.Text = _ReportDataSet.Tables[3].Rows[0]["reqanam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqadat"].ToString();
                        TextObject rpttxtOrdAcme = rptwork.ReportDefinition.ReportObjects["txtOrd"] as TextObject;
                        rpttxtOrdAcme.Text = _ReportDataSet.Tables[3].Rows[0]["appnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["appdat"].ToString();
                        TextObject rpttxtWordAcme = rptwork.ReportDefinition.ReportObjects["txtWord"] as TextObject;
                        rpttxtWordAcme.Text = _ReportDataSet.Tables[3].Rows[0]["ordnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["orddat"].ToString();
                        TextObject rpttxtcheckAcme = rptwork.ReportDefinition.ReportObjects["check"] as TextObject;

                        TextObject txtfaprname = rptwork.ReportDefinition.ReportObjects["txtfaprname"] as TextObject;
                        txtfaprname.Text = _ReportDataSet.Tables[3].Rows[0]["faprname"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["faprdat"].ToString();

                        rpttxtcheckAcme.Text = _ReportDataSet.Tables[3].Rows[0]["checknam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["checkdat"].ToString();
                        TextObject txtAdvancedAcme = rptwork.ReportDefinition.ReportObjects["txtAdvanced"] as TextObject;
                        txtAdvancedAcme.Text = Convert.ToDouble(dtord.Rows[0]["advamt"]).ToString("#,##0.00;(#,##0.00); "); ;
                        //TextObject txtappbyAcme = rptwork.ReportDefinition.ReportObjects["txtappby"] as TextObject;
                        //txtappbyAcme.Text = "Approved By";

                        TextObject txtPhoneNumber = rptwork.ReportDefinition.ReportObjects["txtPhoneNumber"] as TextObject;
                        txtPhoneNumber.Text = Phone;

                        // Section part

                        string Others7 = dtterm.Select("termsid='007'").Length > 0 ? ((dtterm.Select("termsid='007'")[0]["termsdesc"]).ToString()) : "";

                        string Others8 = dtterm.Select("termsid='008'").Length > 0 ? ((dtterm.Select("termsid='008'")[0]["termsdesc"]).ToString()) : "";

                        string Others9 = dtterm.Select("termsid='009'").Length > 0 ? ((dtterm.Select("termsid='009'")[0]["termsdesc"]).ToString()) : "";
                        string Others11 = dtterm.Select("termsid='011'").Length > 0 ? ((dtterm.Select("termsid='011'")[0]["termsdesc"]).ToString()) : "";
                        string Others12 = dtterm.Select("termsid='012'").Length > 0 ? ((dtterm.Select("termsid='012'")[0]["termsdesc"]).ToString()) : "";
                        string Others13 = dtterm.Select("termsid='013'").Length > 0 ? ((dtterm.Select("termsid='013'")[0]["termsdesc"]).ToString()) : "";
                        string Others14 = dtterm.Select("termsid='014'").Length > 0 ? ((dtterm.Select("termsid='014'")[0]["termsdesc"]).ToString()) : "";
                        string Others15 = dtterm.Select("termsid='015'").Length > 0 ? ((dtterm.Select("termsid='015'")[0]["termsdesc"]).ToString()) : "";
                        string Others16 = dtterm.Select("termsid='016'").Length > 0 ? ((dtterm.Select("termsid='016'")[0]["termsdesc"]).ToString()) : "";









                        TextObject txtothers7 = rptwork.ReportDefinition.ReportObjects["others1"] as TextObject;
                        txtothers7.Text = (Others7.Length > 0) ? Others7 : "";
                        rptwork.ReportDefinition.Sections["GroupFooterSection15"].SectionFormat.EnableSuppress = (Others7.Length > 0) ? false : true;

                        TextObject txtothers8 = rptwork.ReportDefinition.ReportObjects["others2"] as TextObject;
                        txtothers8.Text = (Others8.Length > 0) ? Others8 : "";
                        rptwork.ReportDefinition.Sections["GroupFooterSection16"].SectionFormat.EnableSuppress = (Others8.Length > 0) ? false : true;

                        TextObject txtothers9 = rptwork.ReportDefinition.ReportObjects["others3"] as TextObject;
                        txtothers9.Text = (Others9.Length > 0) ? Others9 : "";
                        rptwork.ReportDefinition.Sections["GroupFooterSection17"].SectionFormat.EnableSuppress = (Others9.Length > 0) ? false : true;

                        TextObject txtothers10 = rptwork.ReportDefinition.ReportObjects["others4"] as TextObject;
                        txtothers10.Text = (Others11.Length > 0) ? Others11 : "";
                        rptwork.ReportDefinition.Sections["GroupFooterSection18"].SectionFormat.EnableSuppress = (Others11.Length > 0) ? false : true;
                        TextObject txtothers12 = rptwork.ReportDefinition.ReportObjects["others5"] as TextObject;
                        txtothers12.Text = (Others11.Length > 0) ? Others12 : "";
                        rptwork.ReportDefinition.Sections["GroupFooterSection19"].SectionFormat.EnableSuppress = (Others12.Length > 0) ? false : true;
                        TextObject txtothers13 = rptwork.ReportDefinition.ReportObjects["others6"] as TextObject;
                        txtothers13.Text = (Others13.Length > 0) ? Others13 : "";
                        rptwork.ReportDefinition.Sections["GroupFooterSection20"].SectionFormat.EnableSuppress = (Others13.Length > 0) ? false : true;
                        TextObject txtothers14 = rptwork.ReportDefinition.ReportObjects["others7"] as TextObject;
                        txtothers14.Text = (Others14.Length > 0) ? Others14 : "";
                        rptwork.ReportDefinition.Sections["GroupFooterSection21"].SectionFormat.EnableSuppress = (Others14.Length > 0) ? false : true;
                        TextObject txtothers15 = rptwork.ReportDefinition.ReportObjects["others8"] as TextObject;
                        txtothers15.Text = (Others14.Length > 0) ? Others15 : "";
                        rptwork.ReportDefinition.Sections["GroupFooterSection22"].SectionFormat.EnableSuppress = (Others15.Length > 0) ? false : true;

                        TextObject txtothers16 = rptwork.ReportDefinition.ReportObjects["others9"] as TextObject;
                        txtothers16.Text = (Others16.Length > 0) ? Others16 : "";
                        rptwork.ReportDefinition.Sections["GroupFooterSection23"].SectionFormat.EnableSuppress = (Others16.Length > 0) ? false : true;

                        //TextObject txtothers10 = rptwork.ReportDefinition.ReportObjects["others10"] as TextObject;
                        //txtothers10.Text = (Others10.Length > 0) ?"*"+ Others10 : "";
                        //rptwork.ReportDefinition.Sections["GroupFooterSection24"].SectionFormat.EnableSuppress = (Others10.Length > 0) ? false : true;


                        break;

                    case "3335":
                        rptwork = new RealERPRPT.R_14_Pro.rptWorkOrderEdison();


                        //Sign In


                        TextObject rpttxtReqe = rptwork.ReportDefinition.ReportObjects["txtReq"] as TextObject;
                        rpttxtReqe.Text = _ReportDataSet.Tables[3].Rows[0]["reqnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqdat"].ToString();
                        TextObject rpttxtReqAppe = rptwork.ReportDefinition.ReportObjects["txtReqA"] as TextObject;
                        rpttxtReqAppe.Text = _ReportDataSet.Tables[3].Rows[0]["reqanam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqadat"].ToString();
                        TextObject rpttxtOrde = rptwork.ReportDefinition.ReportObjects["txtOrd"] as TextObject;
                        rpttxtOrde.Text = _ReportDataSet.Tables[3].Rows[0]["appnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["appdat"].ToString();
                        TextObject rpttxtWorde = rptwork.ReportDefinition.ReportObjects["txtWord"] as TextObject;
                        rpttxtWorde.Text = _ReportDataSet.Tables[3].Rows[0]["ordnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["orddat"].ToString();
                        TextObject rpttxtchecke = rptwork.ReportDefinition.ReportObjects["check"] as TextObject;
                        rpttxtchecke.Text = _ReportDataSet.Tables[3].Rows[0]["checknam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["checkdat"].ToString();
                        TextObject txtAdvancede = rptwork.ReportDefinition.ReportObjects["txtAdvanced"] as TextObject;
                        txtAdvancede.Text = Convert.ToDouble(dtord.Rows[0]["advamt"]).ToString("#,##0.00;(#,##0.00); "); ;
                        TextObject txtappbye = rptwork.ReportDefinition.ReportObjects["txtappby"] as TextObject;
                        txtappbye.Text = _ReportDataSet.Tables[3].Rows[0]["ordappnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["ordappdat"].ToString(); ;

                        TextObject txtPhoneNumber2e = rptwork.ReportDefinition.ReportObjects["txtPhoneNumber"] as TextObject;
                        txtPhoneNumber2e.Text = Phone;
                        break;
                    default:
                        rptwork = new RealERPRPT.R_14_Pro.rptWorkOrderBridge();


                        //Sign In


                        TextObject rpttxtReq = rptwork.ReportDefinition.ReportObjects["txtReq"] as TextObject;
                        rpttxtReq.Text = _ReportDataSet.Tables[3].Rows[0]["reqnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqdat"].ToString();
                        TextObject rpttxtReqApp = rptwork.ReportDefinition.ReportObjects["txtReqA"] as TextObject;
                        rpttxtReqApp.Text = _ReportDataSet.Tables[3].Rows[0]["reqanam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["reqadat"].ToString();
                        TextObject rpttxtOrd = rptwork.ReportDefinition.ReportObjects["txtOrd"] as TextObject;
                        rpttxtOrd.Text = _ReportDataSet.Tables[3].Rows[0]["appnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["appdat"].ToString();
                        TextObject rpttxtWord = rptwork.ReportDefinition.ReportObjects["txtWord"] as TextObject;
                        rpttxtWord.Text = _ReportDataSet.Tables[3].Rows[0]["ordnam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["orddat"].ToString();
                        TextObject rpttxtcheck = rptwork.ReportDefinition.ReportObjects["check"] as TextObject;
                        rpttxtcheck.Text = _ReportDataSet.Tables[3].Rows[0]["checknam"].ToString() + "\n" + _ReportDataSet.Tables[3].Rows[0]["checkdat"].ToString();
                        TextObject txtAdvanced = rptwork.ReportDefinition.ReportObjects["txtAdvanced"] as TextObject;
                        txtAdvanced.Text = Convert.ToDouble(dtord.Rows[0]["advamt"]).ToString("#,##0.00;(#,##0.00); "); ;
                        TextObject txtappby = rptwork.ReportDefinition.ReportObjects["txtappby"] as TextObject;
                        txtappby.Text = "Approved By";

                        TextObject txtPhoneNumber2 = rptwork.ReportDefinition.ReportObjects["txtPhoneNumber"] as TextObject;
                        txtPhoneNumber2.Text = Phone;

                        //txtappby.Text = (comcod == "3335") ? "Head of Procurement" : "Approved By";

                        // sign end 
                        break;


                }




                TextObject txtsubject = rptwork.ReportDefinition.ReportObjects["txtsubject"] as TextObject;
                txtsubject.Text = dtord.Rows[0]["subject"].ToString();
                TextObject txtCompany = rptwork.ReportDefinition.ReportObjects["txtCompany"] as TextObject;
                txtCompany.Text = comnam;
                TextObject txtAddress = rptwork.ReportDefinition.ReportObjects["txtAddress"] as TextObject;
                txtAddress.Text = comadd;
                TextObject rptpurno = rptwork.ReportDefinition.ReportObjects["purno"] as TextObject;
                rptpurno.Text = dtord.Rows[0]["orderno"].ToString().Substring(0, 3) + dtord.Rows[0]["orderno"].ToString().Substring(7, 2) + "-" + ASTUtility.Right(dtord.Rows[0]["orderno"].ToString(), 5);
                TextObject rptRefno = rptwork.ReportDefinition.ReportObjects["refno"] as TextObject;
                rptRefno.Text = dtord.Rows[0]["pordref"].ToString();
                TextObject supname = rptwork.ReportDefinition.ReportObjects["supname"] as TextObject;
                supname.Text = SupName;
                TextObject Supadd = rptwork.ReportDefinition.ReportObjects["saddress"] as TextObject;
                Supadd.Text = Address;

                //TextObject Fax = rptwork.ReportDefinition.ReportObjects["txtfax"] as TextObject;
                //Fax.Text =  fax;
                TextObject rptpurdate = rptwork.ReportDefinition.ReportObjects["txtOrderDate"] as TextObject;
                rptpurdate.Text = Orderdate;
                TextObject rptPara1 = rptwork.ReportDefinition.ReportObjects["TxtLETERDES"] as TextObject;
                rptPara1.Text = Para1;
                TextObject rptplace = rptwork.ReportDefinition.ReportObjects["place"] as TextObject;
                rptplace.Text = (place.Length > 0) ? trmplace + place : "";

                rptwork.ReportDefinition.Sections["GroupFooterSection5"].SectionFormat.EnableSuppress = (dtpaycch.Rows.Count > 0) ? false : true;


                TextObject rpttxtsupplydetails = rptwork.ReportDefinition.ReportObjects["txtsupplydetails"] as TextObject;
                rpttxtsupplydetails.Text = dtord.Rows[0]["pordnar"].ToString();
                rptwork.ReportDefinition.Sections["GroupFooterSection9"].SectionFormat.EnableSuppress = (place.Length > 0) ? false : true;


                TextObject rptpdate = rptwork.ReportDefinition.ReportObjects["pdate"] as TextObject;
                rptpdate.Text = (pdate.Length > 0) ? trmpdate + pdate : "";
                rptwork.ReportDefinition.Sections["GroupFooterSection10"].SectionFormat.EnableSuppress = (pdate.Length > 0) ? false : true;


                TextObject rptcarring = rptwork.ReportDefinition.ReportObjects["carring"] as TextObject;
                rptcarring.Text = (carring.Length > 0) ? trmcarring + carring : "";
                rptwork.ReportDefinition.Sections["GroupFooterSection11"].SectionFormat.EnableSuppress = (carring.Length > 0) ? false : true;


                TextObject rptpbill = rptwork.ReportDefinition.ReportObjects["bill"] as TextObject;
                rptpbill.Text = (bill.Length > 0) ? trmbill + bill : "";
                rptwork.ReportDefinition.Sections["GroupFooterSection12"].SectionFormat.EnableSuppress = (bill.Length > 0) ? false : true;

                TextObject rptpayment1 = rptwork.ReportDefinition.ReportObjects["payment1"] as TextObject;
                rptpayment1.Text = (payment.Length > 0) ? trmpayment + payment : "";
                rptwork.ReportDefinition.Sections["GroupFooterSection13"].SectionFormat.EnableSuppress = (payment.Length > 0) ? false : true;



                TextObject txtconcernperson = rptwork.ReportDefinition.ReportObjects["txtconcernperson"] as TextObject;
                txtconcernperson.Text = (cperson.Length > 0) ? cperson : "";
                rptwork.ReportDefinition.Sections["GroupFooterSection3"].SectionFormat.EnableSuppress = (cperson.Length > 0) ? false : true;



                TextObject rptOthrs = rptwork.ReportDefinition.ReportObjects["others"] as TextObject;
                rptOthrs.Text = (Others.Length > 0) ? trmothers + Others : "";
                rptwork.ReportDefinition.Sections["GroupFooterSection14"].SectionFormat.EnableSuppress = (Others.Length > 0) ? false : true;










                ///





                DataTable dtorder = (DataTable)ViewState["tblOrder"];
                DataTable dt1;
                DataTable dt2;
                DataTable dt3;


                // Carring
                DataView dv1 = dtorder.DefaultView;
                dv1.RowFilter = ("grp='A' and rsircode  like '019999901%'");
                dt1 = dv1.ToTable();

                //Deduction
                dv1 = dtorder.DefaultView;
                dv1.RowFilter = ("grp='A' and rsircode like'019999902%'");
                dt2 = dv1.ToTable();

                //Material
                dv1 = dtorder.DefaultView;
                dv1.RowFilter = ("grp='A' and rsircode not like '0199999%'");
                dt3 = dv1.ToTable();


                double amtcar = (dt1.Rows.Count == 0) ? 0.00 : Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(ordramt)", "")) ? 0.00 : dt1.Compute("Sum(ordramt)", "")));
                double amtdis = (dt2.Rows.Count == 0) ? 0.00 : Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(ordramt)", "")) ? 0.00 : dt2.Compute("Sum(ordramt)", "")));
                //



                double amtmat = Convert.ToDouble((Convert.IsDBNull(dt3.Compute("Sum(ordramt)", "")) ? 0.00 : dt3.Compute("Sum(ordramt)", "")));

                TextObject txtcarcost = rptwork.ReportDefinition.ReportObjects["txtcarcost"] as TextObject;
                txtcarcost.Text = amtcar.ToString("#,##0.00;(#,##0.00);");

                TextObject txtdiscount = rptwork.ReportDefinition.ReportObjects["txtdiscount"] as TextObject;
                txtdiscount.Text = amtdis.ToString("#,##0.00;(#,##0.00);");
                TextObject txtnettotal = rptwork.ReportDefinition.ReportObjects["txtnettotal"] as TextObject;
                txtnettotal.Text = (amtmat + amtcar - amtdis).ToString("#,##0.00;(#,##0.00);");



                TextObject txtkword = rptwork.ReportDefinition.ReportObjects["txtkword"] as TextObject;
                txtkword.Text = "In Word: " + ASTUtility.Trans(amtmat + amtcar - amtdis, 2);
                TextObject txtuserinfo = rptwork.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

                // Sub Report 
                //ReportDocument  rptsub= new RealERPRPT.R_14_Pro.RptOrderPaymentSch();
                //rptsub.SetDataSource((DataTable)ViewState["tblpaysch"]);

                rptwork.SetDataSource(dt);
                rptwork.Subreports["RptOrderPaymentSch.rpt"].SetDataSource((DataTable)ViewState["tblpaysch"]);





                string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                rptwork.SetParameterValue("ComLogo", ComLogo);
                Session["Report1"] = rptwork;
                string apppath = Server.MapPath("~") + "\\SupWorkOreder" + "\\" + orderno + ".pdf"; ;

                rptwork.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, apppath);



            }










            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }



        }

        private void SendSSLMail(string orderno)
        {


            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            string comcod = this.GetCompCode();
            string usrid = ((Hashtable)Session["tblLogin"])["usrid"].ToString();
            DataSet dssmtpandmail = this.accData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "SMTPPORTANDMAIL", usrid, "", "", "", "", "", "", "", "");


            string mORDERNO = orderno;

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPUREMAIL", mORDERNO, "", "", "", "", "", "", "", "");

            string subject = "Work Order";
            //SMTP
            string hostname = dssmtpandmail.Tables[0].Rows[0]["smtpid"].ToString();
            int portnumber = Convert.ToInt32(dssmtpandmail.Tables[0].Rows[0]["portno"].ToString());
            string frmemail = dssmtpandmail.Tables[1].Rows[0]["mailid"].ToString();
            string psssword = dssmtpandmail.Tables[1].Rows[0]["mailpass"].ToString();
            string mailtousr = ds1.Tables[0].Rows[0]["mailid"].ToString();
            string apppath = Server.MapPath("~") + "\\SupWorkOreder" + "\\" + mORDERNO + ".pdf";


            EASendMail.SmtpMail oMail = new EASendMail.SmtpMail("TryIt");

            //Connection Details 
            SmtpServer oServer = new SmtpServer(hostname);
            oServer.User = frmemail;
            oServer.Password = psssword;
            oServer.Port = portnumber;
            //oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

            oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;


            EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();
            oMail.From = frmemail;
            oMail.To = mailtousr;
            oMail.Cc = frmemail;
            oMail.Subject = subject;


            oMail.HtmlBody = "<html><head></head><body><pre style='max-width:700px;text-align:justify;'>" + "Dear Sir," + "<br/>" + "please find attached file" + "</pre></body></html>";
            oMail.AddAttachment(apppath);


            //System.Net.Mail.Attachment attachment;

            //attachment = new System.Net.Mail.Attachment(apppath);
            //oMail.AddAttachment(attachment);





            try
            {

                oSmtp.SendMail(oServer, oMail);
                ((Label)this.Master.FindControl("lblmsg")).Text = "Your message has been successfully sent.";

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error occured while sending your message." + ex.Message;
            }

        }


        private void SendNormalMail(string orderno)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            string comcod = this.GetCompCode();
            string usrid = ((Hashtable)Session["tblLogin"])["usrid"].ToString();
            DataSet dssmtpandmail = this.accData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "SMTPPORTANDMAIL", usrid, "", "", "", "", "", "", "", "");


            string mORDERNO = orderno;

            DataSet ds1 = this.accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPUREMAIL", mORDERNO, "", "", "", "", "", "", "", "");

            string subject = "Work Order";
            //SMTP
            string hostname = dssmtpandmail.Tables[0].Rows[0]["smtpid"].ToString();
            int portnumber = Convert.ToInt32(dssmtpandmail.Tables[0].Rows[0]["portno"].ToString());





            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(hostname, portnumber);
            //SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.EnableSsl = true;
            client.EnableSsl = false;
            string frmemail = dssmtpandmail.Tables[1].Rows[0]["mailid"].ToString();
            string psssword = dssmtpandmail.Tables[1].Rows[0]["mailpass"].ToString();
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(frmemail, psssword);
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;

            ///////////////////////

            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            msg.From = new System.Net.Mail.MailAddress(frmemail);



            msg.To.Add(new System.Net.Mail.MailAddress(ds1.Tables[0].Rows[0]["mailid"].ToString()));
            msg.Subject = subject;
            msg.IsBodyHtml = true;

            System.Net.Mail.Attachment attachment;

            string apppath = Server.MapPath("~") + "\\SupWorkOreder" + "\\" + mORDERNO + ".pdf"; ;

            attachment = new System.Net.Mail.Attachment(apppath);
            msg.Attachments.Add(attachment);



            msg.Body = string.Format("<html><head></head><body><pre style='max-width:700px;text-align:justify;'>" + "Dear Sir," + "<br/>" + "please find attached file" + "</pre></body></html>");
            try
            {
                client.Send(msg);

                ((Label)this.Master.FindControl("lblmsg")).Text = "Your message has been successfully sent.";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);


                //string savelocation = Server.MapPath("~") + "\\SupWorkOreder";
                //string[] filePaths = Directory.GetFiles(savelocation);
                //foreach (string filePath in filePaths)
                //    File.Delete(filePath);

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error occured while sending your message." + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }

        protected void btnSetup_Click(object sender, EventArgs e)
        {

        }


        protected void gvreqsecapproved_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink2 = (HyperLink)e.Row.FindControl("hlnkbtnEntrysecapproved");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();
                string comcod1 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();
                //string imesimeno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mimei")).ToString();

                //hlink1.NavigateUrl = "~/F_20_Service/Ser_Print?Type=ProReceived&comcod=" + comcod + "&centrid=" + centrid + "&recvno=" + recvno + "&imesimeno=" + imesimeno;

                hlink2.NavigateUrl = "~/F_12_Inv/PurReqEntry?InputType=ReqSecondApproved&prjcode=" + pactcode + "&genno=" + reqno + "&comcod=" + comcod1;

            }
        }
        protected void btnDelReqsecapproved_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            string url = "PurReqEntry?InputType=ReqFirstApproved";
            DataRow[] dr1 = ASTUtility.PagePermission1(url, (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["delete"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string genno = ((Label)this.gvreqsecapproved.Rows[RowIndex].FindControl("lblgvreqnorqsecapproved")).Text.Trim();


            //accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_INTERFACE", "RPTACCOUNTDASHBOARD", "%", Date, "%", "", "", "", "", "", "");



            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPURREQINFO", genno, "", "", "", "", "", "", "", "");
            //  DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPURBILLINFO", genno, "",

            if (ds1 == null)
                return;


            bool result = this.XmlDataInsertReq(genno, ds1);

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Fail');", true);
                return;

            }


            bool resulbill = accData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELETEREFIRSTAPPROVED", genno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");




            if (!resulbill)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted  Fail');", true);
                return;
            }
            else
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted Successfully.');", true);

            this.PurchaseInfoRpt();
            this.RadioButtonList1_SelectedIndexChanged(null, null);

        }
        protected void gvCRM_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrint");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();
                string reqdat = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "reqdat1")).ToString("dd-MMM-yyyy");
                //string comcod1 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();
                //string imesimeno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mimei")).ToString();

                //hlink1.NavigateUrl = "~/F_20_Service/Ser_Print?Type=ProReceived&comcod=" + comcod + "&centrid=" + centrid + "&recvno=" + recvno + "&imesimeno=" + imesimeno;

                hlink2.NavigateUrl = "~/F_12_Inv/PurReqEntry?InputType=ReqcRMCheck&prjcode=" + pactcode + "&genno=" + reqno;

                hlink1.NavigateUrl = "~/F_99_Allinterface/PurchasePrint?Type=ReqPrint&reqno=" + reqno + "&reqdat=" + reqdat;


            }
        }
        protected void btnCrmDelReq_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";

            string url = "PurReqEntry?InputType=Entry";


            DataRow[] dr1 = ASTUtility.PagePermission1(url, (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["delete"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }

            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string genno = ((Label)this.gvCRM.Rows[RowIndex].FindControl("lblgvreqnorq")).Text.Trim();


            //accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_INTERFACE", "RPTACCOUNTDASHBOARD", "%", Date, "%", "", "", "", "", "", "");



            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPURREQINFO", genno, "", "", "", "", "", "", "", "");
            //  DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPURBILLINFO", genno, "",

            if (ds1 == null)
                return;

            /**/
            bool result = this.XmlDataInsertReq(genno, ds1);

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Fail');", true);
                return;

            }


            bool resulbill = accData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELETEREQINFO", genno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");




            if (!resulbill)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted  Fail');", true);
                return;
            }
            else
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted Successfully.');", true);

            this.PurchaseInfoRpt();
            this.RadioButtonList1_SelectedIndexChanged(null, null);

        }

        protected void btnDirecdelReq_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";

            string url = "PurReqEntry?InputType=Entry";


            DataRow[] dr1 = ASTUtility.PagePermission1(url, (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["delete"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }

            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string genno = ((Label)this.gvReqChk.Rows[RowIndex].FindControl("lblgvreqnorq")).Text.Trim();


            //accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_INTERFACE", "RPTACCOUNTDASHBOARD", "%", Date, "%", "", "", "", "", "", "");



            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPURREQINFO", genno, "", "", "", "", "", "", "", "");
            //  DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPURBILLINFO", genno, "",

            if (ds1 == null)
                return;


            bool result = this.XmlDataInsertReq(genno, ds1);
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Fail');", true);
                return;
            }
            bool resulbill = accData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELETEREQINFO", genno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (!resulbill)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted  Fail');", true);
                return;
            }
            else
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted Successfully.');", true);



            this.PurchaseInfoRpt();
            this.RadioButtonList1_SelectedIndexChanged(null, null);

        }

        protected void btnDelOrderAprv_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";

            string url = "PurWrkOrderEntry?InputType=FirstApp";

            DataRow[] dr1 = ASTUtility.PagePermission1(url, (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["delete"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }

            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

            string orderno = ((Label)this.gvordfapp.Rows[RowIndex].FindControl("lblgvordernoofapp")).Text.Trim();
            string date = System.DateTime.Today.ToString("dd-MMM-yyyy");

            if (orderno == "")
                return;

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPURORDERINFO", orderno, date, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;


            bool result = this.XmlDataInsertOrder(orderno, ds1);

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Fail');", true);
                return;

            }


            bool resulbill = accData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELETEORDERAPPROVAL", orderno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (!resulbill)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted  Fail');", true);
                return;
            }
            else
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted Successfully.');", true);



            this.PurchaseInfoRpt();
            this.RadioButtonList1_SelectedIndexChanged(null, null);
        }


        protected void btnDelFapproval_Click(object sender, EventArgs e)
        {
            string url = "PurWrkOrderEntry?InputType=OrderEntry";
            string comcod = this.GetCompCode();
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataRow[] dr1 = ASTUtility.PagePermission1(url, (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["delete"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string genno = ((Label)this.grvMRec.Rows[RowIndex].FindControl("lblgvorderno")).Text.Trim();
            string toorder = Convert.ToDouble(((Label)this.grvMRec.Rows[RowIndex].FindControl("lblgvWoamt")).Text.Trim()).ToString();

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPURORDERINFO", genno, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            bool result = this.XmlDataInsertOrder(genno, ds1);
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Fail');", true);
                return;
            }

            bool resulbill = accData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELETEORDERNOAAPPROVED", genno, "delsapp", toorder, "", "", "", "", "", "", "", "", "", "", "", "");


            if (!resulbill)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted  Fail');", true);
                return;
            }
            else
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted Successfully.');", true);

            this.PurchaseInfoRpt();
            this.RadioButtonList1_SelectedIndexChanged(null, null);
        }

        protected void gvmrrapp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrintmapp");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntrymapp");
                LinkButton btnDelBill = (LinkButton)e.Row.FindControl("btnDelBillmapp");



                Hashtable hst = (Hashtable)Session["tblLogin"];
                string orderno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "orderno")).ToString();
                string mrrno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mrrno")).ToString();
                string sircode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ssircode")).ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string prjname = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactdesc")).ToString();
                string suppliername = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ssirdesc")).ToString();
                hlink1.NavigateUrl = "~/F_99_Allinterface/PurchasePrint?Type=MRReceipt&mrno=" + mrrno + "&sircode=" + sircode + "&supname=" + suppliername + "&prjname=" + prjname;



                if (orderno.Substring(0, 3) == "POR")
                    hlink2.NavigateUrl = "~/F_12_Inv/PurMRREntry?Type=FinalApp&prjcode=" + pactcode + "&genno=" + mrrno + "&sircode=" + sircode;



            }

        }

        protected void btnDelBillmapp_Click(object sender, EventArgs e)
        {


        }

        protected void lnkRdlcPrint_Recived_Click(object sender, EventArgs e)
        {
            string PrintOpt = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString();
            string comcod = this.GetCompCode();

            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            LinkButton lnkRdlcPrint = ((LinkButton)this.grvMRec.Rows[RowIndex].FindControl("lnkRdlcPrint_Recived"));
            string orderno = ((Label)this.grvMRec.Rows[RowIndex].FindControl("lblgvorderno")).Text.Trim();
            /*
            lnkRdlcPrint.PostBackUrl= "~/F_99_Allinterface/PurchasePrint?Type=OrderPrintNew&orderno=" + orderno + "&PrintOpt=" + PrintOpt;
            */
            string hostname = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/F_99_Allinterface/";
            string currentptah = "PurchasePrint?Type=OrderPrintNew&orderno=" + orderno + "&PrintOpt=" + PrintOpt;
            string totalpath = hostname + currentptah;
            ScriptManager.RegisterStartupScript(this, GetType(), "target", "FunPurchaseOrder('" + totalpath + "');", true);

            //hlink3.NavigateUrl = "~/F_99_Allinterface/PurchasePrint?Type=OrderPrintNew&orderno=" + orderno+ "&PrintOpt="+ PrintOpt;
        }

        protected void btnSaveReqNote_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            string genno = this.lblreqno.Text.ToString();
            string mrfno = this.lblmrfno.Text.ToString();
            string pactcode = this.lblpactcode.Text.ToString();
            string notes = this.txtReqNote.Text.Trim().ToString();

            if (notes.Length == 0 && (comcod=="3340"||comcod=="3101"))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Requisition Note  Required.. !!');", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openReqModal();", true);
                return;
            }

            //accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_INTERFACE", "RPTACCOUNTDASHBOARD", "%", Date, "%", "", "", "", "", "", "");

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPURREQINFO", genno, "", "", "", "", "", "", "", "");

            if (ds1 == null)
                return;

            if (ds1.Tables[5].Rows.Count > 0)
            {
                bool resulbill = accData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "UPDATEREQCRMBACKDATA", genno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");

                if (!resulbill)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Data Back Failed.. !!');", true);
                    return;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Removed Successfully');", true);
                    this.txtReqNote.Text = "";
                }
            }

            else
            {
                bool result = this.XmlDataInsertReq(genno, ds1);
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Update Failed .. !!');", true);
                    return;
                }
                bool resulbill = accData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "DELETEREQINFO", genno, pactcode, mrfno, notes, "", "", "", "", "", "", "", "", "", "", "");

                if (!resulbill)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Delete Failed .. !!');", true);
                    return;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Deleted Successfully');", true);
                    this.txtReqNote.Text = "";
                }

            }


            this.PurchaseInfoRpt();
            this.RadioButtonList1_SelectedIndexChanged(null, null);

        }
    }
}