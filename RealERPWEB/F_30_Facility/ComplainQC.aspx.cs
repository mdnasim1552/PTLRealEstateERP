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
    public partial class ComplainQC : System.Web.UI.Page
    {
        ProcessAccess _process = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getProjUnitddl();
                txtEntryDate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
                btnOKClick.Text = "<span class='fa fa-check-circle' style='color: white;' aria-hidden='true'></span> OK";
                ((Label)this.Master.FindControl("lblTitle")).Text = "Material Requisition";
                if (Request.QueryString["DgNo"] != null)
                {
                    btnOKClick.Enabled = false;
                    btnOKClick.Visible = false;
                    pnlMatReq.Visible = true;
                    btnOKClick.Text = "<span class='fa fa-arrow-circle-left' style='color: white;' aria-hidden='true'></span> New";
                    getComplainUser();
                }
            }
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
                    lblSiteVisisted.Text = row["sitevisiteddate"].ToString();
                    lblComplainDate.Text = Convert.ToDateTime(row["compldate"].ToString()).ToString("dd-MMM-yyyy");
                    lblRemarksCmp.Text = row["complrem"].ToString();
                }
                List<EClass_Complain_List> obj = dt01.DataTableToList<EClass_Complain_List>();
                List<EClass_Complain_List> obj1 = dt03.DataTableToList<EClass_Complain_List>();
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
        private void Session_List()
        {
            try
            {
                List<EClass_Complain_List> obj = (List<EClass_Complain_List>)ViewState["ComplainList"];
                int rowindex = 0;

                for (int i = 0; i < dgv1.Rows.Count; i++)
                {
                    rowindex = (dgv1.PageIndex) * dgv1.PageSize + i;
                    bool qcValue =((CheckBox)this.dgv1.Rows[rowindex].FindControl("chkQC")).Checked;
                    obj[rowindex].qc = qcValue;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{ex.Message.ToString()}" + "');", true);
            }
        }
        protected void btnOKClick_Click(object sender, EventArgs e)
        {
            if (btnOKClick.Text == "<span class='fa fa-check-circle' style='color: white;' aria-hidden='true'></span> OK")
            {
                pnlMatReq.Visible = true;
                btnOKClick.Text = "<span class='fa fa-arrow-circle-left' style='color: white;' aria-hidden='true'></span> New";
                getComplainUser();
            }
            else
            {
                pnlMatReq.Visible = false;
                btnOKClick.Text = "<span class='fa fa-check-circle' style='color: white;' aria-hidden='true'></span> OK";
            }
        }





        protected void lnkSave_Click(object sender, EventArgs e)
        {
            Session_List();
            List<EClass_Complain_List> obj = (List<EClass_Complain_List>)ViewState["ComplainList"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComCode();
            string userId = hst["usrid"].ToString();
            string dgno = Request.QueryString["Dgno"] ?? ddlDgNo.SelectedValue.ToString();
            string qcdate = txtEntryDate.Text;
            string Narration = txtNarration.Text;
            int i = 0;
            bool result = _process.UpdateTransInfo3(comcod, "SP_ENTRY_FACILITYMGT", "UPSERTQCB", dgno, qcdate, Narration, "", "", "", "", "", "", "", "", "", "",
                        "", "", "", "", "", "","","","", userId);
            if (result)
            {
                List<bool> resultCompA = new List<bool>();
                foreach (var item in obj)
                {
                    bool resultA = _process.UpdateTransInfo3(comcod, "SP_ENTRY_FACILITYMGT", "UPSERTQCA", dgno, item.complainDesc, "","", "", "", "", "", "", "", "", "",
                            "", "", "", "", "", "", "", "", "", "", userId);
                    resultCompA.Add(resultA);

                    i++;
                }
                if (resultCompA.Contains(false))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured" + "');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + $"QC Updated" + "');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured" + "');", true);
            }

            



        }
    }
}