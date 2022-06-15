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
                this.txtcreateDate.Text= System.DateTime.Now.ToString("dd-MMM-yyy") ?? "";
            }
        }


        private void getAllData()
        {

            string comcod = this.GetCompCode();
            DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_LOANAPP", "GETLOAN", "", "", "", "", "", "", "", "", "");
            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
                this.gvloan.DataSource = null;
                this.gvloan.DataBind();
                return;
            }

            this.gvloan.DataSource = ds.Tables[0];
            this.gvloan.DataBind();
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

            string loanidFull = dt2.Rows[0]["lnno"].ToString();
            string loanstr = loanidFull.Substring(0, 8);

            string loannum = loanidFull.Remove(0, 8);
            int ln = Convert.ToInt32(loannum) + 1;

            this.txtLoanId.Text = loanstr.ToString() + ln.ToString();


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
            string id = "";
            string loantype = ddlLoanType.SelectedItem.Text.ToString()??"";
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

            string createDate = Convert.ToDateTime(this.txtcreateDate.Text).ToString("dd-MMM-yyyy") ?? "";
            string othincm = "0"+this.txtOI.Text.ToString()??"0";
            string othdeduct = "0"+this.txtOD.Text.ToString();
            string stddeduct = "0"+this.txtStd.Text.ToString();



            //maincode = (editedid != "") ? editedid : maincode;
            bool result = HRData.UpdateTransInfo3(comcod, "dbo_hrm.SP_ENTRY_LOANAPP", "INSERTLOAN", empid, id, loantype, loanamt, instlnum, perinstlamt, loandesc, rate, effedat, posteddate, pstdusrid, pstdsession, pstdtrmnlid, pstdusredt, pstdssnedt, pstdtrmnledt, postdateedited, createDate, stddeduct, othincm, othdeduct,"","");
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


    }
}