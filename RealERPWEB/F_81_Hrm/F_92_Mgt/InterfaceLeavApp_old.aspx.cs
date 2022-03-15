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
using RealERPLIB;
using RealERPRPT;
using RealEntity;
using RealEntity.C_22_Sal;
namespace RealERPWEB.F_81_Hrm.F_92_Mgt
{
    public partial class InterfaceLeavApp : System.Web.UI.Page
    {
        //public static string orderno = "", centrid = "", custid = "", orderno1 = "", orderdat = "", Delstatus = "", Delorderno = "", RDsostatus="";
        ProcessAccess accData = new ProcessAccess();
        Common Common = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect(this.ResolveUrl("~/AcceessError.aspx"));
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);

                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "LEAVE INTERFACE";//
                double day = Convert.ToInt32(System.DateTime.Today.ToString("dd")) - 1;
                this.txFdate.Text = DateTime.Today.AddDays(-day).ToString("dd-MMM-yyyy");
                // this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtdate.Text = Convert.ToDateTime(this.txFdate.Text).AddMonths(2).AddDays(-1).ToString("dd-MMM-yyyy");

                this.RadioButtonList1.SelectedIndex = 0;
                this.pnlInt.Visible = true;
                this.SaleRequRpt();
                this.RadioButtonList1_SelectedIndexChanged(null, null);



            }
        }
        protected void Timer1_Tick(object sender, EventArgs e)
        {


        }

        protected void lnkbtnok_Click(object sender, EventArgs e)
        {
            this.SaleRequRpt();
            this.RadioButtonList1_SelectedIndexChanged(null, null);
        }
        public string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void txtdate_TextChanged(object sender, EventArgs e)
        {
            this.SaleRequRpt();
            this.RadioButtonList1_SelectedIndexChanged(null, null);
        }
        //protected void lnkOk_Click(object sender, EventArgs e)
        //{
        //    this.SaleRequRpt();
        //}
        protected void lnkInterface_Click(object sender, EventArgs e)
        {
            this.pnlInt.Visible = true;
            this.pnlReport.Visible = false;
        }
        protected void lnkRept_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            this.pnlInt.Visible = false;
            this.pnlReport.Visible = true;
            if (ASTUtility.Left(comcod, 1) == "7")
            {
                this.pnlTrade.Visible = true;
            }
            else
            {
                this.plnMgf.Visible = true;
            }
        }
        private void SaleRequRpt()
        {
            Session.Remove("tblaproved");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string usrid = (this.Request.QueryString["Type"] == "Ind") || (this.Request.QueryString["Type"] == "DeptHead") ? hst["usrid"].ToString() : "";
            string fDate = Convert.ToDateTime(this.txFdate.Text).ToString("dd-MMM-yyyy");
            string tDate = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");

            string type = (this.Request.QueryString["Type"]) == "Ind" || (this.Request.QueryString["Type"] == "DeptHead") ? "" : "Management";
            string DeptHead = (this.Request.QueryString["Type"]) == "DeptHead" ? "DeptHead" : "";


            DataSet ds1 = accData.GetTransInfo(comcod, "DBO_HRM.SP_REPORT_HR_INTERFACE", "GETLEAVEREQUEST", fDate, tDate, usrid, type, DeptHead, "", "", "", "");
            if (ds1 == null)
                return;

            this.RadioButtonList1.Items[0].Text = "<span class='fa  fa-signal fan'> </span>" + "<br>" + "<span class='lbldata'>" + ds1.Tables[1].Rows[0]["tcount"].ToString() + "</span>" + "<span class='lbldata2'>" + "Leave Request" + "</span>";
            this.RadioButtonList1.Items[1].Text = "<span class='fa fa-pencil-square-o fan'> </span>" + "<br>" + "<span class='lbldata'>" + ds1.Tables[1].Rows[0]["reqcount"].ToString() + "</span>" + "<span class=lbldata2>" + "Leave Process" + "</span>";
            this.RadioButtonList1.Items[2].Text = "<span class='fa fa-check-square-o fan'> </span>" + "<br>" + "<span class=lbldata>" + ds1.Tables[1].Rows[0]["appcount"].ToString() + "</span>" + "<span class=lbldata2>" + "Leave Approval" + "</span>";

            this.RadioButtonList1.Items[3].Text = "<span class='fa fa-calculator fan'> </span>" + "<br>" + "<span class=lbldata>" + ds1.Tables[1].Rows[0]["fappcount"].ToString() + "</span>" + "<span class=lbldata2>" + "Final Approval" + "</span>";
            //this.RadioButtonList1.Items[3].Text = "<span class='fa fa-calculator fan'> </span>" + "<br>" + "<span class=lbldata>" + ds1.Tables[1].Rows[0]["tappcount"].ToString() + "</span>" + "<span class=lbldata2>" + "Final Approval" + "</span>";
            this.RadioButtonList1.Items[4].Text = "<span class='fa fa-calculator fan'> </span>" + "<br>" + "<span class=lbldata>" + ds1.Tables[1].Rows[0]["tappcount"].ToString() + "</span>" + "<span class=lbldata2>" + "Leave Confirmed" + "</span>";
            //this.RadioButtonList1.Items[4].Text = "<span class='fa fa-credit-card fan'> </span>" + "<br>" + "<span class=lbldata>" + "0" + "</span>" + "<span class=lbldata2>" + "" + "</span>";

            //this.RadioButtonList1.Items[0].Text = "Leave Request" + "<br>" + "<span class=lbldata>" + ds1.Tables[2].Rows[0]["salReq"].ToString() + "</span>";
            //this.RadioButtonList1.Items[1].Text = "Leave Process" + "<br>" + "<span class='lbldata'>" + ds1.Tables[2].Rows[0]["inprocess"].ToString() + "</span>";
            //this.RadioButtonList1.Items[2].Text = "Leave Approval(DPT H)" + "<br>" + "<span class=lbldata>" + ds1.Tables[2].Rows[0]["approved"].ToString() + "</span>";
            //this.RadioButtonList1.Items[3].Text = "Leave Approval(HR H)" + "<br>" + "<span class=lbldata>" + ds1.Tables[2].Rows[0]["approved"].ToString() + "</span>";
            //this.RadioButtonList1.Items[4].Text = "Leave Confirmed" + "<br>" + "<span class=lbldata>" + ds1.Tables[2].Rows[0]["payconf"].ToString() + "</span>";


            Session["tblfirstaproved"] = ds1.Tables[2];
            Session["tbleaproved"] = ds1.Tables[3];
            Session["tblmaproved"] = ds1.Tables[4];

            // All Order
            DataTable dt = new DataTable();

            DataView dv = new DataView();
            dt = ((DataTable)ds1.Tables[0]).Copy();
            //dv = dt.DefaultView;
            // dv.RowFilter=("usrid='" + usrid + "'");
            //dv.Sort = ("Supcode");
            ViewState["tbltotalleav"] = dt;

            this.Data_Bind("gvLvReq", dt);


            //In-process
            dt = ((DataTable)ds1.Tables[0]).Copy();
            dv = dt.DefaultView;
            //dv.RowFilter = ("sostatus = 'In-process' or  sostatus = 'Request' ");
            dv.RowFilter = ("lvstatus='Request' ");
            this.Data_Bind("gvInprocess", dv.ToTable());



            //Approved
            dt = ((DataTable)ds1.Tables[0]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("lvstatus = 'In-process' ");
            //dv.RowFilter = ("sostatus = 'Approved' or sostatus = 'In-process' ");
            this.Data_Bind("gvApproved", dv.ToTable());

            //Final Approved
            dt = ((DataTable)ds1.Tables[0]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("lvstatus = 'Final Approved' ");
            //dv.RowFilter = ("sostatus = 'Approved' or sostatus = 'In-process' ");
            this.Data_Bind("gvfiApproved", dv.ToTable());




            //Confirm
            dt = ((DataTable)ds1.Tables[0]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("lvstatus = 'Approved' ");
            //dv.RowFilter = ("sostatus = 'Approved' or sostatus = 'In-process' ");
            this.Data_Bind("gvConfirm", dv.ToTable());

        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            this.lblprintstkl.Text = "";
            string value = this.RadioButtonList1.SelectedValue.ToString();


            switch (value)
            {
                case "0":


                    this.pnlallReq.Visible = true;
                    this.PnlProcess.Visible = false;
                    this.PnlApp.Visible = false;
                    this.pnlFApp.Visible = false;
                    this.PnlConfrm.Visible = false;



                    this.RadioButtonList1.Items[0].Attributes["style"] = "background: #189697; display:block; -webkit-border-radius: 5px;-moz-border-radius: 5px;border-radius: 5px;";
                    //this.RadioButtonList1.Items[0].Attributes.Add("class","lblactive");
                    //("class", "hidden");
                    // this.RadioButtonList1.Items[0].Attributes.CssStyle.ToString() = "lblactive";
                    //this.RadioButtonList1.Items[0].Attributes["style"] = "background-color:#13A6A8; font-size:16px; -webkit-border-radius: 10px; -moz-border-radius: 10px; border-radius: 10px;  width:30px;";   

                    break;

                case "1":
                    this.pnlallReq.Visible = false;
                    this.PnlProcess.Visible = true;
                    this.PnlApp.Visible = false;
                    this.pnlFApp.Visible = false;
                    this.PnlConfrm.Visible = false;
                    //this.Panelpayment.Visible = false;
                    //this.PanelDispatch.Visible = false;
                    //this.PanelDelivery.Visible = false;
                    //this.PanelInv.Visible = false;
                    this.RadioButtonList1.Items[1].Attributes["style"] = "background: #189697; display:block; -webkit-border-radius: 5px;-moz-border-radius: 5px;border-radius: 5px;";

                    //this.RadioButtonList1.Items[1].Attributes.Add("class", "lblactive");

                    // this.RadioButtonList1.Items[1].Attributes["style"] = "background-color:blue;";      
                    break;
                case "2":
                    this.pnlallReq.Visible = false;
                    this.PnlProcess.Visible = false;
                    this.PnlApp.Visible = true;
                    this.pnlFApp.Visible = false;
                    this.PnlConfrm.Visible = false;
                    //this.Panelpayment.Visible = false;
                    //this.PanelDispatch.Visible = false;
                    //this.PanelDelivery.Visible = false;
                    //this.PanelInv.Visible = false;
                    this.RadioButtonList1.Items[2].Attributes["style"] = "background: #189697; display:block; -webkit-border-radius: 5px;-moz-border-radius: 5px;border-radius: 5px;";


                    break;


                case "3":
                    this.pnlallReq.Visible = false;
                    this.PnlProcess.Visible = false;
                    this.PnlApp.Visible = false;
                    this.pnlFApp.Visible = true;
                    this.PnlConfrm.Visible = false;
                    //this.Panelpayment.Visible = false;
                    //this.PanelDispatch.Visible = false;
                    //this.PanelDelivery.Visible = false;
                    //this.PanelInv.Visible = false;
                    this.RadioButtonList1.Items[3].Attributes["style"] = "background: #189697; display:block; -webkit-border-radius: 5px;-moz-border-radius: 5px;border-radius: 5px;";

                    break;

                case "4":
                    this.pnlallReq.Visible = false;
                    this.PnlProcess.Visible = false;
                    this.PnlApp.Visible = false;
                    this.pnlFApp.Visible = false;
                    this.PnlConfrm.Visible = true;
                    //this.Panelpayment.Visible = false;
                    //this.PanelDispatch.Visible = false;
                    //this.PanelDelivery.Visible = false;
                    //this.PanelInv.Visible = false;
                    this.RadioButtonList1.Items[4].Attributes["style"] = "background: #189697; display:block; -webkit-border-radius: 5px;-moz-border-radius: 5px;border-radius: 5px;";

                    break;


            }
        }

        protected void gvProSlInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyOrderPrint");
                //Hashtable hst = (Hashtable)Session["tblLogin"];
                //string comcod = hst["comcod"].ToString();
                //string centrid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "centrid")).ToString();
                //string orderno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "orderno")).ToString();

                //hlink1.NavigateUrl = "~/F_23_SaM/Print.aspx?Type=OrderPrint&comcod=" + comcod + "&centrid=" + centrid + "&orderno=" + orderno;

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string empid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "empid")).ToString();
                string strtdat = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "strtdat")).ToString();
                string ltrnid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ltrnid")).ToString();




                //hlink1.NavigateUrl = "~/F_20_Service/Ser_Print.aspx?Type=ProReceived&comcod=" + comcod + "&centrid=" + centrid + "&recvno=" + recvno + "&imesimeno=" + imesimeno;

                hlink1.NavigateUrl = "~/F_81_Hrm/F_92_Mgt/PrintLeaveInterface.aspx?Type=ApplyPrint&empid=" + empid + "&strtdat=" + strtdat + "&LeaveId=" + ltrnid;
            }
        }

        //private DataTable HiddenSameData(DataTable dt1)
        //{
        //    if (dt1.Rows.Count == 0)
        //        return dt1;

        //    string centrid = dt1.Rows[0]["centrid"].ToString();
        //    for (int j = 1; j < dt1.Rows.Count; j++)
        //    {
        //        if (dt1.Rows[j]["centrid"].ToString() == centrid)
        //        {
        //            centrid = dt1.Rows[j]["centrid"].ToString();
        //            dt1.Rows[j]["centrdesc"] = "";
        //        }

        //        else
        //            centrid = dt1.Rows[j]["centrid"].ToString();
        //    }

        //    return dt1;
        //}
        private void Data_Bind(string gv, DataTable dt)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();

            switch (gv)
            {
                case "gvLvReq":
                    this.gvLvReq.DataSource = (dt);
                    this.gvLvReq.DataBind();

                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "gvInprocess": 
                    DataView dv = dt.Copy().DefaultView;
                    dv.RowFilter = ("susrid=" + userid);
                    dt = dv.ToTable(); 

                    this.gvInprocess.DataSource = (dt);
                    this.gvInprocess.DataBind();

                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "gvApproved":

                    DataView dv = dt.Copy().DefaultView;
                    dv.RowFilter = ("susrid=" + userid);
                    dt = dv.ToTable();

                    this.gvApproved.DataSource = (dt);
                    this.gvApproved.DataBind();

                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "gvfiApproved":
                    this.gvfiApproved.DataSource = (dt);
                    this.gvfiApproved.DataBind();
                    break;



                case "gvConfirm":
                    this.gvConfirm.DataSource = (dt);
                    this.gvConfirm.DataBind();

                    if (dt.Rows.Count == 0)
                        return;
                    break;


            }




        }



        protected void gvInprocess_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HylvPrint");
                HyperLink hlink3 = (HyperLink)e.Row.FindControl("lnkbtnApp");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string userid = hst["usrid"].ToString();

                string refno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "refno")).ToString();
                string suserid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "susrid")).ToString();
                string ltrnid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ltrnid")).ToString();
                string aplydat = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "aplydat")).ToString("dd-MMM-yyyy");
                hlink3.NavigateUrl = "~/F_81_Hrm/F_84_Lea/EmpLvApproval.aspx?Type=Ind&comcod=" + comcod + "&refno=" + refno + "&ltrnid=" + ltrnid + "&Date=" + aplydat;

                hlink3.Enabled = (userid == suserid) ? true : false;
                hlink3.Attributes["style"] = (userid == suserid) ? "background:blue;" : " background:red;";

                //HyperLink hlink1 = (HyperLink)e.Row.FindControl("HylvPrint");
                //HyperLink hlink3 = (HyperLink)e.Row.FindControl("lnkbtnApp");

                //Hashtable hst = (Hashtable)Session["tblLogin"];
                //string comcod = hst["comcod"].ToString();
                //string refno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "refno")).ToString();
                //string ltrnid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ltrnid")).ToString();
                //string aplydat = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "aplydat")).ToString("dd-MMM-yyyy");

                //hlink3.NavigateUrl = "~/F_81_Hrm/F_84_Lea/EmpLvApproval.aspx?Type=Ind&comcod=" + comcod + "&refno=" + refno + "&ltrnid=" + ltrnid + "&Date=" + aplydat;



            }
        }
        protected void gvApproved_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HylvPrint");
                HyperLink hlink3 = (HyperLink)e.Row.FindControl("lnkbtnApp");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string usrid = hst["usrid"].ToString();
                string refno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "refno")).ToString();
                string urefno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "refno")).ToString();
                string ltrnid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ltrnid")).ToString();
                string aplydat = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "aplydat")).ToString("dd-MMM-yyyy");
                DataTable dt = (DataTable)Session["tblfirstaproved"];
                DataTable dte = (DataTable)Session["tblmaproved"];


                DataRow[] dr1 = dt.Select("usrid='" + usrid + "' and centrid='" + refno + "'");
                DataRow[] dre = dte.Select("usrid='" + usrid + "'");
                hlink3.Enabled = dre.Length > 0 ? true : ((dr1.Length > 0) ? true : false);
                hlink3.Attributes["style"] = (dre.Length > 0) ? "background:blue;" : ((dr1.Length > 0) ? "background:blue;" : " background:red;");
                // Session["tblaproved"] = ds1.Tables[2];
                hlink3.NavigateUrl = "~/F_81_Hrm/F_84_Lea/EmpLvApproval.aspx?Type=Ind&comcod=" + comcod + "&refno=" + refno + "&ltrnid=" + ltrnid + "&Date=" + aplydat;



            }
        }
        protected void gvLvReq_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyperApplyPrint");


                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string empid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "empid")).ToString();
                string strtdat = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "strtdat")).ToString();
                string ltrnid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ltrnid")).ToString();
                //hlink1.NavigateUrl = "~/F_20_Service/Ser_Print.aspx?Type=ProReceived&comcod=" + comcod + "&centrid=" + centrid + "&recvno=" + recvno + "&imesimeno=" + imesimeno;

                hlink1.NavigateUrl = "~/F_81_Hrm/F_92_Mgt/PrintLeaveInterface.aspx?Type=ApplyPrint&empid=" + empid + "&strtdat=" + strtdat + "&LeaveId=" + ltrnid;


            }
        }

        protected void gvfiApproved_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataTable dte = (DataTable)Session["tblmaproved"];
                DataTable dt = (DataTable)Session["tbleaproved"];
                HyperLink hlink3 = (HyperLink)e.Row.FindControl("lnkbtnAppfi");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string usrid = hst["usrid"].ToString();
                string refno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "refno")).ToString();
                string urefno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "refno")).ToString();
                string ltrnid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ltrnid")).ToString();
                string aplydat = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "aplydat")).ToString("dd-MMM-yyyy");
                switch (comcod)
                {
                    case "3348": //Credence
                        break;

                    default:
                        DataRow[] dr1 = dt.Select("usrid='" + usrid + "' and centrid='" + refno + "'");
                        DataRow[] dre = dte.Select("usrid='" + usrid + "'");
                        hlink3.Enabled = dre.Length > 0 ? true : ((dr1.Length > 0) ? true : false);
                        hlink3.Attributes["style"] = (dre.Length > 0) ? "background:blue;" : ((dr1.Length > 0) ? "background:blue;" : " background:red;");
                        break;
                }
                // Session["tblaproved"] = ds1.Tables[2];
                hlink3.NavigateUrl = "~/F_81_Hrm/F_84_Lea/EmpLvApproval.aspx?Type=App&comcod=" + comcod + "&refno=" + refno + "&ltrnid=" + ltrnid + "&Date=" + aplydat;



            }
        }

        protected void lnkRemove_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["delete"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string usrid = hst["usrid"].ToString();
            int index = row.RowIndex;
            string empid = ((Label)this.gvInprocess.Rows[index].FindControl("lblgvempid")).Text.ToString();
            string leavid = ((Label)this.gvInprocess.Rows[index].FindControl("lblLeavId")).Text.ToString();

            DataTable dt = (DataTable)ViewState["tbltotalleav"];
            bool result = accData.UpdateTransInfo(comcod, "DBO_HRM.SP_REPORT_HR_INTERFACE", "DELETELEAVEINFO", leavid, empid, usrid, "", "", "", "", "", "", "", "", "", "", "");
            if (result)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Data deleted successfully')", true);
                int ins = this.gvInprocess.PageSize * this.gvInprocess.PageIndex + index;
                dt.Rows[ins].Delete();
                ViewState.Remove("tbltotalleav");
                DataView dv = dt.DefaultView;
                ViewState["tbltotalleav"] = dv.ToTable();
            }
            this.SaleRequRpt();


        }

        protected void lnkRemoveApp_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["delete"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string usrid = hst["usrid"].ToString();
            int index = row.RowIndex;
            string empid = ((Label)this.gvApproved.Rows[index].FindControl("lblgvempid")).Text.ToString();
            string leavid = ((Label)this.gvApproved.Rows[index].FindControl("lblLeavId")).Text.ToString();
            //ViewState["tbltotalleav"] = dt;
            DataTable dt = (DataTable)ViewState["tbltotalleav"];
            bool result = accData.UpdateTransInfo(comcod, "DBO_HRM.SP_REPORT_HR_INTERFACE", "DELETELEAVEINFO", leavid, empid, usrid, "", "", "", "", "", "", "", "", "", "", "");
            if (result)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Data deleted successfully')", true);
                int ins = this.gvApproved.PageSize * this.gvApproved.PageIndex + index;
                dt.Rows[ins].Delete();
                ViewState.Remove("tbltotalleav");
                DataView dv = dt.DefaultView;
                ViewState["tbltotalleav"] = dv.ToTable();
            }
            this.SaleRequRpt();

        }

        protected void lnkRemoveFAp_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["delete"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string usrid = hst["usrid"].ToString();
            int index = row.RowIndex;
            string empid = ((Label)this.gvfiApproved.Rows[index].FindControl("lblgvempid")).Text.ToString();
            string leavid = ((Label)this.gvfiApproved.Rows[index].FindControl("lblLeavId")).Text.ToString();
            //ViewState["tbltotalleav"] = dt;
            DataTable dt = (DataTable)ViewState["tbltotalleav"];
            bool result = accData.UpdateTransInfo(comcod, "DBO_HRM.SP_REPORT_HR_INTERFACE", "DELETELEAVEINFO", leavid, empid, usrid, "", "", "", "", "", "", "", "", "", "", "");
            if (result)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Data deleted successfully')", true);
                int ins = this.gvfiApproved.PageSize * this.gvfiApproved.PageIndex + index;
                dt.Rows[ins].Delete();
                ViewState.Remove("tbltotalleav");
                DataView dv = dt.DefaultView;
                ViewState["tbltotalleav"] = dv.ToTable();
            }
            this.SaleRequRpt();
        }
    }
}