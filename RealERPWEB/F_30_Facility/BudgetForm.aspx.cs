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
            DataSet ds = _process.GetTransInfo(comcod, "SP_ENTRY_FACILITYMGT", "GETPROJCUSTFROMCOMPLAIN", "", "", "", "", "", "", "", "", "", "", "");
            ddlDgNo.DataSource = ds.Tables[0];
            ddlDgNo.DataTextField = "descval";
            ddlDgNo.DataValueField = "id";
            ddlDgNo.DataBind();
        }

        private void getComplainUser()
        {
            string comcod = GetComCode();
            string complno = Request.QueryString["ComplNo"] ?? ddlComplain.SelectedValue.ToString();
            DataSet ds = _process.GetTransInfo(comcod, "SP_ENTRY_FACILITYMGT", "GETCOMPLAINUSER", complno, "", "", "", "", "", "", "", "", "", "");
            DataTable dt01 = ds.Tables[0];
            DataTable dt02 = ds.Tables[1];
            if(dt02!=null || dt02.Rows.Count > 0)
            {
                var row = dt02.Rows[0];
                lblProjectText.Text = row["pactdesc"].ToString();
                lblCustomerText.Text = row["custdesc"].ToString();
                lblUnitText.Text = row["unit"].ToString();
            }
            
            List<EClass_Complain_List> obj = dt01.DataTableToList<EClass_Complain_List>();
            ViewState["ComplainList"] = obj;
            Bind_Grid(obj);
            dgvUser.DataSource = dt01;
            dgvUser.DataBind();

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
            var obj = (List<EClass_Complain_List>)ViewState["ComplainList"];
            Bind_Grid(obj);
        }

        protected void btnOKClick_Click(object sender, EventArgs e)
        {
            getComplainUser();
        }

        protected void lnkSave_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComCode();
            string dgno = Request.QueryString["EngrNo"] ?? "0";
            string complno = Request.QueryString["ComplNo"] ?? ddlComplain.SelectedValue.ToString();
            string dgdate = txtEntryDate.Text;
            string sitevisiteddate = txtSiteVisisted.Text;
            string estimatedwddate = txtwdtime.Text;
            string userId = hst["usrid"].ToString();
            string addremarks = txtNarration.Text;


            DataSet ds = _process.GetTransInfoNew(comcod, "SP_ENTRY_FACILITYMGT", "UPSERTDIAGNOSISB", null, null, null, dgno, complno, dgdate, sitevisiteddate, estimatedwddate, addremarks, 
                "", "", "", "", "", "", "", "", "", "", "", "", "", userId);
            DataTable dt = ds.Tables[0];
            if (dt != null || dt.Rows.Count > 0)
            {
                int i = 1;
                bool resultDelete = _process.UpdateTransInfo3(comcod, "SP_ENTRY_FACILITYMGT", "DElETEDIAGNOSISA", dt.Rows[0]["dgno"].ToString(), "", "", "", "", "", "", "", "", "", "", "",
                            "", "", "", "", "", "", "", "", "", "", userId);
                if (resultDelete)
                {
                    List<EClass_Complain_List> list = (List<EClass_Complain_List>)ViewState["ComplainList"];
                    foreach (var item in list)
                    {
                        bool resultA = _process.UpdateTransInfo3(comcod, "SP_ENTRY_FACILITYMGT", "UPSERTDIAGNOSISA", dt.Rows[0]["dgno"].ToString(), item.complainDesc, item.remarks, i.ToString(), "", "", "", "", "", "", "", "",
                                "", "", "", "", "", "", "", "", "", "", userId);
                        if (!resultA)
                        {
                            //Failed
                        }
                        i++;
                    }
                    bool resultflag = _process.UpdateTransInfo3(comcod, "SP_ENTRY_FACILITYMGT", "UPDATEENGRFLAG", complno, "", "", "", "", "", "", "", "", "", "", "",
                               "", "", "", "", "", "", "", "", "", "", userId);
                    if (resultflag)
                    {
                        //Update Successful
                        if (dgno == "0")
                        {
                            ClearPage();
                        }
                    }
                        
                }
               
            }
            else
            {

            }



        }
    }
}