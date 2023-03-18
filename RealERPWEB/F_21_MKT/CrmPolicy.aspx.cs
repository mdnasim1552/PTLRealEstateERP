using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_21_MKT
{
    public partial class CrmPolicy : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                this.GetPolicy();
            }
        }
        private void GetPolicy()
        {
            string comcod = GetComCode();
            DataSet ds1 = MktData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_POLICYSETUP", "GETPOLICY");
            Session["Policy"] = ds1.Tables[0];
            ds1.Dispose();
            this.Data_Bind();
        }
        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void Data_Bind()
        {
            try

            {
                DataTable dt = (DataTable)Session["Policy"];
                this.grvPolicy.PageSize = 20;
                this.grvPolicy.DataSource = dt;
                this.grvPolicy.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('"+ex.Message+"');", true);
            }
        }
        protected void grvPolicy_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grvPolicy.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void grvPolicy_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.grvPolicy.EditIndex = e.NewEditIndex;
            this.Data_Bind();
        }

        protected void lbtnUpPer_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            string comcod = GetComCode();
            DataTable dt = (DataTable)ViewState["tblPolicy"];

            foreach (DataRow dr1 in dt.Rows)
            {
                string polcode = dr1["gcod"].ToString();
                string poldetails = dr1["policydesc"].ToString();
                string polday = dr1["policyday"].ToString();
                
                bool UpdateDone = MktData.UpdateTransInfo(comcod, "dbo_kpi.SP_ENTRY_POLICYSETUP", "UPDATEPOLICY", polcode, poldetails, polday);
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);
            this.GetPolicy();
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lbtnTotal_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lbtnUpPer_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        private void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        private void SaveValue()
        {
            int rowindex;
            DataTable tblt02 = (DataTable)Session["Policy"];
            for (int i = 0; i < this.grvPolicy.Rows.Count; i++)
            {
                string gcod = ((Label)this.grvPolicy.Rows[i].FindControl("lblPolCode")).Text.ToString().Trim();
                string gdesc = ((Label)this.grvPolicy.Rows[i].FindControl("lblPolDesc")).Text.ToString().Trim();
                string policydesc = ((TextBox)this.grvPolicy.Rows[i].FindControl("txtPolDetails")).Text.ToString().Trim();
                int policyday = Convert.ToInt32('0' + ((TextBox)this.grvPolicy.Rows[i].FindControl("txtPolDay")).Text.Trim());

                rowindex = (this.grvPolicy.PageSize * this.grvPolicy.PageIndex) + i;

                tblt02.Rows[rowindex]["gcod"] = gcod;
                tblt02.Rows[rowindex]["gdesc"] = gdesc;
                tblt02.Rows[rowindex]["policydesc"] = policydesc;
                tblt02.Rows[rowindex]["policyday"] = policyday;
            }
            //ViewState
            ViewState["tblPolicy"] = tblt02;
            
        }
    }
}