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
    public partial class CustomerList : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //if (dr1.Length == 0)
                //    Response.Redirect("../AcceessError.aspx");
                ((Label)this.Master.FindControl("lblTitle")).Text = "Customer List";
                this.none.Attributes.Add("class", "d-none col-md-4");
                this.gridcol.Attributes.Add("class", "col-md-12");

                this.GetCustomerList();
                this.isFiledClear();
                this.GetCountry();
                this.LoadGrid();
            }
        }
        // create by robi
        private string GetComdCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void GetCustomerList()
        {
            string comcod = this.GetComdCode();
            DataSet dt= MktData.GetTransInfo(comcod, "dbo_ai.SP_ENTRY_CODEBOOK_AI", "GETCUSTOMERLIST", "", "", "", "", "", "");
            if (dt == null)
                return;
            
            Session["tblCustlist"] = dt.Tables[0];
            this.GridcusDetails.DataSource = dt;
            this.GridcusDetails.DataBind();
        }


        private void LoadGrid()
        {

            string comcod = this.GetComdCode();
            string sircode = "";
            DataSet ds1 = MktData.GetTransInfo(comcod, "dbo_ai.SP_ENTRY_CODEBOOK_AI", "AICUSTOMERINSERTUPDATE", sircode, "", "", "", "", "");
            DataTable dt = ds1.Tables[0];
            ViewState["tblcustinf"] = dt;
            this.gvPersonalInfo.DataSource = dt;
            this.gvPersonalInfo.DataBind();
            DropDownList ddlgval;
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string Gcode = dt.Rows[i]["gcod"].ToString();
                switch (Gcode)
                {


                    case "01015": //country
                        DataTable dtc = (DataTable)Session["tblCunt"];
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = true;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "curdesc";
                        ddlgval.DataValueField = "curdesc";
                        ddlgval.DataSource = dtc;
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();

                        break;
                    case "01009"://date time 
                        string gdatat = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                       
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text = gdatat;
                        break;


                    default:
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;

                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = false;

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


        protected void btnCustomerSave_Click(object sender, EventArgs e)
        {
            try
            {
 
                string comcod = this.GetComdCode();
                string sircode = this.GetLastid();

                for (int i = 0; i < this.gvPersonalInfo.Rows.Count; i++)
                {
                    string Gcode = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                    string gtype = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lgvgval")).Text.Trim();
                   

                    string Gvalue = (((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Count == 0) ? ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim() : ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).SelectedValue.ToString();
                    if (Gcode == "01009")
                    {
                        Gvalue = (((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim() == "") ? "01-Jan-1900" : ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim();
                    }

                    Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue) : Gvalue;

                    bool result = MktData.UpdateTransInfo(comcod, "dbo_ai.SP_ENTRY_CODEBOOK_AI", "INSERTUPDATECUSTOMER", sircode, Gcode, gtype, Gvalue, "", "", "", "", "");
                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Updated Fail..!!');", true);
                        return;
                    }
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Customer Saved Successfully');", true);
                this.isFiledClear();
            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);
            }

        }

        private string GetLastid()
        {
            string sircode="";

            string comcod = this.GetComdCode();

            DataSet ds1 = MktData.GetTransInfo(comcod, "dbo_ai.SP_ENTRY_CODEBOOK_AI", "GETSIRCODEID", "", "", "", "", "", "");
            if (ds1 == null)
                return sircode;
            sircode = ds1.Tables[0].Rows[0]["sircode"].ToString();

            return sircode;

        }

        private void isFiledClear()
        {
            for (int i = 0; i < this.gvPersonalInfo.Rows.Count; i++)
            {
                ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text = "";
                ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text = "";

            }

        }

        protected void GridcusDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridcusDetails.PageIndex = e.NewPageIndex;
            this.GetCustomerList();
        }

        protected void tblAddCustomerModal_Click(object sender, EventArgs e)
        {
            this.none.Attributes.Add("class", "d-block col-md-4");
            this.gridcol.Attributes.Add("class", "col-md-8");
        }

        protected void removefield_Click(object sender, EventArgs e)
        {
            this.none.Attributes.Add("class", "d-none col-md-4");
            this.gridcol.Attributes.Add("class", "col-md-12");
        }

        protected void lnkView_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string custcod = ((Label)this.GridcusDetails.Rows[index].FindControl("lblinfcode")).Text.ToString();
            string customername = ((Label)this.GridcusDetails.Rows[index].FindControl("lblinfdesc")).Text.ToString();
            string custphone = ((Label)this.GridcusDetails.Rows[index].FindControl("tblpnoe")).Text.ToString();
            string custcity = ((Label)this.GridcusDetails.Rows[index].FindControl("tblcountry")).Text.ToString();
            string custaddress = ((Label)this.GridcusDetails.Rows[index].FindControl("tbladdress")).Text.ToString();
            this.txtcustname.InnerText = customername;
            this.custAddress.InnerText = custaddress;
            this.custphn.InnerText = custphone;
            this.custCountry.InnerText = custcity;
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenCustomerView();", true);
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComdCode();
            string msg = "";
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string id = ((Label)this.GridcusDetails.Rows[index].FindControl("lblinfcode")).Text.ToString();
            bool result = MktData.UpdateTransInfo(comcod, "dbo_ai.SP_ENTRY_CODEBOOK_AI", "CETCUSTOMER_DELETE", id, "", "", "", "", "", "", "");
            if (result)
            {
                msg = "Deleted Successfully";
                this.GetCustomerList();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
            }
            else
            {
                msg = "Delete Failed";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            this.none.Attributes.Add("class", "d-block col-md-4");
            this.gridcol.Attributes.Add("class", "col-md-8");
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string id = ((Label)this.GridcusDetails.Rows[index].FindControl("lblinfcode")).Text.ToString();
            this.lblinfocode.Text = id;
           // this.CustomerEdit();

        }

        private void CustomerEdit()
        {
            string comcod = this.GetComdCode();
            string sircode = this.lblinfocode.Text;
            DataSet ds = MktData.GetTransInfo(comcod, "dbo_ai.SP_ENTRY_CODEBOOK_AI", "GETCUSTOMER_CODE", sircode, "", "", "", "", "", "");

            if (ds == null)
                return;
            DataTable dt = ds.Tables[0];
            gvPersonalInfo.DataSource = ds.Tables[0];
            gvPersonalInfo.DataBind();

           // this.LoadGrid();



        }
    }
}


