using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Collections;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WinForms;
using System.Data;
using RealERPLIB;
using RealERPRPT;
using RealEntity;
using System.IO;
using System.Drawing;

namespace RealERPWEB.F_09_PImp
{
    public partial class PurLabRequisition02 : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString() == "Entry") ? "Sub-Contractor Bill Requisition"
                //    : (this.Request.QueryString["Type"].ToString() == "Edit") ? " Sub-Contractor Bill Requisition Edit"
                //    : (this.Request.QueryString["Type"].ToString() == "CSApproval") ? " Sub-Contractor Bill CS Approval"
                //    : (this.Request.QueryString["Type"].ToString() == "CSAppEdit") ? " Sub-Contractor Bill CS Approval Edit"
                //    : "Labour Issue Information";



                this.GetProjectList();

                this.GetTrade();
                this.DateForOpeningBill();
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;

                if (this.Request.QueryString["genno"].ToString().Length > 0)
                {
                    this.ibtnPreBillList_Click(null, null);
                    this.ddlPrevISSList.SelectedValue = this.Request.QueryString["genno"].ToString();
                    this.lbtnOk_Click(null, null);
                }
                if (Request.QueryString.AllKeys.Contains("msrno"))
                {
                    this.pannelHide();
                    this.getPreviousMSR();
                }

            }
        }



        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private void pannelHide()
        {
            //Panel3
            string msrno = this.Request.QueryString["genno"].ToString();
            string msrno1 = msrno.Substring(0, 3);
            if (msrno1 == "MSC")
            {
                this.Panel3.Visible = false;
            }


        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void GetLabSuEntNo()
        {

            string comcod = this.GetCompCode();
            string mREQNO = "NEWMISS";
            if (this.ddlPrevISSList.Items.Count > 0)
                mREQNO = this.ddlPrevISSList.SelectedValue.ToString();
            string mREQDAT = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString();

            if (mREQNO == "NEWMISS")
            {
                DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_BILLMGT02", "GET_LAST_LBILL_REQ_NO", mREQDAT,
                        "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    mREQNO = ds2.Tables[0].Rows[0]["maxmisuno"].ToString();
                    this.lblCurISSNo1.Text = ds2.Tables[0].Rows[0]["maxmisuno1"].ToString().Substring(0, 6);
                    this.txtCurISSNo2.Text = ds2.Tables[0].Rows[0]["maxmisuno1"].ToString().Substring(6, 5);
                    this.ddlPrevISSList.DataTextField = "maxmisuno1";
                    this.ddlPrevISSList.DataValueField = "maxmisuno";
                    this.ddlPrevISSList.DataSource = ds2.Tables[0];
                    this.ddlPrevISSList.DataBind();
                }
            }
        }




        private void GetProjectList()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            //string pactcode = "%" + this.txtSrcPro.Text + "%";
            this.txtCurISSDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

            string pactcode = (this.Request.QueryString["prjcode"].ToString()).Length == 0 ? ("%16" + this.txtSrcPro.Text.Trim() + "%") : (this.Request.QueryString["prjcode"].ToString() + "%");
            string userid = hst["usrid"].ToString();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETISSUEPRJLIST", pactcode, userid, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlprjlist.DataTextField = "actdesc1";
            this.ddlprjlist.DataValueField = "actcode";
            this.ddlprjlist.DataSource = ds1.Tables[0];
            this.ddlprjlist.DataBind();
            ds1.Dispose();
        }

        private void GetTrade()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETTRADENAME", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddltrade.DataTextField = "tradedesc";
            this.ddltrade.DataValueField = "tradecod";
            this.ddltrade.DataSource = ds1.Tables[0];
            this.ddltrade.DataBind();

            ds1.Dispose();


        }
        private void DateForOpeningBill()
        {
            string Type = this.Request.QueryString["Type"].ToString();

            if (Type == "Opening")
            {
                string comcod = this.GetCompCode();
                DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETOPENINGDATE", "", "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                this.txtCurISSDate.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["voudat"]).ToString("dd-MMM-yyyy");
                this.txtCurISSDate.Enabled = false;

            }





        }


        protected void lnkPrint_Click(object sender, EventArgs e)

        {
            string comcod = this.GetCompCode();

            string lisuno = this.lblCurISSNo1.Text.Trim().Substring(0, 3) + this.txtCurISSDate.Text.Trim().Substring(7, 4) + this.lblCurISSNo1.Text.Trim().Substring(3, 2) + this.txtCurISSNo2.Text.Trim();
            string pactcode = this.ddlprjlist.SelectedValue.ToString();

            this.printP2P_cs_approval();

            ////string url ="~/F_99_Allinterface/";
            //string url =  HttpContext.Current.Request.Url.Authority + "//F_99_Allinterface/";
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open(' " + url + "PurchasePrint.aspx?Type=SubConBillReq&lisuno=" + lisuno + "&pactcode=" + pactcode
            //           + "', target='_blank');</script>";


            //string hostname = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/F_99_Allinterface/";
            //string currentptah = "PurchasePrint.aspx?Type=SubConBillReq&lisuno=" + lisuno + "&pactcode=" + pactcode;
            //string totalpath = hostname + currentptah;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('" + totalpath + "', target='_blank');</script>";

            //lnkbtnPrint.NavigateUrl = "~/F_99_Allinterface/PurchasePrint.aspx?Type=SubConBillReq&lisuno=" + lisuno + "&pactcode=" + pactcode;

            /*
            string currentptah = this.ResolveUrl("~/F_99_Allinterface/PurchasePrint.aspx?Type=SubConBillReq&lisuno=" + lisuno + "&pactcode=" + pactcode);
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('" + currentptah + "', target='_blank');</script>";
            */

        }


        protected void printP2P_cs_approval()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string CurDate1 = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string comments = this.txtISSNarr.Text.Trim();
            string mMSRNo = "";
            if (Request.QueryString.AllKeys.Contains("msrno"))
            {
                mMSRNo = Request.QueryString["msrno"].ToString();
            }
            else
            {
                DataTable dt = (DataTable)Session["tblmsr01"];
                mMSRNo = dt.Rows[0]["maxmsrno"].ToString();
            }


            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            LocalReport Rpt1 = new LocalReport();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "RPTMARKETSURVEYP02PCSApproval", mMSRNo, CurDate1, "", "", "", "", "", "", "");

            string Projectname = "";
            string Projectlocat = "";
            string Username = "";
            string userdesig = "";

            string rsirdesc = "";
            string txtsign1 = "";
            string txtsign2 = "";
            string txtsign3 = "";

            if (ds1.Tables[3].Rows.Count > 0)
            {
                Projectname = ds1.Tables[3].Rows[0]["pactdesc"].ToString();
                Projectlocat = ds1.Tables[3].Rows[0]["projectadd"].ToString();
                Username = ds1.Tables[3].Rows[0]["usrname"].ToString();
                userdesig = ds1.Tables[3].Rows[0]["userdesig"].ToString();
                rsirdesc = ds1.Tables[3].Rows[0]["rsirdesc"].ToString();

                txtsign1 = ds1.Tables[3].Rows[0]["usrname"].ToString() + "\n" + ds1.Tables[3].Rows[0]["userdesig"].ToString() + "\n" + ds1.Tables[3].Rows[0]["reqdat"].ToString();
                txtsign2 = ds1.Tables[3].Rows[0]["csname"].ToString() + "\n" + ds1.Tables[3].Rows[0]["csdesig"].ToString() + "\n" + ds1.Tables[3].Rows[0]["csdat"].ToString();
                txtsign3 = ds1.Tables[3].Rows[0]["aprvname"].ToString() + "\n" + ds1.Tables[3].Rows[0]["aprdesig"].ToString() + "\n" + ds1.Tables[3].Rows[0]["appdat"].ToString();

            }

            var lst = ds1.Tables[0].DataTableToList<RealEntity.C_14_Pro.EClassPur.MkrServay02>();
            var lst1 = ds1.Tables[1].DataTableToList<RealEntity.C_14_Pro.EClassPur.MkrServay03>();

            string reqinfo = ds1.Tables[3].Rows[0]["reqno"].ToString();
            string csinfo = ds1.Tables[2].Rows[0]["msrno"].ToString() + ", " + Convert.ToDateTime(ds1.Tables[2].Rows[0]["msrdat"]).ToString("dd-MMM-yyyy");


            if (lst1.Count == 5)
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptPurMktSurveyP2P05", lst, lst1, null);
                Rpt1.EnableExternalImages = true;
                DataTable dt = (DataTable)Session["tblt01"];
                int i = 1;
                foreach (RealEntity.C_14_Pro.EClassPur.MkrServay03 lsts in lst1)
                {
                    Rpt1.SetParameters(new ReportParameter("f" + i.ToString() + "head", lsts.ssirdesc.ToString()));
                    Rpt1.SetParameters(new ReportParameter("mobile" + i.ToString() + "", lsts.contact.ToString()));
                    Rpt1.SetParameters(new ReportParameter("qdate" + i.ToString() + "", lsts.qutdate.ToString("dd-MMM-yyyy")));
                    Rpt1.SetParameters(new ReportParameter("worktime" + i.ToString() + "", lsts.worktime.ToString()));
                    Rpt1.SetParameters(new ReportParameter("note" + i.ToString() + "", lsts.notes.ToString()));
                    Rpt1.SetParameters(new ReportParameter("payment" + i.ToString() + "", lsts.payterm.ToString()));
                    Rpt1.SetParameters(new ReportParameter("tvs" + i.ToString() + "", ""));
                    Rpt1.SetParameters(new ReportParameter("security" + i.ToString() + "", ""));
                    Rpt1.SetParameters(new ReportParameter("payterm" + i.ToString() + "", lsts.crperiod.ToString()));// payterm = Effective Credit Period crperiod

                    i++;
                }

                Rpt1.SetParameters(new ReportParameter("txtsign1", txtsign1));
                Rpt1.SetParameters(new ReportParameter("txtsign2", txtsign2));
                Rpt1.SetParameters(new ReportParameter("txtsign3", txtsign3));
                Rpt1.SetParameters(new ReportParameter("comnam", comnam));
                Rpt1.SetParameters(new ReportParameter("Projectname", Projectname));
                Rpt1.SetParameters(new ReportParameter("Projectlocat", Projectlocat));
                Rpt1.SetParameters(new ReportParameter("Username", Username));
                Rpt1.SetParameters(new ReportParameter("userdesig", userdesig));
                Rpt1.SetParameters(new ReportParameter("CurDate1", CurDate1));
                Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
                Rpt1.SetParameters(new ReportParameter("rsirdesc", rsirdesc));
                Rpt1.SetParameters(new ReportParameter("reqinfo", reqinfo));
                Rpt1.SetParameters(new ReportParameter("csinfo", csinfo));
                Rpt1.SetParameters(new ReportParameter("RptTitle", "Comparative Statement"));
                Rpt1.SetParameters(new ReportParameter("comments", comments));
                Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            }


            else if (lst1.Count == 4)
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptPurMktSurveyP2P02", lst, lst1, null);
                Rpt1.EnableExternalImages = true;
                DataTable dt = (DataTable)Session["tblt01"];
                int i = 1;
                foreach (RealEntity.C_14_Pro.EClassPur.MkrServay03 lsts in lst1)
                {
                    Rpt1.SetParameters(new ReportParameter("f" + i.ToString() + "head", lsts.ssirdesc.ToString()));
                    Rpt1.SetParameters(new ReportParameter("mobile" + i.ToString() + "", lsts.contact.ToString()));
                    Rpt1.SetParameters(new ReportParameter("qdate" + i.ToString() + "", lsts.qutdate.ToString("dd-MMM-yyyy")));
                    Rpt1.SetParameters(new ReportParameter("worktime" + i.ToString() + "", lsts.worktime.ToString()));
                    Rpt1.SetParameters(new ReportParameter("note" + i.ToString() + "", lsts.notes.ToString()));
                    Rpt1.SetParameters(new ReportParameter("payment" + i.ToString() + "", lsts.payterm.ToString()));
                    Rpt1.SetParameters(new ReportParameter("tvs" + i.ToString() + "", ""));
                    Rpt1.SetParameters(new ReportParameter("security" + i.ToString() + "", ""));
                    Rpt1.SetParameters(new ReportParameter("payterm" + i.ToString() + "", lsts.crperiod.ToString()));// payterm = Effective Credit Period crperiod



                    i++;
                }

                Rpt1.SetParameters(new ReportParameter("txtsign1", txtsign1));
                Rpt1.SetParameters(new ReportParameter("txtsign2", txtsign2));
                Rpt1.SetParameters(new ReportParameter("txtsign3", txtsign3));
                Rpt1.SetParameters(new ReportParameter("comnam", comnam));
                Rpt1.SetParameters(new ReportParameter("Projectname", Projectname));
                Rpt1.SetParameters(new ReportParameter("Projectlocat", Projectlocat));
                Rpt1.SetParameters(new ReportParameter("Username", Username));
                Rpt1.SetParameters(new ReportParameter("userdesig", userdesig));
                Rpt1.SetParameters(new ReportParameter("CurDate1", CurDate1));
                Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
                Rpt1.SetParameters(new ReportParameter("rsirdesc", rsirdesc));

                Rpt1.SetParameters(new ReportParameter("reqinfo", reqinfo));
                Rpt1.SetParameters(new ReportParameter("csinfo", csinfo));
                Rpt1.SetParameters(new ReportParameter("RptTitle", "Comparative Statement"));
                Rpt1.SetParameters(new ReportParameter("comments", comments));
                Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            }
            else
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptPurMktSurveyP_2_P", lst, lst1, null);
                Rpt1.EnableExternalImages = true;
                DataTable dt = (DataTable)Session["tblt01"];
                int i = 1;
                foreach (RealEntity.C_14_Pro.EClassPur.MkrServay03 lsts in lst1)
                {
                    Rpt1.SetParameters(new ReportParameter("f" + i.ToString() + "head", lsts.ssirdesc.ToString()));
                    Rpt1.SetParameters(new ReportParameter("mobile" + i.ToString() + "", lsts.contact.ToString()));
                    Rpt1.SetParameters(new ReportParameter("qdate" + i.ToString() + "", lsts.qutdate.ToString("dd-MMM-yyyy")));
                    Rpt1.SetParameters(new ReportParameter("worktime" + i.ToString() + "", lsts.worktime.ToString()));
                    Rpt1.SetParameters(new ReportParameter("note" + i.ToString() + "", lsts.notes.ToString()));
                    Rpt1.SetParameters(new ReportParameter("payment" + i.ToString() + "", lsts.payterm.ToString()));
                    Rpt1.SetParameters(new ReportParameter("tvs" + i.ToString() + "", ""));
                    Rpt1.SetParameters(new ReportParameter("security" + i.ToString() + "", ""));
                    Rpt1.SetParameters(new ReportParameter("payterm" + i.ToString() + "", lsts.crperiod.ToString()));// payterm = Effective Credit Period crperiod

                    i++;

                }

                Rpt1.SetParameters(new ReportParameter("txtsign1", txtsign1));
                Rpt1.SetParameters(new ReportParameter("txtsign2", txtsign2));
                Rpt1.SetParameters(new ReportParameter("txtsign3", txtsign3));
                Rpt1.SetParameters(new ReportParameter("comnam", comnam));
                Rpt1.SetParameters(new ReportParameter("Projectname", Projectname));
                Rpt1.SetParameters(new ReportParameter("Projectlocat", Projectlocat));
                Rpt1.SetParameters(new ReportParameter("Username", Username));
                Rpt1.SetParameters(new ReportParameter("userdesig", userdesig));
                Rpt1.SetParameters(new ReportParameter("CurDate1", CurDate1));
                Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
                Rpt1.SetParameters(new ReportParameter("rsirdesc", rsirdesc));
                Rpt1.SetParameters(new ReportParameter("reqinfo", reqinfo));
                Rpt1.SetParameters(new ReportParameter("csinfo", csinfo));
                Rpt1.SetParameters(new ReportParameter("RptTitle", "Comparative Statement"));
                Rpt1.SetParameters(new ReportParameter("comments", comments));
                Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            }

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "','');</script>";


        }

        //protected string GetStdDate(string Date1)
        //{
        //    Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
        //    string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
        //    Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
        //    return Date1;
        //}


        protected void lbtnPrevISSList_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            string ProjectCode = this.ddlprjlist.SelectedValue.ToString();
            string CurDate1 = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString("dd-MMM-yyyy");
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPREVLISSUELIST", ProjectCode, CurDate1, "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlPrevISSList.Items.Clear();
            this.ddlPrevISSList.DataTextField = "lisuno1";
            this.ddlPrevISSList.DataValueField = "lisuno";
            this.ddlPrevISSList.DataSource = ds1.Tables[0];
            this.ddlPrevISSList.DataBind();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)         // OK Button
        {
            if (this.lbtnOk.Text == "New")
            {
                this.lbtnOk.Text = "Ok";
                this.ddlprjlist.Visible = true;
                this.lblddlProject.Visible = false;
                this.lblCurISSNo1.Text = "LIS00-";
                this.txtCurISSNo2.Text = "";
                this.txtISSNarr.Text = "";
                this.lblBillno.Text = "";

                this.lbtnPrevISSList.Visible = true;
                this.ddlPrevISSList.Visible = true;
                this.txtSrcPreBill.Visible = true;
                this.ibtnPreBillList.Visible = true;
                this.txtCurISSDate.Enabled = (this.Request.QueryString["Type"].ToString() == "Opening") ? false : true;
                this.ddlPrevISSList.Items.Clear();



                lstfloor.Items.Clear();
                this.PnlRes.Visible = false;
                this.PnlNarration.Visible = false;
                this.grvissue.DataSource = null;
                this.grvissue.DataBind();
                this.lblBillno.Text = "";
                this.lblvalvounum.Text = "";
               
                // this.txtRefno.Text = "";
                //this.lbljavascript.Text = "";

                return;
            }


            this.lblddlProject.Text = this.ddlprjlist.SelectedItem.Text.Trim() == null ? "" : this.ddlprjlist.SelectedItem.Text.Trim() == "" ? "" : this.ddlprjlist.SelectedItem.Text.Trim();

            this.ddlprjlist.Visible = false;
            this.lblddlProject.Visible = true;

            this.lbtnPrevISSList.Visible = false;
            this.ddlPrevISSList.Visible = false;
            this.txtSrcPreBill.Visible = false;
            this.ibtnPreBillList.Visible = false;

            this.PnlRes.Visible = true;
            this.PnlNarration.Visible = true;
            this.lbtnOk.Text = "New";
           
            this.GetCataGory();
            //this.GetFloorCode();
            this.Get_Issue_Info();
            //this.PenelVisiblity();


        }

        //private void PenelVisiblity ( )
        //{
        //    string comcod = this.GetCompCode ();
        //    switch (comcod)
        //    {
        //        case "3336":
        //        case "3337":
        //            this.pnlsecurity.Visible = true;
        //            break;
        //        default:
        //            break;
        //    }
        //}

        private void GetFloorCode()
        {

            string comcod = this.GetCompCode();
            string pactcode = this.ddlprjlist.SelectedValue.ToString().Trim();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETISSUEFLRLIST", pactcode, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.lstfloor.DataTextField = "flrdes";
            this.lstfloor.DataValueField = "flrcod";
            this.lstfloor.DataSource = ds1.Tables[0];
            this.lstfloor.DataBind();
            this.ddlfloorno_SelectedIndexChanged(null, null);

        }
        protected void ddlfloorno_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetCataGory();
            // this.GetMaterials();
        }

        protected void ibtnSearchMaterisl_Click(object sender, EventArgs e)
        {
            this.GetMaterials();
        }



        private void Get_Issue_Info()
        {

            string comcod = this.GetCompCode();
            string pactcode = this.ddlprjlist.SelectedValue.ToString();
            string CurDate1 = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string reqno = "NEWMISS";
            if (this.ddlPrevISSList.Items.Count > 0)
            {
                this.txtCurISSDate.Enabled = false;
                // this.ddlRA.Enabled = false;
                reqno = this.ddlPrevISSList.SelectedValue.ToString();
            }

            string recomsup = this.RecomSup();
            DataSet ds1 = new DataSet();

            ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_BILLMGT02", "GET_PURLAB_REQ_INFO", reqno, CurDate1, pactcode, recomsup, "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblbillreq"] = ds1.Tables[0];

            if (reqno == "NEWMISS")
            {

                ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_BILLMGT02", "GET_LAST_LBILL_REQ_NO", CurDate1,
                      "", "", "", "", "", "", "");

                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {

                    this.lblCurISSNo1.Text = ds1.Tables[0].Rows[0]["maxmisuno1"].ToString().Substring(0, 6);
                    this.txtCurISSNo2.Text = ds1.Tables[0].Rows[0]["maxmisuno1"].ToString().Substring(6, 5);
                }


                //string SearchMat = "%";
                //DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETLABFLRCODE", pactcode, CurDate1, "", SearchMat, "", "", "", "", "");
                //ViewState["tblbillreq"] = ds2.Tables[0];
                //if (ds2 == null)
                //    return;
                //ds2.Dispose();

                //this.grvissue_DataBind();
                return;
            }
            if (this.Request.QueryString["Type"] == "CSApproval")
            {
                DataTable dt = (DataTable)ViewState["tblbillreq"];
                foreach (DataRow dr1 in dt.Rows)
                {
                    dr1["csircode"] = this.Request.QueryString["recomsup"].ToString();
                }
                ViewState["tblbillreq"] = dt;
            }

            this.lblfloorno.Visible = true;
            this.lstfloor.Visible = true;
            // this.txtSearchLabour.Visible = true;
            //this.ibtnSearchMaterisl.Visible = true;
            lstfloor.Visible = true;
            this.lbtnSelect.Visible = true;
            this.txtRefno.Text = ds1.Tables[1].Rows[0]["refno"].ToString();
            this.lblCurISSNo1.Text = ds1.Tables[1].Rows[0]["lreqno1"].ToString().Substring(0, 6);
            this.txtCurISSNo2.Text = ds1.Tables[1].Rows[0]["lreqno1"].ToString().Substring(6, 5);
            this.txtCurISSDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["reqdat"]).ToString("dd-MMM-yyyy");
            this.ddlprjlist.SelectedValue = ds1.Tables[1].Rows[0]["pactcode"].ToString();
            this.lblddlProject.Text = this.ddlprjlist.SelectedItem.Text.Trim();
            this.txtISSNarr.Text = Convert.ToString(ds1.Tables[1].Rows[0]["rmrks"]);

            ViewState["tbl_contractor"] = ds1.Tables[2];
            this.lblBillno.Text = ds1.Tables[1].Rows[0]["billno"].ToString();
            this.lblvalvounum.Text = ds1.Tables[1].Rows[0]["vounum"].ToString();
            this.ddltrade.SelectedValue = ds1.Tables[1].Rows[0]["tradecod"].ToString();




            double amount = Convert.ToDouble((Convert.IsDBNull(((DataTable)ViewState["tblbillreq"]).Compute("sum(reqamt)", "")) ? 0.00 : ((DataTable)ViewState["tblbillreq"]).Compute("sum(reqamt)", "")));





            this.grvissue_DataBind();

            // ((LinkButton)this.grvissue.FooterRow.FindControl("lnkupdate")).Visible = (ds1.Tables[1].Rows[0]["billno"].ToString() == "00000000000000");

        }

        private string RecomSup()
        {
            string recom = "";
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "1205": //p2p
                case "3351": //p2p
                case "3352": //p2p
                case "8306": //p2p
                case "3370": //cpdl
                case "3101": //pintech

                    if (this.Request.QueryString["Type"] == "CSApproval" || this.Request.QueryString["Type"] == "CSAppEdit")
                    {
                        recom = this.Request.QueryString["recomsup"].ToString();
                    }
                    break;

                default:
                    recom = "";
                    break;
            }

            return recom;

        }


        private string ComCalltype()
        {

            string withmat = "";
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3335":
                case "3333":

                    // case "3101":
                    withmat = "WithMat";
                    break;

                //case "3101":
                //case"3336":
                //case"3337":
                //case"3344":
                //    withmat = "";
                //    break;


                default:
                    withmat = "";
                    break;




            }

            return withmat;


        }


        private string ComZeroBal()
        {

            string zerobal = "";
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3336":
                    zerobal = "Zero";
                    break;

                default:
                    zerobal = "";
                    break;

            }

            return zerobal;


        }


        private void GetCataGory()
        {
            string comcod = this.GetCompCode();
            string pactcode = this.ddlprjlist.SelectedValue.ToString();
            string flrcode ="";
            string date = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string SearchMat = "%" ;
            string withmat = this.ComCalltype();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETCATAGORY", pactcode, date, flrcode, SearchMat, withmat, "", "", "", "");

            if (ds1 == null)
                return;

            this.ddlcatagory.DataValueField = "rsircode";
            this.ddlcatagory.DataTextField = "rsirdesc";
            this.ddlcatagory.DataSource = ds1.Tables[0];
            this.ddlcatagory.DataBind();
            this.GetMaterials();



        }

        private void GetMaterials()
        {
            
            string comcod = this.GetCompCode();
            string pactcode = this.ddlprjlist.SelectedValue.ToString();
            string flrcode = "";
            string date = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString("dd-MMM-yyyy");          
            string SearchMat = (ddlcatagory.SelectedValue.ToString() == "") ? "%" : this.ddlcatagory.SelectedValue.Substring(0, 4) + "%";
           


            string withmat = this.ComCalltype();
            string zerobal = this.ComZeroBal();
            
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETLABFLRCODE", pactcode, date, flrcode, SearchMat, withmat, zerobal, "", "", "");
            ViewState["itemlist"] = ds1.Tables[0];
            if (ds1 == null)
                return;

            this.ddlWorkList.DataTextField = "rsirdesc1";
            this.ddlWorkList.DataValueField = "rsircode";
            this.ddlWorkList.DataSource = ds1.Tables[1];
            this.ddlWorkList.DataBind();
            ds1.Dispose();
            this.GetFloor();
           


        }
        private void GetFloor()
        {
            try
            {

                string Worklist = this.ddlWorkList.SelectedValue.ToString();

                DataTable dt = ((DataTable)ViewState["itemlist"]).Copy();
                DataView dv = dt.DefaultView;
                dv.RowFilter = ("rsircode= '" + Worklist + "'");
                dt = dv.ToTable(true, "flrcod", "flrdes", "flrdes1");
                this.lstfloor.DataTextField = "flrdes";
                this.lstfloor.DataValueField = "flrcod";
                this.lstfloor.DataSource = dt;
                this.lstfloor.DataBind();
            }
            catch (Exception ex)
            {



            }


        }

        protected void ddlWorkList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetFloor();

        }
     
        protected void grvissue_DataBind()
        {
            string comcod = this.GetCompCode();
            if (this.Request.QueryString["Type"] == "CSApproval" || this.Request.QueryString["Type"] == "CSAppEdit")
            {
                this.grvissue.Columns[14].Visible = true;
                this.grvissue.Columns[15].Visible = true;
            }

            this.grvissue.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.grvissue.DataSource = (DataTable)ViewState["tblbillreq"];

            this.grvissue.DataBind();
            this.FooterCalculaton();

        }

        private void FooterCalculaton()
        {

            DataTable dt = (DataTable)ViewState["tblbillreq"];
            if (dt.Rows.Count == 0)
                return;
            ((Label)this.grvissue.FooterRow.FindControl("lblgvFamount")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amount)", "")) ? 0.00 : dt.Compute("Sum(amount)", ""))).ToString("#,##0;(#,##0); ");

        }


        protected void lbtnSelect_Click(object sender, EventArgs e)         // Select Button
        {


            try
            {
                this.SaveValue();

                string rsircode = this.ddlWorkList.SelectedValue.ToString();
                
                string comcod = this.GetCompCode();
                foreach (ListItem flr in lstfloor.Items)
                {
                    if (flr.Selected)
                    {


                        string flrcode = flr.Value;
                        string flrdesc = flr.Text;

                        // string rsirdesc = lab1.Substring(13);

                        DataTable dt = (DataTable)ViewState["tblbillreq"];
                        DataRow[] dr = dt.Select(" flrcod='" + flrcode + "' and rsircode='" + rsircode + "'");
                        DataTable dt1 = (DataTable)ViewState["itemlist"];
                        double balqty = 0.00;

                        if (dr.Length == 0)
                        {

                            DataRow dr1 = dt.NewRow();
                            dr1["flrcod"] = flrcode;
                            dr1["flrdes"] = flrdesc;
                            dr1["rsircode"] = rsircode;
                            dr1["rsirdesc"] = ((DataTable)ViewState["itemlist"]).Select("rsircode='" + rsircode + "' and flrcod= '" + flrcode + "'")[0]["rsirdesc"];
                            //dr1["grp"] = grp;
                            //dr1["grpdesc"] = grpdesc;
                            dr1["rsirunit"] = ((DataTable)ViewState["itemlist"]).Select("rsircode='" + rsircode + "' and flrcod= '" + flrcode + "'")[0]["rsirunit"];
                            dr1["bgdqty"] = Convert.ToDouble(((DataTable)ViewState["itemlist"]).Select("rsircode='" + rsircode+ "' and flrcod= '" + flrcode + "'")[0]["bgdqty"]).ToString();

                            dr1["balqty"] = Convert.ToDouble(((DataTable)ViewState["itemlist"]).Select("rsircode='" + rsircode + "' and flrcod= '" + flrcode + "'")[0]["balqty"]).ToString();
                            balqty = Convert.ToDouble(((DataTable)ViewState["itemlist"]).Select("rsircode='" + rsircode + "' and flrcod= '" + flrcode + "'")[0]["balqty"].ToString());
                            dr1["balamt"] = Convert.ToDouble(((DataTable)ViewState["itemlist"]).Select("rsircode='" + rsircode + "' and flrcod= '" + flrcode + "'")[0]["balamt"]).ToString();
                            dr1["reqqty"] = (comcod == "3370" || comcod == "3101") ? balqty : 0.00;
                            dr1["bgdrat"] = Convert.ToDouble(((DataTable)ViewState["itemlist"]).Select("rsircode='" + rsircode + "' and flrcod= '" + flrcode + "'")[0]["bgdrat"]).ToString();
                            dr1["reqrat"] = Convert.ToDouble(((DataTable)ViewState["itemlist"]).Select("rsircode='" + rsircode + "' and flrcod= '" + flrcode + "'")[0]["isurat"]).ToString();
                            dr1["amount"] = 0.00;
                            dr1["reqamt"] = 0.00;
                            dr1["csircode"] = "000000000000";
                            dr1["approve"] = false;
                            dt.Rows.Add(dr1);

                        }
                        ViewState["tblbillreq"] = dt;
                        this.grvissue_DataBind();
                    }
                }
            }

            catch (Exception ed)
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = ed.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

            }


        }



        protected void lnkTotal_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            this.SaveValue();
            this.grvissue_DataBind();

        }
        protected void lnkCalculation_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblbillreq"];
            int TblRowIndex;
            for (int i = 0; i < this.grvissue.Rows.Count; i++)
            {
                double balqty = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.grvissue.Rows[i].FindControl("lblbalqty")).Text.Trim()));
                double dgvQty = Convert.ToDouble("0" + ((TextBox)this.grvissue.Rows[i].FindControl("txtisuqty")).Text.Trim());
                double labrate = Convert.ToDouble("0" + ((TextBox)this.grvissue.Rows[i].FindControl("txtlabrate")).Text.Trim());
                TblRowIndex = (grvissue.PageIndex) * grvissue.PageSize + i;

                dt.Rows[TblRowIndex]["reqqty"] = dgvQty;
                dt.Rows[TblRowIndex]["reqrat"] = labrate;
                dt.Rows[TblRowIndex]["reqamt"] = labrate * dgvQty;


            }
            ViewState["tblbillreq"] = dt;
            this.grvissue_DataBind();
        }






        protected void lnkupdate_Click(object sender, EventArgs e)      // Update Button
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            this.lnkTotal_Click(null, null);

            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);


            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string Sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            string date1 = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

            this.SaveValue();
            DataTable tbl2 = (DataTable)ViewState["tblbillreq"];
            string comcod = this.GetCompCode();
            if (ddlPrevISSList.Items.Count == 0)
            {
                this.GetLabSuEntNo();
            }
            string mISUNO = this.lblCurISSNo1.Text.Trim().Substring(0, 3) + this.txtCurISSDate.Text.Trim().Substring(7, 4) + this.lblCurISSNo1.Text.Trim().Substring(3, 2) + this.txtCurISSNo2.Text.Trim();
            string Refno = this.txtRefno.Text.Trim();



            string mISUDAT = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString();
            string mPACTCODE = this.ddlprjlist.SelectedValue.ToString().Trim();
            string mISURNAR = this.txtISSNarr.Text.Trim();

            string trade = this.ddltrade.SelectedValue.ToString();

            if (Refno.Length == 0)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Please Fill Ref No";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            if (Request.QueryString["Type"] == "Entry")
            {
                DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_BILLMGT02", "CHECK_DUPLICATE_BILL_REF", Refno, "", "", "", "", "", "", "", "");
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Found Duplicate Ref No";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
            }

            //string appxml = tbl2.Rows[0]["approval"].ToString();


            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_BILLMGT02", "UPDATE_PUR_LAB_REQUISITION_INFO", "PURLISSUEB",
                             mISUNO, mISUDAT, mPACTCODE, mISURNAR, Refno, usrid, Sessionid, trmid, trade);
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            foreach (DataRow dr in tbl2.Rows)
            {
                string Flrcod = dr["flrcod"].ToString();
                //  string grp = dr["grp"].ToString();
                string Rsircode = dr["rsircode"].ToString();
                double reqqty = Convert.ToDouble(dr["reqqty"].ToString().Trim());
                string reqamt = dr["reqamt"].ToString().Trim();
                double balqty = Convert.ToDouble(dr["balqty"].ToString().Trim());

                string csircode = dr["csircode"].ToString();
                string approve = dr["approve"].ToString();
                if (balqty >= reqqty)
                {

                    result = purData.UpdateTransInfo(comcod, "SP_ENTRY_BILLMGT02", "UPDATE_PUR_LAB_REQUISITION_INFO", "PURLISSUEA", mISUNO, Flrcod,
                        Rsircode, reqqty.ToString(), reqamt, csircode, approve);
                    if (!result)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }

                }
                else
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Not Within the Budget ";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
                // if (Isuqty > 0)
            }

            if (Request.QueryString["Type"] == "CSApproval" && result)
            {
                string msrno = Request.QueryString["msrno"].ToString() == "" ? "" : Request.QueryString["msrno"].ToString();
                string prjcode = Request.QueryString["prjcode"].ToString() == "" ? "" : Request.QueryString["prjcode"].ToString();

                result = purData.UpdateTransInfo(comcod, "SP_ENTRY_BILLMGT02", "UPDATEPURMSRCONAINFO", msrno, prjcode, usrid, Sessionid, trmid, date1, "", "");
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }

            }


            this.txtCurISSDate.Enabled = false;

            ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Labour Requistion Information";
                string eventdesc = "Update Labour Bill Requisition QTY & RATE";
                string eventdesc2 = "Req No: " + this.lblCurISSNo1.Text.Trim().Substring(0, 3) +
                        ASTUtility.Right((this.txtCurISSDate.Text.Trim()), 4) + this.lblCurISSNo1.Text.Trim().Substring(3, 2) + this.txtCurISSNo2.Text.Trim();

                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }



        private void SaveValue()
        {

            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            DataTable dt = (DataTable)ViewState["tblbillreq"];
            int TblRowIndex;
            // double labrate
            double adedamt = 0.00;
            for (int i = 0; i < this.grvissue.Rows.Count; i++)
            {
                string rsircode = ((Label)this.grvissue.Rows[i].FindControl("lblitemcode")).Text.Trim();
                double balqty = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.grvissue.Rows[i].FindControl("lblbalqty")).Text.Trim()));
                double dgvQty = ASTUtility.StrPosOrNagative(((TextBox)this.grvissue.Rows[i].FindControl("txtisuqty")).Text.Trim());
                double labrate = Convert.ToDouble("0" + ((TextBox)this.grvissue.Rows[i].FindControl("txtlabrate")).Text.Trim());
                // double balamt = Convert.ToDouble(ASTUtility.StrPosOrNagative(((Label)this.grvissue.Rows[i].FindControl("lblbalamt")).Text.Trim()));

                double amount = ASTUtility.StrPosOrNagative(((TextBox)this.grvissue.Rows[i].FindControl("txtgvamount")).Text.Trim());
                string csircode = ((DropDownList)this.grvissue.Rows[i].FindControl("DdlContractor")).SelectedValue.ToString();
                CheckBox approve = (CheckBox)this.grvissue.Rows[i].FindControl("chkapproved");


                TblRowIndex = (grvissue.PageIndex) * grvissue.PageSize + i;
                string rsirdesc = dt.Rows[TblRowIndex]["rsirdesc"].ToString();

                string comcod = this.GetCompCode();
                bool aprvstatus = false;
                switch (comcod)
                {
                    case "3101":
                    case "3370":
                        if (dgvQty > balqty)
                        {
                            string msg = rsirdesc + " Requisition Qty Can't Excess Balance Qty .. !! ";
                            ((Label)this.Master.FindControl("lblmsg")).Text = msg;
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                            return;
                        }
                        amount = dgvQty * labrate;
                        break;

                    case "1205": // p2p part
                    case "3351": // p2p part
                    case "3352": // p2p part
                        amount = amount > 0 ? amount : dgvQty * labrate;
                        labrate = amount > 0 ? amount / dgvQty : labrate;
                        break;

                    default:
                        amount = dgvQty * labrate;
                        break;
                }

                if (approve.Checked)
                {
                    aprvstatus = true;
                }
                else
                {
                    aprvstatus = false;
                }
                dt.Rows[TblRowIndex]["reqqty"] = dgvQty;
                dt.Rows[TblRowIndex]["reqrat"] = labrate;
                dt.Rows[TblRowIndex]["reqamt"] = amount;
                dt.Rows[TblRowIndex]["amount"] = amount;
                dt.Rows[TblRowIndex]["csircode"] = csircode;
                dt.Rows[TblRowIndex]["approve"] = aprvstatus;


            }
            ViewState["tblbillreq"] = dt;
        }


        protected void grvissue_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblbillreq"];
            string mISUNO = this.lblCurISSNo1.Text.Trim().Substring(0, 3) + ASTUtility.Right((this.txtCurISSDate.Text.Trim()), 4) + this.lblCurISSNo1.Text.Trim().Substring(3, 2) + this.txtCurISSNo2.Text.Trim();
            string Labcode = ((Label)this.grvissue.Rows[e.RowIndex].FindControl("lblitemcode")).Text.Trim();
            string Flrcode = ((Label)this.grvissue.Rows[e.RowIndex].FindControl("lblgvflrCode")).Text.Trim();
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_BILLMGT02", "DELETELAB_REQUISITION_ITEM", mISUNO, Flrcode, Labcode, "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                int rowindex = (this.grvissue.PageSize) * (this.grvissue.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            ViewState.Remove("tblbillreq");
            ViewState["tblbillreq"] = dv.ToTable();
            this.grvissue_DataBind();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Labour Requistion Information";
                string eventdesc = "Delete Requsition Item";
                string eventdesc2 = "Project Name: " + this.ddlprjlist.SelectedItem.Text.Substring(14) + "- " + "REQ No: " + this.lblCurISSNo1.Text.Trim().Substring(0, 3) +
                        ASTUtility.Right((this.txtCurISSDate.Text.Trim()), 4) + this.lblCurISSNo1.Text.Trim().Substring(3, 2) + this.txtCurISSNo2.Text.Trim() + "- " +
                        ((Label)this.grvissue.Rows[e.RowIndex].FindControl("lblitemcode")).Text.Trim();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)ViewState["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }


        }
        protected void grvissue_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.grvissue.PageIndex = e.NewPageIndex;
            this.grvissue_DataBind();
        }



        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.grvissue_DataBind();
        }



        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectList();
        }

        protected void ibtnPreBillList_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string ProjectCode = this.ddlprjlist.SelectedValue.ToString();
            string CurDate1 = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString("dd-MMM-yyyy");


            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_BILLMGT02", "GETPREVL_LABBILL_LIST", ProjectCode, CurDate1, "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlPrevISSList.Items.Clear();
            this.ddlPrevISSList.DataTextField = "lreqno1";
            this.ddlPrevISSList.DataValueField = "lreqno";
            this.ddlPrevISSList.DataSource = ds1.Tables[0];
            this.ddlPrevISSList.DataBind();

        }
        protected void lbtnDeleteBill_Click(object sender, EventArgs e)
        {

            ((Label)this.Master.FindControl("lblmsg")).Visible = true;

            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblbillreq"];
            string mISUNO = this.lblCurISSNo1.Text.Trim().Substring(0, 3) + this.txtCurISSDate.Text.Trim().Substring(7, 4) + this.lblCurISSNo1.Text.Trim().Substring(3, 2) + this.txtCurISSNo2.Text.Trim();

            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_BILLMGT02", "DELETE_ALL_ITEM_LBILL_REQUISITION", mISUNO, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Data Deleted Fail";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            else
                ((Label)this.Master.FindControl("lblmsg")).Text = "Data Deleted Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
        }


        protected void ddlcatagory_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetMaterials();
        }


        protected void grvissue_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList dlist = (DropDownList)e.Row.FindControl("DdlContractor");
                string contractor = ((Label)e.Row.FindControl("LblGvContractor")).Text.ToString();
                DataTable dt = (DataTable)ViewState["tbl_contractor"];
                dlist.DataTextField = "ssirdesc";
                dlist.DataValueField = "ssircode";
                dlist.DataSource = dt;
                dlist.DataBind();
                dlist.SelectedValue = contractor;
                //string recom = "";
                //if (Request.QueryString.AllKeys.Contains("mykey")){
                //    recom = this.Request.QueryString["recomsup"].ToString() ?? "";
                //}

                //dlist.SelectedValue = Request.QueryString.AllKeys.Contains("recomsup") ? this.Request.QueryString["recomsup"].ToString() : contractor;



                bool aprovestatus = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "approve"));
                CheckBox approve = (CheckBox)e.Row.FindControl("chkapproved");

                if (aprovestatus == true)
                {
                    approve.Checked = true;
                }
                else
                {
                    approve.Checked = false;
                }


                TextBox txtlabrate = ((TextBox)e.Row.FindControl("txtlabrate"));
                TextBox txtgvamount = ((TextBox)e.Row.FindControl("txtgvamount"));
                string comcod = this.GetCompCode();
                switch (comcod)
                {
                    case "1205": // p2p
                    case "3351": // p2p
                    case "3352": // p2p
                    case "8306": // p2p
                        txtlabrate.ReadOnly = true;
                        txtgvamount.ReadOnly = true;
                        break;

                    case "3101": // ptl
                    case "3370": // cpdl
                        txtlabrate.ReadOnly = false;
                        txtgvamount.ReadOnly = true;
                        break;

                    default:
                        txtlabrate.ReadOnly = false;
                        txtgvamount.ReadOnly = false;
                        break;
                }


            }
        }

        private void getPreviousMSR()
        {

            string comcod = this.GetCompCode();
            string CurDate1 = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string mMSRNo = this.Request.QueryString["msrno"].ToString() == "" ? "" : this.Request.QueryString["msrno"].ToString();
            string lreqno = this.Request.QueryString["genno"].ToString() == "" ? "" : this.Request.QueryString["genno"].ToString();
            string prjcode = this.Request.QueryString["prjcode"].ToString() == "" ? "" : this.Request.QueryString["prjcode"].ToString();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPURMSRINFO1CON", mMSRNo, CurDate1,
                          lreqno, prjcode, "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["tblt01"] = ds1.Tables[1];
            Session["tblt02"] = this.HiddenSameData(ds1.Tables[2]);
            Session["tblterm"] = ds1.Tables[3];
            Session["tblcsirdesc"] = ds1.Tables[5];


            this.gvMSRInfo_DataBind();
            this.Payterm_DataBind();


        }

        protected void gvMSRInfo2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtrate1 = (TextBox)e.Row.FindControl("txtrate1");
                TextBox txtrate2 = (TextBox)e.Row.FindControl("txtrate2");
                TextBox txtrate3 = (TextBox)e.Row.FindControl("txtrate3");
                TextBox txtrate4 = (TextBox)e.Row.FindControl("txtrate4");
                TextBox txtrate5 = (TextBox)e.Row.FindControl("txtrate5");


                Label txtamt1 = (Label)e.Row.FindControl("lblgvAmount1");
                Label txtamt2 = (Label)e.Row.FindControl("lblgvAmount2");
                Label txtamt3 = (Label)e.Row.FindControl("lblgvAmount3");
                Label txtamt4 = (Label)e.Row.FindControl("lblgvAmount4");
                Label txtamt5 = (Label)e.Row.FindControl("lblgvAmount5");

                TextBox txtbgdrat = (TextBox)e.Row.FindControl("txtgvMSRbgdrat");

                double bdgrate = Convert.ToDouble("0" + txtbgdrat.Text.Trim());
                double rate1 = Convert.ToDouble("0" + txtrate1.Text.Trim());
                double rate2 = Convert.ToDouble("0" + txtrate2.Text.Trim());
                double rate3 = Convert.ToDouble("0" + txtrate3.Text.Trim());
                double rate4 = Convert.ToDouble("0" + txtrate4.Text.Trim());
                double rate5 = Convert.ToDouble("0" + txtrate5.Text.Trim());


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rsircode")).ToString();
                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Left(code, 2) == "71")
                {
                    txtrate1.Style.Add("text-align", "Left");
                    txtrate2.Style.Add("text-align", "Left");
                    txtrate3.Style.Add("text-align", "Left");
                    txtrate4.Style.Add("text-align", "Left");
                    txtrate5.Style.Add("text-align", "Left");
                }
                if (rate1 > bdgrate)
                {
                    txtrate1.ForeColor = Color.Red;
                    txtamt1.ForeColor = Color.Red;
                }
                if (rate2 > bdgrate)
                {
                    txtrate2.ForeColor = Color.Red;
                    txtamt2.ForeColor = Color.Red;
                }
                if (rate3 > bdgrate)
                {
                    txtrate3.ForeColor = Color.Red;
                    txtamt3.ForeColor = Color.Red;
                }
                if (rate4 > bdgrate)
                {
                    txtrate4.ForeColor = Color.Red;
                    txtamt4.ForeColor = Color.Red;
                }
                if (rate5 > bdgrate)
                {
                    txtrate5.ForeColor = Color.Red;
                    txtamt5.ForeColor = Color.Red;
                }

            }
        }

        protected void gvMSRInfo2_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                TableCell cell0 = new TableCell();
                cell0.Text = "";
                cell0.HorizontalAlign = HorizontalAlign.Center;
                cell0.ColumnSpan = 6;
                gvrow.Cells.Add(cell0);
                DataTable dt = (DataTable)Session["tblt01"];
                //int j = 5;
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    TableCell cell = new TableCell();
                    cell.Text = dt.Rows[i]["ssirdesc1"].ToString();
                    cell.HorizontalAlign = HorizontalAlign.Center;
                    cell.ColumnSpan = 2;
                    cell.Font.Bold = true;
                    gvrow.Cells.Add(cell);

                }
                TableCell celll = new TableCell();
                celll.Text = "";
                celll.HorizontalAlign = HorizontalAlign.Center;
                celll.ColumnSpan = 2;
                gvrow.Cells.Add(celll);

                gvMSRInfo2.Controls[0].Controls.AddAt(0, gvrow);
            }

        }

        protected void gvMSRInfo_DataBind()
        {
            this.gvMSRInfo2.DataSource = (DataTable)Session["tblt02"];
            this.gvMSRInfo2.DataBind();
            this.FooterCalculation();
        }

        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblt02"];
            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvMSRInfo2.FooterRow.FindControl("lgvFamt1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt1)", "")) ? 0.00
          : dt.Compute("Sum(amt1)", ""))).ToString("#,##0.00;(#,##0.00);  ");
            ((Label)this.gvMSRInfo2.FooterRow.FindControl("lgvFamt2")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt2)", "")) ? 0.00
        : dt.Compute("Sum(amt2)", ""))).ToString("#,##0.00;(#,##0.00);  ");
            ((Label)this.gvMSRInfo2.FooterRow.FindControl("lgvFamt3")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt3)", "")) ? 0.00
        : dt.Compute("Sum(amt3)", ""))).ToString("#,##0.00;(#,##0.00);  ");
            ((Label)this.gvMSRInfo2.FooterRow.FindControl("lgvFamt4")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt4)", "")) ? 0.00
        : dt.Compute("Sum(amt4)", ""))).ToString("#,##0.00;(#,##0.00);  ");

            ((Label)this.gvMSRInfo2.FooterRow.FindControl("lgvFamt5")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt5)", "")) ? 0.00
        : dt.Compute("Sum(amt5)", ""))).ToString("#,##0.00;(#,##0.00);  ");



        }

        private void Payterm_DataBind()
        {
            this.gvterm.DataSource = (DataTable)Session["tblterm"];
            this.gvterm.DataBind();


        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string rsircode = dt1.Rows[0]["rsircode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["rsircode"].ToString() == rsircode)
                {

                    dt1.Rows[j]["rsirdesc1"] = "";
                }
                rsircode = dt1.Rows[j]["rsircode"].ToString();
            }

            DataView dv = dt1.DefaultView;
            dv.Sort = ("rsircode");
            dt1 = dv.ToTable();
            return dt1;
        }

        protected void DdlContractor_SelectedIndexChanged(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)ViewState["tblbillreq"]; /// gridview main 
            DataTable dt2 = (DataTable)Session["tblcsirdesc"]; // cs 

            int rowindex = ((GridViewRow)((DropDownList)sender).NamingContainer).RowIndex;
            string csircode = ((DropDownList)this.grvissue.Rows[rowindex].FindControl("DdlContractor")).SelectedValue.ToString();
            string rsircode = dt.Rows[rowindex]["rsircode"].ToString();
            if (csircode == "000000000000")
            {
                dt.Rows[rowindex]["reqrat"] = 0.00;
            }
            else
            {
                dt.Rows[rowindex]["reqrat"] = dt2.Select("rsircode='" + rsircode + "' and ssircode='" + csircode + "'")[0]["rate"];
            }

            dt.Rows[rowindex]["csircode"] = csircode;

            double qty = Convert.ToDouble(dt.Rows[rowindex]["reqqty"]);
            double rate = Convert.ToDouble(dt.Rows[rowindex]["reqrat"]);
            dt.Rows[rowindex]["reqamt"] = qty * rate;



            Session["tblbillreq"] = dt;
            this.grvissue_DataBind();

        }

        protected void chkAllapproved_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblbillreq"];
            int i, index;
            if (((CheckBox)this.grvissue.HeaderRow.FindControl("chkAllapproved")).Checked)
            {
                for (i = 0; i < this.grvissue.Rows.Count; i++)
                {
                    ((CheckBox)this.grvissue.Rows[i].FindControl("chkapproved")).Checked = true;
                    index = (this.grvissue.PageSize) * (this.grvissue.PageIndex) + i;
                    dt.Rows[index]["approve"] = "True";
                }
            }
            else
            {
                for (i = 0; i < this.grvissue.Rows.Count; i++)
                {
                    ((CheckBox)this.grvissue.Rows[i].FindControl("chkapproved")).Checked = false;
                    index = (this.grvissue.PageSize) * (this.grvissue.PageIndex) + i;
                    dt.Rows[index]["approve"] = "False";
                }
            }
            Session["tblbillreq"] = dt;
        }

      
    }
}