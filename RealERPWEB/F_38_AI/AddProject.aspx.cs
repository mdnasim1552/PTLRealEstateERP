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
    public partial class AddProject : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = "Add Project";
                this.none.Attributes.Add("class", "d-none col-md-4");
                this.gridcol.Attributes.Add("class", "col-md-12");


                this.isFiledClear();
                this.GetEmployeeName();
                this.GetProjectList();
                this.GetCountry();
                this.GetProjectDetails();
                this.GetCustomerList();
                this.GetLastid();
                this.LoadGrid();
                // btnbatchadd_Click(null, null);

            }
        }

        private string GetComdCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void GetProjectList()
        {
            string comcod = this.GetComdCode();
            DataSet dt = MktData.GetTransInfo(comcod, "dbo_ai.SP_ENTRY_AI", "GETALLPRJLIST", "", "", "", "", "", "");
            if (dt == null)
                return;

            Session["tblprojectlist"] = dt.Tables[0];
            this.GridcusDetails.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());

            this.GridcusDetails.DataSource = dt;
            this.GridcusDetails.DataBind();
            // this.tblAddBatch_Click(null, null);
        }
        private void GetCustomerList()
        {
            string comcod = this.GetComdCode();
            DataSet dt = MktData.GetTransInfo(comcod, "dbo_ai.SP_ENTRY_CODEBOOK_AI", "GETCUSTOMERLIST", "", "", "", "", "", "");
            if (dt == null)
                return;

            Session["tblCustlist"] = dt.Tables[0];

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

            Session["tblempname"] = ds3.Tables[0];

        }
        private void GetProjectDetails()
        {
            string comcod = this.GetComdCode();
            DataSet dt3 = MktData.GetTransInfo(comcod, "dbo_ai.SP_ENTRY_AI", "GETINFORMATIONCODE", "", "", "", "", "");
            if (dt3 == null)
                return;
            Session["tblprojectdetails"] = dt3.Tables[0];
        }
        private void LoadGrid()
        {

            string comcod = this.GetComdCode();
            string sircode = "";
            DataSet ds1 = MktData.GetTransInfo(comcod, "dbo_ai.SP_ENTRY_AI", "AIPROJECTDETAILS", sircode, "", "", "", "", "");
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
                        dv2 = dt3.DefaultView;
                        dv2.RowFilter = ("gcod like '60%' and gcod like'%00'");
                        ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((LinkButton)this.gvProjectInfo.Rows[i].FindControl("btnAdd")).Visible = true;
                        ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval")).Visible = true;
                        ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ddlgval = ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv2.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    case "03003"://Dataset type
                        dv2 = dt3.DefaultView;
                        dv2.RowFilter = ("gcod like '60%' and gcod not like'%00'");
                        ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((LinkButton)this.gvProjectInfo.Rows[i].FindControl("btnAdd")).Visible = true;
                        ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval")).Visible = true;
                        ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ddlgval = ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv2.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    case "03004"://work type
                        dv2 = dt3.DefaultView;
                        dv2.RowFilter = ("gcod like '70%' and gcod not like'%00'");
                        ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((LinkButton)this.gvProjectInfo.Rows[i].FindControl("btnAdd")).Visible = true;
                        ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval")).Visible = true;
                        ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ddlgval = ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv2.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    case "03007"://customer
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
                        ddlgval.SelectedValue = ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;

                    case "03025"://get team leader
                        dv4 = dt5.DefaultView;
                        //dv3.RowFilter = ("infcod like '51%' and infcod not like'%00'");
                        ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((LinkButton)this.gvProjectInfo.Rows[i].FindControl("btnAdd")).Visible = true;
                        ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval")).Visible = true;
                        ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ddlgval = ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "empname";
                        ddlgval.DataValueField = "empid";
                        ddlgval.DataSource = dv4.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    case "03011": //country
                        DataTable dtc = (DataTable)Session["tblCunt"];
                        ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((LinkButton)this.gvProjectInfo.Rows[i].FindControl("btnAdd")).Visible = true;
                        ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval")).Visible = true;
                        ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ddlgval = ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "cursymbol";
                        ddlgval.DataValueField = "cursymbol";
                        ddlgval.DataSource = dtc;
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();

                        break;
                    case "03008"://date time 
                    case "03009"://date time 
                        string gdatat = ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();

                        ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval")).Visible = false;
                        ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvVal")).Visible = false;

                        ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvdVal")).Text = gdatat;
                        break;
                    case "03018":
                        dv2 = dt3.DefaultView;
                        dv2.RowFilter = ("gcod like '80%' and gcod not like'%00'");
                        ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((LinkButton)this.gvProjectInfo.Rows[i].FindControl("btnAdd")).Visible = true;
                        ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval")).Visible = true;
                        ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ddlgval = ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv2.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;

                    default:
                        ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;

                        ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval")).Visible = false;

                        break;

                }

            }

        }


        private void GetCountry()
        {
            string comcod = this.GetComdCode();
            Session.Remove("tblCunt");
            DataSet ds1 = MktData.GetTransInfo(comcod, "dbo_ai.SP_ENTRY_CODEBOOK_AI", "GETCOUNTRY", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["tblCunt"] = ds1.Tables[0];
        }

        private string GetLastid()
        {
            string sircode = "";

            string comcod = this.GetComdCode();

            DataSet ds1 = MktData.GetTransInfo(comcod, "dbo_ai.SP_ENTRY_AI", "GETLASTPRJCODEID", "", "", "", "", "", "");
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

        protected void btnProjectSave_Click(object sender, EventArgs e)
        {
            try
            {

                string comcod = this.GetComdCode();
                string sircode = this.GetLastid();

                for (int i = 0; i < this.gvProjectInfo.Rows.Count; i++)
                {
                    string Gcode = ((Label)this.gvProjectInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                    string gtype = ((Label)this.gvProjectInfo.Rows[i].FindControl("lgvgval")).Text.Trim();


                    string Gvalue = (((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval")).Items.Count == 0) ? ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvVal")).Text.Trim() : ((DropDownList)this.gvProjectInfo.Rows[i].FindControl("ddlval")).SelectedValue.ToString();
                    if (Gcode == "03008" || Gcode == "03009")
                    {
                        Gvalue = (((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim() == "") ? "01-Jan-1900" : ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim();
                    }

                    Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue) : Gvalue;
                    Gvalue = (gtype == "N") ? Convert.ToDouble("0" + Gvalue).ToString() : Gvalue;



                    bool result = MktData.UpdateTransInfo(comcod, "dbo_ai.SP_ENTRY_AI", "PROJECT_INSERTUPDATE", sircode, Gcode, gtype, Gvalue, "", "", "", "");
                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Updated Fail..!!');", true);
                        return;
                    }
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Project Saved Successfully');", true);

            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);
            }
        }

        protected void tblAddCustomerModal_Click(object sender, EventArgs e)
        {
            this.none.Attributes.Add("class", "d-block col-md-4");
            this.gridcol.Attributes.Add("class", "col-md-8");
        }



        protected void tblAddBatch_Click(object sender, EventArgs e)
        {

            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string project = ((Label)this.GridcusDetails.Rows[index].FindControl("lblpactcode")).Text.ToString();
            string projectName = ((Label)this.GridcusDetails.Rows[index].FindControl("lblinfdesc")).Text.ToString();
            this.hiddPrjid.Value = project;
            this.txtproj.Text = projectName;
            this.tblpactcode.Text = project;
            this.txtstartdate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            this.textdelevery.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            this.GetBatchAssingList(project);

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenAddBatch();", true);

           
        }

        protected void GridcusDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridcusDetails.PageIndex = e.NewPageIndex;
            GetProjectList();
        }

       

        private void GetBatchAssingList(string project)
        {
            string comcod = this.GetComdCode();
            string prjid = project + "%";
            DataSet dt = MktData.GetTransInfo(comcod, "dbo_ai.SP_ENTRY_AI", "BATCHASSIGNLIST", prjid, "", "", "", "", "", "");
            if (dt == null)
                return;

            Session["tblbatchassignlist"] = dt.Tables[0];
            this.gv_BatchList.DataSource = dt;
            this.gv_BatchList.DataBind();

        }

        protected void removefield_Click(object sender, EventArgs e)
        {
            this.none.Attributes.Add("class", "d-none col-md-4");
            this.gridcol.Attributes.Add("class", "col-md-12");
        }

        protected void tblSaveBatch_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetComdCode();
                string userid = hst["usrid"].ToString();
                string Terminal = hst["compname"].ToString();
                string Sessionid = hst["session"].ToString();
                string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                string batchcreateid = "";
                string batch = this.txtBatch.Text.ToString();
                string projectname = this.hiddPrjid.Value;
                string createdate = this.txtstartdate.Text.ToString();
                string veliverydate = this.textdelevery.Text.ToString();
                string dtquantity = this.txtdatasetQuantity.Text.ToString();
                bool result = MktData.UpdateTransInfo(comcod, "dbo_ai.SP_ENTRY_AI", "BATCH_INSERTUPDATE", batchcreateid, batch, projectname, createdate, veliverydate, userid, Terminal, Sessionid, Date, dtquantity, "", "", "", "");

                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Updated Fail..!!');", true);
                    return;
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Batch  Saved Successfully');", true);
                //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenAddBatch();", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message.ToString() + "');", true);
            }

        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetProjectList();
        }
    }
}