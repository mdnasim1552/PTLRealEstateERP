using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.IO;
using RealERPLIB;
using System.Linq;
using System.Data.OleDb;
using System.Data;
using System.Web.Script.Services;
using System.Web.Services;
using System.Drawing;
using Microsoft.Reporting.WinForms;
using System.Web.Script.Serialization;
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
                   
                  

                }


                else
                {
                    
                        
                    ViewState["tblempsup"] = ds3.Tables[0];
                    DataTable dt1 = (DataTable)ViewState["tblempsup"];
                    string cdate = todate;
                    string proscod = dt1.Rows[0]["sircode"].ToString();
                    DataSet ds1 = instcrm.GetTransInfo(comcod, "SP_REPORT_CRM_MODULE", "SHOWPROSPECTIVEDISCUSSION", proscod, cdate, "", "", "", "");
                    ViewState["tblfollow"]= ds1.Tables[1];
                    DataTable dt2 = (DataTable)ViewState["tblfollow"];

                    if (ds1.Tables[0].Rows.Count == 0)
                    {

                        this.pnlflw.Visible = true;
                        this.lnkEdit.Visible = false;
                       
                    }
                    else
                    {
                        this.lnkEdit.Visible = true;
                        this.pnlflw.Visible = false;
                    }
                   
                    this.lblname.Text = dt2.Rows[0]["pid"].ToString(); ;
                    this.lblconper.Text = dt1.Rows[0]["sirdesc"].ToString();
                    this.lblmbl.Text = dt1.Rows[0]["phone"].ToString();
                    this.lblhomead.Text = dt1.Rows[0]["caddress"].ToString();
                    this.lblprof.Text = dt2.Rows[0]["profession"].ToString();

                    bool reject = Convert.ToBoolean(dt1.Rows[0]["reject"].ToString());


                    if (reject == true)
                    {
                        this.lblstatus.Text = "Rejected";
                        lblstatus.Attributes["style"] = "font-weight:bold; Color:Red;";
                        this.pnlretrive.Visible = true;
                    }
                    else
                    {
                        this.lblstatus.Text = "Active";
                        this.pnlretrive.Visible = false;
                        lblstatus.Attributes["style"] = "font-weight:bold; Color:Green;";
                    }
                    this.lblproscod.Value = ds1.Tables[0].Rows.Count == 0 ? proscod : ds1.Tables[0].Rows[0]["proscod"].ToString();
                    this.lblgeneratedate.Value = ds1.Tables[1].Rows.Count == 0 ? "01-Jan-1900" : Convert.ToDateTime(ds1.Tables[1].Rows[0]["createdate"]).ToString("dd-MMM-yyyy");
                    this.hiddenLedStatus.Value = (ds1.Tables[0].Rows.Count == 0 ? "" : ds1.Tables[0].Rows[0]["lastlstcode"].ToString());
                    this.lbleditempid.Value = Empid;

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
     
        protected void lnkbtnRetreive_Click(object sender, EventArgs e)
        {
            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

            if (!Convert.ToBoolean(dr1[0]["delete"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }


            DataTable dt = (DataTable)ViewState["tblempsup"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string comcod = this.GetComeCode();
           
           
            string proscod = dt.Rows[0]["sircode"].ToString();
            bool result = instcrm.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "RETREIVEPROSPECT", null, null, null, proscod, userid, Posteddat, "", "", "", "", "", "", "", "", "", "", "", "", "");


            if (!result)
            {


              string Messagesd = "Retreived Fail";
              ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);
                return;

            }


            //dt.Rows[RowIndex].Delete();
        

           

         
           
            string Messages = "Successfully Retreived";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Messages + "');", true);
            this.lnkbtnOk_Click(null, null);
           
            

        }
        public string GetEmpID()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string Empid = (hst["empid"].ToString() == "") ? "93" : hst["empid"].ToString();
            return (Empid);

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
        
        protected void pnlsidebarClose_Click(object sender, EventArgs e)
        {
            this.pnlSidebar.Visible = false;
            this.pnlfollowup.Visible = true;
            this.pnlempinfo.Visible = true;
            this.pnlsrc.Visible = true;
            this.lbllandname.Visible = true;
            this.lnkbtnOk_Click(null, null);

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
        public string GetUserID()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["usrid"].ToString());

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
                        
                          ChkBoxLstFollow.SelectedValue = ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Text.Trim();

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
                        if(ddlVisitor.SelectedValue == "4201006")
                        {
                            ((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstStatus")).Enabled = false;
                            ((Panel)this.gvInfo.Rows[i].FindControl("PnlProject")).Visible = false;
                        }
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
                        if(ChkBoxLstStatus.SelectedValue == "9501001")
                        {
                            
                            ((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstStatus")).Enabled = false;
                            ((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstFollow")).Enabled = false;



                        }

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
        private void clearModalField()
        {

            try
            {

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string empid = hst["empid"].ToString();

                foreach (GridViewRow gvr1 in gvInfo.Rows)
                {

                    string gcode = ((Label)gvr1.FindControl("lblgvItmCodedis")).Text.Trim();
                    switch (gcode)
                    {

                        case "810100101018": //PARTICIPANTS  
                            ListBox ddlPartic = ((ListBox)gvr1.FindControl("ddlPartic"));
                            if (empid.Trim().Length > 0)
                                ddlPartic.SelectedValue = empid;
                            break;

                        default:
                            ((DropDownList)gvr1.FindControl("checkboxReson")).SelectedValue = null;
                            ((CheckBoxList)gvr1.FindControl("ChkBoxLstFollow")).SelectedValue = null;
                            ((CheckBoxList)gvr1.FindControl("ChkBoxLstStatus")).SelectedValue = null;
                            ((TextBox)gvr1.FindControl("txtgvValdis")).Text = "";
                            ((DropDownList)gvr1.FindControl("ddlProject")).SelectedValue = null;
                            ((DropDownList)gvr1.FindControl("ddlUnit")).SelectedValue = null;
                            ((DropDownList)gvr1.FindControl("ddlVisit")).SelectedValue = null;
                            break;
                    }


                }
            }

            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            }


            ////DataTable dt = (DataTable)ViewState["tbModalData"];
            //foreach (GridViewRow gvr1 in gvInfo.Rows)
            //{
            //    ((CheckBoxList)gvr1.FindControl("ChkBoxLstFollow")).Items.Clear();
            //    ((CheckBoxList)gvr1.FindControl("ChkBoxLstStatus")).Items.Clear();            
            //    ((ListBox)gvr1.FindControl("ddlPartic")).Items.Clear();
            //    ((TextBox)gvr1.FindControl("txtgvValdis")).Text = "";           
            //    ((Label)gvr1.FindControl("lblgvTime")).Text = "";
            //    ((DropDownList)gvr1.FindControl("ddlProject")).Items.Clear();
            //    ((DropDownList)gvr1.FindControl("ddlUnit")).Items.Clear();
            //    ((DropDownList)gvr1.FindControl("ddlVisit")).Items.Clear();

            //}




        }
         

      
        protected void lbtnUpdateDiscussion_Click(object sender, EventArgs e)
        {
            try
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                string comcod = this.GetComeCode();
                Hashtable hst = (Hashtable)Session["tblLogin"];

                string hempid = this.lbleditempid.Value;
                string empid = hst["empid"].ToString();

                DataTable dt = ((DataTable)ViewState["tblempsup"]).Copy();

                var query = (from dtl1 in dt.AsEnumerable()
                             where (dtl1.Field<string>("empid") == hempid) || (dtl1.Field<string>("empid") == empid)
                             select dtl1);

                DataTable dtE = query.AsDataView().ToTable();
                if (dtE.Rows.Count == 0)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "This prospect is not your under";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }


                //if (hempid != empid)
                //{

                //    ((Label)this.Master.FindControl("lblmsg")).Text = "This prospect is not your under";
                //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                //    return;

                //} 


                if (empid.Length == 0)
                {

                    ((Label)this.Master.FindControl("lblmsg")).Text = "Employee is not exixted";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;

                }
                string Client = this.lblproscod.Value.ToString();
                string kpigrp = "000000000000";
                string wrkdpt = "000000000000";
                DateTime time = System.DateTime.Now;

                //string cdate = this.txtFrom.Text.ToString() +" "+ time.ToString("HH:mm");

                string cdate = Convert.ToDateTime((((TextBox)this.gvInfo.Rows[0].FindControl("txtgvdValdis")).Text.Trim() + " " + ((DropDownList)this.gvInfo.Rows[0].FindControl("ddlhour")).SelectedValue.ToString()
                            + ":" + ((DropDownList)this.gvInfo.Rows[0].FindControl("ddlMmin")).SelectedValue.ToString() + " " + ((DropDownList)this.gvInfo.Rows[0].FindControl("ddlslb")).SelectedValue.ToString())).ToString("dd-MMM-yyyy HH:mm:ss");



                string Gvalue = "";
                bool result;

                Gvalue = (((CheckBoxList)this.gvInfo.Rows[1].FindControl("ChkBoxLstFollow")).Items.Count == 0) ? ((TextBox)this.gvInfo.Rows[1].FindControl("txtgvValdis")).Text.Trim()
                           : ((CheckBoxList)this.gvInfo.Rows[1].FindControl("ChkBoxLstFollow")).SelectedValue.ToString();
                if (Gvalue.Length == 0)
                {
                    //((Label)this.Master.FindControl("lblmsg")).Text = "Please Select Followup By Type";
                    //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModaldis();", true);
                    return;

                }


                for (int i = 0; i < this.gvInfo.Rows.Count; i++)
                {
                    string Gcode = ((Label)this.gvInfo.Rows[i].FindControl("lblgvItmCodedis")).Text.Trim();
                    string gtype = ((Label)this.gvInfo.Rows[i].FindControl("lgvgvaldis")).Text.Trim();
                    string remarks = "";
                    // Followup

                    if (Gcode == "810100101002")
                    {


                        //Gvalue = (((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstFollow")).Items.Count == 0) ? ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Text.Trim()
                        //    : ((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstFollow")).SelectedValue.ToString();



                        foreach (ListItem chkfollow in ((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstFollow")).Items)
                        {


                            if (chkfollow.Selected)
                            {
                                Gvalue += chkfollow.Value;

                            }

                        }
                    }
                    //Company
                    else if (Gcode == "810100101007")
                    {

                        Gvalue = (((DropDownList)this.gvInfo.Rows[i].FindControl("ddlCompany")).Items.Count == 0) ? this.GetComeCode()
                                    : ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlCompany")).SelectedValue.ToString();
                    }



                    else if (Gcode == "810100101003")
                    {

                        Gvalue = (((DropDownList)this.gvInfo.Rows[i].FindControl("ddlProject")).Items.Count == 0) ? ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Text.Trim()
                                    : ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlProject")).SelectedValue.ToString();
                    }

                    else if (Gcode == "810100101019")
                    {

                        Gvalue = (((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstFollow")).Items.Count == 0) ? ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Text.Trim()
                            : ((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstFollow")).SelectedValue.ToString();
                    }

                    //Lead Reason
                    else if (Gcode == "810100101012")
                    {

                        Gvalue = (((DropDownList)this.gvInfo.Rows[i].FindControl("checkboxReson")).Items.Count == 0) ? ""
                            : ((DropDownList)this.gvInfo.Rows[i].FindControl("checkboxReson")).SelectedValue.ToString();
                    }




                    else if (Gcode == "810100101015")
                    {

                        Gvalue = (((DropDownList)this.gvInfo.Rows[i].FindControl("checkboxReson")).Items.Count == 0) ? ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Text.Trim()
                            : ((DropDownList)this.gvInfo.Rows[i].FindControl("checkboxReson")).SelectedValue.ToString();
                    }
                    else if (Gcode == "810100101016")
                    {

                        Gvalue = (((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstStatus")).Items.Count == 0) ? ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Text.Trim()
                            : ((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstStatus")).SelectedValue.ToString();
                    }

                    else if (Gcode == "810100101017" || Gcode == "810100101014")
                    {

                        Gvalue = (((DropDownList)this.gvInfo.Rows[i].FindControl("ddlVisit")).Items.Count == 0) ? ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Text.Trim()
                            : ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlVisit")).SelectedValue.ToString();
                    }


                    else if (Gcode == "810100101018")
                    {

                        //Gvalue == "";
                        foreach (ListItem item in ((ListBox)this.gvInfo.Rows[i].FindControl("ddlPartic")).Items)
                        {
                            //if (item.Selected)
                            //{

                            if (item.Selected)
                            {
                                Gvalue += item.Value;
                                remarks = remarks + item.Text + ", ";

                            }
                            // }
                        }

                        remarks = (remarks.Length == 0) ? "" : remarks.Substring(0, remarks.Length - 2);


                        //Gvalue = (((ListBox)this.gvInfo.Rows[i].FindControl("ddlPartic")).Items.Count == 0) ? ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Text.Trim()
                        //    : ((ListBox)this.gvInfo.Rows[i].FindControl("ddlPartic")).SelectedValue.ToString();
                    }




                    else if (Gcode == "810100101001" || Gcode == "810100101020")
                    {

                        //string fdatetime = Convert.ToDateTime((((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim()+ " " + ddlhour+":" + ddlMmin +" "+ ddlslb)).ToString("dd-MMM-yyyy HH:mm:ss");

                        Gvalue = (((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdValdis")).Text.Trim() == "") ? System.DateTime.Today.ToString("dd-MMM-yyyy")
                            : Convert.ToDateTime((((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdValdis")).Text.Trim() + " " + ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlhour")).SelectedValue.ToString()
                            + ":" + ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlMmin")).SelectedValue.ToString() + " " + ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlslb")).SelectedValue.ToString())).ToString("dd-MMM-yyyy HH:mm:ss");
                    }
                    //else if (Gcode == "810100101020")
                    //{
                    //    string sdsd = ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim();

                    //    Gvalue = (((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim() == "") ? "1900-01-01 00:00:00"
                    //       : Convert.ToDateTime((((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim() + " " + ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlhour")).SelectedValue.ToString()
                    //        + ":" + ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlMmin")).SelectedValue.ToString() + " " + ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlslb")).SelectedValue.ToString())).ToString("dd-MMM-yyyy HH:mm:ss");
                    //  }
                    else
                    {

                        Gvalue = ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvValdis")).Text.Trim();
                    }

                    Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue) : (gtype == "N") ? Convert.ToDouble("0" + Gvalue).ToString() : Gvalue;




                    if (Gvalue != "")
                    {
                        result = instcrm.UpdateTransInfo3(comcod, "dbo.SP_REPORT_CRM_MODULE", "INSERTUPDATESCDINF", hempid, Client, kpigrp, "", wrkdpt, cdate, Gcode, gtype, Gvalue, remarks);

                        if (result)
                        {
                            this.pnlsidebarClose_Click(null, null);
                            string Messagesd = "Update Successfully";
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Messagesd + "');", true);
                           

                        }
                        else
                        {
                            string Messagesd = "Update Fail";
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messagesd + "');", true);
                            return;
                        }
                        Gvalue = "";
                    }


                }



                this.clearModalField();
                  

                string events = hst["events"].ToString();
                if (Convert.ToBoolean(events) == true)
                {
                    string eventtype = "Update Discussion Information (sales CRM)";
                    string eventdesc = "Update Discussion Information (sales CRM)";
                    string eventdesc2 = "";

                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }
               

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }



        }
        

        protected void pnlEditProspectClose_Click(object sender, EventArgs e)
        {
            this.pnlEditProspect.Visible = false;
            this.pnlfollowup.Visible = true;
            this.pnlempinfo.Visible = true;
            this.pnlsrc.Visible = true;
           
            this.lbllandname.Visible = false;
           
            Response.Redirect(Request.Url.ToString());
        }
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            this.pnlEditProspect.Visible = true;
            this.pnlfollowup.Visible = false;
            this.pnlempinfo.Visible = false;
            this.pnlflw.Visible = false;
            this.pnlsrc.Visible = false;
            DataTable dt2 = (DataTable)ViewState["tblfollow"];

            if(dt2 == null)
            {
                return;
            }
            string comcod = GetComeCode();
            string styleid = dt2.Rows[0]["proscod"].ToString(); 
            string clintIdno = dt2.Rows[0]["pid"].ToString();

            lbllandname.Text = dt2.Rows[0]["pid"].ToString() + ':' + dt2.Rows[0]["prosdesc"].ToString();
            ViewState["existclientcode"] = styleid;
            this.MultiView1.ActiveViewIndex = 0;
            GetData();
            GetAllSubdata();
            ShowPersonalInfo();
            ShowSourceInfo();
            Showpinfo();
            ShowhomeInfo();
            Showbusinfo();
            ShowMoreInfo();
            string Message = "Edit Client Form";

            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Message + "');", true);
            ViewState["tblfollow"] = null;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string events = hst["events"].ToString();
            if (Convert.ToBoolean(events) == true)
            {
                string eventtype = "Edit Client Information (Sales CRM)";
                string eventdesc = "Edit Client Information (Sales CRM)";
                string eventdesc2 = "Edit " + clintIdno;

                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);

               
            }

        }
        protected void ddlval_DataBound(object sender, EventArgs e)
        {

        }
        protected void gvPersonalInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lgcResDesc = (Label)e.Row.FindControl("lgcResDesc1");
                // Label lgcResDesc = (TextBox)e.Row.FindControl("txtgvVal");


                string gcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "gcod")).ToString();
                string gdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "gdesc")).ToString();

                if (gcod == "0301001" || gcod == "0301003")
                {
                    lgcResDesc.Text = gdesc + "<span class='manField'><sup> *</sup></span>";

                    //lgcResDesc.Attributes["style"] = "<span><sup> *</sup></span>";
                }
                TextBox txtbx = (TextBox)e.Row.FindControl("txtgvVal");

                if (gcod == "0301003")
                {

                    txtbx.Attributes.Add("minimum", "11");
                    txtbx.Attributes.Add("maximum", "11");
                    txtbx.Attributes.Add("OnTextChanged", "txtgvVal_TextChanged");
                }
                else
                {
                    txtbx.Attributes.Remove("OnTextChanged");
                }


                if (gcod == "0301025")
                {
                    txtbx.TextMode = TextBoxMode.MultiLine;

                }

                //// custLink.NavigateUrl = "~/F_39_MyPage/ClientDetail.aspx?Type=Mgt";
                //hlnkAdd.NavigateUrl = "~/F_21_Mkt/MktEmpKpiEntry.aspx?Type=Entry&clientid=" + cusCode.Text + "&followupdate=" + napnt;
                //hyledit.NavigateUrl = "~/F_21_Mkt/MktEmpKpiEntry.aspx?Type=Edit&clientid=" + cusCode.Text + "&followupdate=" + date;


            }
        }
        protected void txtgvVal_TextChanged1(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userrole = hst["userrole"].ToString();
            string comcod = this.GetComeCode();
            int RowIndex = ((GridViewRow)((TextBox)sender).NamingContainer).RowIndex;
            string Gcode = ((Label)this.gvPersonalInfo.Rows[RowIndex].FindControl("lblgvItmCodeper")).Text.Trim();
            if (Gcode == "0301003")
            {
                string mobile = ((TextBox)this.gvPersonalInfo.Rows[RowIndex].FindControl("txtgvVal")).Text.Trim();
                string sircode = this.lblnewprospect.Value;
                //txtgvVal = Regex.Match(txtgvVal, @"\d+").Value;


                if (mobile.Length != 11)
                {
                    ((TextBox)this.gvPersonalInfo.Rows[RowIndex].FindControl("txtgvVal")).Text = "";
                    ((TextBox)this.gvPersonalInfo.Rows[RowIndex].FindControl("txtgvVal")).BorderColor = System.Drawing.Color.Red;
                    ((TextBox)this.gvPersonalInfo.Rows[RowIndex].FindControl("txtgvVal")).Focus();
                    ((TextBox)this.gvPersonalInfo.Rows[RowIndex].FindControl("txtgvVal")).ForeColor = System.Drawing.Color.Red;
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please enter valid mobile number');", true);

                    return;
                }
                DataSet ds2 = instcrm.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_CODEBOOK_NEW", "CHECKEDDUPUCLIENTPHONE", mobile, "", "", "", "", "", "", "", "");
                if (ds2.Tables[0].Rows.Count == 0)
                    return;

                string pid = ds2.Tables[0].Rows[0]["pid"].ToString();
                string sirdesc = ds2.Tables[0].Rows[0]["sirdesc"].ToString();
                string supervisor = ds2.Tables[0].Rows[0]["superviser"].ToString();
                string phone = ds2.Tables[0].Rows[0]["phone"].ToString();
                string creatDate = ds2.Tables[0].Rows[0]["creatDate"].ToString();
                string lststdate = ds2.Tables[0].Rows[0]["lststdate"].ToString();

                //string holdername = " His/Her Name " + mobilename;
                string Message = "Duplicate : ";
                string totmsg = Message + phone + ", " + pid + ", Associate: " + sirdesc + ", Team Leader: " + supervisor + ", Create Date: " + creatDate + ", Last Followup Date: " + lststdate;
                ((TextBox)this.gvPersonalInfo.Rows[RowIndex].FindControl("txtgvVal")).Text = "";

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + totmsg + "');", true);


            }
        }

        private void GetData()
        {
            string comcod = this.GetComeCode();
            //string landid = (string)ViewState["sircodegrid"];
            DataSet ds1 = instcrm.GetTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "CLNTNEWCODE", "", "", "", "", "", "", "", "", "");

            string clientcode = (string)ViewState["existclientcode"];

            if (clientcode == null)
            {
                ViewState["newclientcode"] = ds1.Tables[0].Rows[0]["sircode"].ToString();
            }
            else
            {
                ViewState["newclientcode"] = clientcode;
            }


            string ccl = (string)ViewState["newclientcode"];
            this.lblnewprospect.Value = ccl;
            DataSet ds2 = instcrm.GetTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "CLIENTINFODETGRID", ccl, "", "", "", "", "", "", "", "");
            ViewState["tblDatagridbinfo"] = ds2.Tables[0];
            ViewState["tblDatagridsinfo"] = ds2.Tables[1];
            ViewState["tblDatagridpinfo"] = ds2.Tables[2];
            ViewState["tblDatagridhinfo"] = ds2.Tables[3];
            ViewState["tblDatagridbusinfo"] = ds2.Tables[4];
            ViewState["tblDatagridmoreinfo"] = ds2.Tables[5];



        }
      
        private void ShowPersonalInfo()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];

            string comcod = this.GetComeCode();
            //string landid = (string)ViewState["sircodegrid"];

            DataTable dt = (DataTable)ViewState["tblDatagridbinfo"];
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            DataView dv1;
            if (dt == null)
                return;
            this.gvPersonalInfo.DataSource = dt;
            this.gvPersonalInfo.DataBind();
            //txtgvdVal.Attributes.Add("style", "display:none");
            DropDownList ddlgval;
            DropDownList ddlgval1;
            string gvalue = "";
            string ccc = "";
            bool teamLeader = IsTeamLeader();
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string gcod = dt.Rows[i]["gcod"].ToString();

                switch (gcod)
                {
                    case "0301001": //Prospect Name
                        switch (comcod)
                        {
                            case "3368"://Finlay

                                if (lbllandname.Text.Length > 0)
                                {
                                    if (teamLeader)
                                    {
                                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).ReadOnly = false;
                                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("Panegrd")).Visible = false;
                                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = false;
                                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcountryPhone")).Visible = false;


                                    }
                                    else
                                    {
                                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).ReadOnly = true;
                                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("Panegrd")).Visible = false;
                                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = false;
                                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcountryPhone")).Visible = false;

                                    }
                                }
                                else
                                {
                                    ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).ReadOnly = false;
                                    ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                                    ((Panel)this.gvPersonalInfo.Rows[i].FindControl("Panegrd")).Visible = false;
                                    ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                                    ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = false;
                                    ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcountryPhone")).Visible = false;
                                }
                                break;


                            default:
                                ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).ReadOnly = false;
                                ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                                ((Panel)this.gvPersonalInfo.Rows[i].FindControl("Panegrd")).Visible = false;
                                ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                                ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = false;
                                ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcountryPhone")).Visible = false;
                                break;
                        }
                        break;

                    case "0301011": //Profession
                        gvalue = dt.Rows[i]["value"].ToString();
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '86%'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.Items.Insert(0, new ListItem("--Please Select--", ""));
                        ddlgval.SelectedValue = gvalue;
                        break;

                    case "0301003":
                    case "0301004":
                    case "0301005":

                        ccc = dt.Rows[i]["ccc"].ToString();

                        ddlgval1 = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcountryPhone"));


                        dv1 = dt1.Copy().DefaultView;
                        dv1.RowFilter = ("gcod = 'phonecode'");
                        DataTable dtCcc = dv1.ToTable();

                        ddlgval1.DataTextField = "gdesc";
                        ddlgval1.DataValueField = "gdesc";
                        ddlgval1.DataSource = dtCcc;
                        ddlgval1.DataBind();
                        ddlgval1.SelectedValue = (ccc == "" ? "+88" : ccc);





                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Attributes.Add("style", "width:165px; float:right; margin-top:-34px");

                        //((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcountryPhone")).Visible = true;
                        //((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcountryPhone")).Enabled = true;

                        //For changing Mobile No. by Team Leader
                        switch (comcod)
                        {

                            case "3315"://Assure
                            case "3316"://Assure
                                //Edit     
                                if (lbllandname.Text.Length > 0)
                                {
                                    if (teamLeader)
                                    {
                                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).ReadOnly = false;
                                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = false;
                                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcountryPhone")).Visible = true;
                                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcountryPhone")).Enabled = true;

                                    }
                                    else
                                    {
                                        switch (gcod)
                                        {
                                            case "0301004":
                                            case "0301005":
                                                ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).ReadOnly = false;
                                                ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                                                ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = false;
                                                ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcountryPhone")).Visible = true;
                                                ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcountryPhone")).Enabled = true;
                                                break;

                                            default:
                                                ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).ReadOnly = true;
                                                ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                                                ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = false;
                                                ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcountryPhone")).Visible = true;
                                                ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcountryPhone")).Enabled = false;
                                                break;
                                        }

                                    }
                                }
                                else
                                {

                                    ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).ReadOnly = false;
                                    ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                                    ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = false;
                                    ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcountryPhone")).Visible = true;
                                    ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcountryPhone")).Enabled = true;

                                    //if (((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim().Length > 0)
                                    //{
                                    //    ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).ReadOnly = true;
                                    //    ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                                    //    ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = false;
                                    //    ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcountryPhone")).Visible = true;
                                    //    ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcountryPhone")).Enabled = false;

                                    //}
                                    //else
                                    //{
                                    //    ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).ReadOnly = false;
                                    //    ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                                    //    ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = false;
                                    //    ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcountryPhone")).Visible = true;
                                    //    ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcountryPhone")).Enabled = true;

                                    //}
                                }
                                break;

                            // case "3102": 
                            case "3367"://Epic
                            case "3368"://Finlay

                                if (lbllandname.Text.Length > 0)
                                {
                                    if (teamLeader)
                                    {
                                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).ReadOnly = false;
                                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = false;
                                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcountryPhone")).Visible = true;
                                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcountryPhone")).Enabled = true;

                                    }
                                    else
                                    {
                                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).ReadOnly = true;
                                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = false;
                                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcountryPhone")).Visible = true;
                                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcountryPhone")).Enabled = false;

                                    }
                                }
                                else
                                {
                                    ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).ReadOnly = false;
                                    ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                                    ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = false;
                                    ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcountryPhone")).Visible = true;
                                    ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcountryPhone")).Enabled = true;

                                    //if (((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim().Length > 0)
                                    //{
                                    //    ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).ReadOnly = true;
                                    //    ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                                    //    ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = false;
                                    //    ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcountryPhone")).Visible = true;
                                    //    ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcountryPhone")).Enabled = false;

                                    //}
                                    //else
                                    //{
                                    //    ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).ReadOnly = false;
                                    //    ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                                    //    ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = false;
                                    //    ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcountryPhone")).Visible = true;
                                    //    ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcountryPhone")).Enabled = true;

                                    //}
                                }
                                break;


                            default:
                                if (((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim().Length > 0)
                                {
                                    ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).ReadOnly = true;
                                    ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                                    ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = false;
                                    ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcountryPhone")).Visible = true;
                                    ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcountryPhone")).Enabled = true;
                                }
                                else
                                {
                                    ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).ReadOnly = false;
                                    ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                                    ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = false;
                                    ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcountryPhone")).Visible = true;
                                    ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcountryPhone")).Enabled = true;
                                }
                                break;
                        }
                        break;

                    default:
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).ReadOnly = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = false;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcountryPhone")).Visible = false;

                        break;

                }

            }



        }
        private void ShowSourceInfo()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            DataTable dtemp = (DataTable)ViewState["tblempsup"];
            DataTable dt = (DataTable)ViewState["tblDatagridsinfo"];
            DataTable dt1 = ((DataTable)ViewState["tblsubddl"]).Copy();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            // string userid = hst["usrid"].ToString();
            string empid = hst["empid"].ToString();
            string teamid = hst["teamid"].ToString();
            string userrole = hst["userrole"].ToString();
            //  DataSet ds1 = instcrm.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMP_INTERFACE", "GETDEPTDATA", userid, "", "", "", "", "", "", "", "", "");


            DataView dv1;
            if (dt == null)
                return;
            this.gvSourceInfo.DataSource = dt;
            this.gvSourceInfo.DataBind();
            DropDownList ddlgval;
            string gempid = "";
            string assEmpid = "";

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string gcod = dt.Rows[i]["gcod"].ToString();

                switch (gcod)
                {
                    case "0302001": //Source
                        switch (comcod)
                        {
                            //For Not Changing Source without Team Leader
                            //Epic
                            case "3367":
                                //case "3101":
                                dv1 = dt1.DefaultView;
                                dv1.RowFilter = ("gcod like '31%'");
                                dt1 = dv1.ToTable();
                                dt1.Rows.Add("0000000", "None", "0000000", "0");
                                dt1.DefaultView.Sort = "rowid";
                                ((TextBox)this.gvSourceInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                                ((TextBox)this.gvSourceInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                                ddlgval = ((DropDownList)this.gvSourceInfo.Rows[i].FindControl("ddlval"));
                                ddlgval.DataTextField = "gdesc";
                                ddlgval.DataValueField = "gcod";
                                ddlgval.DataSource = dt1;
                                ddlgval.DataBind();
                                ddlgval.SelectedValue = ((TextBox)this.gvSourceInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();

                                bool teamLeader = IsTeamLeader();
                                if (lbllandname.Text.Length > 0)
                                {
                                    if (teamLeader)
                                    {
                                        ddlgval.Enabled = true;
                                    }
                                    else
                                    {
                                        ddlgval.Enabled = false;
                                    }
                                }
                                else
                                {
                                    ddlgval.Enabled = true;
                                }
                                break;

                            default:
                                dv1 = dt1.DefaultView;
                                dv1.RowFilter = ("gcod like '31%'");
                                dt1 = dv1.ToTable();
                                dt1.Rows.Add("0000000", "None", "0000000", "0");
                                dt1.DefaultView.Sort = "rowid";
                                ((TextBox)this.gvSourceInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                                ((TextBox)this.gvSourceInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                                ddlgval = ((DropDownList)this.gvSourceInfo.Rows[i].FindControl("ddlval"));
                                ddlgval.DataTextField = "gdesc";
                                ddlgval.DataValueField = "gcod";
                                ddlgval.DataSource = dt1;
                                ddlgval.DataBind();
                                //ddlgval.Items.Insert(0, new ListItem("--Please Select--", ""));
                                ddlgval.SelectedValue = ((TextBox)this.gvSourceInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                                break;
                        }
                        break;

                    case "0302003": //Team Leader

                        gempid = ((TextBox)this.gvSourceInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();

                        dv1 = ((DataTable)ViewState["tblsubddl"]).Copy().DefaultView;
                        if (userrole == "1")
                            dv1.RowFilter = ("gcod like '93%'");
                        else
                        {
                            if (gempid.Length > 0)
                                dv1.RowFilter = ("gcod like '" + teamid + "' or gcod like '" + gempid + "'");

                            else
                                dv1.RowFilter = ("gcod like '" + teamid + "'");
                        }



                        ((TextBox)this.gvSourceInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvSourceInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvSourceInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        //ddlgval.Items.Insert(0, new ListItem("--Please Select--", ""));

                        // empid = gempid.Length == 0 ? empid : gempid;
                        ddlgval.SelectedValue = gempid;

                        break;

                    case "0302005": //Assign Persion   

                        dv1 = ((DataTable)ViewState["tblsubddl"]).Copy().DefaultView;
                        if (userrole == "1")
                            dv1.RowFilter = ("gcod like '93%'");
                        else
                        {
                            DataTable dts = dv1.ToTable();
                            var query = (from dtl1 in dts.AsEnumerable()
                                         join dtl2 in dtemp.AsEnumerable() on dtl1.Field<string>("gcod") equals dtl2.Field<string>("empid")
                                         where dtl1.Field<string>("gcod").Substring(0, 2) == "93"

                                         select new
                                         {
                                             gcod = dtl1.Field<string>("gcod"),
                                             gdesc = dtl1.Field<string>("gdesc"),
                                             code = dtl1.Field<string>("code")
                                         }).ToList();
                            dv1 = ASITUtility03.ListToDataTable(query).DefaultView;
                        }




                        ((TextBox)this.gvSourceInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvSourceInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvSourceInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        // ddlgval.Items.Insert(0, new ListItem("--Please Select--", ""));
                        gempid = ((TextBox)this.gvSourceInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        // empid = gempid.Length == 0 ? empid : gempid;
                        ddlgval.SelectedValue = gempid;
                        // ddlgval.SelectedValue = ((TextBox)this.gvSourceInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();

                        assEmpid = gempid;


                        break;
                    case "0302009": //Rating
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '38%'");
                        ((TextBox)this.gvSourceInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvSourceInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvSourceInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.Items.Insert(0, new ListItem("--Please Select--", ""));
                        ddlgval.SelectedValue = ((TextBox)this.gvSourceInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    case "0302011": //Industry
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '39%'");
                        ((TextBox)this.gvSourceInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvSourceInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvSourceInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.Items.Insert(0, new ListItem("--Please Select--", ""));
                        ddlgval.SelectedValue = ((TextBox)this.gvSourceInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    case "0302013": //Branch
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '40%'");
                        ((TextBox)this.gvSourceInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvSourceInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvSourceInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.Items.Insert(0, new ListItem("--Please Select--", ""));
                        ddlgval.SelectedValue = ((TextBox)this.gvSourceInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    case "0302015": //Lands Create Department
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '41%'");
                        ((TextBox)this.gvSourceInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvSourceInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvSourceInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.Items.Insert(0, new ListItem("--Please Select--", ""));
                        ddlgval.SelectedValue = ((TextBox)this.gvSourceInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    case "0302017": //Lead Quality
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '42%'");
                        ((TextBox)this.gvSourceInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvSourceInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvSourceInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.Items.Insert(0, new ListItem("--Please Select--", ""));
                        ddlgval.SelectedValue = ((TextBox)this.gvSourceInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    case "0302019": //Lead Status
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '95%'");
                        ((TextBox)this.gvSourceInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvSourceInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvSourceInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.Items.Insert(0, new ListItem("--Please Select--", ""));
                        ddlgval.SelectedValue = ((TextBox)this.gvSourceInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    default:
                        ((TextBox)this.gvSourceInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvSourceInfo.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvSourceInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.gvSourceInfo.Rows[i].FindControl("ddlval")).Visible = false;
                        break;

                }

            }
        }
        protected void gvSourceInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lgcResDesc = (Label)e.Row.FindControl("lgcResDescsr");
                string gcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "gcod")).ToString();
                string gdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "gdesc")).ToString();

                if (gcod == "0302001")
                {
                    lgcResDesc.Text = gdesc + "<span class='manField'><sup> *</sup></span>";
                }
                else if (gcod == "0302003")
                {
                    lgcResDesc.Text = gdesc + "<span class='manField'><sup> *</sup></span>";

                }
                if ((gcod == "0302017") || (gcod == "0302019"))
                {
                    e.Row.Visible = false;
                }

            }
        }
        protected void ddlval_SelectedIndexChanged(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userrole = hst["userrole"].ToString();
            string comcod = this.GetComeCode();


            int RowIndex = ((GridViewRow)((DropDownList)sender).NamingContainer).RowIndex;
            string empid = ((DropDownList)this.gvSourceInfo.Rows[RowIndex].FindControl("ddlval")).SelectedValue;
            string Gcode = ((Label)this.gvSourceInfo.Rows[RowIndex].FindControl("lblgvItmCode")).Text.Trim();
            if (Gcode == "0302005")
            {
                DataSet ds2 = instcrm.GetTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "GETSUPERVISORLISTBYID", empid, "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;
                string teamid = (ds2.Tables[0].Rows[0]["teamid"].ToString() == "" ? "93%" : ds2.Tables[0].Rows[0]["teamid"].ToString());


                DataView dv1;
                dv1 = ((DataTable)ViewState["tblsubddl"]).Copy().DefaultView; ;
                //if (userrole == "1")
                //    dv1.RowFilter = ("gcod like '93%'");
                //else

                dv1.RowFilter = ("gcod like '" + teamid + "'");



                ((TextBox)this.gvSourceInfo.Rows[RowIndex - 1].FindControl("txtgvVal")).Visible = false;
                ((TextBox)this.gvSourceInfo.Rows[RowIndex - 1].FindControl("txtgvdVal")).Visible = false;
                ddlgval = ((DropDownList)this.gvSourceInfo.Rows[RowIndex - 1].FindControl("ddlval"));
                ddlgval.DataTextField = "gdesc";
                ddlgval.DataValueField = "gcod";
                ddlgval.DataSource = dv1.ToTable();
                ddlgval.DataBind();

                ddlgval.SelectedValue = teamid;
            }




        }
        protected void lnkSaveModalEntry_Click(object sender, EventArgs e)
        {
            GetData();
            string comcod = GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            DataSet ds = new DataSet("ds1");
            string clientid = (string)ViewState["newclientcode"];
            string Name = this.txtentryClient.Text;
            string mobile = this.txtentrymobile.Text;
            string email = this.txtentryemail.Text;
            string source = this.ddlgval.SelectedValue.ToString();
            string Date = this.txtentrydate.Text;
            string usrid = hst["usrid"].ToString();
            string empid = this.txtentryEmpID.Text;
            string active = "";
            DataTable dt2 = new DataTable();

            dt2.Clear();
            dt2.Columns.Add("gcod");
            dt2.Columns.Add("gvalue");
            DataRow dr = dt2.NewRow();
            dr["gcod"] = "0302001";
            dr["gvalue"] = source;
            dt2.Rows.Add(dr);
            ds.Tables.Add(dt2);
            ds.Tables[0].TableName = "tbl2";
            bool result = instcrm.UpdateXmlTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "UPDATE_CLNTINFO", ds, null, null, clientid, Name, usrid, mobile, email, empid, "");
            bool resultA = instcrm.UpdateTransInfo3(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "UPDATEACTLAND", clientid, active, usrid, Date);

        }
        protected void ddlvalcom_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                int RowIndex = ((GridViewRow)((DropDownList)sender).NamingContainer).RowIndex;
                string prjcomcod = ((DropDownList)this.gvpinfo.Rows[RowIndex].FindControl("ddlvalcom")).SelectedValue.ToString();
                DataTable dt = (DataTable)ViewState["tblDatagridpinfo"];
                DataView dv1;

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    string gcod = dt.Rows[i]["gcod"].ToString();

                    switch (gcod)
                    {
                        case "0303006": //Project
                            prjcomcod = prjcomcod.Trim().Length == 0 ? this.GetComeCode() : prjcomcod;
                            DataTable dtp = ((DataTable)ViewState["tblproject"]).Copy();
                            dv1 = dtp.DefaultView;
                            dv1.RowFilter = ("comcod='" + prjcomcod + "'");
                            ddlgval = ((DropDownList)this.gvpinfo.Rows[i].FindControl("ddlvalpros"));
                            ddlgval.DataTextField = "pactdesc";
                            ddlgval.DataValueField = "pactcode";
                            ddlgval.DataSource = dv1.ToTable();
                            ddlgval.DataBind();
                            //ddlgval.SelectedValue = ((TextBox)this.gvpinfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                            break;



                    }

                }
            }

            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);


            }




        }
        private void Showpinfo()
        {
            string comcod = this.GetComeCode();
            DataTable dt = (DataTable)ViewState["tblDatagridpinfo"];
            if (dt == null)
                return;
            this.gvpinfo.DataSource = dt;
            this.gvpinfo.DataBind();
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];

            DataView dv1;
            DropDownList ddlgval;
            DropDownList ddlvalcom;
            ListBox location;
            string prjcomcod = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string gcod = dt.Rows[i]["gcod"].ToString();

                switch (gcod)
                {
                    case "0303002": //Company                    
                        ((TextBox)this.gvpinfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvpinfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvpinfo.Rows[i].FindControl("pnlMullocation")).Visible = false;
                        ((ListBox)this.gvpinfo.Rows[i].FindControl("lstlocation")).Items.Clear();
                        ((DropDownList)this.gvpinfo.Rows[i].FindControl("ddlvalpros")).Items.Clear();
                        ((DropDownList)this.gvpinfo.Rows[i].FindControl("ddlvalpros")).Visible = false;
                        ddlvalcom = ((DropDownList)this.gvpinfo.Rows[i].FindControl("ddlvalcom"));
                        ddlvalcom.DataTextField = "comdesc";
                        ddlvalcom.DataValueField = "comcod";
                        ddlvalcom.DataSource = (DataTable)ViewState["tblcompany"];
                        ddlvalcom.DataBind();
                        ddlvalcom.SelectedValue = ((TextBox)this.gvpinfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        prjcomcod = ddlvalcom.SelectedValue.ToString();
                        break;


                    case "0303003": //Apartment Type
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '32%'");
                        ((TextBox)this.gvpinfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvpinfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvpinfo.Rows[i].FindControl("pnlMullocation")).Visible = false;
                        ((DropDownList)this.gvpinfo.Rows[i].FindControl("ddlvalcom")).Items.Clear();
                        ((DropDownList)this.gvpinfo.Rows[i].FindControl("ddlvalcom")).Visible = false;

                        ((ListBox)this.gvpinfo.Rows[i].FindControl("lstlocation")).Items.Clear();
                        ddlgval = ((DropDownList)this.gvpinfo.Rows[i].FindControl("ddlvalpros"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.Items.Insert(0, new ListItem("--Please Select--", ""));
                        ddlgval.SelectedValue = ((TextBox)this.gvpinfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    case "0303005": //Location(List)
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '89%'");
                        ((TextBox)this.gvpinfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvpinfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvpinfo.Rows[i].FindControl("pnlMullocation")).Visible = true;
                        ((ListBox)this.gvpinfo.Rows[i].FindControl("lstlocation")).Items.Clear();
                        ((DropDownList)this.gvpinfo.Rows[i].FindControl("ddlvalcom")).Items.Clear();
                        ((DropDownList)this.gvpinfo.Rows[i].FindControl("ddlvalcom")).Visible = false;
                        ((DropDownList)this.gvpinfo.Rows[i].FindControl("ddlvalpros")).Items.Clear();
                        ((DropDownList)this.gvpinfo.Rows[i].FindControl("ddlvalpros")).Visible = false;
                        location = ((ListBox)this.gvpinfo.Rows[i].FindControl("lstlocation"));
                        location.DataTextField = "gdesc";
                        location.DataValueField = "gcod";
                        location.DataSource = dv1.ToTable();
                        location.DataBind();
                        int count = Convert.ToInt32(dt.Rows[i]["value"].ToString().Trim().Count());

                        int j;
                        int k = 0;
                        string data = "";
                        for (j = 0; j < count / 7; j++)
                        {
                            data = dt.Rows[i]["value"].ToString().Substring(k, 7);
                            foreach (ListItem item in location.Items)
                            {
                                if (item.Value == data)
                                {
                                    item.Selected = true;
                                }

                            }
                            k = k + 7;
                        }


                        break;


                    case "0303006": //Project
                        prjcomcod = prjcomcod.Trim().Length == 0 ? this.GetComeCode() : prjcomcod;
                        DataTable dtp = ((DataTable)ViewState["tblproject"]).Copy();
                        dv1 = dtp.DefaultView;
                        dv1.RowFilter = ("comcod='" + prjcomcod + "'");

                        ((TextBox)this.gvpinfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvpinfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvpinfo.Rows[i].FindControl("pnlMullocation")).Visible = false;
                        ((DropDownList)this.gvpinfo.Rows[i].FindControl("ddlvalcom")).Items.Clear();
                        ((DropDownList)this.gvpinfo.Rows[i].FindControl("ddlvalcom")).Visible = false;
                        ((ListBox)this.gvpinfo.Rows[i].FindControl("lstlocation")).Items.Clear();
                        ddlgval = ((DropDownList)this.gvpinfo.Rows[i].FindControl("ddlvalpros"));
                        ddlgval.DataTextField = "pactdesc";
                        ddlgval.DataValueField = "pactcode";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        // ddlgval.Items.Insert(0, new ListItem("--Please Select--", ""));
                        ddlgval.SelectedValue = ((TextBox)this.gvpinfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;


                    case "0303007": //Apartment Size
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '33%'");
                        ((TextBox)this.gvpinfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvpinfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvpinfo.Rows[i].FindControl("pnlMullocation")).Visible = false;
                        ((DropDownList)this.gvpinfo.Rows[i].FindControl("ddlvalcom")).Items.Clear();
                        ((DropDownList)this.gvpinfo.Rows[i].FindControl("ddlvalcom")).Visible = false;
                        ((ListBox)this.gvpinfo.Rows[i].FindControl("lstlocation")).Items.Clear();
                        ddlgval = ((DropDownList)this.gvpinfo.Rows[i].FindControl("ddlvalpros"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.Items.Insert(0, new ListItem("--Please Select--", ""));
                        ddlgval.SelectedValue = ((TextBox)this.gvpinfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    case "0303009": //Apartment Floor
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '34%'");
                        ((TextBox)this.gvpinfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvpinfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvpinfo.Rows[i].FindControl("pnlMullocation")).Visible = false;
                        ((DropDownList)this.gvpinfo.Rows[i].FindControl("ddlvalcom")).Items.Clear();
                        ((DropDownList)this.gvpinfo.Rows[i].FindControl("ddlvalcom")).Visible = false;
                        ((ListBox)this.gvpinfo.Rows[i].FindControl("lstlocation")).Items.Clear();
                        ddlgval = ((DropDownList)this.gvpinfo.Rows[i].FindControl("ddlvalpros"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.Items.Insert(0, new ListItem("--Please Select--", ""));
                        ddlgval.SelectedValue = ((TextBox)this.gvpinfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    case "0303011": //Facing
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '35%'");
                        ((TextBox)this.gvpinfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvpinfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvpinfo.Rows[i].FindControl("pnlMullocation")).Visible = false;
                        ((DropDownList)this.gvpinfo.Rows[i].FindControl("ddlvalcom")).Items.Clear();
                        ((DropDownList)this.gvpinfo.Rows[i].FindControl("ddlvalcom")).Visible = false;
                        ((ListBox)this.gvpinfo.Rows[i].FindControl("lstlocation")).Items.Clear();
                        ddlgval = ((DropDownList)this.gvpinfo.Rows[i].FindControl("ddlvalpros"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.Items.Insert(0, new ListItem("--Please Select--", ""));
                        ddlgval.SelectedValue = ((TextBox)this.gvpinfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    case "0303013": //View
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '36%'");
                        ((TextBox)this.gvpinfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvpinfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvpinfo.Rows[i].FindControl("pnlMullocation")).Visible = false;
                        ((DropDownList)this.gvpinfo.Rows[i].FindControl("ddlvalcom")).Items.Clear();
                        ((DropDownList)this.gvpinfo.Rows[i].FindControl("ddlvalcom")).Visible = false;
                        ((ListBox)this.gvpinfo.Rows[i].FindControl("lstlocation")).Items.Clear();
                        ddlgval = ((DropDownList)this.gvpinfo.Rows[i].FindControl("ddlvalpros"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.Items.Insert(0, new ListItem("--Please Select--", ""));
                        ddlgval.SelectedValue = ((TextBox)this.gvpinfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    case "0303015": //View
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '37%'");
                        ((TextBox)this.gvpinfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvpinfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((DropDownList)this.gvpinfo.Rows[i].FindControl("ddlvalcom")).Items.Clear();
                        ((DropDownList)this.gvpinfo.Rows[i].FindControl("ddlvalcom")).Visible = false;
                        ddlgval = ((DropDownList)this.gvpinfo.Rows[i].FindControl("ddlvalpros"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.Items.Insert(0, new ListItem("--Please Select--", ""));
                        ddlgval.SelectedValue = ((TextBox)this.gvpinfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    default:
                        ((TextBox)this.gvpinfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvpinfo.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvpinfo.Rows[i].FindControl("ddlvalcom")).Items.Clear();
                        ((DropDownList)this.gvpinfo.Rows[i].FindControl("ddlvalcom")).Visible = false;
                        ((DropDownList)this.gvpinfo.Rows[i].FindControl("ddlvalpros")).Items.Clear();
                        ((DropDownList)this.gvpinfo.Rows[i].FindControl("ddlvalpros")).Visible = false;
                        ((Panel)this.gvpinfo.Rows[i].FindControl("pnlMullocation")).Visible = false;
                        ((ListBox)this.gvpinfo.Rows[i].FindControl("lstlocation")).Items.Clear();
                        break;

                }

            }


        }
        protected void gvpinfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lgcResDesc = (Label)e.Row.FindControl("lgcResDesc1");
                string gcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "gcod")).ToString();
                string gdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "gdesc")).ToString();
                string comcod = this.GetComeCode();
                switch (comcod)
                {
                    case "3354"://Edison
                    case "3101":
                        if (gcod == "0303006")
                        {
                            lgcResDesc.Text = gdesc + "<span class='manField'><sup> *</sup></span>";
                        }
                        break;

                    default:
                        break;
                }

            }
        }
        private void ShowhomeInfo()
        {
            string comcod = this.GetComeCode();
            //string landid = (string)ViewState["sircodegrid"];
            DataTable dt = (DataTable)ViewState["tblDatagridhinfo"];
            if (dt == null)
                return;
            this.gvplot.DataSource = dt;
            this.gvplot.DataBind();
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            DataView dv1;
            DropDownList ddlgval;
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string gcod = dt.Rows[i]["gcod"].ToString();

                switch (gcod)
                {
                    case "0304001": //Country
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '52%' or gcod like '00%'");
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("pnldist")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvald")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvald")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("pnlz")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalz")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalz")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("pnlp")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalp")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalp")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("pnla")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvala")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvala")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("PanelBl")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlblock")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlblock")).Visible = false;

                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalplot"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.Items.Insert(0, new ListItem("--Please Select--", ""));
                        ddlgval.SelectedValue = ((TextBox)this.gvplot.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;



                    case "0304003": //District 
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '53%' or gcod like '00%'");
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalplot")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalplot")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("pnlz")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalz")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalz")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("pnlp")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalp")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalp")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("pnla")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvala")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvala")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("PanelBl")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlblock")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlblock")).Visible = false;

                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvald"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.Items.Insert(0, new ListItem("--Please Select--", ""));
                        ddlgval.SelectedValue = ((TextBox)this.gvplot.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;


                    case "0304005": //Zone

                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '54%' or gcod like '00%'");
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalplot")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalplot")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("pnldist")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvald")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvald")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("pnlp")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalp")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalp")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("pnla")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvala")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvala")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("PanelBl")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlblock")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlblock")).Visible = false;

                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalz"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.Items.Insert(0, new ListItem("--Please Select--", ""));
                        ddlgval.SelectedValue = ((TextBox)this.gvplot.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    case "0304007": //Police Station                    
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '55%' or gcod like '00%'");
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalplot")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalplot")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("pnldist")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvald")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvald")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("pnlz")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalz")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalz")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("pnla")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvala")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvala")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("PanelBl")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlblock")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlblock")).Visible = false;


                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalp"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.Items.Insert(0, new ListItem("--Please Select--", ""));
                        ddlgval.SelectedValue = ((TextBox)this.gvplot.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    case "0304011": //Area

                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '57%' or gcod like '00%'");
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalplot")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalplot")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("pnldist")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvald")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvald")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("pnlz")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalz")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalz")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("pnlp")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalp")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalp")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("PanelBl")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlblock")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlblock")).Visible = false;


                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvala"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.Items.Insert(0, new ListItem("--Please Select--", ""));
                        ddlgval.SelectedValue = ((TextBox)this.gvplot.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    case "0304009": //Block

                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '56%' or gcod like '00%'");
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalplot")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalplot")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("pnldist")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvald")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvald")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("pnlz")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalz")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalz")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("pnlp")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalp")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalp")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("pnla")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvala")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvala")).Visible = false;
                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlblock"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.Items.Insert(0, new ListItem("--Please Select--", ""));
                        ddlgval.SelectedValue = ((TextBox)this.gvplot.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;

                    default:
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalplot")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalplot")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("pnldist")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvald")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvald")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("pnlz")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalz")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalz")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("pnlp")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalp")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalp")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("pnla")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvala")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvala")).Visible = false;
                        ((Panel)this.gvplot.Rows[i].FindControl("PanelBl")).Visible = false;
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlblock")).Items.Clear();
                        ((DropDownList)this.gvplot.Rows[i].FindControl("ddlblock")).Visible = false;
                        break;

                }

            }


        }
        protected void ddlvalplot_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblDatagridhinfo"];
            if (dt == null)
                return;
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            DropDownList ddlgval;

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string gcod = dt.Rows[i]["gcod"].ToString();

                switch (gcod)
                {
                    case "0304003": //District   
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string country = ((DropDownList)this.gvplot.Rows[i - 1].FindControl("ddlvalplot")).Text.Trim();
                        DataView dv2;
                        dv2 = dt1.DefaultView;
                        dv2.RowFilter = ("code='" + country + "'");
                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvald"));

                        if (country == "" || country == "0" || dv2.ToTable().Rows.Count == 0)
                        {
                            ddlgval.Items.Clear();
                            ddlgval.DataSource = null;
                            ddlgval.DataBind();
                            ddlgval.Items.Insert(0, new ListItem("--Add Code to Continue--", "0"));
                        }
                        else
                        {
                            ddlgval.DataTextField = "gdesc";
                            ddlgval.DataValueField = "gcod";
                            ddlgval.DataSource = dv2.ToTable();
                            ddlgval.DataBind();

                        }

                        break;
                    case "0304005": //Zone

                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string district = ((DropDownList)this.gvplot.Rows[i - 1].FindControl("ddlvald")).Text.Trim();

                        DataView dv3;
                        dv3 = dt1.DefaultView;
                        dv3.RowFilter = ("code='" + district + "'");

                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalz"));
                        if (district == "" || district == "0" || dv3.ToTable().Rows.Count == 0)
                        {
                            ddlgval.Items.Clear();
                            ddlgval.DataSource = null;
                            ddlgval.DataBind();
                            ddlgval.Items.Insert(0, new ListItem("--Add Code to Continue--", "0"));
                        }
                        else
                        {
                            ddlgval.DataTextField = "gdesc";
                            ddlgval.DataValueField = "gcod";
                            ddlgval.DataSource = dv3.ToTable();
                            ddlgval.DataBind();

                        }
                        break;
                    case "0304007": //Police Station                    
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string zone = ((DropDownList)this.gvplot.Rows[i - 1].FindControl("ddlvalz")).Text.Trim();



                        DataView dv4;
                        dv4 = dt1.DefaultView;
                        dv4.RowFilter = ("code='" + zone + "'");

                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalp"));
                        if (zone == "" || zone == "0" || dv4.ToTable().Rows.Count == 0)
                        {
                            ddlgval.Items.Clear();
                            ddlgval.DataSource = null;
                            ddlgval.DataBind();
                            ddlgval.Items.Insert(0, new ListItem("--Add Code to Continue--", "0"));
                        }
                        else
                        {
                            ddlgval.DataTextField = "gdesc";
                            ddlgval.DataValueField = "gcod";
                            ddlgval.DataSource = dv4.ToTable();
                            ddlgval.DataBind();

                        }

                        break;
                    case "0304009": //Block

                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string ps = ((DropDownList)this.gvplot.Rows[i - 1].FindControl("ddlvalp")).Text.Trim();
                        DataView dv5;
                        dv5 = dt1.DefaultView;
                        dv5.RowFilter = ("code='" + ps + "'");
                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlblock"));
                        if (ps == "" || ps == "0" || dv5.ToTable().Rows.Count == 0)
                        {
                            ddlgval.Items.Clear();
                            ddlgval.DataSource = null;
                            ddlgval.DataBind();
                            ddlgval.Items.Insert(0, new ListItem("--Add Code to Continue--", "0"));
                        }
                        else
                        {
                            ddlgval.DataTextField = "gdesc";
                            ddlgval.DataValueField = "gcod";
                            ddlgval.DataSource = dv5.ToTable();
                            ddlgval.DataBind();

                        }
                        break;
                    case "0304011": //Area

                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string area = ((DropDownList)this.gvplot.Rows[i - 1].FindControl("ddlblock")).Text.Trim();
                        DataView dv6;
                        dv6 = dt1.DefaultView;
                        dv6.RowFilter = ("code='" + area + "'");
                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvala"));
                        if (area == "" || area == "0" || dv6.ToTable().Rows.Count == 0)
                        {
                            ddlgval.Items.Clear();
                            ddlgval.DataSource = null;
                            ddlgval.DataBind();
                            ddlgval.Items.Insert(0, new ListItem("--Add Code to Continue--", "0"));
                        }
                        else
                        {
                            ddlgval.DataTextField = "gdesc";
                            ddlgval.DataValueField = "gcod";
                            ddlgval.DataSource = dv6.ToTable();
                            ddlgval.DataBind();
                        }



                        break;
                }
            }
        }
        protected void ddlvald_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblDatagridhinfo"];
            if (dt == null)
                return;
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            DropDownList ddlgval;

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string gcod = dt.Rows[i]["gcod"].ToString();

                switch (gcod)
                {
                    case "0304005": //Zone

                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string district = ((DropDownList)this.gvplot.Rows[i - 1].FindControl("ddlvald")).Text.Trim();

                        DataView dv3;
                        dv3 = dt1.DefaultView;
                        dv3.RowFilter = ("code='" + district + "'");

                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalz"));
                        if (district == "" || district == "0" || dv3.ToTable().Rows.Count == 0)
                        {
                            ddlgval.Items.Clear();
                            ddlgval.DataSource = null;
                            ddlgval.DataBind();
                            ddlgval.Items.Insert(0, new ListItem("--Add Code to Continue--", "0"));
                        }
                        else
                        {
                            ddlgval.DataTextField = "gdesc";
                            ddlgval.DataValueField = "gcod";
                            ddlgval.DataSource = dv3.ToTable();
                            ddlgval.DataBind();

                        }
                        break;
                    case "0304007": //Police Station                    
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string zone = ((DropDownList)this.gvplot.Rows[i - 1].FindControl("ddlvalz")).Text.Trim();



                        DataView dv4;
                        dv4 = dt1.DefaultView;
                        dv4.RowFilter = ("code='" + zone + "'");

                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalp"));
                        if (zone == "" || zone == "0" || dv4.ToTable().Rows.Count == 0)
                        {
                            ddlgval.Items.Clear();
                            ddlgval.DataSource = null;
                            ddlgval.DataBind();
                            ddlgval.Items.Insert(0, new ListItem("--Add Code to Continue--", "0"));
                        }
                        else
                        {
                            ddlgval.DataTextField = "gdesc";
                            ddlgval.DataValueField = "gcod";
                            ddlgval.DataSource = dv4.ToTable();
                            ddlgval.DataBind();

                        }

                        break;
                    case "0304009": //Block

                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string ps = ((DropDownList)this.gvplot.Rows[i - 1].FindControl("ddlvalp")).Text.Trim();
                        DataView dv5;
                        dv5 = dt1.DefaultView;
                        dv5.RowFilter = ("code='" + ps + "'");
                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlblock"));
                        if (ps == "" || ps == "0" || dv5.ToTable().Rows.Count == 0)
                        {
                            ddlgval.Items.Clear();
                            ddlgval.DataSource = null;
                            ddlgval.DataBind();
                            ddlgval.Items.Insert(0, new ListItem("--Add Code to Continue--", "0"));
                        }
                        else
                        {
                            ddlgval.DataTextField = "gdesc";
                            ddlgval.DataValueField = "gcod";
                            ddlgval.DataSource = dv5.ToTable();
                            ddlgval.DataBind();

                        }
                        break;
                    case "0304011": //Area

                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string area = ((DropDownList)this.gvplot.Rows[i - 1].FindControl("ddlblock")).Text.Trim();
                        DataView dv6;
                        dv6 = dt1.DefaultView;
                        dv6.RowFilter = ("code='" + area + "'");
                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvala"));
                        if (area == "" || area == "0" || dv6.ToTable().Rows.Count == 0)
                        {
                            ddlgval.Items.Clear();
                            ddlgval.DataSource = null;
                            ddlgval.DataBind();
                            ddlgval.Items.Insert(0, new ListItem("--Add Code to Continue--", "0"));
                        }
                        else
                        {
                            ddlgval.DataTextField = "gdesc";
                            ddlgval.DataValueField = "gcod";
                            ddlgval.DataSource = dv6.ToTable();
                            ddlgval.DataBind();
                        }



                        break;

                }
            }
        }
        protected void ddlvalz_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblDatagridhinfo"];
            if (dt == null)
                return;
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            DropDownList ddlgval;

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string gcod = dt.Rows[i]["gcod"].ToString();

                switch (gcod)
                {
                    case "0304007": //Police Station                    
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string zone = ((DropDownList)this.gvplot.Rows[i - 1].FindControl("ddlvalz")).Text.Trim();



                        DataView dv4;
                        dv4 = dt1.DefaultView;
                        dv4.RowFilter = ("code='" + zone + "'");

                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalp"));
                        if (zone == "" || zone == "0" || dv4.ToTable().Rows.Count == 0)
                        {
                            ddlgval.Items.Clear();
                            ddlgval.DataSource = null;
                            ddlgval.DataBind();
                            ddlgval.Items.Insert(0, new ListItem("--Add Code to Continue--", "0"));
                        }
                        else
                        {
                            ddlgval.DataTextField = "gdesc";
                            ddlgval.DataValueField = "gcod";
                            ddlgval.DataSource = dv4.ToTable();
                            ddlgval.DataBind();

                        }

                        break;
                    case "0304009": //Block

                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string ps = ((DropDownList)this.gvplot.Rows[i - 1].FindControl("ddlvalp")).Text.Trim();
                        DataView dv5;
                        dv5 = dt1.DefaultView;
                        dv5.RowFilter = ("code='" + ps + "'");
                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlblock"));
                        if (ps == "" || ps == "0" || dv5.ToTable().Rows.Count == 0)
                        {
                            ddlgval.Items.Clear();
                            ddlgval.DataSource = null;
                            ddlgval.DataBind();
                            ddlgval.Items.Insert(0, new ListItem("--Add Code to Continue--", "0"));
                        }
                        else
                        {
                            ddlgval.DataTextField = "gdesc";
                            ddlgval.DataValueField = "gcod";
                            ddlgval.DataSource = dv5.ToTable();
                            ddlgval.DataBind();

                        }
                        break;
                    case "0304011": //Area

                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string area = ((DropDownList)this.gvplot.Rows[i - 1].FindControl("ddlblock")).Text.Trim();
                        DataView dv6;
                        dv6 = dt1.DefaultView;
                        dv6.RowFilter = ("code='" + area + "'");
                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvala"));
                        if (area == "" || area == "0" || dv6.ToTable().Rows.Count == 0)
                        {
                            ddlgval.Items.Clear();
                            ddlgval.DataSource = null;
                            ddlgval.DataBind();
                            ddlgval.Items.Insert(0, new ListItem("--Add Code to Continue--", "0"));
                        }
                        else
                        {
                            ddlgval.DataTextField = "gdesc";
                            ddlgval.DataValueField = "gcod";
                            ddlgval.DataSource = dv6.ToTable();
                            ddlgval.DataBind();
                        }



                        break;

                }
            }
        }
        protected void ddlvalp_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblDatagridhinfo"];
            if (dt == null)
                return;

            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            DropDownList ddlgval;

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string gcod = dt.Rows[i]["gcod"].ToString();

                switch (gcod)
                {
                    case "0304009": //Block

                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string ps = ((DropDownList)this.gvplot.Rows[i - 1].FindControl("ddlvalp")).Text.Trim();
                        DataView dv5;
                        dv5 = dt1.DefaultView;
                        dv5.RowFilter = ("code='" + ps + "'");
                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlblock"));
                        if (ps == "" || ps == "0" || dv5.ToTable().Rows.Count == 0)
                        {
                            ddlgval.Items.Clear();
                            ddlgval.DataSource = null;
                            ddlgval.DataBind();
                            ddlgval.Items.Insert(0, new ListItem("--Add Code to Continue--", "0"));
                        }
                        else
                        {
                            ddlgval.DataTextField = "gdesc";
                            ddlgval.DataValueField = "gcod";
                            ddlgval.DataSource = dv5.ToTable();
                            ddlgval.DataBind();

                        }
                        break;
                    case "0304011": //Area

                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string area = ((DropDownList)this.gvplot.Rows[i - 1].FindControl("ddlblock")).Text.Trim();
                        DataView dv6;
                        dv6 = dt1.DefaultView;
                        dv6.RowFilter = ("code='" + area + "'");
                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvala"));
                        if (area == "" || area == "0" || dv6.ToTable().Rows.Count == 0)
                        {
                            ddlgval.Items.Clear();
                            ddlgval.DataSource = null;
                            ddlgval.DataBind();
                            ddlgval.Items.Insert(0, new ListItem("--Add Code to Continue--", "0"));
                        }
                        else
                        {
                            ddlgval.DataTextField = "gdesc";
                            ddlgval.DataValueField = "gcod";
                            ddlgval.DataSource = dv6.ToTable();
                            ddlgval.DataBind();
                        }

                        break;

                }
            }
        }
        protected void ddlblock_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblDatagridhinfo"];
            if (dt == null)
                return;
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            DropDownList ddlgval;

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string gcod = dt.Rows[i]["gcod"].ToString();

                switch (gcod)
                {

                    case "0304011": //Area

                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvplot.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string area = ((DropDownList)this.gvplot.Rows[i - 1].FindControl("ddlblock")).Text.Trim();
                        DataView dv6;
                        dv6 = dt1.DefaultView;
                        dv6.RowFilter = ("code='" + area + "'");
                        ddlgval = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvala"));
                        if (area == "" || area == "0" || dv6.ToTable().Rows.Count == 0)
                        {
                            ddlgval.Items.Clear();
                            ddlgval.DataSource = null;
                            ddlgval.DataBind();
                            ddlgval.Items.Insert(0, new ListItem("--Add Code to Continue--", "0"));
                        }
                        else
                        {
                            ddlgval.DataTextField = "gdesc";
                            ddlgval.DataValueField = "gcod";
                            ddlgval.DataSource = dv6.ToTable();
                            ddlgval.DataBind();
                        }

                        break;
                }
            }
        }
        private void Showbusinfo()
        {
            string comcod = this.GetComeCode();
            DataTable dt = (DataTable)ViewState["tblDatagridbusinfo"];
            if (dt == null)
                return;
            this.gvbusinfo.DataSource = dt;
            this.gvbusinfo.DataBind();
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            DataView dv1;
            DropDownList ddlgval;
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string gcod = dt.Rows[i]["gcod"].ToString();

                switch (gcod)
                {
                    case "0305001": //Country
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '52%' or gcod like '00%'");
                        ((TextBox)this.gvbusinfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvbusinfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvbusinfo.Rows[i].FindControl("pnldist")).Visible = false;
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvald")).Items.Clear();
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvald")).Visible = false;
                        ((Panel)this.gvbusinfo.Rows[i].FindControl("pnlz")).Visible = false;
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvalz")).Items.Clear();
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvalz")).Visible = false;
                        ((Panel)this.gvbusinfo.Rows[i].FindControl("pnlp")).Visible = false;
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvalp")).Items.Clear();
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvalp")).Visible = false;
                        ((Panel)this.gvbusinfo.Rows[i].FindControl("pnla")).Visible = false;
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvala")).Items.Clear();
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvala")).Visible = false;
                        ((Panel)this.gvbusinfo.Rows[i].FindControl("PanelBl")).Visible = false;
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlblock")).Items.Clear();
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlblock")).Visible = false;

                        ddlgval = ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvalplot"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.Items.Insert(0, new ListItem("--Please Select--", ""));
                        ddlgval.SelectedValue = ((TextBox)this.gvbusinfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;



                    case "0305003": //District 
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '53%' or gcod like '00%'");
                        ((TextBox)this.gvbusinfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvbusinfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvbusinfo.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvalplot")).Items.Clear();
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvalplot")).Visible = false;
                        ((Panel)this.gvbusinfo.Rows[i].FindControl("pnlz")).Visible = false;
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvalz")).Items.Clear();
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvalz")).Visible = false;
                        ((Panel)this.gvbusinfo.Rows[i].FindControl("pnlp")).Visible = false;
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvalp")).Items.Clear();
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvalp")).Visible = false;
                        ((Panel)this.gvbusinfo.Rows[i].FindControl("pnla")).Visible = false;
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvala")).Items.Clear();
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvala")).Visible = false;
                        ((Panel)this.gvbusinfo.Rows[i].FindControl("PanelBl")).Visible = false;
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlblock")).Items.Clear();
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlblock")).Visible = false;

                        ddlgval = ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvald"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.Items.Insert(0, new ListItem("--Please Select--", ""));
                        ddlgval.SelectedValue = ((TextBox)this.gvbusinfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;


                    case "0305005": //Zone

                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '54%' or gcod like '00%'");
                        ((TextBox)this.gvbusinfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvbusinfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvbusinfo.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvalplot")).Items.Clear();
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvalplot")).Visible = false;
                        ((Panel)this.gvbusinfo.Rows[i].FindControl("pnldist")).Visible = false;
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvald")).Items.Clear();
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvald")).Visible = false;
                        ((Panel)this.gvbusinfo.Rows[i].FindControl("pnlp")).Visible = false;
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvalp")).Items.Clear();
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvalp")).Visible = false;
                        ((Panel)this.gvbusinfo.Rows[i].FindControl("pnla")).Visible = false;
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvala")).Items.Clear();
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvala")).Visible = false;
                        ((Panel)this.gvbusinfo.Rows[i].FindControl("PanelBl")).Visible = false;
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlblock")).Items.Clear();
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlblock")).Visible = false;

                        ddlgval = ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvalz"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.Items.Insert(0, new ListItem("--Please Select--", ""));
                        ddlgval.SelectedValue = ((TextBox)this.gvbusinfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    case "0305007": //Police Station                    
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '55%' or gcod like '00%'");
                        ((TextBox)this.gvbusinfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvbusinfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvbusinfo.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvalplot")).Items.Clear();
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvalplot")).Visible = false;
                        ((Panel)this.gvbusinfo.Rows[i].FindControl("pnldist")).Visible = false;
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvald")).Items.Clear();
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvald")).Visible = false;
                        ((Panel)this.gvbusinfo.Rows[i].FindControl("pnlz")).Visible = false;
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvalz")).Items.Clear();
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvalz")).Visible = false;
                        ((Panel)this.gvbusinfo.Rows[i].FindControl("pnla")).Visible = false;
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvala")).Items.Clear();
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvala")).Visible = false;
                        ((Panel)this.gvbusinfo.Rows[i].FindControl("PanelBl")).Visible = false;
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlblock")).Items.Clear();
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlblock")).Visible = false;


                        ddlgval = ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvalp"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.Items.Insert(0, new ListItem("--Please Select--", ""));
                        ddlgval.SelectedValue = ((TextBox)this.gvbusinfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    case "0305011": //Area

                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '57%' or gcod like '00%'");
                        ((TextBox)this.gvbusinfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvbusinfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvbusinfo.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvalplot")).Items.Clear();
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvalplot")).Visible = false;
                        ((Panel)this.gvbusinfo.Rows[i].FindControl("pnldist")).Visible = false;
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvald")).Items.Clear();
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvald")).Visible = false;
                        ((Panel)this.gvbusinfo.Rows[i].FindControl("pnlz")).Visible = false;
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvalz")).Items.Clear();
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvalz")).Visible = false;
                        ((Panel)this.gvbusinfo.Rows[i].FindControl("pnlp")).Visible = false;
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvalp")).Items.Clear();
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvalp")).Visible = false;
                        ((Panel)this.gvbusinfo.Rows[i].FindControl("PanelBl")).Visible = false;
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlblock")).Items.Clear();
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlblock")).Visible = false;


                        ddlgval = ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvala"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.Items.Insert(0, new ListItem("--Please Select--", ""));
                        ddlgval.SelectedValue = ((TextBox)this.gvbusinfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    case "0305009": //Block

                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '56%' or gcod like '00%'");
                        ((TextBox)this.gvbusinfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvbusinfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvbusinfo.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvalplot")).Items.Clear();
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvalplot")).Visible = false;
                        ((Panel)this.gvbusinfo.Rows[i].FindControl("pnldist")).Visible = false;
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvald")).Items.Clear();
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvald")).Visible = false;
                        ((Panel)this.gvbusinfo.Rows[i].FindControl("pnlz")).Visible = false;
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvalz")).Items.Clear();
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvalz")).Visible = false;
                        ((Panel)this.gvbusinfo.Rows[i].FindControl("pnlp")).Visible = false;
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvalp")).Items.Clear();
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvalp")).Visible = false;
                        ((Panel)this.gvbusinfo.Rows[i].FindControl("pnla")).Visible = false;
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvala")).Items.Clear();
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvala")).Visible = false;
                        ddlgval = ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlblock"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.Items.Insert(0, new ListItem("--Please Select--", ""));
                        ddlgval.SelectedValue = ((TextBox)this.gvbusinfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;

                    default:
                        ((TextBox)this.gvbusinfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvbusinfo.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvalplot")).Items.Clear();
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvalplot")).Visible = false;
                        ((Panel)this.gvbusinfo.Rows[i].FindControl("pnldist")).Visible = false;
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvald")).Items.Clear();
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvald")).Visible = false;
                        ((Panel)this.gvbusinfo.Rows[i].FindControl("pnlz")).Visible = false;
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvalz")).Items.Clear();
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvalz")).Visible = false;
                        ((Panel)this.gvbusinfo.Rows[i].FindControl("pnlp")).Visible = false;
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvalp")).Items.Clear();
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvalp")).Visible = false;
                        ((Panel)this.gvbusinfo.Rows[i].FindControl("pnla")).Visible = false;
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvala")).Items.Clear();
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvala")).Visible = false;
                        ((Panel)this.gvbusinfo.Rows[i].FindControl("PanelBl")).Visible = false;
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlblock")).Items.Clear();
                        ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlblock")).Visible = false;
                        break;

                }

            }
        }
        protected void ddlvalbusinfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblDatagridbusinfo"];
            if (dt == null)
                return;
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            DropDownList ddlgval;

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string gcod = dt.Rows[i]["gcod"].ToString();

                switch (gcod)
                {
                    case "0305003": //District   
                        ((TextBox)this.gvbusinfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvbusinfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string country = ((DropDownList)this.gvbusinfo.Rows[i - 1].FindControl("ddlvalplot")).Text.Trim();
                        DataView dv2;
                        dv2 = dt1.DefaultView;
                        dv2.RowFilter = ("code='" + country + "'");
                        ddlgval = ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvald"));
                        if (country == "" || country == "0" || dv2.ToTable().Rows.Count == 0)
                        {
                            ddlgval.Items.Clear();
                            ddlgval.DataSource = null;
                            ddlgval.DataBind();
                            ddlgval.Items.Insert(0, new ListItem("--Add Code to Continue--", "0"));
                        }
                        else
                        {
                            ddlgval.DataTextField = "gdesc";
                            ddlgval.DataValueField = "gcod";
                            ddlgval.DataSource = dv2.ToTable();
                            ddlgval.DataBind();

                        }
                        break;
                    case "0305005": //Zone

                        ((TextBox)this.gvbusinfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvbusinfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string district = ((DropDownList)this.gvbusinfo.Rows[i - 1].FindControl("ddlvald")).Text.Trim();

                        DataView dv3;
                        dv3 = dt1.DefaultView;
                        dv3.RowFilter = ("code='" + district + "'");

                        ddlgval = ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvalz"));
                        if (district == "" || district == "0" || dv3.ToTable().Rows.Count == 0)
                        {
                            ddlgval.Items.Clear();
                            ddlgval.DataSource = null;
                            ddlgval.DataBind();
                            ddlgval.Items.Insert(0, new ListItem("--Add Code to Continue--", "0"));
                        }
                        else
                        {
                            ddlgval.DataTextField = "gdesc";
                            ddlgval.DataValueField = "gcod";
                            ddlgval.DataSource = dv3.ToTable();
                            ddlgval.DataBind();

                        }

                        break;
                    case "0305007": //Police Station                    
                        ((TextBox)this.gvbusinfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvbusinfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string zone = ((DropDownList)this.gvbusinfo.Rows[i - 1].FindControl("ddlvalz")).Text.Trim();
                        DataView dv4;
                        dv4 = dt1.DefaultView;
                        dv4.RowFilter = ("code='" + zone + "'");

                        ddlgval = ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvalp"));
                        if (zone == "" || zone == "0" || dv4.ToTable().Rows.Count == 0)
                        {
                            ddlgval.Items.Clear();
                            ddlgval.DataSource = null;
                            ddlgval.DataBind();
                            ddlgval.Items.Insert(0, new ListItem("--Add Code to Continue--", "0"));
                        }
                        else
                        {
                            ddlgval.DataTextField = "gdesc";
                            ddlgval.DataValueField = "gcod";
                            ddlgval.DataSource = dv4.ToTable();
                            ddlgval.DataBind();

                        }

                        break;
                    case "0305009": //Block

                        ((TextBox)this.gvbusinfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvbusinfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string ps = ((DropDownList)this.gvbusinfo.Rows[i - 1].FindControl("ddlvalp")).Text.Trim();
                        DataView dv5;
                        dv5 = dt1.DefaultView;
                        dv5.RowFilter = ("code='" + ps + "'");

                        ddlgval = ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlblock"));
                        if (ps == "" || ps == "0" || dv5.ToTable().Rows.Count == 0)
                        {
                            ddlgval.Items.Clear();
                            ddlgval.DataSource = null;
                            ddlgval.DataBind();
                            ddlgval.Items.Insert(0, new ListItem("--Add Code to Continue--", "0"));
                        }
                        else
                        {
                            ddlgval.DataTextField = "gdesc";
                            ddlgval.DataValueField = "gcod";
                            ddlgval.DataSource = dv5.ToTable();
                            ddlgval.DataBind();

                        }

                        break;
                    case "0305011": //Area

                        ((TextBox)this.gvbusinfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvbusinfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string area = ((DropDownList)this.gvbusinfo.Rows[i - 1].FindControl("ddlblock")).Text.Trim();
                        DataView dv6;
                        dv6 = dt1.DefaultView;
                        dv6.RowFilter = ("code='" + area + "'");

                        ddlgval = ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvala"));
                        if (area == "" || area == "0" || dv6.ToTable().Rows.Count == 0)
                        {
                            ddlgval.Items.Clear();
                            ddlgval.DataSource = null;
                            ddlgval.DataBind();
                            ddlgval.Items.Insert(0, new ListItem("--Add Code to Continue--", "0"));
                        }
                        else
                        {
                            ddlgval.DataTextField = "gdesc";
                            ddlgval.DataValueField = "gcod";
                            ddlgval.DataSource = dv6.ToTable();
                            ddlgval.DataBind();
                        }


                        break;


                }
            }
        }
        protected void ddlvaldbusinfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblDatagridbusinfo"];
            if (dt == null)
                return;
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            DropDownList ddlgval;

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string gcod = dt.Rows[i]["gcod"].ToString();

                switch (gcod)
                {
                    case "0305005": //Zone

                        ((TextBox)this.gvbusinfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvbusinfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string district = ((DropDownList)this.gvbusinfo.Rows[i - 1].FindControl("ddlvald")).Text.Trim();

                        DataView dv3;
                        dv3 = dt1.DefaultView;
                        dv3.RowFilter = ("code='" + district + "'");

                        ddlgval = ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvalz"));
                        if (district == "" || district == "0" || dv3.ToTable().Rows.Count == 0)
                        {
                            ddlgval.Items.Clear();
                            ddlgval.DataSource = null;
                            ddlgval.DataBind();
                            ddlgval.Items.Insert(0, new ListItem("--Add Code to Continue--", "0"));
                        }
                        else
                        {
                            ddlgval.DataTextField = "gdesc";
                            ddlgval.DataValueField = "gcod";
                            ddlgval.DataSource = dv3.ToTable();
                            ddlgval.DataBind();

                        }

                        break;
                    case "0305007": //Police Station                    
                        ((TextBox)this.gvbusinfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvbusinfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string zone = ((DropDownList)this.gvbusinfo.Rows[i - 1].FindControl("ddlvalz")).Text.Trim();
                        DataView dv4;
                        dv4 = dt1.DefaultView;
                        dv4.RowFilter = ("code='" + zone + "'");

                        ddlgval = ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvalp"));
                        if (zone == "" || zone == "0" || dv4.ToTable().Rows.Count == 0)
                        {
                            ddlgval.Items.Clear();
                            ddlgval.DataSource = null;
                            ddlgval.DataBind();
                            ddlgval.Items.Insert(0, new ListItem("--Add Code to Continue--", "0"));
                        }
                        else
                        {
                            ddlgval.DataTextField = "gdesc";
                            ddlgval.DataValueField = "gcod";
                            ddlgval.DataSource = dv4.ToTable();
                            ddlgval.DataBind();

                        }

                        break;
                    case "0305009": //Block

                        ((TextBox)this.gvbusinfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvbusinfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string ps = ((DropDownList)this.gvbusinfo.Rows[i - 1].FindControl("ddlvalp")).Text.Trim();
                        DataView dv5;
                        dv5 = dt1.DefaultView;
                        dv5.RowFilter = ("code='" + ps + "'");

                        ddlgval = ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlblock"));
                        if (ps == "" || ps == "0" || dv5.ToTable().Rows.Count == 0)
                        {
                            ddlgval.Items.Clear();
                            ddlgval.DataSource = null;
                            ddlgval.DataBind();
                            ddlgval.Items.Insert(0, new ListItem("--Add Code to Continue--", "0"));
                        }
                        else
                        {
                            ddlgval.DataTextField = "gdesc";
                            ddlgval.DataValueField = "gcod";
                            ddlgval.DataSource = dv5.ToTable();
                            ddlgval.DataBind();

                        }

                        break;
                    case "0305011": //Area

                        ((TextBox)this.gvbusinfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvbusinfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string area = ((DropDownList)this.gvbusinfo.Rows[i - 1].FindControl("ddlblock")).Text.Trim();
                        DataView dv6;
                        dv6 = dt1.DefaultView;
                        dv6.RowFilter = ("code='" + area + "'");

                        ddlgval = ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvala"));
                        if (area == "" || area == "0" || dv6.ToTable().Rows.Count == 0)
                        {
                            ddlgval.Items.Clear();
                            ddlgval.DataSource = null;
                            ddlgval.DataBind();
                            ddlgval.Items.Insert(0, new ListItem("--Add Code to Continue--", "0"));
                        }
                        else
                        {
                            ddlgval.DataTextField = "gdesc";
                            ddlgval.DataValueField = "gcod";
                            ddlgval.DataSource = dv6.ToTable();
                            ddlgval.DataBind();
                        }


                        break;

                }
            }
        }
        protected void ddlvalzbusinfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblDatagridbusinfo"];
            if (dt == null)
                return;
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            DropDownList ddlgval;

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string gcod = dt.Rows[i]["gcod"].ToString();

                switch (gcod)
                {
                    case "0305007": //Police Station                    
                        ((TextBox)this.gvbusinfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvbusinfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string zone = ((DropDownList)this.gvbusinfo.Rows[i - 1].FindControl("ddlvalz")).Text.Trim();
                        DataView dv4;
                        dv4 = dt1.DefaultView;
                        dv4.RowFilter = ("code='" + zone + "'");

                        ddlgval = ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvalp"));
                        if (zone == "" || zone == "0" || dv4.ToTable().Rows.Count == 0)
                        {
                            ddlgval.Items.Clear();
                            ddlgval.DataSource = null;
                            ddlgval.DataBind();
                            ddlgval.Items.Insert(0, new ListItem("--Add Code to Continue--", "0"));
                        }
                        else
                        {
                            ddlgval.DataTextField = "gdesc";
                            ddlgval.DataValueField = "gcod";
                            ddlgval.DataSource = dv4.ToTable();
                            ddlgval.DataBind();

                        }

                        break;
                    case "0305009": //Block

                        ((TextBox)this.gvbusinfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvbusinfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string ps = ((DropDownList)this.gvbusinfo.Rows[i - 1].FindControl("ddlvalp")).Text.Trim();
                        DataView dv5;
                        dv5 = dt1.DefaultView;
                        dv5.RowFilter = ("code='" + ps + "'");

                        ddlgval = ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlblock"));
                        if (ps == "" || ps == "0" || dv5.ToTable().Rows.Count == 0)
                        {
                            ddlgval.Items.Clear();
                            ddlgval.DataSource = null;
                            ddlgval.DataBind();
                            ddlgval.Items.Insert(0, new ListItem("--Add Code to Continue--", "0"));
                        }
                        else
                        {
                            ddlgval.DataTextField = "gdesc";
                            ddlgval.DataValueField = "gcod";
                            ddlgval.DataSource = dv5.ToTable();
                            ddlgval.DataBind();

                        }

                        break;
                    case "0305011": //Area

                        ((TextBox)this.gvbusinfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvbusinfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string area = ((DropDownList)this.gvbusinfo.Rows[i - 1].FindControl("ddlblock")).Text.Trim();
                        DataView dv6;
                        dv6 = dt1.DefaultView;
                        dv6.RowFilter = ("code='" + area + "'");

                        ddlgval = ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvala"));
                        if (area == "" || area == "0" || dv6.ToTable().Rows.Count == 0)
                        {
                            ddlgval.Items.Clear();
                            ddlgval.DataSource = null;
                            ddlgval.DataBind();
                            ddlgval.Items.Insert(0, new ListItem("--Add Code to Continue--", "0"));
                        }
                        else
                        {
                            ddlgval.DataTextField = "gdesc";
                            ddlgval.DataValueField = "gcod";
                            ddlgval.DataSource = dv6.ToTable();
                            ddlgval.DataBind();
                        }


                        break;


                }
            }
        }
        protected void ddlvalpbusinfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblDatagridbusinfo"];
            if (dt == null)
                return;
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            DropDownList ddlgval;

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string gcod = dt.Rows[i]["gcod"].ToString();

                switch (gcod)
                {
                    case "0305009": //Block

                        ((TextBox)this.gvbusinfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvbusinfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string ps = ((DropDownList)this.gvbusinfo.Rows[i - 1].FindControl("ddlvalp")).Text.Trim();
                        DataView dv5;
                        dv5 = dt1.DefaultView;
                        dv5.RowFilter = ("code='" + ps + "'");

                        ddlgval = ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlblock"));
                        if (ps == "" || ps == "0" || dv5.ToTable().Rows.Count == 0)
                        {
                            ddlgval.Items.Clear();
                            ddlgval.DataSource = null;
                            ddlgval.DataBind();
                            ddlgval.Items.Insert(0, new ListItem("--Add Code to Continue--", "0"));
                        }
                        else
                        {
                            ddlgval.DataTextField = "gdesc";
                            ddlgval.DataValueField = "gcod";
                            ddlgval.DataSource = dv5.ToTable();
                            ddlgval.DataBind();

                        }

                        break;
                    case "0305011": //Area

                        ((TextBox)this.gvbusinfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvbusinfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string area = ((DropDownList)this.gvbusinfo.Rows[i - 1].FindControl("ddlblock")).Text.Trim();
                        DataView dv6;
                        dv6 = dt1.DefaultView;
                        dv6.RowFilter = ("code='" + area + "'");

                        ddlgval = ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvala"));
                        if (area == "" || area == "0" || dv6.ToTable().Rows.Count == 0)
                        {
                            ddlgval.Items.Clear();
                            ddlgval.DataSource = null;
                            ddlgval.DataBind();
                            ddlgval.Items.Insert(0, new ListItem("--Add Code to Continue--", "0"));
                        }
                        else
                        {
                            ddlgval.DataTextField = "gdesc";
                            ddlgval.DataValueField = "gcod";
                            ddlgval.DataSource = dv6.ToTable();
                            ddlgval.DataBind();
                        }


                        break;

                }
            }
        }
        protected void ddlblockbusinfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblDatagridbusinfo"];
            if (dt == null)
                return;
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            DropDownList ddlgval;

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string gcod = dt.Rows[i]["gcod"].ToString();

                switch (gcod)
                {

                    case "0305011": //Area

                        ((TextBox)this.gvbusinfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvbusinfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string area = ((DropDownList)this.gvbusinfo.Rows[i - 1].FindControl("ddlblock")).Text.Trim();
                        DataView dv6;
                        dv6 = dt1.DefaultView;
                        dv6.RowFilter = ("code='" + area + "'");

                        ddlgval = ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvala"));
                        if (area == "" || area == "0" || dv6.ToTable().Rows.Count == 0)
                        {
                            ddlgval.Items.Clear();
                            ddlgval.DataSource = null;
                            ddlgval.DataBind();
                            ddlgval.Items.Insert(0, new ListItem("--Add Code to Continue--", "0"));
                        }
                        else
                        {
                            ddlgval.DataTextField = "gdesc";
                            ddlgval.DataValueField = "gcod";
                            ddlgval.DataSource = dv6.ToTable();
                            ddlgval.DataBind();
                        }


                        break;

                }
            }
        }
        private void ShowMoreInfo()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];

            string comcod = this.GetComeCode();
            DataTable dt = (DataTable)ViewState["tblDatagridmoreinfo"];
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            DataTable dt2 = (DataTable)ViewState["tblstatus"];
            DataView dv1;
            if (dt == null)
                return;
            this.gvMoreInfo.DataSource = dt;
            this.gvMoreInfo.DataBind();
            //txtgvdVal.Attributes.Add("style", "display:none");
            DropDownList ddlgval;
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string gcod = dt.Rows[i]["gcod"].ToString();

                switch (gcod)
                {
                    case "0306001": //Home District
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '53%'");
                        ((TextBox)this.gvMoreInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvMoreInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvMoreInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.Items.Insert(0, new ListItem("--Please Select--", ""));
                        ddlgval.SelectedValue = ((TextBox)this.gvMoreInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    case "0306003": //Home District                   
                        ((TextBox)this.gvMoreInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((Panel)this.gvMoreInfo.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvMoreInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.gvMoreInfo.Rows[i].FindControl("ddlval")).Visible = false;
                        break;
                    case "0306005": //Home District                   
                        ((TextBox)this.gvMoreInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((Panel)this.gvMoreInfo.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvMoreInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.gvMoreInfo.Rows[i].FindControl("ddlval")).Visible = false;
                        break;
                    case "0306011": //Home District
                        dv1 = dt2.DefaultView;
                        dv1.RowFilter = ("gcod like '95%'");
                        ((TextBox)this.gvMoreInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvMoreInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvMoreInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.Items.Insert(0, new ListItem("--Please Select--", "="));
                        ddlgval.SelectedValue = ((TextBox)this.gvMoreInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;

                    default:
                        ((TextBox)this.gvMoreInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvMoreInfo.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvMoreInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.gvMoreInfo.Rows[i].FindControl("ddlval")).Visible = false;
                        break;

                }

            }



        }
        private bool GetComPanyProsActivein()
        {
            string comcod = this.GetComeCode();
            bool active;

            switch (comcod)
            {

                //case "3101": // Assure builders
                //case "3315": // Assure builders
                //case "3316":// Assure Development
                //    active = true;
                //    break;
                default:
                    active = true;
                    break;



            }

            return active;


        }
        private string GetNewId()
        {


            string comcod = this.GetComeCode();
            DataSet ds1 = instcrm.GetTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "CLNTNEWCODE", "", "", "", "", "", "", "", "", "");
            string clientid = ds1.Tables[0].Rows[0]["sircode"].ToString();
            ds1.Dispose();
            this.lblnewprospect.Value = clientid;
            return clientid;




        }
        protected void lnkUpdate_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string comcod = this.GetComeCode();

            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();
            DataTable dt4 = new DataTable();
            DataTable dt5 = new DataTable();
            DataTable dt6 = new DataTable();
            dt1.Clear();
            dt1.Columns.Add("gcod");
            dt1.Columns.Add("gval");
            dt1.Columns.Add("gvalue");
            dt1.Columns.Add("remarks");
            dt1.Columns.Add("ccc");
            string Name = "";
            string Phone = "";
            string CCC0 = ""; //Country Calling Code
            string CCC1 = ""; //Country Calling Code
            string CCC2 = ""; //Country Calling Code
            string altphone1 = "";
            string altphone2 = "";
            string email = "";
            string empid = "";
            string maddress = "";
            string faclass = "fa-exclamation-circle";
            string gval = "";
            string sourcecode = "";
            for (int i = 0; i < this.gvPersonalInfo.Rows.Count; i++)
            {
                DataRow dr = dt1.NewRow();
                string Gcode = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lblgvItmCodeper")).Text.Trim();
                gval = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lgvgval")).Text.Trim();
                if (Gcode == "0301001")
                {
                    Name = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();

                    if (Name.Trim().Length == 0)
                    {
                        string Message = "Name field is not empty";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Message + "');", true);


                        return;

                    }
                }

                if (Gcode == "0301003")
                {
                    Phone = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                    CCC0 = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcountryPhone")).SelectedValue.ToString();

                    if (Phone.Trim().Length == 0)
                    {
                        string Message = "Phone field is not empty";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Message + "');", true);

                        return;


                    }

                    string comcod1 = this.GetComeCode();

                    if (comcod1 == "3315" || comcod1 == "3316")
                    {

                    }

                    else
                    {
                        if (Phone.Trim().Length != 11 && CCC0 == "+88")
                        {
                            string Message = "Mobile Number Must be 11 digit";
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Message + "');", true);


                            return;


                        }

                    }



                }
                if (Gcode == "0301005")
                {
                    altphone2 = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                    CCC1 = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcountryPhone")).SelectedValue.ToString();

                    //if (Phone.Trim().Length > 11)
                    //{
                    //    ((Label)this.Master.FindControl("lblmsg")).Text = "Mobile Number Must be 11 digit";
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    //    return;
                    //}
                }
                if (Gcode == "0301004")
                {
                    altphone1 = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                    CCC2 = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcountryPhone")).SelectedValue.ToString();



                    //if (Phone.Trim().Length > 11)
                    //{
                    //    ((Label)this.Master.FindControl("lblmsg")).Text = "Mobile Number Must be 11 digit";
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    //    return;
                    //}
                }
                if (Gcode == "0301007")
                {
                    email = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();

                    //if (email.Trim().Length == 0)
                    //{

                    //    ((Label)this.Master.FindControl("lblmsg")).Text = "Email field is not empty";
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    //    return;

                    //}
                }



                //if (Gcode == "0301011")
                //{

                //    string profession = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).SelectedValue.ToString();

                //    if (profession.Trim().Length == 0)
                //    {

                //        ((Label)this.Master.FindControl("lblmsg")).Text = "Please Select profession field";
                //        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                //        return;

                //    }
                //}

                //if (Gcode == "0304021")
                //{
                //    maddress = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                //}

                dr["gcod"] = Gcode;
                dr["ccc"] = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlcountryPhone")).SelectedValue.ToString();
                dr["gval"] = gval;
                dr["gvalue"] = (((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Count == 0) ?
                    ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim() :
                    ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).SelectedValue.ToString();
                dt1.Rows.Add(dr);
            }

            for (int i = 0; i < this.gvSourceInfo.Rows.Count; i++)
            {
                DataRow dr = dt1.NewRow();
                string Gcode = ((Label)this.gvSourceInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                gval = ((Label)this.gvSourceInfo.Rows[i].FindControl("lgvgvalsr")).Text.Trim();
                if (Gcode == "0302003")
                {


                    string teamleader = ((DropDownList)this.gvSourceInfo.Rows[i].FindControl("ddlval")).SelectedValue.ToString();
                    if (teamleader.Trim().Length == 0)
                    {

                        string Message = "Please Select Team Leader";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Message + "');", true);
                        return;

                    }
                }

                if (Gcode == "0302005")
                {
                    empid = ((DropDownList)this.gvSourceInfo.Rows[i].FindControl("ddlval")).SelectedValue.ToString();
                }

                //Source
                if (Gcode == "0302001")
                {
                    sourcecode = ((DropDownList)this.gvSourceInfo.Rows[i].FindControl("ddlval")).SelectedValue.ToString();
                    if (sourcecode == "0000000")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Select Source!');", true);
                        return;
                    }
                }

                //Source Remarks
                if (Gcode == "0302002")
                {
                    switch (comcod)
                    {
                        //Edison
                        case "3354":
                            if (sourcecode == "3101004" || sourcecode == "3101005")
                            {
                                string sourceRemarks = ((TextBox)this.gvSourceInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                                if (sourceRemarks.Trim().Length == 0)
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Source Remarks is not Empty!');", true);
                                    return;
                                }
                            }
                            break;
                        //Epic
                        case "3367":
                            if (sourcecode == "3101010")
                            {
                                string sourceRemarks = ((TextBox)this.gvSourceInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                                if (sourceRemarks.Trim().Length == 0)
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('IR Reference is not Empty!');", true);
                                    return;
                                }
                            }
                            break;

                        default:
                            break;
                    }
                }


                dr["gcod"] = Gcode;
                dr["ccc"] = "";
                dr["gval"] = gval;
                dr["gvalue"] = (((DropDownList)this.gvSourceInfo.Rows[i].FindControl("ddlval")).Items.Count == 0) ? ((TextBox)this.gvSourceInfo.Rows[i].FindControl("txtgvVal")).Text.Trim() :
                    ((DropDownList)this.gvSourceInfo.Rows[i].FindControl("ddlval")).SelectedValue.ToString();
                dt1.Rows.Add(dr);
            }


            //Project Info
            for (int i = 0; i < this.gvpinfo.Rows.Count; i++)
            {
                DataRow dr = dt1.NewRow();
                string Gcode = ((Label)this.gvpinfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                gval = ((Label)this.gvpinfo.Rows[i].FindControl("lgvgvalpinf")).Text.Trim();

                dr["gcod"] = Gcode;
                dr["gval"] = gval;
                dr["ccc"] = "";

                //Mandatory
                if (Gcode == "0303006") //Interest Project
                {
                    switch (comcod)
                    {
                        case "3354"://Edison
                                    //  case "3101":
                            string ipactcode = ((DropDownList)this.gvpinfo.Rows[i].FindControl("ddlvalpros")).SelectedValue.ToString();
                            if (ipactcode.Length == 0)
                            {
                                string Message = "Please select Project!";
                                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Message + "');", true);
                                return;
                            }
                            break;

                        default:
                            break;
                    }
                }

                //Company
                if (Gcode == "0303002")
                {


                    dr["gvalue"] = (((DropDownList)this.gvpinfo.Rows[i].FindControl("ddlvalcom")).Items.Count == 0) ? "" : ((DropDownList)this.gvpinfo.Rows[i].FindControl("ddlvalcom")).SelectedValue.ToString();


                    //Gvalue = (((ListBox)this.gvInfo.Rows[i].FindControl("ddlPartic")).Items.Count == 0) ? ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Text.Trim()
                    //    : ((ListBox)this.gvInfo.Rows[i].FindControl("ddlPartic")).SelectedValue.ToString();
                }

                else if (Gcode == "0303005")
                {
                    string Gvalue = "";
                    string remarks = "";
                    //Gvalue == "";
                    foreach (ListItem item in ((ListBox)this.gvpinfo.Rows[i].FindControl("lstlocation")).Items)
                    {
                        if (item.Selected)
                        {

                            if (item.Selected)
                            {
                                Gvalue += item.Value;
                                remarks = remarks + item.Text + ", ";
                            }
                        }
                    }



                    dr["remarks"] = remarks.Length == 0 ? "" : remarks.Substring(0, remarks.Length - 2);
                    dr["gvalue"] = Gvalue;


                    //Gvalue = (((ListBox)this.gvInfo.Rows[i].FindControl("ddlPartic")).Items.Count == 0) ? ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Text.Trim()
                    //    : ((ListBox)this.gvInfo.Rows[i].FindControl("ddlPartic")).SelectedValue.ToString();
                }

                else
                {

                    dr["gvalue"] = (((DropDownList)this.gvpinfo.Rows[i].FindControl("ddlvalpros")).Items.Count == 0) ? ((TextBox)this.gvpinfo.Rows[i].FindControl("txtgvVal")).Text.Trim() : ((DropDownList)this.gvpinfo.Rows[i].FindControl("ddlvalpros")).SelectedValue.ToString();

                }

                dt1.Rows.Add(dr);

            }
            for (int i = 0; i < this.gvplot.Rows.Count; i++)
            {
                DataRow dr = dt1.NewRow();
                string Gcode = ((Label)this.gvplot.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                gval = ((Label)this.gvplot.Rows[i].FindControl("lgvgvalplot")).Text.Trim();
                dr["gcod"] = Gcode;
                dr["gval"] = gval;
                dr["ccc"] = "";

                if (Gcode == "0304001")
                {

                    dr["gvalue"] = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalplot")).SelectedValue.ToString();


                }
                else if (Gcode == "0304003")
                {

                    dr["gvalue"] = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvald")).SelectedValue.ToString();


                }
                else if (Gcode == "0304005")
                {

                    dr["gvalue"] = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalz")).SelectedValue.ToString();


                }
                else if (Gcode == "0304007")
                {

                    dr["gvalue"] = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvalp")).SelectedValue.ToString();


                }
                else if (Gcode == "0304009")
                {

                    dr["gvalue"] = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlblock")).SelectedValue.ToString();


                }
                else if (Gcode == "0304011")
                {

                    dr["gvalue"] = ((DropDownList)this.gvplot.Rows[i].FindControl("ddlvala")).SelectedValue.ToString();


                }

                else if (Gcode == "0304021")
                {
                    maddress = ((TextBox)this.gvplot.Rows[i].FindControl("txtgvVal")).Text.Trim();

                    //if (maddress.Trim().Length == 0)
                    //{

                    //    ((Label)this.Master.FindControl("lblmsg")).Text = "Mailing Address field is not empty";
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    //    return;

                    //}


                    dr["gvalue"] = ((TextBox)this.gvplot.Rows[i].FindControl("txtgvVal")).Text.Trim();
                }
                else
                {
                    dr["gvalue"] = ((TextBox)this.gvplot.Rows[i].FindControl("txtgvVal")).Text.Trim();
                }
                dt1.Rows.Add(dr);
            }
            for (int i = 0; i < this.gvbusinfo.Rows.Count; i++)
            {
                DataRow dr = dt1.NewRow();
                string Gcode = ((Label)this.gvbusinfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                gval = ((Label)this.gvbusinfo.Rows[i].FindControl("lgvgvalbuinf")).Text.Trim();
                dr["gcod"] = Gcode;
                dr["gval"] = gval;
                dr["ccc"] = "";

                if (Gcode == "0305001")
                {

                    dr["gvalue"] = ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvalplot")).SelectedValue.ToString();


                }
                else if (Gcode == "0305003")
                {

                    dr["gvalue"] = ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvald")).SelectedValue.ToString();


                }
                else if (Gcode == "0305005")
                {

                    dr["gvalue"] = ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvalz")).SelectedValue.ToString();


                }
                else if (Gcode == "0305007")
                {

                    dr["gvalue"] = ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvalp")).SelectedValue.ToString();


                }
                else if (Gcode == "0305009")
                {

                    dr["gvalue"] = ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlblock")).SelectedValue.ToString();


                }
                else if (Gcode == "0305011")
                {

                    dr["gvalue"] = ((DropDownList)this.gvbusinfo.Rows[i].FindControl("ddlvala")).SelectedValue.ToString();


                }
                else
                {
                    dr["gvalue"] = ((TextBox)this.gvbusinfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                }
                dt1.Rows.Add(dr);
            }

            for (int i = 0; i < this.gvMoreInfo.Rows.Count; i++)
            {
                DataRow dr = dt1.NewRow();
                string Gcode = ((Label)this.gvMoreInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                gval = ((Label)this.gvMoreInfo.Rows[i].FindControl("lgvgvalminfo")).Text.Trim();
                dr["gcod"] = Gcode;
                dr["gval"] = gval;
                dr["ccc"] = "";
                if (Gcode == "0306001")
                {
                    dr["gvalue"] = ((DropDownList)this.gvMoreInfo.Rows[i].FindControl("ddlval")).SelectedValue.ToString();
                }
                else if (Gcode == "0306003")
                {
                    dr["gvalue"] = ((TextBox)this.gvMoreInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim();
                }
                else if (Gcode == "0306005")
                {
                    dr["gvalue"] = ((TextBox)this.gvMoreInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim();
                }
                else if (Gcode == "0305011")
                {
                    dr["gvalue"] = ((DropDownList)this.gvMoreInfo.Rows[i].FindControl("ddlval")).SelectedValue.ToString();
                }
                else
                {
                    dr["gvalue"] = ((TextBox)this.gvMoreInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                }
                dt1.Rows.Add(dr);
            }
            //Item data exist
            DataView dv = dt1.DefaultView;
            dv.RowFilter = ("gvalue<>''");

            DataSet ds = new DataSet("ds1");
            //ds.Merge(dt);

            ds.Tables.Add(dv.ToTable());
            ds.Tables[0].TableName = "tbl";

            string clientid = (string)ViewState["newclientcode"];
            bool active = this.GetComPanyProsActivein();


            string number = "";
            number = Phone.Length > 0 ? CCC0 + Phone + "," : "";
            number = number + (altphone1.Length > 0 ? CCC1 + altphone1 + "," : "");
            number = number + (altphone2.Length > 0 ? CCC2 + altphone2 + "," : "");
            number = number.Length > 0 ? number.Substring(0, number.Length - 1) : number;




            clientid = this.lbllandname.Visible ? clientid : (this.lblnewprospect.Value.Length == 0 ? this.GetNewId() : this.lblnewprospect.Value);

            //if (this.lbllandname.Visible == true)
            //{
            //lbllandname.Visible = false;
            if (this.lbllandname.Visible == true)
            {
                // Check Duplicate
                DataSet ds2 = instcrm.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_CODEBOOK_NEW", "CHECKEDDUPUCLIENT", number, "", "", "", "", "", "", "", "");

                if (ds2.Tables[0].Rows.Count != 0)
                {
                    string pid = ds2.Tables[0].Rows[0]["pid"].ToString();
                    string sirdesc = ds2.Tables[0].Rows[0]["sirdesc"].ToString();
                    string supervisor = ds2.Tables[0].Rows[0]["superviser"].ToString();
                    string phone = ds2.Tables[0].Rows[0]["phone"].ToString();

                    //string holdername = " His/Her Name " + mobilename;
                    string Message = "Duplicate : ";
                    string totmsg = Message + phone + ", " + pid + ", Associate: " + sirdesc + ", Team Leader: " + supervisor;

                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + totmsg + "');", true);
                    return;
                }
            }


            //this is automatic kpi first discussion entry . recomended by Rahian for all company 20210804 dev by NAHID
            string kpidiscu = "";
            switch (comcod)
            {
                case "3101":
                case "3348":
                case "3335":
                case "3354":
                    kpidiscu = "1";
                    break;

            }
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            //  string xml = ds.GetXml();



            bool result = instcrm.UpdateXmlTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "UPDATE_CLNTINFO", ds, null, null, clientid, Name, usrid, Phone, email, empid, maddress, active.ToString(), kpidiscu, Posteddat, CCC0);
            if (result == true)
            {
                this.pnlEditProspectClose_Click(null, null);
                string totmsg = "Updated Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + totmsg + "');", true);

            }



            else
            {

                string Message = "Updated Failed";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Message + "');", true);

            }


            string events = hst["events"].ToString();
            if (Convert.ToBoolean(events) == true)
            {
                string eventtype = "Data Updated  Information (Sales CRM)";
                string eventdesc = "Data Updated  Information (Sales CRM)";
                string eventdesc2 = lbllandname.Text;

                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
            //  }

            //else
            //{
            //    string pid = ds2.Tables[0].Rows[0]["pid"].ToString();
            //    string sirdesc = ds2.Tables[0].Rows[0]["sirdesc"].ToString();
            //    string supervisor = ds2.Tables[0].Rows[0]["superviser"].ToString();
            //    string phone = ds2.Tables[0].Rows[0]["phone"].ToString();
            //    faclass = "fa-exclamation-circle";
            //    //string holdername = " His/Her Name " + mobilename;
            //    string Message = "Duplicate : ";
            //    string totmsg = Message + phone + ", " + pid + ", Associate:" + sirdesc + ", Team Leader:  " + supervisor;
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success",
            //               "alert('" + totmsg + "')", true);
            //    //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "alert", "alertmsg('" + Message + holdername + "','" + faclass + "');", true);
            //}





            //this.MultiView1.ActiveViewIndex = 0;
        }
        protected void btnaddland_Click(object sender, EventArgs e)
        {
            try
            {
                this.pnlEditProspect.Visible = true;
                this.pnlfollowup.Visible = false;
                this.pnlempinfo.Visible = false;
                this.pnlflw.Visible = false;
                this.pnlsrc.Visible = false;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string empid = hst["empid"].ToString();
                string usrid = hst["usrid"].ToString();
                string userrole = hst["userrole"].ToString();
                if (empid == "" && userrole != "1")
                {
                    string Messaged = "User ID did not set Employee ID, please contact your supervisor";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messaged + "');", true);
                    return;
                }

                GetData();
                this.lbllandname.Text = "";
                if (btnaddland.Text == "Add Lead")
                {
                    string comcod = this.GetComeCode();
                 
                    this.MultiView1.ActiveViewIndex = 0;

                    ShowPersonalInfo();
                    ShowSourceInfo();
                    Showpinfo();
                    ShowhomeInfo();
                    Showbusinfo();
                    ShowMoreInfo();
                    

                    string Message = "Add Client Form";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Message + "');", true);


                    this.lblnewprospect.Value = "";


                    string events = hst["events"].ToString();
                    if (Convert.ToBoolean(events) == true)
                    {
                        string eventtype = "Add Lead (Sales CRM)";
                        string eventdesc = "Add Lead (Sales CRM)";
                        string eventdesc2 = "";
                        bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);



                    }
                    this.lnkbtnOk_Click(null, null);
                    

                    //  }
                }
                else
                {
                    btnaddland.Text = "Add Lead";
                   
                    lbllandname.Visible = false;
                    ViewState["existclientcode"] = null;
                    this.MultiView1.ActiveViewIndex = 1;
                    this.lnkbtnOk_Click(null, null);


                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }
        protected void btnqclink_Click(object sender, EventArgs e)
        {
            try
            {


                this.pnlSidebar.Visible = true;
                this.pnlfollowup.Visible = false;
                this.pnlempinfo.Visible = false;
                this.pnlflw.Visible = false;
                this.pnlsrc.Visible = false;
                ShowDiscussion();




            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }
        protected void lbtnReshedule_Click(object sender, EventArgs e)
        {

        }
        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string GetReschedule(string comcod, string empid, string proscod, string cdate)
        {
            string kpigrp = "000000000000";
            string wrkdpt = "000000000000";
            ProcessAccess JData = new ProcessAccess();
            DataSet ds1 = JData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "DAILYDISCUIND", empid, proscod, kpigrp, "", wrkdpt, cdate, "", "", "", "");


            //DataSet ds1 = instcrm.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "DAILYDISCUIND", Empid, Client, kpigrp, "", wrkdpt, cdate);

            //   DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "DAILYLANDOWNERDISCUS", Empid, Client, kpigrp, "", wrkdpt, cdate);

            if (ds1 == null)
            {
                //List<RealEntity.C_32_Mis.EClassAcc_03.EclassBalSheetSum> lst5 = ds2.Tables[0].DataTableToList<RealEntity.C_32_Mis.EClassAcc_03.EclassBalSheetSum>();
                var lst = ds1.Tables[0].DataTableToList<RealEntity.C_01_LPA.BO_Fesibility.EclassPreLandownerDiscuss>().ToList();
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(lst);
                return json;
            }

            else
            {
                var lst = ds1.Tables[0].DataTableToList<RealEntity.C_01_LPA.BO_Fesibility.EclassPreLandownerDiscuss>().ToList();
                // var lst = new { Message = "Update successfully.", result = true };
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(lst);
                return json;
            }




        }
    }
}