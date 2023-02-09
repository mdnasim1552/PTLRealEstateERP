using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_09_PImp
{
    public partial class WorkExecutionFinal : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitPage();
            }
        }

        public void InitPage()
        {
            txtEntryDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
            GetProject();
        }
        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            return comcod;
        }
        private string GetUserId()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            return userid;
        }
        public void GetProject()
        {
            string userid = GetUserId();
            string comcod = GetComCode();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETISSUEPRJLIST01", "%", "", userid, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProject.DataTextField = "actdesc1";
            this.ddlProject.DataValueField = "actcode";
            this.ddlProject.DataSource = ds1.Tables[0];
            this.ddlProject.DataBind();
        }
        #region Generate Issue No for Work, Material and Labour
        public void GetWENNO()
        {

        }
        #endregion
        //-----410100000000
        public void GetCategory()
        {

        }
        //-----Full 41
        public void GetWorkList()
        {

        }
        public void GetFloor()
        {

        }

        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            GetProject();
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {

        }

        protected void ddlcatagory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlworklist_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}