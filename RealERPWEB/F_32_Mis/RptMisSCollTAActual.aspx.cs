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
namespace RealERPWEB.F_32_Mis
{ 

    
public partial class RptMisSCollTAActual : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                string Date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = Convert.ToDateTime("01" + Date.Substring(2)).ToString("dd-MMM-yyyy");
                this.txttodate.Text = Convert.ToDateTime(txtfromdate.Text.Trim()).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                string Type = this.Request.QueryString["Type"].ToString();
                this.ViewSection();



            }

        }

        protected void ViewSection()
        {
            string mRepID = Request.QueryString["Type"].ToString();
            switch (mRepID)
            {
                case "RptMonSCollTar":
                    this.MultiView1.ActiveViewIndex = 0;

                    break;




            }

        }








        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string mRepID = Request.QueryString["Type"].ToString();
            switch (mRepID)
            {
                case "RptMonSCollTar":
                    this.ShowMonSCollTar();
                    break;







            }

        }
        private void ShowMonSCollTar()
        {
            try
            {
                ViewState.Remove("tblscolltar");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
                string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_MISMKTASAL", "RPTDWISESALVSCOLTAR", frmdate, todate, "", "", "", "", "", "", "");
                if (ds1 == null)
                {
                    this.gvSalVsColl.DataSource = null;
                    this.gvSalVsColl.DataBind();
                    return;
                }
                ViewState["tblscolltar"] = this.HiddenSameData(ds1.Tables[0]);
                this.Data_Bind();
                ds1.Dispose();




            }

            catch (Exception ex)
            {


            }

        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string type = this.Request.QueryString["Type"].ToString().Trim();
            string deptmcode = dt1.Rows[0]["deptmcode"].ToString();
            string deptscode = dt1.Rows[0]["deptscode"].ToString();

            switch (type)
            {
                case "RptMonSCollTar":

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["deptmcode"].ToString() == deptmcode && dt1.Rows[j]["deptscode"].ToString() == deptscode)
                        {

                            dt1.Rows[j]["deptmname"] = "";
                            dt1.Rows[j]["deptsname"] = "";

                        }

                        else
                        {

                            if (dt1.Rows[j]["deptmcode"].ToString() == deptmcode)
                                dt1.Rows[j]["deptmname"] = "";

                            if (dt1.Rows[j]["deptscode"].ToString() == deptscode)
                                dt1.Rows[j]["deptsname"] = "";
                        }

                        deptmcode = dt1.Rows[j]["deptmcode"].ToString();
                        deptscode = dt1.Rows[j]["deptscode"].ToString();

                    }

                    break;



            }


            return dt1;

        }
        private void Data_Bind()
        {

            string mRepID = Request.QueryString["Type"].ToString();
            switch (mRepID)
            {
                case "RptMonSCollTar":
                    this.gvSalVsColl.DataSource = (DataTable)ViewState["tblscolltar"];
                    this.gvSalVsColl.DataBind();
                    break;
            }





        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            string mRepID = Request.QueryString["Type"].ToString();
            switch (mRepID)
            {
                case "RptMonSCollTar":
                    this.PrintMScolltarVsAchieve();
                    break;
            }

        }



        private void PrintMScolltarVsAchieve()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)ViewState["tblscolltar"];
            ReportDocument rptsale = new RealERPRPT.R_32_Mis.rptMonSaleCollTVsAch();
            TextObject rptCname = rptsale.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            rptCname.Text = comnam;
            TextObject txtTitle = rptsale.ReportDefinition.ReportObjects["txtTitle"] as TextObject;
            txtTitle.Text = "Monthly Sales & Collection Target for the month of " + Convert.ToDateTime(this.txttodate.Text).ToString("MMM, yyyy");
            TextObject txtuserinfo = rptsale.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptsale.SetDataSource(dt);
            Session["Report1"] = rptsale;
            lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }
        protected void gvSalVsColl_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label Deptdesc = (Label)e.Row.FindControl("lblgvDepartment");
                Label lgvmonsalamt = (Label)e.Row.FindControl("lgvmonsalamt");
                Label lgvmoncollamt = (Label)e.Row.FindControl("lgvmoncollamt");
                Label lgvamonsalamt = (Label)e.Row.FindControl("lgvamonsalamt");
                Label lgvamoncollamt = (Label)e.Row.FindControl("lgvamoncollamt");
                Label lgvpmonsalamt = (Label)e.Row.FindControl("lgvpmonsalamt");
                Label lgvpmoncollamt = (Label)e.Row.FindControl("lgvpmoncollamt");
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "deptcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 3) == "AAA")
                {

                    Deptdesc.Font.Bold = true;
                    lgvmonsalamt.Font.Bold = true;
                    lgvmoncollamt.Font.Bold = true;
                    lgvamonsalamt.Font.Bold = true;
                    lgvamoncollamt.Font.Bold = true;
                    lgvpmonsalamt.Font.Bold = true;
                    lgvpmoncollamt.Font.Bold = true;
                    Deptdesc.Style.Add("text-align", "right");


                }

            }
        }
    }
}