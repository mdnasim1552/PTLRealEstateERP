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
using RealERPRDLC;
namespace RealERPWEB.F_32_Mis
{
    public partial class BalanceSheet : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                if ((!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp),
                        (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean(hst["permission"]))
                    Response.Redirect("~/AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"] == "CB") ? "Income Statement (Cash Basis)" : "Balance Sheet (Project)";
                //double day = System.DateTime.Today.ToString("dd")) - 1;
                this.txtDatefrom.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                this.ImgbtnFindProjind_Click(null, null);
                this.ddlRptGroup.SelectedIndex = 0;


            }
        }

        protected void ImgbtnFindProjind_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string filter = this.txtSearchpIndp.Text.Trim() + "%";

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PROJECT", "GETCONACCHEAD02", "4[1-7]%", filter, "", "", "", "", "", "", "");
            DataTable dt1 = ds1.Tables[0];
            this.ddlProjectInd.DataSource = dt1;
            this.ddlProjectInd.DataTextField = "actdesc1";
            this.ddlProjectInd.DataValueField = "actcode";
            this.ddlProjectInd.DataBind();
            if (Request.QueryString["prjcode"].Length > 0)
            {
                ddlProjectInd.SelectedValue = Request.QueryString["prjcode"].ToString();
                ddlProjectInd.Enabled = false;
            }
        }

        protected void lbtnOk_OnClick(object sender, EventArgs e)
        {
            Session.Remove("tblprjtbl");

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string date1 = this.txtDatefrom.Text.Substring(0, 11);
            string actcode = this.ddlProjectInd.SelectedValue.ToString();
            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : mRptGroup == "3" ? "9" : "12")));
            //string Calltype = (this.Request.QueryString["Type"] == "CB" && ASTUtility.Left (actcode, 2) == "41") ? "RPTINSTCASHBASIC" :
            // (this.Request.QueryString["Type"] == "CB" && ASTUtility.Left (actcode, 2) == "47") ? "RPTINSTCASHBASIC2" : "RPTINSTACTURALBASIC";

            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", "BLSPROBASIS", "", date1, actcode, mRptGroup, "", "", "", "", "");
            if (ds2 == null)
                return;
            if (ds2.Tables[0].Rows.Count == 0)
            {
                this.gvIncomeSt.DataSource = null;
                this.gvIncomeSt.DataBind();
                return;
            }

            DataTable dt = HiddenSameData(ds2.Tables[0]);
            Session["tblprjtbl"] = dt;
            //DataView dv = dt.DefaultView;
            //dv.RowFilter = ("rescode  not like '31%'");
            this.gvIncomeSt.DataSource = dt;
            this.gvIncomeSt.DataBind();

            //Session["tblFooter"] = ds2.Tables[1];
            //Session["tblPrjname"] = ds2.Tables[1];
        }

        private DataTable HiddenSameData(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;

            int j;
            string actcode;

            actcode = dt1.Rows[0]["actcode"].ToString();
            string grp = dt1.Rows[0]["grp"].ToString();
            for (j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["actcode"].ToString() == actcode)
                {

                    dt1.Rows[j]["actdesc"] = "";

                }
                if (dt1.Rows[j]["grp"].ToString() == grp)
                {


                    dt1.Rows[j]["grpdesc"] = "";
                }
                actcode = dt1.Rows[j]["actcode"].ToString();


            }
            return dt1;
        }
        protected void gvIncomeSt_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            Label lbldesc = (Label)e.Row.FindControl("lgcActDesc");
            Label lblamt = (Label)e.Row.FindControl("lgvAmt");
            //Label lblgvclobal = (Label)e.Row.FindControl ("lblgvclobal");
            //string mCOMCOD = comcod;
            //string mTRNDAT1 = this.txtDatefrom.Text;
            //string mTRNDAT2 = this.txtDateto.Text;
            //string opndate = this.txtOpeningDate.Text.Trim ();

            string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString().Trim();
            string actcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString().Trim();

            //string mACTDESC = Convert.ToString (DataBinder.Eval (e.Row.DataItem, "actdesc4")).ToString ().Trim ();





            if (code == "")
            {
                return;
            }
            if (actcode == "01DAAAAA" || actcode == "02GAAAAA" || actcode == "210900119999")
            {
                lbldesc.Style.Add("color", "blue");
                lblamt.Style.Add("color", "blue");
            }
            if (actcode == "210900119999")
            {
                lbldesc.Style.Add("color", "green");
                lblamt.Style.Add("color", "green");
            }
            if (ASTUtility.Right(actcode, 4) == "0000")
            {
                lbldesc.Style.Add("color", "brown");
                lblamt.Style.Add("color", "brown");
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comcod = hst["comcod"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string proj = this.ddlProjectInd.SelectedItem.Text.Substring(17);


            ////string hostname = hst["hostname"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            DataTable dt1 = (DataTable)Session["tblprjtbl"];
            var list = dt1.DataTableToList<RealEntity.C_32_Mis.EClassAcc_03.RptBalSheet>();
            LocalReport Rpt1 = new LocalReport();

            //if (comcod == "3348")
            //{
            //    Rpt1 = RDLCAccountSetup.GetLocalReport("R_32_Mis.RptProBalSheetCredence", list, null, null);
            //}
            //else
            //{
            Rpt1 = RDLCAccountSetup.GetLocalReport("R_32_Mis.RptProBalSheet", list, null, null);
            //}

            Rpt1 = RDLCAccountSetup.GetLocalReport("R_32_Mis.RptProBalSheet", list, null, null);
            Rpt1.SetParameters(new ReportParameter("comname", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("txtprint", printFooter));
            Rpt1.SetParameters(new ReportParameter("project", proj));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
    }
}