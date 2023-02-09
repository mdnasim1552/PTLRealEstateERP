using Microsoft.Reporting.WinForms;
using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace RealERPWEB.F_32_Mis
{
    public partial class ProjTrialBalanceDayWise : System.Web.UI.Page
    {

        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Project Trail Balance Daywise";

                this.ImgbtnFindProjind_Click(null, null);
                // this.GetResGroup();

                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfromDate.Text = "01" + date.Substring(2);
                this.txttodate.Text = Convert.ToDateTime(this.txtfromDate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                this.ImgbtnFindProjind_Click(null, null);

            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        private void GetResGroup()
        {

            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB_Daywise", "GETRESMAINGROUP", "", "", "", "", "", "", "", "", "");

                this.ddlProGroup.DataSource = ds1.Tables[0];
                this.ddlProGroup.DataTextField = "resdesc";
                this.ddlProGroup.DataValueField = "rescode";
                this.ddlProGroup.DataBind();
                ds1.Dispose();
            }


            catch (Exception ex)
            {

                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);


            }


        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            // string type = this.Request.QueryString["Type"].ToString().Trim();
            this.RtpPrjTrBalGen();

        }

        protected void ImgbtnFindProjind_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            this.MultiView1.ActiveViewIndex = 0;
            //string filter = (this.Request.QueryString["prjcode"].ToString()).Length == 0 ? "%" + this.txtSearchpIndp.Text.Trim() + "%" : this.Request.QueryString["prjcode"].ToString() + "%";
            // string pactcode = (this.Request.QueryString["Type"].ToString() == "LandPrj") ? "16%" : "4[1-9]%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB_Daywise", "GETPROJECTNAME", "", "%");
            if (ds1.Tables[0].Rows.Count == 0)
                return;

            DataTable dt1 = ds1.Tables[0];
            this.ddlProjectInd.DataSource = dt1;
            this.ddlProjectInd.DataTextField = "actdesc1";
            this.ddlProjectInd.DataValueField = "actcode";
            this.ddlProjectInd.DataBind();

        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            Session.Remove("tblprjtbl");

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string frmdate = this.txtfromDate.Text.ToString();
            string todate = this.txttodate.Text.ToString();

            string actcode = (((ASTUtility.Right(this.ddlProjectInd.SelectedValue, 10) == "0000000000") ? this.ddlProjectInd.SelectedValue.ToString().Substring(0, 2)
                : (ASTUtility.Right(this.ddlProjectInd.SelectedValue, 8) == "00000000") ? this.ddlProjectInd.SelectedValue.ToString().Substring(0, 4)
                : (ASTUtility.Right(this.ddlProjectInd.SelectedValue, 4) == "0000") ? this.ddlProjectInd.SelectedValue.ToString().Substring(0, 8) : this.ddlProjectInd.SelectedValue.ToString()) + "%");

            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            mRptGroup = mRptGroup == "0" ? "2" : mRptGroup == "1" ? "4" : mRptGroup == "2" ? "7" : mRptGroup == "3" ? "9" : "12";// (mRptGroup == "0" ? "4" : (mRptGroup == "1" ? "7" : (mRptGroup == "2" ? "9" : "12")));
            string CallType = (ASTUtility.Left(actcode, 2) == "41") ? "RPT_PROJ_TRIALBAL" : (ASTUtility.Left(actcode, 2) == "16") ? "RPT_PROJ_TRIALBAL" : "RPTPROJTRIALBALHF";


            string advance = String.Empty;
            if (comcod == "1205" || comcod == "3351" || comcod == "3352" || comcod == "3101")
            {
                advance = "ADVANCE";
            }


            // DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", CallType, "", date1, actcode, mRptGroup, "", "", "", "", "");

            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB_Daywise", CallType, frmdate, todate, actcode, mRptGroup, "", advance, "", "", "");
            if (ds2 == null)
                return;
            if (ds2.Tables[0].Rows.Count == 0)
            {
                this.gvPrjtrbalance.DataSource = null;
                this.gvPrjtrbalance.DataBind();
                return;
            }

            DataTable dt = HiddenSameData(ds2.Tables[0]);
            //DataTable dt = ds2.Tables[0];
            Session["tblprjtbldaywise"] = dt;
            this.gvPrjtrbalance.DataSource = dt;
            this.gvPrjtrbalance.DataBind();

            Session["tblFooterdaywise"] = ds2.Tables[1];
            Session["tblPrjnamedaywise"] = ds2.Tables[2];


            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
            ((HyperLink)this.gvPrjtrbalance.HeaderRow.FindControl("hlbtntbCdataExel")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

            Session["Report1"] = gvPrjtrbalance;
            ((HyperLink)this.gvPrjtrbalance.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

        }

        private DataTable HiddenSameData(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;
            int i = 0;
            string grpcode;
            grpcode = dt1.Rows[0]["grp"].ToString();
            for (i = 1; i < dt1.Rows.Count; i++)
            {
                if (dt1.Rows[i]["grp"].ToString() == grpcode)
                {
                    grpcode = dt1.Rows[i]["grp"].ToString();
                    dt1.Rows[i]["grpdesc"] = "";
                }
                else
                {
                    grpcode = dt1.Rows[i]["grp"].ToString();
                }

            }
            return dt1;
        }


        protected void gvPrjtrbalance_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink actdesc = (HyperLink)e.Row.FindControl("HLgvDesc");
                Label OpenAmt = (Label)e.Row.FindControl("lgvOpnAmt");
                Label CurAmt = (Label)e.Row.FindControl("lgvCurAmt");
                Label CloseAmt = (Label)e.Row.FindControl("lgvCloseAmt");

                string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
                mRptGroup = (mRptGroup == "0" ? "4" : (mRptGroup == "1" ? "7" : (mRptGroup == "2" ? "9" : "12")));
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right((code), 10) == "0000000000")
                {
                    actdesc.Font.Bold = true;
                    OpenAmt.Font.Bold = true;
                    CurAmt.Font.Bold = true;
                    CloseAmt.Font.Bold = true;

                    actdesc.Attributes["style"] = "color:maroon;";
                }
                else if (code == "000000000001" || (ASTUtility.Right((code), 3) == "000") && mRptGroup == "12")
                {
                    actdesc.Font.Bold = true;
                    OpenAmt.Font.Bold = true;
                    CurAmt.Font.Bold = true;
                    CloseAmt.Font.Bold = true;

                }
                if (code == "999999996666" || code == "999999997777" || code == "999999999999" || code == "000000000002" || code == "000000000003" || code == "000000000004")
                {
                    actdesc.Font.Bold = true;
                    OpenAmt.Font.Bold = true;
                    CurAmt.Font.Bold = true;
                    CloseAmt.Font.Bold = true;
                }
                if (code == "AAAAAAAAAAAA")
                {
                    actdesc.Style.Add("text-align", "Left");
                }


                //if (e.Row.RowType != DataControlRowType.DataRow)
                //    return;
                //string rescode1 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode1")).ToString();
                //HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDesc");
                //string Actcode = this.ddlProjectInd.SelectedValue.ToString();
                //string Date1 = Convert.ToDateTime(this.txttodate).ToString("dd-MMM-yyy");
                //string rescode = ((Label)e.Row.FindControl("lblgvCode")).Text;
                //if (ASTUtility.Left(rescode1, 2) == "51")
                //{
                //    hlink1.NavigateUrl = "RptProjectCollBrkDown.aspx?Type=PrjCol&pactcode=" + Actcode + "&Date1=" + Date1;
                //}

                //else if (ASTUtility.Right((code), 3) != "000" && code != "000000000001" && code != "999999999999" && code != "000000000002")
                //{
                //    hlink1.NavigateUrl = "RptProjectCollBrkDown.aspx?Type=SpLedger&pactcode=" + Actcode + "&Date1=" + Date1 + "&rescode=" + rescode;
                //}
            }
        }

        private void RtpPrjTrBalGen()
        {

            // Iqbal Nayan
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comnam"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string session = hst["session"].ToString();

            DataTable dt1 = (DataTable)Session["tblprjtbldaywise"];
            DataTable dt2 = (DataTable)Session["tblFooterdaywise"];
            DataTable dt3 = (DataTable)Session["tblPrjnamedaywise"];
            if (dt1.Rows.Count == 0)
                return;

            var lst = dt1.DataTableToList<RealEntity.C_32_Mis.EClassAcc_03.ProjectTrlBalDaywise>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_32_Mis.RptProjTrialBalanceDaywise", lst, null, null);
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("txtprjname", "Project Name: " + (dt3.Rows[0]["prjsdesc"]).ToString()));
            Rpt1.SetParameters(new ReportParameter("txtfdate", "From Date: " + Convert.ToDateTime(this.txtfromDate.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("txttodate", "To Date: " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("Rpttitle", "Project Trial Balance Daywise"));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
    }
}