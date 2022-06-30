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
                ((Label)this.Master.FindControl("lblTitle")).Text = "Budget";
                if (Request.QueryString["Type"] != null && Request.QueryString["Type"].ToString() == "Edit")
                {
                    EditFunctionality();
                }
                if (Request.QueryString["Type"] != null && Request.QueryString["Type"].ToString() == "Approval")
                {
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Approval";
                    pnlApproval.Visible = true;
                    txtEntryDate.Enabled = false;
                }
            }
        }

        private void EditFunctionality()
        {
            string comcod = GetComCode();
            string complno = Request.QueryString["Dgno"].ToString();
            DataSet ds = _process.GetTransInfo(comcod, "SP_ENTRY_FACILITYMGT", "GETCOMPLAINFOREDIT", complno, "", "", "", "", "", "", "", "", "", "");
            List<EClass_Complain_List> obj = ds.Tables[0].DataTableToList<EClass_Complain_List>();
            Bind_Grid(obj);
        }

        private string GetComCode()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                return (hst["comcod"].ToString());
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

        }

        private void getProjUnitddl()
        {
            try
            {
                string comcod = GetComCode();
                DataSet ds = _process.GetTransInfo(comcod, "SP_ENTRY_FACILITYMGT", "GETDGNO", "", "", "", "", "", "", "", "", "", "", "");
                if (ds == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{_process.ErrorObject["Msg"].ToString()}" + "');", true);
                    return;
                }
                DataTable dt = ds.Tables[0];
                ddlDgNo.DataSource = dt;
                ddlDgNo.DataTextField = "dgdesc";
                ddlDgNo.DataValueField = "dgno";
                ddlDgNo.DataBind();
                if (Request.QueryString["DgNo"] != null)
                {
                    ddlDgNo.SelectedValue = Request.QueryString["DgNo"].ToString();
                    ddlDgNo.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{ex.Message.ToString()}" + "');", true);
            }
        }
        private void getComplainUser()
        {
            try
            {
                string comcod = GetComCode();
                string dgno = Request.QueryString["DgNo"] ?? ddlDgNo.SelectedValue.ToString();
                DataSet ds = _process.GetTransInfo(comcod, "SP_ENTRY_FACILITYMGT", "GETDGINFO", dgno, "", "", "", "", "", "", "", "", "", "");
                if (ds == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{_process.ErrorObject["Msg"].ToString()}" + "');", true);
                    return;
                }
                DataTable dt01 = ds.Tables[0];
                DataTable dt02 = ds.Tables[1];
                DataTable dt03 = ds.Tables[2];
                if (dt02 != null || dt02.Rows.Count > 0)
                {
                    var row = dt02.Rows[0];
                    lblProjectText.Text = row["pactdesc"].ToString();
                    lblCustomerText.Text = row["custdesc"].ToString();
                    lblUnitText.Text = row["unit"].ToString();
                    lblSiteVisisted.Text = row["sitevisiteddate"].ToString();
                    lblWarranty.Text = row["warrantydesc"].ToString();
                    lblWarrantyCode.Text = row["warranty"].ToString();
                }
                List<EClass_Complain_List> obj = dt01.DataTableToList<EClass_Complain_List>();
                List<EClass_Complain_List> obj1 = dt03.DataTableToList<EClass_Complain_List>();
                gvComplainForm.DataSource = obj1;
                gvComplainForm.DataBind();
                ViewState["ComplainList"] = obj;
                Bind_Grid(obj);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{ex.Message.ToString()}" + "');", true);
            }

        }

        private void getMatCategory()
        {
            try
            {
                string comcod = GetComCode();
                DataSet ds = _process.GetTransInfo(comcod, "SP_ENTRY_FACILITYMGT", "GETMATCATEGORY", "", "", "", "", "", "", "", "", "", "", "");
                if (ds == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{_process.ErrorObject["Msg"].ToString()}" + "');", true);
                    return;
                }
                ddlMatCategory.DataSource = ds.Tables[0];
                ddlMatCategory.DataTextField = "sirdesc";
                ddlMatCategory.DataValueField = "sircode";
                ddlMatCategory.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{ex.Message.ToString()}" + "');", true);
            }


        }
        private void getMaterial()
        {
            try
            {
                string comcod = GetComCode();
                string type = ASTUtility.Right(ddlMatCategory.SelectedValue, 10) == "0000000000" ? ASTUtility.Left(ddlMatCategory.SelectedValue, 2) + "%" : ASTUtility.Left(ddlMatCategory.SelectedValue, 4) + "%";
                DataSet ds = _process.GetTransInfo(comcod, "SP_ENTRY_FACILITYMGT", "GETMATERIAL", type, "", "", "", "", "", "", "", "", "", "");
                if (ds == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{_process.ErrorObject["Msg"].ToString()}" + "');", true);
                    return;
                }
                ddlMaterial.DataSource = ds.Tables[0];
                ViewState["MaterialWithUnit"] = ds.Tables[0];
                ddlMaterial.DataTextField = "sirdesc";
                ddlMaterial.DataValueField = "sircode";
                ddlMaterial.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{ex.Message.ToString()}" + "');", true);
            }
        }

        private void createMaterialList()
        {
            try
            {
                List<EClass_Material_List> obj = new List<EClass_Material_List>();
                string comcod = GetComCode();
                string dgno = Request.QueryString["Dgno"] ?? ddlDgNo.SelectedValue.ToString();
                DataSet ds = _process.GetTransInfo(comcod, "SP_ENTRY_FACILITYMGT", "GETBUDGETINFO", dgno, "", "", "", "", "", "", "", "", "", "");
                if (ds == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{_process.ErrorObject["Msg"].ToString()}" + "');", true);
                    return;
                }
                obj = ds.Tables[0].DataTableToList<EClass_Material_List>();
                ViewState["MaterialList"] = obj;
                Bind_Grid_Material();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{ex.Message.ToString()}" + "');", true);
            }

        }

        private void getUnit()
        {
            try
            {
                DataTable dt = (DataTable)ViewState["MaterialWithUnit"];
                string material = ddlMaterial.SelectedValue.ToString();
                DataTable dt01 = dt.Select("sircode='" + material + "'").CopyToDataTable();
                lblsirval.Text = dt01.Rows[0]["sirval"].ToString();
                lblUnit.Text = dt01.Rows[0]["sirunit"].ToString();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{ex.Message.ToString()}" + "');", true);
            }
        }




        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
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
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + $"Added to the Table" + "');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + $"Already Added-{materialdesc}" + "');", true);
                }
                lbtnTotal_Click(null, null);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "TabState();", true);

            }

            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{ex.Message.ToString()}" + "');", true);
            }
        }


        private void Bind_Grid_Material()
        {
            try
            {
                List<EClass_Material_List> obj = (List<EClass_Material_List>)ViewState["MaterialList"];
                gvMaterials.DataSource = obj;
                gvMaterials.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{ex.Message.ToString()}" + "');", true);
            }
        }

        private void Bind_Grid(List<EClass_Complain_List> obj)
        {
            try
            {
                ViewState["ComplainList"] = obj;
                dgv1.DataSource = obj;
                dgv1.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "TabState();", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{ex.Message.ToString()}" + "');", true);
            }
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
            try
            {
                getComplainUser();
                createMaterialList();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "TabState();", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{ex.Message.ToString()}" + "');", true);
            }

        }

        protected void ddlMatCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                getMaterial();
                getUnit();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "TabState();", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{ex.Message.ToString()}" + "');", true);
            }
        }

        protected void ddlMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                getUnit();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "TabState();", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{ex.Message.ToString()}" + "');", true);
            }
        }

        protected void LnkbtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                GridViewRow row = (GridViewRow)btn.NamingContainer;
                int index = row.RowIndex;
                List<EClass_Material_List> obj = (List<EClass_Material_List>)ViewState["MaterialList"];
                obj.RemoveAt(index);
                ViewState["MaterialList"] = obj;
                Bind_Grid_Material();
                lbtnTotal_Click(null, null);
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Removed from the table" + "');", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "TabState();", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{ex.Message.ToString()}" + "');", true);
            }
        }

        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            try
            {
                List<EClass_Material_List> obj = (List<EClass_Material_List>)ViewState["MaterialList"];
                SessionMaterialList();
                Bind_Grid_Material();
                ((Label)this.gvMaterials.FooterRow.FindControl("lblgvFAmt")).Text = obj.Sum(x => x.amount).ToString("#,##0.00;-#,##0.00;");
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "TabState();", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{ex.Message.ToString()}" + "');", true);
            }
        }
        private void SessionMaterialList()
        {
            try
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
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{ex.Message.ToString()}" + "');", true);
            }
        }

        protected void lnkSave_Click(object sender, EventArgs e)
        {
            try
            {
                List<EClass_Material_List> obj = (List<EClass_Material_List>)ViewState["MaterialList"];
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = GetComCode();
                string userId = hst["usrid"].ToString();
                string dgno = Request.QueryString["Dgno"] ?? ddlDgNo.SelectedValue.ToString();
                string bgddate = txtEntryDate.Text;
                int i = 1;
                List<bool> resultCompA = new List<bool>();


                bool resultDelete = _process.UpdateTransInfo3(comcod, "SP_ENTRY_FACILITYMGT", "DELETEBGD", dgno, "", "", "", "", "", "", "", "", "", "", "",
                             "", "", "", "", "", "", "", "", "", "", userId);
                if (resultDelete)
                {
                    foreach (var item in obj)
                    {
                        bool resultA = _process.UpdateTransInfo3(comcod, "SP_ENTRY_FACILITYMGT", "UPSERTBGD", dgno, item.materialId, item.unit, item.quantity.ToString(), item.amount.ToString(),
                            bgddate, i.ToString(), "", "", "", "", "",
                                 "", "", "", "", "", "", "", "", "", "", userId);
                        resultCompA.Add(resultA);
                        i++;
                    }
                    if (resultCompA.Contains(false))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{ _process.ErrorObject["Msg"].ToString()}" + "');", true);
                        return;
                    }
                    else
                    {
                        if (Request.QueryString["Type"] != null && Request.QueryString["Type"].ToString() == "Approval")
                        {
                            string notes = txtNarration.Text;
                            string warrantyCode = lblWarrantyCode.Text;
                            bool resultR = _process.UpdateTransInfo3(comcod, "SP_ENTRY_FACILITYMGT", "UPSERTAPPROVAL", dgno, notes, warrantyCode, "", "", "", "", "", "", "", "", "",
                                    "", "", "", "", "", "", "", "", "", "", userId);
                            if (resultR)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + $"Budget Approval of Dg-{dgno} - Updated Successful" + "');", true);

                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{ _process.ErrorObject["Msg"].ToString()}" + "');", true);
                                return;
                            }

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + $"Budget of Dg-{dgno} - Updated Successful" + "');", true);

                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{ _process.ErrorObject["Msg"].ToString()}" + "');", true);
                    return;
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "TabState();", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{ex.Message.ToString()}" + "');", true);
            }

        }
    }
}