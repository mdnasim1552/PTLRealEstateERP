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
    public partial class WorkWithSubCon : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;

                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("~/AcceessError.aspx");

                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                //((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                //this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString() == "Current") ? "Sub-Contractor Bill-Work Wise"
                //    : (this.Request.QueryString["Type"].ToString() == "CheckaVerify") ? "Sub-Contractor Bill-Checked"
                //    : (this.Request.QueryString["Type"].ToString() == "FirstRecom") ? "Sub-Contractor Bill-First Recommendation"
                //    : (this.Request.QueryString["Type"].ToString() == "SecRecom") ? "Sub-Contractor Bill-Second Recommendation"
                //    : (this.Request.QueryString["Type"].ToString() == "ThirdRecom") ? "Sub-Contractor Bill-Third Recommendation"
                //    : (this.Request.QueryString["Type"].ToString() == "Edit") ? " Sub-Contractor Bill-Work Wise Edit"
                //    : (this.Request.QueryString["Type"].ToString() == "BillApproval") ? " Sub-Contractor Bill Approval"
                //    : "Labour Issue Information";

                this.GetProjectList();
                this.GetConList();

                DateTime nowDate = DateTime.Now;
                DateTime yearfday = new DateTime(nowDate.Year, 1, 1);
                string fdate = yearfday.ToString("dd-MMM-yyyy");
                this.txtCurISSDate.Text = fdate;
                //((Label)this.Master.FindControl("lblmsg")).Visible = false;

                //if (this.Request.QueryString["genno"].ToString().Length > 0)
                //{
                //    this.ibtnPreBillList_Click(null, null);
                //    if (this.Request.QueryString["Type"].ToString() == "BillApproval")
                //    {
                //        this.lbtnOk_Click(null, null);
                //    }
                //}

            }

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void DateForOpeningBill()
        {
            string Type = "Opening";

            if (Type == "Opening")
            {
                string comcod = this.GetCompCode();
                DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETOPENINGDATE", "", "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                this.txtCurISSDate.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["voudat"]).ToString("dd-MMM-yyyy");
                this.txtCurISSDate.Enabled = false;

            }
        }

        private void GetProjectList()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string ddldesc = hst["ddldesc"].ToString();
            string comcod = this.GetCompCode();
            string pactcode = "%%";
            this.txtCurISSDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string userid = hst["usrid"].ToString();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETISSUEPRJLIST", pactcode, userid, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            string TextField = (ddldesc == "True" ? "actdesc" : "actdesc1");
            this.ddlprjlist.DataTextField = TextField;
            this.ddlprjlist.DataValueField = "actcode";
            this.ddlprjlist.DataSource = ds1.Tables[0];
            this.ddlprjlist.DataBind();
            ds1.Dispose();
        }
        private void GetConList()
        {
            string comcod = this.GetCompCode();
            //string conlist = "%" + this.txtSrcSub.Text + "%";
            string conlist = "%%";

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETISSUECONTLIST", conlist, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlcontractorlist.DataTextField = "sircode1";
            this.ddlcontractorlist.DataValueField = "sircode";
            this.ddlcontractorlist.DataSource = ds1.Tables[0];
            this.ddlcontractorlist.DataBind();


            this.ddlgroup.DataTextField = "grpdesc";
            this.ddlgroup.DataValueField = "grp";
            this.ddlgroup.DataSource = ds1.Tables[1];
            this.ddlgroup.DataBind();
        }
        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectList();
        }
        protected void ibtnFindContractorList_Click(object sender, EventArgs e)
        {
            this.GetConList();
        }
        protected void ibtnPreBillList_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string ProjectCode = this.ddlprjlist.SelectedValue.ToString();
            string CurDate1 = Convert.ToDateTime(this.txtCurISSDate.Text.Trim()).ToString("dd-MMM-yyyy");


            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_BILLMGT02", "GETPREVL_LABBILL_LIST", ProjectCode, CurDate1, "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlPrevISSList.Items.Clear();
            this.ddlPrevISSList.DataTextField = "lreqno1";
            this.ddlPrevISSList.DataValueField = "lreqno";
            this.ddlPrevISSList.DataSource = ds1.Tables[0];
            this.ddlPrevISSList.DataBind();

        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
        protected void ddlgroup_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlcatagory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlWorkList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {

        }

        protected void lbtnSelect_Click(object sender, EventArgs e)
        {

        }

       

        protected void lbtnPrevISSList_Click(object sender, EventArgs e)
        {

        }

        
    }
}