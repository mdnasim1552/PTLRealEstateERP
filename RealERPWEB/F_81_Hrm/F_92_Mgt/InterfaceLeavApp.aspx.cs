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
        Common compUtility = new Common();
        bool sup_app = false, dpthead_app = false, mgt_app = false;
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
                GetStep();                

                this.SaleRequRpt();
                this.RadioButtonList1_SelectedIndexChanged(null, null);

            }
        }
        private void GetStep()
        {
            DataSet copSetup = compUtility.GetCompUtility();
            if (copSetup == null)
                return;
            sup_app = copSetup.Tables[0].Rows.Count == 0 ? false : Convert.ToBoolean(copSetup.Tables[0].Rows[0]["LVAPP_SUPERVISOR"]);
            dpthead_app = copSetup.Tables[0].Rows.Count == 0 ? false : Convert.ToBoolean(copSetup.Tables[0].Rows[0]["LVAPP_DPTHEAD"]);
            mgt_app = copSetup.Tables[0].Rows.Count == 0 ? false : Convert.ToBoolean(copSetup.Tables[0].Rows[0]["LVAPP_MGTHEAD"]);

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
            string usrid = hst["usrid"].ToString();// (this.Request.QueryString["Type"] == "Ind") || (this.Request.QueryString["Type"] == "DeptHead") ? hst["usrid"].ToString() : "";
            string fDate = Convert.ToDateTime(this.txFdate.Text).ToString("dd-MMM-yyyy");
            string tDate = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");

            string type = "";//(this.Request.QueryString["Type"]) == "Ind" || (this.Request.QueryString["Type"] == "DeptHead") ? "" : "Management";
            string DeptHead = "";//(this.Request.QueryString["Type"]) == "DeptHead" ? "DeptHead" : "";


            DataSet ds1 = accData.GetTransInfo(comcod, "DBO_HRM.SP_REPORT_HR_MGT_INTERFACE", "GETLEAVEREQUEST", fDate, tDate, usrid, type, DeptHead, "", "", "", "");
            if (ds1 == null)
                return;

            this.RadioButtonList1.Items[0].Text = "<h4 class='text-center'><span class='lbldata'>" + ds1.Tables[1].Rows[0]["tcount"].ToString() + "</span></h4>" + "<span class='lbldata2'>" + "Leave Request" + "</span>";
            this.RadioButtonList1.Items[1].Text = "<h4 class='text-center'><span class='lbldata'>" + ds1.Tables[1].Rows[0]["reqcount"].ToString() + "</span></h4>" + "<span class=lbldata2>" + "Leave Process" + "</span>";
            this.RadioButtonList1.Items[2].Text = "<h4 class='text-center'><span class='lbldata'>" + ds1.Tables[1].Rows[0]["appcount"].ToString() + "</span></h4>" + "<span class=lbldata2>" + "Leave Approval" + "</span>";
            this.RadioButtonList1.Items[3].Text = "<h4 class='text-center'><span class='lbldata'>" + ds1.Tables[1].Rows[0]["fappcount"].ToString() + "</span></h4>" + "<span class=lbldata2>" + "Final Approval" + "</span>";
            this.RadioButtonList1.Items[4].Text = "<h4 class='text-center'><span class='lbldata'>" + ds1.Tables[1].Rows[0]["tappcount"].ToString() + "</span></h4>" + "<span class=lbldata2>" + "Leave Confirmed" + "</span>";

             
           
            
            // All Order
            DataTable dt = new DataTable();

            DataView dv = new DataView();
            dt = ((DataTable)ds1.Tables[0]).Copy();           
            ViewState["tbltotalleav"] = dt;
            this.Data_Bind("gvLvReq", dt);
             
            //In-process
            dt = ((DataTable)ds1.Tables[0]).Copy();
            dv = dt.DefaultView;
            //dv.RowFilter = ("sostatus = 'In-process' or  sostatus = 'Request' ");
            dv.RowFilter = ("supstatus='' ");
            this.Data_Bind("gvInprocess", dv.ToTable());



            //Approved
            dt = ((DataTable)ds1.Tables[0]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("dptstatus = '' and  supstatus<>''");
            //dv.RowFilter = ("sostatus = 'Approved' or sostatus = 'In-process' ");
            this.Data_Bind("gvApproved", dv.ToTable());

            //Final Approved
            dt = ((DataTable)ds1.Tables[0]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("dptstatus <> '' and  supstatus<>'' and  mgtstatus<>''");
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
                    
                    break;

                case "1":
                    this.pnlallReq.Visible = false;
                    this.PnlProcess.Visible = true;
                    this.PnlApp.Visible = false;
                    this.pnlFApp.Visible = false;
                    this.PnlConfrm.Visible = false;                     
                    this.RadioButtonList1.Items[1].Attributes["style"] = "background: #189697; display:block; -webkit-border-radius: 5px;-moz-border-radius: 5px;border-radius: 5px;";
 
                    break;
                case "2":
                    this.pnlallReq.Visible = false;
                    this.PnlProcess.Visible = false;
                    this.PnlApp.Visible = true;
                    this.pnlFApp.Visible = false;
                    this.PnlConfrm.Visible = false;                   
                    this.RadioButtonList1.Items[2].Attributes["style"] = "background: #189697; display:block; -webkit-border-radius: 5px;-moz-border-radius: 5px;border-radius: 5px;";
                     
                    break;


                case "3":
                    this.pnlallReq.Visible = false;
                    this.PnlProcess.Visible = false;
                    this.PnlApp.Visible = false;
                    this.pnlFApp.Visible = true;
                    this.PnlConfrm.Visible = false;
                    
                    this.RadioButtonList1.Items[3].Attributes["style"] = "background: #189697; display:block; -webkit-border-radius: 5px;-moz-border-radius: 5px;border-radius: 5px;";

                    break;

                case "4":
                    this.pnlallReq.Visible = false;
                    this.PnlProcess.Visible = false;
                    this.PnlApp.Visible = false;
                    this.pnlFApp.Visible = false;
                    this.PnlConfrm.Visible = true;                    
                    this.RadioButtonList1.Items[4].Attributes["style"] = "background: #189697; display:block; -webkit-border-radius: 5px;-moz-border-radius: 5px;border-radius: 5px;";

                    break;


            }
        }

        protected void gvProSlInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyOrderPrint");
                
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string empid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "empid")).ToString();
                string strtdat = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "strtdat")).ToString();
                string ltrnid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ltrnid")).ToString();
                 
                hlink1.NavigateUrl = "~/F_81_Hrm/F_92_Mgt/PrintLeaveInterface.aspx?Type=ApplyPrint&empid=" + empid + "&strtdat=" + strtdat + "&LeaveId=" + ltrnid;
            }
        }

        
        private void Data_Bind(string gv, DataTable dt)
        {


            switch (gv)
            {
                case "gvLvReq":
                    this.gvLvReq.DataSource = (dt);
                    this.gvLvReq.DataBind();

                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "gvInprocess":
                    this.gvInprocess.DataSource = (dt);
                    this.gvInprocess.DataBind();

                    if (dt.Rows.Count == 0)
                        return;
                    break;
                case "gvApproved":
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
                HyperLink hlnEdit = (HyperLink)e.Row.FindControl("lnkbtnEditIN");
                LinkButton hlnDel = (LinkButton)e.Row.FindControl("lnkRemove");
                
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string userid = hst["usrid"].ToString();
                string empusrid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "empusrid")).ToString();
                string empid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "empid")).ToString();
                string strtdat = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "strtdat")).ToString();
                 
                string refno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "refno")).ToString();
                string suserid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "susrid")).ToString();
                string ltrnid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ltrnid")).ToString();
                string aplydat = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "aplydat")).ToString("dd-MMM-yyyy");
                hlink3.NavigateUrl = "~/F_81_Hrm/F_84_Lea/EmpLvApproval.aspx?Type=Ind&comcod=" + comcod + "&refno=" + refno + "&ltrnid=" + ltrnid + "&Date=" + aplydat;

                hlink3.Visible = (userid == suserid) ? true : false;
                hlnDel.Visible = (userid == empusrid) ? true : false;
                hlnEdit.Visible = (userid == empusrid) ? true : false;

                hlnEdit.NavigateUrl = "~/F_81_Hrm/F_84_Lea/MyLeave.aspx?Type=User&empid=" + empid + "&strtdat=" + strtdat + "&LeaveId=" + ltrnid;
                hlink1.NavigateUrl = "~/F_81_Hrm/F_92_Mgt/PrintLeaveInterface.aspx?Type=ApplyPrint&empid=" + empid + "&strtdat=" + strtdat + "&LeaveId=" + ltrnid;

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
                string userid = hst["usrid"].ToString();
                string strtdat = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "strtdat")).ToString();
                string refno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "refno")).ToString();
                string urefno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "refno")).ToString();
                string ltrnid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ltrnid")).ToString();
                string aplydat = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "aplydat")).ToString("dd-MMM-yyyy");
                string dptusid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "dptusid")).ToString();
                string empid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "empid")).ToString();
                hlink3.Visible = (userid == dptusid) ? true : false;                
                hlink3.NavigateUrl = "~/F_81_Hrm/F_84_Lea/EmpLvApproval.aspx?Type=Ind&comcod=" + comcod + "&refno=" + refno + "&ltrnid=" + ltrnid + "&Date=" + aplydat;
                hlink1.NavigateUrl = "~/F_81_Hrm/F_92_Mgt/PrintLeaveInterface.aspx?Type=ApplyPrint&empid=" + empid + "&strtdat=" + strtdat + "&LeaveId=" + ltrnid;

            }
        }
        protected void gvLvReq_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyperApplyPrint");
                HyperLink hlnEdit = (HyperLink)e.Row.FindControl("lnkbtnEditUser");
                

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string usrid = hst["usrid"].ToString();

                string empid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "empid")).ToString();
                string empusrid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "empusrid")).ToString();
                string strtdat = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "strtdat")).ToString();
                string ltrnid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ltrnid")).ToString();
                
                hlnEdit.Visible = (usrid == empusrid) ? true : false;               
                hlnEdit.NavigateUrl = "~/F_81_Hrm/F_84_Lea/MyLeave.aspx?Type=User&empid=" + empid + "&strtdat=" + strtdat + "&LeaveId=" + ltrnid;
                hlink1.NavigateUrl = "~/F_81_Hrm/F_92_Mgt/PrintLeaveInterface.aspx?Type=ApplyPrint&empid=" + empid + "&strtdat=" + strtdat + "&LeaveId=" + ltrnid;


            }
        }

        protected void gvfiApproved_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            { 
                //DataTable dte = (DataTable)Session["tblmaproved"];
                //DataTable dt = (DataTable)Session["tbleaproved"];
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
                        //DataRow[] dr1 = dt.Select("usrid='" + usrid + "' and centrid='" + refno + "'");
                        //DataRow[] dre = dte.Select("usrid='" + usrid + "'");
                        //hlink3.Enabled = dre.Length > 0 ? true : ((dr1.Length > 0) ? true : false);
                        //hlink3.Visible = dre.Length > 0 ? true : ((dr1.Length > 0) ? true : false);
                       
                        break;
                }
                 
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
              //  ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Data deleted successfully')", true);
                string Messaged = "Leave deleted successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentl('" + Messaged + "');", true);

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