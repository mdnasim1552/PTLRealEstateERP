using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.IO;
using RealERPLIB;
using System.Data.OleDb;
using System.Data;
using Microsoft.Reporting.WinForms;
namespace RealERPWEB.F_21_MKT
{
    public partial class AdvancedSearchFilter : System.Web.UI.Page
    {
        ProcessAccess instcrm = new ProcessAccess();
        Common compUtility = new Common();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                ((Label)this.Master.FindControl("lblTitle")).Text = "Advanced Search Filter";
                this.GETEMPLOYEEUNDERSUPERVISED();
                this.GetFollow();
                this.GetParcipants();
                this.GetAllSubdata();
                this.GetVisitoraStatinfo();
                this.GetProjectAUnit();
              
                this.IsTeamLeader();
               
            }
        }
        public string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
       
        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {
            try{
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string userrole = hst["userrole"].ToString();
                string Empid = ((hst["empid"].ToString() == "") ? "%" : hst["empid"].ToString());
                if (userrole == "1")
                {
                    Empid = "%";
                }
                string comcod = this.GetComeCode();
                string Country = "%";
                string Dist = "%";
                string Zone = "%";
                string PStat = "%";
                string Area = "%";
                string Block = "%";
                string Pri = "%";
                string Status = "%";
                string Other = this.ddlOther.SelectedValue.ToString();
                string TxtVal = "%" + this.txtVal.Text;
                string frmdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
                string todate = System.DateTime.Today.ToString("dd-MMM-yyyy");

                string srchempid = ((this.ddlEmpid.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlEmpid.SelectedValue.ToString());

                DataSet ds3 = instcrm.GetTransInfoNew(comcod, "SP_REPORT_CRM_MODULE", "GET_PROSPECT_DETAILS", null, null, null, "8301%", Empid, Country, Dist, Zone, PStat, Block, Area,
                     Pri, Status, Other, TxtVal, todate, srchempid);



                if (ds3 == null || ds3.Tables[0].Rows.Count == 0)
                {
                    string Messagesd = "No data found";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);
                    this.DataBind();

                }
                else
                {
                    ViewState["tblempsup"] = ds3.Tables[0];
                    DataTable dt1 = (DataTable)ViewState["tblempsup"];
                    string cdate = todate;
                    string proscod = dt1.Rows[0]["sircode"].ToString();
                    DataSet ds1 = instcrm.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "SHOWPROSPECTIVEDISCUSSION", proscod, cdate, "", "", "", "");
                    DataTable dt2 = (DataTable)ds1.Tables[1];

                    this.lblname.Text = dt1.Rows[0]["sircode"].ToString();
                    this.lblconper.Text = dt1.Rows[0]["sirdesc"].ToString();
                    this.lblmbl.Text = dt1.Rows[0]["phone"].ToString();
                    this.lblhomead.Text = dt1.Rows[0]["caddress"].ToString();
                    this.lblprof.Text = dt2.Rows[0]["profession"].ToString();
                    this.lblstatus.Text = dt1.Rows[0]["virnotes"].ToString();

                    this.lblgeneratedate.Value = ds1.Tables[1].Rows.Count == 0 ? "01-Jan-1900" : Convert.ToDateTime(ds1.Tables[1].Rows[0]["createdate"]).ToString("dd-MMM-yyyy");
                    this.hiddenLedStatus.Value = (ds1.Tables[0].Rows.Count == 0 ? "" : ds1.Tables[0].Rows[0]["lastlstcode"].ToString());


                    this.rpclientinfo.DataSource = ds1.Tables[0];
                    this.rpclientinfo.DataBind();
                    
                }
                 
               
            }
            catch (Exception ex)
            {

                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
           

        }
        


        private void GETEMPLOYEEUNDERSUPERVISED()
        {
            try
            {
                string comcod = GetComeCode();
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string Empid = hst["empid"].ToString();
                DataSet ds1 = instcrm.GetTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "GETEMPLOYEEUNDERSUPERVISED", Empid, "", "", "", "", "", "", "", "");
                ViewState["tblempsup"] = ds1.Tables[0];

                DataSet ds2 = instcrm.GetTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "CLNTREFINFODDL", "", "", "", "", "", "", "", "", "");
                ViewState["tblsubddl"] = ds2.Tables[0];
                DataTable dt1 = (DataTable)ViewState["tblsubddl"];
                DataTable dtemp = (DataTable)ViewState["tblempsup"];
                DataView dv;
                dv = dt1.Copy().DefaultView;
                string ddlempid = this.ddlEmpid.SelectedValue.ToString();

                string userrole = hst["userrole"].ToString();
                string lempid = hst["empid"].ToString();
                //string empid = (userrole == "1" ? "93" : lempid) + "%";

                DataTable dtE = new DataTable();
                dv.RowFilter = ("gcod like '93%'");
                if (userrole == "1")
                {
                   
                    dtE = dv.ToTable();
                    dtE.Rows.Add("000000000000", "Choose Employee..", "");

                }

                else
                {
                    DataTable dts = dv.ToTable();
                    var query = (from dtl1 in dts.AsEnumerable()
                                 join dtl2 in dtemp.AsEnumerable() on dtl1.Field<string>("gcod") equals dtl2.Field<string>("empid")
                                 select new
                                 {
                                     gcod = dtl1.Field<string>("gcod"),
                                     gdesc = dtl1.Field<string>("gdesc"),
                                     code = dtl1.Field<string>("code")
                                 }).ToList();
                    dtE = ASITUtility03.ListToDataTable(query);
                    if (dtE.Rows.Count >= 2)
                        dtE.Rows.Add("000000000000", "Choose Employee..", "");
                    // if(dtE.Rows.Count>1)
                    //dtE.Rows.Add("000000000000", "Choose Employee..", "");
                }
                ddlEmpid.ClearSelection();
                this.ddlEmpid.DataTextField = "gdesc";
                this.ddlEmpid.DataValueField = "gcod";
                this.ddlEmpid.DataSource = dtE;
                this.ddlEmpid.DataBind();

            }
            catch (Exception ex)
            {

            }
           

        }
        protected void btnqclink_Click(object sender, EventArgs e)
        {
            try
            {


                this.pnlSidebar.Visible = true; 
                this.pnlfollowup.Visible = false;
                this.pnlempinfo.Visible = false;
                ShowDiscussion();




            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }
        protected void pnlsidebarClose_Click(object sender, EventArgs e)
        {
            this.pnlSidebar.Visible = false;
            this.pnlfollowup.Visible = true;
            this.pnlempinfo.Visible = true;
        }
        private void ShowDiscussion()
        {
            string comcod = this.GetComeCode();
            DataTable tbl1 = (DataTable)ViewState["tbModalData"];
            string YmonID = Convert.ToDateTime(System.DateTime.Now).ToString("yyyyMM");
            string Empid = this.ddlEmpid.SelectedValue.ToString();
            //string grpcode = this.lblgrp.Text;
            string todate = System.DateTime.Today.ToString("dd-MMM-yyyy");

            DataTable dt1 = (DataTable)ViewState["tblempsup"];
           
            string Client = dt1.Rows[0]["sircode"].ToString();
           
            string kpigrp = "000000000000";// this.rbtnlist.SelectedValue.ToString();
            string wrkdpt = "000000000000";
            DateTime time = System.DateTime.Now;
            string qcdate = this.Request.QueryString["followupdate"] ?? "";
            string cdate = todate;
           // string cdate = qcdate.Length == 0 ? System.DateTime.Now + " " + time.ToString("HH:mm") : qcdate;


            DataSet ds1 = instcrm.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "DAILYDISCUIND", Empid, Client, kpigrp, "", wrkdpt, cdate);


            // DataSet ds1 = KpiData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "DAILYDISCUIND", Empid, Client, kpigrp, "", wrkdpt, cdate);
            ViewState["tbModalData"] = HiddenSameData(ds1.Tables[0]);
            Modal_Data_Bind();



        }
        bool IsTeamLeader()
        {
            string comcod = GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userrole = hst["userrole"].ToString();
            string empid = hst["empid"].ToString();
            DataSet ds1 = instcrm.GetTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "GETEMPLOYEEUNDERSUPERVISED", empid, "", "", "", "", "", "", "", "");
            DataTable dt = ds1.Tables[1];
            if (dt.Rows.Count > 0 || userrole == "1")
                return true;
            else
                return false;
        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            int j;
            string gp = dt1.Rows[0]["gp"].ToString();
            for (j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["gp"].ToString() == gp)
                {
                    gp = dt1.Rows[j]["gp"].ToString();
                    dt1.Rows[j]["gpdesc"] = "";
                }

                else
                    gp = dt1.Rows[j]["gp"].ToString();
            }


            return dt1;

        }
        private void GetProjectAUnit()
        {
            ViewState.Remove("tblproaunit");
            string comcod = this.GetComeCode();
            DataSet dss = this.instcrm.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "GETPROJECTAUNIT", "", "", "", "", "", "", "", "", "");
            ViewState["tblproaunit"] = dss;
            dss.Dispose();
        }
        private void GetVisitoraStatinfo()
        {
            ViewState.Remove("tblvisitor");
            string comcod = this.GetComeCode();
            DataSet dt11 = this.instcrm.GetTransInfo(comcod, "dbo_kpi.SP_REPORT_EMP_DASEBOARD", "GETVISITOR", "", "", "", "", "", "", "", "", "");
            DataTable dt = dt11.Tables[0];
            ViewState["tblvisiastator"] = dt;

        }
       

        private void GetFollow()
        {
            ViewState.Remove("tblFollow");
            string comcod = this.GetComeCode();
            DataSet dt11 = this.instcrm.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "FOLLOWUPCODE", "", "", "", "", "", "", "", "", "");
            DataTable dt = dt11.Tables[0];
            ViewState["tblFollow"] = dt;

        }


        private void GetParcipants()
        {
            ViewState.Remove("tblparti");
            string comcod = this.GetComeCode();
            DataSet ds1 = this.instcrm.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "PARTICIPANTS", "", "", "", "", "", "", "", "", "");
            DataTable dt11 = ds1.Tables[0];
            ViewState["tblparti"] = dt11;

        }
        private void GetAllSubdata()
        {
            string comcod = GetComeCode();
            DataSet ds2 = instcrm.GetTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "CLNTREFINFODDL", "", "", "", "", "", "", "", "", "");
            ViewState["tblsubddl"] = ds2.Tables[0];
            ViewState["tblstatus"] = ds2.Tables[1];
            ViewState["tblproject"] = ds2.Tables[2];
            ViewState["tblcompany"] = ds2.Tables[3];
            ds2.Dispose();
        }
        private void Modal_Data_Bind()
        {



            //try

            //{

            DataTable dtg = ((DataTable)ViewState["tblsubddl"]).Copy();

            DataTable dt = (DataTable)ViewState["tbModalData"];
            this.gvInfo.DataSource = dt;
            this.gvInfo.DataBind();



            if ((DataSet)ViewState["tblproaunit"] == null)
            {

                this.GetProjectAUnit();
            }
            DataSet ds1 = (DataSet)ViewState["tblproaunit"];
            //GetIntlocation();
            //DataTable dt5 = (DataTable)ViewState["tbllocation"];


            //    GetFollow();
            DataTable dt5 = ((DataTable)ViewState["tblFollow"]).Copy();
            DataView dv1;
            dv1 = dt5.DefaultView;
            dv1.RowFilter = ("gcod like '96%'");



            DataTable dt6 = (DataTable)ViewState["tblparti"];

            DataView dv;
            DataTable dtvs = ((DataTable)ViewState["tblFollow"]).Copy();
            dv = dtvs.DefaultView;
            dv.RowFilter = ("gcod like '95%'");
            DataTable dts = dv.ToTable();









            // Visitor
            GetVisitoraStatinfo();
            DataTable dtv = (DataTable)ViewState["tblvisiastator"];
            DataTable dtprj = ((DataTable)ViewState["tblproject"]).Copy();



            //DataView dv1;


            //DataView dv1;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string empid = (hst["empid"].ToString() == "" ? "93" : hst["empid"].ToString()) + "%";
            string pcomcod = "";
            DropDownList ddlcomp, ddlgval, ddlUnit, ddlVisitor, ddlgval1, ddlgval2, ddlgval3;
            ListBox ddlPartic;
            DataRow dr1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string Gcode = dt.Rows[i]["gcod"].ToString();

                switch (Gcode)
                {




                    case "810100101001": //Meeting Date


                        ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Visible = false;
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlCompany")).Items.Clear();
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlCompany")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("PnlProject")).Visible = false;
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlProject")).Items.Clear();
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlProject")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("PnlUnit")).Visible = false;
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlUnit")).Items.Clear();
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlUnit")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("pnlTime")).Visible = true;




                        ((Label)this.gvInfo.Rows[i].FindControl("lblschedulenumber")).Visible = false;

                        ((DropDownList)this.gvInfo.Rows[i].FindControl("checkboxReson")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("pnlLostResion")).Visible = false;


                        DateTime datetime = System.DateTime.Now;

                        string gTime = ((Label)this.gvInfo.Rows[i].FindControl("lblgvTime")).Text.Trim();

                        ddlgval1 = ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlhour"));
                        ddlgval1.SelectedValue = (gTime.Length == 0) ? datetime.ToString("hh") : ASTUtility.Left(gTime, 2);
                        ddlgval2 = ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlMmin"));
                        ddlgval2.SelectedValue = (gTime.Length == 0) ? datetime.ToString("mm") : gTime.Substring(3, 2);
                        ddlgval3 = ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlslb"));
                        ddlgval3.SelectedValue = (gTime.Length == 0) ? datetime.ToString("tt") : ASTUtility.Right(gTime, 2);


                        if (this.lblgeneratedate.Value.Length > 0)
                        {
                            string comcod = this.GetComeCode();
                            AjaxControlToolkit.CalendarExtender CalendarExtendere21 = (AjaxControlToolkit.CalendarExtender)gvInfo.Rows[i].FindControl("txtgvdValdis_CalendarExtender");

                            DataSet copSetup = compUtility.GetCompUtility();
                            bool bakdatain = copSetup.Tables[0].Rows.Count == 0 ? false : Convert.ToBoolean(copSetup.Tables[0].Rows[0]["crm_backdatain"]);
                            if (bakdatain == false)// its backdate data inserted true/flase if based on prospoect generated date
                            {
                                CalendarExtendere21.StartDate = Convert.ToDateTime(this.lblgeneratedate.Value);
                            }
                            switch (comcod) // its backdate data inserted true/flase if based on cuurent date requirment by pulok assure dev by nahid
                            {
                                case "3101":
                                case "3315":
                                case "3316":
                                    DateTime tomorrow = DateTime.Now.AddDays(-2);

                                    CalendarExtendere21.StartDate = Convert.ToDateTime(tomorrow);

                                    break;
                            }
                        }




                        break;



                    case "810100101002": // Today's Followup
                    case "810100101019"://Next Followup


                        ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdValdis")).Visible = false;
                        ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("pnlParic")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("pnlFollow")).Visible = true;
                        ((Label)this.gvInfo.Rows[i].FindControl("lblgvTime")).Visible = false;

                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlCompany")).Items.Clear();
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlCompany")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("PnlProject")).Visible = false;
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlProject")).Items.Clear();
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlProject")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("PnlUnit")).Visible = false;
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlUnit")).Items.Clear();
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlUnit")).Visible = false;
                        ((Label)this.gvInfo.Rows[i].FindControl("lblschedulenumber")).Visible = false;



                        ((DropDownList)this.gvInfo.Rows[i].FindControl("checkboxReson")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("pnlLostResion")).Visible = false;


                        //((DropDownList)this.gvInfo.Rows[i].FindControl("ChkBoxLstFollow")).Visible = true;                        
                        //ChkBoxLstFollow = ((DropDownList)this.gvInfo.Rows[i].FindControl("ChkBoxLstFollow"));
                        //ChkBoxLstFollow.DataTextField = "gdesc";
                        //ChkBoxLstFollow.DataValueField = "gcod";
                        //ChkBoxLstFollow.DataSource = dt5;
                        //ChkBoxLstFollow.DataBind();
                        //ChkBoxLstFollow.SelectedValue = ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();


                        ((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstFollow")).Visible = true;
                        CheckBoxList ChkBoxLstFollow = ((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstFollow"));
                        ChkBoxLstFollow.DataTextField = "gdesc";
                        ChkBoxLstFollow.DataValueField = "gcod";
                        ChkBoxLstFollow.DataSource = dv1.ToTable();
                        ChkBoxLstFollow.DataBind();
                        //  ChkBoxLstFollow.SelectedValue = ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Text.Trim();

                        break;







                    case "810100101007": //Company
                        ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdValdis")).Visible = false;
                        ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("PnlProject")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("PnlUnit")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("pnlStatus")).Visible = false;
                        ((Label)this.gvInfo.Rows[i].FindControl("lblschedulenumber")).Visible = false;
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("checkboxReson")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("pnlLostResion")).Visible = false;
                        ddlcomp = ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlCompany"));
                        ddlcomp.DataTextField = "comdesc";
                        ddlcomp.DataValueField = "comcod";
                        ddlcomp.DataSource = (DataTable)ViewState["tblcompany"];// ds1.Tables[0];
                        ddlcomp.DataBind();
                        //ddlcomp.SelectedValue = ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Text.Trim();
                        ddlcomp.SelectedValue = this.GetComeCode();
                        pcomcod = ddlcomp.SelectedValue.ToString();
                        break;




                    case "810100101003": //Pactcode

                        pcomcod = pcomcod.Trim().Length == 0 ? this.GetComeCode() : pcomcod;
                        dv = dtprj.DefaultView;
                        dv.RowFilter = ("comcod='" + pcomcod + "'");

                        ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdValdis")).Visible = false;
                        ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Visible = false;
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlCompany")).Items.Clear();
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlCompany")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("PnlUnit")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("pnlStatus")).Visible = false;
                        ((Label)this.gvInfo.Rows[i].FindControl("lblschedulenumber")).Visible = false;


                        ((DropDownList)this.gvInfo.Rows[i].FindControl("checkboxReson")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("pnlLostResion")).Visible = false;
                        ddlgval = ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlProject"));
                        ddlgval.DataTextField = "pactdesc";
                        ddlgval.DataValueField = "pactcode";
                        ddlgval.DataSource = dv.ToTable();// ds1.Tables[0];
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Text.Trim();
                        break;


                    case "810100101004": //Unit
                        ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdValdis")).Visible = false;
                        ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Visible = false;
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlCompany")).Items.Clear();
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlCompany")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("PnlProject")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("pnlStatus")).Visible = false;
                        ((Label)this.gvInfo.Rows[i].FindControl("lblschedulenumber")).Visible = false;


                        ((DropDownList)this.gvInfo.Rows[i].FindControl("checkboxReson")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("pnlLostResion")).Visible = false;

                        string pactcode = ((DropDownList)this.gvInfo.Rows[i - 1].FindControl("ddlProject")).Text.Trim();

                        DataTable dt1 = ds1.Tables[1].Copy();
                        DataView dv2;
                        dv2 = dt1.DefaultView;
                        dv2.RowFilter = ("pactcode='000000000000' or pactcode='" + pactcode + "'");
                        dt1 = dv2.ToTable();

                        ddlUnit = ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlUnit"));
                        ddlUnit.DataTextField = "udesc";
                        ddlUnit.DataValueField = "usircode";
                        ddlUnit.DataSource = dt1;//dv1.ToTable();
                        ddlUnit.DataBind();
                        //ddlUnit.SelectedValue = usircode;
                        ddlUnit.SelectedValue = ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Text.Trim();
                        break;



                    case "810100101012": //Lead Reasion
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlCompany")).Items.Clear();
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlCompany")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("PnlProject")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("PnlUnit")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("pnlStatus")).Visible = true;
                        ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdValdis")).Visible = false;
                        ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("pnlParic")).Visible = false;
                        ((Label)this.gvInfo.Rows[i].FindControl("lblgvTime")).Visible = false;
                        ((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstStatus")).Visible = false;
                        ((Label)this.gvInfo.Rows[i].FindControl("lblschedulenumber")).Visible = false;

                        //add nahid
                        //DataView dvLostReasion;
                        //DataTable dtrvs = ((DataTable)ViewState["tblFollow"]).Copy();
                        dv = dtg.DefaultView;
                        dv.RowFilter = ("gcod like '45%'");

                        //dvLostReasion = dtrvs.DefaultView;
                        //dvLostReasion.RowFilter = ("gcod like '45%'");


                        //add nahid

                        ((DropDownList)this.gvInfo.Rows[i].FindControl("checkboxReson")).Visible = true;
                        ((Panel)this.gvInfo.Rows[i].FindControl("pnlLostResion")).Visible = true;
                        DropDownList checkboxReson = ((DropDownList)this.gvInfo.Rows[i].FindControl("checkboxReson"));
                        checkboxReson.DataTextField = "gdesc";
                        checkboxReson.DataValueField = "gcod";
                        checkboxReson.DataSource = dv.ToTable();
                        checkboxReson.DataBind();

                        ListItem li = new ListItem();
                        li.Text = "None";
                        li.Value = "";
                        checkboxReson.Items.Add(li);
                        checkboxReson.SelectedValue = "";

                        //checkboxReson.SelectedItem = "--None--";


                        //checkboxReson.SelectedValue = ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Text.Trim();
                        //




                        break;


                    case "810100101014": //Lead Quality

                        dv = dtg.DefaultView;
                        dv.RowFilter = ("gcod like '42%'");
                        DataTable dtlq = dv.ToTable();


                        ((Panel)this.gvInfo.Rows[i].FindControl("pnlVisit")).Visible = true;
                        ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdValdis")).Visible = false;
                        ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Visible = false;
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlCompany")).Items.Clear();
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlCompany")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("PnlProject")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("PnlUnit")).Visible = false;
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlVisit")).Visible = true;
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("checkboxReson")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("pnlLostResion")).Visible = false;
                        ddlVisitor = ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlVisit"));
                        ((Label)this.gvInfo.Rows[i].FindControl("lblschedulenumber")).Visible = false;


                        dtlq.Rows.Add();
                        ddlVisitor.DataTextField = "gdesc";
                        ddlVisitor.DataValueField = "gcod";
                        ddlVisitor.DataSource = dtlq;
                        ddlVisitor.DataBind();
                        ddlVisitor.Items.Insert(0, new ListItem("None", ""));
                        ddlVisitor.SelectedValue = ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Text.Trim();
                        break;




                    case "810100101016": //Status
                        string lstleadstatus = this.hiddenLedStatus.Value.ToString();

                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlCompany")).Items.Clear();
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlCompany")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("PnlProject")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("PnlUnit")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("pnlStatus")).Visible = true;
                        ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdValdis")).Visible = false;
                        ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("pnlParic")).Visible = false;
                        ((Label)this.gvInfo.Rows[i].FindControl("lblgvTime")).Visible = false;
                        ((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstStatus")).Visible = true;
                        ((Label)this.gvInfo.Rows[i].FindControl("lblschedulenumber")).Visible = false;

                        ((DropDownList)this.gvInfo.Rows[i].FindControl("checkboxReson")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("pnlLostResion")).Visible = false;

                        CheckBoxList ChkBoxLstStatus = ((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstStatus"));
                        ChkBoxLstStatus.DataTextField = "gdesc";
                        ChkBoxLstStatus.DataValueField = "gcod";
                        ChkBoxLstStatus.DataSource = dts;
                        ChkBoxLstStatus.DataBind();
                        ChkBoxLstStatus.SelectedValue = (lstleadstatus.Length > 0 ? lstleadstatus : ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Text.Trim());


                        break;


                    case "810100101017": //Visit
                        dv = dtv.DefaultView;
                        dv.RowFilter = ("gcod like '92%'");

                        ((Panel)this.gvInfo.Rows[i].FindControl("pnlVisit")).Visible = true;
                        ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdValdis")).Visible = false;
                        ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Visible = false;
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlCompany")).Items.Clear();
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlCompany")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("PnlProject")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("PnlUnit")).Visible = false;
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlVisit")).Visible = true;
                        ((Label)this.gvInfo.Rows[i].FindControl("lblschedulenumber")).Visible = false;

                        ((DropDownList)this.gvInfo.Rows[i].FindControl("checkboxReson")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("pnlLostResion")).Visible = false;


                        ddlVisitor = ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlVisit"));
                        ddlVisitor.DataTextField = "gdesc";
                        ddlVisitor.DataValueField = "gcod";
                        ddlVisitor.DataSource = dv.ToTable();
                        ddlVisitor.DataBind();
                        ddlVisitor.SelectedValue = ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Text.Trim();
                        break;


                    case "810100101018": //PARTICIPANTS  

                        ((Panel)this.gvInfo.Rows[i].FindControl("pnlFollow")).Visible = false;
                        ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdValdis")).Visible = false;
                        ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("pnlParic")).Visible = true;
                        ((ListBox)this.gvInfo.Rows[i].FindControl("ddlPartic")).Visible = true;
                        ((Label)this.gvInfo.Rows[i].FindControl("lblgvTime")).Visible = false;


                        ((DropDownList)this.gvInfo.Rows[i].FindControl("checkboxReson")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("pnlLostResion")).Visible = false;

                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlCompany")).Items.Clear();
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlCompany")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("PnlProject")).Visible = false;
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlProject")).Items.Clear();
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlProject")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("PnlUnit")).Visible = false;
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlUnit")).Items.Clear();
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlUnit")).Visible = false;
                        ((Label)this.gvInfo.Rows[i].FindControl("lblschedulenumber")).Visible = false;


                        ddlPartic = ((ListBox)this.gvInfo.Rows[i].FindControl("ddlPartic"));
                        ddlPartic.DataTextField = "gdesc";
                        ddlPartic.DataValueField = "gcod";
                        ddlPartic.DataSource = dt6;
                        ddlPartic.DataBind();
                        if (((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Text.Trim() != "")
                        {
                            ddlPartic.SelectedValue = ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Text.Trim();
                        }


                        int count = Convert.ToInt32(dt.Rows[i]["gdesc1"].ToString().Count());
                        if (count == 0)
                        {


                            if (empid.Length == 13)
                            {

                                empid = empid.Replace("%", "");
                                ddlPartic.SelectedValue = empid;


                            }
                            //string empid1 = empid.Substring(0, 12);
                            //int index = 0;
                            //DataRow[] rows = dt6.Select("gcod='" + empid1 + "'");

                            //if (rows.Length > 0)
                            //{
                            //    index = Convert.ToInt32(dt6.Rows.IndexOf(rows[0]));
                            //}
                            //ddlPartic.SelectedIndex = index;
                        }

                        int j;
                        int k = 0;
                        string data = "";
                        for (j = 0; j < count / 12; j++)
                        {
                            data = dt.Rows[i]["gdesc1"].ToString().Substring(k, 12);
                            foreach (ListItem item in ddlPartic.Items)
                            {
                                if (item.Value == data)
                                {
                                    item.Selected = true;
                                }

                            }
                            k = k + 12;
                        }


                        break;









                    case "810100101015": //Summary
                    case "810100101025": //Discussion
                        ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdValdis")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("PnlUnit")).Visible = false;
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlCompany")).Items.Clear();
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlCompany")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("PnlProject")).Visible = false;
                        ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).TextMode = TextBoxMode.MultiLine;
                        ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Rows = 3;
                        ((Panel)this.gvInfo.Rows[i].FindControl("pnlStatus")).Visible = false;
                        ((Label)this.gvInfo.Rows[i].FindControl("lblschedulenumber")).Visible = false;


                        ((DropDownList)this.gvInfo.Rows[i].FindControl("checkboxReson")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("pnlLostResion")).Visible = false;


                        TextBox sd = ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis"));
                        sd.Style.Add("background", "#DFF0D8");
                        sd.Style.Add("width", "100%");


                        //((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Height=100;
                        break;

                    case "810100101020": //Next Followup          


                        ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Visible = false;
                        // ((Panel)this.gvInfo.Rows[i].FindControl("Pnlcompany")).Visible = false;
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlCompany")).Items.Clear();
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlCompany")).Visible = false;

                        ((Panel)this.gvInfo.Rows[i].FindControl("PnlProject")).Visible = false;
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlProject")).Items.Clear();
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlProject")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("PnlUnit")).Visible = false;
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlUnit")).Items.Clear();
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlUnit")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("pnlTime")).Visible = true;
                        ((Label)this.gvInfo.Rows[i].FindControl("lblschedulenumber")).Visible = true;


                        ((DropDownList)this.gvInfo.Rows[i].FindControl("checkboxReson")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("pnlLostResion")).Visible = false;


                        string gTime20 = ((Label)this.gvInfo.Rows[i].FindControl("lblgvTime")).Text.Trim();

                        ddlgval1 = ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlhour"));
                        ddlgval1.SelectedValue = (gTime20.Length == 0) ? "" : ASTUtility.Left(gTime20, 2);
                        ddlgval2 = ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlMmin"));
                        ddlgval2.SelectedValue = (gTime20.Length == 0) ? "" : gTime20.Substring(3, 2);
                        ddlgval3 = ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlslb"));
                        ddlgval3.SelectedValue = (gTime20.Length == 0) ? "" : ASTUtility.Right(gTime20, 2);


                        //((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Height=100;
                        break;

                    default:
                        ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdValdis")).Visible = false;
                        //((Panel)this.gvInfo.Rows[i].FindControl("Pnlcompany")).Visible = false;
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlCompany")).Items.Clear();
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlCompany")).Visible = false;

                        ((Panel)this.gvInfo.Rows[i].FindControl("PnlProject")).Visible = false;
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlProject")).Items.Clear();
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlProject")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("PnlUnit")).Visible = false;
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlUnit")).Items.Clear();
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlUnit")).Visible = false;
                        ((Label)this.gvInfo.Rows[i].FindControl("lblschedulenumber")).Visible = false;


                        ((DropDownList)this.gvInfo.Rows[i].FindControl("checkboxReson")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("pnlLostResion")).Visible = false;


                        break;





                }

            }

        }

        protected void lbtnUpdateDiscussion_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            //    string comcod = this.GetComeCode();
            //    Hashtable hst = (Hashtable)Session["tblLogin"];

            //    string hempid = this.lbleditempid.Value;
            //    string empid = hst["empid"].ToString();

            //    DataTable dt = ((DataTable)ViewState["tblempsup"]).Copy();

            //    var query = (from dtl1 in dt.AsEnumerable()
            //                 where (dtl1.Field<string>("empid") == hempid) || (dtl1.Field<string>("empid") == empid)
            //                 select dtl1);

            //    DataTable dtE = query.AsDataView().ToTable();
            //    if (dtE.Rows.Count == 0)
            //    {
            //        ((Label)this.Master.FindControl("lblmsg")).Text = "This prospect is not your under";
            //        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            //        return;
            //    }


            //    //if (hempid != empid)
            //    //{

            //    //    ((Label)this.Master.FindControl("lblmsg")).Text = "This prospect is not your under";
            //    //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            //    //    return;

            //    //} 


            //    if (empid.Length == 0)
            //    {

            //        ((Label)this.Master.FindControl("lblmsg")).Text = "Employee is not exixted";
            //        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            //        return;

            //    }
            //    string Client = this.lblproscod.Value.ToString();
            //    string kpigrp = "000000000000";
            //    string wrkdpt = "000000000000";
            //    DateTime time = System.DateTime.Now;

            //    //string cdate = this.txtFrom.Text.ToString() +" "+ time.ToString("HH:mm");

            //    string cdate = Convert.ToDateTime((((TextBox)this.gvInfo.Rows[0].FindControl("txtgvdValdis")).Text.Trim() + " " + ((DropDownList)this.gvInfo.Rows[0].FindControl("ddlhour")).SelectedValue.ToString()
            //                + ":" + ((DropDownList)this.gvInfo.Rows[0].FindControl("ddlMmin")).SelectedValue.ToString() + " " + ((DropDownList)this.gvInfo.Rows[0].FindControl("ddlslb")).SelectedValue.ToString())).ToString("dd-MMM-yyyy HH:mm:ss");



            //    string Gvalue = "";
            //    bool result;

            //    Gvalue = (((CheckBoxList)this.gvInfo.Rows[1].FindControl("ChkBoxLstFollow")).Items.Count == 0) ? ((TextBox)this.gvInfo.Rows[1].FindControl("txtgvValdis")).Text.Trim()
            //               : ((CheckBoxList)this.gvInfo.Rows[1].FindControl("ChkBoxLstFollow")).SelectedValue.ToString();
            //    if (Gvalue.Length == 0)
            //    {
            //        //((Label)this.Master.FindControl("lblmsg")).Text = "Please Select Followup By Type";
            //        //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            //        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModaldis();", true);
            //        return;

            //    }


            //    for (int i = 0; i < this.gvInfo.Rows.Count; i++)
            //    {
            //        string Gcode = ((Label)this.gvInfo.Rows[i].FindControl("lblgvItmCodedis")).Text.Trim();
            //        string gtype = ((Label)this.gvInfo.Rows[i].FindControl("lgvgvaldis")).Text.Trim();
            //        string remarks = "";
            //        // Followup

            //        if (Gcode == "810100101002")
            //        {


            //            //Gvalue = (((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstFollow")).Items.Count == 0) ? ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Text.Trim()
            //            //    : ((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstFollow")).SelectedValue.ToString();



            //            foreach (ListItem chkfollow in ((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstFollow")).Items)
            //            {


            //                if (chkfollow.Selected)
            //                {
            //                    Gvalue += chkfollow.Value;

            //                }

            //            }
            //        }
            //        //Company
            //        else if (Gcode == "810100101007")
            //        {

            //            Gvalue = (((DropDownList)this.gvInfo.Rows[i].FindControl("ddlCompany")).Items.Count == 0) ? this.GetComeCode()
            //                        : ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlCompany")).SelectedValue.ToString();
            //        }



            //        else if (Gcode == "810100101003")
            //        {

            //            Gvalue = (((DropDownList)this.gvInfo.Rows[i].FindControl("ddlProject")).Items.Count == 0) ? ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Text.Trim()
            //                        : ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlProject")).SelectedValue.ToString();
            //        }

            //        else if (Gcode == "810100101019")
            //        {

            //            Gvalue = (((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstFollow")).Items.Count == 0) ? ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Text.Trim()
            //                : ((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstFollow")).SelectedValue.ToString();
            //        }

            //        //Lead Reason
            //        else if (Gcode == "810100101012")
            //        {

            //            Gvalue = (((DropDownList)this.gvInfo.Rows[i].FindControl("checkboxReson")).Items.Count == 0) ? ""
            //                : ((DropDownList)this.gvInfo.Rows[i].FindControl("checkboxReson")).SelectedValue.ToString();
            //        }




            //        else if (Gcode == "810100101015")
            //        {

            //            Gvalue = (((DropDownList)this.gvInfo.Rows[i].FindControl("checkboxReson")).Items.Count == 0) ? ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Text.Trim()
            //                : ((DropDownList)this.gvInfo.Rows[i].FindControl("checkboxReson")).SelectedValue.ToString();
            //        }
            //        else if (Gcode == "810100101016")
            //        {

            //            Gvalue = (((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstStatus")).Items.Count == 0) ? ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Text.Trim()
            //                : ((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstStatus")).SelectedValue.ToString();
            //        }

            //        else if (Gcode == "810100101017" || Gcode == "810100101014")
            //        {

            //            Gvalue = (((DropDownList)this.gvInfo.Rows[i].FindControl("ddlVisit")).Items.Count == 0) ? ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Text.Trim()
            //                : ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlVisit")).SelectedValue.ToString();
            //        }


            //        else if (Gcode == "810100101018")
            //        {

            //            //Gvalue == "";
            //            foreach (ListItem item in ((ListBox)this.gvInfo.Rows[i].FindControl("ddlPartic")).Items)
            //            {
            //                //if (item.Selected)
            //                //{

            //                if (item.Selected)
            //                {
            //                    Gvalue += item.Value;
            //                    remarks = remarks + item.Text + ", ";

            //                }
            //                // }
            //            }

            //            remarks = (remarks.Length == 0) ? "" : remarks.Substring(0, remarks.Length - 2);


            //            //Gvalue = (((ListBox)this.gvInfo.Rows[i].FindControl("ddlPartic")).Items.Count == 0) ? ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Text.Trim()
            //            //    : ((ListBox)this.gvInfo.Rows[i].FindControl("ddlPartic")).SelectedValue.ToString();
            //        }




            //        else if (Gcode == "810100101001" || Gcode == "810100101020")
            //        {

            //            //string fdatetime = Convert.ToDateTime((((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim()+ " " + ddlhour+":" + ddlMmin +" "+ ddlslb)).ToString("dd-MMM-yyyy HH:mm:ss");

            //            Gvalue = (((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdValdis")).Text.Trim() == "") ? System.DateTime.Today.ToString("dd-MMM-yyyy")
            //                : Convert.ToDateTime((((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdValdis")).Text.Trim() + " " + ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlhour")).SelectedValue.ToString()
            //                + ":" + ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlMmin")).SelectedValue.ToString() + " " + ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlslb")).SelectedValue.ToString())).ToString("dd-MMM-yyyy HH:mm:ss");
            //        }
            //        //else if (Gcode == "810100101020")
            //        //{
            //        //    string sdsd = ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim();

            //        //    Gvalue = (((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim() == "") ? "1900-01-01 00:00:00"
            //        //       : Convert.ToDateTime((((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim() + " " + ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlhour")).SelectedValue.ToString()
            //        //        + ":" + ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlMmin")).SelectedValue.ToString() + " " + ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlslb")).SelectedValue.ToString())).ToString("dd-MMM-yyyy HH:mm:ss");
            //        //  }
            //        else
            //        {

            //            Gvalue = ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Text.Trim();
            //        }

            //        Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue) : (gtype == "N") ? Convert.ToDouble("0" + Gvalue).ToString() : Gvalue;




            //        if (Gvalue != "")
            //        {
            //            result = instcrm.UpdateTransInfo3(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "INSERTUPDATESCDINF", hempid, Client, kpigrp, "", wrkdpt, cdate, Gcode, gtype, Gvalue, remarks);

            //            if (result)
            //            {
            //                ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully";
            //                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            //            }
            //            else
            //            {
            //                ((Label)this.Master.FindControl("lblmsg")).Text = "Update Fail";
            //                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            //                return;
            //            }
            //            Gvalue = "";
            //        }


            //    }



            //    this.clearModalField();


            //    string events = hst["events"].ToString();
            //    if (Convert.ToBoolean(events) == true)
            //    {
            //        string eventtype = "Update Discussion Information (sales CRM)";
            //        string eventdesc = "Update Discussion Information (sales CRM)";
            //        string eventdesc2 = "";

            //        bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            //}



        }



    }
}