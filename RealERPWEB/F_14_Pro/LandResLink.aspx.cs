using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace RealERPWEB.F_14_Pro
{
    public partial class LandResLink : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
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
                //((Label)this.Master.FindControl("lblTitle")).Text = "Land Code Link To Payable";

                //this.LandCode();
                this.PayableCode();
                this.MainCode();
            }
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }

        protected void LandCode()
        {
            string comcod = GetCompCode();
            string ssircode = this.ddlPPayableCode.SelectedValue.ToString().Trim();
            string maincod = this.ddlmaincode.SelectedValue.Substring(0, 4) + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_LPROCUREMENT", "LANDCODELINK", maincod, ssircode, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlLandCode.DataTextField = "sirdesc1";
            this.ddlLandCode.DataValueField = "sircode";
            this.ddlLandCode.DataSource = ds1.Tables[0];
            this.ddlLandCode.DataBind();


        }

        protected void PayableCode()
        {
            string comcod = GetCompCode();


            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_LPROCUREMENT", "PAYABLECODELINK", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlPPayableCode.DataTextField = "sirdesc1";
            this.ddlPPayableCode.DataValueField = "sircode";
            this.ddlPPayableCode.DataSource = ds1.Tables[0];
            this.ddlPPayableCode.DataBind();


        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "New")
            {

                this.ddlLandCode.Enabled = true;

                this.ddlPPayableCode.Enabled = true;
                this.gvLandCodeLink.DataSource = null;
                this.gvLandCodeLink.DataBind();
                this.Panel2.Visible = false;
                this.lbtnOk.Text = "Ok";
                this.MainCode();
                return;
            }



            this.ddlPPayableCode.Enabled = false;

            this.Panel2.Visible = true;
            this.lbtnOk.Text = "New";

            this.Get_Receive_Info();
        }

        private void Get_Receive_Info()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string ssircode = this.ddlPPayableCode.SelectedValue.ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_LPROCUREMENT", "LINKEDPAYABLECODE", ssircode, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["tbPreLink"] = ds1.Tables[0];

            this.Data_Bind();

        }
        protected void Data_Bind()
        {
            DataTable tbl1 = (DataTable)Session["tbPreLink"];
            this.gvLandCodeLink.DataSource = tbl1;
            this.gvLandCodeLink.DataBind();

        }
        protected void lbtnSuplUpdate_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            //this.Session_tbltbPreLink_Update();
            string rescode = this.ddlPPayableCode.SelectedValue.ToString();
            DataTable tbl1 = (DataTable)Session["tbPreLink"];
            for (int i = 0; i < tbl1.Rows.Count; i++)
            {

                string sircode = tbl1.Rows[i]["sircode"].ToString();
                bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_LPROCUREMENT", "INSERORUPDATEPAYCODE",
                              rescode, sircode, "", "", "", "", "", "", "", "", "", "", "", "", "");
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;

                }
            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
        }



        protected void lbtnSelectSupl1_Click(object sender, EventArgs e)
        {
            // this.Session_tbltbPreLink_Update();
            DataTable tbl1 = (DataTable)Session["tbPreLink"];
            string SirCode = this.ddlPPayableCode.SelectedValue.ToString();
            DataRow[] dr2 = tbl1.Select("sircode = '" + SirCode + "'");
            if (dr2.Length == 0)
            {
                DataRow dr1 = tbl1.NewRow();
                // dr1["landcode"] = this.ddlLandCode.SelectedValue.ToString();
                dr1["sircode"] = this.ddlLandCode.SelectedValue.ToString();
                dr1["sirdesc"] = this.ddlLandCode.SelectedItem.Text.Trim();
                tbl1.Rows.Add(dr1);
            }
            Session["tbPreLink"] = tbl1;
            this.Data_Bind();
        }

        protected void lbtnSelectAll_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tbPreLink"];

            for (int i = 0; i < this.ddlLandCode.Items.Count; i++)
            {
                string SirCode = this.ddlLandCode.Items[i].Value;
                DataRow[] dr = dt.Select("sircode='" + SirCode + "'");
                if (dr.Length == 0)
                {
                    DataRow dr1 = dt.NewRow();
                    // dr1["landcode"] = this.ddlLandCode.SelectedValue.ToString();
                    dr1["sircode"] = this.ddlLandCode.Items[i].Value;
                    dr1["sirdesc"] = this.ddlLandCode.Items[i].Text.Trim();
                    dt.Rows.Add(dr1);


                }


            }

            Session["tbPreLink"] = dt;
            this.Data_Bind();
        }
        protected void gvLandCodeLink_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)Session["tbPreLink"];
            string ssircode = this.ddlPPayableCode.SelectedValue.ToString().Trim();
            string sircode = ((Label)this.gvLandCodeLink.Rows[e.RowIndex].FindControl("lblgvprocode")).Text.Trim();
            // string rescode = ((Label)this.gvLandCodeLink.Rows[e.RowIndex].FindControl("lblgvResCod0")).Text.Trim();
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_LPROCUREMENT", "DELETEPAYABLELINK", ssircode, sircode, "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (result == true)
            {
                int rowindex = (this.gvLandCodeLink.PageSize) * (this.gvLandCodeLink.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }
            DataView dv = dt.DefaultView;
            Session.Remove("tbPreLink");
            Session["tbPreLink"] = dv.ToTable();
            this.Data_Bind();
        }
        protected void MainCode()
        {
            string comcod = GetCompCode();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_LPROCUREMENT", "LANDMAINCODE", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlmaincode.DataTextField = "sirdesc1";
            this.ddlmaincode.DataValueField = "sircode";
            this.ddlmaincode.DataSource = ds1.Tables[0];
            this.ddlmaincode.DataBind();
        }
        protected void ddlmaincode_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LandCode();
        }
    }
}