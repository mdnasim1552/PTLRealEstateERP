
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
            }
        }


        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
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


            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");

            DataTable dt = ds1.Tables[0];
            if (this.Request.QueryString["Type"].ToString().Trim() == "DueCollAll")
            {
                DataView dv1 = dt.DefaultView;
                dv1.RowFilter = "pactcode not like '000000000000%'";
                dt = dv1.ToTable();

            }



            this.ddlprjname.DataTextField = "pactdesc";
            this.ddlprjname.DataValueField = "pactcode";
            this.ddlprjname.DataSource = dt;
            this.ddlprjname.DataBind();
            this.ddlprjname_SelectedIndexChanged(null, null);



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
        

        protected void ddlprjname_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetCustomerName();
        }

        private void lbtnPrint_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3370":
                case "3101":
                    this.LetterofAllotmentCPDL();

                    break;
                default:
                    break;
            }

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
                

                string method = "OTHER PAYMENTS ON CUSTOMERS ACCOUNT";
                string head01 = "At actual with incidental expenses at the time of Registration";

                string headtitle01 = "Payment of other Govt. Charges, Gain or Source Tax, Apt VAT, AIT, Stamp Duties etc. : ";
                string bodytitle01 = "At actual with last Installment.";
                string OptionalCost = "Optional Up Gradation Cost : ";
                string optionalDetails = "On acceptance of customers request";
                string type = "Flat";
                string type01 = "FLAT";

                string condition = "GENERAL TERMS & CONDITIONS OF ALLOTMENT FOR " + "<strong>" + type01 + "<strong>";
                string companyname = "CPDL";
                string heading = " " + "<strong>" + companyname + "</strong>" + " is pleased to offer the allotment of Apartment space in your favor only subject to the following Terms and condition " + "<br>" +
                    "but not Limited there to, since variation may take place in case of necessity, for strict adherence by the applicant / allottee.";


                string body = "1. All payment should be made to " + "<strong>" + companyname + "</strong>" + " by Account Payee Cheque or Bank Draft or Pay Order or DD or TT in locally against" + "<br>" +
                          "which respective receipts will be issued. All payments of the applicant / allottee from outside of Chittagong City should " + "<strong>" + companyname + "</strong>" + " <br>" +
                          "be made to by local TT or DD from any scheduled commercial bank. The Bangladeshi residing abroad may remit payments " + "<br>" +
                          "in foreign exchange by international TT or  DD.Any Cash payment is restricted.Payments of installment and other" + "<br>" +
                          " charges are to be made on due dates.The company may issue reminders to the Allottee " + "<br>" +
                          "but notwithstanding the issue of reminders,the Allottee must adhere to the schedule of payment to ensure completion of construction in time. " + "<br><br>" +
                          " 2. Delay in payments beyond the due date will make the allottee liable to pay a delay charge of 3% per 30 Thirty)days on " + "<br>" +
                          "the amount of payment delayed.If the payment is delayed beyond 60(sixty) days or if the allottee wishes to surrender" + "<br>" +
                          "his allotment, the Company shall cancel the allotment without serving any notice to the Allottee. in such an" + "<br>" +
                          "event the amount paid by the allottee will be refunded after deducting 10% service charge from total " + "<br>" +
                          "deposited amount against allotted Apartment space only after 90 working days from the date of cancellation /surrender." + "<br><br>" +
                          "3. The allotee should not have right to transfer the allotment to a third party until full payment of installments and other charges, if any. " + "<br><br>" +
                          "4. Connection fees/charges, security deposits and other incidental expenses relating to gas, water,sewerage and electric" + "<br>" +
                          "connections  as Utility Charges  are not included in the price of " + type + " space. These payments will be made by the" + "<br>" +
                          "company directly to the authorities concerned, on the allottees account." + "<br><br>" +
                          "5. Limited changes in the " + type + " and other facilities may be made by the " + "<b>" + companyname + "</b>" + " for greater and overall interest of the project.  " + "<br><br>" +
                          " 6. No modification from customer's end will be allowed on elevation or which seen from outside of the complex, sanitary line etc.";
         string body02 = 
                          
                          "7. If the allottee intends to have any modification of the civil or electrical work of his " + type + " compared to the standard set by " +
                          "<strong>" + companyname + "</strong>" + " any such modification request shall first be assessed  by the " + "<strong>" + companyname + "</strong>" + " management." +
                          "Implementation of any such modification work whether in part or full, is strictly subject to prior approval of " + "<strong>" + companyname + "</strong>" + " management. "  +
                          "In the event, that an additional cost is involved in implementing any such modification request the concerned allottee must bear such cost. "  +
                          "The allottee is at liberty to select fittings & fixtures of his/her own choice other than those specified in the " + "<strong>" + companyname + "</strong>" + " s " +
                          "standard materials specification sheet." + "<br><br>" +
                          "8. However, if additional cost is involved for use of any such fittings and / or fixtures, the allottee must bear such extra cost. " + "<br><br>" +
                          "9. Civil layout, electrical layout, modification, finishing material confirmation should be completed within 30 days after dispatch of request letter for " +
                          "the mentioned purpose,the mentioned purpose,if it doesn't; civil layout and electrical layout will be made as per  " + "<strong>" + companyname + "</strong>" + " standard. " +                        
                          "However, no modification will be done after official handover of the project. If there is any modification it has to be done by " + type + " owner with his/her own cost and arrangement" + "<br><br>" +
                          "10. The possession of each " + type + " shall duly be handed over to the allottee on completion and on full paymentof installments and other charges and dues." +
                          "Until then the possession will be held by the " + "<strong>" + companyname + "</strong>" + " . If the construction and finishing work of each building is completed on " +
                          "before declared handover tenure due to smoothness of allover activities, the monthly installment schedule of the allottee will be restructured " +
                          "and he/she must liable to pay as per rescheduled amount as well as bound to take the possession of respective " + type + "(s)" + "<br><br>" +
                          "11. Upon registration, the " + type + " owner, irrespective of the floor, will become the proportionate owner of the un-divided and un-demarcated land on  " +
                          "which the building is constructed. After having possession of the" + type + " , the allottee must consult with the " + "<strong>" + companyname + "</strong>" + " before undertaking any " +
                          "structural or layout change within the Apartment complex. Failure to do so will be at the sole risk of allottee." + "<br><br>" +
                          "12. The completion period may be affected and delayed by the unavoidable circumstances beyond the control of the company, like Force Majeure, Natural Calamities,"  +
                          "Political Disturbances, Act of God, Strike Non Availability of Materials, Change in the Policy of the Government etc." + "<br><br>" +
                          "13. " + "<strong>" + companyname + "</strong>" + " will not take any responsibility for providing finishing materials beyond " + "<strong>" + companyname + "</strong>" + " standards" + "<br><br>" +
                          "14. Upon registration, the " + type + " owner, irrespective of the floor, will become the proportionate owner of the un-divided and un-demarcated land "  +
                          "on which the building is constructed. After having possession of the" + type + " , the allottee must consult with the " + "<strong>" + companyname + "</strong>" + " before " +
                          "undertaking any structural or layout change within the " + type + " complex. Failure to do so will be at the sole risk of allottee.";
                string generalTitle = "GENERAL AGREEMENT";
                string generalbody = "The enrollment Form, Materials Specification, Acknowledgement of Booking Amount, Money Receipt" +
                                   "and Payment Schedule will be an integral part of this Allotment Letter. On acceptance of this" + 
                                   "Allotment Letter, please return the duplicate of the same with your signature for our record." +
                                   "The management of " + "<strong>" + companyname + "</strong>" + " congratulates you on this occasion and looks forward to the successful handing over of your " + type + ".";

               

                DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETCUSTOMERDETAILS", prjname, custname, "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;

                
                string dateofbirth = Convert.ToDateTime(ds2.Tables[0].Rows[0]["dateofbirth"].ToString()).ToString("dd-MMM-yyyy");
                string custsignature = (ds2.Tables[0].Rows[0]["custname"].ToString());
               
                
                DataSet ds3 =  purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETDETAILS", prjname, custname, "", "", "", "", "", "", "");
                if (ds3 == null)
                    return;
                string custid = ds3.Tables[0].Rows[0]["customerno"].ToString();
                string floorno = ds3.Tables[0].Rows[0]["floorno"].ToString();
                string unitno = ds3.Tables[0].Rows[0]["unitno"].ToString();
                string size =ds3.Tables[0].Rows[0]["usize"].ToString();
                string carno = ds3.Tables[0].Rows[0]["carno"].ToString();
                string floordesc= ds3.Tables[0].Rows[0]["flrdesc"].ToString();
                string parkingqty= Convert.ToDouble(ds3.Tables[0].Rows[0]["pqty"]).ToString("#,##0.00;(#,##0.00); ");
                string stbokking= Convert.ToDouble(ds3.Tables[0].Rows[0]["stdbookam"]).ToString("#,##0.00;(#,##0.00); ");
                string peribookam= Convert.ToDouble(ds3.Tables[0].Rows[0]["peribookam"]).ToString("#,##0.00;(#,##0.00); ");




                double urate = Convert.ToDouble(ds3.Tables[0].Rows[0]["urate"]);
                double uamt = Convert.ToDouble(ds3.Tables[0].Rows[0]["uamt"]);
                double tamt = Convert.ToDouble(ds3.Tables[0].Rows[0]["tamt"]);
                double pramt = Convert.ToDouble(ds3.Tables[0].Rows[0]["pamt"]);
                double ucharge = Convert.ToDouble(ds3.Tables[0].Rows[0]["utility"]);

               
                string utility = ucharge.ToString("#,##0.00;(#,##0.00); ");
                string pamt = pramt.ToString("#,##0.00;(#,##0.00); ");
                string rate = urate.ToString("#,##0.00;(#,##0.00); ");
                string totalamt = tamt.ToString("#,##0.00;(#,##0.00); ");
                string unit = ds3.Tables[0].Rows[0]["munit"].ToString();
                string aprtsize = size + " " + unit;
                string Location = ds3.Tables[0].Rows[0]["location"].ToString();
                string enrolldate = Convert.ToDateTime(ds3.Tables[0].Rows[0]["enrolldate"]).ToString("dd-MMM-yyyy");                 
                string unitcost = uamt.ToString("#,##0.00;(#,##0.00); ");             
               
                string othercharge = "0.00";
                string discount = "0.00";
              
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
                Rpt1.SetParameters(new ReportParameter("body02", body02));                
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
                Rpt1.SetParameters(new ReportParameter("discount", discount));                       
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

        protected void btnok_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string prjname = this.ddlprjname.SelectedValue.ToString();
                string custname = this.ddlcustomerName.SelectedValue.ToString();
                DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETCUSTOMERDETAILS", prjname, custname, "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;

                Session["tblcudtomerdetails"] = ds2.Tables[0];

            }
            catch(Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }
    }
}