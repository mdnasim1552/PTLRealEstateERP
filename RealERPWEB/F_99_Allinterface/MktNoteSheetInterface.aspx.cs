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
    public partial class MktNoteSheetInterface : System.Web.UI.Page
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
                ((Label)this.Master.FindControl("lblTitle")).Text = "Grand Note sheet Interface";

                ((LinkButton)this.Master.FindControl("lnkPrint")).Visible = false;
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).Visible = false;

                this.GetNoteSheetInterface();
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

  
        protected void txtdate_TextChanged(object sender, EventArgs e)
        {
            this.GetNoteSheetInterface();
            RadioButtonList1_SelectedIndexChanged(null, null);

            
        }

        protected void lnkok_Click(object sender, EventArgs e)
        {
            this.GetNoteSheetInterface();
        }
        private void GetNoteSheetInterface()
        {

            try

            {

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetCompCode();
                string userid = hst["usrid"].ToString();
                string frmdate = this.txtfrmdate.Text.Trim();
                string todate = this.txttodate.Text.Trim();
                //string catcode = this.ddlcatag.SelectedValue.ToString() + "%";
                string mtrrf = "%" + this.txtmtrrf.Text.Trim().ToString() + "%";

                DataSet ds2 = feaData.GetTransInfo(comcod, "SP_REPORT_GRANDNOTESHEET_INTERFACE", "GETNOTESHEETINTERFACE", frmdate, todate , mtrrf, "", "", "", "", "");
                if (ds2 == null)
                {
                    return;
                }



                this.RadioButtonList1.Items[0].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + Convert.ToInt32(ds2.Tables[0].Rows[0]["statuses"]) + "</div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'>Status</div></div></div>";
                this.RadioButtonList1.Items[1].Text = "<div class='circle-tile'><a><div class='circle-tile-heading orange counter'>" + Convert.ToInt32(ds2.Tables[0].Rows[0]["authorize"]) + "</i></div></a><div class='circle-tile-content orange'><div class='circle-tile-description text-faded'>Authorize</div></div></div>";
                this.RadioButtonList1.Items[2].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + Convert.ToInt32(ds2.Tables[0].Rows[0]["recommend"]) + "</i></div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'>Recommended</div></div></div>";
                this.RadioButtonList1.Items[3].Text = "<div class='circle-tile'><a><div class='circle-tile-heading red counter'>" + Convert.ToInt32(ds2.Tables[0].Rows[0]["firstapv"]) + "</i></div></a><div class='circle-tile-content red'><div class='circle-tile-description text-faded'> Approval</div></div></div>";
                this.RadioButtonList1.Items[4].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + Convert.ToInt32(ds2.Tables[0].Rows[0]["finalapv"]) + "</i></div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'>Final Approval</div></div></div>";
                this.RadioButtonList1.Items[5].Text = "<div class='circle-tile'><a><div class='circle-tile-heading red counter'>" + Convert.ToInt32(ds2.Tables[0].Rows[0]["schupdate"]) + "</i></div></a><div class='circle-tile-content red'><div class='circle-tile-description text-faded'>Schedule Update</div></div></div>";

                //Session["tbladdwrk"] = ds2.Tables[0];

                //  DataTable dt = new DataTable();

                //Status

                //Authorize
                DataTable dt = ds2.Tables[1];
                DataTable dt1 = new DataTable();
                DataView dv;







                // Status
                dt1 = (DataTable)dt.Copy();
                dv = dt1.DefaultView;
                this.Data_Bind("gvstatus", dv.ToTable());

                //// Authorize
                dv = dt1.DefaultView;
                dv.RowFilter = ("authorizeid = ''");
                this.Data_Bind("gvauthorize", dv.ToTable());

                ////Recommendation
                //dv = dt1.DefaultView;
                //dv.RowFilter = ("authorizeid <>'' and recommendid=''");
                //this.Data_Bind("gvrecommend", dv.ToTable());


                ////First Approval
                //dv = dt1.DefaultView;
                //dv.RowFilter = ("recommendid <>'' and firstaprvid=''");
                //this.Data_Bind("gvfirstapp", dv.ToTable());


                ////Final Approval
                //dv = dt1.DefaultView;
                //dv.RowFilter = ("firstaprvid <>'' and finalaprvid=''");
                //this.Data_Bind("gvapproval", dv.ToTable());


                ////Schedule Update 
                //dv = dt1.DefaultView;
                //dv.RowFilter = ("firstaprvid <>'' and finalaprvid=''");
                //this.Data_Bind("gvscheduleup", dv.ToTable());
            }

            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);


            }


          

        }



        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {

            string view = this.RadioButtonList1.SelectedValue.ToString();
            switch (view)
            {
                case "0":
                    this.pnlstatus.Visible = true;
                    this.pnlreqchk.Visible = false;
                    this.pnlReqAprv.Visible = false;
                    this.pnlgatepass.Visible = false;
                    this.pnlapproval.Visible = false;
                    this.pnlaccount.Visible = false;
                    this.RadioButtonList1.Items[0].Attributes["class"] = "lblactive blink_me";
                    //this.RadioButtonList1.Items[0].Attributes["style"] = "background: #430000; display:block; ";
                    break;
                // mat req checked
                case "1":
                    this.pnlstatus.Visible = false;
                    this.pnlreqchk.Visible = true;
                    this.pnlReqAprv.Visible = false;
                    this.pnlgatepass.Visible = false;
                    this.pnlapproval.Visible = false;
                    this.pnlaccount.Visible = false;
                    //this.RadioButtonList1.Items[1].Attributes["style"] = "background: #430000; display:block; ";
                    this.RadioButtonList1.Items[1].Attributes["class"] = "lblactive blink_me";
                    break;
                // mat req approval
                case "2":
                    this.pnlstatus.Visible = false;
                    this.pnlreqchk.Visible = false;
                    this.pnlReqAprv.Visible = true;
                    this.pnlgatepass.Visible = false;
                    this.pnlapproval.Visible = false;
                    this.pnlaccount.Visible = false;
                    //this.RadioButtonList1.Items[1].Attributes["style"] = "background: #430000; display:block; ";
                    this.RadioButtonList1.Items[2].Attributes["class"] = "lblactive blink_me";
                    break;
                // gate pass
                case "3":
                    this.pnlstatus.Visible = false;
                    this.pnlreqchk.Visible = false;
                    this.pnlReqAprv.Visible = false;
                    this.pnlgatepass.Visible = true;
                    this.pnlapproval.Visible = false;
                    this.pnlaccount.Visible = false;
                    this.RadioButtonList1.Items[3].Attributes["class"] = "lblactive blink_me";
                    //this.RadioButtonList1.Items[2].Attributes["style"] = "background: #430000; display:block; ";
                    break;
                //gate pass Approval
                case "4":
                    this.pnlstatus.Visible = false;
                    this.pnlreqchk.Visible = false;
                    this.pnlReqAprv.Visible = false;
                    this.pnlgatepass.Visible = false;
                    this.pnlapproval.Visible = true;
                    this.pnlaccount.Visible = false;
                    this.RadioButtonList1.Items[4].Attributes["class"] = "lblactive blink_me";
                    //this.RadioButtonList1.Items[3].Attributes["style"] = "background: #430000; display:block; ";
                    break;

                // audit
                case "5":
                    this.pnlstatus.Visible = false;
                    this.pnlreqchk.Visible = false;
                    this.pnlReqAprv.Visible = false;
                    this.pnlgatepass.Visible = false;
                    this.pnlapproval.Visible = false;
                    this.pnlaccount.Visible = true;
                    this.RadioButtonList1.Items[5].Attributes["class"] = "lblactive blink_me";
                    //this.RadioButtonList1.Items[3].Attributes["style"] = "background: #430000; display:block; ";
                    break;


            }


        }

        private void Data_Bind(string gv, DataTable dt)
        {
            switch (gv)
            {
                case "gvstatus":
                    this.gvstatus.DataSource = dt;
                    this.gvstatus.DataBind();
                   
                    break;

                case "gvauthorize":
                    this.gvauthorize.DataSource = dt;
                    this.gvauthorize.DataBind();
                  
                    break;

                case "gvreqaprv":
                    this.gvreqaprv.DataSource = dt;
                    this.gvreqaprv.DataBind();
                   
                    break;

                case "gvgatepass":
                    this.gvgatepass.DataSource = dt;
                    this.gvgatepass.DataBind();
                   
                    break;

                case "gvapproval":
                    this.gvapproval.DataSource = dt;
                    this.gvapproval.DataBind();
                   
                    break;
                
                case "gvaccount":
                    this.gvaccount.DataSource = dt;
                    this.gvaccount.DataBind();
                   
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
                string status = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "noteshtst")).ToString().Trim();
                Label lblgvstatus = (Label)e.Row.FindControl("lblgvstatus");
                
                switch (status)
                {
                    case "Authorized":
                        lblgvstatus.Attributes.CssStyle.Add("color", "Purple");
                        break;
                    case "Recommended":
                        lblgvstatus.Attributes.CssStyle.Add("color", "Maroon");
                        break;
                    case "Approval":
                        lblgvstatus.Attributes.CssStyle.Add("color", "blue");
                        break;
                    case "Final Approval'":
                        lblgvstatus.Attributes.CssStyle.Add("color", "Green");
                        break;
                    default:
                        break;
                }
             //   hlnkgvgvmrfno.NavigateUrl = "~/F_14_Pro/RptPurchasetracking?Type=TransferReqtrk&mtreqno=" + mtreqno;

            }

        }

        protected void gvreqaprv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{

            //    HyperLink hlnkreqap = (HyperLink)e.Row.FindControl("lnkreqaprv");

            //    string mtreqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mtreqno")).ToString();
            //    //hlnkreqap.NavigateUrl = "~/F_12_Inv/MaterialsTransfer?Type=Entry&genno=" + getpasno;
            //    hlnkreqap.NavigateUrl = "~/F_12_Inv/PurMTReqEntry?Type=ReqApproval&prjcode=" + "" + "&genno=" + mtreqno;

            //}
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

       
            

        protected void lnkremoveaut_Click(object sender, EventArgs e)
        {

            //    string comcod = this.GetCompCode();
            //    int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            //    string mtreqno = ((Label)this.gvreqchk.Rows[Rowindex].FindControl("lblmtreqnochk")).Text.Trim();

            //    DataSet ds1 = feaData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "PrevMTRInfo", mtreqno, "", "", "", "", "", "", "", "");
            //    if (ds1 == null)
            //        return;

            //    bool result = log.XmlDataInsertReq(mtreqno, ds1);

            //    if (!result)
            //    {
            //        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Updated Fail');", true);
            //        return;
            //    }
            //    bool result1 = feaData.UpdateTransInfo(comcod, "[dbo].[SP_REPORT_TRANSFER_INTERFACE]", "DELETEMTRTRANS", mtreqno, "", "", "", "", "", "", "", "", "", "");
            //    if (!result1)
            //    {
            //        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Removed Fail .. !!');", true);
            //        return;
            //    }
            //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Successfully Removed');", true);
            //    this.RadioButtonList1_SelectedIndexChanged(null, null);
            //}
        }

        protected void lnkauthorize_Click(object sender, EventArgs e)
        {

        }

        protected void gvauthorize_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];               
                string userid = hst["usrid"].ToString();

                LinkButton lnkauthorize = (LinkButton)e.Row.FindControl("lnkauthorize");
                HyperLink hlnkreqchk = (HyperLink)e.Row.FindControl("hlnkView");
                string noteshtid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "noteshtid")).ToString();
                string clusteruid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "clusteruid")).ToString();
                if (userid == clusteruid)
                {
                    lnkauthorize.Attributes["style"] = "color:green;";
                    lnkauthorize.Enabled = true;
                }
                else 
                
                {
                    lnkauthorize.Attributes["style"] = "color:red;";
                    lnkauthorize.Enabled = false;

                }

                hlnkreqchk.NavigateUrl = "~/F_22_Sal/MktGrandNoteSheet?Type=Entry&genno=" + noteshtid;
            }
        }
    }
}