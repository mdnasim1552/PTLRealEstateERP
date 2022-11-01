﻿using Microsoft.Reporting.WinForms;
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
    public partial class AIinterface : System.Web.UI.Page
    {
        SendNotifyForUsers UserNotify = new SendNotifyForUsers();

        ProcessAccess AIData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //if (dr1.Length == 0)
                //    Response.Redirect("../AcceessError.aspx");
                //((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = "AI Interface";

                DateTime now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);
                this.txtfrmdate.Text = Convert.ToDateTime(startDate).ToString("dd-MMM-yyyy");
                this.txttodate.Text = Convert.ToDateTime(endDate).ToString("dd-MMM-yyyy");

                //this.GetEmplist();

                //Hashtable hst = (Hashtable)Session["tblLogin"];
                //string comcod = this.GetCompCode();


                ////this.getAllData();
                this.GetAIInterface();
                this.TasktState.SelectedIndex = 0;
                this.TasktState_SelectedIndexChanged(null, null);
                this.GetEmployeeName();
                this.GetAnnotationList();
                this.GetProjectInformation();
                this.CreateTableAssign();



            }
        }
        //protected void Page_PreInit(object sender, EventArgs e)
        //{
        //    // Create an event handler for the master page's contentCallEvent event
        //    GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
        //    int index = row.RowIndex;
        //    //((LinkButton)this.gv_Invoice.FindControl("btninvoivePrint")).Click += new EventHandler(InvoicePrint_Click);


        //    //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        //}
        private void GetEmplist()
        {
            string comcod = this.GetCompCode();
            string txtEmpname = "%%";
            string type = "";
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string empid = hst["empid"].ToString() ?? "";
            DataSet ds1 = AIData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLNEMPLIST", txtEmpname, type, "", "", "", "", "", "", "");

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void GetAIInterface()
        {
            string comcod = this.GetCompCode();
            DataSet ds = AIData.GetTransInfo(comcod, "dbo_ai.SP_INTERFACE_AI", "GETINTERFACE", "", "", "", "", "", "");
            if (ds == null)
                return;

            this.TasktState.Items[0].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + Convert.ToDouble(ds.Tables[3].Rows[0]["prj"]).ToString("#,##0;(#,##0); ") + "</div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'>All Projects</div></div></div>";
            this.TasktState.Items[1].Text = "<div class='circle-tile'><a><div class='circle-tile-heading orange counter'>" + Convert.ToDouble(ds.Tables[3].Rows[0]["batch"]).ToString("#,##0;(#,##0); ") + "</i></div></a><div class='circle-tile-content orange'><div class='circle-tile-description text-faded'>Batch Status</div></div></div>";

            this.TasktState.Items[2].Text = "<div class='circle-tile'><a><div class='circle-tile-heading red counter'>" + Convert.ToDouble(ds.Tables[3].Rows[0]["assing"]).ToString("#,##0;(#,##0); ") + "</i></div></a><div class='circle-tile-content red'><div class='circle-tile-description text-faded'>Assigned</div></div></div>";

            this.TasktState.Items[3].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + Convert.ToDouble(ds.Tables[3].Rows[0]["production"]).ToString("#,##0;(#,##0); ") + "</i></div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'>Production</div></div></div>";

            this.TasktState.Items[4].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + Convert.ToDouble(ds.Tables[3].Rows[0]["qc"]).ToString("#,##0;(#,##0); ") + "</i></div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'>QC</div></div></div>"; //2nd App.

            this.TasktState.Items[5].Text = "<div class='circle-tile'><a><div class='circle-tile-heading orange counter'>" + Convert.ToDouble(ds.Tables[3].Rows[0]["accpt"]).ToString("#,##0;(#,##0); ") + "</i></div></a><div class='circle-tile-content orange'><div class='circle-tile-description text-faded'>Accept/Reject</div></div></div>";

            this.TasktState.Items[6].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-gray counter'>" + Convert.ToDouble(ds.Tables[3].Rows[0]["qa"]).ToString("#,##0;(#,##0); ") + "</i></div></a><div class='circle-tile-content dark-gray'><div class='circle-tile-description text-faded'>QA</div></div></div>";
            this.TasktState.Items[7].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue  counter'>" + Convert.ToDouble(ds.Tables[3].Rows[0]["delivery"]).ToString("#,##0;(#,##0); ") + "</i></div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'>Delivery</div></div></div>";
            this.TasktState.Items[8].Text = "<div class='circle-tile'><a><div class='circle-tile-heading red counter'>" + Convert.ToDouble(ds.Tables[3].Rows[0]["feeback"]).ToString("#,##0;(#,##0); ") + "</i></div></a><div class='circle-tile-content red'><div class='circle-tile-description text-faded'>Feedback</div></div></div>";
            this.TasktState.Items[9].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + Convert.ToDouble(ds.Tables[3].Rows[0]["invoice"]).ToString("#,##0;(#,##0); ") + "</i></div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'>Invoice</div></div></div>";
            this.TasktState.Items[10].Text = "<div class='circle-tile'><a><div class='circle-tile-heading orange counter'>" + Convert.ToDouble(ds.Tables[3].Rows[0]["collct"]).ToString("#,##0;(#,##0); ") + "</i></div></a><div class='circle-tile-content orange'><div class='circle-tile-description text-faded'>Collection</div></div></div>";





            Session["tblprojectlist"] = ds.Tables[0];
            Session["tblassinglist"] = ds.Tables[2];


            this.data_Bind();
        }
        private void StatusCount()
        {
            DataTable tbl1 = (DataTable)Session["tblstatuscount"];


        }
        private void data_Bind()
        {
            DataTable tbl1 = (DataTable)Session["tblprojectlist"];
            DataTable tblasing = (DataTable)Session["tblassinglist"];

            this.gvInterface.DataSource = tbl1;


            this.gvInterface.DataBind();

            this.gvAssingJob.DataSource = tblasing;
            this.gvAssingJob.DataBind();

        }

        protected void TasktState_SelectedIndexChanged(object sender, EventArgs e)
        {

            string value = this.TasktState.SelectedValue.ToString();
            switch (value)
            {
                case "0":
                    this.pnlAllProject.Visible = true;
                    this.pnlStatus.Visible = false;
                    this.pnlAssign.Visible = false;
                    this.pnlProduction.Visible = false;
                    this.pnelQC.Visible = false;
                    this.pnelAReject.Visible = false;
                    this.penlInvoice.Visible = false;
                    this.pnelCollection.Visible = false;
                    this.pnelQA.Visible = false;
                    this.pnelFeedBack.Visible = false;
                    this.Pneldelivery.Visible = false;
                    this.GetAIInterface();
                    break;
                case "1":
                    this.pnlAllProject.Visible = false;
                    this.pnlStatus.Visible = true;
                    this.pnlAssign.Visible = false;
                    this.pnlProduction.Visible = false;
                    this.pnelQC.Visible = false;
                    this.pnelAReject.Visible = false;
                    this.penlInvoice.Visible = false;
                    this.pnelCollection.Visible = false;
                    this.pnelQA.Visible = false;
                    this.pnelFeedBack.Visible = false;
                    this.Pneldelivery.Visible = false;
                    this.GetBatchAssingList();
                    break;
                case "2":
                    this.pnlAllProject.Visible = false;
                    this.pnlStatus.Visible = false;
                    this.pnlAssign.Visible = true;
                    this.pnlProduction.Visible = false;
                    this.pnelQC.Visible = false;
                    this.pnelAReject.Visible = false;
                    this.penlInvoice.Visible = false;
                    this.pnelCollection.Visible = false;
                    this.pnelQA.Visible = false;
                    this.pnelFeedBack.Visible = false;
                    this.Pneldelivery.Visible = false;
                    break;
                case "3":
                    this.pnlAllProject.Visible = false;
                    this.pnlStatus.Visible = false;
                    this.pnlAssign.Visible = false;
                    this.pnlProduction.Visible = true;
                    this.pnelQC.Visible = false;
                    this.pnelAReject.Visible = false;
                    this.penlInvoice.Visible = false;
                    this.pnelCollection.Visible = false;
                    this.pnelQA.Visible = false;
                    this.pnelFeedBack.Visible = false;
                    this.Pneldelivery.Visible = false;
                    this.GetProductionInfo();
                    break;
                case "4":
                    this.pnlAllProject.Visible = false;
                    this.pnlStatus.Visible = false;
                    this.pnlAssign.Visible = false;
                    this.pnlProduction.Visible = false;
                    this.pnelQC.Visible = true;
                    this.pnelAReject.Visible = false;
                    this.penlInvoice.Visible = false;
                    this.pnelCollection.Visible = false;
                    this.pnelQA.Visible = false;
                    this.pnelFeedBack.Visible = false;
                    this.Pneldelivery.Visible = false;
                    this.GetProductionInfo();
                    break;
                case "5":
                    this.pnlAllProject.Visible = false;
                    this.pnlStatus.Visible = false;
                    this.pnlAssign.Visible = false;
                    this.pnlProduction.Visible = false;
                    this.pnelQC.Visible = false;
                    this.pnelAReject.Visible = true;
                    this.penlInvoice.Visible = false;
                    this.pnelCollection.Visible = false;
                    this.pnelQA.Visible = false;
                    this.pnelFeedBack.Visible = false;
                    this.Pneldelivery.Visible = false;
                    this.GetProductionInfo();

                    break;
                case "6":
                    this.pnlAllProject.Visible = false;
                    this.pnlStatus.Visible = false;
                    this.pnlAssign.Visible = false;
                    this.pnlProduction.Visible = false;
                    this.pnelQC.Visible = false;
                    this.pnelAReject.Visible = false;
                    this.penlInvoice.Visible = false;
                    this.pnelCollection.Visible = false;
                    this.pnelQA.Visible = true;
                    this.pnelFeedBack.Visible = false;
                    this.Pneldelivery.Visible = false;
                    this.GetProductionInfo();
                    break;
                case "8":
                    this.pnlAllProject.Visible = false;
                    this.pnlStatus.Visible = false;
                    this.pnlAssign.Visible = false;
                    this.pnlProduction.Visible = false;
                    this.pnelQC.Visible = false;
                    this.pnelAReject.Visible = false;
                    this.penlInvoice.Visible = false;
                    this.pnelCollection.Visible = false;
                    this.pnelQA.Visible = false;
                    this.pnelFeedBack.Visible = true;
                    this.Pneldelivery.Visible = false;
                    break;
                case "7":
                    this.pnlAllProject.Visible = false;
                    this.pnlStatus.Visible = false;
                    this.pnlAssign.Visible = false;
                    this.pnlProduction.Visible = false;
                    this.pnelQC.Visible = false;
                    this.pnelAReject.Visible = false;
                    this.penlInvoice.Visible = false;
                    this.pnelCollection.Visible = false;
                    this.pnelQA.Visible = false;
                    this.pnelFeedBack.Visible = false;
                    this.Pneldelivery.Visible = true;
                    this.GetAIDelivery();
                    break;
                case "9":
                    this.pnlAllProject.Visible = false;
                    this.pnlStatus.Visible = false;
                    this.pnlAssign.Visible = false;
                    this.pnlProduction.Visible = false;
                    this.pnelQC.Visible = false;
                    this.pnelAReject.Visible = false;
                    this.penlInvoice.Visible = true;
                    this.pnelCollection.Visible = false;
                    this.pnelQA.Visible = false;
                    this.pnelFeedBack.Visible = false;
                    this.Pneldelivery.Visible = false;
                    this.GetInvoiceList();
                    break;
                case "10":
                    this.pnlAllProject.Visible = false;
                    this.pnlStatus.Visible = false;
                    this.pnlAssign.Visible = false;
                    this.pnlProduction.Visible = false;
                    this.pnelQC.Visible = false;
                    this.pnelAReject.Visible = false;
                    this.penlInvoice.Visible = false;
                    this.pnelCollection.Visible = true;
                    this.pnelQA.Visible = false;
                    this.pnelFeedBack.Visible = false;
                    this.Pneldelivery.Visible = false;
                    break;

            }

        }

        private void GetBatchAssingList()
        {
            string comcod = this.GetCompCode();

            string prjid = "16%";
            DataSet dt = AIData.GetTransInfo(comcod, "dbo_ai.SP_ENTRY_AI", "BATCHASSIGNLIST", prjid, "", "", "", "", "", "");
            if (dt == null)
                return;

            Session["tblbatchassignlist"] = this.HiddenSameData(dt.Tables[0]);
            this.gv_BatchList.DataSource = dt.Tables[0];
            this.gv_BatchList.DataBind();

        }


        private void GetProductionInfo()
        {
            try
            {

                string comcod = this.GetCompCode();
                DataSet ds = AIData.GetTransInfo(comcod, "dbo_ai.SP_INTERFACE_AI", "GETPRODUCTION_INTERFACE", "", "", "", "", "", "", "");
                if (ds == null)
                    return;
                Session["tblproductioninfo"] = ds.Tables[0];
                DataTable dt1 = new DataTable();
                DataView view = new DataView();
                DataView view1 = new DataView();
                DataView view2 = new DataView();

                view.Table = ds.Tables[0];
                view1.Table = ds.Tables[0];
                view1.RowFilter = " roletype<>'95001' and roletype='95002'";
                dt1 = view1.ToTable();
                this.gv_QCQA.DataSource = dt1;
                this.gv_QCQA.DataBind();


                view.RowFilter = " roletype='95001'";
                dt1 = view.ToTable();
                this.gv_Production.DataSource = dt1;
                this.gv_Production.DataBind();

                view2.Table = ds.Tables[0];
                view2.RowFilter = "roletype='95003'";
                dt1 = view2.ToTable();
                this.gv_AssignQA.DataSource = dt1;
                this.gv_AssignQA.DataBind();

                DataTable dt2 = new DataTable();
                DataView view3 = new DataView();
                view3.Table = ds.Tables[0];
                view3.RowFilter = "roletype<>'95001' and roletype<>'95003' and trackertype <>'99220' and doneqty >'0' ";
                dt1 = view3.ToTable();
                this.gv_AcceptReject.DataSource = dt1;
                this.gv_AcceptReject.DataBind();


            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }


        private void GetAcceptReject()
        {
            try
            {
                string comcod = this.GetCompCode();
                DataSet ds1 = AIData.GetTransInfo(comcod, "dbo_ai.SP_INTERFACE_AI", "GETPPENDING_ACCEPTRJT_INTERFACE", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                Session["tblacceptreject"] = ds1.Tables[0];
                //DataTable dt1 = new DataTable();
                //DataView view1 = new DataView();
                //view1.Table = ds1.Tables[0];
                //view1.RowFilter = "roletype<>'95001' and roletype<>'95003' and trackertype <>'99220' and doneqty >'0' ";
                //dt1 = view1.ToTable();
                //this.gv_AcceptReject.DataSource = dt1;
                //this.gv_AcceptReject.DataBind();
            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }

        }

        protected void tblTaskCreateModal_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenAddTask();", true);
        }

        protected void tbnSave_Click(object sender, EventArgs e)
        {

        }

        protected void btnClient_Click(object sender, EventArgs e)
        {
            //if (this.btnClient.Text == "+")
            //{
            //    this.tblName.Visible = true;
            //    this.textName.Visible = true;
            //    this.tblNumber.Visible = true;
            //    this.TextNumber.Visible = true;
            //    this.tblEmail.Visible = true;
            //    this.TextEmail.Visible = true;
            //    this.textAddess.Visible = true;
            //    this.TextAddress.Visible = true;
            //    this.tbnAdd.Visible = true;

            //}
            //else
            //{
            //    this.tblName.Visible = false;
            //    this.textName.Visible = false;
            //    this.tblNumber.Visible = false;
            //    this.TextNumber.Visible = false;
            //    this.tblEmail.Visible = false;
            //    this.TextEmail.Visible = false;
            //    this.textAddess.Visible = false;
            //    this.TextAddress.Visible = false;
            //    this.btnSave.Visible = false;

            //}
        }

        protected void tbnAdd_Click(object sender, EventArgs e)
        {

        }

        protected void

            _Click(object sender, EventArgs e)
        {

        }

        protected void gvInterface_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvInterface.PageIndex = e.NewPageIndex;
            this.GetAIInterface();
        }
        private DataTable HiddenSameData(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return dt;
            int i = 0;
            string projname = dt.Rows[0]["projname"].ToString();

            foreach (DataRow dr1 in dt.Rows)
            {
                if (i == 0)
                {


                    projname = dr1["projname"].ToString();
                    i++;
                    continue;
                }

                if (dr1["projname"].ToString() == projname)
                {

                    dr1["projname"] = "";


                }


                projname = dr1["projname"].ToString();
            }



            return dt;

        }

        protected void gv_BatchList_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink1 = (HyperLink)e.Row.FindControl("btnview");
                string prjid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "prjid")).ToString().Trim();
                string id = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "id")).ToString().Trim();
                hlink1.NavigateUrl = "~/F_38_AI/Projects.aspx?PID=" + prjid + "&BatchID=" + id;

            }
        }

        protected void gvAssingJob_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink = (HyperLink)e.Row.FindControl("hylnkView");
                string empid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "empid")).ToString().Trim();

                hlink.NavigateUrl = "~/F_38_AI/MyTasks.aspx?Type=MGT&Empid=" + empid;
            }
        }

        protected void gvInterface_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlinkDashboar = (HyperLink)e.Row.FindControl("lnkprjDAshboard");
                HyperLink hlink = (HyperLink)e.Row.FindControl("lnkprjView");
                string projid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "infcod")).ToString().Trim();
                hlink.NavigateUrl = "~/F_38_AI/JobAnalytics.aspx?PID=" + projid;
                hlinkDashboar.NavigateUrl = "~/F_38_AI/JobAnalytics.aspx?PID=" + projid;



            }
        }

        protected void gv_Production_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink = (HyperLink)e.Row.FindControl("hybtnprodlink");
                HyperLink assignlink = (HyperLink)e.Row.FindControl("lnkbtnprodlink");
                string empid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "assignuser")).ToString().Trim();
                string prjid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "prjid")).ToString().Trim();
                string batchid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "batchid")).ToString().Trim();
                string jobid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "jobid")).ToString().Trim();
                hlink.NavigateUrl = "~/F_38_AI/MyTasks.aspx?Type=MGT&EmpID=" + empid + "&JobID=" + jobid + "&BatchID=" + batchid;
                assignlink.NavigateUrl = "~/F_38_AI/Projects.aspx?PID=" + prjid + "&BatchID=" + batchid;

            }
        }

        protected void gv_QCQA_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink = (HyperLink)e.Row.FindControl("hybtnqclink");
                string empid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "assignuser")).ToString().Trim();
                string batchid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "batchid")).ToString().Trim();
                string jobid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "jobid")).ToString().Trim();
                hlink.NavigateUrl = "~/F_38_AI/MyTasks.aspx?Type=MGT&EmpID=" + empid + "&JobID=" + jobid + "&BatchID=" + batchid;
            }
        }

        protected void tblAddBatch_Click(object sender, EventArgs e)
        {
            try
            {
                this.pnlSidebar.Visible = true;
                this.pnlProjectadd.Visible = false;
                this.pnlBatchadd.Visible = true;

                GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
                int index = row.RowIndex;
                string project = ((Label)this.gvInterface.Rows[index].FindControl("lblpactcode")).Text.ToString();
                string projectName = ((Label)this.gvInterface.Rows[index].FindControl("lblprojectName")).Text.ToString();
                string datasettype = ((Label)this.gvInterface.Rows[index].FindControl("lbldataset")).Text.ToString();
                string worktype = ((Label)this.gvInterface.Rows[index].FindControl("lblwrktype")).Text.ToString();
                string deliverydate = ((Label)this.gvInterface.Rows[index].FindControl("lbldeliverydate")).Text.ToString();
                string currncy = ((Label)this.gvInterface.Rows[index].FindControl("lblcurrncy")).Text.ToString();

                //currncy
                this.hiddPrjid.Value = project;
                this.txtproj.Text = projectName;
                this.tblpactcode.Text = project;
                this.txtdataset.Text = datasettype;
                this.txtworktype.Text = worktype;
                this.textdelevery.Text = deliverydate;
                this.txtstartdate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                //this.textdelevery.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                this.spnCurrncy.InnerText = currncy;
                this.txtrate.Attributes.Add("Placeholder", "0.00 " + currncy);
                this.txtAmount.Attributes.Add("Placeholder", "0.00 " + currncy);

                this.GetBatchAssingList(project);


                //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenAddBatch();", true);
            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }

        }
        private void GetBatchAssingList(string project)
        {
            string comcod = this.GetCompCode();
            string prjid = project + "%";
            DataSet ds = AIData.GetTransInfo(comcod, "dbo_ai.SP_ENTRY_AI", "BATCHASSIGNLIST", prjid, "", "", "", "", "", "");
            if (ds == null)
                return;

            Session["tblbatchassignlist"] = ds.Tables[0];
            this.gv_gridBatch.DataSource = ds;
            this.gv_gridBatch.DataBind();

        }

        protected void tblSaveBatch_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetCompCode();
                string postrmid = hst["usrid"].ToString();
                string postseson = hst["compname"].ToString();
                string editbyid = hst["session"].ToString();
                string posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                string id = this.hiidenBatcid.Value;
                string postedbyid = "";
                string editdat = "01-Jan-1900";
                string batchid = this.txtBatch.Text.ToString();
                string prjid = this.hiddPrjid.Value;
                string startdate = this.txtstartdate.Text.ToString();
                string deliverydate = this.textdelevery.Text.ToString();
                double dtquantity = Convert.ToDouble(ASTUtility.ExprToValue("0" + this.txtbatchQuantity.Text.Trim()));
                string datasettype = this.txtdataset.Text.ToString();
                string worktype = this.txtworktype.Text.ToString();
                double totalhour = Convert.ToDouble(ASTUtility.ExprToValue("0" + this.tbltotalOur.Text.Trim()));
                string phdm = this.ddlphdm.SelectedValue.ToString();
                double workperhour = Convert.ToDouble(ASTUtility.ExprToValue("0" + this.txtPerhour.Text.Trim()));
                double textEmpcap = Convert.ToDouble(ASTUtility.ExprToValue("0" + this.textEmpcap.Text.Trim()));
                double rate = Convert.ToDouble(ASTUtility.ExprToValue("0" + this.txtrate.Text.Trim()));
                //comcod,batchid, prjid, startdate, deliverydate, postrmid, postedbyid, postseson, posteddat, editbyid,
                //editdat,datasetqty,datasettype,totalhour,worktype,phdm,pwrkperhour,empcapacity, rate

                bool result = AIData.UpdateTransInfo2(comcod, "dbo_ai.SP_ENTRY_AI", "BATCH_INSERTUPDATE", id, batchid, prjid, startdate,
                    deliverydate, postrmid, postedbyid, postseson, posteddat, editbyid,
                    editdat, dtquantity.ToString(), datasettype, totalhour.ToString(), worktype, phdm,
                    workperhour.ToString(), textEmpcap.ToString(), rate.ToString(), "", "");

                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + AIData.ErrorObject["Msg"].ToString() + "');", true);
                    return;
                }


                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Batch  Saved Successfully');", true);
                this.GetBatchAssingList(prjid);
                this.GetAIInterface();
                this.data_Bind();
                ResetForm();
            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);
            }
        }

        private void ResetForm()
        {
            this.hiidenBatcid.Value = "0";
            txtBatch.Text = "";
            tbltotalOur.Text = "";
            this.ddlphdm.SelectedValue = "0";
            txtstartdate.Text = DateTime.Now.ToString("dd-MMM-yyyy"); ;
            textdelevery.Text = DateTime.Now.ToString("dd-MMM-yyyy"); ;
            txtbatchQuantity.Text = "0";
            txtrate.Text = "0.00";
            txtAmount.Text = "0.00";
            txtPerhour.Text = "";
            textEmpcap.Text = "";
            TextmanPower.Text = "";
        }

        protected void pnlsidebarClose_Click(object sender, EventArgs e)
        {
            this.pnlSidebar.Visible = false;
        }

        protected void btnaddPrj_Click(object sender, EventArgs e)
        {
            this.pnlSidebar.Visible = true;
            this.pnlProjectadd.Visible = true;
            this.pnlBatchadd.Visible = false;

            this.GetEmployeeName();

            this.GetCountry();
            this.GetProjectDetails();
            this.GetCustomerList();
            this.GetLastid();
            this.LoadGrid();

        }
        private void GetEmployeeName()
        {
            Session.Remove("tblempname");
            string comcod = this.GetCompCode();
            string company = "94%";
            string projectName = "%";

            string txtSEmployee = "%%";
            DataSet ds3 = AIData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "GETEMPNAME", company, projectName, txtSEmployee, "", "", "", "", "", "");
            if (ds3 == null)
                return;

            Session["tblempname"] = ds3.Tables[0];
            DataTable dt2 = ds3.Tables[0];

            DataView dv2 = dt2.DefaultView;
            this.ddlassignmember.DataTextField = "empname";
            this.ddlassignmember.DataValueField = "empid";
            this.ddlassignmember.DataSource = dv2.ToTable();
            this.ddlassignmember.DataBind();

        }
        private void GetCountry()
        {
            string comcod = this.GetCompCode();
            Session.Remove("tblCunt");
            DataSet ds1 = AIData.GetTransInfo(comcod, "dbo_ai.SP_ENTRY_CODEBOOK_AI", "GETCOUNTRY", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["tblCunt"] = ds1.Tables[0];
        }
        private void GetCustomerList()
        {
            string comcod = this.GetCompCode();
            DataSet dt = AIData.GetTransInfo(comcod, "dbo_ai.SP_ENTRY_CODEBOOK_AI", "GETCUSTOMERLIST", "", "", "", "", "", "");
            if (dt == null)
                return;

            Session["tblCustlist"] = dt.Tables[0];

        }
        private void GetProjectDetails()
        {
            string comcod = this.GetCompCode();
            DataSet dt3 = AIData.GetTransInfo(comcod, "dbo_ai.SP_ENTRY_AI", "GETINFORMATIONCODE", "", "", "", "", "");
            if (dt3 == null)
                return;
            Session["tblprojectdetails"] = dt3.Tables[0];
        }
        private string GetLastid()
        {
            string sircode = "";

            string comcod = this.GetCompCode();

            DataSet ds1 = AIData.GetTransInfo(comcod, "dbo_ai.SP_ENTRY_AI", "GETLASTPRJCODEID", "", "", "", "", "", "");
            if (ds1 == null)
                return sircode;
            sircode = ds1.Tables[0].Rows[0]["sircode"].ToString();

            return sircode;

        }

        private void isFiledClear()
        {
            try
            {


                for (int i = 0; i < this.gvProjectInfo.Rows.Count; i++)
                {
                    ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvdVal")).Text = "";
                    ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvVal")).Text = "";

                }
            }
            catch (Exception ex)
            {

            }
        }
        private void LoadGrid(string custid = "")
        {

            string comcod = this.GetCompCode();
            string sircode = this.lblproj.Text ?? "";

            DataSet ds1 = AIData.GetTransInfo(comcod, "dbo_ai.SP_ENTRY_AI", "AIPROJECTDETAILS", sircode, "", "", "", "", "");
            if (ds1 == null)
                return;
            DataTable dt = ds1.Tables[0];
            DataTable dt2;

            DataTable dt3 = (DataTable)Session["tblprojectdetails"];
            DataView dv2;
            DataTable dt4 = (DataTable)Session["tblCustlist"];
            DataView dv3;
            DataTable dt5 = (DataTable)Session["tblempname"];
            DataView dv4;
            ViewState["tblcustinf"] = dt;
            this.gvProjectInfo.DataSource = dt;
            this.gvProjectInfo.DataBind();
            DropDownList ddlgval;

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string Gcode = dt.Rows[i]["gcod"].ToString();
                switch (Gcode)
                {
                    case "03002": //project type
                        //gvalue = dt.Rows[i]["value"].ToString();
                        dv2 = dt3.DefaultView;
                        dv2.RowFilter = ("gcod like '60%' and gcod like'%00'");
                        ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((LinkButton)this.gvProjectInfo.Rows[i].FindControl("btnAdd")).Visible = false;
                        ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval")).Visible = true;
                        ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ddlgval = ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv2.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval")).ToString();
                        break;
                    case "03003"://Dataset type
                                 // gvalue = dt.Rows[i]["value"].ToString();
                        dv2 = dt3.DefaultView;
                        dv2.RowFilter = ("gcod like '60%' and gcod not like'%00'");
                        ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((LinkButton)this.gvProjectInfo.Rows[i].FindControl("btnAdd")).Visible = false;
                        ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval")).Visible = true;
                        ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ddlgval = ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv2.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval")).ToString();
                        break;
                    case "03004"://work type
                                 // gvalue = dt.Rows[i]["value"].ToString();
                        dv2 = dt3.DefaultView;
                        dv2.RowFilter = ("gcod like '70%' and gcod not like'%00'");
                        ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((LinkButton)this.gvProjectInfo.Rows[i].FindControl("btnAdd")).Visible = false;
                        ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval")).Visible = true;
                        ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ddlgval = ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv2.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval")).ToString();
                        break;
                    case "03007"://customer
                        //gvalue = dt.Rows[i]["value"].ToString();
                        dv3 = dt4.DefaultView;
                        dv3.RowFilter = ("infcod like '51%' and infcod not like'%00'");
                        ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((LinkButton)this.gvProjectInfo.Rows[i].FindControl("btnAdd")).Visible = true;
                        ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval")).Visible = true;
                        ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ddlgval = ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "infdesc";
                        ddlgval.DataValueField = "infcod";
                        ddlgval.DataSource = dv3.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = (custid == "") ? ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval")).ToString()
                            : custid;
                        break;

                    case "03025"://get team leader
                                 // gvalue = dt.Rows[i]["value"].ToString();
                        dv4 = dt5.DefaultView;
                        //dv3.RowFilter = ("infcod like '51%' and infcod not like'%00'");
                        ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((LinkButton)this.gvProjectInfo.Rows[i].FindControl("btnAdd")).Visible = false;
                        ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval")).Visible = true;
                        ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ddlgval = ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "empname";
                        ddlgval.DataValueField = "empid";
                        ddlgval.DataSource = dv4.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval")).ToString();
                        break;
                    case "03011": //country

                        DataTable dtc = (DataTable)Session["tblCunt"];
                        ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((LinkButton)this.gvProjectInfo.Rows[i].FindControl("btnAdd")).Visible = false;
                        ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval")).Visible = true;
                        ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ddlgval = ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "codedesc";
                        ddlgval.DataValueField = "code";
                        ddlgval.DataSource = dtc;
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval")).ToString();
                        break;
                    case "03008"://date time 
                    case "03009"://date time 

                        string gdatat = ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvdVal")).Text.ToString();
                        ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval")).Visible = false;
                        ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvProjectInfo.Rows[i].FindControl("lgvgdatan")).Visible = false;
                        ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvdVal")).Text = gdatat;
                        break;
                    case "03018":

                        dv2 = dt3.DefaultView;
                        dv2.RowFilter = ("gcod like '80%' and gcod not like'%00'");
                        ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((LinkButton)this.gvProjectInfo.Rows[i].FindControl("btnAdd")).Visible = false;
                        ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval")).Visible = true;
                        ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ddlgval = ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv2.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval")).ToString();
                        break;
                    case "03015":
                    case "03017":
                    case "03019":
                        ((TextBox)this.gvProjectInfo.Rows[i].FindControl("lgvgdatan")).Visible = true;

                        ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval")).Visible = false;
                        break;
                    default:

                        ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((TextBox)this.gvProjectInfo.Rows[i].FindControl("lgvgdatan")).Visible = false;
                        ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval")).Visible = false;
                        ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvVal")).Visible = true;

                        break;

                }

            }

        }

        private void IsClearAddProject()
        {
            try
            {
                DataTable dt = (DataTable)Session["tblprojectdetails"];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string gval = dt.Rows[i]["gval"].ToString();
                    if (gval == "T")
                    {

                        ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvVal")).Text = "";
                        ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvdVal")).Text = "";
                        ((TextBox)this.gvProjectInfo.Rows[i].FindControl("lgvgdatan")).Text = "0.00";

                    }
                }
            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)ViewState["tblcustinf"];
                GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
                int index = row.RowIndex;
                string gcode = ((Label)this.gvProjectInfo.Rows[index].FindControl("lblgvItmCode")).Text.ToString();

                //string Gcode = dt.Rows[0]["gcod"].ToString();
                switch (gcode)
                {
                    case "03007": //customer 
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "CustomerCreate();", true);
                        break;
                }



            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }



        }
        protected void btncustAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string comcod = this.GetCompCode();
                string custname = this.txtcustomername.Text.Trim().ToString();

                DataSet result = AIData.GetTransInfo(comcod, "dbo_ai.SP_ENTRY_CODEBOOK_AI", "CUSTOMER_ADDED", custname);
                if (result == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Create Fail..!!');", true);

                    return;
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Customer Added Successfully');", true);
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#btnAdd", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#btnAdd').hide();", true);
                //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "CustomerCreate();", true);
                string customerId = result.Tables[0].Rows[0]["custid"].ToString();
                GetCustomerList();
                this.LoadGrid(customerId);
            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }

        protected void btnProjectSave_Click(object sender, EventArgs e)
        {
            try
            {
                string prjcode = this.lblproj.Text.Trim().ToString();
                string comcod = this.GetCompCode();
                string sircode = prjcode.Length > 0 ? prjcode : this.GetLastid();

                for (int i = 0; i < this.gvProjectInfo.Rows.Count; i++)
                {
                    string Gcode = ((Label)this.gvProjectInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                    string gtype = ((Label)this.gvProjectInfo.Rows[i].FindControl("lgvgval")).Text.Trim();


                    string Gvalue = (((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval")).Items.Count == 0) ? ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvVal")).Text.Trim() : ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval")).SelectedValue.ToString();
                    if (Gcode == "03008" || Gcode == "03009")
                    {
                        Gvalue = (((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim() == "") ? "01-Jan-1900" : ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim();
                    }
                    if (Gcode == "03015" || Gcode == "03017" || Gcode == "03019")
                    {
                        Gvalue = (((TextBox)this.gvProjectInfo.Rows[i].FindControl("lgvgdatan")).Text.Trim() == "") ? "0.00" : ((TextBox)this.gvProjectInfo.Rows[i].FindControl("lgvgdatan")).Text.Trim();
                    }

                    Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue) : Gvalue;
                    Gvalue = (gtype == "N") ? Convert.ToDouble("0" + Gvalue).ToString() : Gvalue;



                    bool result = AIData.UpdateTransInfo(comcod, "dbo_ai.SP_ENTRY_AI", "PROJECT_INSERTUPDATE", sircode, Gcode, gtype, Gvalue, "", "", "", "");
                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Updated Fail..!!');", true);
                        return;
                    }

                }

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Project Saved Successfully');", true);
                this.TasktState.SelectedIndex = 0;
                this.TasktState_SelectedIndexChanged(null, null);
                this.IsClearAddProject();
            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);
            }
        }

        protected void btnbatchEdit_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string batchid = ((Label)this.gv_gridBatch.Rows[index].FindControl("lblBatchid")).Text.ToString();
            string name = ((Label)this.gv_gridBatch.Rows[index].FindControl("lblbatchname")).Text.ToString();
            string strdate = ((Label)this.gv_gridBatch.Rows[index].FindControl("lblstartdate")).Text.ToString();
            string delvdate = ((Label)this.gv_gridBatch.Rows[index].FindControl("lbldeliverydate")).Text.ToString();
            double dsqty = Convert.ToDouble(((Label)this.gv_gridBatch.Rows[index].FindControl("lbldatasetqty")).Text.ToString());
            double rate = Convert.ToDouble(((Label)this.gv_gridBatch.Rows[index].FindControl("lbldatasetRate")).Text.ToString());
            string lblhourtype = ((Label)this.gv_gridBatch.Rows[index].FindControl("lblhourtype")).Text.ToString();
            string ttlhour = ((Label)this.gv_gridBatch.Rows[index].FindControl("lbldatastotalhour")).Text.ToString();


            this.hiidenBatcid.Value = batchid;

            txtBatch.Text = name;
            tbltotalOur.Text = ttlhour;
            this.ddlphdm.SelectedValue = lblhourtype;
            txtstartdate.Text = strdate;
            textdelevery.Text = delvdate;
            txtbatchQuantity.Text = dsqty.ToString();
            txtrate.Text = rate.ToString();
            txtAmount.Text = Convert.ToDouble(dsqty * rate).ToString();
        }

        protected void btnbatchremoveRow_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string batchid = ((Label)this.gv_gridBatch.Rows[index].FindControl("lblBatchid")).Text.ToString();
            string prjid = this.hiddPrjid.Value;
            bool result = AIData.UpdateTransInfo2(comcod, "dbo_ai.SP_ENTRY_AI", "BATCH_DELETE", batchid, "", "", "", "", userid, Terminal, Sessionid, Date,
                   "", "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Delete Fail..!!');", true);
                return;
            }


            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Batch Deleted Successfully');", true);
            this.GetBatchAssingList(prjid);

        }

        protected void calculateAmount_TextChanged(object sender, EventArgs e)
        {
            string theText = "";
            TextBox textbox = sender as TextBox;
            if (textbox != null)
            {
                theText = textbox.ID;
            }

            double qty = Convert.ToDouble(ASTUtility.ExprToValue("0" + this.txtbatchQuantity.Text.Trim()));
            double rate = Convert.ToDouble(ASTUtility.ExprToValue("0" + this.txtrate.Text.Trim()));
            double amount = Convert.ToDouble(ASTUtility.ExprToValue("0" + this.txtAmount.Text.Trim()));

            double trnamount = 0;
            double trnrte = 0;

            switch (theText)
            {
                case "txtrate":
                    trnamount = (rate > 0 && qty > 0) ? qty * rate : amount;
                    trnrte = rate;
                    break;
                case "txtAmount":
                    trnrte = (amount > 0 && qty > 0) ? amount / qty : rate;
                    trnamount = amount;
                    break;
                default:

                    trnamount = (rate > 0 && qty > 0) ? qty * rate : amount;
                    trnrte = rate;

                    break;
            }



            this.txtrate.Text = trnrte.ToString("#,##0.000000;(#,##0.000000); ");
            this.txtAmount.Text = trnamount.ToString("#,##0.000000;(#,##0.000000); ");


        }

        protected void gvAssingJob_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAssingJob.PageIndex = e.NewPageIndex;
            this.data_Bind();

        }
        private void GetAIDelivery()
        {
            try
            {

                string comcod = this.GetCompCode();
                DataSet ds3 = AIData.GetTransInfo(comcod, "dbo_ai.SP_INTERFACE_AI", "GETAIDELIVERY", "", "", "", "", "");
                if (ds3 == null)
                    return;
                Session["tblAIdelivery"] = ds3.Tables[0];
                DataTable dt2 = new DataTable();
                DataView view3 = new DataView();
                view3.Table = ds3.Tables[0];
                view3.RowFilter = "roletypcode='95003' and doneqty > 0";
                dt2 = view3.ToTable();
                this.gv_Delivery.DataSource = dt2;
                this.gv_Delivery.DataBind();



            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }

        protected void btnproductionlink_Click(object sender, EventArgs e)
        {
            try
            {


                GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
                int index = row.RowIndex;
                string taskid = ((Label)this.gv_Production.Rows[index].FindControl("lblProdtaskid")).Text.ToString();
                string batchid = ((Label)this.gv_Production.Rows[index].FindControl("lblgvbatchid")).Text.ToString();
                string prjid = ((Label)this.gv_Production.Rows[index].FindControl("lblgvpprjid")).Text.ToString();
                string title = ((Label)this.gv_Production.Rows[index].FindControl("lblgvtasktitle")).Text.ToString();
                string assignqty = ((Label)this.gv_Production.Rows[index].FindControl("lblgvdoneqty")).Text.ToString();

                this.txttasktitle.Text = title;
                this.txttasktitle.Enabled = true;
                this.txttasktitle.ReadOnly = true;

                this.txtquantity.Text = assignqty;
                this.HiddinTaskid.Value = taskid;
                this.lblabatchid.Text = batchid;
                this.lblproprjid.Text = prjid;


                this.pnlSidebar.Visible = true;
                this.pnlProjectadd.Visible = false;
                this.pnlBatchadd.Visible = false;
                this.pnlAssginUser.Visible = true;
            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }
        private void GetAnnotationList()
        {
            string comcod = this.GetCompCode();
            string prjlist = "160100000001%";
            string usrrole = this.ddlUserRoleType.SelectedValue.ToString() == "95002" ? "03403" :
                            this.ddlUserRoleType.SelectedValue.ToString() == "95003" ? "03402" : "03401";
            DataSet ds = AIData.GetTransInfo(comcod, "dbo_ai.SP_ENTRY_AI", "GETANNOTAIONID", prjlist, usrrole, "", "", "", "");
            if (ds == null)
                return;

            this.ddlAnnotationid.DataTextField = "item";
            this.ddlAnnotationid.DataValueField = "itemvalue";
            this.ddlAnnotationid.DataSource = ds.Tables[0];
            this.ddlAnnotationid.DataBind();

        }


        protected void ddlUserRoleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetAnnotationList();
        }
        private void GetProjectInformation()
        {
            string comcod = this.GetCompCode();
            DataSet dt2 = AIData.GetTransInfo(comcod, "dbo_ai.SP_ENTRY_AI", "GETINFORMATIONCODE", "", "", "", "", "", "");

            if (dt2 == null)
                return;
            DataTable dt = dt2.Tables[0];
            ViewState["tblgetprojectinfo"] = dt;
            //order type
            DataView dv3 = dt.DefaultView;
            dv3.RowFilter = "gcod like'95%' and gcod not like'%00'";
            this.ddlUserRoleType.DataTextField = "gdesc";
            this.ddlUserRoleType.DataValueField = "gcod";
            this.ddlUserRoleType.DataSource = dv3.ToTable();
            this.ddlUserRoleType.DataBind();

            //task type
            DataView dv2 = dt.DefaultView;
            dv2.RowFilter = " gcod like'71%' ";
            this.ddlassigntype.DataTextField = "gdesc";
            this.ddlassigntype.DataValueField = "gcod";
            this.ddlassigntype.DataSource = dv2.ToTable();
            this.ddlassigntype.DataBind();

        }
        private void VirtualGrid_DataBind()
        {
            DataTable tbl1 = (DataTable)ViewState["tblt01"];
            this.GridVirtual.DataSource = tbl1;
            this.GridVirtual.DataBind();
        }
        private void CreateTableAssign()
        {

            DataTable tblt01 = new DataTable();
            tblt01.Columns.Add("jobid", Type.GetType("System.String"));
            tblt01.Columns.Add("batchid", Type.GetType("System.String"));
            tblt01.Columns.Add("pactcode", Type.GetType("System.String"));
            tblt01.Columns.Add("empid", Type.GetType("System.String"));
            tblt01.Columns.Add("empname", Type.GetType("System.String"));
            tblt01.Columns.Add("assigntype", Type.GetType("System.String"));
            tblt01.Columns.Add("assigndesc", Type.GetType("System.String"));
            tblt01.Columns.Add("annoid", Type.GetType("System.String"));
            tblt01.Columns.Add("roletype", Type.GetType("System.String"));
            tblt01.Columns.Add("roledesc", Type.GetType("System.String"));
            tblt01.Columns.Add("assignqty", Type.GetType("System.Double"));
            tblt01.Columns.Add("workhour", Type.GetType("System.Double"));
            tblt01.Columns.Add("isoutsrc", Type.GetType("System.String"));
            tblt01.Columns.Add("workrate", Type.GetType("System.Double"));

            ViewState["tblt01"] = tblt01;
        }

        protected void btnaddrow_Click(object sender, EventArgs e)
        {

            try
            {
                DataTable tblt01 = (DataTable)ViewState["tblt01"];

                //DataTable tbl1 = (DataTable)ViewState["tblReq"];
                string empid = this.ddlassignmember.SelectedValue.ToString();
                string annoid = this.ddlAnnotationid.SelectedValue.ToString();
                //DataRow[] dr2 = tblt01.Select("empid ='"+ empid + "'");
                //if (dr2.Length == 0)
                //{

                DataRow[] dr3 = tblt01.Select("annoid='" + annoid + "'");
                if (dr3.Length == 0)
                {
                    DataRow dr1 = tblt01.NewRow();
                    DataTable tbl2 = (DataTable)ViewState["tblMat"];
                    dr1["batchid"] = this.lblabatchid.Text.ToString();
                    dr1["empid"] = this.ddlassignmember.SelectedValue.ToString();
                    dr1["empname"] = this.ddlassignmember.SelectedItem.Text;
                    dr1["roletype"] = this.ddlUserRoleType.SelectedItem.Value;
                    dr1["roledesc"] = this.ddlUserRoleType.SelectedItem.Text;
                    dr1["assigntype"] = this.ddlassigntype.SelectedItem.Value.Trim();
                    dr1["assigndesc"] = this.ddlassigntype.SelectedItem.Text.Trim();
                    dr1["annoid"] = this.ddlAnnotationid.SelectedItem.Value.Trim().ToString();
                    dr1["assignqty"] = Convert.ToDouble("0" + this.txtquantity.Text.Trim());
                    dr1["workhour"] = Convert.ToDouble("0" + this.txtworkhour.Text.Trim());
                    dr1["isoutsrc"] = this.checkinoutsourcing.Checked;
                    dr1["workrate"] = this.textrate.Text.Trim();
                    tblt01.Rows.Add(dr1);

                }
                else
                {
                    string msg = "Alredy Exists Annotr ID";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);

                }
                //}
                //else
                //{
                //    string msg = "Alredy Exists";
                //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);

                //}

                ViewState["tblt01"] = tblt01;
                this.VirtualGrid_DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message.ToString() + "');", true);
            }
        }



        protected void btnvrdelete_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)ViewState["tblt01"];
                GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
                int index = row.RowIndex;
                string id = ((Label)this.GridVirtual.Rows[index].FindControl("lbljobid")).Text.Trim();

                if (dt.Rows[index]["jobid"].ToString() == id)
                {
                    dt.Rows[index].Delete();
                }

                ViewState["tblt01"] = dt;
                this.VirtualGrid_DataBind();

            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }

        protected void btntaskSave_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetCompCode();
                DataTable tbl1 = (DataTable)ViewState["tblt01"];
                DataSet ds1 = new DataSet("ds1");
                ds1.Tables.Add(tbl1);
                ds1.Tables[0].TableName = "tbl1";
                string userid = hst["usrid"].ToString();
                string postseson = hst["compname"].ToString();
                string Sessionid = hst["session"].ToString();
                string posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                string batchid = this.lblabatchid.Text;
                string projid = this.lblproprjid.Text;
                string tasktitle = this.txttasktitle.Text.Trim().ToString();
                string taskdesc = this.txtdesc.Text.ToString();
                string tasktype = "";// this.ddlvalocitytype.SelectedValue.ToString();
                string createtask = System.DateTime.Now.ToString("dd-MMM-yyyy");
                string remarks = ""; //this.txtremaks.Text.ToString();
                string estimationtime = "0"; //this.txtworkhour.Text.ToString();
                string dataset = ""; //this.ddldataset.SelectedValue.ToString();
                string qty = "0"; //this.txtquantity.Text.ToString();
                string worktype = ""; //this.ddlworktype.SelectedValue.ToString();
                string perhourqty = "0";//this.txtworkquantity.Text.ToString();
                string anooid = this.ddlAnnotationid.SelectedValue.ToString();
                string roletype = this.ddlUserRoleType.SelectedValue.ToString();
                string assigntype = this.ddlassigntype.SelectedValue.ToString();
                string assignqty = this.txtquantity.Text;
                string workhour = Convert.ToDouble("0" + this.txtworkhour.Text).ToString();
                string workrate = this.textrate.Text;
                string postrmid = "";
                string taskid = this.HiddinTaskid.Value;
                string postedbyid = userid;
                string editdat = "01-Jan-1900";

                string assmember = ""; //this.ddlassignmember.SelectedValue.ToString();
                string annotation = ""; //this.ddlAnnotationid.SelectedValue.ToString();

                //comcod,batchid,tasktitle,taskdesc,tasktype,createtask,createuser,remarks,estimationtime,dataset,qty,worktype,perhourqty, postrmid, postedbyid, postseson,posteddat,prjid,editbyid,editdat
                //comcod, taskid, empid, batchid, annoid,roletype, assigntype,  assignqty, workhour, postedbyid, posteddat, postseson, workrate,isoutsrc

                bool result = AIData.UpdateXmlTransInfo(comcod, "dbo_ai.SP_INTERFACE_AI", "TASK_ASSIGN", ds1, null, null, taskid, postedbyid, createtask, postseson, "", "", "", "");
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + AIData.ErrorObject["Msg"].ToString() + "');", true);
                    return;
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Task Create Saved Successfully');", true);
                this.IsClear();
            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);
            }
        }

        private void IsClear()
        {
            try
            {
                this.HiddinTaskid.Value = "0";
                this.txttasktitle.Text = "";
                this.ddlassignmember.SelectedValue = "";
                this.ddlUserRoleType.SelectedItem.Text = "";
                this.ddlAnnotationid.SelectedItem.Text = "";
                this.ddlassigntype.SelectedItem.Text = "";
                this.txtquantity.Text = "";
                this.txtworkhour.Text = "";
                this.textrate.Text = "";

            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }

        protected void btnqclink_Click(object sender, EventArgs e)
        {
            try
            {


                GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
                int index = row.RowIndex;
                string batchid = ((Label)this.gv_QCQA.Rows[index].FindControl("lblgvqcbatchid")).Text.ToString();
                string taskid = ((Label)this.gv_QCQA.Rows[index].FindControl("lblQCtaskid")).Text.ToString();
                string prjid = ((Label)this.gv_QCQA.Rows[index].FindControl("lblgvqcprjid")).Text.ToString();
                string title = ((Label)this.gv_QCQA.Rows[index].FindControl("lblgvqctasktitle")).Text.ToString();
                string assignqty = ((Label)this.gv_QCQA.Rows[index].FindControl("lblgvqcdoneqty")).Text.ToString();
                this.txttasktitle.Text = title;
                this.txttasktitle.Enabled = true;
                this.txttasktitle.ReadOnly = true;
                this.txtquantity.Text = assignqty;
                this.HiddinTaskid.Value = taskid;
                this.lblproprjid.Text = prjid;
                this.lblabatchid.Text = batchid;


                this.pnlSidebar.Visible = true;
                this.pnlProjectadd.Visible = false;
                this.pnlBatchadd.Visible = false;
                this.pnlAssginUser.Visible = true;
            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }

        protected void btnqalink_Click(object sender, EventArgs e)
        {
            try
            {


                GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
                int index = row.RowIndex;

                string batchid = ((Label)this.gv_AssignQA.Rows[index].FindControl("lblgvqabatchid")).Text.ToString();
                string taskid = ((Label)this.gv_AssignQA.Rows[index].FindControl("lblQAtaskid")).Text.ToString();
                string prjid = ((Label)this.gv_AssignQA.Rows[index].FindControl("lblqaprjid")).Text.ToString();
                string title = ((Label)this.gv_AssignQA.Rows[index].FindControl("lblgvqatasktitle")).Text.ToString();
                string assignqty = ((Label)this.gv_AssignQA.Rows[index].FindControl("lblgvqadoneqty")).Text.ToString();
                this.txttasktitle.Text = title;
                this.txttasktitle.Enabled = true;
                this.txttasktitle.ReadOnly = true;

                this.txtquantity.Text = assignqty;
                this.HiddinTaskid.Value = taskid;
                this.lblabatchid.Text = batchid;
                this.lblproprjid.Text = prjid;


                this.pnlSidebar.Visible = true;
                this.pnlProjectadd.Visible = false;
                this.pnlBatchadd.Visible = false;
                this.pnlAssginUser.Visible = true;
            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }

        protected void btnarlink_Click(object sender, EventArgs e)
        {
            try
            {


                GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
                int index = row.RowIndex;
                string batchid = ((Label)this.gv_AcceptReject.Rows[index].FindControl("lblgvarbatchid")).Text.ToString();
                string taskid = ((Label)this.gv_AcceptReject.Rows[index].FindControl("lblartaskid")).Text.ToString();
                string prjid = ((Label)this.gv_AcceptReject.Rows[index].FindControl("lblgvarprjid")).Text.ToString();
                string title = ((Label)this.gv_AcceptReject.Rows[index].FindControl("lblgvartasktitle")).Text.ToString();
                string assignqty = ((Label)this.gv_AcceptReject.Rows[index].FindControl("lblgvardoneqty")).Text.ToString();
                this.txttasktitle.Text = title;
                this.txttasktitle.Enabled = true;
                this.txttasktitle.ReadOnly = true;

                this.txtquantity.Text = assignqty;
                this.HiddinTaskid.Value = taskid;
                this.lblabatchid.Text = batchid;
                this.lblproprjid.Text = prjid;


                this.pnlSidebar.Visible = true;
                this.pnlProjectadd.Visible = false;
                this.pnlBatchadd.Visible = false;
                this.pnlAssginUser.Visible = true;
            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }

        protected void gv_Delivery_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink = (HyperLink)e.Row.FindControl("lnkInvoice");

                hlink.NavigateUrl = "~/F_38_AI/AIInVoiceCreate.aspx";

            }

        }

        protected void gv_AssignQA_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink = (HyperLink)e.Row.FindControl("hybtnqalink");
                string empid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "assignuser")).ToString().Trim();
                string batchid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "batchid")).ToString().Trim();
                string jobid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "jobid")).ToString().Trim();
                hlink.NavigateUrl = "~/F_38_AI/MyTasks.aspx?EmpID=" + empid + "&JobID=" + jobid + "&BatchID=" + batchid;
            }
        }

        protected void gv_BatchList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_BatchList.PageIndex = e.NewPageIndex;
            this.GetBatchAssingList();
        }

        protected void btnbatchupdate_Click(object sender, EventArgs e)
        {
            try
            {
                this.pnlSidebar.Visible = true;
                this.pnlBatchadd.Visible = true;
                GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
                int index = row.RowIndex;
                string gridid = ((Label)this.gv_BatchList.Rows[index].FindControl("lblbatchid")).Text.ToString();
                string project = ((Label)this.gv_BatchList.Rows[index].FindControl("lblstatusprjid")).Text.ToString();
                string projectName = ((Label)this.gv_BatchList.Rows[index].FindControl("lblbatchprojname")).Text.ToString();
                string batchname = ((Label)this.gv_BatchList.Rows[index].FindControl("lblbatchbatchid")).Text.ToString();
                string startdate = ((Label)this.gv_BatchList.Rows[index].FindControl("lblbatchstartdate")).Text.ToString();
                string totalhour = ((Label)this.gv_BatchList.Rows[index].FindControl("lblbatchtotalhour")).Text.ToString();
                string deliverydate = ((Label)this.gv_BatchList.Rows[index].FindControl("lblbatchdeliverydate")).Text.ToString();
                string orderqty = ((Label)this.gv_BatchList.Rows[index].FindControl("lblbatchdatasetqty")).Text.ToString();
                string rate = ((Label)this.gv_BatchList.Rows[index].FindControl("lblbatchrate")).Text.ToString();
                string orderamount = ((Label)this.gv_BatchList.Rows[index].FindControl("lblbatchamount")).Text.ToString();
                string workperhour = ((Label)this.gv_BatchList.Rows[index].FindControl("lblbatchpwrkperhour")).Text.ToString();
                string empcapa = ((Label)this.gv_BatchList.Rows[index].FindControl("lblbatchempcapacity")).Text.ToString();
                string estimate = ((Label)this.gv_BatchList.Rows[index].FindControl("lblbatchestimatemanpower")).Text.ToString();
                string dataset = ((Label)this.gv_BatchList.Rows[index].FindControl("lblbatchdatasettype")).Text.ToString();
                string worktype = ((Label)this.gv_BatchList.Rows[index].FindControl("lblbatchworktype")).Text.ToString();

                this.tblSaveBatch.Visible = false;
                this.btnbatchUpdate.Visible = true;
                this.hiidenBatcid.Value = gridid;
                this.hiddPrjid.Value = project;
                this.txtproj.Text = projectName;
                this.txtproj.Enabled = true;
                this.txtproj.ReadOnly = true;

                this.txtdataset.Text = dataset;
                this.txtdataset.Enabled = true;
                this.txtdataset.ReadOnly = true;

                this.txtworktype.Text = worktype;
                this.txtworktype.Enabled = true;
                this.txtworktype.ReadOnly = true;

                this.txtBatch.Text = batchname;
                this.tbltotalOur.Text = totalhour;
                this.txtstartdate.Text = startdate;
                this.txtbatchQuantity.Text = orderqty;
                this.txtrate.Text = rate;
                this.txtAmount.Text = orderamount;
                this.textdelevery.Text = deliverydate;
                this.txtPerhour.Text = workperhour;
                this.textEmpcap.Text = empcapa;
                this.TextmanPower.Text = estimate;



            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }

        protected void btnbatchUpdate_Click1(object sender, EventArgs e)
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetCompCode();
                string postrmid = hst["usrid"].ToString();
                string postseson = hst["compname"].ToString();
                string editbyid = hst["session"].ToString();
                string posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                string id = this.hiidenBatcid.Value;
                string postedbyid = "";
                string editdat = "01-Jan-1900";
                string batchid = this.txtBatch.Text.ToString();
                string prjid = this.hiddPrjid.Value;
                string startdate = this.txtstartdate.Text.ToString();
                string deliverydate = this.textdelevery.Text.ToString();
                double dtquantity = Convert.ToDouble(ASTUtility.ExprToValue("0" + this.txtbatchQuantity.Text.Trim()));
                string datasettype = this.txtdataset.Text.ToString();
                string worktype = this.txtworktype.Text.ToString();
                double totalhour = Convert.ToDouble(ASTUtility.ExprToValue("0" + this.tbltotalOur.Text.Trim()));
                string phdm = this.ddlphdm.SelectedValue.ToString();
                double workperhour = Convert.ToDouble(ASTUtility.ExprToValue("0" + this.txtPerhour.Text.Trim()));
                double textEmpcap = Convert.ToDouble(ASTUtility.ExprToValue("0" + this.textEmpcap.Text.Trim()));
                double rate = Convert.ToDouble(ASTUtility.ExprToValue("0" + this.txtrate.Text.Trim()));
                //comcod,batchid, prjid, startdate, deliverydate, postrmid, postedbyid, postseson, posteddat, editbyid,
                //editdat,datasetqty,datasettype,totalhour,worktype,phdm,pwrkperhour,empcapacity, rate

                bool result = AIData.UpdateTransInfo2(comcod, "dbo_ai.SP_ENTRY_AI", "BATCH_INSERTUPDATE", id, batchid, prjid, startdate,
                    deliverydate, postrmid, postedbyid, postseson, posteddat, editbyid,
                    editdat, dtquantity.ToString(), datasettype, totalhour.ToString(), worktype, phdm,
                    workperhour.ToString(), textEmpcap.ToString(), rate.ToString(), "", "");

                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + AIData.ErrorObject["Msg"].ToString() + "');", true);
                    return;
                }


                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Batch  Update Successfully');", true);
                this.GetBatchAssingList();
                this.GetAIInterface();
                this.data_Bind();
                ResetForm();
            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);
            }
        }

        private void GetInvoiceList()
        {
            try
            {
                string comcod = this.GetCompCode();
                DataSet ds = AIData.GetTransInfo(comcod, "dbo_ai.SP_INTERFACE_AI", "GET_INVOICE_SUMMAY_SHEET", "", "", "");
                if (ds == null)
                    return;
                
                Session["tblinvoicelist"] = ds.Tables[0];
                this.gv_Invoice.DataSource = ds.Tables[0];
                this.gv_Invoice.DataBind();

            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);
            }
        }


        protected void btninvoiceprint_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
                int index = row.RowIndex;
                string id = ((Label)this.gv_Invoice.Rows[index].FindControl("lblgvIninvno")).Text.ToString();

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = GetCompCode();
                string comnam = hst["comnam"].ToString();
                string compname = hst["compname"].ToString();
                string username = hst["username"].ToString();
                string comadd = hst["comadd1"].ToString();
                string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
                string printdate = System.DateTime.Now.ToString("dd-MMM-yyyy");
                DataTable dt = (DataTable)Session["tblinvoicelist"];            
               
                DataView dv0 = dt.DefaultView;
                dv0.RowFilter = "invno = '" + id + "'";
                dt = dv0.ToTable();               
                double amount = Convert.ToDouble(dt.Rows[0]["totalamount"]);
                string inword = "In Word: " + ASTUtility.Trans(Math.Round(amount), 2);
                string curency = dt.Rows[0]["currency"].ToString();
                LocalReport Rpt1 = new LocalReport();
                var lst = dt.DataTableToList<RealEntity.C_38_AI.AIallPrint.InvoicePrint>();
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_38_AI.RptAIInvoicePrint", lst, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("comnam", comnam));
                Rpt1.SetParameters(new ReportParameter("inword", inword));
                Rpt1.SetParameters(new ReportParameter("comadd", comadd));
                Rpt1.SetParameters(new ReportParameter("curency", curency));

                Rpt1.SetParameters(new ReportParameter("printdate", printdate));
                Rpt1.SetParameters(new ReportParameter("RptTitle", "INVOICE"));
                Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
                Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
                Session["Report1"] = Rpt1;
                string type = "PDF";
                ScriptManager.RegisterStartupScript(this, GetType(), "target", "SetTarget('" + type + "');", true);

            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);
            }
        }
    }
}