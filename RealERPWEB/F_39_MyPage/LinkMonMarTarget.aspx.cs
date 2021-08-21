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
namespace RealERPWEB.F_39_MyPage
{
    public partial class LinkMonMarTarget : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ((Label)this.Master.FindControl("lblTitle")).Text = "Monthly Program";
                this.lblvalpgno.Text = this.Request.QueryString["prono"].ToString();
                this.lblvalref.Text = this.Request.QueryString["refno"].ToString();
                this.Get_Info();


            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }



        protected void Get_Info()
        {

            string comcod = this.GetComCode();
            string mPGNo = this.lblvalpgno.Text;
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_EMP_KPI", "RPTMARPROINFO", mPGNo, "",
                          "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tblmonpg"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
        }



        private void Data_Bind()
        {


            this.gvPrjInfo.DataSource = (DataTable)ViewState["tblmonpg"];
            this.gvPrjInfo.DataBind();



        }
        private void FooterCalculation()
        {


            //DataTable dt = (DataTable)ViewState["tblprocom"];
            //if(dt.Rows.Count==0)
            //    return;
            //((Label)this.gvPrjInfo.FooterRow.FindControl("lblgvFTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(duration)", "")) ?
            //                    0 : dt.Compute("sum(duration)", ""))).ToString("#,##0;(#,##0); ");



        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;


            string deptcod = dt1.Rows[0]["deptcod"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["deptcod"].ToString() == deptcod)
                    dt1.Rows[j]["deptdesc"] = "";
                deptcod = dt1.Rows[j]["deptcod"].ToString();
            }
            return dt1;

        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //DataTable dt = (DataTable)ViewState["tblmonpg"];

            //ReportDocument rptResource = new  RealERPRPT.R_05_MyPage.rptMonthlyProgram();
            //TextObject rpttxtComName = rptResource.ReportDefinition.ReportObjects["txtComName"] as TextObject;
            //rpttxtComName.Text = comnam;


            //TextObject txtProgramno = rptResource.ReportDefinition.ReportObjects["txtProgramno"] as TextObject;
            //txtProgramno.Text = "Program No: " + this.lblvalpgno.Text;
            //TextObject txtpgdate = rptResource.ReportDefinition.ReportObjects["txtpgdate"] as TextObject;
            //txtpgdate.Text = "Date: " +this.txtCurDate.Text;
            //TextObject txtRefno = rptResource.ReportDefinition.ReportObjects["txtRefno"] as TextObject;
            //txtRefno.Text = "Ref No: " + this.txtRefno.Text;
            //TextObject txtuserinfo = rptResource.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptResource.SetDataSource(dt);
            //Session["Report1"] = rptResource;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }







        private void SaveValue()
        {

            DateTime tstdat, tenddat, acstdat, acenddat;
            DataTable dt = (DataTable)ViewState["tblmonpg"];

            for (int i = 0; i < this.gvPrjInfo.Rows.Count; i++)
            {

                tstdat = ((((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvtStDate")).Text.Trim()) == "") ? Convert.ToDateTime("01-Jan-1900") : Convert.ToDateTime(((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvtStDate")).Text.Trim());
                tenddat = ((((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvTEndDate")).Text.Trim()) == "") ? Convert.ToDateTime("01-Jan-1900") : Convert.ToDateTime(((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvTEndDate")).Text.Trim());
                acstdat = ((((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtacStDate")).Text.Trim()) == "") ? Convert.ToDateTime("01-Jan-1900") : Convert.ToDateTime(((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtacStDate")).Text.Trim());
                acenddat = ((((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtacEndDate")).Text.Trim()) == "") ? Convert.ToDateTime("01-Jan-1900") : Convert.ToDateTime(((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtacEndDate")).Text.Trim());

                dt.Rows[i]["tstdat"] = tstdat;
                dt.Rows[i]["tenddat"] = tenddat;
                dt.Rows[i]["acstdat"] = acstdat;
                dt.Rows[i]["acenddat"] = acenddat;









            }

            ViewState["tblmonpg"] = dt;
            this.Data_Bind();


        }









        protected void gvPrjInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlnkgvcomments = (HyperLink)e.Row.FindControl("hlnkgvcomments");
                Label deloadv = (Label)e.Row.FindControl("lblgvdelay");
                string empid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "empid")).ToString();
                string prono = this.Request.QueryString["prono"].ToString();
                string refno = this.Request.QueryString["refno"].ToString();
                string actcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();
                string actdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actdesc")).ToString();
                string deloadvsign = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "deloadvsign")).ToString();
                string date = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "acenddat")).ToString("dd-MMM-yyyy");
                string sdate = Convert.ToDateTime(System.DateTime.Today).ToString("dd-MMM-yyyy");
                date = (date == "01-Jan-1900") ? sdate : date;
                if (deloadvsign == "delay")
                {
                    deloadv.Style.Add("color", "red");


                }

                hlnkgvcomments.NavigateUrl = "~/F_05_MyPage/LinkRptActiComments02.aspx?empid=" + empid + "&prono=" + prono + "&refno=" + refno + "&actcode=" + actcode + "&actdesc=" + actdesc;


            }



        }
    }
}



