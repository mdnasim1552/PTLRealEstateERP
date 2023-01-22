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
namespace RealERPWEB.F_01_LPA
{
    public partial class MktLandOwnerDiscus : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        private UserManagerKPI objUser = new UserManagerKPI();
        ProcessAccess KpiData = new ProcessAccess();
        public static string TString = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");

                //((Label)this.Master.FindControl("lblTitle")).Text = "Daily Job Execution";
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((DropDownList)this.Master.FindControl("DDPrintOpt")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkPrint")).Visible = false;
                this.txtFrom.Text = (Request.QueryString["nfollow"].Length != 0) ? ((this.Request.QueryString["Type"] == "Edit") ?
                    Convert.ToDateTime(ASTUtility.Left(this.Request.QueryString["nfollow"], 11)).ToString("dd-MMM-yyyy") : System.DateTime.Today.ToString("dd-MMM-yyyy"))
                        : System.DateTime.Today.ToString("dd-MMM-yyyy");

                //this.txtFrom.Text =  System.DateTime.Today.ToString("dd-MMM-yyyy");


                Hashtable hst = (Hashtable)Session["tblLogin"];
                this.lbluseid.Text = (Request.QueryString["Type"] == "Client") ? hst["usrid"].ToString() : "";
                //this.rbtnlist.SelectedValue = "810100601005";//this.Request.QueryString["kpigrp"].ToString();
                this.GetEmpList();

                //this.CommonButton();
            }
        }

        public void CommonButton()
        {
            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace("%20", " "), (DataSet)Session["tblusrlog"]);
            ((Label)this.Master.FindControl("lblmsg")).Visible = false;
            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;


            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;


            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            ((CheckBox)this.Master.FindControl("CheckBox1")).Visible = false;

            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;



        }
        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkprint_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(Modalupdate_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkTotal_Click);
            //((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);

        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }
        private void GetEmpList()
        {
            if (this.lnkok.Text == "New")
                return;
            //-----------Get Person List ---------------//

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = Getcomcod();
            string srchEmp = "%" + "%";
            string userid = hst["usrid"].ToString();//(this.Request.QueryString["Type"] == "Entry") ? hst["usrid"].ToString() : "";
            string deptcode = hst["deptcode"].ToString();
            List<RealEntity.C_47_Kpi.EClassEmpCode> lst3 = new List<RealEntity.C_47_Kpi.EClassEmpCode>();
            lst3 = objUser.GetEmpCode("dbo_kpi.SP_ENTRY_EMP_KPI_SETUP", "GETLANDUSER", srchEmp, userid, deptcode);

            this.ddlEmpid.DataTextField = "empname1";
            this.ddlEmpid.DataValueField = "empid";
            this.ddlEmpid.DataSource = lst3;
            this.ddlEmpid.DataBind();
            this.GetLandownerCode();

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
                if (this.ddlClient.Items.Count == 0)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Land Owner Name Empty";
                    return;
                }
                ViewState.Remove("tblappmnt");
                string comcod = this.Getcomcod();
                this.ddlEmpid.Enabled = false;
                this.ddlClient.Enabled = false;
                //this.ddlClient.Enabled = false;
                this.lnkok.Text = "New";
                this.MultiView1.ActiveViewIndex = 0;
                if (this.Request.QueryString["Type"] == "Edit")
                {
                    this.lbtnEdit_Click(null, null);
                }
                else
                {
                    this.ShowDiscussion();
                }

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
            string kpigrp = "000000000000";
            string wrkdpt = "000000000000";
            DateTime time = System.DateTime.Now;

            string cdate = this.txtFrom.Text + " " + time.ToString("HH:mm");


            DataSet ds1 = KpiData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "DAILYLANDOWNERDISCUS", Empid, Client, kpigrp, "", wrkdpt, cdate);

            //List<RealEntity.C_47_Kpi.EClassLandowner> lst = (List<RealEntity.C_47_Kpi.EClassLandowner>)Session["lstlowner"];
            //var lst1 = lst.FindAll(l => l.ccode == Client);
            //double lsize = lst1[0].lsize;
            //double lamt = lst1[0].lamt;
            //double broamt = lst1[0].broamt;
            //double toamt = lamt + broamt;
            DataTable dt = HiddenSameData(ds1.Tables[0]);
            //(dt.Select("gcod='810100102004'"))[0]["gdesc1"] = lsize.ToString();
            //(dt.Select("gcod='810100102005'"))[0]["gdesc1"] = lamt.ToString();
            //(dt.Select("gcod='810100102007'"))[0]["gdesc1"] = broamt.ToString();
            //(dt.Select("gcod='810100102008'"))[0]["gdesc1"] = toamt.ToString();


            ViewState["tbModalData"] = HiddenSameData(dt);
            this.Modal_Data_Bind();



        }
        private void ShowPreDiscussion()
        {

            try
            {

                ViewState.Remove("tblprediscussion");
                string comcod = this.Getcomcod();
                DataTable tbl1 = (DataTable)ViewState["tbModalData"];
                string YmonID = Convert.ToDateTime(this.txtFrom.Text.Trim()).ToString("yyyyMM");
                string Empid = this.ddlEmpid.SelectedValue.ToString();
                //string grpcode = this.lblgrp.Text;
                string Client = this.ddlClient.SelectedValue.ToString();
                string kpigrp = "000000000000";
                DateTime time = System.DateTime.Now;
                string cdate = this.txtFrom.Text + " " + time.ToString("HH:mm");
                DataSet ds1 = KpiData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "SHOWPRELOWNERDISCUSSION", Empid, Client, cdate, "", "", "");
                this.gvclient.DataSource = ds1.Tables[0];
                this.gvclient.DataBind();
                ViewState["tblprediscussion"] = ds1.Tables[0];
                ds1.Dispose();




                for (int i = 0; i < this.gvclient.Rows.Count; i++)
                {
                    string disgnote = ((Label)gvclient.Rows[i].FindControl("lblgvdisgnote")).Text.Trim();
                    string subgnote = ((Label)gvclient.Rows[i].FindControl("lblgvsubgnote")).Text.Trim();
                    if (disgnote.Length != 0)
                    {
                        this.gvclient.Rows[i].Cells[5].BackColor = System.Drawing.Color.FromName("#6EB6C2");
                        //gvclient.Columns[9].ItemStyle.BackColor = System.Drawing.Color.FromName("#6EB6C2");
                    }
                    if (subgnote.Length != 0)
                    {
                        this.gvclient.Rows[i].Cells[9].BackColor = System.Drawing.Color.FromName("#6EB6C2");
                    }
                }



            }

            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

            }


        }
        private void GetLandownerCode()
        {
            //-----------Get Person List ---------------//
            //Session.Remove("lstlowner");
            UserManagerKPI objUser = new UserManagerKPI();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.Getcomcod();
            string Empid = this.ddlEmpid.SelectedValue.ToString();
            string srchEmp = (string.IsNullOrEmpty(Request.QueryString["clientid"])) ? "%" : this.Request.QueryString["clientid"].ToString(); //"%" + "%";
                                                                                                                                              //List<RealEntity.C_47_Kpi.EClassLandowner> lst3 = new List<RealEntity.C_47_Kpi.EClassLandowner>();
                                                                                                                                              //lst3 = objUser.GetLandowner("dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "GETLANDINFO", srchEmp, Empid);
                                                                                                                                              //string LandOwner = this.Request.QueryString["clientid"].ToString();

            DataSet ds1 = KpiData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "GETLANDINFO", srchEmp, Empid);


            this.ddlClient.DataTextField = "sirdesc";
            this.ddlClient.DataValueField = "sircode";
            this.ddlClient.DataSource = ds1.Tables[0];
            this.ddlClient.DataBind();
            //Session["lstlowner"] = lst3;
            // lst3.Clear();
            // .NET < 4.0
            if (string.IsNullOrEmpty(Request.QueryString["clientid"]))
            {
                this.ddlClient.Enabled = true;

            }
            else
            {
                //string LandOwner = this.Request.QueryString["clientid"].ToString();
                //DataView dv = ds1.Tables[0].Copy().DefaultView;
                //dv.RowFilter = ("sircode='" + LandOwner+"'");
                //if (dv.ToTable().Rows.Count == 0)
                //    return;
                this.ddlClient.SelectedValue = this.Request.QueryString["clientid"].ToString();
                this.ddlClient.Enabled = false;

            }



        }


        protected void ddlEmpid_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetLandownerCode();
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





        private void GetFollow()
        {
            ViewState.Remove("tblFollow");
            string comcod = Getcomcod();
            DataSet dt11 = this.KpiData.GetTransInfo(comcod, "dbo_kpi.SP_REPORT_EMP_DASEBOARD", "FOLLOWUPCODE", "", "", "", "", "", "", "", "", "");
            DataTable dt = dt11.Tables[0];
            ViewState["tblFollow"] = dt;

        }
        //private void GetVisitoraStatinfo()
        //{
        //    ViewState.Remove("tblvisitor");
        //    string comcod = Getcomcod();
        //    DataSet dt11 = this.KpiData.GetTransInfo(comcod, "dbo_kpi.SP_REPORT_EMP_DASEBOARD", "GETVISITOR", "", "", "", "", "", "", "", "", "");
        //    DataTable dt = dt11.Tables[0];

        //    ViewState["tblvisiastator"] = dt;

        //}


        private void GetParcipants()
        {
            ViewState.Remove("tblparti");
            string comcod = Getcomcod();
            DataSet ds1 = this.KpiData.GetTransInfo(comcod, "dbo_kpi.SP_REPORT_EMP_DASEBOARD", "PARTICIPANTS", "", "", "", "", "", "", "", "", "");
            DataTable dt11 = ds1.Tables[0];
            ViewState["tblparti"] = dt11;

        }




        protected void lnkTotal_Click(object sender, EventArgs e)
        {

            //double Oferedprice = Convert.ToDouble("0" + ((TextBox)this.gvInfo.Rows[5].FindControl("txtgvVal")).Text.Trim());
            //double offerbroamt = Convert.ToDouble("0" + ((TextBox)this.gvInfo.Rows[6].FindControl("txtgvVal")).Text.Trim());
            //double oftuamt = Oferedprice + offerbroamt;
            //((TextBox)this.gvInfo.Rows[7].FindControl("txtgvVal")).Text = oftuamt.ToString("#,##0;(#,##0); ");


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



                    if (Gcode == "810100102002")
                    {

                        Gvalue = (((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstFollow")).Items.Count == 0) ? ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Text.Trim()
                            : ((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstFollow")).SelectedValue.ToString();
                    }

                    else if (Gcode == "810100102019")
                    {

                        Gvalue = (((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstFollow")).Items.Count == 0) ? ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Text.Trim()
                            : ((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstFollow")).SelectedValue.ToString();
                    }



                    else if (Gcode == "810100102016")
                    {

                        Gvalue = (((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstStatus")).Items.Count == 0) ? ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Text.Trim()
                            : ((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstStatus")).SelectedValue.ToString();
                    }


                    else if (Gcode == "810100102018")
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




                    else if (Gcode == "810100102001")
                    {

                        //string fdatetime = Convert.ToDateTime((((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim()+ " " + ddlhour+":" + ddlMmin +" "+ ddlslb)).ToString("dd-MMM-yyyy HH:mm:ss");

                        Gvalue = (((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim() == "") ? System.DateTime.Today.ToString("dd-MMM-yyyy")
                            : Convert.ToDateTime((((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim() + " " + ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlhour")).SelectedValue.ToString()
                            + ":" + ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlMmin")).SelectedValue.ToString() + " " + ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlslb")).SelectedValue.ToString())).ToString("dd-MMM-yyyy HH:mm:ss");
                    }
                    else if (Gcode == "810100102020")
                    {
                        string sdsd = ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim();

                        Gvalue = (((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim() == "") ? "1900-01-01 00:00:00"
                           : Convert.ToDateTime((((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim() + " " + ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlhour")).SelectedValue.ToString()
                            + ":" + ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlMmin")).SelectedValue.ToString() + " " + ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlslb")).SelectedValue.ToString())).ToString("dd-MMM-yyyy HH:mm:ss");
                    }
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
                            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail";
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                            return;
                        }
                        Gvalue = "";
                    }


                }

                if (this.Request.QueryString["Type"].ToString() == "Entry" && this.Request.QueryString["nfollow"].Length != 0)
                {
                    string PDisDate = Convert.ToDateTime(ASTUtility.Left(this.Request.QueryString["nfollow"], 11) + " " + ASTUtility.Right(this.Request.QueryString["nfollow"], 5)).ToString("dd-MMM-yyyy HH:mm:ss");

                    bool result2 = KpiData.UpdateTransInfo3(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "UPDATEFOLLOWST", empid, Client, kpigrp, wrkdpt, PDisDate, "810100102020", "Y");


                    if (!result2)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }
                }


                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated";
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
        }

        private void Modal_Data_Bind()
        {

            try
            {
                DataTable dt = (DataTable)ViewState["tbModalData"];
                this.gvInfo.DataSource = dt;
                this.gvInfo.DataBind();


                GetFollow();
                DataTable dt5 = ((DataTable)ViewState["tblFollow"]).Copy(); ;
                DataView dv1;
                dv1 = dt5.DefaultView;
                dv1.RowFilter = ("gcod like '96%'");

                GetParcipants();
                DataTable dt6 = (DataTable)ViewState["tblparti"];

                //GetVisitoraStatinfo();
                DataView dv;
                DataTable dtvs = ((DataTable)ViewState["tblFollow"]).Copy();


                //  Status
                dv = dtvs.DefaultView;
                dv.RowFilter = ("gcod like '95%'");

                //DataTable dts = dv.ToTable();
                //dts.Rows.Add("0000", "0000000", "----Select Status----");
                //dts.DefaultView.Sort = "gcod asc";
                //dts = dts.DefaultView.ToTable();
                //DataView dv1;


                //DataView dv1;

                DropDownList ddlgval1, ddlgval2, ddlgval3;
                ListBox ddlPartic;

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    string Gcode = dt.Rows[i]["gcod"].ToString();

                    switch (Gcode)
                    {



                        case "810100102002":
                        case "810100102019"://Follow

                            ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                            ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                            ((Panel)this.gvInfo.Rows[i].FindControl("pnlParic")).Visible = false;
                            ((Panel)this.gvInfo.Rows[i].FindControl("pnlFollow")).Visible = true;
                            ((Label)this.gvInfo.Rows[i].FindControl("lblgvTime")).Visible = false;
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





                        case "810100102016": //Status

                            ((Panel)this.gvInfo.Rows[i].FindControl("pnlStatus")).Visible = true;
                            ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                            ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                            ((Panel)this.gvInfo.Rows[i].FindControl("pnlParic")).Visible = false;
                            ((Label)this.gvInfo.Rows[i].FindControl("lblgvTime")).Visible = false;
                            ((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstStatus")).Visible = true;

                            CheckBoxList ChkBoxLstStatus = ((CheckBoxList)this.gvInfo.Rows[i].FindControl("ChkBoxLstStatus"));
                            ChkBoxLstStatus.DataTextField = "gdesc";
                            ChkBoxLstStatus.DataValueField = "gcod";
                            ChkBoxLstStatus.DataSource = dv.ToTable();
                            ChkBoxLstStatus.DataBind();
                            ChkBoxLstStatus.SelectedValue = ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                            break;

                        case "810100102018": //PARTICIPANTS  

                            ((Panel)this.gvInfo.Rows[i].FindControl("pnlFollow")).Visible = false;
                            ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                            ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                            ((Panel)this.gvInfo.Rows[i].FindControl("pnlParic")).Visible = true;
                            ((ListBox)this.gvInfo.Rows[i].FindControl("ddlPartic")).Visible = true;
                            ((Label)this.gvInfo.Rows[i].FindControl("lblgvTime")).Visible = false;


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



                        case "810100102015":
                        case "810100102025"://Muliline
                            ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                            ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).TextMode = TextBoxMode.MultiLine;
                            ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Rows = 3;
                            ((Panel)this.gvInfo.Rows[i].FindControl("pnlFollow")).Visible = false;
                            ((Label)this.gvInfo.Rows[i].FindControl("lblgvTime")).Visible = false;

                            //((DropDownList)this.gvInfo.Rows[i].FindControl("ChkBoxLstFollow")).Items.Clear();
                            //((DropDownList)this.gvInfo.Rows[i].FindControl("ChkBoxLstFollow")).Visible = false;

                            TextBox sd = ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal"));
                            sd.Style.Add("background", "#DFF0D8");
                            sd.Style.Add("width", "100%");


                            //((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Height=100;
                            break;

                        case "810100102020": //Date Time
                        case "810100102001": //
                            ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                            ((Panel)this.gvInfo.Rows[i].FindControl("pnlTime")).Visible = true;


                            //string gTime = ((Label)this.gvInfo.Rows[i].FindControl("lblgvTime")).Text.Trim();

                            //ddlgval1 = ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlhour"));
                            //ddlgval1.SelectedValue = ASTUtility.Left(gTime,2);
                            //ddlgval2 = ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlMmin"));
                            //ddlgval2.SelectedValue = gTime.Substring(3,2);
                            //ddlgval3 = ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlslb"));
                            //ddlgval3.SelectedValue = ASTUtility.Right(gTime,2);


                            break;

                        default:
                            ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                            ((ListBox)this.gvInfo.Rows[i].FindControl("ddlPartic")).Items.Clear();
                            ((ListBox)this.gvInfo.Rows[i].FindControl("ddlPartic")).Visible = false;
                            ((Label)this.gvInfo.Rows[i].FindControl("lblgvTime")).Visible = false;

                            break;

                    }

                }
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;

                ((Label)this.Master.FindControl("lblmsg")).Text = ex.ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);


            }
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


            DataSet ds1 = KpiData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "DAILYLANDOWNERDISCUS", Empid, Client, kpigrp, "", wrkdpt, cdate);


            DataTable dt = HiddenSameData(ds1.Tables[0]);

            ViewState["tbModalData"] = HiddenSameData(dt);
            this.Modal_Data_Bind();


        }
        protected void lnkAdddis_Click(object sender, EventArgs e)
        {
            this.txtComm.Text = "";
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

                gcod = "810100102015";
            }
            else
            {
                gcod = "810100102025";

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

        protected void lnkAdddissub_Click(object sender, EventArgs e)
        {
            this.txtComm.Text = "";
            int index = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string cDate = ((Label)this.gvclient.Rows[index].FindControl("lblgvDate")).Text.ToString().Trim();

            string lgvndissub = ((Label)this.gvclient.Rows[index].FindControl("lgvndissub")).Text.ToString().Trim();
            string subgnote = ((Label)this.gvclient.Rows[index].FindControl("lblgvsubgnote")).Text.ToString().Trim();

            this.GetClientData2(cDate, lgvndissub, subgnote);
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);

        }
        private void GetClientData2(string cDate, string lgvndissub, string subgnote)
        {


            this.lbldsi.InnerText = "Subject:";
            this.lbldiscussion.Text = lgvndissub;
            this.lblheader.InnerText = "Add New Comments on Subject for " + Convert.ToDateTime(cDate).ToString("dd-MMM-yyyy hh:mm tt");
            this.lblEmpid.Text = this.ddlEmpid.SelectedValue.ToString();
            this.lblclient.Text = this.ddlClient.SelectedValue.ToString();
            this.lbldate.Text = cDate;
            if (subgnote.Length != 0)
                this.txtComm.Text = subgnote;
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

        protected void gvInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtgvdVal = (TextBox)e.Row.FindControl("txtgvdVal");
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "gcod")).ToString();


                if (this.Request.QueryString["Type"].ToString() == "Edit")
                {

                    if (code == "810100102001")
                    {

                        txtgvdVal.Enabled = false;

                    }
                }


            }
        }
    }
}
