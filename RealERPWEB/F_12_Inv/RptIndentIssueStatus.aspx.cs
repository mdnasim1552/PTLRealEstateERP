using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.IO;
using RealERPLIB;
using RealERPLIB;
using System.Data.OleDb;
using System.Data;

namespace RealERPWEB.F_12_Inv
{
    public partial class RptIndentIssueStatus : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                ((Label)this.Master.FindControl("lblTitle")).Text = "Indent Issue Status";

                this.GetStoreName();
                this.GetDeparment();
            }
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void GetStoreName()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = (hst["usrid"].ToString());
            string comcod = this.GetCompCode();

            // DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO02", "GETPROJECT", Srchname, "", "", "", "", "", "", "", "");
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_REQ_CENSTORE", "PRJCODELIST", "11020099%", "FxtAst", "", userid, "", "", "", "", "");
            if (ds2 == null)
                return;
            ViewState["tblStoreType"] = ds2.Tables[0];
            this.ddlStoreName.DataTextField = "actdesc1";
            this.ddlStoreName.DataValueField = "actcode";
            this.ddlStoreName.DataSource = ds2.Tables[0];
            this.ddlStoreName.DataBind();


        }
        protected void GetDeparment()
        {
            string comcod = this.GetCompCode();
            //string txtSProject = "%" + this.txtSrcPro.Text.Trim() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "FXTASSTGETDEPARTMENT", "%%", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ds1.Tables[0].Rows.Add(comcod, "000000000000", "Department");
            ds1.Tables[0].Rows.Add(comcod, "AAAAAAAAAAAA", "-------Select-----------");


            this.ddldpt.DataTextField = "fxtgdesc";
            this.ddldpt.DataValueField = "fxtgcod";
            this.ddldpt.DataSource = ds1.Tables[0];
            this.ddldpt.DataBind();
            this.ddldpt.SelectedValue = "AAAAAAAAAAAA";
           
        }
        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {
           
            string comcod = this.GetComeCode();
            string prjcode = this.ddlStoreName.SelectedValue.ToString() == "000000000000" ? "11%" : this.ddlStoreName.SelectedValue.ToString() + "%";
            string frmdate = this.txtfrmdate.Text.Trim();
            string todate = this.txttodate.Text.Trim();
            string department = this.ddldpt.SelectedValue.ToString() == "000000000000" ? "94%" : this.ddldpt.SelectedValue.ToString() + "%";


            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_INDENT_STATUS", "RPTMATINDENTISSUESTATUS", prjcode, frmdate, todate, department, "", "", "", "");
            if (ds1 == null)
            {
                this.gvIssuest.DataSource = null;
                this.gvIssuest.DataBind();

                return;
            }
            Session["tblindissuestatus"] = ((DataTable)ds1.Tables[0]).Copy();

            Session["tblindissuestatus01"] = ds1.Tables[0];

           

            this.Data_Bind();
        }
        private string GetComeCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void Data_Bind()
        {

            this.gvIssuest.DataSource = (DataTable)Session["tblindissuestatus"];
            this.gvIssuest.DataBind();
       

        }
    }
}