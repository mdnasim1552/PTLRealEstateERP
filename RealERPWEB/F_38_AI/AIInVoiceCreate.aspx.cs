﻿using RealERPLIB;
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
    public partial class AIInVoiceCreate : System.Web.UI.Page
    {
        ProcessAccess AIData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = "AI Invoice Create";
                string currentdate = DateTime.Now.ToString("dd-MMM-yyyy");
                this.txtdate.Text = currentdate;
                this.GetCustomerList();
                this.GetProjectList();
                this.GetBatchList();
                this.GetDataSet();
                this.GetDataSetList();
                this.GetCurrency();
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
            try
            {
                string comcod = this.GetComdCode();
                DataSet ds = AIData.GetTransInfo(comcod, "dbo_ai.SP_ENTRY_CODEBOOK_AI", "GETCUSTOMERLIST", "", "", "", "", "", "");
                if (ds == null)
                    return;
                DataTable dt = ds.Tables[0];
                Session["tblCustlist"] = ds.Tables[0];
                DataView dv2 = dt.DefaultView;
                this.ddlsuplier.DataTextField = "name";
                this.ddlsuplier.DataValueField = "infcod";
                this.ddlsuplier.DataSource = dv2.ToTable();
                this.ddlsuplier.DataBind();
                //DataView dv3 = dt.DefaultView;
                //this.ddldataset.DataTextField = "dataset";
                //this.ddldataset.DataValueField = "dataset";
                //this.ddldataset.DataSource = dv3.ToTable();
                //this.ddldataset.DataBind();

            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }


        }

        private void GetDataSet()
        {
            try
            {
                string comcod = this.GetComdCode();
                DataSet ds = AIData.GetTransInfo(comcod, "dbo_ai.SP_ENTRY_AI", "GETALLPRJLIST", "", "", "", "", "", "");
                if (ds == null)
                    return;
                DataTable dt = ds.Tables[0];
                DataView dv3 = dt.DefaultView;
                this.ddldataset.DataTextField = "dataset";
                this.ddldataset.DataValueField = "dataset";
                this.ddldataset.DataSource = dv3.ToTable();
                this.ddldataset.DataBind();

            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }


        }
        private void GetProjectList()
        {
            try
            {
                string comcod = this.GetComdCode();
                string client = this.ddlsuplier.SelectedValue.ToString() == "" ? "%16%" : this.ddlsuplier.SelectedValue.ToString();
                DataSet ds = AIData.GetTransInfo(comcod, "dbo_ai.SP_INTERFACE_AI", "PROJECT_LIST", client, "", "", "", "", "");
                if (ds == null)
                    return;
                this.ddlprojname.DataTextField = "infdesc";
                this.ddlprojname.DataValueField = "infcod";
                this.ddlprojname.DataSource = ds.Tables[0];
                this.ddlprojname.DataBind();

            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }

        protected void ddlsuplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetProjectList();
        }

        private void GetBatchList()
        {
            try
            {
                string comcod = this.GetComdCode();
                string projid = this.ddlprojname.SelectedValue.ToString();
                DataSet ds = AIData.GetTransInfo(comcod, "dbo_ai.SP_INTERFACE_AI", "BATCH_LIST", projid, "", "", "", "", "");
                if (ds == null)
                    return;
                this.ddlbatchname.DataTextField = "batrchname";
                this.ddlbatchname.DataValueField = "batchid";
                this.ddlbatchname.DataSource = ds.Tables[0];
                this.ddlbatchname.DataBind();
            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }
        protected void ddlprojname_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetBatchList();
        }

        private void GetDataSetList()
        {
            try
            {
                string comcod = this.GetComdCode();
                string batch = this.ddlbatchname.SelectedItem.Text.ToString();
                DataSet ds = AIData.GetTransInfo(comcod, "dbo_ai.SP_INTERFACE_AI", "DATASETLIST", batch, "", "", "", "", "");
                if (ds == null)
                    return;
                this.ddldataset.DataTextField = "datasettype";
                this.ddldataset.DataValueField = "id";
                this.ddldataset.DataSource = ds.Tables[0];
                this.ddldataset.DataBind();
            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }

        protected void ddlbatchname_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDataSetList();
        }

        private void GetCurrency()
        {
            try
            {
                string comcod = this.GetComdCode();

                DataSet ds = AIData.GetTransInfo(comcod, "dbo_ai.SP_ENTRY_CODEBOOK_AI", "GETCOUNTRY", "", "", "", "", "", "");
                if (ds == null)
                    return;
                this.ddlcurency.DataTextField = "codedesc";
                this.ddlcurency.DataValueField = "code";
                this.ddlcurency.DataSource = ds.Tables[0];
                this.ddlcurency.DataBind();

            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }
        // invoicedate, invoiceno, invoiceno1, duedate,customer,prjname,batchname, dataset,quantity,rate,currency
        private void CreateTableAssign()
        {

            DataTable tblt01 = new DataTable();
            tblt01.Columns.Add("id", Type.GetType("System.String"));
            tblt01.Columns.Add("invoicedate", Type.GetType("System.DateTime"));
            tblt01.Columns.Add("invoiceno", Type.GetType("System.String"));
            tblt01.Columns.Add("invoiceno1", Type.GetType("System.String"));
            tblt01.Columns.Add("duedate", Type.GetType("System.DateTime"));
            tblt01.Columns.Add("customer", Type.GetType("System.String"));
            tblt01.Columns.Add("prjname", Type.GetType("System.String"));
            tblt01.Columns.Add("prjcode", Type.GetType("System.String"));
            tblt01.Columns.Add("batchname", Type.GetType("System.String"));
            tblt01.Columns.Add("datasetcode", Type.GetType("System.String"));
            tblt01.Columns.Add("dataset", Type.GetType("System.String"));
            tblt01.Columns.Add("quantity", Type.GetType("System.Double"));
            tblt01.Columns.Add("rate", Type.GetType("System.Double"));
            tblt01.Columns.Add("currency", Type.GetType("System.String"));
            tblt01.Columns.Add("subjects", Type.GetType("System.String"));

            ViewState["tblt01"] = tblt01;
        }

        private void GetVirtual_Table()
        {
            DataTable tbl1 = (DataTable)ViewState["tblt01"];
            this.gv_AIInvoice.DataSource = tbl1;
            this.gv_AIInvoice.DataBind();
        }
        protected void lnkbtnok_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable tblt01 = (DataTable)ViewState["tblt01"];

                DataRow dr1 = tblt01.NewRow();
                DataTable tbl2 = (DataTable)ViewState["tblMat"];
                dr1["id"] = this.ddlsuplier.SelectedValue.ToString();
                dr1["invoicedate"] = this.txtdate.Text.ToString();
                dr1["invoiceno"] = this.txtInvoiceno.Text.ToString();
                dr1["invoiceno1"] = this.txtInvoiceno2.Text;
                dr1["duedate"] = this.txtduedate.Text.ToString();
                dr1["customer"] = this.ddlsuplier.SelectedItem.Text;
                dr1["prjcode"] = this.ddlprojname.SelectedValue.Trim();
                dr1["prjname"] = this.ddlprojname.SelectedItem.Text.Trim();
                dr1["batchname"] = this.ddlbatchname.SelectedItem.Text.Trim();
                dr1["datasetcode"] = this.ddldataset.SelectedItem.Value.Trim().ToString();
                dr1["dataset"] = this.ddldataset.SelectedItem.Text.Trim().ToString();
                dr1["quantity"] = Convert.ToDouble("0" + this.txtdoneqty.Text.Trim());
                dr1["rate"] = Convert.ToDouble("0" + this.txtrate.Text.Trim());
                dr1["currency"] = this.ddlcurency.SelectedItem.Text.Trim();
                dr1["subjects"] = this.txtsubjects.Text.Trim();
                tblt01.Rows.Add(dr1);


                ViewState["tblt01"] = tblt01;
                this.GetVirtual_Table();


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message.ToString() + "');", true);
            }


        }
    }
}