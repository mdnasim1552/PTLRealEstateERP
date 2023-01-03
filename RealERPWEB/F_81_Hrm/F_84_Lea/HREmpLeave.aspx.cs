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
using Microsoft.Reporting.WinForms;
using RealERPRDLC;
namespace RealERPWEB.F_81_Hrm.F_84_Lea
{
    public partial class HREmpLeave : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                this.ShowView();
                this.GetCompany();
                ((Label)this.Master.FindControl("lblTitle")).Text = "COMPANY LEAVE RULE";
                this.GetProjectName();
                Create_table();
                this.txtaplydate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.ShowInformation();
                //this.lbtnOk_Click(null,null);


                //if (GetComeCode() == "3370")
                //{
                //    this.lnkReset.Visible = false;
                //    this.lnkRule.Visible = false;
                //}

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
        private void GetCompany()
        {
            Session.Remove("tblcompany");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();
            string txtCompany = "%%";

            //DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETCOMPANYNAME", txtCompany, userid, "", "", "", "", "", "", "");
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_BASIC_UTILITY_DATA", "GET_ACCESSED_COMPANYLIST", txtCompany, userid, "", "", "", "", "", "", "");
            this.ddlCompany.DataTextField = "actdesc";
            this.ddlCompany.DataValueField = "actcode";
            this.ddlCompany.DataSource = ds1.Tables[0];
            this.ddlCompany.DataBind();
            Session["tblcompany"] = ds1.Tables[0];
            ds1.Dispose();
            this.ddlCompany_SelectedIndexChanged(null, null);


        }
        protected void imgbtnEmpSeach_Click(object sender, EventArgs e)
        {
            this.ShowValue();
        }

        private void ShowView()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            string comcod = this.GetComeCode();
            switch (type)
            {
                case "LeaveRule":
                    this.txtdate.Text = System.DateTime.Today.ToString("yyyy");
                    this.MultiView1.ActiveViewIndex = 0;
                    break;
                case "LeaveApp":
                    ((Label)this.Master.FindControl("lblTitle")).Text = "LEAVE APPLICATION VIEW/EDIT";

                    this.MultiView1.ActiveViewIndex = 1;
                    switch (comcod)
                    {
                        case "4301":
                        case "4305":
                            this.rblstapptype.SelectedIndex = 2;
                            break;

                        default:
                            this.rblstapptype.SelectedIndex = 0;
                            break;
                    }
                    lblEmpIdSearch.Visible = false;
                    this.divLeaveApp.Visible = false;
                    this.divPage.Visible = false;
                    this.ddlpagesize.Visible = false;                   
                    this.txtEmpSearch.Visible = false;
                    this.imgbtnEmpSeach.Visible = false;
                    break;
                case "FLeaveApp":
                    ((Label)this.Master.FindControl("lblTitle")).Text = "LEAVE APPLICATION FORM VIEW/EDIT";
                    this.txtformdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 2;
                    switch (comcod)
                    {
                        case "4301":
                        case "4305":
                            this.rblstFormType.SelectedIndex = 2;
                            break;
                        default:
                            this.rblstFormType.SelectedIndex = 0;
                            break;
                    }
                    //this.rblstFormType.SelectedIndex = 0;

                    this.divLeaveApp.Visible = false;
                    this.divPage.Visible = false;
                    this.ddlpagesize.Visible = false;                    
                    lblEmpIdSearch.Visible = false;

                    this.txtEmpSearch.Visible = false;
                    this.imgbtnEmpSeach.Visible = false;
                    break;
            }

        }
        private void GetProjectName()
        {
            string comcod = this.GetComeCode();
            string txtSProject = "%%";
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string nozero = (hrcomln == 4) ? "0000" : "00";
            string company = (this.ddlCompany.SelectedValue.Substring(0, hrcomln).ToString() == nozero) ? "%" : this.ddlCompany.SelectedValue.Substring(0, hrcomln).ToString() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPROJECTNAMEFL", txtSProject, company, "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "deptname";
            this.ddlProjectName.DataValueField = "deptid";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();

        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.SelectIndex();
        }
        private void SelectIndex()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "LeaveRule":
                    this.ShowLeaveRule();
                    break;

                case "LeaveApp":
                    this.ShowLeaveApp();
                    break;

                case "FLeaveApp":
                    this.ShowFLeaveApp();
                    break;
            }


        }
        private void ShowLeaveRule()
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.ddlCompany.Enabled = false;
                this.ddlProjectName.Enabled = false;
                this.txtdate.ReadOnly = true;
                this.chkLeave.Visible = true;
                this.ShowValue();
            }
            else
            {
                this.lbtnOk.Text = "Ok";
                this.ddlCompany.Enabled = true;
                this.ddlProjectName.Enabled = true;
                this.txtdate.ReadOnly = false;
                this.chkLeave.Visible = false;
                this.gvLeaveRule.DataSource = null;
                this.gvLeaveRule.DataBind();

            }
        }

        private void ShowLeaveApp()
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.ddlCompany.Enabled = false;
                this.ddlProjectName.Enabled = false;
                this.lblleaveApp.Visible = true;
                this.lblleaveStatus.Visible = true;
                this.lblleaveInformation.Visible = true;
                //this.PnlEmp.Visible = true;
                this.Pnlapply.Visible = true;
                this.PnlRmrks.Visible = true;
                this.divEmpDetails.Visible = true;
                this.lbtnOk.Text = "New";

                this.txtApprdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetLeaveid();
                this.ddlProjectName_SelectedIndexChanged(null, null);
                this.imgbtnlAppEmpSeaarch_Click(null, null);
                //this.imgbtnlFEmpSeaarch_Click(null, null);
            }
            else
            {

                this.lbtnOk.Text = "Ok";
                this.ddlCompany.Enabled = true;
                this.ddlProjectName.Enabled = true;
               // this.PnlEmp.Visible = false;
                this.divEmpDetails.Visible = false;

                this.Pnlapply.Visible = false;
                this.PnlRmrks.Visible = false;
                this.lblleaveApp.Visible = false;
                this.lblleaveStatus.Visible = false;
                this.lblleaveInformation.Visible = false;
                this.ddlEmpName.Items.Clear();

                this.lblComPany.Text = "";
                this.lblSection.Text = "";
                this.lblDesignation.Text = "";
                this.lblJoiningDate.Text = "";

                this.txtLeavLreasons.Text = "";
                this.txtaddofenjoytime.Text = "";
                this.txtLeavRemarks.Text = "";
               
                
                this.ddlPreLeave.Items.Clear();
                this.gvLeaveApp.DataSource = null;
                this.gvLeaveApp.DataBind();
                this.gvLeaveStatus.DataSource = null;
                this.gvLeaveStatus.DataBind();
                this.gvleaveInfo.DataSource = null;
                this.gvleaveInfo.DataBind();
            }
        }
        private void ShowFLeaveApp()
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.ddlCompany.Enabled = false;
                this.ddlProjectName.Enabled = false;
                this.PnlEmplApp.Visible = true;
                this.imgbtnlFEmpSeaarch_Click(null, null);

            }
            else
            {
                this.lbtnOk.Text = "Ok";
                this.ddlCompany.Enabled = true;
                this.ddlProjectName.Enabled = true;
                this.PnlEmplApp.Visible = false;
                this.ddlEmpNamelApp.Items.Clear();
                this.lblComPanylApp.Text = "";
                this.lblSectionlApp.Text = "";
                this.lblDesignationlApp.Text = "";
                this.lblJoiningDatelApp.Text = "";
                this.gvLeaveStatus01.DataSource = null;
                this.gvLeaveStatus01.DataBind();

            }
        }
        private void ShowValue()
        {
            Session.Remove("YearLeav");
            string empcode = this.txtEmpSearch.Text.Trim();
            string comcod = this.GetComeCode();         
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string nozero = (hrcomln == 4) ? "0000" : "00";
            string company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln)+"%";//(this.ddlCompany.SelectedValue.Substring(0, 2).ToString() == "00") ? "%" : this.ddlCompany.SelectedValue.Substring(0, 2).ToString() + "%";
            string yearid = this.txtdate.Text;
            string pactcode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? company : this.ddlProjectName.SelectedValue.ToString() + "%";           
            pactcode = (empcode.Length == 0) ? pactcode : company;
             empcode = empcode + "%"; // for alwayes search empcode wise

            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPLEAVE", yearid, pactcode, company, empcode, "", "", "", "", "");
            if (ds4 == null)
            {
                this.gvLeaveRule.DataSource = null;
                this.gvLeaveRule.DataBind();
                return;
            }
            DataTable dt = HiddenSameData(ds4.Tables[0]);
            Session["YearLeav"] = dt;
            this.LoadGrid();
        }
        protected void ddlModiType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowValue();
        }
        private void LoadGrid()
        {

            DataTable dt = (DataTable)Session["YearLeav"];
            // Wokign Progress
            dt.TableName = "Table";
            DataTable dtfilter = new DataTable();
            DataView view = new DataView();
            view.Table = dt;
            string mtype = this.ddlModiType.SelectedValue.ToString();
            if(mtype== "Updated")
            { 
                //                             
                view.RowFilter = "lvupdate='Updated'";
                dtfilter = view.ToTable();
            }
            else if (mtype == "Notupdate")
            {
                
                view.RowFilter = "lvupdate='NotUpdated'";
                dtfilter = view.ToTable();
            }
            else
            {
                dtfilter = view.ToTable();
            }
            if (GetComeCode() == "3354" || GetComeCode() == "3370")
            {
                this.gvLeaveRule.Columns[0].HeaderText = "sl no";

                this.gvLeaveRule.Columns[13].HeaderText = "Paternity Leave";
                this.gvLeaveRule.Columns[18].HeaderText = "Probation";


            }

            if (GetComeCode() == "3367")
            {
                this.gvLeaveRule.Columns[15].HeaderText = "Special Leave";
            }
            this.gvLeaveRule.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvLeaveRule.DataSource = dtfilter;

            this.gvLeaveRule.DataBind();

        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string secid = dt1.Rows[0]["secid"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["secid"].ToString() == secid)
                {
                    secid = dt1.Rows[j]["secid"].ToString();
                    dt1.Rows[j]["secid"] = "";
                    dt1.Rows[j]["secname"] = "";
                }
                else
                {
                    secid = dt1.Rows[j]["secid"].ToString();
                }
            }
            return dt1;
        }

        private void SaveValue()
        {
            DataTable dt = (DataTable)Session["YearLeav"];
            int TblRowIndex;
            for (int i = 0; i < this.gvLeaveRule.Rows.Count; i++)
            {

                string ernleave = Convert.ToDouble("0" + ((TextBox)this.gvLeaveRule.Rows[i].FindControl("txtgvel")).Text.Trim()).ToString();
                string csleave = Convert.ToDouble("0" + ((TextBox)this.gvLeaveRule.Rows[i].FindControl("txtgvcl")).Text.Trim()).ToString();
                string skleave = Convert.ToDouble("0" + ((TextBox)this.gvLeaveRule.Rows[i].FindControl("txtgvsl")).Text.Trim()).ToString();
                string mtleave = Convert.ToDouble("0" + ((TextBox)this.gvLeaveRule.Rows[i].FindControl("txtgvml")).Text.Trim()).ToString();
                string ptleave = Convert.ToDouble("0" + ((TextBox)this.gvLeaveRule.Rows[i].FindControl("txtgvpl")).Text.Trim()).ToString();
                string wpleave = Convert.ToDouble("0" + ((TextBox)this.gvLeaveRule.Rows[i].FindControl("txtgvWPl")).Text.Trim()).ToString();
                string trpleave = Convert.ToDouble("0" + ((TextBox)this.gvLeaveRule.Rows[i].FindControl("txtgvTrL")).Text.Trim()).ToString();
                string lonproidleave = Convert.ToDouble("0" + ((TextBox)this.gvLeaveRule.Rows[i].FindControl("txtgvLOnProba")).Text.Trim()).ToString();
                string lonsepaleave = Convert.ToDouble("0" + ((TextBox)this.gvLeaveRule.Rows[i].FindControl("txtgvLOnSepa")).Text.Trim()).ToString();
                string LOnApprentice = Convert.ToDouble("0" + ((TextBox)this.gvLeaveRule.Rows[i].FindControl("txtgvLOnApprentice")).Text.Trim()).ToString();
                string LOnHajj = Convert.ToDouble("0" + ((TextBox)this.gvLeaveRule.Rows[i].FindControl("txtgvLOnHajjlv")).Text.Trim()).ToString();

                TblRowIndex = (gvLeaveRule.PageIndex) * gvLeaveRule.PageSize + i;

                dt.Rows[TblRowIndex]["ernleave"] = ernleave;
                dt.Rows[TblRowIndex]["csleave"] = csleave;
                dt.Rows[TblRowIndex]["skleave"] = skleave;
                dt.Rows[TblRowIndex]["mtleave"] = mtleave;
                dt.Rows[TblRowIndex]["ptleave"] = ptleave;
                dt.Rows[TblRowIndex]["wpleave"] = wpleave;
                dt.Rows[TblRowIndex]["trpleave"] = trpleave;
                dt.Rows[TblRowIndex]["lonproidleave"] = lonproidleave;
                dt.Rows[TblRowIndex]["lonsepaleave"] = lonsepaleave;
                dt.Rows[TblRowIndex]["lappreleave"] = LOnApprentice;
                dt.Rows[TblRowIndex]["lapphajjleave"] = LOnHajj;
            }
            Session["YearLeav"] = dt;
        }
        protected void gvLeaveRule_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvLeaveRule.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }

        protected void lnkbtnTotalLeave_Click(object sender, EventArgs e)
        {
            this.SaveLeave();
        }
        protected void lnkbtnFUpLeave_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            string Message;
            DataTable dt = (DataTable)Session["YearLeav"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string yearid = this.txtdate.Text;
            
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string empid = dt.Rows[i]["empid"].ToString();

                string ernid = dt.Rows[i]["ernid"].ToString();
                string ernleave = dt.Rows[i]["ernleave"].ToString();

                string csid = dt.Rows[i]["csid"].ToString();
                string csleave = dt.Rows[i]["csleave"].ToString();

                string skid = dt.Rows[i]["skid"].ToString();
                string skleave = dt.Rows[i]["skleave"].ToString();

                string mtid = dt.Rows[i]["mtid"].ToString();
                string mtleave = dt.Rows[i]["mtleave"].ToString();

                string wpid = dt.Rows[i]["wpid"].ToString();
                string wpleave = dt.Rows[i]["wpleave"].ToString();

                string trpid = dt.Rows[i]["trpid"].ToString();
                string trpleave = dt.Rows[i]["trpleave"].ToString();

                string ptid = dt.Rows[i]["ptid"].ToString();
                string ptleave = dt.Rows[i]["ptleave"].ToString(); // Edison

                string lonproid = dt.Rows[i]["lonproid"].ToString();
                string lonproidleave = dt.Rows[i]["lonproidleave"].ToString();

                string lonsepaid = dt.Rows[i]["lonsepaid"].ToString();
                string lonsepaleave = dt.Rows[i]["lonsepaleave"].ToString();
   


                string lappretiship = dt.Rows[i]["lappretiship"].ToString();
                string lappreleave = dt.Rows[i]["lappreleave"].ToString();

                string lapphajj = dt.Rows[i]["lapphajj"].ToString();
                string lapphajjleave = dt.Rows[i]["lapphajjleave"].ToString();

               
                bool result = HRData.UpdateTransInfo01(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPEMLEAV", yearid, empid, ernid, ernleave, csid, csleave, skid, 
                    skleave, mtid, mtleave, wpid, wpleave, trpid, trpleave, ptid, ptleave, lonproid, lonproidleave, lonsepaid, lonsepaleave, lappretiship, lappreleave, lapphajj, lapphajjleave);
                if (result == false)
                {
                    Message = "Data Is Not Updated ";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Message + "');", true);                     
                }
                else
                {
                    Message = "Updated Successfully";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Message + "');", true);
                }
            }
        }
        protected void lnkbtnGenLeave_Click(object sender, EventArgs e)
        {
            string ernleave = Convert.ToDouble("0" + this.txternleave.Text).ToString();
            string csleave = Convert.ToDouble("0" + this.txtcsleave.Text).ToString();
            string skleave = Convert.ToDouble("0" + this.txtskleave.Text).ToString();
            string mtleave = Convert.ToDouble("0" + this.txtmtleave.Text).ToString();
            string wpleave = Convert.ToDouble("0" + this.txtWPayleave.Text).ToString();
            string trpleave = Convert.ToDouble("0" + this.txtTrainleave.Text).ToString();
            string ptleave = Convert.ToDouble("0" + this.txtptleave.Text).ToString();
            string lonproidleave = Convert.ToDouble("0" + this.txtleaveOnProvi.Text).ToString();
            string lonsepaleave = Convert.ToDouble("0" + this.txtleaveOnSepa.Text).ToString();

            DataTable dt = (DataTable)Session["YearLeav"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                 

                dt.Rows[i]["ernleave"] = ernleave;
                dt.Rows[i]["csleave"] = csleave;
                dt.Rows[i]["skleave"] = skleave;
                dt.Rows[i]["mtleave"] = mtleave;
                dt.Rows[i]["wpleave"] = wpleave;
                dt.Rows[i]["trpleave"] = trpleave;
                dt.Rows[i]["ptleave"] = ptleave;
                dt.Rows[i]["lonproidleave"] = lonproidleave;
                dt.Rows[i]["lonsepaleave"] = lonsepaleave;
            }
            Session["YearLeav"] = dt;
            this.chkLeave.Checked = false;
            this.chkLeave_CheckedChanged(null, null);
            this.LoadGrid();



        }
        protected void chkLeave_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkLeave.Checked == true)
            {
                this.pnlleave.Visible = true;
                this.txtcsleave.Text = "";
                this.txternleave.Text = "";
                this.txtskleave.Text = "";
                this.txtmtleave.Text = "";
                this.txtWPayleave.Text = "";
                this.txtptleave.Text = "";
                this.txtTrainleave.Text = "";               
                this.txtleaveOnProvi.Text = "";
                this.txtleaveOnSepa.Text = "";
                



                string comcod = this.GetComeCode();
                switch (comcod)
                {
                    case "3347"://PEB
                        DataTable dt = (DataTable)Session["YearLeav"];
                        foreach (DataRow dr1 in dt.Rows)
                        {
                            int mon = Convert.ToInt32(dr1["monjoin"].ToString());
                            int ernleave = mon >= 12 ? 20 : (mon >= 6 ? 10 : (mon >= 3 ? 5 : 0));
                            dr1["ernleave"] = ernleave;
                            dr1["csleave"] = 10;
                            dr1["skleave"] = 14;
                            dr1["mtleave"] = 0.00;
                            dr1["wpleave"] = 0.00;
                            dr1["trpleave"] = 0.00;

                        }
                        Session["YearLeav"] = dt;
                        this.chkLeave.Checked = false;
                        this.chkLeave_CheckedChanged(null, null);
                        this.LoadGrid();

                        break;


                    default:
                        break;


                }


            }
            else
            {
                this.pnlleave.Visible = false;
            }
        }




        private void GetEmployeeName()
        {
            Session.Remove("tblEmpDesc");
            Session.Remove("tblleave");
            string comcod = this.GetComeCode();
            string company = (this.ddlCompany.SelectedValue.Substring(0, 2).ToString() == "00") ? "%" : this.ddlCompany.SelectedValue.Substring(0, 2).ToString() + "%";
            string pactcode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            string IdCardNo = "%%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPROJECTWSEMPNAME", pactcode, company, IdCardNo, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlEmpName.DataTextField = "empname";
            this.ddlEmpName.DataValueField = "empid";
            this.ddlEmpName.DataSource = ds1.Tables[0];
            this.ddlEmpName.DataBind();
            Session["tblEmpDesc"] = ds1.Tables[0];
            // Session["tblemplev"] = ds1.Tables[1];

            this.gvLeaveApp.DataSource = ds1.Tables[1];

            this.gvLeaveApp.DataBind();
            Session["tblleave"] = ds1.Tables[1];
            this.ddlEmpName_SelectedIndexChanged(null, null);
        }

        private void GetLeaveid()
        {

            string comcod = this.GetComeCode();
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLEAVEID", "", "", "", "", "", "", "", "", "");
            this.lbltrnleaveid.Text = ds5.Tables[0].Rows[0]["ltrnid"].ToString().Trim();
        }


        private void GetPreLeaveNo()
        {
            string comcod = this.GetComeCode();
            string mTRNNo = "NEWTRN";
            if (this.ddlPreLeave.Items.Count > 0)
                mTRNNo = this.ddlPreLeave.SelectedValue.ToString();

            if (mTRNNo == "NEWTRN")
            {
                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLEAVEID", "", "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.lbltrnleaveid.Text = ds1.Tables[0].Rows[0]["ltrnid"].ToString().Trim();

                    this.ddlPreLeave.DataTextField = "ltrnid";
                    this.ddlPreLeave.DataValueField = "ltrnid";
                    this.ddlPreLeave.DataSource = ds1.Tables[0];
                    this.ddlPreLeave.DataBind();
                }

            }

        }



        protected void ddlEmpName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowEmppLeave();
            this.EmpLeaveInfo();
            this.RefreshLeave(); 
        }

        private void RefreshLeave()
        {
            string comcod = this.GetComeCode();
            string company = (this.ddlCompany.SelectedValue.Substring(0, 2).ToString() == "00") ? "%" : this.ddlCompany.SelectedValue.Substring(0, 2).ToString() + "%";
            string pactcode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            string IdCardNo = "%%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPROJECTWSEMPNAME", pactcode, company, IdCardNo, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.gvLeaveApp.DataSource = ds1.Tables[1];
            this.gvLeaveApp.DataBind();
            EmpLeaveInfo();

        }


        private void ShowEmppLeave()
        {

            this.lbltrnleaveid.Text = "";
            this.ddlPreLeave.Items.Clear();
            this.txtLeavLreasons.Text = "";
            this.txtLeavRemarks.Text = "";
            Session.Remove("tblleavest");
            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            string aplydat = Convert.ToDateTime(this.txtaplydate.Text).ToString("dd-MMM-yyyy");
            DataTable dt = (DataTable)Session["tblEmpDesc"];
            DataRow[] dr1 = dt.Select("empid='" + empid + "'");

            if (dr1.Length > 0)
            {
                this.lblComPany.Text = dr1[0]["companyname"].ToString();
                this.lblSection.Text = dr1[0]["section"].ToString();
                this.lblDesignation.Text = dr1[0]["desig"].ToString();
                this.lblJoiningDate.Text = Convert.ToDateTime(dr1[0]["joindate"]).ToString("dd-MMM-yyyy");

            }

            //string calltype = ((this.rblstapptype.SelectedIndex == 0) ? "LEAVESTATUS" : (this.rblstapptype.SelectedIndex == 1) ? "LEAVESTATUS01" : "LEAVESTATUS02");
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "LEAVESTATUS02", empid, aplydat, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvLeaveStatus.DataSource = null;
                this.gvLeaveStatus.DataBind();
                return;
            }

            Session["tblleavest"] = ds1.Tables[0];

            this.Data_Bind();

        }


        private void EmpLeaveInfo()
        {
            ViewState.Remove("tblempleaveinfo");
            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            string aplydat = Convert.ToDateTime(this.txtaplydate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "LEAVEINFORMATION", empid, aplydat, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvleaveInfo.DataSource = null;
                this.gvleaveInfo.DataBind();
                return;
            }
            DataTable dt1 = ds1.Tables[0];
            if (dt1.Rows.Count == 0)
            {
                this.gvleaveInfo.DataSource = null;
                this.gvleaveInfo.DataBind();
                return;
            }
            string gcod = dt1.Rows[0]["gcod"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["gcod"].ToString() == gcod)
                    dt1.Rows[j]["gdesc"] = "";
                gcod = dt1.Rows[j]["gcod"].ToString();
            }
            ViewState["tblempleaveinfo"] = dt1;
            this.gvleaveInfo.DataSource = dt1;
            this.gvleaveInfo.DataBind();
            ds1.Dispose();
        }

        protected void lnkbtnRef_Click(object sender, EventArgs e)
        {
            this.RefreshLeave();
            this.EmpLeaveInfo();
            this.ShowEmppLeave();
        }
        private void Data_Bind()
        {
            this.gvLeaveStatus.DataSource = (DataTable)Session["tblleavest"];
            this.gvLeaveStatus.DataBind();
        }


        private void SaveLeave()
        {
            this.lblleaveApp.Visible = true;
            DataTable dt = (DataTable)Session["tblleave"];
            DataTable dt1 = (DataTable)Session["tblleavest"];
            for (int i = 0; i < this.gvLeaveApp.Rows.Count; i++)
            {
                //TimeSpan ts = (this.CalExt3.SelectedDate.Value - this.CalExt2.SelectedDate.Value);
                double leaveday = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvLeaveApp.Rows[i].FindControl("txtgvlapplied")).Text.Trim()));
                // double leaveday = Convert.ToInt32("0" + ((TextBox)this.gvLeaveApp.Rows[i].FindControl("txtgvlapplied")).Text.Trim());
                double leaveday1 = Math.Ceiling(leaveday);
                if (leaveday > 0)
                {
                    string stdat = Convert.ToDateTime(((TextBox)this.gvLeaveApp.Rows[i].FindControl("txtgvenjoydt1")).Text.Trim()).ToString("dd-MMM-yyyy");
                    string endat = Convert.ToDateTime(stdat).AddDays(leaveday1 - 1).ToString("dd-MMM-yyyy");
                    dt.Rows[i]["lapplied"] = leaveday;
                    dt.Rows[i]["lenjoydt1"] = stdat;
                    dt.Rows[i]["lenjoydt2"] = endat;
                    //double enjleave = Convert.ToDouble(dt1.Rows[i]["ltaken"]);
                    double Clsleave = Convert.ToDouble(dt1.Rows[i]["pbal"]);
                    dt1.Rows[i]["applyday"] = leaveday;
                    dt1.Rows[i]["appday"] = leaveday;
                    dt1.Rows[i]["applydate"] = stdat;
                    dt1.Rows[i]["appdate"] = stdat;
                    //dt1.Rows[i]["todate"] = endat;
                    //dt1.Rows[i]["pbal"] = Convert.ToInt32(dt1.Rows[i]["pbal"]) - leaveday;
                    //dt1.Rows[i]["ltaken"] = Convert.ToInt32(dt1.Rows[i]["ltaken"]) + leaveday;
                    dt1.Rows[i]["balleave"] = Clsleave - leaveday;
                    dt1.Rows[i]["tltakreq"] = leaveday;


                }
            }

            this.gvLeaveApp.DataSource = dt;
            this.gvLeaveApp.DataBind();
            Session["tblleave"] = dt;


            //---------For Status table --------------------------------------

            Session["tblleavest"] = dt1;
            this.Data_Bind();
        }

        private void GetLveAppEmployeeName()
        {
            Session.Remove("tblEmpDesc");
            string comcod = this.GetComeCode();
            string company = (this.ddlCompany.SelectedValue.Substring(0, 2).ToString() == "00") ? "%" : this.ddlCompany.SelectedValue.Substring(0, 2).ToString() + "%";
            string pactcode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            string IdCardNo = "%%";
            //string date = Convert.ToDateTime(this.txtformdate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPROJECTWSEMPNAME", company, pactcode, IdCardNo, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlEmpNamelApp.DataTextField = "empname";
            this.ddlEmpNamelApp.DataValueField = "empid";
            this.ddlEmpNamelApp.DataSource = ds1.Tables[0];
            this.ddlEmpNamelApp.DataBind();
            Session["tblEmpDesc"] = ds1.Tables[0];
            //Session["tblleaveAd"] = ds1.Tables[0];


            this.ddlEmpNamelApp_SelectedIndexChanged(null, null);



        }


        protected void ddlEmpNamelApp_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session.Remove("tblleavest");

            string comcod = this.GetComeCode();
            string empid = this.ddlEmpNamelApp.SelectedValue.ToString();
            string aplydat = Convert.ToDateTime(this.txtformdate.Text).ToString("dd-MMM-yyyy");
            DataTable dt = (DataTable)Session["tblEmpDesc"];
            DataRow[] dr1 = dt.Select("empid='" + empid + "'");

            if (dr1.Length > 0)
            {

                this.lblComPanylApp.Text = dr1[0]["companyname"].ToString();
                this.lblSectionlApp.Text = dr1[0]["section"].ToString();
                this.lblDesignationlApp.Text = dr1[0]["desig"].ToString();
                this.lblJoiningDatelApp.Text = Convert.ToDateTime(dr1[0]["joindate"]).ToString("dd-MMM-yyyy");
               // this.lblmobno.Text = dr1[0]["phoneno"].ToString();


            }


            //string calltype = ((this.rblstFormType.SelectedIndex == 0) ? "LEAVESTATUS" : (this.rblstFormType.SelectedIndex == 1) ? "LEAVESTATUS01" : "LEAVESTATUS02");
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "LEAVESTATUS02", empid, aplydat, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvLeaveStatus01.DataSource = null;
                this.gvLeaveStatus01.DataBind();
                return;
            }

            this.gvLeaveStatus01.DataSource = ds1.Tables[0];
            this.gvLeaveStatus01.DataBind();
            Session["tblleavest"] = ds1.Tables[0];


        }

        protected void lnkbtnPreLeave_Click(object sender, EventArgs e)
        {
            this.PnlPreLeave.Visible = false;
            this.chkPreLeave.Checked = false;
            this.PreLeaveInfo();
            this.chkPreLeave_CheckedChanged(null, null);
        }
        private void PreLeaveInfo()
        {
            Session.Remove("tblleavest");
            DataTable dt = (DataTable)ViewState["tblprelinf"];
            string ltrnid = this.ddlPreLeave.SelectedValue.ToString();
            DataRow[] drp = dt.Select("ltrnid='" + ltrnid + "'");
            if (dt.Rows.Count == 0)
                return;

            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            string date = Convert.ToDateTime(drp[0]["strtdat"]).ToString("dd-MMM-yyyy");
            //string calltype = ((this.rblstapptype.SelectedIndex == 0) ? "LEAVESTATUS" : (this.rblstapptype.SelectedIndex == 1) ? "LEAVESTATUS01" : "LEAVESTATUS02");

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "LEAVESTATUS02", empid, date, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvLeaveStatus.DataSource = null;
                this.gvLeaveStatus.DataBind();
                return;
            }
            // Session["tblleavest"] = ds1.Tables[0];
            DataTable dt1 = (DataTable)Session["tblleave"];
            DataTable dt2 = ds1.Tables[0];


            string gcod = drp[0]["gcod"].ToString();
            DataRow[] drl = dt1.Select("gcod='" + gcod + "'");
            DataRow[] drls = dt2.Select("gcod='" + gcod + "'");

            //leave-------
            drl[0]["lapplied"] = drp[0]["lapplied"];
            drl[0]["lenjoydt1"] = drp[0]["strtdat"];
            drl[0]["lenjoydt2"] = drp[0]["enddat"];
            //leave status-------
            double leaveday = Convert.ToDouble(drp[0]["lapplied"].ToString());
            double enjleave = Convert.ToDouble(drls[0]["ltaken"]);
            double Clsleave = Convert.ToDouble(drls[0]["pbal"]);
            drls[0]["applyday"] = drp[0]["lapplied"];
            drls[0]["appday"] = drp[0]["lapplied"];
            drls[0]["applydate"] = drp[0]["strtdat"];
            drls[0]["appdate"] = drp[0]["strtdat"];
            // drls[0]["todate"] = drp[0]["strtdat"];

            drls[0]["balleave"] = Clsleave - leaveday;
            drls[0]["tltakreq"] = leaveday;
            //drls[0]["balleave"] = Clsleave - (leaveday + enjleave);
            //drls[0]["tltakreq"] = (leaveday + enjleave);

            Session["tblleave"] = dt1;
            Session["tblleavest"] = dt2;
            //Genral info
            this.lbltrnleaveid.Text = this.ddlPreLeave.SelectedValue.ToString();
            this.txtaplydate.Text = Convert.ToDateTime(drp[0]["aplydat"]).ToString("dd-MMM-yyyy");
            this.txtApprdate.Text = Convert.ToDateTime(drp[0]["aprdat"]).ToString("dd-MMM-yyyy");
            this.txtLeavLreasons.Text = drp[0]["lreason"].ToString();
            this.txtaddofenjoytime.Text = drp[0]["addlentime"].ToString();
            this.txtLeavRemarks.Text = drp[0]["lrmarks"].ToString();
            this.txtdutiesnameandDesig.Text = drp[0]["denameadesig"].ToString();

            //gvbind
            this.gvLeaveApp.DataSource = dt1;
            this.gvLeaveApp.DataBind();
            this.Data_Bind();
        }


        protected void chkPreLeave_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkPreLeave.Checked)
            {
                this.PnlPreLeave.Visible = true;
                this.PreLeaveno();
            }
            else
            {
                this.PnlPreLeave.Visible = false;
            }

        }
        private void PreLeaveno()
        {

            ViewState.Remove("tblprelinf");
            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtaplydate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "PREVIOUSLEAVENO", empid, date, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.ddlPreLeave.Items.Clear();
                return;
            }
            ViewState["tblprelinf"] = ds1.Tables[0];
            this.ddlPreLeave.DataTextField = "ltrndesc";
            this.ddlPreLeave.DataValueField = "ltrnid";
            this.ddlPreLeave.DataSource = ds1.Tables[0];
            this.ddlPreLeave.DataBind();

        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "LeaveRule":
                    break;

                case "LeaveApp":
                    this.PrintLeaveApprove();
                    break;


                case "FLeaveApp":
                    this.PrintLeaveForm();
                    break;
            }


        }
        private void PrintLeaveApprove()
        {
            // if (this.rblstapptype.SelectedIndex == 0 || this.rblstapptype.SelectedIndex == 2)
            //{
            //    this.PrintAppform1();

            //}
            //else 
            //{
            //    this.PrintAppform2();

            //}

            string comcod = this.GetComeCode();

            switch (comcod)
            {
                //case "4101":
                //    this.PrintAppform2();
                //    break;

                case "4301":
                    //case "3101":
                    this.PrintAppform2();
                    break;

                case "4101":
                case "4305":
                case "4315":
                case "4330":
                case "3101":
                    this.PrintAppform13();
                    break;

                default:
                    this.PrintAppform1();
                    break;
            }

        }

        private void PrintAppform1()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataView dv = ((DataTable)Session["tblleavest"]).DefaultView;
            dv.RowFilter = ("appday>0");
            DataTable dt = dv.ToTable();

            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.EmpLeaveAPP>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptHRSetup.GetLocalReport("R_81_Hrm.R_84_Lea.EmpLeavApp", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("txtRecordNo", this.lbltrnleaveid.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("txtleaveday", Convert.ToInt32(dt.Rows[0]["appday"]).ToString("#,##0;(#,##0); ") + " days"));
            Rpt1.SetParameters(new ReportParameter("txtldatefrm", Convert.ToDateTime(dt.Rows[0]["applydate"]).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("txtldateto", Convert.ToDateTime(dt.Rows[0]["applydate"]).AddDays(Convert.ToInt32(dt.Rows[0]["appday"]) - 1).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("txtlday", Convert.ToInt32(dt.Rows[0]["appday"]).ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtRecordNo1", this.lbltrnleaveid.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("txtEmpName", this.ddlEmpName.SelectedItem.Text.Substring(7)));
            Rpt1.SetParameters(new ReportParameter("txtEmpName1", this.ddlEmpName.SelectedItem.Text.Substring(7)));
            Rpt1.SetParameters(new ReportParameter("txtDesig", this.lblDesignation.Text));
            Rpt1.SetParameters(new ReportParameter("txtDesig1", this.lblDesignation.Text));
            Rpt1.SetParameters(new ReportParameter("txtApplydate", Convert.ToDateTime(this.txtaplydate.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("txtReasons", this.txtLeavLreasons.Text));
            Rpt1.SetParameters(new ReportParameter("txttitlelappslip", "Leave Approval Slip"));
            Rpt1.SetParameters(new ReportParameter("txtAppDays", Convert.ToInt32(dt.Rows[0]["appday"]).ToString("#,##0;(#,##0); ") + " days " + dt.Rows[0]["gdesc"].ToString() + " from " + Convert.ToDateTime(dt.Rows[0]["applydate"]).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Leave Application"));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            #region OLD
            //ReportDocument rptTest = new RealERPRPT.R_81_Hrm.R_84_Lea.EmpLeavApp();
            //TextObject txtRptComName = rptTest.ReportDefinition.ReportObjects["txtRptComName"] as TextObject;
            //txtRptComName.Text = comnam;
            //TextObject txtRptCompAdd = rptTest.ReportDefinition.ReportObjects["txtRptCompAdd"] as TextObject;
            //txtRptCompAdd.Text = comadd;
            //TextObject txtRecordNo = rptTest.ReportDefinition.ReportObjects["txtRecordNo"] as TextObject;
            //txtRecordNo.Text = this.lbltrnleaveid.Text.Trim();
            //TextObject txtRecordNo1 = rptTest.ReportDefinition.ReportObjects["txtRecordNo1"] as TextObject;
            //txtRecordNo1.Text = this.lbltrnleaveid.Text.Trim();
            //TextObject txtleaveday = rptTest.ReportDefinition.ReportObjects["txtleaveday"] as TextObject;
            //txtleaveday.Text = Convert.ToInt32(dt.Rows[0]["appday"]).ToString("#,##0;(#,##0); ") + " days";

            //TextObject txtldatefrm = rptTest.ReportDefinition.ReportObjects["txtldatefrm"] as TextObject;
            //txtldatefrm.Text = Convert.ToDateTime(dt.Rows[0]["applydate"]).ToString("dd-MMM-yyyy");
            //TextObject txtldateto = rptTest.ReportDefinition.ReportObjects["txtldateto"] as TextObject;
            //txtldateto.Text = Convert.ToDateTime(dt.Rows[0]["applydate"]).AddDays(Convert.ToInt32(dt.Rows[0]["appday"]) - 1).ToString("dd-MMM-yyyy");
            //TextObject txtlday = rptTest.ReportDefinition.ReportObjects["txtlday"] as TextObject;
            //txtlday.Text = Convert.ToInt32(dt.Rows[0]["appday"]).ToString("#,##0;(#,##0); ");

            //TextObject txtEmpName = rptTest.ReportDefinition.ReportObjects["txtEmpName"] as TextObject;
            //txtEmpName.Text = this.ddlEmpName.SelectedItem.Text.Substring(7);
            //TextObject txtEmpName1 = rptTest.ReportDefinition.ReportObjects["txtEmpName1"] as TextObject;
            //txtEmpName1.Text = this.ddlEmpName.SelectedItem.Text.Substring(7);
            //TextObject txtDesig = rptTest.ReportDefinition.ReportObjects["txtDesig"] as TextObject;
            //txtDesig.Text = this.lblDesignation.Text;
            //TextObject txtDesig1 = rptTest.ReportDefinition.ReportObjects["txtDesig1"] as TextObject;
            //txtDesig1.Text = this.lblDesignation.Text;
            ////TextObject txtApprdate = rptTest.ReportDefinition.ReportObjects["txtApprdate"] as TextObject;
            ////txtApprdate.Text = Convert.ToDateTime(this.txtApprdate.Text).ToString("dd-MMM-yyyy");
            //TextObject txtApplydate = rptTest.ReportDefinition.ReportObjects["txtApplydate"] as TextObject;
            //txtApplydate.Text = Convert.ToDateTime(this.txtaplydate.Text).ToString("dd-MMM-yyyy");

            //TextObject rpttxtReasons = rptTest.ReportDefinition.ReportObjects["txtReasons"] as TextObject;
            //rpttxtReasons.Text = this.txtLeavLreasons.Text;
            //TextObject txttitlelappslip = rptTest.ReportDefinition.ReportObjects["txttitlelappslip"] as TextObject;
            //txttitlelappslip.Text = "Leave Approval Slip";
            //TextObject txtAppDays = rptTest.ReportDefinition.ReportObjects["txtAppDays"] as TextObject;
            //txtAppDays.Text = Convert.ToInt32(dt.Rows[0]["appday"]).ToString("#,##0;(#,##0); ") + " days " + dt.Rows[0]["gdesc"].ToString() + " from " + Convert.ToDateTime(dt.Rows[0]["applydate"]).ToString("dd-MMM-yyyy");
            //rptTest.SetDataSource(((DataTable)Session["tblleavest"]));

            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptTest.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptTest;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
            //                   ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            #endregion
        }
        private void PrintAppform2()
        {
            DataView dv = ((DataTable)Session["tblleavest"]).DefaultView;
            dv.RowFilter = ("gcod='51001'");
            DataTable dt = dv.ToTable();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.EmpLeaveAPP>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptHRSetup.GetLocalReport("R_81_Hrm.R_84_Lea.RptHrEmpLeave02", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("txtRecordNo", this.lbltrnleaveid.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("txtleavedays", Convert.ToInt32(dt.Rows[0]["appday"]).ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtApplydate", Convert.ToDateTime(this.txtaplydate.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("txtlsdate", Convert.ToDateTime(dt.Rows[0]["applydate"]).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("txtledate", Convert.ToDateTime(dt.Rows[0]["applydate"]).AddDays(Convert.ToInt32(dt.Rows[0]["appday"]) - 1).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("txtEmpName", this.ddlEmpName.SelectedItem.Text.Substring(7)));
            Rpt1.SetParameters(new ReportParameter("txtDesig", this.lblDesignation.Text));
            Rpt1.SetParameters(new ReportParameter("txtReasons", this.txtLeavLreasons.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("txtAddofentime", this.txtaddofenjoytime.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("txtremarks", this.txtLeavRemarks.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Leave Application"));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            #region OLD
            //ReportDocument rptTest = new RealERPRPT.R_81_Hrm.R_84_Lea.RptHrEmpLeave02();
            //TextObject txtRptComName = rptTest.ReportDefinition.ReportObjects["txtRptComName"] as TextObject;
            //txtRptComName.Text = comnam;
            //TextObject txtRecordNo = rptTest.ReportDefinition.ReportObjects["txtRecordNo"] as TextObject;
            //txtRecordNo.Text = this.lbltrnleaveid.Text.Trim();
            //TextObject txtEmpName = rptTest.ReportDefinition.ReportObjects["txtEmpName"] as TextObject;
            //txtEmpName.Text = this.ddlEmpName.SelectedItem.Text.Substring(7);

            //TextObject txtDesig = rptTest.ReportDefinition.ReportObjects["txtDesig"] as TextObject;
            //txtDesig.Text = this.lblDesignation.Text;
            //TextObject txtApplydate = rptTest.ReportDefinition.ReportObjects["txtApplydate"] as TextObject;
            //txtApplydate.Text = Convert.ToDateTime(this.txtaplydate.Text).ToString("dd-MMM-yyyy");
            //TextObject txtlsdate = rptTest.ReportDefinition.ReportObjects["txtlsdate"] as TextObject;
            //txtlsdate.Text = Convert.ToDateTime(dt.Rows[0]["applydate"]).ToString("dd-MMM-yyyy");
            //TextObject txtledate = rptTest.ReportDefinition.ReportObjects["txtledate"] as TextObject;
            //txtledate.Text = Convert.ToDateTime(dt.Rows[0]["applydate"]).AddDays(Convert.ToInt32(dt.Rows[0]["appday"]) - 1).ToString("dd-MMM-yyyy");
            //TextObject txtleavedays = rptTest.ReportDefinition.ReportObjects["txtleavedays"] as TextObject;
            //txtleavedays.Text = Convert.ToInt32(dt.Rows[0]["appday"]).ToString("#,##0;(#,##0); ");
            //TextObject rpttxtReasons = rptTest.ReportDefinition.ReportObjects["txtReasons"] as TextObject;
            //rpttxtReasons.Text = this.txtLeavLreasons.Text.Trim();
            //TextObject txtAddofentime = rptTest.ReportDefinition.ReportObjects["txtAddofentime"] as TextObject;
            //txtAddofentime.Text = this.txtaddofenjoytime.Text.Trim();
            //TextObject rpttxtremarks = rptTest.ReportDefinition.ReportObjects["txtremarks"] as TextObject;
            //rpttxtremarks.Text = this.txtLeavRemarks.Text.Trim();

            //rptTest.SetDataSource(dt);
            //Session["Report1"] = rptTest;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            #endregion
        }


        private void PrintAppform13()
        {
            DataTable dtcal = ((DataTable)Session["tblleavest"]).Copy();
            DataView dv = dtcal.DefaultView;
            dv.RowFilter = ("appday>0");
            DataTable dt = dv.ToTable();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.EmpLeaveAPP>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptHRSetup.GetLocalReport("R_81_Hrm.R_84_Lea.RptHrEmpLeave03", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("txtCompanyName", this.lblComPany.Text));
            Rpt1.SetParameters(new ReportParameter("txtEmpName", this.ddlEmpName.SelectedItem.Text.Substring(7)));
            Rpt1.SetParameters(new ReportParameter("txtDesig", this.lblDesignation.Text));
            Rpt1.SetParameters(new ReportParameter("txtDepartment", this.lblSection.Text));
            Rpt1.SetParameters(new ReportParameter("txtJoiningDate", this.lblJoiningDate.Text));
            Rpt1.SetParameters(new ReportParameter("txtReasons", this.txtLeavLreasons.Text));
            Rpt1.SetParameters(new ReportParameter("txtdenameadesig", this.txtdutiesnameandDesig.Text));
            Rpt1.SetParameters(new ReportParameter("txtaveleave", (Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51001'"))[0]["ltaken"]) + Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51001'"))[0]["appday"])).ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtbeleave", Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51001'"))[0]["balleave"]).ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtavcleave", (Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51002'"))[0]["ltaken"]) + Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51002'"))[0]["appday"])).ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtbcleave", Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51002'"))[0]["balleave"]).ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtavsleave", (Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51003'"))[0]["ltaken"]) + Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51003'"))[0]["appday"])).ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtbsleave", Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51003'"))[0]["balleave"]).ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("appday", Convert.ToString(dt.Rows[0]["appday"])));
            Rpt1.SetParameters(new ReportParameter("appdate", Convert.ToDateTime(dt.Rows[0]["appdate"]).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "LEAVE APPLICATION FORM(Human Resource Department)"));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            #region OLD
            //ReportDocument rptTest = new RealERPRPT.R_81_Hrm.R_84_Lea.RptHrEmpLeave03();
            //TextObject txtRptComName = rptTest.ReportDefinition.ReportObjects["txtRptComName"] as TextObject;
            //txtRptComName.Text = comnam;
            //TextObject txtEmpName = rptTest.ReportDefinition.ReportObjects["txtEmpName"] as TextObject;
            //txtEmpName.Text = this.ddlEmpName.SelectedItem.Text.Substring(7);
            //TextObject txtCompanyName = rptTest.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            //txtCompanyName.Text = this.lblComPany.Text;
            //TextObject txtDesig = rptTest.ReportDefinition.ReportObjects["txtDesig"] as TextObject;
            //txtDesig.Text = this.lblDesignation.Text;
            //TextObject txtDepartment = rptTest.ReportDefinition.ReportObjects["txtDepartment"] as TextObject;
            //txtDepartment.Text = this.lblSection.Text;
            //TextObject txtJoiningDate = rptTest.ReportDefinition.ReportObjects["txtJoiningDate"] as TextObject;
            //txtJoiningDate.Text = this.lblJoiningDate.Text;
            //TextObject rpttxtReasons = rptTest.ReportDefinition.ReportObjects["txtReasons"] as TextObject;
            //rpttxtReasons.Text = this.txtLeavLreasons.Text;
            //TextObject txtdenameadesig = rptTest.ReportDefinition.ReportObjects["txtdenameadesig"] as TextObject;
            //txtdenameadesig.Text = this.txtdutiesnameandDesig.Text;
            //TextObject txtaveleave = rptTest.ReportDefinition.ReportObjects["txtaveleave"] as TextObject;
            //txtaveleave.Text = (Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51001'"))[0]["ltaken"]) + Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51001'"))[0]["appday"])).ToString("#,##0;(#,##0); ");
            //TextObject txtbeleave = rptTest.ReportDefinition.ReportObjects["txtbeleave"] as TextObject;
            //txtbeleave.Text = Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51001'"))[0]["balleave"]).ToString("#,##0;(#,##0); ");
            //TextObject txtavcleave = rptTest.ReportDefinition.ReportObjects["txtavcleave"] as TextObject;
            //txtavcleave.Text = (Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51002'"))[0]["ltaken"]) + Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51002'"))[0]["appday"])).ToString("#,##0;(#,##0); ");
            //TextObject txtbcleave = rptTest.ReportDefinition.ReportObjects["txtbcleave"] as TextObject;
            //txtbcleave.Text = Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51002'"))[0]["balleave"]).ToString("#,##0;(#,##0); ");
            //TextObject txtavsleave = rptTest.ReportDefinition.ReportObjects["txtavsleave"] as TextObject;
            //txtavsleave.Text = (Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51003'"))[0]["ltaken"]) + Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51003'"))[0]["appday"])).ToString("#,##0;(#,##0); ");
            //TextObject txtbsleave = rptTest.ReportDefinition.ReportObjects["txtbsleave"] as TextObject;
            //txtbsleave.Text = Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51003'"))[0]["balleave"]).ToString("#,##0;(#,##0); ");

            //rptTest.SetDataSource(dtcal);
            //Session["Report1"] = rptTest;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
            //                   ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            #endregion
        }

        private void PrintLeaveForm()
        {
            string comcod = this.GetComeCode();

            switch (comcod)
            {
                //case "4101":
                //    this.PrintLeaveform2();
                //    break;

                //case "3101":
                case "4301":
                    this.PrintLeaveform2();
                    break;

                case "3101":
                case "4330":
                case "4305":
                case "4315":
                    this.PrintLeaveform3();
                    break;

                default:
                    this.PrintLeaveform1();
                    break;

            }

        }
        private void PrintLeaveform1()
        {

            DataTable dt = ((DataTable)Session["tblleavest"]);
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.EmpLeaveAPP>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptHRSetup.GetLocalReport("R_81_Hrm.R_84_Lea.EmpLeavApp", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("txtRecordNo", ""));
            Rpt1.SetParameters(new ReportParameter("txtleaveday", ""));
            Rpt1.SetParameters(new ReportParameter("txtldatefrm", ""));
            Rpt1.SetParameters(new ReportParameter("txtldateto", ""));
            Rpt1.SetParameters(new ReportParameter("txtlday", ""));
            Rpt1.SetParameters(new ReportParameter("txtRecordNo1", ""));
            Rpt1.SetParameters(new ReportParameter("txtEmpName", this.ddlEmpNamelApp.SelectedItem.Text.Substring(7)));
            Rpt1.SetParameters(new ReportParameter("txtEmpName1", this.ddlEmpNamelApp.SelectedItem.Text.Substring(7)));
            Rpt1.SetParameters(new ReportParameter("txtDesig", this.lblDesignationlApp.Text));
            Rpt1.SetParameters(new ReportParameter("txtDesig1", this.lblDesignationlApp.Text));
            Rpt1.SetParameters(new ReportParameter("txtApplydate", ""));
            Rpt1.SetParameters(new ReportParameter("txtReasons", ""));
            Rpt1.SetParameters(new ReportParameter("txttitlelappslip", "Leave Approval Slip"));
            Rpt1.SetParameters(new ReportParameter("txtAppDays", ""));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Leave Application"));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            #region OLD
            //ReportDocument rptTest = new RealERPRPT.R_81_Hrm.R_84_Lea.EmpLeavApp();
            //TextObject txtRptComName = rptTest.ReportDefinition.ReportObjects["txtRptComName"] as TextObject;
            //txtRptComName.Text = comnam;
            //TextObject txtRptCompAdd = rptTest.ReportDefinition.ReportObjects["txtRptCompAdd"] as TextObject;
            //txtRptCompAdd.Text = comadd;
            //TextObject txtRecordNo = rptTest.ReportDefinition.ReportObjects["txtRecordNo"] as TextObject;
            //txtRecordNo.Text = "";
            //TextObject txtleaveday = rptTest.ReportDefinition.ReportObjects["txtleaveday"] as TextObject;
            //txtleaveday.Text = "";

            //TextObject txtldatefrm = rptTest.ReportDefinition.ReportObjects["txtldatefrm"] as TextObject;
            //txtldatefrm.Text = "";
            //TextObject txtldateto = rptTest.ReportDefinition.ReportObjects["txtldateto"] as TextObject;
            //txtldateto.Text = "";
            //TextObject txtlday = rptTest.ReportDefinition.ReportObjects["txtlday"] as TextObject;
            //txtlday.Text = "";
            //TextObject txtRecordNo1 = rptTest.ReportDefinition.ReportObjects["txtRecordNo1"] as TextObject;
            //txtRecordNo1.Text = "";
            //TextObject txtEmpName = rptTest.ReportDefinition.ReportObjects["txtEmpName"] as TextObject;
            //txtEmpName.Text = this.ddlEmpNamelApp.SelectedItem.Text.Substring(7);
            //TextObject txtEmpName1 = rptTest.ReportDefinition.ReportObjects["txtEmpName1"] as TextObject;
            //txtEmpName1.Text = this.ddlEmpNamelApp.SelectedItem.Text.Substring(7);
            //TextObject txtDesig = rptTest.ReportDefinition.ReportObjects["txtDesig"] as TextObject;
            //txtDesig.Text = this.lblDesignationlApp.Text;
            //TextObject txtDesig1 = rptTest.ReportDefinition.ReportObjects["txtDesig1"] as TextObject;
            //txtDesig1.Text = this.lblDesignationlApp.Text;

            //TextObject txtApplydate = rptTest.ReportDefinition.ReportObjects["txtApplydate"] as TextObject;
            //txtApplydate.Text = "";
            //TextObject rpttxtReasons = rptTest.ReportDefinition.ReportObjects["txtReasons"] as TextObject;
            //rpttxtReasons.Text = "";
            //TextObject txttitlelappslip = rptTest.ReportDefinition.ReportObjects["txttitlelappslip"] as TextObject;
            //txttitlelappslip.Text = "Leave Approval Slip";
            //TextObject txtAppDays = rptTest.ReportDefinition.ReportObjects["txtAppDays"] as TextObject;
            //txtAppDays.Text = "";
            //rptTest.SetDataSource(dt);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptTest.SetParameterValue("ComLogo", ComLogo);

            //Session["Report1"] = rptTest;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
            //                   ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            #endregion
        }
        private void PrintLeaveform2()
        {
            DataView dv = ((DataTable)Session["tblleavest"]).DefaultView;
            dv.RowFilter = ("gcod='51001'");
            DataTable dt = dv.ToTable();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.EmpLeaveAPP>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptHRSetup.GetLocalReport("R_81_Hrm.R_84_Lea.RptHrEmpLeave02", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("txtRecordNo", ""));
            Rpt1.SetParameters(new ReportParameter("txtleavedays", ""));
            Rpt1.SetParameters(new ReportParameter("txtApplydate", ""));
            Rpt1.SetParameters(new ReportParameter("txtlsdate", ""));
            Rpt1.SetParameters(new ReportParameter("txtledate", ""));
            Rpt1.SetParameters(new ReportParameter("txtEmpName", this.ddlEmpNamelApp.SelectedItem.Text.Substring(7)));
            Rpt1.SetParameters(new ReportParameter("txtDesig", this.lblDesignationlApp.Text));
            Rpt1.SetParameters(new ReportParameter("txtReasons", ""));
            Rpt1.SetParameters(new ReportParameter("txtAddofentime", ""));
            Rpt1.SetParameters(new ReportParameter("txtremarks", ""));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Leave Application"));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            #region OLD
            //ReportDocument rptTest = new RealERPRPT.R_81_Hrm.R_84_Lea.RptHrEmpLeave02(); ;
            //TextObject txtRptComName = rptTest.ReportDefinition.ReportObjects["txtRptComName"] as TextObject;
            //txtRptComName.Text = comnam;
            //TextObject txtRecordNo = rptTest.ReportDefinition.ReportObjects["txtRecordNo"] as TextObject;
            //txtRecordNo.Text = "";
            //TextObject txtEmpName = rptTest.ReportDefinition.ReportObjects["txtEmpName"] as TextObject;
            //txtEmpName.Text = this.ddlEmpNamelApp.SelectedItem.Text.Substring(7);

            //TextObject txtDesig = rptTest.ReportDefinition.ReportObjects["txtDesig"] as TextObject;
            //txtDesig.Text = this.lblDesignationlApp.Text;
            //TextObject txtApplydate = rptTest.ReportDefinition.ReportObjects["txtApplydate"] as TextObject;
            //txtApplydate.Text = "";
            //TextObject txtlsdate = rptTest.ReportDefinition.ReportObjects["txtlsdate"] as TextObject;
            //txtlsdate.Text = "";
            //TextObject txtledate = rptTest.ReportDefinition.ReportObjects["txtledate"] as TextObject;
            //txtledate.Text = "";
            //TextObject txtleavedays = rptTest.ReportDefinition.ReportObjects["txtleavedays"] as TextObject;
            //txtleavedays.Text = "";
            //TextObject rpttxtReasons = rptTest.ReportDefinition.ReportObjects["txtReasons"] as TextObject;
            //rpttxtReasons.Text = "";
            //TextObject txtAddofentime = rptTest.ReportDefinition.ReportObjects["txtAddofentime"] as TextObject;
            //txtAddofentime.Text = "";
            //TextObject rpttxtremarks = rptTest.ReportDefinition.ReportObjects["txtremarks"] as TextObject;
            //rpttxtremarks.Text = "";
            //TextObject txtuserinfo = rptTest.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //rptTest.SetDataSource(dt);
            //Session["Report1"] = rptTest;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
            //                   ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            #endregion
        }

        private void PrintLeaveform3()
        {

            DataTable dtcal = ((DataTable)Session["tblleavest"]).Copy();
            DataView dv = dtcal.DefaultView;
            dv.RowFilter = ("gcod='51001'");

            DataTable dt = dv.ToTable();
            DataRow[] dr1 = dt.Select("gcod='51001'");
            dr1[0]["gcod"] = "51008";

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.EmpLeaveAPP>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptHRSetup.GetLocalReport("R_81_Hrm.R_84_Lea.RptHrEmpLeave03", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("txtCompanyName", this.lblComPanylApp.Text));
            Rpt1.SetParameters(new ReportParameter("txtEmpName", this.ddlEmpNamelApp.SelectedItem.Text.Substring(7)));
            Rpt1.SetParameters(new ReportParameter("txtDesig", this.lblDesignationlApp.Text));
            Rpt1.SetParameters(new ReportParameter("txtDepartment", this.lblSectionlApp.Text));
            Rpt1.SetParameters(new ReportParameter("txtJoiningDate", this.lblJoiningDatelApp.Text));
            Rpt1.SetParameters(new ReportParameter("txtaveleave", (Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51001'"))[0]["ltaken"]) + Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51001'"))[0]["appday"])).ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtbeleave", Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51001'"))[0]["balleave"]).ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtavcleave", (Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51002'"))[0]["ltaken"]) + Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51002'"))[0]["appday"])).ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtbcleave", Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51002'"))[0]["balleave"]).ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtavsleave", (Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51003'"))[0]["ltaken"]) + Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51003'"))[0]["appday"])).ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtbsleave", Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51003'"))[0]["balleave"]).ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("appday", Convert.ToString(dt.Rows[0]["appday"])));
            Rpt1.SetParameters(new ReportParameter("appdate", Convert.ToDateTime(dt.Rows[0]["appdate"]).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "LEAVE APPLICATION FORM(Human Resource Department)"));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            #region OLD
            //ReportDocument rptTest = new RealERPRPT.R_81_Hrm.R_84_Lea.RptHrEmpLeave03();
            //TextObject txtRptComName = rptTest.ReportDefinition.ReportObjects["txtRptComName"] as TextObject;
            //txtRptComName.Text = comnam;
            //TextObject txtEmpName = rptTest.ReportDefinition.ReportObjects["txtEmpName"] as TextObject;
            //txtEmpName.Text = this.ddlEmpNamelApp.SelectedItem.Text.Substring(7);
            //TextObject txtCompanyName = rptTest.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            //txtCompanyName.Text = this.lblComPanylApp.Text;
            //TextObject txtDesig = rptTest.ReportDefinition.ReportObjects["txtDesig"] as TextObject;
            //txtDesig.Text = this.lblDesignationlApp.Text;
            //TextObject txtDepartment = rptTest.ReportDefinition.ReportObjects["txtDepartment"] as TextObject;
            //txtDepartment.Text = this.lblSectionlApp.Text;
            //TextObject txtJoiningDate = rptTest.ReportDefinition.ReportObjects["txtJoiningDate"] as TextObject;
            //txtJoiningDate.Text = this.lblJoiningDatelApp.Text;
            //TextObject txtaveleave = rptTest.ReportDefinition.ReportObjects["txtaveleave"] as TextObject;
            //txtaveleave.Text = (Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51001'"))[0]["ltaken"]) + Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51001'"))[0]["appday"])).ToString("#,##0;(#,##0); ");
            //TextObject txtbeleave = rptTest.ReportDefinition.ReportObjects["txtbeleave"] as TextObject;
            //txtbeleave.Text = Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51001'"))[0]["balleave"]).ToString("#,##0;(#,##0); ");
            //TextObject txtavcleave = rptTest.ReportDefinition.ReportObjects["txtavcleave"] as TextObject;
            //txtavcleave.Text = (Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51002'"))[0]["ltaken"]) + Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51002'"))[0]["appday"])).ToString("#,##0;(#,##0); ");
            //TextObject txtbcleave = rptTest.ReportDefinition.ReportObjects["txtbcleave"] as TextObject;
            //txtbcleave.Text = Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51002'"))[0]["balleave"]).ToString("#,##0;(#,##0); ");
            //TextObject txtavsleave = rptTest.ReportDefinition.ReportObjects["txtavsleave"] as TextObject;
            //txtavsleave.Text = (Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51003'"))[0]["ltaken"]) + Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51003'"))[0]["appday"])).ToString("#,##0;(#,##0); ");
            //TextObject txtbsleave = rptTest.ReportDefinition.ReportObjects["txtbsleave"] as TextObject;
            //txtbsleave.Text = Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51003'"))[0]["balleave"]).ToString("#,##0;(#,##0); ");

            //rptTest.SetDataSource(dt);
            //Session["Report1"] = rptTest;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            #endregion
        }
        protected void lnkbtnUpdateLeave_Click(object sender, EventArgs e)
        {
            this.SaveLeave();

            
            DataTable dt = (DataTable)Session["tblleave"];
            string comcod = this.GetComeCode();

            if (this.ddlPreLeave.Items.Count == 0)
                this.GetPreLeaveNo();
           
            string empid = this.ddlEmpName.SelectedValue.ToString();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                double lapplied = Convert.ToDouble(dt.Rows[i]["lapplied"]);
                if (lapplied > 0)
                {
                    string trnid = this.lbltrnleaveid.Text;
                    string gcod = dt.Rows[i]["gcod"].ToString();
                    string frmdate = Convert.ToDateTime(dt.Rows[i]["lenjoydt1"]).ToString("dd-MMM-yyyy");
                    string todate = Convert.ToDateTime(dt.Rows[i]["lenjoydt2"]).ToString("dd-MMM-yyyy");
                    string applydat = Convert.ToDateTime(this.txtaplydate.Text).ToString("dd-MMM-yyyy");
                    string reason = this.txtLeavLreasons.Text.Trim(); ;
                    string addentime = this.txtaddofenjoytime.Text.Trim();
                    string remarks = this.txtLeavRemarks.Text.Trim();
                    string dnameadesig = this.txtdutiesnameandDesig.Text.Trim();

                    string APRdate = Convert.ToDateTime(this.txtApprdate.Text).ToString("dd-MMM-yyyy");
                    bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPEMLEAVAPP", trnid, empid, gcod, frmdate, todate, applydat, reason, remarks, APRdate, addentime, dnameadesig, lapplied.ToString(), "", "", "");
                    this.GetPreLeaveNo();
                }
            }

            this.EmpLeaveInfo();
           string Message = "Updated Successfully";            
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Message + "');", true);


        }


        protected void lbtnDelete_Click(object sender, EventArgs e)
        {


            string comcod = this.GetComeCode();
            string trnid = this.lbltrnleaveid.Text;
            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "DELETEEMLEAVAPP", trnid, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted failed');", true);
                return;
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Sucessfully Deleted');", true);



        }

        protected void imgbtnCompany_Click(object sender, EventArgs e)
        {
            this.GetCompany();
        }
        protected void imgbtnProSrch_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "LeaveRule":
                    this.SaveValue();
                    this.LoadGrid();
                    break;

                case "LeaveApp":
                    break;

                case "FLeaveApp":
                    break;
            }
        }


        protected void imgbtnlAppEmpSeaarch_Click(object sender, EventArgs e)
        {

            this.GetEmployeeName();

        }
        protected void imgbtnlFEmpSeaarch_Click(object sender, EventArgs e)
        {
            this.GetLveAppEmployeeName();
        }


        protected void gvleaveInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label description = (Label)e.Row.FindControl("lgvledescription");
                Label lgvleavedays = (Label)e.Row.FindControl("lgvleavedays");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grpsl")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "BBBB" || ASTUtility.Right(code, 4) == "CCCC")
                {
                    description.Font.Bold = true;
                    lgvleavedays.Font.Bold = true;
                    description.Style.Add("text-align", "right");


                }

            }
        }
        protected void gvleaveInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string comcod = this.GetComeCode();
            DataTable dt = (DataTable)ViewState["tblempleaveinfo"];
            string trnid = ((Label)this.gvleaveInfo.Rows[e.RowIndex].FindControl("lgvltrnleaveid")).Text.Trim();

            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "DELETEEMLEAVAPP", trnid, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted failed');", true);
                return;
            }
            int rowindex = (this.gvleaveInfo.PageSize) * (this.gvleaveInfo.PageIndex) + e.RowIndex;
            dt.Rows[rowindex].Delete();
            DataView dv = dt.DefaultView;
            ViewState.Remove("tblempleaveinfo");
            ViewState["tblempleaveinfo"] = dv.ToTable();
            this.gvleaveInfo.DataSource = dv.ToTable();
            this.gvleaveInfo.DataBind();
        }

        protected void lnkRule_Click(object sender, EventArgs e)
        {
            //Response.Redirect("~/F_81_Hrm/F_84_Lea/CreateLeavRule?Type=");

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadCreateRuleModal();", true);
            return;

        }

        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetEmployeeName();

        }

        protected void ddlyear_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowInformation();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "CloseModal_AlrtMsg();", true);
        }

        public void Create_table()
        {
            DataTable dt = new DataTable();

            //create colums here.
            dt.Columns.Add("comcod", Type.GetType("System.String"));
            dt.Columns.Add("gcod", Type.GetType("System.String"));
            dt.Columns.Add("year", Type.GetType("System.String"));
            dt.Columns.Add("leave", Type.GetType("System.String"));
            ViewState["tblleavinfoCT"] = dt;

        }
        private void ShowInformation()
        {
            Session.Remove("tblleavinfo");
            string comcod = this.GetComeCode();
            string tempddl1 = "51"; //Leave code 
            string tempddl2 = "5"; // Details 
            string year = this.ddlyear.SelectedValue.ToString();
            DataSet ds1 = this.HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_CODEBOOK", "GETLEAVEINFORMATION", tempddl1,
                            tempddl2, year, "", "", "", "", "", "");

            //Session["storedata"] = ds1.Tables[0];

            DataView view = new DataView();
            view.Table = ds1.Tables[0];
            view.RowFilter = "hrgcod <> '51000'";

            Session["tblleavinfo"] = view.ToTable();

            this.LeaveRule_Data_Bind();
        }

        private void LeaveRule_Data_Bind()
        {
            DataTable tbl1 = (DataTable)Session["tblleavinfo"];


            this.grvacc.DataSource = tbl1;
            this.grvacc.DataBind();
        }

        protected void lnkUpdateLeaveRule_Click(object sender, EventArgs e)
        {
            this.GetLeaveData();
            string comcod = this.GetComeCode();
            string year = this.ddlyear.SelectedValue.ToString();
            DataTable dt = (DataTable)ViewState["tblleavinfoCT"];
            if (dt == null)
            {
                return;
            }
            foreach (DataRow dr in dt.Rows)
            {

                string code = dr["gcod"].ToString();
                string leav = dr["leave"].ToString();

                bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_CODEBOOK", "INSERTCOMPLEAVEINFO", year, code, leav);
                if (result == false)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                    return;
                }
                else
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                }
            }

            ShowLeaveRule();
        }

        private void GetLeaveData()
        {

            DataTable dt = (DataTable)ViewState["tblleavinfoCT"];

            //var descdata = Server.HtmlEncode();
            for (int i = 0; i < this.grvacc.Rows.Count; i++)
            {

                Label code = this.grvacc.Rows[i].FindControl("lbgrcod1") as Label;
                TextBox TxtLeav = this.grvacc.Rows[i].FindControl("TxtLeav") as TextBox;

                string lblspcode = code.Text;
                string leave = TxtLeav.Text;

                DataRow dr1 = dt.NewRow();

                dr1["gcod"] = lblspcode;
                dr1["leave"] = leave;
                dt.Rows.Add(dr1);
            }


            ViewState["tblleavinfoCT"] = dt;
        }

        protected void lnkReset_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            string yearid = this.ddlyear.SelectedValue.ToString();
            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "DELETELEAVERULES", yearid, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Rules Applied failed! Try Again');", true);
                return;
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Rules Applied Sucessfully');", true);   
            lbtnOk_Click(null,null);
            lnkUpdateLeaveRule_Click(null, null);
        }
    }
}
