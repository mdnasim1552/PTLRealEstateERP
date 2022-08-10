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


namespace RealERPWEB.F_81_Hrm.F_81_Rec
{
    public partial class NewRecruitment : System.Web.UI.Page
    {
        ProcessAccess RecData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.getDesig();
                //this.getDept();
            }

        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


       private void GetData()
        {
            string comcod = this.GetComeCode();
            DataSet ds = RecData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_NEW_REC", "GEETCODE", "%%", "%%", "", "", "", "", "", "");
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return;
            DataTable dt = ds.Tables[0];
            DataTable dt1 = (DataTable)ViewState["dtDesig"];


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string gcod = dt.Rows[i]["gcod"].ToString();
            }
        }

        private void getDept()
        {
           string comcod = this.GetComeCode();
            DataSet ds3  = RecData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_NEW_REC", "GETDEPTNAME",  "%%", "%%", "", "", "", "", "", "");
            if (ds3 == null || ds3.Tables[0].Rows.Count == 0)
                return;
            //this.ddldept.DataTextField = "deptdesc";
   
            //this.ddldept.DataValueField = "deptcode";
            //this.ddldept.DataSource = ds3.Tables[0];
            //this.ddldept.DataBind();
        }

        private void getDesig()
        {
            string comcod = this.GetComeCode();
            DataSet ds3 = RecData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_NEW_REC", "GETDESIG", "", "", "", "", "", "");
            if (ds3 == null || ds3.Tables[0].Rows.Count == 0)
                return;
            ViewState["dtDesig"] = ds3.Tables[0];
            //this.ddldesig.DataTextField = "hrgdesc";
            //this.ddldesig.DataValueField = "hrgcod";
            //this.ddldesig.DataSource = ds3.Tables[0];
            //this.ddldesig.DataBind();
        }

        //protected void lnkSave_Click(object sender, EventArgs e)
        //{
        //    string comcod = this.GetComeCode();
        //    string name = this.txtname.Text ?? "";
        //    string desig = this.ddldesig.SelectedValue.ToString() ?? "";
        //    string mobile = this.txtmobile.Text ?? "";
        //    string email = this.txtemail.Text ?? "";
        //    string peradd = this.txtPerAdd.Text ?? "";
        //    string preadd = this.txtPreAdd.Text ?? "";
        //    string filename = "docfile";
        //    string dept = this.ddldept.SelectedValue.ToString() ?? "";


        //    bool result = RecData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_NEW_REC", "INSERTUPDATE", name, desig, mobile, email, peradd, peradd, filename, dept, "", "", "", "", "", "", "");

        //}
    }
}