using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_99_Allinterface
{
    public partial class RptFacilityInterface : System.Web.UI.Page
    {
        ProcessAccess _process = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtfrmdate.Text = System.DateTime.Now.AddDays(-30).ToString("dd-MMM-yyyy");
                txttoDate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
                ModuleName();
                getComplainList();
            }
        }
        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void ModuleName()
        {

            string comcod = GetComCode();
            string date1 = txtfrmdate.Text;
            string date2 = txttoDate.Text;
            DataSet ds = _process.GetTransInfo(comcod, "SP_INTERFACE_FACILITYMGT", "GETTOPNUMBER", date1, date2, "", "", "", "", "", "", "", "", "");
            if (ds != null || ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                this.RadioButtonList1.Items[0].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + dt.Rows[0][1].ToString() + "</div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'>Complains</div></div></div>";
                this.RadioButtonList1.Items[1].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + dt.Rows[0][2].ToString() + "</i></div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'>Engr. Check</div></div></div>";

                this.RadioButtonList1.Items[2].Text = "<div class='circle-tile'><a><div class='circle-tile-heading red counter'>" + dt.Rows[0][3].ToString() + "</i></div></a><div class='circle-tile-content red'><div class='circle-tile-description text-faded'>" + "Budget" + "</div></div></div>";
                this.RadioButtonList1.Items[3].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + dt.Rows[0][4].ToString() + "</i></div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'>Approval</div></div></div>";

                this.RadioButtonList1.Items[4].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + dt.Rows[0][5].ToString() + "</i></div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'>Quotation</div></div></div>";

                this.RadioButtonList1.Items[5].Text = "<div class='circle-tile'><a><div class='circle-tile-heading orange counter'>" + dt.Rows[0][6].ToString() + "</i></div></a><div class='circle-tile-content orange'><div class='circle-tile-description text-faded'>Material Req.</div></div></div>";

                this.RadioButtonList1.Items[6].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-gray counter'>" + dt.Rows[0][7].ToString() + "</i></div></a><div class='circle-tile-content dark-gray'><div class='circle-tile-description text-faded'>Status</div></div></div>";
                this.RadioButtonList1.Items[7].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue  counter'>" + dt.Rows[0][8].ToString() + "</i></div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'>QC Pending</div></div></div>";
                this.RadioButtonList1.Items[8].Text = "<div class='circle-tile'><a><div class='circle-tile-heading red counter'>" + dt.Rows[0][9].ToString() + "</i></div></a><div class='circle-tile-content red'><div class='circle-tile-description text-faded'>" + "Customer Care" + "</div></div></div>";
                this.RadioButtonList1.Items[9].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + dt.Rows[0][10].ToString() + "</i></div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'>" + "Work Done" + "</div></div></div>";

            }


        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string value = this.RadioButtonList1.SelectedValue.ToString();
            switch (value)
            {
                case "0":
                    pnlComplainCount.Visible = true;
                    pnlDiagnosis.Visible = false;
                    pnlBudget.Visible = false;
                    pnlApproval.Visible = false;
                    getComplainList();
                    break;
                case "1":
                    pnlComplainCount.Visible = false;
                    pnlDiagnosis.Visible = true;
                    pnlBudget.Visible = false;
                    pnlApproval.Visible = false;
                    getDiagnosisList();
                    break;
                case "2":
                    pnlComplainCount.Visible = false;
                    pnlDiagnosis.Visible = false;
                    pnlBudget.Visible = true;
                    pnlApproval.Visible = false;
                    getBudget();
                    break;
                case "3":
                    pnlComplainCount.Visible = false;
                    pnlDiagnosis.Visible = false;
                    pnlBudget.Visible = false;
                    pnlApproval.Visible = true;
                    getBudgetApproval();
                    break;
            }
        }

        private void getComplainList()
        {

            string comcod = GetComCode();
            string date1 = txtfrmdate.Text;
            string date2 = txttoDate.Text;
            DataSet ds = _process.GetTransInfo(comcod, "SP_INTERFACE_FACILITYMGT", "GETCOMPLAINLIST", date1, date2, "", "", "", "", "", "", "", "", "");
            gvComplainList.DataSource = ds.Tables[0];
            gvComplainList.DataBind();
        }
        private void getDiagnosisList()
        {
            string comcod = GetComCode();
            string date1 = txtfrmdate.Text;
            string date2 = txttoDate.Text;
            DataSet ds = _process.GetTransInfo(comcod, "SP_INTERFACE_FACILITYMGT", "GETDIAGNOSISLIST", date1, date2, "", "", "", "", "", "", "", "", "");
            gvDiagnosis.DataSource = ds.Tables[0];
            gvDiagnosis.DataBind();
        } 

        private void getBudget()
        {
            string comcod = GetComCode();
            string date1 = txtfrmdate.Text;
            string date2 = txttoDate.Text;
            DataSet ds = _process.GetTransInfo(comcod, "SP_INTERFACE_FACILITYMGT", "GETBUDGETLIST", date1, date2, "", "", "", "", "", "", "", "", "");
            gvBudget.DataSource = ds.Tables[0];
            gvBudget.DataBind();
        }
        private void getBudgetApproval()
        {
            string comcod = GetComCode();
            string date1 = txtfrmdate.Text;
            string date2 = txttoDate.Text;
            DataSet ds = _process.GetTransInfo(comcod, "SP_INTERFACE_FACILITYMGT", "GETAPPROVALBUDGET", date1, date2, "", "", "", "", "", "", "", "", "");
            gvApproval.DataSource = ds.Tables[0];
            gvApproval.DataBind();
        }


        protected void lnkEdit_Click(object sender, EventArgs e)
        {

        }

        protected void gvComplainList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink = (HyperLink)e.Row.FindControl("lnkedit");
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkdg");
                string complno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "complno")).ToString();
                hlink.NavigateUrl = "~/F_30_Facility/ComplainForm.aspx?ComplNo=" + complno;
                hlink.ToolTip = "Edit";
                hlink1.NavigateUrl = "~/F_30_Facility/EngrCheck.aspx?ComplNo=" + complno;
                hlink1.ToolTip = "Engr. Check";
            }

        }

        protected void gvDiagnosis_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink = (HyperLink)e.Row.FindControl("lnkedit");
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkdg");
                string dgno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "dgno")).ToString();
                hlink.NavigateUrl = "~/F_30_Facility/EngrCheck.aspx?Type=Edit&Dgno=" + dgno;
                hlink.ToolTip = "Edit";
                hlink1.NavigateUrl = "~/F_30_Facility/BudgetForm.aspx?DgNo=" + dgno;
                hlink1.ToolTip = "Generate Budget";
            }
        }

        
        protected void gvBudget_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink = (HyperLink)e.Row.FindControl("lnkedit");
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkdg");
                string dgno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "dgno")).ToString();
                hlink.NavigateUrl = "~/F_30_Facility/EngrCheck.aspx?Type=Edit&Dgno=" + dgno;
                hlink.ToolTip = "Edit";
                hlink1.NavigateUrl = "~/F_30_Facility/BudgetForm.aspx?Type=Approval&DgNo=" + dgno;
                hlink1.ToolTip = "Approval";
            }
        }

        protected void gvApproval_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }
}