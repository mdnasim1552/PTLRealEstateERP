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
                this.BatchCount();
                this.assigntask.Visible = true;
                this.GetBatchInfo();
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
                    this.GetPeddingAssign();

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
                Session["batchproject"] = ds1;

                this.gv_BatchInfo.DataSource = dt;
                this.gv_BatchInfo.DataBind();


            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }

        private void BatchCount()
        {
            try
            {
                string comcod = this.GetComdCode();
                string batchid = Request.QueryString["BatchID"].ToString();

                DataSet ds1 = MktData.GetTransInfo(comcod, "dbo_ai.SP_INTERFACE_AI ", "GETBATCHDASHBOARD", batchid, "", "", "", "", "");
                if (ds1 == null || ds1.Tables[0].Rows.Count == 0)
                    return;
                string donetask = ds1.Tables[0].Rows[0]["dontask"].ToString();

                string dontask = ds1.Tables[0].Rows[0]["dontask"].ToString() ?? "";
                string pendtask = ds1.Tables[0].Rows[0]["pendtask"].ToString() ?? "";
                string overduetasks = ds1.Tables[0].Rows[0]["overduetasks"].ToString() ?? "";
                string ttltask = ds1.Tables[0].Rows[0]["ttltask"].ToString() ?? "";
                this.dontask.InnerText = dontask;
                this.pendtask.InnerText = pendtask;
                this.overduetasks.InnerText = overduetasks;
                this.ttltask.InnerText = ttltask;

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
            string prjid = Request.QueryString["PID"].ToString() == "" ? "" : Request.QueryString["PID"].ToString();
            string batchid = Request.QueryString["BatchID"].ToString() == "" ? "" : Request.QueryString["BatchID"].ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "dbo_ai.SP_ENTRY_AI", "GETPRJWISEBATCH", prjid, batchid, "", "", "", "", "");
            if (ds1 == null)
                return;
            DataTable dt = ds1.Tables[0];

            string projectname = ds1.Tables[0].Rows[0]["projectname"].ToString();
            this.lbltitleprjectname.Text = projectname == "" ? "Projects" : projectname;
            string batchname = ds1.Tables[0].Rows[0]["batchname"].ToString();
            this.lblbatchid.Text = batchname;
            this.hiddnbatchID.Value = ds1.Tables[0].Rows[0]["id"].ToString();
            ViewState["tblgetprojectwisebatch"] = ds1;
            this.gv_BatchName.DataSource = dt;
            this.gv_BatchName.DataBind();
        }

        protected void btntaskadd_Click(object sender, EventArgs e)
        {

            try
            {
                this.assigntask.Visible = false;
                this.taskoverview.Visible = false;
                this.penddingtask.Visible = false;
                this.returntask.Visible = false;
                this.rejecttask.Visible = false;
                this.task.Visible = true;
                string comcod = this.GetComdCode();             
              
                string prjid = Request.QueryString["PID"].ToString() == "" ? "" : Request.QueryString["PID"].ToString();
                string batchid = Request.QueryString["BatchID"].ToString() == "" ? "" : Request.QueryString["BatchID"].ToString();
                DataSet ds1 = MktData.GetTransInfo(comcod, "dbo_ai.SP_INTERFACE_AI", "ASSIGNQTYCOUNT", prjid, batchid, "", "", "", "", "");
                if (ds1 == null)
                    return;
                DataTable dt = ds1.Tables[0];
                Session["assignqtycount"] = ds1;
                if (dt.Rows.Count > 0)
                {

                    double totalassign= Convert.ToDouble("0" + ds1.Tables[0].Rows[0]["totalassign"].ToString());
                    double pedingannotor = Convert.ToDouble("0" + ds1.Tables[0].Rows[0]["pendingqty"].ToString());
                    double pedingqc = Convert.ToDouble("0" + ds1.Tables[0].Rows[0]["qcpending"].ToString());
                    double pedingqar = Convert.ToDouble("0" + ds1.Tables[0].Rows[0]["qapending"].ToString());
                    this.lblcountannotid.Text = pedingannotor.ToString("#,##0;(#,##0); ");
                    this.lblcountQC.Text = pedingqc.ToString("#,##0;(#,##0); ");
                    this.lblcountQA.Text = pedingqar.ToString("#,##0;(#,##0); ");
                    this.lbltotalassign.Text = totalassign.ToString("#,##0;(#,##0); ");
                }
            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);
            }

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
            string prjlist = Request.QueryString["PID"].ToString() == "" ? "16%" : Request.QueryString["PID"].ToString();
            string usrrole = this.ddlUserRoleType.SelectedValue.ToString() == "95002" ? "03403" :
                            this.ddlUserRoleType.SelectedValue.ToString() == "95003" ? "03402" : "03401";
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

            if(tbl1==null || tbl1.Rows.Count == 0)
            {
                this.GridVirtual.DataSource = null;
            }
            else
            {
                this.GridVirtual.DataSource = tbl1;
                this.GridVirtual.DataBind();
                this.FooterCalculation(tbl1);

            }

        }

        private void FooterCalculation(DataTable tbl1)
        {
            if (tbl1.Rows.Count == 0)
                return;

            ((Label)this.GridVirtual.FooterRow.FindControl("tblsumValoquantity")).Text = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("sum(assignqty)", "")) ? 0.00 :
                tbl1.Compute("sum(assignqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
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
                this.VirtualGrid_DataBind();
                DataSet dt = (DataSet)ViewState["tblgetprojectwisebatch"];
           
                string roletype = this.ddlUserRoleType.SelectedValue;
                double assignqty = Convert.ToDouble("0" + this.txtquantity.Text.ToString());
                double pedingannotor = Convert.ToDouble("0" + this.lblcountannotid.Text.ToString());
                double pedingqc = Convert.ToDouble("0" + this.lblcountQC.Text.ToString());
                double pedingqar = Convert.ToDouble("0" + this.lblcountQA.Text.ToString());
                double doneannotor = Convert.ToDouble("0" + this.lblDoneAnnot.Text.ToString());
                double doneqc = Convert.ToDouble("0" + this.lblDoneQC.Text.ToString());
                double doneqa = Convert.ToDouble("0" + this.lblDoneQA.Text.ToString());
                double totalassign = Convert.ToDouble("0" + dt.Tables[0].Rows[0]["datasetqty"].ToString());

                double total = 0;
                double validtotal = 0;
                if (this.GridVirtual.Rows.Count!= 0)
                {
                     total = Convert.ToDouble("0" + ((Label)this.GridVirtual.FooterRow.FindControl("tblsumValoquantity")).Text.ToString());
                    validtotal = total + assignqty;

                }
          


                if ((roletype == "95001" && totalassign < assignqty) || (roletype == "95001" && pedingannotor < validtotal))
                {


                    string msg = "Assigned Quantity " + assignqty.ToString() + " Grater Then totalassign  " + totalassign.ToString();
                    this.txtquantity.Focus();
                    this.txtquantity.ForeColor = System.Drawing.Color.Red;
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg.ToString() + "');", true);

                }
                else if (roletype == "95002" && doneannotor < assignqty || roletype == "95002" && doneannotor < validtotal)
                {
                    string msg = "Assigned Quantity " + assignqty.ToString() + " Grater Then doneannotor  " + doneannotor.ToString();
                    this.txtquantity.Focus();
                    this.txtquantity.ForeColor = System.Drawing.Color.Red;
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg.ToString() + "');", true);
                }
                else if (roletype == "95003" && doneqc < assignqty || roletype == "95003" && doneqc < validtotal)
                {
                    string msg = "Assigned Quantity " + assignqty.ToString() + " Grater Then doneqc  " + doneqc.ToString();
                    this.txtquantity.Focus();
                    this.txtquantity.ForeColor = System.Drawing.Color.Red;
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg.ToString() + "');", true);
                }
                else
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
                        dr1["batchid"] = this.hiddnbatchID.Value.ToString();
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
                        dr1["workrate"] = this.textrate.Text.Trim() == "" ? "0" : this.textrate.Text.Trim();
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
                string postseson = hst["compname"].ToString();
                string Sessionid = hst["session"].ToString();
                string posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                string batchid = Request.QueryString["BatchID"].ToString();
                string projid = Request.QueryString["PID"].ToString();
                string tasktitle = this.txttasktitle.Text.ToString();
                string taskdesc = this.txtdesc.Text.ToString();
                string tasktype = "";// this.ddlvalocitytype.SelectedValue.ToString();
                string createtask = System.DateTime.Now.ToString("dd-MMM-yyyy");
                string remarks = ""; //this.txtremaks.Text.ToString();
                string estimationtime = "0"; //this.txtworkhour.Text.ToString();
                string dataset = ""; //this.ddldataset.SelectedValue.ToString();
                string qty = "0"; //this.txtquantity.Text.ToString();
                string worktype = ""; //this.ddlworktype.SelectedValue.ToString();
                string perhourqty = "0";//this.txtworkquantity.Text.ToString();
                string postrmid = "";
                string taskid = this.HiddinTaskid.Value;
                string postedbyid = "";
                string editdat = "01-Jan-1900";
                string editbyid = "";
                string assmember = ""; //this.ddlassignmember.SelectedValue.ToString();
                string annotation = ""; //this.ddlAnnotationid.SelectedValue.ToString();
                                        //comcod, taskid, empid, batchid, annoid,roletype, assigntype,  assignqty,workhour, postedbyid=14, posteddat=16, postseson=15,isoutsrc, workrate
                                        //comcod,batchid,tasktitle,taskdesc,tasktype,createtask,createuser,remarks,estimationtime,dataset,qty,worktype,
                                        //    perhourqty, postrmid, postedbyid, postseson,posteddat,prjid,editbyid,editdat


                bool result = MktData.UpdateXmlTransInfo(comcod, "dbo_ai.SP_ENTRY_AI", "TASK_INSERTUPDATE", ds1, null, null, batchid, tasktitle, taskdesc, tasktype,
                    createtask, userid, remarks, estimationtime,
                    dataset, qty, worktype, perhourqty, postrmid, postedbyid, postseson, posteddat, projid, editbyid, editdat, taskid, "", "");
                //if (!result)
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + result.er.Message.ToString() + "');", true);
                //    return;
                //}
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Task Create  Successfully');", true);
                this.perrate.Visible = false;

                this.IsClear();
                //this.task.Visible = false;
                this.assigntask.Visible = true;
                this.taskoverview.Visible = true;
                this.task.Visible = false;

                this.GetBatchInfo();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message.ToString() + "');", true);
            }
        }

        protected void removefield_Click(object sender, EventArgs e)
        {
            // this.task.Attributes.Add("class", "d-none");
            this.task.Visible = false;
            this.assigntask.Visible = true;
            this.taskoverview.Visible = true;
            this.returntask.Visible = false;
            this.rejecttask.Visible = false;

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

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Batch Deleted Successfully');", true);
                this.GetBatchInfo();
            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }

        private void GetPeddingAssign()
        {
            try
            {
                string comcod = this.GetComdCode();
                string batchid = Request.QueryString["BatchID"].ToString() == "" ? "" : Request.QueryString["BatchID"].ToString();
                DataSet ds1 = MktData.GetTransInfo(comcod, "dbo_ai.SP_ENTRY_AI", "PENDINGASSIGNTASK", batchid);
                Session["penddingassign"] = ds1;
                this.gv_PenddingAssign.DataSource = ds1;
                this.gv_PenddingAssign.DataBind();


            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }

        protected void btntaskEdit_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
                int index = row.RowIndex;
                string id = ((Label)this.gv_BatchInfo.Rows[index].FindControl("lblgvjobid")).Text.ToString();
                this.HiddinTaskid.Value = id;
                string titlename = ((Label)this.gv_BatchInfo.Rows[index].FindControl("lblgvtasktitle")).Text.ToString();
                string empname = ((Label)this.gv_BatchInfo.Rows[index].FindControl("lblgvempname")).Text.ToString();
                string empid = ((Label)this.gv_BatchInfo.Rows[index].FindControl("lblempid")).Text.ToString();
                string roletype = ((Label)this.gv_BatchInfo.Rows[index].FindControl("lblrolettpcode")).Text.ToString();
                string anotationid = ((Label)this.gv_BatchInfo.Rows[index].FindControl("lblgvannoid")).Text.ToString();
                string assigntype = ((Label)this.gv_BatchInfo.Rows[index].FindControl("lblgvassigntype")).Text.ToString();
                string assginqty = ((Label)this.gv_BatchInfo.Rows[index].FindControl("lblgvassignqty")).Text.ToString();
                string workhour = ((Label)this.gv_BatchInfo.Rows[index].FindControl("lblgvwrkhour")).Text.ToString();
                string workperrate = ((Label)this.gv_BatchInfo.Rows[index].FindControl("lblgvworkrate")).Text.ToString();

                this.txttasktitle.Text = titlename;
                this.txttasktitle.ReadOnly = true;
                this.ddlassignmember.SelectedValue = empid;
                this.ddlUserRoleType.SelectedValue = roletype;
                this.ddlAnnotationid.SelectedValue= anotationid;
                this.txtquantity.Text = assginqty;
                this.txtworkhour.Text = workhour;
                this.textrate.Text = workperrate;

                this.taskoverview.Visible = false;
                this.task.Visible = true;
                this.btnaddrow.Visible = false;
                this.btntaskSave.Visible = false;
                this.btntaskUpdate.Visible = true;
                this.assigntask.Visible = false;
                this.returntask.Visible = false;
                this.rejecttask.Visible = false;


            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);
            }



        }

        protected void btntaskUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                string comcod = this.GetComdCode();
                string batchid = Request.QueryString["BatchID"].ToString() == "" ? "" : Request.QueryString["BatchID"].ToString();
                string jobid = this.lbltaskbatchid.Text;
                string empname = this.ddlassignmember.SelectedValue.Trim();
                string valueqty = this.txtquantity.Text;
                string type = this.ddlassigntype.SelectedItem.Value;
                string worktype = this.txtworkhour.Text;
                string annodid = this.ddlAnnotationid.SelectedValue.Trim().ToString();
                string roletype = this.ddlUserRoleType.SelectedItem.Value;
                string textrate = this.textrate.Text;


                bool result = MktData.UpdateTransInfo(comcod, "dbo_ai.SP_ENTRY_AI", "EDITASSIGNTASK", empname, valueqty, type, worktype, annodid, batchid, jobid, roletype, textrate);
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Update Fail..!!');", true);
                    return;
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Update Successfully');", true);
                this.assigntask.Visible = true;
                this.task.Visible = false;
                this.GetBatchInfo();


            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }

        protected void btnbatchtask_SelectedIndexChanged(object sender, EventArgs e)
        {
            string value = this.btnbatchtask.SelectedValue.ToString();
            switch (value)
            {
                case "1":
                    this.assigntask.Visible = true;
                    this.penddingtask.Visible = false;
                    this.returntask.Visible = false;
                    this.rejecttask.Visible = false;
                    break;
                case "2":
                    this.assigntask.Visible = false;
                    this.penddingtask.Visible = true;
                    this.returntask.Visible = false;
                    this.rejecttask.Visible = false;
                    break;
                case "3":
                    this.assigntask.Visible = false;
                    this.penddingtask.Visible = false;
                    this.returntask.Visible = true;
                    this.rejecttask.Visible = false;
                    this.GetReturnReject();
                    break;
                case "4":
                    this.assigntask.Visible = false;
                    this.penddingtask.Visible = false;
                    this.returntask.Visible = false;
                    this.rejecttask.Visible = true;
                    this.GetReturnReject();
                    break;


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

        protected void btntaskQC_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            //    int index = row.RowIndex;
            //    string id = ((Label)this.gv_BatchInfo.Rows[index].FindControl("lblgvjobid")).Text.ToString();
            //    this.lbltaskbatchid.Text = id;
            //    string titlename = ((Label)this.gv_BatchInfo.Rows[index].FindControl("lblgvtasktitle")).Text.ToString();
            //    string empname = ((Label)this.gv_BatchInfo.Rows[index].FindControl("lblgvempname")).Text.ToString();
            //    string empid = ((Label)this.gv_BatchInfo.Rows[index].FindControl("lblempid")).Text.ToString();
            //    string roletype = ((Label)this.gv_BatchInfo.Rows[index].FindControl("lblrolettpcode")).Text.ToString();
            //    string anotationid = ((Label)this.gv_BatchInfo.Rows[index].FindControl("lblgvannoid")).Text.ToString();
            //    string assigntype = ((Label)this.gv_BatchInfo.Rows[index].FindControl("lblgvassigntype")).Text.ToString();
            //    string assginqty = ((Label)this.gv_BatchInfo.Rows[index].FindControl("lblgvassignqty")).Text.ToString();
            //    string workhour = ((Label)this.gv_BatchInfo.Rows[index].FindControl("lblgvwrkhour")).Text.ToString();
            //    string workperrate = ((Label)this.gv_BatchInfo.Rows[index].FindControl("lblgvworkrate")).Text.ToString();

            //    this.txttasktitle.Text = titlename;
            //    this.txttasktitle.ReadOnly = true;
            //    this.ddlassignmember.SelectedValue = empid;
            //    this.ddlUserRoleType.SelectedValue = roletype;
            //    this.ddlAnnotationid.SelectedItem.Value = anotationid;
            //    this.txtquantity.Text = assginqty;
            //    this.txtworkhour.Text = workhour;
            //    this.textrate.Text = workperrate;

            //    this.assigntask.Visible = false;
            //    this.taskoverview.Visible = false;
            //    this.task.Visible = true;
            //    this.btnaddrow.Visible = true;
            //    this.btntaskSave.Visible = true;
            //    this.btntaskUpdate.Visible = false;


            //}
            //catch (Exception exp)

            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            //}
        }

        protected void tbnAssignuser_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
                int index = row.RowIndex;
                string id = ((Label)this.gv_PenddingAssign.Rows[index].FindControl("lblgvtaskid")).Text.ToString();
                this.HiddinTaskid.Value = id;


                string titlename = ((Label)this.gv_PenddingAssign.Rows[index].FindControl("lblgvptasktitle")).Text.ToString();
                string roletype = ((Label)this.gv_PenddingAssign.Rows[index].FindControl("lblgvproletype")).Text.ToString();
                string annotorid = ((Label)this.gv_PenddingAssign.Rows[index].FindControl("lblgvpannoid")).Text.ToString();
                string assigntype = ((Label)this.gv_PenddingAssign.Rows[index].FindControl("lblgvpassigntype")).Text.ToString();
                string assignqty = ((Label)this.gv_PenddingAssign.Rows[index].FindControl("lblgvpdoneqty")).Text.ToString();
                this.txttasktitle.Text = titlename;
                this.ddlUserRoleType.SelectedValue = roletype;
                this.ddlAnnotationid.SelectedItem.Value = annotorid;
                this.ddlassigntype.SelectedValue = assigntype;
                this.txtquantity.Text = assignqty;
                this.txttasktitle.ReadOnly = true;

                this.task.Visible = true;
                this.taskoverview.Visible = false;
                this.penddingtask.Visible = false;
                this.assigntask.Visible = false;
                this.btnaddrow.Visible = true;
                this.btntaskSave.Visible = true;
            }
            catch (Exception exp)

            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }

        private void GetReturnReject()
        {
            try
            {
                string comcod = this.GetComdCode();
                string batchid = Request.QueryString["BatchID"].ToString() == "" ? "" : Request.QueryString["BatchID"].ToString();
                string prjid = Request.QueryString["PID"].ToString() == "" ? "" : Request.QueryString["PID"].ToString();
                DataSet ds = MktData.GetTransInfo(comcod, "dbo_ai.SP_ENTRY_AI", "BATCHWISE_RETURN_REJECT", prjid, batchid, "", "", "", "");
                if (ds == null)
                    return;

                Session["tblreturnreject"] = ds.Tables[0];
                DataTable dt1 = new DataTable();
                DataView view = new DataView();
                DataView view1 = new DataView();

                view.Table = ds.Tables[0];
                view.RowFilter = "returnqty > '0' ";
                dt1 = view.ToTable();
                this.gv_ReturnTask.DataSource = dt1;
                this.gv_ReturnTask.DataBind();


                view1.Table = ds.Tables[0];
                view1.RowFilter = "rejectqty > '0' ";
                dt1 = view.ToTable();
                this.gv_Rejecttask.DataSource = dt1;
                this.gv_Rejecttask.DataBind();



            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }

        protected void checkinoutsourcing_CheckedChanged(object sender, EventArgs e)
        {
            bool check = this.checkinoutsourcing.Checked;
            if (!check)
            {
                this.perrate.Visible = false;
                this.textrate.Text = "";

            }
            else
            {
                this.perrate.Visible = true;
                string rate = "80";
                this.textrate.Text = rate;
            }
        }
    }
}