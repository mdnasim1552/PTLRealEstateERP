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
                //this.isFiledClear();
                this.GetProjectList();               
                this.GetCountry();
                this.LoadGrid();
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

            Session["tblCustlist"] = dt.Tables[0];
            this.GridcusDetails.DataSource = dt;
            this.GridcusDetails.DataBind();
        }
        private void LoadGrid()
        {

            string comcod = this.GetComdCode();
            string sircode = "";
            DataSet ds1 = MktData.GetTransInfo(comcod, "dbo_ai.SP_ENTRY_AI", "AIPROJECTDETAILS", sircode, "", "", "", "", "");
            DataTable dt = ds1.Tables[0];           
            DataTable dt2;         
            DataTable dt1 = (DataTable)Session["tblCustlist"];
            DataView dv2;
            ViewState["tblcustinf"] = dt;
            this.gvProjectInfo.DataSource = dt;
            this.gvProjectInfo.DataBind();
            DropDownList ddlgval;
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string Gcode = dt.Rows[i]["gcod"].ToString();
                switch (Gcode)
                {
                    case "03002":
                        dv2 = dt1.DefaultView;
                        dv2.RowFilter = ("gcod like '1601%' and gcod not like'%000'");
                        ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
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
                      

                    case "03011": //country
                        DataTable dtc = (DataTable)Session["tblCunt"];
                        ((TextBox)this.gvProjectInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
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

            DataSet ds1 = MktData.GetTransInfo(comcod, "dbo_ai.SP_ENTRY_CODEBOOK_AI", "GETLASTPRJCODEID", "", "", "", "", "", "");
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
            catch(Exception ex)
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

                    bool result = MktData.UpdateTransInfo(comcod, "dbo_ai.SP_ENTRY_AI", "PROJECT_INSERTUPDATE", sircode, Gcode, gtype, Gvalue, "", "", "", "");
                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Updated Fail..!!');", true);
                        return;
                    }
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Project Saved Successfully');", true);
                this.isFiledClear();
            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);
            }
        }
    }
}