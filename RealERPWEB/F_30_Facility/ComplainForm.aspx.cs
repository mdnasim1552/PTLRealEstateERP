﻿using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static RealERPEntity.C_30_Facility.EClass_Facility_Mgt;

namespace RealERPWEB.F_30_Facility
{
    public partial class ComplainForm : System.Web.UI.Page
    {
        ProcessAccess _process = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtEntryDate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
                txtEstimatedDate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
                CreateComplainList();
                loadWork();
                loadProject();
                ddlProject_SelectedIndexChanged(null, null);
                GETCOMTYPE();
                GETWARRANTY();
                if (Request.QueryString["ComplNo"] != null)
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
            ddlProject.Enabled = false;
            ddlCustomer.Enabled = false;
            ddlIssueType.Enabled = false;
            pnlComplain.Visible = true;            
            btnOKClick.Enabled = false;
            DataTable dt = ds.Tables[1];
            if(dt.Rows.Count>0 || dt == null)
            {
                ddlWarranty.SelectedValue = dt.Rows[0]["warranty"].ToString();
                ddlCommunicationType.SelectedValue = dt.Rows[0]["communicationtype"].ToString();
                txtEstimatedDate.Text = dt.Rows[0]["estimateddate"].ToString();
                txtNarration.Text= dt.Rows[0]["addremarks"].ToString();
                ddlIssueType.SelectedValue = dt.Rows[0]["issuetype"].ToString();
                ddlProject.SelectedValue = dt.Rows[0]["pactcode"].ToString();
                ddlCustomer.SelectedValue = dt.Rows[0]["custcode"].ToString();
                getHandOverAndUnit();
            }
            


        }

        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void loadWork()
        {
            string comcod = GetComCode();
            DataSet ds = _process.GetTransInfo(comcod, "SP_ENTRY_FACILITYMGT", "GETWORKTYPEDDL", "", "", "", "", "", "", "", "", "", "", "");
            ddlIssueType.DataSource = ds.Tables[0];
            ddlIssueType.DataTextField = "sirdesc";
            ddlIssueType.DataValueField = "sircode";
            ddlIssueType.DataBind();
        }
        private void loadProject()
        {
            string comcod = GetComCode();
            DataSet ds = _process.GetTransInfo(comcod, "SP_ENTRY_FACILITYMGT", "GETPROJECTDDL", "", "", "", "", "", "", "", "", "", "", "");
            ddlProject.DataSource = ds.Tables[0];
            ddlProject.DataTextField = "actdesc";
            ddlProject.DataValueField = "actcode";
            ddlProject.DataBind();
        }
        protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            string projectcode = ddlProject.SelectedValue.ToString();
            loadCustomer(projectcode);
        }
        private void loadCustomer(string projectcode)
        {
            string comcod = GetComCode();
            DataSet ds = _process.GetTransInfo(comcod, "SP_ENTRY_FACILITYMGT", "GETCUSTOMERDDL", projectcode, "", "", "", "", "", "", "", "", "", "");
            ddlCustomer.DataSource = ds.Tables[0];
            ddlCustomer.DataTextField = "sirdesc";
            ddlCustomer.DataValueField = "usircode";
            ddlCustomer.DataBind();
        }

        private void getHandOverAndUnit()
        {
            string comcod = GetComCode();
            string projectcode = ddlProject.SelectedValue.ToString();
            string customercode = ddlCustomer.SelectedValue.ToString();
            DataSet ds = _process.GetTransInfo(comcod, "SP_ENTRY_FACILITYMGT", "GETHANDOVERANDUNIT", projectcode, customercode, "", "", "", "", "", "", "", "", "");
            DataTable unitdesc = ds.Tables[0];
            DataTable handover = ds.Tables[1];
            if (unitdesc == null || unitdesc.Rows.Count == 0)
            {
                //Error Message Show
            }
            else
            {
                lblUnitText.Text = unitdesc.Rows[0]["udesc"].ToString();
            }
            if (handover == null || handover.Rows.Count == 0)
            {
                //Error Message Show
                lblHandOverDateText.Text = "None Found";
                lblWarrantyRemainText.Text = "None";
            }
            else
            {
                lblHandOverDateText.Text = handover.Rows[0]["dateho"].ToString();
                string hodate = lblHandOverDateText.Text;
                DateTime warperiod = Convert.ToDateTime(hodate).AddYears(1);
                DateTime todate = System.DateTime.Today;
                if (warperiod > todate)
                {
                    //In Warranty
                    lblWarrantyRemainText.Text = (warperiod - todate).TotalDays.ToString();
                    ddlWarranty.SelectedValue = "43001";
                }
                else
                {
                    //Out of Warranty
                    lblWarrantyRemainText.Text = "Out of Warranty";
                    ddlWarranty.SelectedValue = "43002";
                }


            }
        }

        private void GETCOMTYPE()
        {
            string comcod = GetComCode();
            string gcodetype = "41%";
            DataSet ds = _process.GetTransInfo(comcod, "SP_ENTRY_FACILITYMGT", "GETCOMMUNICATIONTYPE", gcodetype, "", "", "", "", "", "", "", "", "", "");
            ddlCommunicationType.DataSource = ds.Tables[0];
            ddlCommunicationType.DataTextField = "gdesc";
            ddlCommunicationType.DataValueField = "gcod";
            ddlCommunicationType.DataBind();
        }
        private void GETWARRANTY()
        {
            string comcod = GetComCode();
            string gcodetype = "43%";
            DataSet ds = _process.GetTransInfo(comcod, "SP_ENTRY_FACILITYMGT", "GETCOMMUNICATIONTYPE", gcodetype, "", "", "", "", "", "", "", "", "", "");
            ddlWarranty.DataSource = ds.Tables[0];
            ddlWarranty.DataTextField = "gdesc";
            ddlWarranty.DataValueField = "gcod";
            ddlWarranty.DataBind();
        }

        protected void btnOKClick_Click(object sender, EventArgs e)
        {
            if (btnOKClick.Text == "OK")
            {
                ddlProject.Enabled = false;
                ddlCustomer.Enabled = false;
                ddlIssueType.Enabled = false;
                pnlComplain.Visible = true;
                getHandOverAndUnit();
                btnOKClick.Text = "New";
            }
            else
            {
                ddlProject.Enabled = true;
                ddlCustomer.Enabled = true;
                ddlIssueType.Enabled = true;
                pnlComplain.Visible = false;
                btnOKClick.Text = "OK";
            }
        }
        private void CreateComplainList()
        {
            List<EClass_Complain_List> obj = new List<EClass_Complain_List>();
            ViewState["ComplainList"] = obj;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            List<EClass_Complain_List> obj = (List<EClass_Complain_List>)ViewState["ComplainList"];
            int complainId = 1;
            if (obj.Count != 0)
            {
                complainId = obj.Max(x => x.complainId) + 1;
            }
            string complainDesc = txtComplainDesc.Text;
            string remarks = txtComplainRemarks.Text;
            if (complainDesc != "")
            {
                obj.Add(new EClass_Complain_List { complainId = complainId, complainDesc = complainDesc, remarks = remarks });
                Bind_Grid(obj);
                txtComplainDesc.Text = "";
                txtComplainRemarks.Text = "";
            }
        }
        private void Bind_Grid(List<EClass_Complain_List> obj)
        {
            ViewState["ComplainList"] = obj;
            dgv1.DataSource = obj;
            dgv1.DataBind();
        }

        protected void LnkbtnDelete_Click(object sender, EventArgs e)
        {
            GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            int RowIndex = gvr.RowIndex;
            EClass_Complain_List obj = ((List<EClass_Complain_List>)ViewState["ComplainList"])[RowIndex];
            List<EClass_Complain_List> list = (List<EClass_Complain_List>)ViewState["ComplainList"];
            list.Remove(obj);
            Bind_Grid(list);
        }

        protected void LnkbtnEdit_Click(object sender, EventArgs e)
        {
            GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            int RowIndex = gvr.RowIndex;

            string id = ((List<EClass_Complain_List>)ViewState["ComplainList"])[RowIndex].complainId.ToString();
            string complainDesc = ((List<EClass_Complain_List>)ViewState["ComplainList"])[RowIndex].complainDesc.ToString();
            string remarks = ((List<EClass_Complain_List>)ViewState["ComplainList"])[RowIndex].remarks.ToString();
            lblId.Text = id;
            txtProblemDetails.Text = complainDesc;
            txtmodalRemarks.Text = remarks;
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModalComplain();", true);
        }

        protected void lnkUpdateComplain_Click(object sender, EventArgs e)
        {
            List<EClass_Complain_List> list = (List<EClass_Complain_List>)ViewState["ComplainList"];
            string id = lblId.Text;
            string problemDetails = txtProblemDetails.Text;
            string remarks = txtmodalRemarks.Text;
            EClass_Complain_List obj = list.Where(x => x.complainId == Convert.ToInt32(id)).FirstOrDefault();
            if (obj != null)
            {
                obj.complainDesc = problemDetails;
                obj.remarks = remarks;
                Bind_Grid(list);
            }
        }

        protected void lnkProceed_Click(object sender, EventArgs e)
        {

        }

        protected void lnkSave_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComCode();
            string complno = Request.QueryString["ComplNo"]??"0";
            string pactcode = ddlProject.SelectedValue.ToString();
            string custcode = ddlCustomer.SelectedValue.ToString();
            string unit = lblUnitText.Text;
            string warranty = ddlWarranty.SelectedValue.ToString();
            string compldate = txtEntryDate.Text;
            string comunicationtype = ddlCommunicationType.SelectedValue.ToString();
            string estimateddate = txtEstimatedDate.Text;
            string addremarks = txtNarration.Text;
            string userId = hst["usrid"].ToString();
            string issuetype = ddlIssueType.SelectedValue.ToString();

            DataSet ds = _process.GetTransInfoNew(comcod, "SP_ENTRY_FACILITYMGT", "UPSERTCOMPLAINB",null,null,null, complno, pactcode, custcode, unit, warranty, compldate, comunicationtype, estimateddate, addremarks, issuetype,"","","",
                "","","","","","",userId);
            DataTable dt = ds.Tables[0];
            if (dt!=null || dt.Rows.Count>0)
            {
                int i = 1;
                bool resultDelete = _process.UpdateTransInfo3(comcod, "SP_ENTRY_FACILITYMGT", "DElETECOMPLAINA", dt.Rows[0]["complno"].ToString(), "", "", "", "", "", "", "", "", "", "", "",
                            "", "", "", "", "", "", "", "", "", "", userId);
                List<EClass_Complain_List> list = (List<EClass_Complain_List>)ViewState["ComplainList"];
                foreach (var item in list)
                {
                    bool resultA = _process.UpdateTransInfo3(comcod, "SP_ENTRY_FACILITYMGT", "UPSERTCOMPLAINA", dt.Rows[0]["complno"].ToString(), item.complainDesc, item.remarks,i.ToString(), "", "", "", "", "", "", "", "",
                            "", "", "", "", "", "", "", "", "", "", userId);
                    if (!resultA)
                    {
                        //Failed
                    }
                    i++;
                }
                //Update Successful
                if (complno == "0")
                {
                    ClearPage();
                }
            }
            else
            {

            }
        }

        protected void lnkRefresh_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["ComplNo"]==null)
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
            txtEntryDate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
            ddlProject.SelectedIndex = 0;
            ddlCustomer.SelectedIndex = 0;
            ddlIssueType.SelectedIndex = 0;
            getHandOverAndUnit();
            ddlCommunicationType.SelectedIndex = 0;
            txtEstimatedDate.Text= System.DateTime.Now.ToString("dd-MMM-yyyy");
            txtNarration.Text = "";
            CreateComplainList();
            var obj = (List<EClass_Complain_List>)ViewState["ComplainList"];
            Bind_Grid(obj);
            ddlProject.Enabled = true;
            ddlCustomer.Enabled = true;
            ddlIssueType.Enabled = true;
            pnlComplain.Visible = false;
            btnOKClick.Text = "OK";
        }
    }
}