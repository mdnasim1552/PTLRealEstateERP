using Microsoft.Reporting.WinForms;
using RealERPLIB;
using RealERPRDLC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_81_Hrm.F_92_Mgt
{
    public partial class EmpSettlement : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "EMPLOYEE FINAL SETTLEMENT";
                this.txtCurDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                CommonButton();
                this.GetEmployeeName();
                if (this.Request.QueryString["actcode"].ToString().Length != 0)
                {
                    this.lbtnOk_Click(null, null);
                }
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkbtnUpdate_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnApprove")).Click += new EventHandler(lnkbtnApprove_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnApprove")).OnClientClick = "return confirm('Do You want to Approve?')";
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.UrlReferrer.ToString());
        }

        private void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {
            this.Save_Value();
            this.Data_Bind();
        }

        private void CommonButton()
        {
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Text = "Save";
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnApprove")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnApprove")).Text = "Approve";
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;
        }
        private void GetEmployeeName()
        {
            string comcod = this.GetComeCode();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACR_EMPLOYEE", "GET_SEPERATED_EMP", "", "", "", "", "", "", "", "");
            if (ds3.Tables[0].Rows.Count == 0)
                return;
            this.ddlEmpName.DataTextField = "empname1";
            this.ddlEmpName.DataValueField = "empid";
            this.ddlEmpName.DataSource = ds3.Tables[0];
            this.ddlEmpName.DataBind();
            if (this.Request.QueryString["actcode"].ToString().Length != 0)
            {
                this.ddlEmpName.SelectedValue = this.Request.QueryString["actcode"].ToString();
            }

            ViewState["empdata"] = ds3.Tables[0].DataTableToList<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSepEmployee>();
            ds3.Dispose();

        }


        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        protected void GetPerNumber()
        {

        }

        protected void ibtnEmpList_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
                this.GetEmployeeName();
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.ddlEmpName.Enabled = false;
                this.ddlEmpName.Enabled = false;
                this.ViewDataPanel.Visible = true;
                this.ShowPerformance();
                return;
            }
            this.lbtnOk.Text = "Ok";
            this.ViewDataPanel.Visible = false;
            this.ddlEmpName.Enabled = true;
            this.txtCurDate.Enabled = true;
        }

        private void ShowPerformance()

        {
            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            string rpttype = this.rbtnstatement.SelectedIndex.ToString();
            var emplist = (List<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSepEmployee>)ViewState["empdata"];
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACR_EMPLOYEE", "GET_EMP_SETTLEMENT_INFO", empid, rpttype, "", "", "", "", "", "");
            if (ds3 == null)
                return;
            ViewState["tblsttlmnt"] = ds3.Tables[0].DataTableToList<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSttlemntInfo>();

            ViewState["tblsttlmnt1"] = ds3.Tables[0].DataTableToList<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSttlemntInfo>().FindAll(p => p.hrgcod.Substring(0, 3) == "351").OrderBy(x => x.seq).ToList();
            ViewState["tblsttlmnt2"] = ds3.Tables[0].DataTableToList<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSttlemntInfo>().FindAll(p => p.hrgcod.Substring(0, 3) == "352").OrderBy(x => x.seq).ToList();

            var shorempdata = emplist.FindAll(d => d.empid == empid);
            if (rpttype == "0")
            {
                this.engst.Visible = true;
                this.lblname.Text = shorempdata[0].empname.ToString();
                this.lbldesig.Text = shorempdata[0].designation.ToString();
                this.lblidcard.Text = shorempdata[0].idno.ToString();
                this.lblsection.Text = shorempdata[0].deptname.ToString();
                this.lblseptype.Text = shorempdata[0].septypedesc.ToString();
                this.lbljoin.Text = shorempdata[0].joindat.ToString("dd-MMM-yyyy");
                this.lblsep.Text = shorempdata[0].retdat.ToString("dd-MMM-yyyy");
                this.lblservlen.Text = shorempdata[0].servleng.ToString();

            }
            else
            {

            }

            this.Data_Bind();
            if (ds3.Tables[1].Rows.Count > 0)
            {
                this.txtrefno.Text = ds3.Tables[1].Rows[0]["refno"].ToString();
                this.txtCurDate.Text = Convert.ToDateTime(ds3.Tables[1].Rows[0]["billdate"]).ToString("dd-MMM-yyyy");
            }

        }

        private void Data_Bind()
        {
            var sttlmntinfo1 = ((List<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSttlemntInfo>)ViewState["tblsttlmnt1"]).OrderBy(x => x.seq).ToList();
            var sttlmntinfo2 = ((List<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSttlemntInfo>)ViewState["tblsttlmnt2"]).OrderBy(x => x.seq).ToList();
            this.gvsettlemntcredit.DataSource = sttlmntinfo1;
            this.gvsettlemntcredit.DataBind();
            this.gvsttlededuct.DataSource = sttlmntinfo2;
            this.gvsttlededuct.DataBind();
            this.FooterCalculation();


            var paycount = sttlmntinfo1;

            var deductioncount = sttlmntinfo2;


            int i = 0;
            foreach (var item in paycount)
            {
                string gcod = item.hrgcod;
                switch (gcod)
                {

                    //salary
                    case "35101":
                        ((TextBox)this.gvsettlemntcredit.Rows[i].FindControl("txtfrmdat")).Visible = true;
                        ((TextBox)this.gvsettlemntcredit.Rows[i].FindControl("txttodat")).Visible = true;

                        ((Label)this.gvsettlemntcredit.Rows[i].FindControl("lblfrmdat")).Visible = false;
                        ((Label)this.gvsettlemntcredit.Rows[i].FindControl("lbltodat")).Visible = false;
                        break;
                    case "35108":
                        ((TextBox)this.gvsettlemntcredit.Rows[i].FindControl("txtfrmdat")).Visible = true;
                        ((TextBox)this.gvsettlemntcredit.Rows[i].FindControl("txttodat")).Visible = true;
                        ((Label)this.gvsettlemntcredit.Rows[i].FindControl("lblfrmdat")).Visible = false;
                        ((Label)this.gvsettlemntcredit.Rows[i].FindControl("lbltodat")).Visible = false;
                        break;
                    case "35110":
                        ((TextBox)this.gvsettlemntcredit.Rows[i].FindControl("txtfrmdat")).Visible = true;
                        ((TextBox)this.gvsettlemntcredit.Rows[i].FindControl("txttodat")).Visible = true;
                        ((Label)this.gvsettlemntcredit.Rows[i].FindControl("lblfrmdat")).Visible = false;
                        ((Label)this.gvsettlemntcredit.Rows[i].FindControl("lbltodat")).Visible = false;
                        break;
                    default:
                        ((TextBox)this.gvsettlemntcredit.Rows[i].FindControl("txtfrmdat")).Visible = false;
                        ((TextBox)this.gvsettlemntcredit.Rows[i].FindControl("txttodat")).Visible = false;
                        ((Label)this.gvsettlemntcredit.Rows[i].FindControl("lblfrmdat")).Visible = true;
                        ((Label)this.gvsettlemntcredit.Rows[i].FindControl("lbltodat")).Visible = true;
                        break;

                }
                i++;
            }


        }

        private void Save_Value()
        {
            var sttlmntinfo1 = ((List<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSttlemntInfo>)ViewState["tblsttlmnt1"]).OrderBy(x => x.seq).ToList();
            var sttlmntinfo2 = ((List<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSttlemntInfo>)ViewState["tblsttlmnt2"]).OrderBy(x => x.seq).ToList();
            //var sttlmntinfo1 = (List<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSttlemntInfo>)ViewState["tblsttlmnt1"];

            //var sttlmntinfo2 = (List<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSttlemntInfo>)ViewState["tblsttlmnt2"];
            var sttlmntinfo = (List<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSttlemntInfo>)ViewState["tblsttlmnt"];
            for (int i = 0; i < this.gvsettlemntcredit.Rows.Count; i++)
            {
                string hrgcod = ((Label)gvsettlemntcredit.Rows[i].FindControl("lblhrgcod")).Text.ToString();
                double gross = Convert.ToDouble("0" + ((TextBox)gvsettlemntcredit.Rows[i].FindControl("txtgross")).Text.Trim());
                double ttlamt = Convert.ToDouble("0" + ((TextBox)gvsettlemntcredit.Rows[i].FindControl("TtlAmout")).Text.Trim());
                double numofday =Convert.ToDouble("0"+ ((TextBox)gvsettlemntcredit.Rows[i].FindControl("lblnmday")).Text.Trim());
                //var index = sttlmntinfo1.FindIndex(p => p.hrgcod == hrgcod);

                sttlmntinfo1[i].amount = gross;
                sttlmntinfo1[i].numofday = numofday;
                sttlmntinfo1[i].ttlamt = ttlamt;

                switch (hrgcod)
                {
                    case "35101"://salary 36,513/30*23	
                        sttlmntinfo1[i].ttlamt = (gross / 30) * numofday;
          
                        break;

                    case "35108"://earn leave 36,513 * 12 / 365 * 0
                        sttlmntinfo1[i].ttlamt = gross * 12 / 365 * numofday;
                 
                        break;
  

                }



            }

            for (int i = 0; i < this.gvsttlededuct.Rows.Count; i++)
            {
                string hrgcod = ((Label)gvsttlededuct.Rows[i].FindControl("lblhrgcod")).Text.ToString();
                double gross = Convert.ToDouble("0" + ((TextBox)gvsttlededuct.Rows[i].FindControl("txtgross")).Text.Trim());
                double ttlamt = Convert.ToDouble("0" + ((TextBox)gvsttlededuct.Rows[i].FindControl("TtlAmout")).Text.Trim());
                double numofday = Convert.ToDouble("0" + ((TextBox)gvsttlededuct.Rows[i].FindControl("lblnmday")).Text.Trim());
                //var index2 = sttlmntinfo2.FindIndex(p => p.hrgcod == hrgcod);
                sttlmntinfo2[i].amount = gross;
                sttlmntinfo2[i].numofday = numofday;
                sttlmntinfo2[i].ttlamt = ttlamt;


                switch (hrgcod)
                {
                    case "35206"://absent
                        sttlmntinfo2[i].ttlamt = (gross / 30) * numofday;
      
                        break;

                    case "35224"://cl 	
                        sttlmntinfo2[i].ttlamt = gross*12/365* numofday;
                        break;
                    case "35226"://sl leave 
                        sttlmntinfo2[i].ttlamt = gross * 12 / 365 * numofday;


                        break;
                    case "35228"://el leave
                        sttlmntinfo2[i].ttlamt = gross * 12 / 365 * numofday;
                        break;

                }
            }
            ViewState["tblsttlmnt1"] = sttlmntinfo1;
            ViewState["tblsttlmnt2"] = sttlmntinfo2;
            ViewState.Remove("tblsttlmnt");
            ViewState["tblsttlmnt"] = sttlmntinfo1.Concat(sttlmntinfo2).ToList();

        }

       

        private void printRptEnglish()
        {
            string comcod = this.GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");



            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            var emplist = (List<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSepEmployee>)ViewState["empdata"];
            var sttlmntinfo = (List<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSttlemntInfo>)ViewState["tblsttlmnt"];




            var list1 = sttlmntinfo.FindAll(p => p.hrgcod.Substring(0, 3) == "351");
            var list2 = sttlmntinfo.FindAll(p => p.hrgcod.Substring(0, 3) == "352");


            string billDate = emplist[0].billdate.ToString("dd-MMM-yyyy");
            string name = emplist[0].empname.ToString();
            string Desgin = emplist[0].designation.ToString();
            string Id = emplist[0].idno.ToString();
            string Section = emplist[0].deptname.ToString();
            string jobseperation = emplist[0].septypedesc.ToString();
            string joining = emplist[0].joindat.ToString("dd-MMM-yyyy");
            string sepdate = emplist[0].retdat.ToString("dd-MMM-yyyy");
            var netamount = (sttlmntinfo.FindAll(s => s.hrgcod.Substring(0, 3) == "351").Sum(p => p.ttlamt) - sttlmntinfo.FindAll(s => s.hrgcod.Substring(0, 3) == "352").Sum(p => p.ttlamt)).ToString("#,##0.00;(#,##0.00); ");
            var grossslary = sttlmntinfo[0].ttlamt.ToString();
            string servicelength = emplist[0].servleng.ToString();

            double netpay = Convert.ToDouble(netamount);



            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_92_Mgt.RptEmpSattelment", list1, list2, null);
            rpt1.EnableExternalImages = true;

            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("rpttitle", "Employee Final Sattelment"));
            rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            rpt1.SetParameters(new ReportParameter("netamount", netamount));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));

            // for Show EmplInfo
            rpt1.SetParameters(new ReportParameter("billDate", billDate));
            rpt1.SetParameters(new ReportParameter("name", name));
            rpt1.SetParameters(new ReportParameter("Desgin", Desgin));
            rpt1.SetParameters(new ReportParameter("Id", Id));
            rpt1.SetParameters(new ReportParameter("Section", Section));
            rpt1.SetParameters(new ReportParameter("jobseperation", jobseperation));
            rpt1.SetParameters(new ReportParameter("grossslary", grossslary));
            rpt1.SetParameters(new ReportParameter("joining", joining));
            rpt1.SetParameters(new ReportParameter("sepdate", sepdate));
            rpt1.SetParameters(new ReportParameter("servicelength", servicelength));
            rpt1.SetParameters(new ReportParameter("inwords", ASTUtility.Trans(netpay, 2)));


            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" + ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void printRptBangla()
        {
            string comcod = this.GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");



            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            var emplist = (List<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSepEmployee>)ViewState["empdata"];
            var sttlmntinfo = (List<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSttlemntInfo>)ViewState["tblsttlmnt"];




            var list1 = sttlmntinfo.FindAll(p => p.hrgcod.Substring(0, 3) == "351");
            var list2 = sttlmntinfo.FindAll(p => p.hrgcod.Substring(0, 3) == "352");


            string billDate = emplist[0].billdate.ToString("dd-MMM-yyyy");
            string name = emplist[0].empname.ToString();
            string Desgin = emplist[0].designation.ToString();
            string Id = emplist[0].idno.ToString();
            string Section = emplist[0].deptname.ToString();
            string jobseperation = emplist[0].septypedesc.ToString();
            string joining = emplist[0].joindat.ToString("dd-MMM-yyyy");
            string sepdate = emplist[0].retdat.ToString("dd-MMM-yyyy");
            var netamount = (sttlmntinfo.FindAll(s => s.hrgcod.Substring(0, 3) == "351").Sum(p => p.ttlamt) - sttlmntinfo.FindAll(s => s.hrgcod.Substring(0, 3) == "352").Sum(p => p.ttlamt)).ToString("#,##0.00;(#,##0.00); ");
            string servicelength = emplist[0].servleng.ToString();
            var grossslary = sttlmntinfo[0].ttlamt.ToString();

            double netpay = Convert.ToDouble(netamount);



            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_92_Mgt.RptEmpSattelmentBangla", list1, list2, null);
            rpt1.EnableExternalImages = true;

            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("rpttitle", "চূড়ান্ত নিষ্পত্তিকরন বিল"));
            rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            rpt1.SetParameters(new ReportParameter("netamount", netamount));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));

            // for Show EmplInfo
            rpt1.SetParameters(new ReportParameter("billDate", billDate));
            rpt1.SetParameters(new ReportParameter("name", name));
            rpt1.SetParameters(new ReportParameter("Desgin", Desgin));
            rpt1.SetParameters(new ReportParameter("Id", Id));
            rpt1.SetParameters(new ReportParameter("Section", Section));
            rpt1.SetParameters(new ReportParameter("jobseperation", jobseperation));
            rpt1.SetParameters(new ReportParameter("joining", joining));
            rpt1.SetParameters(new ReportParameter("sepdate", sepdate));
            rpt1.SetParameters(new ReportParameter("servicelength", servicelength));
            rpt1.SetParameters(new ReportParameter("inwords", ASTUtility.Trans(netpay, 2)));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" + ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        protected void lnkbtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                this.Save_Value();
                var sttlmntinfo = (List<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSttlemntInfo>)ViewState["tblsttlmnt"];

                string comcod = this.GetComeCode();
                string empid = this.ddlEmpName.SelectedValue.ToString();
                string curdate = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
                string refno = this.txtrefno.Text.Trim();
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string PostedByid = hst["usrid"].ToString();
                string Posttrmid = hst["compname"].ToString();
                string PostSession = hst["session"].ToString();
                string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                DataTable dt = ASITUtility03.ListToDataTable(sttlmntinfo);
                DataSet ds = new DataSet("ds1");
                ds.Tables.Add(dt);
                ds.Tables[0].TableName = "tbl1";
                bool result = HRData.UpdateXmlTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACR_EMPLOYEE", "INSERT_UPDATE_EMP_SETTLEMNT", ds, null, null, empid, curdate, refno, PostedByid, Posttrmid, PostSession, Posteddat, "", "", "", "", "");
                if (!result)
                    return;

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Updated Successfully" + "');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.ToString() + "');", true);
            }

        }

        private void FooterCalculation()
        {
            var sttlmntinfo1 = (List<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSttlemntInfo>)ViewState["tblsttlmnt1"];
            var sttlmntinfo2 = (List<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSttlemntInfo>)ViewState["tblsttlmnt2"];

            ((Label)this.gvsettlemntcredit.FooterRow.FindControl("lblfttlamt")).Text = sttlmntinfo1.FindAll(s => s.hrgcod.Substring(0, 3) == "351").Sum(p => p.ttlamt).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvsttlededuct.FooterRow.FindControl("lblgvfdedttlamt")).Text = sttlmntinfo2.FindAll(s => s.hrgcod.Substring(0, 3) == "352").Sum(p => p.ttlamt).ToString("#,##0.00;(#,##0.00); ");
            this.NetAmount.Text = (sttlmntinfo1.FindAll(s => s.hrgcod.Substring(0, 3) == "351").Sum(p => p.ttlamt) - sttlmntinfo2.FindAll(s => s.hrgcod.Substring(0, 3) == "352").Sum(p => p.ttlamt)).ToString("#,##0.00;(#,##0.00); ");
        }

        private void lnkbtnApprove_Click(object sender, EventArgs e)
        {
            //((Label)this.Master.FindControl("lblmsg")).Visible = true;
            //string comcod = this.GetComeCode();
            //string empid = this.ddlEmpName.SelectedValue.ToString();
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string PostedByid = hst["usrid"].ToString();
            //string Posttrmid = hst["compname"].ToString();
            //string PostSession = hst["session"].ToString();
            //string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            //bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACR_EMPLOYEE", "APPROVE_SETTLEMENT_INFO", empid, PostedByid, Posttrmid, PostSession, Posteddat, "", "", "", "", "");
            //if (!result)
            //    return;

            //((Label)this.Master.FindControl("lblmsg")).Text = "Approve Successfully";
            //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
        }

        private void lbtnPrint_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string empid = this.ddlEmpName.SelectedValue.ToString();
            string rpttype = this.rbtnstatement.SelectedIndex.ToString();


            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            var emplist = (List<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSepEmployee>)ViewState["empdata"];
            var sttlmntinfo = (List<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSttlemntInfo>)ViewState["tblsttlmnt"];

            var list1 = sttlmntinfo.FindAll(p => p.hrgcod.Substring(0, 3) == "351");
            var list2 = sttlmntinfo.FindAll(p => p.hrgcod.Substring(0, 3) == "352");
            var shorempdata = emplist.FindAll(d => d.empid == empid);
            if (rpttype == "0")
            {
                
                string billDate = shorempdata[0].billdate.ToString("dd-MMM-yyyy");
                string name = shorempdata[0].empname.ToString();
                string Desgin = shorempdata[0].designation.ToString();
                string Id = shorempdata[0].idno.ToString();
                string Section = shorempdata[0].deptname.ToString();
                string jobseperation = shorempdata[0].septypedesc.ToString();
                var grossslary = sttlmntinfo[0].amount.ToString();
                string joining = shorempdata[0].joindat.ToString("dd-MMM-yyyy");
                string sepdate = shorempdata[0].retdat.ToString("dd-MMM-yyyy");
                var netamount = (sttlmntinfo.FindAll(s => s.hrgcod.Substring(0, 3) == "351").Sum(p => p.ttlamt) - sttlmntinfo.FindAll(s => s.hrgcod.Substring(0, 3) == "352").Sum(p => p.ttlamt)).ToString("#,##0.00;(#,##0.00); ");
                string servicelength = shorempdata[0].servleng.ToString();

                double netpay = Convert.ToDouble(netamount);


                LocalReport rpt1 = new LocalReport();
                rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_92_Mgt.RptEmpSattelment", list1, list2, null);
                rpt1.EnableExternalImages = true;

                rpt1.SetParameters(new ReportParameter("comnam", comnam));
                rpt1.SetParameters(new ReportParameter("comadd", comadd));
                rpt1.SetParameters(new ReportParameter("rpttitle", "Employee Final Sattelment"));
                rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
                rpt1.SetParameters(new ReportParameter("netamount", netamount));
                rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));

                // for Show EmplInfo
                rpt1.SetParameters(new ReportParameter("billDate", billDate));
                rpt1.SetParameters(new ReportParameter("name", name));
                rpt1.SetParameters(new ReportParameter("Desgin", Desgin));
                rpt1.SetParameters(new ReportParameter("Id", Id));
                rpt1.SetParameters(new ReportParameter("Section", Section));
                rpt1.SetParameters(new ReportParameter("jobseperation", jobseperation));
                rpt1.SetParameters(new ReportParameter("joining", joining));
                rpt1.SetParameters(new ReportParameter("sepdate", sepdate));
                rpt1.SetParameters(new ReportParameter("servicelength", servicelength));
                rpt1.SetParameters(new ReportParameter("inwords", ASTUtility.Trans(netpay, 2)));


                Session["Report1"] = rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" + ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }
            else
            {

            }

           


        }


        protected void gvsettlemntcredit_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string hrgcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "hrgcod")).ToString();
                 
            }
        }

        protected void rbtnstatement_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            this.ShowPerformance();
     
        }

        protected void addRow_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            DataTable dt = ASITUtility03.ListToDataTable(((List<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSttlemntInfo>)ViewState["tblsttlmnt1"]).OrderBy(p=>p.seq).ToList());
            DataTable dt2 = ASITUtility03.ListToDataTable(((List<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSttlemntInfo>)ViewState["tblsttlmnt"]).OrderBy(p => p.seq).ToList());

            DataRow dr;
            DataRow dr2;
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
   

            string hrgdesc = ((TextBox)this.gvsettlemntcredit.Rows[index].FindControl("lblcreditinfo")).Text.ToString();
            string hrgcod = ((Label)this.gvsettlemntcredit.Rows[index].FindControl("lblhrgcod")).Text.ToString();
            string calculation = ((Label)this.gvsettlemntcredit.Rows[index].FindControl("lblcalculation")).Text.ToString().Trim() ?? "";
            int seq = Convert.ToInt32(((Label)this.gvsettlemntcredit.Rows[index].FindControl("lblseq")).Text.ToString().Trim() ?? "99");

            string frmdat = "";
            string todat = "";
            double numofday = 0.00;
        
            double amount = 0.00;
            double ttlamt = 0.00;
            double perday = 0.00;

            if (hrgcod== "35101" || hrgcod== "35108" || hrgcod == "35110")
            {
                 frmdat = "01-Jan-1900";
                 todat =  "01-Jan-1900";
            }
            else
            {
                frmdat = "";
                todat = "";

            }


            dr = dt.NewRow();
           
            dr["comcod"] = comcod;
            dr["hrgdesc"] = hrgdesc;
            dr["hrgcod"] = hrgcod;
            dr["frmdat"] = frmdat;
            dr["todat"] = todat;
            dr["numofday"] = numofday;
            dr["amount"] = amount;
            dr["calculation"] = calculation;
            dr["ttlamt"] = ttlamt;
            dr["perday"] = perday;
            dr["seq"] = seq;




            dr2 = dt2.NewRow();

            dr2["comcod"] = comcod;
            dr2["hrgdesc"] = hrgdesc;
            dr2["hrgcod"] = hrgcod;
            dr2["frmdat"] = frmdat;
            dr2["todat"] = todat;
            dr2["numofday"] = numofday;
            dr2["amount"] = amount;
            dr2["calculation"] = calculation;
            dr2["ttlamt"] = ttlamt;
            dr2["perday"] = perday;
            dr2["seq"] = seq;
            if (hrgcod == "35110")
            {
                dr["seq"] = seq - 1;
                dr2["seq"] = seq - 1;
            }


            dt.Rows.Add(dr);
            dt2.Rows.Add(dr2);
            ViewState["tblsttlmnt1"] = dt.DataTableToList<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSttlemntInfo>();
            ViewState["tblsttlmnt"] = dt2.DataTableToList<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSttlemntInfo>();

            this.Data_Bind();
          
        }

        protected void addRowD_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            DataTable dt = ASITUtility03.ListToDataTable(((List<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSttlemntInfo>)ViewState["tblsttlmnt2"]).OrderBy(p=>p.seq).ToList());
            DataTable dt2 = ASITUtility03.ListToDataTable(((List<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSttlemntInfo>)ViewState["tblsttlmnt"]).OrderBy(p => p.seq).ToList());

            DataRow dr;
            DataRow dr2;
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;


            string hrgdesc = ((TextBox)this.gvsttlededuct.Rows[index].FindControl("lblcreditinfo")).Text.ToString();
            string hrgcod = ((Label)this.gvsttlededuct.Rows[index].FindControl("lblhrgcod")).Text.ToString();
            string calculation = ((Label)this.gvsttlededuct.Rows[index].FindControl("lblcalculation")).Text.ToString().Trim() ?? "";
            int seq = Convert.ToInt32(((Label)this.gvsttlededuct.Rows[index].FindControl("lblseq")).Text.ToString().Trim() ?? "99");

            string frmdat = "";
            string todat = "";
            double numofday = 0.00;

            double amount = 0.00;
            double ttlamt = 0.00;
            double perday = 0.00;

            if (hrgcod == "35101" || hrgcod == "35108" || hrgcod == "35110")
            {
                frmdat = "01-Jan-1900";
                todat = "01-Jan-1900";
            }
            else
            {
                frmdat = "";
                todat = "";

            }


            dr = dt.NewRow();
            dr["comcod"] = comcod;
            dr["hrgdesc"] = hrgdesc;
            dr["hrgcod"] = hrgcod;
            dr["frmdat"] = frmdat;
            dr["todat"] = todat;
            dr["numofday"] = numofday;
            dr["amount"] = amount;
            dr["calculation"] = calculation;
            dr["ttlamt"] = ttlamt;
            dr["perday"] = perday;
            dr["seq"] = seq;

            dr2 = dt2.NewRow();
            dr2["comcod"] = comcod;
            dr2["hrgdesc"] = hrgdesc;
            dr2["hrgcod"] = hrgcod;
            dr2["frmdat"] = frmdat;
            dr2["todat"] = todat;
            dr2["numofday"] = numofday;
            dr2["amount"] = amount;
            dr2["calculation"] = calculation;
            dr2["ttlamt"] = ttlamt;
            dr2["perday"] = perday;
            dr2["seq"] = seq;

            dt.Rows.Add(dr);
            dt2.Rows.Add(dr2);
            ViewState["tblsttlmnt2"] = dt.DataTableToList<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSttlemntInfo>();
            ViewState["tblsttlmnt"] = dt2.DataTableToList<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSttlemntInfo>();

            this.Data_Bind();

        }

        protected void removeRow_Click(object sender, EventArgs e)
        {

     
            DataTable dt = ASITUtility03.ListToDataTable(((List<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSttlemntInfo>)ViewState["tblsttlmnt1"]).OrderBy(p=>p.seq).ToList());


            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string comcod = this.GetComeCode();
            string hrgdesc = ((TextBox)this.gvsettlemntcredit.Rows[index].FindControl("lblcreditinfo")).Text.ToString();
            string hrgcod = ((Label)this.gvsettlemntcredit.Rows[index].FindControl("lblhrgcod")).Text.ToString();


            if (dt.Rows[index]["hrgcod"].ToString() == hrgcod)
            {
                dt.Rows[index].Delete();
            }
            ViewState["tblsttlmnt1"] = dt.DataTableToList<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSttlemntInfo>();
            this.Data_Bind();
        }

        protected void removeRowD_Click(object sender, EventArgs e)
        {


            DataTable dt = ASITUtility03.ListToDataTable(((List<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSttlemntInfo>)ViewState["tblsttlmnt2"]).OrderBy(p => p.seq).ToList());


            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string comcod = this.GetComeCode();
            string hrgdesc = ((TextBox)this.gvsttlededuct.Rows[index].FindControl("lblcreditinfo")).Text.ToString();
            string hrgcod = ((Label)this.gvsttlededuct.Rows[index].FindControl("lblhrgcod")).Text.ToString();


            if (dt.Rows[index]["hrgcod"].ToString() == hrgcod)
            {
                dt.Rows[index].Delete();
            }
            ViewState["tblsttlmnt2"] = dt.DataTableToList<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassSttlemntInfo>();
            this.Data_Bind();
        }
    }
}