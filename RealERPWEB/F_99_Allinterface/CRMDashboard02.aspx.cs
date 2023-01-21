using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_99_Allinterface
{
    public partial class CRMDashboard02 : System.Web.UI.Page
    {
        ProcessAccess instcrm = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("~/AcceessError.aspx");

                //((Label)this.Master.FindControl("lblTitle")).Text = "CRM Dashboard";
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                //  string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //this.txtfodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");




                DateTime curdate = System.DateTime.Today;
                this.txtfrmdate.Text = Convert.ToDateTime("01-Jan-" + curdate.ToString("yyyy")).ToString("dd-MMM-yyyy");
                this.txttodate.Text = Convert.ToDateTime(this.txtfrmdate.Text).AddYears(1).AddDays(-1).ToString("dd-MMM-yyyy");


                //DateTime curdate = System.DateTime.Today;
                //DateTime frmdate = Convert.ToDateTime("01" + curdate.ToString("dd-MMM-yyyy").Substring(2));
                //DateTime todate = Convert.ToDateTime(frmdate.AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy"));               
                //this.txtfrmdate.Text = frmdate.ToString("dd-MMM-yyyy");
                //this.txttodate.Text = todate.ToString("dd-MMM-yyyy");
                    GetAllSubdata();
                GETEMPLOYEEUNDERSUPERVISED();
                ModalDataBind();
                this.GetComponentData();


                //if (empid != "000000000000")
                // this.GetGridSummary();
               
              //  this.GetNotificationByEmployee();
                //lnkbtnOk_Click(null, null);
            }
        }

        private void GetAllSubdata()
        {
            string comcod = GetComeCode();
            DataSet ds2 = instcrm.GetTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "CLNTREFINFODDL", "", "", "", "", "", "", "", "", "");
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
            if (ds1 == null)
                return;
            ViewState["tblempsup"] = ds1.Tables[0];
            ds1.Dispose();


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
           
            string comcod = this.GetComeCode();
            DataTable dtE = new DataTable();
            dv.RowFilter = ("gcod like '93%'");
            if (userrole == "1")
            {

                dtE = dv.ToTable();
                dtE.Rows.Add("000000000000", "All Employee", "");

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
                    dtE.Rows.Add("000000000000", "All Employee", "");
                // if(dtE.Rows.Count>1)
                //dtE.Rows.Add("000000000000", "Choose Employee..", "");
            }

            this.ddlEmpid.DataTextField = "gdesc";
            this.ddlEmpid.DataValueField = "gcod";
            this.ddlEmpid.DataSource = dtE;
            this.ddlEmpid.DataBind();
            this.ddlEmpid.SelectedValue = (userrole == "1")?"000000000000": lempid;



        }
      

        public string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            

            return (hst["comcod"].ToString());
        }

        public  void GetComponentData()
        {
            string comcod = GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string empid = this.ddlEmpid.SelectedValue.ToString();

            DataSet ds1 = instcrm.GetTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "GETEMPLOYEEUNDERSUPERVISED", empid, "", "", "", "", "", "", "", "");
            ViewState["tblempsup"] = ds1.Tables[0];
            ds1.Dispose();
        }

        private void GetNotificationByEmployee()
        {




            string ddlempid = this.ddlEmpid.SelectedValue.ToString();


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userrole = hst["userrole"].ToString();
            string comcod = this.GetComeCode();
            //string Empid = (hst["empid"].ToString() == "") ? "%" : hst["empid"].ToString();
           // string frmdate = this.txtfodate.Text.ToString();
            string fromdate = this.txtfrmdate.Text.ToString();
            string todate = this.txttodate.Text.ToString();
            string condate = todate;
            string Empid = "";
            if (userrole != "1")
            {
                Empid = hst["empid"].ToString();
            }
            //Empid =((ddlempid == "000000000000") ? "" : ddlempid)+"%";
            ddlempid = (ddlempid == "000000000000" ? "93" : ddlempid)+"%";

            DataSet ds3 = instcrm.GetTransInfo(comcod, "dbo_kpi.SP_REPORT_CRM_INTERFACE02", "REPORTCRMDASHBOARD", "8301%", Empid, fromdate, todate, condate, ddlempid);
           // DataSet ds1 = instcrm.GetTransInfo(comcod, "SP_ENTRY_CRM_MODULE", calltype, empid, cdate, prjcode, professioncode, cdatef, sourch, condate, leadstatus);

            Session["tblNotification"] = ds3;
            bindDataIntoLabel();
            this.ShowGraph();
           // this.EmpMonthlyKPI(ddlempid);
        }
        private void bindDataIntoLabel()
        {
            DataSet ds3 = (DataSet)Session["tblNotification"];
            if (ds3 == null)
            {
                return;
            }
          
            this.lblprospect.InnerText =Convert.ToDouble(ds3.Tables[0].Rows[0]["tprospect"]).ToString("#,##0;(#,##0);");
            this.lblqualifylead.InnerText = Convert.ToDouble(ds3.Tables[0].Rows[0]["qualiflead"]).ToString("#,##0;(#,##0);");
            this.lblnego.InnerText = Convert.ToDouble(ds3.Tables[0].Rows[0]["nego"]).ToString("#,##0;(#,##0);");
            this.lblfinnego.InnerText = Convert.ToDouble(ds3.Tables[0].Rows[0]["finalnego"]).ToString("#,##0;(#,##0);");
            this.lblcsigned.InnerText = Convert.ToDouble(ds3.Tables[0].Rows[0]["win"]).ToString("#,##0;(#,##0);");
            this.lbllost.InnerText = Convert.ToDouble(ds3.Tables[0].Rows[0]["lost"]).ToString("#,##0;(#,##0);");
            this.lblDatablank.InnerText = Convert.ToDouble(ds3.Tables[0].Rows[0]["databank"]).ToString("#,##0;(#,##0);");
            this.lbltosign.InnerText = Convert.ToDouble(ds3.Tables[0].Rows[0]["tosign"]).ToString("#,##0;(#,##0);");
            
            string empId = this.ddlEmpid.SelectedValue.ToString();
            string curDate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
           // this.hyplnkOccasion.NavigateUrl="~/Notification/Occasion?EmpId=" + empId +"&curDate="+curDate;
            //hlink2.NavigateUrl = "~/F_12_Inv/PurMRREntry?Type=Entry&prjcode=" + pactcode + "&genno=" + orderno + "&sircode=" + sircode;

        }

       

        private void ShowGraph()
        {

            DataSet ds1 = (DataSet)Session["tblNotification"];


            var jsonSerialiser = new JavaScriptSerializer();

            var lstpwise = ds1.Tables[1].DataTableToList<RealERPEntity.C_21_MKT.EClassCRMDashBoard.EClassPwiseSum>();
            var lstswise = ds1.Tables[2].DataTableToList<RealERPEntity.C_21_MKT.EClassCRMDashBoard.EClassSourceWiseSum>(); ;
            var lstleadwise = ds1.Tables[3].DataTableToList<RealERPEntity.C_21_MKT.EClassCRMDashBoard.EClassLeadWiseSum>(); 



            var data1 = jsonSerialiser.Serialize(lstpwise);
            var data2 = jsonSerialiser.Serialize(lstswise);
            var data3 = jsonSerialiser.Serialize(lstleadwise);
            ScriptManager.RegisterStartupScript(this, GetType(), "chart", "ExecuteGraph('" + data1 + "','" + data2 + "','" + data3 + "')", true);


        }

        [Serializable]
        public class CrmLeadData
        {
            public decimal total { get; set; }
            public decimal call { get; set; }
            public decimal extmeeting { get; set; }
            public decimal intmeeting { get; set; }
            public decimal proposal { get; set; }
            public decimal leads { get; set; }
            public decimal close { get; set; }
            public decimal visit { get; set; }
            public decimal others { get; set; }           
            public string empid { get; set; }
            public string empname { get; set; }
            public decimal tcall { get; set; }
            public decimal textmeeting { get; set; }
            public decimal tintmeeting { get; set; }
            public decimal tproposal { get; set; }
            public decimal tleads { get; set; }
            public decimal tclose { get; set; }
            public decimal tvisit { get; set; }
            public decimal tothers { get; set; }

        }

        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {
           
                // this.GetGridSummary();
                this.GetNotificationByEmployee();
        }

        protected void lbtnOccasion_Click(object sender, EventArgs e)
        {
            string empId = this.ddlEmpid.SelectedValue.ToString();
            string curDate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            string hostname = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/Notification/";
            string currentptah = "Occasion?Type=Report&EmpId=" + empId +"&curDate="+curDate;
            string totalpath = hostname + currentptah;
            ScriptManager.RegisterStartupScript(this, GetType(), "target", "funOccasion('" + totalpath + "');", true);
        }
    }
}