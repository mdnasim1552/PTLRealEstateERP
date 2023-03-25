using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_21_MKT
{
    public partial class CrmClientInfo02 : System.Web.UI.Page
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

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                string comcod = this.GetComeCode();
                this.CompanyTableColumnVisible();

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
                this.GetIRComARefProspect();

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
            }
        }

        private void CompanyTableColumnVisible()
        {

            string comcod = this.GetComeCode();

            switch (comcod)
            {


                case "3348":// Credence .

                    this.gvSummary.Columns[14].Visible = false;
                    this.gvSummary.Columns[13].Visible = false;
                    this.gvSummary.Columns[10].Visible = true;
                    this.gvSummary.Columns[15].Visible = false;
                    this.gvSummary.Columns[16].Visible = false;
                    this.gvSummary.Columns[17].Visible = false;
                    this.gvSummary.Columns[18].Visible = false;
                    this.gvSummary.Columns[19].Visible = false;
                    this.gvSummary.Columns[20].Visible = false;
                    this.gvSummary.Columns[21].Visible = true;
                    this.gvSummary.Columns[22].Visible = false;
                    this.gvSummary.Columns[23].Visible = true;
                    this.gvSummary.Columns[24].Visible = true;

                    //Checkbox Permanent Delete
                    this.divPermntDel.Visible = false;

                    break;

                case "3315"://Assure Builders
                case "3316"://Assure Development
                    this.gvSummary.Columns[6].HeaderText = "Date";
                    this.gvSummary.Columns[8].HeaderText = "Customer's Name";
                    this.gvSummary.Columns[5].Visible = true; // for pid show
                    // this.gvSummary.Columns[8].Visible = false;                
                    this.gvSummary.Columns[11].Visible = false;
                    this.gvSummary.Columns[12].Visible = false;
                    this.gvSummary.Columns[13].Visible = false;
                    this.gvSummary.Columns[23].Visible = false;

                    this.lnkBtnPotentialPros.Visible = false;
                    this.lnkBtnComments.Visible = false;
                    this.lnkBtnDaypassed.Visible = false;
                    this.lnkbtnTODayTask.Visible = false;
                    this.tdaswhtxt.InnerText = "Today Schedules Work";
                    break;



                case "3354"://Edison
                    this.gvSummary.Columns[14].Visible = false;
                    this.gvSummary.Columns[13].Visible = false;
                    this.gvSummary.Columns[10].Visible = true;
                    this.gvSummary.Columns[15].Visible = false;
                    this.gvSummary.Columns[16].Visible = false;
                    this.gvSummary.Columns[17].Visible = false;
                    this.gvSummary.Columns[18].Visible = false;
                    this.gvSummary.Columns[19].Visible = false;
                    this.gvSummary.Columns[20].Visible = false;
                    this.gvSummary.Columns[21].Visible = true;
                    this.gvSummary.Columns[23].Visible = false;
                    this.gvSummary.Columns[27].Visible = true;
                    this.gvSummary.Columns[28].Visible = true;
                    this.gvSummary.Columns[29].Visible = true;
                    break;

                case "3101"://PTL SHOW all Column
                    this.gvSummary.Columns[9].Visible = true;
                    this.gvSummary.Columns[13].Visible = true;
                    this.gvSummary.Columns[14].Visible = true;
                    this.gvSummary.Columns[15].Visible = true;
                    this.gvSummary.Columns[16].Visible = true;
                    this.gvSummary.Columns[17].Visible = true;
                    this.gvSummary.Columns[18].Visible = true;
                    this.gvSummary.Columns[19].Visible = true;
                    this.gvSummary.Columns[20].Visible = true;
                    this.gvSummary.Columns[21].Visible = true;
                    this.gvSummary.Columns[23].Visible = true;
                    this.gvSummary.Columns[27].Visible = true;
                    break;

                case "3367"://Epic

                    this.gvSummary.Columns[13].Visible = false;
                    this.gvSummary.Columns[14].Visible = false;
                    this.gvSummary.Columns[10].Visible = true;
                    this.gvSummary.Columns[15].Visible = false;
                    this.gvSummary.Columns[16].Visible = false;
                    this.gvSummary.Columns[17].Visible = false;
                    this.gvSummary.Columns[18].Visible = false;
                    this.gvSummary.Columns[19].Visible = false;
                    this.gvSummary.Columns[20].Visible = true;
                    this.gvSummary.Columns[21].Visible = true;
                    this.gvSummary.Columns[23].Visible = true;
                    break;

                default:

                    this.gvSummary.Columns[13].Visible = false;
                    this.gvSummary.Columns[14].Visible = false;
                    this.gvSummary.Columns[10].Visible = true;
                    this.gvSummary.Columns[15].Visible = false;
                    this.gvSummary.Columns[16].Visible = false;
                    this.gvSummary.Columns[17].Visible = false;
                    this.gvSummary.Columns[18].Visible = false;
                    this.gvSummary.Columns[19].Visible = false;
                    this.gvSummary.Columns[20].Visible = true;
                    this.gvSummary.Columns[21].Visible = false;
                    this.gvSummary.Columns[23].Visible = true;
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

            //this.ddlCountry_SelectedIndexChanged(null, null);




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
                 Pri, Status, Other, TxtVal, todate, srchempid, "");
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
            //this.lbtPending.Text = "Pending:" + ((dv.ToTable().Rows.Count == 0) ? "" : dv.ToTable().Rows.Count.ToString());
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
            string comcod = this.GetComeCode();
            string empid = (hst["empid"].ToString() == "" ? "93" : hst["empid"].ToString()) + "%";
            string pcomcod = "";
            DropDownList ddlcomp, ddlgval, ddlUnit, ddlVisitor, ddlgval1, ddlgval2, ddlgval3;
            ListBox ddlPartic, ddlProject;
            DataRow dr1;
            int j;
            int k = 0;
            string data = "";
            int count;
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
                        //count = Convert.ToInt32(dt.Rows[i]["gdesc1"].ToString().Count());                     


                        //for (j = 0; j < count / 12; j++)
                        //{
                        //    data = dt.Rows[i]["gdesc1"].ToString().Substring(k, 12);
                        //    foreach (ListItem item in lstProject.Items)
                        //    {
                        //        if (item.Value == data)
                        //        {
                        //            item.Selected = true;
                        //        }

                        //    }
                        //    k = k + 12;
                        //}





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
                            if ((rows[0]["gcod"].ToString() == "9501020") || (rows[0]["gcod"].ToString() == "9501028"))
                            {
                                holdLost = "hold_lost";//rows[0]["gcod"].ToString();
                            }
                        }
                        if (holdLost != "hold_lost")// if hold or lost then lead status enable req by emadad bhai and raihan
                        {
                            index = index - 1;
                            for (int p = 0; p < index; p++)
                            {



                                if (p < index)
                                {
                                    ChkBoxLstStatus.Items[p].Enabled = false;
                                }


                                //if (lstleadstatus == "9501002" && p == 6 && comcod == "3101"  && comcod == "3354")
                                //{
                                //    ChkBoxLstStatus.Items[p].Enabled = false;
                                //}
                            }
                        }


                        //


                        //Lost, Hold & Close Enabled
                        switch (comcod)
                        {
                            //   case "3354"://Edison
                            case "3101"://PTL
                                //Clost Inactive only Query
                                if (lstleadstatus == "9501002") //Query
                                {
                                    if (dts.Select("gcod='9501035'").Length > 0)//Close
                                    {
                                        foreach (ListItem chkboxstatus in ChkBoxLstStatus.Items)
                                        {
                                            string statuscode = chkboxstatus.Value;


                                            if (statuscode == "9501035")
                                            {

                                                chkboxstatus.Enabled = false;
                                                break;

                                            }


                                        }
                                    }


                                    if (dts.Select("gcod='9501020'").Length > 0)//Hold
                                    {



                                        foreach (ListItem chkboxstatus in ChkBoxLstStatus.Items)
                                        {
                                            string statuscode = chkboxstatus.Value;


                                            if (statuscode == "9501020")
                                            {

                                                chkboxstatus.Enabled = false;
                                                break;

                                            }


                                        }
                                    }



                                }

                                else if (lstleadstatus.Length > 0)  //hold or Lost Activef Lead or Upword
                                {



                                    if (dts.Select("gcod='9501028'").Length > 0)//Lost
                                    {



                                        foreach (ListItem chkboxstatus in ChkBoxLstStatus.Items)
                                        {
                                            string statuscode = chkboxstatus.Value;


                                            if (statuscode == "9501028")
                                            {

                                                chkboxstatus.Enabled = false;
                                                break;

                                            }


                                        }
                                    }







                                }

                                //Not Skipping                                   

                                index = rows.Length == 0 ? 0 : (Convert.ToInt32(rows[0]["rowid"]) - 1);
                                int chkstatus = 0;

                                for (int st = index + 1; st < ChkBoxLstStatus.Items.Count; st++)
                                {

                                    string statuscode = ChkBoxLstStatus.Items[st].Value;
                                    if (statuscode == "9501020" || statuscode == "9501028" || statuscode == "9501035")

                                        continue;
                                    else
                                    {
                                        if (chkstatus == 0)
                                        {
                                            ChkBoxLstStatus.Items[st].Enabled = true;
                                            chkstatus++;
                                        }
                                        else
                                            ChkBoxLstStatus.Items[st].Enabled = false;

                                    }






                                }


                                break;

                            default:
                                break;



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


                        count = Convert.ToInt32(dt.Rows[i]["gdesc1"].ToString().Count());
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
    }
}