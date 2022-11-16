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
using AjaxControlToolkit;
using System.IO;
using Microsoft.Reporting.WinForms;
namespace RealERPWEB.F_24_CC
{
    public partial class CustCycInformation : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();

        public static string Url = "";
        int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = "Customer Life Cycle";

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                if (this.ddlPrjName.Items.Count == 0)
                {
                    this.GetProjectName();
                }
                this.GetCatagory();
                if (this.Request.QueryString["prjcode"].Length > 0)
                {
                    this.lbtnOk_Click(null, null);
                }
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
           
            string comcod = this.GetComCode();
            string txtSProject = "%" + this.txtSrcPro.Text + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlPrjName.DataTextField = "pactdesc";
            this.ddlPrjName.DataValueField = "pactcode";
            this.ddlPrjName.DataSource = ds1.Tables[0];
            this.ddlPrjName.DataBind();
            //this.ddlPrjName.SelectedValue = this.Request.QueryString["prjcode"];

            this.ddlProjectName_SelectedIndexChanged(null, null);
        }
        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            this.GetCustomerName();

        }
        protected void imgbtnFindCustomer_Click(object sender, EventArgs e)
        {
            this.GetCustomerName();
        }
        private void GetCustomerName()
        {
            string custotype = this.Request.QueryString["Type"].ToString();
            //string calltype = custotype=="LO"? "GETCUSTOMERNAMELANDOWNER" : "GETCUSTOMERNAME";          
            string comcod = this.GetComCode();
            string pactcode = this.ddlPrjName.SelectedValue.ToString();
            string txtSProject =  "%";
            string islandowner = this.Request.QueryString["Type"] == "LO" ? "1" : "0";
            DataSet ds2 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETCUSTOMERNAME", pactcode, txtSProject, islandowner, "", "", "", "", "", "");
            this.ddlCustName.DataTextField = "custnam";
            this.ddlCustName.DataValueField = "custid";
            this.ddlCustName.DataSource = ds2.Tables[0];
            this.ddlCustName.DataBind();

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
                this.ClearScreen();
            }
        }

        private void ClearScreen()
        {
            this.ddlPrjName.Enabled = true;

            
            ((Label)this.Master.FindControl("lblmsg")).Visible = false;
            ((Label)this.Master.FindControl("lblmsg")).Text = "";

            this.gvCustLCInfo.DataSource = null;
            this.gvCustLCInfo.DataBind();
        }

        private void LoadGrid()
        {

            string comcod = this.GetComCode();
            string ProjectCode = this.Request.QueryString["prjcode"].ToString() == "" ? this.ddlPrjName.SelectedValue.ToString() : this.Request.QueryString["prjcode"].ToString();
            //string fpactcode = (((DataTable)Session["tblpro"]).Select("actcode='" + ProjectCode + "'"))[0]["factcode"].ToString();
            string customer = this.ddlCustName.SelectedValue.ToString();
            //DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_PRJ_INFO", "PROJECTINFO", ProjectCode, customer, "", "", "", "", "", "", "");
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "CLIENT_LIFE_CYC", ProjectCode, customer, "", "", "", "", "", "", "");

            this.gvCustLCInfo.DataSource = ds1.Tables[0];
            this.gvCustLCInfo.DataBind();

            ViewState["CustLC_Entry"] = ds1.Tables[0];

            this.GridTextDDLVisible();

        }

        private void GridTextDDLVisible()
        {
            string comcod = this.GetComCode();
            DataTable dt = (DataTable)ViewState["CustLC_Entry"];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string Gcode = dt.Rows[i]["gcod"].ToString();
                string val = dt.Rows[i]["gdesc1"].ToString();
                switch (Gcode)
                {
                    case "58001":
                    case "58003":
                    case "58005":
                    case "58007":
                    case "58009":
                    case "58011":            

                        ((TextBox)this.gvCustLCInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        break;

                  

                    default:
                        ((TextBox)this.gvCustLCInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                       
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
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = GetComCode();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string comsnam = hst["comsnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string session = hst["session"].ToString();
            //string username = hst["username"].ToString();
            //string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            ////DataTable dt = (DataTable)ViewState["CustLC_Entry"];
            //string ProjectCode = this.ddlPrjName.SelectedValue.ToString();
            //string prjname = this.ddlPrjName.SelectedItem.Text.ToString();
            //string projname = prjname.Substring(13);


            //string fpactcode = (((DataTable)Session["tblpro"]).Select("actcode='" + ProjectCode + "'"))[0]["factcode"].ToString();
            //DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_PRJ_INFO", "PROJECTINFO", ProjectCode, fpactcode, "", "", "", "", "", "", "");
            //if (ds1.Tables[0].Rows.Count == 0)
            //    return;

            //DataTable dt = ds1.Tables[0];

            //LocalReport Rpt1 = new LocalReport();
            //var lst = dt.DataTableToList<RealEntity.C_04_Bgd.BgdProInfo>();
            //Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_04_Bgd.RptPrjInfoEnt", lst, null, null);
            //Rpt1.EnableExternalImages = true;
            //Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            //Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            //Rpt1.SetParameters(new ReportParameter("ProjectNam", projname));
            //Rpt1.SetParameters(new ReportParameter("RptTitle", "Project Information"));
            //Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            //Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            //Session["Report1"] = Rpt1;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
            //  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


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
                string cutcode = this.ddlCustName.SelectedValue.ToString();


                // (((DataTable)Session["tblpro"]).Select("infcod='"+fpactcode+"'"))[0]["pactcode"];

                //string fpactcode = (((DataTable)Session["tblpro"]).Select("actcode='" + pactcode + "'"))[0]["factcode"].ToString();

                for (int i = 0; i < this.gvCustLCInfo.Rows.Count; i++)
                {
                    string Gcode = ((Label)this.gvCustLCInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                    string gtype = ((Label)this.gvCustLCInfo.Rows[i].FindControl("lgvgval")).Text.Trim();
                    
                    string Gvalue1 = ((TextBox)this.gvCustLCInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                   
                    string Gvalue = "";

                    
                   

                    if (Gcode == "58001" || Gcode == "58003" || Gcode == "58005" || Gcode == "58007"|| Gcode == "58009" || Gcode == "58011")
                    {
                        Gvalue = (((TextBox)this.gvCustLCInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim() == "") ? "01-Jan-1900" : ((TextBox)this.gvCustLCInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim();
                    }
                    else
                    {
                        Gvalue = Gvalue1;
                    }
                    Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue) : (gtype == "N") ? Convert.ToDouble("0" + Gvalue).ToString() : Gvalue;
                    MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "INSERTORUPDATECUSTINF", pactcode, cutcode, Gcode, gtype, Gvalue, "", "", "", "", "", "", "", "", "", "");
                }


                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                // ((Label)this.Master.FindControl("lblmsg")).Attributes["Style"] = "color:white; background:green; border:none;";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);




                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Customer Information";
                    string eventdesc = "Update Customer Information";
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

        protected void lbtnEnglish_Click(object sender, EventArgs e)
        {
            string name = this.txtSrcPro.Text.Trim();
            //string bname = ASITUtility02.EngtoBandigit(name);
            string bname1 = ASITUtility02.ToBangla(name);
        }

    }
}



