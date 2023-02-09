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

namespace RealERPWEB.F_81_Hrm.F_82_App
{
    public partial class RptMyInterface : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        Common compUtility = new Common();

        public static string empid = "";
        public static string frmdate = "";
        public static string todate = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                //((Label)this.Master.FindControl("lblTitle")).Text = "MY SERVICES INFORMATION";
                // this.SelectView();
                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                string empid = this.Request.QueryString["empid"] ?? ""; ;
                //string empid = this.ddlEmpName.SelectedValue.ToString();
                if (empid.Length > 0)
                {
                    this.lbtnOk.Visible = false;
                    this.Label17.Visible = false;
                    this.txtDate.Visible = false;
                    this.ddlEmpName.Enabled = false;
                }
                this.GetCompany();
                this.MultiView1.ActiveViewIndex = 0;
                this.lbtnOk_Click(null, null);

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
            return (hst["comcod"].ToString());

        }

        private void GetCompany()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();
            string txtCompany = "%%";
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "GETCOMPANYNAMEINTER", txtCompany, userid, "", "", "", "", "", "", "");
            this.ddlCompany.DataTextField = "actdesc";
            this.ddlCompany.DataValueField = "actcode";
            this.ddlCompany.DataSource = ds5.Tables[0];
            this.ddlCompany.DataBind();
            this.GetEmpName();
            //ds1.Dispose();

        }
        private void GetEmpName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string txtSProject = "%%";
            string companyName = this.ddlCompany.SelectedValue.ToString();
            string empid = this.Request.QueryString["empid"] ?? "";
            string usrid = hst["usrid"].ToString();

            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "GETMYEMPNAME", txtSProject, companyName, usrid, empid, "", "", "", "", "");
            this.ddlEmpName.DataTextField = "empname1";
            //this.ddlEmpName.SelectedValue = "empname";
            this.ddlEmpName.DataValueField = "empid";



            this.ddlEmpName.DataSource = ds3.Tables[0];
            this.ddlEmpName.DataBind();
            this.ddlEmpName.SelectedValue = (empid.Length > 0) ? this.ddlEmpName.Items[0].Value : empid;

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
            ReportDocument rptempservices = new RealERPRPT.R_81_Hrm.R_82_App.RptEmpServices();
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
            this.lblPaySlip.Visible = true;


            this.EmpUserImg.ImageUrl = "~/GetImage.aspx?ImgID=ImgUser";
            //ViewState.Remove("tblservices");
            string comcod = this.GetComeCode();

            string qempid = this.Request.QueryString["empid"] ?? "";
            string empid = qempid.Length > 0 ? qempid : this.ddlEmpName.SelectedValue.ToString(); //this.ddlEmpName.SelectedValue.ToString();
            string Date = this.txtDate.Text.Trim();
            string calltype = "";

            switch (comcod)
            {
                case "3101":  // For BTI as Per Instructiion Emdad Vai and Uzzal Vai  create by Md Ibrahim Khalil
                case "3365":
                    calltype = "RPTMYSERVICESBTI";
                    break;

                default:
                    calltype = "RPTMYSERVICES";
                    break;
            }

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", calltype, empid, Date, "", "", "", "", "", "", "");
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
            ViewState["tblAttHistGraph"] = ds1.Tables[5];
            ViewState["tblPaySlip"] = ds1.Tables[6];



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
            dr1["leave"] = Convert.ToDouble((Convert.IsDBNull(dt3.Compute("Sum(leave)", "")) ? 0.00 : dt3.Compute("Sum(leave)", ""))).ToString("#,##0.00;(#,##0.00)"); ; ;
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


                DataTable dt6 = (DataTable)ViewState["tblPaySlip"];
                this.gvPaySlip.DataSource = dt6;
                this.gvPaySlip.DataBind();


                //HyperLink lnknextbtn = (HyperLink)this.gvLeaveStatus.HeaderRow.FindControl("hlnkbtnNext");
                string comcod = this.GetComeCode();
                //string ymonid = this.txtDate.Text.ToString();
                string frmdate = Convert.ToDateTime("01-Jan-" + this.txtDate.Text.Substring(7)).ToString("dd-MMM-yyyy");
                string todate = Convert.ToDateTime(frmdate).AddYears(1).AddDays(-1).ToString("dd-MMM-yyyy");
                string empid = this.ddlEmpName.SelectedValue.ToString().Trim();
                ////lnknextbtn.NavigateUrl = "~/F_82_App/LinkMyHRLeave.aspx?Type=EmpLeaveSt&empid=" + empid + "&frmdate=" + frmdate + "&todate=" + todate;

                ((HyperLink)this.gvLeaveStatus.HeaderRow.FindControl("hlnkbtnNext")).NavigateUrl = "../F_82_App/LinkMyHRLeave.aspx?Type=EmpLeaveSt&empid=" + empid + "&frmdate=" + frmdate + "&todate=" + todate;

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
                string frmdate = "";
                string date = "";
                switch (comcod)
                {
                    case "3365":
                    case "3101":
                        date = "26-" + ASTUtility.Month3digit(Convert.ToInt32(ymonid.Substring(4, 2))) + "-" + ymonid.Substring(0, 4);
                        frmdate = Convert.ToDateTime(date).AddMonths(-1).ToString("dd-MMM-yyyy");
                        //cudate = date1.AddMonths(-1).ToString("dd-MMM-yyyy");
                        break;

                    default:
                        date = "01-" + ASTUtility.Month3digit(Convert.ToInt32(ymonid.Substring(4, 2))) + "-" + ymonid.Substring(0, 4);
                        frmdate = Convert.ToDateTime(date).ToString("dd-MMM-yyyy");
                        break;
                }

                //string frmdate = Convert.ToDateTime(ymonid.Substring(4, 2) + "/"+ Convert.ToDateTime(ymonid.Substring(4, 2) + "/" + ymonid.Substring(0, 4)).ToString("dd-MMM-yyyy");
                string todate = Convert.ToDateTime(frmdate).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                string empid = this.ddlEmpName.SelectedValue.ToString().Trim();
                lnkyearmon.NavigateUrl = "~/F_81_Hrm/F_82_App/RptMyAttendenceSheet.aspx?Type=&empid=" + empid + "&frmdate=" + frmdate + "&todate=" + todate;
            }



            if (e.Item.ItemType == ListItemType.Footer)
            {

                DataTable dt3 = (DataTable)ViewState["tblAttHist"];
                if (dt3.Rows.Count == 0)
                {

                    ((Label)e.Item.FindControl("lblacintime")).Text = "";
                    ((Label)e.Item.FindControl("lbltotalabs")).Text = "";
                    ((Label)e.Item.FindControl("lbltotallate")).Text = "";
                    ((Label)e.Item.FindControl("lbltotalleave")).Text = "";
                    ((Label)e.Item.FindControl("lbltolvadj")).Text = "";
                    ((Label)e.Item.FindControl("lblfrtolateapp")).Text = "";


                    return;
                }

                DataTable dt5 = (DataTable)ViewState["tblAttHistGraph"];

                ((Label)e.Item.FindControl("lblperIntime")).Text = Convert.ToDouble(dt5.Rows[0]["perpontow"]).ToString("#,##0.00;(#,##0.00)");
                ((Label)e.Item.FindControl("lblPerabs")).Text = Convert.ToDouble(dt5.Rows[0]["perab"]).ToString("#,##0.00;(#,##0.00)");
                ((Label)e.Item.FindControl("lblperLate")).Text = Convert.ToDouble(dt5.Rows[0]["perlate"]).ToString("#,##0.00;(#,##0.00)");
                ((Label)e.Item.FindControl("lblperleave")).Text = Convert.ToDouble(dt5.Rows[0]["perleave"]).ToString("#,##0.00;(#,##0.00)");



                ((Label)e.Item.FindControl("lblacintime")).Text = Convert.ToDouble((Convert.IsDBNull(dt3.Compute("Sum(acintime)", "")) ? 0.00 : dt3.Compute("Sum(acintime)", ""))).ToString("#,##0;(#,##0)");
                ((Label)e.Item.FindControl("lbltotalabs")).Text = Convert.ToDouble((Convert.IsDBNull(dt3.Compute("Sum(absnt)", "")) ? 0.00 : dt3.Compute("Sum(absnt)", ""))).ToString("#,##0;(#,##0)");
                ((Label)e.Item.FindControl("lbltotallate")).Text = Convert.ToDouble((Convert.IsDBNull(dt3.Compute("Sum(aclate)", "")) ? 0.00 : dt3.Compute("Sum(aclate)", ""))).ToString("#,##0;(#,##0)");
                ((Label)e.Item.FindControl("lbltotalleave")).Text = Convert.ToDouble((Convert.IsDBNull(dt3.Compute("Sum(leave)", "")) ? 0.00 : dt3.Compute("Sum(leave)", ""))).ToString("#,##0;(#,##0)");

                ((Label)e.Item.FindControl("lbltolvadj")).Text = Convert.ToDouble((Convert.IsDBNull(dt3.Compute("Sum(lvadj)", "")) ? 0.00 : dt3.Compute("Sum(lvadj)", ""))).ToString("#,##0;(#,##0)");



                ((Label)e.Item.FindControl("lblfrtolateapp")).Text = Convert.ToDouble((Convert.IsDBNull(dt3.Compute("Sum(lateapp)", "")) ? 0.00 : dt3.Compute("Sum(lateapp)", ""))).ToString("#,##0;(#,##0)");



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
            DataTable dt4 = (DataTable)ViewState["tblAttHistGraph"];

            // AttHistoryGraph.Series["Actual in Time"].XValueMember = "workday";
            AttHistoryGraph.Series["Actual in Time"].YValueMembers = "perpontow";

            //AttHistoryGraph.Series["Late"].XValueMember = "workday";
            AttHistoryGraph.Series["Late"].YValueMembers = "perlate";

            // AttHistoryGraph.Series["Absent"].XValueMember = "workday";
            AttHistoryGraph.Series["Absent"].YValueMembers = "perab";

            // AttHistoryGraph.Series["Leave"].XValueMember = "workday";
            AttHistoryGraph.Series["Leave"].YValueMembers = "perleave";



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
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "RPTEMPINFORMATION", empid, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("MMMM dd, yyyy");
            ReportDocument rptempservices = new ReportDocument();

            if (comcod == "3340" || comcod == "3101")
            {
                rptempservices = new RealERPRPT.R_81_Hrm.R_82_App.RptEmpAllInformationUrban();



            }

            else
            {
                rptempservices = new RealERPRPT.R_81_Hrm.R_82_App.RptMyAllInformation();
                TextObject Empsigna = rptempservices.ReportDefinition.ReportObjects["Empsigna"] as TextObject;
                Empsigna.Text = (ds1.Tables[2].Rows.Count == 0) ? "Employee Name" : ds1.Tables[2].Rows[0]["empname"].ToString();
                TextObject txtnetsalary = rptempservices.ReportDefinition.ReportObjects["txtnetsalary"] as TextObject;
                txtnetsalary.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["netsal"]).ToString("#,##0; (#,##0); ");
                TextObject txtNetpayable = rptempservices.ReportDefinition.ReportObjects["txtNetpayable"] as TextObject;
                txtNetpayable.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["netpay"]).ToString("#,##0; (#,##0); ");

            }



            TextObject txtempname = rptempservices.ReportDefinition.ReportObjects["txtempname"] as TextObject;
            txtempname.Text = (ds1.Tables[2].Rows.Count == 0) ? "Employee Name" : ds1.Tables[2].Rows[0]["empname"].ToString();



            TextObject txtCompName = rptempservices.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            txtCompName.Text = (ds1.Tables[2].Rows.Count == 0) ? comnam : ds1.Tables[2].Rows[0]["empcomdesc"].ToString();

            TextObject rpttxtempdept = rptempservices.ReportDefinition.ReportObjects["txtempdept"] as TextObject;
            rpttxtempdept.Text = (ds1.Tables[2].Rows.Count == 0) ? "Department Name: " : "DEPARTMENT NAME: " + ds1.Tables[2].Rows[0]["empdeptdesc"].ToString();
            TextObject txtcomaddress = rptempservices.ReportDefinition.ReportObjects["txtcomaddress"] as TextObject;
            txtcomaddress.Text = comadd;

            rptempservices.SetDataSource(ds1.Tables[0]);
            //string comcod = hst["comcod"].ToString();
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptempservices.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptempservices;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
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

        protected void gvPaySlip_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlnkPrintPaySlip = (HyperLink)e.Row.FindControl("hlnkPrintPaySlip");
                string monthid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "monthid")).ToString();
                string empid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "empid")).ToString();

                hlnkPrintPaySlip.NavigateUrl = "~/F_81_Hrm/F_89_Pay/PrintPaySlip.aspx?Type=paySlip&monthid=" + monthid + "&empid=" +empid ;

            }
        }
    }
}