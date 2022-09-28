﻿using System;
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
using AjaxControlToolkit;
using System.IO;
using Microsoft.Reporting.WinForms;
namespace RealERPWEB.F_22_Sal
{
    public partial class SaleSurPrjInformation : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();

        public static string Url = "";
        int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = "Project Information";

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                if (this.ddlPrjName.Items.Count == 0)
                {
                    this.GetProjectName();
                }
                this.GetCatagory();
              
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
     

        private void GetCatagory()
        {
            string comcod = this.GetComCode();
            Session.Remove("tblcdesc");
            DataSet ds1 = this.MktData.GetTransInfo(comcod, "SP_ENTRY_PRJ_BUDGET", "GETCATAGORY", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                //this.ddlcatagory.DataSource = null;
                //this.ddlcatagory.DataBind();
                return;
            }
            Session["tblcdesc"] = ds1.Tables[0];




        }


        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void GetProjectName()
        {
            Session.Remove("tblpro");
            string comcod = this.GetComCode();
            string txtSProject = "%%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_PRJ_INFO", "GETEXPRJNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlPrjName.DataTextField = "actdesc";
            this.ddlPrjName.DataValueField = "actcode";
            this.ddlPrjName.DataSource = ds1.Tables[0];
            this.ddlPrjName.DataBind();
           // this.ddlPrjName.SelectedValue = this.Request.QueryString["prjcode"];
            Session["tblpro"] = ds1.Tables[0];


        }
        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                
                this.ddlPrjName.Enabled = false;
               
                this.LoadGrid();
                
            }
            else
            {
                this.lbtnOk.Text = "Ok";
                this.ddlPrjName.Enabled = true;
                this.ClearScreen();
            }
        }

        private void ClearScreen()
        {
                      
            this.gvPrjInfo.DataSource = null;
            this.gvPrjInfo.DataBind();
        }

        private void LoadGrid()
        {

            string comcod = this.GetComCode();
            string ProjectCode = this.ddlPrjName.SelectedValue.ToString();
            string fpactcode = (((DataTable)Session["tblpro"]).Select("actcode='" + ProjectCode + "'"))[0]["factcode"].ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_PRJ_INFO", "PROJECTINFO", ProjectCode, fpactcode, "", "", "", "", "", "", "");

            this.gvPrjInfo.DataSource = ds1.Tables[0];
            this.gvPrjInfo.DataBind();
            ViewState["projectEntry"] = ds1.Tables[0];
        
          


            



        }

        private void GridTextDDLVisible()
        {
            string comcod = this.GetComCode();
            DataTable dt = (DataTable)ViewState["projectEntry"];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string Gcode = dt.Rows[i]["gcod"].ToString();
                string val = dt.Rows[i]["gdesc1"].ToString();
                switch (Gcode)
                {
                    case "01003": //Start Date                

                        ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((DropDownList)this.gvPrjInfo.Rows[i].FindControl("ddlcataloc")).Visible = false;
                        break;

                    case "01004": //Start Date                   
                        ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((DropDownList)this.gvPrjInfo.Rows[i].FindControl("ddlcataloc")).Visible = false;
                        break;

                    case "02041": //Location                
                        ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((DropDownList)this.gvPrjInfo.Rows[i].FindControl("ddlcataloc")).Visible = true;
                        ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        DropDownList ddlcataloc = ((DropDownList)this.gvPrjInfo.Rows[i].FindControl("ddlcataloc"));

                        DataSet dsloc = MktData.GetTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "GETLOCATION", "", "", "", "", "", "", "", "", "");
                        ddlcataloc.DataTextField = "prgdesc";
                        ddlcataloc.DataValueField = "prgcod";
                        ddlcataloc.DataSource = dsloc.Tables[0];
                        ddlcataloc.DataBind();
                        ddlcataloc.SelectedValue = val.Length==3 ? "17"+val : val;
                        break;

                    case "02045": //Category                  
                        ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;

                        ((DropDownList)this.gvPrjInfo.Rows[i].FindControl("ddlcataloc")).Visible = true;
                        DropDownList ddlcatag = ((DropDownList)this.gvPrjInfo.Rows[i].FindControl("ddlcataloc"));

                        DataSet dscatg = MktData.GetTransInfo(comcod, "SP_ENTRY_LP_PROFEASIBILITY", "GETCATAGORY", "", "", "", "", "", "", "", "", "");
                        ddlcatag.DataTextField = "prgdesc";
                        ddlcatag.DataValueField = "prgcod";
                        ddlcatag.DataSource = dscatg.Tables[0];
                        ddlcatag.DataBind();
                        ddlcatag.SelectedValue = val.Length == 3 ? "99" + val : val;
                        break;

                    case "02050": //Construcation                  
                        ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;

                        ((DropDownList)this.gvPrjInfo.Rows[i].FindControl("ddlcataloc")).Visible = true;
                        DropDownList contype = ((DropDownList)this.gvPrjInfo.Rows[i].FindControl("ddlcataloc"));
                        contype.DataTextField = "description";
                        contype.DataValueField = "code";
                        contype.DataSource = (DataTable)Session["tblcdesc"];
                        contype.DataBind();
                        contype.SelectedValue = val;
                        break;

                    default:
                        ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((DropDownList)this.gvPrjInfo.Rows[i].FindControl("ddlcataloc")).Visible = false;
                        break;

                }
            }
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            this.PrintPrjInfo();
        }

        private void PrintPrjInfo()
        {
            //Nayan
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            //DataTable dt = (DataTable)ViewState["projectEntry"];
            string ProjectCode = this.ddlPrjName.SelectedValue.ToString();
            string prjname = this.ddlPrjName.SelectedItem.Text.ToString();
            string projname = prjname.Substring(13);


            string fpactcode = (((DataTable)Session["tblpro"]).Select("actcode='" + ProjectCode + "'"))[0]["factcode"].ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_PRJ_INFO", "PROJECTINFO", ProjectCode, fpactcode, "", "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0)
                return;

            DataTable dt = ds1.Tables[0];

            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_04_Bgd.BgdProInfo>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_04_Bgd.RptPrjInfoEnt", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("ProjectNam", projname));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Project Information"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        protected void lUpdatPerInfo_Click(object sender, EventArgs e)
        {
            try
            {

                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                if (!Convert.ToBoolean(dr1[0]["entry"]))
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                    return;
                }
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string pactcode = this.ddlPrjName.SelectedValue.ToString();


                // (((DataTable)Session["tblpro"]).Select("infcod='"+fpactcode+"'"))[0]["pactcode"];

                string fpactcode = (((DataTable)Session["tblpro"]).Select("actcode='" + pactcode + "'"))[0]["factcode"].ToString();

                for (int i = 0; i < this.gvPrjInfo.Rows.Count; i++)
                {
                    string Gcode = ((Label)this.gvPrjInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                    string gtype = ((Label)this.gvPrjInfo.Rows[i].FindControl("lgvgval")).Text.Trim();
                    //string Gvalue = ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                    string Gunit = ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtResunit")).Text.Trim();


                    string Gvalue1 = ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                    DropDownList ddlloc = ((DropDownList)this.gvPrjInfo.Rows[i].FindControl("ddlcataloc")) as DropDownList;

                    string Gvalue = "";

                    if (Gcode == "02041" || Gcode == "02045" || Gcode == "02050")
                    {
                      //  Gvalue = ASTUtility.Right(ddlloc.SelectedValue.ToString(),3);
                        Gvalue =ddlloc.SelectedValue.ToString();  //comment by tarik 

                    }
                    else
                    {
                        Gvalue = Gvalue1;
                    }

                    if (Gcode == "01003" || Gcode == "01004")
                    {
                        Gvalue = (((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim() == "") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim();
                    }

                    Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue) : (gtype == "N") ? Convert.ToDouble("0" + Gvalue).ToString() : Gvalue;
                    MktData.UpdateTransInfo(comcod, "SP_ENTRY_PRJ_INFO", "INSERTORUPDATEPRJINF", pactcode, Gcode, gtype, Gvalue, Gunit, fpactcode, "", "", "", "", "", "", "", "", "");
                }


                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                // ((Label)this.Master.FindControl("lblmsg")).Attributes["Style"] = "color:white; background:green; border:none;";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);




                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Project Information";
                    string eventdesc = "Update Project Information";
                    string eventdesc2 = "";
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }

            }
            catch (Exception ex)
            {

                ((Label)this.Master.FindControl("lblmsg")).Text = ex.Message;
                //((Label)this.Master.FindControl("lblmsg")).Attributes["Style"] = "color:white; background:red; border:none;";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

            }


        }


   
     

       

    }
}



