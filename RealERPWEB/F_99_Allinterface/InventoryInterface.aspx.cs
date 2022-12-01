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


                this.GetFromDate();
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

        private void GetFromDate()
        {

            string comcod = this.GetCompCode();
            string date = System.DateTime.Today.ToString("dd-MMM-yyyy");

            switch (comcod)
            {
                case "3348": // chl  
                case "3101": // pintech  
                case "1205": // p2p  
                case "3351": // p2p  
                case "3352": // p2p  
                case "3367": // epic  
 
                    this.txtfrmdate.Text = Convert.ToDateTime(date.ToString()).AddMonths(-3).ToString("dd-MMM-yyyy");
                    break;


                default:

                    Hashtable hst = (Hashtable)Session["tblLogin"];
                    this.txtfrmdate.Text = Convert.ToDateTime(hst["opndate"].ToString()).AddDays(1).ToString("dd-MMM-yyyy");
                    break;
            }
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
                    this.pnlmgcheck.Visible = false;
                    this.pnlreqchk.Visible = false;
                    this.pnlReqAprv.Visible = false;
                    this.pnlgatepass.Visible = false;
                    this.pnlapproval.Visible = false;
                    this.pnlaudit.Visible = false;
                    this.pnlaccount.Visible = false;
                    this.RadioButtonList1.Items[0].Attributes["class"] = "lblactive blink_me";
                    //this.RadioButtonList1.Items[0].Attributes["style"] = "background: #430000; display:block; ";
                    break;
                
                    // MGT / HOD req checked
                case "1":
                    this.pnlstatus.Visible = false;
                    this.pnlmgcheck.Visible = true;
                    this.pnlreqchk.Visible = false;
                    this.pnlReqAprv.Visible = false;
                    this.pnlgatepass.Visible = false;
                    this.pnlapproval.Visible = false;
                    this.pnlaudit.Visible = false;
                    this.pnlaccount.Visible = false;
                    //this.RadioButtonList1.Items[1].Attributes["style"] = "background: #430000; display:block; ";
                    this.RadioButtonList1.Items[1].Attributes["class"] = "lblactive blink_me";
                    break;
                    
                // mat req checked
                case "2":
                    this.pnlstatus.Visible = false;
                    this.pnlmgcheck.Visible = false;
                    this.pnlreqchk.Visible = true;
                    this.pnlReqAprv.Visible = false;
                    this.pnlgatepass.Visible = false;
                    this.pnlapproval.Visible = false;
                    this.pnlaudit.Visible = false;
                    this.pnlaccount.Visible = false;
                    //this.RadioButtonList1.Items[1].Attributes["style"] = "background: #430000; display:block; ";
                    this.RadioButtonList1.Items[2].Attributes["class"] = "lblactive blink_me";
                    break;
                // mat req approval
                case "3":
                    this.pnlstatus.Visible = false;
                    this.pnlmgcheck.Visible = false;
                    this.pnlreqchk.Visible = false;
                    this.pnlReqAprv.Visible = true;
                    this.pnlgatepass.Visible = false;
                    this.pnlapproval.Visible = false;
                    this.pnlaudit.Visible = false;
                    this.pnlaccount.Visible = false;
                    //this.RadioButtonList1.Items[1].Attributes["style"] = "background: #430000; display:block; ";
                    this.RadioButtonList1.Items[3].Attributes["class"] = "lblactive blink_me";
                    break;
                // gate pass
                case "4":
                    this.pnlstatus.Visible = false;
                    this.pnlmgcheck.Visible = false;
                    this.pnlreqchk.Visible = false;
                    this.pnlReqAprv.Visible = false;
                    this.pnlgatepass.Visible = true;
                    this.pnlapproval.Visible = false;
                    this.pnlaudit.Visible = false;
                    this.pnlaccount.Visible = false;
                    this.RadioButtonList1.Items[4].Attributes["class"] = "lblactive blink_me";
                    //this.RadioButtonList1.Items[2].Attributes["style"] = "background: #430000; display:block; ";
                    break;
                //gate pass Approval
                case "5":
                    this.pnlstatus.Visible = false;
                    this.pnlmgcheck.Visible = false;
                    this.pnlreqchk.Visible = false;
                    this.pnlReqAprv.Visible = false;
                    this.pnlgatepass.Visible = false;
                    this.pnlapproval.Visible = true;
                    this.pnlaudit.Visible = false;
                    this.pnlaccount.Visible = false;
                    this.RadioButtonList1.Items[5].Attributes["class"] = "lblactive blink_me";
                    //this.RadioButtonList1.Items[3].Attributes["style"] = "background: #430000; display:block; ";
                    break;

                // audit
                case "6":
                    this.pnlstatus.Visible = false;
                    this.pnlmgcheck.Visible = false;
                    this.pnlreqchk.Visible = false;
                    this.pnlReqAprv.Visible = false;
                    this.pnlgatepass.Visible = false;
                    this.pnlapproval.Visible = false;
                    this.pnlaudit.Visible = true;
                    this.pnlaccount.Visible = false;
                    this.RadioButtonList1.Items[6].Attributes["class"] = "lblactive blink_me";
                    //this.RadioButtonList1.Items[3].Attributes["style"] = "background: #430000; display:block; ";
                    break;
                case "7":
                    this.pnlstatus.Visible = false;
                    this.pnlmgcheck.Visible = false;
                    this.pnlreqchk.Visible = false;
                    this.pnlReqAprv.Visible = false;
                    this.pnlgatepass.Visible = false;
                    this.pnlapproval.Visible = false;
                    this.pnlaudit.Visible = false;
                    this.pnlaccount.Visible = true;
                    this.RadioButtonList1.Items[7].Attributes["class"] = "lblactive blink_me";
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
            string frmdate = this.txtfrmdate.Text.Trim();
            string todate = this.txttodate.Text.Trim();
            //string catcode = this.ddlcatag.SelectedValue.ToString() + "%";
            string mtrrf = "%" + this.txtmtrrf.Text.Trim().ToString() + "%";

            DataSet ds2 = feaData.GetTransInfo(comcod, "[dbo].[SP_REPORT_TRANSFER_INTERFACE]", "TRANSFERINTERFACE", frmdate, mtrrf, todate, "", "", "", "", "", "");
            if (ds2 == null)
            {
                return;
            }
            string gatePass = "",  approval = "", Audit = "Audit";
            switch (comcod)
            {

                //case "3101":
                case "1205":
                case "3351":
                case "3352":
                    gatePass = "Trans/Gatepass";
                    approval = "Received By";
                    break;

                //case "3101":
                case "3367":
                    gatePass = "Gate Pass";
                    approval = "Received";
                    break;

                case"3370":
                    gatePass = "Gate Pass";
                    approval = "Received";
                    Audit = "Approval";
                    break;     
                    
                default:
                    gatePass = "Gate Pass";
                    approval = "Approval";
                    break;
            }

            this.RadioButtonList1.Items[0].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + Convert.ToInt32(ds2.Tables[8].Rows[0]["statuses"]) + "</div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'>Status</div></div></div>";
            this.RadioButtonList1.Items[1].Text = "<div class='circle-tile'><a><div class='circle-tile-heading red counter'>" + Convert.ToInt32(ds2.Tables[8].Rows[0]["mgchecked"]) + "</i></div></a><div class='circle-tile-content red'><div class='circle-tile-description text-faded'>Admin Checked</div></div></div>";
            this.RadioButtonList1.Items[2].Text = "<div class='circle-tile'><a><div class='circle-tile-heading orange counter'>" + Convert.ToInt32(ds2.Tables[8].Rows[0]["reqchecked"]) + "</i></div></a><div class='circle-tile-content orange'><div class='circle-tile-description text-faded'>Req Checked</div></div></div>";
            this.RadioButtonList1.Items[3].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + Convert.ToInt32(ds2.Tables[8].Rows[0]["reqapproval"]) + "</i></div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'>Req Approval</div></div></div>";
            this.RadioButtonList1.Items[4].Text = "<div class='circle-tile'><a><div class='circle-tile-heading red counter'>" + Convert.ToInt32(ds2.Tables[8].Rows[0]["gatepass"]) + "</i></div></a><div class='circle-tile-content red'><div class='circle-tile-description text-faded'>" + gatePass + "</div></div></div>";
            this.RadioButtonList1.Items[5].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + Convert.ToInt32(ds2.Tables[8].Rows[0]["approval"]) + "</i></div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'>" + approval + "</div></div></div>";
            this.RadioButtonList1.Items[6].Text = "<div class='circle-tile'><a><div class='circle-tile-heading orange counter'>" + Convert.ToInt32(ds2.Tables[8].Rows[0]["audited"]) + "</i></div></a><div class='circle-tile-content orange'><div class='circle-tile-description text-faded'>"+ Audit + "</div></div></div>";
            this.RadioButtonList1.Items[7].Text = "<div class='circle-tile'><a><div class='circle-tile-heading red counter'>" + Convert.ToInt32(ds2.Tables[8].Rows[0]["account"]) + "</i></div></a><div class='circle-tile-content red'><div class='circle-tile-description text-faded'>Accounts Update</div></div></div>";

            Session["tbladdwrk"] = ds2.Tables[0];

            DataTable dt = new DataTable();

            //Status

            this.Data_Bind("gvstatus", ds2.Tables[0]);
            this.Data_Bind("gvmgchecked", ds2.Tables[1]);
            this.Data_Bind("gvreqchk", ds2.Tables[2]);
            this.Data_Bind("gvreqaprv", ds2.Tables[3]);
            this.Data_Bind("gvgatepass", ds2.Tables[4]);
            this.Data_Bind("gvapproval", ds2.Tables[5]);
            this.Data_Bind("gvaudit", ds2.Tables[6]);
            this.Data_Bind("gvaccount", ds2.Tables[7]);

        }


        private void Data_Bind(string gv, DataTable dt)
        {
            switch (gv)
            {
                case "gvstatus":
                    this.gvstatus.DataSource = dt;
                    this.gvstatus.DataBind();
                    if (GetCompCode() == "3370")
                    {
                        this.gvstatus.Columns[8].Visible = false;
                    }
                    if (dt.Rows.Count == 0)
                        return;
                    break;

                case "gvmgchecked":
                    this.gvmgchecked.DataSource = dt;
                    this.gvmgchecked.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;
                
                case "gvreqchk":
                    this.gvreqchk.DataSource = dt;
                    this.gvreqchk.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;

                case "gvreqaprv":
                    this.gvreqaprv.DataSource = dt;
                    this.gvreqaprv.DataBind();
                    if (GetCompCode() == "3370")
                    {
                        this.gvreqaprv.Columns[7].Visible = false;
                    }
                    if (dt.Rows.Count == 0)
                        return;
                    break;

                case "gvgatepass":
                    this.gvgatepass.DataSource = dt;
                    this.gvgatepass.DataBind();
                    if (GetCompCode() == "3370")
                    {
                        this.gvgatepass.Columns[8].Visible = false;
                    }
                    if (dt.Rows.Count == 0)
                        return;
                    break;

                case "gvapproval":
                    this.gvapproval.DataSource = dt;
                    this.gvapproval.DataBind();
                    if (GetCompCode() == "3370")
                    {
                        this.gvapproval.Columns[8].Visible = false;
                    }
                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "gvaudit":
                    this.gvaudit.DataSource = dt;
                    this.gvaudit.DataBind();
                    if (GetCompCode() == "3370")
                    {
                        this.gvaudit.Columns[8].Visible = false;
                    }
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
                HyperLink hlnkreqedit = (HyperLink)e.Row.FindControl("lnkgpareqedit");

                string mtrfno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mtreqno")).ToString();
                string frmprjcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "tfpactcode")).ToString();

                string comcod = this.GetCompCode();
                switch (comcod)
                {
                    case "3340":
                    case "3101":
                        hlnkreqedit.Visible = true;
                        break;
                    default:
                        hlnkreqedit.Visible = false;
                        break;
                }

                //PurMTReqEntry?Type=Entry&prjcode=&genno=
                hlnkchk.NavigateUrl = "~/F_12_Inv/PurMTReqGatePass?Type=Entry&genno=" + mtrfno + "&frmpactcode=" + frmprjcode;
                hlnkreqedit.NavigateUrl = "~/F_12_Inv/PurMTReqEntry?Type=ReqEdit&prjcode=&genno=" + mtrfno;

            }
        }
        protected void gvapproval_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlnkchk = (HyperLink)e.Row.FindControl("lnkapp");
                HyperLink hlnkapedit = (HyperLink)e.Row.FindControl("lnkgpapdit");
                string comcod = this.GetCompCode();

                switch (comcod)
                {
                    case "3340":
                    case "3101":
                        hlnkapedit.Visible = true;
                        break;
                    default:
                        hlnkapedit.Visible = false;
                        break;
                }

                string getpasno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "getpno")).ToString();
                string mtreqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mtreqno")).ToString();
                string getpref = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "getpref")).ToString();

                hlnkchk.NavigateUrl = "~/F_12_Inv/MaterialsTransfer?Type=Entry&genno=" + getpasno;
                hlnkapedit.NavigateUrl = "~/F_12_Inv/PurMTReqGatePass?Type=GpaEdit&genno=" + mtreqno + "&getpasno=" + getpasno + "&gpref=" + getpref;
            }
        }
        protected void gvaudit_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlnkchk = (HyperLink)e.Row.FindControl("lnkad");
                HyperLink hlnkRecv = (HyperLink)e.Row.FindControl("HyInprPrintRecv");

                string mtrfno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "trnno")).ToString();
                hlnkchk.NavigateUrl = "~/F_12_Inv/MaterialsTransfer?Type=Audit&genno=" + mtrfno;
                hlnkRecv.NavigateUrl = "~/F_12_Inv/MaterialsTransfer?Type=Entry&genno=" + mtrfno + "&pType=" + "MRTRECV";


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
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Update Fail .. !!');", true);
                return;

            }
            string callType = this.getGpaDelCallType();

            bool result1 = feaData.UpdateTransInfo(comcod, "[dbo].[SP_REPORT_TRANSFER_INTERFACE]", callType, adno, "", "", "", "", "", "", "", "", "", "");
            if (!result1)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Removed Fail .. !!');", true);
                return;
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Successfully Removed');", true);
            this.RadioButtonList1_SelectedIndexChanged(null, null);

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
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Updated Failed .. !!');", true);

                return;

            }
            string ctype = this.getCompanyRef();
            bool result1 = feaData.UpdateTransInfo(comcod, "SP_REPORT_TRANSFER_INTERFACE", "UPGATEPASSMAT", gatepasno, ctype, "", "", "", "", "", "", "", "", "");
            if (!result1)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Removed Failed .. !!');", true);
                return;
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Successfully Removed');", true);
            this.RadioButtonList1_SelectedIndexChanged(null, null);
        }


        private string getGpaDelCallType()
        {
            string comcod = this.GetCompCode();
            string callType = "";
            switch (comcod)
            {
                // todo for  mrtreq approval part 

                case "3101": // pintech
                case "3367": // epic
                case "1205": // p2p
                case "3351": // p2p
                case "3352": // p2p
                    callType = "DELETEMTRREQAPRV";
                    break;

                // todo for skip mrtreq approval part
                default:
                    callType = "DELETEMTRTRANS";
                    break;
            }
            return callType;
        }

        private string getCompanyRef()
        {
            string comcod = this.GetCompCode();
            string ctype = "";
            switch (comcod)
            {
                //case "3101":
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
                    case "HOD Checked":
                        track.Attributes.CssStyle.Add("color", "red");
                        break;                    
                    case "Req Checked":
                        track.Attributes.CssStyle.Add("color", "Green");
                        break;
                    case "Req Approval":
                        track.Attributes.CssStyle.Add("color", "Purple");
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
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlnkreqap = (HyperLink)e.Row.FindControl("lnkreqaprv");

                string mtreqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mtreqno")).ToString();
                //hlnkreqap.NavigateUrl = "~/F_12_Inv/MaterialsTransfer?Type=Entry&genno=" + getpasno;
                hlnkreqap.NavigateUrl = "~/F_12_Inv/PurMTReqEntry?Type=ReqApproval&prjcode=" + "" + "&genno=" + mtreqno;

            }
        }

        protected void lnkremoverap_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;


            string mtreqno = ((Label)this.gvreqaprv.Rows[Rowindex].FindControl("lbltrnnorap")).Text.Trim();
            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "PrevMTRInfo", mtreqno, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            bool result = log.XmlDataInsertReq(mtreqno, ds1);

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Updated Fail .. !!');", true);
                return;

            }
            string callType = this.getReqCheckCallType();

            bool result1 = feaData.UpdateTransInfo(comcod, "[dbo].[SP_REPORT_TRANSFER_INTERFACE]", callType, mtreqno, "", "", "", "", "", "", "", "", "");
            if (!result1)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Removed Fail .. !!');", true);
                return;
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Successfully Removed');", true);
            this.RadioButtonList1_SelectedIndexChanged(null, null);
            
        }

        private string getReqCheckCallType()
        {
            string comcod = this.GetCompCode();
            string callType = "";
            switch (comcod)
            {
                // todo for  mrtreq approval part 

                case "3101": // pintech
                case "3367": // epic
                case "1205": // p2p
                case "3351": // p2p
                case "3352": // p2p
                    callType = "DELETEMTRREQCHECK";
                    break;

                // todo for skip mrtreq approval part
                default:
                    callType = "DELETEMTRTRANS";
                    break;
            }
            return callType;
        }

        protected void lnkbtnEdit_Click(object sender, EventArgs e)
        {
            //string url = "PurMTReqGatePass?Type=Entry";
            //string comcod = this.GetCompCode();
            //((Label)this.Master.FindControl("lblprintstk")).Text = "";
            //DataRow[] dr1 = ASTUtility.PagePermission1(url, (DataSet)Session["tblusrlog"]);
            //if (!Convert.ToBoolean(dr1[0]["delete"]))
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
            //    return;
            //}

            //int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            //string mtrno = ((Label)this.gvgatepass.Rows[RowIndex].FindControl("lbltrnnog")).Text.Trim();
            //string mrefno = ((Label)this.gvgatepass.Rows[RowIndex].FindControl("lblmtrdgpmanual")).Text.Trim();
            //string mtrdat = ((Label)this.gvgatepass.Rows[RowIndex].FindControl("lblmtrdgp")).Text.Trim();



        }

        protected void lbtnResFooterTotal_Click(object sender, EventArgs e)
        {

        }

        protected void gvMtrReInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gvreqchk_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink lnkreqchk = (HyperLink)e.Row.FindControl("lnkreqchk");
                string mtreqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mtreqno")).ToString();
                lnkreqchk.NavigateUrl = "~/F_12_Inv/PurMTReqEntry?Type=ReqChecked&prjcode=" + "" + "&genno=" + mtreqno;
            }

        }

        protected void lnkremovechk_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string mtreqno = ((Label)this.gvreqchk.Rows[Rowindex].FindControl("lblmtreqnochk")).Text.Trim();

            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "PrevMTRInfo", mtreqno, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            bool result = log.XmlDataInsertReq(mtreqno, ds1);

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Updated Fail');", true);
                return;
            }

            bool result1 = feaData.UpdateTransInfo(comcod, "[dbo].[SP_REPORT_TRANSFER_INTERFACE]", "DELETEMTRREQCHECK", mtreqno, "", "", "", "", "", "", "", "", "", "");
            if (!result1)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Removed Fail .. !!');", true);
                return;
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Successfully Removed');", true);
            this.RadioButtonList1_SelectedIndexChanged(null, null);
        }

        protected void gvmgchecked_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink lnkmgcheked = (HyperLink)e.Row.FindControl("lnkmgcheked");
                string mtreqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mtreqno")).ToString();
                lnkmgcheked.NavigateUrl = "~/F_12_Inv/PurMTReqEntry?Type=MgtChecked&prjcode=" + "" + "&genno=" + mtreqno;
            }
        }

        protected void lnkremovemg_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string mtreqno = ((Label)this.gvreqchk.Rows[Rowindex].FindControl("lblmtreqnochk")).Text.Trim();

            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "PrevMTRInfo", mtreqno, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            bool result = log.XmlDataInsertReq(mtreqno, ds1);

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Updated Fail');", true);
                return;
            }
            bool result1 = feaData.UpdateTransInfo(comcod, "[dbo].[SP_REPORT_TRANSFER_INTERFACE]", "DELETEMTRTRANS", mtreqno, "", "", "", "", "", "", "", "", "", "");
            if (!result1)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Removed Fail .. !!');", true);
                return;
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Successfully Removed');", true);
            this.RadioButtonList1_SelectedIndexChanged(null, null);
        }
    }
}