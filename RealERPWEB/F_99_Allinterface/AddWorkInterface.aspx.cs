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
    public partial class AddWorkInterface : System.Web.UI.Page
    {
        ProcessAccess feaData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                ((Label)this.Master.FindControl("lblTitle")).Text = "Additional Work Interface";

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
            this.GetAddWrkData();
            //this.Data_Bind();
            string view = this.RadioButtonList1.SelectedValue.ToString();
            switch (view)
            {
                case "0":
                    this.pnlinitial.Visible = true;
                    this.pnlcheck.Visible = false;
                    this.pnl1stApp.Visible = false;
                    this.pnl2ndApp.Visible = false;
                    this.pnlaudit.Visible = false;
                    this.pnlapproval.Visible = false;
                    this.RadioButtonList1.Items[0].Attributes["class"] = "lblactive blink_me";
                    //this.RadioButtonList1.Items[0].Attributes["style"] = "background: #430000; display:block; ";
                    break;

                case "1":
                    this.pnlinitial.Visible = false;
                    this.pnlcheck.Visible = true;
                    this.pnl1stApp.Visible = false;
                    this.pnl2ndApp.Visible = false;
                    this.pnlaudit.Visible = false;
                    this.pnlapproval.Visible = false;
                    //this.RadioButtonList1.Items[1].Attributes["style"] = "background: #430000; display:block; ";
                    this.RadioButtonList1.Items[1].Attributes["class"] = "lblactive blink_me";

                    break;

                // 1st approval
                case "2":
                    this.pnlinitial.Visible = false;
                    this.pnlcheck.Visible = false;
                    this.pnl1stApp.Visible = true;
                    this.pnl2ndApp.Visible = false;
                    this.pnlaudit.Visible = false;
                    this.pnlapproval.Visible = false;
                    this.RadioButtonList1.Items[2].Attributes["class"] = "lblactive blink_me";
                    //this.RadioButtonList1.Items[2].Attributes["style"] = "background: #430000; display:block; ";
                    break;
                    
                    // 2d approval
                case "3":
                    this.pnlinitial.Visible = false;
                    this.pnlcheck.Visible = false;
                    this.pnl1stApp.Visible = false;
                    this.pnl2ndApp.Visible = true;
                    this.pnlaudit.Visible = false;
                    this.pnlapproval.Visible = false;
                    this.RadioButtonList1.Items[3].Attributes["class"] = "lblactive blink_me";
                    //this.RadioButtonList1.Items[2].Attributes["style"] = "background: #430000; display:block; ";
                    break;
                
                    //audit / 3rd approval
                case "4":
                    this.pnlinitial.Visible = false;
                    this.pnlcheck.Visible = false;
                    this.pnl1stApp.Visible = false;
                    this.pnl2ndApp.Visible = false;
                    this.pnlaudit.Visible = true;
                    this.pnlapproval.Visible = false;
                    this.RadioButtonList1.Items[4].Attributes["class"] = "lblactive blink_me";
                    //this.RadioButtonList1.Items[2].Attributes["style"] = "background: #430000; display:block; ";
                    break;


                // final approval
                case "5":
                    this.pnlinitial.Visible = false;
                    this.pnlcheck.Visible = false;
                    this.pnl1stApp.Visible = false;
                    this.pnl2ndApp.Visible = false;
                    this.pnlaudit.Visible = false;
                    this.pnlapproval.Visible = true;
                    this.RadioButtonList1.Items[5].Attributes["class"] = "lblactive blink_me";
                    //this.RadioButtonList1.Items[3].Attributes["style"] = "background: #430000; display:block; ";
                    break;

            }
        }
        protected void txtdate_TextChanged(object sender, EventArgs e)
        {
            RadioButtonList1_SelectedIndexChanged(null, null);
            GetAddWrkData();
        }

        protected void lbtnok_Click(object sender, EventArgs e)
        {
            this.GetAddWrkData();        
        }

        private void GetAddWrkData()
        {
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3101":
                case "3367":
                    GetAddWrkDataEpic();
                    //ctype = "PRINTCLIENTMODDASHEP"; // todo only epic 
                    break;
                default:
                    //ctype = "PRINTCLIENTMODDASH";
                    GetAddWrkDataGen();
                    break;
            }
        }

        private void GetAddWrkDataGen()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string userid = hst["usrid"].ToString();
            string todate = this.txttodate.Text.Trim();

            DataSet ds2 = feaData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT02", "PRINTCLIENTMODDASH", todate, "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {

                return;
            }
            this.RadioButtonList1.Items[0].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + Convert.ToInt32(ds2.Tables[1].Rows[0]["intial"]) + "</div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'>Initial</div></div></div>";
            this.RadioButtonList1.Items[1].Text = "<div class='circle-tile'><a><div class='circle-tile-heading red counter'>" + Convert.ToInt32(ds2.Tables[1].Rows[0]["checked"]) + "</i></div></a><div class='circle-tile-content red'><div class='circle-tile-description text-faded'>Checked</div></div></div>";
            this.RadioButtonList1.Items[2].Text = "<div class='circle-tile'><a><div class='circle-tile-heading green counter'>" + Convert.ToInt32(ds2.Tables[1].Rows[0]["fappid"]) + "</i></div></a><div class='circle-tile-content green'><div class='circle-tile-description text-faded'>1st Approval</div></div></div>";
            this.RadioButtonList1.Items[3].Text = "<div class='circle-tile'><a><div class='circle-tile-heading blue counter'>" + Convert.ToInt32(ds2.Tables[1].Rows[0]["sappid"]) + "</i></div></a><div class='circle-tile-content blue'><div class='circle-tile-description text-faded'>2nd Approval</div></div></div>";
            this.RadioButtonList1.Items[4].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + Convert.ToInt32(ds2.Tables[1].Rows[0]["audited"]) + "</i></div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'>Audit</div></div></div>";
            this.RadioButtonList1.Items[5].Text = "<div class='circle-tile'><a><div class='circle-tile-heading orange counter'>" + Convert.ToInt32(ds2.Tables[1].Rows[0]["approv"]) + "</i></div></a><div class='circle-tile-content orange'><div class='circle-tile-description text-faded'>Approval</div></div></div>";

            Session["tbladdwrk"] = ds2.Tables[0];

            DataTable dt = new DataTable();
            DataView dv;
            //Intial
            dt = ((DataTable)ds2.Tables[0]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("approvbyid=''");
            this.Data_Bind("grvRptCliMod", dt);

            ////Checked
            dt = ((DataTable)ds2.Tables[0]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("chkbyid='' and auditid='' and approvbyid=''");
            this.Data_Bind("gvcltmodchk", dv.ToTable());
            //Forward

            ////Audit
            dt = ((DataTable)ds2.Tables[0]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("chkbyid<>'' and auditid='' and approvbyid=''");
            this.Data_Bind("gvCltmodaduit", dv.ToTable());

            ////Approval
            dt = ((DataTable)ds2.Tables[0]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("chkbyid<>'' and auditid<>'' and approvbyid=''");
            this.Data_Bind("gvCltmodapp", dv.ToTable());

        }        
        private void GetAddWrkDataEpic()
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string userid = hst["usrid"].ToString();
            string todate = this.txttodate.Text.Trim();

            DataSet ds2 = feaData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT02", "PRINTCLIENTMODDASHEP", todate, "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                return;
            }
            //intial	checked	audited	approv	fappid	sappid

            this.RadioButtonList1.Items[0].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + Convert.ToInt32(ds2.Tables[1].Rows[0]["intial"]) + "</div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'>Status</div></div></div>";
            this.RadioButtonList1.Items[1].Text = "<div class='circle-tile'><a><div class='circle-tile-heading red counter'>" + Convert.ToInt32(ds2.Tables[1].Rows[0]["checked"]) + "</i></div></a><div class='circle-tile-content red'><div class='circle-tile-description text-faded'>Checked</div></div></div>";
            this.RadioButtonList1.Items[2].Text = "<div class='circle-tile'><a><div class='circle-tile-heading green counter'>" + Convert.ToInt32(ds2.Tables[1].Rows[0]["fappid"]) + "</i></div></a><div class='circle-tile-content green'><div class='circle-tile-description text-faded'>1st Approval</div></div></div>";
            this.RadioButtonList1.Items[3].Text = "<div class='circle-tile'><a><div class='circle-tile-heading blue counter'>" + Convert.ToInt32(ds2.Tables[1].Rows[0]["sappid"]) + "</i></div></a><div class='circle-tile-content blue'><div class='circle-tile-description text-faded'>2nd Approval</div></div></div>";
            this.RadioButtonList1.Items[4].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + Convert.ToInt32(ds2.Tables[1].Rows[0]["audited"]) + "</i></div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'>3rd Approval</div></div></div>";
            this.RadioButtonList1.Items[5].Text = "<div class='circle-tile'><a><div class='circle-tile-heading orange counter'>" + Convert.ToInt32(ds2.Tables[1].Rows[0]["approv"]) + "</i></div></a><div class='circle-tile-content orange'><div class='circle-tile-description text-faded'>Final Approval</div></div></div>";

            Session["tbladdwrk"] = ds2.Tables[0];

            DataTable dt = new DataTable();
            DataView dv;
            //Intial
            dt = ((DataTable)ds2.Tables[0]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("approvbyid=''");
            this.Data_Bind("grvRptCliMod", dt);

            ////Checked
            dt = ((DataTable)ds2.Tables[0]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("chkbyid='' and fappid='' and sappid='' and auditid='' and approvbyid='' and reqchk='True'");
            this.Data_Bind("gvcltmodchk", dv.ToTable());
            //Forward

            ////1st approval
            dt = ((DataTable)ds2.Tables[0]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("chkbyid<>'' and fappid='' and sappid='' and auditid='' and approvbyid='' and reqchk='True'");
            this.Data_Bind("gv1stApp", dv.ToTable());

            ////2nd approval
            dt = ((DataTable)ds2.Tables[0]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("chkbyid<>'' and fappid<>'' and sappid='' and auditid='' and approvbyid='' and reqchk='True'");
            this.Data_Bind("gv2ndApp", dv.ToTable());


            ////Audit / final approval
            dt = ((DataTable)ds2.Tables[0]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("chkbyid<>'' and fappid<>'' and sappid<>'' and auditid='' and approvbyid='' and reqchk='True' ");
            this.Data_Bind("gvCltmodaduit", dv.ToTable());

            ////Approval
            dt = ((DataTable)ds2.Tables[0]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("chkbyid<>'' and fappid<>'' and sappid<>'' and auditid<>'' and approvbyid='' and reqchk='True'");
            this.Data_Bind("gvCltmodapp", dv.ToTable());

        }        

        private void Data_Bind(string gv, DataTable dt)
        {
            switch (gv)
            {
                case "grvRptCliMod":
                    this.grvRptCliMod.DataSource = dt;
                    this.grvRptCliMod.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "gvcltmodchk":
                    this.gvcltmodchk.DataSource = dt;
                    this.gvcltmodchk.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;

                case "gvCltmodaduit":
                    this.gvCltmodaduit.DataSource = dt;
                    this.gvCltmodaduit.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "gvCltmodapp":
                    this.gvCltmodapp.DataSource = dt;
                    this.gvCltmodapp.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;

                case "gv1stApp":
                    this.gv1stApp.DataSource = dt;
                    this.gv1stApp.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;
                
                case "gv2ndApp":
                    this.gv2ndApp.DataSource = dt;
                    this.gv2ndApp.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;


            }

            //DataTable dt = (DataTable)Session["tblfeaprjLand"];



        }



        protected void grvRptCliMod_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                // HyperLink hlnkprj = (HyperLink)e.Row.FindControl("lgvproname");

                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();

                string chkid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "chkbyid")).ToString();
                string auditid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "auditid")).ToString();
                string approvbyid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "approvbyid")).ToString();

                // hlnkprj.NavigateUrl = "~/F_01_LPA/PriLandProposal?Type=Report&prjcode=" + pactcode;

                Label track = (Label)e.Row.FindControl("lgvtrack");


                if (chkid.Trim() == "" && auditid.Trim() == "" && approvbyid.Trim() == "")
                {
                    track.Attributes.CssStyle.Add("color", "Maroon");
                }
                else if (chkid != "" && auditid.Trim() == "" && approvbyid.Trim() == "")
                {
                    track.Attributes.CssStyle.Add("color", "blue");
                }

                else if (chkid != "" && auditid != "" && approvbyid.Trim() == "")
                {
                    track.Attributes.CssStyle.Add("color", "Green");
                }
            }
        }

        protected void gvcltmodchk_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlnkchk = (HyperLink)e.Row.FindControl("lnkchk");

                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "adno")).ToString();
                string date = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "addate")).ToString("dd-MMM-yyyy");

                hlnkchk.NavigateUrl = "~/F_24_CC/CustMaintenanceWork?Type=Check&Genno=" + pactcode + "&Date1=" + date;


            }
        }


        protected void gvCltmodaduit_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlnkchk = (HyperLink)e.Row.FindControl("lnkadt");

                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "adno")).ToString();
                string date = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "addate")).ToString("dd-MMM-yyyy");

                hlnkchk.NavigateUrl = "~/F_24_CC/CustMaintenanceWork?Type=Audit&Genno=" + pactcode + "&Date1=" + date;


            }
        }
        protected void gvCltmodapp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlnkchk = (HyperLink)e.Row.FindControl("lnkapp");
                HyperLink hlnkprintapp = (HyperLink)e.Row.FindControl("hlnkprintapp");
                string adno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "adno")).ToString();
                string date = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "addate")).ToString("dd-MMM-yyyy");
                hlnkchk.NavigateUrl = "~/F_24_CC/CustMaintenanceWork?Type=Approv&Genno=" + adno + "&Date1=" + date;
                hlnkprintapp.NavigateUrl = "~/F_24_CC/CustMaintenanceWork?Type=ReqPrint&Genno=" + adno + "&Date1=" + date;
            }
        }



        private string GetReqApproval(string approval, string type = "")
        {
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string trmnid = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

            DataSet ds1 = new DataSet("ds1");
            System.IO.StringReader xmlSR;

            switch (type)
            {

                case "auditcancel":
                    xmlSR = new System.IO.StringReader(approval);
                    ds1.ReadXml(xmlSR);
                    ds1.Tables[0].TableName = "tbl1";
                    ds1.Tables[0].Rows[0]["chkbyid"] = "";
                    approval = ds1.GetXml();
                    break;

                case "approvcancel":
                    // string xmlDS = ds1.Tables[0].Rows[0]["approval"].ToString();  
                    xmlSR = new System.IO.StringReader(approval);
                    ds1.ReadXml(xmlSR);
                    ds1.Tables[0].TableName = "tbl1";
                    ds1.Tables[0].Rows[0]["auditid"] = "";
                    approval = ds1.GetXml();
                    break;

            }


            return approval;

        }


        protected void lnkremoveadt_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tbladdwrk"];

            string comcod = this.GetCompCode();
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string adno = ((Label)this.gvCltmodaduit.Rows[Rowindex].FindControl("lbladdnoad")).Text.Trim();
            //string pactcode = ((Label)this.gvCltmodaduit.Rows[Rowindex].FindControl("lblpactcodead")).Text.Trim();
            //DataView dv = new DataView();
            //dv = dt.DefaultView;
            //dv.RowFilter = "pactcode=" + pactcode + " and adno="+addno+"";
            //dt = dv.ToTable();
            //string appxml = dt.Rows[0]["approval"].ToString();

            string calltype = this.GetRemoveAuditCType();
            bool result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT02", calltype, adno, "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Deleted failed..!');", true);
                return;
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Successfully Deleted');", true);
            this.lbtnok_Click(null, null);
        }

        private string GetRemoveAuditCType()
        {
            string ctype = "";
            switch (GetCompCode())
            {
                case "3101":
                case "3367":
                    ctype = "UPDATEMODSECONDAPP";
                    break;
                default:
                    ctype = "UPDATEMODCHECK";
                    break;
            }
            return ctype;
        }
        protected void lnkremoveap_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tbladdwrk"];
            string comcod = this.GetCompCode();
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string adno = ((Label)this.gvCltmodapp.Rows[Rowindex].FindControl("lbladdnoap")).Text.Trim();

            //UPDATEMODAPPROVAL
            bool result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT02", "UPDATEMODAUDIT", adno, "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Cancellation failed..!');", true);
                return;
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Successfully Deleted');", true);
            this.lbtnok_Click(null, null);
        }
        protected void hlnkprintchk_Click(object sender, EventArgs e)
        {

            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string adno = ((Label)this.gvcltmodchk.Rows[Rowindex].FindControl("lbladdnochk")).Text.Trim();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            DataTable dt = (DataTable)Session["tbladdwrk"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("adno='" + adno + "'");
            dt = dv.ToTable();

            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            ReportDocument rptstate = new RealERPRPT.R_23_CR.RptCliMod();
            TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            rptCname.Text = comnam;

            TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["ftdate"] as TextObject;
            rptftdate.Text = "Date: " + fromdate + " To " + todate;
            TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            rptstate.SetDataSource(dt);
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptstate.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstate;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        protected void hlnkprintadt_Click(object sender, EventArgs e)
        {
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string adno = ((Label)this.gvCltmodaduit.Rows[Rowindex].FindControl("lbladdnoad")).Text.Trim();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            DataTable dt = (DataTable)Session["tbladdwrk"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("adno='" + adno + "'");
            dt = dv.ToTable();

            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            ReportDocument rptstate = new RealERPRPT.R_23_CR.RptCliMod();
            TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            rptCname.Text = comnam;

            TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["ftdate"] as TextObject;
            rptftdate.Text = "Date: " + fromdate + " To " + todate;
            TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            rptstate.SetDataSource(dt);
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptstate.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstate;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        protected void hlnkprintapp_Click(object sender, EventArgs e)
        {
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string adno = ((Label)this.gvCltmodapp.Rows[Rowindex].FindControl("lbladdnoap")).Text.Trim();
            
            
            Hashtable hst = (Hashtable)Session["tblLogin"];
            DataTable dt = (DataTable)Session["tbladdwrk"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("adno='" + adno + "'");
            dt = dv.ToTable();

            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            ReportDocument rptstate = new RealERPRPT.R_23_CR.RptCliMod();
            TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            rptCname.Text = comnam;

            TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["ftdate"] as TextObject;
            rptftdate.Text = "Date: " + fromdate + " To " + todate;
            TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            rptstate.SetDataSource(dt);
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptstate.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstate;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }     

        protected void gv2ndApp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink lnkchk2nd = (HyperLink)e.Row.FindControl("lnkchk2nd");
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "adno")).ToString();
                string date = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "addate")).ToString("dd-MMM-yyyy");
                lnkchk2nd.NavigateUrl = "~/F_24_CC/CustMaintenanceWork?Type=SecondApproval&Genno=" + pactcode + "&Date1=" + date;
            }

        }

        protected void gv1stApp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink lnkchk1st = (HyperLink)e.Row.FindControl("lnkchk1st");
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "adno")).ToString();
                string date = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "addate")).ToString("dd-MMM-yyyy");
                lnkchk1st.NavigateUrl = "~/F_24_CC/CustMaintenanceWork?Type=FirstApproval&Genno=" + pactcode + "&Date1=" + date;
            }
        }
        protected void hlnkprintchk1st_Click(object sender, EventArgs e)
        {

        }

        protected void hlnkprintchk2nd_Click(object sender, EventArgs e)
        {

        }

        protected void lbtnDel2ndApp_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tbladdwrk"];
            string comcod = this.GetCompCode();
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string adno = ((Label)this.gv2ndApp.Rows[Rowindex].FindControl("lblgv2ndApadno")).Text.Trim();
            bool result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT02", "UPDATEMODFIRSTAPP", adno, "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Cancellation failed..!');", true);
                return;
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Successfully Deleted');", true);
            this.lbtnok_Click(null, null);
        }

        protected void lbtnDel1stApp_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tbladdwrk"];
            string comcod = this.GetCompCode();
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string adno = ((Label)this.gv1stApp.Rows[Rowindex].FindControl("lblgv1stApadno")).Text.Trim();
            bool result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT02", "UPDATEMODCHECK", adno, "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Cancellation failed..!');", true);
                return;
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Successfully Deleted');", true);
            this.lbtnok_Click(null, null);
        }

        protected void lbtnDelChk_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tbladdwrk"];
            string comcod = this.GetCompCode();
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string adno = ((Label)this.gvcltmodchk.Rows[Rowindex].FindControl("lbladdnochk")).Text.Trim();

            bool result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT02", "DELADDWORK", adno, "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Cancellation failed..!');", true);
                return;
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Successfully Deleted');", true);
            this.lbtnok_Click(null, null);
        }
    }
}