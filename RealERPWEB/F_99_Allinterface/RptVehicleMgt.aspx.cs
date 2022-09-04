using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_99_Allinterface
{
    public partial class RptVehicleMgt : System.Web.UI.Page
    {
        ProcessAccess _process = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                //Hashtable hst = (Hashtable)Session["tblLogin"];
                //if ((!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp),
                //        (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean(hst["permission"]))
                //    Response.Redirect("~/AcceessError");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Complaint Management Interface";//

                txtfrmdate.Text = System.DateTime.Now.AddDays(-30).ToString("dd-MMM-yyyy");
                txttoDate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
                ModuleName();
                GetTransportInf();
            }
        }
        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void ModuleName()
        {

            string comcod = GetComCode();
            string date1 = txtfrmdate.Text;
            string date2 = txttoDate.Text;
            DataSet ds = _process.GetTransInfo(comcod, "SP_INTERFACE_VEHICLE_MANAGEMENT", "GETTOPNUMBER", date1, date2, "", "", "", "", "", "", "", "", "");
            string item1, item2, item3, item4, item5;
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                item1 = dt.Rows[0][1].ToString();
                item2 = dt.Rows[0][2].ToString();
                item3 = dt.Rows[0][3].ToString();
                item4 = dt.Rows[0][4].ToString();
                item5 = dt.Rows[0][5].ToString();
            }
            else
            {
                item1 = "0";
                item2 = "0";
                item3 = "0";
                item4 = "0";
                item5 = "0";
            }
            this.RadioButtonList1.Items[0].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + item1 + "</div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'>Total</div></div></div>";
            this.RadioButtonList1.Items[1].Text = "<div class='circle-tile'><a><div class='circle-tile-heading red counter'>" + item2 + "</i></div></a><div class='circle-tile-content red'><div class='circle-tile-description text-faded'>" + "HO Approval" + "</div></div></div>";
            this.RadioButtonList1.Items[2].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + item3 + "</i></div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'>Assign Vehicle</div></div></div>";
            this.RadioButtonList1.Items[3].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + item4 + "</i></div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'>Status</div></div></div>";
            this.RadioButtonList1.Items[4].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + item5 + "</i></div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'>Reject</div></div></div>";


        }


        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string value = this.RadioButtonList1.SelectedValue.ToString();
            switch (value)
            {
                case "0":
                    pnlTotalCount.Visible = true;
                    pnlHOApprov.Visible = false;
                    pnlVehicleAssign.Visible = false;
                    pnlStatus.Visible = false;
                    GetTransportInf();
                    break;
                case "1":
                    pnlTotalCount.Visible = false;
                    pnlHOApprov.Visible = true;
                    pnlVehicleAssign.Visible = false;
                    pnlStatus.Visible = false;
                    GetTransportInfHO();
                    break;
                case "2":
                    pnlTotalCount.Visible = false;
                    pnlHOApprov.Visible = false;
                    pnlVehicleAssign.Visible = true;
                    pnlStatus.Visible = false;
                    GetTransportVehicleAssign();
                    break;
                case "3":
                    pnlTotalCount.Visible = false;
                    pnlHOApprov.Visible = false;
                    pnlVehicleAssign.Visible = false;
                    pnlStatus.Visible = true;
                    GetTransportStatus();
                    break;

            }
        }
        private void GetTransportInf()
        {
            string comcod = GetComCode();
            string trpid = "%";
            string date1 = txtfrmdate.Text;
            string date2 = txttoDate.Text;
            DataSet ds1 = _process.GetTransInfo(comcod, "SP_INTERFACE_VEHICLE_MANAGEMENT", "GETTOTAL", date1, date2, "", "", "", "", "", "", "");
            gvTransportInf.DataSource = ds1.Tables[0];
            gvTransportInf.DataBind();
        }

        private void GetTransportInfHO()
        {
            string comcod = GetComCode();
            string date1 = txtfrmdate.Text;
            string date2 = txttoDate.Text;
            DataSet ds1 = _process.GetTransInfo(comcod, "SP_INTERFACE_VEHICLE_MANAGEMENT", "GETHO", date1, date2, "", "", "", "", "", "", "");
            gvHO.DataSource = ds1.Tables[0];
            gvHO.DataBind();

        }
        private void GetTransportVehicleAssign()
        {
            string comcod = GetComCode();
            string date1 = txtfrmdate.Text;
            string date2 = txttoDate.Text;
            DataSet ds1 = _process.GetTransInfo(comcod, "SP_INTERFACE_VEHICLE_MANAGEMENT", "GETASSIGNVEHICLE", date1, date2, "", "", "", "", "", "", "");
            gvVehicleAssign.DataSource = ds1.Tables[0];
            gvVehicleAssign.DataBind();

        }
        private void GetTransportStatus()
        {
            string comcod = GetComCode();
            string date1 = txtfrmdate.Text;
            string date2 = txttoDate.Text;
            DataSet ds1 = _process.GetTransInfo(comcod, "SP_INTERFACE_VEHICLE_MANAGEMENT", "GETASSIGNVEHICLESTATUS", date1, date2, "", "", "", "", "", "", "");
            gvStatus.DataSource = ds1.Tables[0];
            gvStatus.DataBind();

        }


        protected void lnkbtnok_Click(object sender, EventArgs e)
        {
            ModuleName();
            pnlTotalCount.Visible = true;
            RadioButtonList1_SelectedIndexChanged(null, null);
        }

        protected void gvHO_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvHO.Rows[rowIndex];
                string vehicleno = (row.FindControl("lblvehicleId") as Label).Text;
                if (e.CommandName == "Approve")
                {

                    Hashtable hst = (Hashtable)Session["tblLogin"];
                    string comcod = GetComCode();
                    string userId = hst["usrid"].ToString();
                    string seq = "2";
                    bool resultflag = _process.UpdateTransInfo3(comcod, "SP_ENTRY_VEHICLE_MANAGEMENT", "UPSERTTRANSPORTINFHO", vehicleno, seq, "", "", "", "", "", "", "", "", "", "",
                                             "", "", "", "", "", "", "", "", "", "", userId);
                    if (resultflag)
                    {
                        ModuleName();
                        GetTransportInfHO();
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + $"TRP-{vehicleno} HOD Approval" + "');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured" + "');", true);
                    }
                }
                if (e.CommandName == "Reject")
                {

                    lblDgNoReject.Text = vehicleno;
                    lblProcess.Text = "R1";
                    txtRejectDesc.Text = "";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModalReject();", true);

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured-{ex.Message.ToString()}" + "');", true);
            }
        }

        protected void lnkUpdateReject_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComCode();
            string userId = hst["usrid"].ToString();
            string seq = "11";
            string type = lblProcess.Text;
            string vehicleno = lblDgNoReject.Text;
            string rejectDesc = txtRejectDesc.Text;
            bool resultflag = _process.UpdateTransInfo3(comcod, "SP_ENTRY_VEHICLE_MANAGEMENT", "UPSERTTRANSPORTINFHO", vehicleno, seq, rejectDesc, type, "", "", "", "", "", "", "", "",
                                     "", "", "", "", "", "", "", "", "", "", userId);
            if (resultflag)
            {
                ModuleName();
                GetTransportInfHO();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + $"TRP-{vehicleno} HOD Approval" + "');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured" + "');", true);
            }
        }

        protected void gvVehicleAssign_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink = (HyperLink)e.Row.FindControl("lnkAssign");
            }
        }

        private void getVehicleAndDriver()
        {
            string comcod = GetComCode();            
            DataSet ds1 = _process.GetTransInfo(comcod, "SP_ENTRY_VEHICLE_MANAGEMENT", "GETVEHICLEANDDRIVER", "", "", "", "", "", "", "", "", "");
            ddlVehicle.DataSource = ds1.Tables[0];
            ddlVehicle.DataTextField = "sirdesc";
            ddlVehicle.DataValueField = "sircode";
            ddlVehicle.DataBind();

            ddlDriver.DataSource = ds1.Tables[1];
            ddlDriver.DataTextField = "sirdesc1";
            ddlDriver.DataValueField = "sircode";
            ddlDriver.DataBind();

        }
        private void getVehicleStatus()
        {
            string comcod = GetComCode();
            DataSet ds1 = _process.GetTransInfo(comcod, "SP_ENTRY_VEHICLE_MANAGEMENT", "GETVEHICLESTATUS", "", "", "", "", "", "", "", "", "");
            gvAssignedVehicle.DataSource = ds1.Tables[0];            
            gvAssignedVehicle.DataBind();


        }


        protected void gvVehicleAssign_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvVehicleAssign.Rows[rowIndex];
            string vehicleno = (row.FindControl("lblvehicleId") as Label).Text;
            string fdate= (row.FindControl("lblFDate") as Label).Text;
            string tdate = (row.FindControl("lblTDate") as Label).Text;

            if (e.CommandName == "Approve")
            {
                lblTRPID.Text = vehicleno;
                txtTrpId.Text = "TRP-" + vehicleno;
                txtSDate.Text= Convert.ToDateTime(fdate).ToString("yyyy-MM-dd HH:mm").Replace(' ', 'T');
                txtTDate.Text = Convert.ToDateTime(tdate).ToString("yyyy-MM-dd HH:mm").Replace(' ', 'T');
                txtAssignRemarks.Text = "";
                getVehicleAndDriver();
                getVehicleStatus();
                pnlVehicleAssignEntry.Visible = true;
                //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModalAssign();", true);
            }
            else
            {
                lblDgNoReject.Text = vehicleno;
                lblProcess.Text = "R2";
                txtRejectDesc.Text = "";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModalReject();", true);
            }
        }

        protected void lnkUpdateAssign_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComCode();
            string userId = hst["usrid"].ToString();
            string seq = "3";
            string trpid = lblTRPID.Text;
            string sdatetime = txtSDate.Text.ToString().Replace('T', ' ').Replace(',',' ');
            string edatetime = txtTDate.Text.ToString().Replace('T', ' ').Replace(',', ' ');
            string vehicle = ddlVehicle.SelectedValue.ToString();
            string driver = ddlDriver.SelectedValue.ToString();
            string REMARKS = txtAssignRemarks.Text;

            bool resultflag = _process.UpdateTransInfo3(comcod, "SP_ENTRY_VEHICLE_MANAGEMENT", "UPSERTVEHICLEASSIGN", trpid, seq,  sdatetime, edatetime, vehicle, driver, REMARKS, "", "", "", "", "",
                                     "", "", "", "", "", "", "", "", "", "", userId);
            if (resultflag)
            {
                ModuleName();
                GetTransportVehicleAssign();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + $"TRP-{trpid} Assigned" + "');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured" + "');", true);
            }

        }

        
        protected void lnkUpdateStatus_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComCode();
            string userId = hst["usrid"].ToString();
            string seq = "3";
            string trpid = lblACTtrpid.Text;
            string sdatetime = txtActSDate.Text.ToString().Replace('T', ' ').Replace(',', ' ');
            string edatetime = txtActEDate.Text.ToString().Replace('T', ' ').Replace(',', ' ');          
            string REMARKS = txtAssignRemarks.Text;

            bool resultflag = _process.UpdateTransInfo3(comcod, "SP_ENTRY_VEHICLE_MANAGEMENT", "UPSERTVEHICLEACT", trpid, seq, sdatetime, edatetime, "", "", "", "", "", "", "", "",
                                     "", "", "", "", "", "", "", "", "", "", userId);
            if (resultflag)
            {
                ModuleName();
                GetTransportVehicleAssign();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + $"TRP-{trpid} Assigned" + "');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + $"Error Occured" + "');", true);
            }
        }

        protected void ddlVehicle_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "loadModalAssign();", true);
        }
    }
}