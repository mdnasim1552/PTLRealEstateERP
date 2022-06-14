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

        protected void lnkAdd_Click(object sender, EventArgs e)
        {

            string Message;
            string comcod = this.GetCompCode();
            string empid = "122";
            string id = "";
            string loantype = ddlLoanType.SelectedItem.Text.ToString()??"";
            string loanamt = this.txtLoanAmt.Text.ToString();
            string instlnum = this.txtInstNum.Text.ToString();
            string perinstlamt = this.txtAmtPerIns.Text.ToString();
            string loandesc = this.txtLoanDescc.Text.ToString();
            string rate = this.txtrt.Text.ToString()??"";
            string effectivedate = Convert.ToDateTime(this.txtEffDate.Text).ToString("dd-MMM-yyyy")??"";
            string posteddate = System.DateTime.Now.ToString("dd-MMM-yyy")??"";
            string posteduserid = "11";
            string postedsession = "session";
            string postedterminalid = "terminal";
            string posteduseridedited = "";
            string postedsessionedited = "";
            string postedterminalidedited = "";
            string postdateedited = "";


            //maincode = (editedid != "") ? editedid : maincode;

            bool result = HRData.UpdateTransInfo2(comcod, "dbo_hrm.SP_ENTRY_LOANAPP", "INSERTLOAN", empid, id, loantype, loanamt, instlnum, perinstlamt, loandesc, rate, effectivedate, posteddate, posteduserid, postedsession, postedterminalid, posteduseridedited,postedsessionedited, postedterminalidedited, postdateedited,"","","","");

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