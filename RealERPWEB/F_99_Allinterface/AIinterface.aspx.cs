﻿using RealERPLIB;
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



            }
        }

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

            Session["tblprojectlist"] = ds.Tables[0];
            Session["tblassinglist"] = ds.Tables[2];
            this.data_Bind();
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
                    this.GetAcceptReject();
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
                    this.pnelFeedBack.Visible = true;
                    this.Pneldelivery.Visible = false;
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
                    this.pnelFeedBack.Visible = false;
                    this.Pneldelivery.Visible = true;
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
            string comcod = this.GetCompCode();
            DataSet ds = AIData.GetTransInfo(comcod, "dbo_ai.SP_INTERFACE_AI", "GETPRODUCTION_INTERFACE", "", "", "", "", "", "", "");
            if (ds == null)
                return;
            Session["tblproductioninfo"] = ds.Tables[0];
            DataTable dt1 = new DataTable();
            DataView view = new DataView();
            DataView view1 = new DataView();
            view.Table = ds.Tables[0];
            view1.Table = ds.Tables[0];
            view1.RowFilter = " velocitytype<>'Annot'";
            dt1 = view1.ToTable();
            this.gv_QCQA.DataSource = dt1;
            this.gv_QCQA.DataBind();
            view.RowFilter = " velocitytype='Annot'";
            dt1 = view.ToTable();
            this.gv_Production.DataSource = dt1;
            this.gv_Production.DataBind();
        }


        private void GetAcceptReject()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = AIData.GetTransInfo(comcod, "dbo_ai.SP_INTERFACE_AI", "GETPPENDING_ACCEPTRJT_INTERFACE", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["tblacceptreject"] = ds1.Tables[0];
            this.gv_AcceptReject.DataSource = ds1;
            this.gv_AcceptReject.DataBind();

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

        protected void btnSave_Click(object sender, EventArgs e)
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

                hlink.NavigateUrl = "~/F_38_AI/MyTasks.aspx?Empid=" + empid;
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
                string empid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "assignuser")).ToString().Trim();
                string batchid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "batchid")).ToString().Trim();
                string jobid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "jobid")).ToString().Trim();
                hlink.NavigateUrl = "~/F_38_AI/MyTasks.aspx?EmpID=" + empid + "&JobID=" + jobid + "&BatchID=" + batchid;
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
                hlink.NavigateUrl = "~/F_38_AI/MyTasks.aspx?EmpID=" + empid + "&JobID=" + jobid + "&BatchID=" + batchid;
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
                this.hiddPrjid.Value = project;
                this.txtproj.Text = projectName;
                this.tblpactcode.Text = project;
                this.txtdataset.Text = datasettype;
                this.txtworktype.Text = worktype;
                this.txtstartdate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                this.textdelevery.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                this.GetBatchAssingList(project);

                
                //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenAddBatch();", true);
            }
            catch (Exception ex)
            {

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
                string userid = hst["usrid"].ToString();
                string Terminal = hst["compname"].ToString();
                string Sessionid = hst["session"].ToString();
                string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                string batchcreateid = "";
                string batch = this.txtBatch.Text.ToString();
                string projectname = this.hiddPrjid.Value;
                string createdate = this.txtstartdate.Text.ToString();
                string veliverydate = this.textdelevery.Text.ToString();
                string dtquantity = this.txtbatchQuantity.Text.ToString();
                string dataset = this.txtdataset.Text.ToString();
                string worktype = this.txtworktype.Text.ToString();
                string totalhour = this.tbltotalOur.Text.ToString();
                string phdm = this.ddlphdm.SelectedValue.ToString();
                string workperhour = this.txtPerhour.Text.ToString();
                string textEmpcap = this.textEmpcap.Text.ToString();

                bool result = AIData.UpdateTransInfo2(comcod, "dbo_ai.SP_ENTRY_AI", "BATCH_INSERTUPDATE", batchcreateid, batch, projectname, createdate, veliverydate, userid, Terminal, Sessionid, Date, dtquantity, dataset, totalhour, worktype, phdm, workperhour, textEmpcap, "", "", "", "", "");

                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Updated Fail..!!');", true);
                    return;
                }
                this.GetBatchAssingList(projectname);

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Batch  Saved Successfully');", true);
                 
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message.ToString() + "');", true);
            }
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
                        ddlgval.DataTextField = "curdesc";
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
            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);
            }
        }

        protected void btnbatchEdit_Click(object sender, EventArgs e)
        {

        }

        protected void btnbatchremoveRow_Click(object sender, EventArgs e)
        {

        }
    }
}