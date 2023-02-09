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

namespace RealERPWEB.F_29_Fxt
{
    public partial class FxtAssetRegister : System.Web.UI.Page
    {
        ProcessAccess PurData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                // Session.Remove("Unit");
                //string type = this.Request.QueryString["Type"].ToString();
                //((Label)this.Master.FindControl("lblTitle")).Text = " Fixed Asset Entry";

                //this.txtfromdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //this.txtfromdate.Text = "01" + this.txtfromdate.Text.Trim().Substring(2);
                //this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetDeparment();
                this.Visibility();
                this.Show();
            }

        }

        private void Visibility()
        {

            string comcod = this.GetComeCode();
            switch (comcod)
            {
                case "3305":
                    gvFixAsset.Columns[4].Visible = false;
                    gvFixAsset.Columns[5].Visible = false;
                    gvFixAsset.Columns[6].HeaderText = "Qty";

                    break;

                default:

                    break;
            }

        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void GetDeparment()
        {
            string comcod = this.GetComeCode();

            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "GETASSETCAT", "", "", "", "", "", "", "", "", "");

            ds1.Tables[0].Rows.Add(comcod, "000000000000", "All Type");

            this.ddlProjectName.DataTextField = "sirdesc";
            this.ddlProjectName.DataValueField = "sircode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            this.ddlProjectName.SelectedValue = "000000000000";

        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            //this.lblPage.Visible = true;
            //this.ddlpagesize.Visible = true;
            this.Show();

        }
        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            //this.GetProjectName();
        }

        private void Show()
        {
            Session.Remove("tblcost");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string srchmat = "%" + this.txtSrchMat.Text.Trim() + "%";
            string ddlprojact = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "21%" : this.ddlProjectName.SelectedValue.ToString();
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "GETSHOWASSET", srchmat, ddlprojact, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvFixAsset.DataSource = null;
                this.gvFixAsset.DataBind();
                return;
            }
            this.gvFixAsset.DataSource = ds1.Tables[0];
            this.gvFixAsset.DataBind();
        }
        protected void gvFixAsset_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("hlnkqty");
            string mCOMCOD = comcod;
            string mRsircode = ((Label)e.Row.FindControl("lblcode")).Text;
            string qty = ((HyperLink)e.Row.FindControl("hlnkqty")).Text;
            string mdesc = ((Label)e.Row.FindControl("lbldesc")).Text;

            string pactcode = this.ddlProjectName.SelectedValue.ToString();


            // //string mTRNDAT1 = this.txtDatefrom.Text;
            // //string mTRNDAT2 = this.txtDateto.Text;
            // //------------------------------//////
            ////string qty = (Label)e.Row.FindControl("lblgvcode");
            // //HyperLink actdesc = (HyperLink)e.Row.FindControl("HLgvDesc");

            // //string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "qty")).ToString().Trim();
            // //string lebel2 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "leb2")).ToString().Trim();

            //hlink1.Font.Bold = true;
            hlink1.Font.Bold = true;
            hlink1.Style.Add("text-align", "right");

            hlink1.NavigateUrl = "LinkFxtAssetDetails.aspx?rsircode=" + mRsircode + "&Comcod=" + comcod + "&pactcode=" + pactcode + "&Qty=" + qty + "&rsirdesc=" + mdesc;

            //if (ASTUtility.Right(code, 4) == "0000")
            //{
            //    actcode.Font.Bold = true;
            //    actdesc.Font.Bold = true;
            //    //actdesc.Style.Add("text-align", "right");

            //}
            /////---------------------------------//// 

            //if (ASTUtility.Left(mACTCODE, 1) == "4")
            //{
            //    hlink1.NavigateUrl = "AccProjectReports.aspx?actcode=" + mACTCODE + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;

            //}
        }
        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lbtnOk_Click(null, null);
        }
    }
}