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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = "Create Task";

                this.Txtdate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                this.GetComdCode();
                this.GetCustomerList();
                this.GetProjectList();
                this.GetProjectInformation();
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
            Session["tblCustlist"] = dt.Tables[0];
           
        }
        private void GetProjectList()
        {
            string comcod = this.GetComdCode();
            DataSet dt = MktData.GetTransInfo(comcod, "dbo_ai.SP_ENTRY_AI", "GETALLPRJLIST", "", "", "", "", "", "");
            if (dt == null)
                return;
            this.ddlproject.DataTextField = "projectName";
            this.ddlproject.DataValueField = "pactcode";
            this.ddlproject.DataSource = dt.Tables[0];
            this.ddlproject.DataBind();
            Session["tblprojectlist"] = dt.Tables[0];
            
        }

        private void GetProjectInformation()
        {
            string comcod = this.GetComdCode();
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
            //task type
            DataView dv2 = dt.DefaultView;
            dv1.RowFilter = " gcod like'71%' ";
            this.ddltasktype.DataTextField = "gdesc";
            this.ddltasktype.DataValueField = "gcod";
            this.ddltasktype.DataSource = dv2.ToTable();
            this.ddltasktype.DataBind();

            //project type
            DataView dv3 = dt.DefaultView;
            dv1.RowFilter = " gcod like'60%' and gcod like '%00' ";
            this.ddlprotype.DataTextField = "gdesc";
            this.ddlprotype.DataValueField = "gcod";
            this.ddlprotype.DataSource = dv3.ToTable();
            this.ddlprotype.DataBind();
            //Dataset
            DataView dv4 = dt.DefaultView;
            dv1.RowFilter = " gcod like'60%' and gcod not like'%00'";
            this.ddldataset.DataTextField = "gdesc";
            this.ddldataset.DataValueField = "gcod";
            this.ddldataset.DataSource = dv4.ToTable();
            this.ddldataset.DataBind();
            //order type
            DataView dv5 = dt.DefaultView;
            dv1.RowFilter = " gcod like'80%' and gcod like'%00'";
            this.ddlordertype.DataTextField = "gdesc";
            this.ddlordertype.DataValueField = "gcod";
            this.ddlordertype.DataSource = dv5.ToTable();
            this.ddlordertype.DataBind();

        }

        protected void btntaskcreate_Click(object sender, EventArgs e)
        {

            try { 
            string comcod = this.GetComdCode();
            // batchid = @Desc1,tasktitle = @Desc2,taskdesc = @Desc3,tasktype = @Desc4,createtask = @Desc5,remarks = @Desc6,estimationtime = @Desc7,dataset = @Desc8,qty = @Desc9,worktype = @Desc10,perhourqty = @Desc11
            string batchid = this.ddlbatch.SelectedValue.ToString();
            string tasktitle = this.txttasktitle.Text.Trim().ToString();
            string taskdesc = this.txtdesc.Text.ToString();
            string createtask = Convert.ToDateTime(Txtdate.Text).ToString("dd-M-yyyy");
            string remarks = this.txtremaks.Text.ToString();
            string estimationtime = this.txtworkhour.Text.ToString();
            string dataset = this.ddldataset.SelectedValue.ToString();
            string qty = this.txtquantity.Text.ToString();
            string worktype = this.ddlworktype.SelectedValue.ToString();
            string perhourqty = this.txtworkquantity.Text.ToString();
            bool result = MktData.UpdateTransInfo(comcod, "dbo_ai.SP_ENTRY_AI", "TASK_INSERTUPDATE", batchid, tasktitle, taskdesc, createtask, remarks, estimationtime, dataset, qty, worktype, perhourqty, "", "", "", "");
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Updated Fail..!!');", true);
                return;
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Create Saved Successfully');", true);
            }
            catch(Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message.ToString() + "');", true);
            }

        }
    }
}