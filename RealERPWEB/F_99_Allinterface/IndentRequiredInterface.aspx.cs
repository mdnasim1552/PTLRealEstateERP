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
    public partial class IndentRequiredInterface : System.Web.UI.Page
    {
        ProcessAccess feaData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                this.GetFromDate();
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                ((Label)this.Master.FindControl("lblTitle")).Text = "Indent Required Interface";

                ((LinkButton)this.Master.FindControl("lnkPrint")).Visible = false;
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).Visible = false;
                this.GetIndentRequirdData();
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
                default:
                    Hashtable hst = (Hashtable)Session["tblLogin"];
                    this.txtfrmdate.Text = Convert.ToDateTime(hst["opndate"].ToString()).AddDays(1).ToString("dd-MMM-yyyy");
                    break;
            }
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.GetIndentRequirdData();
            string view = this.RadioButtonList1.SelectedValue.ToString();
            switch (view)
            {
                // All Status
                case "0":
                    this.pnlstatus.Visible = true;
                    this.pnlreqchk.Visible = false;
                    this.pnlReqAprv.Visible = false;
                    this.pnlgatepass.Visible = false;
                    this.pnlcomplete.Visible = false;
                    this.GetIndentRequirdData();



                    this.RadioButtonList1.Items[0].Attributes["class"] = "lblactive blink_me";
                    //this.RadioButtonList1.Items[0].Attributes["style"] = "background: #430000; display:block; ";
                    break;
                // mat req checked
                case "1":
                    this.pnlstatus.Visible = false;
                    this.pnlreqchk.Visible = true;
                    this.pnlReqAprv.Visible = false;
                    this.pnlgatepass.Visible = false;
                    this.pnlcomplete.Visible = false;
                    GetIndentRequirdData();
                    //this.RadioButtonList1.Items[1].Attributes["style"] = "background: #430000; display:block; ";
                    this.RadioButtonList1.Items[1].Attributes["class"] = "lblactive blink_me";
                    break;
                // mat req approval
                case "2":
                    this.pnlstatus.Visible = false;
                    this.pnlreqchk.Visible = false;
                    this.pnlReqAprv.Visible = true;
                    this.pnlgatepass.Visible = false;
                    this.pnlcomplete.Visible = false;
                     GetIndentRequirdData();
                    //this.RadioButtonList1.Items[1].Attributes["style"] = "background: #430000; display:block; ";
                    this.RadioButtonList1.Items[2].Attributes["class"] = "lblactive blink_me";
                    break;
                // Issues
                case "3":
                    this.pnlstatus.Visible = false;
                    this.pnlreqchk.Visible = false;
                    this.pnlReqAprv.Visible = false;
                    this.pnlgatepass.Visible = true;
                    this.pnlcomplete.Visible = false;
                    this.RadioButtonList1.Items[3].Attributes["class"] = "lblactive blink_me";
                    //this.RadioButtonList1.Items[2].Attributes["style"] = "background: #430000; display:block; ";
                    break;
                case "4":
                    this.pnlstatus.Visible = false;
                    this.pnlreqchk.Visible = false;
                    this.pnlReqAprv.Visible = false;
                    this.pnlgatepass.Visible = false;
                    this.pnlcomplete.Visible = true;
                    this.RadioButtonList1.Items[4].Attributes["class"] = "lblactive blink_me";
                    //this.RadioButtonList1.Items[2].Attributes["style"] = "background: #430000; display:block; ";
                    break;
            }


        }

        private void GetIndentRequirdData()
        {
            try
            {

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetCompCode();
                string userid = hst["usrid"].ToString();
                string userrole = hst["usrid"].ToString();
                string frmdate = this.txtfrmdate.Text.Trim();
                string todate = this.txttodate.Text.Trim();
                string mtrrf = "%" + this.txtmtrrf.Text.Trim().ToString() + "%";

                DataSet ds2 = feaData.GetTransInfo(comcod, "[dbo].[SP_REPORT_INDENT_STATUS]", "GETINDENTREQUIREDINTERFACEDATA", frmdate, todate, mtrrf, userrole, userid, "", "", "", "");
                if (ds2 == null)
                {
                    return;
                }


                this.RadioButtonList1.Items[0].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + Convert.ToInt32(ds2.Tables[2].Rows[0]["statuses"]) + "</div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'>Status</div></div></div>";
                this.RadioButtonList1.Items[1].Text = "<div class='circle-tile'><a><div class='circle-tile-heading orange counter'>" + Convert.ToInt32(ds2.Tables[2].Rows[0]["hodchecked"]) + "</i></div></a><div class='circle-tile-content orange'><div class='circle-tile-description text-faded'>HOD Checked</div></div></div>";
                this.RadioButtonList1.Items[2].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + Convert.ToInt32(ds2.Tables[2].Rows[0]["hrapproval"]) + "</i></div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'>HR/Admin Approval</div></div></div>";
                this.RadioButtonList1.Items[3].Text = "<div class='circle-tile'><a><div class='circle-tile-heading red counter'>" + Convert.ToInt32(ds2.Tables[2].Rows[0]["pendingissues"]) + "</i></div></a><div class='circle-tile-content red'><div class='circle-tile-description text-faded'>Pending Issue</div></div></div>";
                this.RadioButtonList1.Items[4].Text = "<div class='circle-tile'><a><div class='circle-tile-heading green counter'>" + Convert.ToInt32(ds2.Tables[2].Rows[0]["complete"]) + "</i></div></a><div class='circle-tile-content green'><div class='circle-tile-description text-faded'>Complete</div></div></div>";

                Session["tbladdwrk"] = ds2.Tables[0];
                Session["tblstepwise"] = ds2.Tables[1];
                
                this.gv_IndRequired.DataSource = ds2.Tables[1];
                this.gv_IndRequired.DataBind();

                DataTable dt1 = new DataTable();
                DataView view1 = new DataView();


                view1.Table = ds2.Tables[1];
                view1.RowFilter = "steptype= 'Entry'";
                dt1 = view1.ToTable();
                this.gv_hodChecked.DataSource = dt1;
                this.gv_hodChecked.DataBind();


                DataTable dt = new DataTable();
                DataView view = new DataView();
                

                view.Table = ds2.Tables[1];
                view.RowFilter = "steptype= 'Checked'";
                dt = view.ToTable();
                this.gv_hradminapproval.DataSource = dt;
                this.gv_hradminapproval.DataBind();


                DataTable dt2 = new DataTable();
                DataView view2 = new DataView();


                view2.Table = ds2.Tables[1];
                view2.RowFilter = "steptype= 'Approve' ";
                dt2 = view2.ToTable();
                this.gv_Pending.DataSource = dt2;
                this.gv_Pending.DataBind();
                //Status

                //this.Data_Bind("gvstatus", ds2.Tables[0]);
                //this.Data_Bind("gvreqchk", ds2.Tables[1]);
                //this.Data_Bind("gvreqaprv", ds2.Tables[2]);
                //this.Data_Bind("gvgatepass", ds2.Tables[3]);
                //this.Data_Bind("gvapproval", ds2.Tables[4]);
                //this.Data_Bind("gvaudit", ds2.Tables[5]);
                //this.Data_Bind("gvaccount", ds2.Tables[6]);
            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }



       


        private void GetAllIndRequired()
        {

        }

        protected void lnkok_Click(object sender, EventArgs e)
        {

            this.GetIndentRequirdData();


        }

        protected void gv_IndRequired_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink = (HyperLink)e.Row.FindControl("hybtnidentlink");

                string issueno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "issueno")).ToString().Trim();

                hlink.NavigateUrl = "~/F_12_Inv/IndentMaterialRequired?Type=Entry&genno=" + issueno;


            }
        }

        protected void btndeleteIndent_Click(object sender, EventArgs e)
        {
            try
            {
                string comcod = this.GetCompCode();
                GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
                int index = row.RowIndex;
                string issueno = ((Label)this.gv_IndRequired.Rows[index].FindControl("lblgvissueno")).Text.ToString();
                bool result = feaData.UpdateTransInfo(comcod, "SP_REPORT_INDENT_STATUS", "GETINDENTREQUIREDDELETE", issueno, "", "", "", "", "", "", "", "");

                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Delete Fail..!!');", true);
                    return;
                }


                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Batch Deleted Successfully');", true);
                this.GetIndentRequirdData();

            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }

        protected void gv_hodChecked_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink = (HyperLink)e.Row.FindControl("hybtnhodidentlink");

                string issueno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "issueno")).ToString().Trim();

                hlink.NavigateUrl = "~/F_12_Inv/IndentMaterialRequired?Type=Checked&genno=" + issueno;


            }
        }
       

        protected void gv_hradminapproval_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink = (HyperLink)e.Row.FindControl("hybtnadidentlink");

                string issueno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "issueno")).ToString().Trim();

                hlink.NavigateUrl = "~/F_12_Inv/IndentMaterialRequired?Type=Approve&genno=" + issueno;


            }
        }
    }
}