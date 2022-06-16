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
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../../AcceessError.aspx");
                getAllData();
                GetLoanType();
                GetGross();
                GetPrevLoan();
                this.txtcreateDate.Text= System.DateTime.Now.ToString("dd-MMM-yyyy");

                DateTime now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);

                this.txtfrmdate.Text = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");
                this.txttodate.Text = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");
                //LoanReport();
                this.LoantState.SelectedIndex = 0;
                LoantState_SelectedIndexChanged(null,null);

            }
        }





        private void getAllData()
        {
    

            string comcod = this.GetCompCode();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_LOANAPP", "GETLOAN", "", "", "", "", "", "", "", "", "");
            if (ds1 == null || ds1.Tables[0].Rows.Count == 0)
              return;

            this.LoantState.Items[0].Text = "<h4 class='text-center'><span class='lbldata'>0</span></h4>" + "<span class='lbldata2'>" + "Loan Queue" + "</span>";
            this.LoantState.Items[1].Text = "<h4 class='text-center'><span class='lbldata'>0</span></h4>" + "<span class=lbldata2>" + "Loan Process" + "</span>";
            this.LoantState.Items[2].Text = "<h4 class='text-center'><span class='lbldata'>0</span></h4>" + "<span class=lbldata2>" + "Loan Approval" + "</span>";
            this.LoantState.Items[3].Text = "<h4 class='text-center'><span class='lbldata'>0</span></h4>" + "<span class=lbldata2>" + "Loan Generate" + "</span>";
            this.LoantState.Items[4].Text = "<h4 class='text-center'><span class='lbldata'>0</span></h4>" + "<span class=lbldata2>" + "Loan Completed" + "</span>";



            DataTable dt = new DataTable();
            DataView dv = new DataView();

            //pending
            dt = ((DataTable)ds1.Tables[0]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("lnstatus=0");
            this.Data_Bind("gvPending", dv.ToTable());

            //Process
            dt = ((DataTable)ds1.Tables[0]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("lnstatus=1");
            this.Data_Bind("gvProcess", dv.ToTable());

           // Approved
           dt = ((DataTable)ds1.Tables[0]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("lnstatus=1");
            this.Data_Bind("gvApproved", dv.ToTable());

            //Generate
            dt = ((DataTable)ds1.Tables[0]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("lnstatus=1");
            this.Data_Bind("gvGen", dv.ToTable());

            //Completed
            dt = ((DataTable)ds1.Tables[0]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("lnstatus=1");
            this.Data_Bind("gvCompleted", dv.ToTable());
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
            
           this.ddlLoanTypeSearch.DataTextField = "loantype";
            this.ddlLoanTypeSearch.DataValueField = "gcod";
            this.ddlLoanTypeSearch.DataSource = ds1.Tables[0];
            this.ddlLoanTypeSearch.DataBind();
        }

        private void GetPrevLoan()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string empid = hst["empid"].ToString()??"";
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
            string empid = hst["empid"].ToString() ?? "";
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

        protected void lnkAdd_Click(object sender, EventArgs e)
        {

            string Message;
            Hashtable hst = (Hashtable)Session["tblLogin"];
          
            string comcod = this.GetCompCode();
            string empid = hst["empid"].ToString()??"";
            string id = "0";
            string loantype = ddlLoanType.SelectedValue.ToString()??"";
            string loanamt = "0"+this.txtLoanAmt.Text.ToString();
            string instlnum = "0"+this.txtInstNum.Text.ToString()??"0";
            string perinstlamt = "0"+this.txtAmtPerIns.Text.ToString()??"0";
            string loandesc = this.txtLoanDescc.Text.ToString();
            string rate = "0"+this.txtrt.Text.ToString()??"0";
            string effedat = Convert.ToDateTime(this.txtEffDate.Text).ToString("dd-MMM-yyyy")??"";
            string posteddate = System.DateTime.Now.ToString("dd-MMM-yyy")??"";
            string pstdusrid = hst["usrid"].ToString();
            string pstdsession = hst["session"].ToString();
            string pstdtrmnlid = hst["compname"].ToString();
            string pstdusredt = "";
            string pstdssnedt = "";
            string pstdtrmnledt = "";
            string postdateedited = "";
            string lnstatus = "0";

            string createDate = Convert.ToDateTime(this.txtcreateDate.Text).ToString("dd-MMM-yyyy") ?? "";
            string othincm = "0"+this.txtOI.Text.ToString()??"0";
            string othdeduct = "0"+this.txtOD.Text.ToString();
            string stddeduct = "0"+this.txtStd.Text.ToString();



            //maincode = (editedid != "") ? editedid : maincode;
            bool result = HRData.UpdateTransInfo3(comcod, "dbo_hrm.SP_ENTRY_LOANAPP", "INSERTLOAN", empid, id, loantype, loanamt, instlnum, perinstlamt, loandesc, rate, effedat, posteddate, pstdusrid, pstdsession, pstdtrmnlid, pstdusredt, pstdssnedt, pstdtrmnledt, postdateedited, createDate, stddeduct, othincm, othdeduct, lnstatus, "");
            if (result == true)
            {

                Message = "Successfully Updated";
                //ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Message + "');", true);
                Response.Redirect(Request.RawUrl);
            }
            else
            {
                Response.Redirect(Request.RawUrl);
            }


        }
        protected void lnkUpdate_Click(object sender, EventArgs e)
        {

            string Message;
            Hashtable hst = (Hashtable)Session["tblLogin"];

            string comcod = this.GetCompCode();
            string empid = hst["empid"].ToString() ?? "";
            string id = this.txtLoanId.Text.ToString();
            string loantype = ddlLoanType.SelectedValue.ToString() ?? "";
            string loanamt = "0" + this.txtLoanAmt.Text.ToString();
            string instlnum = "0" + this.txtInstNum.Text.ToString() ?? "0";
            string perinstlamt = "0" + this.txtAmtPerIns.Text.ToString() ?? "0";
            string loandesc = this.txtLoanDescc.Text.ToString();
            string rate = "0" + this.txtrt.Text.ToString() ?? "0";
            string effedat = Convert.ToDateTime(this.txtEffDate.Text).ToString("dd-MMM-yyyy") ?? "";
            string posteddate = System.DateTime.Now.ToString("dd-MMM-yyy") ?? "";
            string pstdusrid = hst["usrid"].ToString();
            string pstdsession = hst["session"].ToString();
            string pstdtrmnlid = hst["compname"].ToString();
            string pstdusredt = "";
            string pstdssnedt = "";
            string pstdtrmnledt = "";
            string postdateedited = "";
            string lnstatus = "0";

            string createDate = Convert.ToDateTime(this.txtcreateDate.Text).ToString("dd-MMM-yyyy") ?? "";
            string othincm = "0" + this.txtOI.Text.ToString() ?? "0";
            string othdeduct = "0" + this.txtOD.Text.ToString();
            string stddeduct = "0" + this.txtStd.Text.ToString();



            //maincode = (editedid != "") ? editedid : maincode;
            bool result = HRData.UpdateTransInfo3(comcod, "dbo_hrm.SP_ENTRY_LOANAPP", "INSERTLOAN", empid, id, loantype, loanamt, instlnum, perinstlamt, loandesc, rate, effedat, posteddate, pstdusrid, pstdsession, pstdtrmnlid, pstdusredt, pstdssnedt, pstdtrmnledt, postdateedited, createDate, stddeduct, othincm, othdeduct, lnstatus, "");
            if (result == true)
            {

                Message = "Successfully Updated";
                //ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Message + "');", true);
                Response.Redirect(Request.RawUrl);
            }
            else
            {
                Response.Redirect(Request.RawUrl);
            }

        }

        protected void LoantState_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = this.LoantState.SelectedIndex;
            switch (index)
            {
                case 0:
                    this.pnlQue.Visible = true;
                    this.pnlLoanProc.Visible = false;
                    this.pnlLoanAppr.Visible = false;
                    this.pnlLoangen.Visible = false;
                    this.pnlLoanComp.Visible = false;
                break;

                case 1:
                    this.pnlQue.Visible = false;
                    this.pnlLoanProc.Visible = true;
                    this.pnlLoanAppr.Visible = false;
                    this.pnlLoangen.Visible = false;
                    this.pnlLoanComp.Visible = false;
                    break;


                case 2:
                    this.pnlQue.Visible = false;
                    this.pnlLoanProc.Visible = false;
                    this.pnlLoanAppr.Visible = true;
                    this.pnlLoangen.Visible = false;
                    this.pnlLoanComp.Visible = false;
                    break;

                case 3:
                    this.pnlQue.Visible = false;
                    this.pnlLoanProc.Visible = false;
                    this.pnlLoanAppr.Visible = false;
                    this.pnlLoangen.Visible = true;
                    this.pnlLoanComp.Visible = false;
                    break;

                case 4:
                    this.pnlQue.Visible = false;
                    this.pnlLoanProc.Visible = false;
                    this.pnlLoanAppr.Visible = false;
                    this.pnlLoangen.Visible = false;
                    this.pnlLoanComp.Visible = true;
                    break;
            }
        }

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


            string comcod = this.GetCompCode();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string lnid = ((Label)this.gvPending.Rows[index].FindControl("lblidPend")).Text.ToString();
            string empid= ((Label)this.gvPending.Rows[index].FindControl("lblpendempid")).Text.ToString();
            DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_LOANAPP", "GETLOANBYID", empid, lnid, "", "", "", "", "", "", "");
            if (ds == null || ds.Tables.Count == 0)
                return;
            
            DataTable dt = ds.Tables[0];
            this.txtLoanId.Text ="Ln-" +dt.Rows[0]["id"].ToString().;
            this.txtcreateDate.Text = Convert.ToDateTime(dt.Rows[0]["createdate"]).ToString("dd-MMM-yyyy");
            this.txtLoanAmt.Text = dt.Rows[0]["loanamt"].ToString() ?? "";
            this.txtInstNum.Text = dt.Rows[0]["instlnum"].ToString() ?? "";
            this.txtAmtPerIns.Text = dt.Rows[0]["perinstlamt"].ToString() ?? "";
            this.txtStd.Text = dt.Rows[0]["statdeduction"].ToString() ?? "";
            this.txtOI.Text = dt.Rows[0]["othincome"].ToString() ?? "";
            this.txtOD.Text = dt.Rows[0]["othdeduction"].ToString() ?? "";
            this.txtrt.Text = dt.Rows[0]["rate"].ToString() ?? "";
            this.txtEffDate.Text = Convert.ToDateTime(dt.Rows[0]["effdate"]).ToString("dd-MMM-yyyy");
            string loantype = dt.Rows[0]["loantype"].ToString().Trim();
            ddlLoanType.ClearSelection();
            ddlLoanType.Items.FindByValue(loantype).Selected = true;
            this.txtLoanDescc.Text = dt.Rows[0]["loandesc"].ToString() ?? "";
            this.lnkUpdate.Visible = true;
            this.lnkAdd.Visible = false;
            this.lnkCancel.Visible = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "EditLoan();", true);
        }


        protected void pendlnView_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string lnid = ((Label)this.gvPending.Rows[index].FindControl("lblidPend")).Text.ToString().Trim();
            string empid = ((Label)this.gvPending.Rows[index].FindControl("lblpendempid")).Text.ToString().Trim();
            DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_LOANAPP", "GETLOANBYID", empid, lnid ,"", "", "", "", "", "", "", "");
            if (ds == null || ds.Tables.Count == 0)
                return;
            DataTable dt = ds.Tables[0];

      
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
            this.txtLoanId.Text = dt.Rows[0]["id"].ToString() ?? "";
            this.txtcreateDate.Text =Convert.ToDateTime(dt.Rows[0]["createdate"]).ToString("dd-MMM-yyyy");
            this.txtLoanAmt.Text = dt.Rows[0]["loanamt"].ToString() ?? "";
            this.txtInstNum.Text = dt.Rows[0]["instlnum"].ToString() ?? "";
            this.txtAmtPerIns.Text = dt.Rows[0]["perinstlamt"].ToString() ?? "";
            this.txtStd.Text = dt.Rows[0]["statdeduction"].ToString() ?? "";
            this.txtOI.Text = dt.Rows[0]["othincome"].ToString() ?? "";
            this.txtOD.Text = dt.Rows[0]["othdeduction"].ToString() ?? "";
            this.txtrt.Text = dt.Rows[0]["rate"].ToString() ?? "";
            this.txtEffDate.Text =Convert.ToDateTime( dt.Rows[0]["effdate"]).ToString("dd-MMM-yyyy");
            ddlLoanType.ClearSelection();
            string loantype= dt.Rows[0]["loantype"].ToString().Trim();

            ddlLoanType.Items.FindByValue(loantype).Selected = true;
            this.txtLoanDescc.Text = dt.Rows[0]["loandesc"].ToString() ?? "";
            this.lnkUpdate.Visible = false;
            this.lnkAdd.Visible = false;
            this.lnkCancel.Visible = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "ViewLoan();", true);
        }

        protected void lnkApplyModal_Click(object sender, EventArgs e)
        {

            GetLoanType();
            GetGross();
            GetPrevLoan();

            this.txtcreateDate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
            this.txtLoanAmt.Text = "";
            this.txtInstNum.Text = "";
            this.txtStd.Text = "";
            this.txtOI.Text = "";
            this.txtOD.Text = "";
            this.txtrt.Text = "";
            this.txtEffDate.Text = "";
            this.txtLoanDescc.Text = "";

            this.lnkAdd.Visible = true;

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
            this.lnkCancel.Visible = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenApplyLoan();", true);
        }


        protected void confirmDelete_click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string msg = "";

            string lnid = this.delid.InnerText;
            string empid = this.delempid.InnerText;
            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_LOANAPP", "LOANREMOVE", empid, lnid, "", "", "", "", "", "", "");
            if (result)
            {
                msg = "Successfully Deleted";
                Response.Redirect(Request.RawUrl);
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);

                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Leave Requset Forward";
                    string eventdesc = "Leave Requset Forward";
                    string eventdesc2 = lnid;
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

        protected void confmDelModal_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string lnid = ((Label)this.gvPending.Rows[index].FindControl("lblidPend")).Text.ToString();
            string empid = ((Label)this.gvPending.Rows[index].FindControl("lblpendempid")).Text.ToString();
            this.delid.InnerText = lnid;
            this.delempid.InnerText = empid.ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenDeleteModal();", true);
            
        }
    }
}