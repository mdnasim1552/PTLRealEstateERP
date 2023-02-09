using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WinForms;
using RealERPLIB;
namespace RealERPWEB.F_17_Acc
{
    public partial class BgdvsExpenseAdmin : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Budget VS Expenses (Administrative)";
                //this.Master.Page.Title = "Budget VS Expenses (Administrative)";



                DateTime nowdate = DateTime.Now;
                DateTime yFday = new DateTime(nowdate.Year, 1, 1); //Year First Date or Specefic date
                DateTime yLDay = new DateTime(nowdate.Year, 12, 31);  //


                string fDate = yFday.ToString("dd-MMM-yyyy");
                string eDate = yLDay.ToString("dd-MMM-yyyy");
                this.txtDateFrom.Text = fDate;
                this.txtDateto.Text = eDate;
                CommonButton();

            }
        }


        public void CommonButton()
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace("%20", " "), (DataSet)Session["tblusrlog"]);
            ((Label)this.Master.FindControl("lblmsg")).Visible = false;
            ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;


            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            ((CheckBox)this.Master.FindControl("CheckBox1")).Visible = false;

            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }

        private string GetCompcode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }



        protected void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string frmdate = this.txtDateFrom.Text;
                string todate = this.txtDateto.Text;
                DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_BGD", "RPTBGDVSACT", frmdate, todate, "", "", "", "", "", "");

                Session["tblBgd"] = ds1.Tables[0];
                this.Data_Bind();
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }


        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblBgd"];
            this.gvBgdExpense.DataSource = dt;
            this.gvBgdExpense.DataBind();
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string txtuserinfo = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            DataTable dt1 = (DataTable)Session["tblBgd"];
            if (dt1 == null)
                return;



            var lst = dt1.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.RptBgdvsExpense>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptBgdvsExpense", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam.ToUpper()));
            Rpt1.SetParameters(new ReportParameter("footer", txtuserinfo));
            Rpt1.SetParameters(new ReportParameter("date", "(From " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + " )"));



            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void gvBgdExpense_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                string actcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();

                Label des = (Label)e.Row.FindControl("lblactdesc");
                Label bgdamt = (Label)e.Row.FindControl("lblbgdex");
                Label acamt = (Label)e.Row.FindControl("lblactex");
                Label prevamt = (Label)e.Row.FindControl("lblprvyex");
                // string act = actcode.Substring(1, 10);

                if (actcode.Substring(2, 10) == "0000000000")
                {
                    des.Attributes["style"] = "font-weight:bold;color:maroon";
                    bgdamt.Attributes["style"] = "font-weight:bold;color:maroon";
                    acamt.Attributes["style"] = "font-weight:bold;color:maroon";
                    prevamt.Attributes["style"] = "font-weight:bold;color:maroon";
                }

                if (actcode == "AAAAAAAAAAAA")
                {
                    des.Attributes["style"] = "font-weight:bold;color:blue";
                    bgdamt.Attributes["style"] = "font-weight:bold;color:blue";
                    acamt.Attributes["style"] = "font-weight:bold;color:blue";
                    prevamt.Attributes["style"] = "font-weight:bold;color:blue";
                }
            }
        }
    }
}