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
    public partial class WorkExeLock : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                GetProjectList();
                GetCategory();
                GetItem();
                ShowFloorcode();
            }

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {


            ViewState["PreviousPageUrl"] = this.Request.UrlReferrer.ToString();
            // Create an event handler for the master page's contentCallEvent event

            //((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            //((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkupdate_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);



            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        protected void lnkupdate_Click(object sender, EventArgs e)      // Update Button
        {
            SessionList();
            DataTable dt = (DataTable)Session["tblbgd"];
            DataTable dt01 = dt.Select("iscomplete=true").CopyToDataTable();

            string comcod = GetComCode();
            string pactcode = ddlProject.SelectedValue.ToString();
            List<bool> resultArr = new List<bool>();
            foreach (DataRow dr in dt01.Rows)
            {
                string isircode = dr["isircode"].ToString();
                string flrcod = dr["flrcod"].ToString();
                bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "UPDATEBDGWRK", pactcode, isircode, flrcod);
                resultArr.Add(result);
            }
            if (resultArr.Contains(false))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Update Failed" + "');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Update Successfully" + "');", true);
                GetBudgetInfo();
            }
        }

        private DataTable HiddenSameValue(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string flrcod = dt1.Rows[0]["flrcod"].ToString();            
            string isircodegrp = dt1.Rows[0]["misircode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["flrcod"].ToString() == flrcod && dt1.Rows[j]["misircode"].ToString() == isircodegrp)
                {
                    flrcod = dt1.Rows[j]["flrcod"].ToString();                   
                    isircodegrp = dt1.Rows[j]["misircode"].ToString();
                    dt1.Rows[j]["misirdesc"] = "";
                    dt1.Rows[j]["flrdesc"] = "";
                }
                else
                {
                    flrcod = dt1.Rows[j]["flrcod"].ToString();                   
                    isircodegrp = dt1.Rows[j]["misircode"].ToString();
                }
            }
            return dt1;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {

            Response.Redirect((string)ViewState["PreviousPageUrl"]);

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

        private void GetProjectList()
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
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETCATEGORY", "%", "", "", "", "", "", "", "", "");
            this.ddlCategory.DataTextField = "sirdesc";
            this.ddlCategory.DataValueField = "sircode";
            this.ddlCategory.DataSource = ds1.Tables[0];
            this.ddlCategory.DataBind();
        }
        private void ShowFloorcode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComCode();
            string pactcode = this.ddlProject.SelectedValue.ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSIS", "GETFLOORCOD", pactcode, "", "", "", "", "", "", "", "");
            DataTable dt = ds1.Tables[0];

            DataRow dr3 = dt.NewRow();
            dr3["flrcod"] = "AAA";
            dr3["flrdes"] = "All Floors-Details";
            dt.Rows.Add(dr3);
            DataView dv = dt.DefaultView;
            dv.Sort = "flrcod";
            dt = dv.ToTable();
            this.ddlFloor.DataTextField = "flrdes";
            this.ddlFloor.DataValueField = "flrcod";
            this.ddlFloor.DataSource = dt;
            this.ddlFloor.DataBind();
            this.ddlFloor.SelectedValue = "AAA";
        }
        private void GetItem()
        {
            string comcod = GetComCode();
            string cat = ddlCategory.SelectedValue == "XXXXXXXXXXXX" ? "" : ASTUtility.Left(ddlCategory.SelectedValue.ToString(), 4);
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETITEM", cat, "", "", "", "", "", "", "", "");
            this.ddlItem.DataTextField = "sirdesc";
            this.ddlItem.DataValueField = "sircode";
            this.ddlItem.DataSource = ds1.Tables[0];
            this.ddlItem.DataBind();
        }

        private void GetBudgetInfo()
        {
            string comcod = GetComCode();
            string pactcode = ddlProject.SelectedValue.ToString();
            string flrcod = ddlFloor.SelectedValue == "AAA" ? "" : ddlFloor.SelectedValue.ToString();
            string isircode = ddlItem.SelectedValue == "XXXXXXXXXXXX" ? "" : ddlItem.SelectedValue.ToString();
            string cat = ddlCategory.SelectedValue == "XXXXXXXXXXXX" ? "" : ddlCategory.SelectedValue.ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETBUDGETINFO", pactcode, flrcod, isircode, cat, "", "", "", "", "");
            Session["tblbgd"] = ds1.Tables[0];
            Data_Bind();
        }
        private void Data_Bind()
        {
            try
            {
                this.DataGridOne.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                this.DataGridOne.DataSource = HiddenSameValue((DataTable)Session["tblbgd"]);
                this.DataGridOne.DataBind();
            }
            catch (Exception ex)
            {

            }
        }


        private void SessionList()
        {
            DataTable dt = (DataTable)Session["tblbgd"];
            int TblRowIndex;
            for (int i = 0; i < this.DataGridOne.Rows.Count; i++)
            {
                TblRowIndex = (DataGridOne.PageIndex) * DataGridOne.PageSize + i;
                bool islock = ((CheckBox)this.DataGridOne.Rows[i].FindControl("chkLock")).Checked;
                dt.Rows[TblRowIndex]["iscomplete"] = islock;
            }
            Session["tblbgd"] = dt;
        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            if (ddlProject.SelectedValue == "000000000000")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Please Select Project" + "');", true);
            }
            else
            {
                if (lbtnOk.Text == "Ok")
                {
                    ShowFloorcode();
                    GetBudgetInfo();
                    lbtnOk.Text = "New";
                    ddlProject.Enabled = false;
                }
                else
                {
                    lbtnOk.Text = "Ok";
                    DataGridOne.DataSource = null;
                    DataGridOne.DataBind();
                    ddlProject.Enabled = true;
                }
            }
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetItem();
            GetBudgetInfo();
        }


        protected void ddlpagesize_SelectedIndexChanged1(object sender, EventArgs e)
        {
            SessionList();
            this.Data_Bind();
        }

        protected void DataGridOne_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SessionList();
            this.DataGridOne.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetBudgetInfo();
        }

        protected void ddlFloor_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetBudgetInfo();
        }
    }
}