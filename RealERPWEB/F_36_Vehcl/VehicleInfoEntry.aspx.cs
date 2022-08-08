using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_36_Vehcl
{
    public partial class VehicleInfoEntry : System.Web.UI.Page
    {
        ProcessAccess _process = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getVehicleType();
                getVehicleEntryInfo();
                getVehicleInfo();
            }
        }
        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void getVehicleType()
        {
            string comcod = GetComCode();
            DataSet ds = _process.GetTransInfo(comcod, "SP_ENTRY_VEHICLE_MANAGEMENT", "GETVEHICLETYP", "", "", "", "", "", "", "", "", "", "", "");
            if (ds == null)
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{_process.ErrorObject["Msg"].ToString()}" + "');", true);

            ViewState["dtVehicleType"] = ds.Tables[0];

        }

        private void getVehicleInfo()
        {
            string comcod = GetComCode();
            DataSet ds = _process.GetTransInfo(comcod, "SP_ENTRY_VEHICLE_MANAGEMENT", "GETVEHICLEINFO", "", "", "", "", "", "", "", "", "", "", "");
            gvVehicleInfo.DataSource = ds.Tables[0];
            gvVehicleInfo.DataBind();
        }


        private void getVehicleEntryInfo()
        {
            try
            {
                gvVehicleEntry.DataSource = null;
                gvVehicleEntry.DataBind();

                string comcod = GetComCode();
                string vehicleId = lblVehicleId.Text;
                DataSet ds = _process.GetTransInfo(comcod, "SP_ENTRY_VEHICLE_MANAGEMENT", "GETVEHICLEINFOTYP", vehicleId, "", "", "", "", "", "", "", "", "", "");
                if (ds == null)
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{_process.ErrorObject["Msg"].ToString()}" + "');", true);

                ViewState["dtVehicleEntry"] = ds.Tables[0];
                DataTable dt = ds.Tables[0];
                DataTable dt1 = (DataTable)ViewState["dtVehicleType"];
                gvVehicleEntry.DataSource = ds.Tables[0];
                gvVehicleEntry.DataBind();
                DropDownList ddlgval;
                DataView dv1;
                string gvalue = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string gcod = dt.Rows[i]["gcod"].ToString();
                    switch (gcod)
                    {
                        case "50005": //Profession
                            gvalue = dt.Rows[i]["value"].ToString();
                            ((TextBox)this.gvVehicleEntry.Rows[i].FindControl("txtgvVal")).Visible = false;
                            ((TextBox)this.gvVehicleEntry.Rows[i].FindControl("txtgvdVal")).Visible = false;
                            ddlgval = ((DropDownList)this.gvVehicleEntry.Rows[i].FindControl("ddlval"));
                            ddlgval.DataTextField = "sirdesc";
                            ddlgval.DataValueField = "sircode";
                            ddlgval.DataSource = dt1;
                            ddlgval.DataBind();
                            ddlgval.Items.Insert(0, new ListItem("--Please Select--", ""));
                            ddlgval.SelectedValue = gvalue;
                            if (vehicleId != "")
                            {
                                ddlgval.Enabled = false;
                            }
                            break;
                        case "50003":
                        case "50004":
                            string Time = dt.Rows[i]["value"].ToString();
                            DateTime date = DateTime.Parse(Time, System.Globalization.CultureInfo.CurrentCulture);
                            string t = date.ToString("HH:mm");
                            ((TextBox)this.gvVehicleEntry.Rows[i].FindControl("txtgvVal")).Visible = false;
                            ((TextBox)this.gvVehicleEntry.Rows[i].FindControl("txtgvdVal")).Visible = true;
                            ((Panel)this.gvVehicleEntry.Rows[i].FindControl("Panegrd")).Visible = false;
                            ((DropDownList)this.gvVehicleEntry.Rows[i].FindControl("ddlval")).Items.Clear();
                            ((DropDownList)this.gvVehicleEntry.Rows[i].FindControl("ddlval")).Visible = false;
                            ((TextBox)this.gvVehicleEntry.Rows[i].FindControl("txtgvdVal")).Text = vehicleId == "" ? "" : t.ToString();
                            ((TextBox)this.gvVehicleEntry.Rows[i].FindControl("txtgvVal")).Text = "";
                            break;
                        default:
                            gvalue = dt.Rows[i]["value"].ToString();
                            ((TextBox)this.gvVehicleEntry.Rows[i].FindControl("txtgvdVal")).Visible = false;
                            ((Panel)this.gvVehicleEntry.Rows[i].FindControl("Panegrd")).Visible = false;
                            ((DropDownList)this.gvVehicleEntry.Rows[i].FindControl("ddlval")).Items.Clear();
                            ((DropDownList)this.gvVehicleEntry.Rows[i].FindControl("ddlval")).Visible = false;
                            ((TextBox)this.gvVehicleEntry.Rows[i].FindControl("txtgvVal")).Text = gvalue;
                            ((TextBox)this.gvVehicleEntry.Rows[i].FindControl("txtgvdVal")).Text = "";
                            break;

                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{ex.Message.ToString()}" + "');", true);
            }
        }


        protected void lnkSave_Click(object sender, EventArgs e)
        {

            DataTable dt1 = new DataTable();
            dt1.Clear();
            dt1.Columns.Add("gcod");
            dt1.Columns.Add("gvalue");
            dt1.Columns.Add("gval");
            dt1.Columns.Add("remarks");
            string comcod = GetComCode();
            string vehicletypeId = "";
            string vehicleName = "";
            string vehicleId = "";
            string gval = "";
           
            for (int i = 0; i < this.gvVehicleEntry.Rows.Count; i++)
            {
                DataRow dr = dt1.NewRow();
                string Gcode = ((Label)this.gvVehicleEntry.Rows[i].FindControl("lblgvItmCodeper")).Text.Trim();
                gval = ((Label)this.gvVehicleEntry.Rows[i].FindControl("lgvgval")).Text.Trim();
                if (Gcode == "50005")
                {
                    vehicletypeId = ((DropDownList)this.gvVehicleEntry.Rows[i].FindControl("ddlval")).SelectedValue.ToString();
                    if (vehicletypeId.Length == 0)
                    {
                        string Message = "Select Type to continue";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Message + "');", true);
                        return;
                    }
                }
                if (Gcode == "50006")
                {
                    vehicleName = ((TextBox)this.gvVehicleEntry.Rows[i].FindControl("txtgvVal")).Text.Trim();
                    if (vehicleName.Length == 0)
                    {
                        string Message = "Name field cannot be empty";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Message + "');", true);
                        return;
                    }
                }
                dr["gcod"] = Gcode;
                dr["gval"] = gval;
                if(((DropDownList)this.gvVehicleEntry.Rows[i].FindControl("ddlval")).Items.Count == 0)
                {
                    string a = ((TextBox)this.gvVehicleEntry.Rows[i].FindControl("txtgvdVal")).Text.Trim();
                    if (((TextBox)this.gvVehicleEntry.Rows[i].FindControl("txtgvdVal")).Text.Trim().Length == 0)
                    {
                        dr["gvalue"] = ((TextBox)this.gvVehicleEntry.Rows[i].FindControl("txtgvVal")).Text.Trim();
                    }
                    else
                    {
                        dr["gvalue"] = ((TextBox)this.gvVehicleEntry.Rows[i].FindControl("txtgvdVal")).Text.Trim();
                    }
                    
                }
                else
                {
                    dr["gvalue"] = ((DropDownList)this.gvVehicleEntry.Rows[i].FindControl("ddlval")).SelectedValue.ToString();
                }
                dt1.Rows.Add(dr);
            }
            if (vehicletypeId.Length != 0 && vehicleName.Length != 0)
            {
                if (lblVehicleId.Text == "")
                {
                    DataSet ds = _process.GetTransInfo(comcod, "SP_ENTRY_VEHICLE_MANAGEMENT", "GETMAXVEHICLEID", vehicletypeId, vehicleName, "", "", "", "", "", "", "", "", "");
                    if (ds == null)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{_process.ErrorObject["Msg"].ToString()}" + "');", true);
                        return;
                    }
                    vehicleId = ds.Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    vehicleId = lblVehicleId.Text;
                }
                
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string userId = hst["usrid"].ToString();
                List<bool> resultCompA = new List<bool>();
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    string remarks = "";

                    bool resultA = _process.UpdateTransInfo3(comcod, "SP_ENTRY_VEHICLE_MANAGEMENT", "UPSERTVEHICLEINF", vehicleId,
                                   dt1.Rows[i]["gcod"].ToString(), dt1.Rows[i]["gvalue"].ToString(), dt1.Rows[i]["gval"].ToString(), remarks, "", "", "", "", "", "", "",
                                       "", "", "", "", "", "", "", "", "", "", userId);
                    resultCompA.Add(resultA);
                }
                if (resultCompA.Contains(false))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured" + "');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Updated Successful" + "');", true);
                    getVehicleInfo();
                    Clear();
                }
            }
        }



        protected void LnkbtnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string vehicleId = ((Label)this.gvVehicleInfo.Rows[index].FindControl("lblvehicleId")).Text.ToString();
            lblVehicleId.Text = vehicleId;
            getVehicleEntryInfo();
        }
        private void Clear()
        {
            lblVehicleId.Text = "";
            getVehicleEntryInfo();

        }
       

        protected void lnkClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}