using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Net;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using System.Net.Mail;
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_08_PPlan
{
    public partial class ProTargetMonthly : System.Web.UI.Page
    {
        ProcessAccess ImpleData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                // ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "PROJECT COMPLETION INFORMATION VIEW/EDIT";
                this.GetProjectName();

            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }

        private void GetProjectName()
        {
            string comcod = this.GetCompCode();
            string srchTxt = this.txtProjectSearch.Text.Trim() + "%";
            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_ENTRY_PROJECTTARGET", "GETPROJETNAME", srchTxt, "", "", "", "", "", "", "", "");
            this.ddlProject.DataTextField = "actdesc";
            this.ddlProject.DataValueField = "actcode";
            this.ddlProject.DataSource = ds1.Tables[0];
            this.ddlProject.DataBind();
            ds1.Dispose();
            this.GetProDuration();
        }
        private void GetProDuration()
        {
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProject.SelectedValue.ToString();
            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_ENTRY_PROJECTTARGET", "GETPRODURATION", pactcode, "", "", "", "", "", "", "", "");
            this.ddlMonth.DataTextField = "monyear";
            this.ddlMonth.DataValueField = "yearmon";
            this.ddlMonth.DataSource = ds1.Tables[0];
            this.ddlMonth.DataBind();
            ds1.Dispose();

        }
        protected void ImgbtnFindProject_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
                this.GetProjectName();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {

                this.lbtnOk.Text = "New";
                this.lblProjectDesc.Text = this.ddlProject.SelectedItem.Text.Trim();
                this.ddlProject.Visible = false;
                this.ddlMonth.Enabled = false;
                this.lblProjectDesc.Visible = true;
                this.PnlColoumn.Visible = true;
                this.lblPage.Visible = true;
                this.ddlpagesize.Visible = true;
                this.ShowPtarget();

                return;
            }

            this.lbtnOk.Text = "Ok";
            this.ddlProject.Visible = true;
            this.lblProjectDesc.Visible = false;
            this.ddlMonth.Enabled = true;
            this.PnlColoumn.Visible = false;
            this.lblPage.Visible = false;
            this.ddlpagesize.Visible = false;
            this.gvProTarget.DataSource = null;
            this.gvProTarget.DataBind();
        }

        private void ShowPtarget()
        {
            Session.Remove("tblptarget");
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProject.SelectedValue.ToString();
            string YearMon = this.ddlMonth.SelectedValue.ToString();
            string ItemSearch = this.txtSearchItem.Text.Trim() + "%";
            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_ENTRY_PROJECTTARGET", "PROJECTTARGETMON", pactcode, YearMon, ItemSearch, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvProTarget.DataSource = null;
                this.gvProTarget.DataBind();
                return;

            }
            Session["tblptarget"] = this.HiddenSameData(ds1.Tables[0]);
            this.gvProTarget.Columns[9].HeaderText = this.ddlMonth.SelectedItem.Text.Trim();
            this.Data_Bind();
            ds1.Dispose();
        }
        private void SaveValue()
        {


            DataTable tbl1 = (DataTable)Session["tblptarget"];
            int rowindex;
            for (int i = 0; i < this.gvProTarget.Rows.Count; i++)
            {
                double qty = Convert.ToDouble("0" + ((TextBox)this.gvProTarget.Rows[i].FindControl("txtgvQty")).Text.Trim());
                rowindex = (this.gvProTarget.PageSize * this.gvProTarget.PageIndex) + i;
                double bgdqty = Convert.ToDouble(tbl1.Rows[rowindex]["bgdqty"]);
                double balqty = Convert.ToDouble(tbl1.Rows[rowindex]["balqty"]);

                tbl1.Rows[rowindex]["trqty"] = qty;
                tbl1.Rows[rowindex]["difqty"] = balqty - qty;


            }
            Session["tblptarget"] = tbl1;
        }
        private void Data_Bind()
        {
            this.gvProTarget.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvProTarget.DataSource = (DataTable)Session["tblptarget"];
            this.gvProTarget.DataBind();
            //this.gvProTarget.Columns[1].Visible = false;
        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string isircode = dt1.Rows[0]["isircode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["isircode"].ToString() == isircode)
                {
                    isircode = dt1.Rows[j]["isircode"].ToString();
                    dt1.Rows[j]["isirdesc"] = "";
                    dt1.Rows[j]["isirunit"] = "";
                }

                else
                {
                    isircode = dt1.Rows[j]["isircode"].ToString();
                }
            }

            return dt1;

        }



        protected void ImgbtnSearchItem_Click(object sender, EventArgs e)
        {
            this.ShowPtarget();
        }
        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            this.SaveValue();
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProject.SelectedValue.ToString();
            string yearmon = this.ddlMonth.SelectedValue.ToString();
            DataTable tbl1 = (DataTable)Session["tblptarget"];
            bool result = false;

            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string Itemcode = tbl1.Rows[i]["isircode"].ToString();
                string flrcod = tbl1.Rows[i]["flrcod"].ToString();
                double trqty = Convert.ToDouble(tbl1.Rows[i]["trqty"]);
                if (trqty > 0)
                {
                    result = ImpleData.UpdateTransInfo(comcod, "SP_ENTRY_PROJECTTARGET", "INSERTORUPPTARGET", pactcode,
                            Itemcode, flrcod, yearmon, trqty.ToString(), "", "", "", "", "", "", "", "", "", "");
                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Data Updated Fail.');", true);
                        return;
                    }
                }
            }


            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Data Updated successfully');", true);


            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "PROJECT COMPLETION INFORMATION";
                string eventdesc = "Update";
                string eventdesc2 = this.ddlProject.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        protected void gvProTarget_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvProTarget.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetProDuration();
        }
        protected void gvProTarget_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)Session["tblptarget"];
            string pactcode = this.ddlProject.SelectedValue.ToString();
            string Yearmon = this.ddlMonth.SelectedValue.ToString();
            string Itemcode = ((Label)this.gvProTarget.Rows[e.RowIndex].FindControl("lblgvItemcode")).Text.Trim();
            string Flrcode = ((Label)this.gvProTarget.Rows[e.RowIndex].FindControl("lblgvFloorcod")).Text.Trim();
            bool result = ImpleData.UpdateTransInfo(comcod, "SP_ENTRY_PROJECTTARGET", "DELETEPTARGET", pactcode, Itemcode, Flrcode, Yearmon, "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                int rowindex = (this.gvProTarget.PageSize) * (this.gvProTarget.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            Session["tblptarget"] = dv.ToTable();
            this.Data_Bind();
        }
    }
}