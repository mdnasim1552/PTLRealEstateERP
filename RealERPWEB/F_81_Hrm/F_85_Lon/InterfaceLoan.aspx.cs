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
namespace RealERPWEB.F_81_Hrm.F_85_Lon
{
    public partial class InterfaceLoan : System.Web.UI.Page
    {
        SendNotifyForUsers UserNotify = new SendNotifyForUsers();

        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                if (dr1.Length == 0)
                    Response.Redirect("../AcceessError.aspx");
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();

                this.txtcreateDate.Text= System.DateTime.Now.ToString("dd-MMM-yyyy");
                DateTime now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);



                this.txtfrmdate.Text = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");
                this.txttodate.Text = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");
                this.GetEmplist();

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetCompCode();
                string empid = hst["empid"].ToString() ?? "";
                if (empid != "")
                {

                    ddlEmpList.Items.FindByValue(empid).Selected = true;
                }

                this.getAllData();
                this.GetLoanType();
                this.GetGross();
                this.GetPrevLoan();
                this.LoantState.SelectedIndex = 0;
                LoantState_SelectedIndexChanged(null,null);

            }
        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {
          
            this.getAllData();
            this.LoantState_SelectedIndexChanged(null, null);
        }


        private void getAllData()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            string fDate = this.txtfrmdate.Text;
            string tDate = this.txttodate.Text;
            string lType = (this.ddlLoanTypeSearch.SelectedValue.Trim().ToString()=="")?"%%":this.ddlLoanTypeSearch.SelectedValue.ToString();

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_LOANAPP", "GETLOAN", fDate, tDate, usrid, lType, "", "", "", "", "");
            if (ds1 == null || ds1.Tables[0].Rows.Count == 0)
              return;

            this.LoantState.Items[0].Text = "<div class='circle-tile'><a><div class='circle-tile-heading deep-sky-blue counter'>" + Convert.ToDouble(ds1.Tables[1].Rows[0]["tloan"]).ToString("#,##0;(#,##0); ") + "</div></a><div class='circle-tile-content deep-sky-blue'><div class='circle-tile-description txt-white'>Loan Queue</div></div></div>";
            this.LoantState.Items[1].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + Convert.ToDouble(ds1.Tables[1].Rows[0]["lpros"]).ToString("#,##0;(#,##0); ") + "</div></a><div class='circle-tile-content purple'><div class='circle-tile-description txt-white'>Loan Process <small>(HOD)</small></div></div></div>";
            this.LoantState.Items[2].Text = "<div class='circle-tile'><a><div class='circle-tile-heading deep-sky-blue counter'>" + Convert.ToDouble(ds1.Tables[1].Rows[0]["lapp"]).ToString("#,##0;(#,##0); ") + "</div></a><div class='circle-tile-content deep-sky-blue'><div class='circle-tile-description txt-white'>Loan eligibility <small>(HOHR)</small></div></div></div>";
            this.LoantState.Items[3].Text = "<div class='circle-tile'><a><div class='circle-tile-heading deep-pink counter'>" + Convert.ToDouble(ds1.Tables[1].Rows[0]["lapp"]).ToString("#,##0;(#,##0); ") + "</div></a><div class='circle-tile-content deep-pink'><div class='circle-tile-description txt-white'>Ready To Fund <small>(HO Finance)</small></div></div></div>";
            this.LoantState.Items[4].Text = "<div class='circle-tile'><a><div class='circle-tile-heading orange counter'>" + Convert.ToDouble(ds1.Tables[1].Rows[0]["lgen"]).ToString("#,##0;(#,##0); ") + "</div></a><div class='circle-tile-content orange'><div class='circle-tile-description txt-white'>Loan Approval</div></div></div>";
            this.LoantState.Items[5].Text = "<div class='circle-tile'><a><div class='circle-tile-heading deep-pink counter'>" + Convert.ToDouble(ds1.Tables[1].Rows[0]["lgen"]).ToString("#,##0;(#,##0); ") + "</div></a><div class='circle-tile-content deep-pink'><div class='circle-tile-description txt-white'>Loan Generate <small>(HOHR)</small></div></div></div>";
            this.LoantState.Items[6].Text = "<div class='circle-tile'><a><div class='circle-tile-heading deep-green counter'>" + Convert.ToDouble(ds1.Tables[1].Rows[0]["lcomp"]).ToString("#,##0;(#,##0); ") + "</div></a><div class='circle-tile-content green'><div class='circle-tile-description txt-white'>Loan Completed</div></div></div>";
            this.LoantState.Items[7].Text = "<div class='circle-tile'><a><div class='circle-tile-heading danger counter'>" + Convert.ToDouble(ds1.Tables[1].Rows[0]["lcancel"]).ToString("#,##0;(#,##0); ") + "</div></a><div class='circle-tile-content danger'><div class='circle-tile-description txt-white'>Loan Cancelled</div></div></div>";
            


            DataTable dt = new DataTable();
            DataView dv = new DataView();

            //pending
            dt = ((DataTable)ds1.Tables[0]).Copy();
            dv = dt.DefaultView;
            //dv.RowFilter = ("lnstatus=0");
            this.Data_Bind("gvPending", dv.ToTable());

            //Process
            dt = ((DataTable)ds1.Tables[0]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("lnstatus=0");
            this.Data_Bind("gvProcess", dv.ToTable());
            
           // Approved
           dt = ((DataTable)ds1.Tables[0]).Copy();
            dv = dt.DefaultView;//("digstatus = 'Diagnosis' and approved= '' ");
            dv.RowFilter = ("lnstatus=1 and isaproved=0 and iscancelled=0 ");
            this.Data_Bind("gvApproved", dv.ToTable());

            //Generate
            dt = ((DataTable)ds1.Tables[0]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("isaproved=1 and lnno='' ");
            this.Data_Bind("gvGen", dv.ToTable());

            //Completed
            dt = ((DataTable)ds1.Tables[0]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("lnno <> ''");
            this.Data_Bind("gvCompleted", dv.ToTable());


            dt = ((DataTable)ds1.Tables[0]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("iscancelled=1 and isaproved=0 ");
            this.Data_Bind("gvCanc", dv.ToTable());
        }
        protected void LoantState_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = this.LoantState.SelectedIndex; //hiddenSeletedIndex.Value!="" ? Convert.ToInt32(hiddenSeletedIndex.Value): this.LoantState.SelectedIndex;
            switch (index)
            {
                case 0:
                    this.pnlQue.Visible = true;
                    this.pnlLoanProc.Visible = false;
                    this.pnlLoanAppr.Visible = false;
                    this.pnlLoangen.Visible = false;
                    this.pnlLoanComp.Visible = false;
                    this.LoantState.Items[0].Attributes["class"] = "lblactive blink_me";
                    hiddenSeletedIndex.Value = index.ToString();

                    break;

                case 1:
                    this.pnlQue.Visible = false;
                    this.pnlLoanProc.Visible = true;
                    this.pnlLoanAppr.Visible = false;
                    this.pnlLoangen.Visible = false;
                    this.pnlLoanComp.Visible = false;
                    this.LoantState.Items[1].Attributes["class"] = "lblactive blink_me";
                    hiddenSeletedIndex.Value = index.ToString();

                    break;


                case 2:
                    this.pnlQue.Visible = false;
                    this.pnlLoanProc.Visible = false;
                    this.pnlLoanAppr.Visible = true;
                    this.pnlLoangen.Visible = false;
                    this.pnlLoanComp.Visible = false;
                    this.LoantState.Items[2].Attributes["class"] = "lblactive blink_me";
                    hiddenSeletedIndex.Value = index.ToString();

                    break;

                case 3:
                    this.pnlQue.Visible = false;
                    this.pnlLoanProc.Visible = false;
                    this.pnlLoanAppr.Visible = false;
                    this.pnlLoangen.Visible = true;
                    this.pnlLoanComp.Visible = false;
                    this.LoantState.Items[3].Attributes["class"] = "lblactive blink_me";
                    hiddenSeletedIndex.Value = index.ToString();

                    break;

                case 4:
                    this.pnlQue.Visible = false;
                    this.pnlLoanProc.Visible = false;
                    this.pnlLoanAppr.Visible = false;
                    this.pnlLoangen.Visible = false;
                    this.pnlLoanComp.Visible = true;
                    this.LoantState.Items[4].Attributes["class"] = "lblactive blink_me";
                    hiddenSeletedIndex.Value = index.ToString();

                    break;

                case 5:
                    this.pnlQue.Visible = false;
                    this.pnlLoanProc.Visible = false;
                    this.pnlLoanAppr.Visible = false;
                    this.pnlLoangen.Visible = false;
                    this.pnlLoanComp.Visible = false;
                    this.pnlCanc.Visible = false;
                    this.LoantState.Items[5].Attributes["class"] = "lblactive blink_me";
                    hiddenSeletedIndex.Value = index.ToString();

                    break;
            }

        }

        private void Data_Bind(string gv, DataTable dt)
        {
            switch (gv)
            {
                case "gvPending":
                    this.gvPending.DataSource = (dt);
                    this.gvPending.DataBind();

                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "gvProcess":
                    this.gvProcess.DataSource = (dt);
                    this.gvProcess.DataBind();

                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "gvApproved":
                    this.gvApproved.DataSource = (dt);
                    this.gvApproved.DataBind();

                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "gvGen":
                    this.gvGen.DataSource = (dt);
                    this.gvGen.DataBind();
                    break;
                case "gvCompleted":
                    this.gvCompleted.DataSource = (dt);
                    this.gvCompleted.DataBind();

                    if (dt.Rows.Count == 0)
                        return;

                    break;


                case "gvCanc":
                    this.gvCanc.DataSource = (dt);
                    this.gvCanc.DataBind();

                    if (dt.Rows.Count == 0)
                        return;

                    break;
            }
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void GetLoanType()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLOANTYPE", "", "", "", "", "", "", "", "", "");
            this.ddlLoanType.DataTextField = "loantype";
            this.ddlLoanType.DataValueField = "gcod";
            this.ddlLoanType.DataSource = ds1.Tables[0];
            this.ddlLoanType.DataBind();
            ddlLoanType.Items.Insert(0, new ListItem("All loan", ""));
            ddlLoanType.Items[0].Attributes["disabled"] = "disabled";

            this.ddlLoanTypeSearch.DataTextField = "loantype";
            this.ddlLoanTypeSearch.DataValueField = "gcod";
            this.ddlLoanTypeSearch.DataSource = ds1.Tables[0];
            this.ddlLoanTypeSearch.DataBind();
        }

        private void GetPrevLoan()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string empid = this.ddlEmpList.SelectedValue.ToString() ?? "";
            DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_LOANAPP", "PREVLOANAMT", empid, "", "", "", "", "", "", "", "");
            if (ds == null || ds.Tables.Count == 0)
                return;
            DataTable dt1 = ds.Tables[0];
            DataTable dt2 = ds.Tables[1];
            this.txtPloanAmt.Text = dt1.Rows[0]["ttlloanamt"].ToString();

            int loanid = Convert.ToInt32(dt2.Rows[0]["lnno"]) + 1;
  
            this.txtLoanId.Text ="Ln-"+ loanid.ToString();
        }

        private void GetGross()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string empid = this.ddlEmpList.SelectedValue.ToString()?? "";
            DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_LOANAPP", "GETGROSS", empid, "", "", "", "", "", "", "", "");
            if (ds == null || ds.Tables.Count == 0)
                return;
            DataTable dt1 = ds.Tables[0];
            DataTable dt2 = ds.Tables[1];
            DataTable dt3 = ds.Tables[2];
            this.txtGMS.Text = dt1.Rows[0]["grosssal"].ToString()??"";
            this.txtPFAmt.Text = dt2.Rows[0]["pffund"].ToString()??"";
            this.txtTax.Text = dt3.Rows[0]["inctax"].ToString()??"";

        }
        private void GetEmplist()
        {
            string comcod = this.GetCompCode();
            string txtEmpname = "%%";
            string type = "";
            switch (comcod)
            {
                case "3365":
                case "3101":
                    type = "lnemp";
                    break;
                default:
                    type = "";
                    break;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string empid = hst["empid"].ToString() ?? "";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLNEMPLIST", txtEmpname, type, "", "", "", "", "", "", "");
            this.ddlEmpList.DataTextField = "empname";
            this.ddlEmpList.DataValueField = "empid";
            this.ddlEmpList.DataSource = ds1.Tables[0];
            this.ddlEmpList.DataBind();
            if (empid.Length != 0) {
                this.ddlEmpList.SelectedValue = empid;

            }



        }

        //apply loan view
        protected void lnkApplyModal_Click(object sender, EventArgs e)
        {

            this.GetLoanType();
            this.GetGross();
            this.GetPrevLoan();

            this.txtcreateDate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
            this.txtLoanAmt.Text = "";
            this.txtInstNum.Text = "";
            this.txtStd.Text = "";
            this.txtOI.Text = "";
            this.txtOD.Text = "";
            this.txtrt.Text = "";
            this.txtEffDate.Text = "";
            this.txtLoanDescc.Text = "";
            this.txtAmtPerIns.Text = "";

            this.lnkAdd.Visible = true;
            this.ddlEmpList.Enabled = true;
            this.txtcreateDate.Enabled = true;
            this.txtLoanAmt.Enabled = true;
            this.txtInstNum.Enabled = true;
            this.txtStd.Enabled = true;
            this.txtOI.Enabled = true;
            this.txtOD.Enabled = true;
            this.txtrt.Enabled = true;
            this.txtEffDate.Enabled = true;
            this.ddlLoanType.Enabled = true;
            this.txtLoanDescc.Enabled = true;
            this.lnkUpdate.Visible = false;
            this.lnkApprov.Visible = false;
            this.lnkEdited.Visible = false;

            this.lnkCancel.Visible = false;
            this.dibNote.Visible = false;
            loanID.Value = "0";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenApplyLoan();", true);
        }


        //create loan
        protected void lnkAdd_Click(object sender, EventArgs e)
        {
            
            string Message;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string compsms = hst["compsms"].ToString();
            string compmail = hst["compmail"].ToString();
            string ssl = hst["ssl"].ToString();
            string compName = hst["comnam"].ToString();
            string usrid = hst["usrid"].ToString();
            string deptcode = hst["deptcode"].ToString();

            string comcod = this.GetCompCode();
            string empid = this.ddlEmpList.SelectedValue.ToString();// hst["empid"].ToString()??"";
            if (empid == "000000000000")
            {
                Message = "Please select Emp Name";
                return;
            }
            string id = "0";
            string loantype = ddlLoanType.SelectedValue.ToString() ?? "";
            string loanamt = "0" + this.txtLoanAmt.Text.ToString();
            string instlnum = this.txtInstNum.Text.ToString() ?? "0";
            string perinstlamt = "0" + this.txtAmtPerIns.Text.ToString() ?? "0";
            string loandesc = this.txtLoanDescc.Text.ToString();
            string rate = "0" + this.txtrt.Text.ToString() ?? "0";
            string effedat = Convert.ToDateTime(this.txtEffDate.Text).ToString("dd-MMM-yyyy") ?? System.DateTime.Now.ToString("dd-MMM-yyy");
            string posteddate = System.DateTime.Now.ToString("dd-MMM-yyy");
            string pstdusrid = hst["usrid"].ToString();
            string pstdsession = hst["session"].ToString();
            string pstdtrmnlid = hst["compname"].ToString();
            string pstdusredt = "";
            string pstdssnedt = "";
            string pstdtrmnledt = "";
            string postdateedited = "";
            string lnstatus = "0";

            string createDate = Convert.ToDateTime(this.txtcreateDate.Text).ToString("dd-MMM-yyyy") ?? System.DateTime.Now.ToString("dd-MMM-yyy");
            string othincm = "0" + this.txtOI.Text.ToString() ?? "0";
            string othdeduct = "0" + this.txtOD.Text.ToString() ?? "0";
            string stddeduct = "0" + this.txtStd.Text.ToString() ?? "0";

            string loantypeDesc = ddlLoanType.SelectedItem.ToString() ?? "";





            //maincode = (editedid != "") ? editedid : maincode;
            bool result = HRData.UpdateTransInfo3(comcod, "dbo_hrm.SP_ENTRY_LOANAPP", "INSERTLOAN", empid, id, loantype, loanamt, instlnum, perinstlamt, loandesc, rate, effedat, posteddate, pstdusrid, pstdsession, pstdtrmnlid, pstdusredt, pstdssnedt, pstdtrmnledt, postdateedited, createDate, stddeduct, othincm, othdeduct, lnstatus, "");
            if (result == true)
            {

                this.getAllData();
                this.LoantState.SelectedIndex = Convert.ToInt32(hiddenSeletedIndex.Value);
                Message = "Successfully Updated";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Message + "');", true);


                string subj = "New Leave Request";
                string htmbody = "Loan Type: " + loantypeDesc + ", Loan Amount: " + loanamt + ", Purpose  of Loan" + loandesc;
                this.SendNotificaion(createDate, effedat, id, deptcode, compsms, compmail, ssl, compName, htmbody, subj, lnstatus);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#ApplyLoan", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#ApplyLoan').hide();", true);


            }
            else
            {
                this.getAllData();
                this.LoantState.SelectedIndex = Convert.ToInt32(hiddenSeletedIndex.Value);
                Message = "Update Fail";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Message + "');", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "ModalLoanClose();", true);
            }


        }
        //update loan
        protected void lnkUpdate_Click(object sender, EventArgs e)
        {

            string Message;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string compsms = hst["compsms"].ToString();
            string compmail = hst["compmail"].ToString();
            string ssl = hst["ssl"].ToString();
            string compName = hst["comnam"].ToString();
            string usrid = hst["usrid"].ToString();
            string deptcode = hst["deptcode"].ToString();

            string comcod = this.GetCompCode();
            string empid = this.ddlEmpList.SelectedValue.ToString();
            //string empid = hst["empid"].ToString() ?? "";
            string id = this.txtLoanId.Text.ToString().Remove(0,3);
            string loantype = ddlLoanType.SelectedValue.ToString() ?? "";
            string loanamt = "0" + this.txtLoanAmt.Text.ToString();
            string instlnum = this.txtInstNum.Text.ToString() ?? "0";
            string perinstlamt = "0" + this.txtAmtPerIns.Text.ToString() ?? "0";
            string loandesc = this.txtLoanDescc.Text.ToString();
            string rate = "0" + this.txtrt.Text.ToString() ?? "0";
            string effedat = Convert.ToDateTime(this.txtEffDate.Text).ToString("dd-MMM-yyyy") ?? "";
            string pstdusredt = hst["usrid"].ToString();
            string pstdssnedt = hst["session"].ToString();
            string pstdtrmnledt = hst["compname"].ToString();
            string postdateedited = System.DateTime.Now.ToString("dd-MMM-yyy") ?? "";
            string lnstatus = "1";

            string createDate = Convert.ToDateTime(this.txtcreateDate.Text).ToString("dd-MMM-yyyy") ?? "";
            string othincm = "0" + this.txtOI.Text.ToString() ?? "0";
            string othdeduct = "0" + this.txtOD.Text.ToString();
            string stddeduct = "0" + this.txtStd.Text.ToString();

            string loantypeDesc = ddlLoanType.SelectedItem.ToString() ?? "";


            //maincode = (editedid != "") ? editedid : maincode;
            bool result = HRData.UpdateTransInfo3(comcod, "dbo_hrm.SP_ENTRY_LOANAPP", "INSERTLOAN", empid, id, loantype, loanamt, instlnum, perinstlamt, loandesc, rate, effedat, "", "", "", "", pstdusredt, pstdssnedt, pstdtrmnledt, postdateedited, createDate, stddeduct, othincm, othdeduct, lnstatus, "");
            if (result == true)
            {
                this.getAllData();
                this.LoantState.SelectedIndex = Convert.ToInt32(hiddenSeletedIndex.Value);
                Message = "Successfully Updated";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Message + "');", true);


                string subj =  "Loan Process Approved";

                string htmbody = "Loan Type: " + loantypeDesc + ", Loan Amount: " + loanamt + ", Purpose  of Loan" + loandesc;
                this.SendNotificaion(createDate, effedat, id, deptcode, compsms, compmail, ssl, compName, htmbody, subj, lnstatus);


                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#ApplyLoan", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#ApplyLoan').hide();", true);


            }
            else
            {
                this.getAllData();
                this.LoantState.SelectedIndex = Convert.ToInt32(hiddenSeletedIndex.Value);
                Message = "Update Fail";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Message + "');", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "ModalLoanClose();", true);
            }

            LoantState_SelectedIndexChanged(null, null);


        }


        //loan process approval
        protected void lnkApprov_Click(object sender, EventArgs e)
        {
            string empid = this.ddlEmpList.SelectedValue.ToString();
            string loanid = this.txtLoanId.Text.ToString().Remove(0, 3);
            string lnstatus = "1";
            string lsApproved = "1";
            string lsCancelled= "0";
            this.ChangeloanStatus(empid, loanid, lnstatus, lsApproved, lsCancelled);
        }
        //loan cancellation
        protected void lnkCancel_Click(object sender, EventArgs e)
        {
            string empid = this.ddlEmpList.SelectedValue.ToString();
            string loanid = this.txtLoanId.Text.ToString().Remove(0, 3);
            string lnstatus = "0";
            string lsApproved = "0";
            string IsCancelled = "1";
            this.ChangeloanStatus(empid, loanid, lnstatus, lsApproved, IsCancelled);
        }


        //change  loan status
        private void ChangeloanStatus(string empid,string loanid, string lnstatus, string lsApproved, string lsCancelled)
        {
            string note = this.txtnote.Text.ToString() ?? "";
            if (note == "" && lsCancelled=="1")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#ApplyLoan", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#ApplyLoan').hide();", true);

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "checkEmptyNote();", true);
                return;
            }
            string Message;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string compsms = hst["compsms"].ToString();
            string compmail = hst["compmail"].ToString();
            string ssl = hst["ssl"].ToString();
            string compName = hst["comnam"].ToString();
            string usrid = hst["usrid"].ToString();
            string deptcode = hst["deptcode"].ToString();
            string comcod = this.GetCompCode();
            string loantype = ddlLoanType.SelectedValue.ToString() ?? "";
            string loantypeDesc = ddlLoanType.SelectedItem.ToString() ?? "";
            string loanamt = "0" + this.txtLoanAmt.Text.ToString();
            string instlnum = this.txtInstNum.Text.ToString() ?? "0";
            string perinstlamt = "0" + this.txtAmtPerIns.Text.ToString() ?? "0";
            string loandesc = this.txtLoanDescc.Text.ToString();
            string rate = "0" + this.txtrt.Text.ToString() ?? "0";
            string effedat = Convert.ToDateTime(this.txtEffDate.Text).ToString("dd-MMM-yyyy") ?? "";
            string pstdusredt = hst["usrid"].ToString();
            string pstdssnedt = hst["session"].ToString();
            string pstdtrmnledt = hst["compname"].ToString();
            string postdateedited = System.DateTime.Now.ToString("dd-MMM-yyy") ?? "";
            string createDate = Convert.ToDateTime(this.txtcreateDate.Text).ToString("dd-MMM-yyyy") ?? "";
            string othincm = "0" + this.txtOI.Text.ToString() ?? "0";
            string othdeduct = "0" + this.txtOD.Text.ToString();
            string stddeduct = "0" + this.txtStd.Text.ToString();



            bool result = HRData.UpdateTransHREMPInfo3(comcod, "dbo_hrm.SP_ENTRY_LOANAPP", "INSERTLOAN", empid, loanid, loantype, loanamt, instlnum, perinstlamt, loandesc,
                rate, effedat, "", "", "", "", pstdusredt, pstdssnedt, pstdtrmnledt, postdateedited, createDate, stddeduct, othincm, othdeduct, lnstatus, lsApproved, lsCancelled, note, "", "", "", "", "", "", "");
            if (result == true)
            {

                Message = "Successfully Updated";
                //if (qtype != "MGT")
                //{
                string subj = lsCancelled=="1"? "Loan Request Cancel" : lsApproved == "1" ? "Loan Request Approved":"New Loan Request";



                string htmbody = "Loan Type: "+ loantypeDesc+", Loan Amount: "+ loanamt + ", Purpose  of Loan"+loandesc;
                this.SendNotificaion(createDate, effedat, loanid, deptcode, compsms, compmail, ssl, compName, htmbody, subj, lnstatus);

                // }

                string eventdesc2 = "Details: " + "Loan Process Approval";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), "New Leave Request", "", "");

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Message + "');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#ApplyLoan", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#ApplyLoan').hide();", true);
                getAllData();

            }
            else
            {
                Message = "Applied Fail";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Message + "');", true);

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#ApplyLoan", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#ApplyLoan').hide();", true);
                getAllData();


            }

        }
       //edit view  
        protected void pendlnEdit_Click(object sender, EventArgs e)
        {
            this.txtcreateDate.Enabled = true;
            this.txtLoanAmt.Enabled = true;
            this.txtInstNum.Enabled = true;
            this.txtStd.Enabled = true;
            this.txtOI.Enabled = true;
            this.txtOD.Enabled = true;
            this.txtrt.Enabled = true;
            this.txtEffDate.Enabled = true;
            this.ddlLoanType.Enabled = true;
            this.txtLoanDescc.Enabled = true;
            this.ddlEmpList.Enabled = true;
            this.dibNote.Visible = true;
            this.lnkEdited.Visible = true;
            this.dibNote.Visible = false;

            /// false section 
            this.lnkUpdate.Visible = false;
            this.lnkApprov.Visible = false;
            this.lnkCancel.Visible = false;
            /// 
            
                

            string comcod = this.GetCompCode();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string lnid = ((Label)this.gvProcess.Rows[index].FindControl("lblidPend")).Text.ToString();
            string empid= ((Label)this.gvProcess.Rows[index].FindControl("lblpendempid")).Text.ToString();
            loanID.Value = "0";
            this.AllVie_Data(empid, lnid);
            this.GetGross();
            
        }

       //edit view for aproval
        protected void pendlnEditApr_Click(object sender, EventArgs e)
        {
            this.txtcreateDate.Enabled = true;
            this.txtLoanAmt.Enabled = true;
            this.txtInstNum.Enabled = true;
            this.txtStd.Enabled = true;
            this.txtOI.Enabled = true;
            this.txtOD.Enabled = true;
            this.txtrt.Enabled = true;
            this.txtEffDate.Enabled = true;
            this.ddlLoanType.Enabled = true;
            this.txtLoanDescc.Enabled = true;
            string comcod = this.GetCompCode();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string lnid = ((Label)this.gvApproved.Rows[index].FindControl("lblidPend")).Text.Trim().ToString();
            string empid = ((Label)this.gvApproved.Rows[index].FindControl("lblpendempid")).Text.Trim().ToString();
            this.AllVie_Data(empid, lnid);
            this.GetGross();
        }


        //pending loan view
        protected void pendlnView_Click(object sender, EventArgs e)
        {
          
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string lnid = ((Label)this.gvPending.Rows[index].FindControl("lblidPend")).Text.ToString().Trim();
            string empid = ((Label)this.gvPending.Rows[index].FindControl("lblpendempid")).Text.ToString().Trim();

            this.txtcreateDate.Enabled = false;
            this.txtLoanAmt.Enabled = false;
            this.txtInstNum.Enabled = false;
            this.txtStd.Enabled = false;
            this.txtOI.Enabled = false;
            this.txtOD.Enabled = false;
            this.txtrt.Enabled = false;
            this.txtEffDate.Enabled = false;
            this.ddlLoanType.Enabled = false;
            this.txtLoanDescc.Enabled = false;
            this.ddlEmpList.Enabled = false;
            this.lnkUpdate.Visible = false;
            this.lnkAdd.Visible = false;
            this.lnkApprov.Visible = false;
            lnkCancel.Visible = false;
            this.dibNote.Visible = false;

            /// false section 
            this.lnkAdd.Visible = false;
            this.lnkEdited.Visible = false;
            this.lnkUpdate.Visible = false;
            this.lnkApprov.Visible = false;
            this.lnkCancel.Visible = false;
            /// 

            this.AllVie_Data(empid, lnid);
            this.GetGross();


        }
       
        //process loan view
        protected void proslnView_Click(object sender, EventArgs e)
        {

            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string lnid = ((Label)this.gvProcess.Rows[index].FindControl("lblidPend")).Text.ToString().Trim();
            string empid = ((Label)this.gvProcess.Rows[index].FindControl("lblpendempid")).Text.ToString().Trim();

            this.txtcreateDate.Enabled = false;
            this.txtLoanAmt.Enabled = false;
            this.txtInstNum.Enabled = false;
            this.txtStd.Enabled = false;
            this.txtOI.Enabled = false;
            this.txtOD.Enabled = false;
            this.txtrt.Enabled = false;
            this.txtEffDate.Enabled = false;
            this.ddlLoanType.Enabled = false;
            this.txtLoanDescc.Enabled = false;
            this.ddlEmpList.Enabled = false;
            this.lnkUpdate.Visible = false;
            this.lnkAdd.Visible = false;
            this.lnkApprov.Visible = false;
            this.lnkCancel.Visible = false;
            this.dibNote.Visible = false;


            this.AllVie_Data(empid, lnid);
            this.GetGross();

        }

        //approval  for loan process
        protected void AprvProcsView(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string lnid = ((Label)this.gvProcess.Rows[index].FindControl("lblidPend")).Text.ToString().Trim();
            string empid = ((Label)this.gvProcess.Rows[index].FindControl("lblpendempid")).Text.ToString().Trim();

            this.ddlEmpList.Enabled = false;
            this.lnkEdited.Visible = false;
            this.lnkUpdate.Visible = true;
            this.lnkAdd.Visible = false;
            this.lnkApprov.Visible = false;
            lnkCancel.Visible = true;
            this.dibNote.Visible =  true;

            this.txtcreateDate.Enabled = true;
            this.txtLoanAmt.Enabled = true;
            this.txtInstNum.Enabled = true;
            this.txtStd.Enabled = true;
            this.txtOI.Enabled = true;
            this.txtOD.Enabled = true;
            this.txtrt.Enabled = true;
            this.txtEffDate.Enabled = true;
            this.ddlLoanType.Enabled = true;
            this.txtLoanDescc.Enabled = false;
            this.ddlEmpList.Enabled = true;
            this.AllVie_Data(empid, lnid);
            this.GetGross();
        }
  
        //approve loan view
        protected void AprlnView_Click(object sender, EventArgs e)
        {



            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string lnid = ((Label)this.gvApproved.Rows[index].FindControl("lblidPend")).Text.ToString().Trim();
            string empid = ((Label)this.gvApproved.Rows[index].FindControl("lblpendempid")).Text.ToString().Trim();


            this.lnkUpdate.Visible = false;
            this.lnkAdd.Visible = false;
            this.lnkApprov.Visible = true;
       
            lnkCancel.Visible = true;
            this.dibNote.Visible = true;

            /// false section 
            this.lnkAdd.Visible = false;
            this.lnkEdited.Visible = false;
            this.lnkUpdate.Visible = false;
            
            /// 


            this.txtcreateDate.Enabled = true;
            this.txtLoanAmt.Enabled = true;
            this.txtInstNum.Enabled = true;
            this.txtStd.Enabled = true;
            this.txtOI.Enabled = true;
            this.txtOD.Enabled = true;
            this.txtrt.Enabled = true;
            this.txtEffDate.Enabled = true;
            this.ddlLoanType.Enabled = true;
            this.txtLoanDescc.Enabled = false;
            this.ddlEmpList.Enabled = true;
            this.AllVie_Data(empid, lnid);
            this.GetGross();

        }
        //generate loan view
        protected void LoGenlnView_Click(object sender, EventArgs e)
        {

            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string lnid = ((Label)this.gvGen.Rows[index].FindControl("lblidPend")).Text.ToString().Trim();
            string empid = ((Label)this.gvGen.Rows[index].FindControl("lblpendempid")).Text.ToString().Trim();

            this.txtcreateDate.Enabled = false;
            this.txtLoanAmt.Enabled = false;
            this.txtInstNum.Enabled = false;
            this.txtStd.Enabled = false;
            this.txtOI.Enabled = false;
            this.txtOD.Enabled = false;
            this.txtrt.Enabled = false;
            this.txtEffDate.Enabled = false;
            this.ddlLoanType.Enabled = false;
            this.txtLoanDescc.Enabled = false;
            this.ddlEmpList.Enabled = false;
            this.lnkUpdate.Visible = false;
            this.lnkAdd.Visible = false;
            this.lnkApprov.Visible = false;
            this.lnkCancel.Visible = false;
            this.dibNote.Visible = false;

            this.AllVie_Data(empid, lnid);
            this.GetGross();
        }

        //all view
        private void AllVie_Data(string empid, string lnid) {

            string comcod = this.GetCompCode();
            
            DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_LOANAPP", "GETLOANBYID", empid, lnid, "", "", "", "", "", "", "", "");
            if (ds == null || ds.Tables.Count == 0)
                return;
            DataTable dt = ds.Tables[0];
             


            this.txtLoanId.Text = "Ln-" + dt.Rows[0]["id"].ToString();
            this.txtcreateDate.Text = Convert.ToDateTime(dt.Rows[0]["createdate"]).ToString("dd-MMM-yyyy");
            this.txtLoanAmt.Text = dt.Rows[0]["loanamt"].ToString() ?? "";
            this.txtInstNum.Text = dt.Rows[0]["instlnum"].ToString() ?? "";
            this.txtAmtPerIns.Text = dt.Rows[0]["perinstlamt"].ToString() ?? "";
            this.txtStd.Text = dt.Rows[0]["statdeduction"].ToString() ?? "";
            this.txtOI.Text = dt.Rows[0]["othincome"].ToString() ?? "";
            this.txtOD.Text = dt.Rows[0]["othdeduction"].ToString() ?? "";
            this.txtrt.Text = dt.Rows[0]["rate"].ToString() ?? "";
            this.txtEffDate.Text = Convert.ToDateTime(dt.Rows[0]["effdate"]).ToString("dd-MMM-yyyy");
            ddlLoanType.ClearSelection();
            string loantype = dt.Rows[0]["loantype"].ToString().Trim();
            this.ddlEmpList.SelectedValue = empid;
            ddlLoanType.Items.FindByValue(loantype).Selected = true;
            this.txtLoanDescc.Text = dt.Rows[0]["loandesc"].ToString() ?? "";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "ViewLoan();", true);

        }

   
        //delete confirm
        protected void confirmDelete_click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string msg = "";

            string lnid = this.delid.Value;
            string empid = this.delempid.Value;
            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_LOANAPP", "LOANREMOVE", empid, lnid, "", "", "", "", "", "", "");
            if (result)
            {
                msg = "Successfully Deleted";
                Response.Redirect(Request.RawUrl);
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);

                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Loan Requset Delete";
                    string eventdesc = "Loan Requset Delete by ID";
                    string eventdesc2 =  lnid;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }


            }
            else
            {
                msg = "Update Failed";
                Response.Redirect(Request.RawUrl);
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
            }
        }

        //delete popup
        protected void confmDelModal_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string lnid = ((Label)this.gvPending.Rows[index].FindControl("lblidPend")).Text.ToString();
            string empid = ((Label)this.gvPending.Rows[index].FindControl("lblpendempid")).Text.ToString();
            this.delid.Value = lnid;
            this.delempid.Value = empid.ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenDeleteModal();", true);
            
        }

        //loan generate gridview
        protected void gvGen_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
               
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnInd");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string genno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "id")).ToString();


                hlink2.NavigateUrl = "~/F_81_Hrm/F_85_Lon/EmpLoanInfo?Type=Entry&genno="+ genno;

            }
        }
        //loan pending gridview
        protected void gvPending_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkDel = (LinkButton)e.Row.FindControl("confmDelModal");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string userid = hst["usrid"].ToString();
                string empusrid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "empusrid")).ToString().Trim();

                string lnstatus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "lnstatus")).ToString();
                if ((lnstatus == "False") && (userid == empusrid))
                {
                    lnkDel.Visible = true;
                }
                

            }
        }

        //Employee list dropdown
        protected void ddlEmpList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetGross();
            this.GetPrevLoan();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#ApplyLoan", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#ApplyLoan').hide();", true);

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenApplyLoan();", true);
         


        }



        //notification process
        private void SendNotificaion(string frmdate, string todate, string ltrnid, string deptcode, string compsms, string compmail, string ssl, string compName, string htmtableboyd, string subj, string lnstatus)
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string userid = hst["usrid"].ToString(); 
                DataTable dt = (DataTable)ViewState["tblempinfo"];
                string leavedesc = this.ddlLoanType.SelectedItem.ToString();
                string empid = this.ddlEmpList.SelectedValue.ToString();

                
                ///GET SMTP AND SMS API INFORMATION
                #region
                string usrid = ((Hashtable)Session["tblLogin"])["usrid"].ToString();
                DataSet dssmtpandmail = HRData.GetTransInfo(comcod, "SP_UTILITY_ACCESS_PRIVILEGES", "SMTPPORTANDMAIL", usrid, "", "", "", "", "", "", "", "");
                if (dssmtpandmail == null)
                    return;
                //SMTP
                string hostname = dssmtpandmail.Tables[0].Rows[0]["smtpid"].ToString();
                int portnumber = Convert.ToInt32(dssmtpandmail.Tables[0].Rows[0]["portno"].ToString());
                string frmemail = dssmtpandmail.Tables[0].Rows[0]["mailid"].ToString();
                string psssword = dssmtpandmail.Tables[0].Rows[0]["mailpass"].ToString();
                bool isSSL = Convert.ToBoolean(dssmtpandmail.Tables[0].Rows[0]["issl"].ToString());
                #endregion
                #region
                string callType = "GETDPTMGTHEADDATA";
                if (lnstatus == "1")
                {
                    callType = "GETMGTHEADDATA";

                }
                

                var ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_BASIC_UTILITY_DATA", callType, empid, "", ltrnid, "", "", "", "", "", "");

                if (ds1 == null)
                    return;
                string supphone = "";
                string idcard = (string)ds1.Tables[1].Rows[0]["idcard"];
                string empname = (string)ds1.Tables[1].Rows[0]["name"];
                string empdesig = (string)ds1.Tables[1].Rows[0]["desig"];
                string deptname = (string)ds1.Tables[1].Rows[0]["deptname"];
                #endregion

                for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
                {
                    string suserid = ds1.Tables[0].Rows[0]["suserid"].ToString();
                    string tomail = ds1.Tables[0].Rows[0]["mail"].ToString();
                    string roletype = (string)ds1.Tables[0].Rows[0]["roletype"];
                    string uhostname = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/F_81_Hrm/F_85_Lon/";
                    string currentptah = "InterfaceLoan?Type=&comcod=" + comcod + "&refno=" + deptcode + "&ltrnid=" + ltrnid + "&Date=" + frmdate + "&usrid=" + suserid + "&RoleType=" + roletype;
                    string totalpath = uhostname + currentptah;


                    string maildescription = "Dear Sir, Please Approve loan Request." + "<br> Employee ID Card : " + idcard + ",<br>" + "Employee Name : " + empname + ",<br>" + "Designation : " + empdesig + "," + "<br>" +
                         "Department Name : " + deptname + "," + "<br>" + "Leave Type : " + leavedesc + ",<br>" + " Request id: " + ltrnid + ". <br>";
                    maildescription += htmtableboyd;
                    maildescription += "<div style='color:red'><a style='color:blue; text-decoration:underline' href = '" + totalpath + "'>Click for Approved</a> or Login ERP Software and check Loan Interface</div>" + "<br/>";

                    #region
                    
                    string msgbody = maildescription;
                    bool result2 = UserNotify.SendNotification(subj, msgbody, suserid);
                    if (compsms == "True")
                    {
                        SendSmsProcess sms = new SendSmsProcess();
                        string SMSText = "New Loan Request Create Date : " + frmdate + " Effective Date " + todate;// 
                        bool resultsms = sms.SendSmmsPwd(comcod, SMSText, supphone);
                    }
                    if (compmail == "True")
                    {
                        bool Result_email = UserNotify.SendEmailPTL(hostname, portnumber, frmemail, psssword, subj, empname, empdesig, deptname, compName, tomail, msgbody, isSSL);
                        if (Result_email == false)
                        {
                            string Messagesd = "Loan Applied  but Notification has not been sent, Email or SMTP info Empty";
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);
                        }
                    }
                    #endregion
                }

            }
            catch (Exception ex)
            {
                string Messagesd = "Loan Applied but Notification has not been sent " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);
            }

        }

        protected void lnkEdited_Click(object sender, EventArgs e)
        {
            string Message;
            Hashtable hst = (Hashtable)Session["tblLogin"];

            string comcod = this.GetCompCode();
            string empid = this.ddlEmpList.SelectedValue.ToString();
            //string empid = hst["empid"].ToString() ?? "";
            string id = this.txtLoanId.Text.ToString().Remove(0,3);
            string loantype = ddlLoanType.SelectedValue.ToString() ?? "";
            string loanamt = "0" + this.txtLoanAmt.Text.ToString();
            string instlnum = this.txtInstNum.Text.ToString() ?? "0";
            string perinstlamt = "0" + this.txtAmtPerIns.Text.ToString() ?? "0";
            string loandesc = this.txtLoanDescc.Text.ToString();
            string rate = "0" + this.txtrt.Text.ToString() ?? "0";
            string effedat = Convert.ToDateTime(this.txtEffDate.Text).ToString("dd-MMM-yyyy") ?? "";
            string pstdusredt = hst["usrid"].ToString();
            string pstdssnedt = hst["session"].ToString();
            string pstdtrmnledt = hst["compname"].ToString();
            string postdateedited = System.DateTime.Now.ToString("dd-MMM-yyy") ?? "";
            string lnstatus ="0";

            string createDate = Convert.ToDateTime(this.txtcreateDate.Text).ToString("dd-MMM-yyyy") ?? "";
            string othincm = "0" + this.txtOI.Text.ToString() ?? "0";
            string othdeduct = "0" + this.txtOD.Text.ToString();
            string stddeduct = "0" + this.txtStd.Text.ToString();



            //maincode = (editedid != "") ? editedid : maincode;
            bool result = HRData.UpdateTransInfo3(comcod, "dbo_hrm.SP_ENTRY_LOANAPP", "INSERTLOAN", empid, id, loantype, loanamt, instlnum, perinstlamt, loandesc, rate, effedat, "", "", "", "", pstdusredt, pstdssnedt, pstdtrmnledt, postdateedited, createDate, stddeduct, othincm, othdeduct, lnstatus, "");
            if (result == true)
            {
                this.getAllData();
                this.LoantState.SelectedIndex = Convert.ToInt32(hiddenSeletedIndex.Value);
                Message = "Successfully Updated";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Message + "');", true);              
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#ApplyLoan", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#ApplyLoan').hide();", true);


            }
            else
            {
                this.getAllData();
                this.LoantState.SelectedIndex = Convert.ToInt32(hiddenSeletedIndex.Value);
                Message = "Update Fail";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Message + "');", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "ModalLoanClose();", true);
            }

            LoantState_SelectedIndexChanged(null, null);
        }

        protected void gvProcess_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkEdit = (LinkButton)e.Row.FindControl("pendlnEdit");
                LinkButton lnkApp = (LinkButton)e.Row.FindControl("pendlnAproved");
                LinkButton lnkview = (LinkButton)e.Row.FindControl("proslnView");
                
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string userid = hst["usrid"].ToString();
                string empusrid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "empusrid")).ToString().Trim();
                string dptusrid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "dptusid")).ToString().Trim();
                string empid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "empid")).ToString().Trim();                
                string mgtusid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mgtusid")).ToString().Trim();
                string refno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "deptid")).ToString();        
                string id = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "id")).ToString();

                if ((userid == dptusrid) || (userid == mgtusid))
                {
                    lnkApp.Visible = true; 
                }
                if (userid == empusrid)
                {
                    lnkEdit.Visible = true;
                }
                if ((userid == dptusrid) || (userid == mgtusid) || (userid == empusrid))
                {
                    lnkview.Visible = true;
                }

            }
        }
    }
}