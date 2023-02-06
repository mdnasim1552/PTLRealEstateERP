using RealERPLIB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Drawing;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using Microsoft.Reporting.WinForms;
using System.Web.SessionState;
using System.Text.RegularExpressions;

namespace RealERPWEB.F_21_MKT
{
    public partial class CrmClientInfo : System.Web.UI.Page
    {
        ProcessAccess instcrm = new ProcessAccess();
        Common compUtility = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");
                //((Label)this.Master.FindControl("lblTitle")).Text = "Client Information";
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();


                ((Label)this.Master.FindControl("lblmsg")).Visible = false;
                this.txtentryEmpID.Visible = false;
                string comcod = this.GetComeCode();
                this.CompanyTableColumnVisible();
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");

                this.txtfrmdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtkpitodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                this.MultiView1.ActiveViewIndex = 1;
                this.GetAllSubdata();
                this.DataBindStatus();
                this.GETEMPLOYEEUNDERSUPERVISED();
                this.companyModalVisible(); // hide user country,district, area etc
                this.ModalDataBind();
                this.GetGridSummary();
                this.GetNotificationinfo();

                this.GetFollow();
                this.GetParcipants();
                this.ShowDiscussion();
                this.IsTeamLeader();

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "Initializescroll();", true);

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string events = hst["events"].ToString();
                if (Convert.ToBoolean(events) == true)
                {
                    string eventtype = "Click CRM Interface (Sales CRM)";
                    string eventdesc = "Click CRM Interface (Sales CRM)";
                    string eventdesc2 = "";
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }

                // }


            }
        }





        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        private void lnkPrint_Click(object sender, EventArgs e)
        {
            this.CRMClientInformation();
        }

        private void CompanyTableColumnVisible()
        {

            string comcod = this.GetComeCode();

            switch (comcod)
            {


                case "3348":// Credence .
                            //this.gvSummary.Columns[13].Visible = false;
                            //this.gvSummary.Columns[14].Visible = false;
                            //this.gvSummary.Columns[15].Visible = false;
                            //this.gvSummary.Columns[16].Visible = false;
                            //this.gvSummary.Columns[17].Visible = false;
                            //this.gvSummary.Columns[18].Visible = false;
                            //this.gvSummary.Columns[19].Visible = false;


                    this.gvSummary.Columns[13].Visible = false;
                    this.gvSummary.Columns[12].Visible = false;
                    this.gvSummary.Columns[9].Visible = true;
                    this.gvSummary.Columns[14].Visible = false;
                    this.gvSummary.Columns[15].Visible = false;
                    this.gvSummary.Columns[16].Visible = false;
                    this.gvSummary.Columns[17].Visible = false;
                    this.gvSummary.Columns[18].Visible = false;
                    this.gvSummary.Columns[19].Visible = false;
                    this.gvSummary.Columns[20].Visible = true;
                    this.gvSummary.Columns[21].Visible = false;
                    this.gvSummary.Columns[22].Visible = true;
                    this.gvSummary.Columns[23].Visible = true;

                    //Checkbox Permanent Delete
                    this.divPermntDel.Visible = false;

                    break;

                case "3315"://Assure Builders
                case "3316"://Assure Development
                    this.gvSummary.Columns[6].HeaderText = "Date";
                    this.gvSummary.Columns[7].HeaderText = "Customer's Name";
                    this.gvSummary.Columns[5].Visible = true; // for pid show
                    // this.gvSummary.Columns[8].Visible = false;                
                    this.gvSummary.Columns[9].Visible = false;
                    this.gvSummary.Columns[10].Visible = true;
                    this.gvSummary.Columns[11].Visible = false;
                    this.gvSummary.Columns[12].Visible = false;
                    this.gvSummary.Columns[12].Visible = false;
                    this.gvSummary.Columns[22].Visible = false;

                    this.lnkBtnPotentialPros.Visible = false;
                    this.lnkBtnComments.Visible = false;
                    this.lnkBtnDaypassed.Visible = false;
                    this.lnkbtnTODayTask.Visible = false;
                    this.tdaswhtxt.InnerText = "Today Schedules Work";
                    break;



                case "3354"://Edison
                    this.gvSummary.Columns[13].Visible = false;
                    this.gvSummary.Columns[12].Visible = false;
                    this.gvSummary.Columns[9].Visible = true;
                    this.gvSummary.Columns[14].Visible = false;
                    this.gvSummary.Columns[15].Visible = false;
                    this.gvSummary.Columns[16].Visible = false;
                    this.gvSummary.Columns[17].Visible = false;
                    this.gvSummary.Columns[18].Visible = false;
                    this.gvSummary.Columns[19].Visible = false;
                    this.gvSummary.Columns[20].Visible = true;
                    this.gvSummary.Columns[22].Visible = false;
                    this.gvSummary.Columns[26].Visible = true;
                    this.gvSummary.Columns[27].Visible = true;
                    this.gvSummary.Columns[28].Visible = true;
                    break;

                case "3101"://PTL SHOW all Column
                    this.gvSummary.Columns[13].Visible = true;
                    this.gvSummary.Columns[12].Visible = true;
                    this.gvSummary.Columns[9].Visible = true;
                    this.gvSummary.Columns[14].Visible = true;
                    this.gvSummary.Columns[15].Visible = true;
                    this.gvSummary.Columns[16].Visible = true;
                    this.gvSummary.Columns[17].Visible = true;
                    this.gvSummary.Columns[18].Visible = true;
                    this.gvSummary.Columns[19].Visible = true;
                    this.gvSummary.Columns[20].Visible = true;
                    this.gvSummary.Columns[22].Visible = true;
                    this.gvSummary.Columns[26].Visible = true;
                    break;

                case "3367"://Epic
                    this.gvSummary.Columns[13].Visible = false;
                    this.gvSummary.Columns[12].Visible = false;
                    this.gvSummary.Columns[9].Visible = true;
                    this.gvSummary.Columns[14].Visible = false;
                    this.gvSummary.Columns[15].Visible = false;
                    this.gvSummary.Columns[16].Visible = false;
                    this.gvSummary.Columns[17].Visible = false;
                    this.gvSummary.Columns[18].Visible = false;
                    this.gvSummary.Columns[19].Visible = true;
                    this.gvSummary.Columns[20].Visible = true;
                    this.gvSummary.Columns[22].Visible = true;
                    break;

                default:
                    this.gvSummary.Columns[13].Visible = false;
                    this.gvSummary.Columns[12].Visible = false;
                    this.gvSummary.Columns[9].Visible = true;
                    this.gvSummary.Columns[14].Visible = false;
                    this.gvSummary.Columns[15].Visible = false;
                    this.gvSummary.Columns[16].Visible = false;
                    this.gvSummary.Columns[17].Visible = false;
                    this.gvSummary.Columns[18].Visible = false;
                    this.gvSummary.Columns[19].Visible = true;
                    this.gvSummary.Columns[20].Visible = false;
                    this.gvSummary.Columns[22].Visible = true;
                    break;


            }


        }



        public string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        public string GetEmpID()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string Empid = (hst["empid"].ToString() == "") ? "93" : hst["empid"].ToString();
            return (Empid);

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
        public string GetUserID()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["usrid"].ToString());

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
        private void GetAllSubdata()
        {
            string comcod = GetComeCode();
            string filter = comcod == "3374" ? "namdesgsec" : "";
            DataSet ds2 = instcrm.GetTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "CLNTREFINFODDL", filter, "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
            ViewState["tblsubddl"] = ds2.Tables[0];
            ViewState["tblstatus"] = ds2.Tables[1];
            ViewState["tblproject"] = ds2.Tables[2];
            ViewState["tblcompany"] = ds2.Tables[3];
            ds2.Dispose();
        }


        private void DataBindStatus()
        {
            try
            {

                DataTable dt = ((DataTable)ViewState["tblsubddl"]).Copy();
                DataView dv = dt.DefaultView;
                dv.RowFilter = ("gcod like '95%'");
                this.ddlmStatus.DataTextField = "gdesc";
                this.ddlmStatus.DataValueField = "gcod";
                this.ddlmStatus.DataSource = dv.ToTable();
                this.ddlmStatus.DataBind();
            }

            catch (Exception ex)
            {



            }



        }



        private void GETEMPLOYEEUNDERSUPERVISED()
        {
            string comcod = GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string empid = hst["empid"].ToString();
            DataSet ds1 = instcrm.GetTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "GETEMPLOYEEUNDERSUPERVISED", empid, "", "", "", "", "", "", "", "");
            ViewState["tblempsup"] = ds1.Tables[0];

            ds1.Dispose();


        }


        private void DDlModalAssureSrc()
        {
            string comcod = this.GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            DataSet ds1 = instcrm.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMP_INTERFACE", "GETDEPTDATA", usrid, "", "", "", "", "", "", "", "", "");
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            DataView dv1 = dt1.DefaultView;
            dv1.RowFilter = ("gcod like '31%'");
            ddlgval.DataTextField = "gdesc";
            ddlgval.DataValueField = "gcod";
            ddlgval.DataSource = dv1.ToTable();
            ddlgval.DataBind();
        }

        protected void btnaddland_Click(object sender, EventArgs e)
        {
            try
            {
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
                    //if (comcod == "3315" || comcod == "3316")
                    //{
                    //    Hashtable hst = (Hashtable)Session["tblLogin"];
                    //    string usrid = hst["usrid"].ToString();
                    //    DataSet ds1 = instcrm.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMP_INTERFACE", "GETDEPTDATA", usrid, "", "", "", "", "", "", "", "", "");
                    //    if (ds1 != null && ds1.Tables[0].Rows.Count == 1)
                    //    {
                    //        this.txtentryEmpID.Text = ds1.Tables[0].Rows[0]["empid"].ToString();
                    //    }
                    //    else
                    //    {
                    //        this.txtentryEmpID.Text = "";
                    //    }
                    //    this.txtentryClient.Text = "";
                    //    this.txtentrymobile.Text = "";
                    //    this.txtentryemail.Text = "";               
                    //    this.txtentrydate.Text = "";              
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenAssureModal();", true);
                    //}
                    //else
                    //{
                    this.MultiView1.ActiveViewIndex = 0;

                    ShowPersonalInfo();
                    ShowSourceInfo();
                    Showpinfo();
                    ShowhomeInfo();
                    Showbusinfo();
                    ShowMoreInfo();
                    btnaddland.Text = "Cancel";

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


                    //  }
                }
                else
                {
                    btnaddland.Text = "Add Lead";
                    GetGridSummary();
                    lbllandname.Visible = false;
                    ViewState["existclientcode"] = null;
                    this.MultiView1.ActiveViewIndex = 1;


                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
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

                               
                                //IR EPIC
                                empid = dt.Rows[i]["empid"].ToString();
                                if (lbllandname.Text.Length > 0)
                                {
                                    DataSet ds3 = instcrm.GetTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "GET_IR_EMPLOYEE", "", "", "", "", "", "", "", "", "");
                                    if (ds3 == null)
                                        return;

                                    ((TextBox)this.gvSourceInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                                    ((TextBox)this.gvSourceInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                                    ((Panel)this.gvSourceInfo.Rows[i].FindControl("pnlIREmp")).Visible = true;
                                    ddlgval = ((DropDownList)this.gvSourceInfo.Rows[i].FindControl("ddlIREmp"));
                                    ddlgval.DataTextField = "empname";
                                    ddlgval.DataValueField = "empid";
                                    ddlgval.DataSource = ds3.Tables[0];
                                    ddlgval.DataBind();
                                    ddlgval.SelectedValue = empid == "" ? "" : empid;
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

                    case "0303008": //Project Type
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '68%'");
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

        protected void ddlvalbusinfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblDatagridbusinfo"];
            if (dt == null)
                return;
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            DropDownList ddlgval;
            //Comments at 31 Dec 2022
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
            dt1.Columns.Add("empid");
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
                dr["empid"] = "";
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
                    if (sourcecode == "3101010" && comcod == "3367")//IR EPIC
                    {
                        string empIR = ((DropDownList)this.gvSourceInfo.Rows[i].FindControl("ddlIREmp")).SelectedValue.ToString();
                        if (empIR.Trim().Length == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('IR Reference is not Empty!');", true);
                            return;
                        }
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
                        
                        default:
                            break;
                    }
                }


                dr["gcod"] = Gcode;
                dr["ccc"] = "";
                dr["gval"] = gval;
                dr["gvalue"] = (((DropDownList)this.gvSourceInfo.Rows[i].FindControl("ddlval")).Items.Count == 0) ? ((TextBox)this.gvSourceInfo.Rows[i].FindControl("txtgvVal")).Text.Trim() :
                    ((DropDownList)this.gvSourceInfo.Rows[i].FindControl("ddlval")).SelectedValue.ToString();

                dr["empid"] = (((DropDownList)this.gvSourceInfo.Rows[i].FindControl("ddlIREmp")).Items.Count == 0) ? "" :
                   ((DropDownList)this.gvSourceInfo.Rows[i].FindControl("ddlIREmp")).SelectedValue.ToString();
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
                dr["empid"] = "";

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
                dr["empid"] = "";

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
                dr["empid"] = "";

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
                dr["empid"] = "";

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
            if (this.lbllandname.Visible == false)
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

                string totmsg = "Updated Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + totmsg + "');", true);

                btnaddland.Text = "Add Client";

                GetGridSummary();

                ViewState["existclientcode"] = null;
                this.MultiView1.ActiveViewIndex = 1;
            }



            else
            {

                string Message = "Updated Failed";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Message + "');", true);

            }

            //First Time Discussion
            if (this.lbllandname.Visible == false)
            {
                this.GetFirstTimeDiscussion(clientid, empid);

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


        //-------------------------------DashBoard------------------------------------------------

        private void companyModalVisible()
        {
            string comcod = this.GetComeCode();
            switch (comcod)
            {
                case "3101":
                case "3348"://Credence
                    this.ddlCountry.Visible = true;
                    this.ddlDist.Visible = true;
                    this.ddlZone.Visible = true;
                    this.ddlPStat.Visible = true;
                    this.ddlBlock.Visible = true;
                    this.ddlArea.Visible = true;
                    break;

                case "3316"://Assure Design & Dev.
                    this.ddlPri.Visible = false;
                    break;

                default:
                    break;
            }

        }


        private void ModalDataBind()
        {
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            DataTable dtemp = (DataTable)ViewState["tblempsup"];
            DataView dv;
            dv = dt1.Copy().DefaultView;
            string ddlempid = this.ddlEmpid.SelectedValue.ToString();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userrole = hst["userrole"].ToString();
            string lempid = hst["empid"].ToString();
            //string empid = (userrole == "1" ? "93" : lempid) + "%";
            string comcod = this.GetComeCode();
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

            this.ddlEmpid.DataTextField = "gdesc";
            this.ddlEmpid.DataValueField = "gcod";
            this.ddlEmpid.DataSource = dtE;
            this.ddlEmpid.DataBind();
            if (dtE.Rows.Count >= 2)
                this.ddlEmpid.SelectedValue = "000000000000";

            dv = dt1.Copy().DefaultView;
            dv.RowFilter = ("gcod like '52%'");
            DataTable dtCo = dv.ToTable();
            dtCo.Rows.Add("0000000", "Choose Country..", "");
            this.ddlCountry.DataTextField = "gdesc";
            this.ddlCountry.DataValueField = "gcod";
            this.ddlCountry.DataSource = dtCo;
            this.ddlCountry.DataBind();
            this.ddlCountry.SelectedValue = "5201001";

            dv = dt1.Copy().DefaultView;
            dv.RowFilter = ("gcod like '42%'");
            DataTable dtPr = dv.ToTable();
            dtPr.Rows.Add("0000000", "Choose Lead Quality..", "");
            this.ddlPri.DataTextField = "gdesc";
            this.ddlPri.DataValueField = "gcod";
            this.ddlPri.DataSource = dtPr;
            this.ddlPri.DataBind();
            this.ddlPri.SelectedValue = "0000000";


            dv = dt1.Copy().DefaultView;
            dv.RowFilter = ("gcod like '95%'");
            DataTable dtSta = dv.ToTable();
            dtSta.Rows.Add("0000000", "Choose Lead Status..");
            this.ddlStatus.DataTextField = "gdesc";
            this.ddlStatus.DataValueField = "gcod";
            this.ddlStatus.DataSource = dtSta;
            this.ddlStatus.DataBind();
            this.ddlStatus.SelectedValue = "0000000";

            this.ddlCountry_SelectedIndexChanged(null, null);




        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            DataView dv;
            string coun = this.ddlCountry.SelectedValue.ToString();
            dv = dt1.Copy().DefaultView;
            dv.RowFilter = ("gcod like '53%'and code ='" + coun + "'");
            DataTable dtDis = dv.ToTable();
            dtDis.Rows.Add("0000000", "Choose District..", "");
            this.ddlDist.DataTextField = "gdesc";
            this.ddlDist.DataValueField = "gcod";
            this.ddlDist.DataSource = dtDis;
            this.ddlDist.DataBind();
            this.ddlDist.SelectedValue = "5301001";
            this.ddlDist_SelectedIndexChanged(null, null);
        }
        protected void ddlDist_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            string dis = this.ddlDist.SelectedValue.ToString();
            DataView dv;
            dv = dt1.Copy().DefaultView;
            dv.RowFilter = ("gcod like '54%' and code ='" + dis + "'");
            DataTable dtZone = dv.ToTable();
            dtZone.Rows.Add("0000000", "Choose Zone..", "");
            this.ddlZone.DataTextField = "gdesc";
            this.ddlZone.DataValueField = "gcod";
            this.ddlZone.DataSource = dtZone;
            this.ddlZone.DataBind();
            this.ddlZone.SelectedValue = "0000000";
            this.ddlZone_SelectedIndexChanged(null, null);
        }
        protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            string zone = this.ddlZone.SelectedValue.ToString();
            DataView dv;
            dv = dt1.Copy().DefaultView;
            dv.RowFilter = ("gcod like '55%' and code ='" + zone + "'");
            DataTable dtPO = dv.ToTable();
            dtPO.Rows.Add("0000000", "Choose Police Station..", "");
            this.ddlPStat.DataTextField = "gdesc";
            this.ddlPStat.DataValueField = "gcod";
            this.ddlPStat.DataSource = dtPO;
            this.ddlPStat.DataBind();
            this.ddlPStat.SelectedValue = "0000000";
            this.ddlPStat_SelectedIndexChanged(null, null);
        }
        protected void ddlPStat_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            string PoSt = this.ddlPStat.SelectedValue.ToString();
            DataView dv;
            dv = dt1.Copy().DefaultView;
            dv.RowFilter = ("gcod like '56%' and code ='" + PoSt + "'");
            DataTable dtArea = dv.ToTable();
            dtArea.Rows.Add("0000000", "Choose Area..", "");
            this.ddlBlock.DataTextField = "gdesc";
            this.ddlBlock.DataValueField = "gcod";
            this.ddlBlock.DataSource = dtArea;
            this.ddlBlock.DataBind();
            this.ddlBlock.SelectedValue = "0000000";
            this.ddlBlock_SelectedIndexChanged(null, null);
        }

        protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt1 = (DataTable)ViewState["tblsubddl"];
            string Block = this.ddlBlock.SelectedValue.ToString();
            DataView dv;
            dv = dt1.Copy().DefaultView;
            dv.RowFilter = ("gcod like '57%' and code ='" + Block + "'");
            DataTable dtRoad = dv.ToTable();
            dtRoad.Rows.Add("0000000", "Choose Road..", "");
            this.ddlArea.DataTextField = "gdesc";
            this.ddlArea.DataValueField = "gcod";
            this.ddlArea.DataSource = dtRoad;
            this.ddlArea.DataBind();
            this.ddlArea.SelectedValue = "0000000";
        }

        private void GetGridSummary()
        {
            Session.Remove("tblsummData");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userrole = hst["userrole"].ToString();
            string Empid = ((hst["empid"].ToString() == "") ? "%" : hst["empid"].ToString());
            if (userrole == "1")
            {
                Empid = "%";
            }
            string comcod = this.GetComeCode();
            string Country = (this.ddlCountry.SelectedValue.ToString() == "0000000") ? "%" : this.ddlCountry.SelectedValue.ToString() + "%";
            string Dist = (this.ddlDist.SelectedValue.ToString() == "0000000") ? "%" : this.ddlDist.SelectedValue.ToString() + "%";
            string Zone = (this.ddlZone.SelectedValue.ToString() == "0000000") ? "%" : this.ddlZone.SelectedValue.ToString() + "%";
            string PStat = (this.ddlPStat.SelectedValue.ToString() == "0000000") ? "%" : this.ddlPStat.SelectedValue.ToString() + "%";
            string Area = (this.ddlArea.SelectedValue.ToString() == "0000000") ? "%" : this.ddlArea.SelectedValue.ToString() + "%";
            string Block = (this.ddlBlock.SelectedValue.ToString() == "0000000") ? "%" : this.ddlBlock.SelectedValue.ToString() + "%";
            string Pri = (this.ddlPri.SelectedValue.ToString() == "0000000") ? "%" : this.ddlPri.SelectedValue.ToString() + "%";
            string Status = (this.ddlStatus.SelectedValue.ToString() == "0000000") ? "%" : this.ddlStatus.SelectedValue.ToString() + "%";
            string Other = this.ddlOther.SelectedValue.ToString();
            string TxtVal = "%" + this.txtVal.Text + "%";
            string frmdate = this.txtfrmdate.Text.ToString();
            string todate = this.txttodate.Text.ToString();

            string srchempid = ((this.ddlEmpid.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlEmpid.SelectedValue.ToString());

            DataSet ds3 = instcrm.GetTransInfoNew(comcod, "SP_ENTRY_CRM_MODULE", "CLNTINFOSUM", null, null, null, "8301%", Empid, Country, Dist, Zone, PStat, Block, Area,
                 Pri, Status, Other, TxtVal, todate, srchempid,"");
            if (ds3 == null)
            {
                this.gvSummary.DataSource = null;
                this.gvSummary.DataBind();
                return;
            }


            //if (usertype != "admin")
            //{
            //    DataView view = new DataView(ds3.Tables[0]);
            //    DataTable distinctValues = view.ToTable(true, "empid", "assoc", "desig");

            //    distinctValues.Rows.Add("000000000000", "Choose Employee..", "");
            //    this.ddlEmpid.DataTextField = "assoc";
            //    this.ddlEmpid.DataValueField = "empid";
            //    this.ddlEmpid.DataSource = distinctValues;
            //    this.ddlEmpid.DataBind();
            //    this.ddlEmpid.SelectedValue = "000000000000";
            //}



            Session["tblsummData"] = ds3.Tables[0];
            //if (ds3.Tables[0].Rows.Count == 0)
            //    return;
            this.Data_Bind();

            this.gvSummary.Columns[24].Visible = false;

            DataView dv = ds3.Tables[0].Copy().DefaultView;
            dv.RowFilter = ("active='False'");
            this.lbtPending.Text = "Pending:" + ((dv.ToTable().Rows.Count == 0) ? "" : dv.ToTable().Rows.Count.ToString());
        }



        protected void SrchBtn_Click(object sender, EventArgs e)
        {
            GetGridSummary();
        }


        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.Data_Bind();
        }


        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            string Message = "";
            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

            if (!Convert.ToBoolean(dr1[0]["delete"]))
            {
                Message = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Message + "');", true);
                return;


            }


            DataTable dt = (DataTable)Session["tblsummData"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string comcod = this.GetComeCode();
            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            int rowno = (this.gvSummary.PageSize) * (this.gvSummary.PageIndex) + RowIndex;
            string proscod = dt.Rows[rowno]["sircode"].ToString();
            string proscod1 = dt.Rows[rowno]["sircode1"].ToString();


            if (Chkpdelete.Checked)
            {

                bool result = instcrm.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "DELETEPROSPECTPERMANENT", null, null, null, proscod, userid, Posteddat, "", "", "", "", "", "", "", "", "", "", "", "", "");


                if (!result)
                {
                    Message = "Already Follow up exist !!!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Message + "');", true);
                    return;
                }




            }

            else
            {
                bool result = instcrm.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "DELETEPROSPECT", null, null, null, proscod, userid, Posteddat, "", "", "", "", "", "", "", "", "", "", "", "", "");


                if (!result)
                {

                    string msg = "Delete Fail";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                    return;
                }

            }






            //dt.Rows[RowIndex].Delete();
            dt.Rows[rowno].Delete();

            DataView dv = dt.DefaultView;
            Session.Remove("tblsummData");
            Session["tblsummData"] = dv.ToTable();
            this.Data_Bind();



            Message = "Successfully Deleted";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Message + "');", true);

            string events = hst["events"].ToString();
            if (Convert.ToBoolean(events) == true)
            {
                string eventtype = "Delete Row (Sales CRM) ";
                string eventdesc = "Delete Row (Sales CRM) ";
                string eventdesc2 = proscod1;

                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);



            }


        }

        private void Data_Bind()
        {
            DataTable dt = ((DataTable)Session["tblsummData"]).Copy();
            DataView dv = dt.DefaultView;

            if (this.ddlEmpid.SelectedValue != "000000000000")
            {
                string empid = this.ddlEmpid.SelectedValue.ToString();
                dv.RowFilter = ("empid='" + empid + "' or  empid=''");
            }

            //if (this.ddlStatus.SelectedValue != "0000000")
            //{
            //    string LeadScod = this.ddlStatus.SelectedValue.ToString();
            //    dv.RowFilter = ("LeadScod='" + LeadScod + "'");
            //}

            this.gvSummary.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvSummary.DataSource = dv.ToTable();
            this.gvSummary.DataBind();

            if (dv.ToTable().Rows.Count > 0)
            {
                Session["Report1"] = gvSummary;
                ((HyperLink)this.gvSummary.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }
        }
        protected void lnkAct_Click(object sender, EventArgs e)
        {
            int index = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

            this.lblsircode.Text = ((Label)this.gvSummary.Rows[index].FindControl("lsircode")).Text.ToString().Trim();

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);
        }
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            lbllandname.Visible = true;
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string comcod = GetComeCode();
            string styleid = ((Label)this.gvSummary.Rows[index].FindControl("lsircode")).Text.ToString();
            string clintIdno = ((Label)this.gvSummary.Rows[index].FindControl("lsircode1")).Text.ToString();

            lbllandname.Text = ((Label)this.gvSummary.Rows[index].FindControl("lsircode1")).Text.ToString() + ':' + ((Label)this.gvSummary.Rows[index].FindControl("ldesc")).Text.ToString();
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
            btnaddland.Text = "Back";

            string Message = "Edit Client Form";

            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Message + "');", true);


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
        protected void btmodal_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            string comcod = GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string Procode = this.lblsircode.Text;
            string active = "";
            string Message = "";
            bool result = instcrm.UpdateTransInfo3(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "UPDATEACTLAND", Procode, active, userid, Posteddat);
            string faclass = "";
            if (!result)
            {

                Message = "Activation Failed";

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Message + "');", true);

                return;
            }



            Message = "Activated SuccessFully";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Message + "');", true);

            this.GetGridSummary();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Client Info";
                string eventdesc = "Update Info";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }
        protected void gvSummary_RowDataBound(object sender, GridViewRowEventArgs e)
        {






            if (e.Row.RowType == DataControlRowType.DataRow)
            {




                int index = e.Row.RowIndex;
                //Panel Lbtn = (Panel)e.Row.FindControl("pnlfollowup");
                //Lbtn.Attributes.Add("onmouseover", "AddButton(" + index + ")");
                //Lbtn.Attributes.Add("onmouseout", "HiddenButton(" + index + ")");
                //Lbtn.Attributes.Add("style", "cursor:pointer");

                //Panel pnldel = (Panel)e.Row.FindControl("pnldeletePros");
                //pnldel.Attributes.Add("onmouseover", "AddButton(" + index + ")");
                //pnldel.Attributes.Add("onmouseout", "HiddenButton(" + index + ")");
                //Lbtn.Attributes.Add("style", "cursor:pointer");

                //LinkButton Lbtn1 = (LinkButton)e.Row.FindControl("lnkEditfollowup");
                //Lbtn1.Attributes.Add("class", "hiddenb" + index);
                //Lbtn1.Attributes.Add("style", "display:none");

                //LinkButton lbtnView = (LinkButton)e.Row.FindControl("lbtnView");
                //lbtnView.Attributes.Add("class", "hiddenb" + index);
                //lbtnView.Attributes.Add("style", "display:none");



                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string dealcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "teamcod")).ToString();
                string solcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "soldcod")).ToString();
                string Empid = hst["empid"].ToString();
                LinkButton lnkAct = (LinkButton)e.Row.FindControl("lnkAct");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "sircode")).ToString();
                string followupdate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "lnfollowupdate")).ToString("dd-MMM-yyyy");
                string date = this.txttodate.Text;
                //Todays Followup
                if (followupdate == date)
                {
                    e.Row.Attributes["style"] = "background-color:#F3E7F7;";
                }

                //25 to 34 35 or later
                DateTime today = System.DateTime.Today;
                string ldiscusstion = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "appbydat")).ToString("dd-MMM-yyyy");
                DateTime lfollowup = Convert.ToDateTime(ldiscusstion);

                switch (comcod)
                {
                    case "3348"://Credence
                    case "3101":
                        int day = lfollowup.ToString("dd-MMM-yyyy") == "01-Jan-1900" ? 0 : Convert.ToInt32((today - lfollowup).TotalDays.ToString());
                        if (day >= 25 && day < 35)
                        {
                            e.Row.Attributes["style"] = "background-color:yellow; ";
                        }
                        else if (day >= 35)
                        {

                            // ;
                            e.Row.Attributes["style"] = "background-color:#e67070;";
                            // e.Row.Attributes["style"] = "background-color:#E0D9E9;";


                        }
                        break;

                    default:
                        break;



                }



                LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
                LinkButton lnkViewData = (LinkButton)e.Row.FindControl("ViewData");
                LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit");
                Label ldesc = (Label)e.Row.FindControl("ldesc");
                Panel pnlfollowup = (Panel)e.Row.FindControl("pnlfollowup");




                if (solcod == "9601050")
                {
                    e.Row.BackColor = Color.SkyBlue; //
                    e.Row.ToolTip = "Sold";
                }
                if (dealcode == Empid)
                {
                    lnkAct.Visible = true;
                }
                else
                {
                    lnkAct.Visible = false;
                }


                // For Heading
                if (code.Length == 7)
                {
                    lnkDelete.Visible = false;
                    lnkEdit.Visible = false;
                    lnkViewData.Visible = false;
                    pnlfollowup.Visible = false;

                    ldesc.Attributes["style"] = "font-size:13px; font-weight:bold; color:maroon;";
                }
            }
        }
        protected void lbtPending_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblsummData"];
            if (ASTUtility.Left(this.lbtPending.Text, 4) == "Pend")
            {
                this.lbtPending.Text = "Show";
                DataView dv = dt.Copy().DefaultView;
                dv.RowFilter = ("active='False'");
                this.gvSummary.DataSource = dv.ToTable();
                this.gvSummary.DataBind();



                if (dv.ToTable().Rows.Count > 0)
                {
                    Session["Report1"] = gvSummary;
                    ((HyperLink)this.gvSummary.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                }

            }
            else
            {
                this.GetGridSummary();
            }
        }
        protected void ViewData_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string comcod = GetComeCode();
            string code = ((Label)this.gvSummary.Rows[index].FindControl("lsircode")).Text.ToString();
            DataTable dt = (DataTable)Session["tblsummData"];
            if (dt.Rows.Count == 0)
                return;
            DataView dv1 = dt.Copy().DefaultView;
            dv1.RowFilter = ("sircode='" + code + "'");
            this.lname.Text = dv1.ToTable().Rows[0]["sirdesc"].ToString();
            this.lphn.Text = dv1.ToTable().Rows[0]["phone"].ToString();
            this.ldesig.Text = dv1.ToTable().Rows[0]["desig"].ToString();
            this.lheader.Text = dv1.ToTable().Rows[0]["sircode1"].ToString();
            this.lLSrc.Text = dv1.ToTable().Rows[0]["LeadSrc"].ToString();
            this.lassdt.Text = dv1.ToTable().Rows[0]["appbydat"].ToString();
            this.lassto.Text = dv1.ToTable().Rows[0]["assoc"].ToString();
            this.lcreateby.Text = dv1.ToTable().Rows[0]["assoc"].ToString();
            this.lleadquality.Text = dv1.ToTable().Rows[0]["LeadType"].ToString();
            this.lsalfd.Text = "";///dv1.ToTable().Rows[0]["desig"].ToString();

            this.lprjname.Text = "";
            this.ldesc.Text = "";
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "alert", "openViewModal();", true);

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string events = hst["events"].ToString();
            if (Convert.ToBoolean(events) == true)
            {
                string eventtype = "Data view (Sales CRM) ";
                string eventdesc = "Data view (Sales CRM) ";
                string eventdesc2 = "";

                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);



            }
        }
        protected void lnkgvHeader_Click(object sender, EventArgs e)
        {
            //DataTable dt1 = new DataTable();
            //DataTable dt2 = (DataTable)ViewState["tblHeaderCheck"];
            //dt1.Clear();
            //dt1.Columns.Add("gcod");
            //dt1.Columns.Add("gvalue");


            //for (int i = 0; i < gvSummary.Rows[0].Cells.Count; i++)
            //{
            //    string headerRowText = gvSummary.HeaderRow.Cells[i].Text;

            //    if (gvSummary.Columns[i].Visible == false)
            //    {
            //        DataRow dr = dt1.NewRow();
            //        if (headerRowText == "Code")
            //        {
            //        }
            //        else
            //        {
            //            if (headerRowText != "")
            //            {
            //                dr["gcod"] = i;
            //                dr["gvalue"] = headerRowText;
            //                dt1.Rows.Add(dr);
            //            }

            //        }

            //    }

            //}
            //this.gvCurrent.DataSource = dt1;
            //this.gvCurrent.DataBind();
            //this.gvPrev.DataSource = dt2;
            //gvPrev.DataBind();


            //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "alert", "OpenGvModal();", true);
        }
        protected void lnkgvListShow_Click(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();
            dt1.Clear();
            dt1.Columns.Add("gcod");
            dt1.Columns.Add("gvalue");
            for (int i = 0; i < gvCurrent.Rows.Count; i++)
            {
                int index = Convert.ToInt16(((Label)this.gvCurrent.Rows[i].FindControl("lindex")).Text.ToString());
                string desc = ((Label)this.gvCurrent.Rows[i].FindControl("lLSrc")).Text.ToString();
                bool chkbox = ((CheckBox)this.gvCurrent.Rows[i].FindControl("chkgv")).Checked;
                if (chkbox)
                {
                    this.gvSummary.Columns[index].Visible = true;
                    DataRow dr = dt1.NewRow();
                    dr["gcod"] = index;
                    dr["gvalue"] = desc;
                    dt1.Rows.Add(dr);
                }

            }
            for (int i = 0; i < gvPrev.Rows.Count; i++)
            {
                int index = Convert.ToInt16(((Label)this.gvPrev.Rows[i].FindControl("lindex")).Text.ToString());
                string desc = ((Label)this.gvPrev.Rows[i].FindControl("lLSrc")).Text.ToString();
                bool chkbox = ((CheckBox)this.gvPrev.Rows[i].FindControl("chkgv")).Checked;
                if (!chkbox)
                {
                    this.gvSummary.Columns[index].Visible = false;
                }
                else
                {
                    DataRow dr = dt1.NewRow();
                    dr["gcod"] = index;
                    dr["gvalue"] = desc;
                    dt1.Rows.Add(dr);
                }

            }
            ViewState["tblHeaderCheck"] = dt1;

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
        protected void gvplot_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lgcResDesc = (Label)e.Row.FindControl("lgcResDescp");
                string gcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "gcod")).ToString();
                string gdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "gdesc")).ToString();

                //if (gcod == "0304021")
                //{
                //    lgcResDesc.Text = gdesc + "<span class='manField'><sup> *</sup></span>";

                //    //lgcResDesc.Attributes["style"] = "<span><sup> *</sup></span>";


                //}

                //// custLink.NavigateUrl = "~/F_39_MyPage/ClientDetail.aspx?Type=Mgt";
                //hlnkAdd.NavigateUrl = "~/F_21_Mkt/MktEmpKpiEntry.aspx?Type=Entry&clientid=" + cusCode.Text + "&followupdate=" + napnt;
                //hyledit.NavigateUrl = "~/F_21_Mkt/MktEmpKpiEntry.aspx?Type=Edit&clientid=" + cusCode.Text + "&followupdate=" + date;


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

        //protected void gvAssureData_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {

        //        string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "sircode")).ToString();
        //        LinkButton lnkAct = (LinkButton)e.Row.FindControl("lnkdiss");
        //        lnkAct.Attributes.Add("href", "MktEmpKpiEntry.aspx?Type=Entry&clientid=" + code);
        //        lnkAct.Attributes.Add("target", "_blank");

        //    }

        //}
        //protected void EditAssureData_Click(object sender, EventArgs e)
        //{
        //    GetAllSubdata();
        //    DDlModalAssureSrc();
        //    int index = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
        //    ViewState["existclientcode"] = ((Label)this.gvAssureData.Rows[index].FindControl("lsircode")).Text.ToString().Trim();
        //    this.txtentryClient.Text = ((Label)this.gvAssureData.Rows[index].FindControl("ldesc")).Text.ToString().Trim();

        //    this.txtentrymobile.Text = ((Label)this.gvAssureData.Rows[index].FindControl("lappdat")).Text.ToString().Trim();
        //    this.txtentryemail.Text = ((Label)this.gvAssureData.Rows[index].FindControl("lprefdesc")).Text.ToString().Trim();
        //    this.ddlgval.SelectedValue = ((Label)this.gvAssureData.Rows[index].FindControl("lsrccod")).Text.ToString().Trim();
        //    this.txtentrydate.Text = ((Label)this.gvAssureData.Rows[index].FindControl("lblgenerated")).Text.ToString().Trim();
        //    this.txtentryEmpID.Text = ((Label)this.gvAssureData.Rows[index].FindControl("lempid")).Text.ToString().Trim();
        //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenAssureModal();", true);
        //}


        protected void txtgvVal_TextChanged(object sender, EventArgs e)
        {

        }

        private void GetNotificationinfo()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userrole = hst["userrole"].ToString();

            string comcod = this.GetComeCode();
            //string Empid = (hst["empid"].ToString() == "") ? "%" : hst["empid"].ToString();

            string Empid = "";
            if (userrole != "1")
            {
                Empid = hst["empid"].ToString();
            }


            string frmdate = this.txtfrmdate.Text.ToString();
            string todate = this.txttodate.Text.ToString();

            string ddlempid = (this.ddlEmpid.SelectedValue.ToString() == "000000000000" ? "93" : this.ddlEmpid.SelectedValue.ToString()) + "%";
            DataSet ds3 = instcrm.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "GETNOTIFICATIONNUMBER", "8301%", Empid, ddlempid, todate);
            Session["tblNotification"] = ds3;
            bindDataIntoLabel();

        }

        private void GetNotificationByEmployee(string ddlempid)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userrole = hst["userrole"].ToString();
            string comcod = this.GetComeCode();
            //string Empid = (hst["empid"].ToString() == "") ? "%" : hst["empid"].ToString();
            string frmdate = this.txtfrmdate.Text.ToString();
            string todate = this.txttodate.Text.ToString();

            string Empid = "";
            if (userrole != "1")
            {
                Empid = hst["empid"].ToString();
            }
            //string empid = (hst["empid"].ToString() == "") ? "%" : hst["empid"].ToString();
            DataSet ds3 = instcrm.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "GETNOTIFICATIONNUMBER", "8301%", Empid, ddlempid, todate);
            Session["tblNotification"] = ds3;
            bindDataIntoLabel();
        }

        protected void ddlEmpid_SelectedIndexChanged(object sender, EventArgs e)
        {
            string empid = this.ddlEmpid.SelectedValue.ToString();
            if (empid != "000000000000")
                this.GetGridSummary();
            GetNotificationByEmployee(empid);
        }


        private void bindDataIntoLabel()
        {
            DataSet ds3 = (DataSet)Session["tblNotification"];
            if (ds3 == null)
            {
                return;
            }
            this.lbldws.InnerText = ds3.Tables[0].Rows[0]["dws"].ToString();
            this.lbltdt.InnerText = ds3.Tables[0].Rows[0]["tdt"].ToString();
            this.lbldwr.InnerText = ds3.Tables[0].Rows[0]["dwr"].ToString();
            this.lblCall.InnerText = ds3.Tables[0].Rows[0]["call"].ToString();
            this.lblvisit.InnerText = ds3.Tables[0].Rows[0]["visit"].ToString();
            this.lblComments.InnerText = ds3.Tables[0].Rows[0]["comments"].ToString();
            this.lblDayPass.InnerText = ds3.Tables[0].Rows[0]["daypassed"].ToString();
            this.lblFreez.InnerText = ds3.Tables[0].Rows[0]["freezing"].ToString();
            this.lblDeadProspect.InnerText = ds3.Tables[0].Rows[0]["deadprospect"].ToString();
            this.lblcsigned.InnerText = ds3.Tables[0].Rows[0]["signed"].ToString();

            this.lblpme.InnerText = ds3.Tables[0].Rows[0]["pme"].ToString();
            this.lblpmi.InnerText = ds3.Tables[0].Rows[0]["pmi"].ToString();
            this.lblDatablank.InnerText = ds3.Tables[0].Rows[0]["databank"].ToString();


        }


        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string UpdatePost(string comcod, string userid, string proscod, string date, string post, string comdate)
        {

            string gcod = "810100101015";
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            ProcessAccess JData = new ProcessAccess();

            bool result = JData.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "UPDATECOMMENTS", null, null, null, proscod, gcod, date, post, userid, comdate, Posteddat, "", "", "", "", "", "", "", "", "", "", "");

            // bool result = KpiData.UpdateTransInfo3(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "UPDATE_COMM", empid, Client, "000000000000", "", "000000000000", cdate, gcod, "T", Gvalue, Comments, userid, Posteddat);


            if (!result)
            {

                var lst = new { Message = JData.ErrorObject["Msg"].ToString(), result = false };
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(lst);
                return json;
            }

            else
            {

                var lst = new { Message = "Update successfully.", result = true };
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(lst);
                return json;
            }




        }


        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string FollowupCancel(string comcod, string userid, string proscod, string date)
        {



            ProcessAccess JData = new ProcessAccess();
            bool result = JData.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "DELETKPITRANS", null, null, null, proscod, date, "", "", "", "", "", "", "", "", "", "", "", "", "", "");

            // bool result = KpiData.UpdateTransInfo3(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "UPDATE_COMM", empid, Client, "000000000000", "", "000000000000", cdate, gcod, "T", Gvalue, Comments, userid, Posteddat);



            if (!result)
            {

                var lst = new { Message = JData.ErrorObject["Msg"].ToString(), result = false };
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(lst);
                return json;
            }

            else
            {

                var lst = new { Message = "Deleted successfully.", result = true };
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(lst);
                return json;
            }




        }





        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string CheckMobile(string comcod, string sircode, string mobile)
        {




            //string number = "";
            //number = Phone.Length > 0 ? Phone + "," : "";
            //number = number + (altphone1.Length > 0 ? altphone1 + "," : "");
            //number = number + (altphone2.Length > 0 ? altphone2 + "," : "");
            //number = number.Length > 0 ? number.Substring(0, number.Length - 1) : number;

            //Check Duplicate
            //DataSet ds2 = instcrm.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_CODEBOOK_NEW", "CHECKEDDUPUCLIENT", number, "", "", "", "", "", "", "", "");


            //if (ds2 == null)
            //{
            //    return;
            //}

            ProcessAccess _processAccess = new ProcessAccess();

            DataSet ds2 = _processAccess.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_CODEBOOK_NEW", "CHECKEDDUPUCLIENTPHONE", mobile, "", "", "", "", "", "", "", "");
            if (ds2.Tables[0].Rows.Count == 0 || ds2 == null)
            {
                var result = new { Message = "Success", result = true };
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(result);
                return json;

            }


            else
            {

                DataView dv1 = ds2.Tables[0].DefaultView;
                dv1.RowFilter = ("sircode <>'" + sircode + "'");
                DataTable dt1 = dv1.ToTable();






                if (dt1.Rows.Count == 0)
                {

                    var result = new { Message = "Success", result = true };
                    var jsonSerialiser = new JavaScriptSerializer();
                    var json = jsonSerialiser.Serialize(result);
                    return json;

                }




                else
                {


                    string pid = ds2.Tables[0].Rows[0]["pid"].ToString();
                    string sirdesc = ds2.Tables[0].Rows[0]["sirdesc"].ToString();
                    string supervisor = ds2.Tables[0].Rows[0]["superviser"].ToString();
                    string phone = ds2.Tables[0].Rows[0]["phone"].ToString();

                    //string holdername = " His/Her Name " + mobilename;
                    string Message = "Duplicate : ";
                    string totmsg = Message + phone + ", " + pid + ", Associate: " + sirdesc + ", Team Leader: " + supervisor;






                    var result = new { Message = totmsg, result = false };
                    var jsonSerialiser = new JavaScriptSerializer();
                    var json = jsonSerialiser.Serialize(result);

                    return json;



                }
            }




        }




        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string GetSchedulenumber(string comcod, string followupdate, string lastfollowup, string empid)
        {


            string sircode = "8301%";
            ProcessAccess _processAccess = new ProcessAccess();


            DataSet ds2 = _processAccess.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "SCHEDULENUMBER", sircode, empid, lastfollowup, followupdate, "", "", "", "", "", "");
            if (ds2.Tables[0].Rows.Count == 0)
            {
                var result = new { Message = "Schedule:", result = true };
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(result);
                return json;

            }


            else
            {
                var schedule = Convert.ToDouble(ds2.Tables[0].Rows[0]["schnumber"]).ToString("#,##0;(#,##0);");
                var schdesc = (ds2.Tables[0].Rows[0]["schdesc"]);
                var result = new { Message = "Schedule:" + schdesc + "(" + schedule + ")", result = true };
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(result);
                return json;


            }




        }



        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string GetCompanyProject(string comcod, string company)
        {


            ProcessAccess _processAccess = new ProcessAccess();

            DataSet ds2 = _processAccess.GetTransInfo(company, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "GETCOMPROJECT", "", "", "", "", "", "", "", "", "", "");


            if (ds2.Tables[0].Rows.Count == 0)
            {
                var result = new { Message = "Schedule:", result = true };
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(result);
                return json;

            }


            else
            {

                var lst = ds2.Tables[0].DataTableToList<RealEntity.EClassCommon.EClassProject>().ToList();
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(lst);
                return json;


            }







        }



        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string GetLeadReason(string comcod, string leadquality)
        {


            ProcessAccess _processAccess = new ProcessAccess();

            DataSet ds2 = _processAccess.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "GETLEADREASON", leadquality, "", "", "", "", "", "", "", "", "");


            if (ds2.Tables[0].Rows.Count == 0)
            {
                var result = new { Message = "Schedule:", result = true };
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(result);
                return json;

            }


            else
            {

                var lst = ds2.Tables[0].DataTableToList<RealEntity.C_21_Mkt.ECRMClientInfo.EClassLeadReason>().ToList();
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(lst);
                return json;


            }







        }





        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string UpdateStatus(string comcod, string proscod, string statusid, string empid)
        {

            string gcod = "810100101016";

            ProcessAccess JData = new ProcessAccess();

            bool result = JData.UpdateXmlTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "UPDATELASTEMPSTATUS", null, null, null, empid, proscod, gcod, statusid, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            //bool result = KpiData.UpdateTransInfo3(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "UPDATE_COMM", empid, Client, "000000000000", "", "000000000000", cdate, gcod, "T", Gvalue, Comments, userid, Posteddat);

            if (!result)
            {

                var lst = new { Message = JData.ErrorObject["Msg"].ToString(), result = false };
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(lst);
                return json;
            }

            else
            {

                var lst = new { Message = "Update successfully.", result = true };
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(lst);
                return json;
            }
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


        //public static string ShowDetNotification(string comcod, string empid, string rtype, string date)
        //{

        //    ProcessAccess JData = new ProcessAccess();
        //    DataSet ds1 = JData.GetTransInfo(comcod, "SP_ENTRY_LANDPROCUREMENT", "GETNOTIFICATIONDETAILS", "8305%", empid, rtype, date, "");
        //    var lst = ds1.Tables[0].DataTableToList<RealEntity.C_01_LPA.BO_Fesibility.LandNotification>();
        //    var jsonSerialiser = new JavaScriptSerializer();
        //    var json = jsonSerialiser.Serialize(lst);
        //    return json;

        //}


        public void ShowNotifications(string rtype)
        {
            try
            {
                this.hdnlblattribute.Value = "";
                this.lbltodatekpi.Visible = false;
                this.txtkpitodate.Visible = false;
                ProcessAccess JData = new ProcessAccess();
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string userrole = hst["userrole"].ToString();
                string type = rtype;
                string comcod = this.GetComeCode();

                string Empid = "";
                if (userrole != "1")
                {
                    Empid = hst["empid"].ToString();
                }

                string tdate = this.txttodate.Text.ToString();
                string fempid = (this.ddlEmpid.SelectedValue.ToString() == "000000000000" ? "93" : this.ddlEmpid.SelectedValue.ToString()) + "%";
                DataSet ds1 = JData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "GETNOTIFICATIONDETAILS", "8301%", Empid, type, tdate, fempid);
                if (ds1 == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + JData.ErrorObject["Msg"].ToString() + "');", true);
                    return;
                }

                Session["tblsummData"] = ds1.Tables[0];
                if (rtype == "databank")
                {
                    //Prospect Retreive Button
                    this.gvSummary.Columns[25].Visible = true;
                    this.gvSummary.Columns[27].Visible = false;

                }
                else if (rtype == "dws" && (comcod == "3315" || comcod == "3316"))
                {
                    this.gvSummary.Columns[26].HeaderText = "Today's <br> Followup";
                    this.gvSummary.Columns[8].HeaderText = "Last Followup <br> Date";
                    this.gvSummary.Columns[26].Visible = true;
                    this.gvSummary.Columns[17].Visible = true;
                    this.gvSummary.Columns[25].Visible = false;
                    this.gvSummary.Columns[27].Visible = false;
                }
                else
                {
                    this.gvSummary.Columns[26].HeaderText = "Next Followup Date";
                    this.gvSummary.Columns[8].HeaderText = "Followup <br> Date";
                    this.gvSummary.Columns[24].Visible = false;
                    this.gvSummary.Columns[26].Visible = false;
                    this.gvSummary.Columns[25].Visible = false;
                    this.gvSummary.Columns[27].Visible = false;

                }

                this.Data_Bind();
                this.gvkpi.DataSource = null;
                this.gvkpi.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + ex.Message + "');", true);
            }

        }





        protected void lnkOk_Click(object sender, EventArgs e)
        {
            if (this.hdnlblattribute.Value.Trim() == "")
            {
                this.GetGridSummary();
                this.GetNotificationinfo();
            }
            else
            {
                this.EmpMonthlyKPI();


            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string events = hst["events"].ToString();
            if (Convert.ToBoolean(events) == true)
            {
                string eventtype = "Show  Information (sales CRM)";
                string eventdesc = "Show Crm Information (sales CRM)";
                string eventdesc2 = "";
                string comcod = this.GetCompCode();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);



            }


        }
        protected void lnkbtnDws_Click(object sender, EventArgs e)
        {
            string rtype = "dws";
            this.ShowNotifications(rtype);
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string events = hst["events"].ToString();
            if (Convert.ToBoolean(events) == true)
            {
                string eventtype = "Daily work Schedule (sales CRM)";
                string eventdesc = "Daily work Schedule (sales CRM)";
                string eventdesc2 = "";
                string comcod = this.GetCompCode();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }
        protected void lnkBtnDwr_Click(object sender, EventArgs e)
        {
            string rtype = "dwr";
            this.ShowNotifications(rtype);

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string events = hst["events"].ToString();
            if (Convert.ToBoolean(events) == true)
            {
                string eventtype = "Daily work Report (sales CRM)";
                string eventdesc = "Daily work Report (sales CRM)";
                string eventdesc2 = "";
                string comcod = this.GetCompCode();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        protected void lnkBtnCall_Click(object sender, EventArgs e)
        {
            string rtype = "call";
            this.ShowNotifications(rtype);
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string events = hst["events"].ToString();
            if (Convert.ToBoolean(events) == true)
            {
                string eventtype = "Show Call Information (sales CRM)";
                string eventdesc = "Show Call Information (sales CRM)";
                string eventdesc2 = "";
                string comcod = this.GetCompCode();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        protected void lbtnpme_Click(object sender, EventArgs e)
        {
            string rtype = "pme";
            this.ShowNotifications(rtype);
        }
        protected void lbtnpmi_Click(object sender, EventArgs e)
        {
            string rtype = "pmi";
            this.ShowNotifications(rtype);

        }
        protected void lnkBtnVisit_Click(object sender, EventArgs e)
        {
            string rtype = "visit";
            this.ShowNotifications(rtype);

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string events = hst["events"].ToString();
            if (Convert.ToBoolean(events) == true)
            {
                string eventtype = "Show visit Information (sales CRM)";
                string eventdesc = "Show visit Information (sales CRM)";
                string eventdesc2 = "";
                string comcod = this.GetCompCode();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }
        protected void lnkBtnDaypassed_Click(object sender, EventArgs e)
        {
            string rtype = "daypassed";
            this.ShowNotifications(rtype);
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string events = hst["events"].ToString();
            if (Convert.ToBoolean(events) == true)
            {
                string eventtype = "Show daypass Information (sales CRM)";
                string eventdesc = "Show daypass Information (sales CRM)";
                string eventdesc2 = "";
                string comcod = this.GetCompCode();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }

        protected void lnkBtnFreezing_Click(object sender, EventArgs e)
        {
            string rtype = "freezing";
            this.ShowNotifications(rtype);

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string events = hst["events"].ToString();
            if (Convert.ToBoolean(events) == true)
            {
                string eventtype = "Show freezing Information (sales CRM)";
                string eventdesc = "Show freezing Information (sales CRM)";
                string eventdesc2 = "";
                string comcod = this.GetCompCode();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }


        protected void lnkBtnDeadProspect_Click(object sender, EventArgs e)
        {
            string rtype = "deadprospect";
            this.ShowNotifications(rtype);

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string events = hst["events"].ToString();
            if (Convert.ToBoolean(events) == true)
            {
                string eventtype = "Show dead prospect Information (sales CRM)";
                string eventdesc = "Show dead prospect Information (sales CRM)";
                string eventdesc2 = "";
                string comcod = this.GetCompCode();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        protected void lbtnSigned_Click(object sender, EventArgs e)
        {
            string rtype = "signed";
            this.ShowNotifications(rtype);
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string events = hst["events"].ToString();
            if (Convert.ToBoolean(events) == true)
            {
                string eventtype = "Show Signed Information (sales CRM)";
                string eventdesc = "Show Signed Information (sales CRM)";
                string eventdesc2 = "";
                string comcod = this.GetCompCode();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }


        protected void lnkBtnDatablank_Click(object sender, EventArgs e)
        {
            string rtype = "databank";
            this.ShowNotifications(rtype);
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string events = hst["events"].ToString();
            if (Convert.ToBoolean(events) == true)
            {
                string eventtype = "Show Data Bank Information (sales CRM)";
                string eventdesc = "Show Data Bank Information (sales CRM)";
                string eventdesc2 = "";
                string comcod = this.GetCompCode();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        protected void lnkBtnPotentialPros_Click(object sender, EventArgs e)
        {
            string rtype = "potential";
            this.ShowNotifications(rtype);

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string events = hst["events"].ToString();
            if (Convert.ToBoolean(events) == true)
            {
                string eventtype = "Show Potential Prospect Info (CRM)";
                string eventdesc = "Show Potential Prospect Info (CRM)";
                string eventdesc2 = "";
                string comcod = this.GetCompCode();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        protected void lnkbtnReturn_Click(object sender, EventArgs e)
        {
            this.lbltodatekpi.Visible = false;
            this.txtkpitodate.Visible = false;
            this.lblkpiDetails.Visible = false;
            this.hdnlblattribute.Value = "";
            this.txtVal.Text = "";
            this.GetGridSummary();
            this.GetNotificationinfo();
            this.gvkpi.DataSource = null;
            this.gvkpi.DataBind();
            this.gvkpidet.DataSource = null;
            this.gvkpidet.DataBind();
        }
        protected void lbtnView_Click(object sender, EventArgs e)
        {

        }
        private void GetFirstTimeDiscussion(string proscod, string empid)
        {
            try
            {



                string comcod = this.GetComeCode();
                string cdate = this.txttodate.Text.Trim();
                DataSet ds1 = instcrm.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "SHOWPROSPECTIVEDISCUSSION", proscod, cdate, "", "", "", "");

                this.rpclientinfo.DataSource = ds1.Tables[0];
                this.rpclientinfo.DataBind();
                this.lblprosname.InnerText = ds1.Tables[0].Rows.Count == 0 ? ds1.Tables[1].Rows[0]["prosdesc"].ToString() : ds1.Tables[0].Rows[0]["prosdesc"].ToString();
                this.lblContactPerson.InnerText = ds1.Tables[0].Rows.Count == 0 ? ds1.Tables[1].Rows[0]["conperson"].ToString() : ds1.Tables[0].Rows[0]["conperson"].ToString();
                this.lblprosphone.InnerText = ds1.Tables[0].Rows.Count == 0 ? ds1.Tables[1].Rows[0]["phone"].ToString() : ds1.Tables[0].Rows[0]["phone"].ToString();
                this.lblprosaddress.InnerText = ds1.Tables[0].Rows.Count == 0 ? ds1.Tables[1].Rows[0]["haddress"].ToString() : ds1.Tables[0].Rows[0]["haddress"].ToString();
                this.lblnotes.InnerText = ds1.Tables[0].Rows.Count == 0 ? ds1.Tables[1].Rows[0]["virnotes"].ToString() : ds1.Tables[0].Rows[0]["virnotes"].ToString();
                this.lblpreferloc.InnerText = ds1.Tables[0].Rows.Count == 0 ? ds1.Tables[1].Rows[0]["preferloc"].ToString() : ds1.Tables[0].Rows[0]["preferloc"].ToString();
                this.lblaptsize.InnerText = ds1.Tables[0].Rows.Count == 0 ? ds1.Tables[1].Rows[0]["aptsize"].ToString() : ds1.Tables[0].Rows[0]["aptsize"].ToString();
                this.lblproscod.Value = ds1.Tables[0].Rows.Count == 0 ? proscod : ds1.Tables[0].Rows[0]["proscod"].ToString();
                //this.lblproscod.Value = ds1.Tables[0].Rows.Count == 0 ? proscod : ds1.Tables[0].Rows[0]["proscod"].ToString();
                this.lbleditempid.Value = empid;
                this.ddlRating.SelectedValue = ds1.Tables[0].Rows.Count == 0 ? ds1.Tables[1].Rows[0]["rating"].ToString() : ds1.Tables[1].Rows[0]["rating"].ToString();
                this.lbllaststatus.InnerHtml = "Status:" + "<span style='color:#ffef2f; font-size:14px; font-weight:bold'>" + (ds1.Tables[0].Rows.Count == 0 ? "" : ds1.Tables[0].Rows[0]["lastlsdesc"].ToString()) + "</span>";
                this.hiddenLedStatus.Value = (ds1.Tables[0].Rows.Count == 0 ? "" : ds1.Tables[0].Rows[0]["lastlsdesc"].ToString());
                ShowDiscussion();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModaldis();", true);


                Hashtable hst = (Hashtable)Session["tblLogin"];
                string events = hst["events"].ToString();

                //if (Convert.ToBoolean(events) == true)
                //{
                //    string eventtype = "Click Follow UP (Sales CRM) ";
                //    string eventdesc = "Click Follow UP (Sales CRM) ";
                //    string eventdesc2 = follclintidno;

                //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);



                //}

            }

            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

            }

        }
        protected void lnkEditfollowup_Click(object sender, EventArgs e)
        {

            try
            {

                string comcod = this.GetComeCode();
                int rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

                string proscod = ((Label)this.gvSummary.Rows[rowindex].FindControl("lsircode")).Text;
                string gempid = ((Label)this.gvSummary.Rows[rowindex].FindControl("lblgvempid")).Text;
                string follclintidno = ((Label)this.gvSummary.Rows[rowindex].FindControl("lsircode1")).Text;
                string cdate = this.txttodate.Text.Trim();
                DataSet ds1 = instcrm.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "SHOWPROSPECTIVEDISCUSSION", proscod, cdate, "", "", "", "");

                this.rpclientinfo.DataSource = ds1.Tables[0];
                this.rpclientinfo.DataBind();
                this.lblPID.InnerText = ds1.Tables[0].Rows.Count == 0 ? ds1.Tables[1].Rows[0]["pid"].ToString() : ds1.Tables[0].Rows[0]["pid"].ToString();
                this.lblprosname.InnerText = ds1.Tables[0].Rows.Count == 0 ? ds1.Tables[1].Rows[0]["prosdesc"].ToString() : ds1.Tables[0].Rows[0]["prosdesc"].ToString();
                this.lblContactPerson.InnerText = ds1.Tables[0].Rows.Count == 0 ? ds1.Tables[1].Rows[0]["conperson"].ToString() : ds1.Tables[0].Rows[0]["conperson"].ToString();
                this.lblprosphone.InnerText = ds1.Tables[0].Rows.Count == 0 ? ds1.Tables[1].Rows[0]["phone"].ToString() : ds1.Tables[0].Rows[0]["phone"].ToString();
                this.lblprosaddress.InnerText = ds1.Tables[0].Rows.Count == 0 ? ds1.Tables[1].Rows[0]["haddress"].ToString() : ds1.Tables[0].Rows[0]["haddress"].ToString();
                this.lblnotes.InnerText = ds1.Tables[0].Rows.Count == 0 ? ds1.Tables[1].Rows[0]["virnotes"].ToString() : ds1.Tables[0].Rows[0]["virnotes"].ToString();
                this.lblpreferloc.InnerText = ds1.Tables[0].Rows.Count == 0 ? ds1.Tables[1].Rows[0]["preferloc"].ToString() : ds1.Tables[0].Rows[0]["preferloc"].ToString();
                this.lblaptsize.InnerText = ds1.Tables[0].Rows.Count == 0 ? ds1.Tables[1].Rows[0]["aptsize"].ToString() : ds1.Tables[0].Rows[0]["aptsize"].ToString();
                this.lblproscod.Value = ds1.Tables[0].Rows.Count == 0 ? proscod : ds1.Tables[0].Rows[0]["proscod"].ToString();
                //this.lblproscod.Value = ds1.Tables[0].Rows.Count == 0 ? proscod : ds1.Tables[0].Rows[0]["proscod"].ToString();
                this.lbleditempid.Value = gempid;
                this.lblgeneratedate.Value = ds1.Tables[1].Rows.Count == 0 ? "01-Jan-1900" : Convert.ToDateTime(ds1.Tables[1].Rows[0]["createdate"]).ToString("dd-MMM-yyyy");
                this.ddlRating.SelectedValue = ds1.Tables[0].Rows.Count == 0 ? ds1.Tables[1].Rows[0]["rating"].ToString() : ds1.Tables[1].Rows[0]["rating"].ToString();
                this.lbllaststatus.InnerHtml = "Status:" + "<span style='color:#ffef2f; font-size:14px; font-weight:bold'>" + (ds1.Tables[0].Rows.Count == 0 ? "" : ds1.Tables[0].Rows[0]["lastlsdesc"].ToString()) + "</span>";
                this.hiddenLedStatus.Value = (ds1.Tables[0].Rows.Count == 0 ? "" : ds1.Tables[0].Rows[0]["lastlstcode"].ToString());
                this.lblProfession.InnerText = ds1.Tables[0].Rows.Count == 0 ? ds1.Tables[1].Rows[0]["profession"].ToString() : ds1.Tables[0].Rows[0]["profession"].ToString();
                this.lblSource.InnerText = ds1.Tables[0].Rows.Count == 0 ? ds1.Tables[1].Rows[0]["sourcetxt"].ToString() : ds1.Tables[0].Rows[0]["sourcetxt"].ToString();

                ShowDiscussion();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModaldis();", true);


                Hashtable hst = (Hashtable)Session["tblLogin"];
                string events = hst["events"].ToString();

                if (Convert.ToBoolean(events) == true)
                {
                    string eventtype = "Click Follow UP (Sales CRM) ";
                    string eventdesc = "Click Follow UP (Sales CRM) ";
                    string eventdesc2 = follclintidno;

                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);



                }

            }

            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

            }

        }


        private void ShowDiscussion()
        {
            string comcod = this.GetComeCode();
            DataTable tbl1 = (DataTable)ViewState["tbModalData"];
            string YmonID = Convert.ToDateTime(this.txtfrmdate.Text.Trim()).ToString("yyyyMM");
            string Empid = this.ddlEmpid.SelectedValue.ToString();
            //string grpcode = this.lblgrp.Text;
            string Client = this.lblproscod.Value.ToString();
            string kpigrp = "000000000000";// this.rbtnlist.SelectedValue.ToString();
            string wrkdpt = "000000000000";
            DateTime time = System.DateTime.Now;
            string qcdate = this.Request.QueryString["followupdate"] ?? "";
            string cdate = qcdate.Length == 0 ? this.txtfrmdate.Text + " " + time.ToString("HH:mm") : qcdate;


            DataSet ds1 = instcrm.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "DAILYDISCUIND", Empid, Client, kpigrp, "", wrkdpt, cdate);


            // DataSet ds1 = KpiData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "DAILYDISCUIND", Empid, Client, kpigrp, "", wrkdpt, cdate);
            ViewState["tbModalData"] = HiddenSameData(ds1.Tables[0]);
            this.Modal_Data_Bind();



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
            DataView dvLeadStatus;
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

                        ////////////////
                        //////// this below code disbale lead status apply for all company
                        //////////  Nahid 20221222
                        int index = 0;
                        string holdLost = "";
                        DataRow[] rows = dts.Select("gcod='" + lstleadstatus + "'");
                        if (rows.Length > 0)
                        {
                            index = Convert.ToInt32(rows[0]["rowid"]);
                            if((rows[0]["gcod"].ToString()=="9501020") || (rows[0]["gcod"].ToString() == "9501028"))
                            {
                                holdLost = "hold_lost";//rows[0]["gcod"].ToString();
                            }                            
                        }
                        if(holdLost != "hold_lost")// if hold or lost then lead status enable req by emadad bhai and raihan
                        {
                            index = index - 1;
                            for (int p = 0; p < ChkBoxLstStatus.Items.Count; p++)
                            {
                                if (p < index)
                                {
                                    ChkBoxLstStatus.Items[p].Enabled = false;
                                }
                            }
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

        protected void gvInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    TextBox txtgvdVal = (TextBox)e.Row.FindControl("txtgvdVal");
            //    DropDownList ddlhour = (DropDownList)e.Row.FindControl("ddlhour");
            //    DropDownList ddlMmin = (DropDownList)e.Row.FindControl("ddlMmin");
            //    DropDownList ddlslb = (DropDownList)e.Row.FindControl("ddlslb");


            //    string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "gcod")).ToString();

            //    DataSet copSetup = compUtility.GetCompUtility();
            //    if (copSetup == null)

            //        return;
            //    bool bakdatain =  Convert.ToBoolean(copSetup.Tables[0].Rows[0]["crm_backdatain"]);

            //    // string edit = this.lblEdit.Text.Trim();
            //    if (this.Request.QueryString["Type"].ToString() == "Edit")
            //    {

            //        if (code == "810100101001")
            //        {

            //            txtgvdVal.Enabled = false;
            //            ddlhour.Enabled = false;
            //            ddlMmin.Enabled = false;
            //            ddlslb.Enabled = false;

            //        }
            //    }


            //}
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
                        result = instcrm.UpdateTransInfo3(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "INSERTUPDATESCDINF", hempid, Client, kpigrp, "", wrkdpt, cdate, Gcode, gtype, Gvalue, remarks);

                        if (result)
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully";
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

                        }
                        else
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = "Update Fail";
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
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




        protected void lbtnCancel_Click(object sender, EventArgs e)
        {

        }
        protected void lbtnFollowup_Click(object sender, EventArgs e)
        {

        }
        protected void lbtnAddition_Click(object sender, EventArgs e)
        {

        }

        protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DataTable dt = (DataTable)ViewState["tblappmnt"];
            //DataTable ddt = (DataTable)ViewState["tbother"];
            //DataSet ds1 = (DataSet)ViewState["tblproaunit"];
            //string pactcode = ((DropDownList)this.gvInfo.Rows[1].FindControl("ddlProject")).Text.Trim();
            //string usircode = ((DropDownList)this.gvInfo.Rows[2].FindControl("ddlUnit")).Text.Trim();
            ////for (int i = 0; i < this.gvInfo.Rows.Count; i++)
            ////{

            //DataTable dt1 = ds1.Tables[1].Copy();
            //DataView dv1;
            //dv1 = dt1.DefaultView;
            //dv1.RowFilter = ("pactcode='000000000000' or pactcode='" + pactcode + "'");
            //dt1 = dv1.ToTable();
            //((TextBox)this.gvInfo.Rows[3].FindControl("txtgvVal")).Text = ((usircode == "000000000000") ? 0 : Convert.ToDouble((dt1.Select("pactcode='" + pactcode + "' and usircode='" + usircode + "'"))[0]["usize"])).ToString("#,##0;(#,##0); ");
            //((TextBox)this.gvInfo.Rows[4].FindControl("txtgvVal")).Text = ((usircode == "000000000000") ? 0 : Convert.ToDouble((dt1.Select("pactcode='" + pactcode + "' and usircode='" + usircode + "'"))[0]["rate"])).ToString("#,##0;(#,##0); ");
            //((TextBox)this.gvInfo.Rows[5].FindControl("txtgvVal")).Text = ((usircode == "000000000000") ? 0 : Convert.ToDouble((dt1.Select("pactcode='" + pactcode + "' and usircode='" + usircode + "'"))[0]["pamt"])).ToString("#,##0;(#,##0); ");
            //((TextBox)this.gvInfo.Rows[6].FindControl("txtgvVal")).Text = ((usircode == "000000000000") ? 0 : Convert.ToDouble((dt1.Select("pactcode='" + pactcode + "' and usircode='" + usircode + "'"))[0]["othamt"])).ToString("#,##0;(#,##0); ");
            //((TextBox)this.gvInfo.Rows[7].FindControl("txtgvVal")).Text = ((usircode == "000000000000") ? 0 : Convert.ToDouble((dt1.Select("pactcode='" + pactcode + "' and usircode='" + usircode + "'"))[0]["tuamt"])).ToString("#,##0;(#,##0); ");



        }
        protected void lnkbtnKpi_Click(object sender, EventArgs e)
        {

            this.lbltodatekpi.Visible = true;
            this.txtkpitodate.Visible = true;
            this.hdnlblattribute.Value = "Kpi";
            this.EmpMonthlyKPI();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string events = hst["events"].ToString();
            if (Convert.ToBoolean(events) == true)
            {
                string eventtype = "Show Key Performance Indicator (Sales CRM)";
                string eventdesc = "Show Key Performance Indicator (Sales CRM)";
                string eventdesc2 = "";
                string comcod = this.GetCompCode();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }


        }

        private string GetMonkpiCallType()
        {
            string calltype = "";
            string comcod = this.GetComeCode();
            switch (comcod)
            {
                case "3354"://Edison
                case "3101":
                    calltype = "RPTMONTHLYKPIEDISON";
                    break;
                default:
                    calltype = "RPTMONTHLYKPI";
                    break;




            }
            return calltype;

        }

        private void EmpMonthlyKPI()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userrole = hst["userrole"].ToString();
            string comcod = this.GetComeCode();
            string frmdate = this.txttodate.Text.Trim();
            string todate = this.txtkpitodate.Text.Trim();

            string empid = (hst["empid"].ToString() == "" ? "93" : hst["empid"].ToString());
            if (userrole == "1")
            {
                empid = "%";
            }
            string calltype = this.GetMonkpiCallType();
            DataSet ds1 = instcrm.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", calltype, "8301%", frmdate, todate, empid);

            Session["tbltempdt"] = ds1.Tables[0];
            this.gvSummary.DataSource = null;
            this.gvSummary.DataBind();
            this.gvkpi.DataSource = ds1.Tables[0];
            this.gvkpi.DataBind();
            this.footerCalculations();
        }
        private void footerCalculations()
        {
            DataTable dt1 = (DataTable)Session["tbltempdt"];
            if (dt1.Rows.Count == 0)
                return;

            if (dt1.Rows.Count > 0)
            {
                Session["Report1"] = gvkpi;
                ((HyperLink)this.gvkpi.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }

            ((Label)this.gvkpi.FooterRow.FindControl("lblgvFcallsum")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(call)", "")) ?
                    0.00 : dt1.Compute("sum(call)", ""))).ToString("#,##0;(#,##0);-");

            ((Label)this.gvkpi.FooterRow.FindControl("lblgvFexmeetingsum")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(extmeeting)", "")) ?
            0.00 : dt1.Compute("sum(extmeeting)", ""))).ToString("#,##0;(#,##0);-");


            ((Label)this.gvkpi.FooterRow.FindControl("lblgvFintmeetingsum")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(intmeeting)", "")) ?
                    0.00 : dt1.Compute("sum(intmeeting)", ""))).ToString("#,##0;(#,##0);-");

            ((Label)this.gvkpi.FooterRow.FindControl("lblgvFvisitsum")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(visit)", "")) ?
               0.00 : dt1.Compute("sum(visit)", ""))).ToString("#,##0;(#,##0);-");

            ((Label)this.gvkpi.FooterRow.FindControl("lblgvFproposalsum")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(proposal)", "")) ?
               0.00 : dt1.Compute("sum(proposal)", ""))).ToString("#,##0;(#,##0);-");

            ((Label)this.gvkpi.FooterRow.FindControl("lblgvFleadssum")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(leads)", "")) ?
                0.00 : dt1.Compute("sum(leads)", ""))).ToString("#,##0;(#,##0);-");




            ((Label)this.gvkpi.FooterRow.FindControl("lblgvFotherssum")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(others)", "")) ?
              0.00 : dt1.Compute("sum(others)", ""))).ToString("#,##0;(#,##0);-");

            ((Label)this.gvkpi.FooterRow.FindControl("lblgvFclosingsum")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(close)", "")) ?
              0.00 : dt1.Compute("sum(close)", ""))).ToString("#,##0;(#,##0);-");

            ((Label)this.gvkpi.FooterRow.FindControl("lblgvFtotalsum")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(total)", "")) ?
                0.00 : dt1.Compute("sum(total)", ""))).ToString("#,##0;(#,##0);-");


        }


        protected void lnkBtnComments_Click(object sender, EventArgs e)
        {
            string rtype = "comments";
            if (ShowComments(rtype))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openComModal();", true);
            }

        }

        public bool ShowComments(string rtype)
        {
            ProcessAccess JData = new ProcessAccess();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            DataTable dt = new DataTable();
            this.gvComments.DataSource = null;
            this.gvComments.DataBind();
            string type = rtype;
            string comcod = this.GetComeCode();
            //  string empid = (hst["empid"].ToString() == "" ? "93" : hst["empid"].ToString()) + "%";
            string ddlempid = this.ddlEmpid.SelectedValue.ToString();
            string empid = (ddlempid == "000000000000" ? ((hst["empid"].ToString() == "" ? "93" : hst["empid"].ToString())) : ddlempid) + "%";
            string tdate = this.txttodate.Text.ToString();
            DataSet ds1 = JData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "GETNOTIFICATIONDETAILS", "8301%", empid, type, tdate);
            if (ds1.Tables[0].Rows.Count != 0)
            {
                ViewState["tblComData"] = ds1.Tables[0];
                dt = ds1.Tables[0];
                Session["tblCommentData"] = dt;
                this.gvComments.DataSource = ds1.Tables[0];
                this.gvComments.DataBind();
                //this.gvSummary.Visible = false;
            }

            this.gvkpi.DataSource = null;
            this.gvkpi.DataBind();
            return true;

        }
        protected void btnSaveComments_Click(object sender, EventArgs e)
        {
            DataTable dt1 = (DataTable)Session["tblCommentData"];

            for (int i = 0; i < this.gvComments.Rows.Count; i++)
            {
                string sircode1 = ((Label)gvComments.Rows[i].FindControl("lblcomsircode1")).Text.ToString();
                string generated = ((Label)gvComments.Rows[i].FindControl("lblcomgenerated")).Text.ToString(); ;
                string sirdesc = ((Label)gvComments.Rows[i].FindControl("lblcomdesc")).Text.ToString();
                string prefdesc = ((Label)gvComments.Rows[i].FindControl("lblcomprefdesc")).Text.ToString();
                string assoc = ((Label)gvComments.Rows[i].FindControl("lblcomassoc")).Text.ToString();
                string teamdesc = ((Label)gvComments.Rows[i].FindControl("lblcomTeam")).Text.ToString();
                string comment = ((Label)gvComments.Rows[i].FindControl("lblComments")).Text.ToString();
                CheckBox chk = ((CheckBox)gvComments.Rows[i].FindControl("chkCommentView"));

                string checkstatus = (chk.Checked == true) ? "True" : "False";
                //string checkstatu1s = (((CheckBox)gvEmployeeInfo.Rows[i].FindControl("CheckPermission")).Checked) ? "True" : "False";

                string sirc = "";
                if (sircode1.Length > 6)
                {
                    sirc = sircode1.Substring(sircode1.Length - 6);
                }
                dt1.Rows[i]["sircode1"] = sirc;
                dt1.Rows[i]["generated"] = generated;
                //dt1.Rows[i]["ownname"] = prospect;
                dt1.Rows[i]["sirdesc"] = sirdesc;
                dt1.Rows[i]["assoc"] = assoc;
                dt1.Rows[i]["teamdesc"] = teamdesc;
                dt1.Rows[i]["gnote"] = comment;
                dt1.Rows[i]["comview"] = checkstatus;
                Session["tblCommentData"] = dt1;
            }

            if (updateCommentView())
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Comment View Successfully!!";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "closeComModal();", true);
                this.GetNotificationinfo();
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string events = hst["events"].ToString();
            if (Convert.ToBoolean(events) == true)
            {
                string eventtype = "Data Saved (Sales CRM)";
                string eventdesc = "Data Saved (Sales CRM)";
                string eventdesc2 = "";
                string comcod = this.GetCompCode();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);



            }



        }

        private bool updateCommentView()
        {
            DataTable dt1 = (DataTable)Session["tblCommentData"];
            DataView dv = dt1.DefaultView;
            dv.RowFilter = ("comview=true");
            DataSet ds1 = new DataSet("ds1");
            ds1.Tables.Add(dv.ToTable());
            ds1.Tables[0].TableName = "tbl1";
            string comcod = this.GetComeCode();
            //string ss = ds1.GetXml();
            bool result = instcrm.UpdateXmlTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "UPDATECOMMENTVIEW", ds1, null, null);
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = instcrm.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "closeComModal();", true);
            }

            return true;
        }


        private void PurConBillFinal_Print()
        {
            string comcod = this.GetCompCode();

            this.CRMClientInformation();

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void CRMClientInformation()
        {
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            //string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd-MMM-yyyy");
            string txtuserinfo = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            DataTable dt = (DataTable)Session["tblsummData"];
            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_21_Mkt.ECRMClientInfo.CrmClientInfo>();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_21_Mkt.RptCRMClientInfo", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("RptTile", "Client Information"));
            Rpt1.SetParameters(new ReportParameter("Date", printdate));
            Rpt1.SetParameters(new ReportParameter("txtusername", "User Name: " + username));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));

            Rpt1.SetParameters(new ReportParameter("printFooter", txtuserinfo));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
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





        //protected void gvSummary_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    this.gvSummary.PageIndex = e.NewPageIndex;
        //    this.GetGridSummary();
        //}
        //protected void ddlcrmpagesize_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    this.gvSummary.PageSize = Convert.ToInt32(this.ddlcrmpagesize.SelectedValue.ToString());
        //    this.gvSummary.DataSource = (DataTable)ViewState["tblsummData"];
        //    this.gvSummary.DataBind();
        //}


        protected void lnkbtnNotes_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openNotesModal();", true);
        }

        protected void lnkgvkpicall_Click(object sender, EventArgs e)
        {
            try
            {

                string comcod = this.GetComeCode();
                int rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                string gempid = ((Label)this.gvkpi.Rows[rowindex].FindControl("lblgbempid")).Text;
                string folltype = "9601001";
                GetGridSummary_FollowupTye(gempid, folltype);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenKpiDetailsModal();", true);

            }

            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

            }
        }

        private void GetGridSummary_FollowupTye(string teamcode, string Followuptype)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userrole = hst["userrole"].ToString();
            string Empid = teamcode;// ((hst["empid"].ToString() == "") ? "%" : hst["empid"].ToString());
            if (userrole == "1")
            {
                Empid = "%";
            }
            string comcod = this.GetComeCode();

            string frmdate = this.txtfrmdate.Text.ToString();
            string todate = this.txttodate.Text.ToString();
            DataSet ds3 = instcrm.GetTransInfoNew(comcod, "SP_ENTRY_CRM_MODULE", "RPT_MONTHLY_KPI_DETAILS", null, null, null, "8301%", todate, frmdate, Empid, Followuptype);
            if (ds3 == null)
            {
                this.gvKpiDetials.DataSource = null;
                this.gvKpiDetials.DataBind();
                return;
            }

            this.gvKpiDetials.DataSource = ds3.Tables[0];
            this.gvKpiDetials.DataBind();

        }

        protected void lnkkpiDetrailsExMet_Click(object sender, EventArgs e)
        {
            try
            {

                string comcod = this.GetComeCode();
                int rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                string gempid = ((Label)this.gvkpi.Rows[rowindex].FindControl("lblgbempid")).Text;
                string folltype = "9601004";
                GetGridSummary_FollowupTye(gempid, folltype);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenKpiDetailsModal();", true);

            }

            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

            }
        }

        protected void lnkkpiDetrailsINtMet_Click(object sender, EventArgs e)
        {
            try
            {

                string comcod = this.GetComeCode();
                int rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                string gempid = ((Label)this.gvkpi.Rows[rowindex].FindControl("lblgbempid")).Text;
                string folltype = "9601008";
                GetGridSummary_FollowupTye(gempid, folltype);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenKpiDetailsModal();", true);

            }

            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

            }
        }

        protected void lnkkpiDetrailsVisit_Click(object sender, EventArgs e)
        {
            try
            {

                string comcod = this.GetComeCode();
                int rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                string gempid = ((Label)this.gvkpi.Rows[rowindex].FindControl("lblgbempid")).Text;
                string folltype = "9601016";
                GetGridSummary_FollowupTye(gempid, folltype);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenKpiDetailsModal();", true);

            }

            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

            }
        }

        protected void lnkkpiDetrailsProposal_Click(object sender, EventArgs e)
        {
            try
            {

                string comcod = this.GetComeCode();
                int rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                string gempid = ((Label)this.gvkpi.Rows[rowindex].FindControl("lblgbempid")).Text;
                string folltype = "9601020";
                GetGridSummary_FollowupTye(gempid, folltype);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenKpiDetailsModal();", true);

            }

            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

            }
        }

        protected void lnkkpiOrhers_Click(object sender, EventArgs e)
        {
            try
            {

                string comcod = this.GetComeCode();
                int rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                string gempid = ((Label)this.gvkpi.Rows[rowindex].FindControl("lblgbempid")).Text;
                string folltype = "9601024";
                GetGridSummary_FollowupTye(gempid, folltype);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenKpiDetailsModal();", true);

            }

            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

            }
        }

        protected void lblgvkpileads_Click(object sender, EventArgs e)
        {
            try
            {

                string comcod = this.GetComeCode();
                int rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                string gempid = ((Label)this.gvkpi.Rows[rowindex].FindControl("lblgbempid")).Text;
                string folltype = "9601001";
                GetGridSummary_FollowupTye(gempid, folltype);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenKpiDetailsModal();", true);

            }

            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

            }
        }

        protected void lblgvkpisigned_Click(object sender, EventArgs e)
        {
            try
            {

                string comcod = this.GetComeCode();
                int rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                string gempid = ((Label)this.gvkpi.Rows[rowindex].FindControl("lblgbempid")).Text;
                string folltype = "9601001";
                GetGridSummary_FollowupTye(gempid, folltype);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenKpiDetailsModal();", true);

            }

            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

            }
        }

        protected void gvSummary_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvSummary.PageIndex = e.NewPageIndex;
            this.Data_Bind();
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


            DataTable dt = (DataTable)Session["tblsummData"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string comcod = this.GetComeCode();
            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            int rowno = (this.gvSummary.PageSize) * (this.gvSummary.PageIndex) + RowIndex;
            string proscod = dt.Rows[RowIndex]["sircode"].ToString();
            bool result = instcrm.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "RETREIVEPROSPECT", null, null, null, proscod, userid, Posteddat, "", "", "", "", "", "", "", "", "", "", "", "", "");


            if (!result)
            {


                ((Label)this.Master.FindControl("lblmsg")).Text = "Retreive Fail";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;

            }


            //dt.Rows[RowIndex].Delete();
            dt.Rows[rowno].Delete();

            DataView dv = dt.DefaultView;
            Session.Remove("tblsummData");
            Session["tblsummData"] = dv.ToTable();
            this.Data_Bind();

            ((Label)this.Master.FindControl("lblmsg")).Text = "Successfully Retreived";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
        }


        protected void lblgvkpitotal_Click(object sender, EventArgs e)
        {
            this.lblkpiDetails.Visible = true;

            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string empid = ((Label)this.gvkpi.Rows[RowIndex].FindControl("lblgbempid")).Text.Trim() + "%";
            string empname = ((Label)this.gvkpi.Rows[RowIndex].FindControl("lowner")).Text.Trim();

            lblkpiDetails.Text = "Kpi Details: " + empname;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string frmdate = this.txttodate.Text.Trim();
            string todate = this.txtkpitodate.Text.Trim();

            DataSet ds1 = instcrm.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "RPTEMPKPIDETAILS", "8301%", frmdate, todate, empid);


            this.gvSummary.DataSource = null;
            this.gvSummary.DataBind();
            this.gvkpidet.DataSource = ds1.Tables[0];
            this.gvkpidet.DataBind();

            if (gvkpidet.Rows.Count > 0)
            {
                Session["Report1"] = gvkpidet;
                ((HyperLink)this.gvkpidet.HeaderRow.FindControl("hlbtntbCdataExelkpisum")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }

        }
        protected void gvkpidet_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                int index = e.Row.RowIndex;
                Panel Lbtn = (Panel)e.Row.FindControl("pnlfollowupkpisum");
                Lbtn.Attributes.Add("onmouseover", "AddButton(" + index + ")");
                Lbtn.Attributes.Add("onmouseout", "HiddenButton(" + index + ")");
                Lbtn.Attributes.Add("style", "cursor:pointer");
                LinkButton Lbtn1 = (LinkButton)e.Row.FindControl("lnkEditfollowupkpisum");
                Lbtn1.Attributes.Add("class", "hiddenb" + index);
                Lbtn1.Attributes.Add("style", "display:none");

                LinkButton lbtnView = (LinkButton)e.Row.FindControl("lbtnViewkpisum");
                lbtnView.Attributes.Add("class", "hiddenb" + index);
                lbtnView.Attributes.Add("style", "display:none");


                Label ldesc = (Label)e.Row.FindControl("ldesckpisum");
                LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDeletekpisum");
                LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEditkpisum");
                //  LinkButton lnkAct = (LinkButton)e.Row.FindControl("lnkAct");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                // string comcod = hst["comcod"].ToString();
                string proscod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "sircode")).ToString();
                //  string dealcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "dealcode")).ToString();
                //string Empid = hst["empid"].ToString();




                if (proscod.Length == 7)
                {
                    lnkDelete.Visible = false;
                    lnkEdit.Visible = false;
                    ldesc.Attributes["style"] = "font-size:13px; font-weight:bold; color:maroon;";
                }



            }

        }

        protected void lnkDeletekpisum_Click(object sender, EventArgs e)
        {

        }
        protected void lbtnViewkpisum_Click(object sender, EventArgs e)
        {

        }
        protected void lnkEditfollowupkpisum_Click(object sender, EventArgs e)
        {

        }
        protected void lnkEditkpisum_Click(object sender, EventArgs e)
        {

        }

        protected void ddlRating_SelectedIndexChanged(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string ratevalue = this.ddlRating.SelectedValue.ToString();


            string clientid = this.lblproscod.Value.ToString();

            bool result = instcrm.UpdateXmlTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "UPDATE_CLINT_RATE", null, null, null, clientid, ratevalue);
            if (result == true)
            {
                Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }
            else
            {


            }
        }

        protected void ddlval_SelectedIndexChanged(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userrole = hst["userrole"].ToString();
            string comcod = this.GetComeCode();

            int RowIndex = ((GridViewRow)((DropDownList)sender).NamingContainer).RowIndex;
            string ddlValue = ((DropDownList)this.gvSourceInfo.Rows[RowIndex].FindControl("ddlval")).SelectedValue;
            string Gcode = ((Label)this.gvSourceInfo.Rows[RowIndex].FindControl("lblgvItmCode")).Text.Trim();
            if (Gcode == "0302005")
            {
                DataSet ds2 = instcrm.GetTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "GETSUPERVISORLISTBYID", ddlValue, "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;
                string teamid = (ds2.Tables[0].Rows[0]["teamid"].ToString() == "" ? "93%" : ds2.Tables[0].Rows[0]["teamid"].ToString());
                DataView dv1;
                dv1 = ((DataTable)ViewState["tblsubddl"]).Copy().DefaultView; 
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

            //IR EPIC
            if (ddlValue == "3101010" && comcod == "3367") 
            {
                DataSet ds3 = instcrm.GetTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "GET_IR_EMPLOYEE", "", "", "", "", "", "", "", "", "");
                if (ds3 == null)
                    return;
               
                ((TextBox)this.gvSourceInfo.Rows[RowIndex].FindControl("txtgvVal")).Visible = false;
                ((TextBox)this.gvSourceInfo.Rows[RowIndex].FindControl("txtgvdVal")).Visible = false;
                //((Panel)this.gvSourceInfo.Rows[RowIndex].FindControl("Panegrd")).Visible = false;
                ((Panel)this.gvSourceInfo.Rows[RowIndex].FindControl("pnlIREmp")).Visible = true;
                ddlgval = ((DropDownList)this.gvSourceInfo.Rows[RowIndex].FindControl("ddlIREmp"));
                ddlgval.DataTextField = "empname";
                ddlgval.DataValueField = "empid";
                ddlgval.DataSource = ds3.Tables[0];
                ddlgval.DataBind();
            }
            else
            {
                ((Panel)this.gvSourceInfo.Rows[RowIndex].FindControl("pnlIREmp")).Visible = false;
                ((DropDownList)this.gvSourceInfo.Rows[RowIndex].FindControl("ddlIREmp")).Items.Clear();
            }


        }

        protected void ddlval_DataBound(object sender, EventArgs e)
        {

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
                    ((TextBox)this.gvPersonalInfo.Rows[RowIndex].FindControl("txtgvVal")).BorderColor = Color.Red;
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



        protected void ChkBoxLstFollow_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string userrole = hst["userrole"].ToString();
            //string comcod = this.GetComeCode();


            //int RowIndex = ((GridViewRow)((CheckBoxList)sender).NamingContainer).RowIndex;

            //string Gcode = ((Label)this.gvInfo.Rows[RowIndex].FindControl("lblgvItmCodedis")).Text.Trim();
            //string gvalue = ((CheckBoxList)this.gvInfo.Rows[RowIndex].FindControl("ChkBoxLstStatus")).SelectedValue.ToString();
            //if (Gcode == "810100101002" && gvalue == "9601050")
            //{

            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModaldis();", true);

            //}

        }

        protected void lnkbtnTODayTask_Click(object sender, EventArgs e)
        {
            string rtype = "tdt";
            this.ShowNotifications(rtype);
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string events = hst["events"].ToString();
            if (Convert.ToBoolean(events) == true)
            {
                string eventtype = "Daily work Schedule (sales CRM)";
                string eventdesc = "Daily work Schedule (sales CRM)";
                string eventdesc2 = "";
                string comcod = this.GetCompCode();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
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
    }












}


