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
    public partial class WorkExecutionWithIssue : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Permission Part
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                if ((!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp),
                        (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean(hst["permission"]))
                    Response.Redirect("~/AcceessError");
                ((Label)this.Master.FindControl("lblTitle")).Text = "Work Execution With Material Issue";
                if (Request.QueryString["Type"] != null && Request.QueryString["Type"].ToString() == "Edit")
                {
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Work Execution With Material Issue - EDIT MODE";
                }
                InitPage();
            }
        }
        //----SETUP----
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
        public void InitPage()
        {
            txtEntryDate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
            btnOK.Text = "<span class='fa fa-check-circle' style='color: white;' aria-hidden='true'></span> OK";
            GetProject();
            //Default Panel Visible false
        }
        //----END SETUP----
        protected void btnOK_Click(object sender, EventArgs e)
        {
            OnOKClick();
        }
        private void OnOKClick()
        {
            GetProject();
            if (btnOK.Text == "<span class='fa fa-check-circle' style='color: white;' aria-hidden='true'></span> OK")
            {
                //OK Click
                ddlProject.Enabled = false;
                pnl1.Visible = true;
                btnOK.Text = "<span class='fa fa-arrow-circle-left' style='color: white;' aria-hidden='true'></span> New";
                GetCategory();
                GetItem();
                GetDivision();
            }
            else
            {
                //New Click
                ddlProject.Enabled = true;
                pnl1.Visible = false;
                btnOK.Text = "<span class='fa fa-check-circle' style='color: white;' aria-hidden='true'></span> OK";
            }
        }      
        private void GetProject()
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
        private void GetCategory()
        {           
            string comcod = GetComCode();
            string pactcode = this.ddlProject.SelectedValue.ToString();
            string flrcode = "";
            string date = this.txtEntryDate.Text.Trim();
            string txtsrchItem = "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETITEMRESFLRCODE", pactcode, date, flrcode, txtsrchItem, "", "", "", "", "");

            Session["itemlist"] = ds1.Tables[0];
            Session["item"] = ds1.Tables[1];
            if (ds1 == null)
                return;

            this.ddlCategory.DataTextField = "sirdesc";
            this.ddlCategory.DataValueField = "mitemcode";
            this.ddlCategory.DataSource = ds1.Tables[2];
            this.ddlCategory.DataBind();
        }
        private void GetItem()
        {
            string itemcode = this.ddlCategory.SelectedValue.Substring(0, 4).ToString() + "%";
            DataTable dt = ((DataTable)Session["item"]).Copy();
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("itemcode like '" + itemcode + "' ");
            dt = dv.ToTable(true, "itemcode", "workitem");


            this.ddlItem.DataTextField = "workitem";
            this.ddlItem.DataValueField = "itemcode";
            this.ddlItem.DataSource = dt;
            this.ddlItem.DataBind();
        }
        public void GetDivision()
        {
            string Worklists = this.ddlItem.SelectedValue.ToString();
            DataTable dt = ((DataTable)Session["itemlist"]).Copy();
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("itemcode= " + Worklists);
            dt = dv.ToTable(true, "flrcod", "flrdes", "flrdes1");
            this.ddlDivision.DataTextField = "flrdes1";
            this.ddlDivision.DataValueField = "flrdes1";
            this.ddlDivision.DataSource = dt;
            this.ddlDivision.DataBind();
        }
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetItem();
        }

        protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetDivision();
        }
    }
}