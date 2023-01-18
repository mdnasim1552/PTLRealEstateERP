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
//using  RealERPRPT;
using RealEntity;
namespace RealERPWEB.F_21_MKT
{
    public partial class MktEmpKpiEntry : System.Web.UI.Page
    {
        private UserManagerKPI objUser = new UserManagerKPI();
        ProcessAccess KpiData = new ProcessAccess();
        public static string TString = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                //((Label)this.Master.FindControl("lblTitle")).Text = "Client Discussion Information";
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkPrint")).Visible = false;
                this.txtFrom.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                this.lbluseid.Text = (Request.QueryString["Type"] == "Client") ? hst["usrid"].ToString() : "";

                //this.rbtnlist.SelectedValue = "810100601005";//this.Request.QueryString["kpigrp"].ToString();
                this.lblEdit.Text = "";
                GetFollow();
                GetParcipants();
                this.GetEmpList();
            }
        }



        private void GetEmpList()
        {
            if (this.lnkok.Text == "New")
                return;
            //-----------Get Person List ---------------//

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = Getcomcod();
            string srchEmp = "%%";
            string userid = (this.Request.QueryString["Type"] == "Entry") ? hst["usrid"].ToString() : "";
            string deptcode = hst["deptcode"].ToString();
            List<RealEntity.C_47_Kpi.EClassEmpCode> lst3 = new List<RealEntity.C_47_Kpi.EClassEmpCode>();
            lst3 = objUser.GetEmpCode("dbo_kpi.SP_ENTRY_EMP_KPI_SETUP", "GETENTRYEMPID", srchEmp, userid, deptcode);

            this.ddlEmpid.DataTextField = "empname1";
            this.ddlEmpid.DataValueField = "empid";
            this.ddlEmpid.DataSource = lst3;
            this.ddlEmpid.DataBind();

            //string qtype = this.Request.QueryString["UType"].ToString();
            //if (qtype == "Client")
            //{
            //    this.ddlEmpid.SelectedValue = this.Request.QueryString["empid"].ToString();
            //    this.ddlEmpid.Enabled = false;
            //}


            this.GetClientCode();

        }



        //[System.Web.Services.WebMethod(EnableSession=true)]

        //public  static List<RealEntity.C_47_Kpi.EClassEmpCode> GetEmpList02(string srchteam) 
        //{

        //    List<RealEntity.C_47_Kpi.EClassEmpCode> lst = new List<RealEntity.C_47_Kpi.EClassEmpCode>();

        //    //Hashtable hst = (Hashtable)Session["tblLogin"];
        //    //string comcod = Getcomcod();
        //    //string userid = (Request.QueryString["Type"] == "Client") ? hst["usrid"].ToString() : "";
        //    string userid = "";

        //    lst = objUser.GetEmpCode("dbo_kpi.SP_ENTRY_EMP_KPI_SETUP", "GETEMPID", srchteam, userid);
        //    return lst;


        //}


        private string Getcomcod()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }




        protected void lnkok_Click(object sender, EventArgs e)
        {

            if (this.lnkok.Text == "Ok")
            {

                ViewState.Remove("tblappmnt");
                string comcod = this.Getcomcod();
                this.ddlEmpid.Enabled = false;
                //this.ddlClient.Enabled = false;
                this.lnkok.Text = "New";
                this.MultiView1.ActiveViewIndex = 0;
                //this.rbtnlist.Enabled = false;
                this.ShowDiscussion();
                this.ShowPreDiscussion();
                this.lblHeaderPredis.Visible = true;

            }
            else
            {

                this.lnkok.Text = "Ok";
                this.ddlEmpid.Enabled = true;
                this.ddlClient.Enabled = true;

                ((Label)this.Master.FindControl("lblmsg")).Visible = false;
                this.MultiView1.ActiveViewIndex = -1;
                // this.rbtnlist.Enabled = true;
                this.gvInfo.DataSource = null;
                this.gvInfo.DataBind();
                this.gvclient.DataSource = null;
                this.gvclient.DataBind();
                this.lblHeaderPredis.Visible = false;
            }
        }

        private void ShowDiscussion()
        {
            string comcod = this.Getcomcod();
            DataTable tbl1 = (DataTable)ViewState["tbModalData"];
            string YmonID = Convert.ToDateTime(this.txtFrom.Text.Trim()).ToString("yyyyMM");
            string Empid = this.ddlEmpid.SelectedValue.ToString();
            //string grpcode = this.lblgrp.Text;
            string Client = this.ddlClient.SelectedValue.ToString();
            string kpigrp = "000000000000";// this.rbtnlist.SelectedValue.ToString();
            string wrkdpt = "000000000000";
            DateTime time = System.DateTime.Now;
            string qcdate = this.Request.QueryString["followupdate"] ?? "";
            string cdate = qcdate.Length == 0 ? this.txtFrom.Text + " " + time.ToString("HH:mm") : qcdate;


            DataSet ds1 = KpiData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "DAILYDISCUIND", Empid, Client, kpigrp, "", wrkdpt, cdate);


            // DataSet ds1 = KpiData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "DAILYDISCUIND", Empid, Client, kpigrp, "", wrkdpt, cdate);
            ViewState["tbModalData"] = HiddenSameData(ds1.Tables[0]);
            this.Modal_Data_Bind();



        }
        private void ShowPreDiscussion()
        {

            //try
            //{

            ViewState.Remove("tblprediscussion");
            string comcod = this.Getcomcod();
            DataTable tbl1 = (DataTable)ViewState["tbModalData"];
            string YmonID = Convert.ToDateTime(this.txtFrom.Text.Trim()).ToString("yyyyMM");
            string Empid = this.ddlEmpid.SelectedValue.ToString();
            //string grpcode = this.lblgrp.Text;
            string Client = this.ddlClient.SelectedValue.ToString();
            // string kpigrp = this.rbtnlist.SelectedValue.ToString();
            DateTime time = System.DateTime.Now;

            string cdate = this.txtFrom.Text + " " + time.ToString("HH:mm");


            DataSet ds1 = KpiData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "SHOWPREDISCUSSION", Empid, Client, cdate, "", "", "");



            this.gvclient.DataSource = ds1.Tables[0];
            this.gvclient.DataBind();
            ViewState["tblprediscussion"] = ds1.Tables[0];
            ds1.Dispose();


            //}

            //catch (Exception ex) 
            //{
            //    ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);      

            //}


        }
        private void GetClientCode()
        {
            //-----------Get Person List ---------------//
            UserManagerKPI objUser = new UserManagerKPI();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.Getcomcod();
            string Empid = this.ddlEmpid.SelectedValue.ToString();
            string srchEmp = "%%";
            List<RealEntity.C_47_Kpi.EClassClientCode> lst3 = new List<RealEntity.C_47_Kpi.EClassClientCode>();
            lst3 = objUser.GetClientCode("dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "SHOWCLIENTGRP", srchEmp, Empid);

            this.ddlClient.DataTextField = "cdesc";
            this.ddlClient.DataValueField = "ccode";
            this.ddlClient.DataSource = lst3;
            this.ddlClient.DataBind();



            // .NET < 4.0
            if (string.IsNullOrEmpty(Request.QueryString["clientid"]))
            {
                this.ddlClient.Enabled = true;

            }
            else
            {
                this.ddlClient.SelectedValue = this.Request.QueryString["clientid"].ToString();
                this.ddlClient.Enabled = false;

            }



        }
        protected void imgSearchSalesTeam_Click(object sender, EventArgs e)
        {
            this.GetEmpList();
            this.GetClientCode();
        }

        protected void ddlEmpid_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetClientCode();
        }
        protected void btnClient_Click(object sender, EventArgs e)
        {
            this.GetClientCode();
        }
        protected void lnkappupdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;


            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);


            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            try
            {
                string YmonID = Convert.ToDateTime(this.txtFrom.Text.Trim()).ToString("yyyyMM");
                string comcod = this.Getcomcod();
                string empid = this.ddlEmpid.SelectedValue.ToString();
                DataTable tbl1 = (DataTable)ViewState["tbEmpKpiEnrty"];

                foreach (DataRow dr2 in tbl1.Rows)
                {
                    string kpigrp = dr2["kpigrp"].ToString();
                    string stdkpival = dr2["stdkpival"].ToString();
                    string stdtarget = dr2["stdtarget"].ToString();
                    string rmarks = dr2["rmarks"].ToString();

                    bool m = KpiData.UpdateTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_SETUP", "INSERTUPDATESTD", YmonID, empid, kpigrp, stdkpival, "", stdtarget, rmarks);
                    if (m == false)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Error" + KpiData.ErrorObject["Msg"];
                        return;
                    }

                    else
                    {
                        //((Label)this.Master.FindControl("lblmsg")).Text = "Updated";

                        //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);


                    }
                }



                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                    string eventdesc = "Update Info";
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


        protected void lnkprint_Click(object sender, EventArgs e)
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //string comcod = this.Getcomcod();
            //string clientcod = this.ddlClientList.SelectedValue.ToString();
            //DataSet dset1 = this.KpiData.GetTransInfo(comcod, "SP_ENTRY_CLIENT_INFORMATION", "RPTCLIENTCOMUCATION", clientcod, "", "", "", "", "", "", "", "");
            //DataTable dtab1 = dset1.Tables[0];
            //ReportDocument rptAppMonitor = new  RealERPRPT.R_21_Mkt.RptTodaysDisAndNextApp();
            //TextObject CompName = rptAppMonitor.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //CompName.Text = comnam;
            //TextObject txtsalesp = rptAppMonitor.ReportDefinition.ReportObjects["txtsalesp"] as TextObject;
            //txtsalesp.Text = this.ddlSalesTeam.SelectedItem.Text;
            //TextObject txtdate = rptAppMonitor.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
            //TextObject txtclientname = rptAppMonitor.ReportDefinition.ReportObjects["txtclientname"] as TextObject;
            //txtclientname.Text = this.ddlClientList.SelectedItem.Text;
            //rptAppMonitor.SetDataSource(dtab1);
            //Session["Report1"] = rptAppMonitor;
            //this.lblprint.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }





        private void GetIntlocation()
        {
            ViewState.Remove("tbllocation");
            string comcod = Getcomcod();
            DataSet dt11 = this.KpiData.GetTransInfo(comcod, "dbo_kpi.SP_REPORT_EMP_DASEBOARD", "MKTINTLOCATION", "", "", "", "", "", "", "", "", "");
            DataTable dt = dt11.Tables[0];
            ViewState["tbllocation"] = dt;

        }
        private void GetVisitoraStatinfo()
        {
            ViewState.Remove("tblvisitor");
            string comcod = Getcomcod();
            DataSet dt11 = this.KpiData.GetTransInfo(comcod, "dbo_kpi.SP_REPORT_EMP_DASEBOARD", "GETVISITOR", "", "", "", "", "", "", "", "", "");
            DataTable dt = dt11.Tables[0];

            ViewState["tblvisiastator"] = dt;

        }


        private void GetGrade()
        {
            ViewState.Remove("tblgrade");
            string comcod = Getcomcod();
            DataSet ds1 = this.KpiData.GetTransInfo(comcod, "dbo_kpi.SP_REPORT_EMP_DASEBOARD", "MKTIGRADE", "", "", "", "", "", "", "", "", "");
            DataTable dt11 = ds1.Tables[0];
            ViewState["tblgrade"] = dt11;

        }




        private void GetProjectAUnit()
        {
            ViewState.Remove("tblproaunit");
            string comcod = Getcomcod();
            DataSet dss = this.KpiData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "GETPROJECTAUNIT", "", "", "", "", "", "", "", "", "");
            ViewState["tblproaunit"] = dss;
            dss.Dispose();
        }

        protected void lnkTotal_Click(object sender, EventArgs e)
        {

            double UnitSize = Convert.ToDouble("0" + ((TextBox)this.gvInfo.Rows[3].FindControl("txtgvVal")).Text.Trim());
            double Oferedprice = Convert.ToDouble("0" + ((TextBox)this.gvInfo.Rows[8].FindControl("txtgvVal")).Text.Trim());
            double OferPamt = Convert.ToDouble("0" + ((TextBox)this.gvInfo.Rows[9].FindControl("txtgvVal")).Text.Trim());
            double Oferothamt = Convert.ToDouble("0" + ((TextBox)this.gvInfo.Rows[10].FindControl("txtgvVal")).Text.Trim());
            double oftuamt = ((UnitSize * Oferedprice) + OferPamt + Oferothamt);
            ((TextBox)this.gvInfo.Rows[11].FindControl("txtgvVal")).Text = oftuamt.ToString("#,##0;(#,##0); ");


        }
        protected void Modalupdate_Click(object sender, EventArgs e)
        {



            try
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                string comcod = this.Getcomcod();
                string empid = this.ddlEmpid.SelectedValue.ToString();
                string Client = this.ddlClient.SelectedValue.ToString();
                string kpigrp = "000000000000";
                string wrkdpt = "000000000000";
                DateTime time = System.DateTime.Now;

                //string cdate = this.txtFrom.Text.ToString() +" "+ time.ToString("HH:mm");

                string cdate = Convert.ToDateTime((((TextBox)this.gvInfo.Rows[0].FindControl("txtgvdVal")).Text.Trim() + " " + ((DropDownList)this.gvInfo.Rows[0].FindControl("ddlhour")).SelectedValue.ToString()
                            + ":" + ((DropDownList)this.gvInfo.Rows[0].FindControl("ddlMmin")).SelectedValue.ToString() + " " + ((DropDownList)this.gvInfo.Rows[0].FindControl("ddlslb")).SelectedValue.ToString())).ToString("dd-MMM-yyyy HH:mm:ss");



                string Gvalue = "";
                bool result;
                for (int i = 0; i < this.gvInfo.Rows.Count; i++)
                {
                    string Gcode = ((Label)this.gvInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                    string gtype = ((Label)this.gvInfo.Rows[i].FindControl("lgvgval")).Text.Trim();



                    if (Gcode == "810100101002")
                    {

                        Gvalue = (((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstFollow")).Items.Count == 0) ? ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Text.Trim()
                            : ((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstFollow")).SelectedValue.ToString();
                    }

                    else if (Gcode == "810100101003")
                    {

                        Gvalue = (((DropDownList)this.gvInfo.Rows[i].FindControl("ddlProject")).Items.Count == 0) ? ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Text.Trim()
                                    : ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlProject")).SelectedValue.ToString();
                    }

                    else if (Gcode == "810100101019")
                    {

                        Gvalue = (((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstFollow")).Items.Count == 0) ? ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Text.Trim()
                            : ((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstFollow")).SelectedValue.ToString();
                    }



                    else if (Gcode == "810100101016")
                    {

                        Gvalue = (((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstStatus")).Items.Count == 0) ? ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Text.Trim()
                            : ((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstStatus")).SelectedValue.ToString();
                    }

                    else if (Gcode == "810100101017" || Gcode == "810100101014")
                    {

                        Gvalue = (((DropDownList)this.gvInfo.Rows[i].FindControl("ddlVisit")).Items.Count == 0) ? ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Text.Trim()
                            : ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlVisit")).SelectedValue.ToString();
                    }


                    else if (Gcode == "810100101018")
                    {

                        //Gvalue == "";
                        foreach (ListItem item in ((ListBox)this.gvInfo.Rows[i].FindControl("ddlPartic")).Items)
                        {
                            if (item.Selected)
                            {

                                if (item.Selected)
                                {
                                    Gvalue += item.Value;
                                }
                            }
                        }


                        //Gvalue = (((ListBox)this.gvInfo.Rows[i].FindControl("ddlPartic")).Items.Count == 0) ? ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Text.Trim()
                        //    : ((ListBox)this.gvInfo.Rows[i].FindControl("ddlPartic")).SelectedValue.ToString();
                    }




                    else if (Gcode == "810100101001" || Gcode == "810100101020")
                    {

                        //string fdatetime = Convert.ToDateTime((((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim()+ " " + ddlhour+":" + ddlMmin +" "+ ddlslb)).ToString("dd-MMM-yyyy HH:mm:ss");

                        Gvalue = (((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim() == "") ? System.DateTime.Today.ToString("dd-MMM-yyyy")
                            : Convert.ToDateTime((((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim() + " " + ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlhour")).SelectedValue.ToString()
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

                        Gvalue = ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                    }

                    Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue) : (gtype == "N") ? Convert.ToDouble("0" + Gvalue).ToString() : Gvalue;




                    if (Gvalue != "")
                    {
                        result = KpiData.UpdateTransInfo3(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "INSERTUPDATESCDINF", empid, Client, kpigrp, "", wrkdpt, cdate, Gcode, gtype, Gvalue);
                        if (!result)
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = "Update Fail";
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                            return;
                        }
                        Gvalue = "";
                    }


                }

                //if (this.Request.QueryString["Type"].ToString() == "Entry" && this.Request.QueryString["nfollow"].Length != 0)
                //{
                //    string PDisDate = Convert.ToDateTime(ASTUtility.Left(this.Request.QueryString["nfollow"], 11) + " " + ASTUtility.Right(this.Request.QueryString["nfollow"], 5)).ToString("dd-MMM-yyyy HH:mm:ss");

                //    bool result2 = KpiData.UpdateTransInfo3(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "UPDATEFOLLOWST", empid, Client, kpigrp, wrkdpt, PDisDate, "810100102020", "Y");


                //    if (!result2)
                //    {
                //        ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail";
                //        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                //        return;
                //    }
                //}


                ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                this.ShowPreDiscussion();
                // Response.Redirect("~/F_99_Allinterface/KPIDashboard.aspx?Type=Report&comcod=");


                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                    string eventdesc = "Update Info";
                    string eventdesc2 = "";
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }


                //this.ShowData();


            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }






            //try
            //{
            // ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            //    string comcod = this.Getcomcod();
            //    string empid = this.ddlEmpid.SelectedValue.ToString();
            //    string Client = this.ddlClient.SelectedValue.ToString();
            //    string kpigrp = "";//this.rbtnlist.SelectedValue.ToString();

            //    string wrkdpt = "000000000000";

            //    DateTime time = System.DateTime.Now;


            //    string cdate = this.txtFrom.Text.ToString() +" "+ time.ToString("HH:mm");

            //    string Gvalue = "";
            //    bool result;
            //    for (int i = 0; i < this.gvInfo.Rows.Count; i++)
            //    {
            //        string Gcode = ((Label)this.gvInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
            //        string gtype = ((Label)this.gvInfo.Rows[i].FindControl("lgvgval")).Text.Trim();

            //        if (Gcode == "810100101002")
            //        {

            //            Gvalue = (((DropDownList)this.gvInfo.Rows[i].FindControl("ddlProject")).Items.Count == 0) ? ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Text.Trim()
            //                : ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlProject")).SelectedValue.ToString();
            //        }
            //        else if (Gcode == "810100101003")
            //        {

            //            Gvalue = (((DropDownList)this.gvInfo.Rows[i].FindControl("ddlUnit")).Items.Count == 0) ? ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Text.Trim()
            //                : ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlUnit")).SelectedValue.ToString();
            //        }



            //        else if (Gcode == "810100102016")
            //        {

            //            Gvalue = (((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstStatus")).Items.Count == 0) ? ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Text.Trim()
            //                : ((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstStatus")).SelectedValue.ToString();
            //        }






            //        //else if (Gcode == "810100101017")
            //        //{

            //        //    Gvalue = (((DropDownList)this.gvInfo.Rows[i].FindControl("ddllocation")).Items.Count == 0) ? ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Text.Trim()
            //        //        : ((DropDownList)this.gvInfo.Rows[i].FindControl("ddllocation")).SelectedValue.ToString();
            //        //}

            //        else if (Gcode == "810100101017")
            //        {

            //            Gvalue = (((DropDownList)this.gvInfo.Rows[i].FindControl("ddlVisit")).Items.Count == 0) ? ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Text.Trim()
            //                : ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlVisit")).SelectedValue.ToString();
            //        }



            //        else if (Gcode == "810100101018")
            //        {

            //            //Gvalue == "";
            //            foreach (ListItem item in ((ListBox)this.gvInfo.Rows[i].FindControl("ddlPartic")).Items)
            //            {
            //                if (item.Selected)
            //                {

            //                    if (item.Selected)
            //                    {
            //                        Gvalue += item.Value;
            //                    }
            //                }
            //            }


            //            //Gvalue = (((ListBox)this.gvInfo.Rows[i].FindControl("ddlPartic")).Items.Count == 0) ? ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Text.Trim()
            //            //    : ((ListBox)this.gvInfo.Rows[i].FindControl("ddlPartic")).SelectedValue.ToString();
            //        }





            //        else if (Gcode == "810100101001")
            //        {

            //            Gvalue = (((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim() == "") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim();
            //        }
            //        else if (Gcode == "810100101020")
            //        {

            //            Gvalue = (((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim() == "") ? "1900-01-01 00:00:00" : ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim();
            //        }
            //        else
            //        {

            //            Gvalue = ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
            //        }

            //        Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue) : (gtype == "N") ? Convert.ToDouble("0" + Gvalue).ToString() : Gvalue;
            //        if (Gvalue != "")
            //        {
            //             result = KpiData.UpdateTransInfo3(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "INSERTUPDATESCDINF", empid, Client,  kpigrp, "", wrkdpt, cdate, Gcode, gtype, Gvalue);
            //             if (!result)
            //             {
            //              ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail";
            //              ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            //             }
            //        }


            //    }

            // ((Label)this.Master.FindControl("lblmsg")).Text = "Updated";
            // ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            // Response.Redirect("~/F_99_Allinterface/KPIDashboard.aspx?Type=Report&comcod=");


            //    if (ConstantInfo.LogStatus == true)
            //    {
            //        string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
            //        string eventdesc = "Update Info";
            //        string eventdesc2 = "";
            //        bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //    }


            //    //this.ShowData();


            //}
            //catch (Exception ex)
            //{
            // ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            // ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            //}
        }

        private void Modal_Data_Bind()
        {



            //try

            //{



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
            DataTable dt5 = ((DataTable)ViewState["tblFollow"]).Copy(); ;
            DataView dv1;
            dv1 = dt5.DefaultView;
            dv1.RowFilter = ("gcod like '96%'");


            DataTable dt6 = (DataTable)ViewState["tblparti"];



            //DataTable dtvs = ((DataTable)ViewState["tblFollow"]).Copy();



            //GetGrade();
            //DataTable dt6 = (DataTable)ViewState["tblgrade"];

            DataView dv;
            DataTable dtvs = ((DataTable)ViewState["tblFollow"]).Copy();
            dv = dtvs.DefaultView;
            dv.RowFilter = ("gcod like '95%'");
            DataTable dts = dv.ToTable();


            ////  Status
            //dv = dtvs.DefaultView;
            //dv.RowFilter = ("gcod like '95%'");



            // Visitor
            GetVisitoraStatinfo();
            DataTable dtv = (DataTable)ViewState["tblvisiastator"];

            //DataView dv1;


            //DataView dv1;

            DropDownList ddlgval, ddlUnit, ddlVisitor, ddlgval1, ddlgval2, ddlgval3;
            ListBox ddlPartic;
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string Gcode = dt.Rows[i]["gcod"].ToString();

                switch (Gcode)
                {

                    case "810100101002": // Today's Followup
                    case "810100101019"://Next Followup

                        ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("pnlParic")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("pnlFollow")).Visible = true;
                        ((Label)this.gvInfo.Rows[i].FindControl("lblgvTime")).Visible = false;

                        ((Panel)this.gvInfo.Rows[i].FindControl("PnlProject")).Visible = false;
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlProject")).Items.Clear();
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlProject")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("PnlUnit")).Visible = false;
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlUnit")).Items.Clear();
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlUnit")).Visible = false;

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
                        ChkBoxLstFollow.SelectedValue = ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();

                        break;


                    case "810100101003": //Pactcode
                        ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("PnlUnit")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("pnlStatus")).Visible = false;


                        ddlgval = ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlProject"));
                        ddlgval.DataTextField = "pactdesc";
                        ddlgval.DataValueField = "pactcode";
                        ddlgval.DataSource = ds1.Tables[0];
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    case "810100101004": //Unit
                        ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("PnlProject")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("pnlStatus")).Visible = false;


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
                        ddlUnit.SelectedValue = ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;




                    case "810100101014": //Lead Quality

                        dv = dtv.DefaultView;
                        dv.RowFilter = ("gcod like '42%'");

                        ((Panel)this.gvInfo.Rows[i].FindControl("pnlVisit")).Visible = true;
                        ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("PnlProject")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("PnlUnit")).Visible = false;
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlVisit")).Visible = true;
                        ddlVisitor = ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlVisit"));
                        ddlVisitor.DataTextField = "gdesc";
                        ddlVisitor.DataValueField = "gcod";
                        ddlVisitor.DataSource = dv.ToTable();
                        ddlVisitor.DataBind();
                        ddlVisitor.SelectedValue = ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;




                    case "810100101016": //Status

                        ((Panel)this.gvInfo.Rows[i].FindControl("PnlProject")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("PnlUnit")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("pnlStatus")).Visible = true;
                        ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("pnlParic")).Visible = false;
                        ((Label)this.gvInfo.Rows[i].FindControl("lblgvTime")).Visible = false;
                        ((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstStatus")).Visible = true;

                        CheckBoxList ChkBoxLstStatus = ((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstStatus"));
                        ChkBoxLstStatus.DataTextField = "gdesc";
                        ChkBoxLstStatus.DataValueField = "gcod";
                        ChkBoxLstStatus.DataSource = dts;
                        ChkBoxLstStatus.DataBind();
                        ChkBoxLstStatus.SelectedValue = ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;








                    case "810100101017": //Visit
                        dv = dtv.DefaultView;
                        dv.RowFilter = ("gcod like '92%'");

                        ((Panel)this.gvInfo.Rows[i].FindControl("pnlVisit")).Visible = true;
                        ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("PnlProject")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("PnlUnit")).Visible = false;
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlVisit")).Visible = true;
                        ddlVisitor = ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlVisit"));
                        ddlVisitor.DataTextField = "gdesc";
                        ddlVisitor.DataValueField = "gcod";
                        ddlVisitor.DataSource = dv.ToTable();
                        ddlVisitor.DataBind();
                        ddlVisitor.SelectedValue = ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;


                    case "810100101018": //PARTICIPANTS  

                        ((Panel)this.gvInfo.Rows[i].FindControl("pnlFollow")).Visible = false;
                        ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("pnlParic")).Visible = true;
                        ((ListBox)this.gvInfo.Rows[i].FindControl("ddlPartic")).Visible = true;
                        ((Label)this.gvInfo.Rows[i].FindControl("lblgvTime")).Visible = false;

                        ((Panel)this.gvInfo.Rows[i].FindControl("PnlProject")).Visible = false;
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlProject")).Items.Clear();
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlProject")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("PnlUnit")).Visible = false;
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlUnit")).Items.Clear();
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlUnit")).Visible = false;


                        ddlPartic = ((ListBox)this.gvInfo.Rows[i].FindControl("ddlPartic"));
                        ddlPartic.DataTextField = "gdesc";
                        ddlPartic.DataValueField = "gcod";
                        ddlPartic.DataSource = dt6;
                        ddlPartic.DataBind();
                        if (((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Text.Trim() != "")
                        {
                            ddlPartic.SelectedValue = ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        }


                        int count = Convert.ToInt32(dt.Rows[i]["gdesc1"].ToString().Count());
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
                        ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("PnlUnit")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("PnlProject")).Visible = false;
                        ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).TextMode = TextBoxMode.MultiLine;
                        ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Rows = 3;
                        ((Panel)this.gvInfo.Rows[i].FindControl("pnlStatus")).Visible = false;

                        TextBox sd = ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal"));
                        sd.Style.Add("background", "#DFF0D8");
                        sd.Style.Add("width", "100%");


                        //((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Height=100;
                        break;

                    case "810100101020": //Next Followup
                    case "810100101001": //Meeting Date




                        ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("PnlProject")).Visible = false;
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlProject")).Items.Clear();
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlProject")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("PnlUnit")).Visible = false;
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlUnit")).Items.Clear();
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlUnit")).Visible = false;






                        ((Panel)this.gvInfo.Rows[i].FindControl("pnlTime")).Visible = true;


                        string gTime = ((Label)this.gvInfo.Rows[i].FindControl("lblgvTime")).Text.Trim();

                        ddlgval1 = ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlhour"));
                        ddlgval1.SelectedValue = (gTime.Length == 0) ? "" : ASTUtility.Left(gTime, 2);
                        ddlgval2 = ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlMmin"));
                        ddlgval2.SelectedValue = (gTime.Length == 0) ? "" : gTime.Substring(3, 2);
                        ddlgval3 = ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlslb"));
                        ddlgval3.SelectedValue = (gTime.Length == 0) ? "" : ASTUtility.Right(gTime, 2);


                        //((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Height=100;
                        break;

                    default:
                        ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("PnlProject")).Visible = false;
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlProject")).Items.Clear();
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlProject")).Visible = false;
                        ((Panel)this.gvInfo.Rows[i].FindControl("PnlUnit")).Visible = false;
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlUnit")).Items.Clear();
                        ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlUnit")).Visible = false;



                        break;

                }

            }
            //}
            //catch (Exception ex)
            //{
            //    ((Label)this.Master.FindControl("lblmsg")).Visible = true;

            //    ((Label)this.Master.FindControl("lblmsg")).Text = ex.ToString();
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);


            //}


            //try
            //{
            //    DataTable dt = (DataTable)ViewState["tbModalData"];
            //    this.gvInfo.DataSource = dt;
            //    this.gvInfo.DataBind();


            //    GetFollow();
            //    DataTable dt5 = ((DataTable)ViewState["tblFollow"]).Copy(); ;
            //    DataView dv1;
            //    dv1 = dt5.DefaultView;
            //    dv1.RowFilter = ("gcod like '96%'");

            //    GetParcipants();
            //    DataTable dt6 = (DataTable)ViewState["tblparti"];

            //    //GetVisitoraStatinfo();
            //    DataView dv;
            //    DataTable dtvs = ((DataTable)ViewState["tblFollow"]).Copy();


            //    //  Status
            //    dv = dtvs.DefaultView;
            //    dv.RowFilter = ("gcod like '95%'");

            //    //DataTable dts = dv.ToTable();
            //    //dts.Rows.Add("0000", "0000000", "----Select Status----");
            //    //dts.DefaultView.Sort = "gcod asc";
            //    //dts = dts.DefaultView.ToTable();
            //    //DataView dv1;


            //    //DataView dv1;

            //    DropDownList ddlgval1, ddlgval2, ddlgval3;
            //    ListBox ddlPartic;

            //      DropDownList ddlgval, ddlUnit, ddlocation, ddlgrade,ddlVisitor; 
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {

            //        string Gcode = dt.Rows[i]["gcod"].ToString();

            //        switch (Gcode)
            //        {



            //            case "810100101002": //Pactcode
            //                ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
            //                ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
            //                ((Panel)this.gvInfo.Rows[i].FindControl("PnlUnit")).Visible = false;
            //                ((Panel)this.gvInfo.Rows[i].FindControl("pnllocation")).Visible = false;
            //                ((Panel)this.gvInfo.Rows[i].FindControl("pnlGrade")).Visible = false;

            //                ddlgval = ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlProject"));
            //                ddlgval.DataTextField = "pactdesc";
            //                ddlgval.DataValueField = "pactcode";
            //                ddlgval.DataSource = ds1.Tables[0];
            //                ddlgval.DataBind();
            //                ddlgval.SelectedValue = ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
            //                break;
            //            case "810100101003": //Unit
            //                ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
            //                ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
            //                ((Panel)this.gvInfo.Rows[i].FindControl("PnlProject")).Visible = false;
            //                ((Panel)this.gvInfo.Rows[i].FindControl("pnllocation")).Visible = false;
            //                ((Panel)this.gvInfo.Rows[i].FindControl("pnlGrade")).Visible = false;

            //                string pactcode = ((DropDownList)this.gvInfo.Rows[i - 1].FindControl("ddlProject")).Text.Trim();

            //                DataTable dt1 = ds1.Tables[1].Copy();
            //                DataView dv1;
            //                dv1 = dt1.DefaultView;
            //                dv1.RowFilter = ("pactcode='000000000000' or pactcode='" + pactcode + "'");
            //                dt1 = dv1.ToTable();

            //                ddlUnit = ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlUnit"));
            //                ddlUnit.DataTextField = "udesc";
            //                ddlUnit.DataValueField = "usircode";
            //                ddlUnit.DataSource = dt1;//dv1.ToTable();
            //                ddlUnit.DataBind();
            //                //ddlUnit.SelectedValue = usircode;
            //                ddlUnit.SelectedValue = ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
            //                break;



            //            //case "810100101002":
            //            //case "810100101019"://Follow

            //            //    ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
            //            //    ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
            //            //    ((Panel)this.gvInfo.Rows[i].FindControl("pnlParic")).Visible = false;
            //            //    ((Panel)this.gvInfo.Rows[i].FindControl("pnlFollow")).Visible = true;
            //            //    ((Label)this.gvInfo.Rows[i].FindControl("lblgvTime")).Visible = false;
            //            //    //((DropDownList)this.gvInfo.Rows[i].FindControl("ChkBoxLstFollow")).Visible = true;                        
            //            //    //ChkBoxLstFollow = ((DropDownList)this.gvInfo.Rows[i].FindControl("ChkBoxLstFollow"));
            //            //    //ChkBoxLstFollow.DataTextField = "gdesc";
            //            //    //ChkBoxLstFollow.DataValueField = "gcod";
            //            //    //ChkBoxLstFollow.DataSource = dt5;
            //            //    //ChkBoxLstFollow.DataBind();
            //            //    //ChkBoxLstFollow.SelectedValue = ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();


            //            //    ((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstFollow")).Visible = true;
            //            //    CheckBoxList ChkBoxLstFollow = ((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstFollow"));
            //            //    ChkBoxLstFollow.DataTextField = "gdesc";
            //            //    ChkBoxLstFollow.DataValueField = "gcod";
            //            //    ChkBoxLstFollow.DataSource = dv1.ToTable();
            //            //    ChkBoxLstFollow.DataBind();
            //            //    ChkBoxLstFollow.SelectedValue = ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();

            //            //    break;





            //            case "810100101016": //Status

            //                ((Panel)this.gvInfo.Rows[i].FindControl("pnlStatus")).Visible = true;
            //                ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
            //                ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
            //                ((Panel)this.gvInfo.Rows[i].FindControl("pnlParic")).Visible = false;
            //                ((Label)this.gvInfo.Rows[i].FindControl("lblgvTime")).Visible = false;
            //                ((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstStatus")).Visible = true;

            //                CheckBoxList ChkBoxLstStatus = ((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstStatus"));
            //                ChkBoxLstStatus.DataTextField = "gdesc";
            //                ChkBoxLstStatus.DataValueField = "gcod";
            //                ChkBoxLstStatus.DataSource = dv.ToTable();
            //                ChkBoxLstStatus.DataBind();
            //                ChkBoxLstStatus.SelectedValue = ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
            //                break;

            //            case "810100101018": //PARTICIPANTS  

            //                ((Panel)this.gvInfo.Rows[i].FindControl("pnlFollow")).Visible = false;
            //                ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
            //                ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
            //                ((Panel)this.gvInfo.Rows[i].FindControl("pnlParic")).Visible = true;
            //                ((ListBox)this.gvInfo.Rows[i].FindControl("ddlPartic")).Visible = true;
            //                ((Label)this.gvInfo.Rows[i].FindControl("lblgvTime")).Visible = false;


            //                ddlPartic = ((ListBox)this.gvInfo.Rows[i].FindControl("ddlPartic"));
            //                ddlPartic.DataTextField = "gdesc";
            //                ddlPartic.DataValueField = "gcod";
            //                ddlPartic.DataSource = dt6;
            //                ddlPartic.DataBind();
            //                if (((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Text.Trim() != "")
            //                {
            //                    ddlPartic.SelectedValue = ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
            //                }


            //                int count = Convert.ToInt32(dt.Rows[i]["gdesc1"].ToString().Count());
            //                int j;
            //                int k = 0;
            //                string data = "";
            //                for (j = 0; j < count / 12; j++)
            //                {
            //                    data = dt.Rows[i]["gdesc1"].ToString().Substring(k, 12);
            //                    foreach (ListItem item in ddlPartic.Items)
            //                    {
            //                        if (item.Value == data)
            //                        {
            //                            item.Selected = true;
            //                        }

            //                    }
            //                    k = k + 12;
            //                }


            //                break;



            //            case "810100101015":
            //            case "810100101025"://Muliline
            //                ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
            //                ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).TextMode = TextBoxMode.MultiLine;
            //                ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Rows = 3;
            //                ((Panel)this.gvInfo.Rows[i].FindControl("pnlFollow")).Visible = false;
            //                ((Label)this.gvInfo.Rows[i].FindControl("lblgvTime")).Visible = false;

            //                //((DropDownList)this.gvInfo.Rows[i].FindControl("ChkBoxLstFollow")).Items.Clear();
            //                //((DropDownList)this.gvInfo.Rows[i].FindControl("ChkBoxLstFollow")).Visible = false;

            //                TextBox sd = ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal"));
            //                sd.Style.Add("background", "#DFF0D8");
            //                sd.Style.Add("width", "100%");


            //                //((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Height=100;
            //                break;

            //            case "810100101020": //Date Time
            //            case "810100101001": //
            //                ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
            //                ((Panel)this.gvInfo.Rows[i].FindControl("pnlTime")).Visible = true;


            //                //string gTime = ((Label)this.gvInfo.Rows[i].FindControl("lblgvTime")).Text.Trim();

            //                //ddlgval1 = ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlhour"));
            //                //ddlgval1.SelectedValue = ASTUtility.Left(gTime,2);
            //                //ddlgval2 = ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlMmin"));
            //                //ddlgval2.SelectedValue = gTime.Substring(3,2);
            //                //ddlgval3 = ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlslb"));
            //                //ddlgval3.SelectedValue = ASTUtility.Right(gTime,2);


            //                break;

            //            default:
            //                ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
            //                ((ListBox)this.gvInfo.Rows[i].FindControl("ddlPartic")).Items.Clear();
            //                ((ListBox)this.gvInfo.Rows[i].FindControl("ddlPartic")).Visible = false;
            //                ((Label)this.gvInfo.Rows[i].FindControl("lblgvTime")).Visible = false;

            //                break;

            //        }

            //    }
            //}
            //catch (Exception ex)
            //{
            //    ((Label)this.Master.FindControl("lblmsg")).Visible = true;

            //    ((Label)this.Master.FindControl("lblmsg")).Text = ex.ToString();
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);


            //}






        }


        private void GetFollow()
        {
            ViewState.Remove("tblFollow");
            string comcod = Getcomcod();
            DataSet dt11 = this.KpiData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "FOLLOWUPCODE", "", "", "", "", "", "", "", "", "");
            DataTable dt = dt11.Tables[0];
            ViewState["tblFollow"] = dt;

        }


        private void GetParcipants()
        {
            ViewState.Remove("tblparti");
            string comcod = Getcomcod();
            DataSet ds1 = this.KpiData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "PARTICIPANTS", "", "", "", "", "", "", "", "", "");
            DataTable dt11 = ds1.Tables[0];
            ViewState["tblparti"] = dt11;

        }

        protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblappmnt"];
            DataTable ddt = (DataTable)ViewState["tbother"];
            DataSet ds1 = (DataSet)ViewState["tblproaunit"];
            string pactcode = ((DropDownList)this.gvInfo.Rows[1].FindControl("ddlProject")).Text.Trim();
            string usircode = ((DropDownList)this.gvInfo.Rows[2].FindControl("ddlUnit")).Text.Trim();
            //for (int i = 0; i < this.gvInfo.Rows.Count; i++)
            //{

            DataTable dt1 = ds1.Tables[1].Copy();
            DataView dv1;
            dv1 = dt1.DefaultView;
            dv1.RowFilter = ("pactcode='000000000000' or pactcode='" + pactcode + "'");
            dt1 = dv1.ToTable();
            ((TextBox)this.gvInfo.Rows[3].FindControl("txtgvVal")).Text = ((usircode == "000000000000") ? 0 : Convert.ToDouble((dt1.Select("pactcode='" + pactcode + "' and usircode='" + usircode + "'"))[0]["usize"])).ToString("#,##0;(#,##0); ");
            ((TextBox)this.gvInfo.Rows[4].FindControl("txtgvVal")).Text = ((usircode == "000000000000") ? 0 : Convert.ToDouble((dt1.Select("pactcode='" + pactcode + "' and usircode='" + usircode + "'"))[0]["rate"])).ToString("#,##0;(#,##0); ");
            ((TextBox)this.gvInfo.Rows[5].FindControl("txtgvVal")).Text = ((usircode == "000000000000") ? 0 : Convert.ToDouble((dt1.Select("pactcode='" + pactcode + "' and usircode='" + usircode + "'"))[0]["pamt"])).ToString("#,##0;(#,##0); ");
            ((TextBox)this.gvInfo.Rows[6].FindControl("txtgvVal")).Text = ((usircode == "000000000000") ? 0 : Convert.ToDouble((dt1.Select("pactcode='" + pactcode + "' and usircode='" + usircode + "'"))[0]["othamt"])).ToString("#,##0;(#,##0); ");
            ((TextBox)this.gvInfo.Rows[7].FindControl("txtgvVal")).Text = ((usircode == "000000000000") ? 0 : Convert.ToDouble((dt1.Select("pactcode='" + pactcode + "' and usircode='" + usircode + "'"))[0]["tuamt"])).ToString("#,##0;(#,##0); ");



        }
        protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)ViewState["tblappmnt"];
            DataSet ds1 = (DataSet)ViewState["tblproaunit"];
            DropDownList ddlgval;

            string pactcode = ((DropDownList)this.gvInfo.Rows[1].FindControl("ddlProject")).SelectedValue.ToString();


            for (int i = 0; i < this.gvInfo.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();

                switch (Gcode)
                {
                    case "810100101003":
                        DataTable dt1 = ds1.Tables[1].Copy();
                        DataView dv1;
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("pactcode='000000000000' or pactcode='" + pactcode + "'");
                        ddlgval = ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlUnit"));
                        ddlgval.DataTextField = "udesc";
                        ddlgval.DataValueField = "usircode";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        break;

                }
            }



        }
        protected void imgSearchProject_Click(object sender, EventArgs e)
        {

        }
        protected void imgSearchUnit_Click(object sender, EventArgs e)
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

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {

            try
            {
                string comcod = this.Getcomcod();
                DataTable dt = (DataTable)ViewState["tblprediscussion"];
                int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                string empid = this.ddlEmpid.SelectedValue.ToString();
                string client = this.ddlClient.SelectedValue.ToString();
                string cdate = Convert.ToDateTime(dt.Rows[RowIndex]["cdate"]).ToString();
                string kpigrp = dt.Rows[RowIndex]["kpigrp"].ToString();
                bool result = KpiData.UpdateTransInfo3(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "DELETEPREDISCUSSION", empid, client, kpigrp, cdate, "", "", "", "", "", "");
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Deleted Fail";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }

                dt.Rows[RowIndex].Delete();
                DataView dv = dt.DefaultView;
                ViewState.Remove("tblprediscussion");
                ViewState["tblprediscussion"] = dv.ToTable();
                this.gvclient.DataSource = dv.ToTable();
                this.gvclient.DataBind();


                ((Label)this.Master.FindControl("lblmsg")).Text = "Successfully  Delete";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            }


            catch (Exception ex)
            {

                ((Label)this.Master.FindControl("lblmsg")).Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);


            }


        }
        protected void lbtnEdit_Click(object sender, EventArgs e)
        {

            string cdate;

            this.lblEdit.Text = "Edit";
            if (this.Request.QueryString["Type"].ToString() == "Edit")
            {

                cdate = Convert.ToDateTime(ASTUtility.Left(this.Request.QueryString["nfollow"], 11) + " " + ASTUtility.Right(this.Request.QueryString["nfollow"], 5)).ToString("dd-MMM-yyyy HH:mm:ss");
            }
            else
            {
                int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                cdate = ((Label)this.gvclient.Rows[RowIndex].FindControl("lblgvDate")).Text.ToString();

            }


            string comcod = this.Getcomcod();
            DataTable tbl1 = (DataTable)ViewState["tbModalData"];
            string YmonID = Convert.ToDateTime(this.txtFrom.Text.Trim()).ToString("yyyyMM");
            string Empid = this.ddlEmpid.SelectedValue.ToString();

            string Client = this.ddlClient.SelectedValue.ToString();
            string kpigrp = "000000000000";
            string wrkdpt = "000000000000";
            DateTime time = System.DateTime.Now;


            DataSet ds1 = KpiData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "DAILYDISCUIND", Empid, Client, kpigrp, "", wrkdpt, cdate);
            DataTable dt = HiddenSameData(ds1.Tables[0]);

            ViewState["tbModalData"] = HiddenSameData(dt);
            this.Modal_Data_Bind();




        }
        protected void gvInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtgvdVal = (TextBox)e.Row.FindControl("txtgvdVal");
                DropDownList ddlhour = (DropDownList)e.Row.FindControl("ddlhour");
                DropDownList ddlMmin = (DropDownList)e.Row.FindControl("ddlMmin");
                DropDownList ddlslb = (DropDownList)e.Row.FindControl("ddlslb");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "gcod")).ToString();

                string edit = this.lblEdit.Text.Trim();
                if (this.Request.QueryString["Type"].ToString() == "Edit" || edit == "Edit")
                {

                    if (code == "810100101001")
                    {

                        txtgvdVal.Enabled = false;
                        ddlhour.Enabled = false;
                        ddlMmin.Enabled = false;
                        ddlslb.Enabled = false;

                    }
                }


            }
        }
        protected void lnkAdddis_Click(object sender, EventArgs e)
        {
            //this.txtComm.Text = "";
            int index = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string cDate = ((Label)this.gvclient.Rows[index].FindControl("lblgvDate")).Text.ToString().Trim();

            string discussion = ((Label)this.gvclient.Rows[index].FindControl("lgvDiscussion0")).Text.ToString().Trim();
            string disgnote = ((Label)this.gvclient.Rows[index].FindControl("lblgvdisgnote")).Text.ToString().Trim();

            this.GetClientData(cDate, discussion, disgnote);
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);

        }
        private void GetClientData(string cDate, string discussion, string disgnote)
        {
            string comcod = this.Getcomcod();
            this.lbldsi.InnerText = "Discussion:";
            this.lbldiscussion.Text = discussion;
            this.lblheader.InnerText = "Add New Comments on discussion for " + Convert.ToDateTime(cDate).ToString("dd-MMM-yyyy hh:mm tt");
            this.lblEmpid.Text = this.ddlEmpid.SelectedValue.ToString();
            this.lblclient.Text = this.ddlClient.SelectedValue.ToString();
            this.lbldate.Text = cDate;
            if (disgnote.Length != 0)
                this.txtComm.Text = disgnote;
            //DataSet ds2 = KpiData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "SHOWPRECOMM", mobile, email, "", "", "", "", "", "", "");


        }
        protected void lnkAdddissub_Click(object sender, EventArgs e)
        {
            //this.txtComm.Text = "";
            //int index = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            //string cDate = ((Label)this.gvclient.Rows[index].FindControl("lblgvDate")).Text.ToString().Trim();

            //string lgvndissub = ((Label)this.gvclient.Rows[index].FindControl("lgvndissub")).Text.ToString().Trim();
            //string subgnote = ((Label)this.gvclient.Rows[index].FindControl("lblgvsubgnote")).Text.ToString().Trim();

            //this.GetClientData2(cDate, lgvndissub, subgnote);
            //  ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);

        }
        protected void gvclient_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                int index = e.Row.RowIndex;
                //e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#6EB6C2'");
                //e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");
                //e.Row.Attributes.Add("style", "cursor:pointer;");

                //Label Lbtn = (Label)e.Row.FindControl("lgvDiscussion0");
                Panel Lbtn = (Panel)e.Row.FindControl("pnldis");
                Lbtn.Attributes.Add("onmouseover", "AddButton(" + index + ")");
                Lbtn.Attributes.Add("onmouseout", "HiddenButton(" + index + ")");
                //Lbtn.Attributes.Add("style", "cursor:pointer;");



                LinkButton Lbtn1 = (LinkButton)e.Row.FindControl("lnkAdddis");
                Lbtn1.Attributes.Add("class", "hiddenb" + index);
                Lbtn1.Attributes.Add("style", "display:none;");



                Panel pnlsub = (Panel)e.Row.FindControl("pnlsub");
                pnlsub.Attributes.Add("onmouseover", "AddButtonsub(" + index + ")");
                pnlsub.Attributes.Add("onmouseout", "HiddenButtonsub(" + index + ")");
                //Lbtn.Attributes.Add("style", "cursor:pointer;");

                LinkButton Lbtnsub = (LinkButton)e.Row.FindControl("lnkAdddissub");
                Lbtnsub.Attributes.Add("class", "hiddensub" + index);
                Lbtnsub.Attributes.Add("style", "display:none;");






            }
        }

        protected void lUpdatInfo_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

            string comcod = this.Getcomcod();
            string empid = this.lblEmpid.Text;
            string Client = this.lblclient.Text;
            string cdate = Convert.ToDateTime(this.lbldate.Text).ToString("dd-MMM-yyyy HH:mm:ss");
            string Gvalue = this.lbldiscussion.Text;
            string Comments = this.txtComm.Text;
            string gcod = "";
            string Type = this.lbldsi.InnerText;

            if (Type == "Discussion:")
            {

                gcod = "810100101015";
            }
            else
            {
                gcod = "810100101025";

            }
            bool result = KpiData.UpdateTransInfo3(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "UPDATE_COMM", empid, Client, "000000000000", "", "000000000000", cdate, gcod, "T", Gvalue, Comments, userid, Posteddat);

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail";
                return;
            }
            this.txtComm.Text = "";
            this.ShowPreDiscussion();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Client Info";
                string eventdesc = "Update Info";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }


        }
    }
}
