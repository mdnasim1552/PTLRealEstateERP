using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RealERPLIB;
using RealERPRPT;

namespace RealERPWEB.F_81_Hrm.F_92_Mgt
{
    public partial class InterfaceHR1 : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.txtdate_TextChanged(null, null);

            ((Label)this.Master.FindControl("lblTitle")).Text = "HR Interface";

            this.txtdate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
        }

        protected void txtdate_TextChanged(object sender, EventArgs e)
        {
            this.SaleRequRpt();
            this.RadioButtonList1_SelectedIndexChanged(null, null);
        }

        private void SaleRequRpt()
        {
            string comcod = this.GetCompCode();
            // string Date = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = accData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPENDINGCOUNT", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;






            this.RadioButtonList1.Items[0].Text = "<a href='../LetterDefault.aspx?Type=10003&Entry=Offer+Letter+For+General' target='_blank'> Offer Letter" + "<span class=lbldata>" + (string)ds1.Tables[0].Rows[0]["val2"] + "</span></a>";//10002
            this.RadioButtonList1.Items[1].Text = "<a href='../LetterDefault.aspx?Type=10003&Entry=Offer+Letter+For+General' target='_blank'> Appointment Letter" + "<span class=lbldata>" + (string)ds1.Tables[0].Rows[0]["val2"] + "</span></a>";//10002

            this.RadioButtonList1.Items[1].Text = "<a href='../../LetterDefault.aspx?Type=10020&Entry=Joining Report' target='_blank'>Joining Report" + "<span class=lbldata>" + (string)ds1.Tables[0].Rows[0]["val22"] + "</span></a>";//joinint10020

            this.RadioButtonList1.Items[2].Text = "Extension Letter" + "<span class=lbldata>" + (string)ds1.Tables[0].Rows[0]["val6"] + "</span>";   //10006
            this.RadioButtonList1.Items[3].Text = "Promotion Letter" + "<span class=lbldata>" + (string)ds1.Tables[0].Rows[0]["val7"] + "</span>";          //10007
            this.RadioButtonList1.Items[4].Text = "Increment Letter" + "<span class=lbldata>" + (string)ds1.Tables[0].Rows[0]["val8"] + "</span>";   //10008
            this.RadioButtonList1.Items[5].Text = "Transfer Proposal" + "<span class=lbldata>" + (string)ds1.Tables[0].Rows[0]["val1"] + "</span>";
            this.RadioButtonList1.Items[6].Text = "Transfer Letter" + "<span class=lbldata>" + (string)ds1.Tables[0].Rows[0]["val9"] + "</span>";   //10009 
            this.RadioButtonList1.Items[7].Text = "Release Letter" + "<span class=lbldata>" + (string)ds1.Tables[0].Rows[0]["val11"] + "</span>";    //10011
            this.RadioButtonList1.Items[8].Text = "<a href='../../LetterDefault.aspx?Type=10012&Entry=Experience Certificate for current employees' target='_blank'>Experience Certificate" + "<span class=lbldata>" + (string)ds1.Tables[0].Rows[0]["val12"] + "</span></a>"; //10012
            this.RadioButtonList1.Items[9].Text = "<a href='../../LetterDefault.aspx?Type=10015&Entry=Salary Certificate for current employees' target='_blank'>Salary Certificate" + "<span class=lbldata>" + (string)ds1.Tables[0].Rows[0]["val14"] + "</span></a>";
            this.RadioButtonList1.Items[10].Text = "<a href='../F_89_Pay/RpHRtPayroll.aspx?Type=Salary&Entry=Payroll' target='_blank'> Salary Sheet" + "<span class=lbldata>" + (string)ds1.Tables[0].Rows[0]["val1"] + "</span></a>";

            this.RadioButtonList1.Items[11].Text = "<a href='../../LetterDefault.aspx?Type=10020&Entry=Confirmation Letter' target='_blank'>Confermation Letter" + "<span class=lbldata>" + (string)ds1.Tables[0].Rows[0]["val22"] + "</span></a>";//joinint10020

            this.RadioButtonList1.Items[12].Text = "<a href='../../LetterDefault.aspx?Type=10021&Entry=Joining Letter' target='_blank'>Joining Letter" + "<span class=lbldata>" + (string)ds1.Tables[0].Rows[0]["val22"] + "</span></a>";//joinint10020
            this.RadioButtonList1.Items[13].Text = "<a href='../../LetterDefault.aspx?Type=10022&Entry=Certification' target='_blank'>Certification Of Employee" + "<span class=lbldata>" + (string)ds1.Tables[0].Rows[0]["val22"] + "</span></a>";//joinint10020
            this.RadioButtonList1.Items[14].Text = "<a href='../../LetterDefault.aspx?Type=10023&Entry=Termination Letter' target='_blank'>Employee Termination Letter" + "<span class=lbldata>" + (string)ds1.Tables[0].Rows[0]["val22"] + "</span></a>";//joinint10020
            this.RadioButtonList1.Items[15].Text =
                "<a href='../F_81_Rec/CreateOfferLt.aspx?Type=OLCreate' target='_blank'>Create Offer Letter" +
                "<span class=lbldata>" + (string)ds1.Tables[0].Rows[0]["val22"] + "</span></a>";
            this.RadioButtonList1.Items[16].Text =
               "<a href='../F_81_Rec/EmpAssessment.aspx?Type=AssCreate' target='_blank'>Employee Assessment" +
               "<span class=lbldata>" + (string)ds1.Tables[0].Rows[0]["val22"] + "</span></a>";
            this.RadioButtonList1.Items[17].Text =
               "<a href='../F_81_Rec/ConfirmLetter.aspx?Type=Confmletter' target='_blank'>Confirmation Letter" +
               "<span class=lbldata>" + (string)ds1.Tables[0].Rows[0]["val22"] + "</span></a>";




            //this.RadioButtonList1.Items[0].Text = "M. Requsition Approval " + "<span class='lbldata'>" + (string)ds1.Tables[0].Rows[0]["val15"] + "</span>";//

            //this.RadioButtonList1.Items[1].Text = "<a href='../../F_81_Hrm/F_81_Rec/ShortListing.aspx?Type=SList' target='_blank'> Sort Listing" + "<span class='lbldata'>" + (string)ds1.Tables[0].Rows[0]["val16"] + "</span></a>";//
            //this.RadioButtonList1.Items[2].Text = "<a href='../../F_81_Hrm/F_81_Rec/ShortListing.aspx?Type=IResult' target='_blank'>Interview Result Input" + "<span class='lbldata'>" + (string)ds1.Tables[0].Rows[0]["val17"] + "</span></a>";//
            //this.RadioButtonList1.Items[3].Text = "<a href='../../LetterDefault.aspx?Type=10003&Entry=Offer Letter For General' target='_blank'>Offer Letter" + "<span class='lbldata'>" + (string)ds1.Tables[0].Rows[0]["val3"] + "</span></a>";//10003
            //this.RadioButtonList1.Items[4].Text = "Offer Approved" + "<span class=lbldata>" + (string)ds1.Tables[0].Rows[0]["val19"] + "</span></a>";//10003
            //this.RadioButtonList1.Items[5].Text = "<a href='../../LetterDefault.aspx?Type=10002&Entry= Appoinment Latter for employees' target='_blank'> Appointment Letter" + "<span class=lbldata>" + (string)ds1.Tables[0].Rows[0]["val2"] + "</span></a>";//10002
            //this.RadioButtonList1.Items[6].Text = "Appointment Approved" + "<span class=lbldata>" + (string)ds1.Tables[0].Rows[0]["val20"] + "</span></a>";//10002
            //this.RadioButtonList1.Items[7].Text = "<a href='../../LetterDefault.aspx?Type=10020&Entry=Joining Report' target='_blank'>Joining Report" + "<span class=lbldata>" + (string)ds1.Tables[0].Rows[0]["val22"] + "</span></a>";//joinint10020

            //this.RadioButtonList1.Items[8].Text = "Extension Letter" + "<span class=lbldata>" + (string)ds1.Tables[0].Rows[0]["val6"] + "</span>";   //10006
            //this.RadioButtonList1.Items[9].Text = "Promotion Letter" + "<span class=lbldata>" + (string)ds1.Tables[0].Rows[0]["val7"] + "</span>";          //10007
            //this.RadioButtonList1.Items[10].Text = "Increment Letter" + "<span class=lbldata>" + (string)ds1.Tables[0].Rows[0]["val8"] + "</span>";   //10008
            //this.RadioButtonList1.Items[11].Text = "Transfer Proposal" + "<span class=lbldata>" + (string)ds1.Tables[0].Rows[0]["val1"] + "</span>";
            //this.RadioButtonList1.Items[12].Text = "Transfer Letter" + "<span class=lbldata>" + (string)ds1.Tables[0].Rows[0]["val9"] + "</span>";   //10009     
            //this.RadioButtonList1.Items[13].Text = "Acceptance of Registration" + "<span class=lbldata>" + (string)ds1.Tables[0].Rows[0]["val10"] + "</span>";  //10010
            //this.RadioButtonList1.Items[14].Text = "Release Letter" + "<span class=lbldata>" + (string)ds1.Tables[0].Rows[0]["val11"] + "</span>";    //10011
            //this.RadioButtonList1.Items[15].Text = "<a href='../../LetterDefault.aspx?Type=10012&Entry=Experience Certificate for current employees' target='_blank'>Experience Certificate" + "<span class=lbldata>" + (string)ds1.Tables[0].Rows[0]["val12"] + "</span></a>"; //10012
            //this.RadioButtonList1.Items[16].Text = "<a href='../../LetterDefault.aspx?Type=10015&Entry=Salary Certificate for current employees' target='_blank'>Salary Certificate" + "<span class=lbldata>" + (string)ds1.Tables[0].Rows[0]["val14"] + "</span></a>";
            //this.RadioButtonList1.Items[17].Text = "<a href='../F_89_Pay/RpHRtPayroll.aspx?Type=Salary&Entry=Payroll' target='_blank'> Salary Sheet" + "<span class=lbldata>" + (string)ds1.Tables[0].Rows[0]["val1"] + "</span></a>";

            //this.RadioButtonList1.Items[18].Text = "<a href='../../LetterDefault.aspx?Type=10020&Entry=Confermation Letter' target='_blank'>Confermation Letter" + "<span class=lbldata>" + (string)ds1.Tables[0].Rows[0]["val22"] + "</span></a>";//joinint10020


        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            // this.lblprintstkl.Text = "";
            string type = this.RadioButtonList1.SelectedValue.ToString();

            string comcod = this.GetCompCode();
            // string Date = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = accData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLETTERBYTYPE", type, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["ldata"] = ds1;
            switch (type)
            {
                case "13":
                    this.dataBind2();
                    this.RadioButtonList1.Items[0].Attributes["style"] = "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";
                    break;
                case "14":
                    this.dataBind2();
                    this.RadioButtonList1.Items[1].Attributes["style"] = "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";
                    break;
                case "15":
                    this.datafselect();
                    this.RadioButtonList1.Items[2].Attributes["style"] = "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";
                    break;
                case "10003":
                    this.dataBind3();
                    this.RadioButtonList1.Items[3].Attributes["style"] = "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";
                    break;
                case "1000301":
                    this.dataBind3();
                    this.RadioButtonList1.Items[4].Attributes["style"] = "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";
                    break;
                //case "10004":
                //    this.dataBind();
                //    this.RadioButtonList1.Items[3].Attributes["style"] = "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";
                //    break;
                case "10002":
                    this.dataBind();
                    this.RadioButtonList1.Items[5].Attributes["style"] = "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";
                    break;
                case "16000":
                    this.dataBind3();
                    this.RadioButtonList1.Items[6].Attributes["style"] = "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";
                    break;

                case "10005":
                    this.dataBind3();
                    this.RadioButtonList1.Items[7].Attributes["style"] = "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";
                    break;
                case "10006":
                    this.dataBind();
                    this.RadioButtonList1.Items[8].Attributes["style"] = "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";
                    break;
                case "10007":
                    this.dataBind();
                    this.RadioButtonList1.Items[9].Attributes["style"] = "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";
                    break;
                case "10008":
                    this.dataBind();
                    this.RadioButtonList1.Items[10].Attributes["style"] = "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";
                    break;
                case "8":
                    this.dataBind();
                    this.RadioButtonList1.Items[11].Attributes["style"] = "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";
                    break;
                case "10009":
                    this.dataBind();
                    this.RadioButtonList1.Items[12].Attributes["style"] = "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";
                    break;

                case "10010":
                    this.dataBind();
                    this.RadioButtonList1.Items[13].Attributes["style"] = "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";
                    break;
                case "10011":
                    this.dataBind();
                    this.RadioButtonList1.Items[14].Attributes["style"] = "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";
                    break;
                case "10012":
                    this.dataBind();
                    this.RadioButtonList1.Items[13].Attributes["style"] = "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";
                    break;

                case "9":
                    this.dataBind();
                    this.RadioButtonList1.Items[16].Attributes["style"] = "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";
                    break;
                case "16":
                    this.dataBind();
                    this.RadioButtonList1.Items[16].Attributes["style"] = "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";
                    break;
                case "17":
                    this.dataBind();
                    this.RadioButtonList1.Items[16].Attributes["style"] = "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";
                    break;
                case "18":
                    this.dataBind();
                    this.RadioButtonList1.Items[16].Attributes["style"] = "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";
                    break;
            }
        }

        protected void dataBind()
        {
            this.gvappinfo.Visible = false;
            this.gvselect.Visible = false;
            this.gvfselect.Visible = false;
            var ds1 = (DataSet)ViewState["ldata"];
            this.gvSalesUpdate.DataSource = null;
            this.gvSalesUpdate.DataSource = ds1.Tables[0];
            this.gvSalesUpdate.DataBind();
            this.gvSalesUpdate.Visible = true;
        }
        protected void dataBind3()
        {
            this.gvSalesUpdate.Visible = false;
            this.gvappinfo.Visible = false;
            this.gvfselect.Visible = false;
            var ds1 = (DataSet)ViewState["ldata"];
            this.gvselect.DataSource = null;
            this.gvselect.DataSource = ds1.Tables[0];
            this.gvselect.DataBind();
            this.gvselect.Visible = true;

        }

        protected void datafselect()
        {
            string comcod = this.GetCompCode();
            this.gvSalesUpdate.DataSource = null;
            this.gvSalesUpdate.Visible = false;
            this.gvselect.Visible = false;
            this.gvappinfo.Visible = false;
            //  string frmdate = new DateTime(DateTime.Today.Year, 01, 01).ToString("dd-MMM-yyyy");
            // string todate = DateTime.Today.ToString("dd-MMM-yyyy");
            DataSet ds2 = accData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ADVERTISEMENT", "GETFORFINALSELECTION", "", "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
            this.gvfselect.DataSource = ds2.Tables[0];
            this.gvfselect.DataBind();
            this.gvfselect.Visible = true;

        }


        protected void dataBind2()
        {
            string comcod = this.GetCompCode();
            this.gvSalesUpdate.DataSource = null;
            this.gvfselect.Visible = false;
            this.gvSalesUpdate.Visible = false;
            this.gvselect.Visible = false;
            string frmdate = new DateTime(DateTime.Today.Year, 01, 01).ToString("dd-MMM-yyyy");
            string todate = DateTime.Today.ToString("dd-MMM-yyyy");
            DataSet ds2 = accData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETJOBADVERTISEMENT", frmdate, todate, "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
            this.gvappinfo.DataSource = this.HiddenSameData(ds2.Tables[0]);
            this.gvappinfo.DataBind();
            this.gvappinfo.Visible = true;

        }


        protected void gvSalesUpdate_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnPrintIN");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnApp");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string type = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "TYPE")).ToString();
                string empid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "EMPID")).ToString();
                //  string text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "memono1")).ToString();
                //  string date = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "memodat")).ToString("dd-MMM-yyyy");
                hlink2.NavigateUrl = "~/LetterDefault.aspx?Type=" + type + "&Entry=Apprv&ID=" + empid;
                hlink2.Target = "_blank";
                hlink1.NavigateUrl = "~/LetterDefault.aspx?Type=" + type + "&Entry=Apprv&ID=" + empid;
                hlink1.Target = "_blank";
            }
        }


        protected void gvfselect_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkok");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string advno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "advno")).ToString();
                string postdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "postdesc")).ToString();

                hlink1.NavigateUrl = "../F_81_Rec/ShortListing.aspx?Type=Fselection" + "&advno=" + advno + "&postdesc=" + postdesc;
                hlink1.Target = "_blank";
            }
        }



        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string advno = dt1.Rows[0]["advno"].ToString();
            string deptcode = dt1.Rows[0]["deptcode"].ToString();
            string postcode = dt1.Rows[0]["postcode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["advno"].ToString() == advno && dt1.Rows[j]["deptcode"].ToString() == deptcode && dt1.Rows[j]["postcode"].ToString() == postcode)
                {
                    dt1.Rows[j]["advno1"] = "";
                    dt1.Rows[j]["advdat1"] = "";
                    dt1.Rows[j]["refno"] = "";

                    dt1.Rows[j]["company"] = "";
                    dt1.Rows[j]["deptdesc"] = "";
                    dt1.Rows[j]["postdesc"] = "";
                    dt1.Rows[j]["jobsource"] = "";
                }
                else
                {

                    if (dt1.Rows[j]["advno"].ToString() == advno)
                    {
                        dt1.Rows[j]["advno1"] = "";
                        dt1.Rows[j]["advdat1"] = "";
                        dt1.Rows[j]["refno"] = "";
                        dt1.Rows[j]["company"] = "";
                        dt1.Rows[j]["jobsource"] = "";
                    }

                    if (dt1.Rows[j]["deptcode"].ToString() == deptcode)
                    {
                        dt1.Rows[j]["deptdesc"] = "";

                    }
                    if (dt1.Rows[j]["postcode"].ToString() == postcode)
                    {
                        dt1.Rows[j]["postdesc"] = "";

                    }
                    advno = dt1.Rows[j]["advno"].ToString();
                    deptcode = dt1.Rows[j]["deptcode"].ToString();
                    postcode = dt1.Rows[j]["postcode"].ToString();
                }
            }

            return dt1;
        }
        protected void gvappinfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkok");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string type = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "advno")).ToString();
                //string empid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "EMPID")).ToString();
                ////  string text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "memono1")).ToString();
                ////  string date = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "memodat")).ToString("dd-MMM-yyyy");
                //hlink2.NavigateUrl = "~/LetterDefault.aspx?Type=" + type + "&Entry=Apprv&ID=" + empid;
                //hlink2.Target = "_blank";
                //hlink1.NavigateUrl = "~/LetterDefault.aspx?Type=" + type + "&Entry=Apprv&ID=" + empid;
                //hlink1.Target = "_blank";

                hlink1.NavigateUrl = "~/F_81_Hrm/F_81_Rec/JobAdvertisement.aspx?Type=App" + "&genno=" + type;
            }
        }
        protected void gvselect_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnPrintIN2");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnApp2");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string type = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "TYPE")).ToString();
                string empid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "EMPID")).ToString();
                //  string text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "memono1")).ToString();
                //  string date = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "memodat")).ToString("dd-MMM-yyyy");
                hlink2.NavigateUrl = "~/LetterDefault.aspx?Type=" + type + "&Entry=HR&ID=" + empid;
                hlink2.Target = "_blank";
                hlink1.NavigateUrl = "~/LetterDefault.aspx?Type=" + type + "&Entry=Apprv&ID=" + empid;
                hlink1.Target = "_blank";
            }
        }
        protected void chkreq_CheckedChanged(object sender, EventArgs e)
        {
            if (chkreq.Checked == true)
                this.Panel1.Visible = true;

            if (chkreq.Checked == false)
                this.Panel1.Visible = false;
        }
        protected void chkapp_CheckedChanged(object sender, EventArgs e)
        {
            if (chkapp.Checked == true)
                this.Panel2.Visible = true;

            if (chkapp.Checked == false)
                this.Panel2.Visible = false;
        }
        protected void chkatt_CheckedChanged(object sender, EventArgs e)
        {
            if (chkatt.Checked == true)
                this.Panel3.Visible = true;

            if (chkatt.Checked == false)
                this.Panel3.Visible = false;
        }
        protected void ChekAll_CheckedChanged(object sender, EventArgs e)
        {
            if (ChekAll.Checked == true)
                this.Panel4.Visible = true;

            if (ChekAll.Checked == false)
                this.Panel4.Visible = false;
        }
        protected void chkCodebook_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCodebook.Checked == true)
                this.Panel5.Visible = true;

            if (chkCodebook.Checked == false)
                this.Panel5.Visible = false;
        }
    }
}