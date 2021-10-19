using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPLIB;
using RealERPRPT;
using System.Drawing;
using Microsoft.Reporting.WinForms;
namespace RealERPWEB.F_99_Allinterface
{
    public partial class InventoryInterface : System.Web.UI.Page
    {
        ProcessAccess feaData = new ProcessAccess();
        DeleteLog log = new DeleteLog();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));


                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                ((Label)this.Master.FindControl("lblTitle")).Text = "Inventory Interface";

                ((LinkButton)this.Master.FindControl("lnkPrint")).Visible = false;
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).Visible = false;

                RadioButtonList1_SelectedIndexChanged(null, null);
                RadioButtonList1.SelectedIndex = 0;


            }
        }

        public string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.gvPrjInfo_RowDataBound(null, null);
            this.GetintoryData();
            //this.Data_Bind();
            string view = this.RadioButtonList1.SelectedValue.ToString();
            switch (view)
            {
                case "0":
                    this.pnlstatus.Visible = true;
                    this.pnlgatepass.Visible = false;
                    this.pnlapproval.Visible = false;
                    this.pnlaudit.Visible = false;
                    this.pnlaccount.Visible = false;
                    this.RadioButtonList1.Items[0].Attributes["class"] = "lblactive blink_me";

                    this.pnlReqAprv.Visible = false;
                    //this.RadioButtonList1.Items[0].Attributes["style"] = "background: #430000; display:block; ";
                    break;
                 // mat req approval
                case "1":
                    this.pnlstatus.Visible = false;
                    this.pnlgatepass.Visible = false;
                    this.pnlapproval.Visible = false;
                    this.pnlaudit.Visible = false;
                    this.pnlaccount.Visible = false;
                    this.pnlReqAprv.Visible = true;
                    //this.RadioButtonList1.Items[1].Attributes["style"] = "background: #430000; display:block; ";
                    this.RadioButtonList1.Items[1].Attributes["class"] = "lblactive blink_me";

                    break;

                case "2":
                    this.pnlstatus.Visible = false;
                    this.pnlgatepass.Visible = true;
                    this.pnlapproval.Visible = false;
                    this.pnlaudit.Visible = false;
                    this.pnlaccount.Visible = false;

                    this.pnlReqAprv.Visible = false;
                    //this.RadioButtonList1.Items[1].Attributes["style"] = "background: #430000; display:block; ";
                    this.RadioButtonList1.Items[1].Attributes["class"] = "lblactive blink_me";
                    break;
                case "3":
                    this.pnlstatus.Visible = false;
                    this.pnlgatepass.Visible = false;
                    this.pnlapproval.Visible = true;
                    this.pnlaudit.Visible = false;
                    this.pnlaccount.Visible = false;
                    this.pnlReqAprv.Visible = false;

                    this.RadioButtonList1.Items[2].Attributes["class"] = "lblactive blink_me";
                    //this.RadioButtonList1.Items[2].Attributes["style"] = "background: #430000; display:block; ";
                    break;
                case "4":
                    this.pnlstatus.Visible = false;
                    this.pnlgatepass.Visible = false;
                    this.pnlapproval.Visible = false;
                    this.pnlaudit.Visible = true;
                    this.pnlaccount.Visible = false;
                    this.pnlReqAprv.Visible = false;
                    this.RadioButtonList1.Items[3].Attributes["class"] = "lblactive blink_me";
                    //this.RadioButtonList1.Items[3].Attributes["style"] = "background: #430000; display:block; ";
                    break;
                case "5":
                    this.pnlstatus.Visible = false;
                    this.pnlgatepass.Visible = false;
                    this.pnlapproval.Visible = false;
                    this.pnlaudit.Visible = false;
                    this.pnlaccount.Visible = true;
                    this.pnlReqAprv.Visible = false;

                    this.RadioButtonList1.Items[4].Attributes["class"] = "lblactive blink_me";
                    //this.RadioButtonList1.Items[3].Attributes["style"] = "background: #430000; display:block; ";
                    break;

            }


        }
        protected void txtdate_TextChanged(object sender, EventArgs e)
        {
            RadioButtonList1_SelectedIndexChanged(null, null);

            GetintoryData();
        }

        protected void lnkok_Click(object sender, EventArgs e)
        {
            this.GetintoryData();
        }
        private void GetintoryData()
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string userid = hst["usrid"].ToString();
            //string frmdate = this.txtdate.Text.Trim();
            string todate = this.txttodate.Text.Trim();
            //string catcode = this.ddlcatag.SelectedValue.ToString() + "%";


            DataSet ds2 = feaData.GetTransInfo(comcod, "[dbo].[SP_REPORT_TRANSFER_INTERFACE]", "TRANSFERINTERFACE", todate, "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {

                return;
            }



            this.RadioButtonList1.Items[0].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + Convert.ToInt32(ds2.Tables[6].Rows[0]["statuses"]) + "</div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'>Status</div></div></div>";
            this.RadioButtonList1.Items[1].Text = "<div class='circle-tile'><a><div class='circle-tile-heading red counter'>" + Convert.ToInt32(ds2.Tables[6].Rows[0]["reqapproval"]) + "</i></div></a><div class='circle-tile-content red'><div class='circle-tile-description text-faded'>Req Approval</div></div></div>";
            this.RadioButtonList1.Items[2].Text = "<div class='circle-tile'><a><div class='circle-tile-heading red counter'>" + Convert.ToInt32(ds2.Tables[6].Rows[0]["gatepass"]) + "</i></div></a><div class='circle-tile-content red'><div class='circle-tile-description text-faded'>Gate Pass</div></div></div>";
            this.RadioButtonList1.Items[3].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + Convert.ToInt32(ds2.Tables[6].Rows[0]["approval"]) + "</i></div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'>Approval</div></div></div>";
            this.RadioButtonList1.Items[4].Text = "<div class='circle-tile'><a><div class='circle-tile-heading orange counter'>" + Convert.ToInt32(ds2.Tables[6].Rows[0]["audited"]) + "</i></div></a><div class='circle-tile-content orange'><div class='circle-tile-description text-faded'>Audit</div></div></div>";
            this.RadioButtonList1.Items[5].Text = "<div class='circle-tile'><a><div class='circle-tile-heading red counter'>" + Convert.ToInt32(ds2.Tables[6].Rows[0]["account"]) + "</i></div></a><div class='circle-tile-content red'><div class='circle-tile-description text-faded'>Accounts Update</div></div></div>";

            Session["tbladdwrk"] = ds2.Tables[0];

            DataTable dt = new DataTable();
            DataView dv;
            //Status

            this.Data_Bind("gvstatus", ds2.Tables[0]);
            this.Data_Bind("gvreqaprv", ds2.Tables[1]);
            this.Data_Bind("gvgatepass", ds2.Tables[2]);
            this.Data_Bind("gvapproval", ds2.Tables[3]);
            this.Data_Bind("gvaudit", ds2.Tables[4]);
            this.Data_Bind("gvaccount", ds2.Tables[5]);

        }


        private void Data_Bind(string gv, DataTable dt)
        {
            switch (gv)
            {
                case "gvstatus":
                    this.gvstatus.DataSource = dt;
                    this.gvstatus.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;

                case "gvreqaprv":
                    this.gvreqaprv.DataSource = dt;
                    this.gvreqaprv.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;

                case "gvgatepass":
                    this.gvgatepass.DataSource = dt;
                    this.gvgatepass.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;

                case "gvapproval":
                    this.gvapproval.DataSource = dt;
                    this.gvapproval.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "gvaudit":
                    this.gvaudit.DataSource = dt;
                    this.gvaudit.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "gvaccount":
                    this.gvaccount.DataSource = dt;
                    this.gvaccount.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;

            }

        }

        protected void gvgatepass_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlnkchk = (HyperLink)e.Row.FindControl("lnkgpass");

                string mtrfno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mtreqno")).ToString();
                hlnkchk.NavigateUrl = "~/F_12_Inv/PurMTReqGatePass?Type=Entry&genno=" + mtrfno;


            }
        }
        protected void gvapproval_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlnkchk = (HyperLink)e.Row.FindControl("lnkapp");

                string getpasno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "getpno")).ToString();
                hlnkchk.NavigateUrl = "~/F_12_Inv/MaterialsTransfer?Type=Entry&genno=" + getpasno;


            }
        }
        protected void gvaudit_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlnkchk = (HyperLink)e.Row.FindControl("lnkad");

                string mtrfno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "trnno")).ToString();
                hlnkchk.NavigateUrl = "~/F_12_Inv/MaterialsTransfer?Type=Audit&genno=" + mtrfno;


            }
        }
        protected void lnkremovegp_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;


            string adno = ((Label)this.gvgatepass.Rows[Rowindex].FindControl("lbltrnnog")).Text.Trim();
            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "PrevMTRInfo", adno, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            bool result = log.XmlDataInsertReq(adno, ds1);

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Fail');", true);
                return;

            }

            bool result1 = feaData.UpdateTransInfo(comcod, "[dbo].[SP_REPORT_TRANSFER_INTERFACE]", "UPDATEMATTRANS", adno, "", "", "", "", "", "", "", "", "", "");
            if (!result1)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Remove failed!";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Successfully Removed";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
        }
        protected void lnkremoveap_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string gatepasno = ((Label)this.gvapproval.Rows[Rowindex].FindControl("lblgetpasno")).Text.Trim();


            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GETPURGERPASSINFO", gatepasno, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            bool result = log.XmlDataInsertReq(gatepasno, ds1);

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Fail');", true);
                return;

            }
            string ctype = this.getCompanyRef();
            bool result1 = feaData.UpdateTransInfo(comcod, "SP_REPORT_TRANSFER_INTERFACE", "UPGATEPASSMAT", gatepasno, ctype, "", "", "", "", "", "", "", "", "");
            if (!result1)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Remove failed!";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Successfully Removed";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
        }



        private string getCompanyRef()
        {
            string comcod = this.GetCompCode();
            string ctype = "";
            switch (comcod)
            {
                case "3101":
                case "3338":
                    ctype = "SingleGPA";
                    break;

                default:
                    ctype = "";
                    break;
            }
            return ctype;

        }

        protected void lnkremovead_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string trnno = ((Label)this.gvaudit.Rows[Rowindex].FindControl("lblgetrnno")).Text.Trim();

            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "PrevTransferInfo", trnno, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            bool result = log.XmlDataInsertReq(trnno, ds1);

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Fail');", true);
                return;

            }
            bool result1 = feaData.UpdateTransInfo(comcod, "[dbo].[SP_REPORT_TRANSFER_INTERFACE]", "UPAUDITMAT", trnno, "", "", "", "", "", "", "", "", "", "");
            if (!result1)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Remove failed!";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Successfully Removed";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
        }
        protected void lnkremoveac_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string trnno = ((Label)this.gvaccount.Rows[Rowindex].FindControl("lbltrannoac")).Text.Trim();

            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "PrevTransferInfo", trnno, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            bool result = log.XmlDataInsertReq(trnno, ds1);

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Fail');", true);
                return;

            }
            bool result1 = feaData.UpdateTransInfo(comcod, "[dbo].[SP_REPORT_TRANSFER_INTERFACE]", "UPAUDITEDMAT", trnno, "", "", "", "", "", "", "", "", "", "");
            if (!result1)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Remove failed!";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Successfully Removed";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
        }


        protected void gvstatus_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string status = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rstatus")).ToString().Trim();
                Label track = (Label)e.Row.FindControl("lgvtrack");
                //HyperLink hlnkgvgvmrfno = (HyperLink)e.Row.FindControl("lgvmtrno");
                HyperLink hlnkgvgvmrfno = (HyperLink)e.Row.FindControl("lblmtrfno");

                string mtreqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mtreqno")).ToString();
                switch (status)
                {
                    case "Req Approval":
                        track.Attributes.CssStyle.Add("color", "Red");
                        break;
                    case "Gate Pass":
                        track.Attributes.CssStyle.Add("color", "Maroon");
                        break;
                    case "Approval":
                        track.Attributes.CssStyle.Add("color", "blue");
                        break;
                    case "Audit":
                        track.Attributes.CssStyle.Add("color", "Green");
                        break;
                    default:
                        break;
                }
                hlnkgvgvmrfno.NavigateUrl = "~/F_14_Pro/RptPurchasetracking?Type=TransferReqtrk&mtreqno=" + mtreqno;

            }

        }

        protected void gvreqaprv_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void lnkremoverap_Click(object sender, EventArgs e)
        {

        }
    }
}