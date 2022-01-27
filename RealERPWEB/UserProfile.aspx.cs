
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using RealERPLIB;
using CrystalDecisions.CrystalReports.Engine;

namespace RealERPWEB
{
    public partial class UserProfile : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();

        string Upload = "";
        int size = 0;
        System.IO.Stream image_file = null;
        ProcessAccess UserData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            this.GetProfile();

            if (fileuploaddropzone.HasFile)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string UserId = hst["usrid"].ToString();
                string comcod = this.GetCompCode();
                bool updatPhoto;
                string uploadFolder = Request.PhysicalApplicationPath + "Upload\\UserImages\\";
                string extension = Path.GetExtension(fileuploaddropzone.PostedFile.FileName);
                string savelocation = uploadFolder + UserId + extension;
                ((Panel)this.Master.FindControl("AlertArea")).Visible = false;

                image_file = fileuploaddropzone.PostedFile.InputStream;
                size = fileuploaddropzone.PostedFile.ContentLength;
                Session["i"] = image_file;
                Session["s"] = size;

                if (size < 125000)
                {
                    string dburl = "~/Upload/UserImages/" + UserId + extension;
                    var filePath = Server.MapPath("~/Upload/UserImages/" + UserId + extension);
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }


                    fileuploaddropzone.SaveAs(savelocation);
                    
                    updatPhoto = UserData.UpdateTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "INSERTUSERIMAGES", UserId, dburl, "", "", "", "", "", "", "", "");
                    if (updatPhoto)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Your Porofile Picture Updated Successfully');", true);

                    }

                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Your Porofile Picture Updated failed');", true);

                    }


                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Profile Picture Size Large');", true);

                  
                }


            }
        }

        public void GetProfile()
        {
            this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

            Hashtable hst = (Hashtable)Session["tblLogin"];
            if (hst == null)
            {
                Response.Redirect("~/Error404.aspx");
            }
            this.UDesignation.InnerHtml = hst["usrdesig"].ToString();
            UserName.InnerHtml = "Hi, " + hst["username"].ToString();
            UserName1.InnerHtml = "Hey <b>" + hst["username"].ToString() + "!!</b>  do you want to enable Notifications Panel in your Main Dashboard? (Note: ON for Enable and OFF for Disable)";
            userimg.ImageUrl = hst["userimg"].ToString();
            if (hst["events"].ToString() == "True")
            {
                EventSTatus.InnerHtml = "<input type='checkbox'  class='switcher-input'> " +
                                "<span class='switcher-indicator'></span> <span class='switcher-label-on'>ON</span> <span class='switcher-label-off'>OFF</span>";

            }
            else
            {
                EventSTatus.InnerHtml = "<input type='checkbox' class='switcher-input' checked='checked'> " +
                                         "<span class='switcher-indicator'></span> <span class='switcher-label-on'>ON</span> <span class='switcher-label-off'>OFF</span>";

            }
            this.getData();

        }

        [WebMethod]
        public static void ChangeEventsStatus()
        {
            Common comn = new Common();
            string usrid = comn.GetUserCode();
            string comcod = comn.GetCompCode();
            ProcessAccess accData = new ProcessAccess();
            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_LOGSTAUTS", "EVENTSTATUS_UPDATE", usrid, "", "", "", "", "", "");
            if (result == true)
            {

            }
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void getData()
        {

            ViewState.Remove("tblgrph");
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string empid = hst["empid"].ToString();

            //this.lblServHead.Visible = true;
            //this.lbAttHead.Visible = true;
            //this.lblgraph.Visible = true;
            //this.lblleaveHis.Visible = true;
            //this.EmpUserImg.Visible = true;
            //   this.AttHistoryGraph.Visible = true;
            //this.hyplPreviewCv.Visible = true;
            //this.Lbljobres.Visible = true;


            // this.EmpUserImg.ImageUrl = "~/GetImage.aspx?ImgID=ImgUser";
            //ViewState.Remove("tblservices");
            string comcod = this.GetCompCode();

            string qempid = this.Request.QueryString["empid"] ?? "";

            string Date = System.DateTime.Today.ToString("dd-MMM-yyyy");
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "RPTMYSERVICES", empid, Date, "", "", "", "", "", "", "");
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
            this.Data_Bind();


        }
        protected void RptAttHistroy_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string empid = hst["empid"].ToString();

            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                HyperLink lnkyearmon = (HyperLink)e.Item.FindControl("hlnkbtnadd");
                string comcod = this.GetCompCode();
                string ymonid = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "ymonid")).ToString();
                string frmdate = Convert.ToDateTime(ymonid.Substring(4, 2) + "/01/" + ymonid.Substring(0, 4)).ToString("dd-MMM-yyyy");
                string todate = Convert.ToDateTime(frmdate).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");

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

        private void ShowJobRespon()
        {
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string empid = hst["empid"].ToString();
            DataTable dt = (DataTable)ViewState["tblJobRespon"]; ;
            if (dt == null)
            {

                //this.lblnotfound.Visible = true;
                this.grvJobRespo.DataSource = null;
                this.grvJobRespo.DataBind();

                return;

            }
            this.grvJobRespo.DataSource = dt;
            this.grvJobRespo.DataBind();

        }
        private void Data_Bind()
        {
            try
            {
                DataTable dt = (DataTable)ViewState["tblservices"];
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string empid = hst["empid"].ToString();
                this.gvempservices.DataSource = dt;
                this.gvempservices.DataBind();




                DataTable dt2 = (DataTable)ViewState["tblAttHist"];
                this.ShowAttHistoryGraph();
                this.RptAttHistroy.DataSource = dt2;
                this.RptAttHistroy.DataBind();


                DataTable dt3 = (DataTable)ViewState["tblLeaveStatus"];
                this.gvLeaveStatus.DataSource = dt3;
                this.gvLeaveStatus.DataBind();





                string comcod = this.GetCompCode();

                string frmdate = Convert.ToDateTime("01-Jan-" + this.txtDate.Text.Substring(7)).ToString("dd-MMM-yyyy");
                string todate = Convert.ToDateTime(frmdate).AddYears(1).AddDays(-1).ToString("dd-MMM-yyyy");



                this.hlnkbtnNext.NavigateUrl = "../F_81_Hrm/F_82_App/LinkMyHRLeave?Type=EmpLeaveSt&empid=" + empid + "&frmdate=" + frmdate + "&todate=" + todate;

                DataTable dt4 = (DataTable)ViewState["tblEmpimg"];
                DataTable dt5 = (DataTable)ViewState["tblJobRespon"];



            }

            catch (Exception ex)
            { }

        }

        private void ShowAttHistoryGraph()
        {
            DataTable dt4 = (DataTable)ViewState["tblAttHistGraph"];


            double present = Convert.ToDouble(dt4.Rows[0]["perpontow"].ToString());
            double late = Convert.ToDouble(dt4.Rows[0]["perlate"].ToString());
            //  double eleave = Convert.ToDouble(dt4.Rows[0]["earlyLev"].ToString());
            double onlaeve = Convert.ToDouble(dt4.Rows[0]["perleave"].ToString());
            double absent = Convert.ToDouble(dt4.Rows[0]["perab"].ToString());

            this.lblpresent.Text = present.ToString("#,##0.00;(#,##0.00);");
            this.lbllate.Text = late.ToString("#,##0.00;(#,##0.00);");
            //  this.lbleleave.Text = eleave.ToString("#,##0.00;(#,##0.00);");
            this.lblonleave.Text = onlaeve.ToString("#,##0.00;(#,##0.00);");
            this.lblabs.Text = absent.ToString("#,##0.00;(#,##0.00);");




        }
        protected void hyplPreviewCv_Click1(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            //this.hyplPreviewCv.Text = "";
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string empid = hst["empid"].ToString();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "RPTEMPINFORMATION", empid, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;


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
    }
}