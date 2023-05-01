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
    public partial class AddLead : System.Web.UI.Page
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
                ((Label)this.Master.FindControl("lblTitle")).Text = "Client Information";
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();


                ((Label)this.Master.FindControl("lblmsg")).Visible = false;
                this.txtentryEmpID.Visible = false;
                string comcod = this.GetComeCode();              
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");

                this.GetAllSubdata();               
                this.GETEMPLOYEEUNDERSUPERVISED();
                this.GetFollow();                           
                this.IsTeamLeader();
                this.GetIRComARefProspect();
                //this.SoldInfo();

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
                if (this.Request.QueryString["Type"].ToString()=="Edit")
                {
                    //lbllandname.Visible = true;
                    //LinkButton btn = (LinkButton)sender;
                    //GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
                    //int index = row.RowIndex;
                    //string comcod = GetComeCode();
                    //string styleid = ((Label)this.gvSummary.Rows[index].FindControl("lsircode")).Text.ToString();
                    //string clintIdno = ((Label)this.gvSummary.Rows[index].FindControl("lsircode1")).Text.ToString();

                    //lbllandname.Text = ((Label)this.gvSummary.Rows[index].FindControl("lsircode1")).Text.ToString() + ':' + ((Label)this.gvSummary.Rows[index].FindControl("ldesc")).Text.ToString();
                    ViewState["existclientcode"] = this.Request.QueryString["sircode"].ToString();
               
                    GetData();                   
                    ShowPersonalInfo();
                    ShowSourceInfo();
                    Showpinfo();
                    ShowhomeInfo();
                    Showbusinfo();
                    ShowMoreInfo();
                    //btnaddland.Text = "Back";

                    //string Message = "Edit Client Form";

                    //ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Message + "');", true);


                    //Hashtable hst = (Hashtable)Session["tblLogin"];
                    //string events = hst["events"].ToString();
                    //if (Convert.ToBoolean(events) == true)
                    //{
                    //    string eventtype = "Edit Client Information (Sales CRM)";
                    //    string eventdesc = "Edit Client Information (Sales CRM)";
                    //    string eventdesc2 = "Edit " + clintIdno;

                    //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);



                    //} 
                }
                else
                {
                    try
                    {
                       
                        string empid = hst["empid"].ToString();
                        string usrid = hst["usrid"].ToString();
                        string userrole = hst["userrole"].ToString();
                        if (empid == "" && userrole != "1")
                        {
                            string Messaged = "Please add employee id in the user permission! Thank you.";
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Messaged + "');", true);
                            return;
                        }

                        GetData();
                        this.lbllandname.Text = "";                        

                            ShowPersonalInfo();
                            ShowSourceInfo();
                            Showpinfo();
                            ShowhomeInfo();
                            Showbusinfo();
                            ShowMoreInfo();
                           
                            string Message = "Add Client Form";
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Message + "');", true);


                            this.lblnewprospect.Value = "";

                        
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    }
                }


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

        private void GetIRComARefProspect()
        {
            Session.Remove("tblircapro");
            string comcod = this.GetComeCode();
            DataSet ds1 = instcrm.GetTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "GET_IR_EMPLOYEE", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            Session["tblircapro"] = ds1.Tables[0];
            ds1.Dispose();


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
                    //Sub Source
                    case "0302001":
                        switch (comcod)
                        {
                            //For Not Changing Source without Team Leader                            
                            case "3367"://Epic
                            case "3354"://Edison
                            case "3101":
                                dv1 = ((DataTable)ViewState["tblsubddl"]).Copy().DefaultView;
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
                                dv1 = ((DataTable)ViewState["tblsubddl"]).Copy().DefaultView;
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

                        ////IR EPIC
                        //switch (comcod)
                        //{
                        //    case "3367":                                
                        //        empid = dt.Rows[i]["empid"].ToString();
                        //        if (lbllandname.Text.Length > 0)
                        //        {
                        //            DataSet ds3 = instcrm.GetTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "GET_IR_EMPLOYEE", "", "", "", "", "", "", "", "", "");
                        //            if (ds3 == null)
                        //                return;

                        //            ((TextBox)this.gvSourceInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        //            ((TextBox)this.gvSourceInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        //            ((Panel)this.gvSourceInfo.Rows[i].FindControl("pnlIREmp")).Visible = true;
                        //            ddlgval = ((DropDownList)this.gvSourceInfo.Rows[i].FindControl("ddlIREmp"));
                        //            ddlgval.DataTextField = "empname";
                        //            ddlgval.DataValueField = "empid";
                        //            ddlgval.DataSource = ds3.Tables[0];
                        //            ddlgval.DataBind();
                        //            ddlgval.SelectedValue = empid == "" ? "" : empid;
                        //        }
                        //        break;

                        //    default:
                        //        break;
                        //}
                        //this.ddlval_SelectedIndexChanged(null, null);

                        this.DataBindIRaProspect(i, ddlgval.SelectedValue.ToString());
                        break;

                    //Main Source
                    case "0302006":
                        dv1 = ((DataTable)ViewState["tblsubddl"]).Copy().DefaultView;
                        dv1.RowFilter = ("gcod like '29%'");
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
                        //  this.DataBindIRaProspect(i, ddlgval.SelectedValue.ToString());
                        break;

                    //Team Leader
                    case "0302003":

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

                    //Assign Persion 
                    case "0302005":
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
                        dv1 = ((DataTable)ViewState["tblsubddl"]).Copy().DefaultView;
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
                        dv1 = ((DataTable)ViewState["tblsubddl"]).Copy().DefaultView;
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
                        dv1 = ((DataTable)ViewState["tblsubddl"]).Copy().DefaultView;
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
                        dv1 = ((DataTable)ViewState["tblsubddl"]).Copy().DefaultView;
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
                        dv1 = ((DataTable)ViewState["tblsubddl"]).Copy().DefaultView;
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
                        dv1 = ((DataTable)ViewState["tblsubddl"]).Copy().DefaultView;
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


        protected void ddlval_SelectedIndexChanged(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userrole = hst["userrole"].ToString();
            string comcod = this.GetComeCode();

            int RowIndex = ((GridViewRow)((DropDownList)sender).NamingContainer).RowIndex;
            string ddlValue = ((DropDownList)this.gvSourceInfo.Rows[RowIndex].FindControl("ddlval")).SelectedValue;
            string Gcode = ((Label)this.gvSourceInfo.Rows[RowIndex].FindControl("lblgvItmCode")).Text.Trim();

            //DataTable dt1 = ((DataTable)ViewState["tblsubddl"]).Copy();
            if (ddlValue == "2901001")
            {
                DataView dv5;
                dv5 = ((DataTable)ViewState["tblsubddl"]).Copy().DefaultView;
                dv5.RowFilter = ("code like '2901001%'");
                ddlgval = ((DropDownList)this.gvSourceInfo.Rows[RowIndex + 1].FindControl("ddlval"));

                ddlgval.DataTextField = "gdesc";
                ddlgval.DataValueField = "gcod";
                ddlgval.DataSource = dv5;
                ddlgval.DataBind();
            }
            if (ddlValue == "2901002")
            {
                DataView dv5;
                dv5 = ((DataTable)ViewState["tblsubddl"]).Copy().DefaultView;
                dv5.RowFilter = ("code like '2901002%'");
                ddlgval = ((DropDownList)this.gvSourceInfo.Rows[RowIndex + 1].FindControl("ddlval"));

                ddlgval.DataTextField = "gdesc";
                ddlgval.DataValueField = "gcod";
                ddlgval.DataSource = dv5;
                ddlgval.DataBind();
            }
            if (ddlValue == "2901003")
            {
                DataView dv5;
                dv5 = ((DataTable)ViewState["tblsubddl"]).Copy().DefaultView;
                dv5.RowFilter = ("code like '2901003%'");
                ddlgval = ((DropDownList)this.gvSourceInfo.Rows[RowIndex + 1].FindControl("ddlval"));

                ddlgval.DataTextField = "gdesc";
                ddlgval.DataValueField = "gcod";
                ddlgval.DataSource = dv5;
                ddlgval.DataBind();
                this.DataBindIRaProspect(RowIndex, ddlValue);

            }
            if (ddlValue == "2901004")
            {
                DataView dv5;
                dv5 = ((DataTable)ViewState["tblsubddl"]).Copy().DefaultView;
                dv5.RowFilter = ("code like '2901004%'");
                ddlgval = ((DropDownList)this.gvSourceInfo.Rows[RowIndex + 1].FindControl("ddlval"));

                ddlgval.DataTextField = "gdesc";
                ddlgval.DataValueField = "gcod";
                ddlgval.DataSource = dv5;
                ddlgval.DataBind();
                this.DataBindIRaProspect(RowIndex, ddlValue);
            }

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

            //IR, F Prospect
            if (ddlValue == "3101040" || ddlValue == "3101028" || ddlValue == "3101030")
            {
                this.DataBindIRaProspect(RowIndex, ddlValue);


            }
            //else
            //{
            //    ((Panel)this.gvSourceInfo.Rows[RowIndex].FindControl("pnlIREmp")).Visible = false;
            //    ((DropDownList)this.gvSourceInfo.Rows[RowIndex].FindControl("ddlIREmp")).Items.Clear();
            //}


        }

        private void DataBindIRaProspect(int RowIndex, string gcod)
        {
            DataTable dts = ((DataTable)ViewState["tblDatagridsinfo"]).Copy();


            DataTable dt = ((DataTable)Session["tblircapro"]).Copy();
            DataView dv = dt.DefaultView;
            DataRow dr1 = dt.NewRow();
            dr1["empid"] = "";
            dr1["empname"] = "None";
            dt.Rows.Add(dr1);
            string empid = dts.Select("value='" + gcod + "'").Length == 0 ? "" : (dts.Select("value='" + gcod + "'"))[0]["empid"].ToString();



            switch (gcod)
            {

                case "2901003"://From Prospect

                    ((TextBox)this.gvSourceInfo.Rows[RowIndex + 1].FindControl("txtgvVal")).Visible = false;
                    ((TextBox)this.gvSourceInfo.Rows[RowIndex + 1].FindControl("txtgvdVal")).Visible = false;
                    //((Panel)this.gvSourceInfo.Rows[RowIndex].FindControl("Panegrd")).Visible = false;
                    ((Panel)this.gvSourceInfo.Rows[RowIndex + 1].FindControl("pnlIREmp")).Visible = true;
                    ddlgval = ((DropDownList)this.gvSourceInfo.Rows[RowIndex + 1].FindControl("ddlIREmp"));

                    dv.RowFilter = ("empid like '83%' or empname='None'");
                    break;


                case "3101028"://From Prospect

                    ((TextBox)this.gvSourceInfo.Rows[RowIndex].FindControl("txtgvVal")).Visible = false;
                    ((TextBox)this.gvSourceInfo.Rows[RowIndex].FindControl("txtgvdVal")).Visible = false;
                    //((Panel)this.gvSourceInfo.Rows[RowIndex].FindControl("Panegrd")).Visible = false;
                    ((Panel)this.gvSourceInfo.Rows[RowIndex].FindControl("pnlIREmp")).Visible = true;
                    ddlgval = ((DropDownList)this.gvSourceInfo.Rows[RowIndex].FindControl("ddlIREmp"));

                    dv.RowFilter = ("empid like '83%' or empname='None'");
                    break;


                case "2901004"://From Company official

                    ((TextBox)this.gvSourceInfo.Rows[RowIndex + 1].FindControl("txtgvVal")).Visible = false;
                    ((TextBox)this.gvSourceInfo.Rows[RowIndex + 1].FindControl("txtgvdVal")).Visible = false;
                    ((Panel)this.gvSourceInfo.Rows[RowIndex + 1].FindControl("pnlIREmp")).Visible = true;
                    ddlgval = ((DropDownList)this.gvSourceInfo.Rows[RowIndex + 1].FindControl("ddlIREmp"));
                    dv.RowFilter = ("empid like '93%' or empname='None'"); ;
                    break;

                case "3101030"://From Company official

                    ((TextBox)this.gvSourceInfo.Rows[RowIndex].FindControl("txtgvVal")).Visible = false;
                    ((TextBox)this.gvSourceInfo.Rows[RowIndex].FindControl("txtgvdVal")).Visible = false;
                    //((Panel)this.gvSourceInfo.Rows[RowIndex].FindControl("Panegrd")).Visible = false;
                    ((Panel)this.gvSourceInfo.Rows[RowIndex].FindControl("pnlIREmp")).Visible = true;
                    ddlgval = ((DropDownList)this.gvSourceInfo.Rows[RowIndex].FindControl("ddlIREmp"));
                    dv.RowFilter = ("empid like '93%' or empname='None'"); ;
                    break;

                case "3101040"://IR(Person)
                    ((TextBox)this.gvSourceInfo.Rows[RowIndex].FindControl("txtgvVal")).Visible = false;
                    ((TextBox)this.gvSourceInfo.Rows[RowIndex].FindControl("txtgvdVal")).Visible = false;
                    //((Panel)this.gvSourceInfo.Rows[RowIndex].FindControl("Panegrd")).Visible = false;
                    ((Panel)this.gvSourceInfo.Rows[RowIndex].FindControl("pnlIREmp")).Visible = true;
                    ddlgval = ((DropDownList)this.gvSourceInfo.Rows[RowIndex].FindControl("ddlIREmp"));
                    dv.RowFilter = ("empid like '93%' or empname='None'"); ;
                    break;


                default:
                    break;



            }
            dv.Sort = ("empid");
            dt = dv.ToTable();
            ddlgval.DataTextField = "empname";
            ddlgval.DataValueField = "empid";
            ddlgval.DataSource = dt;
            ddlgval.DataBind();
            ddlgval.SelectedValue = empid;



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

               else if (Gcode == "0301003")
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
               else if (Gcode == "0301005")
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
               else if (Gcode == "0301004")
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
               else if (Gcode == "0301007")
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
                    if (sourcecode == "3101040")
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

                               ViewState["existclientcode"] = null;
         
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
                       
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            //lbllandname.Visible = true;
            //LinkButton btn = (LinkButton)sender;
            //GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            //int index = row.RowIndex;
            //string comcod = GetComeCode();
            //string styleid = ((Label)this.gvSummary.Rows[index].FindControl("lsircode")).Text.ToString();
            //string clintIdno = ((Label)this.gvSummary.Rows[index].FindControl("lsircode1")).Text.ToString();

            //lbllandname.Text = ((Label)this.gvSummary.Rows[index].FindControl("lsircode1")).Text.ToString() + ':' + ((Label)this.gvSummary.Rows[index].FindControl("ldesc")).Text.ToString();
            //ViewState["existclientcode"] = styleid;
            //this.MultiView1.ActiveViewIndex = 0;
            //GetData();
            //GetAllSubdata();
            //ShowPersonalInfo();
            //ShowSourceInfo();
            //Showpinfo();
            //ShowhomeInfo();
            //Showbusinfo();
            //ShowMoreInfo();
            //btnaddland.Text = "Back";

            //string Message = "Edit Client Form";

            //ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Message + "');", true);


            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string events = hst["events"].ToString();
            //if (Convert.ToBoolean(events) == true)
            //{
            //    string eventtype = "Edit Client Information (Sales CRM)";
            //    string eventdesc = "Edit Client Information (Sales CRM)";
            //    string eventdesc2 = "Edit " + clintIdno;

            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);



            //}

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

           

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Client Info";
                string eventdesc = "Update Info";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

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
        public static string GetProjectUnit(string comcod, string pactcode)
        {


            ProcessAccess _processAccess = new ProcessAccess();

            DataSet ds2 = _processAccess.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "GETPROSPECIFICUNIT", pactcode, "", "", "", "", "", "", "", "", "");


            if (ds2.Tables[0].Rows.Count == 0)
            {
                var result = new { Message = "Unit", result = true };
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(result);
                return json;

            }


            else
            {

                var lst = ds2.Tables[0].DataTableToList<RealEntity.C_21_Mkt.ECRMClientInfo.EClassProjectUnit>().ToList();
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



        public static string ShowStatusSerial(string comcod, string empid, string proscod)
        {
            string kpigrp = "000000000000";
            string wrkdpt = "000000000000";
            //ProcessAccess JData = new ProcessAccess();
            //DataSet ds1 = JData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "DAILYDISCUIND", empid, proscod, kpigrp, "", wrkdpt, "", "", "", "", "");


            ////DataSet ds1 = instcrm.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "DAILYDISCUIND", Empid, Client, kpigrp, "", wrkdpt, cdate);

            ////   DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "DAILYLANDOWNERDISCUS", Empid, Client, kpigrp, "", wrkdpt, cdate);

            //if (ds1 == null)
            //{
            //    //List<RealEntity.C_32_Mis.EClassAcc_03.EclassBalSheetSum> lst5 = ds2.Tables[0].DataTableToList<RealEntity.C_32_Mis.EClassAcc_03.EclassBalSheetSum>();
            //    var lst = ds1.Tables[0].DataTableToList<RealEntity.C_01_LPA.BO_Fesibility.EclassPreLandownerDiscuss>().ToList();
            //    var jsonSerialiser = new JavaScriptSerializer();
            //    var json = jsonSerialiser.Serialize(lst);
            //    return json;
            //}

            //else
            //{
            //    var lst = ds1.Tables[0].DataTableToList<RealEntity.C_01_LPA.BO_Fesibility.EclassPreLandownerDiscuss>().ToList();
            //    // var lst = new { Message = "Update successfully.", result = true };
            //    var jsonSerialiser = new JavaScriptSerializer();
            //    var json = jsonSerialiser.Serialize(lst);
            //    return json;
            //}


            var lst = new { Message = "Update successfully.", result = true };
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(lst);
            return json;




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


        protected void lbtnView_Click(object sender, EventArgs e)
        {

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

        protected void lnkbtnNotes_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openNotesModal();", true);
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


