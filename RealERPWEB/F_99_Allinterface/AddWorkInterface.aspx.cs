﻿using System;
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

        private string GetCompCode()
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
                    this.pnlaudit.Visible = false;
                    this.pnlapproval.Visible = false;
                    this.RadioButtonList1.Items[0].Attributes["class"] = "lblactive blink_me";
                    //this.RadioButtonList1.Items[0].Attributes["style"] = "background: #430000; display:block; ";
                    break;

                case "1":
                    this.pnlinitial.Visible = false;
                    this.pnlcheck.Visible = true;
                    this.pnlaudit.Visible = false;
                    this.pnlapproval.Visible = false;
                    //this.RadioButtonList1.Items[1].Attributes["style"] = "background: #430000; display:block; ";
                    this.RadioButtonList1.Items[1].Attributes["class"] = "lblactive blink_me";
                    break;
                case "2":
                    this.pnlinitial.Visible = false;
                    this.pnlcheck.Visible = false;
                    this.pnlaudit.Visible = true;
                    this.pnlapproval.Visible = false;
                    this.RadioButtonList1.Items[2].Attributes["class"] = "lblactive blink_me";
                    //this.RadioButtonList1.Items[2].Attributes["style"] = "background: #430000; display:block; ";
                    break;
                case "3":
                    this.pnlinitial.Visible = false;
                    this.pnlcheck.Visible = false;
                    this.pnlaudit.Visible = false;
                    this.pnlapproval.Visible = true;
                    this.RadioButtonList1.Items[3].Attributes["class"] = "lblactive blink_me";
                    //this.RadioButtonList1.Items[3].Attributes["style"] = "background: #430000; display:block; ";
                    break;

            }


        }
        protected void txtdate_TextChanged(object sender, EventArgs e)
        {
            RadioButtonList1_SelectedIndexChanged(null, null);

            GetAddWrkData();
        }

        protected void lnkok_Click(object sender, EventArgs e)
        {
            this.GetAddWrkData();
        }
        private void GetAddWrkData()
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string userid = hst["usrid"].ToString();
            //string frmdate = this.txtdate.Text.Trim();
            string todate = this.txttodate.Text.Trim();
            //string catcode = this.ddlcatag.SelectedValue.ToString() + "%";


            DataSet ds2 = feaData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT02", "PRINTCLIENTMODDASH", todate, "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {

                return;
            }

            this.RadioButtonList1.Items[0].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + Convert.ToInt32(ds2.Tables[1].Rows[0]["intial"]) + "</div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'>Initial</div></div></div>";

            this.RadioButtonList1.Items[1].Text = "<div class='circle-tile'><a><div class='circle-tile-heading red counter'>" + Convert.ToInt32(ds2.Tables[1].Rows[0]["checked"]) + "</i></div></a><div class='circle-tile-content red'><div class='circle-tile-description text-faded'>Checked</div></div></div>";

            this.RadioButtonList1.Items[2].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + Convert.ToInt32(ds2.Tables[1].Rows[0]["audited"]) + "</i></div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'>Audit</div></div></div>";
            this.RadioButtonList1.Items[3].Text = "<div class='circle-tile'><a><div class='circle-tile-heading orange counter'>" + Convert.ToInt32(ds2.Tables[1].Rows[0]["approv"]) + "</i></div></a><div class='circle-tile-content orange'><div class='circle-tile-description text-faded'>Approval</div></div></div>";

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

                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "adno")).ToString();
                string date = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "addate")).ToString("dd-MMM-yyyy");

                hlnkchk.NavigateUrl = "~/F_24_CC/CustMaintenanceWork?Type=Approv&Genno=" + pactcode + "&Date1=" + date;


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

            //string approval = GetReqApproval(appxml, "auditcancel");
            bool result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT02", "UPDATEMODCHK", adno, "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Cancellation failed!";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Successfully Cancelled";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
        }
        protected void lnkremoveap_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tbladdwrk"];
            string comcod = this.GetCompCode();
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string adno = ((Label)this.gvCltmodapp.Rows[Rowindex].FindControl("lbladdnoap")).Text.Trim();
            //string pactcode = ((Label)this.gvCltmodapp.Rows[Rowindex].FindControl("lblpactcodeap")).Text.Trim();
            //DataView dv = new DataView();
            //dv = dt.DefaultView;
            //dv.RowFilter = "pactcode=" + pactcode + " and adno=" + addno + "";
            //dt = dv.ToTable();
            //string appxml = dt.Rows[0]["approval"].ToString();

            //string approval = GetReqApproval(appxml, "approvcancel");
            bool result = feaData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT02", "UPDATEMODADT", adno, "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Cancellation failed!";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Successfully Cancelled";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
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

    }
}