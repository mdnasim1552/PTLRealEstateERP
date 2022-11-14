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
            DataTable dt = (DataTable)ViewState["tbModalData"];
            this.gvInfo.DataSource = dt;
            this.gvInfo.DataBind();




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