using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web.Security;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using System.Web.UI.DataVisualization.Charting;
using System.IO;
using RealEntity;
using RealERPLIB;

using Microsoft.Reporting.WinForms;
namespace RealERPWEB.F_99_Allinterface
{
    public partial class RptLCInterface : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        Common Common = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                //((Label)this.Master.FindControl("lblTitle")).Text = "Import Interface";
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.txtFDate.Text = System.DateTime.Today.ToString("01-MMM-yyyy");
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.RadioButtonList1.SelectedIndex = 0;
                //this.SaleRequRpt();

                this.PnlInt.Visible = true;

                //  txtdate_TextChanged(null, null);

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["mcomcod"].ToString();
                if (comcod != "0000")
                {
                    this.plncop.Visible = true;
                    this.GetCompanyName();
                }
                else
                {
                    comcod = hst["comcod"].ToString();
                    txtdate_TextChanged(null, null);
                    //this.totalCount();



                }

            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Visible = false;
            ((DropDownList)this.Master.FindControl("DDPrintOpt")).Visible = false;
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private void GetCompanyName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = ASTUtility.Right(hst["usrid"].ToString(), 3);
            string comcod = hst["mcomcod"].ToString();
            DataSet ds = accData.GetTransInfo(comcod, "SP_UTILITY_GRPUSER_MGT", "GET_MOTHER_COMPANY", usrid, "", "", "", "", "", "", "", "");
            this.ddlCompany.DataTextField = "comname";
            this.ddlCompany.DataValueField = "comcod";
            this.ddlCompany.DataSource = ds.Tables[0];
            this.ddlCompany.DataBind();
            this.ddlCompany.SelectedValue = this.GetCompCode();
            this.ddlCompany_SelectedIndexChanged(null, null);

        }
        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string mcomcod = hst["mcomcod"].ToString();
            if (mcomcod != "0000")
            {
                txtdate_TextChanged(null, null);

            }

            string comcod = (mcomcod == "0000") ? this.GetCompCode() : this.ddlCompany.SelectedValue.ToString();


        }
        //private void ColoumVisiable()
        //    {
        //        this.gvprobrec.Columns[4].Visible = true;
        //        //this.grvProReq.Columns[4].Visible = true;
        //        ////this.grvProIssue.Columns[4].Visible = true;
        //        //this.grvProdtion.Columns[4].Visible = true;
        //        //this.grvQCEntry.Columns[4].Visible = true;
        //        ////this.gvstorec.Columns[4].Visible = true;
        //        //this.grvComp.Columns[4].Visible = true;
        //        //this.gvProdInfo.Columns[4].Visible = true;
        //    }
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            int day = ASTUtility.Datediffday(Convert.ToDateTime(this.txtFDate.Text), Convert.ToDateTime(this.txtdate.Text));
            if (day != 0)
                return;
            txtdate_TextChanged(null, null);


        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void txtdate_TextChanged(object sender, EventArgs e)
        {
            this.ImportInterFace();
            this.RadioButtonList1_SelectedIndexChanged(null, null);
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.lblmsg.Visible = false;
            this.ImportInterFace();
        }
        private void ImportInterFace()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string mcomcod = hst["mcomcod"].ToString();
            string comcod = (mcomcod == "0000") ? this.GetCompCode() : this.ddlCompany.SelectedValue.ToString();

            string Date1 = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string Date2 = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_LC_INTERFACE", "GET_LC_INTERFACE", Date1, Date2, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tbllcreqest"] = ds1.Tables[0];
            ViewState["tbllcopen"] = ds1.Tables[1];
            ViewState["tbllcrecv"] = ds1.Tables[2];
            ViewState["tbllcQc"] = ds1.Tables[3];
            this.RadioButtonList1.Items[0].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + ds1.Tables[4].Rows[0]["reqqty"].ToString() + "</div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'> Request</div></div></div>";

            this.RadioButtonList1.Items[1].Text = "<div class='circle-tile'><a><div class='circle-tile-heading red counter'>" + ds1.Tables[4].Rows[0]["appqty"].ToString() + "</i></div></a><div class='circle-tile-content red'><div class='circle-tile-description text-faded'> Req Approval</div></div></div>";
            //this.RadioButtonList1.Items[2].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + ds1.Tables[4].Rows[0]["cscrtqty"].ToString() + "</i></div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'> CS Create</div></div></div>";
            //this.RadioButtonList1.Items[3].Text = "<div class='circle-tile'><a><div class='circle-tile-heading orange counter'>" + ds1.Tables[4].Rows[0]["csaprvqty"].ToString() + "</i></div></a><div class='circle-tile-content orange'><div class='circle-tile-description text-faded'> CS Approval</div></div></div>";

            this.RadioButtonList1.Items[4].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-gray counter'>" + ds1.Tables[4].Rows[0]["opnqty"].ToString() + "</i></div></a><div class='circle-tile-content dark-gray'><div class='circle-tile-description text-faded'> LC Opening</div></div></div>";

            this.RadioButtonList1.Items[5].Text = "<div class='circle-tile'><a><div class='circle-tile-heading yellow counter'>" + ds1.Tables[4].Rows[0]["recqty"].ToString() + "</i></div></a><div class='circle-tile-content yellow'><div class='circle-tile-description text-faded'> Received</div></div></div>";

            this.RadioButtonList1.Items[6].Text = "<div class='circle-tile'><a><div class='circle-tile-heading blue counter'>" + ds1.Tables[4].Rows[0]["qcqty"].ToString() + "</i></div></a><div class='circle-tile-content blue'><div class='circle-tile-description text-faded'> Qc Check</div></div></div>";

            this.RadioButtonList1.Items[7].Text = "<div class='circle-tile'><a><div class='circle-tile-heading green counter'>" + ds1.Tables[4].Rows[0]["costqty"].ToString() + "</i></div></a><div class='circle-tile-content green'><div class='circle-tile-description text-faded'> Costing</div></div></div>";
            RadioButtonList1_SelectedIndexChanged(null, null);


        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            this.lblprintstkl.Text = "";
            string value = this.RadioButtonList1.SelectedValue.ToString();
            DataTable dt = (DataTable)ViewState["tbllcreqest"];
            DataTable dt2 = (DataTable)ViewState["tbllcopen"];
            DataTable dt3 = (DataTable)ViewState["tbllcrecv"];
            DataTable dt4 = (DataTable)ViewState["tbllcQc"];
            DataTable Tempdt = new DataTable();
            DataView Tempdv = new DataView();
            switch (value)
            {
                case "0":
                    this.pnlallRec.Visible = true;
                    this.pnlRecApp.Visible = false;
                    this.PanCSCrete.Visible = false;
                    this.PanCsAprv.Visible = false;
                    this.PanelAssorted.Visible = false;
                    this.pnlAssApp.Visible = false;
                    this.Pnlbill.Visible = false;
                    this.PnlCosting.Visible = false;
                    this.Data_Bind("gvprobrec", dt);
                    this.RadioButtonList1.Items[0].Attributes["class"] = "lblactive blink_me";
                    break;

                case "1"://Req Approval
                    this.pnlallRec.Visible = false;
                    this.pnlRecApp.Visible = true;
                    this.PanCSCrete.Visible = false;
                    this.PanCsAprv.Visible = false;
                    this.PanelAssorted.Visible = false;
                    this.pnlAssApp.Visible = false;
                    this.Pnlbill.Visible = false;
                    this.PnlCosting.Visible = false;
                    Tempdt = dt.Copy();
                    Tempdv = Tempdt.DefaultView;
                    Tempdv.RowFilter = ("checked ='Ok'");
                    this.Data_Bind("gvprobapp", Tempdv.ToTable());
                    this.RadioButtonList1.Items[1].Attributes["class"] = "lblactive blink_me";
                    break;
                case "2":
                    this.pnlallRec.Visible = false;
                    this.pnlRecApp.Visible = false;
                    this.PanCSCrete.Visible = true;
                    this.PanCsAprv.Visible = false;
                    this.PanelAssorted.Visible = false;
                    this.pnlAssApp.Visible = false;
                    this.Pnlbill.Visible = false;
                    this.PnlCosting.Visible = false;
                    Tempdt = dt.Copy();
                    Tempdv = Tempdt.DefaultView;
                    Tempdv.RowFilter = ("checked ='Ok' and csstus=''");
                    this.Data_Bind("gvcscrte", Tempdv.ToTable());
                    this.RadioButtonList1.Items[2].Attributes["class"] = "lblactive blink_me";
                    break;
                case "3":
                    this.pnlallRec.Visible = false;
                    this.pnlRecApp.Visible = false;
                    this.PanCSCrete.Visible = false;
                    this.PanCsAprv.Visible = true;
                    this.PanelAssorted.Visible = false;
                    this.pnlAssApp.Visible = false;
                    this.Pnlbill.Visible = false;
                    this.PnlCosting.Visible = false;
                    Tempdt = dt.Copy();
                    Tempdv = Tempdt.DefaultView;
                    Tempdv.RowFilter = ("checked ='Ok' and csstus='Ok' and approved=''");
                    this.Data_Bind("gvcsaprv", Tempdv.ToTable());
                    this.RadioButtonList1.Items[2].Attributes["class"] = "lblactive blink_me";
                    break;
                case "4": //LC Open
                    this.pnlallRec.Visible = false;
                    this.pnlRecApp.Visible = false;
                    this.PanCSCrete.Visible = false;
                    this.PanCsAprv.Visible = false;
                    this.PanelAssorted.Visible = true;
                    this.pnlAssApp.Visible = false;
                    this.Pnlbill.Visible = false;
                    this.PnlCosting.Visible = false;
                    Tempdt = dt.Copy();
                    Tempdv = Tempdt.DefaultView;
                    Tempdv.RowFilter = ("approved='Ok' and lcopen='' ");
                    this.Data_Bind("gvprobass", Tempdv.ToTable());
                    this.RadioButtonList1.Items[2].Attributes["class"] = "lblactive blink_me";
                    break;

                case "5":// LC Received
                    this.pnlallRec.Visible = false;
                    this.pnlRecApp.Visible = false;
                    this.PanCSCrete.Visible = false;
                    this.PanCsAprv.Visible = false;
                    this.PanelAssorted.Visible = false;
                    this.pnlAssApp.Visible = true;
                    this.Pnlbill.Visible = false;
                    this.PnlCosting.Visible = false;
                    Tempdt = dt2.Copy();
                    Tempdv = Tempdt.DefaultView;
                    Tempdv.RowFilter = ("lcopen='Ok' and  balqty>0");
                    this.Data_Bind("gvprobassapp", Tempdv.ToTable());
                    this.RadioButtonList1.Items[3].Attributes["class"] = "lblactive blink_me";
                    break;
                case "6":
                    this.pnlallRec.Visible = false;
                    this.pnlRecApp.Visible = false;
                    this.PanCSCrete.Visible = false;
                    this.PanCsAprv.Visible = false;
                    this.PanelAssorted.Visible = false;
                    this.pnlAssApp.Visible = false;
                    this.Pnlbill.Visible = true;
                    this.PnlCosting.Visible = false;
                    Tempdt = dt3.Copy();
                    Tempdv = Tempdt.DefaultView;
                    //  Tempdv.RowFilter = ("asrstatus ='Ok'");
                    this.Data_Bind("gvprobbill", Tempdv.ToTable());
                    this.RadioButtonList1.Items[4].Attributes["class"] = "lblactive blink_me";
                    break;

                case "7":
                    this.pnlallRec.Visible = false;
                    this.pnlRecApp.Visible = false;
                    this.PanCSCrete.Visible = false;
                    this.PanCsAprv.Visible = false;
                    this.PanelAssorted.Visible = false;
                    this.pnlAssApp.Visible = false;
                    this.Pnlbill.Visible = false;
                    this.PnlCosting.Visible = true;
                    this.Data_Bind("gvCosting", dt4);
                    this.RadioButtonList1.Items[5].Attributes["class"] = "lblactive blink_me";
                    break;


            }
        }


        private void Data_Bind(string gv, DataTable dt)
        {
            switch (gv)
            {
                case "gvprobrec":

                    if (dt.Rows.Count == 0)
                        return;
                    this.gvprobrec.DataSource = dt;
                    this.gvprobrec.DataBind();
                    break;
                case "gvprobapp":
                    this.gvprobapp.DataSource = (dt);
                    this.gvprobapp.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "gvcscrte":
                    this.gvcscrte.DataSource = (dt);
                    this.gvcscrte.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "gvcsaprv":
                    this.gvcsaprv.DataSource = (dt);
                    this.gvcsaprv.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "gvprobass":
                    this.gvprobass.DataSource = (dt);
                    this.gvprobass.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "gvprobassapp":
                    this.gvprobassapp.DataSource = (dt);
                    this.gvprobassapp.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "gvprobbill":
                    this.gvprobbill.DataSource = (dt);
                    this.gvprobbill.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "gvCosting":
                    this.gvCosting.DataSource = (dt);
                    this.gvCosting.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    break;

                    //case "grvComp":
                    //    this.grvComp.DataSource = (dt);
                    //    this.grvComp.DataBind();
                    //    if (dt.Rows.Count == 0)
                    //        return;
                    //    break;
            }
        }
        protected void gvprobrec_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Session["Report1"] = gvprobrec;
                ((HyperLink)this.gvprobrec.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer?PrintOpt=GRIDTOEXCEL";
            }
        }

        protected void gvprobapp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrint");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");

                //Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = DataBinder.Eval(e.Row.DataItem, "comcod").ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();

                hlink1.NavigateUrl = "~/F_11_Pro/PuchasePrint?Type=ReqPrint&comcod=" + comcod + "&reqno=" + reqno;
                hlink2.NavigateUrl = "~/F_12_Inv/PurReqApproval?Type=Approval&prjcode=" + pactcode + "&genno=" + reqno;

                Session["Report1"] = gvprobapp;
                ((HyperLink)this.gvprobapp.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer?PrintOpt=GRIDTOEXCEL";
            }

        }
        protected void gvprobass_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnEdit");
                hlink1.NavigateUrl = "~/F_09_LCM/LCOpening?Type=Open&genno=" + reqno;
                Session["Report1"] = gvprobass;
                ((HyperLink)this.gvprobass.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer?PrintOpt=GRIDTOEXCEL";
            }

        }
        protected void gvcscrte_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string comcod = this.GetCompCode();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnEdit");
                hlink1.NavigateUrl = "~/F_07_Inv/PurMktSurvey02?Type=Create&comcod=" + comcod + "&genno=" + reqno;
                Session["Report1"] = gvcscrte;
                ((HyperLink)this.gvcscrte.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer?PrintOpt=GRIDTOEXCEL";
            }

        }
        protected void gvcsaprv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string comcod = this.GetCompCode();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnEdit");
                hlink1.NavigateUrl = "~/F_07_Inv/PurMktSurvey02?Type=Approved&comcod=" + comcod + "&genno=" + reqno;
                Session["Report1"] = gvcsaprv;
                ((HyperLink)this.gvcsaprv.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer?PrintOpt=GRIDTOEXCEL";
            }

        }
        protected void gvprobassapp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();
                string actcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnEdit");


                hlink1.NavigateUrl = "~/F_09_LCM/LcReceive?Type=Entry&comcod=" + comcod + "&actcode=" + actcode + "&centrid=&genno=";
                hlink1.ToolTip = "Lc Received";


                Session["Report1"] = gvprobassapp;
                ((HyperLink)this.gvprobassapp.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer?PrintOpt=GRIDTOEXCEL";
            }

        }



        protected void gvprobbill_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();
                string rcvno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rcvno")).ToString();
                string actcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();
                string storid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "storid")).ToString();
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnEdit");
                HyperLink editbtn = (HyperLink)e.Row.FindControl("HypPreEdit");
                //if (this.ddlresource.SelectedValue.ToString() == "51")
                //{
                //    this.gvprobbill.Columns[6].Visible = false;
                hlink1.NavigateUrl = "~/F_09_LCM/LcQcRecv?Type=Entry&comcod=" + comcod + "&actcode=" + actcode + "&centrid=" + storid + "&genno=" + rcvno;
                editbtn.NavigateUrl = "~/F_09_LCM/LcReceive?Type=Edit&comcod=" + comcod + "&actcode=" + actcode + "&centrid=" + storid + "&genno=" + rcvno;
                //    hlink1.ToolTip = "Assorted Approve";
                //    hlink1.Visible = true;
                //}
                //else
                //{
                //    this.gvprobbill.Columns[6].Visible = true;
                //}
                Session["Report1"] = gvprobbill;
                ((HyperLink)this.gvprobbill.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer?PrintOpt=GRIDTOEXCEL";
            }
        }
        protected void btnSetup_Click(object sender, EventArgs e)
        {
            this.PnlSalesSetup.Visible = true;
            this.PnlInt.Visible = false;
            this.pnlReprots.Visible = true;
            this.plnMgf.Visible = false;
            //this.lblVal.Visible = false;

        }
        protected void lnkInteface_Click(object sender, EventArgs e)
        {
            this.PnlInt.Visible = true;
            this.pnlReprots.Visible = false;
            //this.plnMgf.Visible = false;
            //this.lblVal.Visible = true;
            this.PnlSalesSetup.Visible = false;
        }
        protected void lnkRept_Click(object sender, EventArgs e)
        {
            this.PnlInt.Visible = false;
            this.pnlReprots.Visible = true;
            this.plnMgf.Visible = true;
            //this.lblVal.Visible = false;
            this.PnlSalesSetup.Visible = false;
        }




        protected void gvCosting_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HypbtnEdit");
                HyperLink btnforward = (HyperLink)e.Row.FindControl("lnkbtnForward");

                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();
                string actcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();
                string grrno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grrno")).ToString();
                string storid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "storid")).ToString();

                hlink1.NavigateUrl = "~/F_09_LCM/LcQcRecv?Type=Edit&comcod=" + comcod + "&actcode=" + actcode + "&centrid=" + storid + "&genno=" + grrno;
                btnforward.NavigateUrl = "~/F_09_LCM/LCCostingDetails?Type=Entry&comcod=" + comcod + "&actcode=" + actcode;

                Session["Report1"] = gvCosting;
                ((HyperLink)this.gvCosting.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer?PrintOpt=GRIDTOEXCEL";
            }
        }

        protected void btnDelRec_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string mcomcod = hst["mcomcod"].ToString();
            string comcod = (mcomcod == "0000") ? this.GetCompCode() : this.ddlCompany.SelectedValue.ToString();

            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string actcode = ((Label)this.gvCosting.Rows[index].FindControl("lblgvlccode")).Text.ToString();
            string grrno = ((Label)this.gvCosting.Rows[index].FindControl("lgvgrrno")).Text.ToString();

            bool result = accData.UpdateTransInfo(comcod, "SP_LC_INTERFACE", "LC_INTERFACE_REVARSE", "COST", actcode, grrno);
            if (result == true)
            {
                this.lblmsg.Text = "GRR Reverse Sucess";
            }
        }



        protected void btnDelQC_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string mcomcod = hst["mcomcod"].ToString();
            string comcod = (mcomcod == "0000") ? this.GetCompCode() : this.ddlCompany.SelectedValue.ToString();

            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string actcode = ((Label)this.gvprobbill.Rows[index].FindControl("lblgvlccode")).Text.ToString();
            string rcvno = ((Label)this.gvprobbill.Rows[index].FindControl("lgvrcvNo")).Text.ToString();

            bool result = accData.UpdateTransInfo(comcod, "SP_LC_INTERFACE", "LC_INTERFACE_REVARSE", "QC", actcode, rcvno);
            if (result == true)
            {
                this.lblmsg.Text = "This QC Reverse Sucessfully";
            }
        }

        protected void btnDelRcv_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string mcomcod = hst["mcomcod"].ToString();
            string comcod = (mcomcod == "0000") ? this.GetCompCode() : this.ddlCompany.SelectedValue.ToString();

            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string actcode = ((Label)this.gvprobassapp.Rows[index].FindControl("lblgvcentrid")).Text.ToString();
            string reqno = ((Label)this.gvprobassapp.Rows[index].FindControl("lgvReqno")).Text.ToString();

            bool result = accData.UpdateTransInfo(comcod, "SP_LC_INTERFACE", "LC_INTERFACE_REVARSE", "RCV", actcode, reqno);
            if (result == true)
            {
                this.lblmsg.Text = "This Received Reverse Sucessfully";
            }
        }
    }
}





