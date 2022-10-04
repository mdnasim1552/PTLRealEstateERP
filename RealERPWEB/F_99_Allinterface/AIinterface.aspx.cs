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

        ProcessAccess HRData = new ProcessAccess();
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

                this.TaskSteps_SelectedIndexChanged(null, null);



            }
        }

        private void GetEmplist()
        {
            string comcod = this.GetCompCode();
            string txtEmpname = "%%";
            string type = "";
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string empid = hst["empid"].ToString() ?? "";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLNEMPLIST", txtEmpname, type, "", "", "", "", "", "", "");




        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void GetAIInterface()
        {
            string comcod = this.GetCompCode();
            DataSet ds = HRData.GetTransInfo(comcod, "dbo_ai.SP_INTERFACE_AI", "GETINTERFACE", "", "", "", "", "", "");
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
                    this.GetBatchAssingList();
                    break;
                case "2":
                    this.pnlStatus.Visible = false;
                    this.pnlAssign.Visible = true;
                    this.pnlProduction.Visible = false;
                    this.pnelQC.Visible = false;
                    this.pnelAReject.Visible = false;
                    this.penlInvoice.Visible = false;
                    this.pnelCollection.Visible = false;
                    break;
                case "3":
                    this.pnlStatus.Visible = false;
                    this.pnlAssign.Visible = false;
                    this.pnlProduction.Visible = true;
                    this.pnelQC.Visible = false;
                    this.pnelAReject.Visible = false;
                    this.penlInvoice.Visible = false;
                    this.pnelCollection.Visible = false;
                    break;
                case "4":
                    this.pnlStatus.Visible = false;
                    this.pnlAssign.Visible = false;
                    this.pnlProduction.Visible = false;
                    this.pnelQC.Visible = true;
                    this.pnelAReject.Visible = false;
                    this.penlInvoice.Visible = false;
                    this.pnelCollection.Visible = false;
                    break;
                case "5":
                    this.pnlStatus.Visible = false;
                    this.pnlAssign.Visible = false;
                    this.pnlProduction.Visible = false;
                    this.pnelQC.Visible = false;
                    this.pnelAReject.Visible = true;
                    this.penlInvoice.Visible = false;
                    this.pnelCollection.Visible = false;
                    break;
                case "6":
                    this.pnlStatus.Visible = false;
                    this.pnlAssign.Visible = false;
                    this.pnlProduction.Visible = false;
                    this.pnelQC.Visible = false;
                    this.pnelAReject.Visible = false;
                    this.penlInvoice.Visible = true;
                    this.pnelCollection.Visible = false;
                    break;
                case "7":
                    this.pnlStatus.Visible = false;
                    this.pnlAssign.Visible = false;
                    this.pnlProduction.Visible = false;
                    this.pnelQC.Visible = false;
                    this.pnelAReject.Visible = false;
                    this.penlInvoice.Visible = false;
                    this.pnelCollection.Visible = true;
                    break;

            }

        }

        protected void TaskSteps_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetAIInterface();
        }

        private void GetBatchAssingList()
        {
            string comcod = this.GetCompCode();

            string prjid = "16%";
            DataSet dt = HRData.GetTransInfo(comcod, "dbo_ai.SP_ENTRY_AI", "BATCHASSIGNLIST", prjid, "", "", "", "", "", "");
            if (dt == null)
                return;

            Session["tblbatchassignlist"] = this.HiddenSameData(dt.Tables[0]);
            this.gv_BatchList.DataSource = dt.Tables[0];
            this.gv_BatchList.DataBind();

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
                hlink1.NavigateUrl = "~/F_38_AI/Projects.aspx?PID="+ prjid + "&BatchID=" + id;

            }
        }
    }
}