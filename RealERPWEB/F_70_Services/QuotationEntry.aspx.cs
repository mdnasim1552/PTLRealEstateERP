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
                string type = Request.QueryString["Type"] ?? "";
                if (type != "")
                {
                    if (type != "Entry")
                    {
                        EditFunctionality();
                    }
                    if (type == "Approval" || type == "ApprovalEdit")
                    {
                        lnkSave.Text = "<span class='fa fa-check' style='color:white;' aria-hidden='true'></span> Approval";
                    }
                }
            }
        }
        private void EditFunctionality()
        {
            string type = Request.QueryString["Type"] ?? "";
            string comcod = GetComCode();
            string quotid = Request.QueryString["QId"] == null ? "" : Request.QueryString["QId"].ToString();
            string status = (type == "Edit" || type == "Check") ? "1" : (type == "CheckEdit" || type == "Approval") ? "2" : "3";
            string isType = type;
            if (quotid != "" || quotid.Length > 0)
            {
                DataSet ds = _process.GetTransInfo(comcod, "[dbo_Services].[SP_ENTRY_QUOTATION]", "EDITINFOQUOT", quotid, status, isType, "", "", "", "", "", "", "", "");
                if (ds == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{_process.ErrorObject["Msg"].ToString()}" + "');", true);
                    return;
                }
                DataTable dt1 = ds.Tables[0];
                List<EQuotation> dt2 = ds.Tables[1].DataTableToList<EQuotation>();
                if (dt1.Rows.Count > 0)
                {
                    ddlCustomer.SelectedValue = dt1.Rows[0]["customerid"].ToString();
                    txtEntryDate.Text = dt1.Rows[0]["quotdate"].ToString();
                    txtNarration.Text = dt1.Rows[0]["remarks"].ToString();
                    lblQuotation.Text = dt1.Rows[0]["quotid"].ToString();
                    txtquotno.Text = dt1.Rows[0]["quotid1"].ToString();
                    lblActcode.Text = dt1.Rows[0]["mapactcode"].ToString();
                    string reqno = dt1.Rows[0]["reqno"].ToString();
                    if (reqno == "")
                    {
                        getReqNo();
                    }
                    else
                    {
                        lblReqno.Text = reqno;
                    }
                    if (type == "ApprovalEdit")
                    {

                        pnlAccept.Visible = true;
                    }
                    else
                    {

                        pnlAccept.Visible = false;
                    }
                    if (type != "Edit")
                    {
                        ddlCustomer.Enabled = false;
                        txtEntryDate.Enabled = false;
                        btnaddcustomer.Enabled = false;
                        btnAddResource.Enabled = false;
                    }
                }
                ViewState["MaterialList"] = dt2;
                lbtnTotal_Click(null, null);
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

                ViewState["ResourceToActcode"] = ds.Tables[1];
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
                        chkrate = sirval,
                        chkqty = 0,
                        aprqty = 0,
                        aprrate = sirval,
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
                string type = Request.QueryString["Type"] ?? "";
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
                    double chkpercnt = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvMaterials.Rows[rowindex].FindControl("txtgvChkPercnt")).Text.Trim()));
                    double chkquantity = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvMaterials.Rows[rowindex].FindControl("txtgvChkQuantity")).Text.Trim()));
                    double chkrate = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvMaterials.Rows[rowindex].FindControl("txtgvChkRate")).Text.Trim()));
                    double chkamount = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvMaterials.Rows[rowindex].FindControl("txtChkAmount")).Text.Trim()));
                    double aprpercnt = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvMaterials.Rows[rowindex].FindControl("txtgvAprPercnt")).Text.Trim()));
                    double aprquantity = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvMaterials.Rows[rowindex].FindControl("txtgvAprQuantity")).Text.Trim()));
                    double aprrate = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvMaterials.Rows[rowindex].FindControl("txtgvAprRate")).Text.Trim()));
                    double apramount = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvMaterials.Rows[rowindex].FindControl("txtAprAmount")).Text.Trim()));


                    switch (type)
                    {
                        case "Approval":
                        case "ApprovalEdit":
                            quantity = Convert.ToDouble("0" + obj[rowindex].qqty);
                            rate = Convert.ToDouble("0" + obj[rowindex].qrate);
                            amount = Convert.ToDouble("0" + obj[rowindex].qamt);
                            percnt = Convert.ToDouble("0" + obj[rowindex].percnt);
                            chkquantity = Convert.ToDouble("0" + obj[rowindex].chkqty);
                            chkrate = Convert.ToDouble("0" + obj[rowindex].chkrate);
                            chkamount = Convert.ToDouble("0" + obj[rowindex].chkamt);
                            chkpercnt = Convert.ToDouble("0" + obj[rowindex].chkpercnt);
                            break;
                    }
                    obj[rowindex].qqty = quantity;
                    obj[rowindex].qrate = rate;
                    obj[rowindex].qamt = materialId == "049700101001" ? amount : quantity * rate;
                    obj[rowindex].percnt = percnt;
                    obj[rowindex].chkqty = chkquantity;
                    obj[rowindex].chkrate = chkrate;
                    obj[rowindex].chkamt = materialId == "049700101001" ? chkamount : chkquantity * chkrate;
                    obj[rowindex].chkpercnt = chkpercnt;
                    obj[rowindex].aprqty = aprquantity;
                    obj[rowindex].aprrate = aprrate;
                    obj[rowindex].apramt = materialId == "049700101001" ? apramount : aprquantity * aprrate;
                    obj[rowindex].aprpercnt = aprpercnt;
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
                string type = Request.QueryString["Type"] ?? "";
                List<EQuotation> obj = (List<EQuotation>)ViewState["MaterialList"];
                var obj1 = obj.OrderBy(x => x.type).ToList();
                ViewState["MaterialList"] = obj1;
                gvMaterials.DataSource = obj1;
                gvMaterials.DataBind();
                if (gvMaterials.Rows.Count > 0)
                {
                    if (type == "Check" || type == "CheckEdit")
                    {
                        gvMaterials.HeaderRow.Cells[6].Visible = true;
                        gvMaterials.Columns[6].Visible = true;
                        gvMaterials.HeaderRow.Cells[7].Visible = true;
                        gvMaterials.Columns[7].Visible = true;
                        gvMaterials.HeaderRow.Cells[8].Visible = true;
                        gvMaterials.Columns[8].Visible = true;
                        gvMaterials.HeaderRow.Cells[9].Visible = true;
                        gvMaterials.Columns[9].Visible = true;

                        gvMaterials.HeaderRow.Cells[10].Visible = true;
                        gvMaterials.Columns[10].Visible = true;
                        gvMaterials.HeaderRow.Cells[11].Visible = true;
                        gvMaterials.Columns[11].Visible = true;
                        gvMaterials.HeaderRow.Cells[12].Visible = true;
                        gvMaterials.Columns[12].Visible = true;
                        gvMaterials.HeaderRow.Cells[13].Visible = true;
                        gvMaterials.Columns[13].Visible = true;
                    }
                    if (type == "Approval" || type == "ApprovalEdit")
                    {
                        gvMaterials.HeaderRow.Cells[6].Visible = false;
                        gvMaterials.Columns[6].Visible = false;
                        gvMaterials.HeaderRow.Cells[7].Visible = false;
                        gvMaterials.Columns[7].Visible = false;
                        gvMaterials.HeaderRow.Cells[8].Visible = false;
                        gvMaterials.Columns[8].Visible = false;
                        gvMaterials.HeaderRow.Cells[9].Visible = false;
                        gvMaterials.Columns[9].Visible = false;

                        gvMaterials.HeaderRow.Cells[14].Visible = true;
                        gvMaterials.Columns[14].Visible = true;
                        gvMaterials.HeaderRow.Cells[15].Visible = true;
                        gvMaterials.Columns[15].Visible = true;
                        gvMaterials.HeaderRow.Cells[16].Visible = true;
                        gvMaterials.Columns[16].Visible = true;
                        gvMaterials.HeaderRow.Cells[17].Visible = true;
                        gvMaterials.Columns[17].Visible = true;
                    }
                }


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{ex.Message.ToString()}" + "');", true);
            }
        }
        protected void lnkRefresh_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }

        private void ClearPage()
        {
            Init();
            Bind_Grid_Material();
            txtNarration.Text = "";
        }

        protected void btnOKClick_Click(object sender, EventArgs e)
        {
            try
            {
                //isMappedCodeDataUpdated();
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
                Bind_Grid_Material();
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
                double sumValuechk = obj.Where(x => x.type == "A").Sum(x => x.chkamt);
                double sumValueapr = obj.Where(x => x.type == "A").Sum(x => x.apramt);

                if (obj.Where(x => x.type == "Z").ToList().Count == 1)
                {

                    double percnt = obj.Where(x => x.type == "Z").FirstOrDefault().percnt;
                    double percntchk = obj.Where(x => x.type == "Z").FirstOrDefault().chkpercnt;
                    double percntapr = obj.Where(x => x.type == "Z").FirstOrDefault().aprpercnt;
                    double percntamt = 0.00;
                    double percntamtchk = 0.00;
                    double percntamtapr = 0.00;
                    if (percnt == 0.00)
                    {
                        percntamt = obj.Where(x => x.type == "Z").FirstOrDefault().qamt;
                        percnt = sumValue == 0.00 ? 0.00 : ((percntamt / sumValue) * 100);
                    }
                    else
                    {
                        percntamt = sumValue * (percnt / 100);
                    }

                    if (percntchk == 0.00)
                    {
                        percntamtchk = obj.Where(x => x.type == "Z").FirstOrDefault().chkamt;
                        percntchk = sumValuechk == 0.00 ? 0.00 : ((percntamt / sumValue) * 100);
                    }
                    else
                    {
                        percntamtchk = sumValuechk * (percntchk / 100);
                    }
                    if (percntapr == 0.00)
                    {
                        percntamtapr = obj.Where(x => x.type == "Z").FirstOrDefault().apramt;
                        percntapr = sumValueapr == 0.00 ? 0.00 : ((percntamt / sumValue) * 100);
                    }
                    else
                    {
                        percntamtapr = sumValueapr * (percntapr / 100);
                    }

                    obj.Where(x => x.type == "Z").FirstOrDefault().qamt = percntamt;
                    obj.Where(x => x.type == "Z").FirstOrDefault().percnt = percnt;
                    obj.Where(x => x.type == "Z").FirstOrDefault().chkamt = percntamtchk;
                    obj.Where(x => x.type == "Z").FirstOrDefault().chkpercnt = percntchk;
                    obj.Where(x => x.type == "Z").FirstOrDefault().apramt = percntamtapr;
                    obj.Where(x => x.type == "Z").FirstOrDefault().aprpercnt = percntapr;
                }
                Bind_Grid_Material();
                if (obj.Count > 0)
                {
                    ((Label)this.gvMaterials.FooterRow.FindControl("lblgvFAmt")).Text = obj.Sum(x => x.qamt).ToString("#,##0.00;-#,##0.00;");
                    ((Label)this.gvMaterials.FooterRow.FindControl("lblgvChkFAmt")).Text = obj.Sum(x => x.chkamt).ToString("#,##0.00;-#,##0.00;");
                    ((Label)this.gvMaterials.FooterRow.FindControl("lblgvAprFAmt")).Text = obj.Sum(x => x.apramt).ToString("#,##0.00;-#,##0.00;");
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
                string type = Request.QueryString["Type"] ?? "";
                //Quotinfb
                if (Request.QueryString["Type"].ToString() == "Entry")
                {
                    getNewQuotationNo();
                }
                lbtnTotal_Click(null, null);
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = GetComCode();
                string userId = hst["usrid"].ToString();
                string date = txtEntryDate.Text;
                string quotid = lblQuotation.Text;
                string customerid = ddlCustomer.SelectedValue.ToString();
                string narration = txtNarration.Text;
                string isCheck = (type == "Check" || type == "CheckEdit") ? "1" : "0";
                string isAppr = "0";
                string status = (type == "Check" || type == "CheckEdit") ? "2" : type == "Approval" ? "3" : "1";
                List<EQuotation> obj = (List<EQuotation>)ViewState["MaterialList"];
                if (obj.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Must Select atleast one Resource" + "');", true);

                }
                else
                {
                    bool result = false;
                    switch (type)
                    {
                        case "ApprovalEdit":

                            result = _process.UpdateTransInfo2(comcod, "[dbo_Services].[SP_ENTRY_QUOTATION]", "UPDATEAPPRQUOTINFB", quotid, status, userId, lblActcode.Text, "", "", "", "",
                                        "", "", "", "", "", "", "", "", "", "", "", "", "");
                            break;
                        case "Check":
                        case "CheckEdit":
                            result = _process.UpdateTransInfo2(comcod, "[dbo_Services].[SP_ENTRY_QUOTATION]", "UPDATECHECKQUOTINFB", quotid, status, userId, "", "", "", "", "",
                                       "", "", "", "", "", "", "", "", "", "", "", "", "");
                            break;
                        default:
                            result = _process.UpdateTransInfo2(comcod, "[dbo_Services].[SP_ENTRY_QUOTATION]", "UPSERTQUOTINFB", quotid, date, customerid, narration, isCheck, isAppr, status, userId,
                                        "", "", "", "", "", "", "", "", "", "", "", "", "");
                            break;
                    }

                    if (result)
                    {
                        bool resultdelete = _process.UpdateTransInfo(comcod, "[dbo_Services].[SP_ENTRY_QUOTATION]", "DELETEQUOTINFA", quotid);
                        if (resultdelete)
                        {
                            List<bool> resultQuotArray = new List<bool>();

                            foreach (var item in obj)
                            {
                                bool resultQuotA = false;
                                if (item.qamt != 0.00 || item.chkamt != 0.00 || item.apramt != 0.00)
                                {
                                    string worktype = item.worktypecode.ToString();
                                    string resource = item.resourcecode.ToString();
                                    string qqty = item.qqty.ToString();
                                    string qamt = item.qamt.ToString();
                                    string chkqty = item.chkqty.ToString();
                                    string chkamt = item.chkamt.ToString();
                                    string aprqty = item.aprqty.ToString();
                                    string apramt = item.apramt.ToString();
                                    string percnt = item.percnt.ToString();
                                    string percntchk = item.chkpercnt.ToString();
                                    string percntapr = item.aprpercnt.ToString();
                                    resultQuotA = _process.UpdateTransInfo2(comcod, "[dbo_Services].[SP_ENTRY_QUOTATION]", "UPSERTQUOTINFA", quotid, worktype, resource, qqty, qamt,
                                        chkqty, chkamt, aprqty, apramt, userId, percnt, percntchk, percntapr, "", "", "", "", "", "", "", "");
                                    resultQuotArray.Add(resultQuotA);



                                }
                            }
                            if (type == "Approval")
                            {
                                string resultCodebook = isMappedCodeDataUpdated();
                                if (resultCodebook != "")
                                {
                                    result = _process.UpdateTransInfo2(comcod, "[dbo_Services].[SP_ENTRY_QUOTATION]", "UPDATEAPPRQUOTINFB", quotid, status, userId, resultCodebook, "", "", "", "",
                                       "", "", "", "", "", "", "", "", "", "", "", "", "");
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured" + "');", true);

                                }

                            }
                            if (resultQuotArray.Contains(false))
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured" + "');", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + $"{quotid} - Updated Successful" + "');", true);
                                if (type == "Entry")
                                {
                                    ClearPage();
                                }
                                if (type == "Approval")
                                {
                                    Response.Redirect("/F_70_Services/QuotationEntry?Type=ApprovalEdit&QId=" + quotid);
                                }
                            }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured." + "');", true);
                    }
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{ex.Message.ToString()}" + "');", true);
            }

        }
        private string isMappedCodeDataUpdated()
        {
            DataTable dt = (DataTable)ViewState["ResourceToActcode"];
            List<EQuotation> obj = (List<EQuotation>)ViewState["MaterialList"];
            string worktype = "";
            if (obj.Count > 0)
            {
                worktype = obj[0].worktypecode;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Must have data to continue" + "');", true);
                return "";
            }


            DataTable dt01 = dt.Select($"acttdesc='{worktype}'").CopyToDataTable();
            if (dt01.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Must Link Work Type with Accounts code-41" + "');", true);
                return "";
            }
            else
            {
                string code = ASTUtility.Left(dt01.Rows[0]["actcode"].ToString(), 4);

                DataTable dt02 = dt.Select($"actcode like '{code}%'").CopyToDataTable();
                if (dt02 == null || dt02.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Must Link Work Type with Accounts code-41" + "');", true);
                    return "";
                }
                else
                {
                    Int64 max = Convert.ToInt64(dt02.AsEnumerable()
                        .Max(row => row["actcode"]));
                    Hashtable hst = (Hashtable)Session["tblLogin"];
                    string userid = hst["usrid"].ToString();
                    string comcod = this.GetComCode();
                    string SubCode2 = max.ToString().Length < 8 ? "" : ASTUtility.Left(max.ToString(), 8);
                    string ProjectName = ddlCustomer.SelectedItem.Text;
                    string ProjectNameBN = "";
                    string ShortName = ddlCustomer.SelectedItem.Text;

                    DataSet ds = _process.GetTransInfo(comcod, "SP_ENTRY_MGT", "INSERTPROJECT", SubCode2, ProjectName, ShortName, userid,
                        ProjectNameBN, "", "", "", "", "", "");
                    if (ds == null)
                    {
                        return "";
                    }
                    else
                    {
                        return ds.Tables[0].Rows[0]["actcode"].ToString();
                    }

                }

            }

        }
        private void getReqNo()
        {
            string comcod = GetComCode();
            string CurDate1 = txtEntryDate.Text;
            DataSet ds1 = _process.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETLASTREQINFO", CurDate1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            if (ds1.Tables[0].Rows.Count > 0)
            {
                lblReqno.Text = ds1.Tables[0].Rows[0]["maxreqno"].ToString();
            }
            else
            {
                lblReqno.Text = "";
            }
        }

        private void CreateDataTable()
        {

            ViewState.Remove("tblapproval");
            DataTable tblt01 = new DataTable();
            tblt01.Columns.Add("fappid", Type.GetType("System.String"));
            tblt01.Columns.Add("fappdat", Type.GetType("System.String"));
            tblt01.Columns.Add("fapptrmid", Type.GetType("System.String"));
            tblt01.Columns.Add("fappseson", Type.GetType("System.String"));
            tblt01.Columns.Add("sappid", Type.GetType("System.String"));
            tblt01.Columns.Add("sappdat", Type.GetType("System.String"));
            tblt01.Columns.Add("sapptrmid", Type.GetType("System.String"));
            tblt01.Columns.Add("sappseson", Type.GetType("System.String"));
            ViewState["tblapproval"] = tblt01;
        }

        private void getApproval()
        {
            
        }


        private void UpdateMaterialRequisition()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string comcod = GetComCode();
            string reqno = lblReqno.Text;
            string reqdate = txtEntryDate.Text;
            string PostedByid = userid;
            string Posttrmid = Terminal;
            string PostSession = Sessionid;
            string PostedDat = Date;
            string pactcode = lblActcode.Text == "" ? "" : "16" + ASTUtility.Right(lblActcode.Text, 10);
            string flrcod = "000";
            string requsrid = "";
            string mrfno = lblQuotation.Text;
            string reqnar = txtNarration.Text;
            string CRMPostedByid = userid;
            string CRMPosttrmid = Terminal;
            string CRMPostSession = Sessionid;
            string CRMPostedDat = Date;
            string checkbyid = userid;
            string quotid = lblQuotation.Text;
            List<EQuotation> obj = ((List<EQuotation>)ViewState["MaterialList"]).Where(x => x.resourcecode.StartsWith("01")).ToList();
            
            DataSet ds1 = new DataSet("ds1");
            this.CreateDataTable();
            DataTable dt = (DataTable)ViewState["tblapproval"];
            DataRow dr1 = dt.NewRow();
            dr1["fappid"] = userid;
            dr1["fappdat"] = Date;
            dr1["fapptrmid"] = Posttrmid;
            dr1["fappseson"] = PostSession;
            dr1["sappid"] = userid;
            dr1["sappdat"] = Date;
            dr1["sapptrmid"] = Posttrmid;
            dr1["sappseson"] = PostSession;
            dt.Rows.Add(dr1);
            ds1.Merge(dt);
            ds1.Tables[0].TableName = "tbl1";
            string approval = ds1.GetXml();

            bool result = _process.UpdateTransInfo2(comcod, "SP_ENTRY_FACILITYMGT", "UPDATEMATPURREQB", reqno, reqdate, pactcode, flrcod, requsrid, mrfno, reqnar,
                PostedByid, Posttrmid, PostSession, PostedDat, CRMPostedByid, CRMPosttrmid, CRMPostSession, CRMPostedDat, "", "", "", "", "", "");


            result = _process.UpdateTransInfo3(comcod, "SP_ENTRY_PURCHASE_01", "UPDATEREQCHECKED", reqno, checkbyid, Terminal,
                Sessionid, Date, approval, "", "",
                "", "AAAAAAAAAAAA", "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result)
            {
                int i = 1;
                List<bool> resultCompA = new List<bool>();
                foreach (var item in obj)
                {
                    string rowId = i.ToString();
                    string mRSIRCODE = item.resourcecode.ToString();
                    string mSPCFCOD = "000000000000";

                    double mPREQTY = Convert.ToDouble(item.aprqty);
                    double mAREQTY = Convert.ToDouble("0.00");
                    double mBgdBalQty = Convert.ToDouble("0.00");
                    string mREQRAT = "0.00";      //item.rate.ToString();
                    string mREQSRAT = "0.00";     //item.rate.ToString();
                    string mPSTKQTY = "0.00";
                    string mEXPUSEDT = "";
                    string mREQNOTE = "";
                    string PursDate = "";
                    string Lpurrate = "0.00";
                    string storecode = "";
                    string ssircode = "";
                    string orderno = "";



                    bool resultA = _process.UpdateTransInfo3(comcod, "SP_ENTRY_FACILITYMGT", "UPDATEMATPURREQA", "",
                              reqno, mRSIRCODE, mSPCFCOD, mPREQTY.ToString(), mAREQTY.ToString(), mREQRAT, mPSTKQTY, mEXPUSEDT, mREQNOTE,
                              PursDate, Lpurrate, storecode, ssircode, orderno, mREQSRAT, rowId, "", "", "", "", "", "");
                    resultCompA.Add(resultA);
                    i++;
                }
                if (resultCompA.Contains(false))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured" + "');", true);
                }
                else
                {
                    bool resultMatReq = _process.UpdateTransInfo2(comcod, "[dbo_Services].[SP_ENTRY_QUOTATION]", "UPDATEMATREQNO", quotid, reqno, "", "", "", "", "", "",
                                       "", "", "", "", "", "", "", "", "", "", "", "", "");

                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + $"Material Requisition Generated" + "');", true);
                }
            }
        }


        protected void gvMaterials_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string type = Request.QueryString["Type"] ?? "";
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string materialId = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "resourcecode")).ToString();
                TextBox amt = (TextBox)e.Row.FindControl("txtAmount");
                TextBox qty = (TextBox)e.Row.FindControl("txtgvQuantity");
                TextBox rate = (TextBox)e.Row.FindControl("txtgvRate");
                TextBox percnt = (TextBox)e.Row.FindControl("txtgvPercnt");
                TextBox chkamt = (TextBox)e.Row.FindControl("txtChkAmount");
                TextBox chkqty = (TextBox)e.Row.FindControl("txtgvChkQuantity");
                TextBox chkrate = (TextBox)e.Row.FindControl("txtgvChkRate");
                TextBox chkpercnt = (TextBox)e.Row.FindControl("txtgvChkPercnt");
                if (materialId == "049700101001")
                {
                    qty.Enabled = false;
                    amt.Enabled = true;
                    rate.Enabled = false;
                    percnt.Enabled = true;

                    chkqty.Enabled = false;
                    chkamt.Enabled = true;
                    chkrate.Enabled = false;
                    chkpercnt.Enabled = true;
                }
                else
                {
                    rate.Enabled = true;
                    qty.Enabled = true;
                    amt.Enabled = false;
                    percnt.Enabled = false;

                    chkrate.Enabled = true;
                    chkqty.Enabled = true;
                    chkamt.Enabled = false;
                    chkpercnt.Enabled = false;
                }
                if (type == "Check")
                {
                    qty.Enabled = false;
                    amt.Enabled = false;
                    rate.Enabled = false;
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
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Updated Failed" + "');", true);
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

        protected void lnkMatReq_Click(object sender, EventArgs e)
        {
            List<EQuotation> obj = (List<EQuotation>)ViewState["MaterialList"];
            int isContain = obj.Where(x => x.resourcecode.StartsWith("01")).ToList().Count;
            if (isContain == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"No Material for Material Requisition" + "');", true);
            }
            else
            {
                UpdateMaterialRequisition();
            }
        }

        protected void lnkReceivable_Click(object sender, EventArgs e)
        {
            UpdateReceivable();
        }

        private DataSet getReceivableInfo()
        {
            string comcod = GetComCode();
            string quotid = lblQuotation.Text;
            DataSet ds = _process.GetTransInfo(comcod, "[dbo_Services].[SP_ENTRY_QUOTATION]", "GETRECEIVABLE", quotid, "", "", "", "", "", "", "", "", "", "");
            if (ds == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{_process.ErrorObject["Msg"].ToString()}" + "');", true);
                return null;
            }
            else
            {
                return ds;
            }
        }

        private void UpdateReceivable()
        {
            //((Label)this.Master.FindControl("lblmsg")).Visible = true;
            //int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

            //// DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            //if (!Convert.ToBoolean(dr1[0]["entry"]))
            //{
            //    ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            //    return;
            //}
            string quotid = lblQuotation.Text;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();
            string Terminal = hst["trmid"].ToString();
            string Sessionid = hst["session"].ToString();
            string Postdat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            //string vounum = this.txtcurrentvou.Text.Trim() + this.txtCurrntlast6.Text.Trim();

            string voudat =System.DateTime.Now.ToString("dd-MMM-yyyy");
           
            string vounum = getVoucher();
            string refnum = lblQuotation.Text;
            string srinfo = "";
            string vounarration1 = "";
            string vounarration2 = (vounarration1.Length > 200 ? vounarration1.Substring(200) : "");           
            string voutype = "Journal Voucher";
            string cactcode = "000000000000";
            string vtcode = "98";
            string edit = "";



            //Existing   Purchase No              

            DataSet ds4 = _process.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "EXISTINGSERVICEVOUCHER", quotid, "", "", "", "", "", "", "", "");
            if (ds4.Tables[0].Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Voucher No already Existing in Sale Journal" + "');", true);
                return;
            }
            try
            {
                //-----------Update Transaction B Table-----------------//
                bool resultb = _process.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", vounum, voudat, refnum, srinfo,
                        vounarration1, vounarration2, voutype, vtcode, edit, userid, Terminal, Sessionid, Postdat, "", "");




                if (!resultb)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"{_process.ErrorObject["Msg"].ToString()}" + "');", true);

                    return;
                }
                //-----------Update Transaction A Table-----------------//

                DataTable dt = getReceivableInfo().Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string actcode = dt.Rows[i]["actcode"].ToString();
                    string rescode = dt.Rows[i]["rescode"].ToString();
                    string spclcode = dt.Rows[i]["spcode"].ToString();
                    string trnqty = "0";
                    double Dramt = Convert.ToDouble("0" + dt.Rows[i]["Dr"].ToString());
                    double Cramt = Convert.ToDouble("0" + dt.Rows[i]["Cr"].ToString());
                    string trnamt = Convert.ToString(Dramt - Cramt);
                    string trnremarks = "";
                    bool resulta = _process.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "ACVUPDATE", vounum,
                            actcode, rescode, cactcode, voudat, trnqty, trnremarks, vtcode, trnamt, spclcode, "", "", "", "", "");
                    if (!resulta)
                    {
                       
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"{_process.ErrorObject["Msg"].ToString()}" + "');", true);

                        return;
                    }

                    if (ASTUtility.Left(actcode, 2) == "18")
                    {
                        resulta = _process.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "UPDATEMAINSERVICEJOURNAL", quotid, vounum, "", "", "", "", "", "", "", "", "", "", "", "", "");
                        if (!resulta)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"{_process.ErrorObject["Msg"].ToString()}" + "');", true);

                            return;
                        }

                    }
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + $"Receivable Generated" + "');", true);

                
                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Sales Journal";
                    string eventdesc = "Update Journal";
                    string eventdesc2 = vounum;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }
                this.lnkReceivable.Enabled = false;
               
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }

        }

        private string getVoucher()
        {
            try
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;

                string comcod = this.GetComCode();

                DataSet ds2 = _process.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETOPENINGDATE", "", "", "", "", "", "", "", "", "");
                if (ds2.Tables[0].Rows.Count == 0)
                {
                    return "";
                }

                DateTime txtopndate = Convert.ToDateTime(ds2.Tables[0].Rows[0]["voudat"]);
                string entrydate = System.DateTime.Today.ToString("dd-MMM-yyyy");
                if (txtopndate >= Convert.ToDateTime(entrydate))
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Voucher Date Must  Be Greater then Opening Date";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return "";

                }

                string VNo3 = "JV";
                
                DataSet ds4 = _process.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETNEWVOUCHER", entrydate, VNo3, "", "", "", "", "", "", "");
                DataTable dt4 = ds4.Tables[0];
                string cvno1 = dt4.Rows[0]["couvounum"].ToString().Substring(0, 8);                
                return dt4.Rows[0]["couvounum"].ToString();

            }
            catch (Exception ex)
            {
                return "";

            }
        }

        private void getSubContractor()
        {
            try
            {
                string comcod = GetComCode();
                DataSet ds = _process.GetTransInfo(comcod, "[dbo_Services].[SP_ENTRY_QUOTATION]", "GETSUBCONTRACTOR", "", "", "", "", "", "", "", "", "", "", "");
                if (ds == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{_process.ErrorObject["Msg"].ToString()}" + "');", true);
                    return;
                }
                ddlSubContractor.DataSource = ds.Tables[0];
                ddlSubContractor.DataTextField = "sirdesc";
                ddlSubContractor.DataValueField = "sircode";
                ddlSubContractor.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{ex.Message.ToString()}" + "');", true);
            }
        }

        protected void lnkSubContractor_Click(object sender, EventArgs e)
        {
            getSubContractor();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenModalSubContractor();", true);
        }
        private void CreateDataTableLISU()
        {

            ViewState.Remove("tblapproval");
            DataTable tblt01 = new DataTable();



            tblt01.Columns.Add("frecid", Type.GetType("System.String"));
            tblt01.Columns.Add("frecdat", Type.GetType("System.String"));
            tblt01.Columns.Add("frectrmid", Type.GetType("System.String"));
            tblt01.Columns.Add("frecseson", Type.GetType("System.String"));
            tblt01.Columns.Add("secrecid", Type.GetType("System.String"));
            tblt01.Columns.Add("secrecdat", Type.GetType("System.String"));
            tblt01.Columns.Add("secrectrmid", Type.GetType("System.String"));
            tblt01.Columns.Add("secrecseson", Type.GetType("System.String"));
            tblt01.Columns.Add("threcid", Type.GetType("System.String"));
            tblt01.Columns.Add("threcdat", Type.GetType("System.String"));
            tblt01.Columns.Add("threctrmid", Type.GetType("System.String"));
            tblt01.Columns.Add("threcseson", Type.GetType("System.String"));


            ViewState["tblapproval"] = tblt01;
        }


        protected void lnkSubContractorSave_Click(object sender, EventArgs e)
        {
            string comcod = GetComCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string trmid = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string curdate = System.DateTime.Today.ToString("dd-MMM-yyyy");

            DataSet ds2 = _process.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETLASTLABISSUENO", curdate,
                      "LIS", "", "", "", "", "", "", "");


            DataSet ds02 = _process.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "LASTCBILLNO", curdate,
                       "", "", "", "", "", "", "", "");

            string csircode = ddlSubContractor.SelectedValue.ToString();
            string pactcode = lblActcode.Text == "" ? "" : "16" + ASTUtility.Right(lblActcode.Text, 10);
            
            string billno = ds02.Tables[0].Rows[0]["maxbillno"].ToString();
            string mISUNO = ds2.Tables[0].Rows[0]["maxmisuno"].ToString();
            string mISURNAR = "";
            string Remarks = "";
            string cbillref = "";
            string trade = "";
            string rano = "";
            string billtype = "";
            string percentage = Convert.ToDouble("0").ToString();
            string sdamt = Convert.ToDouble("0").ToString();
            string dedamt = Convert.ToDouble("0").ToString();
            string Penalty = Convert.ToDouble("0").ToString();
            string advamt = Convert.ToDouble("0").ToString();
            string Reward = Convert.ToDouble("0").ToString();
            string workorder = "";
            string EditByid = "";
            string Editdat = "01-Jan-1900";
            string EditSession = "";
            string Edittrmid = "";

            this.CreateDataTableLISU();
            DataSet ds1 = new DataSet("ds1");
            DataTable dt = (DataTable)ViewState["tblapproval"];
            DataRow dr1 = dt.NewRow();

            dr1["frecid"] = userid;
            dr1["frecdat"] = curdate;
            dr1["frectrmid"] = trmid;
            dr1["frecseson"] = Sessionid;
            dr1["secrecid"] = userid;
            dr1["secrecdat"] = curdate;
            dr1["secrectrmid"] = trmid;
            dr1["secrecseson"] = Sessionid;
            dr1["threcid"] = userid;
            dr1["threcdat"] = curdate;
            dr1["threctrmid"] = trmid;
            dr1["threcseson"] = Sessionid;
            dt.Rows.Add(dr1);


            ds1.Merge(dt);
            ds1.Tables[0].TableName = "tbl1";
            string Approval = ds1.GetXml();

            bool result = _process.UpdateTransInfo2(comcod, "SP_ENTRY_PURCHASE_03", "UPDATEPURLABISSUEINFO", "PURLISSUEB",
                             mISUNO, curdate, pactcode, csircode, mISURNAR, Remarks, userid, Sessionid, trmid, trade, rano, 
                             percentage, sdamt, dedamt, Penalty, advamt, Reward, workorder, "", "");

            result = _process.UpdateTransInfo3(comcod, "SP_ENTRY_PURCHASE_02", "INSORUPDATECBILL", "PURCBILLB", billno, pactcode, 
                csircode, curdate, Remarks, cbillref, percentage, sdamt, dedamt, Penalty, advamt, billtype, Reward, userid,
                curdate, Sessionid, trmid, EditByid, Editdat, EditSession, Edittrmid, Approval);

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured" + "');", true);

            }
            else
            {
                List<EQuotation> obj = ((List<EQuotation>)ViewState["MaterialList"]).Where(x => x.resourcecode.StartsWith("04")).ToList();
                List<bool> resultCompA = new List<bool>();
                foreach (var item in obj)
                {
                    string Flrcod = "000";
                    string Rsircode = item.resourcecode.ToString();
                    string prcent= Convert.ToDouble("0").ToString();
                    string Isuqty = item.aprqty.ToString();
                    string Isuamt = item.apramt.ToString();
                    string wrkqty= Convert.ToDouble("0").ToString();
                    string grp = "001";
                    string mbbook = "";
                    string above = Convert.ToDouble("0").ToString();
                    string dedqty= Convert.ToDouble("0").ToString();
                    string dedunit = "";
                    string idedamt= Convert.ToDouble("0").ToString();

                    bool resultA = _process.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "UPDATEPURLABISSUEINFO", "PURLISSUEA", mISUNO, Flrcod,
                            Rsircode, prcent, Isuqty.ToString(), Isuamt.ToString(), wrkqty, grp, mbbook, above, dedqty, dedunit, idedamt, "");

                    result = _process.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "INSORUPDATECBILL", "PURCBILLA", billno, Flrcod, Rsircode,
                        Isuqty.ToString(), Isuamt, mISUNO, grp, above, dedqty, dedunit, idedamt, "", "", "");


                    resultCompA.Add(resultA);
                    
                }
                if (resultCompA.Contains(false))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured" + "');", true);
                }
                else
                {
                    string quotid = lblQuotation.Text;
                    bool resultMatReq = _process.UpdateTransInfo2(comcod, "[dbo_Services].[SP_ENTRY_QUOTATION]", "UPDATESUBCONTRACTOR", billno, mISUNO, "", "", "", "", "", "",
                                       "", "", "", "", "", "", "", "", "", "", "", "", "");

                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + $"Sub COntractor Bill Generated" + "');", true);
                }
            }


        }
    }
}