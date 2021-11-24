using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_99_Allinterface
{
    public partial class CRMDashboard : System.Web.UI.Page
    {
        ProcessAccess instcrm = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("~/AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = "CRM Dashboard";

                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                GetAllSubdata();
                //this.DataBindStatus();
                GETEMPLOYEEUNDERSUPERVISED();
                ModalDataBind();
                this.GetComponentData();
                ddlEmpid_SelectedIndexChanged(null, null);
            }
        }

        private void GetAllSubdata()
        {
            string comcod = GetComeCode();
            DataSet ds2 = instcrm.GetTransInfo(comcod, "SP_ENTRY_CRM_MODULE", "CLNTREFINFODDL", "", "", "", "", "", "", "", "", "");
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
            this.ddlEmpid.SelectedValue = lempid;

            //dv = dt1.Copy().DefaultView;
            //dv.RowFilter = ("gcod like '52%'");
            //DataTable dtCo = dv.ToTable();
            //dtCo.Rows.Add("0000000", "Choose Country..", "");
            //this.ddlCountry.DataTextField = "gdesc";
            //this.ddlCountry.DataValueField = "gcod";
            //this.ddlCountry.DataSource = dtCo;
            //this.ddlCountry.DataBind();
            //this.ddlCountry.SelectedValue = "5201001";

            //dv = dt1.Copy().DefaultView;
            //dv.RowFilter = ("gcod like '42%'");
            //DataTable dtPr = dv.ToTable();
            //dtPr.Rows.Add("0000000", "Choose Lad Quality..", "");
            //this.ddlPri.DataTextField = "gdesc";
            //this.ddlPri.DataValueField = "gcod";
            //this.ddlPri.DataSource = dtPr;
            //this.ddlPri.DataBind();
            //this.ddlPri.SelectedValue = "0000000";


            //dv = dt1.Copy().DefaultView;
            //dv.RowFilter = ("gcod like '95%'");
            //DataTable dtSta = dv.ToTable();
            //dtSta.Rows.Add("0000000", "Choose Lead Status..");
            //this.ddlStatus.DataTextField = "gdesc";
            //this.ddlStatus.DataValueField = "gcod";
            //this.ddlStatus.DataSource = dtSta;
            //this.ddlStatus.DataBind();
            //this.ddlStatus.SelectedValue = "0000000";

            //this.ddlCountry_SelectedIndexChanged(null, null);

        }
        protected void ddlEmpid_SelectedIndexChanged(object sender, EventArgs e)
        {
            string empid = this.ddlEmpid.SelectedValue.ToString();
            if (empid != "000000000000")
               // this.GetGridSummary();
            GetNotificationByEmployee(empid);
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

        private void GetNotificationByEmployee(string ddlempid)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userrole = hst["userrole"].ToString();
            string comcod = this.GetComeCode();
            //string Empid = (hst["empid"].ToString() == "") ? "%" : hst["empid"].ToString();
            string frmdate = this.txtfodate.Text.ToString();
            string todate = this.txtfodate.Text.ToString();
            string Empid = "";
            if (userrole != "1")
            {
                Empid = hst["empid"].ToString();
            }
            //string empid = (hst["empid"].ToString() == "") ? "%" : hst["empid"].ToString();
            DataSet ds3 = instcrm.GetTransInfo(comcod, "dbo_kpi.SP_REPORT_CRM_INTERFACE", "GETCRMCOMPONENTDATA", "8301%", Empid, ddlempid, todate);
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
            this.lbldwr.InnerText = ds3.Tables[0].Rows[0]["dwr"].ToString();
            //this.lblCall.InnerText = ds3.Tables[0].Rows[0]["call"].ToString();
            //this.lblvisit.InnerText = ds3.Tables[0].Rows[0]["visit"].ToString();
            this.lblprospect.InnerText = ds3.Tables[0].Rows[0]["prospect"].ToString();
            this.lblrating.InnerText = ds3.Tables[0].Rows[0]["rating"].ToString();
            //this.lblFreez.InnerText = ds3.Tables[0].Rows[0]["freezing"].ToString();
            //this.lblDeadProspect.InnerText = ds3.Tables[0].Rows[0]["deadprospect"].ToString();
            this.lblcsigned.InnerText = ds3.Tables[0].Rows[0]["signed"].ToString();

            //this.lblpme.InnerText = ds3.Tables[0].Rows[0]["pme"].ToString();
            //this.lblpmi.InnerText = ds3.Tables[0].Rows[0]["pmi"].ToString();
            this.lblDatablank.InnerText = ds3.Tables[0].Rows[0]["databank"].ToString();


        }

    }
}