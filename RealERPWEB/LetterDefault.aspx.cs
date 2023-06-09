﻿using Microsoft.Reporting.WinForms;
using RealERPLIB;
using RealERPRDLC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB
{
    public partial class LetterDefault1 : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Page"] != null)
                {


                    string pageType = this.Request.QueryString["Page"].ToString().Trim();
                    if (pageType == "NewRec")
                    {
                        this.panl1.Visible = false;
                        this.dnone.Visible = false;
                        this.pnl2.Visible = true;

                        getLetter();

                    }
                    else
                    {
                        this.panl1.Visible = false;
                        this.dnone.Visible = false;
                        this.pnl2.Visible = false;
                        getCustLetter();

                    }
                }
                else
                {


                    // this.CommonButton();
                    var type = this.Request.QueryString["Entry"].ToString().Trim();
                    if (type == "Apprv" || type == "HR")
                    {
                        this.panl1.Visible = false;
                        this.btnsave.Visible = false;
                        this.btnapprv.Visible = false;
                        this.ShowLetter();
                    }


                    // this.txtml.Text = "<h1><span style=" + "text-decoration:" + "underline;" + "><strong>sabid hossain</strong></span></h1>";
                    string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    // this.txtfromdate.Text = "01" + date.Substring(2);
                    this.txttodate.Text = date;
                    // this.ShowView();


                    string type1 = this.Request.QueryString["Type"].ToString().Trim();
                    if (type1 == "10003" || type1 == "10024" || type1 == "10025" || type1 == "10026" || type1 == "10027" || type1 == "10028" || type1 == "10029" || type1 == "10030" || type1 == "10020" || type1 == "10002" || type1 == "10013" || type1 == "10021" || type1 == "10022" || type1 == "10023" || type1 == "10008" || type1 == "10007" || type1 == "10011")
                    {
                        this.GetSelected();
                        this.GetCompany();
                    }
                    else
                    {
                        this.GetEmployee();
                    }

                    string comcod = this.GetCompCode();
                    //  this.GetLettPattern();
                    string title = this.Request.QueryString["Type"].ToString().Trim();

                    switch (title)
                    {
                        case "10002":
                            ((Label)this.Master.FindControl("lblTitle")).Text = "Appoinment Letter";
                            break;
                        case "10003":
                            ((Label)this.Master.FindControl("lblTitle")).Text = "Offer Letter";
                            break;
                        case "10004":
                            ((Label)this.Master.FindControl("lblTitle")).Text = "Confirmation With Promotion Letter";
                            break;
                        case "10005":
                            ((Label)this.Master.FindControl("lblTitle")).Text = "Confirmation Without Increment Letter";
                            break;

                        case "10006":
                            ((Label)this.Master.FindControl("lblTitle")).Text = "Probation Extension Letter";
                            break;
                        case "10007":
                            if (comcod == "3370" || comcod == "3101")
                            {
                                ((Label)this.Master.FindControl("lblTitle")).Text = "Promotion  Letter";
                            }
                            else
                            {
                                ((Label)this.Master.FindControl("lblTitle")).Text = "Confirmation Without Increment Letter";
                            }

                            break;
                        case "10008":
                            ((Label)this.Master.FindControl("lblTitle")).Text = "Salary Certificate Letter";
                            break;
                        case "10025":
                            ((Label)this.Master.FindControl("lblTitle")).Text = "Confirmation Letter";
                            break;
                        default:
                            ((Label)this.Master.FindControl("lblTitle")).Text = "Name of Letter";
                            break;
                    }


                    // ddlEmployee_SelectedIndexChanged(null, null);

                    string Apprv = this.Request.QueryString["Entry"].ToString();
                    if (Apprv == "Apprv")
                    {

                        ((LinkButton)this.Master.FindControl("lnkPrint")).Visible = true;
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).Visible = true;

                    }
                    if (Apprv == "HR")
                    {
                        //((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
                        ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;

                    }
                    else
                    {
                        //((LinkButton)this.Master.FindControl("lnkPrint")).Visible = false;
                        //((DropDownList)this.Master.FindControl("DDPrintOpt")).Visible = false;
                    }
                    this.lbtnOk_Click(null, null);

                }
            }
        }


        private void getCustLetter()
        {
            string comcod = this.GetCompCode();
            string custid = this.Request.QueryString["custid"].ToString().Trim();
            string pactcode = this.Request.QueryString["pactcode"].ToString().Trim();

        
            DataSet ds = HRData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETCUSTOMERINFODETAILS", pactcode, custid, "", "", "", "", "", "", "");
            if (ds == null || ds.Tables[0].Rows.Count == 0)
               return;

            string date = Convert.ToDateTime(System.DateTime.Today).ToString("d MMMM, yyyy");
            string custname = ds.Tables[0].Rows[0]["custname"].ToString() ?? "";
            string custadd = ds.Tables[0].Rows[0]["custadd"].ToString() ?? "";
            string pactdesc = ds.Tables[0].Rows[0]["pactdesc"].ToString().Substring(4);
            string paddress = ds.Tables[0].Rows[0]["paddress"].ToString();
            string lbody = string.Empty;
            string letterType = this.Request.QueryString["Type"].ToString().Trim();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();

            switch (letterType)
            {
                //congratulation letter
                case "10003":
                    if (comcod == "3367" || comcod == "3101")
                    {
                        lbody =
                           "<p style='text-align: left;margin-top:50px;'> " +
                          "<span>"+ "<strong>" + date + "</strong>" + "<span/>" +
                          "<h1 style='text-align: center;'><strong ><u>Congratulation Letter</u></strong> </h1>" +
                           "<p style='margin-bottom:-11px;'>To</p>" +
                           "<p style='margin-bottom:-11px'>"+custname+"</p>" +
                            "<p style='margin-bottom:-11px'>"+custadd+"</p>" + "<br>"+
                            //"<p style='margin-bottom:-11px'>Chatteshwari Road,</p>" +
                            //"<p style='margin-bottom:-11px'>Chittagong.</p>" + "<br>"+

                            "<p>Dear Sir/Madam,</p>" + 
                            "<p><strong>Congratulations!</strong></p>" +
                            "<p>Thank you for your confidence placed in our company by making a booking in " + "<strong>" + pactdesc+ "</strong>"+ " at &nbsp;&nbsp;&nbsp; " +
                           "<strong>" + paddress + "</strong>" + " Hope it becomes a very special place where all your dreams grow." + "<br><br>" +
                            "It is our objective to give you full value for your money and provide you a home to your satisfaction" +
                            "in that our Sales and Marketing Department is committed to give you prompt and efficient services." + "<br><br>" +
                            "Please feel free to contact with me for ant queries about monthly installment, accounts statements," +
                            "loan purpose or any other service regarding financial matter.</p>" +
                            "<p><strong>Please remember that your on time payment will help us to complete the project on time. Your " +
                            "Next payment schedule is Tk..................... on ......................</strong></p>"+
                            "<p>I would like to thank you for patronizing Epic Properties Limited. </p>" + "<br>" +
                            "<p>Yours sincerly,</p>" + "<br>" +

                            "<p style='margin-bottom:-11px;'>..............................</p>" +
                            "<p style='margin-bottom:-11px;'>Tanjina Amin</p>" +
                            "<p style='margin-bottom:-11px;'>Office,CSD</p>" +
                            "<p style='margin-bottom:-11px;'>Cell # 01819836676</p>" +
                            "<p style='margin-bottom:-11px;'>Email: csd.epicpl@gmail.com</p>" + "<br>" +

                            "<p style='margin-bottom:-11px;'>Copy: 1. Office File</p>" +
                            "<p style='margin-bottom:-11px;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2.Email & WhatsApp</p>";
                    }
                    else
                    {
                        lbody = "<p style='text-align: center;'>&nbsp;</p><h3 style='text-align: center;'>" +
                            "<span style='text-decoration: underline;'><strong>Private &amp; Confidential</strong></span>" +
                            "</h3><p>&nbsp;<strong>Ref: SPL/HR/Prom/489/16&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </strong></p><p><strong>16 November, 2016</strong></p><p><strong>&nbsp;</strong><strong></p><p>&nbsp;<strong>Subject: Promotion</strong></p><p>&nbsp;Dear Mr. <strong></strong>,</p><p>&nbsp;We are pleased to inform you that, the company have decided to promote you to the position of <strong><u>Junior Territory Sales Manager</u></strong> recognition of your performance, effective December 1, 2016.</p><p>&nbsp;In view of the decision the breakdown of your revised monthly salary stands as follows:</p><p style='padding-left: 360px;'>Basic Salary &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;7,2000.00&nbsp; &nbsp; &nbsp;</p><p style='padding-left: 360px;'>House Rent Allowance &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;3,600.00</p><p style='padding-left: 360px;'>Transport Allowance &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 660.00</p><p style='padding-left: 360px;'>Medical Allowance &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 540.00</p><p style='padding-left: 360px;'><strong>Total: &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;TK &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;12,000.00</strong></p><p>&nbsp;</p><p>We acknowledge your excellent performance and congratulate you on your well-deserved promotion. We hope you will continue to contribute to the growth and success of the organization in future.</p><p>&nbsp;</p><p>Yours Sincerely,</p>";//<p>&nbsp;<strong>Moshiur Hossain</strong></p><p><strong>Managing Director.</strong></p><p><strong><u>Copy to:</u></strong></p><p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;HRIS</p><p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Personal File</p>";

                    }
                    break;
                //Registration letter
                case "10004":
                    if (comcod == "3367" || comcod == "3101")
                    {
                        lbody =
                           "<p style='text-align: left;margin-top:50px;'> " +
                          "<p>Date: " + date + "</strong>" + "<p/>" +
                           "<p style='margin-bottom:-11px;'>To</p>" +
                           "<p style='margin-bottom:-11px'>" + custname + "</p>" +
                            "<p style='margin-bottom:-11px'>" + custadd + "</p>" +

                           "<p>Subject:<strong> Registration of your apt. "+pactdesc+" at "+paddress+"</strong></p>" +
                            "<p>Dear Sir,</p>" +
                            "<p>Assalamu Alaikum,</p>" +
                            "<p>You will be glad to know that the registration procedure of the project " + "<strong>" + pactdesc + "</strong>" + " is going to " +
                            "be started." + "<br><br>" +
                            "Therefore, you are requested to provide your information for starting the registration"+
                            " procedure within &nbsp;------------------- If you have any kind of queries, you will contact Phone " +
                            "0312864801 or our Legal Ofiicer Advocate Fhim Ibne Rahman cell # 01844-558546.</p>" + "<br>"+

                            "<p style='margin-bottom:-11px;'>--------------------------</p>" +
                            "<p style='margin-bottom:-11px;'>Tanjina Amin</p>" +
                            "<p style='margin-bottom:-11px;'>Office,CSD</p>"+
                            "<p style='margin-bottom:-11px;'>Cell # 01819836676</p>"+
                            "<p style='margin-bottom:-11px;'>Email: csd.epicpl@gmail.com</p>"+ "<br><br>" +

                            "<p style='margin-bottom:-11px;'>Copy: 1. Office File</p>" +
                            "<p style='margin-bottom:-11px;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2.Legal Dept.</p>";
                    }
                    else
                    {
                        lbody = "<p style='text-align: center;'>&nbsp;</p><h3 style='text-align: center;'>" +
                            "<span style='text-decoration: underline;'><strong>Private &amp; Confidential</strong></span>" +
                            "</h3><p>&nbsp;<strong>Ref: SPL/HR/Prom/489/16&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </strong></p><p><strong>16 November, 2016</strong></p><p><strong>&nbsp;</strong><strong></p><p>&nbsp;<strong>Subject: Promotion</strong></p><p>&nbsp;Dear Mr. <strong></strong>,</p><p>&nbsp;We are pleased to inform you that, the company have decided to promote you to the position of <strong><u>Junior Territory Sales Manager</u></strong> recognition of your performance, effective December 1, 2016.</p><p>&nbsp;In view of the decision the breakdown of your revised monthly salary stands as follows:</p><p style='padding-left: 360px;'>Basic Salary &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;7,2000.00&nbsp; &nbsp; &nbsp;</p><p style='padding-left: 360px;'>House Rent Allowance &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;3,600.00</p><p style='padding-left: 360px;'>Transport Allowance &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 660.00</p><p style='padding-left: 360px;'>Medical Allowance &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 540.00</p><p style='padding-left: 360px;'><strong>Total: &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;TK &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;12,000.00</strong></p><p>&nbsp;</p><p>We acknowledge your excellent performance and congratulate you on your well-deserved promotion. We hope you will continue to contribute to the growth and success of the organization in future.</p><p>&nbsp;</p><p>Yours Sincerely,</p>";//<p>&nbsp;<strong>Moshiur Hossain</strong></p><p><strong>Managing Director.</strong></p><p><strong><u>Copy to:</u></strong></p><p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;HRIS</p><p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Personal File</p>";

                    }
                    break;
                case "10005":
                    if (comcod == "3367" || comcod == "3101")
                    {
                        lbody =
                           "<p style='text-align: left;margin-top:50px;'> " +
                          "<p>Date: " + date + "</strong>" + "<p/>" +
                           "<p style='margin-bottom:-11px;'>To</p>" +
                           "<p style='margin-bottom:-11px'>" + custname + "</p>" +
                            "<p style='margin-bottom:-11px'>" + custadd + "</p>" +
                            "<p><strong>Referrence: a)</strong></p>" +
                           "<p>Subject:<strong> Reminder to complete registration" + pactdesc + " of apt. at  " + paddress + "</strong></p>" +
                            "<p>Dear Sir,</p>" +
                            "<p>Assalamu Alaikum,</p>" +
                            "<p>Reference to the above subject matter you didn't take any initiative regarding registration your " +
                            "above mentioned apartment. Please note that you may be faced legal complicacies for not " +
                            "complying the mentioned date of Registration. If you have any kind of queries, you will contact " +
                            "Phone 0312864801 or our Legal Officer Advocate Fhim Ibne Rahman cell # 01844-558546" + "<br><br>" +
                            "So, you are requested to provide your information for starting registration procedure within  ------------------------" +
                           

                            "<p style='margin-bottom:-11px;'>--------------------------</p>" +
                            "<p style='margin-bottom:-11px;'>Tanjina Amin</p>" +
                            "<p style='margin-bottom:-11px;'>Office,CSD</p>" +
                            "<p style='margin-bottom:-11px;'>Cell # 01819836676</p>" +
                            "<p style='margin-bottom:-11px;'>Email: csd.epicpl@gmail.com</p>" + "<br><br>" +

                            "<p style='margin-bottom:-11px;'>Copy: 1. Office File</p>" +
                            "<p style='margin-bottom:-11px;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2.Legal Dept.</p>";
                    }
                    else
                    {
                        lbody = "<p style='text-align: center;'>&nbsp;</p><h3 style='text-align: center;'>" +
                            "<span style='text-decoration: underline;'><strong>Private &amp; Confidential</strong></span>" +
                            "</h3><p>&nbsp;<strong>Ref: SPL/HR/Prom/489/16&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </strong></p><p><strong>16 November, 2016</strong></p><p><strong>&nbsp;</strong><strong></p><p>&nbsp;<strong>Subject: Promotion</strong></p><p>&nbsp;Dear Mr. <strong></strong>,</p><p>&nbsp;We are pleased to inform you that, the company have decided to promote you to the position of <strong><u>Junior Territory Sales Manager</u></strong> recognition of your performance, effective December 1, 2016.</p><p>&nbsp;In view of the decision the breakdown of your revised monthly salary stands as follows:</p><p style='padding-left: 360px;'>Basic Salary &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;7,2000.00&nbsp; &nbsp; &nbsp;</p><p style='padding-left: 360px;'>House Rent Allowance &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;3,600.00</p><p style='padding-left: 360px;'>Transport Allowance &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 660.00</p><p style='padding-left: 360px;'>Medical Allowance &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 540.00</p><p style='padding-left: 360px;'><strong>Total: &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;TK &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;12,000.00</strong></p><p>&nbsp;</p><p>We acknowledge your excellent performance and congratulate you on your well-deserved promotion. We hope you will continue to contribute to the growth and success of the organization in future.</p><p>&nbsp;</p><p>Yours Sincerely,</p>";//<p>&nbsp;<strong>Moshiur Hossain</strong></p><p><strong>Managing Director.</strong></p><p><strong><u>Copy to:</u></strong></p><p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;HRIS</p><p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Personal File</p>";

                    }
                    break;

            }
            this.txtml.Text = lbody;

        }
        private void getLetter()
        {
            string comcod = this.GetCompCode();
            string advno = this.Request.QueryString["advno"].ToString().Trim();

            DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_NEW_REC", "GETRECEMP", advno, "", "", "", "", "", "");
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return;

            string gendercode = ds.Tables[0].Rows[0]["gendercode"].ToString() ?? "";
            string surname = "";
            if (gendercode == "99001")
            {
                surname = "Mr.";
            }
            else
            {
                surname = "Ms.";
            }
            string grdcde = ds.Tables[0].Rows[0]["gradecode"].ToString() == "" ? "0" : ds.Tables[0].Rows[0]["gradecode"].ToString();
            int gradecode = 0;
            gradecode = Convert.ToInt32(grdcde);
            string probduration = ds.Tables[0].Rows[0]["probation"].ToString();
            string termiduration = "";

            string adv = ds.Tables[0].Rows[0]["advno"].ToString() ?? "";
            string name = ds.Tables[0].Rows[0]["name"].ToString() ?? "";
            string desig = ds.Tables[0].Rows[0]["desig"].ToString();
            string dept = ds.Tables[0].Rows[0]["dept"].ToString() ?? "";
            string mobile = ds.Tables[0].Rows[0]["mobile"].ToString() ?? "";
            string email = ds.Tables[0].Rows[0]["email"].ToString() ?? "";
            string preadd = ds.Tables[0].Rows[0]["preadd"].ToString() ?? "";
            string peradd = ds.Tables[0].Rows[0]["peradd"].ToString() ?? "";
            string sec = ds.Tables[0].Rows[0]["sec"].ToString() ?? "";

            string bsal = ds.Tables[0].Rows[0]["bsal"].ToString() ?? "";
            string hrent = ds.Tables[0].Rows[0]["hrent"].ToString() ?? "";
            string cven = ds.Tables[0].Rows[0]["conven"].ToString() ?? "";
            string mallow = ds.Tables[0].Rows[0]["mallow"].ToString() ?? "";
            string total = ds.Tables[0].Rows[0]["total"].ToString() ?? "";
            string grade = ds.Tables[0].Rows[0]["grade"].ToString() ?? "";
            string doj = ds.Tables[0].Rows[0]["doj"].ToString() ?? "";
            string refno = ds.Tables[0].Rows[0]["refno"].ToString() ?? "";
            string cur_year = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy");
            string incmtax = "";
            string payablesal = "";
            string idcard = "";
            string inwords = ASTUtility.Trans(Convert.ToDouble(total), 2);
            double inwrd = 0;
            string temptable = "";
            int count = 1;

            if (gradecode > 3304004)
            {
                termiduration = "1 (One) months";
            }
            else
            {
                termiduration = "2 (Two) months";
            }
            //double netamt = Convert.ToDouble(dt.Rows[0]["netpay"]);
            //string Inword =  ASTUtility.Trans(netamt, 2);
            if (ds.Tables[1].Rows.Count != 0)
            {

                DataTable dtemplv = ds.Tables[1];

                if (dtemplv.Rows.Count > 0)
                {
                    foreach (DataRow drlv in dtemplv.Rows)
                    {

                        temptable = temptable + "<tr style='border-style:solid;border: 1px solid black;'><td style='border-style:solid;border: 1px solid black;text-align:center'>" + count + "</td><td style='border-style:solid;border: 1px solid black;'>" + drlv["lvname"].ToString() + "</td><td style='border-style:solid;border:1px solid black;text-align:center'>" + drlv["leave"].ToString() + " </td></tr>";
                        count++;
                    }
                }
            }
            string date = Convert.ToDateTime(System.DateTime.Today).ToString("dd-MMM-yyyy");

            string lbody = string.Empty;
            string letterType = this.Request.QueryString["Type"].ToString().Trim();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();

            switch (letterType)
            {
                //confirmation letter
                case "10025":

                    if (this.GetCompCode() == "3354" || this.GetCompCode() == "3101")
                    {
                        lbody =
                           "<p style='text-align:right;style='margin-bottom:-11px''> " + date + "</p>" +
                           "<p style='margin-bottom:-11px'><strong>Ref:" + refno + "</strong></p>" +
                           "<p style='margin-bottom:-11px'><strong>Mr " + name + "</strong></p>" +
                            "<p style='margin-bottom:-11px'>" + desig + "</p>" +
                            "<p style='margin-bottom:-11px'>" + dept + "</p>" +
                            "<p style='margin-bottom:-11px'>" + sec + "</p>" +
                            "<p>Subject:<strong>Confirmation Letter.</strong> </p>" +
                            "<p>Dear Mr " + name + "</p>" +
                            "<p><strong>Congratulations!</strong></p>" +
                            "<p>We would like to congratulate you on your successful completion of the probation period in our organization. We are glad to have received satisfactory reports from your superior regarding your performance during the said period. </p>" +
                            "<p>You have good product knowledge & inventory accuracy; are good at execution of tasks; able to maintain transparency in documentation; are punctual and disciplined at work; respond positively to other assigned tasks and we appreciate you for that. We are expecting an increase in work knowledge and an improvement in warehouse capacity management from you which will foster your performance in the future.</p>" +
                            "<p>The management wishes to confirm your employment with us as a " + desig + " of the " + dept + " department under the " + sec + " with an effective date from May 10, 2022. Your salary has been revised as well and your new salary is BDT " + total + ".     </p>" +
                            "<p>Now that you are going to be even an integral part of the organization, we would expect greater efforts from you to strive to do better at work for ultimately setting the organization on the growth path.  We have complete faith in you. </p>" +
                                  "<p></p>" +
                            "<p>Wishing you all the very best</p>" +
                             "<p></p>" +
                            "Regards," +
                            "<p></p>" +
                          "<p></p>" +
                              "<p></p>" +

                           "<p style='margin-bottom:-5px;display:inline;'><span style='border-top:1px solid; display:inline-block;margin-bottom:-11px;float-left'><strong>Md. Mizanur Rahman Khan</strong></span><span style='border-top:1px solid; display:inline-block;margin-bottom:-11px;float:right'><strong>S. M. Sahedul Karim Munna </strong></p></span></p>" +
                             "<br>" +
                           "<p style='margin-bottom:-5px;display:inline'><span style=' display:inline-block;margin-bottom:-11px;float-left'><strong>Senior Manager – HR</strong></span><span style='display:inline-block;margin-bottom:-11px;float:right;'><strong>Chief Operating Officer</strong></p></span></p>" +
                           "<p></p>" +
                           "<p style='display:inline-block;border-bottom:1px solid;margin-bottom:-11px;'>CC:</p>" +
                           "<p style='margin-left:10px;margin-bottom:-11px;'>1.Personal file</p>" +
                           "<p style='margin-left:10px;margin-bottom:-11px;'>2.Office file</p>";

                    }
                    else
                    {
                        lbody =
                                  "<p style='text-align:right;style='margin-bottom:-11px''> " + date + "</p>" +
                                  "<p style='margin-bottom:-11px'><strong>Ref:ERE/HR/CL/2022/027</strong></p>" +
                                  "<p style='margin-bottom:-11px'><strong>" + name + "</strong></p>" +
                                   "<p style='margin-bottom:-11px'>Employee ID : " + idcard + "</p>" +
                                   "<p style='margin-bottom:-11px'>" + desig + "</p>" +
                                   "<p style='margin-bottom:-11px'>" + dept + "</p>" +
                                   "<p style='margin-bottom:-11px'>" + sec + "</p>" +
                                   "<p>Subject:<strong>Confirmation Letter.</strong> </p>" +
                                   "<p>Dear " + name + "</p>" +
                                   "<p><strong>Congratulations!</strong></p>" +
                                   "<p>We would like to congratulate you on your successful completion of the probation period in our organization. We are glad to have received satisfactory reports from your superior regarding your performance during the said period. </p>" +
                                   "<p>You have good product knowledge & inventory accuracy; are good at execution of tasks; able to maintain transparency in documentation; are punctual and disciplined at work; respond positively to other assigned tasks and we appreciate you for that. We are expecting an increase in work knowledge and an improvement in warehouse capacity management from you which will foster your performance in the future.</p>" +
                                   "<p>The management wishes to confirm your employment with us as a " + desig + " of the " + dept + " department under the " + sec + " with an effective date from May 10, 2022. Your salary has been revised as well and your new salary is BDT 17,000 (Seventeen Thousand Only).     </p>" +
                                   "<p>Now that you are going to be even an integral part of the organization, we would expect greater efforts from you to strive to do better at work for ultimately setting the organization on the growth path.  We have complete faith in you. </p>" +
                                         "<p></p>" +
                                   "<p>Wishing you all the very best</p>" +
                                    "<p></p>" +
                                   "Regards," +
                                   "<p></p>" +
                                 "<p></p>" +
                                     "<p></p>" +

                                  "<p style='margin-bottom:-5px;display:inline;'><span style='border-top:1px solid; display:inline-block;margin-bottom:-11px;float-left'><strong>MR X</strong></span><span style='border-top:1px solid; display:inline-block;margin-bottom:-11px;float:right'><strong>MR X</strong></p></span></p>" +
                                    "<br>" +
                                  "<p style='margin-bottom:-5px;display:inline'><span style=' display:inline-block;margin-bottom:-11px;float-left'><strong>Senior Manager – HR</strong></span><span style='display:inline-block;margin-bottom:-11px;float:right;'><strong>Chief Operating Officer</strong></p></span></p>" +
                                  "<p></p>" +
                                  "<p style='display:inline-block;border-bottom:1px solid;margin-bottom:-11px;'>CC:</p>" +
                                  "<p style='margin-left:10px;margin-bottom:-11px;'>1.Personal file</p>" +
                                  "<p style='margin-left:10px;margin-bottom:-11px;'>2.Office file</p>";
                    }

                    break;

                //appoinment letter
                case "10002":
                    if (this.GetCompCode() == "3354")
                    {

                        lbody =
                        "<p style='text-align:right;style='margin-bottom:-11px''><strong>Ref:" + refno + "</strong></p>" +
                        "<p style='margin-bottom:-11px'><strong> " + surname + " " + name + "</strong></p>" +
                        "<p style='margin-bottom:-11px'><strong>Present Address:</strong> " + preadd + "</p>" +
                        "<p><strong>Subject:</strong> Appointment Letter - <strong>" + desig + "</strong></p>" +
                        "<p>Dear " + surname + " " + name + ",</p>" +
                        //body
                        "<p>We are delighted to appoint you as a  <strong>" + desig + "</strong> of the <strong>" + dept + "</strong> department under <strong>" + sec + "</strong> with our organization. If you join our organization, you will become a part of a fast-paced and dedicated team that works together to perform the highest possible level to achieve organizational goal.  </p>" +
                        "<p> As a member of our team, we would ask for your commitment to deliver outstanding quality of results.In addition, we expect your personal accountability in all the service, solutions, actions, communications, advice and results.In return, we are committed to providing you with every opportunity to learn, grow and stretch to the highest level of your ability and potentiality. </p>" +
                        "<p We are confident, you will f>We are confident, you will find this new opportunity both challenging and rewarding. The following points outline the terms and conditions we are proposing.</p>" +

                        //position info
                        //"<p style='margin-bottom:-11px'><strong>Designation</strong><span>: " + desig + "</span></p>" +
                        //"<p style='margin-bottom:-11px'><strong>Employee Grade</strong><span>: M4</span></p>" +
                        //"<p style='margin-bottom:-11px'><strong>Probable Job Start Date</strong><span>: " + doj + "</span></p>" +
                       "<table style='border:none'> <tr style='border:none'><td style='border:none'><strong>Designation</strong></td><td style='border:none'>: " + desig + "</td> </tr><tr style='border:none'><td style='border:none'> <strong>Employee Grade</strong></td><td style='border:none'>: " + grade + "</td></tr><tr style='border:none'><td style='border:none'> <strong>Probable Job Start Date</strong></td><td style='border:none'>: " + date + "</td></tr><tr style='border:none'><td style='border:none'> <strong>Salary Breakdown</strong></td><td style='border:none'>:</td></tr></table>" +


                        //salary break down table

                        "<table style='width:70%;margin-left:20px;border-style:solid; border: 1px solid black;margin-top:7px'><tr style='border-style:solid;border: 1px solid black;'><th style='width:50px;text-align:center;border-style:solid;border:1px solid black;'>SL</th><th>Particulars</th><th style='border-style:solid;border: 1px solid black;'>Amount in BDT</th></tr>" +
                        "<tr style='border-style:solid;border: 1px solid black;'><td style='text-align:center;border-style:solid;border: 1px solid black;'>1</td><td style='border-style:solid;border: 1px solid black;'>Basic (60% of Gross)</td><td style='text-align:center'>" + bsal + "</td></tr>" +
                        "<tr style='border-style:solid;border: 1px solid black;'><td style='text-align:center;border-style:solid;border: 1px solid black;'>2</td><td style='border-style:solid;border: 1px solid black;'>House Rent (30% of Gross)</td><td style='text-align:center;border-style:solid;border: 1px solid black;'>" + hrent + "</td></tr>" +
                        "<tr style='border-style:solid;border: 1px solid black;'><td style='text-align:center;border-style:solid;border: 1px solid black;'>3</td><td style='border-style:solid;border: 1px solid black;'>Medical Allowance (6% of Gross)</td><td style='text-align:center;border-style:solid;border: 1px solid black;'>" + mallow + "</td></tr>" +
                        "<tr style='border-style:solid;border: 1px solid black;'><td style='text-align:center;border-style:solid;border: 1px solid black;'>4</td><td style='border-style:solid;border: 1px solid black;'>Conveyance Allowance (4% of Gross)</td><td style='text-align:center'>" + cven + "</td></tr>" +
                        "<tr style='border-style:solid;border: 1px solid black;'><td style='text-align:center;border-style:solid;border: 1px solid black;' colspan='2'><strong>Total</strong></td><td style='text-align: center;border-style:solid;border:1px solid black;'><strong>" + total + "</strong></td></tr></table>" +
                         "<p style='margin-left:30px;'>In words : " + inwords + "</p>" +

                        "<p></p>" +
                        //"<p style='margin-bottom:-11px'><strong>Mobile Allowance</strong><span>: As per company policy.</span></p>" +
                        //"<p style='margin-bottom:-11px'><strong>Festival Bonus</strong><span>: You will be entitled for two festival bonuses yearly.</span></p>" +

                       "<table style='border:none'> <tr style='border:none'><td style='border:none'><strong>Mobile Allowance</strong></td><td style='border:none'>:  As per company policy.</td> </tr><tr style='border:none'><td style='border:none'> <strong>Festival Bonus</strong></td><td style='border:none'>: You will be entitled for two festival bonuses yearly.</td></tr><tr style='border:none'><td style='border:none'> <strong>Leave Allocation </strong></td><td style='border:none'>:</td></tr></table>" +


                        //leave allocation

                        "<table style='width:70%;margin-left:20px;border-style:solid;border:1px solid black;margin-top:7px'><tr style='border-style:solid;border:1px solid black;'><th style='width:50px;border-style:solid;border:1px solid black;'>SL</th><th style='border-style:solid;border:1px solid black;'>Types of Leave</th><th style='border-style:solid;border: 1px solid black;'>Total Leave in a Year</th></tr>" + temptable + "</table>" +

                             "<p></p>" +
                             "<p></p>" +

                        "<ul><li style='margin-top:100px;'>You will be able to avail earned leave after completion of 1 (one) year employment with Edison Real Estate Limited. Casual leave and sick leave shall be allocated as pro rata basis calculated from your date of joining. </li>" +
                        "<li style='margin-top:10px;'>During probation period, you will be able to avail maximum 2 (two) days leave in a month from your casual/sick leave. Any other absent will be counted as leave without pay.</li></ul>" +

                        //probation
                        "<p style='margin-bottom:-11px'><strong>Probation Period :</strong> Your employment is subject to a " + probduration + " probation period. After successful completion of the probation period, your job will be confirmed based on your satisfactory performance and necessary revision will be done accordingly.</p>" +
                        "<p>Following the initial probation period, a progression and performance review will be conducted on a quarterly basis to assess performance to-date and to clarify the arrangement, as the need may arise.</p>" +

                        //termination
                        "<p style='margin-bottom:-11px'><strong>Termination of Employeement:</strong> : During the probation period, the company can terminate this contract without any prior notice based on management decisions. After the completion of the probation period, any time company can terminate this contract with/without a notice period of " + termiduration + ". If an employee wants to leave the organization, must have to provide a notice to the company " + termiduration + " prior. For any type of violation of the company code of conduct, the employee might be terminated immediately.     </p>" +
                               "<p></p>" +
                            "<p></p>" +

                           "<div style='float:left;width:50%;'>" +
                            "<p style='border-top:1px solid; display:inline-block;margin:0;min-width:190px'><strong>Md. Mizanur Rahman Khan</strong></p>" +
                           "<p style='margin:0px;'><strong>Senior Manager – HR</strong></p>" +
                             "</div>" +

                            "<div style='float:left;width:50%;margin-bottom:40px;'>" +
                             "<p style='border-top:1px solid; display:inline-block;min-width:190px;margin:0px'><strong>Ahmed Pasha</strong></p>" +
                           "<p style='margin:0px;'><strong>Chief Business Officer</strong></p>" +
                             "</div>" +


                       //         "<p style='margin-bottom:-5px;display:inline;'><span style='border-top:1px solid; display:inline-block;margin-bottom:-11px;float-left;margin-right:200px;'><strong>Md. Mizanur Rahman Khan</strong></span>  <span style='border-top:1px solid; display:inline-block;margin-bottom:-11px;text-align:left;'><strong>Ahmed Pasha</strong></p></span></p>" +
                       //"<br>" +
                       //         "<p style='margin-bottom:-5px;display:inline'><span style=' display:inline-block;margin-bottom:-11px;float-left;margin-right:200px;'><strong>Senior Manager – HR</strong></span>                           <span style='display:inline-block;margin-bottom:-11px;margin-left:250px;text-align:left;'><strong>Chief Business Officer</strong></p></span></p>" +


                       "<div style='margin-top:45px'><p style='text-align:left'>I, <strong>" + name + "</strong>, confirm that I have read the terms of employment set out in this letter and I fully understood them and their implications and I now accept the offer of employment.</p></div>" +

                       "<p></p>" +
                       "<div><p style='border-top:1px solid; display:inline-block;margin-bottom:-11px;min-width:120px;'><strong>" + name + "</strong></p></div>";
                    }
                    else if (this.GetCompCode() == "3365")
                    {

                        lbody = "<div style='font-size:13px; font-family: TimesNewRoman, 'Times New Roman', Times, Baskerville, Georgia, serif'><p style='margin-bottom:-11px'>Ref: " + refno + "</p><p >" + System.DateTime.Now.ToString("dd MMM yyyy")
                            + "</p><p style='margin-bottom:-11px'> To </p><p style='margin-bottom:-11px'><strong>" + surname + " " + name + "</strong></p>" +
                            "<p style='margin-bottom:-11px'>Address: " + preadd + "</p>" +
                            "<p style='margin:5px 0'><br>Mobile : " + mobile + "</p><p></p><p><strong> <u> Appointment Letter" +
                            "</u></strong></p><p>Dear <strong> " + surname + " " + name + "," + "</strong></p>" +
                            "<p>Reference is made herewith to your application for the position of <strong>  “" + desig + ", " + sec + "” </strong>  and subsequent interview with us, the Management is pleased to give you a career" +
                            " opportunity with “" + comnam + "” as per the following terms and conditions.<br></p>" +
                            "<p><strong>1.</strong> Postion: <strong> " + desig + ", " + sec + "</strong></p>" +
                            "<p><strong>2.</strong> Job Location: <strong>" + comadd + "</strong></p>" +
                            "<p style='text-align: justify;'><strong>3.</strong> <strong>Date of Commencement: </strong>Your employment shall commence on <strong> " + doj + " </strong> and shall continue until separated/terminated as per the provision " +
                            "of company rules or resigned in accordance with terms of this letter of Appointment or rules of the company.<br> </p>" +
                            "<p style='text-align: justify;'><strong>4.</strong> <strong>Probation:</strong>" +
                            "<p style='text-align: justify;'>4.1  On appointment, you shall be on probation for a period of " + probduration + " from the date of joining. Upon satisfactory completion of probation, you will be confirmed in the regular service of the company according to the Management decision.</p> " +
                            "<p style='text-align: justify;'>4.2  During the probationary period, your service may be terminated at any time without assigning any reason whatsoever or issuing any notice or payment of salary in lieu thereof.  </p> " +
                            "<p style='text-align: justify;'>4.3  If no letter is issued for extension of probation period or for confirmation of service it will be deemed by default that probation has been extended and will continue indefinitely until the letter of confirmation is issued.</p> " +

                             "<p style='text-align: justify;'><strong>5.</strong> <strong>Compensation /Pay Scale: </strong> Gross Salary/Month: <strong> Taka " + bsal + "/- (In word: " + ASTUtility.Trans(Convert.ToDouble(bsal), 2) + ").</strong></p>" +
                             "<p style='text-align: justify;'><strong>6. </strong><strong>Salary Review: </strong> Salary will be reviewed after completion of 01 (one) year or at the discretion of Management. </p>" +
                             "<p style='text-align: justify;'><strong>7. </strong><strong>Festival Bonus: </strong>You are entitled to two festival bonuses in Eid-Ul-Fitr & Eid-Ul-Adha as per company policy. </p>" +
                             "<p style='text-align: justify;'><strong>8. </strong><strong>Income Taxes: </strong>All taxes on salaries and allowances shall be borne by you in accordance with laws of the land. Income Tax at source will be deducted monthly or by other manner as deemed fit by the management.</p>" +
                             "<p style='text-align: justify;'><strong>9. </strong><strong>Gratuity: </strong>Gratuity equivalent to one month’s basic pay for each completed year’s of service payable after three years of service. </p>" +
                             "<p style='text-align: justify;'><strong>10. </strong><strong >Obligation of Confidence: </strong>You may have access during the course of employment to or become acquainted with information, which may be designated by the company as confidential or reasonably be regarded as a trade secret. " +
                             "</p>" +
                             "<p style='text-align: justify;'>10.1  The confidential information may include (without limitation), any document or information marked as confidential, and any other information, which you may receive or develop in the course of your employment, which is not publicly available and relates to the business. E.g. operations, finance, legal affairs and other conditions of the company or its associated companies and other matters not readily available to persons not connected with the company or its associated companies either at all or without a significant expenditure of labor, skill and money. </p> " +
                             "<p style='text-align: justify;'>10.2  You shall agree, both during and after your employment, to maintain the confidentiality of this information and to take reasonable measures to prevent unauthorized disclosure or to use by any other person or entity. You shall also agree not to use, both during and after employment the confidential information for any purpose other than the benefit of the company as determined by the Management. Indulgence in such activity shall render you liable for termination with immediate effect notwithstanding any other terms mentioned in the appointment letter.  </p> " +

                            "<p style='text-align: justify;'><strong>11. Leave: </strong></p>" +

                            "<p style='text-align: justify;'>11.1  Existing company rules shall be applicable.  </p> " +
                            "<p style='text-align: justify;'>11.2  Leave is a facility and cannot be claimed as a right.  </p> " +
                            "<p style='text-align: justify;'>11.3  16 days Earned Leave for each completed year of service or pay in lieu thereof as per company policy. </p> " +
                            "<p style='text-align: justify;'>11.4  10 days Casual Leave and 14 days Sick Leave for every twelve months’ of service. </p> " +
                            "<p style='text-align: justify;'>11.5  Annual leave entitlement is calculated on period of service based on a calendar year. </p> " +
                            "<p style='text-align: justify;'>11.6  Approval of leave shall remain at the sole discretion of the company depending on the workloads and the business needs. </p> " +
                            "<p style='text-align: justify;'>11.7  In case of employment in middle of a calendar year, the calculation of leave will be on prorata basis.   </p> " +


                            "<p style='text-align: justify;'><strong>12. Disciplinary Action: </strong>Company service rules shall prevail. Company reserves the right to proceed with legal and other action in case of serious irregularities" +
                            "including financial and other related matters mentioned in company rules.</p>" +
                             "<p style='text-align: justify;'><strong>13. Transfer: </strong>Your service are at the disposal of the Management and are transferable to any Project, sister concerns or offices at any location and you may be entrusted with some other jobs as and when deemed fit by the management. </p>" +
                             "<p style='text-align: justify;'><strong>14. Separation from Services:</strong>" +

                             "<p style='text-align: justify;'>14.1  On confirmation, your services may be terminated with 30 (Thirty) days notice or pay in lieu thereof from either side.</p> " +
                             "<p style='text-align: justify;'>14.2  You are required to deal with the Company's money, material and documents with utmost honesty and professional ethics. If you are found guilty at any point of time of moral turpitude or misappropriation regardless of the value involved, your services would be terminated with immediate" +
                             "effect notwithstanding other terms and conditions mentioned in this letter. </p> " +
                             "<p style='text-align: justify;'>14.3  You have been engaged on the presumption that the particulars furnished by you in your application and/ or bio-data are correct. In case the said particulars are found to be incorrect or that you have concealed or withheld some other relevant facts, " +
                             "your appointment with the company shall stand terminated/ canceled without any notice. </p> " +
                              "<p style='text-align: justify;'>14.4 For other reasons and systems of separation e.g.Dismissal, termination, discharge etc. company policy shall prevail.</p> " +


                             "<p><strong>15. Work Conditions: </strong>" +
                             "<p>15.1  You are employed as <strong> “" + desig + ", " + sec + "” </strong> and vested with such powers to enable you to function and perform your duties.</p> " +
                             "<p style='text-align: justify;'>15.2  You are required to perform your function strictly in accordance with the instructions of your superiors and according to working program provided to you by the Company.  </p> " +
                             "<p style='text-align: justify;'>15.3  You will not engage yourself in any other employment, occupation, or business of any nature for remuneration or profit. </p> " +
                             "<p style='text-align: justify;'>15.4  Your services will be governed by the Rules of the Company in force from time to time. </p> " +

                            "<p style='text-align: justify;'><strong>16. Required Documents: </strong> You are requested to submit the following documents within 07 (seven) days from the date of receipt of this appointment letter: " +

                             "<p style='text-align: justify;'>16.1  Original & photocopies of all academic certificates.  </p> " +
                             "<p style='text-align: justify;'>16.2  Original & photocopies of Letter of acceptance of resignation in the Company Letter Head from the previous employer.  </p> " +
                             "<p style='text-align: justify;'>16.3  Original & photocopies of all Experience Certificate(s). </p> " +
                             "<p style='text-align: justify;'>16.4  4 copies Passport size photograph  </p> " +
                             "<p style='text-align: justify;'>16.5  2 copies Passport size photograph for Employee Nominee. </p> " +
                             "<p style='text-align: justify;'>16.6  Photocopy of passport / any other photo ID / photocopy of National ID Card. </p> " +
                             "<p style='text-align: justify;'>16.7  You are to submit us your clearance letter, which has been duly issued by your previous employer at the time of joining. </p> " +

                             "<p style='text-align: justify;'><strong>17. Acceptance: </strong> Please signify your acceptance of the above terms and conditions of your employment in the company by signing the duplicate copy of this letter of appointment as a token of your acceptance. </p>" +
                             "<p style='text-align: justify;'><strong>18. Miscellaneous: </strong>Other terms and conditions of service will be in accordance with the company rules and regulations, which may be altered by the company from time to time.</p>" +


                            "<p>&nbsp;For <strong>" + comnam + " </strong>," +
                            "</p><p>&nbsp;</p><p class='pImage'><strong></p>  <p class='pUname'><span style='border-top:1px solid black'><strong>" + "Brig Gen Mohammad Ayub Ansary, psc (Retd)" + "</span></strong></p> <p>" + "Additional Managing Director and Head of HR" + "</p><p>" + "Admin and Security Department" + "</p>" +
                            "<p style='text-align:center'><strong><u>ACCEPTANCE</u></strong></p>" +
                            "<p style='text-align: justify;'>I, <strong> " + name + " </strong>  have read and fully understood the terms and conditions set out in the Letter of Appointment dated <strong>" + System.DateTime.Now.ToString("dd MMM yyyy") +
                            "</strong> in particular, I have read and  fully understood clauses of the Letter of Appointment. I do hear by confirm my acceptance of the terms and conditions in the aforesaid document and agree to be bound by the terms accordingly. </p>" +

                            "<p>Signature:-------------------------------------Date:------------------ </p>" +
                            "</div>";

                    }
                    else if (this.GetCompCode() == "3369" || this.GetCompCode() == "3101")
                    {
                        lbody =
                       "<p style='text-align:right;style='margin-bottom:-11px''><strong>ID:" + idcard + "</strong></p>" +
                        "<p>" + date + "</p>" +
                       "<p style='margin-bottom:-11px'><strong> " + surname + " " + name + "</strong></p>" +
                       "<p style='margin-bottom:-11px'><strong>Present Address:</strong> " + preadd + "</p>" +
                       "<p style='margin-bottom:-11px'><strong>Mobile: </strong>" + mobile + "</p>" +
                       "<p style='margin-bottom:-11px'><strong>Email:</strong> " + email + "</p>" +
                       "<p><strong>Appointment letter for the role of Production Executive.</strong></p>" +
                       "<p>Dear " + surname + " " + name + ",</p>" +
                       //body
                       "<p>With reference to your CV, subsequent interviews, follow-up discussions the management of Acme AI Ltd. is pleased to appoint you as " + desig + " with effect from " + date + " under the following terms and conditions: </p>" +
                        "<p></p>" +
                        "<p>Terms and conditions:</p>" +
                          "<ol>" +
                              "<li style='margin-top:10px!important'>Duration of Contract: Your contract period will be effective from 28/09/2022 for the duration of the project.</li>" +
                              "<li style='margin-top:10px!important'>Employment Status: Your appointment will be treated as a full-time project staff when you complete your probation period successfully.</li>" +
                              "<li style='margin-top:10px!important'>Work Station: Your initial workstation will be at Acme AI Ltd. office at DOHS Mirpur. To further Acme AI Ltd. interest, you may be required to work remotely if the situation demand it.</li>" +
                              "<li style='margin-top:10px!important'>Reporting Supervisor: You will report to your team/project lead for the time being subject to the supervisory changes in the future.</li>" +
                              "<li style='margin-top:10px!important'>Salary and Allowances: The breakdown of your monthly salary will be as below: </li>" +
                     //salary break down table
                     "<table style='width:70%;margin-left:20px;border:0'>" +

                       "<tr style='border:0;'><td style='border:0;>Basic (30% of Gross)</td><td style='text-align:center;border:0;'>:" + bsal + "</td></tr>" +
                       "<tr style='border:0;'><td style='border:0;>House Allowance (30% of Gross)</td><td style='text-align:center;border:0;'>:" + hrent + "</td></tr>" +
                       "<tr style='border:0;'><td style='border:0;>Medical Allowance (20% of Gross)</td><td style='text-align:center;border:0;'>:" + mallow + "</td></tr>" +
                       "<tr style='border:0;'><td style='border:0;>Transport Allowance (20% of Gross)</td><td style='text-align:center;border:0;'>:" + cven + "</td></tr>" +
                       "<tr style='border-top:1px solid!important;'><td style='text-align:center;'><strong>Total Gross Salary</strong></td><td style='text-align: center;'><strong>:" + total + "</strong></td></tr></table>" +

                              "<li style='margin-top:10px!important'>Probation: You will be on probation for a period of 06 (six) months from the date of appointment. At the end of your probation, if your services are found to be satisfactory, your appointment may be confirmed in writing as a full-time project staff. Unsatisfactory progress during probation may result in an extension of the probation period by three months or termination from the service at the discretion of the company.</li>" +
                              "<li style='margin-top:10px!important'>Working Days and Hours Your official working days are 05 days a week starting from Monday to Friday. Office hours start at 10 AM and ends at 7 PM. with 40 minutes lunch break from 2 PM to 2.40 PM and 20 minutes Coffee Break from 11.50 to 12 AM and from 5 PM to 5.10 PM. For the requirement of the office you may have to work on a shift basis (morning shift 8:00 am – 2:00 pm and day shift 2:30 pm – 8:30 pm without any lunch break) on a requirement basis</li>" +
                              "<li style='margin-top:10px!important'>Caution: You are not permitted to engage in other concurrent employment during your contract period with Acme AI Ltd. Any breach of this provision and breach of the NDA provision will be grounds for instant dismissal.</li>" +
                              "<li style='margin-top:10px!important'>Duties: Your duties will be as per the job description. </li>" +
                              "<li style='margin-top:10px!important'> Leave Entitlement: For leave eligibility, refer to section 24.2 Salient Points of ACME Technologies HR Manual (2015) and its amendments from time to time. During the probationary period there is no paid leave except emergency sick leave and government holidays. After the probationary period the total allotted paid leaves per calendar year is 34 days (excluding industrial government holidays) for permanent staff. Specific breakdown of leaves is as followed:</li>" +
                       "<table style='width:70%;margin-left:20px;border-style:solid; border: 1px solid black;margin-top:7px'>" +

                       "<tr style='border-style:solid;border: 1px solid black;'>" +
                            "<td style='text-align:center;border-style:solid;border: 1px solid black'>Annual Leave</td>" +
                            "<td style='text-align:center;border-style:solid;border: 1px solid black'>10 days</td>" +
                            "<td style='text-align:center;border-style:solid;border: 1px solid black'>Will be entitled after completion of one-year services. Carried forward of annual leave is discouraged. Annual leaves can be carried forward to the forthcoming years for a maximum of 20 days. Annual Leave can also be en-cashed based on staff preferences. Employees have to book leaves prior to 2 weeks for approval.</td>" +
                       "</tr>" +

                        "<tr style='border-style:solid;border: 1px solid black;'>" +
                            "<td style='text-align:center;border-style:solid;border: 1px solid black'>Casual Leave</td>" +
                            "<td style='text-align:center;border-style:solid;border: 1px solid black'>10 days</td>" +
                            "<td style='text-align:center;border-style:solid;border: 1px solid black'>Casual leaves can be taken in a calendar year. Any due casual leaves will be forfeit upon the beginning of the next fiscal year cycle. Employees have to book leaves prior to 2 weeks for approval.</td>" +
                       "</tr>" +

                      "<tr style='border-style:solid;border: 1px solid black;'>" +
                            "<td style='text-align:center;border-style:solid;border: 1px solid black'>Sick Leave</td>" +
                            "<td style='text-align:center;border-style:solid;border: 1px solid black'>14 days</td>" +
                            "<td style='text-align:center;border-style:solid;border: 1px solid black'>Entitled sick leaves for employees can be taken at any time subject to providing evidence of sickness e.g., diagnosis and prescription.</td>" +
                       "</tr>" +

                       "</table>" +
                               "<li style='margin-top:10px!important'>Unauthorized Leave: If you remain absent beyond the period of leave granted or extended, you shall be liable for dismissal, unless you return within three days of expiry of leave and give a satisfactory explanation of your failure to return. Notice of dismissal may be served as set out hereunder. Alternatively, at management’s discretion, you may be suspended without salary for up to 07 days as a measure of necessary action.</li>" +
                              "<li style='margin-top:10px!important'>Medical Examination: Every person before her/his appointment to the company require to submit medical fitness certificate after completing general blood and urine test and general medical examination from a certified physician.</li>" +
                              "<li style='margin-top:10px!important'>. Income Tax: Staff members who qualify for income tax payments the company will deduct at source as required by law.</li>" +
                              "<li style='margin-top:10px!important'>Discharge: You may be discharged from service on 01 (one) months’ notice for reasons of physical or mental incapacity or continued ill health not amounting to misconduct.</li>" +
                              "<li style='margin-top:10px!important'>Dismissal: You may be dismissed with immediate effect if: a) You are convicted of any offence or moral turpitude b) The company finds you guilty of misconduct.</li>" +
                              "<li style='margin-top:10px!important'>Termination: During the probation period the Company may terminate your services on 14 days’ written notice, without any reason for termination being assigned. The company at any time may discharge or terminate the service of an employee by giving 01(one) months’ notice or one month’s basic pay in lieu thereof, without any reason for termination being assigned</li>" +
                              "<li style='margin-top:10px!important'>Resignation: During probation and at any time of your service period, you will be allowed to leave the job from Acme AI Ltd. with 01 (one) month advance notice or pay in lieu of.</li>" +
                              "<li style='margin-top:10px!important'>estival Bonus: As per ACME Technology HR Policy as amended from time to time</li>" +
                              "<li style='margin-top:10px!important'>Annual Increment: After confirmation as regular project staff, you will get an annual increment in a standard service year depending upon your performance evaluation as per ACME Technologies HR policy. The standard service year is from July to June.</li>" +
                              "<li style='margin-top:10px!important'>. Notice: Any notice of dismissal, discharge, or other termination may be given by serving a copy of the notice personally on the employee or by posting to the employee’s last known address and by mailing a copy to the email address.</li>" +
                              "<li style='margin-top:10px!important'>Rules and Regulations: In addition to your Job responsibilities, upon agreeing to this contract, we understand that you are abiding by the rules, conduct, and policies stated in your appointment letter, offer letter, the non-disclosure agreement you signed, and conditions mentioned in the HR manual of ACME Technologies Ltd. and all of its future amendments for all other employment condition.</li>" +

                          "</ol>" +
                          "<p></p>" +
                          "<p></p>" +
                       "<p>If you agree to accept this employment on the terms and conditions noted above, please sign the duplicate of this letter in the section below and return it to us. We welcome you to Acme AI Ltd. and sincerely hope that your career with us will be prosperous and rewarding.</p>" +

                            "<p></p>" +
                          "<p></p>" +


                        "<p>Sincerely,</p>" +
                        "<p></p>" +
                        "<p></p>" +
                        "<p><strong>Shafiqul Islam</strong></p>" +
                        "<p><strong>Managing director, Acme AI Ltd</strong></p>" +

                        "<p></p>" +
                        "<p></p>" +
                        "<p>TO BE FILLED BY THE APPOINTEE </p>" +
                        "<p>I <span style='width:200px;border-bottom:1px solid;display:inline-block'></span>confirm that I accept the employment with the terms and conditions specified above.</p>" +
                        "<p></p>" +
                        "<p></p>" +
                        "<p style='margin-top:'>Signature & Date</p>";



                    }
                    break;

                //offer letter
                case "10003":
                    if (this.GetCompCode() == "3354")
                    {
                        lbody =
                       "<p style='text-align:right;style='margin-bottom:-11px''> " + date + "</p>" +
                       "<p style='margin-bottom:-11px'><strong>" + surname + " " + name + "</strong></p>" +
                       "<p style='margin-bottom:-11px'><strong>Present Address:</strong> " + preadd + "</p>" +
                       "<p><strong>Subject:</strong> Offer Letter-<strong>" + desig + "</strong></p>" +
                       "<p>Dear " + surname + " " + name + ",</p>" +

                       "<p style='margin-bottom:-11px'>We are delighted to offer you the position of <strong>" + desig + "</strong> of the <strong>" + sec + "</strong> department under <strong>" + sec + "</strong> with our organization. If you join our organization, you will become a part of a fast-paced and dedicated team that works together to perform the highest possible level to achieve organizational goal.  </p>" +
                       "<p style='margin-bottom:-11px'> As a member of our team, we would ask for your commitment to deliver outstanding quality of results.In addition, we expect your personal accountability in all the service, solutions, actions, communications, advice and results.In return, we are committed to providing you with every opportunity to learn, grow and stretch to the highest level of your ability and potentiality. </p>" +
                       "<p We are confident, you will f>We are confident, you will find this new opportunity both challenging and rewarding. The following points outline the terms and conditions we are proposing.</p>" +
                       "<table style='border:none'> <tr style='border:none'><td style='border:none'><strong>Designation</strong></td><td style='border:none'>: " + desig + "</td> </tr><tr style='border:none'><td style='border:none'> <strong>Employee Grade</strong></td><td style='border:none'>: " + grade + "</td></tr><tr style='border:none'><td style='border:none'> <strong>Probable Job Start Date </strong></td><td style='border:none'>: " + date + "</td></tr><tr style='border:none'><td style='border:none'> <strong>Salary</strong> </td><td style='border:none'>: As negotiated and agreed upon by both parties</td></tr><tr style='border:none'><td style='border:none'><strong>Mobile Allowance</strong></td><td style='border:none'>: As per company policy</td></tr></table>" +
                       //"<p style='margin-bottom:-11px'><strong>Designation</strong><span>: " + desig + "</span></p>" +
                       //"<p style='margin-bottom:-11px'><strong>Employee Grade</strong><span>: "+grade+"</span></p>" +
                       //"<p style='margin-bottom:-11px'><strong>Probable Job Start Date</strong><span>: " + date + "</span></p>" +
                       //"<p style='margin-bottom:-11px'><strong>Salary</strong><span>: As negotiated and agreed upon by both parties</span></p>" +
                       //"<p><strong>Mobile Allowance</strong><span>: As per company policy</span></p>" +

                       "<p style='margin-bottom:-11px'><strong>Probation Period :</strong> Your employment is subject to a " + probduration + " probation period. After successful completion of the probation period, your job will be confirmed based on your satisfactory performance and necessary revision will be done accordingly.</p>" +
                       "<p>Following the initial probation period, a progression and performance review will be conducted on a quarterly basis to assess performance to-date and to clarify the arrangement, as the need may arise.</p>" +
                       "<p style='margin-bottom:-11px'><strong>Termination of Employment:</strong> : During the probation period, the company can terminate this contract without any prior notice based on management decisions. After the completion of the probation period, any time company can terminate this contract with/without a notice period of " + termiduration + ". If an employee wants to leave the organization, must have to provide a notice to the company " + termiduration + " prior. For any type of violation of the company code of conduct, the employee might be terminated immediately.     </p>" +
                       "<p style='margin-bottom:-11px'>We look forward to the opportunity to work with you in an atmosphere that is successful and mutually challenging and rewarding.If you don’t accept this letter, please let us know within " + date + ".</p>" +

                           "<p></p>" +
                            "<p></p>" +

                            "<div style='float:left;width:50%;'>" +
                            "<p style='border-top:1px solid; display:inline-block;margin:0;min-width:190px'><strong>Md. Mizanur Rahman Khan</strong></p>" +
                           "<p style='margin:0px;'><strong>Senior Manager – HR</strong></p>" +
                             "</div>" +

                            "<div style='float:left;width:50%;margin-bottom:40px;'>" +
                             "<p style='border-top:1px solid; display:inline-block;min-width:190px;margin:0px'><strong>Ahmed Pasha</strong></p>" +
                           "<p style='margin:0px;'><strong>Chief Business Officer</strong></p>" +
                             "</div>" +


                       //         "<p style='margin-bottom:-5px;display:inline;'><span style='border-top:1px solid; display:inline-block;margin-bottom:-11px;float-left;margin-right:200px;'><strong>Md. Mizanur Rahman Khan</strong></span>  <span style='border-top:1px solid; display:inline-block;margin-bottom:-11px;text-align:left;'><strong>Ahmed Pasha</strong></p></span></p>" +
                       //"<br>" +
                       //         "<p style='margin-bottom:-5px;display:inline'><span style=' display:inline-block;margin-bottom:-11px;float-left;margin-right:200px;'><strong>Senior Manager – HR</strong></span>                           <span style='display:inline-block;margin-bottom:-11px;margin-left:250px;text-align:left;'><strong>Chief Business Officer</strong></p></span></p>" +


                       "<div style='margin-top:45px'><p style='text-align:left'>I, <strong>" + name + "</strong>, confirm that I have read the terms of employment set out in this letter and I fully understood them and their implications and I now accept the offer of employment.</p></div>" +


                       "<div><p style='border-top:1px solid; display:inline-block;margin-bottom:-11px;min-width:120px;'><strong>" + name + "</strong></p></div>";

                    }
                    else if (this.GetCompCode() == "3365")
                    {
                        lbody = "<p style='margin-bottom:0'><br/><br/>Ref: " + refno + "</p>" + date + "<p style='margin-bottom:-11px'>To</p><p style='margin-bottom:-11px'><strong>Mr " + name + "</strong></p>" +
       "<p style='margin-bottom:-11px'>Address: " + peradd + " </p>" +
       "<p style='margin-bottom:0'><br>Mobile : " + mobile + "</p><p><strong>Subject: Offer for Employment</strong></p><p><br>Dear <strong>Mr " + name + "," + "</strong></p>" +
       "<p>With reference to discussions with you and your willingness to join our company, we are pleased to offer you appointment as <strong> " + desig + ", " + sec + ",</strong> in " + dept + ", which shall commence on or before <strong> " + doj + " </strong>." +
       "<p>Before that you are requested to submit a copy of your resignation letter, which has been duly received by your present employer within 3 (Three) days from the date of receipt of this offer letter and a clearance letter at the time of joining.</p></p><p>&nbsp;The Letter of Appointment will be issued soon.</p><p>&nbsp; Please bring the following papers on the date of joining:</p><ol>" +
       "<li style='margin-top:5px'>Release letter / Letter of acceptance of resignation in the Company Letter Head from the previous employer.</li><li style='margin-top:5px'>Original & photocopies of all certificates (experience, academic, professional courses etc).</li><li style='margin-top:5px'>Photocopy of National ID card/ Passport (employee and nominee).</li><li style='margin-top:5px'>Passport size photograph (employee 7 copies and Nominee 3 copies)." +
       "</li><li style='margin-top:5px'>Pay Slip/ Proof of Salary and ETIN.</li></ol><p>&nbsp;</p><p>Yours Sincerely,</p><p class='pImage'><strong></p>  <p class='pUname'><span style='border-top:1px solid black'><strong>" + "Brig Gen Mohammad Ayub Ansary, psc (Retd)" + "</span></strong></p> <p>" + "Additional Managing Director" + "</p><p>" + "Head of HR,Admin,Security and Fire Safety Department" + "</p>";

                    }
                    else if (this.GetCompCode() == "3369" || this.GetCompCode() == "3101")
                    {
                        lbody =
                              "<p></p>" +
                       "<p></p>" +
                       "<p></p>" +
                       "<p></p>" +
                       "<p></p>" +
                         "<p></p>" +
                       "<p style='margin-bottom:-11px''><strong>" + date + "</strong></p>" +
                       "<p style='margin-bottom:-11px'> " + surname + " " + name + "</p>" +
                       "<p style='margin-bottom:-11px'>" + preadd + "</p>" +
                       "<p style='margin-bottom:-11px'>Mobile: " + mobile + "</p>" +
                       "<p style='margin-bottom:-11px'>Email: " + email + "</p>" +

                       "<p><strong> Offer letter for the position of ' " + desig + " ' </strong></p>" +
                       "<p>Dear " + surname + " " + name + ",</p>" +
                       //body
                       "<p style='margin-bottom:-11px'> Further to our interviews and follow-up discussions, the management of Acme AI is pleased to offer you an appointment in the Company as <strong>" + desig + "</strong> on the following terms and conditions:" +

                 "<ol>" +
                    "<li style='margin-top:10px;'>Your employment with the Company will commence on 15 September 2022.</li>" +
                    "<li style='margin-top:10px;'>Duty station is in Acme AI Ltd.’s Dhaka office but you may be asked to work remotely if the situation demands it. </li>" +
                    "<li style='margin-top:10px;'>You will report directly to the Team Leader/Project Leader also with Floor Manager forthe time being subject to supervisory changes in the future</li>" +
                    "<li style='margin-top:10px;'> You will be on probation for 6 months from the date of your appointment. On satisfactorycompletion of the period, you will be confirmed as a permanent employee</li>" +
                    "<li style='margin-top:10px;'>Leaving the job without one month’s prior notice can cause a deduction for the salary of One month.</li>" +
                    "<li style='margin-top:10px;'>After confirmation of your role at the end of probation, you may be confirmed as a full-timeproject employee subject to the availability of vacancy, your performance and your workethic </li>" +
                    "<li style='margin-top:10px;'>Unsatisfactory progress during probation may result in the extension of the probationperiodor termination from the service, at the discretion of the Company. During the periodof probation, your service may be terminated with 14 days notice without assigning anyreasons for termination.</li>" +
                    "</ol>" +

                    "<p></p>" +
                    "<p></p>" +
                    "<p></p>" +
                    "<p style='margin-bottom:100px'>Effective from the date of your joining, the following salary and allowances will be paid to you in arrears:</p>" +

                       "<p></p>" +
                       "<p></p>" +
                       "<p></p>" +
                       "<p></p>" +
                       "<p></p>" +
                       "<p></p>" +
                       "<p></p>" +


                       //salary break down table

                       "<table style='width:70%;margin-left:20px;border-style:solid; border: 1px solid black;margin-top:7px'>" +
                       "<tr style='border-style:solid;border: 1px solid black;'>" +
                         "<th style='width:50px;text-align:center;border-style:solid;border:1px solid black;'>SL</th>" +
                         "<th style='width:200px;text-align:center;border-style:solid;border:1px solid black;'><strong>Position/Designation </strong></th>" +
                         "<th style='border-style:solid;border: 1px solid black;width:100px;'>Total</th>" +
                         "<th style='border-style:solid;border: 1px solid black;width:100px;'>Basic 30 % of Gross</th>" +
                         "<th style='border-style:solid;border: 1px solid black;width:100px;'>H/Rt 30% of Gross</th>" +
                         "<th style='border-style:solid;border: 1px solid black;width:100px;'>Med 20% of Gross</th>" +
                         "<th style='border-style:solid;border: 1px solid black;width:100px;'>Tpt 20% of Gross</th>" +

                       "</tr>" +
                       "<tr style='border-style:solid;border: 1px solid black;'>" +
                            "<td style='text-align:center;border-style:solid;border: 1px solid black'>1</td>" +
                            "<td style='text-align:center;border-style:solid;border: 1px solid black'>" + desig + "</td>" +
                            "<td style='text-align:center;border-style:solid;border: 1px solid black'>" + total + "</td>" +
                            "<td style='text-align:center;border-style:solid;border: 1px solid black'>" + bsal + "</td>" +
                            "<td style='text-align:center;border-style:solid;border: 1px solid black'>" + hrent + "</td>" +
                            "<td style='text-align:center;border-style:solid;border: 1px solid black'>" + mallow + "</td>" +
                            "<td style='text-align:center;border-style:solid;border: 1px solid black'>" + cven + "</td></tr>" +

                       "</table>" +

                       "<p></p>" +

                       "<p>You will be entitled to festival bonuses as per the Acme Technology HR manual and Policies.</p>" +
                       "<p></p>" +

                       "<p>If you agree to accept this employment on the terms and conditions noted above, please sign the duplicate of this letter below and return it to us. We would welcome you to the company and sincerely hope that your career with the Company will be prosperous and rewarding.</p>" +


                        "<p></p>" +
                        "<p></p>" +
                        "<p></p>" +
                       "<p style='margin-bottom:-11px'>Best Regards,</p>" +
                      "<div><p style='margin-bottom:-11px'><strong>Shafiqul Islam</strong></p></div>" +
                       "<div><p style='margin-bottom:-11px'><strong>Managing director, Acme AI Ltd.</strong></p></div>";

                    }
                    else
                    {
                        lbody = "<p style='margin-bottom:0'><br/><br/>Ref: HR/2021/</p>" + date + "<p style='margin-bottom:-11px'>To</p><p style='margin-bottom:-11px'><strong>" + name + "</strong></p>" +
"<p style='margin-bottom:-11px'>Address: House: 271, Tejgaon I/A, </p>" +
"<p style='margin-bottom:-11px'>Tejgaon, Dhaka-1208</p><p style='margin-bottom:0'><br>Mobile : 01xxxxxxxxxx</p><p><strong>Subject: Offer for Employment</strong></p><p><br>Dear <strong>" + name + "," + "</strong></p>" +
"<p>With reference to discussions with you and your willingness to join our company, we are pleased to offer you appointment as <strong> " + desig + ", " + dept + ",</strong> in " + sec + ", which shall commence on or before <strong> " + date + " </strong>." +
"<p>Before that you are requested to submit a copy of your resignation letter, which has been duly received by your present employer within 3 (Three) days from the date of receipt of this offer letter and a clearance letter at the time of joining.</p></p><p>&nbsp;The Letter of Appointment will be issued soon.</p><p>&nbsp; Please bring the following papers on the date of joining:</p><ol>" +
"<li>Release letter / Letter of acceptance of resignation in the Company Letter Head from the previous employer.</li><li>Original & photocopies of all certificates (experience, academic, professional courses etc).</li><li>Photocopy of National ID card/ Passport (employee and nominee).</li><li>Passport size photograph (employee 7 copies and Nominee 3 copies)." +
"</li><li>Pay Slip/ Proof of Salary and ETIN.</li></ol><p>&nbsp;</p><p>Yours Sincerely,</p><p class='pImage'><strong></p>  <p class='pUname'><span style='border-top:1px solid black'><strong>" + "Mr x" + "</span></strong></p> <p>" + "Additional Managing Director and Head of HR" + "</p><p>" + "Admin and Security Department" + "</p>";

                    }
                    break;


            }
            this.txtml.Text = lbody;

        }
        private void ShowLetter()
        {

            string type = this.Request.QueryString["Type"].ToString().Trim();
            string comcod = this.GetCompCode();
            string empid = this.Request.QueryString["ID"].ToString().Trim();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLETTER", empid, type, "", "", "", "", "", "", "");
            if (ds3.Tables[0].Rows.Count == 0)
                return;
            string lett = (string)ds3.Tables[0].Rows[0]["LETTDESC"];
            this.txtml.Text = lett;

            ViewState["letter"] = ds3.Tables[0];
        }
        private void GetEmployee()
        {
            string comcod = this.GetCompCode();
            string txtSProject = "%%";
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_LEAVESTATUS", "GETEMPNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlEmployee.DataTextField = "empname";
            this.ddlEmployee.DataValueField = "empid";
            this.ddlEmployee.DataSource = ds3.Tables[0];
            this.ddlEmployee.DataBind();
            ds3.Dispose();
            ViewState["empinfo"] = ds3;

            //this.GetCompany();

        }

        private void CommonButton()
        {
            ((Label)this.Master.FindControl("lblANMgsBox")).Visible = false;
            ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;

            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;

            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = false;

            string Type = this.Request.QueryString["Entry"].ToString();
            if (Type == "Apprv")
            {
                ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Text = "Approve";
                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = true;

            }


        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);



        }

        private void GetCompany()
        {
            Session.Remove("tblcompany");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            string txtCompany = "%%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETCOMPANYNAME1", txtCompany, userid, "", "", "", "", "", "", "");
            this.ddlCompany.DataTextField = "actdesc";
            this.ddlCompany.DataValueField = "actcode";
            this.ddlCompany.DataSource = ds1.Tables[0];
            this.ddlCompany.DataBind();
            Session["tblcompany"] = ds1.Tables[0];
            // this.ddlCompany_SelectedIndexChanged(null, null);
            ds1.Dispose();
            this.GetProjectName();

        }
        private void GetProjectName()
        {

            string comcod = this.GetCompCode();
            if (this.ddlCompany.Items.Count == 0)
                return;


            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";

            string txtSProject = "%%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETPROJECTNAME", Company, txtSProject, "", "", "", "", "", "", "");
            this.ddlDepartment.DataTextField = "actdesc";
            this.ddlDepartment.DataValueField = "actcode";
            this.ddlDepartment.DataSource = ds1.Tables[0];
            this.ddlDepartment.DataBind();
            this.ddlDepartment_SelectedIndexChanged(null, null);

        }
        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SectionName();
        }
        private void SectionName()
        {

            string comcod = this.GetCompCode();
            string projectcode = this.ddlDepartment.SelectedValue.ToString();
            string txtSSec = "%%";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "SECTIONNAME", projectcode, txtSSec, "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "sectionname";
            this.ddlProjectName.DataValueField = "section";
            this.ddlProjectName.DataSource = ds2.Tables[0];
            this.ddlProjectName.DataBind();
            // this.GetEmployee();

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            string Type = this.Request.QueryString["Entry"].ToString();
            switch (Type)
            {
                case "Apprv":
                    this.mgtprint();
                    break;

                default:
                    this.hrPrint();
                    break;
            }
        }

        private void mgtprint()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string comcod = GetCompCode();

            //string msg = this.txtml.Text;
            //DataTable dt1 = (DataTable)ViewState["letter"];
            //string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            //LocalReport Rpt1 = new LocalReport();
            //string img = string.Empty;
            //try
            //{
            //    if (!(dt1.Rows[0]["EMPSIGN"] is DBNull))
            //        img = Convert.ToBase64String((byte[])dt1.Rows[0]["EMPSIGN"]);
            //}
            //catch (Exception)
            //{
            //    img = string.Empty;

            //}
            //Rpt1 = RptHRSetup.GetLocalReport("RD_81_Hrm.LetterDefault01", null, null, null);
            //Rpt1.EnableExternalImages = true;
            //Rpt1.SetParameters(new ReportParameter("rpetext", msg));
            //Rpt1.SetParameters(new ReportParameter("ComImg", ComLogo));
            //Rpt1.SetParameters(new ReportParameter("comName", comnam));
            //Rpt1.SetParameters(new ReportParameter("Comadd", comadd));
            //Rpt1.SetParameters(new ReportParameter("ApprvDesig", (string)dt1.Rows[0]["apprvdesig"]));
            //Rpt1.SetParameters(new ReportParameter("ApprvName", (string)dt1.Rows[0]["apprvname"]));
            //Rpt1.SetParameters(new ReportParameter("Apprasign", img));
            //Session["Report1"] = Rpt1;

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('RDLCViewerWin.aspx?PrintOpt=" +
            //         ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void hrPrint()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string usrid = hst["usrid"].ToString();
            string username = hst["username"].ToString();
            string comadd = hst["comadd1"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comcod = GetCompCode();

            string msg = this.txtml.Text;

            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_CODEBOOK", "SHOWUSERSIGN", usrid, "", "", "", "");

            DataTable dt1 = ds3.Tables[0];
            // DataTable dt1 = (DataTable)ViewState["letter"];



            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            LocalReport Rpt1 = new LocalReport();
            string img = string.Empty;
            try
            {
                if (!(dt1.Rows[0]["empsign"] is DBNull))
                    img = Convert.ToBase64String((byte[])dt1.Rows[0]["empsign"]);
                //img = string.Empty;
            }
            catch (Exception)
            {
                img = string.Empty;
            }
            Rpt1 = RDLCAccountSetup.GetLocalReport("RD_81_Hrm.LetterDefault01", null, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("rpetext", msg));
            Rpt1.SetParameters(new ReportParameter("ComImg", ComLogo));
            Rpt1.SetParameters(new ReportParameter("comName", comnam));
            Rpt1.SetParameters(new ReportParameter("Comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("ApprvDesig", (string)dt1.Rows[0]["desig"]));
            Rpt1.SetParameters(new ReportParameter("ApprvName", (string)dt1.Rows[0]["empname"]));
            Rpt1.SetParameters(new ReportParameter("Apprasign", img));
            Session["Report1"] = Rpt1;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {

            string type1 = this.Request.QueryString["Type"].ToString().Trim();
            if (type1 == "10021" || type1 == "10022" || type1 == "10023")
            {
                bool result = false;
                var empid = this.ddlEmployee.SelectedValue.ToString();
                var strval = this.txtml.Text;
                var type = this.Request.QueryString["Type"].ToString().Trim();
                var date = this.txttodate.Text;
                string comcod = this.GetCompCode();
                string refno = "";

                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "UPDATEJLTCMPLTTRMLT", empid, type, date, refno, strval, "", "", "", "", "", "", "", "", "", "");
                if (!result)
                {
                    this.lblmsg1.Text = "Save Failed";
                    this.lblmsg1.Visible = true;
                }

                this.lblmsg1.Text = "Save Successfully";
                this.lblmsg1.Visible = true;

            }
        }

        private void GetSelected()
        {
            string comcod = this.GetCompCode();

            string qtype = this.Request.QueryString["Type"].ToString();
            string empid = this.Request.QueryString["empid"] ?? "%%";
            string callType = "";
            if (qtype == "10002") // appointment letter
            {
                callType = "GETCANDIDATELIST";
            }
            else if (qtype == "10013" || qtype == "10020" || qtype == "10021" || qtype == "10022" || qtype == "10023" || qtype == "10028")
            {
                // callType = "GETCONFIRMEMP";
                callType = "CONFIRMEMPINFO";
            }


            else
            {
                this.dptDiv.Visible = false;
                this.sectDiv.Visible = false;
                callType = "GETCANDIDATELIST";

            }

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_LETTER", callType, qtype, empid, "", "", "", "", "", "", "");
            if (ds1 == null || ds1.Tables[0].Rows.Count == 0)
                return;

            this.ddlEmployee.Items.Clear();
            this.ddlEmployee.DataTextField = "empname";
            this.ddlEmployee.DataValueField = "empid";
            this.ddlEmployee.DataSource = ds1.Tables[0];
            this.ddlEmployee.DataBind();

            ViewState["empinfo"] = ds1;


        }

        protected void GetLettPattern()
        {

        }

        protected string data(string type01)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string cdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string compname = hst["compname"].ToString();
            string usrid = hst["usrid"].ToString();
            string username = hst["username"].ToString();
            string comadd = hst["comadd1"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comcod = GetCompCode();

            //created by nahid for dynamic html value

            string comaddress = (((DataTable)Session["tbllog1"]).Rows[0]["comadd"].ToString().Substring(0, 6) == "<br />") ? ((DataTable)Session["tbllog1"]).Rows[0]["comadd"].ToString().Substring(6) : ((DataTable)Session["tbllog1"]).Rows[0]["comadd"].ToString();

            string imgpge = "~/Image/" + "LOGO" + ((DataTable)Session["tbllog1"]).Rows[0]["comcod"].ToString() + ".PNG";

            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_LETTER", "SHOWUSERSIGN", usrid, "", "", "", "");
            DataTable dt1 = ds3.Tables[0];
            string empid = this.ddlEmployee.SelectedValue.ToString();

            string type1 = this.Request.QueryString["Type"].ToString().Trim();

            string calltype = (Request.QueryString["Type"].ToString() == "10002") ? "GETEMPSALINFOAPP" : "GETEMPSALINFO";


            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_MGT_INTERFACE", calltype, empid, "", "", "", "", "", "", "", "");
            DataTable dtsalery = ds5.Tables[0];
            DataView dvr = new DataView();
            DataTable dtr1 = new DataTable();
            dtr1 = dtsalery;
            dvr = dtr1.DefaultView;
            dvr.RowFilter = ("gcod like '040%'");
            dtr1 = dvr.ToTable();
            DataTable dtsal = dtr1;
            double SalAdd = Convert.ToDouble((Convert.IsDBNull(dtsal.Compute("sum(gval)", "")) ? 0.00 : dtsal.Compute("sum(gval)", "")));
            string ttlsalary = SalAdd.ToString("#,##0.00;(#,##0.00); ");
            string inwords = "In Word: " + ASTUtility.Trans(SalAdd, 2);
            //Building an HTML string.
            StringBuilder html = new StringBuilder();

            //Table start.
            html.Append("<table width='400' border = '1'>");

            //Building the Header row.
            html.Append("<tr><th style='width:100px;font-weight:bold'>Desctiption</th><th style='width:100px;font-weight:bold'>Value</th></tr>");

            //Building the Data rows.
            foreach (DataRow dr1 in dtsal.Rows)
            {
                html.Append("<tr>");
                html.Append("<td style='width:100px;'>" + dr1["gdesc"].ToString() + "</td>" + "<td style='width:80px; text-align:right'>" + Convert.ToDouble(dr1["gval"]).ToString("#,##0.00;(#,##0.00); ") + "</td>");
                html.Append("</tr>");
            }
            html.Append("<tr><th style='width:100px;font-weight:bold'>Total</th><th style='width:100px;font-weight:bold; text-align:right'>" + ttlsalary + "</th></tr>");
            //Table end.
            html.Append("</table>");
            string tablesale = html.ToString();
            DataSet dtempinf = (DataSet)ViewState["empinfo"];
            DataView dv = dtempinf.Tables[0].DefaultView;
            dv.RowFilter = ("empid='" + empid + "'");
            DataTable dtempinf_ = dv.ToTable();
            string jdate = "", idCard = "";

            string bsal = "";
            string hrent = "";
            string cven = "";
            string mallow = "";
            string total = "";
            string incmtax = "";
            string payablesal = "";
            string idcard = "";
            string inwords2 = "";
            double inwrd = 0;



            if (ds5.Tables[3].Rows.Count > 0)
            {
                idcard = ds5.Tables[3].Rows[0]["idcard"].ToString();
            }
            DataTable dtempsal = ds5.Tables[2];

            if (dtempsal.Rows.Count > 0)
            {
                bsal = dtempsal.Rows[0]["bsal"].ToString() ?? "";
                hrent = dtempsal.Rows[0]["hrent"].ToString() ?? "";
                cven = dtempsal.Rows[0]["cven"].ToString() ?? "";
                mallow = dtempsal.Rows[0]["mallow"].ToString() ?? "";
                total = dtempsal.Rows[0]["total"].ToString() ?? "";
                incmtax = dtempsal.Rows[0]["incmtax"].ToString() ?? "";
                payablesal = dtempsal.Rows[0]["payablesal"].ToString() ?? "";
                inwrd = Convert.ToDouble(dtempsal.Rows[0]["payablesal"]);

                string amt1 = ASTUtility.Trans(Math.Round(inwrd), 2);
                int len = amt1.Length;
                inwords2 = amt1.Substring(7, (len - 8));
            }
            DataTable dtemplv = ds5.Tables[1];
            string temptable = "";
            int count = 1;
            if (dtemplv.Rows.Count > 0)
            {
                foreach (DataRow drlv in dtemplv.Rows)
                {

                    temptable = "<tr style='border-style:solid;border: 1px solid black;'><td style='border-style:solid;border: 1px solid black;>" + count + "</td><td style='border-style:solid;border: 1px solid black;>" + drlv["lvname"].ToString() + "</td><td style='border-style:solid;border:1px solid black;>" + drlv["leave"].ToString() + " </td></tr>";
                    count++;
                }
            }

            if (type1 == "10015" || type1 == "10012")
            {
                jdate = Convert.ToDateTime(dtempinf_.Rows[0]["jdate"]).ToString("dd-MMM-yyy");
                idCard = dtempinf_.Rows[0]["idcard"].ToString();

            }

            if (type1 == "10021" || type1 == "10022" || type1 == "10023")
            {
                this.btnsave.Visible = true;
            }


            string date = "Date:" + System.DateTime.Now.ToString("MMM dd,yyyy");
            string Cdate = "Date:" + System.DateTime.Now.ToString("dd MMMM,yyyy");
            string year = System.DateTime.Now.ToString("yyyy");

            string lbody = string.Empty;


            string section = this.ddlProjectName.SelectedItem.ToString();
            string companme = this.ddlCompany.SelectedItem.Text.ToString();

            string congimg = Server.MapPath("~/Upload/cong/cong.jpg");
            string name = this.ddlEmployee.SelectedItem.Text.ToString();
            string Desig = (dtempinf_.Rows.Count == 0) ? "" : dtempinf_.Rows[0]["desig"].ToString();//(string)ViewState["desig"];
            string depart = (dtempinf_.Rows.Count == 0) ? "" : dtempinf_.Rows[0]["dptdesc"].ToString();//(string)ViewState["section"];
            string dptdesc = (dtempinf_.Rows.Count == 0) ? "" : dtempinf_.Rows[0]["section"].ToString();//(string)ViewState["section"];

            string usrdesig = (dt1.Rows.Count == 0) ? "" : dt1.Rows[0]["desig"].ToString();
            string usersign = "";//(dt1.Rows.Count == 0) ? "" : dt1.Rows[0]["empsign"].ToString(); //Convert.ToBase64String((byte[])dt1.Rows[0]["empsign"]);
            string uname = (dt1.Rows.Count == 0) ? "" : dt1.Rows[0]["empname"].ToString();
            string empMobile = (dtempinf_.Rows.Count == 0) ? "" : dtempinf_.Rows[0]["mobile"].ToString();//(string)ViewState["section"];
            int i = 0;
            switch (type01)
            {
                //"<img class='Companylogo' src='Image/LOGO8701.PNG' />";
                case "10001":
                    lbody = "<div><p><strong>Ref: SPL/HR/Appt/16/524</strong></p><p><strong>Nov 20, 2016</strong></p><p><strong>Abdullah Al Noman</strong></p><p><strong>C/O: Abdul Bashar&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</strong></p><p><strong>Vill: Boro Hossainput, PO: Banglabazar-3822,</strong></p><p><strong>PS: Begumganj, Dist: Noakhali&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</strong></p><p>&nbsp;</p><p><strong>Subject: <u>Letter of Appointment.</u></strong></p><p>&nbsp;</p><p>Dear <strong>Mr. Abdullah</strong>,</p><p>&nbsp;With reference to your application and subsequent interview with us, we have the pleasure to inform you that the Management of Star Paradise Ltd is pleased to appoint you as <strong>&ldquo;Field Officer&rdquo;</strong> in our Company <strong>Star Paradise Ltd </strong>on the following terms and conditions:</p><p>&nbsp;</p><p><strong>Commencement and nature of Appointment:</strong></p><ol><li>Your appointment is effective from <strong>Nov 20, 2016</strong>.</li><li>Your appointment is initially on probation basis for 6 (Six) months from the date of your joining. On satisfactory completion of your probationary period, your service may be confirmed. Otherwise, your probationary period may be extended for such a period as may be decided upon by the Management.</li><li>Your place of posting shall be at <strong>'Comilla &ndash; Chandpur&rdquo;</strong> and you shall work under the supervision of <strong>Divisional Head</strong>.</li></ol><p>&nbsp;</p><ol start='2'><li><strong>Placement, Compensation and other benefits:</strong></li><li>You will be entitled to Festival Bonuses as per Company policy, which are two (02) Eid Bonus, 100% of your Gross Salary each.</li><li>Your monthly salary as in &ldquo;Annexure-A&rdquo; only, which includes all your perquisites and allowances. Compensation will be governed by the rules of the Company on the subject, as applicable and / or amended hereafter.</li><li>Personal Income Tax, if any will be on your account and will be deducted each month by the company at source at the time of monthly salary disbursement for onward submission to the relevant Income Tax authorities.</li><li>You are not to disclose / discuss your salary with anyone related to this organization and keep it strictly confidential.</li><li>This is a position of full time and continuous responsibilities and will not engage yourself any Part-time work, profession or employment without written permission from the management.</li><li>You will be entitled with other benefits of the organization time to time as per the Company policy.</li></ol><p>&nbsp;</p><ol start='3'><li><strong>Duties and responsibilities:</strong></li><li>You will carry on with the duties and responsibilities entrusted to you and also the duties and responsibilities that may be entrusted to you by the Management from time to time. You will require working late hours whenever necessary for the greater interest of the organization.</li><li>You have to abide by all instructions and orders issued by the management in good spirit.</li><li>You will retire attaining the age limit fixed by the Bangladesh Govt. through Bangladesh Labor Act.</li></ol><p>&nbsp;</p><ol start='4'><li><strong>Transfer:</strong></li><li>Your Service is transferable from one project to another project of the Company for the greater interest of the Organization.</li><li>The Management may change your designation, duties and responsibilities from time to time as they think fit and proper without disturbing salary and allowances.</li></ol><p>&nbsp;</p><ol start='5'><li><strong>Termination of Service:</strong></li><li>The Management reserves the right to terminate your service at any time without assigning any reason, if your work, attitude or behavior not found satisfactory.</li><li>Either party may however, terminate the contract of employment by giving a notice period of 60 (Sixty) days in writing or in lieu thereof an equivalent of two months&rsquo; basic salary, will have to be paid by the company / surrendered by you in case of failure in giving two months&rsquo; prior notice after confirmation of service.</li><li>During the probation period, either party may terminate the contract of employment with 30 (Thirty) days prior notice.</li><li>When you intend to resign you will have to handover official charges to the nominated person of the Company.</li></ol><p>&nbsp;</p><ol start='6'><li><strong>Confidentiality:</strong></li></ol><p>You shall not, at any time, during the continuance or even after the cessation of your employment hereunder, disclose or divulge or make public, except on legal obligations, either directly to any person, firm or company or use for yourself any trade secret or confidential, technical knowledge, formula, process, compositions, ideas or documents, concerning the business and affairs of the company or any of its dealings, transactions or affairs which you may have acquired from the company or have to your knowledge during the course of and incidental to your employment. If you disclose any such information to any other person(s) or organization, the Company shall prosecute against you for such breach of code of conducts, as it considers necessary to protect its interest and enforce its rights.</p><p>&nbsp;</p><h2>Annexure &ndash; A</h2><p>&nbsp;</p><p><strong>Dear Mr. Abdullah,</strong></p><p>&nbsp;</p><p>You shall be placed at <strong>Grade-2</strong>, the monthly Gross salary of <strong>Tk. 6,000.00 (Six Thousand)</strong> only which is broken down as follows:</p><p>&nbsp;</p><p>&nbsp;<strong><u>Particulars &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; In Taka</u></strong></p><p><strong><u>Basic Salary &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 3,600.00&nbsp;&nbsp;</u></strong></p><p><strong><u>House Rent &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;1,800.00</u></strong></p><p><strong><u>Conveyance Allowance &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;330.00</u></strong></p><p><strong><u>Medical Allowance &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;270.00</u></strong></p><p><strong><u>Total Salary &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;6,000.00 &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;(Six Thousand)</u></strong></p><p>&nbsp;</p><p>If you are agreeable to the aforesaid offer, please acknowledge this letter by way of formal acceptance and return immediately for further action.</p><p>&nbsp;We have the pleasure in welcoming you and sincerely hope that our company will get benefited from your service.</p><p>&nbsp;</p><p>For <strong>Star Paradise Ltd</strong>,</p><p>&nbsp;</p><p>&nbsp;<strong>(Moshiur Hossain Uday)</strong></p><p><strong>Managing Director</strong></p><p>&nbsp;</p><p>I<strong>, Abdullah Al Noman</strong> have fully understood the contents of the letter of appointment and willingly agree to abide by the terms and conditions as stipulated herein above.</p><p>&nbsp;</p><p>&nbsp;</p><p>______________________</p><p>Signature of the Employee</p><p>&nbsp;</p><p>Date: __________________</p><p><strong>&nbsp;</strong></p><p><strong>Copy to:</strong></p><ol><li>HRIS</li><li>Personal File</li></ol><div>";
                    break;
                //appoinment letter for BTI 
                case "10002":
                    if (this.GetCompCode() == "3365")
                    {

                        lbody = "<div style='font-size:13px; font-family: TimesNewRoman, 'Times New Roman', Times, Baskerville, Georgia, serif'><p style='margin-bottom:-11px'>Ref: bti/HR/" + year + "</p><p >" + System.DateTime.Now.ToString("dd MMM yyyy")
                            + "</p><p style='margin-bottom:-11px'> To </p><p style='margin-bottom:-11px'><strong>" + name + "</strong></p>" +
                            "<p style='margin-bottom:-11px'>Address: 56/7/1-2, Nort Bashbo</p><p style='margin-bottom:-11px'>" +
                            "Sobujbugbag, Dhaka</strong></p><p style='margin:5px 0'><br>Mobile : " + empMobile + "</p><p></p><p><strong> <u> Appointment Letter" +
                            "</u></strong></p><p>Dear <strong>" + name + "," + "</strong></p>" +
                            "<p>Reference is made herewith to your application for the position of <strong>  “" + Desig + ", " + section + "” </strong>  and subsequent interview with us, the Management is pleased to give you a career" +
                            " opportunity with “" + comnam + "” as per the following terms and conditions.<br></p>" +
                            "<p><strong>1.</strong> Postion: <strong> " + Desig + ", " + section + "</strong></p>" +
                            "<p><strong>2.</strong> Job Location: <strong>Dhaka</strong></p>" +
                            "<p style='text-align: justify;'><strong>3.</strong> <strong>Date of Commencement: </strong>Your employment shall commence on <strong> " + System.DateTime.Now.ToString("dd MMM yyyy") + " </strong> and shall continue until separated/terminated as per the provision " +
                            "of company rules or resigned in accordance with terms of this letter of Appointment or rules of the company.<br> </p>" +
                            "<p style='text-align: justify;'><strong>4.</strong> <strong>Probation:</strong>" +
                            "<p style='text-align: justify;'>4.1  On appointment, you shall be on probation for a period of 6 (six) months from the date of joining. Upon satisfactory completion of probation, you will be confirmed in the regular service of the company according to the Management decision.</p> " +
                            "<p style='text-align: justify;'>4.2  During the probationary period, your service may be terminated at any time without assigning any reason whatsoever or issuing any notice or payment of salary in lieu thereof.  </p> " +
                            "<p style='text-align: justify;'>4.3  If no letter is issued for extension of probation period or for confirmation of service it will be deemed by default that probation has been extended and will continue indefinitely until the letter of confirmation is issued.</p> " +

                             "<p style='text-align: justify;'><strong>5.</strong> <strong>Compensation /Pay Scale: </strong> Gross Salary/Month: <strong> Taka 50,000/- (In word: Fifty Thousand only).</strong></p>" +
                             "<p style='text-align: justify;'><strong>6. </strong><strong>Salary Review: </strong> Salary will be reviewed after completion of 01 (one) year or at the discretion of Management. </p>" +
                             "<p style='text-align: justify;'><strong>7. </strong><strong>Festival Bonus: </strong>You are entitled to two festival bonuses in Eid-Ul-Fitr & Eid-Ul-Adha as per company policy. </p>" +
                             "<p style='text-align: justify;'><strong>8. </strong><strong>Income Taxes: </strong>All taxes on salaries and allowances shall be borne by you in accordance with laws of the land. Income Tax at source will be deducted monthly or by other manner as deemed fit by the management.</p>" +
                             "<p style='text-align: justify;'><strong>9. </strong><strong>Gratuity: </strong>Gratuity equivalent to one month’s basic pay for each completed year’s of service payable after three years of service. </p>" +
                             "<p style='text-align: justify;'><strong>10. </strong><strong >Obligation of Confidence: </strong>You may have access during the course of employment to or become acquainted with information, which may be designated by the company as confidential or reasonably be regarded as a trade secret. " +
                             "</p>" +
                             "<p style='text-align: justify;'>10.1  The confidential information may include (without limitation), any document or information marked as confidential, and any other information, which you may receive or develop in the course of your employment, which is not publicly available and relates to the business. E.g. operations, finance, legal affairs and other conditions of the company or its associated companies and other matters not readily available to persons not connected with the company or its associated companies either at all or without a significant expenditure of labor, skill and money. </p> " +
                             "<p style='text-align: justify;'>10.2  You shall agree, both during and after your employment, to maintain the confidentiality of this information and to take reasonable measures to prevent unauthorized disclosure or to use by any other person or entity. You shall also agree not to use, both during and after employment the confidential information for any purpose other than the benefit of the company as determined by the Management. Indulgence in such activity shall render you liable for termination with immediate effect notwithstanding any other terms mentioned in the appointment letter.  </p> " +

                            "<p style='text-align: justify;'><strong>11. Leave: </strong></p>" +

                            "<p style='text-align: justify;'>11.1  Existing company rules shall be applicable.  </p> " +
                            "<p style='text-align: justify;'>11.2  Leave is a facility and cannot be claimed as a right.  </p> " +
                            "<p style='text-align: justify;'>11.3  16 days Earned Leave for each completed year of service or pay in lieu thereof as per company policy. </p> " +
                            "<p style='text-align: justify;'>11.4  10 days Casual Leave and 14 days Sick Leave for every twelve months’ of service. </p> " +
                            "<p style='text-align: justify;'>11.5  Annual leave entitlement is calculated on period of service based on a calendar year. </p> " +
                            "<p style='text-align: justify;'>11.6  Approval of leave shall remain at the sole discretion of the company depending on the workloads and the business needs. </p> " +
                            "<p style='text-align: justify;'>11.7  In case of employment in middle of a calendar year, the calculation of leave will be on prorata basis.   </p> " +


                            "<p style='text-align: justify;'><strong>12. Disciplinary Action: </strong>Company service rules shall prevail. Company reserves the right to proceed with legal and other action in case of serious irregularities" +
                            "including financial and other related matters mentioned in company rules.</p>" +
                             "<p style='text-align: justify;'><strong>13. Transfer: </strong>Your service are at the disposal of the Management and are transferable to any Project, sister concerns or offices at any location and you may be entrusted with some other jobs as and when deemed fit by the management. </p>" +
                             "<p style='text-align: justify;'><strong>14. Separation from Services:</strong>" +

                             "<p style='text-align: justify;'>14.1  On confirmation, your services may be terminated with 30 (Thirty) days notice or pay in lieu thereof from either side.</p> " +
                             "<p style='text-align: justify;'>14.2  You are required to deal with the Company's money, material and documents with utmost honesty and professional ethics. If you are found guilty at any point of time of moral turpitude or misappropriation regardless of the value involved, your services would be terminated with immediate" +
                             "effect notwithstanding other terms and conditions mentioned in this letter. </p> " +
                             "<p style='text-align: justify;'>14.3  You have been engaged on the presumption that the particulars furnished by you in your application and/ or bio-data are correct. In case the said particulars are found to be incorrect or that you have concealed or withheld some other relevant facts, " +
                             "your appointment with the company shall stand terminated/ canceled without any notice. </p> " +
                              "<p style='text-align: justify;'>14.4 For other reasons and systems of separation e.g.Dismissal, termination, discharge etc. company policy shall prevail.</p> " +


                             "<p><strong>15. Work Conditions: </strong>" +
                             "<p>15.1  You are employed as <strong> “" + Desig + ", " + section + "” </strong> and vested with such powers to enable you to function and perform your duties.</p> " +
                             "<p style='text-align: justify;'>15.2  You are required to perform your function strictly in accordance with the instructions of your superiors and according to working program provided to you by the Company.  </p> " +
                             "<p style='text-align: justify;'>15.3  You will not engage yourself in any other employment, occupation, or business of any nature for remuneration or profit. </p> " +
                             "<p style='text-align: justify;'>15.4  Your services will be governed by the Rules of the Company in force from time to time. </p> " +

                            "<p style='text-align: justify;'><strong>16. Required Documents: </strong> You are requested to submit the following documents within 07 (seven) days from the date of receipt of this appointment letter: " +

                             "<p style='text-align: justify;'>16.1  Original & photocopies of all academic certificates.  </p> " +
                             "<p style='text-align: justify;'>16.2  Original & photocopies of Letter of acceptance of resignation in the Company Letter Head from the previous employer.  </p> " +
                             "<p style='text-align: justify;'>16.3  Original & photocopies of all Experience Certificate(s). </p> " +
                             "<p style='text-align: justify;'>16.4  4 copies Passport size photograph  </p> " +
                             "<p style='text-align: justify;'>16.5  2 copies Passport size photograph for Employee Nominee. </p> " +
                             "<p style='text-align: justify;'>16.6  Photocopy of passport / any other photo ID / photocopy of National ID Card. </p> " +
                             "<p style='text-align: justify;'>16.7  You are to submit us your clearance letter, which has been duly issued by your previous employer at the time of joining. </p> " +

                             "<p style='text-align: justify;'><strong>17. Acceptance: </strong> Please signify your acceptance of the above terms and conditions of your employment in the company by signing the duplicate copy of this letter of appointment as a token of your acceptance. </p>" +
                             "<p style='text-align: justify;'><strong>18. Miscellaneous: </strong>Other terms and conditions of service will be in accordance with the company rules and regulations, which may be altered by the company from time to time.</p>" +


                            "<p>&nbsp;For <strong>" + companme + " </strong>," +
                            "</p><p>&nbsp;</p><p class='pImage'><strong></p>  <p class='pUname'><span style='border-top:1px solid black'><strong>" + "Brig Gen Mohammad Ayub Ansary, psc (Retd)" + "</span></strong></p> <p>" + "Additional Managing Director and Head of HR" + "</p><p>" + "Admin and Security Department" + "</p>" +
                            "<p style='text-align:center'><strong><u>ACCEPTANCE</u></strong></p>" +
                            "<p style='text-align: justify;'>I, <strong> " + name + " </strong>  have read and fully understood the terms and conditions set out in the Letter of Appointment dated <strong>" + System.DateTime.Now.ToString("dd MMM yyyy") +
                            "</strong> in particular, I have read and  fully understood clauses of the Letter of Appointment. I do hear by confirm my acceptance of the terms and conditions in the aforesaid document and agree to be bound by the terms accordingly. </p>" +

                            "<p>Signature:-------------------------------------Date:------------------ </p>" +
                            "</div>";

                    }
                    else if (this.GetCompCode() == "3354")
                    {

                        lbody =
                        "<p style='text-align:right;style='margin-bottom:-11px''><strong>Ref:EREL/AL2022/027</strong></p>" +
                        "<p style='margin-bottom:-11px'><strong>" + name + "</strong></p>" +
                        "<p style='margin-bottom:-11px'><strong>Present Address:</strong> House: 271, Tejgaon I/A</p>" +
                        "<p><strong>Subject:</strong> Appointment Letter - <strong>" + Desig + "</strong></p>" +
                        "<p>Dear " + name + ",</p>" +
                        //body
                        "<p style='margin-bottom:-11px'>We are delighted to appoint you as a  <strong>" + Desig + "</strong> of the <strong>" + depart + "</strong> department under <strong>" + dptdesc + "</strong> with our organization. If you join our organization, you will become a part of a fast-paced and dedicated team that works together to perform the highest possible level to achieve organizational goal.  </p>" +
                        "<p> As a member of our team, we would ask for your commitment to deliver outstanding quality of results.In addition, we expect your personal accountability in all the service, solutions, actions, communications, advice and results.In return, we are committed to providing you with every opportunity to learn, grow and stretch to the highest level of your ability and potentiality. </p>" +
                        "<p We are confident, you will f>We are confident, you will find this new opportunity both challenging and rewarding. The following points outline the terms and conditions we are proposing.</p>" +

                        //position info
                        "<p style='margin-bottom:-11px'><strong>Designation</strong><span>: " + Desig + "</span></p>" +
                        "<p style='margin-bottom:-11px'><strong>Employee Grade</strong><span>: M4</span></p>" +
                        "<p style='margin-bottom:-11px'><strong>Probable Job Start Date</strong><span>: " + cdate + "</span></p>" +

                        //salary break down table
                        "<p><strong>Salary Breakdown :</strong></p>" +
                        "<table style='width:70%;margin-left:20px;border-style:solid; border: 1px solid black;'><tr style='border-style:solid;border: 1px solid black;'><th style='width:50px;text-align:center;border-style:solid;border:1px solid black;'>SL</th><th>Particulars</th><th style='border-style:solid;border: 1px solid black;'>Amount in BDT</th></tr>" +
                        "<tr style='border-style:solid;border: 1px solid black;'><td style='text-align:center;border-style:solid;border: 1px solid black;'>1</td><td style='border-style:solid;border: 1px solid black;'>Basic (60% of Gross)</td><td style='text-align:center'>" + bsal + "</td></tr>" +
                        "<tr style='border-style:solid;border: 1px solid black;'><td style='text-align:center;border-style:solid;border: 1px solid black;'>2</td><td style='border-style:solid;border: 1px solid black;'>House Rent (30% of Gross)</td><td style='text-align:center;border-style:solid;border: 1px solid black;'>" + hrent + "</td></tr>" +
                        "<tr style='border-style:solid;border: 1px solid black;'><td style='text-align:center;border-style:solid;border: 1px solid black;'>3</td><td style='border-style:solid;border: 1px solid black;'>Medical Allowance (6% of Gross)</td><td style='text-align:center;border-style:solid;border: 1px solid black;'>" + mallow + "</td></tr>" +
                        "<tr style='border-style:solid;border: 1px solid black;'><td style='text-align:center;border-style:solid;border: 1px solid black;'>4</td><td style='border-style:solid;border: 1px solid black;'>Conveyance Allowance (4% of Gross)</td><td style='text-align:center'>" + cven + "</td></tr>" +
                        "<tr style='border-style:solid;border: 1px solid black;'><td style='text-align:center;border-style:solid;border: 1px solid black;' colspan='2'><strong>Total</strong></td><td style='text-align: center;border-style:solid;border:1px solid black;'><strong>" + total + "</strong></td></tr></table>" +

                        "<p style='margin-bottom:-11px'><strong>Mobile Allowance</strong><span>: As per company policy.</span></p>" +
                        "<p style='margin-bottom:-11px'><strong>Festival Bonus</strong><span>: You will be entitled for two festival bonuses yearly.</span></p>" +

                        //leave allocation
                        "<p><strong>Leave Allocation :</strong></p>" +
                        "<table style='width:70%;margin-left:20px;border-style:solid;border:1px solid black;'><tr style='border-style:solid;border:1px solid black;'><th style='width:50px;border-style:solid;border:1px solid black;'>SL</th><th style='border-style:solid;border:1px solid black;'>Types of Leave</th><th style='border-style:solid;border: 1px solid black;'>Total Leave in a Year</th></tr>" + temptable + "</table>" +


                        "<ul><li style='margin-top:100px;'>You will be able to avail earned leave after completion of 1 (one) year employment with Edison Real Estate Limited. Casual leave and sick leave shall be allocated as pro rata basis calculated from your date of joining. </li>" +
                        "<li style='margin-top:10px;'>During probation period, you will be able to avail maximum 2 (two) days leave in a month from your casual/sick leave. Any other absent will be counted as leave without pay.</li></ul>" +

                        //probation
                        "<p style='margin-bottom:-11px'><strong>Probation Period :</strong> Your employment is subject to a three-month probation period. After successful completion of the probation period, your job will be confirmed based on your satisfactory performance and necessary revision will be done accordingly.</p>" +
                        "<p>Following the initial probation period, a progression and performance review will be conducted on a quarterly basis to assess performance to-date and to clarify the arrangement, as the need may arise.</p>" +

                        //termination
                        "<p style='margin-bottom:-11px'><strong>Termination of Employeement:</strong> : During the probation period, the company can terminate this contract without any prior notice based on management decisions. After the completion of the probation period, any time company can terminate this contract with/without a notice period of 2 (Two) months. If an employee wants to leave the organization, must have to provide a notice to the company 2 (Two) months prior. For any type of violation of the company code of conduct, the employee might be terminated immediately.     </p>" +
                        "<p></p>" +
                        "<p></p>" +

                        //footer
                        "<p style='margin-bottom:-5px;display:inline;'><span style='border-top:1px solid; display:inline-block;margin-bottom:-11px;float-left'><strong>Md. Mizanur Rahman Khan</strong></span><span style='border-top:1px solid; display:inline-block;margin-bottom:-11px;float:right'><strong>Ahmed Pasha</strong></p></span></p>" +
                        "<br>" +
                        "<p style='display:inline'><span style=' display:inline-block;margin-bottom:-11px;float-left'><strong>Senior Manager – HR</strong></span><span style='display:inline-block;margin-bottom:-11px;float:right;'><strong>Chief Business Officer</strong></p></span></p>" +
                        "<p style='text-align:left'>I, <strong>" + name + "</strong>, confirm that I have read the terms of employment set out in this letter and I fully understood them and their implications and I now accept the offer of employment.</p>" +
                        "<p></p>" +
                        "<p></p>" +
                        "<p style='border-top:1px solid; display:inline-block;margin-bottom:-11px;'><strong>" + name + "</strong></p>";
                    }
                    else
                    {
                        lbody =
                        "<p style='text-align:right;style='margin-bottom:-11px''><strong>Ref:EREL/AL2022/027</strong></p>" +
                        "<p style='margin-bottom:-11px'><strong>" + name + "</strong></p>" +
                        "<p style='margin-bottom:-11px'><strong>Present Address:</strong> House: 271, Tejgaon I/A</p>" +
                        "<p><strong>Subject:</strong> Appointment Letter - <strong>" + Desig + "</strong></p>" +
                        "<p>Dear " + name + ",</p>" +

                        "<p style='margin-bottom:-11px'>We are delighted to appoint you as a  <strong>" + Desig + "</strong> of the <strong>" + depart + "</strong> department under <strong>" + dptdesc + "</strong> with our organization. If you join our organization, you will become a part of a fast-paced and dedicated team that works together to perform the highest possible level to achieve organizational goal.  </p>" +
                        "<p> As a member of our team, we would ask for your commitment to deliver outstanding quality of results.In addition, we expect your personal accountability in all the service, solutions, actions, communications, advice and results.In return, we are committed to providing you with every opportunity to learn, grow and stretch to the highest level of your ability and potentiality. </p>" +
                        "<p We are confident, you will f>We are confident, you will find this new opportunity both challenging and rewarding. The following points outline the terms and conditions we are proposing.</p>" +

                        "<p style='margin-bottom:-11px'><strong>Designation</strong><span>: " + Desig + "</span></p>" +
                        "<p style='margin-bottom:-11px'><strong>Employee Grade</strong><span>: M4</span></p>" +
                        "<p style='margin-bottom:-11px'><strong>Probable Job Start Date</strong><span>: " + cdate + "</span></p>" +

                        //salary break down table
                        "<p><strong>Salary Breakdown :</strong></p>" +
                        "<table style='width:70%;margin-left:20px;border-style:solid; border: 1px solid black;'><tr style='border-style:solid;border: 1px solid black;'><th style='width:50px;text-align:center;border-style:solid;border:1px solid black;'>SL</th><th>Particulars</th><th style='border-style:solid;border: 1px solid black;'>Amount in BDT</th></tr>" +
                        "<tr style='border-style:solid;border: 1px solid black;'><td style='text-align:center;border-style:solid;border: 1px solid black;'>1</td><td style='border-style:solid;border: 1px solid black;'>Basic (60% of Gross)</td><td style='text-align:center'>" + bsal + "</td></tr>" +
                        "<tr style='border-style:solid;border: 1px solid black;'><td style='text-align:center;border-style:solid;border: 1px solid black;'>2</td><td style='border-style:solid;border: 1px solid black;'>House Rent (30% of Gross)</td><td style='text-align:center;border-style:solid;border: 1px solid black;'>" + hrent + "</td></tr>" +
                        "<tr style='border-style:solid;border: 1px solid black;'><td style='text-align:center;border-style:solid;border: 1px solid black;'>3</td><td style='border-style:solid;border: 1px solid black;'>Medical Allowance (6% of Gross)</td><td style='text-align:center;border-style:solid;border: 1px solid black;'>" + mallow + "</td></tr>" +
                        "<tr style='border-style:solid;border: 1px solid black;'><td style='text-align:center;border-style:solid;border: 1px solid black;'>4</td><td style='border-style:solid;border: 1px solid black;'>Conveyance Allowance (4% of Gross)</td><td style='text-align:center'>" + cven + "</td></tr>" +
                        "<tr style='border-style:solid;border: 1px solid black;'><td style='text-align:center;border-style:solid;border: 1px solid black;' colspan='2'><strong>Total</strong></td><td style='text-align: center;border-style:solid;border:1px solid black;'><strong>" + total + "</strong></td></tr></table>" +

                        "<p style='margin-bottom:-11px'><strong>Mobile Allowance</strong><span>: As per company policy.</span></p>" +
                        "<p style='margin-bottom:-11px'><strong>Festival Bonus</strong><span>: You will be entitled for two festival bonuses yearly.</span></p>" +

                        //leave allocation
                        "<p><strong>Leave Allocation :</strong></p>" +
                        "<table style='width:70%;margin-left:20px;border-style:solid;border:1px solid black;'><tr style='border-style:solid;border:1px solid black;'><th style='width:50px;border-style:solid;border:1px solid black;'>SL</th><th style='border-style:solid;border:1px solid black;'>Types of Leave</th><th style='border-style:solid;border: 1px solid black;'>Total Leave in a Year</th></tr>" + temptable + "</table>" +
                        "<ul><li style='margin-top:50px;'>You will be able to avail earned leave after completion of 1 (one) year employment with Edison Real Estate Limited. Casual leave and sick leave shall be allocated as pro rata basis calculated from your date of joining. </li>" +
                         "<li style='margin-top:10px;'>During probation period, you will be able to avail maximum 2 (two) days leave in a month from your casual/sick leave. Any other absent will be counted as leave without pay.</li></ul>" +
                        "<p style='margin-bottom:-11px'><strong>Probation Period :</strong> Your employment is subject to a three-month probation period. After successful completion of the probation period, your job will be confirmed based on your satisfactory performance and necessary revision will be done accordingly.</p>" +
                        "<p>Following the initial probation period, a progression and performance review will be conducted on a quarterly basis to assess performance to-date and to clarify the arrangement, as the need may arise.</p>" +
                        "<p style='margin-bottom:-11px'><strong>Termination of Employeement:</strong> : During the probation period, the company can terminate this contract without any prior notice based on management decisions. After the completion of the probation period, any time company can terminate this contract with/without a notice period of 2 (Two) months. If an employee wants to leave the organization, must have to provide a notice to the company 2 (Two) months prior. For any type of violation of the company code of conduct, the employee might be terminated immediately.     </p>" +
                        "<p></p>" +
                        "<p></p>" +
                        "<p style='margin-bottom:-5px;display:inline;'><span style='border-top:1px solid; display:inline-block;margin-bottom:-11px;float-left'><strong>MR.X</strong></span><span style='border-top:1px solid; display:inline-block;margin-bottom:-11px;float:right'><strong>MR X</strong></p></span></p>" +
                        "<br>" +
                        "<p style='margin-bottom:-5px;display:inline'><span style=' display:inline-block;margin-bottom:-11px;float-left'><strong></strong></span><span style='display:inline-block;margin-bottom:-11px;float:right;'><strong></strong></p></span></p>" +
                        "<p style='text-align:left'>I, <strong>" + name + "</strong>, confirm that I have read the terms of employment set out in this letter and I fully understood them and their implications and I now accept the offer of employment.</p>" +
                        "<p></p>" +
                        "<p></p>" +
                        "<p style='border-top:1px solid; display:inline-block;margin-bottom:-11px;'><strong>" + name + "</strong></p>";
                    }

                    break;

                //offer later for sales department;
                case "10003":

                    if (this.GetCompCode() == "3354")
                    {
                        lbody =
                       "<p style='text-align:right;style='margin-bottom:-11px''> " + date + "</p>" +
                       "<p style='margin-bottom:-11px'><strong>" + name + "</strong></p>" +
                       "<p style='margin-bottom:-11px'><strong>Present Address:</strong> House: 271, Tejgaon I/A</p>" +
                       "<p><strong>Subject:</strong> Offer Letter-<strong>" + Desig + "</strong></p>" +
                       "<p>Dear " + name + ",</p>" +

                       "<p style='margin-bottom:-11px'>We are delighted to offer you the position of <strong>" + Desig + "</strong> of the <strong>" + depart + "</strong> department under <strong>" + dptdesc + "</strong> with our organization. If you join our organization, you will become a part of a fast-paced and dedicated team that works together to perform the highest possible level to achieve organizational goal.  </p>" +
                       "<p style='margin-bottom:-11px'> As a member of our team, we would ask for your commitment to deliver outstanding quality of results.In addition, we expect your personal accountability in all the service, solutions, actions, communications, advice and results.In return, we are committed to providing you with every opportunity to learn, grow and stretch to the highest level of your ability and potentiality. </p>" +
                       "<p We are confident, you will f>We are confident, you will find this new opportunity both challenging and rewarding. The following points outline the terms and conditions we are proposing.</p>" +
                        "<p style='margin-bottom:-11px'><strong>Designation</strong><span>: " + Desig + "</span></p>" +
                       "<p style='margin-bottom:-11px'><strong>Employee Grade</strong><span>: M4</span></p>" +
                       "<p style='margin-bottom:-11px'><strong>Probable Job Start Date</strong><span>: " + cdate + "</span></p>" +
                       "<p style='margin-bottom:-11px'><strong>Salary</strong><span>: As negotiated and agreed upon by both parties</span></p>" +
                       "<p><strong>Mobile Allowance</strong><span>: As per company policy</span></p>" +

                       "<p style='margin-bottom:-11px'><strong>Probation Period :</strong> Your employment is subject to a three-month probation period. After successful completion of the probation period, your job will be confirmed based on your satisfactory performance and necessary revision will be done accordingly.</p>" +
                       "<p>Following the initial probation period, a progression and performance review will be conducted on a quarterly basis to assess performance to-date and to clarify the arrangement, as the need may arise.</p>" +
                       "<p style='margin-bottom:-11px'><strong>Termination of Employeement:</strong> : During the probation period, the company can terminate this contract without any prior notice based on management decisions. After the completion of the probation period, any time company can terminate this contract with/without a notice period of 2 (Two) months. If an employee wants to leave the organization, must have to provide a notice to the company 2 (Two) months prior. For any type of violation of the company code of conduct, the employee might be terminated immediately.     </p>" +
                       "<p style='margin-bottom:-11px'>We look forward to the opportunity to work with you in an atmosphere that is successful and mutually challenging and rewarding.If you don’t accept this letter, please let us know within " + cdate + ".</p>" +

                           "<p></p>" +

                           "<p style='margin-bottom:-5px;display:inline;'><span style='border-top:1px solid; display:inline-block;margin-bottom:-11px;float-left'><strong>Md. Mizanur Rahman Khan</strong></span><span style='border-top:1px solid; display:inline-block;margin-bottom:-11px;float:right'><strong>Ahmed Pasha</strong></p></span></p>" +
                  "<br>" +
                           "<p style='margin-bottom:-5px;display:inline'><span style=' display:inline-block;margin-bottom:-11px;float-left'><strong>Senior Manager – HR</strong></span><span style='display:inline-block;margin-bottom:-11px;float:right;'><strong>Chief Business Officer</strong></p></span></p>" +


                       "<p style='text-align:left'>I, <strong>" + name + "</strong>, confirm that I have read the terms of employment set out in this letter and I fully understood them and their implications and I now accept the offer of employment.</p>" +


                       "<p style='border-top:1px solid; display:inline-block;margin-bottom:-11px;'><strong>" + name + "</strong></p>";

                    }

                    else if (this.GetCompCode() == "3365")
                    {
                        lbody = "<p style='margin-bottom:0'><br/><br/>Ref: bti/HR/2021/</p>" + date + "<p style='margin-bottom:-11px'>To</p><p style='margin-bottom:-11px'><strong>" + name + "</strong></p>" +
       "<p style='margin-bottom:-11px'>Address: House: 271, Tejgaon I/A, </p>" +
       "<p style='margin-bottom:-11px'>Tejgaon, Dhaka-1208</p><p style='margin-bottom:0'><br>Mobile : 01913169818</p><p><strong>Subject: Offer for Employment</strong></p><p><br>Dear <strong>" + name + "," + "</strong></p>" +
       "<p>With reference to discussions with you and your willingness to join our company, we are pleased to offer you appointment as <strong> " + Desig + ", " + dptdesc + ",</strong> in " + depart + ", which shall commence on or before <strong> " + cdate + " </strong>." +
       "<p>Before that you are requested to submit a copy of your resignation letter, which has been duly received by your present employer within 3 (Three) days from the date of receipt of this offer letter and a clearance letter at the time of joining.</p></p><p>&nbsp;The Letter of Appointment will be issued soon.</p><p>&nbsp; Please bring the following papers on the date of joining:</p><ol>" +
       "<li>Release letter / Letter of acceptance of resignation in the Company Letter Head from the previous employer.</li><li>Original & photocopies of all certificates (experience, academic, professional courses etc).</li><li>Photocopy of National ID card/ Passport (employee and nominee).</li><li>Passport size photograph (employee 7 copies and Nominee 3 copies)." +
       "</li><li>Pay Slip/ Proof of Salary and ETIN.</li></ol><p>&nbsp;</p><p>Yours Sincerely,</p><p class='pImage'><strong></p>  <p class='pUname'><span style='border-top:1px solid black'><strong>" + "Brig Gen Mohammad Ayub Ansary, psc (Retd)" + "</span></strong></p> <p>" + "Additional Managing Director and Head of HR" + "</p><p>" + "Admin and Security Department" + "</p>";

                    }
                    else
                    {
                        lbody = "<p style='margin-bottom:0'><br/><br/>Ref: HR/2021/</p>" + date + "<p style='margin-bottom:-11px'>To</p><p style='margin-bottom:-11px'><strong>" + name + "</strong></p>" +
"<p style='margin-bottom:-11px'>Address: House: 271, Tejgaon I/A, </p>" +
"<p style='margin-bottom:-11px'>Tejgaon, Dhaka-1208</p><p style='margin-bottom:0'><br>Mobile : 01xxxxxxxxxx</p><p><strong>Subject: Offer for Employment</strong></p><p><br>Dear <strong>" + name + "," + "</strong></p>" +
"<p>With reference to discussions with you and your willingness to join our company, we are pleased to offer you appointment as <strong> " + Desig + ", " + dptdesc + ",</strong> in " + depart + ", which shall commence on or before <strong> " + cdate + " </strong>." +
"<p>Before that you are requested to submit a copy of your resignation letter, which has been duly received by your present employer within 3 (Three) days from the date of receipt of this offer letter and a clearance letter at the time of joining.</p></p><p>&nbsp;The Letter of Appointment will be issued soon.</p><p>&nbsp; Please bring the following papers on the date of joining:</p><ol>" +
"<li>Release letter / Letter of acceptance of resignation in the Company Letter Head from the previous employer.</li><li>Original & photocopies of all certificates (experience, academic, professional courses etc).</li><li>Photocopy of National ID card/ Passport (employee and nominee).</li><li>Passport size photograph (employee 7 copies and Nominee 3 copies)." +
"</li><li>Pay Slip/ Proof of Salary and ETIN.</li></ol><p>&nbsp;</p><p>Yours Sincerely,</p><p class='pImage'><strong></p>  <p class='pUname'><span style='border-top:1px solid black'><strong>" + "Mr x" + "</span></strong></p> <p>" + "Additional Managing Director and Head of HR" + "</p><p>" + "Admin and Security Department" + "</p>";

                    }


                    break;
                //offer Later For general
                case "10004":
                    lbody = "<p><strong>SPL/HR/Ofr/16/559</strong></p><p><strong>" + name + "</strong></p><p><strong>Vill: West Vashanchar, PO: Ambikapur</strong></p><p><strong>PS: Faridpur, Dist: Faridpur</strong></p><p>&nbsp;</p><p>&nbsp;Dear <strong>" + name + "</strong></p><p>&nbsp;</p><p>&nbsp;<span style='text-decoration: underline;'><strong>Offer for Employment</strong></span></p><p>&nbsp;With reference to our discussions with you and your willingness to join our company, we are pleased to offer you appointment as  <strong>&ldquo;Plant Manager-Pole Project&rdquo; </strong>in <strong>" + comnam + ".</strong> .”, which shall commence on or before  <strong>11 December 2021.</strong></p><p>&nbsp;</p><p>Before that you are requested to submit a copy of your resignation letter, which has been duly received by your present employer within 3 (Three) days from the date of receipt of this offer letter and a clearance letter at the time of joining.</p><p><br>The Letter of Appointment will be issued soon.</p><p>Please bring the following papers on the date of joining: </p><ol><li>1.	date</li><li>Original & photocopies of all certificates (experience, academic, professional courses etc).</li><li>Photocopy of National ID card/ Passport (employee and nominee).</li><li>Passport size photograph (employee 7 copies and Nominee 3 copies).</li><li>Pay Slip/ Proof of Salary and ETIN.</li></ol><p>&nbsp;</p><p>&nbsp;</p><p>&nbsp; &nbsp; Yours Sincerely,</p>";
                    break;
                //confirmation letter
                case "10005":
                    lbody = "<p>&nbsp;Ref.: SPL<strong>/</strong>HR/Conf/<strong>555/16 &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</strong>Date: December 1, 2016</p><p>&nbsp;<strong>" + name + ", </strong>Staff ID #<strong>128</strong></p><p><strong>STSM, Bagerhat</strong></p><p><strong>Khulna.</strong></p><p>&nbsp;</p><p>&nbsp;Subject:&nbsp; <strong><u>Confirmation of Service</u></strong></p><p>&nbsp;</p><p><strong>Dear " + name + ",</strong></p><p><strong>&nbsp;</strong><strong>&nbsp;</strong>This has reference to your appointment letter.</p><p>&nbsp;We are pleased to inform you that, on successful completion of your period of probation, your services in " + comnam + " have been confirmed effective from <strong>3 November, 2016.</strong></p><p>&nbsp;We hope that you will extend your full support and cooperation to promote company activities and growth in the days to come.</p><p>&nbsp;</p><p>&nbsp;</p><p>Yours faithfully,</p><p>&nbsp;</p><p>&nbsp;<strong>Moshiur Hossain </strong></p><p><strong>Managing Director.</strong></p><p>&nbsp;CC: &nbsp;</p><p>HRIS</p><p>Personal File</p>";
                    break;
                // extension letter
                case "10006":
                    lbody = "<p><strong>Ref. &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; : SPL/HR/Exten/511/15</strong></p><p>Date &nbsp;&nbsp;&nbsp;&nbsp; : December 1, 2016</p><p>&nbsp;<strong>" + name + "</strong></p><p><strong>Staff ID # 124</strong></p><p>" + Desig + "</p><p>" + depart + "</p><p>&nbsp;Subject: <strong>Unsatisfactory Performance during Probationary Period.</strong></p><p>&nbsp;</p><p>Dear Mr. <strong>" + name + "</strong>,</p><p>&nbsp;Please be informed that after a careful review of your performance during probation period, it is found that you have failed to show the satisfactory performance, which is required for your position. Due to your unsatisfactory performance, we are unable to confirm you after six months&rsquo; probation period.</p><p>Your supervisor has suggested areas for improvement in your probationary assessment sheet, and it is expected that you will seek every opportunity to improve the highlighted areas.Therefore, through this letter you are placed under observation for <strong>3 months</strong> beginning from, <strong>01.11.2016.</strong> After completion of the observation period, your performance will re-evaluated by your supervisor. If you fail to show satisfactory performance during that period, your service may be no longer required for the organization.</p><p>&nbsp;</p><p>Thank you and hope you will make every efforts to improve yourself within <strong>next three months</strong>.</p><p>&nbsp;</p><p><strong>Moshiur Hossain</strong></p><p><strong>Managing Director.</strong></p><p>&nbsp;CC:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; HRIS</p><p>Personal File&nbsp;</p>";
                    break;
                //promotion letter
                case "10007":

                    if (comcod == "3370" || comcod == "3101")
                    {
                        lbody =
                             "<p style='text-align: center;'><strong >CONFIDENTIAL</strong> </p>"+
                            "<p style='text-align: left'>" +
                            "<strong>Ref : CPDL/TM/PROMOTION/23-" +
                            "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                            "<strong style='text-align: right'>" + date + "</strong>" + "</strong></p><p>" +
                            "Name      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :&nbsp; " + name + "" + "</p><p>" +
                            "Designation  :&nbsp;" + Desig + "" + "</p><p>" +
                            "Department   :&nbsp;" + depart + "" + "</p><p>" +
                            "<br>" +
                            "<br>" +


                            "<strong>Subject &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;: Promotion in Service</strong></p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                        "<h1 style='text-align:center'>CONGRATULATIONS!!!<h1>" +
                            //"<img style='margin-bottom:-11px;text-align: center;' src = '" + congimg + "' Height = '80' Width = '300' CssClass = 'rounded-circle img-responsive' />" +
                            "<p>It is the immense pleasure to inform you that, on reviewing of your performance and progress of your responsibilities, the management is pleased to promote you as  <strong>" + Desig + " in "
                            + depart + "</strong> with effect from<strong> 1st January 2022.</strong></p>" +
                            "<p>We are delighted towards your commitment and integrity shown towards your responsibilities in" +
                            "the past. We believe that you will continue to deliver your best performance and add on to the " +
                            "growth of the company.</p>" +

                            "<p>Promotion to higher level is recognitions to your performance which includes expectation of" +
                            "greater skills and higher responsibilities for you. </p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                             "<h3 style='margin-bottom:-11px;text-align: center;'>We wish you all the very best.</h3>&nbsp;" +
                            "<p>Thanking you</p>&nbsp;" +
                             "<br> &nbsp; " +
                            "<br>&nbsp; " +
                     
                            "</p><p><strong></strong></p><p>" +

                            "<strong>------------------------------------------</strong> </p><p>" +
                            "<p style='margin-bottom:-11px;'><strong> Md.Khairuzzaman Joarder</strong> </p><p>" +
                            "<p style='margin-bottom:-11px;'><strong> Chief Operating Officer-CPDL</strong> </p><p>" +
                            "</p><p style='margin-bottom:-11px;'>CC: </p>" +
                            "</p><p style='margin-bottom:-11px;'>President-CPDL </p>" +
                            "</p><p style='margin-bottom:-11px;'>Personal File </p>"
                            ;


                    }
                    else
                    {
                        lbody = "<p style='text-align: center;'>&nbsp;</p><h3 style='text-align: center;'>" +
                            "<span style='text-decoration: underline;'><strong>Private &amp; Confidential</strong></span>" +
                            "</h3><p>&nbsp;<strong>Ref: SPL/HR/Prom/489/16&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </strong></p><p><strong>16 November, 2016</strong></p><p><strong>&nbsp;</strong><strong>" + name + ", ID: 278</strong></p><p>" + Desig + ",</p><p>" + depart + "</p><p>&nbsp;<strong>Subject: Promotion</strong></p><p>&nbsp;Dear Mr. <strong>" + name + "</strong>,</p><p>&nbsp;We are pleased to inform you that, the company have decided to promote you to the position of <strong><u>Junior Territory Sales Manager</u></strong> recognition of your performance, effective December 1, 2016.</p><p>&nbsp;In view of the decision the breakdown of your revised monthly salary stands as follows:</p><p style='padding-left: 360px;'>Basic Salary &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;7,2000.00&nbsp; &nbsp; &nbsp;</p><p style='padding-left: 360px;'>House Rent Allowance &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;3,600.00</p><p style='padding-left: 360px;'>Transport Allowance &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 660.00</p><p style='padding-left: 360px;'>Medical Allowance &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 540.00</p><p style='padding-left: 360px;'><strong>Total: &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;TK &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;12,000.00</strong></p><p>&nbsp;</p><p>We acknowledge your excellent performance and congratulate you on your well-deserved promotion. We hope you will continue to contribute to the growth and success of the organization in future.</p><p>&nbsp;</p><p>Yours Sincerely,</p>";//<p>&nbsp;<strong>Moshiur Hossain</strong></p><p><strong>Managing Director.</strong></p><p><strong><u>Copy to:</u></strong></p><p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;HRIS</p><p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Personal File</p>";

                    }
                    break;
                //increment letter
                case "10008":
                    if (comcod == "3370" || comcod == "3101")
                    {
                        lbody =
                             "<p style='text-align: center;'><strong >CONFIDENTIAL</strong> </p>"+
                      "<p style='text-align: left'>" +
                      "<strong >" + date + "</strong>" + "</strong>" +
                            "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                            "<strong>Ref: CPDL/TM/Increment / 22- " + "</strong></p>" +
                        "<strong>Name &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;: " + name + "</strong></p><p>" +
                       "<strong>Personnel ID &nbsp;&nbsp;: " + idcard + "</strong></p><p>" +
                       "<strong>Designation &nbsp;&nbsp;&nbsp;: " + Desig + "</strong></p><p>" +
                       "<strong>Department   &nbsp;&nbsp;&nbsp;&nbsp;: " + dptdesc + "</strong></p><p>" +
                       "<h2 style='border-bottom: 1px solid #0000;display: inline-block'><strong><u>SUB:ANNUAL INCREMENT.</u></strong></h2>" +



                        "<p> In respect of the above, company management has reviewed your overall performances in <br>" +
                        "terms of Individual KPI Score, Development Team Recommended Score, Head of the <br>" +
                        "Department Recommendation as well as in the eye of Top Management and in respect of al <br>" +
                        "those, President of CPDL is satisfied to increase your gross salary from Tk. <strong>Present Salary</strong> <br>" +
                        "/- to Tk. <strong>Increase Salary /-</strong>, (Mobile Allowance & Fooding allowance as per company policy) <br>" +
                        " Now your pay as below:</p>" +
                                                "<table style='width:60%;margin-left:80px;jalign-content: center;border-style:solid; border: 1px solid white;'><tr style='border-style:solid;border: 0px solid white;'></tr>" +
                        "<tr style='border-style:solid;border: 1px solid white;'><td style='border-style:solid;border: 1px solid white;'>1.&nbsp;&nbsp;Basic             </td><td style='text-align:left;'>:&nbsp;&nbsp;12000</td></tr>" +
                        "<tr style='border-style:solid;border: 1px solid white;'><td style='border-style:solid;border: 1px solid white;'>2.&nbsp;House rent        </td><td style='text-align:left;'>:&nbsp;&nbsp;20000</td></tr>" +
                        "<tr style='border-style:solid;border: 1px solid white;'><td style='border-style:solid;border: 1px solid white;'>3.&nbsp;Medical Allowance </td><td style='text-align:left;'>:&nbsp;&nbsp;4500</td></tr>" +
                        "<tr style='border-style:solid;border: 1px solid white;'><td style='border-style:solid;border: 1px solid white;'>4.&nbsp;Others pay        </td><td style='text-align:left;'>:&nbsp;&nbsp;6945</td></tr>" +
                        "<tr style='border-style:solid;border: 1px solid white;'><td style='border-style:solid;border: 1px solid white;'>5.&nbsp;Special allowance </td><td style='text-align:left;'>:&nbsp;&nbsp;5678</td></tr></table>" +

          


                          "<p style='margin-bottom:-11px;text-align: center;'><span> All pay with effect from <strong>Increment Effect Date.</strong></span></p>" +
                        "<p style='margin-bottom:-11px'>We hope this increment will encourage your working efficiency and devotion to your<br> responsibilities.</p>" +


                        "</p><p></strong></p><p>" +
                             "<p style='margin-bottom:-11px;text-align: right;'></strong>------------------------------------------<strong> </p><p>" +
                             "<p style='margin-bottom:-11px;text-align: right;'><strong> Md.Khairuzzaman Joarder</strong> </p><p>" +
                              "<p style='margin-bottom:-11px;text-align: right; margin-right:10px;'><strong> Chief Operating Officer</strong> </p><p>" +
                              "</p><p style='margin-bottom:-11px;'>CC: </p>" +
                               "</p><p style='margin-bottom:-11px;'>President-CPDL </p>" +
                               "</p><p style='margin-bottom:-11px;'>Personal File </p>";






                    }
                    else
                    {
                        lbody = "<p style='text-align: center;'>&nbsp;</p><p style='padding-left: 360px;'>&nbsp;</p><h3 style='text-align: center;'><span style='text-decoration: underline;'><strong>&nbsp;</strong><strong>Private &amp; Confidential</strong></span></h3><p>&nbsp;</p><p><strong>REF: SPL/HR/INCREMENT/16</strong></p><p><strong>Date: July 12, 2016</strong></p><p>&nbsp;<strong>" + name + ", ID: 101</strong></p><p><strong>" + Desig + ",</strong></p><p>" + depart + "</p><p><strong>Factory.</strong></p><p>&nbsp;</p><p><strong>Subject: Increment of Salary</strong></p><p>&nbsp;</p><p><strong>Dear " + name + ",</strong></p><p>&nbsp;We are pleased to inform you that the management has decided to review your monthly gross salary in recognition of your performance during the year 2015-2016, effective from <strong>July 01, 2016</strong>.</p><p>&nbsp;In view of the decision the breakdown of your revised monthly salary stands as follows:</p><p style='padding-left: 330px;'>Basic Salary &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;7,2000.00&nbsp; &nbsp; &nbsp;</p><p style='padding-left: 330px;'>House Rent Allowance &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;3,600.00</p><p style='padding-left: 330px;'>Transport Allowance &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 660.00</p><p style='padding-left: 330px;'>Medical Allowance &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 540.00</p><p style='padding-left: 330px;'><strong>Total: &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;TK &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;12,000.00</strong></p><p>&nbsp;</p><p>&nbsp;We acknowledge your good performance and hope that you will continue to contribute to the growth and success of the organization in future.</p><p>&nbsp;We wish you all the best and look forward to better performance in future.</p><p>&nbsp;</p><p>With best regards</p>";//<p>&nbsp;<strong>Moshiur Hossain Uday</strong></p><p>Managing Director</p><p>&nbsp;CC: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p><p>Personal File</p><p>HRIS</p><p>&nbsp;</p>";
                    }
                    break;

                //transfer lettre
                case "10009":
                    lbody = "<p>&nbsp;<strong>Ref: SPL/HR/TL/558/16</strong></p><p><strong>December 10, 2016</strong></p><p><strong>Mr.</strong> <strong>" + name + "</strong> <strong>(ID # 202)</strong></p><p><strong>TSM,</strong><strong>Dhaka South</strong></p><p>&nbsp;</p><p>&nbsp;Subject: <strong><u>Transfer of Service </u></strong></p><p><br /> <strong>Dear Mr. </strong><strong>" + name + "</strong>,</p><p>&nbsp;In consideration of the exigencies of the Company, it has been decided to transfer you from <strong>Dhaka South (Munshigonj) to Faridpur (Barishal) </strong>effective from <strong>20 Dec, 2016</strong>.</p><p>&nbsp;Please note that the terms and conditions of your service shall not be changed due to this transfer.</p><p>&nbsp;It is expected that you will continue to provide your best services in achieving business goals and objectives of the Company in the days to come.</p><p>&nbsp;Wish you a happy career in Star Paradise Limited.</p><p>&nbsp;</p><p>Yours sincerely<br /> &nbsp;</p>"; //<p>&nbsp;_______________________</p><p><strong>Ridwan Rouf Khan</strong></p><p>Assistant Manager-Human Resources</p><p>&nbsp;&nbsp; C.C</p><ul><li>HRIS</li><li>Personal File</li></ul><p>&nbsp;</p>";
                    break;
                //acceptance of resignation
                case "10010":
                    lbody = "<p>July 13, 2016</p><p>&nbsp;<strong>" + name + ", ID: 106</strong></p><p>CMO,</p><p>" + Desig + "</p><p>" + depart + "</p><p>&nbsp;Subject:<strong><u> Acceptance of Resignation</u></strong></p><p>&nbsp;</p><p>Dear Mr. <strong>" + name + "</strong>,</p><p>This with reference to your letter dated <strong>July 1, 2016</strong> in which you have expressed your inability to continue your service with the organization. We would like to inform you that the management has accepted your resignation with effect from <strong>July 11, 2016.</strong></p><p>Accordingly, you will be released from your work at the close of business of <strong>July 10, 2016</strong> subject to a clearance certificate being issued to you by the concerned departments to the effect that you do not owe to <strong>Fidelity Holdings Ltd</strong> any outstanding dues and or any liabilities thereof.</p><p>You are, also, requested to submit the Identity Card and others official things to HR Department to facilitate your quick clearance from the service.</p><p>We take this opportunity to wish you well and success in all your future endeavors.</p><p>&nbsp;</p><p>&nbsp;</p><p>Sincerely,</p>";
                    break;
                //resign letter
                case "10011":
                    if(comcod == "3370" || comcod == "3101")
                    {
                        lbody =
                            "<p style='text-align: center;'><strong >CONFIDENTIAL</strong> </p>" +
                            "<p style='text-align: left'> " +
                           "<span>" + date + "</strong>" + "<span/>" +
                            "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                            "<span>Our Ref: CPDL/TM/Acceptance/22- " + "</span>"+
                            "<p style='margin-bottom:-11px;'>To</p>" +
                            "<p style='margin-bottom:-11px;'>Name &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;" + name + "</p>" +

                             "<p style='margin-bottom:-11px;'>Designation &nbsp;&nbsp;&nbsp;" + Desig + "</p>" +
                             "<p style='margin-bottom:-11px;'>ID &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :&nbsp;" + idcard + "</p>" +
                             "<p style='margin-bottom:-11px;'>Department &nbsp;:&nbsp;" + depart + "</p>" +
                             "<p style='margin-bottom:-11px;'>CPDL, Chittagong.</p>" +
                             "<br>" +
                             "<p><strong>Sub &nbsp;&nbsp;: &nbsp;&nbsp; <u>Acceptance of Resignation Letter.</u></strong> </p>" +

                             "<p style='margin-top:5px;'><strong>Dear Name of Personnel,</strong><p>" +
                            "<p>In reference to your letter dated <strong>" + date + "</strong>, Company management reviewed your" +
                            "application and decided to release you from company service on"+ date +". Please update &" +
                            "hand over your present responsibilities before…...</p>" +

                             "<p>You are requested to hand over your responsibilities to <strong>Responsibility Handover Personnel </strong> and" +
                            "finally get necessary clearance from all concerned departments (with form no: CF-HR-08) on as" +
                            "per management decision.</p>" +
                            "<p>You are further advised to contact with the Accounts & Finance department on or before….. for " +
                            "your financial settlement if any.</p>" +



                             "<p></p>" +
                             "<p style='margin-bottom:-11px;text-align:left;'>------------------------------------------</p><p>" +
                             "<p style='margin-bottom:-11px;text-align: left;'><strong> Md.Khairuzzaman Joarder</strong> </p><p>" +
                              "<p style='margin-bottom: 5px;text-align: left; margin-left:10px;'><strong> Chief Operating Officer</strong> </p><p>" +
                             

                            "<p style='margin-bottom:-11px;'><Strong>C.C.</strong></p>" +
                            "<p style='margin-bottom:-11px;'>President-CPDL</p>" +
                            "<p style='margin-bottom:-11px;'>Department Head</p>" +
                            "<p style='margin-bottom:-11px;'>Personal File.</p>";
                    }
                    else
                    {
                        lbody = "<p><strong>June 04, 2015</strong></p><p><strong>" + name + "</strong></p><p>" + Desig + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p><p>Accounts&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p><p>" + comnam + "</p><p><strong>&nbsp;</strong></p><p>&nbsp;</p><h3 style=" + "text-align: center;" + "><span style=" + "text-decoration: underline;" + "><strong>RELEASE LETTER</strong></span></h3><p>&nbsp;</p><p>&nbsp;<strong>Dear Mr. </strong><strong>" + name + "</strong><strong>,</strong></p><p><strong>&nbsp;</strong>With reference to, your letter dated <strong>March 05, 2015;</strong> you are hereby released from the service of <strong>" + comnam + "</strong> as at close of business on <strong>April 30, 2015</strong>.</p><p>&nbsp;We wish you all success in life.</p><p>&nbsp;</p><p>&nbsp;</p><p>Yours Sincerely,</p>";//<p>&nbsp;</p><p><strong>Ridwan Rouf Khan</strong></p><p><strong>Assistant Manager-Human Resources.</strong></p>";

                    }
                    break;


                //experence certificate for current employee
                case "10012":
                    lbody = "<div class='printHeader'> <p style='text-align:right'> <br /><br /><br /><br /><br /><br /><br />  Date: " + cdate + "</p><p class='cfHead'>To Whom it May Concern</p><p></br></br></br><br></br></br></br></p><p> This is to certify that <strong>" + name + "</strong>, <strong> " + Desig + " </strong> -  <strong>" + dptdesc + ", " + " <strong> Employee ID No. " + idCard + " </strong> working in " + comnam + " from <strong>" + jdate + " to till date</strong>.</p><p>The organization has never found any serious misconduct from his end and has no objection recommending him.</p><p>&nbsp;</p><p>I wish him all round success.</p><p>&nbsp;</p><p class='pImage'><strong><img src='data:Image/png;base64," + usersign + "' width='200px' height='80px' ></img></p>  <p class='pUname'><strong>" + uname + "</p> <p class='pUname'><strong>" + usrdesig + "</strong></p>";
                    break;
                ///Salary certificate
                case "10015":
                    lbody = "<div class='printHeader'> <p style='text-align:right'><br /><br /><br /><br /><br /><br /><br />Date: " + cdate + "</p><p class='cfHead'>To Whom it May Concern</p><p></br></br></br><br></br></br></br></p><p>This is to certify that <strong>" + name + "</strong>, <strong> " + Desig + " </strong> -  <strong>" + dptdesc + ", " + "</strong> has been working as a regular employee in our company since  <strong>" + jdate + "</strong>.</p><p class='pMargin'><strong>Breakdown of his monthly salary stands as follow:</strong></p><div style='width:400px; margin:0 auto;'>" + tablesale + "</div><p>&nbsp;</p><p><strong>" + inwords + "</strong></p><p>&nbsp;</p> <p class='pFottext'>I hereby certify that the above mentioned information is correct and accurate to the best of my knowledge it also noticeable that company will not be responsible for any sort of loan or personal transection of the above mentioned employee </p>  <br><br><hr width='20%'><p class=''><strong><u><b>HR & Administrator</b></u></p>  <p class='pUname'><strong>" + uname + "</p> <p class='pUname'><strong>" + usrdesig + "</strong></p>";
                    break;
                //Joining Letter for factory    
                case "10020":
                    lbody = "<div class='printHeader'><p>Ref: SPL/HR/Appt/16/524</p><p>" + cdate + "</p><p><strong>" + name + "</strong></p><p><strong>C/O: Abdul Bashar&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</strong></p><p><strong>Vill: Boro Hossainput, PO: Banglabazar-3822,</strong></p><p><strong>PS: Begumganj, Dist: Noakhali&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</strong></p><p>&nbsp;</p><p><strong>Subject: <u>Confirmation of Service.</u></strong></p><p>&nbsp;</p> <p></br></br></br><br></br></br></br></p><p>Dear <strong>" + name + "</strong>,</p><p>This has reference to your appointment letter.</p><p>We are pleased to inform you that, on successful completion of your period of probation, your services in Star Paradise Ltd have been confirmed effective from 3 November, 2016. </p><p>We hope that you will extend your full support and cooperation to promote company activities and growth in the days to come.</p>Yours faithfully,<p><p class='pImage'><strong><img src='data:Image/png;base64," + usersign + "' width='200px' height='80px' ></img></p>  <p class='pUname'><strong>" + uname + "</p> <p class='pUname'><strong>" + usrdesig + "</strong></p>";
                    break;
                case "10021":
                    lbody = "<div><p>Date-</p></div><div><p>To<br />The HRD<br />Acme Technologies Ltd.<br />House- 630, Road-9, Mirpur DOHS,<br />Dhaka-1216.</p></div>" +
                    "<div><p>Dear concern,<br />In response to your offer letter provided dated on &hellip;&hellip;&hellip;........ in connection with the interviews held on" + "&hellip;&hellip;........, I&rsquo;d like to inform that I&rsquo;ve joined your organization today at &hellip;................ as an &hellip;................in the" + " &hellip;&hellip;....................... department.</p></div>" +
                    "<div><p>I thanks for providing me the opportunity to serve the institute. I&rsquo;ll perform my duties sincerely, honestly and to the best of my abilities.</p>" +
                    "</div><div><p>I therefore, request you to accept my joining letter. Please, accord and oblige.</p></div>Sincerely yours,";
                    break;
                case "10022":
                    lbody = "<div style='text-align: left;'><br /><br /><br /> Date...................................</div><div style='text-align: left;'>&nbsp;</div>" +
                            "<div style='text-align: center;'><strong>To Whom It May Concern</strong></div><div style='text-align: left;'>&nbsp;</div>" +
                            "<div style='text-align: left;'>This is to certify that Mr. /Ms. ................................., worked in our organization, ACME" + "Technologies Ltd as an Executive in our ............... dept. from&nbsp; ................................(D-M-Y) to .............................(D-M-Y)." + "<br /><br /></div><div style='text-align: left;'>During his/ her tenure with us, we found him/her to be a sincere, hardworking, loyal " + "and orks well as part of a team.<br /><br /></div><div style='text-align: left;'>We thank her/" + "him for her/his contribution and wish her/him success in her/his future endeavors.</div>" + "<div style='text-align: left;'>&nbsp;</div>" + "<div style='text-align: left;'>Best " + "regards<br /><br /></div><div style='text-align: left;'>Name&nbsp; &nbsp;" +
                            " &nbsp; &nbsp; ..........................................<br /><br /></div><div style=text-align: " +
                            "left;'>Position: ..........................................<br /><br /></div><div style='text-align: left;'>ACMETechnologies Ltd</div><div>&nbsp;" +
                            "</div>";
                    break;
                case "10023":
                    lbody = "<div style='text-align: left;'><p style='text-align: left;'>Date-d/m/y<br /><br />Mr. ................<br />House no- &hellip;.......... " +
                              "Road-...............<br />Dhaka<br /><br />Dear .....................,</p><p style='text-align: left;'>I regret to inform you that your" +
                            " employment with ACME Technologies Ltd. is terminated effective as of 10<sup>th</sup> Sep, 18.<br /><br />The reasons for your termination" +
                            " are as follows:<br /" +
    ">&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;" +
                            "&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;" +
                            "&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;" +
                            "<br />&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;" +
                            "&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;" +
                            "&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;" +
                            "<br />&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;" +
                            "&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;" +
                            "&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;" +
                            "</p><p style='text-align: left;'>Within &hellip;&hellip;&hellip;.. Days of the effective date, you must return all company documents and " +
                              "property to the company within this date (d/m/y). (Optional) [According to our records, the following company property is in your" +
                            " possession:<br /" +
                            ">&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;" +
                            "&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;" +
                            "&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;" +
                            "<br />&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;" +
                            "&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;" +
                            "&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;&hellip;" +
     "</p><p style='text-align: left;'>Sincerely yours,</p><p style='text-align: left;'>(Appointing Authority)</p></div><div style='text-align: left;'>&nbsp;</div>";
                    break;

                //confirmation with increment with promotion
                case "10024":
                    if (this.GetCompCode() == "3354")
                    {
                        lbody =
                           "<p style='text-align:right;style='margin-bottom:-11px''> " + date + "</p>" +
                           "<p style='margin-bottom:-11px'><strong>Ref:ERE/HR/CL/2022/027</strong></p>" +
                           "<p style='margin-bottom:-11px'><strong>" + name + "</strong></p>" +
                            "<p style='margin-bottom:-11px'>Employee ID : " + idcard + "</p>" +
                            "<p style='margin-bottom:-11px'>" + Desig + "</p>" +
                            "<p style='margin-bottom:-11px'>" + depart + "</p>" +
                            "<p style='margin-bottom:-11px'>" + dptdesc + "</p>" +
                            "<p>Subject:<strong>Confirmation with Promotion</strong> </p>" +
                            "<p>Dear " + name + "</p>" +
                            "<p><strong>Congratulations!</strong></p>" +
                            "<p>We would like to congratulate you on your successful completion of the probation period in our organization. We are glad to have received satisfactory reports from your superior regarding your performance during the said period. </p>" +
                            "<p>You have good product knowledge & inventory accuracy, are good at execution of tasks, able to maintain transparency in documentation; are punctual and disciplined at work; respond positively to other assigned tasks and we appreciate you for that. We are expecting an increase in work knowledge and an improvement in warehouse capacity management from you which will foster your performance in the future.</p>" +
                            "<p> The management wishes to confirm your employment with us and reward you with a promotion from Store Officer to Senior Store Officer of the Construction Operations department under the Project Implementation division with an effective date from May 10, 2022. Your salary has been revised as well and your new salary is BDT 17,000 (Seventeen Thousand Only). </p>" +
                            "<p>Now that you are going to be even an integral part of the organization, we would expect greater efforts from you to strive to do better at work for ultimately setting the organization on the growth path.  We have complete faith in you. </p>" +
                            "<p></p>" +
                            "<p>Wishing you all the very best</p>" +
                             "<p></p>" +
                            "Regards," +
                            "<p></p>" +
                          "<p></p>" +
                              "<p></p>" +

                           "<p style='margin-bottom:-5px;display:inline;'><span style='border-top:1px solid; display:inline-block;margin-bottom:-11px;float-left'><strong>Md. Mizanur Rahman Khan</strong></span><span style='border-top:1px solid; display:inline-block;margin-bottom:-11px;float:right'><strong>S. M. Sahedul Karim Munna </strong></p></span></p>" +
                             "<br>" +
                           "<p style='margin-bottom:-5px;display:inline'><span style=' display:inline-block;margin-bottom:-11px;float-left'><strong>Senior Manager – HR</strong></span><span style='display:inline-block;margin-bottom:-11px;float:right;'><strong>Chief Operating Officer</strong></p></span></p>" +
                           "<p></p>" +
                           "<p style='display:inline-block;border-bottom:1px solid;margin-bottom:-11px;'>CC:</p>" +
                           "<p style='margin-left:10px;margin-bottom:-11px;'>1.Personal file</p>" +
                           "<p style='margin-left:10px;margin-bottom:-11px;'>2.Office file</p>";

                    }
                    else
                    {
                        lbody =
                                   "<p style='text-align:right;style='margin-bottom:-11px''> " + date + "</p>" +
                                   "<p style='margin-bottom:-11px'><strong>Ref:ERE/HR/CL/2022/027</strong></p>" +
                                   "<p style='margin-bottom:-11px'><strong>" + name + "</strong></p>" +
                                    "<p style='margin-bottom:-11px'>Employee ID : " + idcard + "</p>" +
                                    "<p style='margin-bottom:-11px'>" + Desig + "</p>" +
                                    "<p style='margin-bottom:-11px'>" + depart + "</p>" +
                                    "<p style='margin-bottom:-11px'>" + dptdesc + "</p>" +
                                    "<p>Subject:<strong>Confirmation with Promotion.</strong> </p>" +
                                    "<p>Dear " + name + "</p>" +
                                    "<p><strong>Congratulations!</strong></p>" +
                                    "<p>We would like to congratulate you on your successful completion of the probation period in our organization. We are glad to have received satisfactory reports from your superior regarding your performance during the said period. </p>" +
                                    "<p>You have good product knowledge & inventory accuracy; are good at execution of tasks; able to maintain transparency in documentation; are punctual and disciplined at work; respond positively to other assigned tasks and we appreciate you for that. We are expecting an increase in work knowledge and an improvement in warehouse capacity management from you which will foster your performance in the future.</p>" +
                                    "<p>The management wishes to confirm your employment with us as a " + Desig + " of the " + depart + " department under the " + dptdesc + " with an effective date from May 10, 2022. Your salary has been revised as well and your new salary is BDT 17,000 (Seventeen Thousand Only).     </p>" +
                                     "<p>Now that you are going to be even an integral part of the organization, we would expect greater efforts from you to strive to do better at work for ultimately setting the organization on the growth path.  We have complete faith in you. </p>" +

                                          "<p></p>" +
                                    "<p>Wishing you all the very best</p>" +
                                     "<p></p>" +
                                    "Regards," +
                                    "<p></p>" +
                                  "<p></p>" +
                                      "<p></p>" +

                                   "<p style='margin-bottom:-5px;display:inline;'><span style='border-top:1px solid; display:inline-block;margin-bottom:-11px;float-left'><strong>MR X</strong></span><span style='border-top:1px solid; display:inline-block;margin-bottom:-11px;float:right'><strong>MR X</strong></p></span></p>" +
                                     "<br>" +
                                   "<p style='margin-bottom:-5px;display:inline'><span style=' display:inline-block;margin-bottom:-11px;float-left'><strong>Senior Manager – HR</strong></span><span style='display:inline-block;margin-bottom:-11px;float:right;'><strong>Chief Operating Officer</strong></p></span></p>" +
                                   "<p></p>" +
                                   "<p style='display:inline-block;border-bottom:1px solid;margin-bottom:-11px;'>CC:</p>" +
                                   "<p style='margin-left:10px;margin-bottom:-11px;'>1.Personal file</p>" +
                                   "<p style='margin-left:10px;margin-bottom:-11px;'>2.Office file</p>";
                    }
                    break;
                //confirmation letter
                case "10025":
                    if (this.GetCompCode() == "3354")
                    {
                        lbody =
                             "<p style='text-align: center;'><strong >CONFIDENTIAL</strong> </p>"+
                           "<p style='text-align:right;style='margin-bottom:-11px''> " + date + "</p>" +
                           "<p style='margin-bottom:-11px'><strong>Ref:ERE/HR/CL/2022/027</strong></p>" +
                           "<p style='margin-bottom:-11px'><strong>" + name + "</strong></p>" +
                            "<p style='margin-bottom:-11px'>Employee ID : " + idcard + "</p>" +
                            "<p style='margin-bottom:-11px'>" + Desig + "</p>" +
                            "<p style='margin-bottom:-11px'>" + depart + "</p>" +
                            "<p style='margin-bottom:-11px'>" + dptdesc + "</p>" +
                            "<p>Subject:<strong>Confirmation Letter.</strong> </p>" +
                            "<p>Dear " + name + "</p>" +
                            "<p><strong>Congratulations!</strong></p>" +
                            "<p>We would like to congratulate you on your successful completion of the probation period in our organization. We are glad to have received satisfactory reports from your superior regarding your performance during the said period. </p>" +
                            "<p>You have good product knowledge & inventory accuracy; are good at execution of tasks; able to maintain transparency in documentation; are punctual and disciplined at work; respond positively to other assigned tasks and we appreciate you for that. We are expecting an increase in work knowledge and an improvement in warehouse capacity management from you which will foster your performance in the future.</p>" +
                            "<p>The management wishes to confirm your employment with us as a " + Desig + " of the " + depart + " department under the " + dptdesc + " with an effective date from May 10, 2022. Your salary has been revised as well and your new salary is BDT 17,000 (Seventeen Thousand Only).     </p>" +
                            "<p>Now that you are going to be even an integral part of the organization, we would expect greater efforts from you to strive to do better at work for ultimately setting the organization on the growth path.  We have complete faith in you. </p>" +
                                  "<p></p>" +
                             "<p>Wishing you all the very best</p>" +
                                     "<p></p>" +
                                    "Regards," +
                                    "<p></p>" +
                                  "<p></p>" +
                                      "<p></p>" +

                                   "<p style='margin-bottom:-5px;display:inline;'><span style='border-top:1px solid; display:inline-block;margin-bottom:-11px;float-left'><strong>MR X</strong></span><span style='border-top:1px solid; display:inline-block;margin-bottom:-11px;float:right'><strong>MR X</strong></p></span></p>" +
                                     "<br>" +
                                   "<p style='margin-bottom:-5px;display:inline'><span style=' display:inline-block;margin-bottom:-11px;float-left'><strong>Senior Manager – HR</strong></span><span style='display:inline-block;margin-bottom:-11px;float:right;'><strong>Chief Operating Officer</strong></p></span></p>" +
                                   "<p></p>" +
                                   "<p style='display:inline-block;border-bottom:1px solid;margin-bottom:-11px;'>CC:</p>" +
                                   "<p style='margin-left:10px;margin-bottom:-11px;'>1.Personal file</p>" +
                                   "<p style='margin-left:10px;margin-bottom:-11px;'>2.Office file</p>";

                    }
                    else if (this.GetCompCode() == "3370" || this.GetCompCode() == "3101")
                    {
                        lbody = "<p style='text-align: left'> " +
                            "Ref : CPDL/TM/Confirmation/22-" +
                            "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                            "<span style='text-align: right'>" + Cdate + "</span></p>" +
                            "<p style='margin-bottom:-11px;'><strong>NAME &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;" + name + "</strong></p>" +

                             "<p style='margin-bottom:-11px;'><strong>Designation :&nbsp;" + Desig + "</strong></p>" +
                             "<p style='margin-bottom:-11px;'><strong>ID &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :&nbsp;" + idcard + "</strong></p>" +
                             "<p style='margin-bottom:-11px;'><strong>Department &nbsp;:&nbsp;" + depart + "</strong></p>" +
                             "<br>"+
                             "<p><strong>Sub &nbsp;&nbsp;: &nbsp;&nbsp; <u>CONFIRMATION IN SERVICE.</u></strong> </p>" +
                            
                             "<h1 style='text-align:center'>CONGRATULATIONS!!!<h1>"  +
                            "<p>In respect of the above, company management has reviewed your overall performances and as " +
                            "such President is satisfied to confirm your job with effected from xxxxxxx. You will now " +
                            "entitled with the following facilities</p>" +
                        "<ul><li style='margin-top:15px;'><strong>Leave:</strong> CL- 14 days, SL- 10 days and LWP-90 days AL- 10 days (after one year service completion from confirmation date). For this year you will get CL- xxx days, SL- xxx days and LWP- xxx days.</li>" +
                         "<li style='margin-top:10px;'><Strong>Special Medical Allowance:</strong> As per company policy you can now apply for special medical allowance</li>" +
                         "<li style='margin-top:10px;'><Strong>Others Benefit:</strong> You will be eligible for other benefits as per our Service Code-2018.</li>" +
                         "<li style='margin-top:10px;'><Strong>Provident Fund: If you wish to entitle the membership of Provident Fund & Welfare Fund, you need to contact with Talent Management Department and fill up prescribed membership form</strong> As per policy 10% of your basic salary will be deducted from your salary every month as PF and same 10% will be added in PF as company contribution. For contribution in Welfare Fund as per company policy Tk. xxx/- will be deducted every month.</li>" +
                         "<li style='margin-top:10px;'><Strong>Provident Fund Disbursement:</strong>If you wish to resign after confirmation, you should be informed by written before Three (03) months to the management of the company or surrender 03(Three) months gross salary to company in lieu thereof. In that case for provident fund disbursement you will get full of your own contribution and in case of company contribution for successful service completion of 4 years 50%, 6 years 75% and for 8 years 100% will be provided.</li></ul>" +
                             "<p>We hope, your confirmation in service will encourage you in discharging your duty with due care. </p>" +
                              "<p style='text-align:right;margin-left:30px;'>Thanking you,&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </p>" +
                            
                             "<p></p>" +

                             "<p style='margin-bottom:-11px;text-align: right;margin-right:20px;'><strong> Md.Khairuzzaman Joarder</strong> </p><p>" +
                              "<p style='margin-bottom:-11px;text-align: right; margin-right:30px;'> Chief Operating Officer </p><p>" +
                           
                     
                            "<p style='display:inline-block;margin-bottom:-11px;'>CC:</p>" +
                            "<p style='margin-left:30px;margin-bottom:-11px;'>President </p>" +
                            "<p style='margin-left:30px;margin-bottom:-11px;'>Department Head</p>" +
                            "<p style='margin-left:30px;margin-bottom:-11px;'>Personal File.</p>";
                    }
                    else
                    {
                        lbody =
                                   "<p style='text-align:right;style='margin-bottom:-11px''> " + date + "</p>" +
                                   "<p style='margin-bottom:-11px'><strong>Ref:ERE/HR/CL/2022/027</strong></p>" +
                                   "<p style='margin-bottom:-11px'><strong>" + name + "</strong></p>" +
                                    "<p style='margin-bottom:-11px'>Employee ID : " + idcard + "</p>" +
                                    "<p style='margin-bottom:-11px'>" + Desig + "</p>" +
                                    "<p style='margin-bottom:-11px'>" + depart + "</p>" +
                                    "<p style='margin-bottom:-11px'>" + dptdesc + "</p>" +
                                    "<p>Subject:<strong>Confirmation Letter.</strong> </p>" +
                                    "<p>Dear " + name + "</p>" +
                                    "<p><strong>Congratulations!</strong></p>" +
                                    "<p>We would like to congratulate you on your successful completion of the probation period in our organization. We are glad to have received satisfactory reports from your superior regarding your performance during the said period. </p>" +
                                    "<p>You have good product knowledge & inventory accuracy; are good at execution of tasks; able to maintain transparency in documentation; are punctual and disciplined at work; respond positively to other assigned tasks and we appreciate you for that. We are expecting an increase in work knowledge and an improvement in warehouse capacity management from you which will foster your performance in the future.</p>" +
                                    "<p>The management wishes to confirm your employment with us as a " + Desig + " of the " + depart + " department under the " + dptdesc + " with an effective date from May 10, 2022. Your salary has been revised as well and your new salary is BDT 17,000 (Seventeen Thousand Only).     </p>" +
                                    "<p>Now that you are going to be even an integral part of the organization, we would expect greater efforts from you to strive to do better at work for ultimately setting the organization on the growth path.  We have complete faith in you. </p>" +
                                          "<p></p>" +
                                    "<p>Wishing you all the very best</p>" +
                                     "<p></p>" +
                                    "Regards," +
                                    "<p></p>" +
                                  "<p></p>" +
                                      "<p></p>" +

                                   "<p style='margin-bottom:-5px;display:inline;'><span style='border-top:1px solid; display:inline-block;margin-bottom:-11px;float-left'><strong>MR X</strong></span><span style='border-top:1px solid; display:inline-block;margin-bottom:-11px;float:right'><strong>MR X</strong></p></span></p>" +
                                     "<br>" +
                                   "<p style='margin-bottom:-5px;display:inline'><span style=' display:inline-block;margin-bottom:-11px;float-left'><strong>Senior Manager – HR</strong></span><span style='display:inline-block;margin-bottom:-11px;float:right;'><strong>Chief Operating Officer</strong></p></span></p>" +
                                   "<p></p>" +
                                   "<p style='display:inline-block;border-bottom:1px solid;margin-bottom:-11px;'>CC:</p>" +
                                   "<p style='margin-left:10px;margin-bottom:-11px;'>1.Personal file</p>" +
                                   "<p style='margin-left:10px;margin-bottom:-11px;'>2.Office file</p>";
                    }
                    break;
                // extension letter
                case "10026":
                    if (this.GetCompCode() == "3354")
                    {
                        lbody =
                           "<p style='text-align:right;style='margin-bottom:-11px''> " + date + "</p>" +
                           "<p style='margin-bottom:-11px'><strong>Ref:ERE/HR/CL/2022/027</strong></p>" +
                           "<p style='margin-bottom:-11px'><strong>" + name + "</strong></p>" +
                            "<p style='margin-bottom:-11px'>Employee ID : " + idcard + "</p>" +
                            "<p style='margin-bottom:-11px'>" + Desig + "</p>" +
                            "<p style='margin-bottom:-11px'>" + depart + "</p>" +
                            "<p style='margin-bottom:-11px'>" + dptdesc + "</p>" +
                            "<p>Subject:<strong>Extension of the Probationary Period.</strong> </p>" +
                            "<p>Dear " + name + "</p>" +
                            "<p>Based on the assessment of your performance during your probationary period, we regret to inform you that your performance is unsatisfactory and we are unable to confirm your employment at this point. Thus, ERE management has decided to extend your probationary period for a further 1 (One) month starting from May 13, 2022 to June 13, 2022. </p>" +
                                                          "<p>Within this period, we are expecting a noticeable improvement in performance, development in your skills, and to work closely with your supervisor for guidance and feedback. </p>" +
                            "<p>At the end of this period, your performance will be evaluated and the final decision regarding your employment will be made in view of your performance.</p>" +
                            "<p></p>" +
                            "<p>Thank you.</p>" +
                                "<p></p>" +
                                    "<p></p>" +
                                        "<p></p>" +


                           "<p style='margin-bottom:-5px;display:inline;'><span style='border-top:1px solid; display:inline-block;margin-bottom:-11px;float-left'><strong>Md. Mizanur Rahman Khan</strong></span><span style='border-top:1px solid; display:inline-block;margin-bottom:-11px;float:right'><strong>Ahmed Pasha </strong></p></span></p>" +
                             "<br>" +
                           "<p style='margin-bottom:-5px;display:inline'><span style=' display:inline-block;margin-bottom:-11px;float-left'><strong>Senior Manager – HR</strong></span><span style='display:inline-block;margin-bottom:-11px;float:right;'><strong>Chief Business Officer</strong></p></span></p>" +
                           "<p></p>" +
                           "<p>Attachment: Evaluation Form – Supervisor</p>" +
                           "<p style='display:inline-block;border-bottom:1px solid;margin-bottom:-11px;'>CC:</p>" +

                           "<p style='margin-left:10px;margin-bottom:-11px;'>1.Personal file</p>" +
                           "<p style='margin-left:10px;margin-bottom:-11px;'>2.Office file</p>";
                    }
                    else
                    {
                        lbody =
                           "<p style='text-align:right;style='margin-bottom:-11px''> " + date + "</p>" +
                           "<p style='margin-bottom:-11px'><strong>Ref:ERE/HR/CL/2022/027</strong></p>" +
                           "<p style='margin-bottom:-11px'><strong>" + name + "</strong></p>" +
                            "<p style='margin-bottom:-11px'>Employee ID : " + idcard + "</p>" +
                            "<p style='margin-bottom:-11px'>" + Desig + "</p>" +
                            "<p style='margin-bottom:-11px'>" + depart + "</p>" +
                            "<p style='margin-bottom:-11px'>" + dptdesc + "</p>" +
                            "<p>Subject:<strong>Extension of the Probationary Period.</strong> </p>" +
                            "<p>Dear " + name + "</p>" +
                            "<p>Based on the assessment of your performance during your probationary period, we regret to inform you that your performance is unsatisfactory and we are unable to confirm your employment at this point. Thus, ERE management has decided to extend your probationary period for a further 1 (One) month starting from May 13, 2022 to June 13, 2022. </p>" +
                                                          "<p>Within this period, we are expecting a noticeable improvement in performance, development in your skills, and to work closely with your supervisor for guidance and feedback. </p>" +
                            "<p>At the end of this period, your performance will be evaluated and the final decision regarding your employment will be made in view of your performance.</p>" +
                            "<p></p>" +
                            "<p>Thank you.</p>" +
                                "<p></p>" +
                                    "<p></p>" +
                                        "<p></p>" +


                           "<p style='margin-bottom:-5px;display:inline;'><span style='border-top:1px solid; display:inline-block;margin-bottom:-11px;float-left'><strong>MR X</strong></span><span style='border-top:1px solid; display:inline-block;margin-bottom:-11px;float:right'><strong>MR X </strong></p></span></p>" +
                             "<br>" +
                           "<p style='margin-bottom:-5px;display:inline'><span style=' display:inline-block;margin-bottom:-11px;float-left'><strong>Senior Manager – HR</strong></span><span style='display:inline-block;margin-bottom:-11px;float:right;'><strong>Chief Business Officer</strong></p></span></p>" +
                           "<p></p>" +
                           "<p>Attachment: Evaluation Form – Supervisor</p>" +
                           "<p style='display:inline-block;border-bottom:1px solid;margin-bottom:-11px;'>CC:</p>" +

                           "<p style='margin-left:10px;margin-bottom:-11px;'>1.Personal file</p>" +
                           "<p style='margin-left:10px;margin-bottom:-11px;'>2.Office file</p>";
                    }

                    break;
                //confirmation letter without increment
                case "10027":
                    if (this.GetCompCode() == "3354")
                    {
                        lbody =
                           "<p style='text-align:right;style='margin-bottom:-11px''> " + date + "</p>" +
                           "<p style='margin-bottom:-11px'><strong>Ref:ERE/HR/CL/2022/027</strong></p>" +
                           "<p style='margin-bottom:-11px'><strong>" + name + "</strong></p>" +
                            "<p style='margin-bottom:-11px'>Employee ID : " + idcard + "</p>" +
                            "<p style='margin-bottom:-11px'>" + Desig + "</p>" +
                            "<p style='margin-bottom:-11px'>" + depart + "</p>" +
                            "<p style='margin-bottom:-11px'>" + dptdesc + "</p>" +
                            "<p>Subject:<strong>Confirmation Letter.</strong> </p>" +
                            "<p>Dear " + name + "</p>" +
                            "<p><strong>Congratulations!</strong></p>" +
                            "<p>We would like to congratulate you on your successful completion of the probation period in our organization. We are glad to have received satisfactory reports from your superior regarding your performance during the said period. </p>" +
                            "<p>You have good product knowledge & inventory accuracy; are good at execution of tasks; able to maintain transparency in documentation; are punctual and disciplined at work; respond positively to other assigned tasks and we appreciate you for that. We are expecting an increase in work knowledge and an improvement in warehouse capacity management from you which will foster your performance in the future.</p>" +

                            "<p>The management wishes to confirm your employment with us as a " + Desig + " of the " + depart + " department under the " + dptdesc + " with an effective date from May 10, 2022</p>" +
                            "<p>Now that you are going to be even an integral part of the organization, we would expect greater efforts from you to strive to do better at work for ultimately setting the organization on the growth path.  We have complete faith in you. </p>" +
                                  "<p></p>" +
                            "<p>Wishing you all the very best</p>" +
                             "<p></p>" +
                            "Regards," +
                            "<p></p>" +
                          "<p></p>" +
                              "<p></p>" +

                           "<p style='margin-bottom:-5px;display:inline;'><span style='border-top:1px solid; display:inline-block;margin-bottom:-11px;float-left'><strong>Md. Mizanur Rahman Khan</strong></span><span style='border-top:1px solid; display:inline-block;margin-bottom:-11px;float:right'><strong>S. M. Sahedul Karim Munna </strong></p></span></p>" +
                             "<br>" +
                           "<p style='margin-bottom:-5px;display:inline'><span style=' display:inline-block;margin-bottom:-11px;float-left'><strong>Senior Manager – HR</strong></span><span style='display:inline-block;margin-bottom:-11px;float:right;'><strong>Chief Operating Officer</strong></p></span></p>" +
                           "<p></p>" +
                           "<p style='display:inline-block;border-bottom:1px solid;margin-bottom:-11px;'>CC:</p>" +
                           "<p style='margin-left:10px;margin-bottom:-11px;'>1.Personal file</p>" +
                           "<p style='margin-left:10px;margin-bottom:-11px;'>2.Office file</p>";

                    }
                    else
                    {
                        lbody =
                                   "<p style='text-align:right;style='margin-bottom:-11px''> " + date + "</p>" +
                                   "<p style='margin-bottom:-11px'><strong>Ref:ERE/HR/CL/2022/027</strong></p>" +
                                   "<p style='margin-bottom:-11px'><strong>" + name + "</strong></p>" +
                                    "<p style='margin-bottom:-11px'>Employee ID : " + idcard + "</p>" +
                                    "<p style='margin-bottom:-11px'>" + Desig + "</p>" +
                                    "<p style='margin-bottom:-11px'>" + depart + "</p>" +
                                    "<p style='margin-bottom:-11px'>" + dptdesc + "</p>" +
                                    "<p>Subject:<strong>Confirmation Letter.</strong> </p>" +
                                    "<p>Dear " + name + "</p>" +
                                    "<p><strong>Congratulations!</strong></p>" +
                                    "<p>We would like to congratulate you on your successful completion of the probation period in our organization. We are glad to have received satisfactory reports from your superior regarding your performance during the said period. </p>" +
                                    "<p>You have good product knowledge & inventory accuracy; are good at execution of tasks; able to maintain transparency in documentation; are punctual and disciplined at work; respond positively to other assigned tasks and we appreciate you for that. We are expecting an increase in work knowledge and an improvement in warehouse capacity management from you which will foster your performance in the future.</p>" +
                                    "<p>The management wishes to confirm your employment with us as a " + Desig + " of the " + depart + " department under the " + dptdesc + " with an effective date from May 10, 2022.</p>" +
                                    "<p>Now that you are going to be even an integral part of the organization, we would expect greater efforts from you to strive to do better at work for ultimately setting the organization on the growth path.  We have complete faith in you. </p>" +
                                          "<p></p>" +
                                    "<p>Wishing you all the very best</p>" +
                                     "<p></p>" +
                                    "Regards," +
                                    "<p></p>" +
                                  "<p></p>" +
                                      "<p></p>" +

                                   "<p style='margin-bottom:-5px;display:inline;'><span style='border-top:1px solid; display:inline-block;margin-bottom:-11px;float-left'><strong>MR X</strong></span><span style='border-top:1px solid; display:inline-block;margin-bottom:-11px;float:right'><strong>MR X</strong></p></span></p>" +
                                     "<br>" +
                                   "<p style='margin-bottom:-5px;display:inline'><span style=' display:inline-block;margin-bottom:-11px;float-left'><strong>Senior Manager – HR</strong></span><span style='display:inline-block;margin-bottom:-11px;float:right;'><strong>Chief Operating Officer</strong></p></span></p>" +
                                   "<p></p>" +
                                   "<p style='display:inline-block;border-bottom:1px solid;margin-bottom:-11px;'>CC:</p>" +
                                   "<p style='margin-left:10px;margin-bottom:-11px;'>1.Personal file</p>" +
                                   "<p style='margin-left:10px;margin-bottom:-11px;'>2.Office file</p>";
                    }
                    break;
                //salary certificate
                case "10028":
                    if (this.GetCompCode() == "3354" || this.GetCompCode() == "3101")
                    {
                        lbody = "<p style='text-align:right;margin-bottom:-11px'>" + date + "</p>" +

                            "<div style='display:flex;justify-content:center'><h3 style='display:inline-block;border-bottom:1px solid;font-size:16px'>TO WHOM IT MAY CONCERN</h3></div>" +
                            "<p>This is to certify that " + name + " is a permanent employee of " + companme + ". His employee ID is " + idcard + " and serving as a " + Desig + ". <p/>" +

                                 //salary break down table
                                 "<p>His salary break down is as follows:</p>" +
                                 "<table style='width:70%;border-style:solid; border: 1px solid black;'><tr style='border-style:solid;border: 1px solid black;background:#B4C6E7;'><th style='width:50px;text-align:center;border-style:solid;border:1px solid black;'>SL</th><th>Particulars</th><th style='border-style:solid;border: 1px solid black;'>Amount in BDT</th></tr>" +
                                 "<tr style='border-style:solid;border: 1px solid black;background:#D9E1F2;'><td style='text-align:center;border-style:solid;border: 1px solid black;' colspan='3'><strong>Earnings</strong></td></tr>" +
                                 "<tr style='border-style:solid;border: 1px solid black;'><td style='text-align:center;border-style:solid;border: 1px solid black;'>1</td><td style='border-style:solid;border: 1px solid black;'>Basic (60% of Gross)</td><td style='text-align:right';color:red>" + bsal + "</td></tr>" +
                                 "<tr style='border-style:solid;border: 1px solid black;'><td style='text-align:center;border-style:solid;border: 1px solid black;'>2</td><td style='border-style:solid;border: 1px solid black;'>House Rent (30% of Gross)</td><td style='text-align:right;border-style:solid;border: 1px solid black;color:red'>" + hrent + "</td></tr>" +
                                 "<tr style='border-style:solid;border: 1px solid black;'><td style='text-align:center;border-style:solid;border: 1px solid black;'>3</td><td style='border-style:solid;border: 1px solid black;'>Medical Allowance (6% of Gross)</td><td style='text-align:right;border-style:solid;border: 1px solid black;color:red;'>" + mallow + "</td></tr>" +
                                 "<tr style='border-style:solid;border: 1px solid black;'><td style='text-align:center;border-style:solid;border: 1px solid black;'>4</td><td style='border-style:solid;border: 1px solid black;'>Conveyance Allowance (4% of Gross)</td><td style='text-align:right;color:red'>" + cven + "</td></tr>" +
                                 "<tr style='border-style:solid;border: 1px solid black;background:#D9E1F2;'><td style='text-align:center;border-style:solid;border: 1px solid black;' colspan='2'><strong>Total</strong></td><td style='text-align:right;border-style:solid;border:1px solid black;'><strong style='color:red;'>" + total + "</strong></td></tr>" +
                                 "<tr style='border-style:solid;border: 1px solid black;background:#D9E1F2;'><td style='text-align:center;border-style:solid;border: 1px solid black;' colspan='3'><strong>Deduction</strong></td></tr>" +
                                 "<tr style='border-style:solid;border: 1px solid black;'><td style='text-align:center;border-style:solid;border: 1px solid black;'>1</td><td style='border-style:solid;border: 1px solid black;'>Income Tax</td><td style='text-align:right;color:red'>" + incmtax + "</td></tr>" +
                                 "<tr style='border-style:solid;border: 1px solid black;'><td style='text-align:center;border-style:solid;border: 1px solid black;'>2</td><td style='border-style:solid;border: 1px solid black;'>Stamp</td><td style='text-align:center;border-style:solid;border: 1px solid black;'>-</td></tr>" +
                                 "<tr style='border-style:solid;border: 1px solid black;'><td style='text-align:center;border-style:solid;border: 1px solid black;'>3</td><td style='border-style:solid;border: 1px solid black;'>Others</td><td style='text-align:center;border-style:solid;border: 1px solid black;'>-</td></tr>" +
                                 "<tr style='border-style:solid;border: 1px solid black;'><td style='text-align:center;border-style:solid;border: 1px solid black;' colspan='2'><strong>Total Deduction</strong></td><td style='text-align: right;border-style:solid;border:1px solid black;'><strong style='color:red;'>" + incmtax + "</strong></td></tr>" +
                                 "<tr style='border-style:solid;border: 1px solid black;'><td style='text-align:center;border-style:solid;border: 1px solid black;' colspan='2'><strong>Payable Salay in Bank</strong></td><td style='text-align: right;border-style:solid;border:1px solid black;'><strong style='color:red;'>" + payablesal + "</strong></td></tr>" +
                                  "<tr style='border-style:solid;border: 1px solid black;'><td style='text-align:center;border-style:solid;border: 1px solid black;' colspan='2'><strong>Payable Salary in Cash</strong></td><td style='text-align:center;border-style:solid;border:1px solid black;'><strong style='color:red;'>-</strong></td></tr>" +

                                 "</table>" +

                                 "<p>In words: " + inwords2 + "</p>" +
                                       "<p></p>" +

                                  "<p><strong>Thanking you,</strong></p>" +
                                  "<p></p>" +
                                  "<p></p>" +
                                  "<p></p>" +

                                  "<p style='color:red';margin-bottom:-11px;><strong>Md. Mizanur Rahman Khan</strong></p>" +
                                  "<p style='color:red;margin-bottom:-11px;'>Senior Manager</p>" +
                                  "<p style='margin-bottom:-11px;'>Human Resources </p>" +
                                  "<p style='margin-bottom:-11px;'>" + comnam + "</p>" +
                                 "<p></p>" +
                                  "<p></p>" +
                                  "<p style='font-style:italic;font-weight:bold'>This certificate is issued to him for loan purpose on his specific request .</p>";


                    }
                    else
                    {
                        lbody = "<p style='text-align:right;margin-bottom:-11px'>" + date + "</p>" +

                "<div style='display:flex;justify-content:center'><h3 style='display:inline-block;border-bottom:1px solid;font-size:16px'>TO WHOM IT MAY CONCERN</h3></div>" +
                "<p>This is to certify that " + name + " is a permanent employee of " + companme + ". His employee ID is " + idcard + " and serving as a " + Desig + ". <p/>" +
                     //salary break down table


                     "<p>His salary break down is as follows:</p>" +
                     "<table style='width:70%;border-style:solid; border: 1px solid black;'><tr style='border-style:solid;border: 1px solid black;background:#B4C6E7;'><th style='width:50px;text-align:center;border-style:solid;border:1px solid black;'>SL</th><th>Particulars</th><th style='border-style:solid;border: 1px solid black;'>Amount in BDT</th></tr>" +
                     "<tr style='border-style:solid;border: 1px solid black;background:#D9E1F2;'><td style='text-align:center;border-style:solid;border: 1px solid black;' colspan='3'><strong>Earnings</strong></td></tr>" +
                     "<tr style='border-style:solid;border: 1px solid black;'><td style='text-align:center;border-style:solid;border: 1px solid black;'>1</td><td style='border-style:solid;border: 1px solid black;'>Basic (60% of Gross)</td><td style='text-align:right';color:red>" + bsal + "</td></tr>" +
                     "<tr style='border-style:solid;border: 1px solid black;'><td style='text-align:center;border-style:solid;border: 1px solid black;'>2</td><td style='border-style:solid;border: 1px solid black;'>House Rent (30% of Gross)</td><td style='text-align:right;border-style:solid;border: 1px solid black;color:red'>" + hrent + "</td></tr>" +
                     "<tr style='border-style:solid;border: 1px solid black;'><td style='text-align:center;border-style:solid;border: 1px solid black;'>3</td><td style='border-style:solid;border: 1px solid black;'>Medical Allowance (6% of Gross)</td><td style='text-align:right;border-style:solid;border: 1px solid black;color:red;'>" + mallow + "</td></tr>" +
                     "<tr style='border-style:solid;border: 1px solid black;'><td style='text-align:center;border-style:solid;border: 1px solid black;'>4</td><td style='border-style:solid;border: 1px solid black;'>Conveyance Allowance (4% of Gross)</td><td style='text-align:right;color:red'>" + cven + "</td></tr>" +
                     "<tr style='border-style:solid;border: 1px solid black;background:#D9E1F2;'><td style='text-align:center;border-style:solid;border: 1px solid black;' colspan='2'><strong>Total</strong></td><td style='text-align:right;border-style:solid;border:1px solid black;'><strong style='color:red;'>" + total + "</strong></td></tr>" +
                     "<tr style='border-style:solid;border: 1px solid black;background:#D9E1F2;'><td style='text-align:center;border-style:solid;border: 1px solid black;' colspan='3'><strong>Deduction</strong></td></tr>" +
                     "<tr style='border-style:solid;border: 1px solid black;'><td style='text-align:center;border-style:solid;border: 1px solid black;'>1</td><td style='border-style:solid;border: 1px solid black;'>Income Tax</td><td style='text-align:right;color:red'>" + incmtax + "</td></tr>" +
                     "<tr style='border-style:solid;border: 1px solid black;'><td style='text-align:center;border-style:solid;border: 1px solid black;'>2</td><td style='border-style:solid;border: 1px solid black;'>Stamp</td><td style='text-align:center;border-style:solid;border: 1px solid black;'>-</td></tr>" +
                     "<tr style='border-style:solid;border: 1px solid black;'><td style='text-align:center;border-style:solid;border: 1px solid black;'>3</td><td style='border-style:solid;border: 1px solid black;'>Others</td><td style='text-align:center;border-style:solid;border: 1px solid black;'>-</td></tr>" +
                     "<tr style='border-style:solid;border: 1px solid black;'><td style='text-align:center;border-style:solid;border: 1px solid black;' colspan='2'><strong>Total Deduction</strong></td><td style='text-align: right;border-style:solid;border:1px solid black;'><strong style='color:red;'>" + incmtax + "</strong></td></tr>" +
                     "<tr style='border-style:solid;border: 1px solid black;'><td style='text-align:center;border-style:solid;border: 1px solid black;' colspan='2'><strong>Payable Salay in Bank</strong></td><td style='text-align: right;border-style:solid;border:1px solid black;'><strong style='color:red;'>" + payablesal + "</strong></td></tr>" +
                      "<tr style='border-style:solid;border: 1px solid black;'><td style='text-align:center;border-style:solid;border: 1px solid black;' colspan='2'><strong>Payable Salary in Cash</strong></td><td style='text-align:center;border-style:solid;border:1px solid black;'><strong style='color:red;'>-</strong></td></tr>" +

                     "</table>" +

                     "<p>In words: " + inwords2 + "</p>" +
                           "<p></p>" +

                      "<p><strong>Thanking you,</strong></p>" +
                      "<p></p>" +
                      "<p></p>" +
                      "<p></p>" +

                      "<p style='color:red';margin-bottom:-11px;><strong>Md. Mizanur Rahman Khan</strong></p>" +
                      "<p style='color:red;margin-bottom:-11px;'>Senior Manager</p>" +
                      "<p style='margin-bottom:-11px;'>Human Resources </p>" +
                      "<p style='margin-bottom:-11px;'>" + comnam + "</p>" +
                     "<p></p>" +
                      "<p></p>" +
                      "<p style='font-style:italic;font-weight:bold'>This certificate is issued to him for loan purpose on his specific request .</p>";

                    }
                    //experience certificate
                    break;
                case "10029":
                    if (this.GetCompCode() == "3354")
                    {

                        lbody = "<p style='text-align:right;margin-bottom:-11px'><strong> " + date + "</strong></p>" +
                            "<p></p>" +
"<div style='display:flex;justify-content:center'><h3 style='display:inline-block;border-bottom:1px solid;font-size:16px'>TO WHOM IT MAY CONCERN</h3></div>" +
                             "<p>This is to certify that <strong> " + name + "</strong> Employee ID: " + idcard + ", worked as an “" + Desig + "” (July 1, 2021 up to May 10, 2022) at " + comnam + ". He is hereby released from the services of the company with an effective date of May 11, 2022. </p>" +
                             "<p></p>" +

                              "<p>We wish him all the best in his future endeavors.</p>" +
                               "<p></p>" +
                               "<p>Your Sincerely</p>" +
                               "<p></p>" +
                               "<p></p>" +

                  "<p style='margin-bottom:-11px;border-top:1px solid;display:inline-block'><strong>Md. Mizanur Rahman Khan</strong></p>" +
                 "<p style='margin-bottom:-11px;'>Senior Manager</p>" +
                 "<p style='margin-bottom:-11px;'>Human Resources </p>" +
                 "<p style='margin-bottom:-11px;'>" + comnam + "</p>";
                    }
                    else
                    {
                        lbody = "<p style='text-align:right;margin-bottom:-11px'><strong> " + date + "</strong></p>" +
                            "<p></p>" +
                             "<h3 style='text-align:center;font-weight:bold;border-bottom:1px solid;'>TO WHOM IT MAY CONCERN</h3>" +
                             "<p>This is to certify that <strong> " + name + "</strong> Employee ID: " + idcard + ", worked as an “" + Desig + "” (July 1, 2021 up to May 10, 2022) at " + comnam + ". He is hereby released from the services of the company with an effective date of May 11, 2022. </p>" +
                             "<p></p>" +

                              "<p>We wish him all the best in his future endeavors.</p>" +
                               "<p></p>" +
                               "<p>Your Sincerely</p>" +
                               "<p></p>" +
                               "<p></p>" +

                  "<p style='margin-bottom:-11px;border-top:1px solid;display:inline-block'><strong>Md. Mizanur Rahman Khan</strong></p>" +
                 "<p style='margin-bottom:-11px;'>Senior Manager</p>" +
                 "<p style='margin-bottom:-11px;'>Human Resources </p>" +
                 "<p style='margin-bottom:-11px;'>" + comnam + "</p>";
                    }

                    break;
                case "10030" :
                    if (comcod == "3370" || comcod == "3101")
                    {
                        lbody = "<p style='text-align: center;'><strong >CLEARANCE FORM</strong> </p>" +
                             "<p style='margin-bottom:-11px;'>Name &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;" + name + "</p>" +

                        "<p style='margin-bottom:-5px;'><span text-align:left;> Designation :&nbsp;" + Desig + "</span>" +
                        "<span style='margin-bottom:-5px;text-align:center;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Department: &nbsp;&nbsp;" + depart + "</span></p>" +
                        "<p style='margin-bottom:-5px;'>Responsibility Handover To :&nbsp;</p>" +
                        "<p style='margin-bottom:-5px;'><u>Company Name: [Please</u><strong>click</strong> (√)]</p>" +
                        "<p style='margin-top:10px;'><input type='checkbox' /> CA PROPERTY DEVELOPMENT LTD <input type='checkbox' style='margin-bottom:2px;' /> CPDL Ltd<input type='checkbox' style='margin-bottom:2px;' />PARAGONTECH LTD.<input type='checkbox' style='margin-bottom:2px;' />NEO SOUL" +
                        "<p style='margin-bottom:-5px;'>I do hereby declare that, on acceptance of my Resignation/Termination/Dismiss/Discharge from service by the" +
                        "management with effect from Effect Date, company has no claim on me </p>" +
                        "<br>" +
                        "<p style='margin-bottom:-11px;text-align:left;'>------------------------------------------</p><p>" +
                        "<p style='margin-bottom:-11px;margin-left:50px;text-align: left;'><strong>Signature</strong>" +

                        "<span style='margin-bottom:-11px;text-align: right;'> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Date: &nbsp;&nbsp;/ &nbsp;&nbsp;/</span></p>" +
                         "<br>" +
                        "<p style='text-align: center;'><strong>Departmental Clearance Remarks & Signature for final Settlement </strong> </p>" +
                  "<table style='width:90%;border-style:solid; border: 1px solid black;'><tr style='border-style:solid;border: 1px solid black;background:#B4C6E7;'><th style='width:40%;border-style:solid;border: 1px solid black;'>Department/Section </th><th style='width:40%;border-style:solid;border: 1px solid black;'>Particulars</th><th style='border-style:solid;border: 1px solid black;'>Outstanding</th><th style='border-style:solid;border: 1px solid black;'>NotOutstanding</th><th style='border-style:solid;border: 1px solid black;'>Signature</th></tr>" +

                    "<tr style='border-style:solid;border: 1px solid black;'><td style='text-align:left;border-style:solid;border: 1px solid black;'>RESPECTIVE DEPARTMENT</td><td style='border-style:solid;border: 1px solid black;'></td><td style='text-align:right;border-style:solid;border: 1px solid black;'></td><td style='text-align:right;border-style:solid;border: 1px solid black;'></td><td style='text-align:right;border-style:solid;border: 1px solid black;'></td></tr>" +
                    "<tr style='border-style:solid;border: 1px solid black;'><td style='text-align:left;border-style:solid;border: 1px solid black;'>HTALENT MANAGEMENT</td><td style='border-style:solid;border: 1px solid black;'>ID Card:<br>Loan issue :<br> Others:</td><td style='border-style:solid;border: 1px solid black;'></td><td style='text-align:right;border-style:solid;border: 1px solid black;'></td></tr>" +
                     "<tr style='border-style:solid;border: 1px solid black;'><td style='text-align:left;border-style:solid;border: 1px solid black;'>LOGISTIC MANAGEMENT</td><td style='border-style:solid;border: 1px solid black;'>P. Number :<br>Unadjusted M. Bill:<br>SIM Card:<br>Stationery :</td><td style='text-align:right;border-style:solid;border: 1px solid black;'></td><td style='text-align:right;border-style:solid;border: 1px solid black;'></td></tr>" +
                    "<tr style='border-style:solid;border: 1px solid black;'><td style='text-align:left;border-style:solid;border: 1px solid black;'>MIS/IT</td><td style='border-style:solid;border: 1px solid black;'>Hardware:<br>Email ID:<br>What’s app Group:</td><td style='text-align:right;border-style:solid;border: 1px solid black;'></td><td style='text-align:right;border-style:solid;border: 1px solid black;'></td></tr>" +

                    "<tr style='border-style:solid;border: 1px solid black;'><td style='text-align:left;border-style:solid;border: 1px solid black;'>ENGINEERING/ iNNSTUDIO/BOQ /PARAGONTECH</td><td style='border-style:solid;border: 1px solid black;'></td><td style='text-align:right;color:red'></td><td style='text-align:right;border-style:solid;border: 1px solid black;'></td></tr>" +
                     "<tr style='border-style:solid;border: 1px solid black;'><td style='text-align:left;border-style:solid;border: 1px solid black;'>ELECTRO MECHANICAL</td><td style='border-style:solid;border: 1px solid black;'></td><td style='text-align:center;border-style:solid;border: 1px solid black;'></td><td style='text-align:right;border-style:solid;border: 1px solid black;'></td></tr>" +
                     "<tr style='border-style:solid;border: 1px solid black;'><td style='text-align:left;border-style:solid;border: 1px solid black;'>ACCOUNTS & FINANCE</td><td style='border-style:solid;border: 1px solid black;'>Unadjusted Amount:</td><td style='text-align:center;border-style:solid;border: 1px solid black;'></td><td style='text-align:right;border-style:solid;border: 1px solid black;'></td><td style='text-align:right';></td></tr>" +
                    "<tr style='border-style:solid;border: 1px solid black;'><td style='text-align:left;border-style:solid;border: 1px solid black;'>SUPPLY CHAIN MANAGEMENT</td><td style='border-style:solid;border: 1px solid black;'></td><td style='text-align:center;border-style:solid;border: 1px solid black;'></td><td style='text-align:right;border-style:solid;border: 1px solid black;'></td></tr>" +
                     "<tr style='border-style:solid;border: 1px solid black;'><td style='text-align:left;border-style:solid;border: 1px solid black;'>PROPERTY SOLUTION</td><td style='border-style:solid;border: 1px solid black;'></td><td style='text-align:center;border-style:solid;border: 1px solid black;'></td><td style='text-align:right;border-style:solid;border: 1px solid black;'></td></tr>" +
                     "<tr style='border-style:solid;border: 1px solid black;'><td style='text-align:left;border-style:solid;border: 1px solid black;'>HAPPINESS TEAM</td><td style='border-style:solid;border: 1px solid black;'></td><td style='text-align:center;border-style:solid;border: 1px solid black;'></td><td style='text-align:right;border-style:solid;border: 1px solid black;'></td></tr>" +
                     "<tr style='border-style:solid;border: 1px solid black;'><td style='text-align:left;border-style:solid;border: 1px solid black;'>OTHERS</td><td style='border-style:solid;border: 1px solid black;'></td><td style='text-align:center;border-style:solid;border: 1px solid black;'></td><td style='text-align:right;border-style:solid;border: 1px solid black;'></td></tr>" +

                     "</table>" +
                 "<br>" +
                        "<p style='margin-bottom:-11px;text-align: right;margin-right:20px;'>----------------------------------------------------</p><p>" +
                         "<p style='margin-bottom:-11px;text-align: right; margin-right:50px;'> Chief Operating Officer </p><p>" +
                        "<br>" +
                         "<br>" +
                          "<br>" +
                        "<p style='text-align: center;'><strong ><u>PERSONNEL EXIT REPORT</u></strong> </p>" +
                        "<br>" +



                         "<p style='margin-bottom:-5px;'><span text-align:left;> Name &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;: " + name + "</span>" +
                         "<span style='margin-bottom:-5px;text-align:center;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Department&nbsp;:" + depart + "</span></p>" +
                         "<p style='margin-bottom:-5px;'>Designation :&nbsp;</p>" +
                         "<p style='margin-bottom:-5px;'><span text-align:left;>Joining Date :&nbsp;" + jdate + "</span>" +
                         "<span style='margin-bottom:-5px;text-align:center;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Personal Mobile: &nbsp;&nbsp;" + empMobile + "</span></p>" +

                     "<br>" +
                        "<p style='margin-bottom:-5px;'><Strong>The Personel: click (√) the type of left</p>" +

                        "<br>" +
                        "<table style='width:90%;border-style:solid; border: 1px solid black;'><tr style='border-style:solid;border: 1px solid black;'><th style='width:140px;border-style:solid;border: 1px solid black;background:#B4C6E7;'>Types of Left</th><th style='border-style:solid;border: 1px solid black;'>Application Date</th><th style='border-style:solid;border: 1px solid black;'>Effected Date</th></tr>" +

                   "<tr style='border-style:solid;border: 1px solid black;'><td style='text-align:left;border-style:solid;border: 1px solid black;background:#B4C6E7;'>Resigned</td><td style='border-style:solid;border: 1px solid black;'></td><td style='text-align:right;border-style:solid;border: 1px solid black;'></td></tr>" +
                   "<tr style='border-style:solid;border: 1px solid black;'><td style='text-align:left;border-style:solid;border: 1px solid black;background:#B4C6E7;'>Termination</td><td style='text-align:center;border-style:solid;border: 1px solid black;'>-</td><td style='text-align:center;border-style:solid;border: 1px solid black;'>-</td></tr>" +
                    "<tr style='border-style:solid;border: 1px solid black;'><td style='text-align:left;border-style:solid;border: 1px solid black;background:#B4C6E7;'>Discharged</td><td style='text-align:center;border-style:solid;border: 1px solid black;'>-</td><td style='text-align:center;border-style:solid;border: 1px solid black;'>-</td></tr>" +
                   "<tr style='border-style:solid;border: 1px solid black;'><td style='text-align:left;border-style:solid;border: 1px solid black;background:#B4C6E7;'>Dismiss</td><td style='text-align:center;border-style:solid;border: 1px solid black;'>-</td><td style='text-align:center;border-style:solid;border: 1px solid black;'>-</td></tr>" +

                    "</table>" +
                     "<br>" +

                         "<p style='margin-bottom:-5px;'><span style='text-align:left;'><strong>Reasons for Resign &nbsp; : As mention his/her resignation letter</strong>" +
                            "<br>" +
                             "<br>" +
                        "<p style='margin-bottom:-11px;text-align:Right;'>--------------------------</p><p>" +
                        "<p style='margin-bottom:-11px;margin-left:80px;text-align:Right;'><strong>Signature</strong></p>" +

                        "<br>" +
                        "<p style='text-align: center;'><strong ><u>Fill up by Talent Management</u></strong> </p>" +
                        

                         "<p style='margin-bottom:-5px;text-align:left;'><span>Is re-employment recommended?&nbsp;Yes  <input type='checkbox' />&nbsp; No  <input type='checkbox' /></p> " +
                         "<p style='margin-bottom:-5px;text-align:left;'><span>If No state reason: </p>" +


                        "<br>" +
                        "<p style='margin-bottom:-11px;text-align: left;margin-right:20px;'>Signed----------------------------------------------------</p>" +
                        "<p style='margin-bottom:-11px;text-align: right;margin-right:50px;'>Date:</p>" +
                        "<p style='margin-bottom:-11px;text-align: left;margin-right:20px;'>Name----------------------------------------------------</p>" +
                        "<br>" +
                        "<p style='margin-bottom:-1px;text-align: left;margin-right:20px;'>Designation-----------------------------------------------------</p>" +
                        "<br>" +
                        "<br>" +
                        "<br>" +
                        "<br>" +
                        "<br>" +
                        "<br>" +
                        "<br>" +
                        "<br>" +
                        "<br>" +
                        "<br>" +
                        "<br>" +


                       "<p style='text-align: center;'><strong ><u>EXIT INTERVIEW QUESTIONNAIRES</u></strong> </p>" +
                      
                       "<p style='margin-bottom:-5px;text-align:left;'><span>01. How long have you been served in CPDL:-</p>" +
                       "<p style='margin-bottom:-5px;text-align:left;'><span>02. How did you enjoy your job in CPDL?</p>" +
                       "<ul><li style='list-style-type:none;'>a) Very satisfied</li><li style='list-style-type:none;'>b) Satisfied</li><li style='list-style-type:none;'>c) Neutral</li><li style='list-style-type:none;'>d) Dissatisfied</li></ul>" +

                       "<p style='margin-bottom:-5px;text-align:left;'><span>03. How did you feel the working environment of CPDL?</p>" +
                       "<ul><li style='list-style-type:none;'>a) Very satisfied</li><li style='list-style-type:none;'>b) Satisfied</li><li style='list-style-type:none;'>c) Neutral</li><li style='list-style-type:none;'>d) Dissatisfied</li></ul>" +

                       "<p style='margin-bottom:-5px;text-align:left;'><span>04. Are you satisfied with the appreciation or reward system provided by CPDL Management?</p>" +
                       "<ul><li style='list-style-type:none;'>a) High Satisfied</li><li style='list-style-type:none;'>b) Satisfied</li><li style='list-style-type:none;'>c) Needs Improvement.</li></ul>" +

                        "<p style='margin-bottom:-5px;text-align:left;'><span>05. Are you satisfied with bonus and incentives given?</p>" +
                       "<ul><li style='list-style-type:none;'>a) High Satisfied</li><li style='list-style-type:none;'>b) Satisfied</li><li style='list-style-type:none;'>c) Needs Improvement.</li></ul>" +

                        "<p style='margin-bottom:-5px;text-align:left;'><span>06. Did you feel that there is an overall employee satisfaction in CPDL?</p>" +
                        "<ul><li style='list-style-type:none;'>a) Yes</li><li style='list-style-type:none;'>b) No</li></ul>" +

                        "<p style='margin-bottom:-5px;text-align:left;'><span>07. What is your opinion about the smooth relationship with CPDL Management and Members?</p>" +
                        "<ul><li style='list-style-type:none;'>a) Very High</li><li style='list-style-type:none;'>b) High</li><li style='list-style-type:none;'>c) Medium</li><li style='list-style-type:none;'>d) Low</li></ul>" +

                         "<p style='margin-bottom:-5px;text-align:left;'><span>08. What is our opinion about the level of Personnel career development system in CPDL based on arranged " +
                         "training program, workshop &others programs ?</p>" +
                        "<ul><li style='list-style-type:none;'>a) Excellent</li><li style='list-style-type:none;'>b) Very good</li><li style='list-style-type:none;'>c) Average</li><li style='list-style-type:none;'>d) Needs to improvement.</li></ul>" +

                        "<p style='margin-bottom:-5px;text-align:left;'><span>09. Are you motivated during your services period by company’s existing rules and regulation?</p>" +
                        "<ul><li style='list-style-type:none;'>a) Highly Motivated</li><li style='list-style-type:none;'>b) Neutral </li><li style='list-style-type:none;'>c) Needs to improvement.</li></ul>" +

                        "<p style='margin-bottom:-5px;text-align:left;'><span>10. What changes or policy implementation will you recommend to HRD to satisfy the Personnel? </p>" +
                         "<p style='margin-bottom:-5px;text-align:left;'>Yes <input type='checkbox' /> No <input type='checkbox' style='margin-bottom:2px;'/> </p>" +
                        "<br>"+
                        "<p style='margin-bottom:-5px;text-align:left;'>If Yes: ------------------------------------------------------------------------------------------------------------" +
                        "-------------------------------------------------------------------------------------------------------------------------------------------------------------------" +
                        "-------------------------------------------------------------------------------------------------------------------------------------------------------------------- </p>"+
                        "<br>" +
                        "<p style='margin-bottom:-11px;text-align:left;'>------------------------------------------" +
                        "<span style='margin-bottom:-11px;text-align:right;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Date:</span></p>" +
                        "<p style='margin-bottom:-11px;margin-left:30px;text-align: left;'><strong>Signature of Personnel</strong></p>";

                    }
                    else
                    {
                        lbody = "<div class='printHeader'><p>Ref: SPL/HR/Appt/16/524</p><p>" + cdate + "</p><p><strong>" + name + "</strong></p><p><strong>C/O: Abdul Bashar&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</strong></p><p><strong>Vill: Boro Hossainput, PO: Banglabazar-3822,</strong></p><p><strong>PS: Begumganj, Dist: Noakhali&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</strong></p><p>&nbsp;</p><p><strong>Subject: <u>Confirmation of Service.</u></strong></p><p>&nbsp;</p> <p></br></br></br><br></br></br></br></p><p>Dear <strong>" + name + "</strong>,</p><p>This has reference to your appointment letter.</p><p>We are pleased to inform you that, on successful completion of your period of probation, your services in Star Paradise Ltd have been confirmed effective from 3 November, 2016. </p><p>We hope that you will extend your full support and cooperation to promote company activities and growth in the days to come.</p>Yours faithfully,<p><p class='pImage'><strong><img src='data:Image/png;base64," + usersign + "' width='200px' height='80px' ></img></p>  <p class='pUname'><strong>" + uname + "</p> <p class='pUname'><strong>" + usrdesig + "</strong></p>";
                    }
                    break;
                default:
                    break;
            }
            return lbody;
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            this.txtml.Text = "";
            if (chkpre.Checked)
            {
                this.PreviousD();
            }
            else
            {
                this.ShowView();
            }
        }


        private void ShowView()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            this.txtml.Text = this.data(type);
        }

        private void PreviousD()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            string comcod = this.GetCompCode();
            string empid = this.ddlPrevious.SelectedValue.ToString();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLETTER", empid, type, "", "", "", "", "", "", "");
            if (ds3.Tables[0].Rows.Count == 0)
                return;
            this.ddlEmployee.Items.Clear();
            this.ddlEmployee.DataTextField = "empname";
            this.ddlEmployee.DataValueField = "empid";
            this.ddlEmployee.DataSource = ds3.Tables[0];
            this.ddlEmployee.DataBind();

            string lett = (string)ds3.Tables[0].Rows[0]["LETTDESC"];
            this.txtml.Text = lett;
            ViewState["letter"] = ds3.Tables[0];
        }



        protected void btnSendLetter_Click(object sender, EventArgs e)
        {
            string letterType = this.Request.QueryString["Type"].ToString().Trim();
            string advno = this.Request.QueryString["advno"].ToString().Trim();
            string comcod = this.GetCompCode();
            string msg = "";
            if (letterType == "10003")
            {
                bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_NEW_REC", "UPDTLETSTATUS", "true", "false", "false", advno, "", "");
                if (result)
                {
                    msg = "Updated success";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);

                }
            }
            else if (letterType == "10002")
            {
                bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_NEW_REC", "UPDTLETSTATUS", "true", "true", "false", advno, "", "");
                if (result)
                {
                    msg = "Updated success";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);

                }
            }
            else if (letterType == "10025")
            {
                bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_NEW_REC", "UPDTLETSTATUS", "true", "true", "true", advno, "", "");
                if (result)
                {
                    msg = "Updated success";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);

                }
            }
        }
    }
}