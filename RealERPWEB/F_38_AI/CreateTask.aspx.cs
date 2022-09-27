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
    public partial class CreateTask : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        DataTable vt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            vt.Columns.AddRange(new DataColumn[] { new DataColumn("lblmember"), new DataColumn("anotation"), new DataColumn("tbltype"), new DataColumn("tblValoquantity"), new DataColumn("tblworkhour") });
            if (!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = "Create Task";

                this.VirtualGrid();
                this.Txtdate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                this.GetComdCode();
                this.GetCustomerList();
                this.GetEmployeeName();
                this.GetProjectList();
                this.GetBatchList();
                this.GetAnnotationList();
                this.GetProjectInformation();
                this.CreateTableAssign();
            }
        }
        private string GetComdCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void GetCustomerList()
        {
            string comcod = this.GetComdCode();
            DataSet dt = MktData.GetTransInfo(comcod, "dbo_ai.SP_ENTRY_CODEBOOK_AI", "GETCUSTOMERLIST", "", "", "", "", "", "");
            if (dt == null)
                return;
            this.ddlcustomer.DataTextField = "name";
            this.ddlcustomer.DataValueField = "infcod";
            this.ddlcustomer.DataSource = dt.Tables[0];
            this.ddlcustomer.DataBind();

            ListItem li = new ListItem();
            li.Text = "--- Select Customer----";
            li.Value = "%%";
            ddlcustomer.Items.Add(li);
            this.ddlcustomer.SelectedValue = "%%";

            //this.ddlcustomer_SelectedIndexChanged(null, null);
        }
        private void GetProjectList()
        {
            string comcod = this.GetComdCode();
            string prjtype = this.ddlprotype.SelectedValue.ToString();
            string customer = this.ddlcustomer.SelectedValue.ToString() == "" ? "51%" : this.ddlcustomer.SelectedValue.ToString();
            DataSet ds = MktData.GetTransInfo(comcod, "dbo_ai.SP_ENTRY_AI", "GETALLPRJLISTCUSTOMERWISE", prjtype, customer, "", "", "", "");
            if (ds == null)
                return;

            this.ddlproject.DataTextField = "projectName";
            this.ddlproject.DataValueField = "pactcode";
            this.ddlproject.DataSource = ds.Tables[0];
            this.ddlproject.DataBind();
            this.ddlproject_SelectedIndexChanged(null, null);

        }

        private void GetBatchList()
        {
            string comcod = this.GetComdCode();
            string prjlist = this.ddlproject.SelectedValue.ToString() == "" ? "16%" : this.ddlproject.SelectedValue.ToString() + "%";
            DataSet ds = MktData.GetTransInfo(comcod, "dbo_ai.SP_ENTRY_AI", "BATCHASSIGNLIST", prjlist, "", "", "", "", "");
            if (ds == null)
                return;

            this.ddlbatch.DataTextField = "batchdesc";
            this.ddlbatch.DataValueField = "batchid";
            this.ddlbatch.DataSource = ds.Tables[0];
            this.ddlbatch.DataBind();

        }


        private void GetAnnotationList()
        {
            string comcod = this.GetComdCode();
            string prjlist = this.ddlproject.SelectedValue.ToString() == "" ? "16%" : this.ddlproject.SelectedValue.ToString() + "%";
            string usrrole = this.ddlUserRoleType.SelectedValue.ToString();
            DataSet ds = MktData.GetTransInfo(comcod, "dbo_ai.SP_ENTRY_AI", "GETANNOTAIONID", prjlist, usrrole, "", "", "", "");
            if (ds == null)
                return;

            this.ddlAnnotationid.DataTextField = "item";
            this.ddlAnnotationid.DataValueField = "itemvalue";
            this.ddlAnnotationid.DataSource = ds.Tables[0];
            this.ddlAnnotationid.DataBind();

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

        private void GetProjectInformation()
        {
            string comcod = this.GetComdCode();
            string prjcode = this.ddlproject.SelectedValue.ToString();
            DataSet ds = MktData.GetTransInfo(comcod, "dbo_ai.SP_ENTRY_AI", "PROJECTGETINFORMATIONCODE", prjcode, "", "", "", "", "");
            if (ds == null)
                return;
            DataSet dt2 = MktData.GetTransInfo(comcod, "dbo_ai.SP_ENTRY_AI", "GETINFORMATIONCODE", "", "", "", "", "", "");

            if (dt2 == null)
                return;
            DataTable dt = dt2.Tables[0];
            ViewState["tblgetprojectinfo"] = dt;

            //work type
            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = " gcod like'70%' and gcod not like'%00'";
            this.ddlworktype.DataTextField = "gdesc";
            this.ddlworktype.DataValueField = "gcod";
            this.ddlworktype.DataSource = dv1.ToTable();
            this.ddlworktype.DataBind();
            this.ddlworktype.SelectedValue = ds.Tables[0].Rows[2]["gcod"].ToString();
            this.ddlworktype.Enabled = false;
            //task type
            DataView dv2 = dt.DefaultView;
            dv2.RowFilter = " gcod like'71%' ";
            this.ddltasktype.DataTextField = "gdesc";
            this.ddltasktype.DataValueField = "gcod";
            this.ddltasktype.DataSource = dv2.ToTable();
            this.ddltasktype.DataBind();

            //project type
            DataView dv3 = dt.DefaultView;
            dv3.RowFilter = " gcod like'60%' and gcod like '%00' ";
            this.ddlprotype.DataTextField = "gdesc";
            this.ddlprotype.DataValueField = "gcod";
            this.ddlprotype.DataSource = dv3.ToTable();
            this.ddlprotype.DataBind();
            this.ddlprotype.Enabled = false;
            this.ddlprotype.SelectedValue = ds.Tables[0].Rows[0]["gcod"].ToString();

            //Dataset
            DataView dv4 = dt.DefaultView;
            dv4.RowFilter = " gcod like'60%' and gcod not like'%00'";
            this.ddldataset.DataTextField = "gdesc";
            this.ddldataset.DataValueField = "gcod";
            this.ddldataset.DataSource = dv4.ToTable();
            this.ddldataset.DataBind();
            this.ddldataset.Enabled = false;
            this.ddldataset.SelectedValue = ds.Tables[0].Rows[1]["gcod"].ToString();

            //order type
            DataView dv5 = dt.DefaultView;
            dv5.RowFilter = "gcod like'80%' and gcod like'%00'";
            this.ddlordertype.DataTextField = "gdesc";
            this.ddlordertype.DataValueField = "gcod";
            this.ddlordertype.DataSource = dv5.ToTable();
            this.ddlordertype.DataBind();

            //order type
            DataView dv6 = dt.DefaultView;
            dv6.RowFilter = "gcod like'95%' and gcod not like'%00'";
            this.ddlUserRoleType.DataTextField = "gdesc";
            this.ddlUserRoleType.DataValueField = "gcod";
            this.ddlUserRoleType.DataSource = dv6.ToTable();
            this.ddlUserRoleType.DataBind();

        }

        protected void btntaskcreate_Click(object sender, EventArgs e)
        {

            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetComdCode();
                // batchid = @Desc1,tasktitle = @Desc2,taskdesc = @Desc3,tasktype = @Desc4,createtask = @Desc5,remarks = @Desc6,estimationtime = @Desc7,dataset = @Desc8,qty = @Desc9,worktype = @Desc10,perhourqty = @Desc11

                string userid = hst["usrid"].ToString();
                string Terminal = hst["compname"].ToString();
                string Sessionid = hst["session"].ToString();
                string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                string batchid = this.ddlbatch.SelectedValue.ToString();
                string projid = this.ddlproject.SelectedValue.ToString();
                string tasktitle = this.txttasktitle.Text.Trim().ToString();
                string taskdesc = this.txtdesc.Text.ToString();
                string tasktype = this.ddltasktype.SelectedValue.ToString();
                string createtask = Txtdate.Text.ToString();
                string remarks = this.txtremaks.Text.ToString();
                string estimationtime = this.txtworkhour.Text.ToString();
                string dataset = this.ddldataset.SelectedValue.ToString();
                string qty = this.txtquantity.Text.ToString();
                string worktype = this.ddlworktype.SelectedValue.ToString();
                string perhourqty = this.txtworkquantity.Text.ToString();
                string taskid = "";
                string assmember = this.ddlassignmember.SelectedValue.ToString();
                string annotation = this.ddlAnnotationid.SelectedValue.ToString();

                bool result = MktData.UpdateTransInfo2(comcod, "dbo_ai.SP_ENTRY_AI", "TASK_INSERTUPDATE", batchid, tasktitle, taskdesc, tasktype, createtask, remarks, estimationtime, dataset, qty, worktype, perhourqty, userid, Terminal, Sessionid, Date, projid, taskid, "", "", "", "");
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

        protected void ddlcustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetProjectList();
        }

        protected void ddlproject_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetProjectInformation();
            GetBatchList();
            GetAnnotationList();
        }
        private void VirtualGrid()
        {
            this.GridVirtual.DataSource = vt;
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
            DataTable tblt01 = (DataTable)ViewState["tblt01"];


            vt.Rows.Add(ddlassignmember.SelectedItem.Text.Trim(), ddlAnnotationid.SelectedItem.Text.Trim(), ddltasktype.SelectedItem.Text.Trim(), txtquantity.Text.Trim(), txtworkhour.Text.Trim());
            //vt.DefaultView.Sort = "txtmember";
            VirtualGrid();
        }

        protected void ddlbatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            string comcod = this.GetComdCode();
            string batchlist = this.ddlbatch.SelectedValue.ToString();
            DataSet ds = MktData.GetTransInfo(comcod, "dbo_ai.SP_ENTRY_AI", "GETBATCHINFOBYID", batchlist, "", "", "", "", "");
            if (ds == null)
                return;

            
        }

        protected void ddlUserRoleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAnnotationList();
        }
    }
}