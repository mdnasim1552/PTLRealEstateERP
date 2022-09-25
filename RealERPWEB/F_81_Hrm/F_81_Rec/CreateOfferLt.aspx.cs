using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WinForms;
using RealERPLIB;
using RealERPRDLC;
namespace RealERPWEB.F_81_Hrm.F_81_Rec
{
    public partial class CreateOfferLt : System.Web.UI.Page
    {

        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                this.txtCurDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                ((Label)this.Master.FindControl("lblTitle")).Text = "Offer Letter Creation";
                // this.GetLoanNo();
                //this.GetEmplist();
                //this.txtstrdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
            }
        }


        protected void lbtnOk_OnClick(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";

                this.lbtnPrevOffLtNo.Visible = false;
                this.ddlPrevOffLtNo.Visible = false;
                this.ShowLoanInfo();
                return;
            }
            this.lbtnOk.Text = "Ok";


            this.ddlPrevOffLtNo.Items.Clear();
            this.lbtnPrevOffLtNo.Visible = true;
            this.ddlPrevOffLtNo.Visible = true;
            this.txtCurDate.Enabled = true;

            ////this.pnlloan.Visible = false;
            this.gvOfferltr.DataSource = null;
            this.gvOfferltr.DataBind();
        }
        private string GetComeCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void ShowLoanInfo()
        {
            ViewState.Remove("tblln");
            string comcod = this.GetComeCode();
            string CurDate1 = this.txtCurDate.Text.Trim();
            string mLNNo = "NEWOFF";
            if (this.ddlPrevOffLtNo.Items.Count > 0)
            {
                this.txtCurDate.Enabled = false;

                mLNNo = this.ddlPrevOffLtNo.SelectedValue.ToString();


            }
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_APPOINTMENT_LETTER", "GETOFFINFO", mLNNo, "",
                         "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ///////Here was
            ViewState["tblln"] = ds1.Tables[0];



            if (mLNNo == "NEWOFF")
            {
                DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_APPOINTMENT_LETTER", "LASTOFFINFO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds3 == null)
                    return;
                this.lblCurNo1.Text = ds3.Tables[0].Rows[0]["maxlnno1"].ToString().Substring(0, 6);
                this.lblCurNo2.Text = ds3.Tables[0].Rows[0]["maxlnno1"].ToString().Substring(6);
                this.GetOfferGenInfo();
                return;

            }
            ViewState["tblln1"] = ds1.Tables[1];
            if (ds1.Tables[1].Rows.Count == 0)
                return;

            this.lblCurNo1.Text = ds1.Tables[1].Rows[0]["offltno1"].ToString().Substring(0, 6);
            this.lblCurNo2.Text = ds1.Tables[1].Rows[0]["offltno1"].ToString().Substring(6, 5);
            this.txtCurDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["offdate"]).ToString("dd-MMM-yyyy");
            this.Data_DataBind();




        }

        private void GetOfferGenInfo()
        {
            string comcod = this.GetComeCode();

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_APPOINTMENT_LETTER", "GETOFFGENINFO", "", "",
                         "", "", "", "", "", "", "");


            ViewState["tblln"] = ds1.Tables[0];
            this.Data_DataBind();
        }


        private void Data_DataBind()
        {

            this.gvOfferltr.DataSource = (DataTable)ViewState["tblln"];
            this.gvOfferltr.DataBind();
        }



        private void GetPreOffNo()
        {


            string comcod = this.GetComeCode();
            string curdate = this.txtCurDate.Text.Trim();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_APPOINTMENT_LETTER", "GETPREVOFFNO", curdate, "", "", "", "", "", "", "", "");

            if (ds1 == null)
                return;
            this.ddlPrevOffLtNo.DataTextField = "offno1";
            this.ddlPrevOffLtNo.DataValueField = "offltno";
            this.ddlPrevOffLtNo.DataSource = ds1.Tables[0];
            this.ddlPrevOffLtNo.DataBind();
        }

        protected void lbtnPrevOffLtNo_OnClick(object sender, EventArgs e)
        {
            this.GetPreOffNo();
        }

        protected void GetOffNO()
        {
            string comcod = this.GetComeCode();
            string mLNNO = "NEWOFF";
            if (this.ddlPrevOffLtNo.Items.Count > 0)
                mLNNO = this.ddlPrevOffLtNo.SelectedValue.ToString();

            string date = this.txtCurDate.Text; ;
            if (mLNNO == "NEWOFF")
            {
                DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_APPOINTMENT_LETTER", "LASTOFFINFO", date, "", "", "", "", "", "", "", "");

                if (ds3 == null)
                    return;
                if (ds3.Tables[0].Rows.Count > 0)
                {

                    this.lblCurNo1.Text = ds3.Tables[0].Rows[0]["maxlnno1"].ToString().Substring(0, 6);
                    this.lblCurNo2.Text = ds3.Tables[0].Rows[0]["maxlnno1"].ToString().Substring(6);

                    this.ddlPrevOffLtNo.DataTextField = "maxlnno1";
                    this.ddlPrevOffLtNo.DataValueField = "maxlnno";
                    this.ddlPrevOffLtNo.DataSource = ds3.Tables[0];
                    this.ddlPrevOffLtNo.DataBind();
                }
            }

        }



        private void SaveValue()
        {
            DataTable dt = (DataTable)ViewState["tblln"];
            // DataTable dt = (DataTable)ViewState["tblmatissue"];
            int TblRowIndex;
            for (int i = 0; i < this.gvOfferltr.Rows.Count; i++)
            {

                string txtgcod = ((TextBox)this.gvOfferltr.Rows[i].FindControl("txtoffcod")).Text.Trim();
                string txtdesc = ((TextBox)this.gvOfferltr.Rows[i].FindControl("txtdesc")).Text.Trim();


                TblRowIndex = (gvOfferltr.PageIndex) * gvOfferltr.PageSize + i;
                dt.Rows[TblRowIndex]["gcod"] = txtgcod;
                dt.Rows[TblRowIndex]["descp"] = txtdesc;



            }
            ViewState["tblln"] = dt;
        }


        protected void lUpdate_OnClick(object sender, EventArgs e)
        {
            try
            {
                string comcod = this.GetComeCode();
                if (this.ddlPrevOffLtNo.Items.Count == 0)
                    this.GetOffNO();

                this.SaveValue();
                DataTable dt = (DataTable)ViewState["tblln"];
                string curdate = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
                string offno = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();
                string txtref = this.txtOffRef.Text.Trim();
                bool result = false;
                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_APPOINTMENT_LETTER", "INSERTORUPDATEOFF", "OFFINFB", offno, curdate, txtref, "", "", "", "", "", "", "", "", "", "", "");

                if (!result)
                {
                    return;

                }

                foreach (DataRow dr1 in dt.Rows)
                {

                    string gcod = dr1["gcod"].ToString();
                    string desc = dr1["descp"].ToString();

                    result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_APPOINTMENT_LETTER", "INSERTORUPDATEOFF", "OFFINFA", offno, gcod, desc, "", "",
                        "", "", "", "", "", "", "", "", "");
                    if (!result)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = HRData.ErrorObject["Msg"].ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }

                }


                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error: " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

            }

        }

        private void GetSalInfo()
        {

            string comcod = this.GetComeCode();
            string curdate = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string offno = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_APPOINTMENT_LETTER", "GETOFFC", offno, "",
                        "", "", "", "", "", "", "");


            ViewState["tblSalinf"] = ds1.Tables[0];
            ViewState["tblhrginf"] = ds1.Tables[2];
            this.Data_DataBind();

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {


            this.GetSalInfo();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comcod = hst["comcod"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string session = hst["session"].ToString();
            //string username = hst["username"].ToString();
            ////string hostname = hst["hostname"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            DataTable dt1 = (DataTable)ViewState["tblln"];
            DataTable dt2 = (DataTable)ViewState["tblSalinf"];


            var list = dt1.DataTableToList<RealEntity.C_81_Hrm.C_81_Rec.CreateOffLt>();
            var list1 = list.FindAll(p => p.gcod == "0202002" || p.gcod == "0202003" || p.gcod == "0202004" || p.gcod == "0202005" || p.gcod == "0202006");
            var list2 = dt2.DataTableToList<RealEntity.C_81_Hrm.C_81_Rec.SalInfo>();
            var list3 = list.FindAll(p => p.gcod == "0202008");


            //string name = list[0].descp.Where(l => l.gcod == "0201002").ToString();
            string date = list.FindAll(l => l.gcod == "0201001")[0].descp;
            string name = list.FindAll(l => l.gcod == "0201002")[0].descp;
            string address = list.FindAll(l => l.gcod == "0201003")[0].descp;
            string txtdear = list.FindAll(l => l.gcod == "0202001")[0].descp;
            string benefit = list.FindAll(l => l.gcod == "0202008")[0].descp;
            string effect = list.FindAll(l => l.gcod == "0202007")[0].descp;
            string bodytxt = "";


            //Further to our interviews and follow-up discussions, " +
            //                     "the management of " + comnam + "(hereinafter referred to as the Company) is pleased " +
            //                     "to offer you an appointment in the Company as Site Engineer (Officer) on the following terms and conditions:
            //  string effect = "6.	Effective from the date of your joining, the following salary and allowances will be paid to you in arrears:";
            //string additon = "In addition, you will be paid tk 300 for your mobile allowance. You will be entitled to Incentives/Bonuses which will depend on the existing Bonus Scheme and shall start after completion of your probation period.";
            string txtcon =
                "If you agree to accept this employment on the terms and conditions noted above, please sign the duplicate of this letter" +
                " and return to us. We would welcome you to the company and sincerely hope that your career with the Company will be prosperous and rewarding.";

            string lettype = "Offer Letter";
            string txtsin = "Yours sincerely,";
            string txtsngn = "Signature: 	____________________";
            string txtdesig = "";
            //string comp = "Acme Technologies Ltd";
            string recon =
                "I ______________________________ confirm that I accept the employment with the terms and conditions specified above.";

            list.RemoveAll(l => l.gcod == "0201001" || l.gcod == "0201002" || l.gcod == "0201003" || l.gcod == "0202001");

            LocalReport Rpt1 = new LocalReport();
            //string basic, house, medical, conven, mobile, internet;
            //string tbasic, thouse, tmedical, tconven, tmobile, tinternet;
            //double basicp, housep, medicalp, convenp, mobilep, internetp;
            switch (comcod)
            {
                case "3338":
                    //DataTable dt3 = (DataTable)ViewState["tblhrginf"];
                    //var list4 = dt2.DataTableToList<RealEntity.C_81_Hrm.C_81_Rec.HrgInfo>();
                    //basic = list4.FindAll(l => l.hrgcod == "04001")[0].hrgdesc;
                    //basicp = list4.FindAll(l => l.hrgcod == "04001")[0].percnt;
                    //house = list4.FindAll(l => l.hrgcod == "04002")[0].hrgdesc;
                    //housep = list4.FindAll(l => l.hrgcod == "04002")[0].percnt;
                    //medical = list4.FindAll(l => l.hrgcod == "04003")[0].hrgdesc;
                    //medicalp = list4.FindAll(l => l.hrgcod == "04003")[0].percnt;
                    //conven = list4.FindAll(l => l.hrgcod == "04004")[0].hrgdesc;
                    //convenp = list4.FindAll(l => l.hrgcod == "04004")[0].percnt;
                    //mobile = list4.FindAll(l => l.hrgcod == "04008")[0].hrgdesc;
                    //mobilep = list4.FindAll(l => l.hrgcod == "04008")[0].percnt;
                    //internet = list4.FindAll(l => l.hrgcod == "04009")[0].hrgdesc;
                    //internetp = list4.FindAll(l => l.hrgcod == "04009")[0].percnt;
                    txtdesig = "Sr Executive (HR)";
                    Rpt1 = RDLCAccountSetup.GetLocalReport("R_81_Hrm.R_81_Rec.RptCreateOffLtAcme", list1, list2, list3);
                    break;
                default:
                    Rpt1 = RDLCAccountSetup.GetLocalReport("R_81_Hrm.R_81_Rec.RptCreateOffLt", list1, list2, list3);
                    txtdesig = "HR Executive";
                    break;

            }
            Rpt1.SetParameters(new ReportParameter("txtname", name));
            Rpt1.SetParameters(new ReportParameter("txtAddress", address));
            Rpt1.SetParameters(new ReportParameter("txtLetterType", lettype));
            Rpt1.SetParameters(new ReportParameter("txtgrettings", txtdear));
            //  Rpt1.SetParameters(new ReportParameter("txtbody", bodytxt));
            Rpt1.SetParameters(new ReportParameter("txtconsent", txtcon));
            Rpt1.SetParameters(new ReportParameter("txtSincer", txtsin));
            Rpt1.SetParameters(new ReportParameter("txtDesig", txtdesig));
            Rpt1.SetParameters(new ReportParameter("txtCom", comnam));
            Rpt1.SetParameters(new ReportParameter("txtreConsent", recon));
            Rpt1.SetParameters(new ReportParameter("txtdate", date));
            Rpt1.SetParameters(new ReportParameter("txtsign", txtsngn));
            Rpt1.SetParameters(new ReportParameter("txtsign", txtsngn));
            Rpt1.SetParameters(new ReportParameter("txteffective", effect));
            //Rpt1.SetParameters(new ReportParameter("txtaddi", additon));
            Rpt1.SetParameters(new ReportParameter("txtbenefit", benefit));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";






        }
        protected void gvOfferltr_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string gcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "gcod")).ToString();
                LinkButton status = (LinkButton)e.Row.FindControl("btnAdd");
                if (gcod == "0202007")
                {
                    status.Visible = true;
                }
                else
                {
                    status.Visible = false;
                }
            }
        }

        protected void btnSalaryupdate_Click(object sender, EventArgs e)
        {
            this.GetOffNO();
            string comcod = this.GetComeCode();
            string curdate = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string offno = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();
            string cat = this.txtcat.Text.Trim();
            string grade = this.txtgrade.Text.Trim();
            string desig = this.txtdesignation.Text.Trim();
            double gross = Convert.ToDouble(this.txtgross.Text.Trim());


            double hrent = 0;
            double bsal = 0;
            double medall = 0;
            double tpt = 0;
            double mob = 0;
            double internet = 0;
            double acbsal = 0;

            switch (comcod)
            {
                //case "3101":
                case "3338":
                    acbsal = gross;  // todo for acme calculate based on basic salary
                    hrent = acbsal == 0 ? 0 : (acbsal / 100) * 100;
                    bsal = acbsal;
                    medall = acbsal == 0 ? 0 : (acbsal / 100) * 60;
                    tpt = acbsal == 0 ? 0 : (acbsal / 100) * 35;
                    mob = acbsal == 0 ? 0 : (acbsal / 100) * 15;
                    internet = acbsal == 0 ? 0 : (acbsal / 100) * 10;
                    gross = bsal + hrent + medall + tpt + mob + internet;
                    break;


                default:

                    hrent = gross == 0 ? 0 : (gross / 100) * 30;
                    bsal = gross == 0 ? 0 : (gross / 100) * 30;
                    medall = gross == 0 ? 0 : (gross / 100) * 20;
                    tpt = gross == 0 ? 0 : (gross / 100) * 12;
                    mob = gross == 0 ? 0 : (gross / 100) * 5;
                    internet = gross == 0 ? 0 : (gross / 100) * 3;
                    break;
            }


            string rmks = this.txtrmks.Text.Trim();
            bool result;
            result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_APPOINTMENT_LETTER", "INUPOFFC", offno, cat, grade, gross.ToString(), bsal.ToString(), hrent.ToString(), medall.ToString(), tpt.ToString(), mob.ToString(), internet.ToString(), rmks, desig, "", "", "");

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = HRData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                this.GetSalInfo();
                DataTable dt = (DataTable)ViewState["tblSalinf"];
                //if(dt.Rows.Count==0){
                //    return;
                //}
                this.txtcat.Text = dt.Rows.Count > 0 ? dt.Rows[0]["cat"].ToString() : "";
                this.txtgrade.Text = dt.Rows.Count > 0 ? dt.Rows[0]["grade"].ToString() : "";
                this.txtdesignation.Text = dt.Rows.Count > 0 ? dt.Rows[0]["desig"].ToString() : "";
                this.txtrmks.Text = dt.Rows.Count > 0 ? dt.Rows[0]["rmks"].ToString() : "";

                switch (this.GetComeCode())
                {
                    //case "3101":
                    case "3338":
                        this.lblSalary.InnerText = "Basic Salary";
                        this.txtgross.Text = dt.Rows.Count > 0 ? dt.Rows[0]["bassal"].ToString() : "";

                        break;
                    default:
                        this.lblSalary.InnerText = "Gross Salary";
                        this.txtgross.Text = dt.Rows.Count > 0 ? dt.Rows[0]["gross"].ToString() : "";

                        break;
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);
            }


            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error: " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }
    }
}