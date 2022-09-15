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
               

            }
        }

        private string GetComdCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        protected void AddCustomerModal_Click(object sender, EventArgs e)
        {
            this.LoadGrid();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenAddCustomer();", true);
        }
        private void LoadGrid()
        {
            
            string comcod = this.GetComdCode();

            DataSet ds1 = MktData.GetTransInfo(comcod, "dbo_ai.SP_ENTRY_CODEBOOK_AI", "AICUSTOMERINSERTUPDATE", "","", "", "", "", "");

          
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
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = true;


                        //dv1 = dt1.DefaultView;
                        //dv1.RowFilter = ("gcod like '85%'");
                        //((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        //((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        //string gdesc1 = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();

                        //string provcode = (comcod == "3365" || comcod == "3101") ? "85006" : comcod == "3354" ? "85099" : "85001";

                        //ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        //ddlgval.DataTextField = "gdesc";
                        //ddlgval.DataValueField = "gcod";
                        //ddlgval.DataSource = dv1.ToTable();
                        //ddlgval.DataBind();
                        //ddlgval.SelectedValue = gdesc1 == "" ? provcode : gdesc1;
                        //ddlval_SelectedIndexChanged(null, null);
                        break;
                    case "01009"://date time 
                       string gdatat = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                       // ((Panel)this.gvPersonalInfo.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        //((LinkButton)this.gvPersonalInfo.Rows[i].FindControl("ibtngrdEmpList")).Visible = false;
                        //string Joindate = (gdatat == "") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim();
                        //string Joindate = (gdatat == "") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim();
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text = gdatat;
                        break;

                    default:
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;           
                       
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = false;
                  

                        break;

                }

            }

            //\\this.ddlimgperson_SelectedIndexChanged(null, null);

        }
       
    }

    
}

