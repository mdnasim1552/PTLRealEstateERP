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
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                if ((!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp),
                        (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean(hst["permission"]))
                    Response.Redirect("~/AcceessError");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Complaint Management Interface";//
                this.GetFromDate();
                //  txtfrmdate.Text = this.GetFromDate(); 
                this.txttoDate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
                ModuleName();
                getComplainList();
                txtRejectDesc.Text = "";
            }
        }


        private void GetFromDate()
        {

            string comcod = this.GetComCode();

            switch (comcod)
            {
                case "3101": //own 
                case "3367"://Epic
                case "3368"://Finlay

                    Hashtable hst = (Hashtable)Session["tblLogin"];
                    this.txtfrmdate.Text = Convert.ToDateTime(hst["opndate"].ToString()).AddDays(1).ToString("dd-MMM-yyyy");

                    break;

                default:
                    this.txtfrmdate.Text = System.DateTime.Now.AddDays(-30).ToString("dd-MMM-yyyy");
                    break;



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
            string item1, item2, item3, item4, item5, item6, item7, item8, item9, item10, item11;
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                item1 = dt.Rows[0][1].ToString();
                item2 = dt.Rows[0][2].ToString();
                item3 = dt.Rows[0][3].ToString();
                item4 = dt.Rows[0][4].ToString();
                item5 = dt.Rows[0][5].ToString();
                item6 = dt.Rows[0][6].ToString();
                item7 = dt.Rows[0][7].ToString();
                item8 = dt.Rows[0][8].ToString();
                item9 = dt.Rows[0][9].ToString();
                item10 = dt.Rows[0][10].ToString();
                item11= dt.Rows[0][11].ToString();
            }
            else
            {
                item1 = "0";
                item2 = "0";
                item3 = "0";
                item4 = "0";
                item5 = "0";
                item6 = "0";
                item7 = "0";
                item8 = "0";
                item9 = "0";
                item10 = "0";
                item11 = "0";
            }
            this.RadioButtonList1.Items[0].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + item1 + "</div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'>Complaints</div></div></div>";
            this.RadioButtonList1.Items[1].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + item2 + "</i></div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'>Engr. Check</div></div></div>";

            this.RadioButtonList1.Items[2].Text = "<div class='circle-tile'><a><div class='circle-tile-heading red counter'>" + item3 + "</i></div></a><div class='circle-tile-content red'><div class='circle-tile-description text-faded'>" + "Budget" + "</div></div></div>";
            this.RadioButtonList1.Items[3].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + item4 + "</i></div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'>Approval</div></div></div>";

            this.RadioButtonList1.Items[4].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + item5 + "</i></div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'>Quotation</div></div></div>";

            this.RadioButtonList1.Items[5].Text = "<div class='circle-tile'><a><div class='circle-tile-heading orange counter'>" + item6 + "</i></div></a><div class='circle-tile-content orange'><div class='circle-tile-description text-faded'>Material Req.</div></div></div>";

            this.RadioButtonList1.Items[6].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-gray counter'>" + item7 + "</i></div></a><div class='circle-tile-content dark-gray'><div class='circle-tile-description text-faded'>Status</div></div></div>";
            this.RadioButtonList1.Items[7].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue  counter'>" + item8 + "</i></div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'>QC Pending</div></div></div>";
            this.RadioButtonList1.Items[8].Text = "<div class='circle-tile'><a><div class='circle-tile-heading red counter'>" + item9 + "</i></div></a><div class='circle-tile-content red'><div class='circle-tile-description text-faded'>" + "Customer Care" + "</div></div></div>";
            this.RadioButtonList1.Items[9].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + item10 + "</i></div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'>" + "Work Done" + "</div></div></div>";
            this.RadioButtonList1.Items[10].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + item11 + "</i></div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'>" + "Reject" + "</div></div></div>";






        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string value = this.RadioButtonList1.SelectedValue.ToString();
            switch (value)
            {
                case "0":
                    pnlComplainCount.Visible = true;
                    pnlComplainToDiagnosis.Visible = false;
                    pnlDiagnosis.Visible = false;
                    pnlBudget.Visible = false;
                    pnlApproval.Visible = false;
                    pnlMatReq.Visible = false;
                    pnlStatus.Visible = false;
                    pnlQC.Visible = false;
                    pnlCustomerCare.Visible = false;
                    pnlWorkDone.Visible = false;
                    pnlReject.Visible = false;
                    getComplainList();
                    break;
                case "1":
                    pnlComplainCount.Visible = false;
                    pnlComplainToDiagnosis.Visible = true;
                    pnlDiagnosis.Visible = false;
                    pnlBudget.Visible = false;
                    pnlApproval.Visible = false;
                    pnlMatReq.Visible = false;
                    pnlStatus.Visible = false;
                    pnlQC.Visible = false;
                    pnlCustomerCare.Visible = false;
                    pnlWorkDone.Visible = false;
                    pnlReject.Visible = false;
                    getDiagnosisList();
                    break;
                case "2":
                    pnlComplainCount.Visible = false;
                    pnlComplainToDiagnosis.Visible = false;
                    pnlDiagnosis.Visible = true;
                    pnlBudget.Visible = false;
                    pnlApproval.Visible = false;
                    pnlMatReq.Visible = false;
                    pnlStatus.Visible = false;
                    pnlQC.Visible = false;
                    pnlCustomerCare.Visible = false;
                    pnlWorkDone.Visible = false;
                    pnlReject.Visible = false;
                    getBudget();
                    break;
                case "3":
                    pnlComplainCount.Visible = false;
                    pnlComplainToDiagnosis.Visible = false;
                    pnlDiagnosis.Visible = false;
                    pnlBudget.Visible = true;
                    pnlApproval.Visible = false;
                    pnlMatReq.Visible = false;
                    pnlStatus.Visible = false;
                    pnlQC.Visible = false;
                    pnlCustomerCare.Visible = false;
                    pnlWorkDone.Visible = false;
                    pnlReject.Visible = false;
                    getBudgetApproval();
                    break;
                case "4":
                    pnlComplainCount.Visible = false;
                    pnlComplainToDiagnosis.Visible = false;
                    pnlDiagnosis.Visible = false;
                    pnlBudget.Visible = false;
                    pnlApproval.Visible = true;
                    pnlMatReq.Visible = false;
                    pnlStatus.Visible = false;
                    pnlQC.Visible = false;
                    pnlCustomerCare.Visible = false;
                    pnlWorkDone.Visible = false;
                    pnlReject.Visible = false;
                    getQuotList();
                    break;
                case "5":
                    pnlComplainCount.Visible = false;
                    pnlComplainToDiagnosis.Visible = false;
                    pnlDiagnosis.Visible = false;
                    pnlBudget.Visible = false;
                    pnlApproval.Visible = false;
                    pnlMatReq.Visible = true;
                    pnlStatus.Visible = false;
                    pnlQC.Visible = false;
                    pnlCustomerCare.Visible = false;
                    pnlWorkDone.Visible = false;
                    pnlReject.Visible = false;
                    getMATREQ();
                    break;
                case "6":
                    pnlComplainCount.Visible = false;
                    pnlComplainToDiagnosis.Visible = false;
                    pnlDiagnosis.Visible = false;
                    pnlBudget.Visible = false;
                    pnlApproval.Visible = false;
                    pnlMatReq.Visible = false;
                    pnlStatus.Visible = true;
                    pnlQC.Visible = false;
                    pnlCustomerCare.Visible = false;
                    pnlWorkDone.Visible = false;
                    pnlReject.Visible = false;
                    getStatus();
                    break;
                case "7":
                    pnlComplainCount.Visible = false;
                    pnlComplainToDiagnosis.Visible = false;
                    pnlDiagnosis.Visible = false;
                    pnlBudget.Visible = false;
                    pnlApproval.Visible = false;
                    pnlMatReq.Visible = false;
                    pnlStatus.Visible = false;
                    pnlQC.Visible = true;
                    pnlCustomerCare.Visible = false;
                    pnlWorkDone.Visible = false;
                    pnlReject.Visible = false;
                    getQC();
                    break;
                case "8":
                    pnlComplainCount.Visible = false;
                    pnlComplainToDiagnosis.Visible = false;
                    pnlDiagnosis.Visible = false;
                    pnlBudget.Visible = false;
                    pnlApproval.Visible = false;
                    pnlMatReq.Visible = false;
                    pnlStatus.Visible = false;
                    pnlQC.Visible = false;
                    pnlCustomerCare.Visible = true;
                    pnlWorkDone.Visible = false;
                    pnlReject.Visible = false;
                    getCustomerCare();
                    break;
                case "9":
                    pnlComplainCount.Visible = false;
                    pnlComplainToDiagnosis.Visible = false;
                    pnlDiagnosis.Visible = false;
                    pnlBudget.Visible = false;
                    pnlApproval.Visible = false;
                    pnlMatReq.Visible = false;
                    pnlStatus.Visible = false;
                    pnlQC.Visible = false;
                    pnlCustomerCare.Visible = false;
                    pnlWorkDone.Visible = true;
                    pnlReject.Visible = false;
                    getWorkDone();
                    break;
                case "10":
                    pnlComplainCount.Visible = false;
                    pnlComplainToDiagnosis.Visible = false;
                    pnlDiagnosis.Visible = false;
                    pnlBudget.Visible = false;
                    pnlApproval.Visible = false;
                    pnlMatReq.Visible = false;
                    pnlStatus.Visible = false;
                    pnlQC.Visible = false;
                    pnlCustomerCare.Visible = false;
                    pnlWorkDone.Visible = false;
                    pnlReject.Visible = true;
                    GetReject();
                    break;
            }
        }

        private void getStatus()
        {
            string comcod = GetComCode();
            string date1 = txtfrmdate.Text;
            string date2 = txttoDate.Text;
            DataSet ds = _process.GetTransInfo(comcod, "SP_INTERFACE_FACILITYMGT", "GETSTATUSLIST", date1, date2, "", "", "", "", "", "", "", "", "");
            gvStatus.DataSource = ds.Tables[0];
            gvStatus.DataBind();
        }
        private void getWorkDone()
        {
            string comcod = GetComCode();
            string date1 = txtfrmdate.Text;
            string date2 = txttoDate.Text;
            DataSet ds = _process.GetTransInfo(comcod, "SP_INTERFACE_FACILITYMGT", "GETWORKDONE", date1, date2, "", "", "", "", "", "", "", "", "");
            gvWO.DataSource = ds.Tables[0];
            gvWO.DataBind();
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
            gvCmpltoDg.DataSource = ds.Tables[0];
            gvCmpltoDg.DataBind();
        }

        private void getBudget()
        {
            string comcod = GetComCode();
            string date1 = txtfrmdate.Text;
            string date2 = txttoDate.Text;
            DataSet ds = _process.GetTransInfo(comcod, "SP_INTERFACE_FACILITYMGT", "GETBUDGETLIST", date1, date2, "", "", "", "", "", "", "", "", "");
            gvDiagnosis.DataSource = ds.Tables[0];
            gvDiagnosis.DataBind();
        }
        private void getBudgetApproval()
        {
            string comcod = GetComCode();
            string date1 = txtfrmdate.Text;
            string date2 = txttoDate.Text;
            DataSet ds = _process.GetTransInfo(comcod, "SP_INTERFACE_FACILITYMGT", "GETAPPROVALBUDGET", date1, date2, "", "", "", "", "", "", "", "", "");
            gvBudget.DataSource = ds.Tables[0];
            gvBudget.DataBind();
        }
        private void getQuotList()
        {
            string comcod = GetComCode();
            string date1 = txtfrmdate.Text;
            string date2 = txttoDate.Text;
            DataSet ds = _process.GetTransInfo(comcod, "SP_INTERFACE_FACILITYMGT", "GETQUOTLIST", date1, date2, "", "", "", "", "", "", "", "", "");
            gvApproval.DataSource = ds.Tables[0];
            gvApproval.DataBind();
        }
        private void getMATREQ()
        {
            string comcod = GetComCode();
            string date1 = txtfrmdate.Text;
            string date2 = txttoDate.Text;
            DataSet ds = _process.GetTransInfo(comcod, "SP_INTERFACE_FACILITYMGT", "GETMATREQLIST", date1, date2, "", "", "", "", "", "", "", "", "");
            gvMatReq.DataSource = ds.Tables[0];
            gvMatReq.DataBind();
        }
        private void getQC()
        {
            string comcod = GetComCode();
            string date1 = txtfrmdate.Text;
            string date2 = txttoDate.Text;
            DataSet ds = _process.GetTransInfo(comcod, "SP_INTERFACE_FACILITYMGT", "GETQCLIST", date1, date2, "", "", "", "", "", "", "", "", "");
            gvQC.DataSource = ds.Tables[0];
            gvQC.DataBind();
        }

        private void getCustomerCare()
        {
            string comcod = GetComCode();
            string date1 = txtfrmdate.Text;
            string date2 = txttoDate.Text;
            DataSet ds = _process.GetTransInfo(comcod, "SP_INTERFACE_FACILITYMGT", "GETCUSTOMERCARE", date1, date2, "", "", "", "", "", "", "", "", "");
            gvCustomerCare.DataSource = ds.Tables[0];
            gvCustomerCare.DataBind();
        }


        private void GetReject()
        {
            string comcod = GetComCode();
            string date1 = txtfrmdate.Text;
            string date2 = txttoDate.Text;
            DataSet ds = _process.GetTransInfo(comcod, "SP_INTERFACE_FACILITYMGT", "GETREJECTLIST", date1, date2, "", "", "", "", "", "", "", "", "");
            gvReject.DataSource = ds.Tables[0];
            gvReject.DataBind();
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {

        }

        protected void gvComplainList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink = (HyperLink)e.Row.FindControl("lnkedit");
                LinkButton llink = (LinkButton)e.Row.FindControl("lnkProceed");

                string complno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "complno")).ToString();
                bool status = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "engrcheck"));
                if (!status)
                {
                    hlink.NavigateUrl = "~/F_30_Facility/ComplainForm.aspx?ComplNo=" + complno;
                    hlink.ToolTip = "Edit";
                    llink.Visible = true;
                    hlink.Visible = true;
                }
                else
                {
                    hlink.Visible = false;
                    llink.Visible = false;
                }
                llink.Attributes["onclick"] = "if(!confirm('Do you want to Proceed: C-" + complno + " to Engineer Check? ')){ return false; };";
            }

        }
        protected void gvComplainList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Select")
                {
                    int rowIndex = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = gvComplainList.Rows[rowIndex];
                    string complno = (row.FindControl("lblComplno") as Label).Text;
                    Hashtable hst = (Hashtable)Session["tblLogin"];
                    string comcod = GetComCode();
                    string userId = hst["usrid"].ToString();
                    bool resultflag = _process.UpdateTransInfo3(comcod, "SP_ENTRY_FACILITYMGT", "UPDATEENGRFLAG", complno, "", "", "", "", "", "", "", "", "", "", "",
                                             "", "", "", "", "", "", "", "", "", "", userId);
                    if (resultflag)
                    {

                        ModuleName();
                        getComplainList();
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + $"C-{complno} proceeded to Engr. Check" + "');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured" + "');", true);
                    }

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{ex.Message.ToString()}" + "');", true);
            }

        }

        protected void gvDiagnosis_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink = (HyperLink)e.Row.FindControl("lnkedit");
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkdg");
                LinkButton llink = (LinkButton)e.Row.FindControl("lnkProceed");
                string dgno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "dgno")).ToString();
                bool isApproval = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "isApproval"));
                bool isbudget = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "isbudgetVal"));
                hlink.NavigateUrl = "~/F_30_Facility/BudgetForm.aspx?Type=Edit&Dgno=" + dgno;
                hlink.ToolTip = "Edit";
                hlink1.NavigateUrl = "~/F_30_Facility/BudgetForm.aspx?DgNo=" + dgno;
                hlink1.ToolTip = "Generate Budget";
                llink.Attributes["onclick"] = "if(!confirm('Do you want to Proceed: Dg-" + dgno + " to Approval? ')){ return false; };";
                if (!isApproval && isbudget == false)
                {
                    hlink.Visible = false;
                    llink.Visible = false;
                    hlink1.Visible = true;
                }
                else if (isApproval == true && isbudget == true)
                {
                    hlink.Visible = false;
                    llink.Visible = false;
                    hlink1.Visible = false;
                }
                else
                {
                    hlink.Visible = true;
                    llink.Visible = true;
                    hlink1.Visible = false;
                }
            }
        }


        protected void gvBudget_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkdg");
                string dgno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "dgno")).ToString();
                hlink1.NavigateUrl = "~/F_30_Facility/BudgetForm.aspx?Type=Approval&DgNo=" + dgno;
                hlink1.ToolTip = "Approval";
            }
        }

        protected void gvApproval_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink = (HyperLink)e.Row.FindControl("hnkCollection");
                LinkButton llink = (LinkButton)e.Row.FindControl("lnkProceed");
                LinkButton llink01 = (LinkButton)e.Row.FindControl("lnkMatReq");
                LinkButton llink02 = (LinkButton)e.Row.FindControl("lnkReject");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lblDgNo1");
                string dgno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "dgno")).ToString();
                string isMatReq = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "isMatReq")).ToString();
                bool isquoted = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "isquoted"));
                hlink2.NavigateUrl = "~/F_30_Facility/Quotation.aspx?DgNo=" + dgno;
                if (!Convert.ToBoolean(isMatReq))
                {
                    llink01.ToolTip = "Proceed to Material Req.";
                    llink01.Attributes["onclick"] = "if(!confirm('Do you want to Proceed: Dg-" + dgno + " to Material Req.? ')){ return false; };";
                }
                else
                {
                    llink01.Visible = false;
                }
                if (isquoted)
                {
                    llink.Visible = false;
                    llink02.Visible = false;
                }
                else
                {
                    llink.Attributes["onclick"] = "if(!confirm('Do you want to Accept Quotation of: Dg-" + dgno + "? ')){ return false; };";
                    llink.ToolTip = "Accept Quotation";
                    llink02.ToolTip = "Reject Quotation";
                    llink01.Visible = false;
                    llink02.Visible = true;
                }



            }
        }

        protected void gvCmpltoDg_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink = (HyperLink)e.Row.FindControl("lnkedit");
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkdg");
                LinkButton llink = (LinkButton)e.Row.FindControl("lnkProceed");
                LinkButton llink1 = (LinkButton)e.Row.FindControl("lnkReject");
                string complno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "complno")).ToString();
                string dgno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "dgno")).ToString();
                bool lblisBudget = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "isbudget"));
                hlink1.NavigateUrl = "~/F_30_Facility/EngrCheck.aspx?ComplNo=" + complno;
                hlink1.ToolTip = "Engr. Check";
                if (dgno == "0" && lblisBudget == true)
                {
                    hlink.Visible = false;
                    llink.Visible = false;
                    hlink1.Visible = true;                    
                }
                else if (lblisBudget == false && dgno != "0")
                {
                    hlink1.Visible = false;
                    hlink.NavigateUrl = "~/F_30_Facility/EngrCheck.aspx?Type=Edit&ComplNo=" + complno + "&Dgno=" + dgno;
                    hlink.ToolTip = "Edit";
                    llink.Attributes["onclick"] = "if(!confirm('Do you want to Proceed: Dg-" + dgno + " to Budget? ')){ return false; };";
                    hlink.Visible = true;
                    llink.Visible = true;
                   

                }
                else
                {
                    hlink.Visible = false;
                    llink.Visible = false;
                    hlink1.Visible = false;

                }
            }
        }
        protected void gvCmpltoDg_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvCmpltoDg.Rows[rowIndex];
                string dgno = (row.FindControl("lbldgno") as Label).Text;
                string complain = (row.FindControl("lblComplain") as Label).Text;
                if (e.CommandName == "Select")
                {
                    Hashtable hst = (Hashtable)Session["tblLogin"];
                    string comcod = GetComCode();
                    string userId = hst["usrid"].ToString();
                    bool resultflag = _process.UpdateTransInfo3(comcod, "SP_ENTRY_FACILITYMGT", "UPDATEBGDFLAG", dgno, "", "", "", "", "", "", "", "", "", "", "",
                                             "", "", "", "", "", "", "", "", "", "", userId);
                    if (resultflag)
                    {
                        ModuleName();
                        getDiagnosisList();
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + $"Dg-{dgno} proceeded to Budget" + "');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured" + "');", true);
                    }
                }
                if (e.CommandName == "Reject")
                {
                    lblComplainReject.Text = complain;
                    lblDgNoReject.Text = dgno;
                    lblProcess.Text = dgno == "0" ? "R":"R1";
                    txtRejectDesc.Text = "";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModalReject();", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{ex.Message.ToString()}" + "');", true);
            }

        }
        protected void gvMatReq_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //LinkButton llink = (LinkButton)e.Row.FindControl("lnkProceed");
                HyperLink hlink = (HyperLink)e.Row.FindControl("lnkdg");
                string dgno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "dgno")).ToString();

                hlink.NavigateUrl = "~/F_30_Facility/ComplaintMatReq.aspx?DgNo=" + dgno;
                //llink.Attributes["onclick"] = "if(!confirm('Do you want to Accept Quotation of: Dg-" + dgno + "? ')){ return false; };";
                //llink.ToolTip = "Accept Quotation";
            }
        }

        protected void lnkbtnok_Click(object sender, EventArgs e)
        {
            ModuleName();
            pnlComplainCount.Visible = true;
            pnlComplainToDiagnosis.Visible = false;
            pnlDiagnosis.Visible = false;
            pnlBudget.Visible = false;
            pnlApproval.Visible = false;
            pnlMatReq.Visible = false;
            getComplainList();
            RadioButtonList1_SelectedIndexChanged(null, null);
        }

        protected void gvDiagnosis_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvDiagnosis.Rows[rowIndex];
                string dgno = (row.FindControl("lbldgno") as Label).Text;
                string complain = (row.FindControl("lblComplain") as Label).Text;
                if (e.CommandName == "Select")
                {
                    
                    Hashtable hst = (Hashtable)Session["tblLogin"];
                    string comcod = GetComCode();
                    string userId = hst["usrid"].ToString();
                    bool resultflag = _process.UpdateTransInfo3(comcod, "SP_ENTRY_FACILITYMGT", "UPDATEAPPRBGDFLAG", dgno, "", "", "", "", "", "", "", "", "", "", "",
                                             "", "", "", "", "", "", "", "", "", "", userId);
                    if (resultflag)
                    {
                        ModuleName();
                        getBudget();
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + $"Dg-{dgno} proceeded to Approval" + "');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured" + "');", true);
                    }
                }
                if (e.CommandName == "Reject")
                {
                    lblComplainReject.Text = complain;
                    lblDgNoReject.Text = dgno;
                    lblProcess.Text = "R2";
                    txtRejectDesc.Text = "";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModalReject();", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{ex.Message.ToString()}" + "');", true);
            }
        }
        protected void gvBudget_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvBudget.Rows[rowIndex];
            string dgno = (row.FindControl("lbldgno") as Label).Text;
            string complain = (row.FindControl("lblComplain") as Label).Text;
            if (e.CommandName == "Reject")
            {
                lblComplainReject.Text = complain;
                lblDgNoReject.Text = dgno;
                lblProcess.Text = "R3";
                txtRejectDesc.Text = "";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModalReject();", true);
            }
        }
        protected void gvApproval_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Select")
                {
                    int rowIndex = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = gvApproval.Rows[rowIndex];
                    string dgno = (row.FindControl("lbldgno") as Label).Text;
                    Hashtable hst = (Hashtable)Session["tblLogin"];
                    string comcod = GetComCode();
                    string userId = hst["usrid"].ToString();
                    bool resultflag = _process.UpdateTransInfo3(comcod, "SP_ENTRY_FACILITYMGT", "UPDATEQUOTAPPRFLAG", dgno, "", "", "", "", "", "", "", "", "", "", "",
                                             "", "", "", "", "", "", "", "", "", "", userId);
                    if (resultflag)
                    {
                        ModuleName();
                        getQuotList();
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + $"Dg-{dgno} proceeded to Approval" + "');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured" + "');", true);
                    }
                }
                else if (e.CommandName == "Proceed")
                {
                    int rowIndex = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = gvApproval.Rows[rowIndex];
                    string dgno = (row.FindControl("lbldgno") as Label).Text;
                    Hashtable hst = (Hashtable)Session["tblLogin"];
                    string comcod = GetComCode();
                    string userId = hst["usrid"].ToString();
                    bool resultflag = _process.UpdateTransInfo3(comcod, "SP_ENTRY_FACILITYMGT", "UPDATEMATREQFLAG", dgno, "", "", "", "", "", "", "", "", "", "", "",
                                             "", "", "", "", "", "", "", "", "", "", userId);
                    if (resultflag)
                    {
                        ModuleName();
                        getQuotList();
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + $"Dg-{dgno} proceeded to Material Req." + "');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured" + "');", true);
                    }
                }
                else if (e.CommandName == "Reject")
                {
                    int rowIndex = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = gvApproval.Rows[rowIndex];
                    string dgno = (row.FindControl("lbldgno") as Label).Text;
                    string complain = (row.FindControl("lblComplain") as Label).Text;
                    lblComplainReject.Text = complain;
                    lblDgNoReject.Text = dgno;
                    lblProcess.Text = "R4";
                    txtRejectDesc.Text = "";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModalReject();", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{ex.Message.ToString()}" + "');", true);
            }
        }

        protected void gvQC_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink = (HyperLink)e.Row.FindControl("lnkdg");
                string dgno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "dgno")).ToString();

                hlink.NavigateUrl = "~/F_30_Facility/ComplainQC.aspx?Type=QC&DgNo=" + dgno;

            }
        }
        protected void gvCustomerCare_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink = (HyperLink)e.Row.FindControl("lnkdg");
                string dgno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "dgno")).ToString();
                hlink.NavigateUrl = "~/F_30_Facility/ComplainQC.aspx?Type=CustomerCare&DgNo=" + dgno;
            }
        }

        protected void gvStatus_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton llink = (LinkButton)e.Row.FindControl("lnkStatusProceed");
                string complno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "complno")).ToString();
                string dgno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "dgno")).ToString();
                string lblisQC =DataBinder.Eval(e.Row.DataItem, "isQC").ToString();
                if (lblisQC=="1")
                {
                    llink.Visible = true;
                }
                else
                {
                    llink.Visible = false;
                }
            }
        }

        protected void gvStatus_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Proceed")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvStatus.Rows[rowIndex];
                string dgno = (row.FindControl("lbldgno") as Label).Text;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = GetComCode();
                string userId = hst["usrid"].ToString();
                bool resultflag = _process.UpdateTransInfo3(comcod, "SP_ENTRY_FACILITYMGT", "UPDATEQCFLAG", dgno, "", "", "", "", "", "", "", "", "", "", "",
                                         "", "", "", "", "", "", "", "", "", "", userId);
                if (resultflag)
                {
                    ModuleName();
                    getStatus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + $"Dg-{dgno} proceeded to QC Pending" + "');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured" + "');", true);
                }
            }
        }

        protected void lnkUpdateReject_Click(object sender, EventArgs e)
        {
            string comcod = GetComCode();
            string dgno = lblDgNoReject.Text;
            string compno = lblComplainReject.Text;
            string rejectdesc = txtRejectDesc.Text;
            string type = lblProcess.Text;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userId = hst["usrid"].ToString();
            bool resultflag = false;
            if (dgno == "0")
            {
                resultflag = _process.UpdateTransInfo3(comcod, "SP_ENTRY_FACILITYMGT", "UPDATEREJECTCOMP", compno, type, rejectdesc, userId, "", "", "", "", "", "", "", "",
                                        "", "", "", "", "", "", "", "", "", "", "");
            }
            else
            {
                if (type == "R4")
                {
                    resultflag = _process.UpdateTransInfo3(comcod, "SP_ENTRY_FACILITYMGT", "UPDATEREJECTBGDAPPR", dgno, type, rejectdesc, userId, compno, "", "", "", "", "", "", "",
                                        "", "", "", "", "", "", "", "", "", "", "");
                }
                else
                {
                    resultflag = _process.UpdateTransInfo3(comcod, "SP_ENTRY_FACILITYMGT", "UPDATEREJECTDG", dgno, type, rejectdesc, userId, compno, "", "", "", "", "", "", "",
                                        "", "", "", "", "", "", "", "", "", "", "");
                }
                
            }
            if (resultflag)
            {
                ModuleName();
                if (type == "R1" || type == "R")
                {
                    getDiagnosisList();
                }
                if (type == "R2")
                {
                    getBudget();
                }
                if (type == "R3")
                {
                    getBudgetApproval();
                }
                if (type == "R4")
                {
                    getQuotList();
                }
                if (dgno == "0")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + $"Compl-{compno} rejected." + "');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + $"Dg-{dgno} rejected." + "');", true);
                }
                txtRejectDesc.Text = "";
                
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured" + "');", true);
            }
        }

        
    }
}