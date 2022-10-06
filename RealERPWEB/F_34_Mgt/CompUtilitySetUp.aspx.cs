using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.IO;
using RealERPLIB;
using RealERPLIB;
using System.Data.OleDb;
namespace RealERPWEB.F_34_Mgt
{
    public partial class CompUtilitySetUp : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = "Company Utility SetUp";
                this.GetComUtility();
            }
                
        }
        private void GetComUtility()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = (hst["usrid"].ToString());
            string comcod = this.GetComeCode();

            // DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO02", "GETPROJECT", Srchname, "", "", "", "", "", "", "", "");
            DataSet ds2 = purData.GetTransInfo("","SP_UTILITY_ACCESS_PRIVILEGES", "GETCOMPUITILITYSETUP", "", "", "","", "", "", "", "", "");
            if (ds2 == null)
                return;
            ViewState["tblCOMPUITILITYSETUP"] = ds2.Tables[0];
            DataTable dt = (DataTable)ViewState["tblCOMPUITILITYSETUP"];
           this.txtIdcard.Text = dt.Rows[0]["HR_IDCARDLEN"].ToString();
           this.txtstrtdat.Text = dt.Rows[0]["HR_ATTSTART_DAT"].ToString();
           this.chkCRMddata.Checked =  Convert.ToBoolean(dt.Rows[0]["CRM_BACKDATAIN"].ToString());
           this.chkBag.Checked =  Convert.ToBoolean(dt.Rows[0]["LANG_BANG"].ToString());
           this.chkPay.Checked =  Convert.ToBoolean(dt.Rows[0]["ISVISIBILEPAYSLIP"].ToString());
           this.chkLvapp.Checked =  Convert.ToBoolean(dt.Rows[0]["LVAPP_SUPERVISOR"].ToString());



        }
        private string GetComeCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

     

        protected void lnkUpdate_Click(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)Session["tblCOMPUITILITYSETUP"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string IdCardLen = this.txtIdcard.Text.ToString();
            string Startdat = this.txtstrtdat.Text;
            string chkCRMddata = this.chkCRMddata.Checked.ToString();
            string chkBag = this.chkBag.Checked.ToString();
            string chkLvapp = this.chkLvapp.Checked.ToString();
            string chkPay = this.chkPay.Checked.ToString();

            bool result = purData.UpdateTransInfo(comcod, "SP_UTILITY_ACCESS_PRIVILEGES", "INSERTUPDATECOMPUITILITYSETUP", IdCardLen, Startdat, chkCRMddata, chkBag, chkLvapp, chkPay, "", "",
                     "", "", "", "", "", "", "");
            if (!result)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Updated Fail..!!');", true);

            }

            else
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Update Successful');", true);
            }

        }


        //protected void lnkbtnOk_Click(object sender, EventArgs e)
        //{

        //    string comcod = this.GetComeCode();
        //    string prjcode = this.ddlStoreName.SelectedValue.ToString() == "000000000000" ? "11%" : this.ddlStoreName.SelectedValue.ToString() + "%";
        //    string frmdate = this.txtfrmdate.Text.Trim();
        //    string todate = this.txttodate.Text.Trim();
        //    string department = this.ddldpt.SelectedValue.ToString() == "000000000000" ? "94%" : this.ddldpt.SelectedValue.ToString() + "%";


        //    DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_INDENT_STATUS", "RPTMATINDENTISSUESTATUS", prjcode, frmdate, todate, department, "", "", "", "");
        //    if (ds1 == null)
        //    {
        //        this.gvIssuest.DataSource = null;
        //        this.gvIssuest.DataBind();

        //        return;
        //    }
        //    Session["tblindissuestatus"] = ((DataTable)ds1.Tables[0]).Copy();

        //    Session["tblindissuestatus01"] = ds1.Tables[0];



        //    this.Data_Bind();
        //}
    }
}