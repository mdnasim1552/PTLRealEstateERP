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
    public partial class Quotation : System.Web.UI.Page
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
                ((Label)this.Master.FindControl("lblTitle")).Text = "Process";
                //if (Request.QueryString["Type"] != null)
                //{
                //    pnlApproval.Visible = true;
                //    txtEntryDate.Enabled = false;
                //}
            }
        }

        private void EditFunctionality()
        {
            string comcod = GetComCode();
            string complno = Request.QueryString["ComplNo"].ToString();
            DataSet ds = _process.GetTransInfo(comcod, "SP_ENTRY_FACILITYMGT", "GETCOMPLAINFOREDIT", complno, "", "", "", "", "", "", "", "", "", "");
            List<EClass_Complain_List> obj = ds.Tables[0].DataTableToList<EClass_Complain_List>();
           
        }

        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

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
                    lblWarranty.Text = row["warrantydesc"].ToString();
                    lblWarrantyCode.Text = row["warranty"].ToString();
                    lblComplainDate.Text = Convert.ToDateTime(row["compldate"].ToString()).ToString("dd-MMM-yyyy");
                    lblRemarksCmp.Text= row["complrem"].ToString(); 
                    lblEngrDate.Text= Convert.ToDateTime(row["dgdate"].ToString()).ToString("dd-MMM-yyyy");
                    lblRemarksDg.Text= row["dgrem"].ToString();
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
                if (ds.Tables[1] != null || ds.Tables[1].Rows.Count > 0)
                {
                    var row = ds.Tables[1].Rows[0];
                    lblBgdDate.Text= Convert.ToDateTime(row["bgddate"].ToString()).ToString("dd-MMM-yyyy");
                    lblApprDate.Text= Convert.ToDateTime(row["approvalDate"].ToString()).ToString("dd-MMM-yyyy");
                    lblRemarksAppr.Text= row["notes"].ToString();
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
        protected void btnOKClick_Click(object sender, EventArgs e)
        {
            getComplainUser();
            createMaterialList();
        }

        protected void lnkQuotAcc_Click(object sender, EventArgs e)
        {
            string dgno = Request.QueryString["Dgno"] ?? ddlDgNo.SelectedValue.ToString();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComCode();
            string userId = hst["usrid"].ToString();
            bool resultflag = _process.UpdateTransInfo3(comcod, "SP_ENTRY_FACILITYMGT", "UPDATEQUOTAPPRFLAG", dgno, "", "", "", "", "", "", "", "", "", "", "",
                                             "", "", "", "", "", "", "", "", "", "", userId);
            if (resultflag)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + $"Quotation for Dg-{dgno} is Accepted" + "');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured" + "');", true);
            }
        }

        protected void lnkCollection_Click(object sender, EventArgs e)
        {

        }

        protected void lnkReq_Click(object sender, EventArgs e)
        {

        }
    }
}