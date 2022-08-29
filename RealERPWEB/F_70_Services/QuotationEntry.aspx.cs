using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static RealERPEntity.C_70_Services.EClass_Quotation;

namespace RealERPWEB.F_70_Services
{
    public partial class QuotationEntry : System.Web.UI.Page
    {
        ProcessAccess _process = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtEntryDate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
                Init();
                ((Label)this.Master.FindControl("lblTitle")).Text = "Service Quotation";
            }
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


        private void getWorkType()
        {
            try
            {
                string comcod = GetComCode();
                DataSet ds = _process.GetTransInfo(comcod, "[dbo_Services].[SP_ENTRY_QUOTATION]", "GETWORKTYPE", "", "", "", "", "", "", "", "", "", "", "");
                if (ds == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{_process.ErrorObject["Msg"].ToString()}" + "');", true);
                    return;
                }
                ddlWorkType.DataSource = ds.Tables[0];
                ddlWorkType.DataTextField = "sirdesc";
                ddlWorkType.DataValueField = "sircode";
                ddlWorkType.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{ex.Message.ToString()}" + "');", true);
            }


        }
        private void getResource()
        {
            try
            {
                string comcod = GetComCode();
                string type = "%";
                DataSet ds = _process.GetTransInfo(comcod, "[dbo_Services].[SP_ENTRY_QUOTATION]", "GETRESOURCE", type, "", "", "", "", "", "", "", "", "", "");
                if (ds == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{_process.ErrorObject["Msg"].ToString()}" + "');", true);
                    return;
                }
                ViewState["MaterialWithUnit"] = ds.Tables[0];
                ddlResource.DataSource = ds.Tables[0];
                ddlResource.DataTextField = "sirdesc";
                ddlResource.DataValueField = "sircode";
                ddlResource.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{ex.Message.ToString()}" + "');", true);
            }
        }
        private void getNewQuotationNo()
        {
            string comcod = GetComCode();
            string date = txtEntryDate.Text;
            DataSet ds = _process.GetTransInfo(comcod, "[dbo_Services].[SP_ENTRY_QUOTATION]", "GETQUOTNO", date, "", "", "", "", "", "", "", "", "", "");
            if (ds == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{_process.ErrorObject["Msg"].ToString()}" + "');", true);
                return;
            }
            txtquotno.Text = ds.Tables[0].Rows[0]["maxquotno1"].ToString();
            lblQuotation.Text = ds.Tables[0].Rows[0]["maxquotno"].ToString();
        }
        private void getCustomer()
        {
            try
            {
                string comcod = GetComCode();
                //string type = "%";
                DataSet ds = _process.GetTransInfo(comcod, "[dbo_Services].[SP_ENTRY_QUOTATION]", "GETCUSTOMER", "", "", "", "", "", "", "", "", "", "", "");
                if (ds == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{_process.ErrorObject["Msg"].ToString()}" + "');", true);
                    return;
                }
                ddlCustomer.DataSource = ds.Tables[0];
                ddlCustomer.DataTextField = "sirdesc";
                ddlCustomer.DataValueField = "sircode";
                ddlCustomer.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{ex.Message.ToString()}" + "');", true);
            }
        }
        private void Init()
        {
            try
            {
                getNewQuotationNo();
                getCustomer();
                getWorkType();
                getResource();
                List<EQuotation> obj = new List<EQuotation>();
                ViewState["MaterialList"] = obj;
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
                string material = ddlResource.SelectedValue.ToString();
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
                getUnit();
                string worktype = ddlWorkType.SelectedValue.ToString();
                string worktypedesc = ddlWorkType.SelectedItem.Text;
                string material = ddlResource.SelectedValue.ToString();
                string materialdesc = ddlResource.SelectedItem.Text;
                string unit = lblUnit.Text;
                double sirval = Convert.ToDouble(lblsirval.Text == "" ? "0.00" : lblsirval.Text);
                List<EQuotation> obj = (List<EQuotation>)ViewState["MaterialList"];
                SessionMaterialList();
                var value = obj.Where(x => x.resourcecode == material && x.worktypecode == worktype).Any();
                if (!value)
                {
                    obj.Add(new EQuotation
                    {
                        worktypecode = worktype,
                        worktypedesc = worktypedesc,
                        resourcecode = material,
                        resourcedesc = materialdesc,
                        unit = unit,
                        qrate = sirval,
                        qqty = 0,
                        qamt = 0,
                        chkamt = 0,
                        chkqty = 0,
                        aprqty = 0,
                        apramt = 0,
                        percnt = 0,
                        type = material == "049700101001" ? "Z" : "A"
                    });
                    ViewState["MaterialList"] = obj;
                    Bind_Grid_Material();
                    //lbtnTotal_Click(null, null);
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
        private void SessionMaterialList()
        {
            try
            {
                List<EQuotation> obj = (List<EQuotation>)ViewState["MaterialList"];
                int rowindex = 0;

                for (int i = 0; i < gvMaterials.Rows.Count; i++)
                {
                    rowindex = (gvMaterials.PageIndex) * gvMaterials.PageSize + i;
                    string worktypecode = ((Label)this.gvMaterials.Rows[rowindex].FindControl("lblgvworktypecode")).Text.Trim();
                    string materialId = ((Label)this.gvMaterials.Rows[rowindex].FindControl("lblgvconcatcode")).Text.Trim();
                    double percnt = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvMaterials.Rows[rowindex].FindControl("txtgvPercnt")).Text.Trim()));
                    double quantity = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvMaterials.Rows[rowindex].FindControl("txtgvQuantity")).Text.Trim()));
                    double rate = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvMaterials.Rows[rowindex].FindControl("txtgvRate")).Text.Trim()));
                    double amount = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvMaterials.Rows[rowindex].FindControl("txtAmount")).Text.Trim()));
                    obj[rowindex].qqty = quantity;
                    obj[rowindex].qrate = rate;
                    obj[rowindex].qamt = materialId == "049700101001" ? amount : quantity * rate;
                    obj[rowindex].percnt = percnt;
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
                List<EQuotation> obj = (List<EQuotation>)ViewState["MaterialList"];
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
                List<EQuotation> obj = (List<EQuotation>)ViewState["MaterialList"];
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
                List<EQuotation> obj = (List<EQuotation>)ViewState["MaterialList"];
                SessionMaterialList();
                double sumValue = obj.Where(x => x.type == "A").Sum(x => x.qamt);
                if (obj.Where(x => x.type == "Z").ToList().Count == 1)
                {

                    double percnt = obj.Where(x => x.type == "Z").FirstOrDefault().percnt;
                    double percntamt = 0.00;
                    if (percnt == 0.00)
                    {
                        percntamt = obj.Where(x => x.type == "Z").FirstOrDefault().qamt;
                        percnt = sumValue == 0.00 ? 0.00 : ((percntamt / sumValue) * 100);
                    }
                    else
                    {
                        percntamt = sumValue * (percnt / 100);
                    }
                    obj.Where(x => x.type == "Z").FirstOrDefault().qamt = percntamt;
                    obj.Where(x => x.type == "Z").FirstOrDefault().percnt = percnt;
                }
                Bind_Grid_Material();
                if (obj.Count > 0)
                {
                    ((Label)this.gvMaterials.FooterRow.FindControl("lblgvFAmt")).Text = obj.Sum(x => x.qamt).ToString("#,##0.00;-#,##0.00;");
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
                //Quotinfb
                getNewQuotationNo();
                lbtnTotal_Click(null, null);
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = GetComCode();
                string userId = hst["usrid"].ToString();
                string date = txtEntryDate.Text;
                string quotid = lblQuotation.Text;
                string customerid = ddlCustomer.SelectedValue.ToString();
                string narration = txtNarration.Text;
                string isCheck = "0";
                string isAppr = "0";
                string status = "1";
                List<EQuotation> obj = (List<EQuotation>)ViewState["MaterialList"];
                bool result = _process.UpdateTransInfo2(comcod, "[dbo_Services].[SP_ENTRY_QUOTATION]", "UPSERTQUOTINFB", quotid, date, customerid, narration, isCheck, isAppr, status, userId,
                    "", "", "", "", "", "", "", "", "", "", "", "", "");
                if (result)
                {
                    bool resultdelete = _process.UpdateTransInfo(comcod, "[dbo_Services].[SP_ENTRY_QUOTATION]", "DELETEQUOTINFA", quotid);
                    if (resultdelete)
                    {
                        List<bool> resultQuotArray = new List<bool>();
                        foreach (var item in obj)
                        {
                            if (item.qamt != 0.00)
                            {
                                string worktype = item.worktypecode.ToString();
                                string resource = item.resourcecode.ToString();
                                string qqty = item.qqty.ToString();
                                string qamt = item.qamt.ToString();
                                string chkqty = item.chkqty.ToString();
                                string chkamt = item.chkamt.ToString();
                                string aprqty = item.aprqty.ToString();
                                string apramt = item.apramt.ToString();
                                bool resultQuotA = _process.UpdateTransInfo2(comcod, "[dbo_Services].[SP_ENTRY_QUOTATION]", "UPSERTQUOTINFA", quotid, worktype, resource, qqty, qamt,
                                        chkqty, chkamt, aprqty, apramt, userId, "", "", "", "", "", "", "", "", "", "", "");
                                resultQuotArray.Add(resultQuotA);
                            }
                        }
                        if (resultQuotArray.Contains(false))
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured" + "');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + $"{quotid} - Updated Successful" + "');", true);
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured." + "');", true);
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
                string materialId = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "resourcecode")).ToString();
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

        protected void btnaddcustomer_Click(object sender, EventArgs e)
        {
            txtCustomerName.Text = "";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenModal();", true);
        }

        private void getResourceType()
        {
            try
            {
                string comcod = GetComCode();
                //string type = "%";
                DataSet ds = _process.GetTransInfo(comcod, "[dbo_Services].[SP_ENTRY_QUOTATION]", "SIRINFTYPE", "", "", "", "", "", "", "", "", "", "", "");
                if (ds == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{_process.ErrorObject["Msg"].ToString()}" + "');", true);
                    return;
                }
                ddlResourceType.DataSource = ds.Tables[0];
                ddlResourceType.DataTextField = "sirdesc";
                ddlResourceType.DataValueField = "sircode";
                ddlResourceType.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{ex.Message.ToString()}" + "');", true);
            }
        }



        protected void btnAddResource_Click(object sender, EventArgs e)
        {
            getResourceType();
            txtResource.Text = "";
            txtRate.Text = "";
            txtUnit.Text = "";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenModalResource();", true);
        }

        protected void lnkUpdateModal_Click(object sender, EventArgs e)
        {
            try
            {

                string customerName = txtCustomerName.Text;
                if (customerName != "" || customerName.Length > 0)
                {
                    Hashtable hst = (Hashtable)Session["tblLogin"];
                    string comcod = GetComCode();
                    string userId = hst["usrid"].ToString();
                    DataSet ds = _process.GetTransInfo(comcod, "[dbo_Services].[SP_ENTRY_QUOTATION]", "INSERTSIRINF", "6101%", customerName, "", "0.00", userId, "", "", "", "", "", "");


                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + $"Updated Successful" + "');", true);
                        getCustomer();
                        ddlCustomer.SelectedValue = ds.Tables[0].Rows[0]["sircode"].ToString();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Updated Faileds" + "');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Please Enter Information To Continue" + "');", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenModal();", true);
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{ex.Message.ToString()}" + "');", true);
            }
        }

        protected void lnkUpdateResourceModal_Click(object sender, EventArgs e)
        {
            try
            {

                string resourceName = txtResource.Text;
                string unit = txtUnit.Text;
                string rate = txtRate.Text == "" ? "0.00" : txtRate.Text;
                string resourcetype = ASTUtility.Left(ddlResourceType.SelectedValue.ToString(), 9) + "%";
                if (resourceName != "" || resourceName.Length > 0)
                {
                    Hashtable hst = (Hashtable)Session["tblLogin"];
                    string comcod = GetComCode();
                    string userId = hst["usrid"].ToString();
                    DataSet ds = _process.GetTransInfo(comcod, "[dbo_Services].[SP_ENTRY_QUOTATION]", "INSERTSIRINF", resourcetype, resourceName, unit, rate, userId, "", "", "", "", "", "");


                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + $"Updated Successful" + "');", true);
                        getResource();
                        ddlResource.SelectedValue = ds.Tables[0].Rows[0]["sircode"].ToString();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Updated Faileds" + "');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Please Enter Information To Continue" + "');", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenModal();", true);
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{ex.Message.ToString()}" + "');", true);
            }
        }
    }
}