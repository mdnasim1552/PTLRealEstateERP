using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_07_Ten
{
    public partial class TASSchduleRate : System.Web.UI.Page
    {
        ProcessAccess tasData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                // this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Schedule Rate Inforamtion";
                this.GetItem();




            }
        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }

        private void GetItem()
        {

            string comcod = this.GetCompCode();
            string txtSProject = this.txtSrcItem.Text.Trim() + "%";
            DataSet ds1 = tasData.GetTransInfo(comcod, "SP_TAS_ENTRY_ANASHEET", "ITMCODELIST", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlItem.DataTextField = "infdesc1";
            this.ddlItem.DataValueField = "infcod";
            this.ddlItem.DataSource = ds1.Tables[0];
            this.ddlItem.DataBind();

        }



        protected void imgbtnFindProject_OnClick(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
                this.GetItem();
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.lblItem.Text = this.ddlItem.SelectedItem.Text;
                this.ddlItem.Visible = false;
                this.lblItem.Visible = true;
                this.ShowShcRate();
                return;
            }

            this.lbtnOk.Text = "Ok";
            this.ddlItem.Visible = true;
            this.lblItem.Visible = false;
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            this.gvschrate.DataSource = null;
            this.gvschrate.DataBind();
        }

        private void ShowShcRate()
        {
            Session.Remove("tblschdule");
            string comcod = this.GetCompCode();
            string ItemCode = this.ddlItem.SelectedValue.ToString();
            DataSet ds1 = tasData.GetTransInfo(comcod, "SP_TAS_ENTRY_ANASHEET", "GETSCHRATE", ItemCode, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvschrate.DataSource = null;
                this.gvschrate.DataBind();
                return;
            }

            Session["tblschdule"] = ds1.Tables[0];
            this.Data_Bind();

        }
        private void Data_Bind()
        {
            DataTable dt1 = (DataTable)Session["tblschdule"];
            this.gvschrate.DataSource = dt1;
            this.gvschrate.DataBind();
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }

        private void SaveValue()
        {

            DataTable dt = (DataTable)Session["tblschdule"];
            int TblRowIndex;
            for (int i = 0; i < this.gvschrate.Rows.Count; i++)
            {
                double schrate1 = Convert.ToDouble("0" + ((TextBox)this.gvschrate.Rows[i].FindControl("txtgvschrate1")).Text.Trim());
                double schrate2 = Convert.ToDouble("0" + ((TextBox)this.gvschrate.Rows[i].FindControl("txtgvschrate2")).Text.Trim());
                TblRowIndex = (gvschrate.PageIndex) * gvschrate.PageSize + i;

                dt.Rows[TblRowIndex]["schrate1"] = schrate1;
                dt.Rows[TblRowIndex]["schrate2"] = schrate2;


            }
            Session["tblschdule"] = dt;
        }

        protected void lbtnFinalUpdate_Click(object sender, EventArgs e)
        {

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            this.SaveValue();
            DataTable tbl2 = (DataTable)Session["tblschdule"];
            string comcod = this.GetCompCode();
            string Itmcode = this.ddlItem.SelectedValue.ToString();
            foreach (DataRow dr2 in tbl2.Rows)
            {
                string Flrcod = dr2["flrcod"].ToString();
                string schrate1 = Convert.ToDouble(dr2["schrate1"].ToString().Trim()).ToString();
                string schrate2 = Convert.ToDouble(dr2["schrate2"].ToString().Trim()).ToString();
                bool result = tasData.UpdateTransInfo(comcod, "SP_TAS_ENTRY_ANASHEET", "INSORUPSHCRATE", Itmcode, Flrcod,
                        schrate1, schrate2, "", "", "", "", "", "", "", "", "", "", "");
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = tasData.ErrorObject["Msg"].ToString();
                    return;
                }

            }
           ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";

        }


    }
}