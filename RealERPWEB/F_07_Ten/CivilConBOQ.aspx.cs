using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_07_Ten
{
    public partial class CivilConBOQ : System.Web.UI.Page
    {
        ProcessAccess ImpleData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Civil Construction BOQ";

                this.GetProjectsList();
                this.GetWorksGroup();
                
                this.CreateTable();
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            //((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
        }
        private void CreateTable()
        {
            DataTable tblt01 = new DataTable();
            tblt01.Columns.Add("actcode", Type.GetType("System.String"));
            tblt01.Columns.Add("actdesc", Type.GetType("System.String"));
            
            tblt01.Columns.Add("subcode", Type.GetType("System.String"));            
            tblt01.Columns.Add("subdesc", Type.GetType("System.String"));
            tblt01.Columns.Add("sdetails", Type.GetType("System.String"));
            tblt01.Columns.Add("trnqty", Type.GetType("System.Double"));
            tblt01.Columns.Add("trnunit", Type.GetType("System.String"));
            tblt01.Columns.Add("trnrate", Type.GetType("System.Double"));
            tblt01.Columns.Add("trnamt", Type.GetType("System.Double"));
            tblt01.Columns.Add("trnsbtamt", Type.GetType("System.Double"));
            tblt01.Columns.Add("actcostamt", Type.GetType("System.Double"));
            tblt01.Columns.Add("costvatoh", Type.GetType("System.Double"));
            tblt01.Columns.Add("actant", Type.GetType("System.Double"));
            tblt01.Columns.Add("diffamt", Type.GetType("System.Double"));             
            tblt01.Columns.Add("rmrks", Type.GetType("System.String"));
            
            ViewState["tblt01"] = tblt01;
        }
        private void GetProjectsList()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_ENTRY_PRJ_BUDGET", "PRJCODELIST", "%%", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProject.DataTextField = "prjdesc2";
            this.ddlProject.DataValueField = "prjcod";
            this.ddlProject.DataSource = ds1.Tables[0];
            this.ddlProject.DataBind();
        }

        private void GetWorksGroup()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_TANDER_PROCESS", "GETWORKGROUPLIST", "%%", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlWorkGroup.DataTextField = "workdesc";
            this.ddlWorkGroup.DataValueField = "workcode";
            this.ddlWorkGroup.DataSource = ds1.Tables[0];
            this.ddlWorkGroup.DataBind();
        }
        private void GetWorksList()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_TANDER_PROCESS", "GETWORKLIST", "%%", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlWorkList.DataTextField = "workdesc";
            this.ddlWorkList.DataValueField = "workcode";
            this.ddlWorkList.DataSource = ds1.Tables[0];
            this.ddlWorkList.DataBind();
        }


        protected void lnkbtnOK_Click(object sender, EventArgs e)
        {

        }

        protected void lnkbtnAdd_Click(object sender, EventArgs e)
        {
            DataTable tblt01 = (DataTable)ViewState["tblt01"];

            string actcode = this.ddlProject.SelectedValue.ToString();
            string workcode = this.ddlWorkList.SelectedValue.ToString();


        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlWorkGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetWorksList();
        }
    }
}