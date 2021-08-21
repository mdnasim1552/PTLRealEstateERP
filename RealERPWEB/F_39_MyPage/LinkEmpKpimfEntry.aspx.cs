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
namespace RealERPWEB.F_39_MyPage
{
    public partial class LinkEmpKpimfEntry : System.Web.UI.Page
    {
        private UserManagerKPI objUser = new UserManagerKPI();
        ProcessAccess KpiData = new ProcessAccess();
        public static string TString = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = "Daily Job Execution";
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkPrint")).Visible = false;
                this.txtFrom.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                this.lbluseid.Text = (Request.QueryString["Type"] == "Client") ? hst["usrid"].ToString() : "";
                this.rbtnlist.SelectedValue = this.Request.QueryString["kpigrp"].ToString();
                this.GetEmpList();
                this.GetClientCode();

            }
        }



        private void GetEmpList()
        {
            if (this.lnkok.Text == "New")
                return;
            //-----------Get Person List ---------------//

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = Getcomcod();
            string srchEmp = "%" + this.txtSrchSalesTeam.Text.Trim() + "%";
            string userid = (this.Request.QueryString["Type"] == "Client") ? hst["usrid"].ToString() : "";
            string deptcode = hst["deptcode"].ToString();
            List<RealEntity.C_47_Kpi.EClassEmpCode> lst3 = new List<RealEntity.C_47_Kpi.EClassEmpCode>();
            lst3 = objUser.GetEmpCode("dbo_kpi.SP_ENTRY_EMP_KPI_SETUP", "GETENTRYEMPID", srchEmp, userid, deptcode);

            this.ddlEmpid.DataTextField = "empname";
            this.ddlEmpid.DataValueField = "empid";
            this.ddlEmpid.DataSource = lst3;
            this.ddlEmpid.DataBind();
            this.ddlEmpid.SelectedValue = this.Request.QueryString["empid"].ToString();
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
                // this.ddlClient.Enabled = false;
                this.lnkok.Text = "New";
                this.MultiView1.ActiveViewIndex = 0;
                this.rbtnlist.Enabled = false;
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
                this.rbtnlist.Enabled = true;
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
            string Client = this.ddlEmpid.SelectedValue.ToString();
            string kpigrp = this.rbtnlist.SelectedValue.ToString();
            string wrkdpt = "000000000000";
            string cdate = this.txtFrom.Text;
            DataSet ds1 = KpiData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "DAILYDISCUIND", Empid, Client, kpigrp, "", wrkdpt, cdate);

            ViewState["tbModalData"] = HiddenSameData(ds1.Tables[0]);
            this.Modal_Data_Bind();



        }
        private void ShowPreDiscussion()
        {

            string comcod = this.Getcomcod();
            DataTable tbl1 = (DataTable)ViewState["tbModalData"];
            string YmonID = Convert.ToDateTime(this.txtFrom.Text.Trim()).ToString("yyyyMM");
            string Empid = this.ddlEmpid.SelectedValue.ToString();
            //string grpcode = this.lblgrp.Text;
            string Client = this.ddlClient.SelectedValue.ToString();
            string kpigrp = this.rbtnlist.SelectedValue.ToString();

            string cdate = this.txtFrom.Text;
            DataSet ds1 = KpiData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "SHOWPREMFDISCUSSION", Empid, Client, cdate, "", "", "");

            this.gvclient.DataSource = ds1.Tables[0];
            this.gvclient.DataBind();


        }
        private void GetClientCode()
        {
            //-----------Get Person List ---------------//
            UserManagerKPI objUser = new UserManagerKPI();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.Getcomcod();
            string Empid = this.ddlEmpid.SelectedValue.ToString();
            string srchEmp = "%" + this.txtClient.Text.Trim() + "%";
            List<RealEntity.C_47_Kpi.EClassClientCode> lst3 = new List<RealEntity.C_47_Kpi.EClassClientCode>();
            lst3 = objUser.GetClientCode("dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "SHOWCLIENTGRP", srchEmp, Empid);

            this.ddlClient.DataTextField = "cdesc";
            this.ddlClient.DataValueField = "ccode";
            this.ddlClient.DataSource = lst3;
            this.ddlClient.DataBind();
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
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
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
                }
             ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
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









        private void GetProjectAUnit()
        {
            ViewState.Remove("tblproaunit");
            string comcod = Getcomcod();
            string Empid = this.ddlEmpid.SelectedValue.ToString();
            DataSet dss = this.KpiData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_ENTRY", "GETSALESACUSTOMER", Empid, "", "", "", "", "", "", "", "");
            ViewState["tblproaunit"] = dss;
            dss.Dispose();
        }

        protected void lnkTotal_Click(object sender, EventArgs e)
        {

            //double UnitSize = Convert.ToDouble("0" + ((TextBox)this.gvInfo.Rows[3].FindControl("txtgvVal")).Text.Trim());
            //double Oferedprice = Convert.ToDouble("0" + ((TextBox)this.gvInfo.Rows[8].FindControl("txtgvVal")).Text.Trim());
            //double OferPamt = Convert.ToDouble("0" + ((TextBox)this.gvInfo.Rows[9].FindControl("txtgvVal")).Text.Trim());
            //double Oferothamt = Convert.ToDouble("0" + ((TextBox)this.gvInfo.Rows[10].FindControl("txtgvVal")).Text.Trim());
            //double oftuamt = ((UnitSize * Oferedprice) + OferPamt + Oferothamt);
            //((TextBox)this.gvInfo.Rows[11].FindControl("txtgvVal")).Text = oftuamt.ToString("#,##0;(#,##0); ");


        }
        protected void Modalupdate_Click(object sender, EventArgs e)
        {


            try
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                string comcod = this.Getcomcod();
                string empid = this.ddlEmpid.SelectedValue.ToString();
                string Client = this.ddlClient.SelectedValue.ToString();
                string kpigrp = this.rbtnlist.SelectedValue.ToString();

                string wrkdpt = "000000000000";
                string cdate = this.txtFrom.Text.ToString();
                string Gvalue = "";
                bool result;
                for (int i = 0; i < this.gvInfo.Rows.Count; i++)
                {
                    string Gcode = ((Label)this.gvInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                    string gtype = ((Label)this.gvInfo.Rows[i].FindControl("lgvgval")).Text.Trim();

                    if (Gcode == "810100101002")
                    {

                        Gvalue = (((DropDownList)this.gvInfo.Rows[i].FindControl("ddlProject")).Items.Count == 0) ? ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Text.Trim()
                            : ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlProject")).SelectedValue.ToString();
                    }
                    else if (Gcode == "810100101003")
                    {

                        Gvalue = (((DropDownList)this.gvInfo.Rows[i].FindControl("ddlUnit")).Items.Count == 0) ? ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Text.Trim()
                            : ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlUnit")).SelectedValue.ToString();
                    }
                    else if (Gcode == "810100101020" || Gcode == "810100101001")
                    {

                        Gvalue = (((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim() == "") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim();
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
                        }
                    }



                }

             ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
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
            }
        }

        private void Modal_Data_Bind()
        {

            try
            {
                DataTable dt = (DataTable)ViewState["tbModalData"];
                this.gvInfo.DataSource = dt;
                this.gvInfo.DataBind();

                if ((DataSet)ViewState["tblproaunit"] == null)
                {

                    this.GetProjectAUnit();
                }
                DataSet ds1 = (DataSet)ViewState["tblproaunit"];
                //DataView dv1;

                DropDownList ddlgval, ddlUnit;
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    string Gcode = dt.Rows[i]["gcod"].ToString();

                    switch (Gcode)
                    {
                        case "810100101002": //Pactcode
                            ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                            ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                            ddlgval = ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlProject"));
                            ddlgval.DataTextField = "pactdesc";
                            ddlgval.DataValueField = "pactcode";
                            ddlgval.DataSource = ds1.Tables[0];
                            ddlgval.DataBind();
                            ddlgval.SelectedValue = ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                            break;
                        //case "810100101003": //Unit
                        //    ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        //    ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        //    ((Panel)this.gvInfo.Rows[i].FindControl("PnlProject")).Visible = false;
                        //    string pactcode = ((DropDownList)this.gvInfo.Rows[i - 1].FindControl("ddlProject")).Text.Trim();

                        //    DataTable dt1 = ds1.Tables[1].Copy();
                        //    DataView dv1;
                        //    dv1 = dt1.DefaultView;
                        //    dv1.RowFilter = ("pactcode='000000000000' or pactcode='" + pactcode + "'");
                        //    dt1 = dv1.ToTable();

                        //    ddlUnit = ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlUnit"));
                        //    ddlUnit.DataTextField = "udesc";
                        //    ddlUnit.DataValueField = "usircode";
                        //    ddlUnit.DataSource = dt1;//dv1.ToTable();
                        //    ddlUnit.DataBind();
                        //    //ddlUnit.SelectedValue = usircode;
                        //    ddlUnit.SelectedValue = ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        //    break;


                        case "810100101005": //Muliline
                        case "810100101015": //Muliline
                            ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                            ((Panel)this.gvInfo.Rows[i].FindControl("PnlProject")).Visible = false;
                            ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).TextMode = TextBoxMode.MultiLine;


                            //((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Height=100;
                            break;

                        case "810100101020": //Date Time
                        case "810100101001": //
                            ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                            ((Panel)this.gvInfo.Rows[i].FindControl("PnlProject")).Visible = false;
                            ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlProject")).Items.Clear();
                            ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlProject")).Visible = false;
                            //((TextBox)this.gvInfo.Rows[i].FindControl("txtgvVal")).Height=100;
                            break;

                        default:
                            ((TextBox)this.gvInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                            ((Panel)this.gvInfo.Rows[i].FindControl("PnlProject")).Visible = false;
                            ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlProject")).Items.Clear();
                            ((DropDownList)this.gvInfo.Rows[i].FindControl("ddlProject")).Visible = false;
                            break;

                    }

                }
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;

                ((Label)this.Master.FindControl("lblmsg")).Text = ex.ToString();


            }
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

    }
}
