using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_07_Ten
{
    public partial class UnitConversion : System.Web.UI.Page
    {

        ProcessAccess mgt = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "UNIT CONVERSION";

                this.GetUnitName();
                this.GetUnitConversion();
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            //((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkbtnSave_Click);

            //((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        protected void btnClose_Click(object sender, EventArgs e)
        {
            string prevPage = (string)ViewState["prevPage"];
            Response.Redirect(prevPage);
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void GetUnitName()
        {
            string comcod = this.GetCompCode();

            DataSet dsone = mgt.GetTransInfo(comcod, "SP_ENTRY_MGT", "GET_UNIT_NAME", "", "", "", "", "", "", "");
            this.ddlbUnit.DataTextField = "gdesc";
            this.ddlbUnit.DataValueField = "gcod";
            this.ddlbUnit.DataSource = dsone.Tables[0];
            this.ddlbUnit.DataBind();


            this.ddlcUnit.DataTextField = "gdesc";
            this.ddlcUnit.DataValueField = "gcod";
            this.ddlcUnit.DataSource = dsone.Tables[0];
            this.ddlcUnit.DataBind();



        }
        private void GetUnitConversion()
        {
            string comcod = this.GetCompCode();

            DataSet ds = mgt.GetTransInfo(comcod, "SP_ENTRY_MGT", "GET_UNIT_CONVRT_INF", "", "", "", "", "", "", "");
            List<RealEntity.C_34_Mgt.EclassUnitConversion> unitcon = ds.Tables[0].DataTableToList<RealEntity.C_34_Mgt.EclassUnitConversion>();
            ViewState["tblUnitCon"] = unitcon;
            this.Data_Bind();

        }


        protected void lnkbtnSave_Click(object sender, EventArgs e)
        {
            string comcod = GetCompCode();
            this.Save_Value();

            List<RealEntity.C_34_Mgt.EclassUnitConversion> unitcon = (List<RealEntity.C_34_Mgt.EclassUnitConversion>)ViewState["tblUnitCon"];

            DataTable dt1 = ASITUtility03.ListToDataTable(unitcon);
            DataSet ds1 = new DataSet("ds1");
            ds1.Tables.Add(dt1);
            ds1.Tables[0].TableName = "tbl1";

            bool result = mgt.UpdateXmlTransInfo(comcod, "SP_ENTRY_MGT", "UPDATE_UNIT_CONVERSION", ds1, null, null, "", "", "", "", "");


            if (result)
            {


                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                return;
            }

        }


        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        private void Data_Bind()
        {
            List<RealEntity.C_34_Mgt.EclassUnitConversion> unitcon = (List<RealEntity.C_34_Mgt.EclassUnitConversion>)ViewState["tblUnitCon"];
            if (unitcon.Count == 0)
            {
                return;
            }
            this.gvunit.DataSource = unitcon;
            this.gvunit.DataBind();



        }

        private void Save_Value()
        {

            List<RealEntity.C_34_Mgt.EclassUnitConversion> unitcon = (List<RealEntity.C_34_Mgt.EclassUnitConversion>)ViewState["tblUnitCon"];

            for (int i = 0; i < gvunit.Rows.Count; i++)
            {
                unitcon[i].conrat = Convert.ToDouble("0" + ((TextBox)gvunit.Rows[i].FindControl("txtgvConrat")).Text.Trim());
                unitcon[i].remarks = ((TextBox)gvunit.Rows[i].FindControl("txtgvRemarks")).Text.Trim().ToString();

            }
            ViewState["tblUnitCon"] = unitcon;

        }




        protected void lbtnAdd_Click(object sender, EventArgs e)
        {

            List<RealEntity.C_34_Mgt.EclassUnitConversion> unitcon = (List<RealEntity.C_34_Mgt.EclassUnitConversion>)ViewState["tblUnitCon"];
            string bcod = this.ddlbUnit.SelectedValue.ToString();
            string bcodesc = this.ddlbUnit.SelectedItem.ToString();
            string ccod = this.ddlcUnit.SelectedValue.ToString();
            string ccodesc = this.ddlcUnit.SelectedItem.ToString();
            var checklist = unitcon.FindAll(p => p.bcod == bcod && p.ccod == ccod);
            if (checklist.Count == 0)
            {

                unitcon.Add(new RealEntity.C_34_Mgt.EclassUnitConversion(bcod, bcodesc, ccod, ccodesc, 0.00, ""));

            }
            else
            {

                ((Label)this.Master.FindControl("lblmsg")).Text = "Exist in the list";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            ViewState["tblUnitCon"] = unitcon;
            this.Data_Bind();
        }


        protected void gvunit_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            List<RealEntity.C_34_Mgt.EclassUnitConversion> unitcon = (List<RealEntity.C_34_Mgt.EclassUnitConversion>)ViewState["tblUnitCon"];
            int rowindex = (this.gvunit.PageSize) * (this.gvunit.PageIndex) + e.RowIndex;
            unitcon.RemoveAt(rowindex);
            ViewState["tblUnitCon"] = unitcon;
            this.Data_Bind();
        }
    }
}