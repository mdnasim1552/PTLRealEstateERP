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
using System.Web.UI.WebControls;
namespace RealERPWEB.F_62_Mis
{
    public partial class RptMyInterface : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();

        public static string empid = "";
        public static string frmdate = "";
        public static string todate = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = "MY SERVICES INFORMATION";
                // this.SelectView();
                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetCompany();
                this.MultiView1.ActiveViewIndex = 0;

            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            //((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }



        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["hcomcod"].ToString());

        }

        private void GetCompany()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["hcomcod"].ToString();
            string userid = hst["usrid"].ToString();
            string txtCompany = "%" + this.txtSrcCompany.Text.Trim() + "%";
            DataSet ds5 = HRData.GetTransInfo(comcod, "SP_REPORT_EMP_DASEBOARD", "GETCOMPANYNAME", txtCompany, userid, "", "", "", "", "", "", "");
            this.ddlCompany.DataTextField = "actdesc";
            this.ddlCompany.DataValueField = "actcode";
            this.ddlCompany.DataSource = ds5.Tables[0];
            this.ddlCompany.DataBind();
            this.GetEmpName();
            //ds1.Dispose();

        }
        private void GetEmpName()
        {

            string comcod = this.GetComeCode();
            string txtSProject = "%" + this.txtEmpSrc.Text.Trim() + "%";
            string companyName = this.ddlCompany.SelectedValue.ToString();
            DataSet ds3 = HRData.GetTransInfo(comcod, "SP_REPORT_EMP_DASEBOARD", "GETMYTIDNAME", txtSProject, companyName, "", "", "", "", "", "", "");
            this.ddlEmpName.DataTextField = "empname1";
            //this.ddlEmpName.SelectedValue = "empname";
            this.ddlEmpName.DataValueField = "empid";
            this.ddlEmpName.DataSource = ds3.Tables[0];
            this.ddlEmpName.DataBind();
            Session["tblempname"] = ds3.Tables[0];

            ds3.Dispose();


            ViewState["tblemp"] = ds3.Tables[0];
        }



        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            this.PrintServices();
        }
        private void PrintServices()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)ViewState["tblservices"];
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("MMMM dd, yyyy");
            ReportDocument rptempservices = new RealERPRPT.R_62_Mis.RptEmpServices();
            TextObject txtempname = rptempservices.ReportDefinition.ReportObjects["txtempname"] as TextObject;
            txtempname.Text = "Name: " + this.ddlEmpName.SelectedItem.Text.Trim();
            TextObject rptdate = rptempservices.ReportDefinition.ReportObjects["date"] as TextObject;
            rptdate.Text = "Date: " + date;
            TextObject txtuserinfo = rptempservices.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptempservices.SetDataSource(dt);
            //string comcod = this.GetComeCode();
            string comcod = hst["comcod"].ToString();
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptempservices.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptempservices;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }



        protected void ibtnEmpList_Click(object sender, EventArgs e)
        {
            this.GetEmpName();
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            ViewState.Remove("tblgrph");
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";

            this.lblServHead.Visible = true;
            this.lbAttHead.Visible = true;
            this.lblgraph.Visible = true;
            this.lblleaveHis.Visible = true;
            this.EmpUserImg.Visible = true;
            this.AttHistoryGraph.Visible = true;
            this.hyplPreviewCv.Visible = true;
            this.Lbljobres.Visible = true;


            this.EmpUserImg.ImageUrl = "~/GetImage.aspx?ImgID=ImgUser";
            //ViewState.Remove("tblservices");
            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            string Date = this.txtDate.Text.Trim();
            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_REPORT_EMP_DASEBOARD", "RPTMYSERVICES", empid, Date, "", "", "", "", "", "", "");
            //this.lbldesg.Visible = true;

            if (ds1 == null)
            {
                this.gvempservices.DataSource = null;
                this.gvempservices.DataBind();
                return;
            }
            ViewState["tblservices"] = ds1.Tables[0];
            ViewState["tblAttHist"] = ds1.Tables[1];
            ViewState["tblLeaveStatus"] = ds1.Tables[2];
            Session["tblEmpimg"] = ds1.Tables[3];
            ViewState["tblJobRespon"] = ds1.Tables[4];


            if (ViewState["tblgrph"] == null)
            {
                DataTable tblt01 = new DataTable();
                tblt01.Columns.Add("yearmon", Type.GetType("System.String"));
                tblt01.Columns.Add("absnt", Type.GetType("System.Double"));
                tblt01.Columns.Add("acintime", Type.GetType("System.Double"));
                tblt01.Columns.Add("aclate", Type.GetType("System.Double"));
                tblt01.Columns.Add("leave", Type.GetType("System.Double"));
                ViewState["tblgrph"] = tblt01;

                this.ShowJobRespon();
            }

            DataTable dt3 = (DataTable)ViewState["tblAttHist"];


            DataTable dt1 = (DataTable)ViewState["tblgrph"];

            DataRow dr1 = dt1.NewRow();
            dr1["yearmon"] = "";
            dr1["absnt"] = Convert.ToDouble((Convert.IsDBNull(dt3.Compute("Sum(absnt)", "")) ? 0.00 : dt3.Compute("Sum(absnt)", ""))).ToString("#,##0;(#,##0)"); ;
            dr1["acintime"] = Convert.ToDouble((Convert.IsDBNull(dt3.Compute("Sum(acintime)", "")) ? 0.00 : dt3.Compute("Sum(acintime)", ""))).ToString("#,##0;(#,##0)"); ; ;
            dr1["aclate"] = Convert.ToDouble((Convert.IsDBNull(dt3.Compute("Sum(aclate)", "")) ? 0.00 : dt3.Compute("Sum(aclate)", ""))).ToString("#,##0;(#,##0)"); ; ;
            dr1["leave"] = Convert.ToDouble((Convert.IsDBNull(dt3.Compute("Sum(leave)", "")) ? 0.00 : dt3.Compute("Sum(leave)", ""))).ToString("#,##0;(#,##0)"); ; ;
            dt1.Rows.Add(dr1);
            ViewState["tblgrph"] = dt1;
            this.EmpUserImg.ImageUrl = "~/GetImage.aspx?ImgID=ImgEmp";
            this.Data_Bind();


        }

        private void Data_Bind()
        {
            try
            {
                DataTable dt = (DataTable)ViewState["tblservices"];

                this.gvempservices.DataSource = dt;
                this.gvempservices.DataBind();




                DataTable dt2 = (DataTable)ViewState["tblAttHist"];
                this.ShowAttHistoryGraph();
                this.RptAttHistroy.DataSource = dt2;
                this.RptAttHistroy.DataBind();


                DataTable dt3 = (DataTable)ViewState["tblLeaveStatus"];
                this.gvLeaveStatus.DataSource = dt3;
                this.gvLeaveStatus.DataBind();




                //HyperLink lnknextbtn = (HyperLink)this.gvLeaveStatus.HeaderRow.FindControl("hlnkbtnNext");
                string comcod = this.GetComeCode();
                //string ymonid = this.txtDate.Text.ToString();
                string frmdate = Convert.ToDateTime("01-Jan-" + this.txtDate.Text.Substring(7)).ToString("dd-MMM-yyyy");
                string todate = Convert.ToDateTime(frmdate).AddYears(1).AddDays(-1).ToString("dd-MMM-yyyy");
                string empid = this.ddlEmpName.SelectedValue.ToString().Trim();






                ////lnknextbtn.NavigateUrl = "~/F_82_App/LinkMyHRLeave.aspx?Type=EmpLeaveSt&empid=" + empid + "&frmdate=" + frmdate + "&todate=" + todate;

                ((HyperLink)this.gvLeaveStatus.HeaderRow.FindControl("hlnkbtnNext")).NavigateUrl = "LinkMyHRLeave.aspx?Type=EmpLeaveSt&empid=" + empid + "&frmdate=" + frmdate + "&todate=" + todate;





                DataTable dt4 = (DataTable)ViewState["tblEmpimg"];
                DataTable dt5 = (DataTable)ViewState["tblJobRespon"];



            }

            catch (Exception ex)
            { }

        }

        //protected void lnkTest_Click(object sender, EventArgs e)
        //{
        //    LinkButton lnk = sender as LinkButton;
        //    RepeaterItem ri = lnk.NamingContainer;
        //    Label lblRowId1 = ri.FindControl("lblRowID") as Label;
        //    int RowId = lblRowId1.Text;
        //    string s1 = RowId.ToString();
        //    Response.Write("<script>alert('" + s1 + "');</script>");
        //}
        protected void lnkBtn_Click(object sender, EventArgs e)
        {



            //string comcod = this.GetComeCode();
            //string frmdate = this.RptAttHistroy
            //string todate = Convert.ToDateTime(frmdate).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
            //string empid = this.ddlEmpName.SelectedValue.ToString().Trim();
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('RptMyAttendenceSheet.aspx?Type=MyAttn&comcod=" + comcod + "&empid=" + empid + "&frmdate=" + frmdate + "&todate=" + todate + "', target='_self');</script>";
        }
        protected void RptAttHistroy_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {


            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                HyperLink lnkyearmon = (HyperLink)e.Item.FindControl("hlnkbtnadd");
                string comcod = this.GetComeCode();
                string ymonid = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "ymonid")).ToString();
                string frmdate = Convert.ToDateTime(ymonid.Substring(4, 2) + "/01/" + ymonid.Substring(0, 4)).ToString("dd-MMM-yyyy");
                string todate = Convert.ToDateTime(frmdate).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                string empid = this.ddlEmpName.SelectedValue.ToString().Trim();
                lnkyearmon.NavigateUrl = "RptMyAttendenceSheet.aspx?Type=&empid=" + empid + "&frmdate=" + frmdate + "&todate=" + todate;
            }



            if (e.Item.ItemType == ListItemType.Footer)
            {

                DataTable dt3 = (DataTable)ViewState["tblAttHist"];
                ((Label)e.Item.FindControl("lblacintime")).Text = Convert.ToDouble((Convert.IsDBNull(dt3.Compute("Sum(acintime)", "")) ? 0.00 : dt3.Compute("Sum(acintime)", ""))).ToString("#,##0;(#,##0)");
                ((Label)e.Item.FindControl("lbltotalabs")).Text = Convert.ToDouble((Convert.IsDBNull(dt3.Compute("Sum(absnt)", "")) ? 0.00 : dt3.Compute("Sum(absnt)", ""))).ToString("#,##0;(#,##0)");
                ((Label)e.Item.FindControl("lbltotallate")).Text = Convert.ToDouble((Convert.IsDBNull(dt3.Compute("Sum(aclate)", "")) ? 0.00 : dt3.Compute("Sum(aclate)", ""))).ToString("#,##0;(#,##0)");
                ((Label)e.Item.FindControl("lbltotalleave")).Text = Convert.ToDouble((Convert.IsDBNull(dt3.Compute("Sum(leave)", "")) ? 0.00 : dt3.Compute("Sum(leave)", ""))).ToString("#,##0;(#,##0)");


            }

        }






        protected void ibtnEmpListAllinfo_Click(object sender, EventArgs e)
        {
            this.GetEmpName();
        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string comname = dt1.Rows[0]["comname"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["comname"].ToString() == comname)
                {
                    comname = dt1.Rows[j]["comname"].ToString();
                    dt1.Rows[j]["comdesc"] = "";
                }

                else
                    comname = dt1.Rows[j]["comname"].ToString();
            }

            return dt1;

        }




        private void ShowAttHistoryGraph()
        {
            DataTable dt4 = (DataTable)ViewState["tblgrph"]; ;

            AttHistoryGraph.Series["Actual in Time"].XValueMember = "yearmon";
            AttHistoryGraph.Series["Actual in Time"].YValueMembers = "acintime";

            AttHistoryGraph.Series["Late"].XValueMember = "yearmon";
            AttHistoryGraph.Series["Late"].YValueMembers = "aclate";

            AttHistoryGraph.Series["Absent"].XValueMember = "yearmon";
            AttHistoryGraph.Series["Absent"].YValueMembers = "absnt";

            AttHistoryGraph.Series["Leave"].XValueMember = "yearmon";
            AttHistoryGraph.Series["Leave"].YValueMembers = "leave";



            //AttHistoryGraph.Series["Series1"].LegendText = "Month";
            //AttHistoryGraph.Series["Series2"].LegendText = "Status";

            AttHistoryGraph.DataSource = dt4;
            AttHistoryGraph.DataBind();


        }


        protected void ddlEmpName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DataTable dt= ViewState["tblemp"] 
            string empid = this.ddlEmpName.SelectedValue.ToString();
            //this.lbldesg.Text = "Designation: " + (((DataTable)ViewState["tblemp"]).Select("empid='" + empid + "'"))[0]["desg"].ToString();
            //this.lbtnOk_Click(null, null);
        }

        protected void hyplPreviewCv_Click1(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            //this.hyplPreviewCv.Text = "";
            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString().Trim();
            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_REPORT_EMP_DASEBOARD", "RPTEMPINFORMATION", empid, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("MMMM dd, yyyy");

            ReportDocument rptempservices = new RealERPRPT.R_62_Mis.RptMyAllInformation();

            TextObject txtempname = rptempservices.ReportDefinition.ReportObjects["txtempname"] as TextObject;
            txtempname.Text = (ds1.Tables[2].Rows.Count == 0) ? "Employee Name" : ds1.Tables[2].Rows[0]["empname"].ToString();

            TextObject Empsigna = rptempservices.ReportDefinition.ReportObjects["Empsigna"] as TextObject;
            Empsigna.Text = (ds1.Tables[2].Rows.Count == 0) ? "Employee Name" : ds1.Tables[2].Rows[0]["empname"].ToString();



            TextObject txtCompName = rptempservices.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            txtCompName.Text = (ds1.Tables[2].Rows.Count == 0) ? comnam : ds1.Tables[2].Rows[0]["empcomdesc"].ToString();

            TextObject rpttxtempdept = rptempservices.ReportDefinition.ReportObjects["txtempdept"] as TextObject;
            rpttxtempdept.Text = (ds1.Tables[2].Rows.Count == 0) ? "Department Name: " : "DEPARTMENT NAME: " + ds1.Tables[2].Rows[0]["empdeptdesc"].ToString();
            TextObject txtcomaddress = rptempservices.ReportDefinition.ReportObjects["txtcomaddress"] as TextObject;
            txtcomaddress.Text = comadd;
            TextObject txtnetsalary = rptempservices.ReportDefinition.ReportObjects["txtnetsalary"] as TextObject;
            txtnetsalary.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["netsal"]).ToString("#,##0; (#,##0); ");
            TextObject txtNetpayable = rptempservices.ReportDefinition.ReportObjects["txtNetpayable"] as TextObject;
            txtNetpayable.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["netpay"]).ToString("#,##0; (#,##0); ");
            rptempservices.SetDataSource(ds1.Tables[0]);
            //string comcod = hst["comcod"].ToString();
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptempservices.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptempservices;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                         ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            this.ShowAttHistoryGraph();
            //this.lbtnOk_Click(null, null);

        }


        private void ShowJobRespon()
        {
            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            DataTable dt = (DataTable)ViewState["tblJobRespon"]; ;
            if (dt == null)
            {

                this.lblnotfound.Visible = true;
                this.grvJobRespo.DataSource = null;
                this.lblnotfound.Text = "No Result Found";
                this.grvJobRespo.DataBind();

                return;

            }
            this.grvJobRespo.DataSource = dt;
            this.grvJobRespo.DataBind();

        }
        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetEmpName();
        }
        protected void imgbtnCompany_Click(object sender, EventArgs e)
        {
            this.GetCompany();
            this.GetEmpName();

        }

    }
}