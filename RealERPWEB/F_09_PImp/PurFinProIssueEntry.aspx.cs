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
namespace RealERPWEB.F_09_PImp
{
    public partial class PurFinProIssueEntry : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        DataTable tempforgrid = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Work Execution - Finishing Project View/Edit";
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.GetProjectName();
                this.GetMonth();


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

        protected void GetProjectName()
        {

            string comcod = this.GetCompCode();
            string SearchProject = "%" + this.txtsrchProject.Text.Trim() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_04", "GETFINPROJECT", SearchProject, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlprjlist.DataTextField = "actdesc";
            this.ddlprjlist.DataValueField = "actcode";
            this.ddlprjlist.DataSource = ds1.Tables[0];
            this.ddlprjlist.DataBind();
            ds1.Dispose();

        }
        private void GetMonth()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_04", "GETYEARMON", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlyearmon.DataTextField = "yearmon";
            this.ddlyearmon.DataValueField = "ymon";
            this.ddlyearmon.DataSource = ds1.Tables[0];
            this.ddlyearmon.DataBind();
            this.ddlyearmon.SelectedValue = System.DateTime.Today.ToString("yyyyMM");
            ds1.Dispose();

        }
        private void GetItemList()
        {
            ViewState.Remove("tblitem");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string txtsrchItem = this.txtsrchItemName.Text.Trim() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_04", "GETITEMLIST", txtsrchItem, "", "", "", "", "", "", "", "");
            ViewState["tblitem"] = ds1.Tables[0];
            if (ds1 == null)
                return;
            this.ddlitemlist.DataTextField = "itemdesc";
            this.ddlitemlist.DataValueField = "itemcode";
            this.ddlitemlist.DataSource = ds1.Tables[0];
            this.ddlitemlist.DataBind();
            ds1.Dispose();
        }


        protected void imgbtnSearchProject_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
                this.GetProjectName();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")

            {
                this.lblddlProject.Text = this.ddlprjlist.SelectedItem.Text.Trim();
                this.lbtnOk.Text = "New";
                this.ddlprjlist.Visible = false;
                this.lblddlProject.Visible = true;
                this.pnlSub.Visible = true;
                this.GetIssue_Info();
                return;


            }
         ((Label)this.Master.FindControl("lblmsg")).Text = "";
            this.lbtnOk.Text = "Ok";
            this.ddlprjlist.Visible = true;
            this.lblddlProject.Visible = false;
            this.pnlSub.Visible = false;
            this.grvissue.DataSource = null;
            this.grvissue.DataBind();
        }

        protected void GetIssue_Info()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string pactcode = this.ddlprjlist.SelectedValue.ToString();
            string Month = this.ddlyearmon.SelectedValue.ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_04", "GETPURISSUEINFO", Month, pactcode, "", "", "", "", "", "", "");

            if (ds1 == null)
            {
                this.grvissue.DataSource = null;
                this.grvissue.DataBind();
                return;

            }

            ViewState["tblissue"] = ds1.Tables[0];
            this.Data_Bind();
        }


        protected void lbtnISelect_Click(object sender, EventArgs e)
        {
            this.SaveValue();

            string ItemCode = this.ddlitemlist.SelectedValue.ToString().Trim();
            DataTable dt = (DataTable)ViewState["tblissue"];
            DataRow[] dr1 = dt.Select(" itemcode='" + ItemCode + "'");
            if (dr1.Length == 0)
            {
                DataRow dr = dt.NewRow();
                dr["itemunit"] = (((DataTable)ViewState["tblitem"]).Select("itemcode='" + ItemCode + "'"))[0]["itemunit"];
                dr["itemcode"] = this.ddlitemlist.SelectedValue.ToString();
                dr["itemdesc"] = this.ddlitemlist.SelectedItem.ToString().Trim();
                dr["mqty"] = 0.00;
                dr["monqty"] = 0.00;
                dr["exeqty"] = 0.00;
                dr["rate"] = 0.00;
                dr["masamt"] = 0.00;
                dr["monamt"] = 0.00;
                dr["exeamt"] = 0.00;
                dt.Rows.Add(dr);

            }
            ViewState["tblissue"] = dt;
            this.Data_Bind();


        }


        private void SaveValue()
        {
            DataTable dt = (DataTable)ViewState["tblissue"];
            int TblRowIndex;
            for (int i = 0; i < this.grvissue.Rows.Count; i++)
            {
                double masqty = Convert.ToDouble("0" + ((TextBox)this.grvissue.Rows[i].FindControl("txtgvmqty")).Text.Trim());
                double monqty = Convert.ToDouble("0" + ((TextBox)this.grvissue.Rows[i].FindControl("txtgvmonqty")).Text.Trim());
                double exeqty = Convert.ToDouble("0" + ((TextBox)this.grvissue.Rows[i].FindControl("txtgvexeqty")).Text.Trim());
                double Rate = Convert.ToDouble("0" + ((TextBox)this.grvissue.Rows[i].FindControl("txtgvrate")).Text.Trim());
                TblRowIndex = (grvissue.PageIndex) * grvissue.PageSize + i;
                dt.Rows[TblRowIndex]["mqty"] = masqty;
                dt.Rows[TblRowIndex]["monqty"] = monqty;
                dt.Rows[TblRowIndex]["exeqty"] = exeqty;
                dt.Rows[TblRowIndex]["rate"] = Rate;
                dt.Rows[TblRowIndex]["masamt"] = masqty * Rate;
                dt.Rows[TblRowIndex]["monamt"] = monqty * Rate;
                dt.Rows[TblRowIndex]["exeamt"] = exeqty * Rate;
            }
            ViewState["tblissue"] = dt;
        }

        private void Data_Bind()
        {
            DataTable tbl1 = (DataTable)ViewState["tblissue"];
            this.grvissue.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.grvissue.DataSource = tbl1;
            this.grvissue.DataBind();
            this.FooterCalCulation(tbl1);


        }

        private void FooterCalCulation(DataTable dt)
        {

            if (dt.Rows.Count == 0)
                return;

            ((Label)this.grvissue.FooterRow.FindControl("lgvmFTotalAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(masamt)", "")) ? 0.00
              : dt.Compute("Sum(masamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.grvissue.FooterRow.FindControl("lgvmonFTotalAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(monamt)", "")) ? 0.00
              : dt.Compute("Sum(monamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.grvissue.FooterRow.FindControl("lgvexeFTotalAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(exeamt)", "")) ? 0.00
              : dt.Compute("Sum(exeamt)", ""))).ToString("#,##0;(#,##0); ");



        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }
        protected void imgbtnSearchItem_Click(object sender, EventArgs e)
        {
            this.GetItemList();
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        protected void grvissue_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.grvissue.PageIndex = e.NewPageIndex;
            this.Data_Bind();

        }
        protected void grvissue_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }



        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            this.SaveValue();
            DataTable tbl2 = (DataTable)ViewState["tblissue"];
            string comcod = this.GetCompCode();
            string yearmon = this.ddlyearmon.SelectedValue.ToString();
            string Pactcode = this.ddlprjlist.SelectedValue.ToString().Trim();

            for (int i = 0; i < tbl2.Rows.Count; i++)
            {
                string ItemCode = tbl2.Rows[i]["itemcode"].ToString();
                double masqty = Convert.ToDouble(tbl2.Rows[i]["mqty"].ToString());
                double monqty = Convert.ToDouble(tbl2.Rows[i]["monqty"].ToString());
                double exeqty = Convert.ToDouble(tbl2.Rows[i]["exeqty"].ToString());
                string rate = Convert.ToDouble(tbl2.Rows[i]["rate"]).ToString();

                if (masqty > 0 || monqty > 0 || exeqty > 0)
                {
                    bool result = purData.UpdateTransInfo2(comcod, "SP_ENTRY_PURCHASE_04", "INOUPFINPURISSUEINFO", yearmon, Pactcode, ItemCode, masqty.ToString(),
                                monqty.ToString(), exeqty.ToString(), rate, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                    if (!result)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                        return;
                    }
                }
            }

         ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";



        }
        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
    }
}
