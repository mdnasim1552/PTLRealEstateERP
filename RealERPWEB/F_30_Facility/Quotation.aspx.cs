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
               
                if (Request.QueryString["EngrNo"] != null)
                {
                    EditFunctionality();
                }
                if (Request.QueryString["Type"] != null)
                {
                    pnlApproval.Visible = true;
                    txtEntryDate.Enabled = false;
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

            if (Request.QueryString["Type"] != null && Request.QueryString["Type"].ToString()=="Approval")
            { 
                DataTable dt = ds.Tables[0].Select("isbudget=1").CopyToDataTable();
                ddlDgNo.DataSource = dt;
            }
            else
            {
                DataTable dt = ds.Tables[0].Select("isbudget=0").CopyToDataTable();
                ddlDgNo.DataSource = dt;
            }
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
            }
            List<EClass_Complain_List> obj = dt01.DataTableToList<EClass_Complain_List>();
            ViewState["ComplainList"] = obj;
            Bind_Grid(obj);


        }

      

       

        private void Bind_Grid(List<EClass_Complain_List> obj)
        {
            ViewState["ComplainList"] = obj;
            gvQuotation.DataSource = obj;
            gvQuotation.DataBind();
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


        


        protected void lnkSave_Click(object sender, EventArgs e)
        {
            List<EClass_Material_List> obj = (List<EClass_Material_List>)ViewState["MaterialList"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComCode();
            string userId = hst["usrid"].ToString();
            string dgno = Request.QueryString["Dgno"] ?? ddlDgNo.SelectedValue.ToString();
            string bgddate = txtEntryDate.Text;
            int i = 1;
            foreach (var item in obj)
            {
                bool resultA = _process.UpdateTransInfo3(comcod, "SP_ENTRY_FACILITYMGT", "UPSERTBGD", dgno, item.materialId, item.unit, item.quantity.ToString(), item.amount.ToString(), 
                    bgddate, i.ToString(), "", "", "", "", "",
                         "", "", "", "", "", "", "", "", "", "", userId);
                i++;
            }
            bool resultflag = _process.UpdateTransInfo3(comcod, "SP_ENTRY_FACILITYMGT", "UPDATEBGDFLAG", dgno, "", "", "", "", "", "", "", "", "", "", "",
                              "", "", "", "", "", "", "", "", "", "", userId);

            if (Request.QueryString["Type"]!=null && Request.QueryString["Type"].ToString() == "Approval")
            {
                string notes = txtNarration.Text;
                bool resultR = _process.UpdateTransInfo3(comcod, "SP_ENTRY_FACILITYMGT", "UPSERTAPPROVAL", dgno, notes, "", "", "", "", "", "", "", "", "", "",
                        "", "", "", "", "", "", "", "", "", "", userId);
                
            }
        }
    }
}