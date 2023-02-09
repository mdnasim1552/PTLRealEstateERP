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
using System.IO;
using RealERPLIB;
using RealERPRPT;
using AjaxControlToolkit;
namespace RealERPWEB.F_08_PPlan
{
    public partial class PrjCompArchiaInterior : System.Web.UI.Page
    {
        string Upload = "";
        int size = 0;
        System.IO.Stream image_file = null;
        ProcessAccess MktData = new ProcessAccess();
        int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));
                string qType = this.Request.QueryString["Type"].ToString();
                //((Label)this.Master.FindControl("lblTitle")).Text = (qType == "Architecture") ? "Project Design" : (qType == "Legal") ? "Project Interior"
                //    : (qType == "Logistic") ? "Project Pre-Planning-Logistic" : (qType == "Design") ? "Project Pre-Planning-Design" 
                //    : (qType == "Landscape") ? "Landscape Design" : (qType == "MasterPlan") ? "Master Plan" : "Project Pre-Planning-Brand";

                this.GetWork();
                this.GetProjectName();
                
                //this.GetJOB();
                this.Getuser();
                // this.CreateTable();

                if (this.Request.QueryString["prjcode"].Length > 0)
                {
                    this.lbtnOk_Click(null, null);
                }

            }
        }

        private string GetCompcode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());



        }

       
        private void GetWork()
        {

            try
            {
                string comcod = this.GetCompcode();
                string Type = this.Request.QueryString["Type"].ToString();
                string SType = Type == "Architecture" ? "Architecture" : Type == "Interior" ? "Interior" : Type== "Landscape" ? "Landscape" : "MasterPlan";
                DataSet ds1 = this.MktData.GetTransInfo(comcod, "SP_ENTRY_PROJECTCOMFCHART", "GETPROJECTDESIGNWORK", SType, "", "", "", "", "", "", "", "");

                this.ddlwork.DataTextField = "actdesc";
                this.ddlwork.DataValueField = "actcode";
                this.ddlwork.DataSource = ds1.Tables[0];
                this.ddlwork.DataBind();
                ds1.Dispose();

            }

            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);


            }

        }
        private void GetJOB()
        {
            //try
            //{
            //    string comcod = this.GetCompcode();
            //    string Type = this.Request.QueryString["Type"] == "Architecture" ? "Architecture" : "Interior";
            //    DataSet ds1 = this.MktData.GetTransInfo(comcod, "SP_ENTRY_PROJECTCOMFCHART", "GETPROJECTDESIGNJOB", Type, "", "", "", "", "", "", "", "");

            //    this.lstJob.DataTextField = "jobdesc";
            //    this.lstJob.DataValueField = "jobcode";
            //    this.lstJob.DataSource = ds1.Tables[0];
            //    this.lstJob.DataBind();
            //    ds1.Dispose();

            //}

            //catch (Exception ex)
            //{
            //    ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            //    ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

            //}



        }
        private void Getuser()
        {
            try
            {

                Session.Remove("tbluser");
                string comcod = this.GetCompcode();
                DataSet ds1 = this.MktData.GetTransInfo(comcod, "SP_ENTRY_PROJECTCOMFCHART", "GETUSER", "", "", "", "", "", "", "", "", "");
                Session["tbluser"] = ds1.Tables[0];
                ds1.Dispose();

            }

            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

            }


        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void GetProjectName()
        {

            string comcod = this.GetComCode();
            string txtSProject = "%" + "%";
            string wokcode = this.ddlwork.SelectedValue.ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_PROJECTCOMFCHART", "SHOWPROJECTACUSTOMER", txtSProject, wokcode, "", "", "", "", "", "", "");
            this.ddlPrjName.DataTextField = "actdesc";
            this.ddlPrjName.DataValueField = "actcode";
            this.ddlPrjName.DataSource = ds1.Tables[0];
            this.ddlPrjName.DataBind();
            this.ddlPrjName.SelectedValue = this.Request.QueryString["prjcode"];

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
                this.ddlwork.Enabled = false;
                //this.pnlJob.Visible = this.Request.QueryString["Type"]=="Interior" ;
                this.LoadGrid();


            }
            else
            {

                this.lbtnOk.Text = "Ok";
                this.ddlwork.Enabled = true;
                // this.pnlJob.Visible = false;
                this.ddlPrjName.Enabled = true;
                this.gvPrjInfo.DataSource = null;
                this.gvPrjInfo.DataBind();
                //DataTable dt = (DataTable)Session["tbljob"];
                //DataView dv = dt.DefaultView;
                //dv.RowFilter = ("jobcode=''");
                //Session["tbljob"] = dv.ToTable();




            }
        }


        protected void Chboxchild_CheckedChanged(object sender, EventArgs e)
        {
            this.LoadGrid();
        }
        private void LoadGrid()
        {
            Session.Remove("tbljob");
            string comcod = this.GetComCode();
            string ProjectCode = this.ddlPrjName.SelectedValue.ToString().Substring(0,12);
            string workcode = this.ddlwork.SelectedValue.ToString();
            string usircode = this.ddlPrjName.SelectedValue.ToString().Substring(12);

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_PROJECTCOMFCHART", "SHOWPROJECTDESIGNINFO", ProjectCode, usircode, workcode, "", "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0)
                return;
            this.GetDues();
            Session["tblprocom"] = this.HiddenSameData(ds1.Tables[0]);
            Session["tbljob"] = ds1.Tables[1];
            this.Data_Bind();
        }

        private void GetDues()
        {

            Session.Remove("tbljob");
            string comcod = this.GetComCode();
            string ProjectCode ="18"+ this.ddlPrjName.SelectedValue.ToString().Substring(2,10);
            string usircode = this.ddlPrjName.SelectedValue.ToString().Substring(12);
            string workcode = this.ddlwork.SelectedValue.ToString();
            string date = System.DateTime.Today.ToString("dd-MMM-yyyy");

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_PROJECTCOMFCHART", "SHOWPROJECTDUES", ProjectCode, workcode, date, usircode, "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0)
                return;
           
            Session["tblprodues"] = ds1.Tables[0];
           


        }

        private DataTable HiddenSameData(DataTable dt1)
        {


            if (dt1.Rows.Count == 0)
                return dt1;
            int i = 0;
            string actcode = dt1.Rows[0]["actcode"].ToString();

            foreach (DataRow dr1 in dt1.Rows)
            {
                if (i == 0)
                {


                    actcode = dr1["actcode"].ToString();
                    i++;
                    continue;
                }

                if (dr1["actcode"].ToString() == actcode)
                {

                    // dr1["actdesc"] = "";
                    dr1["proesam"] = 0.00;
                    dr1["proadam"] = 0.00;
                    dr1["dueam"] = 0.00;

                }


                actcode = dr1["actcode"].ToString();
            }



            return dt1;

        }
        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblprocom"];
            DataTable dtj = (DataTable)Session["tbljob"];
            DataTable dtusr = (DataTable)Session["tbluser"];
            DataTable dtdues = (DataTable)Session["tblprodues"];
            double paidamt = 0;
            DateTime schdate;
            DateTime curdate = System.DateTime.Today;
            string jobcode ;


            int i, j = 4, k;
            for (i = 4; i < this.gvPrjInfo.Columns.Count; i++)
                this.gvPrjInfo.Columns[i].Visible = false;

            foreach (DataRow dr1 in dtj.Rows)
            {
                jobcode = dr1["jobcode"].ToString();
                paidamt = dtdues.Select("jobcode='" + jobcode + "'").Length == 0 ? 0 : Convert.ToDouble(dtdues.Select("jobcode='" + jobcode + "'")[0]["paidamt"]);
                schdate = dtdues.Select("jobcode='" + jobcode + "'").Length == 0 ?Convert.ToDateTime("01-Jan-1900") : Convert.ToDateTime(dtdues.Select("jobcode='" + jobcode + "'")[0]["schdate"]);
                this.gvPrjInfo.Columns[j].Visible = true;

                if (paidamt > 0)
                {

                    this.gvPrjInfo.Columns[j].HeaderText = dr1["jobdesc"].ToString();
                    this.gvPrjInfo.Columns[j].HeaderStyle.ForeColor = System.Drawing.Color.Green;
                    this.gvPrjInfo.Columns[j].HeaderStyle.Font.Bold = true ;


                }

              else   if (paidamt == 0 && schdate <= curdate)
                {
                    this.gvPrjInfo.Columns[j].HeaderText = dr1["jobdesc"].ToString();
                    this.gvPrjInfo.Columns[j].HeaderStyle.ForeColor = System.Drawing.Color.Red;

                }

              else  
                {
                    this.gvPrjInfo.Columns[j].HeaderText = dr1["jobdesc"].ToString();
                    

                }


                j++;


            }


            this.gvPrjInfo.DataSource = dt;
            this.gvPrjInfo.DataBind();




            ListBox ddlassignuser;
            i = 0;
            foreach (DataRow dr1 in dt.Rows)
            {

                string Gcode = dt.Rows[i]["gcod"].ToString();

                switch (Gcode)
                {



                    case "1001001": //Percentage 
                    case "1001002": //Amount  



                        for (k = 1; k <= 14; k++)
                        {

                            ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvjob" + k.ToString())).Visible = true;
                            ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvjobd" + k.ToString())).Visible = false;
                            ((ListBox)this.gvPrjInfo.Rows[i].FindControl("ddlassignuser" + k.ToString())).Visible = false;
                            ((Label)this.gvPrjInfo.Rows[i].FindControl("lblgvjob" + k.ToString())).Visible = false;

                        }






                        break;


                    case "1001014": //Remarks  
                        for (k = 1; k <= 14; k++)
                        {

                            ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvjob" + k.ToString())).Visible = true;
                            ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvjob" + k.ToString())).Attributes["style"] = "textmode:multiline;";
                            ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvjob" + k.ToString())).TextMode = TextBoxMode.MultiLine;
                            ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvjobd" + k.ToString())).Visible = false;
                            ((ListBox)this.gvPrjInfo.Rows[i].FindControl("ddlassignuser" + k.ToString())).Visible = false;
                            ((Label)this.gvPrjInfo.Rows[i].FindControl("lblgvjob" + k.ToString())).Visible = false;

                        }
                        break;


                    case "1001003": //Concern Person, 
                    case "1001005": //Target Start Date, 
                    case "1001007": //Target End Date, 
                    case "1001009": //Actual Start Date, 
                    case "1001011": //Actual End Date, 
                    case "1001013": //Remarks(Text), 

                        for (k = 1; k <= 14; k++)
                        {
                            ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvjob" + k.ToString())).Visible = false;
                            ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvjobd" + k.ToString())).Visible = false;
                            ((ListBox)this.gvPrjInfo.Rows[i].FindControl("ddlassignuser" + k.ToString())).Visible = false;
                            ((Label)this.gvPrjInfo.Rows[i].FindControl("lblgvjob" + k.ToString())).Visible = true;
                        }
                        break;


                    case "1001004": //Dropdown Concern Person,    
                        for (k = 1; k <= 14; k++)
                        {
                            ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvjob" + k.ToString())).Visible = false;
                            ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvjobd" + k.ToString())).Visible = false;
                            ((ListBox)this.gvPrjInfo.Rows[i].FindControl("ddlassignuser" + k.ToString())).Visible = true;
                            ((Label)this.gvPrjInfo.Rows[i].FindControl("lblgvjob" + k.ToString())).Visible = false;
                            ddlassignuser = ((ListBox)this.gvPrjInfo.Rows[i].FindControl("ddlassignuser" + k.ToString()));
                            ddlassignuser.DataTextField = "usrname";
                            ddlassignuser.DataValueField = "usrid";
                            ddlassignuser.DataSource = dtusr;
                            ddlassignuser.DataBind();

                            string assignuser = dt.Rows[i]["job" + k.ToString()].ToString();
                            int count = assignuser.Length;
                            string data = "";
                            int st = 0;
                            for (j = 0; j < count / 7; j++)
                            {
                               
                                data = assignuser.Substring(st, 7);
                                foreach (ListItem item in ddlassignuser.Items)
                                {
                                    if (item.Value == data)
                                    {
                                        item.Selected = true;
                                    }

                                }
                                st = st + 7;
                            }



                           // ddlassignuser.SelectedValue = ((Label)this.gvPrjInfo.Rows[i].FindControl("lblgvjob" + k.ToString())).Text.Trim();
                        }


                        // int count = Convert.ToInt32(dt.Rows[i]["gdesc1"].ToString().Count());
                        //if (count == 0)
                        //{


                        //    if (empid.Length == 13)
                        //    {

                        //        empid = empid.Replace("%", "");
                        //        ddlPartic.SelectedValue = empid;


                        //    }
                        //}


                            //for (j = 0; j < count / 12; j++)
                            //{
                            //    data = dt.Rows[i]["gdesc1"].ToString().Substring(k, 12);
                            //    foreach (ListItem item in ddlPartic.Items)
                            //    {
                            //        if (item.Value == data)
                            //        {
                            //            item.Selected = true;
                            //        }

                            //    }
                            //    k = k + 12;
                            //}


                            break;


                    case "1001006": //Target Start Date Value, 
                    case "1001008": //Target End Date Value, 
                    case "1001010": //Actual Start Date Value, 
                    case "1001012": //Actual End Date Value, 
                        for (k = 1; k <= 14; k++)
                        {

                            ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvjob" + k.ToString())).Visible = false;
                            ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvjobd" + k.ToString())).Visible = true;
                            ((ListBox)this.gvPrjInfo.Rows[i].FindControl("ddlassignuser" + k.ToString())).Visible = false;
                            ((Label)this.gvPrjInfo.Rows[i].FindControl("lblgvjob" + k.ToString())).Visible = false;
                        }
                        break;

                    default:

                        for (k = 1; k <= 14; k++)
                        {

                            ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvjob" + k.ToString())).Visible = false;
                            ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvjobd" + k.ToString())).Visible = false;
                            ((ListBox)this.gvPrjInfo.Rows[i].FindControl("ddlassignuser" + k.ToString())).Visible = false;
                            ((Label)this.gvPrjInfo.Rows[i].FindControl("lblgvjob" + k.ToString())).Visible = true;
                        }


                        break;





                }

                i++;

            }


        }
        protected void lbtnCalculaton_Click(object sender, EventArgs e)
        {
            string inidate, inidate1, preenddate;
            DataTable dt = (DataTable)Session["tblprocom"];

            for (int i = 0; i < this.gvPrjInfo.Rows.Count; i++)
            {

                int duration = Convert.ToInt32("0" + ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvduration")).Text.Trim());
                string inidate2 = ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txttarEndDate")).Text.Trim();

                if (duration > 0 && inidate2 == "")
                {
                    //Convert.ToDateTime(this.txtdate.Text);
                    inidate1 = ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtTarsDate")).Text.Trim();
                    if (i != 0)
                    {
                        inidate = (((TextBox)this.gvPrjInfo.Rows[i - 1].FindControl("txttarEndDate")).Text.Trim() == "") ? ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtTarsDate")).Text.Trim()
                            : ((TextBox)this.gvPrjInfo.Rows[i - 1].FindControl("txttarEndDate")).Text.Trim();


                    }
                    else
                    {
                        inidate = inidate1;
                    }
                    if (inidate == "")
                        continue;

                    dt.Rows[i]["duration"] = duration;
                    dt.Rows[i]["tarsdat"] = inidate;//(inidate1 == "") ? inidate.ToString() : inidate1;
                    dt.Rows[i]["taredat"] = Convert.ToDateTime(inidate).AddDays(duration - 1); //(inidate1 == "") ? inidate.AddDays(duration - 1) : Convert.ToDateTime(inidate1).AddDays(duration - 1); //inidate.AddDays(duration - 1);

                    ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtTarsDate")).Text = inidate;
                    ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txttarEndDate")).Text = Convert.ToDateTime(inidate).AddDays(duration - 1).ToString();
                }







            }

            Session["tblprocom"] = dt;
            this.Data_Bind();

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //DataTable dt = (DataTable)Session["tblprocom"];

            //ReportDocument rptResource = new RealERPRPT.R_08_PPlan.rptProFlowChart01();
            //TextObject rpttxtComName = rptResource.ReportDefinition.ReportObjects["txtComName"] as TextObject;
            //rpttxtComName.Text = comnam;

            //TextObject rpttxtProName = rptResource.ReportDefinition.ReportObjects["txtprojectname"] as TextObject;
            //rpttxtProName.Text = this.ddlPrjName.SelectedItem.ToString().Substring(13);

            //TextObject rpttxtDate = rptResource.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //rpttxtDate.Text = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");

            //TextObject txtuserinfo = rptResource.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptResource.SetDataSource(dt);
            //Session["Report1"] = rptResource;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }







        protected void gvPrjInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {



            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //  LinkButton lbtnAdd = (LinkButton)e.Row.FindControl("lbtnAdd"); 



                string gcod = DataBinder.Eval(e.Row.DataItem, "gcod").ToString();

                //if (gcod == "1001002")
                //{
                //    TextBox txtgvjob1 = (TextBox)e.Row.FindControl("txtgvjob1");
                //    TextBox txtgvjob2 = (TextBox)e.Row.FindControl("txtgvjob2");
                //    TextBox txtgvjob3 = (TextBox)e.Row.FindControl("txtgvjob3");
                //    TextBox txtgvjob4 = (TextBox)e.Row.FindControl("txtgvjob4");
                //    TextBox txtgvjob5 = (TextBox)e.Row.FindControl("txtgvjob5");
                //    TextBox txtgvjob6 = (TextBox)e.Row.FindControl("txtgvjob6");
                //    TextBox txtgvjob7 = (TextBox)e.Row.FindControl("txtgvjob7");
                //    TextBox txtgvjob8 = (TextBox)e.Row.FindControl("txtgvjob8");
                //    TextBox txtgvjob9 = (TextBox)e.Row.FindControl("txtgvjob9");
                //    TextBox txtgvjob10 = (TextBox)e.Row.FindControl("txtgvjob10");
                //    TextBox txtgvjob11 = (TextBox)e.Row.FindControl("txtgvjob11");
                //    TextBox txtgvjob12 = (TextBox)e.Row.FindControl("txtgvjob12");
                //    TextBox txtgvjob13 = (TextBox)e.Row.FindControl("txtgvjob13");
                //    TextBox txtgvjob14 = (TextBox)e.Row.FindControl("txtgvjob14");   

                //    txtgvjob1.Attributes["style"] = "text-align:right;";
                //    txtgvjob2.Attributes["style"] =  "text-align:right;";
                //    txtgvjob3.Attributes["style"] =  "text-align:right;";
                //    txtgvjob4.Attributes["style"] =  "text-align:right;";
                //    txtgvjob5.Attributes["style"] =  "text-align:right;";
                //    txtgvjob6.Attributes["style"] =  "text-align:right;";
                //    txtgvjob7.Attributes["style"] =  "text-align:right;";
                //    txtgvjob8.Attributes["style"] =  "text-align:right;";
                //    txtgvjob9.Attributes["style"] =  "text-align:right;";
                //    txtgvjob10.Attributes["style"] = "text-align:right;";
                //    txtgvjob11.Attributes["style"] = "text-align:right;";
                //    txtgvjob12.Attributes["style"] = "text-align:right;";
                //    txtgvjob13.Attributes["style"] = "text-align:right;";
                //    txtgvjob14.Attributes["style"] = "text-align:right;";

                //}

                if (gcod == "1001003" || gcod == "1001005" || gcod == "1001007" || gcod == "1001009" || gcod == "1001011" || gcod == "1001013")
                {
                    Label lblgvjob1 = (Label)e.Row.FindControl("lblgvjob1");
                    Label lblgvjob2 = (Label)e.Row.FindControl("lblgvjob2");
                    Label lblgvjob3 = (Label)e.Row.FindControl("lblgvjob3");
                    Label lblgvjob4 = (Label)e.Row.FindControl("lblgvjob4");
                    Label lblgvjob5 = (Label)e.Row.FindControl("lblgvjob5");
                    Label lblgvjob6 = (Label)e.Row.FindControl("lblgvjob6");
                    Label lblgvjob7 = (Label)e.Row.FindControl("lblgvjob7");
                    Label lblgvjob8 = (Label)e.Row.FindControl("lblgvjob8");
                    Label lblgvjob9 = (Label)e.Row.FindControl("lblgvjob9");
                    Label lblgvjob10 = (Label)e.Row.FindControl("lblgvjob10");
                    Label lblgvjob11 = (Label)e.Row.FindControl("lblgvjob11");
                    Label lblgvjob12 = (Label)e.Row.FindControl("lblgvjob12");
                    Label lblgvjob13 = (Label)e.Row.FindControl("lblgvjob13");
                    Label lblgvjob14 = (Label)e.Row.FindControl("lblgvjob14");


                    // e.Row.Attributes["style"] = "background:gray;";
                    lblgvjob1.Attributes["style"] = "background-color:#f7f7f7; font-weight:bold; text-align:center;";
                    lblgvjob2.Attributes["style"] = "background-color:#f7f7f7; font-weight:bold;text-align:center;";
                    lblgvjob3.Attributes["style"] = "background-color:#f7f7f7; font-weight:bold;text-align:center;";
                    lblgvjob4.Attributes["style"] = "background-color:#f7f7f7; font-weight:bold;text-align:center;";
                    lblgvjob5.Attributes["style"] = "background-color:#f7f7f7; font-weight:bold;text-align:center;";
                    lblgvjob6.Attributes["style"] = "background-color:#f7f7f7; font-weight:bold;text-align:center;";
                    lblgvjob7.Attributes["style"] = "background-color:#f7f7f7; font-weight:bold;text-align:center;";
                    lblgvjob8.Attributes["style"] = "background-color:#f7f7f7; font-weight:bold;text-align:center;";
                    lblgvjob9.Attributes["style"] = "background-color:#f7f7f7; font-weight:bold;text-align:center;";
                    lblgvjob10.Attributes["style"] = "background-color:#f7f7f7; font-weight:bold;text-align:center;";
                    lblgvjob11.Attributes["style"] = "background-color:#f7f7f7; font-weight:bold;text-align:center;";
                    lblgvjob12.Attributes["style"] = "background-color:#f7f7f7; font-weight:bold;text-align:center;";
                    lblgvjob13.Attributes["style"] = "background-color:#f7f7f7; font-weight:bold;text-align:center;";
                    lblgvjob14.Attributes["style"] = "background-color:#f7f7f7; font-weight:bold;text-align:center;";


                }





            }

        }


        //protected void lbtnAdd_Click(object sender, EventArgs e)
        //{
        //    DataTable dt = (DataTable)Session["tbljob"];
        //    string jobcode="";
        //    string jobdesc="";

        //    foreach (ListItem litem in lstJob.Items)
        //    {

        //        if (litem.Selected)
        //        {

        //            jobcode = litem.Value;
        //            jobdesc = litem.Text;
        //            DataRow[] dr = dt.Select("jobcode='" + jobcode + "'");

        //            if (dr.Length == 0)
        //            {
        //                DataRow dr1 = dt.NewRow();
        //                dr1["jobcode"] = jobcode;
        //                dr1["jobdesc"] = jobdesc;
        //                dt.Rows.Add(dr1);
        //            }
        //        }

        //    }
        //    this.Data_Bind();



        //}
        protected void lbtnAddCode_Click(object sender, EventArgs e)
        {

            ((Label)this.Master.FindControl("lblmsg")).Visible = true;

            try
            {

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string userid = hst["usrid"].ToString();
                string pactcode = this.ddlPrjName.SelectedValue.ToString();
                string actcode = this.lblactcode.Text;

                string gdesc = this.txtComments.Text.Trim();


                DataTable dt = (DataTable)ViewState["tblComm"];

                string cdate = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

                DataRow[] dr2 = dt.Select("cdate='" + cdate + "'");
                if (dr2.Length == 0)
                {
                    dt.Rows.Add(comcod, cdate, gdesc, userid, "");
                    //dr2[0]["cdate"] = cdate;
                    //dr2[0]["comments"] = gdesc;
                    //dr2[0]["usrid"] = userid;
                }




                DataSet ds = new DataSet("ds2");
                //ds.Merge(dt);

                dt.Columns.Remove("comcod");
                dt.Columns.Remove("username");

                ds.Merge(dt);
                ds.Tables[0].TableName = "tbl1";



                if (gdesc.Length == 0)
                {

                    ((Label)this.Master.FindControl("lblmsg")).Text = "Comments is not empty";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModal();", true);
                    return;
                }
                else
                {
                    bool result = MktData.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "UPDATE_PREPLAN_COMM", ds, null, null, pactcode, actcode, "", "", "", "", "", "", "", "", "", "", "", "");



                    if (!result)
                    {

                        ((Label)this.Master.FindControl("lblmsg")).Text = MktData.ErrorObject["Msg"].ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;

                    }


                    ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                    //this.ShowInformation();


                }




            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error: " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }

        }


        protected void lbtnAddUsr_Click(object sender, EventArgs e)
        {
            GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            int RowIndex = gvr.RowIndex;




            string pactcode = this.ddlPrjName.SelectedValue.ToString();
            string actcode = ((Label)this.gvPrjInfo.Rows[RowIndex].FindControl("lgvActcode")).Text.ToString();

            DataTable dt = ((DataTable)Session["tblprocom"]).Copy();

            DataView dv = dt.DefaultView;
            dv.RowFilter = ("actcode='" + actcode + "' and assusrid <> ''");
            DataTable dt1 = dv.ToTable();
            if (dt1.Rows.Count != 0)
            {
                this.ddlUserList.SelectedValue = dv.ToTable().Rows[0]["assusrid"].ToString();

            }

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();


            //DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_PROJECTCOMFCHART", "SHOWCOMMENTS", pactcode, actcode, "", "", "", "", "", "");

            //ViewState["tblComm"] = ds1.Tables[0];

            //this.gvComm.DataSource = ds1.Tables[0];
            //this.gvComm.DataBind();


            this.lblactcode.Text = actcode;


            // Project Link

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "UsrloadModal();", true);
        }

        protected void lbtnUpdateUsr_Click(object sender, EventArgs e)
        {

            ((Label)this.Master.FindControl("lblmsg")).Visible = true;

            try
            {

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string userid = hst["usrid"].ToString();
                string pactcode = this.ddlPrjName.SelectedValue.ToString();
                string actcode = this.lblactcode.Text;




                DataTable dt = (DataTable)ViewState["tblComm"];

                string Assuserid = this.ddlUserList.SelectedValue.ToString();



                if (Assuserid.Length == 0)
                {

                    ((Label)this.Master.FindControl("lblmsg")).Text = "Comments is not empty";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModal();", true);
                    return;
                }
                else
                {
                    bool result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_PROJECTCOMFCHART", "UPDATEUSER", pactcode, actcode, Assuserid);



                    if (!result)
                    {

                        ((Label)this.Master.FindControl("lblmsg")).Text = MktData.ErrorObject["Msg"].ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;

                    }


                    ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                    //this.ShowInformation();


                }




            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error: " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }

        }

        protected void lbtnDoc_Click(object sender, EventArgs e)
        {
            //GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            //int RowIndex = gvr.RowIndex;

            //string pactcode = this.ddlPrjName.SelectedValue.ToString();
            //string actcode = ((Label)this.gvPrjInfo.Rows[RowIndex].FindControl("lgvActcode")).Text.ToString();

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();


            //DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_PROJECTCOMFCHART", "SHOWUPDOC", pactcode, actcode, "", "", "", "", "", "");

            //ViewState["tblupdoc"] = ds1.Tables[0];


            //ListViewEmpAll.DataSource = ds1.Tables[0];
            //ListViewEmpAll.DataBind();
            //this.lblImcode.Text = actcode;
            // ScriptManager.RegisterStartupScript(this, GetType(), "alert", "DocloadModal();", true);
        }
        protected void ListViewEmpAll_ItemDataBound(object sender, ListViewItemEventArgs e)
        {

            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Image imgname = (Image)e.Item.FindControl("GetImg");
                Label imglink = (Label)e.Item.FindControl("ImgLink");
                string extension = Path.GetExtension(imglink.Text.ToString());
                switch (extension)
                {
                    case ".PNG":
                    case ".png":
                    case ".JPEG":
                    case ".JPG":
                    case ".jpg":
                    case ".jpeg":
                    case ".GIF":
                    case ".gif":
                        imgname.ImageUrl = imglink.Text.ToString();
                        break;
                    case ".PDF":
                    case ".pdf":
                        imgname.ImageUrl = "~/Images/pdf.png";
                        break;
                    case ".xls":
                    case ".xlsx":
                        imgname.ImageUrl = "~/Images/excel.svg";
                        break;
                    case ".doc":
                    case ".docx":
                        imgname.ImageUrl = "~/Images/word.png";
                        break;


                }

            }

        }

        protected void FileUploadComplete(object sender, AsyncFileUploadEventArgs e)
        {
            string comcod = this.GetComCode();
            string filename = System.IO.Path.GetFileName(AsyncFileUpload1.FileName);
            string pactcode = this.ddlPrjName.SelectedValue.ToString();
            string actcode = this.lblImcode.Text;
            string docurl = "";
            Hashtable hst = (Hashtable)Session["tblLogin"];

            string userid = hst["usrid"].ToString();

            if (AsyncFileUpload1.HasFile)
            {

                string extension = Path.GetExtension(AsyncFileUpload1.PostedFile.FileName);
                string random = ASTUtility.RandNumber(1, 99999).ToString();
                AsyncFileUpload1.SaveAs(Server.MapPath("~/Upload/Legal/") + random + extension);

                //docurl = "~/Upload/Legal/" + random + extension;

                docurl = "Upload/Legal/" + random + extension;


                DataTable dt = (DataTable)ViewState["tblupdoc"];

                string cdate = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

                DataRow[] dr2 = dt.Select("cdate='" + cdate + "'");
                if (dr2.Length == 0)
                {
                    dt.Rows.Add(comcod, cdate, docurl, userid, "");
                }

                DataSet ds = new DataSet("ds2");
                //ds.Merge(dt);

                dt.Columns.Remove("comcod");
                dt.Columns.Remove("username");

                ds.Merge(dt);
                ds.Tables[0].TableName = "tbl1";

                if (docurl.Length == 0)
                {

                    ((Label)this.Master.FindControl("lblmsg")).Text = "Doc is not empty";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModal();", true);
                    return;
                }
                else
                {

                    bool result = MktData.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "UPDATE_DOC", ds, null, null, pactcode, actcode, "", "", "", "", "", "", "", "", "", "", "", "");

                    if (result)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                        return;

                    }

                    ((Label)this.Master.FindControl("lblmsg")).Text = MktData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
            }


            //this.lbtnDoc_Click(null, null);
        }
        protected void btnDelall_OnClick(object sender, EventArgs e)
        {
            string comcod = this.GetComCode();
            string pactcode = this.ddlPrjName.SelectedValue.ToString();
            string actcode = this.lblImcode.Text;
            for (int j = 0; j < this.ListViewEmpAll.Items.Count; j++)
            {
                string cdate = ((Label)this.ListViewEmpAll.Items[j].FindControl("lblcdate")).Text.ToString();
                string filesname = ((Label)this.ListViewEmpAll.Items[j].FindControl("ImgLink")).Text.ToString();
                if (((CheckBox)this.ListViewEmpAll.Items[j].FindControl("ChDel")).Checked == true)
                {
                    bool result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_PROJECTCOMFCHART", "DELETEXMLDATA", pactcode, actcode, cdate, "", "", "", "", "", "", "", "", "", "");

                    if (result == true)
                    {
                        //string filePath = Server.MapPath("/Upload/Legal/");
                        string filePath = Server.MapPath("~/");
                        System.IO.File.Delete(filePath + filesname);


                        ((Label)this.Master.FindControl("lblmsg")).Text = " Files Removed ";
                    }


                }
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "DocloadModal();", true);
            //if (this.Request.QueryString["genno"].Length != 0)
            //{
            //    this.lnkReqList_Click(null, null);
            //    this.lbtnMSROk.Text = "Ok";
            //    this.lbtnMSROk_Click(null, null);

            //}
            //this.viewseciton();

        }
        //protected void ChboxAll_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (ChboxAll.Checked==false)
        //    {
        //        foreach (ListItem litem in lstJob.Items)
        //        {

        //            if (litem.Selected)
        //            {
        //                litem.Selected = false;

        //            }

        //        }

        //    }

        //    else
        //    {

        //        foreach (ListItem litem in lstJob.Items)
        //        {

        //         litem.Selected = true;
        //        }


        //    }
        //}

        private void SaveValue()
        {

            DataTable dt = (DataTable)Session["tblprocom"];
            int i = 0, k;
            foreach (GridViewRow gv1 in gvPrjInfo.Rows)
            {


                string gcod = ((Label)gv1.FindControl("lblgvcode")).Text.Trim();
                double proesam = ASTUtility.StrPosOrNagative(((TextBox)gv1.FindControl("txtgvestamt")).Text.Trim());
                double proadam = ASTUtility.StrPosOrNagative(((TextBox)gv1.FindControl("txtgvadvamt")).Text.Trim());
                switch (gcod)
                {



                    case "1001001": //Percentage  
                    case "1001014": //Remarks  
                        for (k = 1; k <= 14; k++)
                        {

                            dt.Rows[i]["job" + k.ToString()] = ((TextBox)gv1.FindControl("txtgvjob" + k.ToString())).Text.ToString();
                        }
                        break;


                    case "1001002": //Amount  

                        for (k = 1; k <= 14; k++)
                        {
                            //double dueam = Convert.ToDouble(dt.Rows[0]["dueam"]);
                            //int indexofper = dt.Rows[0]["job" + k.ToString()].ToString().IndexOf("%");
                            //if (indexofper == -1)
                            //{
                            //    dt.Rows[i]["job" + k.ToString()] = "";
                            //    continue;
                            //}
                            //double percnt = Convert.ToDouble(dt.Rows[0]["job" + k.ToString()].ToString().Substring(0, indexofper).Trim());
                            //double jobam = percnt * dueam * 0.01;
                           // dt.Rows[i]["job" + k.ToString()] = jobam.ToString("#,##0.00;(#,##)0.00; ");
                            dt.Rows[i]["job" + k.ToString()] = ((TextBox)gv1.FindControl("txtgvjob" + k.ToString())).Text.ToString();
                           
                        }
                        break;


                    case "1001004": //Dropdown Concern Person, 
                      
                        


                        for (k = 1; k <= 14; k++)
                        {

                            ListBox ddlassignperson = ((ListBox)gv1.FindControl("ddlassignuser"+k.ToString()));
                            string assigperson = "";
                            foreach (ListItem item in ddlassignperson.Items)
                            {

                                if (item.Selected)
                                {
                                    assigperson += item.Value;
                                }

                            }

                            dt.Rows[i]["job" + k.ToString()] = assigperson;




                        }

                        break;


                    case "1001006": //Target Start Date Value, 
                    case "1001008": //Target End Date Value, 
                    case "1001010": //Actual Start Date Value, 
                    case "1001012": //Actual End Date Value, 
                        for (k = 1; k <= 14; k++)
                        {

                            dt.Rows[i]["job" + k.ToString()] = ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvjobd" + k.ToString())).Text.Trim();


                        }
                        break;

                }

                dt.Rows[i]["proesam"] = proesam;
                dt.Rows[i]["proadam"] = proadam;
                dt.Rows[i]["dueam"] = proesam - proadam;
                i++;

            }
            this.Data_Bind();
        }

        protected void lnkgvTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();

        }
        protected void lnkgvUpdate_Click(object sender, EventArgs e)
        {

            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            string comcod = this.GetComCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string PostbyId = hst["usrid"].ToString();
            string posttrmid = hst["compname"].ToString();
            string postsesson = hst["session"].ToString();
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

            DataTable dt = (DataTable)Session["tblprocom"];
            DataTable dtj = (DataTable)Session["tbljob"];
            int lenj = dtj.Rows.Count;
            int i = 0, k, j;
            string pactcode = this.ddlPrjName.SelectedValue.ToString().Substring(0,12);
            string usircode = this.ddlPrjName.SelectedValue.ToString().Substring(12);
            string actcode = this.ddlwork.SelectedValue.ToString();
            string proesam = dt.Rows[0]["proesam"].ToString();
            string proadam = dt.Rows[0]["proadam"].ToString();
            string dueam = dt.Rows[0]["dueam"].ToString();
            string gval = "";
            string jobcode = "", jobvalue = "";



            //-----------Update pdesignb-----------------//
            bool resultb = MktData.UpdateTransInfo2(comcod, "SP_ENTRY_PROJECTCOMFCHART", "INSERTORUPDATEPDESIGNB", pactcode, actcode, proesam, proadam, dueam,
                            PostbyId, Posteddat, posttrmid, postsesson, usircode, "", "", "", "", "", "", "", "", "", "", "");

            if (!resultb)
            {

                ((Label)this.Master.FindControl("lblmsg")).Text = MktData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;

            }


            foreach (DataRow dr1 in dt.Rows)
            {


                string gcod = dr1["gcod"].ToString();
                gval = dr1["gval"].ToString();




                //foreach (DataRow drj in dtj.Rows)
                //{


                switch (gcod)
                {



                    case "1001001": //Percentage  
                    case "1001014": //Remarks  

                        // j = 0;
                        for (k = 1; k <= lenj; k++)
                        {


                            jobvalue = dt.Rows[i]["job" + k.ToString()].ToString();
                            jobcode = dtj.Rows[k - 1]["jobcode"].ToString();
                            bool resulta = MktData.UpdateTransInfo2(comcod, "SP_ENTRY_PROJECTCOMFCHART", "INSERTORUPDATEPDESIGNA", pactcode, actcode, jobcode, gcod, gval,
                            jobvalue, usircode, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                            if (!resulta)
                            {
                                ((Label)this.Master.FindControl("lblmsg")).Text = MktData.ErrorObject["Msg"].ToString();
                                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                                return;

                            }


                        }
                        break;


                    case "1001002": //Amount  

                        for (k = 1; k <= lenj; k++)
                        {


                            jobvalue = Convert.ToDouble("0" + dt.Rows[i]["job" + k.ToString()].ToString()).ToString();
                            jobcode = dtj.Rows[k - 1]["jobcode"].ToString();
                            bool resulta = MktData.UpdateTransInfo2(comcod, "SP_ENTRY_PROJECTCOMFCHART", "INSERTORUPDATEPDESIGNA", pactcode, actcode, jobcode, gcod, gval,
                               jobvalue, usircode, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                            if (!resulta)
                            {
                                ((Label)this.Master.FindControl("lblmsg")).Text = MktData.ErrorObject["Msg"].ToString();
                                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                                return;

                            }


                        }


                        break;


                    case "1001004": //Dropdown Concern Person, 
                        for (k = 1; k <= lenj; k++)
                        {

                            jobcode = dtj.Rows[k - 1]["jobcode"].ToString();
                            jobvalue = dt.Rows[i]["job" + k.ToString()].ToString();
                            bool resulta = MktData.UpdateTransInfo2(comcod, "SP_ENTRY_PROJECTCOMFCHART", "INSERTORUPDATEPDESIGNA", pactcode, actcode, jobcode, gcod, gval,
                               jobvalue, usircode, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                            if (!resulta)
                            {
                                ((Label)this.Master.FindControl("lblmsg")).Text = MktData.ErrorObject["Msg"].ToString();
                                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                                return;

                            }


                        }

                        break;


                    case "1001006": //Target Start Date Value, 
                    case "1001008": //Target End Date Value, 
                    case "1001010": //Actual Start Date Value, 
                    case "1001012": //Actual End Date Value, 
                        for (k = 1; k <= lenj; k++)
                        {

                            jobcode = dtj.Rows[k - 1]["jobcode"].ToString();
                            jobvalue = dt.Rows[i]["job" + k.ToString()].ToString().Trim().Length == 0 ? "01-Jan-1900" : Convert.ToDateTime(dt.Rows[i]["job" + k.ToString()].ToString()).ToString("dd-MMM-yyyy");
                            bool resulta = MktData.UpdateTransInfo2(comcod, "SP_ENTRY_PROJECTCOMFCHART", "INSERTORUPDATEPDESIGNA", pactcode, actcode, jobcode, gcod, gval,
                               jobvalue, usircode, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                            if (!resulta)
                            {
                                ((Label)this.Master.FindControl("lblmsg")).Text = MktData.ErrorObject["Msg"].ToString();
                                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                                return;

                            }


                        }
                        break;

                }
                //}


                i++;

            }


            // Description
            foreach (DataRow dr1 in dtj.Rows)
            {


                string rowid =dr1["rowid"].ToString();
                jobcode = dr1["jobcode"].ToString();
                jobvalue = dr1["jobdesc"].ToString();
                bool resulta = MktData.UpdateTransInfo2(comcod, "SP_ENTRY_PROJECTCOMFCHART", "INSERTORUPDATEPDESIGNC", pactcode, actcode, jobcode, jobvalue, rowid,
                   usircode, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");

                if (!resulta)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = MktData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;

                }

            }







                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully.";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
        }
        protected void lbtnDeleteJob_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComCode();
            string pactcode = this.ddlPrjName.SelectedValue.ToString();
            DataTable dtj = (DataTable)Session["tbljob"];
            int codesl = Convert.ToInt32(((LinkButton)(sender)).CommandArgument);
            string jobcode = dtj.Rows[codesl - 1]["jobcode"].ToString();


            bool result = MktData.UpdateTransInfo2(comcod, "SP_ENTRY_PROJECTCOMFCHART", "DELETEJOB", pactcode, jobcode, "", "", "",
                                 "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = MktData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;

            }
            this.LoadGrid();
            ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully.";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            return;



        }
    }


}
