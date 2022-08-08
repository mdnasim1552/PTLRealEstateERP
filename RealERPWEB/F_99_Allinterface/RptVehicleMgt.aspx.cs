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
            this.RadioButtonList1.Items[2].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + item3 + "</i></div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'>Final Approval</div></div></div>";
            this.RadioButtonList1.Items[3].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + item4 + "</i></div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'>Complete</div></div></div>";

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
                    GetTransportInf();
                    break;
                case "1":
                    pnlTotalCount.Visible = false;
                    pnlHOApprov.Visible = true;
                    GetTransportInfHO();
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
            string trpid = "%";
            string date1 = txtfrmdate.Text;
            string date2 = txttoDate.Text;
            DataSet ds1 = _process.GetTransInfo(comcod, "SP_INTERFACE_VEHICLE_MANAGEMENT", "GETHO", date1, date2, "", "", "", "", "", "", "");
            gvHO.DataSource = ds1.Tables[0];
            gvHO.DataBind();

        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {

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
    }
}