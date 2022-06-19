using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static RealEntity.C_30_Facility.EClass_Facility_Mgt;


namespace RealERPWEB.F_30_Facility
{
    public partial class BudgetForm : System.Web.UI.Page
    {
        ProcessAccess _process = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtEntryDate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
                getProjUnitddl();
                getComplainUser();
                createMaterialList();
                getMatCategory();
                getMaterial();
                getUnit();
                if (Request.QueryString["EngrNo"] != null)
                {
                    EditFunctionality();
                }
            }
        }

        private void EditFunctionality()
        {
            string comcod = GetComCode();
            string complno = Request.QueryString["ComplNo"].ToString();
            DataSet ds = _process.GetTransInfo(comcod, "SP_ENTRY_FACILITYMGT", "GETCOMPLAINFOREDIT", complno, "", "", "", "", "", "", "", "", "", "");
            List<EClass_Complain_List> obj = ds.Tables[0].DataTableToList<EClass_Complain_List>();
            Bind_Grid(obj);
        }

        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void getProjUnitddl()
        {
            string comcod = GetComCode();
            DataSet ds = _process.GetTransInfo(comcod, "SP_ENTRY_FACILITYMGT", "GETDGNO", "", "", "", "", "", "", "", "", "", "", "");
            ddlDgNo.DataSource = ds.Tables[0];
            ddlDgNo.DataTextField = "dgdesc";
            ddlDgNo.DataValueField = "dgno";
            ddlDgNo.DataBind();
        }






        private void getComplainUser()
        {
            string comcod = GetComCode();
            string dgno = Request.QueryString["EngrNo"] ?? ddlDgNo.SelectedValue.ToString();
            DataSet ds = _process.GetTransInfo(comcod, "SP_ENTRY_FACILITYMGT", "GETDGINFO", dgno, "", "", "", "", "", "", "", "", "", "");
            DataTable dt01 = ds.Tables[0];
            DataTable dt02 = ds.Tables[1];
            if (dt02 != null || dt02.Rows.Count > 0)
            {
                var row = dt02.Rows[0];
                lblProjectText.Text = row["pactdesc"].ToString();
                lblCustomerText.Text = row["custdesc"].ToString();
                lblUnitText.Text = row["unit"].ToString();
                lblSiteVisisted.Text = row["sitevisiteddate"].ToString();
            }
            List<EClass_Complain_List> obj = dt01.DataTableToList<EClass_Complain_List>();
            ViewState["ComplainList"] = obj;
            Bind_Grid(obj);


        }

        private void getMatCategory()
        {
            string comcod = GetComCode();
            DataSet ds = _process.GetTransInfo(comcod, "SP_ENTRY_FACILITYMGT", "GETMATCATEGORY", "", "", "", "", "", "", "", "", "", "", "");
            ddlMatCategory.DataSource = ds.Tables[0];
            ddlMatCategory.DataTextField = "sirdesc";
            ddlMatCategory.DataValueField = "sircode";
            ddlMatCategory.DataBind();
        }
        private void getMaterial()
        {
            string comcod = GetComCode();
            string type = ASTUtility.Right(ddlMatCategory.SelectedValue, 10) == "0000000000" ? ASTUtility.Left(ddlMatCategory.SelectedValue, 2) + "%" : ASTUtility.Left(ddlMatCategory.SelectedValue, 4) + "%";
            DataSet ds = _process.GetTransInfo(comcod, "SP_ENTRY_FACILITYMGT", "GETMATERIAL", type, "", "", "", "", "", "", "", "", "", "");
            ddlMaterial.DataSource = ds.Tables[0];
            ViewState["MaterialWithUnit"] = ds.Tables[0];
            ddlMaterial.DataTextField = "sirdesc";
            ddlMaterial.DataValueField = "sircode";
            ddlMaterial.DataBind();
        }

        private void createMaterialList()
        {
            List<EClass_Material_List> obj = new List<EClass_Material_List>();
            ViewState["MaterialList"] = obj;
        }

        private void getUnit()
        {
            DataTable dt = (DataTable)ViewState["MaterialWithUnit"];
            string material = ddlMaterial.SelectedValue.ToString();
            DataTable dt01 = dt.Select("sircode='" + material + "'").CopyToDataTable();
            lblsirval.Text = dt01.Rows[0]["sirval"].ToString();
            lblUnit.Text = dt01.Rows[0]["sirunit"].ToString();
        }




        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string material = ddlMaterial.SelectedValue.ToString();
            string materialdesc = ddlMaterial.SelectedItem.Text;
            string unit = lblUnit.Text;
            double sirval = Convert.ToDouble(lblsirval.Text == "" ? "0.00" : lblsirval.Text);
            List<EClass_Material_List> obj = (List<EClass_Material_List>)ViewState["MaterialList"];
            var value = obj.Where(x => x.materialId == material).Any();
            if (!value)
            {
                obj.Add(new EClass_Material_List
                {
                    materialId = material,
                    materialDesc = materialdesc,
                    unit = unit,
                    rate = sirval,
                    quantity = 0,
                    amount = 0
                });
               
                ViewState["MaterialList"] = obj;
                Bind_Grid_Material();
            }
            lbtnTotal_Click(null,null);
        }


        private void Bind_Grid_Material()
        {
            List<EClass_Material_List> obj = (List<EClass_Material_List>)ViewState["MaterialList"];
            gvMaterials.DataSource = obj;
            gvMaterials.DataBind();
        }

        private void Bind_Grid(List<EClass_Complain_List> obj)
        {
            ViewState["ComplainList"] = obj;
            dgv1.DataSource = obj;
            dgv1.DataBind();
        }


        protected void lnkProceed_Click(object sender, EventArgs e)
        {

        }



        protected void lnkRefresh_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["ComplNo"] == null)
            {
                ClearPage();
            }
            else
            {
                Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }
        }

        private void ClearPage()
        {

        }

        protected void btnOKClick_Click(object sender, EventArgs e)
        {
            getComplainUser();
        }

        protected void ddlMatCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            getMaterial();
            getUnit();
        }

        protected void ddlMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            getUnit();
        }

        protected void LnkbtnDelete_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            int index = row.RowIndex;
            List<EClass_Material_List> obj = (List<EClass_Material_List>)ViewState["MaterialList"];
            obj.RemoveAt(index);
            ViewState["MaterialList"] = obj;
            Bind_Grid_Material(); 
            lbtnTotal_Click(null, null);
        }

        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            List<EClass_Material_List> obj = (List<EClass_Material_List>)ViewState["MaterialList"];
            SessionMaterialList();
            Bind_Grid_Material();
            ((Label)this.gvMaterials.FooterRow.FindControl("lblgvFAmt")).Text = obj.Sum(x => x.amount).ToString("#,##0.00;-#,##0.00;");
            

        }
        private void SessionMaterialList()
        {
            List<EClass_Material_List> obj = (List<EClass_Material_List>)ViewState["MaterialList"];
            int rowindex = 0;

            for (int i = 0; i < gvMaterials.Rows.Count; i++)
            {
                rowindex = (gvMaterials.PageIndex) * gvMaterials.PageSize + i;
                double quantity = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvMaterials.Rows[rowindex].FindControl("txtgvQuantity")).Text.Trim()));
                double rate = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvMaterials.Rows[rowindex].FindControl("txtgvRate")).Text.Trim()));
                double amount = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvMaterials.Rows[rowindex].FindControl("txtAmount")).Text.Trim()));
                obj[rowindex].quantity = quantity;
                obj[rowindex].rate = rate;
                obj[rowindex].amount = quantity * rate;
            }
        }
    }
}