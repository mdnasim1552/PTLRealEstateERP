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
    public partial class PurConWrkOrderEntry02 : System.Web.UI.Page
    {

        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //this.lnkPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //----------------udate-20150120---------
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Contractor Work Order(Standard)";
                this.InitialLoad();
                this.GetContractorList();
                this.GetProjectList();
                this.GetBillType();


                string sbody = "Reference to our duccussion and your quotation dated " + System.DateTime.Today.ToString("dd.MM.yyyy") + " , we are pleased to issue works order under the following specifications, terms and conditions and mode of payment.";



                this.txtLETDES.Text = sbody;

                this.txtCurISSDate_CalendarExtender.EndDate = System.DateTime.Today;
                this.txtCurISSDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtcomncdat.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtcompltdat.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");



            }




        }

        private void InitialLoad()
        {
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3336":
                    this.comncdate.Visible = false;
                    this.compltdate.Visible = false;
                    this.txtcomncdat.Visible = false;
                    this.txtcompltdat.Visible = false;
                    break;

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
            string conlist = "%%";
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
            string srchproject = "%%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETISSUEPRJODRLIST", srchproject, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlprjlist.DataTextField = "actdesc1";
            this.ddlprjlist.DataValueField = "actcode";
            this.ddlprjlist.DataSource = ds1.Tables[0];
            this.ddlprjlist.DataBind();
            ds1.Dispose();


        }

        private void GetBillType()
        {
            string comcod = this.GetCompCode();
            string serch1 = "%%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETBILLTYPE", serch1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlbilltype.DataTextField = "billtype";
            this.ddlbilltype.DataValueField = "billtcode";
            this.ddlbilltype.DataSource = ds1.Tables[0];
            this.ddlbilltype.DataBind();


        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();

            switch (comcod)
            {
                case "3330":
                    this.PrintGeneral();
                    break;

                case "3336":
                case "3337":
                //case "3101":
                case "1103":
                    this.PrintOrderSuvastu();

                    break;
                case "3338":
                    // case "3101":
                    this.PrintOrderAcme();
                    break;

                case "3351":
                case "3352":
                case "1205":
               
                    this.PrintOrderP2P();
                    break;

                case "1206":
                case "3101":
                    this.PrintOrderAcmeCon();
                    break;

                default:
                    this.PrintGeneral();
                    break;

            }
        }
        private void PrintOrderAcmeCon()
        {
            try
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
                string refNo = this.txtOrderRef.Text.ToString();
                string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

                string mOrdernoO = this.lblCurISSNo1.Text.Trim().Substring(0, 3) + this.txtCurISSDate.Text.Trim().Substring(7, 4) + this.lblCurISSNo1.Text.Trim().Substring(3, 2) + this.txtCurISSNo2.Text.Trim();
                string ordercopy = this.GetCompOrderCopy();
                //DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "SHOWCONORKORDERINFO", mOrdernoO, ordercopy, "", "", "", "", "", "", "");
                //ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETCONORDERINFO", orderno, "",
                //        "", "", "", "", "", "", "");
                string csircode = this.ddlContractorlist.SelectedValue.ToString();
                string CurDate1 = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString("dd-MMM-yyyy");
                string mNEWORDNo = "NEWORDER";
                //DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETCONORDERINFO", mNEWORDNo, CurDate1,
                //          csircode, "", "", "", "", "", "");
                //string orderno = this.ddlcopyorder.SelectedValue.ToString();


                DataSet ds1 = new DataSet();
                if (this.ddlPrevList.Items.Count > 0)
                {
                    this.txtCurISSDate.Enabled = false;
                    mNEWORDNo = this.ddlPrevList.SelectedValue.ToString();
                }
                ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETCONORDERINFO", mNEWORDNo, CurDate1,
                             csircode, "", "", "", "", "", "");
                if (ds1 == null)
                    return;

                string orderno= ds1.Tables[1].Rows[0]["orderno"].ToString();
                string refNo1 = ds1.Tables[1].Rows[0]["pordref"].ToString();
                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
                string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

                string subject = ds1.Tables[1].Rows[0]["subject"].ToString(); 
                string supaddress = ds1.Tables[1].Rows[0]["supaddress"].ToString(); 
                string prjaddress = ds1.Tables[1].Rows[0]["prjaddress"].ToString(); 
                string typeofcont = ds1.Tables[1].Rows[0]["typeofcont"].ToString(); 
                string contactno = ds1.Tables[1].Rows[0]["contactno"].ToString(); 
                string projname = ds1.Tables[1].Rows[0]["projname"].ToString();
                string nameofContrator = " ";
                string nameofCompany = " ";
                string comncdat = Convert.ToDateTime(ds1.Tables[1].Rows[0]["comncdat"].ToString()).ToString("dd-MMM-yyyy"); 
                string compltdat = Convert.ToDateTime(ds1.Tables[1].Rows[0]["compltdat"].ToString()).ToString("dd-MMM-yyyy");
                string term = this.txtTerm.Text.ToString();
                string proposalform = ds1.Tables[3].Rows[0]["froms"].ToString();
                string companyname = " ";
                string companyAddress = " ";
                string signature = " ";
                LocalReport Rpt1 = new LocalReport();     


                var lst = ds1.Tables[0].DataTableToList<RealEntity.C_09_PIMP.EClassOrder.Workorder03>();
               

                bool check = this.checkletter.Checked;
                if (check)
                {
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptProposalFromAcmeConst", lst, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("RptTitle", "Proposal Form"));
                    Rpt1.SetParameters(new ReportParameter("proposalform", proposalform));
                    Rpt1.SetParameters(new ReportParameter("companyname", companyname));
                    Rpt1.SetParameters(new ReportParameter("companyAddress", companyAddress));
                    Rpt1.SetParameters(new ReportParameter("signature", signature));
                }
                else
                {
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptWorkOrderAcmeConst", lst, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("term", term));
                    Rpt1.SetParameters(new ReportParameter("RptTitle", "Work Order"));
                    Rpt1.SetParameters(new ReportParameter("typeofcont", typeofcont));
                    Rpt1.SetParameters(new ReportParameter("contactno", contactno));
                    Rpt1.SetParameters(new ReportParameter("comncdat", comncdat));
                    Rpt1.SetParameters(new ReportParameter("compltdat", compltdat));
                    Rpt1.SetParameters(new ReportParameter("supaddress", supaddress));
                    Rpt1.SetParameters(new ReportParameter("nameofContrator", nameofContrator));
                    Rpt1.SetParameters(new ReportParameter("nameofCompany", nameofCompany));
                }
              


                Rpt1.SetParameters(new ReportParameter("comnam", comnam));
                Rpt1.SetParameters(new ReportParameter("orderno", orderno));
                Rpt1.SetParameters(new ReportParameter("comadd", comadd));
                Rpt1.SetParameters(new ReportParameter("CurDate",CurDate1));
                Rpt1.SetParameters(new ReportParameter("refNo", refNo1));
                Rpt1.SetParameters(new ReportParameter("projname", projname));
                Rpt1.SetParameters(new ReportParameter("prjaddress", prjaddress));                                             
                Rpt1.SetParameters(new ReportParameter("subject", subject));                  
                Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));               
                Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));


                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            }
            catch(Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }
        private void PrintOrderP2P()
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
            string refNo = this.txtOrderRef.Text.ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            string mOrdernoO = this.lblCurISSNo1.Text.Trim().Substring(0, 3) + this.txtCurISSDate.Text.Trim().Substring(7, 4) + this.lblCurISSNo1.Text.Trim().Substring(3, 2) + this.txtCurISSNo2.Text.Trim();
            string ordercopy = this.GetCompOrderCopy();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "SHOWCONORKORDERINFO", mOrdernoO, ordercopy, "", "", "", "", "", "", "");


            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            LocalReport Rpt1 = new LocalReport();

            //  DataTable dt1 = (DataTable)ViewState["UserLog"];GetWorkOrder1
           

             var lst = ds1.Tables[0].DataTableToList<RealEntity.C_09_PIMP.EClassOrder.GetWorkOrder>();
            var lst1 = ds1.Tables[1].DataTableToList<RealEntity.C_09_PIMP.EClassOrder.GetWorkOrder1>();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptWorkOrderP2P", lst, null, null);

            string txtSign1 = ds1.Tables[2].Rows[0]["usrname"].ToString()+" ," + ds1.Tables[2].Rows[0]["usrdesig"].ToString() +" \n" + Convert.ToDateTime(ds1.Tables[2].Rows[0]["POSTEDDAT"]).ToString("dd-MMM-yyyy");
            string txtSign2 = "";
            string txtSign3 = "";
            string txtSign4 = "";

            string Address = lst[0].conadd.ToString();
            string Attn = lst[0].atten.ToString();
            string body = lst1[0].leterdes.ToString();
            string subject = lst1[0].subject.ToString();
            string Term = lst1[0].term.ToString();
            string Suppl = lst1[0].csirdesc.ToString();
            string GDesc = lst[0].grpdesc;

            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
           

            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("CurDate", "Date: " + CurDate));
            Rpt1.SetParameters(new ReportParameter("refNo", "Ref:" + refNo));
            Rpt1.SetParameters(new ReportParameter("Address", Address));
            Rpt1.SetParameters(new ReportParameter("Attn", "Attn: " + Attn));
            Rpt1.SetParameters(new ReportParameter("body", body));
            Rpt1.SetParameters(new ReportParameter("subject", "Subject: " + subject));
            Rpt1.SetParameters(new ReportParameter("Term", Term));
            Rpt1.SetParameters(new ReportParameter("Suppl", Suppl));
            Rpt1.SetParameters(new ReportParameter("GDesc", GDesc));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Work Order"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("txtSign1", txtSign1));
            Rpt1.SetParameters(new ReportParameter("txtSign2", txtSign2));
            Rpt1.SetParameters(new ReportParameter("txtSign3", txtSign3));
            Rpt1.SetParameters(new ReportParameter("txtSign4", txtSign4));
            //Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }



        private void PrintBridgeHolding()
        {


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
            string refNo = this.txtOrderRef.Text.ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            string mOrdernoO = this.lblCurISSNo1.Text.Trim().Substring(0, 3) + this.txtCurISSDate.Text.Trim().Substring(7, 4) + this.lblCurISSNo1.Text.Trim().Substring(3, 2) + this.txtCurISSNo2.Text.Trim();
            string ordercopy = this.GetCompOrderCopy();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "SHOWCONORKORDERINFO", mOrdernoO, ordercopy, "", "", "", "", "", "", "");


            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            LocalReport Rpt1 = new LocalReport();

            //  DataTable dt1 = (DataTable)ViewState["UserLog"];GetWorkOrder1


            var lst = ds1.Tables[0].DataTableToList<RealEntity.C_09_PIMP.EClassOrder.GetWorkOrder>();
            var lst1 = ds1.Tables[1].DataTableToList<RealEntity.C_09_PIMP.EClassOrder.GetWorkOrder1>();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptWorkOrder", lst, null, null);

            //if (comcod == "3101" || comcod == "3333")
            //{
            //    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_90_PF.RptIndvPfAlli", pflist, null, null);
            //}
            //else
            //{
            //    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptIndvPf", pflist, null, null);
            //}

            string Address = lst[0].conadd.ToString();
            string Attn = lst[0].atten.ToString();
            string body = lst1[0].leterdes.ToString();
            string subject = lst1[0].subject.ToString();
            string Term = lst1[0].term.ToString();
            string Suppl = lst1[0].csirdesc.ToString();
            string GDesc = lst[0].grpdesc;

            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("CurDate", "Date: " + CurDate));
            Rpt1.SetParameters(new ReportParameter("refNo", "Ref:" + refNo));
            Rpt1.SetParameters(new ReportParameter("Address", Address));
            Rpt1.SetParameters(new ReportParameter("Attn", "Attn: " + Attn));
            Rpt1.SetParameters(new ReportParameter("body", body));
            Rpt1.SetParameters(new ReportParameter("subject", "Subject: " + subject));
            Rpt1.SetParameters(new ReportParameter("Term", Term));
            Rpt1.SetParameters(new ReportParameter("Suppl", Suppl));
            Rpt1.SetParameters(new ReportParameter("GDesc", GDesc));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Work Worder"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            //Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintOrderAcme()
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
            string refNo = this.txtOrderRef.Text.ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string ProjectName = this.ddlprjlist.SelectedItem.Text.Substring(19).Trim().ToString();
            // string Supp2 = this.ddlContractorlist.SelectedItem.Text.Substring (13).Trim ().ToString ();

            string ContractorName = this.ddlContractorlist.SelectedItem.Text.Substring(13).Trim().ToString();
            // string CurDate1 = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString();


            string mOrdernoO = this.lblCurISSNo1.Text.Trim().Substring(0, 3) + this.txtCurISSDate.Text.Trim().Substring(7, 4) + this.lblCurISSNo1.Text.Trim().Substring(3, 2) + this.txtCurISSNo2.Text.Trim();
            string ordercopy = this.GetCompOrderCopy();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "SHOWCONORKORDERINFO", mOrdernoO, ordercopy, "", "", "", "", "", "", "");


            string mOrderno1 = this.lblCurISSNo1.Text.Trim().Substring(3, 2) + '-' + this.txtCurISSNo2.Text.Trim();

            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            LocalReport Rpt1 = new LocalReport();

            //  DataTable dt1 = (DataTable)ViewState["UserLog"];GetWorkOrder1


            var lst = ds1.Tables[0].DataTableToList<RealEntity.C_09_PIMP.EClassOrder.GetWorkOrder>();
            //DataTable dt = ds1.Tables[1];
            var lst1 = ds1.Tables[1].DataTableToList<RealEntity.C_09_PIMP.EClassOrder.GetWorkOrder1>();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptWorkOrderAcme", lst, null, null);

            string Address = lst[0].conadd.ToString();
            //string Attn = lst[0].atten.ToString ();
            //string body = lst1[0].leterdes.ToString ();
            string subject = lst1[0].subject.ToString();
            string Term = lst1[0].term.ToString();
            //string Suppl = lst1[0].csirdesc.ToString ();
            //string GDesc = lst[0].grpdesc;
            //string duration = lst1[0].duration.ToString ();
            string billtype = lst1[0].billtype.ToString();
            string comncdat = lst[0].comncdat.ToString();
            string compltdat = lst[0].compltdat.ToString();
            string email = lst[0].email.ToString();
            string MobileNo = lst[0].mobile.ToString();
            string Paddress = lst1[0].paddress.ToString();
            // double tAmt = lst.Select (p => p.ordamt).Sum ();


            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("CurDate", CurDate));
            Rpt1.SetParameters(new ReportParameter("refNo", refNo));
            Rpt1.SetParameters(new ReportParameter("Address", Address));


            Rpt1.SetParameters(new ReportParameter("subject", subject));
            Rpt1.SetParameters(new ReportParameter("Term", Term));
            Rpt1.SetParameters(new ReportParameter("billtype", billtype));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("Orderno", mOrderno1));
            Rpt1.SetParameters(new ReportParameter("ContractorName", ContractorName));
            Rpt1.SetParameters(new ReportParameter("ProjectName", ProjectName));
            Rpt1.SetParameters(new ReportParameter("MobileNo", MobileNo));
            Rpt1.SetParameters(new ReportParameter("Paddress", Paddress));

            Rpt1.SetParameters(new ReportParameter("comncdat", comncdat));
            Rpt1.SetParameters(new ReportParameter("compltdat", compltdat));
            Rpt1.SetParameters(new ReportParameter("email", email));

            // Rpt1.SetParameters (new ReportParameter ("txtTotalAmount", (tAmt == 0 ? "" : "Total Amount")));
            // Rpt1.SetParameters (new ReportParameter ("InWrd", (tAmt == 0 ? "" : "In Words Taka : " + ASTUtility.Trans (Math.Round (tAmt), 2))));



            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void PrintOrderSuvastu()

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
            string refNo = this.txtOrderRef.Text.ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string ProjectName = this.ddlprjlist.SelectedItem.Text.Substring(19).Trim().ToString();
            string Supp2 = this.ddlContractorlist.SelectedItem.Text.Substring(13).Trim().ToString();

            string ContractorName = this.ddlContractorlist.SelectedItem.Text.Substring(13).Trim().ToString();
            // string CurDate1 = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString();


            string mOrdernoO = this.lblCurISSNo1.Text.Trim().Substring(0, 3) + this.txtCurISSDate.Text.Trim().Substring(7, 4) + this.lblCurISSNo1.Text.Trim().Substring(3, 2) + this.txtCurISSNo2.Text.Trim();
            string ordercopy = this.GetCompOrderCopy();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "SHOWCONORKORDERINFO", mOrdernoO, ordercopy, "", "", "", "", "", "", "");


            string mOrderno1 = this.lblCurISSNo1.Text.Trim().Substring(3, 2) + '-' + this.txtCurISSNo2.Text.Trim();

            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            LocalReport Rpt1 = new LocalReport();

            //  DataTable dt1 = (DataTable)ViewState["UserLog"];GetWorkOrder1


            var lst = ds1.Tables[0].DataTableToList<RealEntity.C_09_PIMP.EClassOrder.GetWorkOrder>();
            //DataTable dt = ds1.Tables[1];
            var lst1 = ds1.Tables[1].DataTableToList<RealEntity.C_09_PIMP.EClassOrder.GetWorkOrder1>();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptWorkOrderSuvastu", lst, null, null);

            string Address = lst[0].conadd.ToString();
            string Attn = lst[0].atten.ToString();
            string body = lst1[0].leterdes.ToString();
            string subject = lst1[0].subject.ToString();
            string Term = lst1[0].term.ToString();
            string Suppl = lst1[0].csirdesc.ToString();
            string GDesc = lst[0].grpdesc;
            string duration = lst1[0].duration.ToString();
            string billtype = lst1[0].billtype.ToString();

            string MobileNo = lst[0].mobile.ToString();
            string Paddress = lst1[0].paddress.ToString();
            double tAmt = lst.Select(p => p.ordamt).Sum();


            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("CurDate", "Order Date: " + CurDate));
            Rpt1.SetParameters(new ReportParameter("refNo", refNo));
            Rpt1.SetParameters(new ReportParameter("Address", "Address: " + Address));
            Rpt1.SetParameters(new ReportParameter("Attn", "Attn: " + Attn));
            Rpt1.SetParameters(new ReportParameter("body", body));
            Rpt1.SetParameters(new ReportParameter("subject", "Subject: " + subject));
            Rpt1.SetParameters(new ReportParameter("Term", Term));
            Rpt1.SetParameters(new ReportParameter("Suppl", Suppl));
            Rpt1.SetParameters(new ReportParameter("GDesc", GDesc));
            Rpt1.SetParameters(new ReportParameter("billtype", " Nature Of Work: " + billtype));
            Rpt1.SetParameters(new ReportParameter("duration", "Duration: " + duration));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Work Worder"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("Orderno", mOrderno1));
            Rpt1.SetParameters(new ReportParameter("ContractorName", ContractorName));
            Rpt1.SetParameters(new ReportParameter("ProjectName", ProjectName));
            Rpt1.SetParameters(new ReportParameter("MobileNo", MobileNo));
            Rpt1.SetParameters(new ReportParameter("Paddress", Paddress));
            Rpt1.SetParameters(new ReportParameter("Supp2", Supp2));
            Rpt1.SetParameters(new ReportParameter("txtTotalAmount", (tAmt == 0 ? "" : "Total Amount")));
            Rpt1.SetParameters(new ReportParameter("InWrd", (tAmt == 0 ? "" : "In Words Taka : " + ASTUtility.Trans(Math.Round(tAmt), 2))));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

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
                this.txtTerm.Text = "";

                this.PnlRes.Visible = false;
                this.PnlNarration.Visible = false;
                this.txtOrderRef.Text = "";
                ((Label)this.Master.FindControl("lblmsg")).Text = "";
                this.gvorder.DataSource = null;
                this.gvorder.DataBind();
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
            this.PnlNarration.Visible = true;
            this.lbtnOk.Text = "New";

            this.Get_Work_Info();
            this.GetGroupaMat();

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
            ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETCONORDERINFO", mNEWORDNo, CurDate1,
                         csircode, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tblorder"] = ds1.Tables[0];
            ViewState["UserLog"] = ds1.Tables[1];
            this.txtTerm.Text = ds1.Tables[2].Rows[0]["term"].ToString();

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
            this.txtTerm.Text =(comcod=="3101")? ds1.Tables[2].Rows[0]["term"].ToString() : ds1.Tables[1].Rows[0]["pordnar"].ToString();
            this.txtOrderRef.Text = ds1.Tables[1].Rows[0]["pordref"].ToString();
            this.txtduration.Text = ds1.Tables[1].Rows[0]["duration"].ToString();
            this.ddlbilltype.SelectedValue = ds1.Tables[1].Rows[0]["billtype"].ToString();
            this.Data_Bind();
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

        private void GetGroupaMat()
        {
            string comcod = this.GetCompCode();
            string pactcode = this.ddlprjlist.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string SearchMat = "%%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETLABOURWORK", pactcode, date, SearchMat, "", "", "", "", "");
            ViewState["itemlist"] = ds1.Tables[0];
            if (ds1 == null)
                return;

            this.ddlgroup.DataTextField = "mlabdesc";
            this.ddlgroup.DataValueField = "mlabcode";
            this.ddlgroup.DataSource = ds1.Tables[1];
            this.ddlgroup.DataBind();
            ds1.Dispose();
            this.GetMaterials();


        }

        private void GetMaterials()
        {
            DataTable dt = ((DataTable)ViewState["itemlist"]).Copy();
            string mlabcode = this.ddlgroup.SelectedValue.ToString();

            DataView dv = dt.DefaultView;
            dv.RowFilter = ("mlabcode='" + mlabcode + "'");
            DataTable dt1 = dv.ToTable();
            this.ddllabour.DataTextField = "rsirdesc";
            this.ddllabour.DataValueField = "rsircode";
            this.ddllabour.DataSource = dt;
            this.ddllabour.DataBind();
            dt1.Dispose();






        }


        protected void Data_Bind()
        {
            this.gvorder.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue);
            this.gvorder.DataSource = (DataTable)ViewState["tblorder"]; 
            this.gvorder.DataBind();
            string comcod = this.GetCompCode();
            if (comcod == "3336" || comcod == "3337")
            {
                gvorder.Columns[5].Visible = false;
            }
            // this.FooterCalculation();
        }


        private void FooterCalculation()
        {

            DataTable dt = (DataTable)ViewState["tblorder"]; ;

            if (dt.Rows.Count == 0)
                return;


            ((Label)this.gvorder.FooterRow.FindControl("lblFgvordamt")).Text =
                Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ordamt)", "")) ?
                    0.00 : dt.Compute("Sum(ordamt)", ""))).ToString("#,##0;(#,##0); ");

        }

        protected void lbtnSelect_Click(object sender, EventArgs e)
        {


            try
            {
                this.SaveValue();
                string rsircode = this.ddllabour.SelectedValue.ToString();
                DataTable dt = (DataTable)ViewState["tblorder"];
                DataRow[] dr = dt.Select("rsircode='" + rsircode + "'");
                DataTable dt1 = (DataTable)ViewState["itemlist"];

                if (dr.Length == 0)
                {

                    DataRow dr1 = dt.NewRow();
                    dr1["flrcod"] = "000";
                    dr1["flrdes"] = "";
                    dr1["rsircode"] = rsircode;
                    dr1["rsirdesc"] = (((DataTable)ViewState["itemlist"]).Select("rsircode='" + rsircode + "'"))[0]["rsirdesc"];
                    dr1["sdetails"] = (((DataTable)ViewState["itemlist"]).Select("rsircode='" + rsircode + "'"))[0]["sdetails"];
                    dr1["rsirunit"] = (((DataTable)ViewState["itemlist"]).Select("rsircode='" + rsircode + "'"))[0]["rsirunit"];
                    dr1["bgdqty"] = (((DataTable)ViewState["itemlist"]).Select("rsircode='" + rsircode + "'"))[0]["bgdqty"];

                    double ordqty = 0.00;
                    double ordrate = Convert.ToDouble((((DataTable)ViewState["itemlist"]).Select("rsircode='" + rsircode + "'"))[0]["isurat"]);
                    dr1["ordqty"] = ordqty.ToString();
                    dr1["ordrrate"] = ordrate.ToString();
                    dr1["ordamt"] = (ordqty * ordrate).ToString();
                    dr1["rmrks"] = "";
                    dr1["serial"] = "0";
                    dt.Rows.Add(dr1);

                }
                ViewState["tblorder"] = dt;
                this.Data_Bind();
            }


            catch (Exception ed)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = ed.Message;

            }





        }


        protected void lbtnSelectAll_Click(object sender, EventArgs e)
        {

            try
            {
                this.SaveValue();
                string mlabcode = this.ddlgroup.SelectedValue.ToString().Trim();

                DataTable dts = (DataTable)ViewState["itemlist"];
                DataView dv = dts.DefaultView;
                dv.RowFilter = ("mlabcode='" + mlabcode + "'");
                DataTable dt1 = dv.ToTable();

                foreach (DataRow dr1 in dt1.Rows)
                {
                    string rsircode = dr1["rsircode"].ToString();
                    // string rsirdesc = lab1.Substring(13);

                    DataTable dt = (DataTable)ViewState["tblorder"];
                    DataRow[] dr = dt.Select("rsircode='" + rsircode + "'");


                    if (dr.Length == 0)
                    {

                        DataRow dr2 = dt.NewRow();
                        dr2["flrcod"] = "000";
                        dr2["flrdes"] = "";
                        dr2["rsircode"] = dr1["rsircode"].ToString(); ;
                        dr2["rsirdesc"] = dr1["rsirdesc"].ToString();
                        dr2["rsirunit"] = dr1["rsirunit"].ToString();
                        dr2["sdetails"] = dr1["sdetails"].ToString();


                        double ordqty = 0.00;
                        double ordrate = Convert.ToDouble(dr1["isurat"]);
                        dr2["ordqty"] = ordqty.ToString();
                        dr2["bgdqty"] = (((DataTable)ViewState["itemlist"]).Select("rsircode='" + rsircode + "'"))[0]["bgdqty"];


                        dr2["ordrrate"] = ordrate.ToString();
                        dr2["ordamt"] = (ordqty * ordrate).ToString();
                        dr2["rmrks"] = "";
                        dr2["serial"] = "0";

                        dt.Rows.Add(dr2);

                    }
                    ViewState["tblorder"] = dt;
                    this.Data_Bind();
                }
            }

            catch (Exception ed)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = ed.Message;

            }
        }
        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
            this.FooterCalculation();

        }
        protected void lnkupdate_Click(object sender, EventArgs e)
        {

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
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

            string mOrdernoO = this.lblCurISSNo1.Text.Trim().Substring(0, 3) + this.txtCurISSDate.Text.Trim().Substring(7, 4) + this.lblCurISSNo1.Text.Trim().Substring(3, 2) + this.txtCurISSNo2.Text.Trim();
            string mDATE = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString();







            //////////



            string csircode = this.ddlContractorlist.SelectedValue.ToString();
            string mPACTCODE = this.ddlprjlist.SelectedValue.ToString().Trim();
            string subject = this.txtSubject.Text.Trim();
            string letdes = this.txtLETDES.Text.Trim();
            string term = this.txtTerm.Text.Trim();
            string days = this.txtduration.Text.Trim();
            string comncdat = txtcomncdat.Text.Trim();
            string compltdat = txtcompltdat.Text.Trim();

            string biltype = this.ddlbilltype.SelectedValue.ToString();
            bool result = purData.UpdateTransInfo3(comcod, "SP_ENTRY_PURCHASE_03", "INSERTORUPDATECORDER", "PURCORDERB",
                             mOrdernoO, mDATE, csircode, mPACTCODE, mRef, subject, letdes, term, PostedByid, Posttrmid, PostSession, Posteddat, EditByid, Edittrmid, EditSession, Editdat, days, biltype, comncdat, compltdat, "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                return;
            }





            foreach (DataRow dru in tbl2.Rows)
            {
                string flrcod = dru["flrcod"].ToString();
                string Rsircode = dru["rsircode"].ToString();
                string qty = Convert.ToDouble(dru["ordqty"].ToString()).ToString();
                double rate = Convert.ToDouble(dru["ordrrate"].ToString());
                string amt = Convert.ToDouble(dru["ordamt"].ToString()).ToString();
                string txtremarks = dru["rmrks"].ToString();
                string sdetails = dru["sdetails"].ToString();
                string spec = dru["spec"].ToString();
                string serial = Convert.ToDouble(dru["serial"]).ToString();



                if (rate > 0)
                {

                    result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "INSERTORUPDATECORDER", "PURCORDERA", mOrdernoO,
                        flrcod, Rsircode, rate.ToString(), txtremarks, qty, amt, sdetails, spec, serial, "", "", "", "");
                    if (!result)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }
                }
            }
           ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
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
            int TblRowIndex;
            for (int i = 0; i < this.gvorder.Rows.Count; i++)
            {
                double ordrate = Convert.ToDouble("0" + ((TextBox)this.gvorder.Rows[i].FindControl("txtgvrate")).Text.Trim());
                double ordqty = Convert.ToDouble("0" + ((TextBox)this.gvorder.Rows[i].FindControl("txtgvordqty")).Text.Trim());
                string txtisurmk = ((TextBox)this.gvorder.Rows[i].FindControl("txtisurmk")).Text.Trim();
                string sdetails = ((TextBox)this.gvorder.Rows[i].FindControl("txtwrkdesc")).Text.Trim();
                string spec = ((TextBox)this.gvorder.Rows[i].FindControl("txtspec")).Text.Trim();
                double serial = Convert.ToInt32("0" + ((TextBox)this.gvorder.Rows[i].FindControl("txtserial")).Text.Trim());
                TblRowIndex = (gvorder.PageIndex) * gvorder.PageSize + i;




                dt.Rows[TblRowIndex]["ordqty"] = ordqty.ToString();
                dt.Rows[TblRowIndex]["ordrrate"] = ordrate.ToString();
                dt.Rows[TblRowIndex]["ordamt"] = (ordqty * ordrate).ToString();
                dt.Rows[TblRowIndex]["rmrks"] = txtisurmk;
                dt.Rows[TblRowIndex]["sdetails"] = sdetails;
                dt.Rows[TblRowIndex]["spec"] = spec;
                dt.Rows[TblRowIndex]["serial"] = serial;


            }
            ViewState["tblorder"] = dt;
        }


        protected void gvorder_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
        protected void gvorder_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvorder.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void ibtnSearchMaterisl_Click(object sender, EventArgs e)
        {

        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }




        protected void ddlgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetMaterials();

        }

        protected void lbtndelete_Click(object sender, EventArgs e)
        {

            GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            int rownum = gvr.RowIndex;
            int rowindex = (this.gvorder.PageSize) * (this.gvorder.PageIndex) + rownum;

            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblorder"];


            string mOrdernoO = this.lblCurISSNo1.Text.Trim().Substring(0, 3) + this.txtCurISSDate.Text.Trim().Substring(7, 4) + this.lblCurISSNo1.Text.Trim().Substring(3, 2) + this.txtCurISSNo2.Text.Trim();

            string flrcod = dt.Rows[rowindex]["flrcod"].ToString();
            string rsircode = dt.Rows[rowindex]["rsircode"].ToString();

            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "DELETEORDERRESCODE", mOrdernoO, flrcod, rsircode, "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {

                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            ViewState.Remove("tblorder");
            ViewState["tblorder"] = dv.ToTable();
            this.Data_Bind();
        }
        protected void ibtnCopyorderno_Click(object sender, EventArgs e)
        {
            this.GetCopyOrder();
        }

        private void GetCopyOrder()
        {
            string comcod = this.GetCompCode();
            // string CurDate1 = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString("dd-MMM-yyyy");
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPRVCORDERNO", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlcopyorder.Items.Clear();
            this.ddlcopyorder.DataTextField = "orderno1";
            this.ddlcopyorder.DataValueField = "orderno";
            this.ddlcopyorder.DataSource = ds1.Tables[0];
            this.ddlcopyorder.DataBind();

        }
        protected void lbtnCopyOrder_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();

            string orderno = this.ddlcopyorder.SelectedValue.ToString();
            string CurDate1 = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString();

            DataSet ds1 = new DataSet();

            ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETCONORDERINFO", orderno, "",
                         "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tblorder"] = ds1.Tables[0];
            ViewState["UserLog"] = ds1.Tables[1].Clone();
            this.txtTerm.Text = ds1.Tables[2].Rows[0]["term"].ToString();


            this.chkCopy.Checked = false;
            this.chkCopy_CheckedChanged(null, null);
            this.Data_Bind();
        }
        protected void chkCopy_CheckedChanged(object sender, EventArgs e)
        {
            this.PnlCopy.Visible = this.chkCopy.Checked;
        }
    }
}