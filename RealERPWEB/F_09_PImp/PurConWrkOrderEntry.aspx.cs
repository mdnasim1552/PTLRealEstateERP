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
using RealERPLIB;
using RealERPRPT;
using Microsoft.Reporting.WinForms;
namespace RealERPWEB.F_09_PImp
{
    public partial class PurConWrkOrderEntry : System.Web.UI.Page
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

                //this.lnkPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //----------------udate-20150120---------
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                string type = this.Request.QueryString["Type"].ToString();
                //((Label)this.Master.FindControl("lblTitle")).Text = type == "Entry" ? "Contractor Work Order" : "Contractor Work Order Edit";
                this.GetContractorList();
                this.GetProjectList();

                string sbody = "Reference to our discussion and your quotation dated " + System.DateTime.Today.ToString("dd.MM.yyyy") + " , we are pleased to issue work order under the following specifications, terms and conditions and mode of payment.";



                this.txtLETDES.Text = sbody;
                string cudate = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtCurISSDate_CalendarExtender.EndDate = System.DateTime.Today;
                this.txtCurISSDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtcomncdat.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtcompltdat.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                string genno = this.Request.QueryString["genno"] ?? "";

                if (this.Request.QueryString["genno"] == "SubConOrder")
                {

                }
                else
                {
                    if (genno.Length > 0)
                    {
                        //  PnlRes.Visible = false;
                        string contractor = this.Request.QueryString["sircode"] ?? "";
                        if (contractor.Length > 0)
                        {
                            this.ddlContractorlist.SelectedValue = contractor;
                        }

                        string actcode = this.Request.QueryString["actcode"] ?? "";
                        if (actcode.Length > 0)
                        {
                            this.ddlprjlist.SelectedValue = actcode;
                        }
                        this.lbtnOk_Click(null, null);
                    }
                }
                if (Request.QueryString.AllKeys.Contains("orderno") && (Request.QueryString["Type"].ToString() =="Entry"))
                {
                    this.printWorkOrderFInt();
                }
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        protected void lbtnFindProject_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.GetProjectList();
            }

        }


        protected void lbtnFindContractor_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.GetContractorList();
            }
        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        protected void GetPreOrderNo()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string mNEWORDNo = "NEWORDER";
            if (this.ddlPrevList.Items.Count > 0)
                mNEWORDNo = this.ddlPrevList.SelectedValue.ToString();

            string mDAT = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString();
            if (mNEWORDNo == "NEWORDER")
            {
                DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETLASTCORDERNO", mDAT,
                       "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;
                if (ds2.Tables[0].Rows.Count > 0)
                {

                    this.lblCurISSNo1.Text = ds2.Tables[0].Rows[0]["maxno1"].ToString().Substring(0, 6);
                    this.txtCurISSNo2.Text = ds2.Tables[0].Rows[0]["maxno1"].ToString().Substring(6, 5);


                    this.ddlPrevList.DataTextField = "maxno1";
                    this.ddlPrevList.DataValueField = "maxno";
                    this.ddlPrevList.DataSource = ds2.Tables[0];
                    this.ddlPrevList.DataBind();
                }
            }





        }
        private void GetContractorList()
        {
            string comcod = this.GetCompCode();
            string conlist = "%" + this.txtsrchContractor.Text + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETISSUECONTLIST", conlist, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlContractorlist.DataTextField = "sircode1";
            this.ddlContractorlist.DataValueField = "sircode";
            this.ddlContractorlist.DataSource = ds1.Tables[0];
            this.ddlContractorlist.DataBind();

        }


        private void GetProjectList()
        {

            string comcod = this.GetCompCode();
            this.txtCurISSDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string srchproject = "%" + this.txtsrchproject.Text.Trim() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETISSUEPRJODRLIST", srchproject, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlprjlist.DataTextField = "actdesc1";
            this.ddlprjlist.DataValueField = "actcode";
            this.ddlprjlist.DataSource = ds1.Tables[0];
            this.ddlprjlist.DataBind();
            ds1.Dispose();


        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();

            switch (comcod)
            {
                case "3330":
                    //case "3101":
                    this.PrintGeneral();
                    break;

                //case "1205":
                //case "3351":

                case "3101": // pintech
                case "3368": // Finaly
                case "3370": // cpdl
                case "1205": // p2p
                case "3351": // p2p
                case "3352": // p2p
                    this.printWorkOrderFInt();
                    break;

                default:
                    this.PrintGeneral();
                    break;

            }
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

        private void PrintGeneral()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string CurDate = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string refNo = "";
            string Supp2 = this.ddlContractorlist.SelectedItem.Text.Trim().Substring(13).ToString();
            string mOrdernoO = this.lblCurISSNo1.Text.Trim().Substring(0, 3) + this.txtCurISSDate.Text.Trim().Substring(7, 4) + this.lblCurISSNo1.Text.Trim().Substring(3, 2) + this.txtCurISSNo2.Text.Trim();
            string ordercopy = this.GetCompOrderCopy();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "SHOWCONORKORDERINFO", mOrdernoO, ordercopy, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            LocalReport Rpt1 = new LocalReport();

            //  DataTable dt1 = (DataTable)ViewState["UserLog"];GetWorkOrder1


            var lst = ds1.Tables[0].DataTableToList<RealEntity.C_09_PIMP.EClassOrder.GetWorkOrder>();
            var lst1 = ds1.Tables[1].DataTableToList<RealEntity.C_09_PIMP.EClassOrder.GetWorkOrder1>();


            string Address = lst[0].conadd.ToString();
            string Attn = lst[0].atten.ToString();
            string body = lst1[0].leterdes.ToString();
            string subject = lst1[0].subject.ToString();
            string Term = lst1[0].term.ToString();
            string Suppl = lst1[0].csirdesc.ToString();
            string GDesc = lst[0].grpdesc;
            string prjname = lst1[0].pactdesc.ToString();


            if (comcod == "1205" || comcod == "3351" || comcod == "3352")
            {
                refNo = this.txtOrderRef.Text.ToString();
                string txtSign1 = ds1.Tables[2].Rows[0]["usrname"].ToString() + " ," + ds1.Tables[2].Rows[0]["usrdesig"].ToString() + " \n" + Convert.ToDateTime(ds1.Tables[2].Rows[0]["POSTEDDAT"]).ToString("dd-MMM-yyyy");
                string txtSign2 = "";
                string txtSign3 = "";
                string txtSign4 = "";
                string lang = ds1.Tables[1].Rows[0]["lang"].ToString();
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptWorkOrderP2PBN", lst, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("txtSign1", txtSign1));
                Rpt1.SetParameters(new ReportParameter("txtSign2", txtSign2));
                Rpt1.SetParameters(new ReportParameter("txtSign3", txtSign3));
                Rpt1.SetParameters(new ReportParameter("txtSign4", txtSign4));
                Rpt1.SetParameters(new ReportParameter("Suppl1", Suppl));
                Rpt1.SetParameters(new ReportParameter("Suppl2", Supp2));
                Rpt1.SetParameters(new ReportParameter("lang", lang));

            }
            else
            {
                refNo = this.txtOrderRef.Text.ToString();
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptWorkOrder", lst, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("Suppl", Suppl));

            }

            //Commented 
            // Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptWorkOrder", lst, null, null);

            //Comment End 

            //if (comcod == "3101" || comcod == "3333")
            //{
            //    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_90_PF.RptIndvPfAlli", pflist, null, null);
            //}
            //else
            //{
            //    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptIndvPf", pflist, null, null);
            //}


            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("CurDate", "Date: " + CurDate));
            Rpt1.SetParameters(new ReportParameter("refNo", "Ref:" + refNo));
            Rpt1.SetParameters(new ReportParameter("Address", Address));
            Rpt1.SetParameters(new ReportParameter("Attn", "Attn: " + Attn));
            Rpt1.SetParameters(new ReportParameter("body", body));
            Rpt1.SetParameters(new ReportParameter("subject", subject));
            Rpt1.SetParameters(new ReportParameter("Term", Term));
            Rpt1.SetParameters(new ReportParameter("GDesc", GDesc));
            Rpt1.SetParameters(new ReportParameter("prjname", prjname));

            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Work Order"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


        private void printWorkOrderFInt()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];

            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comfadd = hst["comadd"].ToString().Replace("<br />", "\n");
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string CurDate = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string refNo = "";
            string Supp2 = this.ddlContractorlist.SelectedItem.Text.Trim().Substring(13).ToString();
            string morderno = "";
            bool isself = false;
            if (this.Request.QueryString.AllKeys.Contains("orderno"))
            {
                morderno = this.Request.QueryString["orderno"].ToString() == "" ? "" : this.Request.QueryString["orderno"].ToString();
                isself = true;
            }
            else
            {
                morderno = this.lblCurISSNo1.Text.Trim().Substring(0, 3) + this.txtCurISSDate.Text.Trim().Substring(7, 4) + this.lblCurISSNo1.Text.Trim().Substring(3, 2) + this.txtCurISSNo2.Text.Trim();
                isself = false;
            }
            string ordercopy = this.GetCompOrderCopy();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "SHOWCONORKORDERINFO", morderno, ordercopy, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            LocalReport Rpt1 = new LocalReport();

            //  DataTable dt1 = (DataTable)ViewState["UserLog"];GetWorkOrder1


            var lst = ds1.Tables[0].DataTableToList<RealEntity.C_09_PIMP.EClassOrder.GetWorkOrder>();
            var lst1 = ds1.Tables[1].DataTableToList<RealEntity.C_09_PIMP.EClassOrder.GetWorkOrder1>();

            string Address = lst[0].conadd.ToString();
            string Attn = lst[0].atten.ToString();
            string body = lst1[0].leterdes.ToString();
            string subject = lst1[0].subject.ToString();
            string Term = lst1[0].term.ToString();
            string Suppl = lst1[0].csirdesc.ToString();
            string GDesc = lst[0].grpdesc;
            string prjname = lst1[0].pactdesc.ToString();
            string lang = ds1.Tables[1].Rows[0]["lang"].ToString();

            if (comcod == "1205" || comcod == "3351" || comcod == "3352")
            {
                refNo = Request.QueryString["genno"].ToString();
                string txtSign1 = ds1.Tables[2].Rows[0]["usrname"].ToString() + " ," + ds1.Tables[2].Rows[0]["usrdesig"].ToString() + " \n" + Convert.ToDateTime(ds1.Tables[2].Rows[0]["POSTEDDAT"]).ToString("dd-MMM-yyyy");
                string txtSign2 = "";
                string txtSign3 = "";
                string txtSign4 = "";

                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptWorkOrderP2PBN", lst, null, null);
                //Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptWorkOrder2", lst, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("txtSign1", txtSign1));
                Rpt1.SetParameters(new ReportParameter("txtSign2", txtSign2));
                Rpt1.SetParameters(new ReportParameter("txtSign3", txtSign3));
                Rpt1.SetParameters(new ReportParameter("txtSign4", txtSign4));
                Rpt1.SetParameters(new ReportParameter("lang", lang));
                Rpt1.SetParameters(new ReportParameter("Suppl1", Suppl));
                Rpt1.SetParameters(new ReportParameter("Suppl2", Supp2));

            }
            else if (comcod == "3370" || comcod == "3368" || comcod == "3101")
            {
                refNo = ds1.Tables[1].Rows[0]["pordref"].ToString();
                string orderno = ASTUtility.CustomReqFormat(ds1.Tables[1].Rows[0]["orderno"].ToString());
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptWorkOrderCPDL", lst, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("workSuppl", Suppl));
                Rpt1.SetParameters(new ReportParameter("fullComAdd", comfadd));
                Rpt1.SetParameters(new ReportParameter("refNo1", refNo));
                Rpt1.SetParameters(new ReportParameter("orderno", orderno));
                Rpt1.SetParameters(new ReportParameter("lang", lang));
            }
            else
            {
                refNo = this.txtOrderRef.Text.ToString();
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptWorkOrder", lst, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("Suppl", Suppl));
            }

            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("CurDate", "Date: " + CurDate));
            Rpt1.SetParameters(new ReportParameter("refNo", "Ref:" + refNo));
            Rpt1.SetParameters(new ReportParameter("Address", Address));
            Rpt1.SetParameters(new ReportParameter("Attn", "Attn: " + Attn));
            Rpt1.SetParameters(new ReportParameter("body", body));
            Rpt1.SetParameters(new ReportParameter("subject", subject));
            Rpt1.SetParameters(new ReportParameter("Term", Term));
            Rpt1.SetParameters(new ReportParameter("GDesc", GDesc));
            Rpt1.SetParameters(new ReportParameter("prjname", prjname));

            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Work Order"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            if (isself)
            {
                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
            }
            else
            {
                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }

        }




        protected void lbtnPrevList_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            string CurDate1 = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString("dd-MMM-yyyy");
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPREVCORDERLIST", CurDate1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlPrevList.Items.Clear();
            this.ddlPrevList.DataTextField = "orderno1";
            this.ddlPrevList.DataValueField = "orderno";
            this.ddlPrevList.DataSource = ds1.Tables[0];
            this.ddlPrevList.DataBind();


        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "New")
            {
                this.lbtnOk.Text = "Ok";
                this.lbtnPrevList.Visible = true;
                this.ddlPrevList.Visible = true;
                this.ddlPrevList.Items.Clear();

                this.ddlContractorlist.Visible = true;
                this.lblddlContractor.Visible = false;
                this.ddlprjlist.Visible = true;
                this.lblddlProject.Visible = false;
                this.txtCurISSDate.Enabled = true;
                this.lblCurISSNo1.Text = "ISU" + DateTime.Today.ToString("MM") + "-";
                this.txtCurISSNo2.Text = "";
                this.DropCheck1.Items.Clear();
                this.txtTerm.Text = "";

                this.PnlRes.Visible = false;
                this.PnlNarration.Visible = false;
                this.txtOrderRef.Text = "";
                ((Label)this.Master.FindControl("lblmsg")).Text = "";
                this.grvissue.DataSource = null;
                this.grvissue.DataBind();

                this.ChkLanguage.Visible = false;
                return;
            }
            this.lbtnPrevList.Visible = false;
            this.ddlPrevList.Visible = false;
            //this.txtsmcr.Visible = false;
            this.lblddlContractor.Text = this.ddlContractorlist.SelectedItem.Text.Trim();
            this.ddlContractorlist.Visible = false;//it will be used
            this.lblddlContractor.Visible = true;
            this.lblddlProject.Text = this.ddlprjlist.SelectedItem.Text.Trim();
            this.ddlprjlist.Visible = false;
            this.lblddlProject.Visible = true;
            this.PnlRes.Visible = true;
            string genno = this.Request.QueryString["genno"] ?? "";
            if (genno.Length > 0)
            {
                this.PnlRes.Visible = false;
            }

            this.PnlNarration.Visible = true;
            this.lbtnOk.Text = "New";
            this.GetFloorCode();
            this.Get_Work_Info();
            this.ibtnSearchMaterisl_Click(null, null);
            this.HideLanguage();

        }

        private void HideLanguage()
        {
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3101":
                case "3368":
                case "3370": // cpdl
                case "1205": // p2p 
                case "3351":  // p2p 
                case "3352":  // p2p 
                    this.ChkLanguage.Visible = true;
                    break;
                default:
                    this.ChkLanguage.Visible = false;
                    break;
            }
        }


        private void GetFloorCode()
        {

            string comcod = this.GetCompCode();
            string pactcode = this.ddlprjlist.SelectedValue.ToString().Trim();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETISSUEFLRLIST", pactcode, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlfloorno.DataTextField = "flrdes";
            this.ddlfloorno.DataValueField = "flrcod";
            this.ddlfloorno.DataSource = ds1.Tables[0];
            this.ddlfloorno.DataBind();
            this.ddlfloorno_SelectedIndexChanged(null, null);

        }

        private void Get_Work_Info()
        {

            string comcod = this.GetCompCode();
            string csircode = this.ddlContractorlist.SelectedValue.ToString();
            string CurDate1 = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString();
            string mNEWORDNo = "NEWORDER";
            DataSet ds1 = new DataSet();
            if (this.ddlPrevList.Items.Count > 0)
            {
                this.txtCurISSDate.Enabled = false;
                mNEWORDNo = this.ddlPrevList.SelectedValue.ToString();
            }
            string lreqno = this.Request.QueryString["genno"] ?? "";
            ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETCONORDERINFO", mNEWORDNo, CurDate1,
                         csircode, lreqno, "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tblorder"] = ds1.Tables[0];
            ViewState["UserLog"] = ds1.Tables[1];
            this.txtTerm.Text = ds1.Tables[2].Rows[0]["term"].ToString();

            this.grvissue_DataBind();
            if (mNEWORDNo == "NEWORDER")
            {
                ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETLASTCORDERNO", CurDate1,
                       "", "", "", "", "", "", "", "");

                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {


                    this.lblCurISSNo1.Text = ds1.Tables[0].Rows[0]["maxno1"].ToString().Substring(0, 6);
                    this.txtCurISSNo2.Text = ds1.Tables[0].Rows[0]["maxno1"].ToString().Substring(6, 5);

                }
                return;
            }



            this.lblCurISSNo1.Text = ds1.Tables[1].Rows[0]["orderno1"].ToString().Substring(0, 6);
            this.txtCurISSNo2.Text = ds1.Tables[1].Rows[0]["orderno1"].ToString().Substring(6, 5);
            this.txtCurISSDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["orderdat"]).ToString("dd-MMM-yyyy");
            this.ddlContractorlist.SelectedValue = ds1.Tables[1].Rows[0]["csircode"].ToString();
            this.lblddlContractor.Text = this.ddlContractorlist.SelectedItem.Text.Trim();

            this.ddlprjlist.SelectedValue = ds1.Tables[1].Rows[0]["pactcode"].ToString();
            this.lblddlProject.Text = this.ddlprjlist.SelectedItem.Text.Trim();
            this.txtTerm.Text = ds1.Tables[1].Rows[0]["pordnar"].ToString();
            this.txtOrderRef.Text = ds1.Tables[1].Rows[0]["pordref"].ToString();




        }

        private string CompReceived()
        {

            string comcod = this.GetCompCode();
            string CallType = "";
            switch (comcod)
            {
                case "3315":
                case "3316":
                case "3317":
                    CallType = "GETMETERIALSMRR";
                    break;

                default:
                    CallType = "GETMETERIALS";
                    break;
            }
            return CallType;

        }


        private string CompBalConMat()
        {

            string comcod = this.GetCompCode();
            string conbal = "";
            switch (comcod)
            {
                case "3301":
                case "1301":
                    //case "3101":
                    conbal = "notcon";
                    break;

                default:
                    conbal = "GETMETERIALS";
                    break;

            }

            return conbal;

        }



        private void GetMaterials()
        {


            string comcod = this.GetCompCode();
            string pactcode = this.ddlprjlist.SelectedValue.ToString();
            string flrcode = this.ddlfloorno.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string SearchMat = "%" + this.txtSearchLabour.Text.Trim() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETLABFLRCODE", pactcode, date, flrcode, SearchMat, "", "", "", "", "");
            ViewState["itemlist"] = ds1.Tables[0];
            if (ds1 == null)
                return;

            this.DropCheck1.DataTextField = "rsirdesc1";
            this.DropCheck1.DataValueField = "rsirdesc1";
            this.DropCheck1.DataSource = ds1.Tables[0];
            this.DropCheck1.DataBind();
            ds1.Dispose();

        }


        protected void grvissue_DataBind()
        {
            this.grvissue.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue);
            this.grvissue.DataSource = (DataTable)ViewState["tblorder"];
            this.grvissue.DataBind();
            this.FooterCalculation();
        }
        private void FooterCalculation()
        {
            DataTable dt = (DataTable)ViewState["tblorder"];
            if (dt.Rows.Count == 0)
                return;

            ((Label)this.grvissue.FooterRow.FindControl("lblgvFQty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ordqty)", "")) ? 0.00
                : dt.Compute("Sum(ordqty)", ""))).ToString("#,##0.00;(#,##0.00);  ");

            ((Label)this.grvissue.FooterRow.FindControl("lblgvFamount")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ordamt)", "")) ? 0.00
                : dt.Compute("Sum(ordamt)", ""))).ToString("#,##0.00;(#,##0.00);  ");

        }
        protected void lbtnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                this.SaveValue();
                string flrcode = this.ddlfloorno.SelectedValue.ToString().Trim();

                String[] lab = this.DropCheck1.Text.Trim().Split(',');
                foreach (string lab1 in lab)
                {
                    string rsircode = lab1.Substring(0, 12);
                    // string rsirdesc = lab1.Substring(13);

                    DataTable dt = (DataTable)ViewState["tblorder"];
                    DataRow[] dr = dt.Select(" flrcod='" + flrcode + "' and rsircode='" + rsircode + "'");

                    DataTable dt1 = (DataTable)ViewState["itemlist"];
                    if (dr.Length == 0)
                    {

                        DataRow dr1 = dt.NewRow();
                        dr1["flrcod"] = this.ddlfloorno.SelectedValue.ToString();
                        dr1["flrdes"] = this.ddlfloorno.SelectedItem.Text.Trim();
                        dr1["rsircode"] = rsircode;
                        dr1["rsirdesc"] = (((DataTable)ViewState["itemlist"]).Select("rsircode='" + rsircode + "'"))[0]["rsirdesc"];
                        dr1["sdetails"] = (((DataTable)ViewState["itemlist"]).Select("rsircode='" + rsircode + "'"))[0]["sdetails"];
                        dr1["rsirunit"] = (((DataTable)ViewState["itemlist"]).Select("rsircode='" + rsircode + "'"))[0]["rsirunit"];
                        dr1["spec"] = "";
                        dr1["ordrrate"] = Convert.ToDouble((((DataTable)ViewState["itemlist"]).Select("rsircode='" + rsircode + "'"))[0]["isurat"]).ToString();
                        dr1["ordqty"] = 0.00;
                        dr1["ordamt"] = 0.00;
                        dr1["rmrks"] = "";

                        dt.Rows.Add(dr1);

                    }
                    ViewState["tblorder"] = dt;
                    this.grvissue_DataBind();
                }
            }

            catch (Exception ed)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = ed.Message;

            }




            // this.SaveValue();
            // string rsircode = this.ddlMaterials.SelectedValue.ToString().Trim();

            // DataTable dt = (DataTable)ViewState["tblmatissue"];
            // DataRow[] dr = dt.Select("rsircode='" + rsircode + "'");

            //DataTable dt1= (DataTable)Session["itemlist"];
            // if(dr.Length==0)
            // {

            // DataRow dr1 = dt.NewRow();
            // dr1["rsircode"] = this.ddlMaterials.SelectedValue.ToString();
            // dr1["rsirdesc"] = this.ddlMaterials.SelectedItem.Text.Trim();
            // dr1["rsirunit"] = (((DataTable)Session["itemlist"]).Select("rsircode='" + rsircode + "'"))[0]["rsirunit"];
            // dr1["balqty"] = ((((DataTable)Session["itemlist"]).Select("rsircode='" + rsircode + "'")).Length == 0) ? "0.00" : Convert.ToDouble((((DataTable)Session["itemlist"]).Select("rsircode='" + rsircode + "'"))[0]["bbgdqty"]).ToString();
            // dr1["isuqty"] = 0.00;

            // dr1["useoflocation"] = "";
            // dr1["remarks"] = "";   
            // dt.Rows.Add(dr1);  

            // }
            // ViewState["tblmatissue"] = dt;
            // this.grvissue_DataBind();


        }

        protected void lnkupdate_Click(object sender, EventArgs e)
        {
            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                Response.Redirect("~/AcceessError.aspx");
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            DataTable dtuser = (DataTable)ViewState["UserLog"];
            string tblPostedByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postedbyid"].ToString();
            string tblPostedtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postrmid"].ToString();
            string tblPostedSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postseson"].ToString();
            string tblPosteddat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string PostedByid = (this.Request.QueryString["type"] == "Entry") ? userid : (tblPostedByid == "") ? userid : tblPostedByid;
            string Posttrmid = (this.Request.QueryString["type"] == "Entry") ? Terminal : (tblPostedtrmid == "") ? Terminal : tblPostedtrmid;
            string PostSession = (this.Request.QueryString["type"] == "Entry") ? Sessionid : (tblPostedSession == "") ? Sessionid : tblPostedSession;
            string Posteddat = (this.Request.QueryString["type"] == "Entry") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : (tblPosteddat == "01-Jan-1900") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : tblPosteddat;

            string EditByid = (this.Request.QueryString["type"] == "Entry") ? "" : userid;
            string Edittrmid = (this.Request.QueryString["type"] == "Entry") ? "" : Terminal;
            string EditSession = (this.Request.QueryString["type"] == "Entry") ? "01-Jan-1900" : System.DateTime.Today.ToString("dd-MMM-yyyy");
            string Editdat = (this.Request.QueryString["type"] == "Entry") ? "01-Jan-1900" : System.DateTime.Today.ToString("dd-MMM-yyyy");



            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            this.SaveValue();
            DataTable tbl2 = (DataTable)ViewState["tblorder"];

            DataRow[] dr = tbl2.Select("ordrrate=0.00");

            if (dr.Length > 0)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Please Fillup Qtuantity Field ";
                return;
            }

            string comcod = this.GetCompCode();
            if (ddlPrevList.Items.Count == 0)
            {
                this.GetPreOrderNo();
            }

            string mRef = this.txtOrderRef.Text;

            string order1= this.lblCurISSNo1.Text.Trim().Substring(0, 3) + this.txtCurISSDate.Text.Trim().Substring(7, 4) + this.lblCurISSNo1.Text.Trim().Substring(3, 2) + this.txtCurISSNo2.Text.Trim();
            string mOrdernoO = this.Request.QueryString["Type"].ToString() == "Edit" ? this.Request.QueryString["orderno"].ToString() : order1;
           
            string mDATE = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString();

            //////////

            string language = this.ChkLanguage.Checked ? "True" : "False";

            string csircode = this.ddlContractorlist.SelectedValue.ToString();
            string mPACTCODE = this.ddlprjlist.SelectedValue.ToString().Trim();
            string subject = this.txtSubject.Text.Trim();
            string letdes = this.txtLETDES.Text.Trim();
            string comncdat = txtcomncdat.Text.Trim();
            string compltdat = txtcompltdat.Text.Trim();
            string term = this.txtTerm.Text.Trim();
            string days = "";
            string biltype = "";
            string lreqno = this.Request.QueryString["genno"] ?? "";
            bool result = purData.UpdateTransHREMPInfo3(comcod, "SP_ENTRY_PURCHASE_03", "INSERTORUPDATECORDER", "PURCORDERB",
                             mOrdernoO, mDATE, csircode, mPACTCODE, mRef, subject, letdes, term, PostedByid, Posttrmid, PostSession, Posteddat, EditByid, Edittrmid, EditSession, Editdat, days, biltype, comncdat, compltdat, lreqno,
                             language, "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                return;
            }

            foreach (DataRow dru in tbl2.Rows)
            {
                string flrcod = dru["flrcod"].ToString();
                string Rsircode = dru["rsircode"].ToString();
                double rate = Convert.ToDouble(dru["ordrrate"].ToString());
                double qty = Convert.ToDouble(dru["ordqty"].ToString());
                double amt = Convert.ToDouble(dru["ordamt"].ToString());
                string txtremarks = dru["rmrks"].ToString();
                string sdetails = dru["sdetails"].ToString();
                string spec = dru["spec"].ToString();


                if (rate > 0)
                {

                    result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "INSERTORUPDATECORDER", "PURCORDERA", mOrdernoO,
                        flrcod, Rsircode, rate.ToString(), txtremarks, qty.ToString(), amt.ToString(), sdetails, spec, "", "", "");
                    if (!result)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                        return;
                    }
                }
            }
           ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully";
            //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Data Updated successfully');", true);

            this.txtCurISSDate.Enabled = false;
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Materials Issue Information";
                string eventdesc = "Update Issue QTY";
                string eventdesc2 = "Issue No: " + this.lblCurISSNo1.Text.Trim() + this.txtCurISSNo2.Text.Trim();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        private void SaveValue()
        {
            DataTable dt = (DataTable)ViewState["tblorder"];
            //bool isFault = false;
            int TblRowIndex;
            for (int i = 0; i < this.grvissue.Rows.Count; i++)
            {
                string txtisurmk = ((TextBox)this.grvissue.Rows[i].FindControl("txtisurmk")).Text.Trim();
                string sdetails = ((TextBox)this.grvissue.Rows[i].FindControl("txtwrkdesc")).Text.Trim();
                string wrkdesc = ((Label)this.grvissue.Rows[i].FindControl("lblwrkdesc")).Text.Trim();
                //string spec = ((TextBox)this.grvissue.Rows[i].FindControl ("txtspec")).Text.Trim ();
                TblRowIndex = (grvissue.PageIndex) * grvissue.PageSize + i;

                double gvrate = Convert.ToDouble("0" + ((TextBox)this.grvissue.Rows[i].FindControl("txtgvrate")).Text.Trim());
                double gvQty = Convert.ToDouble("0" + ((TextBox)this.grvissue.Rows[i].FindControl("txtgvQty")).Text.Trim());
                double orqty = Convert.ToDouble("0" + dt.Rows[TblRowIndex]["ordqty"].ToString());

                if (gvQty > orqty)
                {
                    string msg = wrkdesc + " Order Qty Can't Excess Requisition Qty .. !! ";
                    //ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('"+ msg + "');", true);
                    ((Label)this.Master.FindControl("lblmsg")).Text = msg;
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
                double amt = gvQty * gvrate;
                ((TextBox)this.grvissue.Rows[i].FindControl("txtgvAmount")).Text = amt.ToString("#,##0.000;(#,##0.000); ");
                ((TextBox)this.grvissue.Rows[i].FindControl("txtgvrate")).Text = gvrate.ToString("#,##0.000;(#,##0.000); ");
                ((TextBox)this.grvissue.Rows[i].FindControl("txtgvQty")).Text = gvQty.ToString("#,##0.000;(#,##0.000); ");

                dt.Rows[TblRowIndex]["ordqty"] = gvQty;
                dt.Rows[TblRowIndex]["ordrrate"] = gvrate;
                dt.Rows[TblRowIndex]["ordamt"] = amt;
                dt.Rows[TblRowIndex]["rmrks"] = txtisurmk;
                dt.Rows[TblRowIndex]["sdetails"] = sdetails;
            }

            ViewState["tblorder"] = dt;
        }


        protected void grvissue_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //string comcod = this.GetCompCode();
            //DataTable dt = (DataTable)ViewState["tblorder"];
            //string mISUNO = this.lblCurISSNo1.Text.Trim().Substring(0, 3) + ASTUtility.Right((this.txtCurISSDate.Text.Trim()), 4) + this.lblCurISSNo1.Text.Trim().Substring(3, 2) + this.txtCurISSNo2.Text.Trim();
            //string MatCode = ((Label)this.grvissue.Rows[e.RowIndex].FindControl("lblitemcode")).Text.Trim();
            //string spcfcode = ((Label)this.grvissue.Rows[e.RowIndex].FindControl("lblgvspcfcode")).Text.Trim();
            //bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "DELETEMATISUE", mISUNO, MatCode, spcfcode, "", "", "", "", "", "", "", "", "", "", "", "");

            //if (result == true)
            //{
            //    int rowindex = (this.grvissue.PageSize) * (this.grvissue.PageIndex) + e.RowIndex;
            //    dt.Rows[rowindex].Delete();
            //}

            //DataView dv = dt.DefaultView;
            //ViewState.Remove("tblorder");
            //ViewState["tblorder"] = dv.ToTable();
            //this.grvissue_DataBind();


        }
        protected void grvissue_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.grvissue.PageIndex = e.NewPageIndex;
            this.grvissue_DataBind();
        }
        protected void ibtnSearchMaterisl_Click(object sender, EventArgs e)
        {
            this.GetMaterials();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.grvissue_DataBind();
        }



        protected void ddlfloorno_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetMaterials();
        }

        protected void lnkTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.grvissue_DataBind();
        }
    }
}