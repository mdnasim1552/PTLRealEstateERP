
using Microsoft.Reporting.WinForms;
using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_17_Acc
{
    public partial class RptLetterOfAlotment : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = "Customer Sales Reports";
                this.GetProjectName();
                this.GetCustomerName();
                this.selectview();
            }
        }


        protected void Page_PreInit(object sender, EventArgs e)
        {

            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "Allotment":
                    ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;
                
                    ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
                    
                    ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);


                   
                    break;
                case "CustomerSettlement":
                    ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;
                    ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
                    ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
                    ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lFinalUpdate_Click);
                    ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
                    break;
            }
           
        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        private void GetProjectName()
        {
            string comcod = this.GetCompCode();
            string txtSProject = "%%";
            Session.Remove("tblproject");

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlprjname.DataTextField = "pactdesc";
            this.ddlprjname.DataValueField = "pactcode";
            this.ddlprjname.DataSource = ds1.Tables[0];
            this.ddlprjname.DataBind();
            Session["tblproject"] = ds1.Tables[0];
            this.ddlprjname_SelectedIndexChanged(null, null);
            ds1.Dispose();



        }

        private void GetCustomerName()
        {
            string custotype = this.Request.QueryString["Type"].ToString();
            //string calltype = custotype=="LO"? "GETCUSTOMERNAMELANDOWNER" : "GETCUSTOMERNAME";          
            string comcod = this.GetCompCode();
            string pactcode = this.ddlprjname.SelectedValue == " " ? "51%" : this.ddlprjname.SelectedValue.ToString() + "%";
            string islandowner = this.Request.QueryString["Type"] == "Allotment" ? "0" : "1";
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETCUSTLIST", pactcode, "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
            this.ddlcustomerName.DataTextField = "gdatat";
            this.ddlcustomerName.DataValueField = "usircode";
            this.ddlcustomerName.DataSource = ds2.Tables[0];
            this.ddlcustomerName.DataBind();

        }
        public void selectview()
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "Allotment":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;
                case "CustomerSettlement":
                    this.MultiView1.ActiveViewIndex = 1;
                    break;
            }
        }

        protected void ddlprjname_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetCustomerName();
        }


        private void lbtnPrint_Click(object sender, EventArgs e)
        {
            string qtype = this.Request.QueryString["Type"].ToString().Trim();

            if (qtype == "Allotment")
            {
                this.LetterofAllotmentCPDL();
            }

            else if (qtype == "CustomerSettlement")
            {
                string comcod = this.GetCompCode();
                switch (comcod)
                {
                    case "3374":
                    case "3376":
                        this.CustomerSettlementANGAN();
                        break;
                    default:
                        if (this.ddlreptype.SelectedValue == "1")
                        {
                            this.CustomerSettlementCPDL();
                        }
                        else
                        {
                            this.CustomerFinalstatementCPDL();
                        }
                        break;
                }


                        
            }
            

            //string comcod = this.GetCompCode();
            //switch (comcod)
            //{
            //    case "3370":
            //    case "3101":
            //        string qtype = this.Request.QueryString["Type"].ToString().Trim();
            //        if (qtype == "Allotment")
            //        {
            //            this.LetterofAllotmentCPDL();
            //        }
            //        else if (qtype == "CustomerSettlement")
            //        {
            //            this.CustomerSettlementCPDL();
            //        }
            //        break;
            //    default:
            //        break;
            //}

        }

        private void LetterofAllotmentCPDL()
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
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string prjname = this.ddlprjname.SelectedValue.ToString();
            string ProjectName = this.ddlprjname.SelectedItem.ToString();
            string custname = this.ddlcustomerName.SelectedValue.ToString();


            DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETCUSTOMERDETAILS", prjname, custname, "", "", "", "", "", "", "");
            if (ds2 == null)
                return;



            string method = "OTHER PAYMENTS ON CUSTOMERS ACCOUNT";
            string head01 = "At actual with incidental expenses at the time of Registration";

            string headtitle01 = "Payment of other Govt. Charges, Gain or Source Tax, Apt VAT, AIT, Stamp Duties etc. : ";
            string bodytitle01 = "At actual with last Installment.";
            string OptionalCost = "Optional Up Gradation Cost : ";
            string optionalDetails = "On acceptance of customers request";
            string type = ds2.Tables[0].Rows[0]["flrdesc"].ToString();
            string type01 = ds2.Tables[0].Rows[0]["flrdesc"].ToString(); ;

            string condition = "GENERAL TERMS & CONDITIONS OF ALLOTMENT FOR " + "<strong>" + type01 + "<strong>";
            string companyname = "CPDL";
            string heading = " " + "<strong>" + companyname + "</strong>" + " is pleased to offer the allotment of the property as schedule below in your favor  subject to the following Terms and" + "<br>" +
                "condition, but not limited there to, since variation may take place in case of necessity, for strict adherence by the applicant /" +
                " allottee.";


            //string body = "<div style='text-align: justify; text-justify: inter-character;'>1. All payment should be made to CPDL by Account Payee Cheque or Bank Draft or Pay Order or DD or TT in locally against which respective receipts will be issued. All payments of the applicant / allottee from outside of Chittagong City should be made to CPDL by local TT or DD from any scheduled commercial bank. The Bangladeshi residing abroad may remit payments in foreign exchange by international TT or DD. Any type of Cash payment is totally restricted. Payments of installment and other charges are to be made on due dates. The company may issue reminders to the Allottee but not withstanding the issue of reminders,the Allottee must adhere to the schedule of payment to ensure completion of construction in time"+"</div>";

            string body = "<div style='text-align: justify; text-justify: inter-character;'>1. All payment should be made to " + "<strong>" + companyname + "</strong>" + " by Account Payee Cheque or Bank Draft or Pay Order or DD or TT in locally against" +
                      " which respective receipts will be issued. All payments of the applicant / allottee from outside of Chittagong City should " +
                      "be made to " + "<strong>" + companyname + "</strong>" + " by local TT or DD from any scheduled commercial bank. The Bangladeshi residing abroad may remit payments " +
                      "in foreign exchange by international TT or  DD. Any type of Cash payment is totally restricted. Payments of installment and" +
                      " other charges are to be made on due dates. The company may issue reminders to the Allottee " +
                      "but not withstanding the issue of reminders,the Allottee must adhere to the schedule of payment to ensure completion of construction in time." + "</div>" + "<br>" +
                      "2. Delay in payments beyond the due date will make the allottee liable to pay a delay charge of 3% per 30 (Thirty) days on " +
                      "the amount of payment delayed.If the payment is delayed beyond 60(sixty) days or if the allottee wishes to surrender " +
                      "his allotment, the Company shall cancel the allotment without serving any notice to the Allottee. In such an " +
                      "event the amount paid by the allottee will be refunded after deducting 10% service charge from total " +
                      "deposited amount against allotted " + "<strong>" + type + "</strong>" + " space only after 90 working days from the date of cancellation /surrender." + "<br><br>" +
                      "3. The allotee should not have right to transfer the allotment to a third party until full payment of installments and other charges, if any. " + "<br><br>" +
                      "4. Connection fees/charges, security deposits and other incidental expenses relating to gas, water,sewerage and electric " +
                      "connections  as Utility Charges  are not included in the price of " + "<strong>" + type + "</strong>" + " space. These payments will be made by the " +
                      "company directly to the authorities concerned, on the allottees account." + "<br><br>" +
                      "5. Limited changes in the " + type + " and other facilities may be made by the " + "<b>" + companyname + "</b>" + " for greater and overall interest of the project.  " + "<br><br>" +
                      " 6. No modification from customer's end will be allowed on elevation or which seen from outside of the complex, sanitary line etc." + "<br><br>" +
                      "7. If the allottee intends to have any modification of the civil or electrical work of his " + type +
                      " compared to the standard set by " + "<strong>" + companyname + "</strong>" + ", any such modification request shall first be assessed  by the " + "<strong>" + companyname + "</strong>" + " management." +
                      "Implementation of any such modification work, whether in part or full, is strictly subject to prior approval of " + "<strong>" + companyname + "</strong>" + " management. " +
                      "In the event, additional cost is involved in implementing any such modification request, the concerned allottee must bear such cost. " +
                      "The allottee is at liberty to select fittings & fixtures of his/her own choice other than those specified in the " + "<strong>" + companyname + "</strong>" + " s " +
                      "standard materials specification sheet." + "<br><br>" +
                      "8. However, if additional cost is involved for use of any such fittings and / or fixtures, the allottee must bear such extra cost. " + "<br><br><br><br>" +
                      "9. Civil layout, electrical layout, modification, finishing material confirmation should be completed within 30 days after dispatch of request letter for " +
                      "the mentioned purpose,if it doesn't; civil layout and electrical layout will be made as per  " + "<strong>" + companyname + "</strong>" + " standard. " +
                      "However, no modification will be done after official handover of the project. If there is any modification it has to be done by " + "<strong>" + type + "</strong>" + " owner with his/her own cost and arrangement." + "<br><br>" +
                      "10. The possession of each " + "<strong>" + type + "</strong>" + " shall duly be handed over to the allottee on completion and on full payment of installments and other charges and dues." +
                      "Until then the possession will be held by the " + "<strong>" + companyname + "</strong>" + " . If the construction and finishing work of each building is completed on " +
                      "before declared handover tenure due to smoothness of allover activities, the monthly installment schedule of the allottee will be restructured " +
                      "and he/she must liable to pay as per rescheduled amount as well as bound to take the possession of respective " + "<strong>" + type + "</strong>" + "(s)." + "<br><br>" +
                      "11. Upon registration, the " + "<strong>" + type + "</strong>" + " owner, irrespective of the floor, will become the proportionate owner of the un-divided and un-demarcated land on  " +
                      "which the building is constructed. After having possession of the " + "<strong>" + type + "</strong>" + " , the allottee must consult with the " + "<strong>" + companyname + "</strong>" + " prior to undertake " +
                      "structural or layout change within the " + "<strong>" + type + "</strong>" + " complex. Failure to do so will be at the sole risk of allottee." + "<br><br>" +
                      "12. The completion period may be affected and delayed by the unavoidable circumstances beyond the control of the company, like Force Majeure, Natural Calamities," +
                      "Political Disturbances, Act of God, Strike, Non Availability of Materials, Change in the Policy of the Government etc." + "<br><br>" +
                      "13. " + "<strong>" + companyname + "</strong>" + " will not take any responsibility to provide finishing materials beyond " + "<strong>" + companyname + "</strong>" + " standard." + "<br><br>" +
                      "14. After handing over the " + "<strong>" + type + "</strong>" + " to the allottee by " + "<strong>" + companyname + "</strong>" + ", an association of Apartment space " +
                      "owners namely Collective Management Committee (CMC) will be formed. All the " + type + " owners " +
                      "will be the member of the committee by contributing in the reserve fund. The Collective Management " +
                      "Committee (CMC) will manage the common facility of the Apartment and all the common interest of the " +
                      "<strong>" + type + "</strong>" + " owners.</p>";



            string generalTitle = "GENERAL AGREEMENT";
            string generalbody = "Applicants Part of the Application Form, Materials Specification, Acknowledgement of Booking " +
                               "Amount, Money Receipt and Payment Schedule will be treated as an integral part of this Allotment " +
                               "Letter. On acceptance of this Allotment Letter, please return the duplicate of the same with your " +
                               "signature for our record." + "<br><br>" +
                               "The management of " + "<strong>" + companyname + "</strong>" + " congratulates you on this occasion and look forward for successful " +
                               "handing over of your " + type + ".";





            string dateofbirth = Convert.ToDateTime(ds2.Tables[0].Rows[0]["dateofbirth"].ToString()).ToString("dd-MMM-yyyy");
            string custsignature = (ds2.Tables[0].Rows[0]["custname"].ToString());

            DataSet ds3 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETDETAILS", prjname, custname, "", "", "", "", "", "", "");
            if (ds3 == null)
                return;
            string custid = ds3.Tables[0].Rows[0]["customerno"].ToString();
            string floorno = ds3.Tables[0].Rows[0]["floorno"].ToString();
            string unitno = ds3.Tables[0].Rows[0]["udesc"].ToString();
            string size = Convert.ToDouble(ds3.Tables[0].Rows[0]["usize"]).ToString("#,##0;(#,##0); ");
            string carno = ds3.Tables[0].Rows[0]["carno"].ToString();
            string floordesc = ds3.Tables[0].Rows[0]["flrdesc"].ToString();
            string parkingqty = Convert.ToDouble(ds3.Tables[0].Rows[0]["pqty"]).ToString("#,##0;(#,##0); ");
            string stbokking = Convert.ToDouble(ds3.Tables[0].Rows[0]["stdbookam"]).ToString("#,##0.00;(#,##0.00); ");
            string peribookam = Convert.ToDouble(ds3.Tables[0].Rows[0]["peribookam"]).ToString("#,##0.00;(#,##0.00); ");




            double urate = Convert.ToDouble(ds3.Tables[0].Rows[0]["urate"]);
            double uamt = Convert.ToDouble(ds3.Tables[0].Rows[0]["urate"]);
            double tamt = Convert.ToDouble(ds3.Tables[0].Rows[0]["tamt"]);
            double pramt = Convert.ToDouble(ds3.Tables[0].Rows[0]["pamt"]);
            double ucharge = Convert.ToDouble(ds3.Tables[0].Rows[0]["utility"]);
            string othercharge = Convert.ToDouble(ds3.Tables[0].Rows[0]["others"]).ToString("#,##0.00;(#,##0.00); ");


            string utility = ucharge.ToString("#,##0.00;(#,##0.00); ");
            string pamt = pramt.ToString("#,##0.00;(#,##0.00); ");
            string rate = urate.ToString("#,##0.00;(#,##0.00); ");
            string totalamt = tamt.ToString("#,##0.00;(#,##0.00); ");
            string unit = ds3.Tables[0].Rows[0]["munit"].ToString();
            string aprtsize = size + " " + unit;
            string Location = ds3.Tables[0].Rows[0]["location"].ToString();
            string enrolldate = Convert.ToDateTime(ds3.Tables[0].Rows[0]["enrolldate"]).ToString("dd-MMM-yyyy");
            string unitcost = uamt.ToString("#,##0;(#,##0); ");


            string initialpayment = Convert.ToDouble("0" + ds3.Tables[0].Rows[0]["initialpament"].ToString()).ToString("#,##0.00;(#,##0.00); ");
            string dnpayment = Convert.ToDouble("0" + ds3.Tables[0].Rows[0]["downpayment"].ToString()).ToString("#,##0.00;(#,##0.00); ");
            string upDatePaym = Convert.ToDouble("0" + ds3.Tables[0].Rows[0]["updatpayamount"].ToString()).ToString("#,##0.00;(#,##0.00); ");
            string Uppay = Convert.ToDouble("0" + ds3.Tables[0].Rows[0]["updatpay"].ToString()).ToString("#,##0.00;(#,##0.00); ");
            // string totalcost = Convert.ToDouble("0" + ds3.Tables[0].Rows[0]["unittotalcost"].ToString()).ToString("#,##0.00;(#,##0.00); ");

            string expectdate = Convert.ToDateTime(ds3.Tables[0].Rows[0]["handoverdat"].ToString()).ToString("dd-MMM-yyyy");


            LocalReport Rpt1 = new LocalReport();
            var lst = ds2.Tables[0].DataTableToList<RealEntity.C_22_Sal.Sales_BO.AllotmentInfo>();
            var lst2 = ds3.Tables[1].DataTableToList<RealEntity.C_22_Sal.EClassSales_02.Rptalloreport>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptLetterOfAllotmentCPDL", lst, lst2, null);
            Rpt1.EnableExternalImages = true;

            Rpt1.SetParameters(new ReportParameter("RptTitle", "LETTER OF ALLOTMENT"));
            Rpt1.SetParameters(new ReportParameter("dateofbirth", dateofbirth));
            // Rpt1.SetParameters(new ReportParameter("body02", body02));                
            Rpt1.SetParameters(new ReportParameter("custid", custid));
            Rpt1.SetParameters(new ReportParameter("heading", heading));
            Rpt1.SetParameters(new ReportParameter("method", method));
            Rpt1.SetParameters(new ReportParameter("head01", head01));
            Rpt1.SetParameters(new ReportParameter("headtitle01", headtitle01));
            Rpt1.SetParameters(new ReportParameter("bodytitle01", bodytitle01));
            Rpt1.SetParameters(new ReportParameter("OptionalCost", OptionalCost));
            Rpt1.SetParameters(new ReportParameter("optionalDetails", optionalDetails));
            Rpt1.SetParameters(new ReportParameter("peribookam", peribookam));
            Rpt1.SetParameters(new ReportParameter("condition", condition));
            Rpt1.SetParameters(new ReportParameter("body", body));
            Rpt1.SetParameters(new ReportParameter("totalamt", totalamt));
            Rpt1.SetParameters(new ReportParameter("stbokking", stbokking));
            Rpt1.SetParameters(new ReportParameter("prjname", ProjectName));
            Rpt1.SetParameters(new ReportParameter("Location", Location));
            Rpt1.SetParameters(new ReportParameter("usize", size));
            Rpt1.SetParameters(new ReportParameter("size", unitno));
            Rpt1.SetParameters(new ReportParameter("floordesc", floordesc));
            Rpt1.SetParameters(new ReportParameter("aprtsize", aprtsize));
            Rpt1.SetParameters(new ReportParameter("floorno", floorno));
            Rpt1.SetParameters(new ReportParameter("enrolldate", enrolldate));
            Rpt1.SetParameters(new ReportParameter("price", rate));
            Rpt1.SetParameters(new ReportParameter("ParkingQty", parkingqty));
            Rpt1.SetParameters(new ReportParameter("unitcost", unitcost));
            Rpt1.SetParameters(new ReportParameter("parkingcost", pamt));
            Rpt1.SetParameters(new ReportParameter("utilityCharge", utility));
            Rpt1.SetParameters(new ReportParameter("othercharge", othercharge));
            // Rpt1.SetParameters(new ReportParameter("discount", discount));                       
            Rpt1.SetParameters(new ReportParameter("initialpayment", initialpayment));
            Rpt1.SetParameters(new ReportParameter("dnpayment", dnpayment));
            Rpt1.SetParameters(new ReportParameter("upDatePaym", upDatePaym));
            Rpt1.SetParameters(new ReportParameter("Uppay", Uppay));
            Rpt1.SetParameters(new ReportParameter("expectdate", expectdate));
            Rpt1.SetParameters(new ReportParameter("generalTitle", generalTitle));
            Rpt1.SetParameters(new ReportParameter("generalbody", generalbody));
            Rpt1.SetParameters(new ReportParameter("custsignature", custsignature));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("totalcost", tamt.ToString("#,##0.00;(#,##0.00); ")));



            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void CustomerSettlementCPDL()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy");
            string prjname = this.ddlprjname.SelectedValue.ToString();
            string ProjectName = this.ddlprjname.SelectedItem.ToString();
            string custname = this.ddlcustomerName.SelectedValue.ToString();
            if (custname != "")
            {


                DataSet ds3 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETSETTLEMENTDETAILS", prjname, custname, "", "", "", "", "", "", "");
                DataTable dt = (DataTable)ds3.Tables[0];
                DataView dv = dt.DefaultView;
                dv.RowFilter = "code='05AAA'";
                DataRow[] dr = dt.Select("code='05AAA'");
                double totaltk = Convert.ToDouble(dr[0]["amt"]);


                string amginword = ASTUtility.Trans(Convert.ToDouble(totaltk), 2);


                if (ds3 == null)
                    return;
                LocalReport Rpt1 = new LocalReport();
                var lst = ds3.Tables[0].DataTableToList<RealEntity.C_22_Sal.EClassSales_02.RptCustomerSettlement>();
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptCustomerSettlementCPDL", lst, null, null);
                Rpt1.EnableExternalImages = true;


                string customername = ds3.Tables[1].Rows[0]["custname"].ToString();
                string projectname = ds3.Tables[1].Rows[0]["projectname"].ToString();
                string unitname = ds3.Tables[1].Rows[0]["udesc"].ToString();
                string usize = Convert.ToDouble(ds3.Tables[1].Rows[0]["usize"]).ToString("#,##0;(#,##0); ");
                string floordesc = ds3.Tables[1].Rows[0]["flrdesc"].ToString();
                string unit = ds3.Tables[1].Rows[0]["munit"].ToString();
                string aprtsize = usize + " " + unit;
                string location = ds3.Tables[1].Rows[0]["location"].ToString();
                string refdesc = ds3.Tables[2].Rows[0]["refdesc"].ToString();


                Rpt1.SetParameters(new ReportParameter("printdate", printdate));
                Rpt1.SetParameters(new ReportParameter("customername", customername));
                Rpt1.SetParameters(new ReportParameter("projectname", projectname));
                Rpt1.SetParameters(new ReportParameter("unitname", unitname));
                Rpt1.SetParameters(new ReportParameter("location", location));
                Rpt1.SetParameters(new ReportParameter("usize", usize));
                Rpt1.SetParameters(new ReportParameter("refdesc", "Ref: " + refdesc));
                Rpt1.SetParameters(new ReportParameter("unit", unit));
                Rpt1.SetParameters(new ReportParameter("aprtsize", aprtsize));
                Rpt1.SetParameters(new ReportParameter("floordesc", floordesc));
                Rpt1.SetParameters(new ReportParameter("totaltk", amginword));

                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }


        }
        private void CustomerFinalstatementCPDL()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy");
            string prjname = this.ddlprjname.SelectedValue.ToString();
            string ProjectName = this.ddlprjname.SelectedItem.ToString();
            string custname = this.ddlcustomerName.SelectedValue.ToString();
            if (custname != "")
            {


                DataSet ds3 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTHANDOVERFORM", prjname, custname, "", "", "", "", "", "", "");
                DataTable dt = (DataTable)ds3.Tables[0];
                DataTable dt1 = (DataTable)ds3.Tables[1];
                string customername = dt1.Rows[0]["custname"].ToString();
                string udesc = dt.Rows[0]["udesc"].ToString();
                string bookingdate = Convert.ToDateTime(dt1.Rows[0]["bookingdate"]).ToString("dd-MMM-yyyy");

                string delaycharge = Convert.ToDecimal(dt.Rows[0]["delaycharge"]).ToString("#,##0.00;(#,##0.00);");
                string pqty = Convert.ToDecimal(dt.Rows[0]["pqty"]).ToString("#,##0.00;(#,##0.00);");
                string usize = Convert.ToDecimal(dt.Rows[0]["usize"]).ToString("#,##0.00;(#,##0.00);");
                string totalprice = Convert.ToDecimal(dt.Rows[0]["totalprice"]).ToString("#,##0.00;(#,##0.00);");
                string total = Convert.ToDecimal(dt.Rows[0]["total"]).ToString("#,##0.00;(#,##0.00);");
                string totalcostprop = Convert.ToDecimal(dt.Rows[0]["totalcostprop"]).ToString("#,##0.00;(#,##0.00);");
                string totalcostprop1 = Convert.ToDecimal(dt.Rows[0]["totalcostprop1"]).ToString("#,##0.00;(#,##0.00);");

                if (ds3 == null)
                    return;
                LocalReport Rpt1 = new LocalReport();
                var lst = dt.DataTableToList<RealEntity.C_22_Sal.EClassSales_02.RptCustomerFinalSteatement>();
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptCustomerFinalSteatement", lst, null, null);
                Rpt1.EnableExternalImages = true;


               

                Rpt1.SetParameters(new ReportParameter("printdate", printdate));
                Rpt1.SetParameters(new ReportParameter("total", total));
                Rpt1.SetParameters(new ReportParameter("totalcostprop", totalcostprop));
                Rpt1.SetParameters(new ReportParameter("totalcostprop1", totalcostprop1));
                Rpt1.SetParameters(new ReportParameter("ProjectName", ProjectName));
                Rpt1.SetParameters(new ReportParameter("customername", customername));
                Rpt1.SetParameters(new ReportParameter("bookingdate", bookingdate));
                Rpt1.SetParameters(new ReportParameter("pqty", pqty));
                Rpt1.SetParameters(new ReportParameter("udesc", udesc));
                Rpt1.SetParameters(new ReportParameter("delaycharge", delaycharge));
                Rpt1.SetParameters(new ReportParameter("totalprice", totalprice));
                Rpt1.SetParameters(new ReportParameter("usize", usize));
                Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
                //Rpt1.SetParameters(new ReportParameter("customername", customername));
             

                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }


        }
        private void CustomerSettlementANGAN()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy");
            string prjname = this.ddlprjname.SelectedValue.ToString();
            string ProjectName = this.ddlprjname.SelectedItem.ToString();
            string custname = this.ddlcustomerName.SelectedValue.ToString();
            if (custname != "")
            {


                DataSet ds3 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETSETTLEMENTDETAILSANGAN", prjname, custname, "", "", "", "", "", "", "");
                DataTable dt = (DataTable)ds3.Tables[0];
                DataView dv = dt.DefaultView;
                dv.RowFilter = "code='05AAA'";
                DataRow[] dr = dt.Select("code='05AAA'");
                double totaltk = Convert.ToDouble(dr[0]["amt"]);


                string amginword = ASTUtility.Trans(Convert.ToDouble(totaltk), 2);


                if (ds3 == null)
                    return;
                LocalReport Rpt1 = new LocalReport();
                var lst = ds3.Tables[0].DataTableToList<RealEntity.C_22_Sal.EClassSales_02.RptCustomerSettlement>();
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptCustomerSettlementANGAN", lst, null, null);
                Rpt1.EnableExternalImages = true;


                Rpt1.SetParameters(new ReportParameter("comadd", comadd));
                Rpt1.SetParameters(new ReportParameter("compname", comnam));
                Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));

                string customername = ds3.Tables[1].Rows[0]["custname"].ToString();
                string unitname = ds3.Tables[1].Rows[0]["udesc"].ToString();
                string usize = Convert.ToDouble(ds3.Tables[1].Rows[0]["usize"]).ToString("#,##0;(#,##0); ");
                string floordesc = ds3.Tables[1].Rows[0]["flrdesc"].ToString();
                string unit = ds3.Tables[1].Rows[0]["munit"].ToString();
                string aprtsize = usize + " " + unit;
                string location = ds3.Tables[1].Rows[0]["location"].ToString();
                string refdesc = ds3.Tables[2].Rows[0]["refdesc"].ToString();
                string unitrate = Convert.ToDouble("0" + ds3.Tables[1].Rows[0]["unitrate"].ToString()).ToString("#,##0.00;(#,##0.00); ");
                string bookingdate = Convert.ToDateTime(ds3.Tables[1].Rows[0]["bookingdate"]).ToString("dd-MMM-yyyy") == "01-Jan-1900" ? "" : Convert.ToDateTime(ds3.Tables[1].Rows[0]["bookingdate"]).ToString("dd-MMM-yyyy");
                //string handovdate1 = Convert.ToDateTime(ds3.Tables[1].Rows[0]["handovdate"]).ToString("dd-MMM-yyyy");
                string aggrementdate = Convert.ToDateTime(ds3.Tables[1].Rows[0]["aggrementdate"]).ToString("dd-MMM-yyyy") == "01-Jan-1900" ? "" : Convert.ToDateTime(ds3.Tables[1].Rows[0]["aggrementdate"]).ToString("dd-MMM-yyyy");

                Rpt1.SetParameters(new ReportParameter("printdate", printdate));
                Rpt1.SetParameters(new ReportParameter("customername", customername));
                Rpt1.SetParameters(new ReportParameter("projectname", ProjectName));
                Rpt1.SetParameters(new ReportParameter("unitname", unitname));
                Rpt1.SetParameters(new ReportParameter("location", location));
                Rpt1.SetParameters(new ReportParameter("usize", usize));
                Rpt1.SetParameters(new ReportParameter("refdesc", "Ref: " + refdesc));
                Rpt1.SetParameters(new ReportParameter("unit", unit));
                Rpt1.SetParameters(new ReportParameter("aprtsize", aprtsize));
                Rpt1.SetParameters(new ReportParameter("floordesc", floordesc));
                Rpt1.SetParameters(new ReportParameter("unitrate", unitrate));
                Rpt1.SetParameters(new ReportParameter("aggrementdate", aggrementdate));
                Rpt1.SetParameters(new ReportParameter("bookingdate", bookingdate));
                Rpt1.SetParameters(new ReportParameter("totaltk", amginword));

                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }


        }
        private void GetSettleMentno()
        {

            string comcod = this.GetCompCode();
            string CurDate1 = this.txtdate.Text;

            string pactcode = this.ddlprjname.SelectedValue.ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETSETTLEMENTREFNO", CurDate1, pactcode, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            if (ds1.Tables[0].Rows.Count > 0)
            {
                this.txtrefno.Text = ds1.Tables[0].Rows[0]["maxno"].ToString();
                this.txtrefdesc.Text = ds1.Tables[0].Rows[0]["maxno1"].ToString();

            }




        }


        protected void btnok_Click(object sender, EventArgs e)
        {
            try
            {
                //string Type=this.rq



                DateTime nowDate = DateTime.Now;
                this.divdate.Visible = true;
                this.txtdate.Text = nowDate.ToString("dd-MMM-yyyy");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string prjname = this.ddlprjname.SelectedValue.ToString();
                string custname = this.ddlcustomerName.SelectedValue.ToString();
                string type = this.Request.QueryString["Type"].ToString();


               

                if (type == "CustomerSettlement")
                {
                    DataSet ds1 = new DataSet();
                    if (comcod == "3374" || comcod == "3376")
                    {
                         ds1 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETSETTLEMENTDETAILSANGAN", prjname, custname, "", "", "", "", "", "", "");
                    }
                    else
                    {
                         ds1 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETSETTLEMENTDETAILS", prjname, custname, "", "", "", "", "", "", "");
                    }
                    
                    if (ds1.Tables[0].Rows.Count == 0)
                    {
                        this.gvcustsettlement.DataSource = null;
                        this.gvcustsettlement.DataBind();
                        return;
                    }

                    Session["storedata"] = ds1;
                    this.gvcustsettlement.DataSource = ds1.Tables[0];
                    this.gvcustsettlement.DataBind();


                    if (ds1.Tables[2].Rows.Count == 0)
                    {

                        this.txtdate.Enabled = true;
                        this.GetSettleMentno();
                        return;

                    }


                    this.txtdate.Text = Convert.ToDateTime(ds1.Tables[2].Rows[0]["settlemntdat"]).ToString("dd-MMM-yyyy");
                    this.txtrefdesc.Text = ds1.Tables[2].Rows[0]["refdesc"].ToString();
                    this.txtrefno.Text = ds1.Tables[2].Rows[0]["refno"].ToString();
                    this.txtdate.Enabled = false;



                }

            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }


        protected void lFinalUpdate_Click(object sender, EventArgs e)
        {

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('You have no permission');", true);
                return;
            }



            string comcod = this.GetCompCode();
            string PactCode = this.ddlprjname.SelectedValue.ToString();
            string usircode = this.ddlcustomerName.SelectedValue.ToString();

            string date = this.txtdate.Text.Trim();
            if (this.txtdate.Enabled == true)
            {
                this.GetSettleMentno();
            }
            string refno = this.txtrefno.Text.Trim();
            string refdesc = this.txtrefdesc.Text;

            bool result = purData.UpdateTransInfo2(comcod, "SP_ENTRY_SALSMGT", "INSERTORSETTLEMENT", PactCode, usircode, refno, refdesc, date, "", "", "", "", "", "", "", "", "", "", "", "", "", "","", "");

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Failed');", true);

            }


            this.txtdate.Enabled = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);


        }


        private void btnClose_Click(object sender, EventArgs e)
        {


            Response.Redirect(this.Request.UrlReferrer.ToString());

        }

    }
}