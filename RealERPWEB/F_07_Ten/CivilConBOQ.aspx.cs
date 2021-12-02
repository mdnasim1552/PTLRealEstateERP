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
            tblt01.Columns.Add("qty", Type.GetType("System.Double"));
            tblt01.Columns.Add("unit", Type.GetType("System.String"));
            tblt01.Columns.Add("rate", Type.GetType("System.Double"));
            tblt01.Columns.Add("ordam", Type.GetType("System.Double"));
            tblt01.Columns.Add("sbtrate", Type.GetType("System.Double"));
            tblt01.Columns.Add("sbtamt", Type.GetType("System.Double"));
            tblt01.Columns.Add("costvatoh", Type.GetType("System.Double"));
            tblt01.Columns.Add("actamt", Type.GetType("System.Double"));
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

            string workgrp = (this.ddlWorkGroup.SelectedValue.ToString() == "000000000000") ? "%" : ASTUtility.Left(this.ddlWorkGroup.SelectedValue.ToString(), 4) + "%";

            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_TANDER_PROCESS", "GETWORKLIST", workgrp, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlWorkList.DataTextField = "workdesc";
            this.ddlWorkList.DataValueField = "workcode";
            this.ddlWorkList.DataSource = ds1.Tables[0];
            this.ddlWorkList.DataBind();

            //this.DropCheck1.DataTextField = "workdesc";
            //this.DropCheck1.DataValueField = "workcode";
            //this.DropCheck1.DataSource = ds1.Tables[0];
            //this.DropCheck1.DataBind();

        }


        protected void lnkbtnOK_Click(object sender, EventArgs e)
        {
            if (this.lnkbtnOK.Text == "Ok")
            {
                this.lnkbtnOK.Text = "New";
                this.ddlProject.Enabled = false;
                this.divResource.Visible = true;
                this.gvCivilBoq.Visible = true;

                return;
            }
            this.lnkbtnOK.Text = "Ok";
            this.divResource.Visible = false;
            this.gvCivilBoq.Visible = false;

            this.ddlProject.Enabled = true;
            


        }

        protected void lnkbtnAdd_Click(object sender, EventArgs e)
        {


            DataTable tblt01 = (DataTable)ViewState["tblt01"];

            string actcode = this.ddlProject.SelectedValue.ToString();
            string actdesc = this.ddlProject.SelectedItem.ToString();
            string workcode =  this.ddlWorkList.SelectedValue.ToString();
            string subdesc =  this.ddlWorkList.SelectedItem.ToString();

            DataRow[] dr2 = tblt01.Select("actcode='" + actcode + "'  and subcode='" + workcode + "'");
            if (dr2.Length > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Item Exist In The List" + "');", true);
                return;
            }

            DataRow dr1 = tblt01.NewRow();
            dr1["actcode"] = actcode;
            dr1["actdesc"] = actdesc;
            dr1["subcode"] = workcode;
            dr1["subdesc"] = subdesc;
            dr1["sdetails"] = "";

            dr1["qty"] = 0.00;
            dr1["unit"] = "";
            dr1["rate"] = 0.00;
            dr1["ordam"] = 0.00;
            dr1["sbtrate"] = 0.00;
            dr1["sbtamt"] = 0.00;
            dr1["costvatoh"] = 0.00;
            dr1["actamt"] = 0.00;
            dr1["diffamt"] = 0.00;
            dr1["rmrks"] = "";
            tblt01.Rows.Add(dr1);


            ViewState["tblt01"] = tblt01;
            this.Data_Bind();
        }

        protected void Data_Bind()
        {
            DataTable tblt01 = (DataTable)ViewState["tblt01"];


            this.gvCivilBoq.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvCivilBoq.DataSource = tblt01;
            this.gvCivilBoq.DataBind();

        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlWorkGroup_SelectedIndexChanged(object sender, EventArgs e)
        {

            GetWorksList();
        }

        protected void gvCivilBoq_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvCivilBoq_RowCreated(object sender, GridViewRowEventArgs e)
        {

        }
    }
}