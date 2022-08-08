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
                    lblDgNo.Text = Request.QueryString["Dgno"].ToString();
                    EditFunctionality();
                }
                if (Request.QueryString["Type"] != null && Request.QueryString["Type"].ToString() == "Approval")
                {
                    lblDgNo.Text = Request.QueryString["Dgno"].ToString();
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Approval";
                    lnkSave.Text = "Approve";
                    pnlApproval.Visible = true;
                    txtEntryDate.Enabled = false;
                    lnkProceed.Visible = false;
                }
            }
        }

        private void EditFunctionality()
        {
            string comcod = GetComCode();
            string complno = Request.QueryString["Dgno"].ToString();
            DataSet ds = _process.GetTransInfo(comcod, "SP_ENTRY_FACILITYMGT", "GETCOMPLAINFOREDIT", complno, "", "", "", "", "", "", "", "", "", "");
            List<EClass_Complain_List> obj = ds.Tables[0].DataTableToList<EClass_Complain_List>();
            //Bind_Grid(obj);
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
                lbtnTotal_Click(null, null);
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
                SessionMaterialList();
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
                        amount = 0,
                        percnt = 0,
                        type = material == "049700101001" ? "Z" : "A"
                    });
                    ViewState["MaterialList"] = obj;
                    lbtnTotal_Click(null, null);
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + $"Added to the Table" + "');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + $"Already Added-{materialdesc}" + "');", true);
                }

                

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
                var obj1 = obj.OrderBy(x => x.type).ToList();
                ViewState["MaterialList"] = obj1;
                gvMaterials.DataSource = obj1;
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
                
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{ex.Message.ToString()}" + "');", true);
            }
        }


        protected void lnkProceed_Click(object sender, EventArgs e)
        {
            string dgno = lblDgNo.Text;
            if (dgno == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Please Save to Proceed to Next Step" + "');", true);
            }
            else
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = GetComCode();
                string userId = hst["usrid"].ToString();

                bool resultflag = _process.UpdateTransInfo3(comcod, "SP_ENTRY_FACILITYMGT", "UPDATEAPPRBGDFLAG", dgno, "", "", "", "", "", "", "", "", "", "", "",
                                     "", "", "", "", "", "", "", "", "", "", userId);
                if (resultflag)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + $"Dg-{dgno} proceeded to Budget" + "');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured" + "');", true);
                }

            }
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
                lbtnTotal_Click(null, null);
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Removed from the table" + "');", true);
                
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

                double sumValue = obj.Where(x => x.type == "A").Sum(x => x.amount);
                if (obj.Where(x => x.type == "Z").ToList().Count == 1)
                {

                    double percnt = obj.Where(x => x.type == "Z").FirstOrDefault().percnt;
                    double percntamt = 0.00;
                    if (percnt == 0.00)
                    {
                        percntamt = obj.Where(x => x.type == "Z").FirstOrDefault().amount;
                        percnt = sumValue == 0.00 ? 0.00 : ((percntamt / sumValue) * 100);
                    }
                    else
                    {
                        percntamt = sumValue * (percnt / 100);
                    }
                    obj.Where(x => x.type == "Z").FirstOrDefault().amount = percntamt;
                    obj.Where(x => x.type == "Z").FirstOrDefault().percnt = percnt;
                }
                Bind_Grid_Material();
                if (obj.Count > 0)
                {
                    ((Label)this.gvMaterials.FooterRow.FindControl("lblgvFAmt")).Text = obj.Sum(x => x.amount).ToString("#,##0.00;-#,##0.00;");
                }
                
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
                    string materialId = ((Label)this.gvMaterials.Rows[rowindex].FindControl("lblgvconcatcode")).Text.Trim();
                    double percnt = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvMaterials.Rows[rowindex].FindControl("txtgvPercnt")).Text.Trim()));
                    double quantity = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvMaterials.Rows[rowindex].FindControl("txtgvQuantity")).Text.Trim()));
                    double rate = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvMaterials.Rows[rowindex].FindControl("txtgvRate")).Text.Trim()));
                    double amount = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvMaterials.Rows[rowindex].FindControl("txtAmount")).Text.Trim()));
                    obj[rowindex].quantity = quantity;
                    obj[rowindex].rate = rate;
                    obj[rowindex].amount = materialId == "049700101001" ? amount : quantity * rate;
                    obj[rowindex].percnt = percnt;

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
                            bgddate, i.ToString(), item.percnt.ToString(), "", "", "", "",
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
                                if (warrantyCode != "43002")
                                {
                                    bool resultflag = _process.UpdateTransInfo3(comcod, "SP_ENTRY_FACILITYMGT", "UPDATEMATREQFLAG", dgno, "", "", "", "", "", "", "", "", "", "", "",
                                                             "", "", "", "", "", "", "", "", "", "", userId);
                                    if (!resultflag)
                                    {
                                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured" + "');", true);
                                        return;
                                    }
                                }
                                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + $"Budget Approval of Dg-{dgno} - Updated Successful" + "');", true);

                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{ _process.ErrorObject["Msg"].ToString()}" + "');", true);
                                return;
                            }
                            if (obj.Where(x => x.materialId.StartsWith("01")).ToList().Count == 0)
                            {
                                bool resultflag = _process.UpdateTransInfo3(comcod, "SP_ENTRY_FACILITYMGT", "UPDATEQCFLAG", dgno, "", "", "", "", "", "", "", "", "", "", "",
                                                         "", "", "", "", "", "", "", "", "", "", userId);
                                if (!resultflag)
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured" + "');", true);
                                }
                            }

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + $"Budget of Dg-{dgno} - Updated Successful" + "');", true);

                        }
                        lblDgNo.Text = dgno;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{ _process.ErrorObject["Msg"].ToString()}" + "');", true);
                    return;
                }

                
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{ex.Message.ToString()}" + "');", true);
            }

        }

        protected void gvMaterials_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string materialId = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "materialId")).ToString();
                TextBox amt = (TextBox)e.Row.FindControl("txtAmount");
                TextBox qty = (TextBox)e.Row.FindControl("txtgvQuantity");
                TextBox rate = (TextBox)e.Row.FindControl("txtgvRate");
                TextBox percnt = (TextBox)e.Row.FindControl("txtgvPercnt");
                if (materialId == "049700101001")
                {
                    qty.Enabled = false;
                    amt.Enabled = true;
                    rate.Enabled = false;
                    percnt.Enabled = true;
                }
                else
                {
                    rate.Enabled = true;
                    qty.Enabled = true;
                    amt.Enabled = false;
                    percnt.Enabled = false;
                }
            }
        }
    }
}