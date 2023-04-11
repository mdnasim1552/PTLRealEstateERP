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
    public partial class KPIGeneralCode : System.Web.UI.Page
    {
        ProcessRAccess Rprss = new ProcessRAccess();
        ProcessAccess da = new ProcessAccess();
        //static string tempddl1 = "", tempddl2 = "";
        string msg = "";

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
                //((Label)this.Master.FindControl("lblTitle")).Text = "Payment Schedule Code";

            }
            if (this.ddlSalPayment.Items.Count == 0)
                this.Load_CodeBooList();
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.gvPaySch_DataBind();
            }
            catch (Exception ex)
            {
            }
        }
        protected void gvPaySch_DataBind()
        {
            try
            {

                DataTable tbl1 = (DataTable)Session["storedata"];
                this.gvPaySch.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                this.gvPaySch.DataSource = tbl1;
                this.gvPaySch.DataBind();


            }
            catch (Exception ex)
            {
            }

        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        protected void Load_CodeBooList()
        {

            try
            {




                string comcod = this.GetCompCode();
                //string Code = (this.Request.QueryString["Type"].ToString() == "Mkt") ? "80" : (this.Request.QueryString["Type"].ToString() == "Sales") ? "81" : "59";



                DataSet dsone = this.da.GetTransInfo(comcod, "[dbo_kpi].[SP_ENTRY_CODEBOOK_NEW]", "OACCOUNTKPICODE",
                                "01", "", "", "", "", "", "", "", "");
                this.ddlSalPayment.DataTextField = "gdesc";
                this.ddlSalPayment.DataValueField = "gcod";
                this.ddlSalPayment.DataSource = dsone.Tables[0];
                this.ddlSalPayment.DataBind();
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }

        }
        protected void gvPaySch_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {


            this.gvPaySch.EditIndex = -1;
            this.gvPaySch_DataBind();

        }
        protected void gvPaySch_RowEditing(object sender, GridViewEditEventArgs e)
        {

            this.gvPaySch.EditIndex = e.NewEditIndex;
            this.gvPaySch_DataBind();
        }
        protected void gvPaySch_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }
        protected void lnkok_Click(object sender, EventArgs e)
        {
            if (this.lnkok.Text == "Ok")
            {
                this.lnkok.Text = "New";
                try
                {
                    Hashtable hst = (Hashtable)Session["tblLogin"];
                    string comcod = hst["comcod"].ToString();
                    Session.Remove("storedata");
                    // this.lnkok.Visible = false;
                    // this.lnkcancel.Visible = true;
                    this.ddlSalPayment.Visible = false;
                    this.ddlOthersBookSegment.Visible = false;
                    //this.LblBookName1.Visible = false;
                    this.lbalterofddl.Visible = true;
                    this.lbalterofddl0.Visible = true;
                    this.lbalterofddl.Text = this.ddlSalPayment.SelectedItem.ToString().Trim();
                    // + " " + "(" + this.ddlOthersBookSegment.SelectedItem.ToString().Trim() + ")";
                    this.lbalterofddl0.Text = this.ddlOthersBookSegment.SelectedItem.ToString().Trim();

                    this.ShowInformation();
                    this.gvPaySch.EditIndex = -1;
                    this.gvPaySch_DataBind();

                }
                catch (Exception ex)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Information not found!!!!";
                }
            }
            else
            {
                this.lnkok.Text = "Ok";
                this.lnkok.Visible = true;
                //this.lnkcancel.Visible = false;
                //this.LblBookName1.Visible = true;
                this.lbalterofddl.Visible = false;
                this.lbalterofddl0.Visible = false;
                this.ddlSalPayment.Visible = true;
                this.ddlOthersBookSegment.Visible = true;

                this.gvPaySch.DataSource = null;
                this.gvPaySch.DataBind();
            }
        }
        private void ShowInformation()
        {
            string comcod = this.GetCompCode();
            string tempddl1 = (this.ddlSalPayment.SelectedValue.ToString()).Substring(0, 2);
            //string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();

            DataSet ds1 = this.da.GetTransInfo(comcod, "[dbo_kpi].[SP_ENTRY_CODEBOOK_NEW]", "LOADKPISCHEDULE", tempddl1,
                            "", "", "", "", "", "", "", "");

            Session["storedata"] = ds1.Tables[0];
        }

        protected void gvPaySch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvPaySch.PageIndex = e.NewPageIndex;
            this.gvPaySch_DataBind();
        }

        protected void lbtnAdd_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                msg = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                return;
            }

            GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            int RowIndex = gvr.RowIndex;

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            int index = this.gvPaySch.PageSize * this.gvPaySch.PageIndex + RowIndex;
            string gcod = ((DataTable)Session["storedata"]).Rows[index]["gcod"].ToString();
            //this.lbgrcod.Text = gcod;
            //this.paymentcodchk.Text = gcod;
            //this.txtpaymentcode.Text = gcod.Substring(0, 2) + "-" + gcod.Substring(2, 3) + "-" + ASTUtility.Right(gcod, 2);

            //this.Chboxchild.Checked = (ASTUtility.Right(gcod, 2) == "00" && ASTUtility.Right(gcod, 5) != "00000") || (ASTUtility.Right(gcod, 2) == "00");
            //this.chkbod.Visible = (ASTUtility.Right(gcod, 2) == "00" && ASTUtility.Right(gcod, 5) != "00000") || (ASTUtility.Right(gcod, 2) == "00");
            //this.lblchild.Visible = (ASTUtility.Right(gcod, 2) == "00" && ASTUtility.Right(gcod, 5) != "00000") || (ASTUtility.Right(gcod, 2) == "00");

            //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModalAddCode();", true);
        }

        protected void lbtnAddCode_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            string isgcod = paymentcodchk.Text.Trim().Replace("-", "");


            string tpaymentcode = this.txtpaymentcode.Text.Trim().Replace("-", "");
            string Desc = this.txtDesc.Text.Trim();
            string DescBN = this.txtDescBN.Text.Trim();
            //string gtype = this.txttype.Text.Trim();
            string gtype = "";
            string Gtype = "";
            //string Gtype = (gtype.ToString() == "") ? "T" : gtype;
            string mnumber = (isgcod == tpaymentcode) ? "" : "manual";

            bool isResultValid = true;
            if (Desc.Length == 0)
            {
                msg = "Resource Head is not empty";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModalAddCode();", true);
                //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModal();", true);
                isResultValid = false;
                return;
            }
            string gcod = this.Chboxchild.Checked ? ((ASTUtility.Right(isgcod, 5) == "00000") ? (ASTUtility.Left(isgcod, 2) + "001" + ASTUtility.Right(isgcod, 2)) : ASTUtility.Left(isgcod, 5) + "01")

                   : ((isgcod != tpaymentcode) ? tpaymentcode : isgcod);

            bool result = da.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "INSERTPAYMENTCODE", gcod,
                          Desc, DescBN, Gtype, mnumber, "", "", "", "", "");

            if (result == true)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = " Successfully Created ";
            }

            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Create Failed";
            }
            ShowInformation();
            gvPaySch_DataBind();
        }
    }
}