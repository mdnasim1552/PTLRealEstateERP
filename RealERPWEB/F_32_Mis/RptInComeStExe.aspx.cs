using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_32_Mis
{
    public partial class RptInComeStExe : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetProjectName();
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "INCOME STATEMENT- EXECUTION BASIS";
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void GetProjectName()
        {
            string comcod = this.GetComeCode();
            string txtSProject = this.txtSrcProject.Text.Trim();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_IS_BS_R2", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();



        }
        protected void imgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            Session.Remove("tblincome");
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_IS_BS_R2", "RPTINCOMESTATMENTINPRJEXE", "", date, pactcode, "000", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvIncome.DataSource = null;
                this.gvIncome.DataBind();
                return;
            }
            Session["tblincome"] = HiddenSameData(ds2.Tables[0]);
            this.txtpercnt.Text = Convert.ToDouble(ds2.Tables[1].Rows[0]["percnt"]).ToString("#,##0.0000;(#,##0.0000);") + "%";
            this.Data_Bind();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Income Statement (Execution Basis)";
                string eventdesc = "Show Report";
                string eventdesc2 = this.ddlProjectName.SelectedItem.Text;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string actcode = dt1.Rows[0]["actcode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["actcode"].ToString() == actcode)
                {
                    actcode = dt1.Rows[j]["actcode"].ToString();
                    dt1.Rows[j]["actdesc"] = "";

                }

                else
                {
                    actcode = dt1.Rows[j]["actcode"].ToString();
                }



            }
            return dt1;


        }


        private void Data_Bind()
        {
            this.gvIncome.DataSource = (DataTable)Session["tblincome"];
            this.gvIncome.DataBind();



        }


        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblincome"];
            //    double percent = Convert.ToDouble("0"+this.txtpercnt.Text.Trim().Replace("%",""));
            //    double bgdam=0, exam=0, diffam=0;
            //    for (int i = 0; i < dt.Rows.Count; i++) 
            //    {
            //        bgdam = Convert.ToDouble(dt.Rows[i]["bgdam"]);
            //        exam= (bgdam == 0) ? 0 : (bgdam * percent*0.01);
            //        diffam = Convert.ToDouble(dt.Rows[i]["trnam"]) - exam;
            //        dt.Rows[i]["bgdam"] = bgdam;
            //        dt.Rows[i]["exam"] = exam; ;
            //        dt.Rows[i]["diffam"] = diffam;

            //    }
            //    Session["tblincome"] = dt;
            //    this.Data_Bind();

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)Session["tblincome"];
            if (dt1.Rows.Count == 0)
                return;
            ReportDocument rptstk = new RealERPRPT.R_17_Acc.RptInComeStExeBas();

            TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            txtCompany.Text = comnam;
            TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["projectname"] as TextObject;
            txtprojectname.Text = this.ddlProjectName.SelectedItem.Text;
            TextObject txtpercntage = rptstk.ReportDefinition.ReportObjects["txtpercntage"] as TextObject;
            txtpercntage.Text = "Execution: " + this.txtpercnt.Text.Trim();
            TextObject txtdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            txtdate.Text = "Date: " + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Income Statement (Execution Basis)";
                string eventdesc = "Print Report";
                string eventdesc2 = this.ddlProjectName.SelectedItem.Text;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
            rptstk.SetDataSource(dt1);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        protected void gvIncome_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label acresdesc = (Label)e.Row.FindControl("lgcActDesc");
                Label lblBgdamt = (Label)e.Row.FindControl("lgvBgdamt");
                Label lgvActualamt = (Label)e.Row.FindControl("lgvActualamt");
                Label lgvexamt = (Label)e.Row.FindControl("lgvexamt");
                Label lgvDifamt = (Label)e.Row.FindControl("lgvDifamt");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    acresdesc.Font.Bold = true;
                    lblBgdamt.Font.Bold = true;
                    lgvActualamt.Font.Bold = true;
                    lgvexamt.Font.Bold = true;
                    lgvDifamt.Font.Bold = true;
                    acresdesc.Style.Add("text-align", "right");


                }

            }
        }
    }
}
