using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_38_AI
{
    public partial class Projects : System.Web.UI.Page
    {

        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = "Batch OverView";
                this.GetProjectwiseBatch();
                //this.GetBatchInfo();
                this.MultiView1.ActiveViewIndex = 0;
                ProjectDetails_SelectedIndexChanged1(null, null);
            }

        }
        private string GetComdCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void ProjectDetails_SelectedIndexChanged1(object sender, EventArgs e)
        {
            string value = this.ProjectDetails.SelectedValue.ToString();
            switch (value)
            {
                case "1":
                    this.MultiView1.ActiveViewIndex = 0;

                    this.GetPrjOverView();
                    this.GetEmployeeName();
                    this.GetAnnotationList();
                    this.GetProjectInformation();
                    this.GetBatchInfo();
                    this.VirtualGrid_DataBind();
                    this.CreateTableAssign();

                    break;
                case "2":
                    this.MultiView1.ActiveViewIndex = 1;
                    break;
                case "3":
                    this.MultiView1.ActiveViewIndex = 2;
                    break;
                case "4":
                    this.MultiView1.ActiveViewIndex = 3;
                    break;
                case "5":
                    this.MultiView1.ActiveViewIndex = 4;
                    break;
                case "6":
                    this.MultiView1.ActiveViewIndex = 5;
                    break;
                case "7":
                    this.MultiView1.ActiveViewIndex = 6;
                    break;


            }
        }
        private void GetBatchInfo()
        {
            try
            {
                string comcod = this.GetComdCode();
                string batchid = Request.QueryString["BatchID"].ToString();

                DataSet ds1 = MktData.GetTransInfo(comcod, "dbo_ai.SP_ENTRY_AI", "GETBATCHTASKINFO", batchid, "", "", "", "", "");
                if (ds1 == null)
                    return;
                DataTable dt = ds1.Tables[0];

                this.gv_BatchInfo.DataSource = dt;
                this.gv_BatchInfo.DataBind();


            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }
        

        private void GetPrjOverView()
        {
            try
            {

                string comcod = this.GetComdCode();
                string sircode = Request.QueryString["PID"].ToString();
                //string batchid = Request.QueryString["BatchID"].ToString();               

                DataSet ds1 = MktData.GetTransInfo(comcod, "dbo_ai.SP_ENTRY_AI", "GETPRJOVERVIEW", sircode, "", "", "", "", "");
                if (ds1 == null)
                    return;
                DataTable dt = ds1.Tables[0];

                this.gv_projOverView.DataSource = dt;
                this.gv_projOverView.DataBind();


            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);
            }

        }

        private void GetProjectwiseBatch()
        {

            string comcod = this.GetComdCode();
            string prjid = Request.QueryString["PID"].ToString()==""?"": Request.QueryString["PID"].ToString();
            string batchid = Request.QueryString["BatchID"].ToString()=="" ? "": Request.QueryString["BatchID"].ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "dbo_ai.SP_ENTRY_AI", "GETPRJWISEBATCH", prjid, batchid, "", "", "", "", "");
            if (ds1 == null)
                return;
            DataTable dt = ds1.Tables[0];
            string batchname = ds1.Tables[0].Rows[0]["batchname"].ToString();
            this.lblbatchid.Text = batchname;
            this.hiddnbatchID.Value = ds1.Tables[0].Rows[0]["id"].ToString();
            ViewState["tblgetprojectwisebatch"] = dt;
            this.gv_BatchName.DataSource = dt;
            this.gv_BatchName.DataBind();
        }

        protected void btntaskadd_Click(object sender, EventArgs e)
        {
           
            this.task.Attributes.Add("class", " col-md-12");

        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
           
        }

        private void GetEmployeeName()
        {
            Session.Remove("tblempname");
            string comcod = this.GetComdCode();
            string company = "94%";
            string projectName = "%";

            string txtSEmployee = "%%";
            DataSet ds3 = MktData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "GETEMPNAME", company, projectName, txtSEmployee, "", "", "", "", "", "");
            if (ds3 == null)
                return;
            DataTable dt2 = ds3.Tables[0];
            Session["tblempname"] = ds3.Tables[0];
            DataView dv2 = dt2.DefaultView;
            this.ddlassignmember.DataTextField = "empname";
            this.ddlassignmember.DataValueField = "empid";
            this.ddlassignmember.DataSource = dv2.ToTable();
            this.ddlassignmember.DataBind();

        }
        private void GetAnnotationList()
        {
            string comcod = this.GetComdCode();
            string prjlist = Request.QueryString["PID"].ToString() == "" ? "16%" : Request.QueryString["PID"].ToString() ;
            string usrrole = this.ddlUserRoleType.SelectedValue.ToString() == "95002" ? "03402" :
                            this.ddlUserRoleType.SelectedValue.ToString() == "95003" ? "03403" : "03401";
            DataSet ds = MktData.GetTransInfo(comcod, "dbo_ai.SP_ENTRY_AI", "GETANNOTAIONID", prjlist, usrrole, "", "", "", "");
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
            string comcod = this.GetComdCode();            
            DataSet dt2 = MktData.GetTransInfo(comcod, "dbo_ai.SP_ENTRY_AI", "GETINFORMATIONCODE", "", "", "", "", "", "");

            if (dt2 == null)
                return;
            DataTable dt = dt2.Tables[0];
            ViewState["tblgetprojectinfo"] = dt;
            //order type
            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = "gcod like'95%' and gcod not like'%00'";
            this.ddlUserRoleType.DataTextField = "gdesc";
            this.ddlUserRoleType.DataValueField = "gcod";
            this.ddlUserRoleType.DataSource = dv1.ToTable();
            this.ddlUserRoleType.DataBind();

            //task type
            DataView dv2 = dt.DefaultView;
            dv2.RowFilter = " gcod like'71%' ";
            this.ddlvalocitytype.DataTextField = "gdesc";
            this.ddlvalocitytype.DataValueField = "gcod";
            this.ddlvalocitytype.DataSource = dv2.ToTable();
            this.ddlvalocitytype.DataBind();

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
            tblt01.Columns.Add("valocitycode", Type.GetType("System.String"));
            tblt01.Columns.Add("valocitydesc", Type.GetType("System.String"));
            tblt01.Columns.Add("annoid", Type.GetType("System.String"));
            tblt01.Columns.Add("valocityqty", Type.GetType("System.Double"));
            tblt01.Columns.Add("workhour", Type.GetType("System.Double"));

            ViewState["tblt01"] = tblt01;
        }

        protected void btnaddrow_Click(object sender, EventArgs e)
        {

            try
            {
                DataTable tblt01 = (DataTable)ViewState["tblt01"];

                //DataTable tbl1 = (DataTable)ViewState["tblReq"];
                string empid = this.ddlassignmember.SelectedValue.ToString();
                DataRow[] dr2 = tblt01.Select("empid = '" + empid + "'");
                if (dr2.Length == 0)
                {
                    DataRow dr1 = tblt01.NewRow();
                    DataTable tbl2 = (DataTable)ViewState["tblMat"];
                    dr1["batchid"] = this.hiddnbatchID.Value.ToString();
                    dr1["empid"] = this.ddlassignmember.SelectedValue.ToString();
                    dr1["empname"] = this.ddlassignmember.SelectedItem.Text;
                    dr1["valocitycode"] = this.ddlUserRoleType.SelectedItem.Text.Trim();
                    dr1["annoid"] = this.ddlAnnotationid.SelectedValue.ToString();
                    dr1["valocitydesc"] = this.ddlvalocitytype.SelectedItem.Text.Trim();
                    dr1["valocityqty"] = this.txtquantity.Text.Trim();
                    dr1["workhour"] = this.txtworkhour.Text.Trim();
                    tblt01.Rows.Add(dr1);

                }

                ViewState["tblt01"] = tblt01;
                this.VirtualGrid_DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message.ToString() + "');", true);
            }
        }

        protected void btntaskSave_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetComdCode();
                DataTable tbl1 = (DataTable)ViewState["tblt01"];

                DataSet ds1 = new DataSet("ds1");
                ds1.Tables.Add(tbl1);
                ds1.Tables[0].TableName = "tbl1";


                string userid = hst["usrid"].ToString();
                string Terminal = hst["compname"].ToString();
                string Sessionid = hst["session"].ToString();
                string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                string batchid = Request.QueryString["BatchID"].ToString();
                string projid = Request.QueryString["PID"].ToString();
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
                string taskid = "0";
                string assmember = ""; //this.ddlassignmember.SelectedValue.ToString();
                string annotation = ""; //this.ddlAnnotationid.SelectedValue.ToString();

                bool result = MktData.UpdateXmlTransInfo(comcod, "dbo_ai.SP_ENTRY_AI", "TASK_INSERTUPDATE", ds1, null, null, batchid, tasktitle, taskdesc, tasktype, createtask, remarks, estimationtime, dataset, qty, worktype, perhourqty, userid, Terminal, Sessionid, Date, projid, taskid, "", "", "", "");
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Updated Fail..!!');", true);
                    return;
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Create Saved Successfully');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message.ToString() + "');", true);
            }
        }

        protected void removefield_Click(object sender, EventArgs e)
        {
            this.task.Attributes.Add("class", "d-none");
        }

        protected void removeRow_Click(object sender, EventArgs e)
        {
            try
            {
                string comcod = this.GetComdCode();
                Hashtable hst = (Hashtable)Session["tblLogin"];
                int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                int index = (this.gv_BatchInfo.PageSize * this.gv_BatchInfo.PageIndex) + rowIndex;
                string jobid = ((Label)this.gv_BatchInfo.Rows[index].FindControl("lblgvjobid")).Text.Trim();
               

                bool result = MktData.UpdateTransInfo(comcod, "dbo_ai.SP_ENTRY_AI", "DELETEBATCH", jobid, "", "", "", "", "");
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Delete Fail..!!');", true);
                    return;
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Delete Successfully');", true);
                this.GetBatchInfo();
            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }
    }
}